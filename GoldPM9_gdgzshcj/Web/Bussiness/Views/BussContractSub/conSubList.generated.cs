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
    
    #line 1 "..\..\Views\BussContractSub\conSubList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractSub/conSubList.cshtml")]
    public partial class _Views_BussContractSub_conSubList_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussContractSub>
    {
        public _Views_BussContractSub_conSubList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        debugger;
        JQ.datagrid.datagrid({
            JQButtonTypes: ['moreSearch'],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'BussContractSubGrid',//数据表格ID
            JQDialogTitle: '付款合同',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQExcludeCol: [1, 14, 15, 16],//导出execl排除的列
            url: '");

            
            #line 16 "..\..\Views\BussContractSub\conSubList.cshtml"
             Write(Url.Action("json","BussContractSub",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=FGS\',//请求的地址\r\n            queryParams: { projectID:\'");

            
            #line 17 "..\..\Views\BussContractSub\conSubList.cshtml"
                                  Write(Request.Params["projectID"]==null?"0":Request.Params["projectID"].ToString());

            
            #line default
            #line hidden
WriteLiteral("\',EngineeringNumber:\'");

            
            #line 17 "..\..\Views\BussContractSub\conSubList.cshtml"
                                                                                                                                      Write(Request.Params["EngineeringNumber"]==null?"0":Request.Params["EngineeringNumber"].ToString());

            
            #line default
            #line hidden
WriteLiteral("\',ConSubCustId:");

            
            #line 17 "..\..\Views\BussContractSub\conSubList.cshtml"
                                                                                                                                                                                                                                                    Write(Request.Params["ConSubCustId"] ==null?"0":Request.Params["ConSubCustId"]);

            
            #line default
            #line hidden
WriteLiteral(" },\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#BussCont" +
"ractSubPanel\',//工具栏的容器ID\r\n            columns: [[\r\n                { title: \'外委合" +
"同编号\', field: \'ConSubNumber\', width: \'8%\', halign: \'center\', align: \'left\', sorta" +
"ble: true },\r\n                { title: \'外委合同名称\', field: \'ConSubName\', width: \'12" +
"%\', align: \'left\', halign: \'center\', sortable: true },\r\n                { title:" +
" \'外委合同类型\', field: \'ConSubTypeName\', width: \'80\', align: \'center\', sortable: true" +
" },\r\n                { title: \'外委合同类别\', field: \'ConSubCategoryName\', width: \'80\'" +
", align: \'center\', sortable: true },\r\n                { title: \'外委合同状态\', field: " +
"\'ConSubStatusName\', width: \'80\', align: \'center\', sortable: true },\r\n           " +
"     { title: \'外委合同单位\', field: \'CustName\', width: \'12%\', align: \'left\', halign: " +
"\'center\', sortable: true },\r\n                { title: \'签订日期\', field: \'ConSubDate" +
"\', width: \'80\', align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFor" +
"matterByT },\r\n                { title: \'合同金额(元)\', field: \'ConSubFee\', width: \'10" +
"0\', align: \'right\', halign: \'center\', sortable: true },\r\n                { title" +
": \'已付款(元)\', field: \'AlreadySumFeeMoney\', width: \'80\', align: \'right\', halign: \'c" +
"enter\', sortable: true },\r\n                { title: \'未付款(元)\', field: \'UnPay\', wi" +
"dth: \'80\', align: \'right\', halign: \'center\', sortable: true },\r\n                " +
"{ title: \'已收票(元)\', field: \'AlreadySumInvoiceMoney\', width: \'80\', align: \'right\'," +
" halign: \'center\', sortable: true },\r\n            ]],\r\n            onBeforeCheck" +
": function (rowIndex, rowData) {\r\n                return rowData._AllowCheck;\r\n " +
"           },\r\n            onClickRow: function (rowIndex, rowData) {\r\n         " +
"       if (!rowData._AllowCheck) {\r\n                    $(this).datagrid(\'unsele" +
"ctRow\', rowIndex);\r\n                }\r\n            },\r\n            onLoadSuccess" +
": function (data) {\r\n                $(\"div.datagrid-header-check input[type=\'ch" +
"eckbox\']\").attr(\"style\", \"display:none\");\r\n                var rowViews = $(\"#Bu" +
"ssContractSubGrid\").parent().find(\".datagrid-view2 .datagrid-body tr.datagrid-ro" +
"w\");\r\n                for (var i = 0; i < data.rows.length; i++) {\r\n            " +
"        if (!data.rows[i]._AllowCheck) {\r\n                        rowViews.filte" +
"r(\"[datagrid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"dis" +
"abled\", \"disabled\");\r\n                    }\r\n                }\r\n            }\r\n " +
"       });\r\n    })\r\n</script>\r\n<table");

WriteLiteral(" id=\"BussContractSubGrid\"");

WriteLiteral("></table>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
