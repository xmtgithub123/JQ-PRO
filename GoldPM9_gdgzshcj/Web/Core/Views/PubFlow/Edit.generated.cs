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
    
    #line 2 "..\..\Views\PubFlow\Edit.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PubFlow/Edit.cshtml")]
    public partial class _Views_PubFlow_Edit_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.FlowModel>
    {
        public _Views_PubFlow_Edit_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'FlowModel',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
    JQ.textbox.selEmp({
        id: ""choosetosendEmp"",
        setID: ""ModelFinishSend"",
        width: 638
    });
    $(""#ModelIsWord"").val(""");

            
            #line 17 "..\..\Views\PubFlow\Edit.cshtml"
                       Write(Model.ModelIsWord ? 1:0);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    $(\"#ModelIsRun\").val(\"");

            
            #line 18 "..\..\Views\PubFlow\Edit.cshtml"
                      Write(Model.ModelIsRun?1:0);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    $(\"#ModelType\").val(\"");

            
            #line 19 "..\..\Views\PubFlow\Edit.cshtml"
                     Write(Model.ModelType);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    $(\"#ModeModular\").val(\"");

            
            #line 20 "..\..\Views\PubFlow\Edit.cshtml"
                       Write(Model.ModeModular);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    $(\"#IsRefProject\").val(\"");

            
            #line 21 "..\..\Views\PubFlow\Edit.cshtml"
                        Write(ViewBag.IsRefProject);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n    $(\"#IsAllowUndo\").val(\"");

            
            #line 22 "..\..\Views\PubFlow\Edit.cshtml"
                       Write(ViewBag.IsAllowUndo);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n</script>\r\n");

            
            #line 24 "..\..\Views\PubFlow\Edit.cshtml"
 using (Html.BeginForm("save", "PubFlow", FormMethod.Post, new { @area = "Core", @id = "FlowModel", @name = "FlowModel" }))
{
    
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\PubFlow\Edit.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\PubFlow\Edit.cshtml"
                                

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">关联表名</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelRefTable\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,400]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1298), Tuple.Create("\"", 1326)
            
            #line 31 "..\..\Views\PubFlow\Edit.cshtml"
                                                                    , Tuple.Create(Tuple.Create("", 1306), Tuple.Create<System.Object, System.Int32>(Model.ModelRefTable
            
            #line default
            #line hidden
, 1306), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">模板名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1556), Tuple.Create("\"", 1580)
            
            #line 35 "..\..\Views\PubFlow\Edit.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 1564), Tuple.Create<System.Object, System.Int32>(Model.ModelName
            
            #line default
            #line hidden
, 1564), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">模板编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1812), Tuple.Create("\"", 1838)
            
            #line 41 "..\..\Views\PubFlow\Edit.cshtml"
                                     , Tuple.Create(Tuple.Create("", 1820), Tuple.Create<System.Object, System.Int32>(Model.ModelNumber
            
            #line default
            #line hidden
, 1820), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">版本号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelVersion\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2041), Tuple.Create("\"", 2068)
            
            #line 45 "..\..\Views\PubFlow\Edit.cshtml"
                                      , Tuple.Create(Tuple.Create("", 2049), Tuple.Create<System.Object, System.Int32>(Model.ModelVersion
            
            #line default
            #line hidden
, 2049), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否自定义表单</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ModelIsWord\"");

WriteLiteral(" name=\"ModelIsWord\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否流转</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ModelIsRun\"");

WriteLiteral(" name=\"ModelIsRun\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </td>\r\n        </tr>\r\n       " +
" <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">模版类型</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ModelType\"");

