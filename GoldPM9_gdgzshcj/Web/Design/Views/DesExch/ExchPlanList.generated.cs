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
    
    #line 4 "..\..\Views\DesExch\ExchPlanList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesExch/ExchPlanList.cshtml")]
    public partial class _Views_DesExch_ExchPlanList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesExch_ExchPlanList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\DesExch\ExchPlanList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\DesExch\ExchPlanList.cshtml"
                     Write(Url.Action("SpecPlanListJson", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'export\'],//需要显示的按钮\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n" +
"                JQID: \'DesTaskGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息" +
"\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'1" +
"00%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,datag" +
"rid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                url: requestUrl" +
",//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                JQExc" +
"ludeCol: [1, 10],//导出execl排除的列\r\n                JQMergeCells: { fields: [\'ProjNu" +
"mber\', \'ProjName\'], Only: \'Id\' },//当字段值相同时会自动的跨行(只对相邻有效)\r\n                toolba" +
"r: \'#DesTaskPanel\',//工具栏的容器ID\r\n                columns: [[\r\n                    " +
"{ field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t        " +
"    { title: \'项目编号\', field: \'ProjNumber\', width: 100, align: \'center\', sortable:" +
" true },\r\n\t\t            { title: \'项目名称\', field: \'ProjName\', width: 300, align: \'" +
"left\', sortable: true },\r\n                    { title: \'阶段\', field: \'ProjPhaseNa" +
"me\', width: 100, align: \'center\', sortable: true },\r\n                    { title" +
": \'阶段负责人\', field: \'ProjPhaseEmpName\', width: 100, align: \'center\', sortable: tru" +
"e },\r\n                    { title: \'专业\', field: \'ProjSpecName\', width: 100, alig" +
"n: \'center\', sortable: true },\r\n                    { title: \'专业负责人\', field: \'Pr" +
"ojSpecEmpName\', width: 100, align: \'center\', sortable: true },\r\n\t\t            { " +
"title: \'计划开始\', field: \'DatePlanStart\', width: 100, align: \'center\', sortable: tr" +
"ue, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t            { title: \'计划结束\', " +
"field: \'DatePlanFinish\', width: 100, align: \'center\', sortable: true, formatter:" +
" JQ.tools.DateTimeFormatterByT },\r\n                    {\r\n                      " +
"  title: \'操作\', field: \'Id\', width: 120, align: \'center\', formatter: function (va" +
"lue, row, index) {\r\n                            //var _titleName = \"[\" + row.Pro" +
"jNumber.escapeHTML() + \"]\" + row.ProjName.escapeHTML();\r\n                       " +
"     var _titleName = \"提资策划：[{0}]{1} &gt; {2} &gt; {3}\".format(row.ProjNumber.es" +
"capeHTML(), row.ProjName.escapeHTML(), row.ProjPhaseName, row.ProjSpecName);\r\n\r\n" +
"                            //var _titleName = \"[\" + decodeURIComponent(row.Proj" +
"Number) + \"]\" + decodeURIComponent(row.ProjName);\r\n                            /" +
"/return \"<a class=\\\"easyui-linkbutton\\\" data-options=\\\"plain:true,iconCls:\'fa fa" +
"-trash\'\\\" onclick=\\\"openProjPlanInfo(\'\" + _titleName + \"\',\" + row.Id + \",\" + row" +
".TaskGroupId + \")\\\">提资策划</a>\";//openProjPlanInfo(\\\"\" + _titleName + \"\\\",\" + row." +
"Id + \",\" + row.TaskGroupId + \")\r\n                            return \"<a class=\\\"" +
"easyui-linkbutton\\\" data-options=\\\"plain:true,iconCls:\'fa fa-trash\'\\\" onclick=\\\"" +
"openExchPlanInfo(\'{0}\',{1},{2},{3})\\\">提资策划</a>\".format(_titleName, row.Id, row.T" +
"askGroupId, row.ProjSpecId);\r\n\r\n                        }\r\n                    }" +
"\r\n                ]]\r\n            });\r\n\r\n            $(\"#JQSearch\").textbox({\r\n " +
"               buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n " +
"               height: 25,\r\n                prompt: \'项目名称、编号\',\r\n                " +
"onClickButton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n     " +
"                   {\r\n                            dgID: \'DesTaskGrid\',\r\n        " +
"                    loadingType: \'datagrid\',\r\n                            queryP" +
"arams: null\r\n                        });\r\n                }\r\n            });\r\n\r\n" +
"        });\r\n\r\n        function openExchPlanInfo(title, projId, taskGroupId, tas" +
"kSpecId) {\r\n            var url = \'");

            
            #line 65 "..\..\Views\DesExch\ExchPlanList.cshtml"
                  Write(Url.Action("ExchPlanInfo"));

            
            #line default
            #line hidden
WriteLiteral(@"?tabId=2&projID=' + projId + '&taskGroupId=' + taskGroupId + '&taskSpecId=' + taskSpecId;

            JQ.dialog.dialogIframe({
                title: title,
                url: url,
                width: 1200,
                height: 800
            });
        }
    </script>
");

            
            #line 75 "..\..\Views\DesExch\ExchPlanList.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n\r\n    <table");

WriteLiteral(" id=\"DesTaskGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"DesTaskPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral("></span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ProjNumber,ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
