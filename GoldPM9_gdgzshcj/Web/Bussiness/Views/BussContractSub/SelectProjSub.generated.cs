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
    
    #line 1 "..\..\Views\BussContractSub\SelectProjSub.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractSub/SelectProjSub.cshtml")]
    public partial class _Views_BussContractSub_SelectProjSub_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussContractSub_SelectProjSub_cshtml()
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
        JQ.datagird.datagird({
            JQButtonTypes: [],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'SelectProjSubGrid',//数据表格ID
            JQDialogTitle: '选择外包工程',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: false,
            //JQCustomSearch: [  //自定义搜索的字段
            //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
            //],
            //JQExcludeCol: [1, 4, 10],//导出execl排除的列
            //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
            //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 26 "..\..\Views\BussContractSub\SelectProjSub.cshtml"
             Write(Url.Action("JsonSelectProjSub", "BussContractSub", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=");

            
            #line 26 "..\..\Views\BussContractSub\SelectProjSub.cshtml"
                                                                                                          Write(Request.Params["CompanyType"].ToString());

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#Se" +
"lectProjSubPanel\',//工具栏的容器ID\r\n            columns: [[\r\n                { field: " +
"\'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                { t" +
"itle: \'外包项目编号\', field: \'SubNumber\', width: 150, align: \'center\', sortable: true " +
"},\r\n                { title: \'外包项目名称\', field: \'SubName\', width: 150, align: \'cen" +
"ter\', sortable: true },\r\n                { title: \'外包金额\', field: \'SubFee\', width" +
": 150, align: \'center\', sortable: true },\r\n                { title: \'计划开始时间\', fi" +
"eld: \'SubPlanFinishDate\', width: 150, align: \'center\', sortable: true, formatter" +
": JQ.tools.DateTimeFormatterByT },\r\n                { title: \'计划完成时间\', field: \'S" +
"ubFactFinishDate\', width: 150, align: \'center\', sortable: true, formatter: JQ.to" +
"ols.DateTimeFormatterByT },\r\n            ]],\r\n\r\n        });\r\n        $(\"#JQSearc" +
"h\").textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa-sea" +
"rch\',\r\n            height: 25,\r\n            prompt: \'外包项目编号、外包项目名称\',\r\n          " +
"  onClickButton: function () {\r\n                JQ.datagird.searchGrid(\r\n       " +
"     {\r\n                dgID: \'SelectProjSubGrid\',\r\n                loadingType:" +
" \'datagrid\',\r\n                queryParams: null\r\n            });\r\n            }\r" +
"\n        });\r\n    });\r\n\r\n    function selectProjSub() {\r\n        var row = $(\'#S" +
"electProjSubGrid\').datagrid(\'getSelections\');\r\n        if (row.length < 1) {\r\n  " +
"          window.top.JQ.dialog.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n  " +
"      }\r\n        var ids = [];\r\n        var _rows = [];\r\n        for (var i = 0;" +
" i < row.length; i++) {\r\n            ids.push(row[i][\"Id\"]);\r\n            _rows." +
"push(row[i]);\r\n        }\r\n\r\n        _ProjSubCallBack(ids);\r\n        JQ.dialog.di" +
"alogClose();\r\n    }\r\n\r\n</script>\r\n\r\n<table");

WriteLiteral(" id=\"SelectProjSubGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"SelectProjSubPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProjSub()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'SubNumber,SubName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n    ");

WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
