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
    
    #line 4 "..\..\Views\EmpManage\EngineeringList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/EmpManage/EngineeringList.cshtml")]
    public partial class _Views_EmpManage_EngineeringList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_EmpManage_EngineeringList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\EmpManage\EngineeringList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\EmpManage\EngineeringList.cshtml"
                     Write(Url.Action("EngineeringJson", "EmpManage",new { @area="Engineering"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'export\'],//需要显示的按钮\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n" +
"                idField: \'Id\',\r\n                JQID: \'EmpManageGrid\',//数据表格ID\r\n" +
"                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹" +
"出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'" +
"datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQExcludeCol: [4],//导出ex" +
"ecl排除的列\r\n                JQSearch: {\r\n                    id: \'JQSearch\',\r\n     " +
"               prompt: \'项目名称\',\r\n                    loadingType: \'datagrid\',//默认" +
"值为datagrid可以不传\r\n                },\r\n                url: requestUrl,//请求的地址\r\n   " +
"             checkOnSelect: false,//是否包含check\r\n                fitColumns: false" +
",\r\n                nowrap: false,\r\n                toolbar: \'#EmpManagePanel\',//" +
"工具栏的容器ID\r\n                columns: [[\r\n\t\t            { title: \'项目编号\', field: \'Pr" +
"ojNumber\', width: 120, align: \'center\', sortable: true },\r\n\t\t            { title" +
": \'项目名称\', field: \'ProjName\', width: 250, align: \'center\', sortable: true },\r\n\t\t " +
"           { title: \'项目阶段\', field: \'ProjPhase\', width: 100, align: \'center\', sor" +
"table: true },\r\n                    {\r\n                        title: \'操作\', fiel" +
"d: \'EngineeringId\', width: 155, align: \'center\', sortable: true, formatter: func" +
"tion (value, row, index) {\r\n                            var html = \'\';\r\n\r\n      " +
"                      if (row.IsWDEmp > 0) {\r\n                                ht" +
"ml += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"ProjNot" +
"e(\' + row.EngineeringId + \',\'+ row.DesTaskGroupId +\')\">记事</a>\';\r\n               " +
"                 html += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px" +
";\" onclick=\"WD(\' + row.Id + \')\">资料上传</a>\'\r\n                            }\r\n      " +
"                      //if (row.IsXCEmp > 0) {\r\n                                " +
"html += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"Weekl" +
"y(\' + row.EngineeringId + \',\\\'\' + \'[\' + row.ProjNumber + \']\' + row.ProjName + \' " +
"周报列表\\\')\">周报</a>\';\r\n                            //}\r\n\r\n                          " +
"  return html;\r\n                        }\r\n                    }\r\n              " +
"  ]]\r\n            });\r\n        });\r\n\r\n        // 填写项目记事\r\n        function ProjNo" +
"te(engid, DesTaskGroupId) {\r\n            debugger;\r\n            JQ.dialog.dialog" +
"Iframe({\r\n                title: \"项目记事\",\r\n                //title: \"填写项目记事\",\r\n  " +
"              url: \'");

            
            #line 58 "..\..\Views\EmpManage\EngineeringList.cshtml"
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

        function WD(tid) {
            JQ.dialog.dialog({
                title: ""资料上传"",
                url: '");

            
            #line 70 "..\..\Views\EmpManage\EngineeringList.cshtml"
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

        // 周报列表
        function Weekly(engid, title) {
            JQ.dialog.dialog({
                title: title,
                url: '");

            
            #line 83 "..\..\Views\EmpManage\EngineeringList.cshtml"
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

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591