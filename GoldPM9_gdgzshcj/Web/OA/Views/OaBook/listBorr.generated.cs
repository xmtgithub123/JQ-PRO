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
    
    #line 4 "..\..\Views\OaBook\listBorr.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaBook/listBorr.cshtml")]
    public partial class _Views_OaBook_listBorr_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaBook_listBorr_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaBook\listBorr.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\OaBook\listBorr.cshtml"
                     Write(Url.Action("json", "OaBook",new { @area="OA"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\OaBook\listBorr.cshtml"
                    Write(Url.Action("add","OaBook",new {@area= "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\OaBook\listBorr.cshtml"
                     Write(Url.Action("edit", "OaBook",new {@area= "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\OaBook\listBorr.cshtml"
                    Write(Url.Action("del", "OaBook", new { @area = "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n        $.extend($.fn.datagrid.defaults.view, {\r\n            onAfterRender:" +
" function () {\r\n                $(\"a[flag=\'menubutton\']\").mouseover(function () " +
"{\r\n                    document.getElementById(\"mm3\").setAttribute(\"rowid\", this" +
".getAttribute(\"dataid\"));\r\n                }).menubutton({\r\n                    " +
"iconCls: \'icon-edit\',\r\n                    menu: \'#mm3\'\r\n                });\r\n  " +
"          }\r\n        });\r\n\r\n        $(function () {\r\n            //$(\"#JQSearch\"" +
").textbox({\r\n            //    buttonText: \'筛选\',\r\n            //    buttonIcon: " +
"\'fa fa-search\',\r\n            //    height: 25,\r\n            //    prompt: \'请输入图书" +
"名称或编号\',\r\n            //    onClickButton: function () {\r\n            //        r" +
"efreshGrid();\r\n            //    }\r\n            //});\r\n\r\n            JQ.datagrid" +
".datagrid({\r\n                //JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],/" +
"/需要显示的按钮\r\n                //JQAddUrl: addUrl, //添加的路径\r\n                //JQEditU" +
"rl: editUrl,//编辑的路径\r\n                //JQDelUrl: delUrl, //删除的路径\r\n              " +
"  JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'OaBookGrid\',//数据表格ID\r\n      " +
"          JQDialogTitle: \'图书管理\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽" +
"\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'data" +
"grid\',//数据表格类型 [datagrid,treegrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n " +
"               url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否" +
"包含check\r\n                toolbar: \'#OaBookPanel\',//工具栏的容器ID\r\n                col" +
"umns: [[\r\n                    { field: \'ck\', align: \'center\', checkbox: true, JQ" +
"IsExclude: true },\r\n\t\t            { title: \'图书类别\', field: \'BookTypeName\', width:" +
" 100, align: \'center\', sortable: true },\r\n\t\t            { title: \'图书名称\', field: " +
"\'BookName\', width: 100, align: \'center\', sortable: true },\r\n\t\t            { titl" +
"e: \'图书编号\', field: \'BookNameNumber\', width: 100, align: \'center\', sortable: true " +
"},\r\n\t\t            { title: \'图书数量\', field: \'BookQuantity\', width: 100, align: \'ce" +
"nter\', sortable: true },\r\n                       { title: \'可借数量\', field: \'Count\'" +
", width: 100, align: \'center\', sortable: true },\r\n                     { title: " +
"\'出版商\', field: \'BookPublisher\', width: 100, align: \'center\', sortable: true },\r\n " +
"                     { title: \'作者\', field: \'BookAuthor\', width: 100, align: \'cen" +
"ter\', sortable: true },\r\n                    {\r\n                        field: \'" +
"opt\', title: \'操作\', width: 100, align: \'center\', JQIsExclude: true,\r\n            " +
"            formatter: function (value, row, index) {\r\n                         " +
"   return \"<a href=\\\"javascript:void(0)\\\" id=\\\"mb\" + index + \"\\\" dataid=\\\"\" + ro" +
"w.Id + \"\\\" flag=\\\"menubutton\\\"   >操作</a>\";\r\n                        }\r\n         " +
"           }\r\n                ]],\r\n                queryParams: getQueryParamete" +
"rs()\r\n            });\r\n        });\r\n\r\n      \r\n\r\n        //window.refreshGrid = f" +
"unction () {\r\n        //    var query = getQueryParameters();\r\n        //    que" +
"ry.bookType = $(\"#BookType\").combotree(\"getValues\");\r\n        //    $(\"#OaBookGr" +
"id\").datagrid(\"load\", query);\r\n        //}\r\n\r\n        function getQueryParameter" +
"s() {\r\n            return { text: $(\"#JQSearch\").textbox(\"getText\") };\r\n        " +
"}\r\n\r\n        function menuHandler(item) {\r\n            var key = $(\"#mm3\").attr(" +
"\"rowid\");\r\n            switch (item.text) {\r\n                case \"查看\":\r\n       " +
"             JQ.dialog.dialog({\r\n                        title: \"查看\",\r\n         " +
"               url: \'");

            
            #line 87 "..\..\Views\OaBook\listBorr.cshtml"
                         Write(Url.Action("Query"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?Id=' + key,
                        width: '1024',
                        height: '100%',
                        JQID: 'OaBookGrid',
                        JQLoadingType: 'datagrid',
                        iconCls: 'fa fa-plus'
                    });
                    break;
                default:
                    break;
            }
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"OaBookGrid\"");

WriteLiteral("></table>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
