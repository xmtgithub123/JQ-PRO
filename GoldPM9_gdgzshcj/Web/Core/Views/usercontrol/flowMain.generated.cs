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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/usercontrol/flowMain.cshtml")]
    public partial class _Views_usercontrol_flowMain_cshtml : System.Web.Mvc.WebViewPage<DataModel.FlowControlPar>
    {
        public _Views_usercontrol_flowMain_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<table");

WriteLiteral(" style=\"width:100%;\"");

WriteLiteral(" cellpadding=\"5\"");

WriteLiteral(" id=\"flowpanelt\"");

WriteLiteral(">\r\n    <tr>\r\n        <td>\r\n            <div>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-hand-o-right\',iconAlign:\'right\'\"");

WriteLiteral(" id=\"flowNextByA\"");

WriteLiteral(">提交下一步</a>\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" name=\"state\"");

WriteLiteral(" style=\"width:130px;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"AL\"");

WriteLiteral(">吴俊鹏</option>\r\n                    <option");

WriteLiteral(" value=\"AK\"");

WriteLiteral(">吴三</option>\r\n                </select>\r\n            </div>\r\n            <div>\r\n " +
"               <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-hand-o-right\',iconAlign:\'right\'\"");

WriteLiteral(">提交跳一步</a>\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" name=\"state\"");

WriteLiteral(" style=\"width:130px;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"AL\"");

WriteLiteral(">吴俊鹏</option>\r\n                    <option");

WriteLiteral(" value=\"AK\"");

WriteLiteral(">吴三</option>\r\n                </select>\r\n            </div>\r\n            <div>\r\n " +
"               <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-hand-o-right\',iconAlign:\'right\'\"");

WriteLiteral(">工作移交给</a>\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" name=\"state\"");

WriteLiteral(" style=\"width:130px;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"AL\"");

WriteLiteral(">吴俊鹏</option>\r\n                    <option");

WriteLiteral(" value=\"AK\"");

WriteLiteral(">吴三</option>\r\n                </select>\r\n            </div>\r\n            <div>\r\n " +
"               <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-hand-o-right\',iconAlign:\'right\'\"");

WriteLiteral(">回退上一步</a>\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" name=\"state\"");

WriteLiteral(" style=\"width:130px;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"AL\"");

WriteLiteral(">吴俊鹏</option>\r\n                    <option");

WriteLiteral(" value=\"AK\"");

WriteLiteral(">吴三</option>\r\n                </select>\r\n            </div>\r\n        </td>\r\n     " +
"   <td");

WriteLiteral(" style=\"width:3px;\"");

WriteLiteral("></td>\r\n        <td>\r\n            <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true,prompt:\'请填写处理意见\'\"");

WriteLiteral(" style=\"width:100%;height:100px\"");

WriteLiteral(">\r\n        </td>\r\n    </tr>\r\n</table>\r\n<script>\r\n    $(function () {\r\n        var" +
" formid = \'");

            
            #line 42 "..\..\Views\usercontrol\flowMain.cshtml"
                 Write(Model.formid);

            
            #line default
            #line hidden
WriteLiteral(@"';
        var $form;
        $.parser.parse($(""#flowpanelt""));
        $(""#flowNextByA"").click(function () {
            if (JQ.tools.isNotEmpty(formid)) {
                $form = $(""#"" + formid);
            }
            else {
                $form = JQ.tools.findDialogInfo().$div.find(""form:last"");
            }
            JQ.form.submit({
                $form: $form,
                succesCollBack: function (backJson) {
                    var refID = backJson.stateValue;
                    if (refID == null || isNaN(refID) || parseInt(refID, 10) <= 0) {
                        refID = '");

            
            #line 57 "..\..\Views\usercontrol\flowMain.cshtml"
                            Write(Model.flowRefID);

            
            #line default
            #line hidden
WriteLiteral(@"';
                    }
                    if (parseInt(refID, 10) <= 0) {
                        JQ.dialog.error(""获取FlowRefID失败"");
                    }
                    else {
                        JQ.tools.ajax({
                            url: '");

            
            #line 64 "..\..\Views\usercontrol\flowMain.cshtml"
                             Write(Url.Action("flowsubnext"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                            data: {

                            },
                            succesCollBack: function (backdata) {

                            }
                        });
                    }
                }
            });
        });
    });
</script>
");

        }
    }
}
#pragma warning restore 1591
