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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesExch/DesSpecExchList.cshtml")]
    public partial class _Views_DesExch_DesSpecExchList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesExch_DesSpecExchList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(function () {\r\n        DesExchGrid_");

            
            #line 4 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("();\r\n        window.FormExchRefresh_");

            
            #line 5 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                           Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("=DesExchGrid_");

            
            #line 5 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                                                           Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(";//用作回调函数\r\n    })\r\n    function DesExchGrid_");

            
            #line 7 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                     Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("() {\r\n        JQ.datagrid.datagrid({\r\n            JQPrimaryID: \'Id\',//主键的字段\r\n    " +
"        JQID: \"DesSpecExchGrid_");

            
            #line 10 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                              Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\",//数据表格ID\r\n            JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,datagrid]\r\n " +
"           checkOnSelect: false,\r\n            selectOnCheck: false,\r\n           " +
" fit: false,\r\n            queryParams: { taskId: \'");

            
            #line 15 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                               Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral("\' },\r\n            pagination: false,\r\n            collapsible: true,\r\n           " +
" JQMergeCells: { fields: [\'Id\', \'ExchTitle\', \'ExchSpecName\', \'ExchEmpName\', \'Spe" +
"cIdent\', \'EmpIdent\', \'dateIdent\'] },\r\n            ");

WriteLiteral("\r\n            url: \'");

            
            #line 20 "..\..\Views\DesExch\DesSpecExchList.cshtml"
             Write(Url.Action("DesSpecExchInfoList", "DesExch", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?taskId=");

            
            #line 20 "..\..\Views\DesExch\DesSpecExchList.cshtml"
                                                                                            Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            columns: [[\r\n                {\r\n                    title: \'资料名称\'" +
", field: \'ExchTitle\', width: 220, align: \'left\', halign: \'center\',\r\n            " +
"        formatter: function (val, row) {\r\n                        return row.Exc" +
"hTitle;\r\n                    }\r\n                },\r\n                {\r\n         " +
"           title: \'提资专业\', field: \'SpecIdent\', width: 120, align: \'center\', halig" +
"n: \'center\',\r\n                    formatter: function (val, row, index) {\r\n     " +
"                   return row.ExchSpecName;\r\n                    }\r\n            " +
"    },\r\n                {\r\n                    title: \'提资人\', field: \'EmpIdent\', " +
"width: 90, align: \'center\', halign: \'center\',\r\n                    formatter: fu" +
"nction (val, row) {\r\n                        return row.ExchEmpName;\r\n          " +
"          }\r\n                },\r\n                { title: \'收资专业\', field: \'RecSpe" +
"cName\', width: 120, align: \'left\', halign: \'center\' },\r\n                { title:" +
" \'收资人\', field: \'RecEmpName\', width: 90, align: \'left\', halign: \'center\' },\r\n    " +
"            { title: \'接收日期\', field: \'RecTime\', width: 110, align: \'left\', halign" +
": \'center\', formatter: JQ.tools.DateTimeFormatterByT },\r\n                {\r\n    " +
"                title: \'提资期限\', field: \'dateIdent\', width: 100, align: \'center\', " +
"halign: \'center\',\r\n                    formatter: function (val, row) {\r\n       " +
"                 var value = \"\";\r\n                        if (JQ.tools.isNotEmpt" +
"y(row.ExcLastPutTime)) {\r\n                            value = row.ExcLastPutTime" +
".replace(\"T\", \" \").substring(0, 10);\r\n\r\n                            if (row.ExcL" +
"astPutTime.substring(0, 4) <= 1900)\r\n                                value = \"\";" +
"\r\n                        }\r\n                        return value;\r\n            " +
"        }\r\n                }\r\n            ]]\r\n        })\r\n\r\n    }\r\n\r\n</script>\r\n" +
"\r\n<div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteAttribute("id", Tuple.Create(" id=\"", 3096), Tuple.Create("\"", 3138)
, Tuple.Create(Tuple.Create("", 3101), Tuple.Create("DesSpecExchListDiv_", 3101), true)
            
            #line 65 "..\..\Views\DesExch\DesSpecExchList.cshtml"
, Tuple.Create(Tuple.Create("", 3120), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 3120), false)
);

WriteLiteral(" style=\"width:100%;height:100%;\"");

WriteLiteral(">\r\n            <table");

WriteAttribute("id", Tuple.Create(" id=\"", 3192), Tuple.Create("\"", 3231)
, Tuple.Create(Tuple.Create("", 3197), Tuple.Create("DesSpecExchGrid_", 3197), true)
            
            #line 66 "..\..\Views\DesExch\DesSpecExchList.cshtml"
, Tuple.Create(Tuple.Create("", 3213), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 3213), false)
);

WriteLiteral("></table>\r\n                \r\n        </div>\r\n    </div>\r\n</div>");

        }
    }
}
#pragma warning restore 1591