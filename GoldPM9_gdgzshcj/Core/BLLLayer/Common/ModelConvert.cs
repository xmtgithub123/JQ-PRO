using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using System.Collections;
using Common.Data.Extenstions;
using System.Web.UI;
using System.Web;

using System.Web.UI.WebControls;
using System.Web.SessionState;
using JQ.Util;
using DataModel;
using System.Xml;
using System.Web.Script.Serialization;

namespace Common
{
    public class ModelConvert
    {
        /// <summary>
        /// 数据行转换为指定类型数据，如果数据为null 转换为类型默认值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ConvertToDefaultType<T>(object t)
        {
            T result = default(T);
            if (t != null) result = (T)Convert.ChangeType(t, typeof(T));
            return result;
        }

        /// <summary>
        /// 获取对象的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ConvertToDefault<T>(T t)
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();

            string tempName = "";
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                // 判断此属性是否有Setter
                if (!pi.CanWrite) continue;

                string TypeName = pi.PropertyType.Name;

                object value = null;
                switch (TypeName)
                {
                    case "Boolean":
                        value = false;
                        break;
                    case "String":
                        value = "";
                        break;
                    case "Int16":
                    case "Int32":
                    case "Int64":
                        value = 0;
                        break;
                    case "DateTime":
                        value = new DateTime(1900, 1, 1);
                        break;
                    case "ToDecimal":
                    case "ToDouble":
                        value = 0m;
                        break;
                }

