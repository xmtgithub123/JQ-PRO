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
    
    #line 4 "..\..\Views\BaseHandover\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseHandover/list.cshtml")]
    public partial class _Views_BaseHandover_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BaseHandover_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BaseHandover\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\BaseHandover\list.cshtml"
                     Write(Url.Action("json", "BaseHandover",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\BaseHandover\list.cshtml"
                    Write(Url.Action("add","BaseHandover",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\BaseHandover\list.cshtml"
                     Write(Url.Action("edit", "BaseHandover",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\BaseHandover\list.cshtml"
                    Write(Url.Action("del", "BaseHandover", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n        " +
"        JQID: \'BaseHandoverGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',/" +
"/弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'600\'" +
",//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]" +
"\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                url: requestUrl,//请" +
"求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                fitColumn" +
"s: true,\r\n                JQExcludeCol: [1],//导出execl排除的列\r\n                toolb" +
"ar: \'#BaseHandoverPanel\',//工具栏的容器ID\r\n                JQSearch: {\r\n              " +
"      id: \'JQSearch\',\r\n                    prompt: \'移交原因\',\r\n                    " +
"loadingType: \'datagrid\',//默认值为datagrid可以不传\r\n                },\r\n                " +
"columns: [[\r\n                        { field: \'ck\', align: \'center\', checkbox: t" +
"rue, JQIsExclude: true },\r\n\t\t                { title: \'移交人姓名\', field: \'HandEmpNa" +
"me\', width: 80, align: \'center\', sortable: true },\r\n\t\t                { title: \'" +
"移交成员\', field: \'HandEmps\', width: 80, align: \'center\', sortable: true },\r\n\t\t     " +
"           {\r\n\t\t                    title: \'创建时间\', field: \'CreationTime\', width:" +
" 80, align: \'center\', sortable: true, formatter: function (value,row,index) {\r\n\t" +
"\t                        row.CreationTime = JQ.tools.DateTimeFormatterByT(value," +
" row, index);\r\n\t\t                        return row.CreationTime;\r\n\t\t           " +
"         }\r\n\t\t                },\r\n\t\t                { title: \'创建人姓名\', field: \'Cr" +
"eatorEmpName\', width: 80, align: \'center\', sortable: true },\r\n\t\t                " +
"{ title: \'移交原因\', field: \'HandReason\', width: 150, align: \'left\', halign: \'center" +
"\', sortable: true },\r\n\t\t                { title: \'移交备注\', field: \'HandNote\', widt" +
"h: 100, align: \'left\', halign: \'center\', sortable: true }\r\n                ]],\r\n" +
"                onRowContextMenu: function (e, index, row) { //右键时触发事件\r\n        " +
"            var $m = $(\'#userMenu\');\r\n                    $.data(document.body, " +
"\"selectTempData\", row);//把临时数据存在缓存中\r\n                    $m.menu(\'show\', {\r\n    " +
"                    left: e.pageX,\r\n                        top: e.pageY\r\n      " +
"              });\r\n                    e.preventDefault();\r\n                }\r\n " +
"           });\r\n        });\r\n\r\n        function menuHandler(item) {\r\n           " +
" var data = $.data(document.body, \"selectTempData\");//获取临时数据\r\n            if (JQ" +
".tools.isJson(data)) {\r\n                switch (item.id) {\r\n                    " +
"case \"ChangeFlow\":\r\n                        JQ.dialog.dialog({\r\n                " +
"            title: \"移交待办审批\",\r\n                            url: \'");

            
            #line 67 "..\..\Views\BaseHandover\list.cshtml"
                             Write(Url.Action("flowlistview", "BaseHandover",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?id=' + data.Id,
                            width: '1200',
                            height: '800',
                            JQID: 'BaseHandoverGrid',
                            JQLoadingType: 'datagrid',
                            iconCls: 'fa fa-plus'
                        });
                        break;
                    case ""ChangeTask"":
                        JQ.dialog.dialog({
                            title: ""移交待办任务"",
                            url: '");

            
            #line 78 "..\..\Views\BaseHandover\list.cshtml"
                             Write(Url.Action("tasklistview", "BaseHandover",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?id=' + data.Id,
                            width: '1200',
                            height: '800',
                            JQID: 'BaseHandoverGrid',
                            JQLoadingType: 'datagrid',
                            iconCls: 'fa fa-plus'
                        });
                        break;
                }
            }
            $.data(document.body, ""selectTempData"", null);//清空临时数据
            return false;
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BaseHandoverGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BaseHandoverPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'HandReason\', \'Contract\': \'like\' }\"");

WriteLiteral("/>\r\n\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"userMenu\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(" style=\"width:100px;display: none;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"ChangeFlow\"");

WriteLiteral(">移交待办审批</div>\r\n        <div");

WriteLiteral(" id=\"ChangeTask\"");

WriteLiteral(">移交待办任务</div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
