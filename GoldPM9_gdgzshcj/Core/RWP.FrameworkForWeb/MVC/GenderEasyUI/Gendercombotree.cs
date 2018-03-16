using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.IO;
using JQ.Util;

namespace JQ.Web
{
    public static class Gendercombotree
    {
        public static MvcHtmlString GetComboTree(this HtmlHelper html, Condition condition)
        {
            if (string.IsNullOrEmpty(condition.Area) || string.IsNullOrEmpty(condition.ControllerName) || string.IsNullOrEmpty(condition.ActionName))
            {
                return MvcHtmlString.Create(string.Format("url参数错误~"));
            }
            if (string.IsNullOrEmpty(condition.controlName))
            {
                return MvcHtmlString.Create(string.Format("请为控件name和id赋值~"));
            }
            StringBuilder sb = new StringBuilder();
            string url = Path.Combine("/" + HttpContext.Current.Request.Url.Segments[1], condition.Area + "/" + condition.ControllerName + "/" + condition.ActionName);
            sb.Append(string.Format("<select id=\"{0}\" name=\"{0}\"  style=\"width:{1};height:28px;\"  JQWhereOptions=\"{2}\"></select>&nbsp;&nbsp;"
              , condition.controlName
              , !string.IsNullOrEmpty(condition.width) ? condition.width : "200px"
              ,condition.queryOptions
              ));
            sb.Append("<script type=\"text/javascript\">"
                                + "$(function(){"
                                         + "$('#" + condition.controlName + "').combotree({"
                                                 + "animate:true,"
                                                 + "cascadeCheck: false,"
                                                 + "multiple:" + condition.isMult.ToString().ToLower() + ","
                                                 + "url: '" + url + "',"
                                                 + "prompt:'请选择" + condition.prompt + "',"
                                                 + "onlyLeafCheck:true,"
                                                + "})"
                                         + "})"
                      + "</script>");
            return MvcHtmlString.Create(sb.ToString());
        }

    }
    public class Condition
    {
        public string Area { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string controlName { get; set; }

        /// <summary>
        /// 搜索条件
        /// </summary>
        public string queryOptions { get; set; }

        /// <summary>
        /// 是否多选 
        /// </summary>
        public bool isMult { get; set; }

        /// <summary>
        /// 控件宽
        /// </summary>
        public string width { get; set; }

        public string prompt { get; set; }
    }
}
