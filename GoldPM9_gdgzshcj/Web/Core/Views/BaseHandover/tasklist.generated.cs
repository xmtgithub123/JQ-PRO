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
    
    #line 2 "..\..\Views\BaseHandover\tasklist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseHandover/tasklist.cshtml")]
    public partial class _Views_BaseHandover_tasklist_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseHandover>
    {
        public _Views_BaseHandover_tasklist_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"border-bottom-width:1px;overflow:hidden\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"height:30px;padding:5px 0px;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" style=\"float:left;margin-left:5px;\"");

WriteLiteral(">\r\n                被移交人：<b>");

            
            #line 8 "..\..\Views\BaseHandover\tasklist.cshtml"
                   Write(ViewBag.HandEmpName);

            
            #line default
            #line hidden
WriteLiteral("</b>&nbsp;&nbsp;移交给:\r\n                <select");

WriteLiteral(" id=\"HandEmpId\"");

WriteLiteral(" name=\"HandEmpId\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:\'auto\'\"");

WriteLiteral(">\r\n");

            
            #line 10 "..\..\Views\BaseHandover\tasklist.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 10 "..\..\Views\BaseHandover\tasklist.cshtml"
                      
                        if (ViewBag.Emps != null)
                        {
                            foreach (var emp in ViewBag.Emps)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <option");

WriteAttribute("value", Tuple.Create(" value=\"", 735), Tuple.Create("\"", 753)
            
            #line 15 "..\..\Views\BaseHandover\tasklist.cshtml"
, Tuple.Create(Tuple.Create("", 743), Tuple.Create<System.Object, System.Int32>(emp.Key
            
            #line default
            #line hidden
, 743), false)
);

WriteLiteral(">");

            
            #line 15 "..\..\Views\BaseHandover\tasklist.cshtml"
                                                       Write(emp.Value);

            
            #line default
            #line hidden
WriteLiteral("</option>\r\n");

            
            #line 16 "..\..\Views\BaseHandover\tasklist.cshtml"
                            }
                        }
                    
            
            #line default
            #line hidden
WriteLiteral("\r\n                </select>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"saveChange()\"");

WriteLiteral(">保存</a>\r\n            </div>\r\n            <div");

WriteLiteral(" style=\"float:right;margin-right:5px\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" id=\"itemStatusRemind\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-play-circle-o\',toggle: true,group:\'type\',selected:t" +
"rue\"");

WriteLiteral(" onclick=\"changeTaskStatus(\'Remind\')\"");

WriteLiteral(">安排或在办</a>\r\n                <a");

WriteLiteral(" id=\"itemStatusFinished\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-check-circle\',toggle: true,group:\'type\'\"");

WriteLiteral(" onclick=\"changeTaskStatus(\'Finished\')\"");

WriteLiteral(">已完</a>\r\n                <a");

WriteLiteral(" id=\"itemStatusBack\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-minus-circle\',toggle: true,group:\'type\'\"");

WriteLiteral(" onclick=\"changeTaskStatus(\'Back\')\"");

WriteLiteral(">回退</a>\r\n                <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ItemPath1,ItemPath2\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" id=\"DesTaskGrid\"");

