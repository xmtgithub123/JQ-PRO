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
    
    #line 2 "..\..\Views\OaNew\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaNew/info.cshtml")]
    public partial class _Views_OaNew_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaNew>
    {
        public _Views_OaNew_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'OaNewForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeFormSubmit: function () {
            document.getElementById(""NewsContent"").setAttribute(""value"", document.getElementById(""contentframe"").contentWindow.getContent());
        }
    });
</script>
<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 650), Tuple.Create("\"", 701)
            
            #line 16 "..\..\Views\OaNew\info.cshtml"
, Tuple.Create(Tuple.Create("", 656), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/ajaxfileupload.js")
            
            #line default
            #line hidden
, 656), false)
);

WriteLiteral("></script>\r\n");

            
            #line 17 "..\..\Views\OaNew\info.cshtml"
 using (Html.BeginForm("save", "OaNew", FormMethod.Post, new { @area = "Oa", @id = "OaNewForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\OaNew\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\OaNew\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(">类别</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 25 "..\..\Views\OaNew\info.cshtml"
           Write(BaseData.getBaseSystem(new Params()
           {
               controlName = "NewsTypeID",
               isRequired = true,
               engName = "NewsType",
               width = "98%",
               ids = Model.NewsTypeID.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" text-align=\"center\"");

WriteLiteral(">标题</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"NewsTitle\"");

WriteLiteral(" style=\"width:762px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1545), Tuple.Create("\"", 1569)
            
            #line 39 "..\..\Views\OaNew\info.cshtml"
                                                                  , Tuple.Create(Tuple.Create("", 1553), Tuple.Create<System.Object, System.Int32>(Model.NewsTitle
            
            #line default
            #line hidden
, 1553), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" text-align=\"center\"");

WriteLiteral(">图片</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" id=\"divImg\"");

WriteLiteral(">\r\n                <div>\r\n                    <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-image\'\"");

WriteLiteral(" onclick=\"chooseIamge()\"");

WriteLiteral(">选择图片</a>\r\n                    <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" id=\"btnClearImage\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-minus\'\"");

WriteLiteral(" onclick=\"clearImage()\"");

WriteLiteral(">清除图片</a>\r\n                    <span");

WriteLiteral(" style=\"color:red;margin-left:20px;\"");

WriteLiteral(">*建议大小（宽650px 高450px）,否则将不清晰</span>\r\n                </div>\r\n                <img" +
"");

WriteLiteral(" id=\"newsimg\"");

WriteLiteral(" name=\"newsimg\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"NewsImage\"");

WriteLiteral(" name=\"NewsImage\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"imgdescription\"");

WriteLiteral(">\r\n            <th>图片描述</th>\r\n            <td>\r\n                <input");

WriteLiteral(" id=\"NewsImageDescription\"");

WriteLiteral(" name=\"NewsImageDescription\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2511), Tuple.Create("\"", 2541)
            
            #line 57 "..\..\Views\OaNew\info.cshtml"
                       , Tuple.Create(Tuple.Create("", 2519), Tuple.Create<System.Object, System.Int32>(Model.NewsDescription
            
            #line default
            #line hidden
, 2519), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(">内容</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(">\r\n                <iframe");

WriteLiteral(" id=\"contentframe\"");

WriteLiteral(" frameborder=\"0\"");

WriteLiteral(" style=\"width:100%;height:300px\"");

WriteLiteral("></iframe>\r\n                <input");

WriteLiteral(" id=\"NewsContent\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"NewsContent\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(">\r\n                上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(">\r\n");

            
            #line 72 "..\..\Views\OaNew\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 72 "..\..\Views\OaNew\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaNew";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 77 "..\..\Views\OaNew\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 77 "..\..\Views\OaNew\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"divNewsContent\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">");

            
            #line 82 "..\..\Views\OaNew\info.cshtml"
                                              Write(Html.Raw(Model.NewsContent));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 83 "..\..\Views\OaNew\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(""#NewsImageDescription"").textbox();
    document.getElementById(""newsimg"").onload = function () {
        showImage();
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
    }
");

            
            #line 109 "..\..\Views\OaNew\info.cshtml"
    
            
            #line default
            #line hidden
            
            #line 109 "..\..\Views\OaNew\info.cshtml"
      
        if (string.IsNullOrEmpty(Model.NewsImage))
        {

            
            #line default
            #line hidden
WriteLiteral("            ");

WriteLiteral("\r\n    clearImage();\r\n    ");

WriteLiteral("\r\n");

            
            #line 115 "..\..\Views\OaNew\info.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            ");

WriteLiteral("\r\n    document.getElementById(\"newsimg\").setAttribute(\"src\", \"");

            
            #line 119 "..\..\Views\OaNew\info.cshtml"
                                                        Write(Url.Action("ShowImage","OaNew",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral("?path=\" + \"");

            
            #line 119 "..\..\Views\OaNew\info.cshtml"
                                                                                                                        Write(HttpUtility.UrlEncode(Model.NewsImage));

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    ");

WriteLiteral("\r\n");

            
            #line 121 "..\..\Views\OaNew\info.cshtml"
        }
    
            
            #line default
            #line hidden
WriteLiteral(@"

    function appendFile() {
        if (document.getElementById(""imguploader"")) {
            document.getElementById(""imguploader"").parentNode.removeChild(document.getElementById(""imguploader""));
        }
        var _file = document.createElement(""input"");
        _file.type = ""file"";
        _file.setAttribute(""id"", ""imguploader"");
        _file.setAttribute(""name"", ""imguploader"");
        _file.style.display = ""none"";
        document.getElementById(""divImg"").appendChild(_file);
        _file.onchange = function () {
            $.ajaxFileUpload({
                url: """);

            
            #line 136 "..\..\Views\OaNew\info.cshtml"
                  Write(Url.Action("UploadImage", "OaNew", new { @area="OA"}));

            
            #line default
            #line hidden
WriteLiteral(@""",
                secureuri: false,
                fileElementId: ""imguploader"",
                dataType: ""json"",
                success: function (data, status) {
                    if (data.Result == false) {
                        JQ.dialog.error(data.Message);
                        return;
                    }
                    //插入图片
                    var _newsimg = document.getElementById(""newsimg"");
                    _newsimg.style.width = ""auto"";
                    _newsimg.style.height = ""auto"";
                    document.getElementById(""NewsImage"").setAttribute(""value"", data.FileName);
                    _newsimg.setAttribute(""src"", """);

            
            #line 150 "..\..\Views\OaNew\info.cshtml"
                                              Write(Url.Action("Download","ProcessFile",new {  area="Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?id=0&name="" + data.FileName + ""&realName="" + data.FileName + ""&timestamp="" + new Date().valueOf());
                }
            });
        }
    }


    function showImage() {
        document.getElementById(""newsimg"").style.display = """";
        document.getElementById(""imgdescription"").style.display = """";
        document.getElementById(""btnClearImage"").style.visibility = ""visible"";
    }

    function clearImage() {
        document.getElementById(""newsimg"").style.display = ""none"";
        document.getElementById(""btnClearImage"").style.visibility = ""hidden"";
        document.getElementById(""imgdescription"").style.display = ""none"";
        document.getElementById(""NewsImage"").setAttribute(""value"", ""clear"");
    }

    document.getElementById(""contentframe"").onload = function () {
        var _newsContent = document.getElementById(""divNewsContent"");
        document.getElementById(""contentframe"").contentWindow.loadEditor(_newsContent.innerHTML);
        _newsContent.parentNode.removeChild(_newsContent);
    }

    function chooseIamge() {
        appendFile();
        document.getElementById(""imguploader"").click();
    }
    document.getElementById(""contentframe"").setAttribute(""src"", """);

            
            #line 180 "..\..\Views\OaNew\info.cshtml"
                                                             Write(Url.Action("Editor","OaNew",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral("\");\r\n</script>");

        }
    }
}
#pragma warning restore 1591
