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
    
    #line 1 "..\..\Views\OaEquipment\UsageList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaEquipment/UsageList.cshtml")]
    public partial class _Views_OaEquipment_UsageList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaEquipment_UsageList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.datagrid.datagrid({
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'UsageListGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '1000',//弹出窗体宽
            JQHeight: '800',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 13 "..\..\Views\OaEquipment\UsageList.cshtml"
             Write(Url.Action("GetUsageList", "OaEquipment", new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("?EquId=");

            
            #line 13 "..\..\Views\OaEquipment\UsageList.cshtml"
                                                                                 Write(Request.QueryString["EquId"]);

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
            checkOnSelect: true,//是否包含check
            toolbar: '#UsageListPanel',//工具栏的容器ID
            columns: [[
                { title: '使用人', field: 'UseEmpName', width: ""10%"", align: 'center', sortable: true },
                { title: '设备编号', field: 'EquNumber', width: ""10%"", align: 'center', sortable: true },
                { title: '设备名称', field: 'EquName', width: ""20%"", align: 'center', sortable: true },
                { title: '规格', field: 'EquModel', width: ""10%"", align: 'center', sortable: true },
                { title: '使用时间', field: 'UseTime', width: ""10%"", align: 'center', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },
                { title: '数量', field: 'Count', width: ""10%"", align: 'center', sortable: true }
            ]]
        });
    });
</script>

<table");

WriteLiteral(" id=\"UsageListGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"UsageListPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n    </span>\r\n</div>\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
