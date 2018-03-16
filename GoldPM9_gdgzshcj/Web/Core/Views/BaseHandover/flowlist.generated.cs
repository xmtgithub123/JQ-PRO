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
    
    #line 2 "..\..\Views\BaseHandover\flowlist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseHandover/flowlist.cshtml")]
    public partial class _Views_BaseHandover_flowlist_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseHandover>
    {
        public _Views_BaseHandover_flowlist_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'BaseHandoverForm',//formid提交需要用到
        buttonTypes: ['close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeButtons: [
            {
                icon: ""fa-save"", text: ""保存"", onClick: function () {
                    saveChange();
                }
            },
        ],
        onInit: function ($panel) {

        }
    });
    $(""#JQSearch"").textbox({
        buttonText: ""筛选"",
        buttonIcon: ""fa fa-search"",
        height: 25,
        prompt: ""筛选字段"",
        onClickButton: function () {
            refreshGrid()
        }
    });

    JQ.datagrid.datagrid({
        JQPrimaryID: ""FlowNodeID"",
        JQID: ""tbList"",
        JQDialogTitle: ""流程模版"",
        JQWidth: ""768"",//弹出窗体宽
        JQHeight: '700',//弹出窗体高
        JQLoadingType: ""datagrid"",
        url: """);

            
            #line 39 "..\..\Views\BaseHandover\flowlist.cshtml"
          Write(Url.Action("GetToDoList", "PubFlow", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        checkOnSelect: true,\r\n        toolbar: \"#list_toolbar\",\r\n        colu" +
"mns: [[\r\n            { field: \"ck\", align: \"center\", checkbox: true, JQIsExclude" +
": true },\r\n            { title: \"标题\", field: \"FlowTitle\", width: 250, align: \"le" +
"ft\", halign: \"center\" },\r\n            { title: \"流程名称\", field: \"FlowName\", width:" +
" 100, align: \"center\", halign: \"center\" },\r\n            { title: \"节点名称\", field: " +
"\"FlowNodeName\", width: 100, align: \"center\", halign: \"center\" },\r\n            {\r" +
"\n                title: \"节点轮到日期\", field: \"FlowNodeFromDateTime\", width: 80, alig" +
"n: \"center\", halign: \"center\", formatter: function (value) {\r\n                  " +
"  return JQ.Flow.parseDateText(value);\r\n                }\r\n            },\r\n     " +
"       { title: \"节点状态\", field: \"FlowNodeStatusName\", width: 60, align: \"center\"," +
" halign: \"center\" },\r\n            { title: \"处理人\", field: \"FlowNodeEmpName\", widt" +
"h: 60, align: \"center\", halign: \"center\" },\r\n            {\r\n                titl" +
"e: \"流程发起日期\", field: \"FlowStartDate\", width: 80, align: \"center\", halign: \"center" +
"\", formatter: function (value) {\r\n                    return JQ.Flow.parseDateTe" +
"xt(value);\r\n                }\r\n            },\r\n             {\r\n                 " +
"title: \"流程状态\", field: \"FlowStatusName\", width: 100, align: \"center\", halign: \"ce" +
"nter\", formatter: function (value, rowData) {\r\n                     var span = d" +
"ocument.createElement(\"span\");\r\n                     span.appendChild(document.c" +
"reateTextNode(value));\r\n                     span.style.color = \"#165778\";\r\n    " +
"                 span.style.cursor = \"pointer\";\r\n                     span.setAt" +
"tribute(\"onclick\", \"showProgress(\" + rowData.FlowNodeID + \")\");\r\n               " +
"      return span.outerHTML;\r\n                 }\r\n             },\r\n        ]],\r\n" +
"        queryParams: getQueryParameters()\r\n    });\r\n\r\n    window.refreshGrid = f" +
"unction () {\r\n        $(\"#tbList\").datagrid(\"load\", getQueryParameters());\r\n    " +
"}\r\n\r\n    function getQueryParameters() {\r\n        return { text: $(\"#JQSearch\")." +
"textbox(\"getText\"), statusID: 1, modular: \"");

            
            #line 78 "..\..\Views\BaseHandover\flowlist.cshtml"
                                                                             Write(Request.QueryString["modular"]??"");

            
            #line default
            #line hidden
WriteLiteral("\",empID: \"");

            
            #line 78 "..\..\Views\BaseHandover\flowlist.cshtml"
                                                                                                                            Write(Model.HandEmpId);

            
            #line default
            #line hidden
WriteLiteral(@""",useEmpID:""1"" };
        }

        function showProcess(flowNodeID) {
            var datas = $(""#tbList"").datagrid(""getData"").rows;
            var flowNodeUrl = """";
            var title = """";
        for (var i in datas) {
                if (datas[i].FlowNodeID == flowNodeID) {
                    flowNodeUrl = datas[i].FlowNodeUrl;
                    title = datas[i].FlowName + "" "" + datas[i].FlowNodeName;
                    break;
                }
            }
            JQ.dialog.dialog({
                title: decodeURIComponent(title),
            url: """);

            
            #line 94 "..\..\Views\BaseHandover\flowlist.cshtml"
              Write(ViewBag.sitePath);

            
            #line default
            #line hidden
WriteLiteral(@""" + flowNodeUrl,
            width: 800,
            height: 680,
            JQID: """",
            JQLoadingType: """",
            iconCls: ""fa fa-table""
            });
            JQ.Flow.stopBubble();
        }

        function showProgress(flowNodeID) {
            var datas = $(""#tbList"").datagrid(""getData"").rows;
            var title = """";
            var flowID = 0;
        for (var i in datas) {
                if (datas[i].FlowNodeID == flowNodeID) {
                    title = datas[i].FlowName;
                    flowID = datas[i].FlowID;
                    break;
                }
            }
            JQ.dialog.dialog({
                title: decodeURIComponent(title),
            url: """);

            
            #line 117 "..\..\Views\BaseHandover\flowlist.cshtml"
              Write(Url.Action("Progress", "PubFlow", new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?flowID="" + flowID,
            width: 800,
            height: 600,
            iconCls: ""fa fa-list""
            });
            JQ.Flow.stopBubble();
        }
        function saveChange() {
            var empID = $(""#HandEmpId"").combobox(""getValue"");
            if (!empID) {
                return;
            }
            var $tbList = $(""#tbList"");
            var checkedItems = $tbList.datagrid(""getChecked"");
            if (checkedItems.length == 0) {
                return;
            }
            var list = [];
            for (var i = 0; i < checkedItems.length; i++) {
            list.push(checkedItems[i].FlowNodeID + ""|"" + checkedItems[i].FlowMultiSignID);
        }
        $tbList.datagrid(""loading"");
        $.ajax({
            url: """);

            
            #line 140 "..\..\Views\BaseHandover\flowlist.cshtml"
              Write(Url.Action("BatchTransfer","PubFlow",new { @area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            type: \"POST\",\r\n            data: { flowNodeIDs: list.join(\",\"), e" +
"mpID: empID, handOverID: \"");

            
            #line 142 "..\..\Views\BaseHandover\flowlist.cshtml"
                                                                        Write(Request.QueryString["id"]);

            
            #line default
            #line hidden
WriteLiteral(@""" },
            success: function (result) {
                $tbList.datagrid(""loaded"");
                if (result.Result == false) {
                    JQ.dialog.error(result.Message);
                    return;
                }
                window.refreshGrid();
            },
            error: function () {
                $tbList.datagrid(""loaded"");
            }
        });
    }
</script>
");

            
            #line 157 "..\..\Views\BaseHandover\flowlist.cshtml"
 using (Html.BeginForm("save", "BaseHandover", FormMethod.Post, new { @area = "Core", @id = "BaseHandoverForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\BaseHandover\flowlist.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\BaseHandover\flowlist.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" id=\"tbList\"");

WriteLiteral(" style=\"height:800px; min-height:560px\"");

WriteLiteral("></table>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"list_toolbar\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            移交给:\r\n            <select");

WriteLiteral(" id=\"HandEmpId\"");

WriteLiteral(" name=\"HandEmpId\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:\'auto\'\"");

WriteLiteral(">\r\n");

            
            #line 165 "..\..\Views\BaseHandover\flowlist.cshtml"
                
            
            #line default
            #line hidden
            
            #line 165 "..\..\Views\BaseHandover\flowlist.cshtml"
                  
                    if (ViewBag.Emps != null)
                    {
                        foreach (var emp in ViewBag.Emps)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <option");

WriteAttribute("value", Tuple.Create(" value=\"", 6785), Tuple.Create("\"", 6803)
            
            #line 170 "..\..\Views\BaseHandover\flowlist.cshtml"
, Tuple.Create(Tuple.Create("", 6793), Tuple.Create<System.Object, System.Int32>(emp.Key
            
            #line default
            #line hidden
, 6793), false)
);

WriteLiteral(">");

            
            #line 170 "..\..\Views\BaseHandover\flowlist.cshtml"
                                                   Write(emp.Value);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 171 "..\..\Views\BaseHandover\flowlist.cshtml"
                        }
                    }
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </select>\r\n        </span>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n    </div>\r\n");

            
            #line 178 "..\..\Views\BaseHandover\flowlist.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
