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
    
    #line 2 "..\..\Views\BaseFeedBack\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseFeedBack/info.cshtml")]
    public partial class _Views_BaseFeedBack_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseFeedBack>
    {
        public _Views_BaseFeedBack_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'BaseFeedBackForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });

</script>
");

            
            #line 14 "..\..\Views\BaseFeedBack\info.cshtml"
 using (Html.BeginForm("save", "BaseFeedBack", FormMethod.Post, new { @area = "Core", @id = "BaseFeedBackForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\BaseFeedBack\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 16 "..\..\Views\BaseFeedBack\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">创建时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CreationTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 847), Tuple.Create("\"", 874)
            
            #line 22 "..\..\Views\BaseFeedBack\info.cshtml"
                                                          , Tuple.Create(Tuple.Create("", 855), Tuple.Create<System.Object, System.Int32>(Model.CreationTime
            
            #line default
            #line hidden
, 855), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">创建人姓名</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CreatorEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1129), Tuple.Create("\"", 1158)
            
            #line 26 "..\..\Views\BaseFeedBack\info.cshtml"
                                                                                        , Tuple.Create(Tuple.Create("", 1137), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 1137), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">反馈原因</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SendReason\"");

WriteLiteral(" style=\"width:98%; height:40px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1435), Tuple.Create("\"", 1460)
            
            #line 33 "..\..\Views\BaseFeedBack\info.cshtml"
                                                                                , Tuple.Create(Tuple.Create("", 1443), Tuple.Create<System.Object, System.Int32>(Model.SendReason
            
            #line default
            #line hidden
, 1443), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">反馈状态</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"SendStatus\"");

WriteLiteral(" name=\"SendStatus\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">新反馈</option>\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">已处理</option>\r\n                </select>\r\n            </td>\r\n        </tr>\r\n\r\n   " +
"     <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">反馈备注</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SendNote\"");

WriteLiteral("   style=\"width:98%; height:60px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2041), Tuple.Create("\"", 2064)
            
            #line 47 "..\..\Views\BaseFeedBack\info.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 2049), Tuple.Create<System.Object, System.Int32>(Model.SendNote
            
            #line default
            #line hidden
, 2049), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 55 "..\..\Views\BaseFeedBack\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 55 "..\..\Views\BaseFeedBack\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BaseFeedBack";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 60 "..\..\Views\BaseFeedBack\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 60 "..\..\Views\BaseFeedBack\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 65 "..\..\Views\BaseFeedBack\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
