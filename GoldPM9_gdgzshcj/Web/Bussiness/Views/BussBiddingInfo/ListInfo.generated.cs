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
    
    #line 1 "..\..\Views\BussBiddingInfo\ListInfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingInfo/ListInfo.cshtml")]
    public partial class _Views_BussBiddingInfo_ListInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussBiddingInfo_ListInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 25), Tuple.Create("\"", 91)
            
            #line 3 "..\..\Views\BussBiddingInfo\ListInfo.cshtml"
, Tuple.Create(Tuple.Create("", 31), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/extension/datagrid-detailview.js")
            
            #line default
            #line hidden
, 31), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 6 "..\..\Views\BussBiddingInfo\ListInfo.cshtml"
                 Write(Url.Action("GetListInfo", "BussBiddingInfo", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=");

            
            #line 6 "..\..\Views\BussBiddingInfo\ListInfo.cshtml"
                                                                                                       Write(Request.Params["CompanyType"]);

            
            #line default
            #line hidden
WriteLiteral("\';//请求的地址\r\n    $(function () {\r\n        debugger;\r\n        JQ.datagrid.datagrid({" +
"\r\n            JQButtonTypes: [],//需要显示的按钮\r\n            JQPrimaryID: \'Id\',//主键的字段" +
"\r\n            JQID: \'BiddingInfoGrid\',//数据表格ID\r\n            JQDialogTitle: \'信息\'," +
"//弹出窗体标题\r\n            JQWidth: \'1000\',//弹出窗体宽\r\n            JQHeight: \'100%\',//弹出" +
"窗体高\r\n            JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n       " +
"     url: requestUrl,\r\n            checkOnSelect: false,\r\n            singleSele" +
"ct:true,\r\n            toolbar: \'#BiddingInfoPanel\',//工具栏的容器ID\r\n            colum" +
"ns: [[\r\n                 { title: \'投标编号\', field: \'BiddingNumber\', width: \'20%\', " +
"align: \'left\', sortable: true },\r\n                 { title: \'项目名称\', field: \'Engi" +
"neeringName\', width: \'40%\', align: \'left\', sortable: true },\r\n                 {" +
" title: \'商务联系人\', field: \'DelegatorName\', width: \'10%\', align: \'left\', sortable: " +
"true },\r\n                 { title: \'技术联系人\', field: \'BiddingManageName\', width: \'" +
"10%\', align: \'left\', sortable: true },\r\n                 { title: \'开标日期\', field:" +
" \'BiddingOpeningTime\', width: \'10%\', align: \'center\', sortable: true, formatter:" +
" JQ.tools.DateTimeFormatterByT }\r\n            ]]\r\n        });\r\n\r\n        $(\"#JQS" +
"earch\").textbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa" +
"-search\',\r\n            height: 25,\r\n            prompt: \'投标编号、项目名称\',\r\n          " +
"  onClickButton: function () {\r\n                JQ.datagrid.searchGrid(\r\n       " +
"     {\r\n                dgID: \'BiddingInfoGrid\',\r\n                loadingType: \'" +
"datagrid\',\r\n                queryParams: null\r\n            });\r\n            }\r\n " +
"       });\r\n    });\r\n\r\n    function selectProj() {\r\n        debugger;\r\n        v" +
"ar row = $(\"#BiddingInfoGrid\").datagrid(\"getSelected\");\r\n        if (row == \"\") " +
"{\r\n            JQ.dialog.warning(\"请选择投标信息!!!\");\r\n        }\r\n        else {\r\n    " +
"        _BussBiddingPackageBack(row);\r\n            JQ.dialog.dialogClose();\r\n   " +
"     }\r\n    }\r\n    function closedialog() {\r\n        JQ.dialog.dialogClose();\r\n " +
"   }\r\n\r\n</script>\r\n\r\n\r\n\r\n\r\n<table");

WriteLiteral(" id=\"BiddingInfoGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"BiddingInfoPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProj()\"");

WriteLiteral(">确定选择</a>\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"closedialog()\"");

WriteLiteral(">关闭</a>\r\n    </span>\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n    开标时间\r\n    <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BiddingOpeningTimeS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n    至\r\n    <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BiddingOpeningTimeE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'txtLike\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n</div>\r\n\r\n<input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"choiceIds\"");

WriteLiteral(" name=\"choiceIds\"");

WriteLiteral(" />");

        }
    }
}
#pragma warning restore 1591
