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
    
    #line 5 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoGCDZKCTJDJZMobile/info.cshtml")]
    public partial class _Views_IsoGCDZKCTJDJZMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoGCDZKCTJDJZ>
    {
        public _Views_IsoGCDZKCTJDJZMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
  
    Layout = "~/Areas/Core/Views/Mobile/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .aui-btn {\r\n        padding: 10px 15px;\r\n        font-size: 13px;\r\n    }\r\n" +
"\r\n    .aui-btn-row:after {\r\n        border: 0;\r\n    }\r\n    .p-15{\r\n        paddi" +
"ng:15px;\r\n    }\r\n\r\n\r\n    .set-disabled {\r\n        pointer-events: none !importan" +
"t\r\n    }\r\n\r\n        .error {\r\n            color: red;\r\n            font-size: 14" +
"px;\r\n        }\r\n\r\n    .hidden {\r\n        display: none;\r\n    }\r\n\r\n    .aui-input" +
"-hook {\r\n        line-height: 34px;\r\n        text-align: left;\r\n        padding-" +
"left: 20px;\r\n        background: #efefef;\r\n    }\r\n\r\n    .aui-display-hook {\r\n   " +
"     display: none;\r\n        width: 30%;\r\n        float: right;\r\n        font-si" +
"ze: 13px;\r\n        line-height: 33px;\r\n        margin-right: 6px;\r\n        paddi" +
"ng: 3px 6px;\r\n    }\r\n\r\n    .aui-input-width {\r\n        width: 98%;\r\n    }\r\n\r\n   " +
" .hiddenShow {\r\n        height: 0;\r\n    }\r\n\r\n    .aui-input-row label.aui-input-" +
"addon {\r\n        min-width: 90px;\r\n    }\r\n\r\n    .set-selected-icon select {\r\n   " +
"     float: right;\r\n        margin-right: 5px;\r\n        border: 0;\r\n        marg" +
"in-bottom: 0;\r\n    }\r\n\r\n    .set-selected-icon i {\r\n        position: absolute;\r" +
"\n        right: 25px;\r\n        top: 15px;\r\n    }\r\n    .aui-input-row-position {\r" +
"\n        position: relative;\r\n        display: table;\r\n        padding: 6px 0;\r\n" +
"    }\r\n\r\n    .aui-input-row label.aui-input-addon {\r\n        font-size: 14px;\r\n " +
"   }\r\n\r\n    .jq-text-disabled {\r\n        color: #898787;\r\n        width: 98%;\r\n " +
"       pointer-events: none;\r\n        background-color: #efefef !important;\r\n   " +
"     border: 0;\r\n        font-size: 13px;\r\n    }\r\n    .aui-input-check-disable {" +
"\r\n        font-size: 12px !important;\r\n        font-weight: normal !important;\r\n" +
"    }\r\n\r\n    .aui-input-check-disable label {\r\n        line-height: 45px;\r\n     " +
"   margin-right: 15px;\r\n    }\r\n\r\n        .aui-input-check-disable input[type=\"ch" +
"eckbox\"], .aui-input-check-disable input[type=\"radio\"] {\r\n            width: aut" +
"o;\r\n            margin: 0 5px 0 0;\r\n            line-height: 45px;\r\n        }\r\n<" +
"/style>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"aui-content\"");

WriteLiteral(">\r\n        <form");

WriteLiteral(" id=\"IsoGCDZKCTJDJZForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n               表 <label>");

            
            #line 117 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                   Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2416), Tuple.Create("\"", 2442)
            
            #line 117 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                 , Tuple.Create(Tuple.Create("", 2424), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 2424), false)
);

