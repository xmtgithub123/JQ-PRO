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
    
    #line 2 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjectDeliver/infoForPublish.cshtml")]
    public partial class _Views_IsoFormProjectDeliver_infoForPublish_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.DesTask>
    {
        public _Views_IsoFormProjectDeliver_infoForPublish_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'IsoFormProjectDeliverForm',//formid提交需要用到
        buttonTypes: ['close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
</script>
");

            
            #line 13 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
 using (Html.BeginForm("save", "IsoFormProjectDeliver", FormMethod.Post, new { @area = "ISO", @id = "IsoFormProjectDeliverForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"30%\"");

WriteLiteral(">交付时间</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 20 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
           Write(ViewBag.DateForPublish);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 24 "..\..\Views\IsoFormProjectDeliver\infoForPublish.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral(" \r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
