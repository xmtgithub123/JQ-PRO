﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
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
    
    #line 1 "..\..\Views\IsoFormProjectDeliver\FilterList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjectDeliver/FilterList.cshtml")]
    public partial class _Views_IsoFormProjectDeliver_FilterList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjectDeliver_FilterList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.datagrid.datagrid({
            JQButtonTypes: ['moreSearch'],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'InfoListGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '1200',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQExcludeCol: [1, 16],//导出execl排除的列
            //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
            //JQFields: ["""", """"],//追加的字段名
            url: '");

            
            #line 17 "..\..\Views\IsoFormProjectDeliver\FilterList.cshtml"
             Write(Url.Action("GeTaskListJson", "IsoFormProjectDeliver", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("?ProjId=");

            
            #line 17 "..\..\Views\IsoFormProjectDeliver\FilterList.cshtml"
                                                                                                  Write(ViewBag.ProjId);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            checkOnSelect: true,\r\n            toolbar: \'#InfoListPanel\',//工具栏" +
"的容器ID\r\n            columns: [[\r\n                  { field: \'ck\', align: \'center\'" +
", checkbox: true },\r\n              { title: \'项目编号\', field: \'ProjNumber\', width: " +
"120, align: \'left\', sortable: true },\r\n              { title: \'项目名称\', field: \'Pr" +
"ojName\', width: 280, align: \'left\', sortable: true },\r\n              { title: \'阶" +
"段\', field: \'PhaseName\', width: 90, align: \'left\', sortable: true },\r\n           " +
"   { title: \'专业\', field: \'SpecialtyName\', width: 80, align: \'left\', sortable: tr" +
"ue },\r\n              { title: \'卷册名称\', field: \'TaskName\', width: 160, align: \'lef" +
"t\', sortable: true },\r\n              { title: \'设计人员\', field: \'TaskEmpName\', widt" +
"h: 80, align: \'center\', sortable: true },\r\n              {\r\n                  ti" +
"tle: \'计划完成时间\', field: \'DatePlanFinish\', width: 80, align: \'center\', sortable: tr" +
"ue, formatter: JQ.tools.DateTimeFormatterByT\r\n              }\r\n            ]]\r\n " +
"       });\r\n        $(\"#JQSearch\").textbox({\r\n            buttonText: \'筛选\',\r\n   " +
"         buttonIcon: \'fa fa-search\',\r\n            height: 25,\r\n            promp" +
"t: \'项目编号，项目名称\',\r\n            queryOptions: { \'Key\': \'TextCondtion\', \'Contract\': " +
"\'like\' },\r\n            onClickButton: function () {\r\n                JQ.datagrid" +
".searchGrid(\r\n                    {\r\n                        dgID: \'InfoListGrid" +
"\',\r\n                        loadingType: \'datagrid\',\r\n                        qu" +
"eryParams: {\r\n                            KeyJQSearch: $(\"#JQSearch\").val(),\r\n  " +
"                      }\r\n                    });\r\n            }\r\n        });\r\n  " +
"  });\r\n\r\n\r\n    function selectProj() {\r\n        var row = $(\'#InfoListGrid\').dat" +
"agrid(\'getSelections\');\r\n        if (row.length < 1) {\r\n            window.top.J" +
"Q.dialog.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n        }\r\n        var i" +
"ds = [];\r\n        var _rows = [];\r\n        for (var i = 0; i < row.length; i++) " +
"{\r\n            ids.push(row[i][\"Id\"]);\r\n            _rows.push(row[i]);\r\n       " +
" }\r\n\r\n        _ProjCallBack(ids);\r\n        JQ.dialog.dialogClose();\r\n    }\r\n\r\n\r\n" +
"</script>\r\n<table");

WriteLiteral(" id=\"InfoListGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"InfoListPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProj()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n");

WriteLiteral("    ");

            
            #line 77 "..\..\Views\IsoFormProjectDeliver\FilterList.cshtml"
Write(BaseData.getBases(new Params() { engName = "ProjectPhase", queryOptions = "{ Key:'TaskPhaseId', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral("  />\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
