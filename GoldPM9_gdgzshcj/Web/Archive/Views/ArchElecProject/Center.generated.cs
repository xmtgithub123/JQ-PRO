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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchElecProject/Center.cshtml")]
    public partial class _Views_ArchElecProject_Center_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ArchElecProject_Center_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ArchElecProject\Center.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

        $(function () {
            initGrid();
            //默认选中第一个
            $(""#lefttree"").treegrid({
                idField: ""id"",
                treeField: ""text"",
                border: 0,
                fit: true,
                url: """);

            
            #line 15 "..\..\Views\ArchElecProject\Center.cshtml"
                  Write(Url.Action("GetTreeList", "ArchElecProject", new { @area= "Archive"}));

            
            #line default
            #line hidden
WriteLiteral(@""",
                singleSelect: true,
                columns: [[
                    {
                        title: ""档案节点"", field: ""text"", width: 240, align: ""left"", halign: ""center"", formatter: function (value) {
                            return decodeURIComponent(value.replace(/\+/g, "" ""));
                        }
                    }
                ]],
                queryParams: { projectID: """);

            
            #line 24 "..\..\Views\ArchElecProject\Center.cshtml"
                                       Write(Request.QueryString["projectID"]);

            
            #line default
            #line hidden
WriteLiteral("\", taskGroupID: \"");

            
            #line 24 "..\..\Views\ArchElecProject\Center.cshtml"
                                                                                           Write(Request.QueryString["taskGroupID"]);

            
            #line default
            #line hidden
WriteLiteral(@""" },
                onSelect: function (node) {
                    loadGrid(node.id);
                }
            });
        });

        function loadGrid(archiveID) {
            var $archivegrid = $(""#archivegrid"");
            var options = $archivegrid.datagrid(""options"");
            options.url = """);

            
            #line 34 "..\..\Views\ArchElecProject\Center.cshtml"
                       Write(Url.Action("dirjson", "ArchElecFile", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral("?refid=\" + archiveID;\r\n            $archivegrid.datagrid(\"load\", { \"fields[]\": \"E" +
"lecFileName,ElecSize,ElecFileVersionId,CreationTime,CreatorEmpName\" });\r\n       " +
" }\r\n\r\n        function initGrid() {\r\n            var $archivegrid = $(\"#archiveg" +
"rid\");\r\n            $archivegrid.datagrid({\r\n                fit: true,\r\n       " +
"         columns: [[\r\n                    { field: \"ck\", align: \"center\", checkb" +
"ox: true, JQIsExclude: true },\r\n                    { title: \"名称\", field: \"ElecF" +
"ileName\", width: 200, align: \"left\", halign: \"center\" },\r\n                    {\r" +
"\n                        title: \"大小\", field: \"ElecSize\", width: 100, align: \"rig" +
"ht\", halign: \"center\", formatter: function (value) {\r\n                          " +
"  return getFileSizeText(value);\r\n                        }\r\n                   " +
" },\r\n                    { title: \"版本\", field: \"ElecFileVersionId\", width: 100, " +
"align: \"center\" },\r\n                    { title: \'上传时间\', field: \"CreationTime\", " +
"width: 100, align: \"center\", formatter: JQ.tools.DateTimeFormatterByT },\r\n      " +
"              { title: \'上传人\', field: \"CreatorEmpName\", width: 100, align: \"cente" +
"r\" },\r\n                ]],\r\n                pagination: true,\r\n                r" +
"ownumbers: true,\r\n                singleSelect: true,\r\n                pageSize:" +
" 20,\r\n                loadMsg: \"正在努力加载档案中...\",\r\n                border: false,\r\n" +
"            }).datagrid(\"getPager\").pagination({\r\n                pageList: [10," +
" 15, 20, 25, 30, 50],\r\n                beforePageText: \'第\',\r\n                aft" +
"erPageText: \'页    共 {pages} 页\',\r\n                displayMsg: \'当前显示 {from} - {to}" +
" 条记录   共 {total} 条记录\',\r\n                total: 0\r\n            });\r\n        }\r\n  " +
"      function getFileSizeText(size) {\r\n            if (!size && size != 0) {\r\n " +
"               return \"\";\r\n            }\r\n            var st = \"\";\r\n            " +
"if (size >= 1024 * 1024 * 1024) {\r\n                st = (size / 1024 / 1024 / 10" +
"24).toFixed(2) + \"GB\";\r\n            } else if (size >= 1024 * 1024) {\r\n         " +
"       st = (size / 1024 / 1024).toFixed(2) + \"MB\";\r\n            } else if (size" +
" >= 1024) {\r\n                st = (size / 1024).toFixed(2) + \"KB\";\r\n            " +
"} else {\r\n                st = size + \"B\";\r\n            }\r\n            return st" +
";\r\n        }\r\n    </script>\r\n");

});

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" data-options=\"region:\'west\',split:false,border:false\"");

WriteLiteral(" style=\"width:250px;border-right:1px solid #D2D2D2\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" id=\"lefttree\"");

WriteLiteral("></table>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <table");

WriteLiteral(" id=\"archivegrid\"");

WriteLiteral("></table>\r\n    </div>\r\n");

});

        }
    }
}
#pragma warning restore 1591
