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
    
    #line 4 "..\..\Views\IsoRWPSTZD\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoRWPSTZD/list.cshtml")]
    public partial class _Views_IsoRWPSTZD_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoRWPSTZD_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoRWPSTZD\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        var requestUrl = \'");

            
            #line 8 "..\..\Views\IsoRWPSTZD\list.cshtml"
                     Write(Url.Action("json", "IsoRWPSTZD", new { @area= "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n           addUrl = \'");

            
            #line 9 "..\..\Views\IsoRWPSTZD\list.cshtml"
                Write(Url.Action("add", "IsoRWPSTZD", new {@area= "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n           editUrl = \'");

            
            #line 10 "..\..\Views\IsoRWPSTZD\list.cshtml"
                 Write(Url.Action("edit", "IsoRWPSTZD", new {@area= "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n           delUrl = \'");

            
            #line 11 "..\..\Views\IsoRWPSTZD\list.cshtml"
                Write(Url.Action("del", "IsoRWPSTZD", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral(@"';

        $(function () {

            JQ.datagrid.datagrid({
                JQButtonTypes: ['add', 'edit', 'del', 'export', 'moreSearch'],//需要显示的按钮
                JQAddUrl: addUrl,//添加的路径
                JQEditUrl: editUrl,//编辑的路径
                JQDelUrl: '");

            
            #line 19 "..\..\Views\IsoRWPSTZD\list.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\',//删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'IsoR" +
"WPSTZDGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n            " +
"    JQWidth: \'1024\',//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n         " +
"       JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                J" +
"QExcludeCol: [1, 9, 10, 11],//导出execl排除的列\r\n                JQIsSearch: true,\r\n  " +
"              url: requestUrl,//请求的地址\r\n                checkOnSelect: true,\r\n   " +
"             toolbar: \'#IsoRWPSTZDPanel\',//工具栏的容器ID\r\n                columns: [[" +
"\r\n                  { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude:" +
" true },\r\n\t\t          { title: \'项目编号\', field: \'ProjNumber\', width: \"8%\", align: " +
"\'left\', halign: \"center\", sortable: true },\r\n\t\t          { title: \'项目名称\', field:" +
" \'ProjName\', width: \"20%\", align: \'left\', halign: \"center\", sortable: true },\r\n\t" +
"\t          { title: \'阶段名称\', field: \'ProjPhaseName\', width: \"6%\", align: \'left\', " +
"halign: \"center\", sortable: true },\r\n\t\t          { title: \'任务创建依据\', field: \'Task" +
"PursuantName\', width: \"8%\", align: \'left\', halign: \"center\", sortable: true },\r\n" +
"                  { title: \'工程等级\', field: \'ProjectClassName\', width: \"8%\", align" +
": \'left\', halign: \"center\", sortable: true },\r\n                  { title: \'评审方式\'" +
", field: \'AppraisalWayName\', width: \"8%\", align: \'left\', halign: \"center\", sorta" +
"ble: true },\r\n                  { title: \'任务内容\', field: \'TaskContents1\', width: " +
"\"8%\", align: \'left\', halign: \"center\", sortable: true },\r\n                  { fi" +
"eld: \'JQFiles\', title: \'附件\', align: \'center\', width: \"4%\", JQIsExclude: true, JQ" +
"RefTable: \'IsoBCRWTZD\' },\r\n                   {\r\n                       field: \"" +
"FlowProgress\", title: \"流程进度\", align: \"left\", halign: \"center\", width: \"8%\", form" +
"atter: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"FlowStatusID\", \"FlowStatusNam" +
"e\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 42 "..\..\Views\IsoRWPSTZD\list.cshtml"
                                                                                                                                                                                                                                    Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                   },\r\n                   {\r\n                       field: \'o" +
"pt\', title: \'操作\', width: \"4%\", align: \'center\', JQIsExclude: true,\r\n            " +
"           formatter: function (value, row, index) {\r\n                          " +
" var title = \"查看\";\r\n                           if (row._EditStatus == 1) {\r\n    " +
"                           title = \"修改\";\r\n                           }\r\n        " +
"                   else if (row._EditStatus == 2) {\r\n                           " +
"    title = \"处理\";\r\n                           }\r\n                           retu" +
"rn \'<a class=\"easyui-linkbutton\" onclick=\"Info(\' + row.Id + \')\">\' + title + \'</a" +
">\';\r\n                       }\r\n                   }\r\n                ]],\r\n      " +
"          onBeforeCheck: function (rowIndex, rowData) {\r\n                    ret" +
"urn rowData._AllowCheck;\r\n                },\r\n                onClickRow: functi" +
"on (rowIndex, rowData) {\r\n                    if (!rowData._AllowCheck) {\r\n     " +
"                   $(this).datagrid(\'unselectRow\', rowIndex);\r\n                 " +
"   }\r\n                },\r\n                onLoadSuccess: function (data) {\r\n    " +
"                $(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"styl" +
"e\", \"display:none\");\r\n                    var rowViews = $(\"#IsoRWPSTZDGrid\").pa" +
"rent().find(\".datagrid-view2 .datagrid-body tr.datagrid-row\");\r\n                " +
"    for (var i = 0; i < data.rows.length; i++) {\r\n                        if (!d" +
"ata.rows[i]._AllowCheck) {\r\n                            rowViews.filter(\"[datagr" +
"id-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"disabled\", \"d" +
"isabled\");\r\n                        }\r\n                    }\r\n                }\r" +
"\n            });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonTe" +
"xt: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height: " +
"25,\r\n                prompt: \'项目编号、项目名称\',\r\n                onClickButton: functi" +
"on () {\r\n                    JQ.datagrid.searchGrid(\r\n                        {\r" +
"\n                            dgID: \'IsoRWPSTZDGrid\',\r\n                          " +
"  loadingType: \'datagrid\'\r\n                        });\r\n                }\r\n     " +
"       });\r\n            $(\"a[jqpermissionname=\'edit\']\").hide();\r\n        });\r\n\r\n" +
"\r\n        window.refreshGrid = function () {\r\n            $(\"#JQSearch\").textbox" +
"(\"button\").click();\r\n        }\r\n\r\n        function Info(Id) {\r\n            JQ.di" +
"alog.dialog({\r\n                title: \'任务评审通知单\',\r\n                width: \'1000\'," +
"\r\n                height: \'100%\',\r\n                url: \'");

            
            #line 102 "..\..\Views\IsoRWPSTZD\list.cshtml"
                 Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?Id=\' + Id,\r\n                onClose: function () {\r\n                    $(\"#IsoR" +
"WPSTZDGrid\").datagrid(\"reload\");\r\n                }\r\n            });\r\n        }\r" +
"\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoRWPSTZDGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoRWPSTZDPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n        <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 119 "..\..\Views\IsoRWPSTZD\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "TaskPursuant", queryOptions = "{ Key:'TaskPursuantState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 120 "..\..\Views\IsoRWPSTZD\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "AppraisalWay", queryOptions = "{ Key:'AppraisalWayState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 121 "..\..\Views\IsoRWPSTZD\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjectClass", queryOptions = "{ Key:'ProjectClassState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            发起时间\r\n            <input");

WriteLiteral(" id=\"CTStartTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始日期\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CTStartTime\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"CTEndTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束日期\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CTEndTime\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n        </div>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
