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
    
    #line 4 "..\..\Views\PubFlow\TodoList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PubFlow/TodoList.cshtml")]
    public partial class _Views_PubFlow_TodoList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PubFlow_TodoList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\PubFlow\TodoList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        //alert(window)
        $(function () {
            $(""#statuslist"").combobox({
                panelHeight: ""auto"",
                editable: false,
                onSelect: function () {
                    refreshGrid();
                }
            });
            $(""#JQSearch"").textbox({
                buttonText: ""筛选"",
                buttonIcon: ""fa fa-search"",
                height: 25,
                prompt: ""标题、流程名称、节点名称"",
                onClickButton: function () {
                    refreshGrid()
                }
            });

            JQ.datagrid.datagrid({
                JQButtonTypes: [""""],
                JQPrimaryID: ""FlowNodeID"",
                JQID: ""tbList"",
                JQDialogTitle: ""流程模版"",
                JQWidth: ""768"",//弹出窗体宽
                JQHeight: '700',//弹出窗体高
                JQLoadingType: ""datagrid"",
                url: """);

            
            #line 34 "..\..\Views\PubFlow\TodoList.cshtml"
                  Write(Url.Action("GetToDoList", "PubFlow", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                checkOnSelect: true,\r\n                toolbar: \"#list_toolbar" +
"\",\r\n                columns: [[\r\n                    { field: \"ck\", align: \"cent" +
"er\", checkbox: true, JQIsExclude: true },\r\n                    { title: \"标题\", fi" +
"eld: \"FlowTitle\", width: 380, align: \"left\", halign: \"center\" },\r\n              " +
"      { title: \"流程名称\", field: \"FlowName\", width: 120, align: \"center\", halign: \"" +
"center\" },\r\n                    { title: \"节点名称\", field: \"FlowNodeName\", width: 1" +
"00, align: \"center\", halign: \"center\" },\r\n                    {\r\n               " +
"         title: \"节点轮到日期\", field: \"FlowNodeFromDateTime\", width: 90, align: \"cent" +
"er\", halign: \"center\", formatter: function (value) {\r\n                          " +
"  return JQ.Flow.parseDateText(value);\r\n                        }\r\n             " +
"       },\r\n                    {\r\n                        title: \"节点完成日期\", field" +
": \"FlowNodeDate\", width: 90, align: \"center\", halign: \"center\", formatter: funct" +
"ion (value) {\r\n                            return JQ.Flow.parseDateText(value);\r" +
"\n                        }\r\n                    },\r\n                     { title" +
": \"节点状态\", field: \"FlowNodeStatusName\", width: 60, align: \"center\", halign: \"cent" +
"er\" },\r\n                    //{ title: \"处理人\", field: \"FlowNodeEmpName\", width: 6" +
"0, align: \"center\", halign: \"center\" },\r\n                    {\r\n                " +
"        title: \"流程发起日期\", field: \"FlowStartDate\", width: 90, align: \"center\", hal" +
"ign: \"center\", formatter: function (value) {\r\n                            return" +
" JQ.Flow.parseDateText(value);\r\n                        }\r\n                    }" +
",\r\n                    {\r\n                        title: \"流程完成日期\", field: \"FlowF" +
"inishDate\", width: 90, align: \"center\", halign: \"center\", formatter: function (v" +
"alue) {\r\n                            return JQ.Flow.parseDateText(value);\r\n     " +
"                   }\r\n                    },\r\n                     {\r\n          " +
"               title: \"流程状态\", field: \"FlowStatusName\", width: 140, align: \"cente" +
"r\", halign: \"center\", formatter: function (value, rowData) {\r\n                  " +
"           var span = document.createElement(\"span\");\r\n                         " +
"    span.appendChild(document.createTextNode(value));\r\n                         " +
"    span.style.color = \"#165778\";\r\n                             span.style.curso" +
"r = \"pointer\";\r\n                             span.setAttribute(\"onclick\", \"showP" +
"rogress(\" + rowData.FlowNodeID + \")\");\r\n                             return span" +
".outerHTML;\r\n                         }\r\n                     }, {\r\n            " +
"             title: \"操作\", field: \"oao\", width: 60, align: \"center\", halign: \"cen" +
"ter\", formatter: function (value, rowData) {\r\n                             if ($" +
"(\"#statuslist\").combobox(\"getValue\") == 2) {\r\n                                 r" +
"eturn \"<a href=\\\"javascript:void(0)\\\" onclick=\\\"showProcess(\" + rowData.FlowNode" +
"ID + \")\\\">查看</a>\";\r\n                             } else {\r\n                     " +
"            return \"<a href=\\\"javascript:void(0)\\\" onclick=\\\"showProcess(\" + row" +
"Data.FlowNodeID + \")\\\">处理</a>\";\r\n                             }\r\n               " +
"          }\r\n                     }\r\n                    //,{ title: \"处理路径\", fie" +
"ld: \"FlowNodeUrl\", width: 120, align: \"left\", halign: \"center\" },\r\n             " +
"       //{ title: \"流程ID\", field: \"FlowID\", width: 45, align: \"right\", halign: \"c" +
"enter\" },\r\n                    //{ title: \"节点ID\", field: \"FlowNodeID\", width: 45" +
", align: \"right\", halign: \"center\" },\r\n                    //{ title: \"会签节点ID\", " +
"field: \"FlowMultiSignID\", width: 45, align: \"right\", halign: \"center\" }\r\n       " +
"         ]],\r\n                queryParams: getQueryParameters()\r\n            });" +
"\r\n        });\r\n\r\n        window.refreshGrid = function () {\r\n            $(\"#tbL" +
"ist\").datagrid(\"load\", getQueryParameters());\r\n        }\r\n\r\n        function get" +
"QueryParameters() {\r\n            return { text: $(\"#JQSearch\").textbox(\"getText\"" +
"), statusID: $(\"#statuslist\").combobox(\"getValue\"), modular: \"");

            
            #line 96 "..\..\Views\PubFlow\TodoList.cshtml"
                                                                                                                     Write(Request.QueryString["modular"]??"");

            
            #line default
            #line hidden
WriteLiteral(@""" };
        }

        function showProcess(flowNodeID) {
            var datas = $(""#tbList"").datagrid(""getData"").rows;
            for (var i in datas) {
                if (datas[i].FlowNodeID == flowNodeID) {
                    if (datas[i].FlowType == 0) {
                        JQ.dialog.dialog({
                            title: decodeURIComponent(datas[i].FlowName + "" "" + datas[i].FlowNodeName),
                            url: """);

            
            #line 106 "..\..\Views\PubFlow\TodoList.cshtml"
                              Write(ViewBag.sitePath);

            
            #line default
            #line hidden
WriteLiteral(@""" + datas[i].FlowNodeUrl,
                            width: datas[i].DialogWidth,
                            height: datas[i].DialogHeight,
                            JQID: """",
                            JQLoadingType: """",
                            iconCls: ""fa fa-table"",
                            onClose: function () {
                                $(""#tbList"").datagrid(""load"", getQueryParameters());
                            }
                        });
                    }
                    else {
                        new YChart.Dialog({
                            height: datas[i].DialogHeight,
                            width: datas[i].DialogWidth,
                            icon: ""fa-table"",
                            title: decodeURIComponent(datas[i].FlowName + "" "" + datas[i].FlowNodeName),
                            url: """);

            
            #line 123 "..\..\Views\PubFlow\TodoList.cshtml"
                              Write(Url.Action("Edit","Manage",new { @area="Form" }));

            
            #line default
            #line hidden
WriteLiteral(@"?activityID="" + datas[i].FlowNodeID + ""&processID="" + datas[i].FlowID + ""&formTemplateID="" + datas[i].FormTemplateID,
                            api: {
                                refreshGrid: function () {
                                    window.refreshGrid();
                                }
                            }
                        });
                    }
                    break;
                }
            }
            JQ.Flow.stopBubble();
        }

        function showProgress(flowNodeID) {
            var datas = $(""#tbList"").datagrid(""getData"").rows;
            var title = """";
            var flowID = 0;
            var flowType = 0;
            for (var i in datas) {
                if (datas[i].FlowNodeID == flowNodeID) {
                    title = datas[i].FlowName;
                    flowID = datas[i].FlowID;
                    flowType = datas[i].FlowType;
                    break;
                }
            }
            if (flowType == 0) {
                JQ.dialog.dialog({
                    title: decodeURIComponent(title),
                    url: """);

            
            #line 153 "..\..\Views\PubFlow\TodoList.cshtml"
                      Write(Url.Action("Progress", "PubFlow", new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?flowID="" + flowID,
                    width: 800,
                    height: 600,
                    iconCls: ""fa fa-list""
                });
            }
            else {
                new YChart.Dialog({
                    height: 600,
                    width: 800,
                    icon: ""fa-th-list"",
                    title: decodeURIComponent(title),
                    url: """);

            
            #line 165 "..\..\Views\PubFlow\TodoList.cshtml"
                      Write(Url.Action("Progress", "Widget", new { @area="Process" }));

            
            #line default
            #line hidden
WriteLiteral("?id=\" + flowID\r\n                });\r\n            }\r\n            JQ.Flow.stopBubbl" +
"e();\r\n        }\r\n    </script>\r\n");

});

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"tbList\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"list_toolbar\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n        <select");

WriteLiteral(" id=\"statuslist\"");

WriteLiteral(">\r\n            <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">未审批</option>\r\n            <option");

WriteLiteral(" value=\"2\"");

WriteLiteral(">已审批</option>\r\n        </select>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n    </div>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 8784), Tuple.Create("\"", 8832)
            
            #line 183 "..\..\Views\PubFlow\TodoList.cshtml"
, Tuple.Create(Tuple.Create("", 8790), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/chart/chart.js")
            
            #line default
            #line hidden
, 8790), false)
);

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 8879), Tuple.Create("\"", 8934)
            
            #line 184 "..\..\Views\PubFlow\TodoList.cshtml"
, Tuple.Create(Tuple.Create("", 8885), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/chart/chart.dialog.js")
            
            #line default
            #line hidden
, 8885), false)
);

WriteLiteral("></script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
