﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 4 "..\..\Views\HREmpWinManager\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/HREmpWinManager/list.cshtml")]
    public partial class _Views_HREmpWinManager_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_HREmpWinManager_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\HREmpWinManager\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\HREmpWinManager\list.cshtml"
                     Write(Url.Action("json", "HREmpWinManager",new { @area="HR"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\HREmpWinManager\list.cshtml"
                    Write(Url.Action("add","HREmpWinManager",new {@area="HR" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\HREmpWinManager\list.cshtml"
                     Write(Url.Action("edit", "HREmpWinManager",new {@area="HR" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\HREmpWinManager\list.cshtml"
                    Write(Url.Action("del", "HREmpWinManager", new { @area = "HR" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n        " +
"        JQID: \'HREmpWinManagerGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息" +
"\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'1" +
"00%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treeg" +
"rid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                JQSearch: {\r\n  " +
"                  id: \'JQSearch\',\r\n                    prompt: \'筛选字段\',\r\n        " +
"            loadingType: \'datagrid\',//默认值为datagrid可以不传\r\n                },\r\n    " +
"            url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含c" +
"heck\r\n                fitColumns: false,\r\n                nowrap: false,\r\n      " +
"          JQExcludeCol:[1,5],\r\n                toolbar: \'#HREmpWinManagerPanel\'," +
"//工具栏的容器ID\r\n                columns: [[\r\n                    { field: \'ck\', alig" +
"n: \'center\', checkbox: true, JQIsExclude: true },\r\n                    { title: " +
"\'奖项名称\', field: \'AptitudeName\', width: 100, align: \'center\', sortable: true },\r\n " +
"                   { title: \'获奖时间\', field: \'RegisterTime\', width: 100, align: \'c" +
"enter\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n           " +
"         { title: \'获奖人员\', field: \'AptitudeEmpName\', width: 100, align: \'center\'," +
" sortable: true },\r\n                    { field: \'JQFiles\', title: \'附件\', align: " +
"\'center\', width: 80, JQIsExclude: true, JQRefTable: \'HREmpWinManager\' }\r\n       " +
"         ]]\r\n            });\r\n        });\r\n    </script>\r\n\r\n");

WriteLiteral("    ");

            
            #line 46 "..\..\Views\HREmpWinManager\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"HREmpWinManagerGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"HREmpWinManagerPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'AptitudeName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
