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
    
    #line 1 "..\..\Views\IsoFormProjectPublish\FilterList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjectPublish/FilterList.cshtml")]
    public partial class _Views_IsoFormProjectPublish_FilterList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjectPublish_FilterList_cshtml()
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
            JQDialogTitle: '选择项目',//弹出窗体标题
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

            
            #line 26 "..\..\Views\IsoFormProjectPublish\FilterList.cshtml"
             Write(Url.Action("JsonProject", "BussContract", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#Bu" +
"ssProjectPanel\',//工具栏的容器ID\r\n            columns: [[\r\n                { field: \'c" +
"k\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                { tit" +
"le: \'项目编号\', field: \'ProjNumber\', width: \"15%\", align: \'left\', sortable: true },\r" +
"\n                { title: \'项目名称\', field: \'ProjName\', width: \"25%\", align: \'left\'" +
", sortable: true },\r\n                { title: \'客户单位\', field: \'CustName\', width: " +
"\"25%\", align: \'center\', sortable: true },\r\n                { title: \'客户联系人\', fie" +
"ld: \'CustLinkMan\', width: \"20%\", align: \'center\', sortable: true },\r\n           " +
" ]],\r\n\r\n        });\r\n        $(\"#JQSearch\").textbox({\r\n            buttonText: \'" +
"筛选\',\r\n            buttonIcon: \'fa fa-search\',\r\n            height: 25,\r\n        " +
"    prompt: \'项目编号、项目名称\',\r\n            onClickButton: function () {\r\n            " +
"    JQ.datagrid.searchGrid(\r\n            {\r\n                dgID: \'BussProjectGr" +
"id\',\r\n                loadingType: \'datagrid\',\r\n                queryParams: nul" +
"l\r\n            });\r\n            }\r\n        });\r\n    });\r\n\r\n    //function select" +
"Proj() {\r\n    //    var row = $(\'#BussProjectGrid\').datagrid(\'getSelections\');\r\n" +
"    //    if (row.length < 1) {\r\n    //        window.top.JQ.dialog.warning(\'您必须" +
"选择至少一项！！！\');\r\n    //        return;\r\n    //    }\r\n    //    var ids = [];\r\n    /" +
"/    var _rows = [];\r\n    //    for (var i = 0; i < row.length; i++) {\r\n    //  " +
"      ids.push(row[i][\"Id\"]);\r\n    //        _rows.push(row[i]);\r\n    //    }\r\n\r" +
"\n    //    _ProjCallBack(ids);\r\n    //    JQ.dialog.dialogClose();\r\n    //}\r\n\r\n\r" +
"\n    function selectProj() {\r\n        var row = $(\'#BussProjectGrid\').datagrid(\'" +
"getSelections\');\r\n        if (row.length < 1) {\r\n            window.top.JQ.dialo" +
"g.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n        }\r\n        var ids = []" +
";\r\n        var _rows = [];\r\n        for (var i = 0; i < 1; i++) {\r\n            i" +
"ds.push(row[0][\"ProjName\"]);\r\n            _rows.push(row[0]);\r\n        }\r\n      " +
"  _ProjCallBackSingLe(ids);\r\n        JQ.dialog.dialogClose();\r\n    }\r\n\r\n\r\n</scri" +
"pt>\r\n\r\n<table");

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

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'ProjNumber,ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