WriteLiteral(" />\r\n                <br />编号 <input");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" style=\"display:inline;min-width: 60px;max-width: 60%;border: 1px solid #dbdbdb; " +
"border-radius: 6px;padding: 3px 6px;margin:0 3px;text-align:center;\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2731), Tuple.Create("\"", 2752)
            
            #line 118 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                                                                                   , Tuple.Create(Tuple.Create("", 2739), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2739), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2946), Tuple.Create("\"", 2963)
            
            #line 122 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2954), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 2954), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 123 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                        Write(Model.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3127), Tuple.Create("\"", 3152)
            
            #line 124 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 3135), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 3135), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"FormType\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3271), Tuple.Create("\"", 3296)
            
            #line 125 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3279), Tuple.Create<System.Object, System.Int32>(ViewBag.FormType
            
            #line default
            #line hidden
, 3279), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3498), Tuple.Create("\"", 3519)
            
            #line 129 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3506), Tuple.Create<System.Object, System.Int32>(Model.ProjID
            
            #line default
            #line hidden
, 3506), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3609), Tuple.Create("\"", 3632)
            
            #line 130 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3617), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 3617), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 131 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                        Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;阶段名称</label>\r\n                <input");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3934), Tuple.Create("\"", 3962)
            
            #line 135 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                      , Tuple.Create(Tuple.Create("", 3942), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 3942), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 136 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                        Write(Model.ProjPhaseName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseId\"");

WriteLiteral(" id=\"ProjPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4113), Tuple.Create("\"", 4139)
            
            #line 137 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4121), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 4121), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;成果交付日期</label>\r\n                <input");

WriteLiteral(" type=\"date\"");

WriteLiteral(" id=\"EngCGJFRQ\"");

WriteLiteral(" name=\"EngCGJFRQ\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4365), Tuple.Create("\"", 4412)
            
            #line 141 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 4373), Tuple.Create<System.Object, System.Int32>(Model.EngCGJFRQ.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 4373), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;工程地点</label>\r\n                <input");

WriteLiteral(" name=\"EngGCDD\"");

WriteLiteral(" id=\"EngGCDD\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4632), Tuple.Create("\"", 4654)
            
            #line 145 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 4640), Tuple.Create<System.Object, System.Int32>(Model.EngGCDD
            
            #line default
            #line hidden
, 4640), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;顾客</label>\r\n                <input");

WriteLiteral(" id=\"CustID\"");

WriteLiteral(" name=\"CustID\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4960), Tuple.Create("\"", 4983)
            
            #line 150 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 4968), Tuple.Create<System.Object, System.Int32>(Model.CustName
            
            #line default
            #line hidden
, 4968), false)
);

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;顾客联系人</label>\r\n                <input");

WriteLiteral(" id=\"CustLinkId\"");

WriteLiteral(" name=\"CustLinkId\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustLinkManName\"");

WriteLiteral(" name=\"CustLinkManName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral("  class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5313), Tuple.Create("\"", 5343)
            
            #line 155 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                         , Tuple.Create(Tuple.Create("", 5321), Tuple.Create<System.Object, System.Int32>(Model.CustLinkManName
            
            #line default
            #line hidden
, 5321), false)
);

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;顾客联系电话</label>\r\n                <input");

WriteLiteral(" id=\"CustLinkManTel\"");

WriteLiteral(" name=\"CustLinkManTel\"");

WriteLiteral(" type=\"text\"");

WriteLiteral("  style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create("  value=\"", 5597), Tuple.Create("\"", 5627)
            
            #line 159 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 5606), Tuple.Create<System.Object, System.Int32>(Model.CustLinkManTel
            
            #line default
            #line hidden
, 5606), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设计人</label>\r\n                <input");

WriteLiteral(" id=\"EngSJRId\"");

WriteLiteral(" name=\"EngSJRId\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5856), Tuple.Create("\"", 5879)
            
            #line 163 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 5864), Tuple.Create<System.Object, System.Int32>(Model.EngSJRId
            
            #line default
            #line hidden
, 5864), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"EngSJR\"");

WriteLiteral(" name=\"EngSJR\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5963), Tuple.Create("\"", 5984)
            
            #line 164 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
      , Tuple.Create(Tuple.Create("", 5971), Tuple.Create<System.Object, System.Int32>(Model.EngSJR
            
            #line default
            #line hidden
, 5971), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设计人电话</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"EngSJRDH\"");

