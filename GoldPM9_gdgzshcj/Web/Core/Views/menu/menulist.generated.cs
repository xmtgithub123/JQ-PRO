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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/menu/menulist.cshtml")]
    public partial class _Views_menu_menulist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_menu_menulist_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\menu\menulist.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.treegrid.treegrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'export\', \'refreshByTree\'],\r\n                JQAdd" +
"Url: \'");

            
            #line 9 "..\..\Views\menu\menulist.cshtml"
                      Write(Url.Action("addmenu"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                JQEditUrl: \'");

            
            #line 10 "..\..\Views\menu\menulist.cshtml"
                       Write(Url.Action("editmenu"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                JQDelUrl: \'");

            
            #line 11 "..\..\Views\menu\menulist.cshtml"
                      Write(Url.Action("delmenu"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                JQPrimaryID: \'id\',\r\n                JQID: \'tt\',\r\n            " +
"    JQDialogTitle: \'菜单信息\',\r\n                JQWidth: \'560\',\r\n                JQH" +
"eight: \'80%\',\r\n                JQLoadingType: \'treegrid\',\r\n                url: " +
"\'");

            
            #line 18 "..\..\Views\menu\menulist.cshtml"
                 Write(Url.Action("menujsonlist", "menu",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?isAll=true',
                toolbar: '#tb',
                JQExcludeCol: [6, 7],
                animate: true,
                columns: [[
                   { title: '主键', field: 'id', width: 60, align: 'center', hidden: true },
                   { title: '菜单名称', field: 'text', width: 250, align: 'left' },
                   { title: '菜单路径', field: 'attributes', width: 380, align: 'center' },
                   { title: '菜单排序', field: 'orderCode', width: 100, align: 'left' },
                   {
                       title: '菜单图标', field: 'MenuImageUrl', width: 80, align: 'center', formatter: function (v) {
                           return ""<div class='"" + v + ""'/>"";
                       }
                   }
                ]]
            });
            $('#MenuCombox').combobox({
                url: '");

            
            #line 35 "..\..\Views\menu\menulist.cshtml"
                 Write(Url.Action("PermitTopjson", "menu", new { @area = "Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
                valueField: 'MenuID',
                textField: 'MenuName',
                panelHeight: ""auto"",
                editable: false, //不允许手动输入
                onLoadSuccess: function () { //数据加载完毕事件
                    var data = $('#MenuCombox').combobox('getData');
                    if (data.length > 0) {
                        $(""#MenuCombox"").combobox('select', data[0].MenuID);
                    }
                },
                onSelect: function (node) {
                    $('#tt').treegrid('load', {
                        MenuID: $(""#MenuCombox"").combobox('getValue'),
                    });
                }
            });
        });

        function updateMove(type) {
            var row = $('#tt').treegrid('getSelections');
            if (row.length != 1) {
                window.top.JQ.dialog.warning('您必须选择至少一项进行操作！！！');
            }
            else {
                window.top.JQ.dialog.confirm(""您是否确定要移动该数据？"", function () {
                    var id = row[0]['id'];
                    JQ.tools.ajax({
                        url:  '");

            
            #line 63 "..\..\Views\menu\menulist.cshtml"
                          Write(Url.Action("movemenu", "menu", new { @area = "Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
                        data: { id: id, type: type },
                        succesCollBack: function () {
                            $('#tt').treegrid('reload');
                        }
                    });
                }, null)
            }
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"tt\"");

WriteLiteral("> </table>\r\n    <div");

WriteLiteral(" id=\"tb\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-arrow-up\'\"");

WriteLiteral(" onclick=\"updateMove(1)\"");

WriteLiteral(">上移</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-arrow-down\'\"");

WriteLiteral(" onclick=\"updateMove(2)\"");

WriteLiteral(">下移</a>\r\n            <select");

WriteLiteral(" id=\"MenuCombox\"");

WriteLiteral(" name=\"MenuCombox\"");

WriteLiteral(" style=\"width:100px;height:28px;\"");

WriteLiteral(" data-options=\"editable: false\"");

WriteLiteral("></select>\r\n        </span>\r\n    </div>\r\n");

});

        }
    }
}
#pragma warning restore 1591