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
    
    #line 2 "..\..\Views\OaMeetingUse\selectReview.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaMeetingUse/selectReview.cshtml")]
    public partial class _Views_OaMeetingUse_selectReview_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaMeetingUse_selectReview_cshtml()
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
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'OaMeetingRoomGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '550',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQFields: [""Id""],//追加的字段名
            JQSearch: {
                id: 'JQSearch',
                prompt: '会议名称',
                loadingType: 'datagrid',//默认值为datagrid可以不传
            },
            url: '");

            
            #line 24 "..\..\Views\OaMeetingUse\selectReview.cshtml"
             Write(Url.Action("SelectRoom", "OaMeetingRoom",new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            JQMergeCells: { fields: [\'Id\', \'RoomName\', \'PeoIdent\', \'St" +
"ateIdent\', \'ProcessId\'] },\r\n            checkOnSelect: true,//是否包含check\r\n       " +
"     fitColumns: false,\r\n            singleSelect: true,\r\n            nowrap: fa" +
"lse,\r\n            toolbar: \'#OaMeetingRoomPanel\',//工具栏的容器ID\r\n            columns" +
": [[\r\n              { field: \'Id\', align: \'center\', checkbox: true, JQIsExclude:" +
" true },\r\n    { title: \'会议室名称\', field: \'RoomName\', width: \'20%\', align: \'center\'" +
", sortable: true },\r\n    {\r\n        title: \'会议室容纳人数\', field: \'PeoIdent\', width: " +
"\'13%\', align: \'center\', sortable: true,\r\n        formatter:function(val,row)\r\n  " +
"      {\r\n            return row.RoomPeoLength;\r\n        }\r\n    },\r\n    {\r\n      " +
"  title: \'会议室状态\', field: \'StateIdent\', width: \'10%\', align: \'center\', sortable: " +
"true,\r\n        formatter: function (val, row) {\r\n            if (row.RoomStatus " +
"== \"1466\") {\r\n                return \'<span style=\"color:blue;\">\' + row.RoomStat" +
"usName + \'</span>\';\r\n            }\r\n            else {\r\n                if (row." +
"RoomStatus == \"1467\") {\r\n                    return \'<span style=\"color:red;\">\' " +
"+ row.RoomStatusName + \'</span>\'\r\n                }\r\n                else {\r\n   " +
"                 return row.RoomStatusName;\r\n                }\r\n            }\r\n " +
"       }\r\n    },\r\n        { title: \'开始时间\', field: \'StartDate\', width: \'14%\', ali" +
"gn: \'center\', sortable: true },\r\n        { title: \'结束时间\', field: \'EndDate\', widt" +
"h: \'14%\', align: \'center\', sortable: true },\r\n    { title: \'会议室用途\', field: \'Meet" +
"ingUseConent\', width: \'20%\', align: \'center\', sortable: true },\r\n        //{\r\n  " +
"      //    field: \"FlowProgress\", title: \"流程进度\", align: \"center\", halign: \"cent" +
"er\", width: \"5%\", formatter: JQ.Flow.showFlowProgress(\"OaMeetingRoomGrid\", \"OaMe" +
"etingUse\", \"MeetingId\")\r\n        //}\r\n\r\n            ]]\r\n        });\r\n        $(\"" +
"#JQSearch\").textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'f" +
"a fa-search\',\r\n            height: 25,\r\n            prompt: \'会议室名称\',\r\n          " +
"  onClickButton: function () {\r\n                JQ.datagrid.searchGrid(\r\n       " +
"     {\r\n                dgID: \'OaMeetingRoomGrid\',\r\n                loadingType:" +
" \'datagrid\',\r\n                queryParams: { text: $(\"#JQSearch\").textbox(\'getVa" +
"lue\') }\r\n            });\r\n            }\r\n        });\r\n    });\r\n\r\n    function se" +
"lectReivew() {\r\n        var rows = $(\'#OaMeetingRoomGrid\').datagrid(\'getSelectio" +
"ns\');\r\n        if (rows.length < 1) {\r\n            window.top.JQ.dialog.warning(" +
"\'您必须选择至少一项！！！\');\r\n            return;\r\n        }\r\n        if (rows[0].RoomStatus" +
" == \"1466\")\r\n        {\r\n            window.top.JQ.dialog.info(\'当前会议室在维护中！！！\');\r\n" +
"            return;\r\n        }\r\n        if (rows[0].RoomStatus == \"1467\") {\r\n   " +
"         window.top.JQ.dialog.info(\'当前会议室废弃！！！\');\r\n            return;\r\n        " +
"}\r\n        selectRoom(rows[0]);\r\n        JQ.dialog.dialogClose();\r\n    }\r\n\r\n</sc" +
"ript>\r\n\r\n<table");

WriteLiteral(" id=\"OaMeetingRoomGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"OaMeetingRoomPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectReivew()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'RoomName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
