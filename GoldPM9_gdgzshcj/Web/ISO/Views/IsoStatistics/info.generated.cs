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
    
    #line 1 "..\..\Views\IsoStatistics\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoStatistics/info.cshtml")]
    public partial class _Views_IsoStatistics_info_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoStatistics_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 4 "..\..\Views\IsoStatistics\info.cshtml"
                 Write(Url.Action("jsonList", "IsoStatistics", new { @area="ISO"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    $(function () {\r\n        debugger;\r\n\r\n        JQ.datagrid.datagrid({\r\n   " +
"         JQButtonTypes: [\'export\'],//需要显示的按钮\r\n            JQPrimaryID: \'Id\',//主键" +
"的字段\r\n            JQID: \'ISOCheckStatisticsInfoGrid\',//数据表格ID\r\n            JQDial" +
"ogTitle: \'信息\',//弹出窗体标题\r\n            JQWidth: \'1024\',//弹出窗体宽\r\n            JQHeigh" +
"t: \'100%\',//弹出窗体高\r\n            JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,tree" +
"grid]\r\n            JQFields: [\"Id\"],//追加的字段名\r\n            url: requestUrl,//请求的地" +
"址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#ISOCheckI" +
"nfoPanel\',//工具栏的容器ID\r\n            JQExcludeCol: [1, 9],\r\n            JQIsSearch:" +
" true,//是否加载数据之前就获取搜索参数值，可在搜索控件中添加默认值(默认为false)\r\n            columns: [[\r\n      " +
"        { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n  " +
"            { title: \'项目编号\', field: \'ProjNumber\', width: \"12%\", halign: \'center\'" +
", align: \'left\', },\r\n              { title: \'项目名称\', field: \'ProjName\', width: \"1" +
"8%\", halign: \'center\', align: \'left\', },\r\n              { title: \'阶段\', field: \'P" +
"haseName\', width: \'6%\', align: \'center\', },\r\n              { title: \'专业\', field:" +
" \'SpecialtyName\', width: \'6%\', align: \'center\', },\r\n              { title: \'任务名称" +
"\', field: \'TaskName\', width: \"16%\", halign: \'center\', align: \'left\', },\r\n       " +
"       { title: \'设计人\', field: \'TaskEmpName\', width: \'6%\', align: \'center\', },\r\n " +
"             { title: \'校审意见\', field: \'CheckNote\', width: \"16%\", halign: \'center\'" +
", align: \'left\', },\r\n              { title: \'提出人\', field: \'CheckEmpIDName\', widt" +
"h: \'6%\', align: \'center\',  },\r\n              {\r\n                  title: \'提出日期\'," +
" field: \'CheckDate\', width: \'10%\', align: \'center\', formatter: function (value,r" +
"ow,index) {\r\n                      row.CheckDate = JQ.tools.DateTimeFormatterByT" +
"(value, row, index);\r\n                      return row.CheckDate;\r\n             " +
"     }\r\n              }\r\n            ]],\r\n            queryParams:\r\n            " +
"    {\r\n                    PhaseID: \"");

            
            #line 41 "..\..\Views\IsoStatistics\info.cshtml"
                         Write(ViewBag.PhaseID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                    SpecialID: \"");

            
            #line 42 "..\..\Views\IsoStatistics\info.cshtml"
                           Write(ViewBag.SpecialID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                    CheckType: \"");

            
            #line 43 "..\..\Views\IsoStatistics\info.cshtml"
                           Write(ViewBag.CheckType);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                    IsoCheckModelId: \"");

            
            #line 44 "..\..\Views\IsoStatistics\info.cshtml"
                                 Write(ViewBag.IsoCheckModelId);

            
            #line default
            #line hidden
WriteLiteral(@"""
                }
        });
        $(""#JQSearch"").textbox({
            buttonText: '筛选',
            buttonIcon: 'fa fa-search',
            height: 25,
            prompt: '项目名称、任务名称',
            onClickButton: function () {
                JQ.datagrid.searchGrid({
                    dgID: 'ISOCheckStatisticsInfoGrid',
                    loadingType: 'datagrid',
                    queryParams: {
                        PhaseID: """);

            
            #line 57 "..\..\Views\IsoStatistics\info.cshtml"
                             Write(ViewBag.PhaseID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                        SpecialID: \"");

            
            #line 58 "..\..\Views\IsoStatistics\info.cshtml"
                               Write(ViewBag.SpecialID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                        CheckType: \"");

            
            #line 59 "..\..\Views\IsoStatistics\info.cshtml"
                               Write(ViewBag.CheckType);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                        IsoCheckModelId: \"");

            
            #line 60 "..\..\Views\IsoStatistics\info.cshtml"
                                     Write(ViewBag.IsoCheckModelId);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n                    }\r\n                });\r\n            }\r\n        });\r\n    })" +
";\r\n\r\n    function closedialog() {\r\n        JQ.dialog.dialogClose();\r\n    }\r\n    " +
"//提交前验证\r\n</script>\r\n\r\n\r\n<table");

WriteLiteral(" id=\"ISOCheckStatisticsInfoGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"ISOCheckInfoPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-close\'\"");

WriteLiteral(" onclick=\"closedialog()\"");

WriteLiteral(">关闭</a>\r\n    </span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'p.ProjName,p.ProjNumber,TaskName\', \'Contract\': \'like\' }\"" +
"");

WriteLiteral(" />\r\n    <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n        ");

WriteLiteral("\r\n\r\n        ");

WriteLiteral("\r\n\r\n        ");

WriteLiteral("\r\n\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
