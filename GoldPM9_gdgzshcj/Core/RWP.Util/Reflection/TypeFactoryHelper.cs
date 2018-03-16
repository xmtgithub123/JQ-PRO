/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2014/11/28
*           175417739@qq.com                   
********************************************************************/

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace JQ.Util
{
    /// <summary>
    /// 类型实例化工厂
    /// </summary>
    public static class TypeFactoryHelper
    {
        /// <summary>
        /// 缓存池
        /// </summary>
        private static readonly ConcurrentDictionary<Tuple<string, string>, Delegate> _typeFactoryCache = new ConcurrentDictionary<Tuple<string, string>, Delegate>();

        /// <summary>
        /// 创建对应类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T createInstance<T>()
        {
            return convertToDerivedType<object, T>(null);
        }

        /// <summary>
        /// 类型转换（基类到子类）
        /// </summary>
        /// <typeparam name="TBase">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="t">源类型实例</param>
        /// <returns></returns>
        public static T convertToDerivedType<TBase, T>(TBase t) where T : TBase
        {
            var factory = readTypeFactoryFromCache(typeof(TBase), typeof(T));
            if (factory != null)
                return (factory as Func<TBase, T>)(t);
            return default(T);
        }

        /// <summary>
        /// 类型转换（子类到基类）
        /// </summary>
        /// <typeparam name="T">源类型</typeparam>
        /// <typeparam name="TBase">目标类型</typeparam>
        /// <param name="t">源类型实例</param>
        /// <returns></returns>
        public static TBase convertToBaseType<T, TBase>(T t) where T : TBase
        {
            var factory = readTypeFactoryFromCache(typeof(T), typeof(TBase));
            if (factory != null)
                return (factory as Func<T, TBase>)(t);
            return default(TBase);
        }

        /// <summary>
        /// 从缓存中读取类型工厂
        /// </summary>
        /// <param name="fromType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        private static Delegate readTypeFactoryFromCache(Type fromType, Type targetType)
        {
            Delegate factory;
            var key = Tuple.Create<string, string>(fromType.FullName, targetType.FullName);
            if (!_typeFactoryCache.TryGetValue(key, out factory))
            {
                factory = getTypeFactory(fromType, targetType);
                if (factory != null)
                {
                    _typeFactoryCache[key] = factory;
                }
            }
            return factory;
        }

        /// <summary>
        /// 获取类型工厂
        /// </summary>
        /// <param name="fromType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        private static Delegate getTypeFactory(Type fromType, Type targetType)
        {
            if (!(fromType.IsAssignableFrom(targetType) || targetType.IsAssignableFrom(fromType)))
                throw new ArgumentException(String.Format("{0} 无法转换成 {1}。", fromType.FullName, targetType.FullName));

            var baseType = fromType.IsAssignableFrom(targetType) ? fromType : targetType;  //获取基类

            var constructor = targetType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
            if (constructor == null)
                throw new ArgumentException(String.Format("类型 {0} 必须包含一个无参数构造函数。", targetType));

            // 创建动态方法
            var dynamicMethod = new DynamicMethod(getDynamicMethodName(fromType, targetType), targetType, new Type[] { fromType }, true);
            var il = dynamicMethod.GetILGenerator();

            il.DeclareLocal(targetType);  //声明局部变量
            il.Emit(OpCodes.Newobj, constructor);

            if (fromType != typeof(object))
            {
                il.Emit(OpCodes.Stloc_0);  //将创建的新实例保存到局部变量中                
                foreach (var property in baseType.GetProperties())
                {
                    il.Emit(OpCodes.Ldloc_0);  //将局部变量加载到堆栈
                    il.Emit(OpCodes.Ldarg_0);   //将源类型实例加载到堆栈
                    il.Emit(OpCodes.Callvirt, property.GetGetMethod());  //获取 源类型实例 对应属性值
                    il.Emit(OpCodes.Callvirt, property.GetSetMethod(true));   //设置 新创建目标类型实例 对应属性值                 
                }
                il.Emit(OpCodes.Ldloc_0);  //将局部变量加载到堆栈              
            }

            il.Emit(OpCodes.Ret);

            return dynamicMethod.CreateDelegate(Expression.GetFuncType(fromType, targetType));
        }

        /// <summary>
        /// 获取动态方法的名称
        /// </summary>
        /// <param name="fromType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns></returns>
        private static string getDynamicMethodName(Type fromType, Type targetType)
        {
            return "rwp_factory_" + targetType.FullName + "|from|" + fromType.FullName;
        }
    }
}
