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
    
    #line 2 "..\..\Views\BussCustomerEvaluate\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussCustomerEvaluate/list.cshtml")]
    public partial class _Views_BussCustomerEvaluate_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussCustomerEvaluate_list_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    JQ.datagrid.datagrid({\r\n        JQButtonTypes: [\'add\', \'edit\', \'del\', \'e" +
"xport\'],//需要显示的按钮\r\n        JQAddUrl: \'");

            
            #line 8 "..\..\Views\BussCustomerEvaluate\list.cshtml"
              Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',//添加的路径\r\n        JQEditUrl: \'");

            
            #line 9 "..\..\Views\BussCustomerEvaluate\list.cshtml"
               Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',//编辑的路径\r\n        JQDelUrl: \'");

            
            #line 10 "..\..\Views\BussCustomerEvaluate\list.cshtml"
              Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
        JQPrimaryID: 'Id',//主键的字段
        JQID: 'BussCustomerEvaluateGrid',//数据表格ID
        JQDialogTitle: '信息',//弹出窗体标题
        JQWidth: '1000',//弹出窗体宽
        JQHeight: '100%',//弹出窗体高
        JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
        url: '");

            
            #line 17 "..\..\Views\BussCustomerEvaluate\list.cshtml"
         Write(Url.Action("json","BussCustomerEvaluate",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("?CustID=");

            
            #line 17 "..\..\Views\BussCustomerEvaluate\list.cshtml"
                                                                                    Write(Request.QueryString["CustID"]);

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n        checkOnSelect: true,//是否包含check\r\n        JQExcludeCol: [1, 9]," +
"//导出execl排除的列\r\n        toolbar: \'#BussCustomerEvaluatePanel\',//工具栏的容器ID\r\n       " +
" columns: [[\r\n          { field: \'ck\', align: \'center\', checkbox: true, JQIsExcl" +
"ude: true },\r\n{ title: \'顾客名称\', field: \'CustName\', width: \'15%\', align: \'left\', s" +
"ortable: true },\r\n{ title: \'顾客类型\', field: \'CustTypeName\', width: \'10%\', align: \'" +
"center\', sortable: true },\r\n{ title: \'项目编号\', field: \'ProjNumber\', width: \'10%\', " +
"align: \'center\', sortable: true },\r\n{ title: \'项目名称\', field: \'ProjName\', width: \'" +
"18%\', align: \'center\', sortable: true },\r\n{ title: \'评价时间\', field: \'EvalateDate\'," +
" width: \'10%\', align: \'center\', sortable: true, formatter: JQ.tools.DateTimeForm" +
"atterByT },\r\n{ title: \'评价结果\', field: \'EvaluateResult\', width: \'10%\', align: \'cen" +
"ter\', sortable: true },\r\n{ title: \'评价内容\', field: \'EvaluaterNote\', width: \'15%\', " +
"align: \'center\', sortable: true },\r\n                {\r\n                    field" +
": \'JQFiles\', title: \'附件\', align: \'center\', width: \"8%\", JQIsExclude: true, JQExc" +
"ludeFlag: \"grid_files\", JQRefTable: \'BussCustEvaluate\',\r\n                    for" +
"matter: function (value, row) {\r\n                        return \"<span id=\\\"grid" +
"_files_\" + row.Id + \"\\\"></span>\"\r\n                    }\r\n                }]]\r\n  " +
"  });\r\n    $(\"#JQSearch\").textbox({\r\n        buttonText: \'筛选\',\r\n        buttonIc" +
"on: \'fa fa-search\',\r\n        height: 25,\r\n        prompt: \'评价内容\',\r\n        query" +
"Options: { Key: \'EvaluaterNote\', Contract: \'like\' },\r\n        onClickButton: fun" +
"ction () {\r\n            JQ.datagrid.searchGrid(\r\n                {\r\n            " +
"        dgID: \'BussCustomerEvaluateGrid\',\r\n                    loadingType: \'dat" +
"agrid\',\r\n                    queryParams: null\r\n                });\r\n        }\r\n" +
"    });\r\n\r\n</script>\r\n");

            
            #line 54 "..\..\Views\BussCustomerEvaluate\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n\r\n\r\n\r\n\r\n<table");

WriteLiteral(" id=\"BussCustomerEvaluateGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"BussCustomerEvaluatePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n    </span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EvaluaterNote\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n</div>\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