WriteLiteral(" name=\"ModelType\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">表单流程</option>\r\n                    <option");

WriteLiteral(" value=\"2\"");

WriteLiteral(">校审流程</option>\r\n                </select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">所属模块</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ModeModular\"");

WriteLiteral(" name=\"ModeModular\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">项目表单</option>\r\n                    <option");

WriteLiteral(" value=\"2\"");

WriteLiteral(">办公表单</option>\r\n                </select>\r\n            </td>\r\n        </tr>\r\n    " +
"    <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">签名列表控件</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"ModelSign\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3733), Tuple.Create("\"", 3759)
            
            #line 82 "..\..\Views\PubFlow\Edit.cshtml"
                                                , Tuple.Create(Tuple.Create("", 3741), Tuple.Create<System.Object, System.Int32>(Model.ModelSign
            
            #line default
            #line hidden
, 3741), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">与项目关联</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"IsRefProject\"");

WriteLiteral(" name=\"IsRefProject\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                </select>\r\n            </td>\r\n        </tr>\r\n       " +
" <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">消息发送格式</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ModelRole\"");

WriteLiteral(" style=\"width:98%;height:60px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" spellcheck=\"false\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteLiteral(">");

            
            #line 94 "..\..\Views\PubFlow\Edit.cshtml"
                                                                                                                                                                       Write(Model.ModelRole);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">流转处理页面路径</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelUrl\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" spellcheck=\"false\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4691), Tuple.Create("\"", 4714)
            
            #line 100 "..\..\Views\PubFlow\Edit.cshtml"
                                                     , Tuple.Create(Tuple.Create("", 4699), Tuple.Create<System.Object, System.Int32>(Model.ModelUrl
            
            #line default
            #line hidden
, 4699), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>处理页面宽度</th>\r" +
"\n            <td><input");

WriteLiteral(" id=\"DialogWidth\"");

WriteLiteral(" name=\"DialogWidth\"");

WriteLiteral(" style=\"width:60%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4899), Tuple.Create("\"", 4929)
            
            #line 105 "..\..\Views\PubFlow\Edit.cshtml"
                               , Tuple.Create(Tuple.Create("", 4907), Tuple.Create<System.Object, System.Int32>(ViewBag.DialogWidth
            
            #line default
            #line hidden
, 4907), false)
);

WriteLiteral(" /> px</td>\r\n            <th>处理页面高度</th>\r\n            <td><input");

WriteLiteral(" id=\"DialogHeight\"");

WriteLiteral(" name=\"DialogHeight\"");

WriteLiteral(" style=\"width:60%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5076), Tuple.Create("\"", 5107)
            
            #line 107 "..\..\Views\PubFlow\Edit.cshtml"
                                 , Tuple.Create(Tuple.Create("", 5084), Tuple.Create<System.Object, System.Int32>(ViewBag.DialogHeight
            
            #line default
            #line hidden
, 5084), false)
);

WriteLiteral(" /> px</td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">流转列表页面路径</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelListUrl\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5345), Tuple.Create("\"", 5372)
            
            #line 112 "..\..\Views\PubFlow\Edit.cshtml"
                                      , Tuple.Create(Tuple.Create("", 5353), Tuple.Create<System.Object, System.Int32>(Model.ModelListUrl
            
            #line default
            #line hidden
, 5353), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">审批结束发送人员</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ModelFinishSend\"");

WriteLiteral(" id=\"ModelFinishSend\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5591), Tuple.Create("\"", 5623)
            
            #line 118 "..\..\Views\PubFlow\Edit.cshtml"
         , Tuple.Create(Tuple.Create("", 5599), Tuple.Create<System.Object, System.Int32>(Model.ModelFinishSend
            
            #line default
            #line hidden
, 5599), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"choosetosendEmp\"");

WriteLiteral(" name=\"choosetosendEmp\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5718), Tuple.Create("\"", 5755)
            
            #line 119 "..\..\Views\PubFlow\Edit.cshtml"
                  , Tuple.Create(Tuple.Create("", 5726), Tuple.Create<System.Object, System.Int32>(ViewBag.FinishSendEmpNames
            
            #line default
            #line hidden
, 5726), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">审批结束后可以修改控件</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ModelFinishControls\"");

WriteLiteral(" style=\"width:98%;height:60px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 125 "..\..\Views\PubFlow\Edit.cshtml"
                                                                                                                                                               Write(Model.ModelFinishControls);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n  " +
"              审批结束后允许撤销\r\n            </th>\r\n            <td>\r\n                <s" +
"elect");

WriteLiteral(" id=\"IsAllowUndo\"");

WriteLiteral(" name=\"IsAllowUndo\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"panelHeight:58,editable:false\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                </select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                审批结束后可修改（撤销）角色\r\n            </th>\r\n            <td>\r\n         " +
"       <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"cbAdminsitrator\"");

WriteLiteral(" name=\"cbApprovedEditRoles\"");

WriteLiteral(" value=\"Administrator\"");

WriteAttribute("checked", Tuple.Create(" checked=\"", 6715), Tuple.Create("\"", 6761)
            
            #line 142 "..\..\Views\PubFlow\Edit.cshtml"
                                       , Tuple.Create(Tuple.Create("", 6725), Tuple.Create<System.Object, System.Int32>(ViewBag.ApproveEdit_Administrator
            
            #line default
            #line hidden
, 6725), false)
);

WriteLiteral(" /><label");

WriteLiteral(" for=\"cbAdminsitrator\"");

WriteLiteral(">系统管理员</label>\r\n                <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"cbCreator\"");

WriteLiteral(" name=\"cbApprovedEditRoles\"");

WriteLiteral(" value=\"Creator\"");

WriteAttribute("checked", Tuple.Create(" checked=\"", 6905), Tuple.Create("\"", 6945)
            
            #line 143 "..\..\Views\PubFlow\Edit.cshtml"
                           , Tuple.Create(Tuple.Create("", 6915), Tuple.Create<System.Object, System.Int32>(ViewBag.ApproveEdit_Creator
            
            #line default
            #line hidden
, 6915), false)
);

WriteLiteral("/><label");

WriteLiteral(" for=\"cbCreator\"");

WriteLiteral(">创建人</label>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 147 "..\..\Views\PubFlow\Edit.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
