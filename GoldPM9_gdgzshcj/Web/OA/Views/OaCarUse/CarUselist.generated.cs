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
    
    #line 4 "..\..\Views\OaCarUse\CarUselist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaCarUse/CarUselist.cshtml")]
    public partial class _Views_OaCarUse_CarUselist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaCarUse_CarUselist_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaCarUse\CarUselist.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\OaCarUse\CarUselist.cshtml"
                     Write(Url.Action("jsonStatu", "OaCarUse",new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n              " +
"  JQButtonTypes: [\'export\',\'moreSearch\'],//需要显示的按钮\r\n                JQPrimaryID:" +
" \'Id\',//主键的字段\r\n                JQID: \'OaCarUseGrid\',//数据表格ID\r\n                JQ" +
"DialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n            " +
"    JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型" +
" [datagrid,treegrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n               " +
" url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n     " +
"           toolbar: \'#OaCarUsePanel\',//工具栏的容器ID\r\n                JQExcludeCol: [" +
"11],//导出execl排除的列\r\n                JQExportName: \'车辆使用记录\',\r\n                nowr" +
"ap: true,\r\n                columns:\r\n                    [[\r\n\t\t              { t" +
"itle: \'用途\', field: \'UsePurpose\', width: \'20%\', align: \'left\', halign: \'center\' }" +
",\r\n\t\t              { title: \'车辆信息\', field: \'CarInfo\', width: \'10%\', align: \'left" +
"\', halign: \'center\' },\r\n\t\t              { title: \'申请日期\', field: \'UseApplyDatetim" +
"e\', width: \'8%\', align: \'center\', halign: \'center\', formatter: JQ.tools.DateTime" +
"FormatterByT },\r\n\t\t              { title: \'去向\', field: \'UsePlace\', width: \'10%\'," +
" align: \'left\', halign: \'center\' },\r\n\t\t              { title: \'带车责任人\', field: \'U" +
"seLeaderEmpName\', width: \'80px\', align: \'center\' },\r\n\t\t              { title: \'计" +
"划出车时间\', field: \'DateLeavePlan\', width: \'8%\', align: \'center\', formatter: JQ.tool" +
"s.DateTimeFormatterByH },\r\n\t\t              { title: \'计划归队时间\', field: \'DateArrive" +
"Plan\', width: \'8%\', align: \'center\', formatter: JQ.tools.DateTimeFormatterByH }," +
"\r\n\t\t              { title: \'实际出车时间\', field: \'DateLeave\', width: \'8%\', align: \'ce" +
"nter\', formatter: JQ.tools.DateTimeFormatterByH },\r\n\t\t              { title: \'实际" +
"归队时间\', field: \'DateArrive\', width: \'8%\', align: \'center\', formatter: JQ.tools.Da" +
"teTimeFormatterByH },\r\n\t\t              { title: \'驾驶员\', field: \'UseCarDriver\', wi" +
"dth: \'60px\', align: \'center\' },\r\n                      { field: \'JQFiles\', title" +
": \'附件\', align: \'center\', width: \'40\', JQIsExclude: true, JQRefTable: \'OaCarUse\' " +
"}\r\n                    ]]\r\n            });\r\n\r\n            $(\"#JQSearch\").textbox" +
"({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search" +
"\',\r\n                height: 25,\r\n                prompt: \'用途\',\r\n                " +
"onClickButton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n     " +
"                   {\r\n                            dgID: \'OaCarUseGrid\',\r\n       " +
"                     loadingType: \'datagrid\',\r\n                            query" +
"Params: null\r\n                        });\r\n                }\r\n            });\r\n\r" +
"\n\r\n            $(\"#CarName\").combobox({\r\n                url: \'");

            
            #line 58 "..\..\Views\OaCarUse\CarUselist.cshtml"
                 Write(Url.Action("GetAllCarInfo", "OaCar",new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
                valueField: 'Id',
                textField: ""text"",
                onChange: function () {
                    ResGrid();
                }, onHidePanel: function () {
                    comBoxHidePanel(this);
                }
            });

            $(""#CarHeader"").combobox({
                url: '");

            
            #line 69 "..\..\Views\OaCarUse\CarUselist.cshtml"
                 Write(Url.Action("employeeJson", "usercontrol",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
                valueField: 'EmpID',
                textField: ""EmpName"",
                onChange: function () {
                    ResGrid();
                }, onHidePanel: function () {
                    comBoxHidePanel(this);
                }
            });

            $(""#endTime"").datebox({
                onSelect: function () {
                    ResGrid();
                }
            });


            $(""#startTime"").datebox({
                onSelect: function () {
                    ResGrid();
                }
            });

            $(""#sj_startTime"").datebox({
                onSelect: function () {
                    ResGrid();
                }
            });

            $(""#sj_endTime"").datebox({
                onSelect: function () {
                    ResGrid();
                }
            });

        });

        function ResGrid() {
            JQ.datagrid.searchGrid({
                dgID: 'OaCarUseGrid',
                loadingType: 'datagrid',
                queryParams: null
            });
        }

        function Opencheckin(sId) {
            JQ.dialog.dialog({
                title: ""收车登记"",
                url: '");

            
            #line 117 "..\..\Views\OaCarUse\CarUselist.cshtml"
                 Write(Url.Action("Edit01"));

            
            #line default
            #line hidden
WriteLiteral("\' + \'?id=\' + sId,\r\n                width: \'700\',\r\n                height: \'800\',\r" +
"\n                JQID: \'CheckIn\',\r\n                JQLoadingType: \'treegrid\',\r\n " +
"               iconCls: \'fa fa-plus\'\r\n            });\r\n        }\r\n    </script>\r" +
"\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"OaCarUseGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"OaCarUsePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"padding-left:20px;\"");

WriteLiteral(">\r\n                车辆信息：<select");

WriteLiteral(" id=\"CarName\"");

WriteLiteral(" name=\"CarName\"");

WriteLiteral(" data-options=\"prompt: \'车辆名称\'\"");

WriteLiteral(" style=\"width:150px;\"");

WriteLiteral(" JQWhereOptions=\"{\'Key\':\'CarID\',\'Contract\':\'in\',filedType:\'Int\'}\"");

WriteLiteral("></select>\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:20px;\"");

WriteLiteral(">\r\n                带车责任人：<select");

WriteLiteral(" id=\"CarHeader\"");

WriteLiteral(" name=\"CarHeader\"");

WriteLiteral(" data-options=\"prompt: \'带车责任人\'\"");

WriteLiteral(" style=\"width:150px;\"");

WriteLiteral(" JQWhereOptions=\"{\'Key\':\'UseLeaderEmp\',\'Contract\':\'in\',filedType:\'Int\'}\"");

WriteLiteral("></select>\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:20px;\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'计划出车时间\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateLeavePlan\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n                至\r\n                <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'计划归队时间\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateArrivePlan\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;&nbsp;&nbsp;&nbsp;\r\n                <input");

WriteLiteral(" id=\"sj_startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'实际出车时间\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateLeave\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n                至\r\n                <input");

WriteLiteral(" id=\"sj_endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'实际归队时间\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'DateArrive\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            </span>\r\n        </span>\r\n    </div>\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ExportType\"");

WriteLiteral(" name=\"ExportType\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" />\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
