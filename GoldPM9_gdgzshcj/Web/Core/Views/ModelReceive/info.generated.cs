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
    
    #line 2 "..\..\Views\ModelReceive\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ModelReceive/info.cshtml")]
    public partial class _Views_ModelReceive_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ModelReceive>
    {
        public _Views_ModelReceive_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'ModelReceiveForm\',//formid提交需要用到\r\n " +
"       buttonTypes: ");

            
            #line 6 "..\..\Views\ModelReceive\info.cshtml"
                Write(Html.Raw(ViewBag.editPermission));

            
            #line default
            #line hidden
WriteLiteral(@",//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeFormSubmit: function () {
            var flag1 = false;
            //判断checkbox是否选中
            $(""input[name='ModelReceiveIsMust']"").each(function () {
                if ($(this).attr(""checked"")) {
                    flag1 = true;
                }
            }); 
            if (flag1) {
                return true;
            }
            else {
                JQ.dialog.warning('请选择是否必要条件');
                return false;
            }
        }
    });
    $(""input[name='ModelReceiveIsMust']"").click(function () {
        if ($(this).attr(""checked"")) {
            $(""input[name='ModelReceiveIsMust']"").attr(""checked"", false);
            $(this).attr(""checked"", true);
        }
    });
</script>
");

            
            #line 35 "..\..\Views\ModelReceive\info.cshtml"
 using (Html.BeginForm("save", "ModelReceive", FormMethod.Post, new { @area = "Core", @id = "ModelReceiveForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 37 "..\..\Views\ModelReceive\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 37 "..\..\Views\ModelReceive\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目类型</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 43 "..\..\Views\ModelReceive\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ProjectTypeID",
               isRequired = true,
               engName = "ProjectType",
               width = "98%",
               typeId = "1",
               ids = Model.ProjectTypeID.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">阶段</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 56 "..\..\Views\ModelReceive\info.cshtml"
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
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">模板名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelReceiveName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2398), Tuple.Create("\"", 2429)
            
            #line 69 "..\..\Views\ModelReceive\info.cshtml"
                                                                       , Tuple.Create(Tuple.Create("", 2406), Tuple.Create<System.Object, System.Int32>(Model.ModelReceiveName
            
            #line default
            #line hidden
, 2406), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否必要条件</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

            
            #line 73 "..\..\Views\ModelReceive\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 73 "..\..\Views\ModelReceive\info.cshtml"
                  
    var ischecked = "";
    if (ViewBag.isAdd == null)
    {
        ischecked = "checked='checked'";
    }
    var val = Model.ModelReceiveIsMust;

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"ModelReceiveIsMust\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" ");

            
            #line 80 "..\..\Views\ModelReceive\info.cshtml"
                                                           Write(val == true ? ischecked : "");

            
            #line default
            #line hidden
WriteLiteral(" />");

WriteLiteral("是");

WriteLiteral("\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"ModelReceiveIsMust\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" ");

            
            #line 81 "..\..\Views\ModelReceive\info.cshtml"
                                                           Write(val == false ? ischecked : "");

            
            #line default
            #line hidden
WriteLiteral(" />");

WriteLiteral("否");

WriteLiteral("\r\n");

            
            #line 82 "..\..\Views\ModelReceive\info.cshtml"
                
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">收资内容</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ExchReceiveItem\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" data-options=\"required:true,multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3232), Tuple.Create("\"", 3262)
            
            #line 89 "..\..\Views\ModelReceive\info.cshtml"
                                                                                                , Tuple.Create(Tuple.Create("", 3240), Tuple.Create<System.Object, System.Int32>(Model.ExchReceiveItem
            
            #line default
            #line hidden
, 3240), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        ");

WriteLiteral("\r\n    </table>\r\n");

            
            #line 107 "..\..\Views\ModelReceive\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591