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
    
    #line 2 "..\..\Views\ModelVolCatalog\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ModelVolCatalog/info.cshtml")]
    public partial class _Views_ModelVolCatalog_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ModelVolCatalog>
    {
        public _Views_ModelVolCatalog_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'ModelVolCatalogForm\',//formid提交需要用到" +
"\r\n        buttonTypes: ");

            
            #line 6 "..\..\Views\ModelVolCatalog\info.cshtml"
                Write(Html.Raw(ViewBag.editPermission));

            
            #line default
            #line hidden
WriteLiteral(",//默认按钮\r\n        succesCollBack: function (data) {//成功回调函数,data为服务器返回值\r\n         " +
"   JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用\r\n            JQ.dialo" +
"g.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源\r\n        }\r\n    });\r\n</script>\r\n");

            
            #line 13 "..\..\Views\ModelVolCatalog\info.cshtml"
 using (Html.BeginForm("save", "ModelVolCatalog", FormMethod.Post, new { @area = "Core", @id = "ModelVolCatalogForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\ModelVolCatalog\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\ModelVolCatalog\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral("> \r\n        <tr> \r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelVolNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 881), Tuple.Create("\"", 910)
            
            #line 20 "..\..\Views\ModelVolCatalog\info.cshtml"
                                                                      , Tuple.Create(Tuple.Create("", 889), Tuple.Create<System.Object, System.Int32>(Model.ModelVolNumber
            
            #line default
            #line hidden
, 889), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelVolName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1141), Tuple.Create("\"", 1168)
            
            #line 24 "..\..\Views\ModelVolCatalog\info.cshtml"
                                                                   , Tuple.Create(Tuple.Create("", 1149), Tuple.Create<System.Object, System.Int32>(Model.ModelVolName
            
            #line default
            #line hidden
, 1149), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">阶段</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 31 "..\..\Views\ModelVolCatalog\info.cshtml"
           Write(BaseData.getBase(new Params()
                    {
                        controlName = "PhaseID",
                        isRequired = true,
                        engName = "ProjectPhase",
                        width = "98%",
                        ids = Model.PhaseID.ToString()
                    }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">专业</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 42 "..\..\Views\ModelVolCatalog\info.cshtml"
           Write(BaseData.getBase(new Params()
                    {
                        controlName = "SpecialID",
                        isRequired = true,
                        engName = "Special",
                        width = "98%",
                        ids = Model.SpecialID.ToString()
                    }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr> \r\n        <tr> \r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelVolNote\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,500]\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2316), Tuple.Create("\"", 2343)
            
            #line 55 "..\..\Views\ModelVolCatalog\info.cshtml"
                                                                               , Tuple.Create(Tuple.Create("", 2324), Tuple.Create<System.Object, System.Int32>(Model.ModelVolNote
            
            #line default
            #line hidden
, 2324), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>  \r\n    </table>\r\n");

            
            #line 59 "..\..\Views\ModelVolCatalog\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
