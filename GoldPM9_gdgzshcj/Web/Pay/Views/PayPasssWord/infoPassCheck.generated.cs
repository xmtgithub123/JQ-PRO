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
    
    #line 2 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayPasssWord/infoPassCheck.cshtml")]
    public partial class _Views_PayPasssWord_infoPassCheck_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseEmployee>
    {
        public _Views_PayPasssWord_infoPassCheck_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'ajaxform',//formid提交需要用到
        buttonTypes: [],//默认按钮 'submit', 'close'
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
           
        },
        buttons: [
             {
                 icon: ""fa-check"", id: ""check"", text: ""验证"", onClick: function () {
                     if ($(""#SalaryPassword"").val() == """") {
                         return JQ.dialog.info(""请输入绩效验证密码!"");
                     }
                     $.ajax({
                         url: """);

            
            #line 17 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
                           Write(Url.Action("savePassCheck", "PayPasssWord", new { @area = "Pay" }));

            
            #line default
            #line hidden
WriteLiteral(@""",
                         data: { SalaryPassword: $(""#SalaryPassword"").val() },
                         type: ""POST"",
                         success: function (result) {
                             if (result == ""0"") {
                                 return JQ.dialog.info(""绩效验证密码有误!"");
                             } else {
                                 JQ.dialog.dialogClose();
                                 JQ.dialog.info(""验证成功!"");
                                 //loadDataByTree(1861);
                                 _loadJxMenuData();
                             }
                         }
                     });
                 }
             }
        ]
    });
    function _loadJxMenuData() {
        $('#easyLayoutByIndex').layout('show', 'west');
        loadDataByTree(1861);
    }
    function loadDataByTree(menuid) {
        $.post(
        """);

            
            #line 41 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
    Write(Url.Action("menujson", "menu",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@""",
    {
        id: menuid,
        level: 2,
        isIcon: true
    },
    function (data) {
        if (data == null) {
            $(""#RightTree"").tree({
                data: []
            });
            JQ.dialog.show(""没有菜单数据!"");
            ");

WriteLiteral(@"
            return;
        } else {

            $(""#RightTree"").tree({
                data: data,
                onClick: function (node) {
                    if (node.state == 'closed') {
                        $(this).tree('expand', node.target);
                    } else if (node.state == 'open') {
                        $(this).tree('collapse', node.target);
                        if (node.children == undefined) {
                            addTab(node.text, ");

            
            #line 71 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
                                          Write(ViewBag.sitePath);

            
            #line default
            #line hidden
WriteLiteral(@"+node.attributes, node.iconCls);
                        }
                    }
                }
            });
        }
    }
             , 'json');
    }


    $(""#toolbar"").children('a').eq(0).children('span').children().eq(0).text(""验证"");
</script>

");

            
            #line 85 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
 using (Html.BeginForm("savePassCheck", "PayPasssWord", FormMethod.Post, new { @area = "Pay", @id = "ajaxform" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tbody>\r\n            <tr>\r\n                <th>\r\n                    绩" +
"效密码：\r\n                    ");

WriteLiteral("\r\n                </th>\r\n                <td>\r\n                    <input");

WriteLiteral(" id=\"SalaryPassword\"");

WriteLiteral(" name=\"SalaryPassword\"");

WriteLiteral(" type=\"password\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" autocomplete=\"off\"");

WriteLiteral(" />\r\n                </td>\r\n            </tr>\r\n\r\n        </tbody>\r\n    </table>\r\n" +
"");

            
            #line 101 "..\..\Views\PayPasssWord\infoPassCheck.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