                if (value != DBNull.Value && value != null)
                    pi.SetValue(t, value, null);
            }
            return t;
        }

        /// <summary>
        /// 数据行转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ConvertToDataModel<T>(T t, DataRow dr)
        {
            PropertyInfo[] propertys = t.GetType().GetProperties();

            string tempName = "";
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                //判断列是否存在


                if (!dr.Table.Columns.Contains(tempName)) continue;

                // 判断此属性是否有Setter
                if (!pi.CanWrite) continue;

                object value = dr[tempName];
                if (value != DBNull.Value)
                    pi.SetValue(t, value, null);
            }
            return t;
        }

        /// <summary>
        /// 获取对象的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ConvertToDefault<T>(object t)
        {
            T result = default(T);
            if (typeof(T) == typeof(DateTime))
            {
                result = (T)Convert.ChangeType("1900-1-1", typeof(T));
            } 
            try
            {
                if (t != null) result = (T)Convert.ChangeType(t, typeof(T));
            }
            catch 
            {
                if (t.GetType().ToString()=="DateTime")
                {
                    result = (T)Convert.ChangeType(new DateTime(1900, 1, 1),typeof(T));
                }
            }
            return result;
        }

        /// <summary>
        /// 新增表单时、 将主表的默认字段赋值
        /// LastModificationTime、LastModifierUserId等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="emp"></param>
        public static void MvcDefaultSave<T>(T m, EmpSession emp)
        {
            if (emp == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改人、创建人
                    case "CreatorEmpId":
                    case "LastModifierEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.EmpID));
                        break;
                    //最后修改人名称、创建人名称
                    case "CreatorEmpName":
                    case "LastModifierEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.EmpName));
                        break;
                    //最后修改时间、创建时间
                    case "CreationTime":
                    case "LastModificationTime":
                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //创建人部门
                    case "CreatorDepID":
                    case "CreatorDepId":
                        p.SetValue(m, TypeHelper.parseInt(emp.EmpDepID));
                        break;
                    case "CreatorDepName":
                        p.SetValue(m, TypeHelper.parseString(emp.EmpDepName));
                        break;
                    //最后修改代理人、创建代理人
                    case "AegnEmpId":
                    case "AgenCreatorEmpId":
                    case "AgenLastModifierEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.AgenEmpID));
                        break;
                    //最后修改代理人名称、创建代理人姓名
                    case "AegnEmpName":
                    case "AgenCreatorEmpName":
                    case "AgenLastModifierEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.AgenEmpName));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 修改表单时、 将主表的默认字段赋值
        /// LastModificationTime、LastModifierUserId等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="emp"></param>
        public static void MvcDefaultEdit<T>(T m, EmpSession emp)
        {
            if (emp == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改人
                    case "LastModifierEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.EmpID));
                        break;
                    //最后修改人名称
                    case "LastModifierEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.EmpName));
                        break;
                    //最后修改时间
                    case "LastModificationTime":
                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //最后修改代理人
                    case "AgenLastModifierEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.AgenEmpID));
                        break;
                    //最后修改代理人名称
                    case "AgenLastModifierEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.AgenEmpName));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 表单表单时、 将主表的默认字段赋值
        /// LastModificationTime、LastModifierUserId等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="emp"></param>
        public static void MvcDefaultDel<T>(T m, EmpSession emp)
        {
            if (emp == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改人
                    case "DeleterEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.EmpID));
                        break;
                    //最后修改人名称
                    case "DeleterEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.EmpName));
                        break;
                    //最后修改时间
                    case "DeletionTime":
                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //最后修改代理人
                    case "AgenDeleterEmpId":
                        p.SetValue(m, TypeHelper.parseInt(emp.AgenEmpID));
                        break;
                    //最后修改代理人名称
                    case "AgenDeleterEmpName":
                        p.SetValue(m, TypeHelper.parseString(emp.AgenEmpName));
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 将从另一个实体中获取 属性参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="OldTarget"></param>
        public static void MvcChangeTarget<T>(T m, T OldTarget)
        {
            List<string> _propertyInfos = OldTarget.GetType().GetProperties().Select(p => p.Name.ToLower()).ToList();
            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                if (p.Name == "Id")
                {

                }
                //if(p.CustomAttributes.FirstOrDefault())
                if (_propertyInfos.Contains(p.Name.ToLower()))
                {
                    if (p.Name.ToLower().Contains("fk_"))
                    {
                        continue;
                    }
                    var val = p.GetValue(OldTarget, null);
                    if (p.PropertyType.Equals(typeof(String)))
                    {
                        p.SetValue(m, TypeHelper.parseString(val));
                    }
                    else if (p.PropertyType.Equals(typeof(int)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(int?)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(long)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(long?)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(decimal)))
                    {
                        p.SetValue(m, TypeHelper.parseDecimal(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(System.DateTime)))
                    {
                        p.SetValue(m, TypeHelper.parseDateTime(val, Convert.ToDateTime("1900-01-01")));
                    }
                    else if (p.PropertyType.Equals(typeof(bool)))
                    {
                        p.SetValue(m, TypeParse.parse<bool>(val, false));
                    }
                    else if (p.PropertyType.Equals(typeof(double)))
                    {
                        p.SetValue(m, TypeParse.parse<double>(val, 0));
                    }
                }
            }
        }

        /// <summary>
        /// 将从另一个实体中获取 属性参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="OldTarget"></param>
        public static void MvcChangeTarget<T, T2>(T m, T2 OldTarget)
        {
            List<string> _propertyInfos = OldTarget.GetType().GetProperties().Select(p => p.Name.ToLower()).ToList();
            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                //if(p.CustomAttributes.FirstOrDefault())
                if (_propertyInfos.Contains(p.Name.ToLower()))
                {
                    if (p.Name.ToLower().Contains("fk_"))
                    {
                        continue;
                    }
                    var val = p.GetValue(OldTarget, null);
                    if (p.PropertyType.Equals(typeof(String)))
                    {
                        p.SetValue(m, TypeHelper.parseString(val));
                    }
                    else if (p.PropertyType.Equals(typeof(int)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(int?)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(long)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(long?)))
                    {
                        p.SetValue(m, TypeHelper.parseInt(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(decimal)))
                    {
                        p.SetValue(m, TypeHelper.parseDecimal(val, 0));
                    }
                    else if (p.PropertyType.Equals(typeof(System.DateTime)))
                    {
                        p.SetValue(m, TypeHelper.parseDateTime(val, Convert.ToDateTime("1900-01-01")));
                    }
                    else if (p.PropertyType.Equals(typeof(bool)))
                    {
                        p.SetValue(m, TypeParse.parse<bool>(val, false));
                    }
                    else if (p.PropertyType.Equals(typeof(double)))
                    {
                        p.SetValue(m, TypeParse.parse<double>(val, 0));
                    }
                }
            }
        }

        /// <summary>
        /// xml字符串转化实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="xml"></param>
        public static T XmlToModel<T>(T m, string xml)
        {
            Type t = m.GetType();
            PropertyInfo[] property = t.GetProperties();
            foreach (PropertyInfo p in property)
            {

                string val = Common.XmlConvert.GetXmlValue(xml, p.Name);
                if (p.PropertyType.Equals(typeof(String)))
                {
                    p.SetValue(m, TypeHelper.parseString(val));
                }
                else if (p.PropertyType.Equals(typeof(int)))
                {
                    p.SetValue(m, TypeHelper.parseInt(val, 0));
                }
                else if (p.PropertyType.Equals(typeof(int?)))
                {
                    p.SetValue(m, TypeHelper.parseInt(val, 0));
                }
                else if (p.PropertyType.Equals(typeof(decimal)))
                {
                    p.SetValue(m, TypeHelper.parseDecimal(val, 0));
                }
                else if (p.PropertyType.Equals(typeof(System.DateTime)))
                {
                    p.SetValue(m, TypeHelper.parseDateTime(val, Convert.ToDateTime("1900-01-01")));
                }
                else if (p.PropertyType.Equals(typeof(bool)))
                {
                    p.SetValue(m, TypeParse.parse<bool>(val, false));
                }
                else if (p.PropertyType.Equals(typeof(double)))
                {
                    p.SetValue(m, TypeParse.parse<double>(val, 0));
                }
            }
            return m;
        }

        /// <summary>
        /// 根据Model生成xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string ModelToXml<T>(T m)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes");

            XmlElement rootXml = xdoc.CreateElement("root");
            Type t = m.GetType();
            PropertyInfo[] property = t.GetProperties();
            foreach (PropertyInfo info in property)
            {
                XmlElement ele = xdoc.CreateElement(info.Name);
                ele.InnerText = info.GetValue(m, null) == null ? "" : info.GetValue(m, null).ToString();
                rootXml.AppendChild(ele);

            }
            xdoc.AppendChild(rootXml);
            return xdoc.InnerXml;
        }

        /// <summary>
        /// xml文本转json文本
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string Xml2Json(string xml)
        {
            return JavaScriptSerializerUtil.Xml2Json(xml);
        }

        /// <summary>
        /// json文本转xml文本
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonList2XmlA<T>(string json)
        {

            //XmlDocument xdoc = new XmlDocument();
            ////xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes"));

            //XmlElement rootXml = xdoc.CreateElement("root");
            //XmlAttribute attJson = xdoc.CreateAttribute("json", "xmlns", "http://james.newtonking.com/projects/json");
            //rootXml.Attributes.Append(attJson);

            StringBuilder xdoc = new StringBuilder();
            xdoc.Append("<root xmlns:json=\"http://james.newtonking.com/projects/json\">");

            List <T> list = new JavaScriptSerializer().Deserialize<List<T>>(json);
            foreach(var flowNode in list)
            {
                //XmlElement itemXml = xdoc.CreateElement("item");
                //XmlAttribute attArray = xdoc.CreateAttribute("Array", "json", "true");
                //itemXml.Attributes.Append(attJson);

                xdoc.Append("<item json:Array=\"true\" ");

                Type t = flowNode.GetType();
                PropertyInfo[] property = t.GetProperties();
                foreach (PropertyInfo info in property)
                {
                    //XmlAttribute ele = xdoc.CreateAttribute(info.Name);
                    //ele.Value = info.GetValue(flowNode, null) == null ? "" : info.GetValue(flowNode, null).ToString();
                    //itemXml.Attributes.Append(ele);

                    xdoc.Append(info.Name + "=\"" + (info.GetValue(flowNode, null) == null ? "" : info.GetValue(flowNode, null).ToString()) + "\" ");

                }
                //rootXml.AppendChild(itemXml);

                xdoc.Append("/>");
            }

            //xdoc.AppendChild(rootXml);
            //return xdoc.OuterXml;

            xdoc.Append("</root>");
            return xdoc.ToString();
        }
    }


}
