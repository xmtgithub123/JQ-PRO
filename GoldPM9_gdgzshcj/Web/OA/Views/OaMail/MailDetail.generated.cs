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
    
    #line 2 "..\..\Views\OaMail\MailDetail.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaMail/MailDetail.cshtml")]
    public partial class _Views_OaMail_MailDetail_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaMail>
    {
        public _Views_OaMail_MailDetail_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'OaMailForm\',//formid提交需要用到\r\n       " +
" buttonTypes: [\'close\'],//默认按钮\r\n    });\r\n\r\n    function delMailDetail() {\r\n\r\n   " +
"     var Url = \"\";\r\n        if(");

            
            #line 12 "..\..\Views\OaMail\MailDetail.cshtml"
      Write(ViewBag.ReceiveFlag);

            
            #line default
            #line hidden
WriteLiteral(" == 0)\r\n        {\r\n            Url = \'");

            
            #line 14 "..\..\Views\OaMail\MailDetail.cshtml"
              Write(Url.Action("del", "OaMail", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        }\r\n        else\r\n        {\r\n            Url = \'");

            
            #line 18 "..\..\Views\OaMail\MailDetail.cshtml"
              Write(Url.Action("del", "OaMailRead", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        }\r\n        JQ.tools.ajax({\r\n            doBackResult: true,\r\n        " +
"    url: Url,\r\n            data: \"Id=\" + \"");

            
            #line 23 "..\..\Views\OaMail\MailDetail.cshtml"
                      Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        });\r\n\r\n        JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用" +
"\r\n        JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源\r\n    }\r\n</script>\r\n");

            
            #line 30 "..\..\Views\OaMail\MailDetail.cshtml"
 using (Html.BeginForm("save1", "OaMail", FormMethod.Post, new { @area = "Oa", @id = "OaMailForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\OaMail\MailDetail.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\OaMail\MailDetail.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" JQPanel=\'dialogButtonPanel\'");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-remove (alias)\'\"");

WriteLiteral(" onclick=\"delMailDetail()\"");

WriteLiteral(">删除</a>\r\n    </div>\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                发件人\r\n            </th>\r\n            <td>\r\n");

            
            #line 42 "..\..\Views\OaMail\MailDetail.cshtml"
                
            
            #line default
            #line hidden
            
            #line 42 "..\..\Views\OaMail\MailDetail.cshtml"
                  
                    if (Model.MailIsBBC==1)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <span>******</span>\r\n");

            
            #line 46 "..\..\Views\OaMail\MailDetail.cshtml"
                    }
                    else
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <span>\r\n");

WriteLiteral("                            ");

            
            #line 50 "..\..\Views\OaMail\MailDetail.cshtml"
                        Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </span>\r\n");

            
            #line 52 "..\..\Views\OaMail\MailDetail.cshtml"
                    }
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   收件人\r\n            </th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 61 "..\..\Views\OaMail\MailDetail.cshtml"
            Write(ViewBag.SendEmp);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   时间\r\n            </th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 69 "..\..\Views\OaMail\MailDetail.cshtml"
            Write(Model.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   内容\r\n            </th>\r\n            <td");

WriteLiteral(" style=\"font-family:\'Microsoft YaHei\';font-size:16px;cursor:default\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" style=\"min-height:100px\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 78 "..\..\Views\OaMail\MailDetail.cshtml"
                Write(Html.Raw(Model.MailNote));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n       " +
"     <th>附件</th>\r\n            <td>\r\n");

            
            #line 85 "..\..\Views\OaMail\MailDetail.cshtml"
                
            
            #line default
            #line hidden
            
            #line 85 "..\..\Views\OaMail\MailDetail.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaMail";
                    uploader.Name = "UploadFile1";
                    uploader.AllowUpload = false;
                    uploader.AllowDelete = false;
                    
            
            #line default
            #line hidden
            
            #line 92 "..\..\Views\OaMail\MailDetail.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 92 "..\..\Views\OaMail\MailDetail.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 97 "..\..\Views\OaMail\MailDetail.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
