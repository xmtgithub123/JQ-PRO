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
    
    #line 4 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceEngineering/TechList.cshtml")]
    public partial class _Views_PayBalanceEngineering_TechList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceEngineering_TechList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
                     Write(Url.Action("TechJson", "PayBalanceEngineering",new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'export\', \'moreSearch\'],//需要显示的按钮\r\n                JQPrimaryID: " +
"\'Id\',//主键的字段\r\n                JQID: \'PayBalanceTechGrid\',//数据表格ID\r\n             " +
"   JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'1000\',//弹出窗体宽\r\n      " +
"          JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//" +
"数据表格类型 [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r\n           " +
"     singleSelect: false,//是否包含check\r\n                JQExcludeCol: [1, 10],//导出" +
"execl排除的列\r\n                checkOnSelect: true,//是否包含check\r\n                tool" +
"bar: \'#PayBalanceTechPanel\',//工具栏的容器ID\r\n                columns: [[\r\n           " +
"       { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t{" +
" title: \'项目编号\', field: \'ProjNumber\', width: \"14%\", align: \'left\', sortable: true" +
" },\r\n\t\t{ title: \'项目名称\', field: \'ProjName\', width: \"18%\", align: \'left\', sortable" +
": true },\r\n\t\t{ title: \'设总\', field: \'ProjEmpName\', width: \"8%\", align: \'center\', " +
"sortable: true },\r\n\t\t{ title: \'立项时间\', field: \'DateCreate\', width: \"8%\", align: \'" +
"center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{\r\n\t\t   " +
" title: \'结清状态\', field: \'Status\', width: \"8%\", align: \'center\', sortable: true,\r\n" +
"\t\t    formatter: function (val, row) {\r\n\t\t        if (row.Status == \"未结清\") {\r\n\t\t" +
"            return \'<span style=\"color:red;\">\' + row.Status + \'</span>\';\r\n\t\t    " +
"    }\r\n\t\t        else {\r\n\t\t            return \'<span style=\"color:green;\">\' + ro" +
"w.Status + \'</span>\';\r\n\t\t        }\r\n\t\t    }\r\n\t\t},\r\n        { title: \'产值\', field:" +
" \'ProjBanlanceFee\', width: \"10%\", align: \'right\', sortable: true },\r\n        { t" +
"itle: \'分配产值\', field: \'DistributeFee\', width: \"10%\", align: \'right\', sortable: tr" +
"ue },\r\n        { title: \'未分配产值\', field: \'UnDistrFee\', width: \"10%\", align: \'righ" +
"t\', sortable: true },\r\n            {\r\n                field: \'opt\', title: \'操作\'," +
" width: \"10%\", align: \'center\',\r\n                formatter: function (value, row" +
", index) {\r\n                    return \"<a href=\\\"javascript:void(0)\\\" jqpermiss" +
"ionname=\'add\' id=\\\"mb\" + index + \"\\\" projId=\\\"\" + row.projId + \"\\\" dataid=\\\"\" + " +
"row.Id + \"\\\" flag=\\\"menubutton\\\"   >操作</a>\";\r\n                }\r\n            }\r\n" +
"\r\n                ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n   " +
"             buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n   " +
"             height: 25,\r\n                prompt: \'项目名称\',\r\n                query" +
"Options: { Key: \'ProjName\', Contract: \'like\' },\r\n                onClickButton: " +
"function () {\r\n                    JQ.datagrid.searchGrid(\r\n                    " +
"    {\r\n                            dgID: \'PayBalanceTechGrid\',\r\n                " +
"            loadingType: \'datagrid\',\r\n                            queryParams: {" +
"\r\n                                KeyJQSearch: $(\"#JQSearch\").val(),\r\n          " +
"                      startTime: $(\"#startTime\").textbox(\'getValue\'),\r\n         " +
"                       endTime: $(\"#endTime\").textbox(\'getValue\'),\r\n            " +
"                    ProjectPhase: $(\'#ProjectPhase\').textbox(\'getValue\'),\r\n     " +
"                       }\r\n                        });\r\n                }\r\n      " +
"      });\r\n        });\r\n\r\n        $.extend($.fn.datagrid.defaults.view, {\r\n     " +
"       onAfterRender: function () {\r\n                $(\"a[flag=\'menubutton\']\").m" +
"ouseover(function () {\r\n                    document.getElementById(\"mm3\").setAt" +
"tribute(\"rowid\", this.getAttribute(\"dataid\"));\r\n                    document.get" +
"ElementById(\"mm3\").setAttribute(\"projId\", this.getAttribute(\"projId\"));\r\n       " +
"         }).menubutton({\r\n                    iconCls: \'fa fa-edit\',\r\n          " +
"          menu: \'#mm3\'\r\n                });\r\n            }\r\n        });\r\n\r\n     " +
"   function menuHandler(item) {\r\n            var key = $(\"#mm3\").attr(\"rowid\");\r" +
"\n            var projId = $(\"#mm3\").attr(\"projId\");\r\n            switch (item.te" +
"xt) {\r\n                case \"明细\":\r\n                    TechInfoDetail(key);\r\n   " +
"                 break;\r\n                case \"历史明细\":\r\n                    TechH" +
"istoryInfoDetail(projId);\r\n                    break;\r\n                default:\r" +
"\n                    break;\r\n            }\r\n        }\r\n        //技术人员历史信息明细\r\n   " +
"     function TechHistoryInfoDetail(EngiID) {\r\n            JQ.dialog.dialog({\r\n " +
"               title: \'技术人员结算信息\',\r\n                height: \'100%\',\r\n            " +
"    width: 1024,\r\n                url: \'");

            
            #line 105 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
                 Write(Url.Action("HisTechDetailInfo", "PayBalanceUserAccount", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral(@"?id=' + EngiID,
                onClose: function () {
                    $(""#PayBalanceEngineeringGrid"").datagrid(""reload"");
                }
            });
        }
        function getRows() {
            var rows = $(""#PayBalanceTechGrid"").datagrid('getSelections');
            if (rows.length <= 0 || rows == null || rows == undefined) {
                JQ.dialog.warning(""请选择要编辑记录"");
                return false;
            }
            return rows;
        }

        //撤销结算
        function CancelBalance() {
            var RowID = [];
            var rows = getRows();
            if (rows != false) {
                for (var index = 0; index < rows.length; index++) {
                    RowID.push(rows[index].Id);
                }
                $.post(""");

            
            #line 128 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
                   Write(Url.Action("CancelBalance"));

            
            #line default
            #line hidden
WriteLiteral(@""", { RowID: RowID.join(',') }, function (result) {
                    JQ.dialog.info(""撤销结算成功！"");
                    $(""#PayBalanceTechGrid"").datagrid('reload');
                });
            }
        }
        function SetProduct() {
            var rows = getRows();
            if (rows != false) {
                if (rows[0].projId == ""0"") {
                    Jq.dialog.warning(""当前项目工程不存在"");
                }
                JQ.dialog.dialog({
                    title: '设置产值',
                    height: '100%',
                    width: 1024,
                    url: '");

            
            #line 144 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
                     Write(Url.Action("SetProduct", "PayBalanceEngineering",new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral(@"?id=' + rows[0].projId,
                    onClose: function () {
                        $(""#PayBalanceTechGrid"").datagrid('reload');//设置关闭时执行的事件
                    }
                });
            }
        }
        //技术人员信息明细
        function TechInfoDetail(EngiBanlanceId) {
            JQ.dialog.dialog({
                title: '技术人员结算信息',
                height: '100%',
                width: 1024,
                url: '");

            
            #line 157 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
                 Write(Url.Action("TechInfo", "PayBalanceEngineering",new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + EngiBanlanceId,\r\n                onClose: function () {\r\n                " +
"    $(\"#PayBalanceTechGrid\").datagrid(\"reload\");\r\n                }\r\n           " +
" });\r\n        }\r\n    </script>\r\n");

WriteLiteral("    ");

            
            #line 164 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"PayBalanceTechGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"PayBalanceTechPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" jqpermissionname=\'add\'");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-edit\'\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(" onclick=\"CancelBalance();\"");

WriteLiteral(">撤销结算</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" jqpermissionname=\'add\'");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-edit\'\"");

WriteLiteral(" onclick=\"SetProduct();\"");

WriteLiteral(">设定工程产值</a>\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 177 "..\..\Views\PayBalanceEngineering\TechList.cshtml"
       Write(BaseData.getBases(
                                                      new Params() { isMult = false, engName = "ProjectPhase" }
                         ));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n            ");

WriteLiteral("\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'立项开始\'\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'立项结束\'\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"mm3\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"div1\"");

WriteLiteral(">明细</div>\r\n        <div");

WriteLiteral(" id=\"div2\"");

WriteLiteral(">历史明细</div>\r\n    </div>\r\n\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
