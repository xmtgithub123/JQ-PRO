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
    
    #line 4 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoForm/ProjFormDetail.cshtml")]
    public partial class _Views_IsoForm_ProjFormDetail_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoForm_ProjFormDetail_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        $(function () {
            $(""#JQSearch"").textbox({
                buttonText: ""筛选"",
                buttonIcon: ""fa fa-search"",
                height: 25,
                prompt: ""表单名称"",
                onClickButton: function () {
                    refreshGrid()
                }
            });

            JQ.datagrid.datagrid({
                JQButtonTypes: [''],
                JQPrimaryID: ""FlowID"",
                JQID: ""tbList"",
                JQDialogTitle: ""表单"",
                JQWidth: ""768"",//弹出窗体宽
                JQHeight: '700',//弹出窗体高
                JQLoadingType: ""datagrid"",
                url: """);

            
            #line 26 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                  Write(Url.Action("GetFormsByProjID1", "IsoForm", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                checkOnSelect: true,\r\n                //fitColumns: true,\r\n  " +
"              nowrap: false,\r\n                toolbar: \"#list_toolbar\",\r\n       " +
"         columns: [[\r\n                    { field: \"ck\", align: \"center\", checkb" +
"ox: true, JQIsExclude: true },\r\n                    { title: \"表单标题\", field: \"Flo" +
"wTitle\", width: \"40%\", align: \"left\", halign: \"center\" },\r\n                    {" +
" title: \"流程名称\", field: \"FlowName\", width: \"12%\", align: \"left\", halign: \"center\"" +
" },\r\n                    { title: \"发起日期\", field: \"CreationTime\", width: \"80\", al" +
"ign: \"center\", halign: \"center\", formatter: JQ.tools.DateTimeFormatterByT },\r\n  " +
"                  { title: \"发起人\", field: \"CreatorEmpName\", width: \"80\", align: \"" +
"center\", halign: \"center\" },\r\n                    {\r\n                        tit" +
"le: \"进度\", field: \"FlowStatusName\", width: \"15%\", align: \"left\", halign: \"center\"" +
", formatter: function (value, rowData) {\r\n                            var a = do" +
"cument.createElement(\"a\");\r\n                            a.setAttribute(\"href\", \"" +
"javascript:void(0)\");\r\n                            a.appendChild(document.create" +
"TextNode(value));\r\n                            a.setAttribute(\"onclick\", \"openPr" +
"ogressDialog(\" + rowData.ID + \",\'\" + encodeURIComponent(rowData.FlowName) + \"\');" +
"\");\r\n                            return a.outerHTML;\r\n                        }\r" +
"\n                    },\r\n                    {\r\n                        title: \"" +
"操作\", field: \"CZ\", width: \"40\", align: \"center\", halign: \"center\",\r\n             " +
"           formatter: function (value, row, index) {\r\n                          " +
"  return \"<a btn  class=\'easyui-linkbutton\'  data-options=\'iconCls:\\\"fa fa-check" +
"\\\"\'   onclick=\\\"Edit(\'\" + row.FlowUrl + \"\')\\\">\" + row.NodeState + \"</a>\";\r\n     " +
"                   }\r\n                    }\r\n                ]],\r\n              " +
"  queryParams: getQueryParameters()\r\n            });\r\n\r\n            JQ.datagrid." +
"datagrid({\r\n                JQPrimaryID: \"id\",\r\n                JQID: \"tbListSpe" +
"c\",\r\n                JQDialogTitle: \"表单\",\r\n                JQWidth: \"768\",//弹出窗体" +
"宽\r\n                JQHeight: \'700\',//弹出窗体高\r\n                JQLoadingType: \"data" +
"grid\",\r\n                url: \"");

            
            #line 63 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                  Write(Url.Action("GetSpecByProjID", "IsoForm", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                checkOnSelect: true,\r\n                fitColumns: true,\r\n    " +
"            nowrap: false,\r\n                columns: [[\r\n                    { t" +
"itle: \"专业\", field: \"SpecName\", width: \"15%\", align: \"center\", halign: \"center\" }" +
",\r\n                    { title: \"卷册\", field: \"TaskName\", width: \"20%\", align: \"c" +
"enter\", halign: \"center\" },\r\n                    { title: \"设计人员\", field: \"TaskEm" +
"pName\", width: \"10%\", align: \"center\", halign: \"center\" },\r\n                    " +
"{\r\n                        title: \"操作\", field: \"CZ\", width: \"350px\", align: \"cen" +
"ter\", halign: \"center\", formatter: function (value, row, index) {\r\n             " +
"               var html = \"\";\r\n                            if (row.TaskSpecId > " +
"0) {\r\n                                html += \"<a id=\\\"btn\\\" class=\\\"easyui-link" +
"button\\\"  data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\"  onclick=\\\"ShowDetail(\' \"" +
" + row.TaskID + \"\',0)\\\">校审单</a> &nbsp;&nbsp;\";\r\n                                " +
"html += \"<a id=\\\"btn\\\" class=\\\"easyui-linkbutton\\\"  data-options=\\\"iconCls:\'fa f" +
"a-file-word-o\'\\\"  onclick=\\\"ShowDetail(\' \" + row.TaskID + \"\',1)\\\">(校对)校审单 </a> &" +
"nbsp;&nbsp;\";\r\n                                html += \"<a id=\\\"btn\\\" class=\\\"ea" +
"syui-linkbutton\\\"  data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\"  onclick=\\\"ShowD" +
"etail(\' \" + row.TaskID + \"\',2)\\\">(审核)校审单</a> &nbsp;&nbsp;\";\r\n                   " +
"             html += \"<a id=\\\"btn\\\" class=\\\"easyui-linkbutton\\\"  data-options=\\\"" +
"iconCls:\'fa fa-file-word-o\'\\\"  onclick=\\\"ShowDetail(\' \" + row.TaskID + \"\',3)\\\">(" +
"审定)校审单</a> &nbsp;&nbsp;\";\r\n                                html += \"<a id=\\\"DesE" +
"xch\\\" class=\\\"easyui-linkbutton\\\"  data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\" " +
" onclick=\\\"ShowDesExch(\' \" + row.TaskID + \"\')\\\">提资单</a> \";\r\n\r\n\r\n                " +
"            } else {\r\n                                html += \"<a id=\\\"btn\\\" cla" +
"ss=\\\"easyui-linkbutton\\\"  data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\"  onclick=" +
"\\\"ShowDetail(\' \" + row.TaskID + \"\',0)\\\">校审单</a> &nbsp;&nbsp;\";\r\n                " +
"                html += \"<a id=\\\"btn\\\" class=\\\"easyui-linkbutton\\\"  data-options" +
"=\\\"iconCls:\'fa fa-file-word-o\'\\\"  onclick=\\\"ShowDetail(\' \" + row.TaskID + \"\',1)\\" +
"\">(校对)校审单 </a> &nbsp;&nbsp;\";\r\n                                html += \"<a id=\\\"" +
"btn\\\" class=\\\"easyui-linkbutton\\\"  data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\" " +
" onclick=\\\"ShowDetail(\' \" + row.TaskID + \"\',2)\\\">(审核)校审单</a> &nbsp;&nbsp;\";\r\n   " +
"                             html += \"<a id=\\\"btn\\\" class=\\\"easyui-linkbutton\\\" " +
" data-options=\\\"iconCls:\'fa fa-file-word-o\'\\\"  onclick=\\\"ShowDetail(\' \" + row.Ta" +
"skID + \"\',3)\\\">(审定)校审单</a> &nbsp;&nbsp;\";\r\n                            }\r\n      " +
"                      return html;\r\n                        }\r\n                 " +
"   }\r\n                ]],\r\n                queryParams: { ProjID: \'");

            
            #line 92 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                                   Write(ViewBag.projID);

            
            #line default
            #line hidden
WriteLiteral("\', TaskGroupID: \'");

            
            #line 92 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                                                                   Write(ViewBag.taskGroupId);

            
            #line default
            #line hidden
WriteLiteral(@"' }
            });
        });
        //end $(function(){})
        window.refreshGrid = function () {
            var query = getQueryParameters();
            query.FormIDs = $(""#allformSelect"").combotree(""getValues"");
            query.FlowStatus = $(""#FlowStatusEnum"").combotree(""getValue"");
            $(""#tbList"").datagrid(""load"", query);
        }

        function getQueryParameters() {
            return { text: $(""#JQSearch"").textbox(""getText""), projectID: """);

            
            #line 104 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                                                                      Write(ViewBag.projID);

            
            #line default
            #line hidden
WriteLiteral("\", phaseID: \"");

            
            #line 104 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                                                                                                    Write(ViewBag.PhaseID);

            
            #line default
            #line hidden
WriteLiteral("\" };\r\n        }\r\n\r\n\r\n        function Edit(FlowUrl) {\r\n            JQ.dialog.dial" +
"og({\r\n                title: \"查看\",\r\n                url: \"");

            
            #line 111 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                  Write(ViewBag.sitePath);

            
            #line default
            #line hidden
WriteLiteral("\" + FlowUrl,\r\n                width: \'1024\',\r\n                height: \'100%\',\r\n  " +
"          });\r\n        }\r\n\r\n        //校审单\r\n        function ShowDetail(id,type) " +
"{\r\n            JQ.dialog.dialog({\r\n                title: \"查看校审单\",\r\n            " +
"    url: \'");

            
            #line 121 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                 Write(Url.Action("desTaskCheckDetail", "IsoTaskCheck", new { @area = "ISO" }));

            
            #line default
            #line hidden
WriteLiteral(@"?taskId=' + id + '&type=' + type + '',
                width: '1024',
                height: '100%',
                JQID: 'ISOCheckList',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-th-list',
            });
        }

        //提资单
        function ShowDesExch(id) {

            JQ.dialog.dialog({
                title: ""查看提资信息"",
                url: '");

            
            #line 135 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                 Write(Url.Action("ProjDesSpecExchList", "IsoForm", new { @area = "ISO" }));

            
            #line default
            #line hidden
WriteLiteral(@"?taskId=' + id,
                width: '1024',
                height: '100%',
                JQID: 'ISOCheckList',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-th-list',
            });
        }

        function openProgressDialog(flowID, flowName) {
            JQ.Flow.showProgressDialog(decodeURIComponent(flowName), """);

            
            #line 145 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
                                                                  Write(Url.Action("Progress","PubFlow",new { @area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("?flowID=\" + flowID);\r\n            JQ.Flow.stopBubble();\r\n        }\r\n    </script>" +
"\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n\r\n    <div");

WriteLiteral(" data-options=\"region:\'north\'\"");

WriteLiteral(" style=\"width:205px;height:40%;\"");

WriteLiteral(">\r\n\r\n        <table");

WriteLiteral(" id=\"tbList\"");

WriteLiteral("></table>\r\n        <div");

WriteLiteral(" id=\"list_toolbar\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" class=\"label label-default\"");

WriteLiteral("><strong");

WriteLiteral(" style=\"padding:10px;\"");

WriteLiteral(">项目表单</strong></span>\r\n            <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            </span>\r\n");

WriteLiteral("            ");

            
            #line 160 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
       Write(Html.GetComboTree(new Condition()
       {
           Area = "Iso",
           ControllerName = "IsoForm",
           ActionName = "getAllFormJson",
           controlName = "allformSelect",
           isMult = true,
           prompt = "表单类型"
       }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 169 "..\..\Views\IsoForm\ProjFormDetail.cshtml"
       Write(Html.GetComboTree(new Condition()
       {
           Area = "Iso",
           ControllerName = "IsoForm",
           ActionName = "getFlowStatusEnum",
           controlName = "FlowStatusEnum",
           isMult = false,
           prompt = "审批状态"
       }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"ProjMainTab\"");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" id=\"tbListSpec\"");

WriteLiteral("></table>\r\n    </div>\r\n\r\n");

});

        }
    }
}
#pragma warning restore 1591