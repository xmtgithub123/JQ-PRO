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
    
    #line 4 "..\..\Views\DesEvent\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesEvent/list.cshtml")]
    public partial class _Views_DesEvent_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesEvent_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\DesEvent\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\DesEvent\list.cshtml"
                     Write(Url.Action("ProjectPlanListJson", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?FormMenu=EngineerLog\';\r\n        $(function () {\r\n            JQ.datagrid.datagri" +
"d({\r\n                JQButtonTypes: [\'export\'],//需要显示的按钮\r\n                JQPrim" +
"aryID: \'Id\',//主键的字段\r\n                JQID: \'DesTaskGrid\',//数据表格ID\r\n             " +
"   JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n       " +
"         JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数" +
"据表格类型 [datagrid,datagrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n          " +
"      url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n" +
"                JQMergeCells: { fields: [\'ProjNumber\', \'ProjName\'], Only: \'Id\' }" +
",//当字段值相同时会自动的跨行(只对相邻有效)\r\n                toolbar: \'#DesTaskPanel\',//工具栏的容器ID\r\n " +
"               columns: [[\r\n                    { field: \'ck\', align: \'center\', " +
"checkbox: true, JQIsExclude: true },\r\n\t\t            { title: \'项目编号\', field: \'Pro" +
"jNumber\', width: \"20%\", halign: \'center\', align: \'left\', sortable: true },\r\n\t\t  " +
"          { title: \'项目名称\', field: \'ProjName\', width: \"32%\", halign: \'center\', al" +
"ign: \'left\', sortable: true },\r\n                    { title: \'阶段\', field: \'ProjP" +
"haseName\', width: \"20%\", halign: \'center\', align: \'left\', sortable: true },\r\n   " +
"                 { title: \'负责人\', field: \'ProjPhaseEmpName\', width: 100, halign: " +
"\'center\', align: \'center\', sortable: true },\r\n\t\t            { title: \'计划开始\', fie" +
"ld: \'DatePlanStart\', width: 100, halign: \'center\', align: \'center\', sortable: tr" +
"ue, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t            { title: \'计划结束\', " +
"field: \'DatePlanFinish\', width: 100, halign: \'center\', align: \'center\', sortable" +
": true, formatter: JQ.tools.DateTimeFormatterByT },\r\n                    {\r\n    " +
"                    title: \'操作\', field: \'Id\', width: 40, halign: \'center\', align" +
": \'center\', formatter: function (value, row, index) {\r\n                         " +
"   var _titleName = \"[\" + row.ProjNumber.escapeHTML() + \"]\" + row.ProjName.escap" +
"eHTML();\r\n                            return \"<a class=\\\"easyui-linkbutton\\\" dat" +
"a-options=\\\"plain:true,iconCls:\'fa fa-trash\'\\\" onclick=\'openProjEventInfo(\\\"\" + " +
"_titleName + \"\\\",\" + row.Id + \",\" + row.TaskGroupId + \")\'>记事</a>\";\r\n            " +
"            }\r\n                    }\r\n                ]],\r\n                JQExc" +
"ludeCol: [1, 8],//导出execl排除的列\r\n            });\r\n\r\n            $(\"#JQSearch\").tex" +
"tbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-se" +
"arch\',\r\n                height: 25,\r\n                prompt: \'项目名称、编号\',\r\n       " +
"         onClickButton: function () {\r\n                    JQ.datagrid.searchGri" +
"d(\r\n                        {\r\n                            dgID: \'DesTaskGrid\',\r" +
"\n                            loadingType: \'datagrid\',\r\n                         " +
"   queryParams: null\r\n                        });\r\n                }\r\n          " +
"  });\r\n\r\n        });\r\n\r\n        function openProjEventInfo(title, projId, taskGr" +
"oupId) {\r\n            window.top.addTab(\"记事:\" + title, \'");

            
            #line 58 "..\..\Views\DesEvent\list.cshtml"
                                         Write(Url.Action("ProjEventList"));

            
            #line default
            #line hidden
WriteLiteral("?projID=\' + projId + \'&taskGroupId=\' + taskGroupId, \"\");\r\n        }\r\n\r\n        wi" +
"ndow.refreshGrid = function () {\r\n            $(\"#JQSearch\").textbox(\"button\").c" +
"lick();\r\n        }\r\n    </script>\r\n");

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

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'p.ProjNumber,p.ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
