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
    
    #line 2 "..\..\Views\DesTaskCheck\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTaskCheck/info.cshtml")]
    public partial class _Views_DesTaskCheck_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.DesTaskCheck>
    {
        public _Views_DesTaskCheck_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'DesTaskCheckform',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
</script>
");

            
            #line 13 "..\..\Views\DesTaskCheck\info.cshtml"
 using (Html.BeginForm("save", "DesTaskCheck", FormMethod.Post, new { @area = "Design", @id = "DesTaskCheckForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\DesTaskCheck\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\DesTaskCheck\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\t \r\n                        <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目ID</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("   validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 861), Tuple.Create("\"", 882)
            
            #line 21 "..\..\Views\DesTaskCheck\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 869), Tuple.Create<System.Object, System.Int32>(Model.ProjID
            
            #line default
            #line hidden
, 869), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">阶段ID</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"PhaseID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("   validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1114), Tuple.Create("\"", 1136)
            
            #line 25 "..\..\Views\DesTaskCheck\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 1122), Tuple.Create<System.Object, System.Int32>(Model.PhaseID
            
            #line default
            #line hidden
, 1122), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n \r\n           " +
"             <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">设计节点</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t");

WriteLiteral("\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">提出节点</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t");

WriteLiteral("\r\n                        </td>\r\n                        </tr>\r\n \r\n              " +
"          <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">错误类型</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"CheckNote\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("  data-options=\"required:true\"");

WriteLiteral("  validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2173), Tuple.Create("\"", 2197)
            
            #line 46 "..\..\Views\DesTaskCheck\info.cshtml"
                                                         , Tuple.Create(Tuple.Create("", 2181), Tuple.Create<System.Object, System.Int32>(Model.CheckNote
            
            #line default
            #line hidden
, 2181), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n \r\n           " +
"             <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">提出人</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"CheckEmpId\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("   validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2495), Tuple.Create("\"", 2520)
            
            #line 53 "..\..\Views\DesTaskCheck\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 2503), Tuple.Create<System.Object, System.Int32>(Model.CheckEmpId
            
            #line default
            #line hidden
, 2503), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">设计确认</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t");

WriteLiteral("\r\n                        </td>\r\n                        </tr>\r\n \r\n              " +
"          <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">校审确认</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t");

WriteLiteral("\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">校审建议</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t");

WriteLiteral("\r\n                        </td>\r\n                        </tr>\r\n \r\n              " +
"          <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">贴图</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"CheckImage\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("   validType=\"length[0,2147483647]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3660), Tuple.Create("\"", 3685)
            
            #line 75 "..\..\Views\DesTaskCheck\info.cshtml"
                                   , Tuple.Create(Tuple.Create("", 3668), Tuple.Create<System.Object, System.Int32>(Model.CheckImage
            
            #line default
            #line hidden
, 3668), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">提出日期</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"CheckDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral("   validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3913), Tuple.Create("\"", 3937)
            
            #line 79 "..\..\Views\DesTaskCheck\info.cshtml"
                          , Tuple.Create(Tuple.Create("", 3921), Tuple.Create<System.Object, System.Int32>(Model.CheckDate
            
            #line default
            #line hidden
, 3921), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n \r\n           " +
"             <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">校审答复</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"CheckXml\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("   validType=\"length[0,-1]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4228), Tuple.Create("\"", 4251)
            
            #line 86 "..\..\Views\DesTaskCheck\info.cshtml"
                         , Tuple.Create(Tuple.Create("", 4236), Tuple.Create<System.Object, System.Int32>(Model.CheckXml
            
            #line default
            #line hidden
, 4236), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n        <tr>\r\n" +
"            <th>\r\n                上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 94 "..\..\Views\DesTaskCheck\info.cshtml"
           Write(Html.Action("uploadFile", "usercontrol", new
           {
               @area = "Core",
               AttachRefID = Model.Id,
               AttachRefTable = "DesTaskCheck"
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 103 "..\..\Views\DesTaskCheck\info.cshtml"
	
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
