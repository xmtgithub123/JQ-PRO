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
    
    #line 4 "..\..\Views\EmpManage\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/EmpManage/list.cshtml")]
    public partial class _Views_EmpManage_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_EmpManage_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\EmpManage\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\EmpManage\list.cshtml"
                     Write(Url.Action("json", "EmpManage",new { @area="Engineering"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\EmpManage\list.cshtml"
                    Write(Url.Action("add","EmpManage",new {@area= "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\EmpManage\list.cshtml"
                     Write(Url.Action("edit", "EmpManage",new {@area= "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\EmpManage\list.cshtml"
                    Write(Url.Action("del", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl + \"?type=1\", //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n  " +
"              JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的" +
"字段\r\n                idField: \'Id\',\r\n                JQID: \'EmpManageGrid\',//数据表格" +
"ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\'" +
",//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingTyp" +
"e: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQExcludeCol: [1,2, " +
"10,11],//导出execl排除的列\r\n                JQSearch: {\r\n                    id: \'JQSe" +
"arch\',\r\n                    prompt: \'工程名称\',\r\n                    loadingType: \'d" +
"atagrid\',//默认值为datagrid可以不传\r\n                },\r\n                url: requestUrl" +
",//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                fitCo" +
"lumns: false,\r\n                nowrap: false,\r\n                toolbar: \'#EmpMan" +
"agePanel\',//工具栏的容器ID\r\n                columns: [[\r\n                    { field: " +
"\'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                   " +
" { field: \'Id\', align: \'center\', hidden:true },\r\n\t\t            { title: \'工程名称\', " +
"field: \'ProjName\', width: 250, align: \'center\', sortable: true },\r\n\t\t           " +
" { title: \'工程阶段\', field: \'ProjPhase\', width: 250, align: \'center\', sortable: tru" +
"e },\r\n\t\t            {\r\n\t\t                title: \'项目经理\', field: \'EngineeringEmpNa" +
"me\', width: 120, align: \'center\', sortable: true, formatter: function (value, ro" +
"w, index) {\r\n\t\t                    return \'<a class=\"easyui-linkbutton\" style=\"m" +
"argin:0 5px 0 5px;\" onclick=\"Pro(\\\'\' + row.EngineeringId + \'\\\',\\\'\' + row.Enginee" +
"ringEmpName + \'\\\',\\\'\' + row.Id + \'\\\')\">\' + row.EngineeringEmpName + \'</a>\';\r\n\t\t " +
"               }\r\n\t\t            },\r\n\t\t            {\r\n\t\t                title: \'安" +
"全员\', field: \'SafeEmpName\', width: 120, align: \'center\', sortable: true, formatte" +
"r: function (value, row, index) {\r\n\t\t                    return \'<a class=\"easyu" +
"i-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"Safe(\\\'\' + row.EngineeringId " +
"+ \'\\\',\\\'\' + row.SafeEmpName + \'\\\',\\\'\' + row.Id + \'\\\')\">\' + row.SafeEmpName + \'</" +
"a>\';\r\n\t\t                }\r\n\t\t            },\r\n\t\t            {\r\n\t\t                " +
"title: \'采购员\', field: \'CGEmpName\', width: 120, align: \'center\', sortable: true, f" +
"ormatter: function (value, row, index) {\r\n\t\t                    return \'<a class" +
"=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"CG(\\\'\' + row.Engineeri" +
"ngId + \'\\\',\\\'\' + row.CGEmpName + \'\\\',\\\'\' + row.Id + \'\\\')\">\' + row.CGEmpName + \'<" +
"/a>\';\r\n\t\t                }\r\n\t\t            },\r\n\t\t            {\r\n\t\t               " +
" title: \'技术员\', field: \'JSEmpName\', width: 120, align: \'center\', sortable: true, " +
"formatter: function (value, row, index) {\r\n\t\t                    return \'<a clas" +
"s=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"JS(\\\'\' + row.Engineer" +
"ingId + \'\\\',\\\'\' + row.JSEmpName + \'\\\',\\\'\' + row.Id + \'\\\')\">\' + row.JSEmpName + \'" +
"</a>\';\r\n\t\t                }\r\n\t\t            },\r\n\t\t            { title: \'文档员\', fie" +
"ld: \'WDEmpName\', width: 120, align: \'center\', sortable: true },\r\n\t\t            {" +
" title: \'现场负责人\', field: \'XCEmpName\', width: 120, align: \'center\', sortable: true" +
" },\r\n                    { field: \'JQFiles\', title: \'附件\', align: \'center\', width" +
": 120, JQIsExclude: true, JQRefTable: \'EmpManage\' },\r\n                    {\r\n   " +
"                     field: \"FlowProgress\", title: \"流程进度\", align: \"left\", halign" +
": \"center\", width: \"8%\", formatter: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"" +
"FlowStatusID\", \"FlowStatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 64 "..\..\Views\EmpManage\list.cshtml"
                                                                                                                                                                                                                                     Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                    },\r\n                    {\r\n                        title:" +
" \'操作\', field: \'EngineeringId\', width: 155, align: \'center\', sortable: true, form" +
"atter: function (value, row, index) {\r\n                            var html = \'\'" +
";\r\n                            html += \'<a class=\"easyui-linkbutton\" style=\"marg" +
"in:0 5px 0 5px;\" onclick=\"ProjNote(\' + row.EngineeringId + \',\' + row.DesTaskGrou" +
"pId + \')\">记事</a>\';\r\n                            html += \'<a class=\"easyui-linkbu" +
"tton\" style=\"margin:0 5px 0 5px;\" onclick=\"WD(\' + row.Id + \')\">资料上传</a>\'\r\n      " +
"                            + \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px " +
"0 5px;\" onclick=\"Weekly(\' + row.EngineeringId + \',\\\'\' + \'[\' + row.ProjNumber + \'" +
"]\' + row.ProjName + \' 周报列表\\\')\">周报</a>\';\r\n                            //}\r\n\r\n    " +
"                        return html;\r\n                        }\r\n               " +
"     }\r\n                ]],\r\n                onLoadSuccess: function (data) {\r\n " +
"                   $(\".datagrid-header-check\").html(\"\");\r\n                    $(" +
"\'#EmpManageGrid\').datagrid(\'clearSelections\');\r\n                    // 加载完毕后获取所有" +
"的checkbox遍历\r\n                    if (data.rows.length > 0) {\r\n                  " +
"      // 循环判断操作为新增的不能选择\r\n                        for (var i = 0; i < data.rows.l" +
"ength; i++) {\r\n                            //根据operate让某些行不可选\r\n                 " +
"           if (data.rows[i].FlowID && 1 != data.rows[i]._EditStatus) {\r\n        " +
"                        $(\"input[type=\'checkbox\']\")[i].disabled = true;\r\n       " +
"                     }\r\n                        }\r\n                    }\r\n      " +
"          },\r\n                onClickRow: function (rowIndex, rowData) {\r\n      " +
"              // 根据FlowID值,单击单选行不可用\r\n                    if (rowData.FlowID && 1" +
" != rowData._EditStatus) {\r\n                        $(\'#EmpManageGrid\').datagrid" +
"(\'unselectRow\', rowIndex);\r\n                    }\r\n                },\r\n         " +
"       onSelectAll: function (rows) {\r\n                    // 根据FlowID值,全选时某些行不选" +
"中\r\n                    $.each(rows, function (i, n) {\r\n                        i" +
"f (n.FlowID && 1 != n._EditStatus) {\r\n                            $(\'#EmpManageG" +
"rid\').datagrid(\'unselectRow\', i);\r\n                        }\r\n                  " +
"  });\r\n                }\r\n            });\r\n        });\r\n        function ProjNot" +
"e(engid, DesTaskGroupId) {\r\n            debugger;\r\n            JQ.dialog.dialogI" +
"frame({\r\n                title: \"项目记事\",\r\n                //title: \"填写项目记事\",\r\n   " +
"             url: \'");

            
            #line 113 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("ProjEventList", "DesEvent", new { @area = "design" }));

            
            #line default
            #line hidden
WriteLiteral(@"?fromType=1&taskGroupId=' + DesTaskGroupId + '&projID=' + engid,//+ '&empId=' + id,
                width: '1200',
                height: '100%',
                JQID: 'ProjTable',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }

        function Pro(EngineeringId, name, id) {
            JQ.dialog.dialogIframe({
                title: ""目标策划"",
                url: '");

            
            #line 125 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("list", "ProjManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?inType=2&EngineeringId=' + EngineeringId + '&name=' + name + '&empid=' + id,
                width: '1200',
                height: '100%',
                JQID: 'ProjTabled',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }

        function WD(tid) {
            JQ.dialog.dialog({
                title: ""资料上传"",
                url: '");

            
            #line 137 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("ProjDesReceiveList", "DesReceive", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?taskGroupId=' + tid,
                width: '1200',
                height: '100%',
                JQID: 'WDTabled',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }


        function JS(EngineeringId,name,id) {
            JQ.dialog.dialogIframe({
                title: ""实施策划"",
                url: '");

            
            #line 150 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("list", "SSManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?inType=2&EngineeringId=' + EngineeringId + '&name=' + name + '&empid=' + id,
                width: '1200',
                height: '100%',
                JQID: 'SSTabled',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }

        function Safe(EngineeringId, name,id) {
            JQ.dialog.dialogIframe({
                title: ""安全策划"",
                url: '");

            
            #line 162 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("list", "SafeManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?inType=2&EngineeringId=' + EngineeringId + '&name=' + name + '&empid=' + id,
                width: '1200',
                height: '100%',
                JQID: 'SafeTabled',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }

        function CG(EngineeringId, name,id) {
            JQ.dialog.dialogIframe({
                title: ""采购策划"",
                url: '");

            
            #line 174 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("list", "CGManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?inType=2&EngineeringId=' + EngineeringId + '&name=' + name + '&empid=' + id,
                width: '1200',
                height: '100%',
                JQID: 'CGTabled',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-list',
            });
        }

        function Weekly(engid, title) {
            JQ.dialog.dialog({
                title: title,
                url: '");

            
            #line 186 "..\..\Views\EmpManage\list.cshtml"
                 Write(Url.Action("weeklist", "EmpManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral("?taskGroupId=\' + engid,\r\n                width: \'1200\',\r\n                height: " +
"\'100%\',\r\n                JQID: \'ProjTable\',\r\n                JQLoadingType: \'dat" +
"agrid\',\r\n                iconCls: \'fa fa-list\',\r\n            });\r\n        }\r\n   " +
" </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"EmpManageGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"EmpManagePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EngineeringEmpName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