WriteLiteral("></table>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    var requestUrl = \'");

            
            #line 36 "..\..\Views\BaseHandover\tasklist.cshtml"
                 Write(Url.Action("TaskRemindListJson", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n    var queryParams = {\r\n        itemStatus: \"Remind\",\r\n        showAll: 0," +
"\r\n        EmpID: (");

            
            #line 41 "..\..\Views\BaseHandover\tasklist.cshtml"
            Write(ViewBag.HandEmpId);

            
            #line default
            #line hidden
WriteLiteral(")\r\n    };\r\n\r\n    JQ.datagrid.datagrid({\r\n        JQID: \'DesTaskGrid\',//数据表格ID\r\n  " +
"      JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,datagrid]\r\n        url: reque" +
"stUrl,//请求的地址\r\n        checkOnSelect: true,//是否包含check\r\n        selectOnCheck: f" +
"alse,\r\n        singleSelect: true,\r\n        fitColumns: false,\r\n        nowrap: " +
"false,\r\n        queryParams: queryParams,\r\n        JQSearch: {\r\n            id: " +
"\'JQSearch\',\r\n            prompt: \'任务路径\',\r\n            loadingType: \'datagrid\',//" +
"默认值为datagrid可以不传\r\n        },\r\n        columns: [[\r\n            { field: \"ck\", al" +
"ign: \"center\", checkbox: true, JQIsExclude: true },\r\n            {\r\n            " +
"    title: \'状态\', field: \'ItemStatusName\', width: 40, align: \'center\', formatter:" +
" function (value, row, index) {\r\n                    var icon = \"\";\r\n           " +
"         var title = \"\";\r\n                    switch (row.ItemStatus) {\r\n       " +
"                 case 0:\r\n                            title = \"未启动\";\r\n          " +
"                  icon = \"fa-circle-o\";\r\n                            break;\r\n   " +
"                     case 1:\r\n                            title = \"已轮到\";\r\n      " +
"                      icon = \"fa-dot-circle-o\";\r\n                            bre" +
"ak;\r\n                        case 2:\r\n                            title = \"进行中\";" +
"\r\n                            icon = \"fa-play-circle\";\r\n                        " +
"    break;\r\n                        case 3:\r\n                            title =" +
" \"已完成\";\r\n                            icon = \"fa-check-circle\";\r\n                " +
"            break;\r\n                        case 4:\r\n                           " +
" title = \"已停止\";\r\n                            icon = \"fa-minus-circle\";\r\n        " +
"                    break;\r\n                    }\r\n                    row.ItemS" +
"tatusName = title;\r\n                    return \"<i class=\\\"fa \" + icon + \"\\\" tit" +
"le=\\\"\" + title + \"\\\"></i>\";\r\n                }\r\n            },\r\n\t\t    {\r\n\t\t     " +
"   title: \'任务路径\', field: \'ItemPathText\', width: \'40%\', align: \'left\', formatter:" +
" function (value, row, index) {\r\n\t\t            var itemPathText = \"\";\r\n\t\t       " +
"     if (row.ItemPath1 == \"\" || row.ItemPath2 == \"\") {\r\n\t\t                itemPa" +
"thText = row.ItemName.escapeHTML();\r\n\t\t                row.ItemPathText = row.It" +
"emName;\r\n\t\t            } else {\r\n\t\t                itemPathText = createTaskPath" +
"(row);\r\n\t\t                row.ItemPathText = createTaskPathText(row);\r\n\t\t       " +
"     }\r\n\t\t            return itemPathText;\r\n\t\t        }\r\n\t\t    },\r\n            {" +
" title: \'任务负责人\', field: \'ItemEmpName\', width: \'10%\', align: \'center\', sortable: " +
"true },\r\n\t\t    { title: \'计划开始\', field: \'DatePlanStart\', width: \'10%\', align: \'ce" +
"nter\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t    { titl" +
"e: \'计划结束\', field: \'DatePlanFinish\', width: \'10%\', align: \'center\', sortable: tru" +
"e, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t    { title: \'实际完成\', field: \'D" +
"ateActualFinish\', width: \'10%\', align: \'center\', sortable: true, formatter: JQ.t" +
"ools.DateTimeFormatterByT },\r\n            {\r\n                title: \'操作\', field:" +
" \'ItemAction\', width: 80, align: \'center\', formatter: function (value, row, inde" +
"x) {\r\n                    //return value;\r\n                    var title = value" +
" + \"：\" + row.ItemPathText.escapeHTML();\r\n                    return \"<a href=\\\"j" +
"avascript:void(0);\\\" class=\\\"easyui-linkbutton\\\" data-options=\\\"plain:true,iconC" +
"ls:\'fa fa-trash\'\\\" onclick=\'openDialog(\\\"\" + title + \"\\\",\" + row.ItemType + \",\" " +
"+ row.ProjId + \",\" + row.TaskGroupId + \",\" + row.TaskSpecId + \",\" + row.TaskId +" +
" \",\" + row.TabId + \")\'>\" + value + \"</a>\";\r\n                }\r\n            }\r\n  " +
"      ]]\r\n    });\r\n\r\n    $(\"#JQSearch\").textbox({\r\n        buttonText: \'筛选\',\r\n  " +
"      buttonIcon: \'fa fa-search\',\r\n        height: 25,\r\n        prompt: \'项目名称、任务" +
"名称\',\r\n        onClickButton: function () {\r\n            JQ.datagrid.searchGrid({" +
"\r\n                dgID: \'DesTaskGrid\',\r\n                loadingType: \'datagrid\'," +
"\r\n                queryParams: queryParams\r\n            });\r\n        }\r\n    });\r" +
"\n\r\n    // 切换待办任务状态\r\n    function changeTaskStatus(status) {\r\n        queryParams" +
".itemStatus = status;\r\n        JQ.datagrid.searchGrid({\r\n            dgID: \'DesT" +
"askGrid\',\r\n            loadingType: \'datagrid\',\r\n            queryParams: queryP" +
"arams\r\n        });\r\n    }\r\n\r\n    // 获取任务路径（纯文本）\r\n    function createTaskPathText" +
"(row) {\r\n        var jsonItemPath1 = row.ItemPath1;\r\n        var jsonItemPath2 =" +
" row.ItemPath2;\r\n\r\n        var separator = \" ＞ \";\r\n        var arrItemPath1 = JS" +
"ON.parse(jsonItemPath1);\r\n        var arrItemPath2 = JSON.parse(jsonItemPath2);\r" +
"\n\r\n        var path1 = (Enumerable.From(arrItemPath1).OrderBy(\"x=>x.rownum\").Sel" +
"ect(function (x) { return x.text; }).ToArray());\r\n        var path2 = (Enumerabl" +
"e.From(arrItemPath2).OrderBy(\"x=>x.rownum\").Select(function (x) { return x.text;" +
" }).ToArray());\r\n        var path = [];\r\n        if (path1 != undefined && path1" +
" != null) {\r\n            path.push(path1.join(separator));\r\n        }\r\n        i" +
"f (path2 != undefined && path2 != null && path2.length > 0) {\r\n            path." +
"push(path2.join(separator));\r\n        }\r\n\r\n        switch (row.ItemType) {\r\n    " +
"        case 5:\r\n                path.push(\"{0}\".format(row.ItemName));\r\n       " +
"         break;\r\n        }\r\n\r\n        return path.join(separator);\r\n\r\n    }\r\n\r\n " +
"   // 获取任务路径（带LinkButton）\r\n    function createTaskPath(row) {\r\n        var jsonI" +
"temPath1 = row.ItemPath1;\r\n        var jsonItemPath2 = row.ItemPath2;\r\n\r\n       " +
" var separator = \"<i class=\'fa fa-caret-right\' style=\'margin: 0 5px;\'></i>\";\r\n  " +
"      var arrItemPath1 = JSON.parse(jsonItemPath1);\r\n        var arrItemPath2 = " +
"JSON.parse(jsonItemPath2);\r\n\r\n        var path1 = (Enumerable.From(arrItemPath1)" +
".OrderBy(\"x=>x.rownum\").Select(function (x) { return createLinkButton(x); }).ToA" +
"rray());\r\n        var path2 = (Enumerable.From(arrItemPath2).OrderBy(\"x=>x.rownu" +
"m\").Select(function (x) { return createLinkButton(x); }).ToArray());\r\n        va" +
"r path = [];\r\n        var pathExcel = [];\r\n        if (path1 != undefined && pat" +
"h1 != null) {\r\n            path.push(path1.join(separator));\r\n            pathEx" +
"cel.push(path1.join(\'→\'));\r\n        }\r\n        if (path2 != undefined && path2 !" +
"= null && path2.length > 0) {\r\n            path.push(path2.join(separator));\r\n  " +
"          pathExcel.push(path2.join(\'→\'));\r\n        }\r\n\r\n        switch (row.Ite" +
"mType) {\r\n            case 5:\r\n                path.push(\"<a href=\'javascript:vo" +
"id(0);\' onclick=\'\'>{0}</a>\".format(row.ItemName));\r\n                pathExcel.pu" +
"sh(row.ItemName);\r\n                break;\r\n        }\r\n        return path.join(s" +
"eparator);\r\n\r\n    }\r\n\r\n    function createLinkButton(data) {\r\n        //[{\"rownu" +
"m\":1,\"value\":34,\"text\":\"常州河海路110kV输变电2#主变扩建工程\",\"active\":\"TaskGroup\"},{...}]\r\n   " +
"     var btn = \"\";\r\n        switch (data.active) {\r\n            case \"Project\":\r" +
"\n            case \"TaskGroup\":\r\n            case \"Task\":\r\n                btn = " +
"\"<a href=\'javascript:void(0);\' onclick=\'\'>\" + data.text + \"</a>\";\r\n             " +
"   break;\r\n            default:\r\n                btn = \"<a href=\'javascript:void" +
"(0);\' onclick=\'\'>\" + data.text + \"</a>\";\r\n                break;\r\n        }\r\n   " +
"     return btn;\r\n    }\r\n\r\n    function openDialog(title, itemType, projId, task" +
"GroupId, taskSpecId, taskId, tabId) {\r\n        var url = \"\";\r\n        switch (it" +
"emType) {\r\n            case 1:\r\n                //  项目策划\r\n                if (ta" +
"bId == 0) {\r\n                    url = \'");

            
            #line 225 "..\..\Views\BaseHandover\tasklist.cshtml"
                      Write(Url.Action("PlanTableInfo", "DesTask", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?from=Remind&tabId=\' + tabId + \'&projID=\' + projId + \'&taskGroupId=\' + taskGroupI" +
"d;\r\n                } else {\r\n                    url = \'");

            
            #line 227 "..\..\Views\BaseHandover\tasklist.cshtml"
                      Write(Url.Action("ProjectPlanInfo", "DesTask", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral(@"?from=Remind&tabId=' + tabId + '&projID=' + projId + '&taskGroupId=' + taskGroupId;
                }
                //window.top.addTab(title, url, """");
                JQ.dialog.dialogIframe({
                    title: title,
                    url: url,
                    width: 1200,
                    height: 900,
                    onClose: function () {
                        $(""#DesTaskGrid"").datagrid(""reload"");
                    }
                });
                break;
            case 2:
                //  专业策划
                url = '");

            
            #line 242 "..\..\Views\BaseHandover\tasklist.cshtml"
                  Write(Url.Action("SpecPlanInfo", "DesTask", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral(@"?from=Remind&tabId=' + tabId + '&projID=' + projId + '&taskGroupId=' + taskGroupId + '&taskSpecId=' + taskSpecId;
                //window.top.addTab(title, url, """");
                JQ.dialog.dialogIframe({
                    title: title,
                    url: url,
                    width: 1200,
                    height: 900,
                    onClose: function () {
                        $(""#DesTaskGrid"").datagrid(""reload"");
                    }
                });
                break;
            case 3:
                //  提交设计
                url = '");

            
            #line 256 "..\..\Views\BaseHandover\tasklist.cshtml"
                  Write(Url.Action("TaskInfo", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?from=Remind&Id=\' + taskId + \'&ViewEmpID=");

            
            #line 256 "..\..\Views\BaseHandover\tasklist.cshtml"
                                                                                                                   Write(ViewBag.HandEmpId);

            
            #line default
            #line hidden
WriteLiteral(@"';
                JQ.dialog.dialog({
                    title: title,
                    url: url,
                    width: 1200,
                    height: 900,
                    onClose: function () {
                        $(""#DesTaskGrid"").datagrid(""reload"");
                    }
                });
                break;
            case 4:
                //  设计校审
                url = '");

            
            #line 269 "..\..\Views\BaseHandover\tasklist.cshtml"
                  Write(Url.Action("TaskInfoApprove", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?from=Remind&Id=\' + taskId + \'&ViewEmpID=");

            
            #line 269 "..\..\Views\BaseHandover\tasklist.cshtml"
                                                                                                                          Write(ViewBag.HandEmpId);

            
            #line default
            #line hidden
WriteLiteral(@"';
                JQ.dialog.dialog({
                    title: title,
                    url: url,
                    width: 1200,
                    height: 900,
                    onClose: function () {
                        $(""#DesTaskGrid"").datagrid(""reload"");
                    }
                });
                break;
            case 5:
                //会签任务
                debugger;
                url = '");

            
            #line 283 "..\..\Views\BaseHandover\tasklist.cshtml"
                  Write(Url.Action("Edit", "DesMutiSign", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral(@"?from=Remind&Id=' + tabId + '&RecID=' + taskGroupId;
                JQ.dialog.dialog({
                    title: title,
                    url: url,
                    width: 800,
                    height: 800,
                    onClose: function () {
                        $(""#DesTaskGrid"").datagrid(""reload"");
                    }
                });
                break;
        }
    }

    function saveChange() {
        var empID = $(""#HandEmpId"").combobox(""getValue"");
        if (!empID) {
            return;
        }
        var $tbList = $(""#DesTaskGrid"");
        var checkedItems = $tbList.datagrid(""getChecked"");
        if (checkedItems.length == 0) {
            return;
        }
        var list = [];
        $.each(checkedItems, function (index, item) {
            list.push(item);
        });
        JQ.tools.ajax({
            url: '");

            
            #line 312 "..\..\Views\BaseHandover\tasklist.cshtml"
             Write(Url.Action("TaskWorkBatchTransfer", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?fromEmpId=");

            
            #line 312 "..\..\Views\BaseHandover\tasklist.cshtml"
                                                                                              Write(ViewBag.HandEmpId);

            
            #line default
            #line hidden
WriteLiteral("&toEmpId=\' + empID,\r\n            data: { TransferData: JSON.stringify(list) },\r\n " +
"           succesCollBack: function (backJson) {\r\n                $tbList.datagr" +
"id(\"reload\");\r\n            }\r\n        });\r\n    }\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
