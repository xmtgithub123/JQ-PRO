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
    
    #line 4 "..\..\Views\BussBiddingCost\list_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingCost/list_sj.cshtml")]
    public partial class _Views_BussBiddingCost_list_sj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussBiddingCost_list_sj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussBiddingCost\list_sj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\BussBiddingCost\list_sj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        var requestUrl = \'");

            
            #line 9 "..\..\Views\BussBiddingCost\list_sj.cshtml"
                     Write(Url.Action("GetCostList", "BussBiddingCost", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',\r\n            addUrl = \'");

            
            #line 10 "..\..\Views\BussBiddingCost\list_sj.cshtml"
                 Write(Url.Action("add", "BussBiddingCost", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',\r\n            editUrl = \'");

            
            #line 11 "..\..\Views\BussBiddingCost\list_sj.cshtml"
                  Write(Url.Action("edit", "BussBiddingCost", new {@area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',\r\n            delUrl = \'");

            
            #line 12 "..\..\Views\BussBiddingCost\list_sj.cshtml"
                 Write(Url.Action("del", "BussBiddingCost", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n               " +
" JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n      " +
"          JQAddUrl: addUrl,//添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n" +
"                JQDelUrl: delUrl,//删除的路径\r\n                JQPrimaryID: \'Id\',//主键" +
"的字段\r\n                JQID: \'BussBiddingCostGrid\',//数据表格ID\r\n                JQDia" +
"logTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'900\',//弹出窗体宽\r\n               " +
" JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [d" +
"atagrid,treegrid]\r\n                frozenColumns: [[\r\n                    { fiel" +
"d: \'ck\', align: \'center\', checkbox: true },\r\n                    { title: \"招标批次\"" +
", field: \"BiddingBatch\", halign: \"left\", width: \"10%\", sortable: true },\r\n      " +
"              { title: \"投标编号\", field: \"BiddingNumber\", halign: \"left\", width: \"1" +
"0%\", sortable: true },\r\n                    { title: \'包号\', field: \'PackageNumber" +
"\', width: \"10%\", align: \'left\', sortable: true }, ]],\r\n                //JQCusto" +
"mSearch: [  //自定义搜索的字段\r\n                //{ value: \'DepartmentName\', key: \'人员部门\'" +
", type: \'string\' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname" +
"\r\n                //],\r\n                JQExcludeCol: [1, 20],//导出execl排除的列\r\n   " +
"             //JQMergeCells: { fields: [\'DepartmentName\', \'EmpName\', \'EmpLogin\']" +
", Only: \'DepartmentName\' },//当字段值相同时会自动的跨行(只对相邻有效)\r\n                //JQEnableFi" +
"lter: true,//是否启用表格字段检索，只支持当页数据\r\n                //JQFields: [\"\", \"\"],//追加的字段名\r\n" +
"                url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是" +
"否包含check\r\n                toolbar: \'#BussBiddingCostPanel\',//工具栏的容器ID\r\n         " +
"       columns: [[\r\n                  { title: \'投标状态\', field: \'ProgressName\', wi" +
"dth: 80, align: \'center\', sortable: true },\r\n                  { title: \'中标日期\', " +
"field: \'WinTime\', width: 100, align: \'center\', sortable: true, formatter: JQ.too" +
"ls.DateTimeFormatterByT },\r\n                  { title: \'标书费\', field: \'TenderFee\'" +
", width: 100, align: \'right\', sortable: true },\r\n\t\t          { title: \'标书费缴纳时间\'," +
" field: \'TenderFeePayTime\', width: 100, align: \'center\', sortable: true, formatt" +
"er: JQ.tools.DateTimeFormatterByT },\r\n\t\t          { title: \'招标代理费\', field: \'Tend" +
"erAgentFee\', width: 100, align: \'right\', sortable: true },\r\n\t\t          { title:" +
" \'招标代理费缴纳时间\', field: \'TenderAgentFeePayTime\', width: 120, align: \'center\', sorta" +
"ble: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t          { title: \'投标" +
"保证金缴纳金额\', field: \'BidBondPay\', width: 120, align: \'right\', sortable: true },\r\n\t\t" +
"          { title: \'投标保证金缴纳时间\', field: \'BidBondPayTime\', width: 120, align: \'cen" +
"ter\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t          {" +
" title: \'投标保证金退还金额\', field: \'BidBondBack\', width: 120, align: \'right\', sortable:" +
" true },\r\n\t\t          { title: \'投标保证金退还时间\', field: \'BidBondBackTime\', width: 120" +
", align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n" +
"\t\t          { title: \'履约保证金缴纳金额\', field: \'PerformanceBondPay\', width: 120, align" +
": \'right\', sortable: true },\r\n\t\t          { title: \'履约保证金缴纳时间\', field: \'Performa" +
"nceBondPayTime\', width: 120, align: \'center\', sortable: true, formatter: JQ.tool" +
"s.DateTimeFormatterByT },\r\n\t\t          { title: \'履约保证金退还金额\', field: \'Performance" +
"BondBack\', width: 120, align: \'right\', sortable: true },\r\n\t\t          { title: \'" +
"履约保证金退还时间\', field: \'PerformanceBondBackTime\', width: 120, align: \'center\', sorta" +
"ble: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t          { title: \'备注" +
"说明\', field: \'CostNote\', width: 200, align: \'left\', sortable: true },\r\n          " +
"       {\r\n                     field: \'JQFiles\', title: \'附件\', align: \'center\', w" +
"idth: 80, JQIsExclude: true, JQExcludeFlag: \"grid_files\", JQRefTable: \'BussBiddi" +
"ngCost\', formatter: function (value, row) {\r\n                         return \"<s" +
"pan id=\\\"grid_files_\" + row.Id + \"\\\"></span>\"\r\n                     }\r\n         " +
"        }\r\n                ]]\r\n            });\r\n            $(\"#JQSearch\").textb" +
"ox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-sear" +
"ch\',\r\n                height: 25,\r\n                prompt: \'投标编号,招标批次名称,包号\',\r\n  " +
"              queryOptions: { \'Key\': \'CostNote\', \'Contract\': \'like\' },\r\n        " +
"        onClickButton: function () {\r\n                    JQ.datagrid.searchGrid" +
"(\r\n                        {\r\n                            dgID: \'BussBiddingCost" +
"Grid\',\r\n                            loadingType: \'datagrid\',\r\n                  " +
"          queryParams: null\r\n                        });\r\n                }\r\n   " +
"         });\r\n\r\n            window.refreshGrid = function () {\r\n                " +
"$(\"#JQSearch\").textbox(\"button\").click();\r\n            }\r\n        });\r\n\r\n\r\n    <" +
"/script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussBiddingCostGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussBiddingCostPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n\r\n");

WriteLiteral("        ");

            
            #line 95 "..\..\Views\BussBiddingCost\list_sj.cshtml"
   Write(BaseData.getBases(new Params() { isMult = true, engName = "BiddingProgress", queryOptions = "{ Key:'BiddingProgress', Contract:'in'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n\r\n            中标时间\r\n            <input");

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

WriteLiteral(">&nbsp;\r\n\r\n            投标保证金缴纳时间\r\n            <input");

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

WriteLiteral(" style=\"width: 200px; height: 28px; display: none;\"");

WriteLiteral(" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:true, data:[{id:0," +
"text:\'已退还\'},{id:1,text:\'未退还\'}],prompt:\'投标保证金是否退还\',animate:true\"");

WriteLiteral("></select>&nbsp;\r\n\r\n            <select");

WriteLiteral(" id=\"isStartSearch2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'PerformanceBondBack\', Contract:\'in\',filedType:\'Int\'}\"");

WriteLiteral(" class=\"easyui-combotree combotree-f combo-f textbox-f\"");

WriteLiteral(" style=\"width: 200px; height: 28px; display: none;\"");

WriteLiteral(" data-options=\"onlyLeafCheck:true,cascadeCheck: false,multiple:true,data:[{id:0,t" +
"ext:\'已退还\'},{id:1,text:\'未退还\'}],prompt:\'履约保证金是否退还\',animate:true\"");

WriteLiteral("></select>\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591