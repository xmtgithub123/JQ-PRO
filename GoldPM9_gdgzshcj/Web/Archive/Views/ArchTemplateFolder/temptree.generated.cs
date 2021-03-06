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
    
    #line 4 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchTemplateFolder/temptree.cshtml")]
    public partial class _Views_ArchTemplateFolder_temptree_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ArchTemplateFolder_temptree_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n        .datagrid-header {\r\n            position: absolute;\r\n            visib" +
"ility: hidden;\r\n        }\r\n    </style>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            $(function () {\r\n                var MbId" +
" = $(\"#MbId\").val();\r\n                var myurls = \'");

            
            #line 16 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
                         Write(Url.Action("treejson", "ArchTemplateFolder", new { @area= "Archive"}));

            
            #line default
            #line hidden
WriteLiteral(@"';
                myurls += ""?MbId="" + MbId;
                $('#lefttree').treegrid({
                    url: myurls,
                    idField: 'id',
                    treeField: 'text',
                    border: 0,
                    toolbar: '#tbleft',
                    fit: true,
                    animate: true,
                    singleSelect: true,
                    columns: [[
                       { title: '', field: 'text', width: 300, align: 'left' }
                    ]],
                    onLoadSuccess: function (row, data) {
                        FileBtnShow(true);
                    },
                    onSelect: function (node) {
                        $(""#AttID"").val(node.id);
                    }
                });
            });
        });

        function save(flag) {
            var node = $(""#lefttree"").treegrid(""getSelected"");
            if (!isNaN(node)) {
                JQ.dialog.info(""请选择"");
                return;
            }
            var id = node.id;
            var Url = '';
            if (flag == 1) {
                Url = '");

            
            #line 49 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
                  Write(Url.Action("add", "ArchTemplateFolder", new {@area="Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"';
                JQ.dialog.dialog({
                    title: ""模板目录项"",
                    url: Url + '?ParentId=' + id,
                    width: '600',
                    height: '400',
                    iconCls: 'fa fa-plus',
                    onClose: function () {
                        $(""#lefttree"").treegrid('reload');
                    }
                });
            }
            else if (flag == 2) {
                var row = $(""#lefttree"").treegrid(""getSelected"");
                if (row._parentId != undefined) {
                    Url = '");

            
            #line 64 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
                      Write(Url.Action("edit", "ArchTemplateFolder", new {@area="Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"';
                    JQ.dialog.dialog({
                        title: ""模板目录项"",
                        url: Url + '?Id=' + id,
                        width: '600',
                        height: '400',
                        iconCls: 'fa fa-plus',
                        onClose: function () {
                            $(""#lefttree"").treegrid('reload');
                        }
                    });
                } else {
                    JQ.dialog.warning(""根节点不能编辑！"");
                }
            }
            else {
                Url = '");

            
            #line 80 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
                  Write(Url.Action("del", "ArchTemplateFolder", new { @area = "Archive", }));

            
            #line default
            #line hidden
WriteLiteral(@"';
                JQ.tools.ajax({
                    doBackResult: true,
                    url: Url,
                    async: false,
                    data: 'Id=' + id
                });
                $('#lefttree').treegrid('reload');
                return;
            }
        }


        function FileBtnShow(b) {
            if (b) {
                $(""#fileadd"").hide();
                $(""#fileedit"").hide();
                $(""#filedel"").hide();
            }
            else {
                $(""#fileadd"").show();
                $(""#fileedit"").show();
                $(""#filedel"").show();
            }
        }
    </script>
");

WriteLiteral("    ");

            
            #line 106 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"treePanel\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" style=\"overflow: hidden\"");

WriteLiteral(" fit=\"true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" data-options=\"region:\'center\'\"");

WriteLiteral(" style=\"background:#eee;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"lefttree\"");

WriteLiteral("></table>\r\n        </div>\r\n        <div");

WriteLiteral(" id=\"tbleft\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" JQPermissionName=\"add\"");

WriteLiteral(" onclick=\"save(1)\"");

WriteLiteral(">新增</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-pencil\'\"");

WriteLiteral(" JQPermissionName=\"edit\"");

WriteLiteral(" onclick=\"save(2)\"");

WriteLiteral(">修改</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-trash\'\"");

WriteLiteral(" JQPermissionName=\"del\"");

WriteLiteral(" onclick=\"save(3)\"");

WriteLiteral(">删除</a>\r\n        </div>\r\n    </div>\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" id=\"AttID\"");

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4547), Tuple.Create("\"", 4568)
            
            #line 121 "..\..\Views\ArchTemplateFolder\temptree.cshtml"
, Tuple.Create(Tuple.Create("", 4555), Tuple.Create<System.Object, System.Int32>(ViewBag.MbId
            
            #line default
            #line hidden
, 4555), false)
);

WriteLiteral(" id=\"MbId\"");

WriteLiteral(" />\r\n");

});

        }
    }
}
#pragma warning restore 1591
