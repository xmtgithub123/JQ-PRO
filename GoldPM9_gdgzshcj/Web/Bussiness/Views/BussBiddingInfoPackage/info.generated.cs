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
    
    #line 2 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingInfoPackage/info.cshtml")]
    public partial class _Views_BussBiddingInfoPackage_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussBiddingInfoPackage>
    {
        public _Views_BussBiddingInfoPackage_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'BussBiddingInfoPackageForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.treegrid.autoReftreegrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
        });
    });
</script>
");

            
            #line 15 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
 using (Html.BeginForm("save", "BussBiddingInfoPackage", FormMethod.Post, new { @area = "Bussiness", @id = "BussBiddingInfoPackageForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\t \r\n                        <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">主表ID</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("\t\t\t\t\t\t  ");

            
            #line 23 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
   Write(BaseData.getBase(new Params()
           {
               controlName = "BussBiddingInfoID",
               isRequired = true,
               engName = "department",
               width = "98%",
               ids = Model.BussBiddingInfoID.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">包号</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"PackageNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("  data-options=\"required:true\"");

WriteLiteral("  validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1401), Tuple.Create("\"", 1429)
            
            #line 34 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
                                                           , Tuple.Create(Tuple.Create("", 1409), Tuple.Create<System.Object, System.Int32>(Model.PackageNumber
            
            #line default
            #line hidden
, 1409), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n \r\n           " +
"             <tr>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标进度</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"BiddingProgress\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("   validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1733), Tuple.Create("\"", 1763)
            
            #line 41 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
                                      , Tuple.Create(Tuple.Create("", 1741), Tuple.Create<System.Object, System.Int32>(Model.BiddingProgress
            
            #line default
            #line hidden
, 1741), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">中标时间</th>\r\n                        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\t\t\t\t\t\t<input");

WriteLiteral(" name=\"WinTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral("   validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1989), Tuple.Create("\"", 2011)
            
            #line 45 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
                        , Tuple.Create(Tuple.Create("", 1997), Tuple.Create<System.Object, System.Int32>(Model.WinTime
            
            #line default
            #line hidden
, 1997), false)
);

WriteLiteral(" />\r\n                        </td>\r\n                        </tr>\r\n        <tr>\r\n" +
"            <th>\r\n                上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 53 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
           Write(Html.Action("uploadFile", "usercontrol", new
           {
               @area = "Core",
               AttachRefID = Model.Id,
               AttachRefTable = "BussBiddingInfoPackage"
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 62 "..\..\Views\BussBiddingInfoPackage\info.cshtml"
	
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591