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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/SpecPlanBatch.cshtml")]
    public partial class _Views_DesTask_SpecPlanBatch_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_SpecPlanBatch_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: ""projectplanbatchform"",
        buttonTypes: [""close""],
        beforeButtons: [
            {
                text: ""确定"", icon: ""fa-check"", onClick: function () {
                    if(!$(""#projectplanbatchform"").form(""validate"")) {
                        return;
                    }
                    var data=getSaveData();
                    if(data.length==0) {
                        return;
                    }
                    $(""#tbList_PlanBatch"").datagrid(""loading"");
                    $.ajax({
                        url:""");

            
            #line 18 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                         Write(Url.Action("SpecPlanBatchSaveSpecPlanData", "DesTask",new { area= "Design" }));

            
            #line default
            #line hidden
WriteLiteral(@""",
                        type:""POST"",
                        data:{ data:JSON.stringify(data),shenHe:$(""#task_shenhe"").combobox(""getValue""),piZhun:$(""#task_pizhun"").combobox(""getValue"") },
                        success:function(){
                            JQ.dialog.dialogClose();
                        },complete:function(){
                            $(""#tbList_PlanBatch"").datagrid(""loaded"");
                        }
                    });
                }
            }
        ],
        succesCollBack: function (data) {
            JQ.dialog.dialogClose();
        }
    });
</script>
<form");

WriteLiteral(" id=\"projectplanbatchform\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" id=\"tbList_toolbar\"");

WriteLiteral(">\r\n        审核：<input");

WriteLiteral(" id=\"task_shenhe\"");

WriteLiteral(" name=\"task_shenhe\"");

WriteLiteral(" /> 批准：<input");

WriteLiteral(" id=\"task_pizhun\"");

WriteLiteral(" name=\"task_pizhun\"");

WriteLiteral(" />\r\n    </div>\r\n    <table");

WriteLiteral(" id=\"tbList_PlanBatch\"");

WriteLiteral("></table>\r\n</form>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var _Qualification=");

            
            #line 42 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                   Write(Html.Raw(ViewBag.Qualification));

            
            #line default
            #line hidden
WriteLiteral(@";
    var height = document.getElementById(""projectplanbatchform"").parentNode.clientHeight;
    var width = document.getElementById(""projectplanbatchform"").parentNode.clientWidth;
    $(""#tbList_PlanBatch"").datagrid({
        idField:""TaskId"",
        rownumbers: true,
        toolbar: ""#tbList_toolbar"",
        url: """);

            
            #line 49 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
          Write(Url.Action("SpecPlanBatchGetSpecPlanDataJson", "DesTask", new { @area="Design" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        queryParams: { taskIDSet: \"");

            
            #line 50 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                               Write(Request.QueryString["taskIDs"]);

            
            #line default
            #line hidden
WriteLiteral("\" },\r\n        columns: [[\r\n            {\r\n                title: \"任务路径\", field: \'" +
"ItemPathText\', align: \'left\', halign: \"center\", width: 700, formatter: function " +
"(value, rowData) {\r\n                    var itemPathText = \"\";\r\n                " +
"    if (rowData.ItemPath1 == \"\" || rowData.ItemPath2 == \"\") {\r\n                 " +
"       itemPathText = rowData.ItemName.escapeHTML();\r\n                        ro" +
"wData.ItemPathText = row.ItemName;\r\n                    } else {\r\n              " +
"          itemPathText = createTaskPath(rowData);\r\n                        rowDa" +
"ta.ItemPathText = createTaskPathText(rowData);\r\n                    }\r\n         " +
"           return itemPathText;\r\n                }\r\n            }, {\r\n          " +
"      title: \"任务名称\", field: \"TaskName\", align: \"left\", halign: \"center\", width: " +
"100, formatter: function (value, rowData) {\r\n                    return \"<input " +
"id=\\\"taskname_\" + rowData.TaskId + \"\\\" flag=\\\"taskname\\\" />\";\r\n                }" +
"\r\n            }, {\r\n                title: \"设计\", field: \"SheJi\", align: \"left\", " +
"halign: \"center\", width: 80, formatter: function (value, rowData) {\r\n           " +
"         return \"<input id=\\\"task_sheji_\" + rowData.TaskId + \"\\\" flag=\\\"task_she" +
"ji\\\" specialtyid=\\\"\" + rowData.TaskSpecId + \"\\\" />\";\r\n                }\r\n       " +
"     }, {\r\n                title: \"校核\", field: \"JiaoHe\", align: \"left\", halign: " +
"\"center\", width: 80, formatter: function (value, rowData) {\r\n                   " +
" return \"<input id=\\\"task_jiaohe_\" + rowData.TaskId + \"\\\" flag=\\\"task_jiaohe\\\" s" +
"pecialtyid=\\\"\" + rowData.TaskSpecId + \"\\\" />\";\r\n                }\r\n            }" +
", {\r\n                title: \"计划开始时间\", field: \"PlanStart\", align: \"left\", halign:" +
" \"center\", width: 100, formatter: function (value, rowData) {\r\n                 " +
"   var s=rowData.DatePlanStart.substring(0,10);\r\n                    if(s == \"19" +
"00-01-01\") {\r\n                        s=\"\";\r\n                    }\r\n            " +
"        return \"<input id=\\\"taskplanstart_\" + rowData.TaskId + \"\\\" flag=\\\"taskpl" +
"anstart\\\" value=\\\"\"+s+\"\\\" />\";\r\n                }\r\n            }, {\r\n           " +
"     title: \"计划完成时间\", field: \"PlanFinish\", align: \"left\", halign: \"center\", widt" +
"h: 100, formatter: function (value, rowData) {\r\n                    var s=rowDat" +
"a.DatePlanFinish.substring(0,10);\r\n                    if(s == \"1900-01-01\") {\r\n" +
"                        s= \"\";\r\n                    }\r\n                    retur" +
"n \"<input id=\\\"taskplanfinish_\" + rowData.TaskId + \"\\\" flag=\\\"taskplanfinish\\\" v" +
"alue=\\\"\"+s+\"\\\" />\";\r\n                }\r\n            }\r\n        ]],\r\n        heig" +
"ht: height - 3,\r\n        width: width - 3,\r\n        selectOnCheck: false,\r\n     " +
"   checkOnSelect: false,\r\n        onLoadSuccess: function (datas) {\r\n           " +
" console.log(datas);\r\n            var _parent = $(\"#tbList_PlanBatch\").parent();" +
"\r\n            _parent.find(\"[flag=\'taskname\']\").textbox({\r\n                width" +
": 92,\r\n                value:\"设计任务\",\r\n                required:true\r\n           " +
" });\r\n            _parent.find(\"[flag=\'taskplanstart\'],[flag=\'taskplanfinish\']\")" +
".datebox({\r\n                width: 92\r\n            });\r\n            var specialt" +
"yID,empData;\r\n            _parent.find(\"[flag=\'task_sheji\']\").each(function () {" +
"\r\n                if(!specialtyID) {\r\n                    specialtyID=this.getAt" +
"tribute(\"specialtyid\") ;\r\n                }\r\n                empData= Enumerable" +
".From(_Qualification).Where(\"x=>x.QualificationSpecID==\" + this.getAttribute(\"sp" +
"ecialtyid\") + \"&&x.QualificationValue==");

            
            #line 114 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                                                                                                                                                      Write((int)DataModel.NodeType.设计);

            
            #line default
            #line hidden
WriteLiteral(@""").Select(""x=>{EmpID:x.EmpID,EmpName:x.EmpName}"").Distinct(""x=>x.EmpID"").ToArray();
                $(this).combobox({
                    valueField: 'EmpID',
                    textField: 'EmpName',
                    data: empData,
                    width: ""75px"",
                    panelMaxHeight: '360px',
                    required: true,
                    editable: false
                });
                if(empData.length>0) {
                    $(this).combobox(""setValue"",empData[0].EmpID);
                }
            });
            _parent.find(""[flag='task_jiaohe']"").each(function () {
                empData= Enumerable.From(_Qualification).Where(""x=>x.QualificationSpecID=="" + this.getAttribute(""specialtyid"") + ""&&x.QualificationValue==");

            
            #line 129 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                                                                                                                                                      Write((int)DataModel.NodeType.校对);

            
            #line default
            #line hidden
WriteLiteral(@""").Select(""x=>{EmpID:x.EmpID,EmpName:x.EmpName}"").Distinct(""x=>x.EmpID"").ToArray();
                $(this).combobox({
                    valueField: 'EmpID',
                    textField: 'EmpName',
                    data:empData,
                    width: ""75px"",
                    panelMaxHeight: '360px',
                    required: true,
                    editable: false
                });
                if(empData.length>0) {
                    $(this).combobox(""setValue"",empData[0].EmpID);
                }
            });
            if(!specialtyID) {
                return;
            }
            empData=Enumerable.From(_Qualification).Where(""x=>x.QualificationSpecID=="" + specialtyID + ""&&x.QualificationValue==");

            
            #line 146 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                                                                                                                            Write((int)DataModel.NodeType.审核);

            
            #line default
            #line hidden
WriteLiteral(@""").Select(""x=>{EmpID:x.EmpID,EmpName:x.EmpName}"").Distinct(""x=>x.EmpID"").ToArray();
            $(""#task_shenhe"").combobox({
                valueField: 'EmpID',
                textField: 'EmpName',
                data: empData,
                width: ""75px"",
                panelMaxHeight: '360px',
                required: true,
                editable: false
            });
            if(empData.length>0) {
                $(""#task_shenhe"").combobox(""setValue"",empData[0].EmpID);
            }
            empData=Enumerable.From(_Qualification).Where(""x=>x.QualificationSpecID=="" + specialtyID + ""&&x.QualificationValue==");

            
            #line 159 "..\..\Views\DesTask\SpecPlanBatch.cshtml"
                                                                                                                            Write((int)DataModel.NodeType.批准);

            
            #line default
            #line hidden
WriteLiteral("\").Select(\"x=>{EmpID:x.EmpID,EmpName:x.EmpName}\").Distinct(\"x=>x.EmpID\").ToArray(" +
");\r\n            $(\"#task_pizhun\").combobox({\r\n                valueField: \'EmpID" +
"\',\r\n                textField: \'EmpName\',\r\n                data: empData,\r\n     " +
"           width: \"75px\",\r\n                panelMaxHeight: \'360px\',\r\n           " +
"     required: true,\r\n                editable: false\r\n            });\r\n        " +
"    if(empData.length>0) {\r\n                $(\"#task_pizhun\").combobox(\"setValue" +
"\",empData[0].EmpID);\r\n            }\r\n        },\r\n        onDblClickRow:function(" +
"index,row){\r\n            //双击同步选中\r\n            var sheji=$(\"#task_sheji_\"+row.Ta" +
"skId).combobox(\"getValue\");\r\n            var jiaohe=$(\"#task_jiaohe_\"+row.TaskId" +
").combobox(\"getValue\");\r\n            var rows=$(\"#tbList_PlanBatch\").datagrid(\"g" +
"etRows\");\r\n            for(var i=0;i<rows.length;i++){\r\n                if(row.T" +
"askId==rows[i].TaskId) {\r\n                    continue;\r\n                }\r\n    " +
"            $(\"#task_sheji_\"+rows[i].TaskId).combobox(\"setValue\",sheji);\r\n      " +
"          $(\"#task_jiaohe_\"+rows[i].TaskId).combobox(\"setValue\",jiaohe);\r\n      " +
"      }\r\n        }\r\n    });\r\n    // 获取任务路径（纯文本）\r\n    function createTaskPathText" +
"(row) {\r\n        var jsonItemPath1 = row.ItemPath1;\r\n        var jsonItemPath2 =" +
" row.ItemPath2;\r\n        var separator = \" ＞ \";\r\n        var arrItemPath1 = JSON" +
".parse(jsonItemPath1);\r\n        var arrItemPath2 = JSON.parse(jsonItemPath2);\r\n " +
"       var path1 = (Enumerable.From(arrItemPath1).OrderBy(\"x=>x.rownum\").Select(" +
"function (x) { return x.text; }).ToArray());\r\n        var path2 = (Enumerable.Fr" +
"om(arrItemPath2).OrderBy(\"x=>x.rownum\").Select(function (x) { return x.text; })." +
"ToArray());\r\n        var path = [];\r\n        if (path1 != undefined && path1 != " +
"null) {\r\n            path.push(path1.join(separator));\r\n        }\r\n        if (p" +
"ath2 != undefined && path2 != null && path2.length > 0) {\r\n            path.push" +
"(path2.join(separator));\r\n        }\r\n\r\n        switch (row.ItemType) {\r\n        " +
"    case 5:\r\n                path.push(\"{0}\".format(row.ItemName));\r\n           " +
"     break;\r\n        }\r\n\r\n        return path.join(separator);\r\n\r\n    }\r\n\r\n    /" +
"/ 获取任务路径（带LinkButton）\r\n    function createTaskPath(row) {\r\n        var jsonItemP" +
"ath1 = row.ItemPath1;\r\n        var jsonItemPath2 = row.ItemPath2;\r\n\r\n        var" +
" separator = \"<i class=\'fa fa-caret-right\' style=\'margin: 0 5px;\'></i>\";\r\n      " +
"  var arrItemPath1 = JSON.parse(jsonItemPath1);\r\n        var arrItemPath2 = JSON" +
".parse(jsonItemPath2);\r\n\r\n        var path1 = (Enumerable.From(arrItemPath1).Ord" +
"erBy(\"x=>x.rownum\").Select(function (x) { return createLinkButton(x); }).ToArray" +
"());\r\n        var path2 = (Enumerable.From(arrItemPath2).OrderBy(\"x=>x.rownum\")." +
"Select(function (x) { return createLinkButton(x); }).ToArray());\r\n        var pa" +
"th = [];\r\n        var pathExcel = [];\r\n        if (path1 != undefined && path1 !" +
"= null) {\r\n            path.push(path1.join(separator));\r\n            pathExcel." +
"push(path1.join(\'→\'));\r\n        }\r\n        if (path2 != undefined && path2 != nu" +
"ll && path2.length > 0) {\r\n            path.push(path2.join(separator));\r\n      " +
"      pathExcel.push(path2.join(\'→\'));\r\n        }\r\n\r\n        switch (row.ItemTyp" +
"e) {\r\n            case 5:\r\n                path.push(\"<a href=\'javascript:void(0" +
");\' onclick=\'\'>{0}</a>\".format(row.ItemName));\r\n                pathExcel.push(r" +
"ow.ItemName);\r\n                break;\r\n        }\r\n        return path.join(separ" +
"ator);\r\n\r\n    }\r\n\r\n    function createLinkButton(data) {\r\n        //[{\"rownum\":1" +
",\"value\":34,\"text\":\"常州河海路110kV输变电2#主变扩建工程\",\"active\":\"TaskGroup\"},{...}]\r\n       " +
" var btn = \"\";\r\n        switch (data.active) {\r\n            case \"Project\":\r\n   " +
"         case \"TaskGroup\":\r\n            case \"Task\":\r\n                btn = \"<a " +
"href=\'javascript:void(0);\' onclick=\'\'>\" + data.text + \"</a>\";\r\n                b" +
"reak;\r\n            default:\r\n                btn = \"<a href=\'javascript:void(0);" +
"\' onclick=\'\'>\" + data.text + \"</a>\";\r\n                break;\r\n        }\r\n       " +
" return btn;\r\n    }\r\n\r\n\r\n    function getSaveData() {\r\n        var result=[];\r\n " +
"       var rows=$(\"#tbList_PlanBatch\").datagrid(\"getRows\");\r\n        for(var i=0" +
";i<rows.length;i++){\r\n            result.push({\r\n                Id:rows[i].Task" +
"Id,\r\n                TaskGroupId:rows[i].TaskGroupId,\r\n                TaskNumbe" +
"r:\'\',\r\n                TaskName:$(\"#taskname_\"+rows[i].TaskId).textbox(\"getText\"" +
"),\r\n                DatePlanStart: $(\"#taskplanstart_\"+rows[i].TaskId).datebox(\"" +
"getText\"),\r\n                DatePlanFinish:$(\"#taskplanfinish_\"+rows[i].TaskId)." +
"datebox(\"getText\"),\r\n                SheJi:$(\"#task_sheji_\"+rows[i].TaskId).comb" +
"obox(\"getValue\"),\r\n                JiaoHe:$(\"#task_jiaohe_\"+rows[i].TaskId).comb" +
"obox(\"getValue\")\r\n            });\r\n        }\r\n        return result;\r\n    }\r\n</s" +
"cript>");

        }
    }
}
#pragma warning restore 1591
