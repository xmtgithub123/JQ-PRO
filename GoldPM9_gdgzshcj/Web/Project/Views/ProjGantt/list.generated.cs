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
    
    #line 7 "..\..\Views\ProjGantt\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProjGantt/list.cshtml")]
    public partial class _Views_ProjGantt_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ProjGantt_list_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\ProjGantt\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 9 "..\..\Views\ProjGantt\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 11 "..\..\Views\ProjGantt\list.cshtml"
                     Write(Url.Action("ProjectPlanListJson", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?FormMenu=ProjGanttList\';\r\n        $(function () {\r\n            JQ.datagrid.datag" +
"rid({\r\n                JQButtonTypes: [\'export\'],//需要显示的按钮\r\n                JQPr" +
"imaryID: \'Id\',//主键的字段\r\n                JQID: \'DesTaskGrid\',//数据表格ID\r\n           " +
"     JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n     " +
"           JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',/" +
"/数据表格类型 [datagrid,datagrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n        " +
"        url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check" +
"\r\n                JQMergeCells: { fields: [\'ProjNumber\', \'ProjName\'], Only: \'Id\'" +
" },//当字段值相同时会自动的跨行(只对相邻有效)\r\n                toolbar: \'#DesTaskPanel\',//工具栏的容器ID\r" +
"\n                JQExcludeCol: [1, 8],//导出execl排除的列\r\n                columns: [[" +
"\r\n                    { field: \'ck\', align: \'center\', checkbox: true, JQIsExclud" +
"e: true },\r\n\t\t            { title: \'项目编号\', field: \'ProjNumber\', width: \"10%\", ha" +
"lign: \'center\', align: \'left\', sortable: true },\r\n\t\t            { title: \'项目名称\'," +
" field: \'ProjName\', width: \"40%\", align: \'left\', sortable: true },\r\n            " +
"        { title: \'阶段\', field: \'ProjPhaseName\', width: \"10%\", align: \'left\', sort" +
"able: true },\r\n                    { title: \'负责人\', field: \'ProjPhaseEmpName\', wi" +
"dth: \'10%\', align: \'center\', sortable: true },\r\n\t\t            { title: \'计划开始\', f" +
"ield: \'DatePlanStart\', width: \'10%\', align: \'center\', sortable: true, formatter:" +
" JQ.tools.DateTimeFormatterByT },\r\n\t\t            { title: \'计划结束\', field: \'DatePl" +
"anFinish\', width: \'10%\', align: \'center\', sortable: true, formatter: JQ.tools.Da" +
"teTimeFormatterByT },\r\n                    {\r\n                        title: \'操作" +
"\', field: \'Id\', width: \'6%\', align: \'center\', formatter: function (value, row, i" +
"ndex) {\r\n                            var _titleName = \"关键节点：[{0}]{1} &gt; {2}\".f" +
"ormat(row.ProjNumber.escapeHTML(), row.ProjName.escapeHTML(), row.ProjPhaseName)" +
";\r\n                            return \"<a class=\\\"easyui-linkbutton\\\" data-optio" +
"ns=\\\"plain:true,iconCls:\'fa fa-trash\'\\\" onclick=\'openProjPlanInfo(\\\"\" + _titleNa" +
"me + \"\\\",\" + row.Id + \",\" + row.TaskGroupId + \")\'>关键节点</a>\";\r\n                  " +
"      }\r\n                    }\r\n                ]]\r\n            });\r\n\r\n         " +
"   $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n                " +
"buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n                prompt" +
": \'项目名称、编号\',\r\n                onClickButton: function () {\r\n                    " +
"JQ.datagrid.searchGrid(\r\n                        {\r\n                            " +
"dgID: \'DesTaskGrid\',\r\n                            loadingType: \'datagrid\',\r\n    " +
"                        queryParams: null\r\n                        });\r\n        " +
"        }\r\n            });\r\n\r\n        });\r\n\r\n        function openProjPlanInfo(t" +
"itle, projId, taskGroupId) {\r\n            var url = \'");

            
            #line 62 "..\..\Views\ProjGantt\list.cshtml"
                  Write(Url.Action("ProjectGantt", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral(@"?tabId=4&projID=' + projId + '&taskGroupId=' + taskGroupId + '&from=gantt';       
            //window.top.addTab(title, url, """");
            JQ.dialog.dialogIframe({
                title: title,
                url: url,
                width: 1200,
                height: 800
            });
        }

    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"DesTaskGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"DesTaskPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-play-circle\'\"");

WriteLiteral(" onclick=\"\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">对比</a>\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'g.ProjNumber,g.ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
