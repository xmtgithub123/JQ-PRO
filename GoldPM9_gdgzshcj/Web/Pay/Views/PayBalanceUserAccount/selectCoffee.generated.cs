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
    
    #line 2 "..\..\Views\PayBalanceUserAccount\selectCoffee.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceUserAccount/selectCoffee.cshtml")]
    public partial class _Views_PayBalanceUserAccount_selectCoffee_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceUserAccount_selectCoffee_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .rwpdialogdiv {\r\n        padding: 0px;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 9 "..\..\Views\PayBalanceUserAccount\selectCoffee.cshtml"
                 Write(Url.Action("TransferJson", "PayPasssWord", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?RowId=");

            
            #line 9 "..\..\Views\PayBalanceUserAccount\selectCoffee.cshtml"
                                                                                          Write(Request.QueryString["RowId"]);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    $(function () {\r\n        $(\".dialog-toolbar :last\").hide();\r\n\r\n        //" +
"数据表格\r\n        JQ.datagrid.datagrid({\r\n            JQPrimaryID: \'EmpID\',//主键的字段\r\n" +
"            JQID: \'CoffeeGrid\',//数据表格ID\r\n            JQDialogTitle: \'用户信息\',//弹出窗" +
"体标题\r\n            JQWidth: \'760\',//弹出窗体宽\r\n            JQHeight: \'100%\',//弹出窗体高\r\n " +
"           JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n            J" +
"QFields: [\"EmpID\"],//追加的字段名\r\n            JQSearch: {\r\n                id: \'JQSea" +
"rch\',\r\n                prompt: \'姓名\',\r\n                loadingType: \'datagrid\',//" +
"默认值为datagrid可以不传\r\n                $panel: $(\"#tb\")//搜索的容器，可以不传\r\n            },\r\n" +
"            url: requestUrl,//请求的地址\r\n            checkOnSelect: true,//是否包含check" +
"\r\n            toolbar: \'#tb\',//工具栏的容器ID\r\n            columns: [[\r\n              " +
"  {field: \'ck\', align: \'center\', checkbox: true},\r\n              { title: \'姓名\', " +
"field: \'EmpName\', width: \"18%\", align: \'center\', sortable: true },\r\n            " +
"  { title: \'用户名\', field: \'EmpLogin\', width: \"18%\", align: \'center\', sortable: tr" +
"ue },\r\n              { title: \'所属部门\', field: \'DepartmentName\', width: \"20%\", ali" +
"gn: \'center\', sortable: true },//, JQIsExclude: true\r\n              { title: \'管理" +
"人员系数\', field: \'PayManageCoeff\', width: \"15%\", align: \'center\', sortable: true }\r" +
"\n            ]]\r\n        });\r\n\r\n\r\n    });\r\n    function selectCoffee() {\r\n      " +
"  var rows = $(\'#CoffeeGrid\').datagrid(\'getSelections\');\r\n        if (rows.lengt" +
"h < 1) {\r\n            window.top.JQ.dialog.warning(\'您必须选择至少一项！！！\');\r\n           " +
" return;\r\n        }\r\n        for (var rowindex = 0; rowindex < rows.length; rowi" +
"ndex++) {\r\n            var tab = $(\'#mainTab\').tabs(\'getSelected\');\r\n           " +
" var index = $(\'#mainTab\').tabs(\'getTabIndex\', tab);\r\n            $(\"#mainTab\")." +
"find(\"iframe\")[index].contentWindow.copyData(rows[rowindex]);\r\n        }\r\n\r\n    " +
"    JQ.dialog.dialogClose();\r\n    }\r\n</script>\r\n\r\n<table");

WriteLiteral(" id=\"CoffeeGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"tb\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectCoffee()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral("  style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'EmpName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    <span");

WriteLiteral(" style=\"font-size:14px;color:red;margin-left:20px;\"");

WriteLiteral(">*请到绩效模板菜单中，维护管理系数(显示管理系数>0)</span>\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
