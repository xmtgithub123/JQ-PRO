﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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
    
    #line 4 "..\..\Views\BaseConfig\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseConfig/list.cshtml")]
    public partial class _Views_BaseConfig_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BaseConfig_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BaseConfig\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\BaseConfig\list.cshtml"
                     Write(Url.Action("json", "BaseConfig",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 8 "..\..\Views\BaseConfig\list.cshtml"
                     Write(Url.Action("edit", "BaseConfig",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes:");

            
            #line 11 "..\..\Views\BaseConfig\list.cshtml"
                         Write(Html.Raw( ViewBag.permission ));

            
            #line default
            #line hidden
WriteLiteral(", // [\'edit\',\'export\'],//需要显示的按钮\r\n                JQEditUrl: editUrl,//编辑的路径\r\n   " +
"             JQPrimaryID: \'ConfigID\',//主键的字段\r\n                JQID: \'BaseConfigG" +
"rid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQW" +
"idth: \'500\',//弹出窗体宽\r\n                JQHeight: \'400\',//弹出窗体高\r\n                JQ" +
"LoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQFields: " +
"[\"ConfigID\"],//追加的字段名\r\n                url: requestUrl,//请求的地址\r\n                " +
"checkOnSelect: true,//是否包含check\r\n                JQIsSearch: true,\r\n            " +
"    JQExcludeCol: [0,1],//导出execl排除的列\r\n                toolbar: \'#BaseConfigPane" +
"l\',//工具栏的容器ID\r\n                columns: [[\r\n                        { field: \'ck" +
"\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t                { ti" +
"tle: \'配置名称\', field: \'ConfigName\', width: 200, align: \'center\', sortable: true }," +
"\r\n\t\t                { title: \'配置英文标识\', field: \'ConfigEngName\', width: 150, align" +
": \'center\', sortable: true },\r\n\t\t                { title: \'配置内容\', field: \'Config" +
"Value\', width: 200, align: \'center\', sortable: true },\r\n\t\t                { titl" +
"e: \'配置备注\', field: \'ConfigNote\', width: 200, align: \'center\', sortable: true }\r\n " +
"               ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n      " +
"          buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n      " +
"          height: 25,\r\n                prompt: \'配置名称\',\r\n                onClickB" +
"utton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n             " +
"           {\r\n                            dgID: \'BaseConfigGrid\',\r\n             " +
"               loadingType: \'datagrid\',\r\n                            queryParams" +
": null\r\n                        });\r\n                }\r\n            });\r\n       " +
" });\r\n    </script>\r\n");

});

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BaseConfigGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BaseConfigPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'ConfigName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConfigEmpID\', Contract:\'>\',filedType:\'Int\' }\"");

WriteLiteral(" value=\"-1\"");

WriteLiteral(">\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
