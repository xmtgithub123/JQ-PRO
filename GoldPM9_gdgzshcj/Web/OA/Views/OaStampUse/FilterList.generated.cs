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
    
    #line 1 "..\..\Views\OaStampUse\FilterList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaStampUse/FilterList.cshtml")]
    public partial class _Views_OaStampUse_FilterList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaStampUse_FilterList_cshtml()
        {
        }
        public override void Execute()
        {
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
            JQID: 'BussProjectGrid',//数据表格ID
            JQDialogTitle: '选择印章',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: true,
            //JQCustomSearch: [  //自定义搜索的字段
            //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
            //],
            //JQExcludeCol: [1, 4, 10],//导出execl排除的列
            //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
            //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 26 "..\..\Views\OaStampUse\FilterList.cshtml"
             Write(Url.Action("FilterJson", "OaStampUse", new { @area = "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#Bu" +
"ssProjectPanel\',//工具栏的容器ID\r\n            columns: [[\r\n                { field: \'c" +
"k\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                { tit" +
"le: \'印章名称\', field: \'StampName\', width: \"40%\", align: \'center\', sortable: true }," +
"\r\n                { title: \'印章类型\', field: \'StampTypeID\', width: \"15%\", align: \'c" +
"enter\', sortable: true },\r\n                {\r\n                    title: \'印章状态\'," +
" field: \'StampStatusID\', width: \"10%\", align: \'center\', sortable: true,\r\n       " +
"             formatter: function (val, row) {\r\n                        return va" +
"l == \"作废\" ? \"<span style=\'color:red\'>作废</span>\" : \"<span style=\'color:green\'>使用中" +
"</span>\";\r\n                    }\r\n                },\r\n                {\r\n       " +
"             title: \'最近借出记录\', field: \'LoanRecord\', width: \"30%\", align: \'center\'" +
", sortable: true\r\n                }\r\n            ]],\r\n            onBeforeCheck:" +
" function (rowIndex, rowData) {\r\n                if (rowData[\"StampStatusID\"] ==" +
" \'作废\') {\r\n                    alert(\"该印章已作废!\");\r\n                    $(\".datagri" +
"d-row[datagrid-row-index=\" + rowIndex + \"] input[type=\'checkbox\']\").attr(\"disabl" +
"ed\") = \"disabled\";\r\n                    return false;\r\n                }\r\n      " +
"          if (rowData[\"LoanRecord\"] != \'\') {\r\n                    alert(\"该印章正在使用" +
"中!\");\r\n                    $(\".datagrid-row[datagrid-row-index=\" + rowIndex + \"]" +
" input[type=\'checkbox\']\").attr(\"disabled\") = \"disabled\";\r\n                    re" +
"turn false;\r\n                }\r\n            },\r\n\r\n        });\r\n        $(\"#JQSea" +
"rch\").textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa-s" +
"earch\',\r\n            height: 25,\r\n            prompt: \'工程编号、工程名称\',\r\n            " +
"onClickButton: function () {\r\n                JQ.datagrid.searchGrid(\r\n         " +
"   {\r\n                dgID: \'BussProjectGrid\',\r\n                loadingType: \'da" +
"tagrid\',\r\n                queryParams: null\r\n            });\r\n            }\r\n   " +
"     });\r\n    });\r\n\r\n\r\n    function selectProj() {\r\n        var row = $(\'#BussPr" +
"ojectGrid\').datagrid(\'getSelections\');\r\n        if (row.length < 1) {\r\n         " +
"   window.top.JQ.dialog.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n        }" +
"\r\n        var ids = [];\r\n        var FlowModelID = [];\r\n        var _rows = [];\r" +
"\n        for (var i = 0; i < 1; i++) {\r\n            ids.push(row[0][\"Id\"]);\r\n   " +
"         ids.push(row[0][\'StampTypeID\']);\r\n            ids.push(row[0][\'StampNam" +
"e\']);\r\n            FlowModelID.push(row[0][\"FlowModelID\"]);\r\n            _rows.p" +
"ush(row[0]);\r\n        }\r\n        _ProjCallBackSingLe(ids);\r\n        _ProjCallBac" +
"kFlowModelID(FlowModelID);\r\n        JQ.dialog.dialogClose();\r\n    }\r\n\r\n\r\n</scrip" +
"t>\r\n\r\n<table");

WriteLiteral(" id=\"BussProjectGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"BussProjectPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProj()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    ");

WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
