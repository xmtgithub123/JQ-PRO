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
    
    #line 4 "..\..\Views\BussTendersCost\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussTendersCost/list.cshtml")]
    public partial class _Views_BussTendersCost_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussTendersCost_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussTendersCost\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\BussTendersCost\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 9 "..\..\Views\BussTendersCost\list.cshtml"
                     Write(Url.Action("json", "BussTendersCost", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n          addUrl = \'");

            
            #line 10 "..\..\Views\BussTendersCost\list.cshtml"
               Write(Url.Action("add", "BussTendersCost", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n          editUrl = \'");

            
            #line 11 "..\..\Views\BussTendersCost\list.cshtml"
                Write(Url.Action("edit", "BussTendersCost", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n          delUrl = \'");

            
            #line 12 "..\..\Views\BussTendersCost\list.cshtml"
               Write(Url.Action("del", "BussTendersCost", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n        " +
"        JQAddUrl: \'");

            
            #line 16 "..\..\Views\BussTendersCost\list.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 17 "..\..\Views\BussTendersCost\list.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 18 "..\..\Views\BussTendersCost\list.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\',//删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'Buss" +
"TendersCostGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n       " +
"         JQWidth: \'1024\',//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n    " +
"            JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n            " +
"    JQExcludeCol: [1, 18],//导出execl排除的列\r\n                frozenColumns: [[\r\n    " +
"                { field: \'ck\', align: \'center\', checkbox: true }, { title: \"招标编号" +
"\", field: \"TendersNumber\", halign: \"left\", width: \"6%\" },\r\n                    {" +
" title: \"招标名称\", field: \"TendersName\", halign: \"left\", width: \"8%\" },\r\n          " +
"          { title: \'投标单位\', field: \'CustName\', width: \"8%\", align: \'left\', sortab" +
"le: true }, ]],\r\n                url: requestUrl,//请求的地址\r\n                checkO" +
"nSelect: true,//是否包含check\r\n                toolbar: \'#BussTendersCostPanel\',//工具" +
"栏的容器ID\r\n                columns: [[\r\n                  { title: \'标书费\', field: \'T" +
"enderFee\', width: \"8%\", align: \'right\', halign: \"center\", sortable: true },\r\n\t\t " +
"         { title: \'标书费缴纳时间\', field: \'TenderFeePayTime\', width: \"100px\", align: \'" +
"center\', halign: \"center\", sortable: true, formatter: JQ.tools.DateTimeFormatter" +
"ByT },\r\n\t\t          { title: \'招标代理费\', field: \'TenderAgentFee\', width: \"8%\", alig" +
"n: \'right\', halign: \"center\", sortable: true },\r\n\t\t          { title: \'招标代理费缴纳时间" +
"\', field: \'TenderAgentFeePayTime\', width: \"100px```\", align: \'center\', halign: \"" +
"center\", sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t        " +
"  { title: \'投标保证金缴纳金额\', field: \'BidBondPay\', width: \"8%\", align: \'right\', halign" +
": \"center\", sortable: true },\r\n\t\t          { title: \'投标保证金缴纳时间\', field: \'BidBond" +
"PayTime\', width: \"100px\", align: \'center\', halign: \"center\", sortable: true, for" +
"matter: JQ.tools.DateTimeFormatterByT },\r\n\t\t          { title: \'投标保证金退还金额\', fiel" +
"d: \'BidBondBack\', width: \"8%\", align: \'right\', halign: \"center\", sortable: true " +
"},\r\n\t\t          { title: \'投标保证金退还时间\', field: \'BidBondBackTime\', width: \"100px\", " +
"align: \'center\', halign: \"center\", sortable: true, formatter: JQ.tools.DateTimeF" +
"ormatterByT },\r\n\t\t          { title: \'履约保证金缴纳金额\', field: \'PerformanceBondPay\', w" +
"idth: \"8%\", align: \'right\', halign: \"center\", sortable: true },\r\n\t\t          { t" +
"itle: \'履约保证金缴纳时间\', field: \'PerformanceBondPayTime\', width: \"100px\", align: \'cent" +
"er\', halign: \"center\", sortable: true, formatter: JQ.tools.DateTimeFormatterByT " +
"},\r\n\t\t          { title: \'履约保证金退还金额\', field: \'PerformanceBondBack\', width: \"8%\"," +
" align: \'right\', halign: \"center\", sortable: true },\r\n\t\t          { title: \'履约保证" +
"金退还时间\', field: \'PerformanceBondBackTime\', width: \"100px\", align: \'center\', halig" +
"n: \"center\", sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t    " +
"      { title: \'备注说明\', field: \'CostNote\', width: \"12%\", align: \'left\', halign: \"" +
"center\", sortable: true },\r\n                  {\r\n                      field: \'J" +
"QFiles\', title: \'附件\', align: \'center\', width: \"40px\", JQIsExclude: true, JQExclu" +
"deFlag: \"grid_files\", JQRefTable: \'BussTendersCost\', formatter: function (value," +
" row) {\r\n                          return \"<span id=\\\"grid_files_\" + row.Id + \"\\" +
"\"></span>\"\r\n                      }\r\n                  }\r\n                ]]\r\n  " +
"          });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText:" +
" \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height: 25," +
"\r\n                prompt: \'招标编号,招标名称,投标单位\',\r\n                queryOptions: { \'Ke" +
"y\': \'txtLike\', \'Contract\': \'like\' },\r\n                onClickButton: function ()" +
" {\r\n                    JQ.datagrid.searchGrid(\r\n                        {\r\n    " +
"                        dgID: \'BussTendersCostGrid\',\r\n                          " +
"  loadingType: \'datagrid\',\r\n                            queryParams: null\r\n     " +
"                   });\r\n                }\r\n            });\r\n\r\n            window" +
".refreshGrid = function () {\r\n                $(\"#JQSearch\").textbox(\"button\").c" +
"lick();\r\n            }\r\n        });\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussTendersCostGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussTendersCostPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 81 "..\..\Views\BussTendersCost\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "BidState", queryOptions = "{ Key:'WinState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            投标保证金缴纳时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BidBondPayTimeS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BidBondPayTimeE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            投标保证金退还时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BidBondBackTimeS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BidBondBackTimeE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n\r\n            中标时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'WinTimeS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'WinTimeE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n\r\n            履约保证金缴纳时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondPayTimeS\', Contract:\'>=\',filedType:\'Date\' " +
"}\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondPayTimeE\', Contract:\'<=\',filedType:\'Date\' " +
"}\"");

WriteLiteral(">&nbsp;\r\n            履约保证金退还时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondBackTimeS\', Contract:\'>=\',filedType:\'Date\'" +
" }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondBackTimeE\', Contract:\'<=\',filedType:\'Date\'" +
" }\"");

WriteLiteral(">&nbsp;\r\n\r\n            <select");

WriteLiteral(" id=\"isStartSearch1\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BidBondBack\', Contract:\'in\',filedType:\'Int\'}\"");

WriteLiteral(" class=\"easyui-combotree combotree-f combo-f textbox-f\"");

WriteLiteral("  style=\"width: 200px; height: 28px; display: none;\"");

WriteLiteral(" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:true, data:[{id:0," +
"text:\'已退还\'},{id:1,text:\'未退还\'}],prompt:\'投标保证金是否退还\',animate:true\"");

WriteLiteral("></select>&nbsp;\r\n\r\n            <select");

WriteLiteral(" id=\"isStartSearch2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondBack\', Contract:\'in\',filedType:\'Int\'}\"");

WriteLiteral(" class=\"easyui-combotree combotree-f combo-f textbox-f\"");

WriteLiteral("  style=\"width: 200px; height: 28px; display: none;\"");

WriteLiteral(" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:true,data:[{id:0,t" +
"ext:\'已退还\'},{id:1,text:\'未退还\'}],prompt:\'履约保证金是否退还\',animate:true\"");

WriteLiteral("></select>\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
