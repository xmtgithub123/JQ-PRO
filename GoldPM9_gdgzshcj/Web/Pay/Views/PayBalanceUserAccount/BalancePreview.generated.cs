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
    
    #line 4 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceUserAccount/BalancePreview.cshtml")]
    public partial class _Views_PayBalanceUserAccount_BalancePreview_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceUserAccount_BalancePreview_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        $(function () {
            $(""#TotalProduct"").text(parseFloat($(""#TotalProduct"").text()).toFixed(2).toString());

            $(""#Dept"").combobox({
                prompt: '下拉选择部门',
                valueField: 'id',
                textField: 'text',
                url: ' ");

            
            #line 14 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
                  Write(Url.Action("treejson", "basedata", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?engName=department',
                onChange: function (newVal, oldVal) {
                    if (newVal != ""0"") {
                        var  Money= parseFloat($(""#PerMoney"").numberspinner('getValue'));
                        var product = parseFloat($(""#TotalProduct"").text());
                        var PerMoney = 0.00;
                        if (product != 0)
                        {
                            PerMoney=Money / product;
                        }
                        
                        $(""#AllPersonBallance"").datagrid(""reload"", '");

            
            #line 25 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
                                                               Write(Url.Action("GetPersonBalanceInfo", "PayBalanceUserAccount", new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?DeptID=\' + newVal + \"&PerMoney=\"+PerMoney);\r\n                        //var PerMo" +
"ney = parseFloat($(\"#PerMoney\").numberspinner(\'getValue\'));\r\n                   " +
"     //if (PerMoney > 0) {\r\n                        //    var product = parseFlo" +
"at($(\"#TotalProduct\").text());\r\n                        //    var money = 0;\r\n  " +
"                      //    if (product != 0) {\r\n                        //     " +
"   money = PerMoney / product;\r\n                        //        var rows = $(\"" +
"#AllPersonBallance\").datagrid(\"getRows\");\r\n                        //        con" +
"sole.log(rows);\r\n                        //        if (rows.length > 0) {\r\n     " +
"                   //            for (var index = 0; index < rows.length; index+" +
"+) {\r\n                        //                var targetvalue = (parseFloat(ro" +
"ws[index][\"EmpProduct\"]) * money).toFixed(2);\r\n                        //       " +
"         $(\'#AllPersonBallance\').datagrid(\'updateRow\', {\r\n                      " +
"  //                    index: index,\r\n                        //               " +
"     row: {\r\n                        //                        EmpMoney: targetv" +
"alue\r\n                        //                    }\r\n                        /" +
"/                });\r\n                        //                rows[index][\"Emp" +
"Money\"] = parseFloat(rows[index][\"EmpProduct\"]) * money;\r\n                      " +
"  //                $(\'#AllPersonBallance\').datagrid(\'refreshRow\', index);\r\n    " +
"                    //            }\r\n                        //            //con" +
"sole.log(rows);\r\n                        //            ///$(\"#AllPersonBallance\"" +
").datagrid(\"loadData\", rows);\r\n                        //        }\r\n            " +
"            //    }\r\n                        //}\r\n                    }\r\n       " +
"         }\r\n            });\r\n\r\n            $(\"#AllPersonBallance\").datagrid({\r\n " +
"               iconCls: \'icon-edit\',\r\n                multiSelect: true,\r\n      " +
"          rownumbers: true,\r\n                fitColumns: true,\r\n                " +
"url: \'");

            
            #line 60 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
                 Write(Url.Action("GetPersonBalanceInfo", "PayBalanceUserAccount", new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                height: \'350\',\r\n                columns: [[\r\n         " +
"            {\r\n                         field: \'ck\', align: \'center\', checkbox: " +
"true\r\n                     }, {\r\n\r\n                         title: \'部门名称\', field" +
": \'DeptName\', width: \"25%\", align: \'center\'\r\n                     },\r\n          " +
"           {\r\n\r\n                         title: \'姓名\', field: \'EmpName\', width: \"" +
"25%\", align: \'center\'\r\n                     },\r\n                     {\r\n\r\n      " +
"                   title: \'产值\', field: \'EmpProduct\', width: \"25%\", align: \'right" +
"\',\r\n                         formatter: function (val, row) {\r\n                 " +
"            return parseFloat(row.EmpProduct).toFixed(2).toString();//显示二位小数\r\n  " +
"                       }\r\n                     },\r\n                     {\r\n     " +
"                    title: \'金额\', field: \'EmpMoney\', width: \"20%\", align: \'right\'" +
",\r\n                         formatter: function (val, row) {\r\n                  " +
"           return parseFloat(row.EmpMoney).toFixed(2).toString();//显示二位小数\r\n     " +
"                    }\r\n                     }\r\n                ]]\r\n            }" +
");\r\n\r\n            $(\"#PerMoney\").numberspinner({\r\n                precision: 2,\r" +
"\n                onChange: function (newVal, oldVal) {\r\n                    if (" +
"isNaN(newVal)) {\r\n                        JQ.dialog.warning(\"请输入数字\");\r\n         " +
"               $(this).numberspinner(\'setValue\', \'0.00\');\r\n                     " +
"   $(\"#Money\").text(\"0.00\");\r\n                    }\r\n                    else {\r" +
"\n                        var PerMoney = parseFloat($(this).numberspinner(\'getVal" +
"ue\'));\r\n                        if (PerMoney < 0) {\r\n                           " +
" JQ.dialog.warning(\"不能为负值\");\r\n                            $(this).numberspinner(" +
"\'setValue\', \'0.00\');\r\n                            $(\"#Money\").text(\"0.00\");\r\n\r\n " +
"                       }\r\n                        else {\r\n                      " +
"      if (PerMoney > 0) {\r\n                                var product = parseFl" +
"oat($(\"#TotalProduct\").text());\r\n                                var money = 0;\r" +
"\n                                if (product != 0) {\r\n                          " +
"          money = PerMoney / product;\r\n                                    var r" +
"ows = $(\"#AllPersonBallance\").datagrid(\"getRows\");\r\n                            " +
"        if (rows.length > 0) {\r\n                                        for (var" +
" index = 0; index < rows.length; index++) {\r\n                                   " +
"         var targetvalue = (parseFloat(rows[index][\"EmpProduct\"]) * money).toFix" +
"ed(2);\r\n                                            $(\'#AllPersonBallance\').data" +
"grid(\'updateRow\', {\r\n                                                index: inde" +
"x,\r\n                                                row: {\r\n                    " +
"                                EmpMoney: targetvalue\r\n                         " +
"                       }\r\n                                            });\r\n     " +
"                                   }\r\n                                    }\r\n   " +
"                                 $(\"#Money\").text(money.toFixed(6).toString());\r" +
"\n                                }\r\n                            }\r\n             " +
"               else {\r\n                                $(this).numberspinner(\'se" +
"tValue\', \'0.00\');\r\n                                $(\"#Money\").text(\"0.00\");\r\n  " +
"                          }\r\n                        }\r\n                    }\r\n " +
"               }\r\n            });\r\n        })\r\n\r\n        function Balance() {\r\n " +
"           var product = parseFloat($(\"#TotalProduct\").text());\r\n            if " +
"(product != 0) {\r\n                var PerMoney = parseFloat($(\"#PerMoney\").numbe" +
"rspinner(\'getValue\'));\r\n                if (PerMoney <= 0) {\r\n                  " +
"  JQ.dialog.warning(\"请输入大于0的分配金额！！！\");\r\n                }\r\n                else " +
"{\r\n                    var Name = $(\"#Name\").textbox(\'getValue\');\r\n             " +
"       if (Name != \"\") {\r\n                        var product = parseFloat($(\"#T" +
"otalProduct\").text());\r\n                        var money = PerMoney / product;\r" +
"\n                        $.post(\'");

            
            #line 148 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
                           Write(Url.Action("Balance", "PayBalanceUserAccount",new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral(@"', { Name: Name, money: money },
                            function (result) {
                                JQ.dialog.info(""结算成功"");
                                $(""#AllPersonBallance"").datagrid(""reload"");
                                $(""#Name"").textbox('setValue', '');
                                $(""#TotalProduct"").text(""0.00"");
                                $(""#PerMoney"").numberspinner(""setValue"", ""0.00"");
                                $(""#Money"").text(""0.00"");
                            });
                    }
                    else {
                        JQ.dialog.warning(""请输入批次名称！！！"");
                    }
                }
            }
            else {
                JQ.dialog.warning(""当前产值为0，无法进行分配"");
            }
        }


    </script>
");

WriteLiteral("    ");

            
            #line 170 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"JsonRows\"");

WriteLiteral(" id=\"JsonRows\"");

WriteLiteral(" />\r\n    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(" style=\"width:95%;\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"5\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" style=\"font-size:16px;\"");

WriteLiteral(">本批次结算信息</span>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" style=\"width:15%;\"");

WriteLiteral(">\r\n                总产值:\r\n            </th>\r\n            <td");

WriteLiteral(" style=\"width:25%;\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"TotalProduct\"");

WriteLiteral(">");

            
            #line 186 "..\..\Views\PayBalanceUserAccount\BalancePreview.cshtml"
                                    Write(ViewBag.TotalProduct);

            
            #line default
            #line hidden
WriteLiteral("</label>\r\n            </td>\r\n            <th");

WriteLiteral(" style=\"width:18%\"");

WriteLiteral(">\r\n                查看部门:\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"2\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" id=\"Dept\"");

WriteLiteral(" style=\"width:200px;\"");

WriteLiteral("></select>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n   " +
"             本次分配奖金金额\r\n            </th>\r\n            <td>\r\n                <inp" +
"ut");

WriteLiteral(" id=\"PerMoney\"");

WriteLiteral(" name=\"PerMoney\"");

WriteLiteral(" style=\"width:120px;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" data-options=\"min:0\"");

WriteLiteral(" validtype=\"length[0,15]\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n                <label");

WriteLiteral(" id=\"Money\"");

WriteLiteral(" style=\"margin-left:40px;\"");

WriteLiteral(">0.00</label>元/产值\r\n            </td>\r\n            <th>\r\n                结算批次名称\r\n " +
"           </th>\r\n            <td");

WriteLiteral(" colspan=\"2\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"Name\"");

WriteLiteral(" style=\"width:200px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" />\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" jqpermissionname=\"add\"");

WriteLiteral(" onclick=\"Balance();\"");

WriteLiteral(">正式结算</a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"5\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" style=\"font-size:16px;\"");

WriteLiteral(">所有人员结算信息</span>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <td" +
"");

WriteLiteral(" colspan=\"5\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"AllPersonBallance\"");

WriteLiteral("></table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591