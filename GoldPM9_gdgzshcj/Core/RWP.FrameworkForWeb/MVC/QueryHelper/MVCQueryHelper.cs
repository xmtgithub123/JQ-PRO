
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JQ.Util;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using DataModel;

namespace JQ.Web
{
    /// <summary>
    /// MVC参数帮助
    /// </summary>
    public static class MVCQueryHelper
    {
        #region MVC为实体赋值 wirte by 吴俊鹏
        /// <summary>
        /// MVC为实体赋值 wirte by 吴俊鹏
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void MvcSetValue<T>(this T m)
        {
            Type t = m.GetType();
            var r = HttpContext.Current.Request;
            foreach (var k in r.Form.AllKeys)
            {              
                PropertyInfo p = t.GetProperties().Where(o => o.Name == k).FirstOrDefault();
                if (p == null || string.IsNullOrEmpty(p.Name))
                    continue;
                string val = r.HttpMethod.ToUpper() == "GET" ? r.QueryString[k] : r[k];
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
                else if (p.PropertyType.Equals(typeof(long)))
                {
                    p.SetValue(m, TypeParse.parse<long>(val, 0));
                }
            }
        }
        #endregion

        /// <summary>
        /// 在一个页面编辑编辑多个Model时，防止多个Id同时绑定一个值,排除不赋值的Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="Keys"></param>
        public static void MvcSetValueExceptKeys<T>(this T m, params string[] Keys)
        {
            Type t = m.GetType();
            var r = HttpContext.Current.Request;
            foreach (var k in r.Form.AllKeys)
            {
                PropertyInfo p = t.GetProperties().Where(o => o.Name == k).FirstOrDefault();
                if (p == null || string.IsNullOrEmpty(p.Name))
                    continue;
                if (Keys != null)
                {
                    if (Keys.Contains(k))
                        continue;
                }
                string val = r.HttpMethod.ToUpper() == "GET" ? r.QueryString[k] : r[k];
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
        }

        /// <summary>
        /// 实体转化xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="xml"></param>
        public static T XmlToModel<T>(this T m,string xml)
        {
            Type t = m.GetType();
            PropertyInfo [] property = t.GetProperties();
            foreach(PropertyInfo p in property)
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
        public static string ModelToXml<T>(this T m)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", Encoding.UTF8.ToString(), "yes");

            XmlElement rootXml = xdoc.CreateElement("root");
            Type t = m.GetType();
            PropertyInfo[] property = t.GetProperties();
            foreach(PropertyInfo info in property)
            {
                XmlElement ele = xdoc.CreateElement(info.Name);
                ele.InnerText = info.GetValue(m,null) == null ? "" : info.GetValue(m, null).ToString();
                rootXml.AppendChild(ele);

            }
            xdoc.AppendChild(rootXml);
            return xdoc.InnerXml;
        }
        /// <summary>
        /// 将从另一个实体中获取 属性参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="OldTarget"></param>
        public static void MvcChangeTarget<T>(this T m, T OldTarget)
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
        /// 新增表单时、 将主表的默认字段赋值
        /// LastModificationTime、LastModifierUserId等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="EmpID"></param>
        public static void MvcDefaultSave<T>(this T m, int EmpID)
        {
            //DataModel.Models.BaseEmployee EmpModel = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(EmpID);//获取部门名称时为empty
            DataModel.Models.AllBaseEmployee EmpModel = new DBSql.Sys.AllBaseEmployee().FirstOrDefault(p=>p.EmpID==EmpID);
            if (EmpModel == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改时间、创建时间
                    case "LastModificationTime":
                    case "CreationTime":
                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //最后修改人、创建人
                    case "LastModifierEmpId":
                    case "CreatorEmpId":
                    case "AgenEmpId":
                        p.SetValue(m, TypeHelper.parseInt(EmpModel.EmpID));
                        break;
                    //最后修改人名称、创建人名称
                    case "LastModifierEmpName":
                    case "CreatorEmpName":
                    case "AgenEmpName":
                        p.SetValue(m, TypeHelper.parseString(EmpModel.EmpName));
                        break;
                    //创建人部门
                    case "CreatorDepId":
                        p.SetValue(m, TypeHelper.parseInt(EmpModel.EmpDepID));
                        break;
                    case "CreatorDepID"://有的数据库表中存的字段是ID大写，导致部门ID为0
                        p.SetValue(m, TypeHelper.parseInt(EmpModel.EmpDepID));
                        break;
                    case "CreatorDepName":
                        //p.SetValue(m, TypeHelper.parseString(EmpModel.EmpDepName));//获取的值为""
                        p.SetValue(m, TypeHelper.parseString(EmpModel.DepartmentName));
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
        /// <param name="EmpID"></param>
        public static void MvcDefaultEdit<T>(this T m, int EmpID)
        {
            DataModel.Models.BaseEmployee EmpModel = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(EmpID);
            if (EmpModel == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改时间、创建时间
                    case "LastModificationTime":

                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //最后修改人、创建人
                    case "LastModifierEmpId":
                        p.SetValue(m, TypeHelper.parseInt(EmpModel.EmpID));
                        break;
                    //最后修改人名称、创建人名称
                    case "LastModifierEmpName":
                        p.SetValue(m, TypeHelper.parseString(EmpModel.EmpName));
                        break;

                }
            }
        }

        public static void MvcDefaultDel<T>(this T m, int EmpID)
        {
            DataModel.Models.BaseEmployee EmpModel = new DBSql.Sys.BaseEmployee().GetBaseEmployeeInfo(EmpID);
            if (EmpModel == null) return;

            Type t = m.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                switch (p.Name)
                {
                    //最后修改时间、创建时间
                    case "DeletionTime":
                        p.SetValue(m, TypeHelper.parseDateTime(System.DateTime.Now));
                        break;
                    //最后修改人、创建人
                    case "DeleterEmpId":
                        p.SetValue(m, TypeHelper.parseInt(EmpModel.EmpID));
                        break;
                    //最后修改人名称、创建人名称
                    case "DeleterEmpName":
                        p.SetValue(m, TypeHelper.parseString(EmpModel.EmpName));
                        break;

                }
            }
        }


        /// <summary>
        /// 新增表单时、 将主表的默认字段赋值
        /// LastModificationTime、LastModifierUserId等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="emp"></param>
        public static void MvcDefaultSave<T>(this T m, EmpSession emp)
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
                    case "CreatorDepId":
                    case "CreatorDepID":
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
                        //p.SetValue(m, TypeHelper.parseString(emp.AgenEmpName));
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
        public static void MvcDefaultEdit<T>(this T m, EmpSession emp)
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
                        //p.SetValue(m, TypeHelper.parseString(emp.AegnEmpName));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 删除表单时、 将主表的默认字段赋值    
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="emp"></param>
        public static void MvcDefaultDel<T>(this T m, EmpSession emp)
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
                        //p.SetValue(m, TypeHelper.parseString(emp.AegnEmpName));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 获取对象属值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            property.SetValue(t, "");

            if (o == null) return string.Empty;

            return o.ToString();
        }
    }
}
