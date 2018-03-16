/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2012/8/9
*           175417739@qq.com                   
********************************************************************/

using System;
using System.ComponentModel;
using System.Reflection;

namespace JQ.Util
{
    /// <summary>
    /// 反射助手
    /// </summary>
    public abstract class ReflectHelper
    {
        //TypeFromAssembly("Oracle.DataAccess.Client.OracleCommand", "Oracle.DataAccess");
        /// <summary>
        /// 反射程序集中的方法
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Type typeFromAssembly(string typeName, string assemblyName)
        {
            try
            {
                // Try to get the type from an already loaded assembly
                Type type = Type.GetType(typeName);

                if (type != null)
                {
                    return type;
                }

                if (assemblyName == null)
                {
                    // No assembly was specified for the type, so just fail
                    string message = "Could not load type " + typeName + ". Possible cause: no assembly name specified.";
                    throw new TypeLoadException(message);
                }

                Assembly assembly = Assembly.Load(assemblyName);

                if (assembly == null)
                {
                    throw new InvalidOperationException("Can't find assembly: " + assemblyName);
                }

                type = assembly.GetType(typeName);

                if (type == null)
                {
                    return null;
                }

                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取说明
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static string getDescription(object member)
        {
            object[] descripts;
            string memberName;
            if (member is Type)
            {
                Type _t = (Type)member;
                descripts = _t.GetCustomAttributes(typeof(DescriptionAttribute), false);
                memberName = _t.Name;
            }
            else
            {
                MethodInfo _m = (MethodInfo)member;
                descripts = _m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                memberName = _m.Name;
            }
            if (descripts.Length > 0)
            {
                return (descripts[0] as DescriptionAttribute).Description;
            }
            else
            {
                return memberName;
            }
        }

        /// <summary>
        /// 返回Description
        /// </summary>
        public static string getDescription(FieldInfo fi)
        {
            DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            return attributes.Length > 0 ? attributes[0].Description : fi.Name;
        }
    }
}
