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
    
    #line 2 "..\..\Views\ArchShareFolderML\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchShareFolderML/info.cshtml")]
    public partial class _Views_ArchShareFolderML_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ArchShareFolderML>
    {
        public _Views_ArchShareFolderML_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'ArchShareFolderMLForm\',//formid提交需要" +
"用到\r\n        buttonTypes: [\'submit\', \'close\'],//默认按钮\r\n        succesCollBack: fun" +
"ction (data) {//成功回调函数,data为服务器返回值\r\n            debugger;\r\n            var divId" +
" = \'");

            
            #line 9 "..\..\Views\ArchShareFolderML\info.cshtml"
                    Write(Request.Params["divid"]);

            
            #line default
            #line hidden
WriteLiteral(@"';
            if (divId != null) {
                if (typeof (window.top[""_DialogCallback_""+divId]) == ""function"") {
                    window.top[""_DialogCallback_""+divId]();
                }
            }
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
</script>
");

            
            #line 19 "..\..\Views\ArchShareFolderML\info.cshtml"
 using (Html.BeginForm("save", "ArchShareFolderML", FormMethod.Post, new { @area = "Archive", @id = "ArchShareFolderMLForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\ArchShareFolderML\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\ArchShareFolderML\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                文件编号\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"FlderId\"");

WriteLiteral(" name=\"FlderId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1004), Tuple.Create("\"", 1050)
            
            #line 26 "..\..\Views\ArchShareFolderML\info.cshtml"
, Tuple.Create(Tuple.Create("", 1012), Tuple.Create<System.Object, System.Int32>(Request.Params["FolderId"].ToString()
            
            #line default
            #line hidden
, 1012), false)
);

WriteLiteral("\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FileNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1238), Tuple.Create("\"", 1263)
            
            #line 29 "..\..\Views\ArchShareFolderML\info.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 1246), Tuple.Create<System.Object, System.Int32>(Model.FileNumber
            
            #line default
            #line hidden
, 1246), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">责任者</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Manager\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1460), Tuple.Create("\"", 1482)
            
            #line 33 "..\..\Views\ArchShareFolderML\info.cshtml"
                                , Tuple.Create(Tuple.Create("", 1468), Tuple.Create<System.Object, System.Int32>(Model.Manager
            
            #line default
            #line hidden
, 1468), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">文件材料题名</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FileName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1714), Tuple.Create("\"", 1737)
            
            #line 40 "..\..\Views\ArchShareFolderML\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 1722), Tuple.Create<System.Object, System.Int32>(Model.FileName
            
            #line default
            #line hidden
, 1722), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Time\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1930), Tuple.Create("\"", 1949)
            
            #line 44 "..\..\Views\ArchShareFolderML\info.cshtml"
                             , Tuple.Create(Tuple.Create("", 1938), Tuple.Create<System.Object, System.Int32>(Model.Time
            
            #line default
            #line hidden
, 1938), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">页数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"PageNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2181), Tuple.Create("\"", 2206)
            
            #line 51 "..\..\Views\ArchShareFolderML\info.cshtml"
                                     , Tuple.Create(Tuple.Create("", 2189), Tuple.Create<System.Object, System.Int32>(Model.PageNumber
            
            #line default
            #line hidden
, 2189), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th>序号</th>\r\n            <td><input");

WriteLiteral(" name=\"ByOrder\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2362), Tuple.Create("\"", 2384)
            
            #line 54 "..\..\Views\ArchShareFolderML\info.cshtml"
                                  , Tuple.Create(Tuple.Create("", 2370), Tuple.Create<System.Object, System.Int32>(Model.ByOrder
            
            #line default
            #line hidden
, 2370), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Remark\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2595), Tuple.Create("\"", 2616)
            
            #line 59 "..\..\Views\ArchShareFolderML\info.cshtml"
                                , Tuple.Create(Tuple.Create("", 2603), Tuple.Create<System.Object, System.Int32>(Model.Remark
            
            #line default
            #line hidden
, 2603), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 67 "..\..\Views\ArchShareFolderML\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 67 "..\..\Views\ArchShareFolderML\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "ArchShareFolderML";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 72 "..\..\Views\ArchShareFolderML\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 72 "..\..\Views\ArchShareFolderML\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 77 "..\..\Views\ArchShareFolderML\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591