WriteLiteral(" name=\"EngSJRDH\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6207), Tuple.Create("\"", 6230)
            
            #line 168 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 6215), Tuple.Create<System.Object, System.Int32>(Model.EngSJRDH
            
            #line default
            #line hidden
, 6215), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设计说明</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"SJSM\"");

WriteLiteral(" id=\"SJSM\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 172 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                 Write(Model.SJSM);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;勘察工作要求</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"KCGZYQ\"");

WriteLiteral(" id=\"KCGZYQ\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 176 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                     Write(Model.KCGZYQ);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;附图</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"FTSM\"");

WriteLiteral(" id=\"FTSM\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 180 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                 Write(Model.FTSM);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;成果要求内容</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"CGYQ\"");

WriteLiteral(" id=\"CGYQ\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 184 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                 Write(Model.CGYQ);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;成果特殊要求</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"CGTSYQ\"");

WriteLiteral(" id=\"CGTSYQ\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 188 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                                                                                                                                                     Write(Model.CGTSYQ);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" id=\"EngineeringId\"");

WriteLiteral(" name=\"EngineeringId\"");

WriteLiteral(" hidden");

WriteAttribute("value", Tuple.Create(" value=\"", 7995), Tuple.Create("\"", 8023)
            
            #line 192 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 8003), Tuple.Create<System.Object, System.Int32>(Model.EngineeringId
            
            #line default
            #line hidden
, 8003), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"EngineeringName\"");

WriteLiteral(" name=\"EngineeringName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8125), Tuple.Create("\"", 8155)
            
            #line 193 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                        , Tuple.Create(Tuple.Create("", 8133), Tuple.Create<System.Object, System.Int32>(Model.EngineeringName
            
            #line default
            #line hidden
, 8133), false)
);

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n               \r\n                <input");

WriteLiteral(" id=\"EngineeringNumber\"");

WriteLiteral(" name=\"EngineeringNumber\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8410), Tuple.Create("\"", 8442)
            
            #line 198 "..\..\Views\IsoGCDZKCTJDJZMobile\info.cshtml"
                            , Tuple.Create(Tuple.Create("", 8418), Tuple.Create<System.Object, System.Int32>(Model.EngineeringNumber
            
            #line default
            #line hidden
, 8418), false)
);

WriteLiteral(">\r\n            </div>\r\n\r\n\r\n\r\n            ");

