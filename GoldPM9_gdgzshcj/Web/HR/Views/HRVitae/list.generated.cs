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
    
    #line 4 "..\..\Views\HRVitae\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/HRVitae/list.cshtml")]
    public partial class _Views_HRVitae_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_HRVitae_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\HRVitae\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t var requestUrl = \'");

            
            #line 7 "..\..\Views\HRVitae\list.cshtml"
               Write(Url.Action("json", "HRVitae",new { @area="Hr"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\HRVitae\list.cshtml"
                 Write(Url.Action("add","HRVitae",new {@area="Hr" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\HRVitae\list.cshtml"
                  Write(Url.Action("edit", "HRVitae",new {@area="Hr" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\HRVitae\list.cshtml"
                 Write(Url.Action("del", "HRVitae", new { @area = "Hr" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\',\'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n        " +
"        JQAddUrl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n " +
"               JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键" +
"的字段\r\n                JQID: \'HRVitaeGrid\',//数据表格ID\r\n                JQDialogTitle" +
": \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeigh" +
"t: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid," +
"treegrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n\t\t\t\tJQSearch: {\r\n         " +
"           id: \'JQSearch\',\r\n                    prompt: \'姓名\',\r\n                 " +
"   loadingType: \'datagrid\',//默认值为datagrid可以不传\r\n                },\r\n             " +
"   url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n\t\t\t" +
"\tfitColumns: false,\r\n                nowrap: false,\r\n                toolbar: \'#" +
"HRVitaePanel\',//工具栏的容器ID\r\n                JQExcludeCol:[1,8],\r\n                c" +
"olumns: [[\r\n                    { field: \'ck\', align: \'center\', checkbox: true, " +
"JQIsExclude: true },\r\n\t\t            { title: \'员工\', field: \'EmpName\', width: 100," +
" align: \'center\', sortable: true  },\r\n\t\t            { title: \'原单位\', field: \'OldC" +
"om\', width: 100, align: \'center\', sortable: true  },\r\n\t\t            { title: \'原部" +
"门\', field: \'OldDep\', width: 100, align: \'center\', sortable: true  },\r\n\t\t        " +
"    { title: \'原职务\', field: \'OldPost\', width: 100, align: \'center\', sortable: tru" +
"e  },\r\n\t\t            { title: \'开始日期\', field: \'StarDate\', width: 100, align: \'cen" +
"ter\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t          " +
"  { title: \'结束日期\', field: \'EndDate\', width: 100, align: \'center\', sortable: true" +
", formatter: JQ.tools.DateTimeFormatterByT },\r\n                    {\r\n          " +
"              field: \'JQFiles\', title: \'附件\', align: \'center\', width: \"5%\", JQIsE" +
"xclude: true, JQExcludeFlag: \"grid_files\", JQRefTable: \'HRVitae\', formatter:\r\n  " +
"                        function (value, row) {\r\n                              r" +
"eturn \"<span id=\\\"grid_files_\" + row.Id + \"\\\"></span>\"\r\n                        " +
"  }\r\n                    }\r\n                ]]\r\n            });\r\n            $(\"" +
"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n                button" +
"Icon: \'fa fa-search\',\r\n                height: 25,\r\n                prompt: \'姓名\'" +
",\r\n                queryOptions: { \'Key\': \'EmpName\', \'Contract\': \'like\' },\r\n    " +
"            onClickButton: function () {\r\n                    JQ.datagrid.search" +
"Grid(\r\n                        {\r\n                            dgID: \'HRVitaeGrid" +
"\',\r\n                            loadingType: \'datagrid\',\r\n                      " +
"      queryParams: { StarDateEnd: $(\"#endDate\").datebox(\"getValue\"), EndDateEnd:" +
" $(\"#endDate1\").datebox(\"getValue\") }\r\n                        });\r\n            " +
"    }\r\n            });\r\n        });\r\n        function openDetailDialog(rowID) {\r" +
"\n            JQ.dialog.dialog({\r\n                title: \"修改\",\r\n                u" +
"rl: \'");

            
            #line 70 "..\..\Views\HRVitae\list.cshtml"
                 Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\' + \'?Id=\' + rowID,\r\n                width: \'1024\',\r\n                height: \'100" +
"%\',\r\n                JQID: \'HRVitaeGrid\',\r\n                JQLoadingType: \'datag" +
"rid\',\r\n                iconCls: \'fa fa-plus\'\r\n            });\r\n        }\r\n    </" +
"script>\r\n");

WriteLiteral("    ");

            
            #line 79 "..\..\Views\HRVitae\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"HRVitaeGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"HRVitaePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            \r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral("  JQWhereOptions=\"{ \'Key\': \'EmpName\', \'Contract\': \'like\' }\"");

WriteLiteral("/>\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'StarDateBegin\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endDate\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始日期\'\"");

WriteLiteral(">&nbsp;\r\n            <input");

WriteLiteral(" id=\"startTime1\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'EndDateBegin\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endDate1\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束日期\'\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
