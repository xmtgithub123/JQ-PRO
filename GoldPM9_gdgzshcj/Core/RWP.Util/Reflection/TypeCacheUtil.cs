/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace JQ.Util
{
    /// <summary>
    /// 类型缓存助手
    /// </summary>
    public abstract class TypeCacheUtil
    {
        /// <summary>
        /// 缓存查找的类型
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<Type>> _typeCache = new ConcurrentDictionary<string, List<Type>>();
        /// <summary>
        /// 从所有程序集中获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheName">缓存名称</param>
        /// <returns></returns>
        public static List<Type> getFilteredTypesFromAssemblies<T>(string cacheName)
        {
            return readTypesFromCache(cacheName, isFilterType<T>, null);
        }
        /// <summary>
        /// 从指定程序集中获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="assemblyNames">指定程序集</param>
        /// <returns></returns>
        public static List<Type> getFilteredTypesFromAssemblies<T>(string cacheName, IEnumerable<string> assemblyNames)
        {
            return readTypesFromCache(cacheName, isFilterType<T>, assemblyNames);
        }
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="predicate">过滤值</param>
        /// <param name="inAssemblies">指定程序集</param>
        /// <returns></returns>
        public static List<Type> readTypesFromCache(string cacheName, Predicate<Type> predicate, IEnumerable<string> assemblyNames)
        {
            List<Type> types;
            if (!_typeCache.TryGetValue("cacheName", out types))
            {
                types = filterTypesInAssemblies(predicate, assemblyNames);
                _typeCache[cacheName] = types;
            }
            return types;
        }

        /// <summary>
        /// 过滤类型从程序集中
        /// </summary>
        /// <param name="predicate">过滤值</param>
        /// <param name="inAssemblies">指定程序集</param>
        /// <returns></returns>
        public static List<Type> filterTypesInAssemblies(Predicate<Type> predicate, IEnumerable<string> assemblyNames)
        {
            IEnumerable<Type> typesSoFar = Type.EmptyTypes;
            ICollection assemblies;
            if (assemblyNames.isNotEmpty())
            {
                List<Assembly> assemblyList = new List<Assembly>();
                foreach (var assemblyName in assemblyNames)
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    if (assembly == null)
                    {
                        throw new InvalidOperationException("找不到程序集: " + assemblyName);
                    }
                    assemblyList.Add(assembly);
                }
                assemblies = assemblyList;
            }
            else
            {
                //获取所有程序集合
                assemblies = BuildManager.GetReferencedAssemblies();
            }
            foreach (Assembly assembly in assemblies)
            {
                Type[] typesInAsm;
                try
                {
                    typesInAsm = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    typesInAsm = ex.Types;
                }
                typesSoFar = typesSoFar.Concat(typesInAsm);
            }
            return typesSoFar.Where(type => typeIsPublicClass(type) && predicate(type)).ToList();
        }
        /// <summary>
        /// 类型为公共类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool typeIsPublicClass(Type type)
        {
            return (type != null && type.IsPublic && type.IsClass && !type.IsAbstract);
        }
        /// <summary>
        /// 过滤值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool isFilterType<T>(Type type)
        {
            return
                typeof(T).IsAssignableFrom(type) &&
                type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}