WriteLiteral("\r\n        </form>\r\n    </div>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        $(function () {\r\n            if (window.JinQuMobile == undefined) {\r" +
"\n                //initFormBegin(JSON.stringify({\r\n                //    \"Result" +
"\": true,\r\n                //    \"Message\": null,\r\n                //    \"NodeNam" +
"e\": null,\r\n                //    \"AllowEditControls\": \"ColAttVal1;btnChooseProje" +
"ct;ProjIDName;Number;ProjID;Words;ConstructUnit;SupplyUnit;FormDate;ColAttVal2;M" +
"odifyContent;DesignName;FormNote;DesChangeUpload;ColAttVal3;ProjName;DesignReaso" +
"n;ColAttVal4\",\r\n                //    \"SignControls\": \"DesignName(签名)\",\r\n       " +
"         //    \"SignControls\": null,\r\n                //    //\"AllowEditControls" +
"\":\"\"\r\n\r\n                //}));\r\n                //initFormBegin();\r\n            " +
"}\r\n\r\n        })\r\n        //页面初始化\r\n        function initFormBegin(params) {\r\n\r\n  " +
"          \r\n            //去除换行\r\n            //alert(window.location.href)\r\n     " +
"       params = params.replace(/[\\r\\n]/g, \"\");\r\n            params = params.repl" +
"ace(/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n\r\n\r\n          " +
"  //按钮替换\r\n            paramsObj.AllowEditControls = paramsObj.AllowEditControls " +
"|| \'-\';\r\n\r\n            //清除成果交付日期，默认为1900-01-01改为空\r\n            var EngCGJFRQ = " +
"$(\'#EngCGJFRQ\').val();;\r\n            if ((EngCGJFRQ == \'1900-01-01\')) {\r\n       " +
"         $(\'#EngCGJFRQ\').val(\'\');\r\n            }\r\n\r\n             //设置表单不可编辑样式及只读" +
"\r\n            $(\'form :input\').addClass(\'jq-text-disabled\').attr(\"readonly\", \"re" +
"adonly\");\r\n            DomUtil.setCtrlsReadonly(paramsObj.AllowEditControls, doc" +
"ument.getElementById(\'IsoGCDZKCTJDJZForm\'));\r\n\r\n            //勘察工作要求，替换换行符\r\n    " +
"        var KCGZYQValue = $(\'#KCGZYQ\').val();\r\n            KCGZYQValue = KCGZYQV" +
"alue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#KCGZYQ\').val(KCG" +
"ZYQValue);\r\n\r\n            //成果要求内容，替换换行符\r\n            var CGYQValue = $(\'#CGYQ\')" +
".val();\r\n            CGYQValue = CGYQValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g," +
" \"\")\r\n            $(\'#CGYQ\').val(CGYQValue);\r\n\r\n            $(\'#KCGZYQ,#CGYQ\').r" +
"emoveClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n            $(\"#KCG" +
"ZYQ,#CGYQ\").css({\r\n                overflow: \'auto\',\r\n                background" +
": \'#efefef\',\r\n                color: \'#898787\'\r\n            })\r\n\r\n            $(" +
"\'input[type=\"checkbox\"]\').attr(\"disabled\", \"disabled\");\r\n            $(\'input[ty" +
"pe=\"radio\"]\').attr(\"disabled\", \"disabled\");\r\n            //告诉移动端页面初始完完毕\r\n       " +
"     if (window.JinQuMobile) {\r\n                window.JinQuMobile.FormEnd();\r\n " +
"           }\r\n        }\r\n\r\n\r\n       \r\n        /*--------------------------------" +
"--------------------------------------------------------------*/\r\n        //表单验证" +
"交互\r\n\r\n        function validateFormBegin() {\r\n            var formVali = $(\'#Iso" +
"GCDZKCTJDJZForm\').validate({\r\n                rules: {\r\n                    EngC" +
"GJFRQ: \'required\',//成果交付日期\r\n                },\r\n                messages: {\r\n   " +
"                 EngCGJFRQ:\'请输入成果交付日期\',\r\n                   \r\n                }\r" +
"\n            });\r\n            //if ($(\'#ProjName\').val() == \'\') {\r\n            /" +
"/    $.alert(\'请选择项目！\')\r\n            //    return false;\r\n            //}\r\n      " +
"      isValidate = formVali.form();\r\n            if (isValidate) {\r\n            " +
"    var formData = DomUtil.serialize(document.getElementById(\'IsoGCDZKCTJDJZForm" +
"\'));//json or =&\r\n                console.log(formData)\r\n                //告诉移动端" +
"页面初始完完毕\r\n                if (window.JinQuMobile) {\r\n                    window.J" +
"inQuMobile.validateFormEnd(JSON.stringify(formData));\r\n                }\r\n      " +
"      }\r\n        }\r\n\r\n        /*------------------------------------------------" +
"----------------------------------------------*/\r\n        //不走验证流程\r\n        func" +
"tion noValidateFormBegin() {\r\n            var formData = DomUtil.serialize(docum" +
"ent.getElementById(\'IsoGCDZKCTJDJZForm\'));//json or =&\r\n            //告诉移动端页面验证完" +
"毕\r\n            if (window.JinQuMobile) {\r\n                window.JinQuMobile.val" +
"idateFormEnd(JSON.stringify(formData));\r\n            }\r\n        }\r\n\r\n    </scrip" +
"t>\r\n");

});

        }
    }
}
#pragma warning restore 1591
