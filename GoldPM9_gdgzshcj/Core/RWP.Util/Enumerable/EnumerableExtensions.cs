/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2013/3/5
*           175417739@qq.com                   
********************************************************************/

using System.Collections.Generic;
using System.Linq;

namespace JQ.Util
{
    /// <summary>
    /// 判断序列（或集合）是否包含元素
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 序列（或集合）不包含元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool isEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }
            return !source.Any();
        }
        /// <summary>
        /// 序列（或集合）包含元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool isNotEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return false;
            }
            return source.Any();
        }
    }
}
