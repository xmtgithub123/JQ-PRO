/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using JQ.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace JQ.Web
{
    /// <summary>
    /// 授权助手
    /// </summary>
    public static class AuthHelper
    {
        #region 从程序集中读取动作 缓存
        /// <summary>
        /// 动作缓存
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<MvcActionInfo>> _actionCache = new ConcurrentDictionary<string, List<MvcActionInfo>>();

        /// <summary>
        /// 区域缓存
        /// </summary>
        private static ConcurrentBag<string> _areaCache = new ConcurrentBag<string>();

        /// <summary>
        /// 读取区域
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> readAreasFromCache()
        {
            List<string> areas = new List<string>();
            if (_areaCache.isEmpty())
            {
                var areaNames = RouteTable.Routes.OfType<Route>()
                    .Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area"))
                    .Select(r => TypeHelper.parseString(r.DataTokens["area"]));
                if (areaNames.isNotEmpty())
                {
                    _areaCache = new ConcurrentBag<string>(areaNames.Distinct());  //区域名去重
                }
            }
            if (_areaCache.isNotEmpty())
            {
                areas.AddRange(_areaCache);
            }
            return areas;
        }

        /// <summary>
        /// 读取控制器
        /// </summary>
        /// <param name="baseName">基类名称</param>
        /// <param name="assemblyNames">指定程序集</param>
        /// <returns></returns>
        public static List<MvcActionInfo> readControllersFromCache(string baseName, IEnumerable<string> assemblyNames)
        {
            string cacheName = baseName;
            if (assemblyNames.isNotEmpty())
            {
                cacheName += string.Join(",", assemblyNames);
            }

            List<MvcActionInfo> actions;
            if (!_actionCache.TryGetValue(cacheName, out actions))
            {
                actions = getFilteredActionsFromAssemblies(baseName, cacheName, assemblyNames);
                _actionCache[baseName] = actions;
            }
            return actions;
        }
        /// <summary>
        /// 从程序集中获取动作
        /// </summary>
        /// <param name="baseName">基类名称</param>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="assemblyNames">指定程序集</param>
        /// <returns></returns>
        private static List<MvcActionInfo> getFilteredActionsFromAssemblies(string baseName, string cacheName, IEnumerable<string> assemblyNames)
        {
            List<MvcActionInfo> mvcActions = new List<MvcActionInfo>();
            //从程序集中获取控制器
            List<Type> AllTypes = TypeCacheUtil.getFilteredTypesFromAssemblies<Controller>(cacheName, assemblyNames);
            if (AllTypes.isNotEmpty())
            {
                //获取继承的子类
                var baseTypes = AllTypes.Where(type => type.BaseType.Name == baseName);
                if (baseTypes.isNotEmpty())
                {
                    foreach (Type controller in baseTypes)
                    {
                        string areaAssemblyName = controller.Assembly.GetName().Name;
                        string descriptname = ReflectHelper.getDescription(controller);
                        MethodInfo[] members = controller.GetMethods();
                        IEnumerable<MethodInfo> actions = members.Where(m => m.ReturnType.Name == "ActionResult");
                        IEnumerable<MvcActionInfo> _children = null;
                        if (actions.isNotEmpty())
                        {
                            //将动作MethodInfo转为MvcActionInfo
                            _children = actions.Select(m => new MvcActionInfo
                            {
                                area = areaAssemblyName,
                                display = ReflectHelper.getDescription(m),
                                controllerAction = string.Format("{0}-{1}", controller.Name, m.Name),
                                typeOf = TypeOf.action
                            }).ToList();
                        }
                        mvcActions.Add(new MvcActionInfo
                        {
                            area = areaAssemblyName,
                            display = descriptname,
                            controllerAction = controller.Name,
                            typeOf = TypeOf.controllers,
                            children = _children,
                            state= "closed"
                        });
                    }
                }
            }
            return mvcActions;
        }
        #endregion

        #region 程序集集合 缓存
        /// <summary>
        /// 动作说明 缓存后缀名
        /// </summary>
        private const string _displayname = "_Display";
        /// <summary>
        /// 读取说明
        /// </summary>
        /// <returns></returns>
        public static List<MvcActionInfo> readDisplayFromCache(string baseName, IEnumerable<string> assemblyNames)
        {
            string cacheName = baseName;
            if (assemblyNames.isNotEmpty())
            {
                cacheName += string.Join(",", assemblyNames);
            }
            cacheName += _displayname;
            List<MvcActionInfo> display;
            if (!_actionCache.TryGetValue(cacheName, out display))
            {
                display = getDisplayFromAssemblies(readControllersFromCache(baseName, assemblyNames)).ToList();
                _actionCache[_displayname] = display;
            }
            return display;
        }
        /// <summary>
        /// 从程序集中获取说明
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<MvcActionInfo> getDisplayFromAssemblies(List<MvcActionInfo> actions)
        {
            foreach (var item in actions)
            {
                yield return new MvcActionInfo
                {
                    area = item.area,
                    display = item.display,
                    controllerAction = item.controllerAction,
                    typeOf = item.typeOf
                };
                if (item.children != null && item.children.Count() > 0)
                {
                    foreach (var children in item.children)
                    {
                        dynamic _children = children;
                        yield return new MvcActionInfo
                        {
                            area = _children.area,
                            display = _children.display,
                            controllerAction = _children.controllerAction,
                            typeOf = _children.typeOf
                        };
                    }
                }
            }
        }
        #endregion
    }
}
