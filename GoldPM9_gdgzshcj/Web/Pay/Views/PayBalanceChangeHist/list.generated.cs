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
    
    #line 2 "..\..\Views\PayBalanceChangeHist\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceChangeHist/list.cshtml")]
    public partial class _Views_PayBalanceChangeHist_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceChangeHist_list_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 5 "..\..\Views\PayBalanceChangeHist\list.cshtml"
                     Write(Url.Action("json", "PayBalanceChangeHist",new { @area= "Pay" }));

            
            #line default
            #line hidden
WriteLiteral("?payBalanceAcountId=");

            
            #line 5 "..\..\Views\PayBalanceChangeHist\list.cshtml"
                                                                                                         Write(Request.QueryString["payBalanceAcountId"]);

            
            #line default
            #line hidden
WriteLiteral(@"';
            JQ.datagrid.datagrid({
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'changeListGrid',//数据表格ID
                JQDialogTitle: '信息',//弹出窗体标题
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                url: requestUrl,//请求的地址
                singleSelect: false,//是否包含check
                checkOnSelect: true,//是否包含check
                toolbar: '#PayModelPanel',//工具栏的容器ID
                columns: [[
                  { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
		{ title: '修改前金额', field: 'BalanceOldMoney', width: ""18%"", align: 'right', sortable: true },
		{ title: '修改后金额', field: 'BalanceNewMoney', width: ""18%"", align: 'right', sortable: true },
		{ title: '修改时间', field: 'date', width: ""18%"", align: 'center', sortable: true },
		{ title: '修改原因', field: 'BalanceChangeNote', width: ""40%"", align: 'center', sortable: true },
        ]]
        });



    </script>



    <table");

WriteLiteral(" id=\"changeListGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"PayModelPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n    </div>\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
