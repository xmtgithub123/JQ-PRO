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
    
    #line 2 "..\..\Views\ArchProjectFolder\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchProjectFolder/info.cshtml")]
    public partial class _Views_ArchProjectFolder_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ArchProjectFolder>
    {
        public _Views_ArchProjectFolder_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'ArchShareFolderForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });

    JQ.combobox.selEmp({
        id: 'FolderManagerId'
    });
</script>
");

            
            #line 17 "..\..\Views\ArchProjectFolder\info.cshtml"
 using (Html.BeginForm("save", "ArchShareFolder", FormMethod.Post, new { @area = "Archive", @id = "ArchShareFolderForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\ArchProjectFolder\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\ArchProjectFolder\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">父节点名称</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ParentName\"");

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 902), Tuple.Create("\"", 929)
            
            #line 24 "..\..\Views\ArchProjectFolder\info.cshtml"
                               , Tuple.Create(Tuple.Create("", 910), Tuple.Create<System.Object, System.Int32>(ViewBag.ParentName
            
            #line default
            #line hidden
, 910), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"FolderParentID\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 993), Tuple.Create("\"", 1022)
            
            #line 25 "..\..\Views\ArchProjectFolder\info.cshtml"
, Tuple.Create(Tuple.Create("", 1001), Tuple.Create<System.Object, System.Int32>(Model.FolderParentID
            
            #line default
            #line hidden
, 1001), false)
);

WriteLiteral("  />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">文件夹名称</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FolderName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1284), Tuple.Create("\"", 1309)
            
            #line 31 "..\..\Views\ArchProjectFolder\info.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 1292), Tuple.Create<System.Object, System.Int32>(Model.FolderName
            
            #line default
            #line hidden
, 1292), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">描述</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral("  name=\"FolderRemark\"");

WriteLiteral(" style=\"width:98%; height:60px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 38 "..\..\Views\ArchProjectFolder\info.cshtml"
                                                                                                                           Write(Model.FolderRemark);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        ");

WriteLiteral("\r\n    </table>\r\n");

            
            #line 55 "..\..\Views\ArchProjectFolder\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
