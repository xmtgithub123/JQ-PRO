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
    
    #line 2 "..\..\Views\DesPlanTable\selectPlan.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesPlanTable/selectPlan.cshtml")]
    public partial class _Views_DesPlanTable_selectPlan_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesPlanTable_selectPlan_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .rwpdialogdiv {\r\n        padding: 0px;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        window.top.selectProjectPlan = undefined;
        $("".dialog-toolbar :last"").hide();
        JQ.datagrid.datagrid({
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'desProjectPlan',//数据表格ID
            JQDialogTitle: '选择工程',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: true,
            pagination: true,
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 22 "..\..\Views\DesPlanTable\selectPlan.cshtml"
             Write(Url.Action("JsonPlan", "DesPlanTable"));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#Bu" +
"ssProjectPanel\',//工具栏的容器ID\r\n            columns: [[\r\n                { field: \'c" +
"k\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                { tit" +
"le: \'项目编号\', field: \'ProjNumber\', width: \'15%\', align: \'left\', },\r\n              " +
"  { title: \'项目名称\', field: \'ProjName\', width: \'35%\', align: \'center\', },\r\n       " +
"         { title: \'项目阶段\', field: \'PhaseName\', width: \'10%\', align: \'center\', },\r" +
"\n                { title: \'设总\', field: \'ProjEmpName\', width: \'10%\', align: \'cent" +
"er\', },\r\n                { title: \'计划开始\', field: \'DatePlanStart\', width: \'10%\', " +
"align: \'center\', formatter: JQ.tools.DateTimeFormatterByT },\r\n                { " +
"title: \'计划完成\', field: \'DatePlanFinish\', width: \'10%\', align: \'center\', formatter" +
": JQ.tools.DateTimeFormatterByT },\r\n            ]],\r\n        });\r\n        $(\"#JQ" +
"Search\").textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa f" +
"a-search\',\r\n            height: 25,\r\n            prompt: \'项目编号、项目名称\',\r\n         " +
"   onClickButton: function () {\r\n                JQ.datagrid.searchGrid({\r\n     " +
"               dgID: \'desProjectPlan\',\r\n                    loadingType: \'datagr" +
"id\',\r\n                    queryParams: null\r\n                });\r\n            }\r" +
"\n        });\r\n    });\r\n\r\n    function selectProj() {\r\n        var row = $(\'#desP" +
"rojectPlan\').datagrid(\'getChecked\');\r\n        if (row.length < 1) {\r\n           " +
" window.top.JQ.dialog.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n        }\r\n" +
"       \r\n        window.top.selectProjectPlan = row[0];\r\n        JQ.dialog.dialo" +
"gClose();\r\n    }\r\n\r\n</script>\r\n\r\n<table");

WriteLiteral(" id=\"desProjectPlan\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"BussProjectPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProj()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n");

WriteLiteral("    ");

            
            #line 70 "..\..\Views\DesPlanTable\selectPlan.cshtml"
Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjectPhase", queryOptions = "{ Key:'TaskGroupPhaseId', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'p.ProjNumber,p.ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n    ");

WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
