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
    
    #line 1 "..\..\Views\IsoAging\infoList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoAging/infoList.cshtml")]
    public partial class _Views_IsoAging_infoList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoAging_infoList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

    $(function () {
        for (var i = 0; i < window.top.length; i++) {
            var _top = window.top[i];
            if (_top.document.getElementById(""TaskIds"")) {
                $(""#DesTaskIds"").val(_top.document.getElementById(""TaskIds"").value)
                break;
            }
        }
        if ($(""#DesTaskIds"").val() == """") {
            return;
        }
        JQ.datagrid.datagrid({
            JQButtonTypes: ['export', 'moreSearch'],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'InfoListGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQExcludeCol: [8],//导出execl排除的列
            //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
            //JQFields: ["""", """"],//追加的字段名
            url: '");

            
            #line 27 "..\..\Views\IsoAging\infoList.cshtml"
             Write(Url.Action("GeTaskListJson", "IsoAnalysis", new { @area= "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            checkOnSelect: true,\r\n            toolbar: \'#InfoListPanel\',//工具栏" +
"的容器ID\r\n            columns: [[\r\n              { title: \'项目编号\', field: \'ProjNumbe" +
"r\', width: \"10%\", align: \'left\', sortable: true },\r\n              { title: \'项目名称" +
"\', field: \'ProjName\', width: \"20%\", align: \'left\', sortable: true },\r\n          " +
"    { title: \'阶段\', field: \'PhaseName\', width: \"10%\", align: \'left\', sortable: tr" +
"ue },\r\n              { title: \'专业\', field: \'SpecialtyName\', width: 80, align: \'l" +
"eft\', sortable: true },\r\n              { title: \'卷册名称\', field: \'TaskName\', width" +
": 120, align: \'left\', sortable: true },\r\n              { title: \'计划完成日期\', field:" +
" \'DatePlanFinish\', width: 80, align: \'left\', sortable: true,formatter: JQ.tools." +
"DateTimeFormatterByT },\r\n              { title: \'实际完成时间\', field: \'DateActualFini" +
"sh\', width: 80, align: \'left\', sortable: true, formatter: JQ.tools.DateTimeForma" +
"tterByT },\r\n              {\r\n                  field: \"opt\", title: \"查看\", align:" +
" \"center\", halign: \"center\", width: \"60px\", formatter: function (value, row, ind" +
"ex) {\r\n                      debugger;\r\n                      return \"<a href=\\\"" +
"#\\\" class=\\\"easyui-linkbutton\\\"  style=\\\"line-height:22px;\\\" onclick=\\\"ShowTaskI" +
"nfoDialogue(\'\" + row.Id + \"\')\\\">查看</>\";\r\n                  }\r\n              }\r\n\r" +
"\n            ]],\r\n            queryParams: {\r\n                DesTaskIds: $(\"#De" +
"sTaskIds\").val()\r\n            }\r\n        });\r\n        $(\"#JQSearch\").textbox({\r\n" +
"            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa-search\',\r\n        " +
"    height: 25,\r\n            prompt: \'项目编号，项目名称\',\r\n            queryOptions: { \'" +
"Key\': \'TextCondtion\', \'Contract\': \'like\' },\r\n            onClickButton: function" +
" () {\r\n                JQ.datagrid.searchGrid(\r\n                    {\r\n         " +
"               dgID: \'InfoListGrid\',\r\n                        loadingType: \'data" +
"grid\',\r\n                        queryParams: {\r\n                            DesT" +
"askIds: $(\"#DesTaskIds\").val()\r\n                        }\r\n                    }" +
");\r\n            }\r\n        });\r\n    });\r\n    function closedialog() {\r\n        J" +
"Q.dialog.dialogClose();\r\n    }\r\n\r\n    function ShowTaskInfoDialogue(Id) {\r\n     " +
"   JQ.dialog.dialog({\r\n            title: \"查看设计任务\",\r\n            url: \"");

            
            #line 75 "..\..\Views\IsoAging\infoList.cshtml"
             Write(Url.Action("TaskInfo", "DesTask", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?Id=\" + Id,\r\n            width: \'1200\',\r\n        height: \'800\',\r\n        toolbar:" +
" null,\r\n        iconCls: \'fa fa-list\',\r\n        });\r\n    }\r\n</script>\r\n\r\n\r\n\r\n<ta" +
"ble");

WriteLiteral(" id=\"InfoListGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"InfoListPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-close\'\"");

WriteLiteral(" onclick=\"closedialog()\"");

WriteLiteral(">关闭</a>\r\n    </span>\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n");

WriteLiteral("    ");

            
            #line 92 "..\..\Views\IsoAging\infoList.cshtml"
Write(BaseData.getBases(new Params() { engName = "ProjectPhase", queryOptions = "{ Key:'TaskPhaseId', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'TextCondtion\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"DesTaskIds\"");

WriteLiteral(" name=\"DesTaskIds\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591