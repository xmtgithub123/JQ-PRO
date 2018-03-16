using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
/********************************************************************
*           CLR Version:     v4.0        
*           Created by 吴俊鹏 at 2010/5/1
*           175417739@qq.com                   
********************************************************************/
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Xml;

namespace JQ.Util
{
    /// <summary>
    /// JavaScriptSerializer Json操作助手
    /// </summary>
    public abstract class JavaScriptSerializerUtil
    {
        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string getJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
            //JavaScriptSerializer jserializer = new JavaScriptSerializer();
            //StringBuilder stringBuilder = new StringBuilder();
            //jserializer.Serialize(obj, stringBuilder);
            //return stringBuilder.ToString();
        }

        /// <summary>
        /// 把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="szJson">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T parseFormJson<T>(string szJson)
        {
            return JsonConvert.DeserializeObject<T>(szJson);
            //JsonSerializer serializer = new JsonSerializer();
            //StringReader sr = new StringReader(szJson);
            //return serializer.Deserialize<T>(new JsonTextReader(sr));
            // List<Hotel_CheckRecord> list444 = o as List<Hotel_CheckRecord>;
            //JavaScriptSerializer jserializer = new JavaScriptSerializer();
            //return jserializer.Deserialize<T>(szJson);
        }

        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T objectToEntity<T>(object obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            string valueJson = TypeHelper.parseString(obj);

            return parseFormJson<T>(valueJson);
        }

        /// <summary>
        /// 将 JSON 格式字符串转换为指定类型的对象。
        /// </summary>
        /// <param name="szJson"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object deserializeObject(string szJson, Type type)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(szJson);
            return serializer.Deserialize(new JsonTextReader(sr), type);
            //JavaScriptSerializer jserializer = new JavaScriptSerializer();
            //return jserializer.Deserialize(szJson, type);
        }

        /// <summary>
        /// xml文本转json文本
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string Xml2Json(string xml)
        {
            if (String.IsNullOrWhiteSpace(xml)) return "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            return Regex.Replace(JsonConvert.SerializeXmlNode(xmldoc.DocumentElement), "(?<=\")(@)(?!.*\":\\s )", string.Empty, RegexOptions.IgnoreCase);
        }

        #region 将json转换为DataTable
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace("*", "|xing|").Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));
            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');
                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');
                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("|xiang|", "*").Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }
        #endregion
    }
}
