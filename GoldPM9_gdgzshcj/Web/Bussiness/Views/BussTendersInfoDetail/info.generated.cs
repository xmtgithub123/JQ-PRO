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
    
    #line 2 "..\..\Views\BussTendersInfoDetail\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussTendersInfoDetail/info.cshtml")]
    public partial class _Views_BussTendersInfoDetail_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussTendersInfoDetail>
    {
        public _Views_BussTendersInfoDetail_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'BussTendersInfoDetailForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.treegrid.autoReftreegrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
        });
    });
</script>
");

            
            #line 15 "..\..\Views\BussTendersInfoDetail\info.cshtml"
 using (Html.BeginForm("save", "BussTendersInfoDetail", FormMethod.Post, new { @area = "Bussiness", @id = "BussTendersInfoDetailForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\BussTendersInfoDetail\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\BussTendersInfoDetail\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">BussTendersInfo表ID</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 23 "..\..\Views\BussTendersInfoDetail\info.cshtml"
           Write(BaseData.getBase(new Params()
                     {
                         controlName = "BussTendersInfoID",
                         isRequired = true,
                         engName = "department",
                         width = "98%",
                         ids = Model.BussTendersInfoID.ToString()
                     }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委表ID</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustomerID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1396), Tuple.Create("\"", 1421)
            
            #line 34 "..\..\Views\BussTendersInfoDetail\info.cshtml"
                                         , Tuple.Create(Tuple.Create("", 1404), Tuple.Create<System.Object, System.Int32>(Model.CustomerID
            
            #line default
            #line hidden
, 1404), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">商务得分</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BusinessPoints\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1663), Tuple.Create("\"", 1692)
            
            #line 41 "..\..\Views\BussTendersInfoDetail\info.cshtml"
                                             , Tuple.Create(Tuple.Create("", 1671), Tuple.Create<System.Object, System.Int32>(Model.BusinessPoints
            
            #line default
            #line hidden
, 1671), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">技术得分</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TechnologyPoints\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1905), Tuple.Create("\"", 1936)
            
            #line 45 "..\..\Views\BussTendersInfoDetail\info.cshtml"
                                               , Tuple.Create(Tuple.Create("", 1913), Tuple.Create<System.Object, System.Int32>(Model.TechnologyPoints
            
            #line default
            #line hidden
, 1913), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">总体得分</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TotalityPoints\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2178), Tuple.Create("\"", 2207)
            
            #line 52 "..\..\Views\BussTendersInfoDetail\info.cshtml"
                                             , Tuple.Create(Tuple.Create("", 2186), Tuple.Create<System.Object, System.Int32>(Model.TotalityPoints
            
            #line default
            #line hidden
, 2186), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否中标</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 64 "..\..\Views\BussTendersInfoDetail\info.cshtml"
           Write(Html.Action("uploadFile", "usercontrol", new
           {
               @area = "Core",
               AttachRefID = Model.Id,
               AttachRefTable = "BussTendersInfoDetail"
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 73 "..\..\Views\BussTendersInfoDetail\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
