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
    
    #line 1 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceUserAccount/HisTechDetailInfo.cshtml")]
    public partial class _Views_PayBalanceUserAccount_HisTechDetailInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceUserAccount_HisTechDetailInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    ");

WriteLiteral("\r\n    $(function () {\r\n        $(\"#TechDetailGrid\").datagrid({\r\n            iconC" +
"ls: \'icon-edit\',\r\n            multiSelect: false,\r\n            rownumbers: true," +
"\r\n            url: \'");

            
            #line 33 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
             Write(Url.Action("TechStatisticsDetailJson", "PayBalanceUserAccount", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?EngiID=");

            
            #line 33 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                                                                                                            Write(Request.QueryString["id"]);

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            fitColumns: true,\r\n            columns: [[\r\n        { titl" +
"e: \'结算日期\', field: \'BalanceDate\', width: \"8%\", align: \'center\', sortable: true, f" +
"ormatter: JQ.tools.DateTimeFormatterByT },\r\n        { title: \'结算批次名称\', field: \'L" +
"otName\', width: \"20%\", align: \'left\', sortable: true },\r\n        { title: \'分配产值\'" +
", field: \'EngAmountArrange\', width: \"8%\", align: \'right\', sortable: true },\r\n   " +
"     { title: \'人员\', field: \'TechEmpName\', width: \"8%\", align: \'center\', sortable" +
": true },\r\n        { title: \'来源\', field: \'BalanceReason\', width: \"8%\", align: \'c" +
"enter\', sortable: true },\r\n        { title: \'产值\', field: \'BalanceAmount\', width:" +
" \"8%\", align: \'right\', sortable: true },\r\n        { title: \'提奖金额（元）\', field: \'Ba" +
"lanceMoney\', width: \"10%\", align: \'right\', sortable: true },\r\n        { title: \'" +
"备注\', field: \'BalanceNote\', width: \"14%\", align: \'center\', sortable: true },\r\n   " +
"     {\r\n            title: \'修改\', field: \'Opt\', width: \"14%\", align: \'center\', so" +
"rtable: true,\r\n            formatter: function (val, row) {\r\n                if " +
"(parseInt(row.ChangeCount) > 0) {\r\n                    return \'<a ShowDialog hre" +
"f=\"#\" class=\"easyui-linkbutton\" onclick=\"ShowDialog(\' + row.Id + \')\">修改</a>\' + \'" +
"&nbsp;&nbsp;&nbsp;<a ShowList href=\"#\" class=\"easyui-linkbutton\" onclick=\"ShowLi" +
"st(\' + row.Id + \')\">查看</a>\'\r\n                }\r\n                return \'<a ShowD" +
"ialog href=\"#\" class=\"easyui-linkbutton\" onclick=\"ShowDialog(\' + row.Id + \')\">修改" +
"</a>\'\r\n            }\r\n        }\r\n            ]],\r\n            onLoadSuccess: fun" +
"ction (row) {\r\n                MergeCells(\'BalanceDate\', $(this).datagrid(\'getRo" +
"ws\'));\r\n                MergeCells(\'LotName\', $(this).datagrid(\'getRows\'));\r\n   " +
"             MergeCells(\'EngAmountArrange\', $(this).datagrid(\'getRows\'));\r\n\r\n\r\n " +
"               $(\"a[ShowDialog]\").linkbutton({ iconCls: \'fa fa-edit\' });\r\n      " +
"          $(\"a[ShowList]\").linkbutton({ iconCls: \'fa fa-edit\' });\r\n            }" +
"\r\n        });\r\n\r\n        //setTextAlign(\"LotName\", \"text-align:center;\")\r\n      " +
"  //setTextAlign(\"EngAmountArrange\", \"text-align:center;\")\r\n        //setTextAli" +
"gn(\"BalanceAmount\", \"text-align:center;\")\r\n        //setTextAlign(\"BalanceMoney\"" +
", \"text-align:center;\")\r\n    })\r\n\r\n    //function setTextAlign(filed, align) {\r\n" +
"    //    $(\"tr[class=\'datagrid-header-row\']\").find(\"td[field=\'\" + filed + \"\']\")" +
".children().removeAttr(\"style\").attr(\"style\", align);\r\n    //}\r\n\r\n    function S" +
"howDialog(id) {\r\n        JQ.dialog.dialog({\r\n            title: \'修改历史结算信息\',\r\n   " +
"         width: \'800\',\r\n            height: \'400\',\r\n            url: \'");

            
            #line 80 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
             Write(Url.Action("edit", "PayBalanceChangeHist",new { @area="pay"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id=' + id,
            onClose: function () {
                $(""#TechDetailGrid"").datagrid(""reload"");
            }
        });
    }

    function ShowList(id) {
        JQ.dialog.dialog({
            title: '修改历史结算信息',
            width: '800',
            height: '650',
            url: '");

            
            #line 92 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
             Write(Url.Action("list", "PayBalanceChangeHist",new { @area="pay"}));

            
            #line default
            #line hidden
WriteLiteral(@"?payBalanceAcountId=' + id
        });
    }

    function MergeCells(field, rows) {
        var merges = [];
        for (var i = 0; i < rows.length; i++) {
            if (i == 0) {
                merges.push({ index: i, rowspan: 1, val: rows[i][field] });
            }
            else {
                if (merges[merges.length - 1].val == rows[i][field]) {
                    merges[merges.length - 1].rowspan += 1;
                }
                else {
                    merges.push({ index: i, rowspan: 1, val: rows[i][field] });
                }
            }
        }
        for (var i = 0; i < merges.length; i++) {
            if (merges[i].rowspan > 1) {
                $(""#TechDetailGrid"").datagrid('mergeCells', {
                    index: merges[i].index,
                    rowspan: merges[i].rowspan,
                    field: field
                });

            }
        }
    }
</script>

<table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n    <tr>\r\n        <td");

WriteLiteral(" colspan=\"6\"");

WriteLiteral(" style=\"border:none;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"font-size:16px;font-weight:bold;\"");

WriteLiteral(">工程结算明细</span>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <th");

WriteLiteral(" style=\"width:13%;\"");

WriteLiteral(">\r\n            项目名称\r\n        </th>\r\n        <td");

WriteLiteral(" style=\"width:29%;\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(">");

            
            #line 135 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                            Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n        <th");

WriteLiteral(" style=\"width:13%;\"");

WriteLiteral(">\r\n            是否结清\r\n        </th>\r\n        <td");

WriteLiteral(" style=\"width:13%;\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" id=\"BalanceState\"");

WriteLiteral(">");

            
            #line 141 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                                Write(ViewBag.BalanceState);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n        <th");

WriteLiteral(" style=\"width:13%;\"");

WriteLiteral(">\r\n            工程产值\r\n        </th>\r\n        <td");

WriteLiteral(" style=\"width:20%;\"");

WriteLiteral(">\r\n            <label");

WriteLiteral(" id=\"Amount\"");

WriteLiteral(">");

            
            #line 147 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                          Write(ViewBag.Amount);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <th>\r\n            已分配产值\r\n  " +
"      </th>\r\n        <td>\r\n            <label");

WriteLiteral(" id=\"DistributeAmount\"");

WriteLiteral(">");

            
            #line 155 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                                    Write(ViewBag.DistributeAmount);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n        <th>\r\n            未分配产值\r\n        </th>\r\n        " +
"<td>\r\n            <label");

WriteLiteral(" id=\"UnDistributeAmount\"");

WriteLiteral(">");

            
            #line 161 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                                      Write(ViewBag.UnDistrFee);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n        <th>\r\n            已分配金额\r\n        </th>\r\n        " +
"<td>\r\n            <label");

WriteLiteral(" id=\"DistributeMoney\"");

WriteLiteral(">");

            
            #line 167 "..\..\Views\PayBalanceUserAccount\HisTechDetailInfo.cshtml"
                                   Write(ViewBag.DistributeMoney);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <td");

WriteLiteral(" colspan=\"6\"");

WriteLiteral(" style=\"border:none;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"font-size:16px;font-weight:bold;\"");

WriteLiteral(">技术人员结算信息</span>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <td");

WriteLiteral(" colspan=\"6\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"TechDetailGrid\"");

WriteLiteral("></table>\r\n        </td>\r\n    </tr>\r\n</table>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
