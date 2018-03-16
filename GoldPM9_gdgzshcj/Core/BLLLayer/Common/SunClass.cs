using JQ.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Common
{
    /// <summary>
    /// 一些常用的查询
    /// </summary>
    public class SunClass
    {
        /// <summary>
        /// 查询页面提交的参数值
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static string GetRequestVal(string queryInfo, string queryStr)
        {
            string res = string.Empty;
            List<Common.Data.Extenstions.Filter> Filter = JavaScriptSerializerUtil.objectToEntity<List<Common.Data.Extenstions.Filter>>(queryInfo);
            if (queryInfo.isNotEmpty())
            {
                foreach (Common.Data.Extenstions.Filter item in Filter)
                {
                    Common.Data.Extenstions.FilterChilde _child = item.list[0];
                    if (_child.Key == queryStr)
                    {
                        res = _child.Value;
                        break;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 生成DropDownList
        /// </summary>
        /// <param name="isAll">是否有全部选项</param>
        /// <param name="dt">数据源</param>
        /// <param name="name">select控件属性name</param>
        /// <param name="val">option值</param>
        /// <param name="text">option文本</param>
        /// <param name="defaultVal">默认值</param>
        /// <returns></returns>
        public MvcHtmlString CreatDropDownList(bool isAll, DataTable dt, string name, string val, string text, string defaultVal)
        {
            string html = string.Empty;
            if (dt.Rows.Count > 0)
            {
                html += string.Format("<select name=\"{0}\">", name);
                //全部选项
                if (isAll)
                {
                    html += string.Format("<option value = \"0\">全部</option>");
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[val].ToString() == defaultVal)
                    {
                        html += string.Format("<option selected=\"selected\" value = \"{0}\">{1}</option>", dr[val].ToString(), dr[text].ToString());
                    }
                    else
                    {
                        html += string.Format("<option value = \"{0}\">{1}</option>", dr[val].ToString(), dr[text].ToString());
                    }
                }
                html += "</select>";
            }
            return MvcHtmlString.Create(html);
        }
        /// <summary>
        /// 转换日期
        /// </summary>
        /// <param name="input"></param>
        /// <param name="deft"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string input, DateTime deft)
        {
            DateTime res = deft;
            try
            {
                res = Convert.ToDateTime(input);
                if (res < Convert.ToDateTime("1900-01-01"))
                {
                    res = deft;
                }
                if (res > Convert.ToDateTime("9999-01-01"))
                {
                    res = Convert.ToDateTime("9999-01-01");
                }
            }
            catch (Exception)
            {
                res = deft;
            }
            return res;
        }
    }
}
