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
    
    #line 2 "..\..\Views\OaNew\Query.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaNew/Query.cshtml")]
    public partial class _Views_OaNew_Query_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaNew>
    {
        public _Views_OaNew_Query_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'OaNewForm',//formid提交需要用到
        buttonTypes: [],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
    ");

WriteLiteral("\r\n    function closeDio() {\r\n        JQ.datagrid.autoRefdatagrid();\r\n        JQ.d" +
"ialog.dialogClose();\r\n    }\r\n</script>\r\n");

            
            #line 23 "..\..\Views\OaNew\Query.cshtml"
 using (Html.BeginForm("save", "OaNew", FormMethod.Post, new { @area = "Oa", @id = "OaNewForm", @style = "background-color:#FFFFFF" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" style=\"padding:0px 10px 0px 10px;font-family:\'Microsoft YaHei\';cursor:default\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:28px;font-weight:bolder;float:left" +
";\"");

WriteLiteral(">");

            
            #line 26 "..\..\Views\OaNew\Query.cshtml"
                                                                                            Write(Model.NewsTitle);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        <div");

WriteLiteral(" style=\"width:100%;font-size:14px;text-align:center;color:#888888;float:left;bord" +
"er-bottom:1px solid #AAAAAA;padding:0px 0px 5px 0px\"");

WriteLiteral(">");

            
            #line 27 "..\..\Views\OaNew\Query.cshtml"
                                                                                                                                              Write(Model.CreatorEmpName + " " + Model.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") + " " + ViewBag.NewsTypeText);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n\r\n            <div");

WriteLiteral(" id=\"imagecontainer\"");

WriteLiteral(" style=\"float:left;width:100%;text-align:center;font-size:12px;margin:10px 0px 5p" +
"x 0px\"");

WriteLiteral(">\r\n                <div>\r\n                    <img");

WriteLiteral(" id=\"newsimg\"");

WriteLiteral(" />\r\n                </div>\r\n                <div>");

            
            #line 34 "..\..\Views\OaNew\Query.cshtml"
                 Write(Model.NewsDescription);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n        <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n            <tr>\r\n                <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">内容</th>\r\n                <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" style=\"margin-top:5px;float:left;font-size:16px\"");

WriteLiteral(">");

            
            #line 41 "..\..\Views\OaNew\Query.cshtml"
                                                                      Write(Html.Raw(Model.NewsContent));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n             " +
"   <th>附件列表</th>\r\n                <td>\r\n\r\n                    <div");

WriteLiteral(" style=\"margin-top:5px;float:left;width:100%\"");

WriteLiteral(">\r\n");

            
            #line 49 "..\..\Views\OaNew\Query.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 49 "..\..\Views\OaNew\Query.cshtml"
                          
    var uploader = new DataModel.FileUploader();
    uploader.RefID = Model.Id;
    uploader.RefTable = "OaNew";
    uploader.Name = "UploadFile1";
    uploader.AllowDelete = false;
    uploader.AllowUpload = false;
                
            
            #line default
            #line hidden
            
            #line 56 "..\..\Views\OaNew\Query.cshtml"
            Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 56 "..\..\Views\OaNew\Query.cshtml"
                                                                                               
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n                </td>\r\n            </tr>\r\n\r\n       " +
" </table>\r\n\r\n    </div>\r\n");

WriteLiteral("    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"divPanel\"");

WriteLiteral(" JQPanel=\"dialogButtonPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-close\'\"");

WriteLiteral(" onclick=\"closeDio()\"");

WriteLiteral(">关闭</a>\r\n        </span>\r\n    </div>\r\n");

            
            #line 71 "..\..\Views\OaNew\Query.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n");

            
            #line 73 "..\..\Views\OaNew\Query.cshtml"
    
            
            #line default
            #line hidden
            
            #line 73 "..\..\Views\OaNew\Query.cshtml"
      
        if (string.IsNullOrEmpty(Model.NewsImage))
        {

            
            #line default
            #line hidden
WriteLiteral("            ");

WriteLiteral("\r\n    document.getElementById(\"imagecontainer\").parentNode.removeChild(document.g" +
"etElementById(\"imagecontainer\"));\r\n    ");

WriteLiteral("\r\n");

            
            #line 79 "..\..\Views\OaNew\Query.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            ");

WriteLiteral(@"
    document.getElementById(""newsimg"").onload = function () {
        var maxHeight = 450, maxWidth = 650;
        imgHeight = this.clientHeight;
        imgWidth = this.clientWidth;
        var scale = maxHeight / maxWidth;
        if (imgHeight > maxHeight || imgWidth > maxWidth) {
            if (imgWidth * scale > imgHeight) {
                //以宽度为准
                imgHeight = imgHeight * (maxWidth / imgWidth);
                this.style.height = imgHeight + ""px"";
                this.style.width = maxWidth + ""px"";
                imgWidth = maxWidth;
            }
            else {
                //以高度为准
                imgWidth = imgWidth * (maxHeight / imgHeight);
                this.style.height = maxHeight + ""px"";
                this.style.width = imgWidth + ""px"";
                imgHeight = maxHeight;
            }
        }
        this.style.margin = ""0px auto 0px auto"";
    }
    document.getElementById(""newsimg"").setAttribute(""src"", """);

            
            #line 106 "..\..\Views\OaNew\Query.cshtml"
                                                        Write(Url.Action("ShowImage","OaNew",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral("?path=\" + \"");

            
            #line 106 "..\..\Views\OaNew\Query.cshtml"
                                                                                                                        Write(HttpUtility.UrlEncode(Model.NewsImage));

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    ");

WriteLiteral("\r\n");

            
            #line 108 "..\..\Views\OaNew\Query.cshtml"
        }
    
            
            #line default
            #line hidden
WriteLiteral("\r\n</script>");

        }
    }
}
#pragma warning restore 1591
