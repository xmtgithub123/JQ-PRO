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
    
    #line 2 "..\..\Views\DesExch\ExchModelData.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesExch/ExchModelData.cshtml")]
    public partial class _Views_DesExch_ExchModelData_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesExch_ExchModelData_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .rwpdialogdiv {\r\n        padding: 0px;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        $("".dialog-toolbar :last"").hide();
        JQ.datagrid.datagrid({
            JQButtonTypes: [],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'ExchDataGrid',//数据表格ID
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: false,
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 18 "..\..\Views\DesExch\ExchModelData.cshtml"
             Write(Url.Action("ExchModelJson", "DesExch", new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?SpecId=");

            
            #line 18 "..\..\Views\DesExch\ExchModelData.cshtml"
                                                                                   Write(Request.Params["SpecId"]);

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            JQMergeCells:" +
" { fields: [\'PhaseName\', \'ModelExchangeName\', \'ModelExchangeContent\'] },\r\n      " +
"      toolbar: \'#ExchDataPanel\',//工具栏的容器ID\r\n            columns: [[\r\n           " +
"     { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n     " +
"           { title: \'阶段\', field: \'PhaseName\', width: 150, align: \'center\', sorta" +
"ble: true },\r\n                { title: \'专业\', field: \'ExchSpecName\', width: 200, " +
"align: \'center\', sortable: true },\r\n                { title: \'资料名称\', field: \'Exc" +
"hTitle\', width: 200, align: \'center\', sortable: true },\r\n                { title" +
": \'资料内容\', field: \'ExchContent\', width: 200, align: \'center\', sortable: true },\r\n" +
"                { title: \'接收专业\', field: \'RecSpecName\', width: 200, align: \'cente" +
"r\', sortable: true }\r\n            ]],\r\n\r\n        });\r\n        $(\"#KeyJQSearch\")." +
"textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa-search\'" +
",\r\n            height: 25,\r\n            prompt: \'资料名称\',\r\n            onClickButt" +
"on: function () {\r\n                JQ.datagrid.searchGrid(\r\n            {\r\n     " +
"           dgID: \'ExchDataGrid\',\r\n                loadingType: \'datagrid\',\r\n    " +
"            queryParams: { KeyJQSearch: $(\"#KeyJQSearch\").textbox(\'getValue\') }\r" +
"\n            });\r\n            }\r\n        });\r\n    });\r\n\r\n    function selectExch" +
"Data() {\r\n        var rows = $(\'#ExchDataGrid\').datagrid(\'getSelections\');\r\n    " +
"    if (rows.length < 1) {\r\n            window.top.JQ.dialog.warning(\'您必须选择至少一项！" +
"！！\');\r\n            return;\r\n        }\r\n        var data = JSON.stringify(rows);\r" +
"\n        JQ.tools.ajax({\r\n            url: \'");

            
            #line 56 "..\..\Views\DesExch\ExchModelData.cshtml"
             Write(Url.Action("SaveInsertExchModelData", "DesExch"));

            
            #line default
            #line hidden
WriteLiteral("?projID=");

            
            #line 56 "..\..\Views\DesExch\ExchModelData.cshtml"
                                                                      Write(ViewBag.projID);

            
            #line default
            #line hidden
WriteLiteral("&taskGroupId=");

            
            #line 56 "..\..\Views\DesExch\ExchModelData.cshtml"
                                                                                                  Write(ViewBag.taskGroupId);

            
            #line default
            #line hidden
WriteLiteral(@"',
            data: 'JsonRows='+data,
            succesCollBack: function (data) {
                closeDialog();
            }
        });


    }
    //关闭当前弹出对话框
    function closeDialog()
    {
        JQ.dialog.dialogClose();
    }
</script>

<table");

WriteLiteral(" id=\"ExchDataGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"ExchDataPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectExchData()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n    <input");

WriteLiteral(" id=\"KeyJQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ModelExchangeName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
