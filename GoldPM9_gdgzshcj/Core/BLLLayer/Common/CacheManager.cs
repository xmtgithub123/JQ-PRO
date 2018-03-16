using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web.Caching;
using System.Reflection;
using DataModel.Models;
namespace Common
{
    public class CacheManager
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }
        /// <summary>
        /// 缓存对象列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> CacheTable<TEntity>(string CacheKey, IEnumerable<TEntity> objObject)
        {
            if (GetCache(CacheKey) == null)
            {
                int CacheTime = 10;//缓存时间10小时
                SetCache(CacheKey, objObject, DateTime.Now.AddHours(CacheTime), TimeSpan.Zero);
            }
            return objObject;
        }
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }
        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void CacheRemove(params string[] cacheKey)
        {
            foreach (string key in cacheKey)
            {
                if (HttpRuntime.Cache[key] == null) continue;
                HttpRuntime.Cache.Remove(key);
            }
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            if (_cache.Count > 0)
            {
                ArrayList al = new ArrayList();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
                foreach (string key in al)
                {
                    _cache.Remove(key);
                }
            }
        }
        /// <summary>
        /// 以列表形式返回已存在的所有缓存
        /// </summary>
        /// <returns></returns>
        public static ArrayList ShowAllCache()
        {
            ArrayList al = new ArrayList();
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            if (_cache.Count > 0)
            {
                IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
                while (CacheEnum.MoveNext())
                {
                    al.Add(CacheEnum.Key);
                }
            }
            return al;
        } 
    }
}
