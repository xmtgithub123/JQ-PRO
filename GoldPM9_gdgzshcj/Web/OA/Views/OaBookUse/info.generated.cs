﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
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
    
    #line 2 "..\..\Views\OaBookUse\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaBookUse/info.cshtml")]
    public partial class _Views_OaBookUse_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaBookUse>
    {
        public _Views_OaBookUse_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        JQ.form.submitInit({
            formid: 'OaBookUseForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
            ,
            beforeFormSubmit: function () {
            var result = true;
            var LendCount = $(""#LendCount"").textbox(""getValue"");
            var rowCount = ");

            
            #line 15 "..\..\Views\OaBookUse\info.cshtml"
                      Write(ViewBag.rowCount);

            
            #line default
            #line hidden
WriteLiteral(@";
                if (LendCount<=0||LendCount>rowCount) {
                    result = false;
                    JQ.dialog.info(""请填写正确的[借阅数量]!<br/>借阅数量不能超过["" + rowCount + ""]本"");
                }
                //if (LendCount>rowCount) {
                //    result = false;
                //    JQ.dialog.info(""借阅数量不能超过--["" + rowCount + ""]--本"");
                //}
            return result;
        }


    });

</script>
");

            
            #line 31 "..\..\Views\OaBookUse\info.cshtml"
 using (Html.BeginForm("save", "OaBookUse", FormMethod.Post, new { @area = "OA", @id = "OaBookUseForm" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" name=\"Id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1233), Tuple.Create("\"", 1250)
            
            #line 33 "..\..\Views\OaBookUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 1241), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 1241), false)
);

WriteLiteral(" />\r\n");

            
            #line 34 "..\..\Views\OaBookUse\info.cshtml"

    
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\OaBookUse\info.cshtml"
Write(Html.HiddenFor(m => m.ReturnEmpId));

            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\OaBookUse\info.cshtml"
                                       
    
            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\OaBookUse\info.cshtml"
Write(Html.HiddenFor(m => m.ReturnEmpName));

            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\OaBookUse\info.cshtml"
                                         
    var bookModel = ViewBag.bookModel as DataModel.Models.OaBook;

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" name=\"BookID\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1447), Tuple.Create("\"", 1468)
            
            #line 38 "..\..\Views\OaBookUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 1455), Tuple.Create<System.Object, System.Int32>(bookModel.Id
            
            #line default
            #line hidden
, 1455), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">图书编号 </th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 44 "..\..\Views\OaBookUse\info.cshtml"
           Write(Html.Label("BookNameNumber", bookModel.BookNameNumber));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">图书名称</th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 51 "..\..\Views\OaBookUse\info.cshtml"
           Write(Html.Label("BookName", bookModel.BookName));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">借阅时间</th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"DateLend\"");

WriteLiteral(" name=\"DateLend\"");

WriteLiteral(" style=\"width:50%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2095), Tuple.Create("\"", 2118)
            
            #line 57 "..\..\Views\OaBookUse\info.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 2103), Tuple.Create<System.Object, System.Int32>(Model.DateLend
            
            #line default
            #line hidden
, 2103), false)
);

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">借阅数量</th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"LendCount\"");

WriteLiteral(" name=\"LendCount\"");

WriteLiteral(" style=\"width:50%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2390), Tuple.Create("\"", 2414)
            
            #line 64 "..\..\Views\OaBookUse\info.cshtml"
                                                       , Tuple.Create(Tuple.Create("", 2398), Tuple.Create<System.Object, System.Int32>(Model.LendCount
            
            #line default
            #line hidden
, 2398), false)
);

WriteLiteral(" />\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">计划归还时间</th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"DateReturnPlan\"");

WriteLiteral(" name=\"DateReturnPlan\"");

WriteLiteral(" style=\"width:50%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2678), Tuple.Create("\"", 2707)
            
            #line 72 "..\..\Views\OaBookUse\info.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 2686), Tuple.Create<System.Object, System.Int32>(Model.DateReturnPlan
            
            #line default
            #line hidden
, 2686), false)
);

WriteLiteral(" />\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"20%\"");

WriteLiteral(">借阅备注</th>\r\n            <td");

WriteLiteral(" width=\"80%\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"LendNote\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validtype=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 79 "..\..\Views\OaBookUse\info.cshtml"
                                                                                                                                                    Write(Model.LendNote);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 83 "..\..\Views\OaBookUse\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591