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
    
    #line 5 "..\..\Views\BussBiddingFile\list_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingFile/list_cj.cshtml")]
    public partial class _Views_BussBiddingFile_list_cj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussBiddingFile_list_cj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\BussBiddingFile\list_cj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 7 "..\..\Views\BussBiddingFile\list_cj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 10 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                     Write(Url.Action("GetBiddingFileList", "BussBiddingFile", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',\r\n            addUrl = \'");

            
            #line 11 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                 Write(Url.Action("add", "BussBiddingFile", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',\r\n            editUrl = \'");

            
            #line 12 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                  Write(Url.Action("edit", "BussBiddingFile", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 13 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                 Write(Url.Action("del", "BussBiddingFile", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n               " +
" JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n      " +
"          JQAddUrl: addUrl,//添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n" +
"                JQDelUrl: delUrl,//删除的路径\r\n                JQPrimaryID: \'Id\',//主键" +
"的字段\r\n                JQID: \'BussBiddingFileGrid\',//数据表格ID\r\n                JQDia" +
"logTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'1024\',//弹出窗体宽\r\n              " +
"  JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [" +
"datagrid,treegrid]\r\n                JQExcludeCol: [1, 12],//导出execl排除的列\r\n       " +
"         url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含chec" +
"k\r\n                toolbar: \'#BussBiddingFilePanel\',//工具栏的容器ID\r\n                " +
"columns: [[\r\n                 { field: \'ck\', align: \'center\', checkbox: true, JQ" +
"IsExclude: true },\r\n                 { title: \'投标编号\', field: \'BiddingNumber\', wi" +
"dth: \"12%\", align: \'left\', sortable: true },\r\n\t\t         { title: \'项目名称\', field:" +
" \'EngineeringName\', width: \"20%\", align: \'left\', sortable: true },\r\n            " +
"     { title: \'投标文件名\', field: \'ProjName\', width: \"15%\", align: \'left\', sortable:" +
" true },\r\n\t\t         { title: \'商务负责人\', field: \'BusinessEmpName\', width: \"6%\", al" +
"ign: \'center\', sortable: true },\r\n\t\t         { title: \'技术负责人\', field: \'Technolog" +
"yEmpName\', width: \"6%\", align: \'center\', sortable: true },\r\n\t\t         { title: " +
"\'计划开始时间\', field: \'DatePlanStart\', width: \"6%\", align: \'center\', sortable: true, " +
"formatter: JQ.tools.DateTimeFormatterByT },\r\n                 { title: \'计划完成日期\'," +
" field: \'DatePlanFinish\', width: \"6%\", align: \'center\', sortable: true, formatte" +
"r: JQ.tools.DateTimeFormatterByT },\r\n                 {\r\n                   fiel" +
"d: \'JQFiles\', title: \'附件\', align: \'center\', width: \"4%\", JQIsExclude: true, JQEx" +
"cludeFlag: \"grid_files\", JQRefTable: \'Project\', formatter: function (value, row)" +
" {\r\n                         return \"<span id=\\\"grid_files_\" + row.Id + \"\\\"></sp" +
"an>\"\r\n                     }\r\n                 },\r\n                 {\r\n         " +
"            field: \"FlowProgress\", title: \"流程进度\", align: \"left\", halign: \"center" +
"\", width: \"8%\", formatter: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"FlowStatu" +
"sID\", \"FlowStatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 46 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                                                                                                                                                                                                                                  Write(ViewBag.CurrEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                 },\r\n                 {\r\n                    field: \'opt\', ti" +
"tle: \'操作\', width: 100, align: \'center\', JQIsExclude: true,\r\n                    " +
"formatter: function (value, row, index) {\r\n                        var title = \"" +
"查看\";\r\n                        if (row._EditStatus == 1) {\r\n                     " +
"       title = \"修改\";\r\n                        }\r\n                        else if" +
" (row._EditStatus == 2) {\r\n                            title = \"处理\";\r\n          " +
"              }\r\n                        return \"<a href=\\\"javascript:void(0)\\\" " +
"onclick=\\\"ShowBiddingFileDialogue(\'\" + row.Id + \"\');\\\">\"+title+\"</a>\"\r\n         " +
"           }\r\n                }\r\n                ]],\r\n                onBeforeCh" +
"eck: function (rowIndex, rowData) {\r\n                    return rowData._AllowCh" +
"eck;\r\n                },\r\n                onClickRow: function (rowIndex, rowDat" +
"a) {\r\n                    if (!rowData._AllowCheck) {\r\n                        $" +
"(this).datagrid(\'unselectRow\', rowIndex);\r\n                    }\r\n              " +
"  },\r\n                onLoadSuccess: function (data) {\r\n                    $(\"d" +
"iv.datagrid-header-check input[type=\'checkbox\']\").attr(\"style\", \"display:none\");" +
"\r\n                    var rowViews = $(\"#BussBiddingFileGrid\").parent().find(\".d" +
"atagrid-view2 .datagrid-body tr.datagrid-row\");\r\n                    for (var i " +
"= 0; i < data.rows.length; i++) {\r\n                        if (!data.rows[i]._Al" +
"lowCheck) {\r\n                            rowViews.filter(\"[datagrid-row-index=\'\"" +
" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"disabled\", \"disabled\");\r\n   " +
"                     }\r\n                    }\r\n                }\r\n            })" +
";\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n    " +
"            buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n          " +
"      prompt: \'投标编号、项目名称\',\r\n                onClickButton: function () {\r\n      " +
"              JQ.datagrid.searchGrid(\r\n                        {\r\n              " +
"              dgID: \'BussBiddingFileGrid\',\r\n                            loadingT" +
"ype: \'datagrid\',\r\n                            queryParams: null\r\n               " +
"         });\r\n                }\r\n            });\r\n        });\r\n\r\n        functio" +
"n ShowBiddingFileDialogue(Id) {\r\n            JQ.dialog.dialog({\r\n               " +
" title: \"投标信息明细\",\r\n                url: \'");

            
            #line 99 "..\..\Views\BussBiddingFile\list_cj.cshtml"
                 Write(Url.Action("edit", "BussBiddingFile", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=CJ&id=' + Id,
                width: '1024',
                height: '100%',
                JQID: 'BussBiddingFileGrid',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus'
            });
        }

        window.refreshGrid = function () {
            $(""#JQSearch"").textbox(""button"").click();
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussBiddingFileGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussBiddingFilePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            登记时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateCreateS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateCreateE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            计划开始时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DatePlanStartS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DatePlanStartE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            计划完成日期\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DatePlanFinishS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DatePlanFinishE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591