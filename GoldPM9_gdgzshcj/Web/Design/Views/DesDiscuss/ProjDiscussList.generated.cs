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
    
    #line 4 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesDiscuss/ProjDiscussList.cshtml")]
    public partial class _Views_DesDiscuss_ProjDiscussList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesDiscuss_ProjDiscussList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
                     Write(Url.Action("json", "DesDiscuss", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        addUrl = \'");

            
            #line 8 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
             Write(Url.Action("add", "DesDiscuss", new {@area= "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?taskGroupId=");

            
            #line 8 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
                                                                                  Write(ViewBag.taskGroupId);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 9 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
                 Write(Url.Action("del", "DesDiscuss", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        editUrl = \'");

            
            #line 10 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
              Write(Url.Action("edit", "DesDiscuss", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            $(\"#JQSearch\").textbox({\r\n              " +
"  buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n              " +
"  height: 25,\r\n                prompt: \'标题\',\r\n                onClickButton: fun" +
"ction () {\r\n                    refreshGrid();\r\n                }\r\n            }" +
");\r\n\r\n            JQ.datagrid.datagrid({\r\n                JQButtonTypes: [\'add\'," +
" \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAddUrl: addUrl, //添加的路径\r\n" +
"                JQEditUrl: editUrl,\r\n                JQDelUrl: delUrl, //删除的路径\r\n" +
"                JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'DesDiscussGrid" +
"\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidt" +
"h: \'600\',//弹出窗体宽\r\n                JQHeight: \'400\',//弹出窗体高\r\n                JQLoa" +
"dingType: \'datagrid\',//数据表格类型 [datagrid,datagrid]\r\n                JQFields: [\"I" +
"d\"],//追加的字段名\r\n                url: requestUrl,//请求的地址\r\n                checkOnSe" +
"lect: true,//是否包含check\r\n                toolbar: \'#DesDiscussPanel\',//工具栏的容器ID\r\n" +
"                columns: [[\r\n                    { field: \'ck\', align: \'center\'," +
" checkbox: true, JQIsExclude: true },\r\n\t\t            { title: \'标题\', field: \'Talk" +
"Title\', width: \"60%\", halign: \'center\', align: \'left\', sortable: true },\r\n      " +
"              { title: \'回复/阅读\', field: \'HY\', width: 100, halign: \'center\', align" +
": \'center\', sortable: true },\r\n                    { title: \'创建人姓名\', field: \'Cre" +
"atorEmpName\', width: 80, halign: \'center\', align: \'center\', sortable: true },\r\n\t" +
"\t            {\r\n\t\t                title: \'创建时间\', field: \'CreationTime\', width: 8" +
"0, halign: \'center\', align: \'center\', sortable: true, formatter: function (value" +
",row,index) {\r\n\t\t                    row.CreationTime = JQ.tools.DateTimeFormatt" +
"erByT(value, row, index);\r\n\t\t                    return row.CreationTime;\r\n\t\t   " +
"             }\r\n\t\t            },\r\n                    {\r\n                       " +
" title: \'操作\', field: \'CZ\', width: 60, halign: \'center\', align: \'center\', sortabl" +
"e: true, formatter: function (value, row, index) {\r\n                            " +
"var _titleName = \"[\" + row.TalkTitle + \"]\";\r\n                            return " +
"\"<a class=\\\"easyui-linkbutton\\\" data-options=\\\"plain:true,iconCls:\'fa fa-trash\'\\" +
"\" onclick=\'openProjDiscussInfo(\\\"\" + _titleName + \"\\\",\" + row.Id + \")\'>查看</a>\";\r" +
"\n                        }\r\n                    }\r\n\r\n                ]],\r\n      " +
"          queryParams: getQueryParameters(),\r\n                JQExcludeCol: [1, " +
"6]\r\n            });\r\n\r\n\r\n\r\n        });\r\n\r\n        window.refreshGrid = function " +
"() {\r\n            var query = getQueryParameters();\r\n            query.text = $(" +
"\"#JQSearch\").textbox(\"getText\");\r\n            $(\"#DesDiscussGrid\").datagrid(\"loa" +
"d\", query);\r\n        }\r\n\r\n        function getQueryParameters() {\r\n            r" +
"eturn { taskGroupId: \'");

            
            #line 71 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
                              Write(ViewBag.taskGroupId);

            
            #line default
            #line hidden
WriteLiteral("\' };\r\n        }\r\n\r\n        function openProjDiscussInfo(title, id) {\r\n\r\n         " +
"   ");

WriteLiteral("\r\n\r\n            ");

WriteLiteral("\r\n            window.top.JQ.dialog.dialogIframe({\r\n                title: title +" +
" \" - 讨论\",\r\n                url: \'");

            
            #line 89 "..\..\Views\DesDiscuss\ProjDiscussList.cshtml"
                 Write(Url.Action("ProjDiscussListView"));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + id,\r\n                width: 830,\r\n                height: 680,\r\n         " +
"       iconCls: \"fa fa-file\"\r\n            });\r\n        }\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n\r\n    <table");

WriteLiteral(" id=\"DesDiscussGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"DesDiscussPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
