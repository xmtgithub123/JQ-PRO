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
    
    #line 4 "..\..\Views\BaseLog\Busslist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseLog/Busslist.cshtml")]
    public partial class _Views_BaseLog_Busslist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BaseLog_Busslist_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BaseLog\Busslist.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        $(function () {
            JQ.datagrid.datagrid({
                JQButtonTypes: ['export'],//需要显示的按钮
                JQPrimaryID: 'BaseLogID',//主键的字段
                JQID: 'BaseLogGrid',//数据表格ID
                JQDialogTitle: '信息',//弹出窗体标题
                JQWidth: '768',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                pagination: true,//是否分页
                url: '");

            
            #line 17 "..\..\Views\BaseLog\Busslist.cshtml"
                 Write(Url.Action("json","BaseLog",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                checkOnSelect: false,//是否包含check\r\n                //fi" +
"tColumns: true,\r\n                nowrap: false,\r\n                JQIsSearch: tru" +
"e,\r\n                toolbar: \'#BaseLogPanel\',//工具栏的容器ID\r\n                columns" +
": [[\r\n\t\t                { title: \'人员\', field: \'EmpName\', width: \'8%\', align: \'ce" +
"nter\', sortable: true },\r\n                        { title: \'日志日期\', field: \'BaseL" +
"ogDate\', width: \'12%\', align: \'center\', sortable: true, formatter: JQ.tools.Date" +
"TimeFormatterByH },\r\n\t\t                { title: \'记录IP\', field: \'BaseLogIP\', widt" +
"h: \'12%\', align: \'center\', sortable: true },\r\n\t\t                { title: \'引用表名称\'" +
", field: \'BaseLogRefTable\', width: \'15%\', align: \'center\', sortable: true },\r\n\t\t" +
"                { title: \'引用表ID\', field: \'BaseLogRefID\', width: \'8%\', align: \'ce" +
"nter\', sortable: true },\r\n\t\t                { title: \'日志内容\', field: \'BaseLogRefH" +
"TML\', width: \'42%\', align: \'left\', sortable: true }\r\n                ]]\r\n       " +
"     });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\'" +
",\r\n                buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n   " +
"             prompt: \'人员、IP、内容\',\r\n                onClickButton: function () {\r\n" +
"                    JQ.datagrid.searchGrid(\r\n                        {\r\n        " +
"                    dgID: \'BaseLogGrid\',\r\n                            loadingTyp" +
"e: \'datagrid\',\r\n                            queryParams: null\r\n                 " +
"       });\r\n                }\r\n            });\r\n        });\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BaseLogGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BaseLogPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n        <input");

WriteLiteral("   type=\"hidden\"");

WriteLiteral(" value=\"10\"");

WriteLiteral(" JQWhereOptions=\"[{ Key:\'BaseLogTypeID\', Contract:\'>=\',filedType:\'Int\'}]\"");

WriteLiteral("/>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EmpName,BaseLogIP,BaseLogRefHTML\', \'Contract\': \'like\' " +
"}\"");

WriteLiteral("/>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
