﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
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
    
    #line 4 "..\..\Views\PayBalanceLot\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceLot/list.cshtml")]
    public partial class _Views_PayBalanceLot_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceLot_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\PayBalanceLot\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\PayBalanceLot\list.cshtml"
                     Write(Url.Action("json", "PayBalanceLot", new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n\r\n            JQ.datagrid.datagrid({\r\n              " +
"  JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'rightGrid\',//数据表格ID\r\n       " +
"         JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n               " +
" url: \'");

            
            #line 14 "..\..\Views\PayBalanceLot\list.cshtml"
                 Write(Url.Action("ManagerBalanceJson", "PayBalanceUserAccount", new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                singleSelect: true,//是否包含check\r\n                checkO" +
"nSelect: true,//是否包含check\r\n                columns: [[\r\n\t\t{ title: \'姓名\', field: " +
"\'EmpName\', width: \"15%\", align: \'center\', sortable: true },\r\n\t\t{ title: \'系数\', fi" +
"eld: \'ManageCoeff\', width: \"13%\", align: \'right\', sortable: true },\r\n        \t\t{" +
" title: \'产值\', field: \'BalanceAmount\', width: \"15%\", align: \'right\', sortable: tr" +
"ue },\r\n                { title: \'金额\', field: \'BalanceMoney\', width: \"15%\", align" +
": \'right\', sortable: true },\r\n                { title: \'备注\', field: \'BalanceNote" +
"\', width: \"18%\", align: \'center\', sortable: true },\r\n                {\r\n        " +
"            title: \'操作\', field: \'Opt\', width: \"18%\", align: \'center\',\r\n         " +
"           formatter: function (val, row) {\r\n                        if (parseIn" +
"t(row.ChangeCount) > 0) {\r\n                            return \'<a ShowDialog hre" +
"f=\"#\"   class=\"easyui-linkbutton\" onclick=\"ShowDialog(\' + row.Id + \')\">修改</a>\' +" +
" \'&nbsp;&nbsp;&nbsp;<a ShowList href=\"#\"    class=\"easyui-linkbutton\" onclick=\"S" +
"howList(\' + row.Id + \')\">查看</a>\'\r\n                        }\r\n                   " +
"     return \'<a ShowDialog href=\"#\"    class=\"easyui-linkbutton\" onclick=\"ShowDi" +
"alog(\' + row.Id + \')\">修改</a>\'\r\n                    }\r\n                }\r\n       " +
"         ]],\r\n                onLoadSuccess: function (row, data) {\r\n           " +
"         $(\"a[ShowDialog]\").linkbutton({ iconCls: \'fa fa-edit\' });\r\n            " +
"        $(\"a[ShowList]\").linkbutton({ iconCls: \'fa fa-edit\' });\r\n               " +
" }\r\n            });\r\n\r\n            JQ.datagrid.datagrid({\r\n                JQPri" +
"maryID: \'Id\',//主键的字段\r\n                JQID: \'leftGrid\',//数据表格ID\r\n               " +
" JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                url: re" +
"questUrl,//请求的地址\r\n                singleSelect: true,//是否包含check\r\n              " +
"  checkOnSelect: true,//是否包含check\r\n                toolbar: \'#PayLotPanel\',//工具栏" +
"的容器ID\r\n\r\n                columns: [[\r\n\t\t{\r\n\t\t    title: \'批次名称\', field: \'BalanceL" +
"otName\', width: \"53%\", align: \'left\', sortable: true,\r\n\t\t    formatter: function" +
" (val, row) {\r\n\t\t        return \'<a class=\"easyui-linkbutton\" href=\"#\"  onclick=" +
"\"ShowDetail(\' + row.Id + \')\">\' + row.BalanceLotName + \'</a>\'\r\n\t\t    }\r\n\t\t},\r\n\t\t{" +
" title: \'结算日期\', field: \'BalanceDate\', width: \"40%\", align: \'center\', sortable: t" +
"rue, formatter: JQ.tools.DateTimeFormatterByT }\r\n\r\n                ]],\r\n        " +
"        onLoadSuccess: function (data) {\r\n                    var rows = $(\"#lef" +
"tGrid\").datagrid(\'getRows\');\r\n                    if (rows.length > 0) {\r\n      " +
"                  ShowDetail(rows[0][\"Id\"]);\r\n                    }\r\n           " +
"     }\r\n            });\r\n            setTextAlign(\"BalanceLotName\", \"text-align:" +
"center;\")\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选" +
"\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n  " +
"              prompt: \'批次名称\',\r\n                queryOptions: { \'Key\': \'BalanceLo" +
"tName\', \'Contract\': \'like\' },\r\n                onClickButton: function () {\r\n   " +
"                 JQ.datagrid.searchGrid(\r\n                        {\r\n           " +
"                 dgID: \'leftGrid\',\r\n                            loadingType: \'da" +
"tagrid\',\r\n                            queryParams: null\r\n                       " +
" });\r\n                }\r\n\r\n            });\r\n            $(\"#westRegion\").find(\"d" +
"iv[class=\'datagrid-view\']\").next().find(\'a\').eq(0).hide();\r\n            $(\"#west" +
"Region\").find(\"div[class=\'datagrid-view\']\").next().find(\'a\').eq(3).hide();\r\n    " +
"        $(\"#westRegion\").find(\"select[class=\'pagination-page-list\']\").hide();\r\n " +
"           $(\"#westRegion\").find(\"div[class=\'pagination-info\']\").hide();\r\n\r\n    " +
"    });\r\n        function ShowDialog(id) {\r\n            JQ.dialog.dialog({\r\n    " +
"            title: \'修改历史结算信息\',\r\n                width: \'800\',\r\n                h" +
"eight: \'400\',\r\n                url: \'");

            
            #line 93 "..\..\Views\PayBalanceLot\list.cshtml"
                 Write(Url.Action("edit", "PayBalanceChangeHist",new { @area="pay"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id=' + id,
                onClose: function () {
                    $(""#rightGrid"").datagrid(""reload"");
                }
            });
        }

        function ShowList(id) {
            JQ.dialog.dialog({
                title: '修改历史结算信息',
                width: '800',
                height: '650',
                url: '");

            
            #line 105 "..\..\Views\PayBalanceLot\list.cshtml"
                 Write(Url.Action("list", "PayBalanceChangeHist",new { @area="pay"}));

            
            #line default
            #line hidden
WriteLiteral("?payBalanceAcountId=\' + id\r\n            });\r\n        }\r\n        function ShowDeta" +
"il(id) {\r\n            $.post(\'");

            
            #line 109 "..\..\Views\PayBalanceLot\list.cshtml"
               Write(Url.Action("modelJson", "PayBalanceLot", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral(@"', { Id: id }, function (result) {
                $(""#BalanceLotName"").text(result.BalanceLotName);
                $(""#AllAmount"").text(result.AllAmount);
                $(""#AllMoney"").text(result.AllMoney);
                $(""#BalanceDate"").text(result.BalanceDate);
                $(""#ManageAmount"").text(result.ManageAmount);
                $(""#ManageBase"").text(result.ManageBase);
                $(""#MoneyPerAmount"").text(result.MoneyPerAmount);
                $(""#TechAmount"").text(result.TechAmount);
                $(""#TechEmpCount"").text(result.TechEmpCount);
            });
            $(""#rightGrid"").datagrid(""reload"", '");

            
            #line 120 "..\..\Views\PayBalanceLot\list.cshtml"
                                           Write(Url.Action("ManagerBalanceJson", "PayBalanceUserAccount", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?LotID=\' + id);\r\n        }\r\n\r\n        function setTextAlign(filed, align) {\r\n    " +
"        $(\"tr[class=\'datagrid-header-row\']\").find(\"td[field=\'\" + filed + \"\']\").c" +
"hildren().removeAttr(\"style\").attr(\"style\", align);\r\n        }\r\n    </script>\r\n");

            
            #line 127 "..\..\Views\PayBalanceLot\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"lay\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" style=\"overflow: hidden\"");

WriteLiteral(" fit=\"true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" data-options=\"region:\'north\'\"");

WriteLiteral(" style=\"height:40px;line-height:40px;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"margin-left:20px;font-size:13px;\"");

WriteLiteral(">筛选条件:</span>\r\n            <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:230px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'BalanceLotName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n            <span");

WriteLiteral(" style=\"margin-left:20px;font-size:13px;\"");

WriteLiteral(">结算日期:</span>\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:100px\"");

WriteLiteral(" data-options=\"prompt: \'结算开始时间\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'BalanceDate\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            --\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:100px\"");

WriteLiteral(" data-options=\"prompt: \'结算结束时间\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'BalanceDate\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n\r\n\r\n            <div");

WriteLiteral(" id=\"PayLotPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n\r\n            </div>\r\n        </div>\r\n        <div");

WriteLiteral(" id=\"westRegion\"");

WriteLiteral(" data-options=\"region:\'west\'\"");

WriteLiteral(" style=\"width:240px;margin-top:0px;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"leftGrid\"");

WriteLiteral("></table>\r\n        </div>\r\n        <div");

WriteLiteral(" data-options=\"region:\'center\'\"");

WriteLiteral(" style=\"background:#fff;overflow:scroll;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(" style=\"width:95%;\"");

WriteLiteral(">\r\n                <tr>\r\n                    <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(">\r\n                        <span");

WriteLiteral(" style=\"font-size:16px;font-weight:bold;\"");

WriteLiteral(">本批次结算的相关信息</span>\r\n                    </td>\r\n                </tr>\r\n           " +
"     <tr>\r\n                    <th");

WriteLiteral(" style=\"width:14%;\"");

WriteLiteral(">\r\n                        批次名称\r\n                    </th>\r\n                    <" +
"td>\r\n                        <label");

WriteLiteral(" id=\"BalanceLotName\"");

WriteLiteral("></label>\r\n                    </td>\r\n                    <th");

WriteLiteral(" style=\"width:14%;\"");

WriteLiteral(">\r\n                        结算日期\r\n                    </th>\r\n                    <" +
"td>\r\n                        <label");

WriteLiteral(" id=\"BalanceDate\"");

WriteLiteral("></label>\r\n                    </td>\r\n                </tr>\r\n                <tr>" +
"\r\n                    <th>\r\n                        技术人员数量\r\n                    " +
"</th>\r\n                    <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                        <label");

WriteLiteral(" id=\"TechEmpCount\"");

WriteLiteral("></label>\r\n                    </td>\r\n                </tr>\r\n                <tr>" +
"\r\n                    <th>\r\n                        总产值\r\n                    </t" +
"h>\r\n                    <td>\r\n                        <label");

WriteLiteral(" id=\"AllAmount\"");

WriteLiteral("></label>\r\n                    </td>\r\n                    <th>\r\n                 " +
"       技术总产值\r\n                    </th>\r\n                    <td>\r\n             " +
"           <label");

WriteLiteral(" id=\"TechAmount\"");

WriteLiteral("></label>\r\n                    </td>\r\n\r\n                </tr>\r\n                <t" +
"r>\r\n                    <th>\r\n                        管理总产值\r\n                   " +
" </th>\r\n                    <td>\r\n                        <label");

WriteLiteral(" id=\"ManageAmount\"");

WriteLiteral("></label>\r\n                    </td>\r\n                    <th>\r\n                 " +
"       基数\r\n                    </th>\r\n                    <td>\r\n                " +
"        <label");

WriteLiteral(" id=\"ManageBase\"");

WriteLiteral("></label>\r\n                    </td>\r\n                </tr>\r\n                <tr>" +
"\r\n                    <th>\r\n                        全部金额\r\n                    </" +
"th>\r\n                    <td>\r\n                        <label");

WriteLiteral(" id=\"AllMoney\"");

WriteLiteral("></label>\r\n                    </td>\r\n                    <th>\r\n                 " +
"       元/产值\r\n                    </th>\r\n                    <td>\r\n              " +
"          <label");

WriteLiteral(" id=\"MoneyPerAmount\"");

WriteLiteral("></label>\r\n                    </td>\r\n                </tr>\r\n            </table>" +
"\r\n            <table");

WriteLiteral(" id=\"rightGrid\"");

WriteLiteral(" style=\"width:95%;\"");

WriteLiteral("></table>\r\n\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
