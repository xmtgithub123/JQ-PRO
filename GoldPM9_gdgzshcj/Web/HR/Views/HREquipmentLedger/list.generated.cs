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
    
    #line 4 "..\..\Views\HREquipmentLedger\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/HREquipmentLedger/list.cshtml")]
    public partial class _Views_HREquipmentLedger_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_HREquipmentLedger_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\HREquipmentLedger\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\HREquipmentLedger\list.cshtml"
                     Write(Url.Action("json", "HREquipmentLedger",new { @area="HR"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\HREquipmentLedger\list.cshtml"
                    Write(Url.Action("add","HREquipmentLedger",new {@area="HR" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\HREquipmentLedger\list.cshtml"
                     Write(Url.Action("edit", "HREquipmentLedger",new {@area="HR" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\HREquipmentLedger\list.cshtml"
                    Write(Url.Action("del", "HREquipmentLedger", new { @area = "HR" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            var btnPer=");

            
            #line 12 "..\..\Views\HREquipmentLedger\list.cshtml"
                  Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral(";\r\n            JQ.datagrid.datagrid({\r\n                JQButtonTypes: [\'add\', \'ed" +
"it\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAddUrl: addUrl, //添加的路径\r\n    " +
"            JQEditUrl: editUrl,//编辑的路径\r\n                JQDelUrl: delUrl, //删除的路" +
"径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'HREquipment" +
"LedgerGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n            " +
"    JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n          " +
"      JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQ" +
"Fields: [\"Id\"],//追加的字段名\r\n                JQSearch: {\r\n                    id: \'J" +
"QSearch\',\r\n                    prompt: \'设备名称、设备型号\',\r\n                    loading" +
"Type: \'datagrid\',//默认值为datagrid可以不传\r\n                },\r\n                url: re" +
"questUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n             " +
"   fitColumns: false,\r\n                nowrap: false,\r\n                JQExclude" +
"Col:[1,8,9],\r\n                toolbar: \'#HREquipmentLedgerPanel\',//工具栏的容器ID\r\n   " +
"             columns: [[\r\n                    { field: \'ck\', align: \'center\', ch" +
"eckbox: true, JQIsExclude: true },\r\n\t\t            { title: \'设备名称\', field: \'Equip" +
"mentName\', width: \'10%\', align: \'center\', sortable: true },\r\n\t\t            { tit" +
"le: \'设备型号\', field: \'EquipmentType\', width: \'10%\', align: \'center\', sortable: tru" +
"e },\r\n\t\t            { title: \'采购时间\', field: \'PurchasingTime\', width: \'10%\', alig" +
"n: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t    " +
"        { title: \'报废时间\', field: \'ScrapTime\', width: \'10%\', align: \'center\', sort" +
"able: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t            { title: " +
"\'设备保管人姓名\', field: \'EquipmentEmpName\', width: \'10%\', align: \'center\', sortable: t" +
"rue },\r\n\t\t            {\r\n\t\t                title: \'设备状态\', field: \'ShowState\', wi" +
"dth: \'5%\', align: \'center\', sortable: true, formatter: function (data, row) {\r\n\t" +
"\t                    var show = \"\";\r\n\t\t                    if (row.State == 0) {" +
"\r\n\t\t                        show = \"<font color=\'green\'>正常</font>\";\r\n           " +
"                     row.ShowState = \"正常\";\r\n\t\t                    } else if (row" +
".State == 1) {\r\n\t\t                        show = \"<font color=\'red\'>报废</font>\";\r" +
"\n                                row.ShowState = \"报废\";\r\n\t\t                    } " +
"else if (row.State == 2) {\r\n\t\t                        show = \"<font color=\'gray\'" +
">维修</font>\";\r\n                                row.ShowState = \"维修\";\r\n\t\t         " +
"           } else {\r\n\t\t                        show = \"<font color=\'blue\'>其它</fo" +
"nt>\";\r\n                                row.ShowState = \"其它\";\r\n\t\t                " +
"    }\r\n\t\t                    if($.inArray(\"edit\", btnPer)!=-1||$.inArray(\"alledi" +
"t\", btnPer)!=-1){\r\n                                return show;\r\n\t\t             " +
"       }\r\n\t\t                }\r\n\t\t            },\r\n                    { field: \'J" +
"QFiles\', title: \'附件\', align: \'center\', width: \'5%\', JQIsExclude: true, JQRefTabl" +
"e: \'HREquipmentLedger\' },\r\n                    {\r\n                        field:" +
" \'CZ\', title: \'操作\', align: \'center\', width: 100, JQIsExclude: true, formatter: f" +
"unction (value, row, index) {\r\n                            if($.inArray(\"edit\",b" +
"tnPer)!=-1||$.inArray(\"alledit\",btnPer)!=-1){\r\n                                r" +
"eturn \"<a href=\\\"javascript:void(0)\\\"  JQPermissionName=\\\"add\\\" id=\\\"mb\" + index" +
" + \"\\\" dataid=\\\"\" + row.Id + \"\\\" flag=\\\"menubutton\\\" >操作</a>\";\r\n                " +
"            }\r\n                        }\r\n                    }\r\n               " +
" ]],\r\n                rowStyler: function (index, row) {\r\n                    if" +
" (row.Id != undefined && row.State == \"0\") {\r\n                        debugger;\r" +
"\n                        if (Date.jsonStringToDateString(row.ScrapTime, \"yyyy-MM" +
"-dd\") != \"\") {\r\n                            var timeNow = \'");

            
            #line 77 "..\..\Views\HREquipmentLedger\list.cshtml"
                                      Write(ViewBag.TimeNow);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                            var difDay = Date.jsonStringToDate(row.ScrapTime)" +
".dateDiff(\"d\", timeNow);\r\n                            if (difDay > -30) {\r\n     " +
"                           return \'background-color:pink;color:blue;font-weight:" +
"bold;\';\r\n                            }\r\n                        }\r\n             " +
"       }\r\n                }\r\n            });\r\n        });\r\n\r\n        $.extend($." +
"fn.datagrid.defaults.view, {\r\n            onAfterRender: function () {\r\n        " +
"        $(\"a[flag=\'menubutton\']\").mouseover(function () {\r\n                    $" +
"(\"#list\").attr(\"rowid\", $(this).attr(\"dataid\"));\r\n                    $(\"#div1\")" +
".html(\'<div class=\"menu-text\" style=\"height: 20px; line-height: 20px;\">正常</div>\'" +
")\r\n                    $(\"#div2\").html(\'<div class=\"menu-text\" style=\"height: 20" +
"px; line-height: 20px;\">报废</div>\')\r\n                    $(\"#div3\").html(\'<div cl" +
"ass=\"menu-text\" style=\"height: 20px; line-height: 20px;\">维修</div>\')\r\n           " +
"         $(\"#div4\").html(\'<div class=\"menu-text\" style=\"height: 20px; line-heigh" +
"t: 20px;\">其它</div>\')\r\n                }).menubutton({\r\n                    iconC" +
"ls: \'fa fa-edit\',\r\n                    menu: \'#list\'\r\n                });\r\n     " +
"       }\r\n        });\r\n\r\n        function menuHandler(item) {\r\n            JQ.di" +
"alog.confirm(\"是否确认修改此设备状态\", function () {\r\n                var state = 0;\r\n     " +
"           switch (item.text) {\r\n                    case \"正常\":\r\n               " +
"         state = 0;\r\n                        break;\r\n                    case \"报" +
"废\":\r\n                        state = 1;\r\n                        break;\r\n       " +
"             case \"维修\":\r\n                        state = 2;\r\n                   " +
"     break;\r\n                    case \"其它\":\r\n                        state = 3;\r" +
"\n                        break;\r\n                }\r\n                var key = $(" +
"\"#list\").attr(\"rowid\");\r\n\r\n                jQuery.post(\"");

            
            #line 122 "..\..\Views\HREquipmentLedger\list.cshtml"
                        Write(Url.Action("editState", "HREquipmentLedger", new {area="HR"}));

            
            #line default
            #line hidden
WriteLiteral("\", \"id=\" + key+\"&state=\"+state+\"\", function (data) {\r\n                    jQuery(" +
"\"#HREquipmentLedgerGrid\").datagrid(\'reload\');\r\n                },\"json\");\r\n\r\n   " +
"         });\r\n        }\r\n\r\n    </script>\r\n\r\n");

WriteLiteral("    ");

            
            #line 131 "..\..\Views\HREquipmentLedger\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"HREquipmentLedgerGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"HREquipmentLedgerPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EquipmentName,EquipmentType\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" id=\"list\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" id=\"div1\"");

WriteLiteral(">正常</div>\r\n            <div");

WriteLiteral(" id=\"div2\"");

WriteLiteral(">报废</div>\r\n            <div");

WriteLiteral(" id=\"div3\"");

WriteLiteral(">维修</div>\r\n            <div");

WriteLiteral(" id=\"div4\"");

WriteLiteral(">其它</div>\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
