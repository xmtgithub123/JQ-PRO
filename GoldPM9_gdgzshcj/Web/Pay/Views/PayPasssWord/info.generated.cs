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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayPasssWord/info.cshtml")]
    public partial class _Views_PayPasssWord_info_cshtml : System.Web.Mvc.WebViewPage<JQ.Web.PassWordInfo>
    {
        public _Views_PayPasssWord_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'ajaxform',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.dialog.dialogClose();
        }
    });
    $(function () {
        $(""#EmpID"").val('");

            
            #line 12 "..\..\Views\PayPasssWord\info.cshtml"
                    Write(ViewBag.EmpID);

            
            #line default
            #line hidden
WriteLiteral(@"');
       
        $.extend($.fn.validatebox.defaults.rules, {
            /*必须和某个字段相等*/
            equalTo: {
                validator: function (value, param) {
                    return $(param[0]).val() == value;
                },
                message: '字段不匹配'
            }

        });
    });
</script>

");

            
            #line 27 "..\..\Views\PayPasssWord\info.cshtml"
 using (Html.BeginForm("save", "PayPasssWord", FormMethod.Post, new { @id = "ajaxform" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tbody>\r\n            <tr>\r\n                <th>\r\n                    旧" +
"密码：\r\n                    <input");

WriteLiteral(" id=\"EmpID\"");

WriteLiteral(" name=\"EmpID\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral("  />\r\n                </th>\r\n                <td><input");

WriteLiteral(" id=\"oldPassWord\"");

WriteLiteral(" name=\"oldPassWord\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" /></td>\r\n            </tr>\r\n            <tr>\r\n                <th>新密码：</th>\r\n   " +
"             <td><input");

WriteLiteral(" id=\"newPassWord\"");

WriteLiteral(" name=\"newPassWord\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"length[4,32]\"");

WriteLiteral(" invalidmessage=\"输入的密码长度必须是4到32个字符\"");

WriteLiteral(" /></td>\r\n            </tr>\r\n            <tr>\r\n                <th>确认密码：</th>\r\n  " +
"              <td><input");

WriteLiteral(" id=\"inputNewPassWord\"");

WriteLiteral(" name=\"inputNewPassWord\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"equalTo[\'#newPassWord\']\"");

WriteLiteral(" invalidmessage=\"两次输入密码不匹配\"");

WriteLiteral(" /></td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n");

            
            #line 48 "..\..\Views\PayPasssWord\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
