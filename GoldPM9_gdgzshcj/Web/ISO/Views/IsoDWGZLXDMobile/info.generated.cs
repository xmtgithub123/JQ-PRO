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
    
    #line 5 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoDWGZLXDMobile/info.cshtml")]
    public partial class _Views_IsoDWGZLXDMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoDWGZLXD>
    {
        public _Views_IsoDWGZLXDMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
  
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

WriteLiteral(" id=\"IsoDWGZLXDForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n               表 <label>");

            
            #line 117 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                   Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2408), Tuple.Create("\"", 2434)
            
            #line 117 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                 , Tuple.Create(Tuple.Create("", 2416), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 2416), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 2723), Tuple.Create("\"", 2744)
            
            #line 118 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                                                                                                                                                                                                   , Tuple.Create(Tuple.Create("", 2731), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2731), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2938), Tuple.Create("\"", 2955)
            
            #line 122 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2946), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 2946), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3049), Tuple.Create("\"", 3074)
            
            #line 123 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 3057), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 3057), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 124 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(Model.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3429), Tuple.Create("\"", 3452)
            
            #line 128 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3437), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 3437), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 129 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;阶段</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3752), Tuple.Create("\"", 3780)
            
            #line 133 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                      , Tuple.Create(Tuple.Create("", 3760), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 3760), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 134 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(Model.ProjPhaseName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;主题</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"ZhuTi\"");

WriteLiteral(" id=\"ZhuTi\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 138 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                                                                                                                                   Write(Model.ZhuTi);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;发文单位</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"CompanyName\"");

WriteLiteral(" name=\"CompanyName\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4390), Tuple.Create("\"", 4418)
            
            #line 142 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 4398), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyName
            
            #line default
            #line hidden
, 4398), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;电  话</label>\r\n                <input");

WriteLiteral(" type=\"number\"");

WriteLiteral(" id=\"CompanyTel\"");

WriteLiteral(" name=\"CompanyTel\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4646), Tuple.Create("\"", 4673)
            
            #line 146 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 4654), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyTel
            
            #line default
            #line hidden
, 4654), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;主送</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"ZhuSong\"");

WriteLiteral(" name=\"ZhuSong\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4891), Tuple.Create("\"", 4913)
            
            #line 150 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 4899), Tuple.Create<System.Object, System.Int32>(Model.ZhuSong
            
            #line default
            #line hidden
, 4899), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;拟稿人</label>\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 154 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(ViewBag.DrafterName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"DrafterName\"");

WriteLiteral(" name=\"DrafterName\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5215), Tuple.Create("\"", 5243)
            
            #line 155 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 5223), Tuple.Create<System.Object, System.Int32>(ViewBag.DrafterName
            
            #line default
            #line hidden
, 5223), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;抄送</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"ChaoSong\"");

WriteLiteral(" id=\"ChaoSong\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 159 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                                                                                                                                         Write(Model.ChaoSong);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目负责人</label>\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 163 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(ViewBag.ProjPhaseEmpName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjPhaseEmpName\"");

WriteLiteral(" name=\"ProjPhaseEmpName\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5880), Tuple.Create("\"", 5913)
            
            #line 164 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                            , Tuple.Create(Tuple.Create("", 5888), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjPhaseEmpName
            
            #line default
            #line hidden
, 5888), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;副项目负责人</label>\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 168 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                        Write(ViewBag.FProjEmpName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"FProjEmpName\"");

WriteLiteral(" name=\"FProjEmpName\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6221), Tuple.Create("\"", 6250)
            
            #line 169 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 6229), Tuple.Create<System.Object, System.Int32>(ViewBag.FProjEmpName
            
            #line default
            #line hidden
, 6229), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;签发时间</label>\r\n                <input");

WriteLiteral(" type=\"date\"");

WriteLiteral(" id=\"SignTime\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"SignTime\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6472), Tuple.Create("\"", 6518)
            
            #line 173 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 6480), Tuple.Create<System.Object, System.Int32>(Model.SignTime.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 6480), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;内容及附件名称</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"Contents\"");

WriteLiteral(" id=\"Contents\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 177 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                                                                                                                                                         Write(Model.Contents);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;接受单位</label>\r\n                ");

WriteLiteral("\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"AcceptUnit\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"AcceptUnit\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7141), Tuple.Create("\"", 7166)
            
            #line 182 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 7149), Tuple.Create<System.Object, System.Int32>(Model.AcceptUnit
            
            #line default
            #line hidden
, 7149), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;接收人</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"AcceptPerson\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"AcceptPerson\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7395), Tuple.Create("\"", 7422)
            
            #line 186 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 7403), Tuple.Create<System.Object, System.Int32>(Model.AcceptPerson
            
            #line default
            #line hidden
, 7403), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;接受时间</label>\r\n                <input");

WriteLiteral(" type=\"date\"");

WriteLiteral(" id=\"AcceptTime\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"AcceptTime\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7648), Tuple.Create("\"", 7696)
            
            #line 190 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 7656), Tuple.Create<System.Object, System.Int32>(Model.AcceptTime.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 7656), false)
);

WriteLiteral(" />\r\n            </div>\r\n\r\n\r\n\r\n\r\n\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"mainProjId\"");

WriteLiteral(" name=\"mainProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7798), Tuple.Create("\"", 7825)
            
            #line 197 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 7806), Tuple.Create<System.Object, System.Int32>(ViewBag.MainProjId
            
            #line default
            #line hidden
, 7806), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projId\"");

WriteLiteral(" name=\"projId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7889), Tuple.Create("\"", 7910)
            
            #line 198 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 7897), Tuple.Create<System.Object, System.Int32>(Model.ProjID
            
            #line default
            #line hidden
, 7897), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projPhaseId\"");

WriteLiteral(" name=\"projPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7984), Tuple.Create("\"", 8010)
            
            #line 199 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 7992), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 7992), false)
);

WriteLiteral(" />\r\n            ");

WriteLiteral("\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"desTaskGroupId\"");

WriteLiteral(" name=\"desTaskGroupId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8212), Tuple.Create("\"", 8243)
            
            #line 201 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
  , Tuple.Create(Tuple.Create("", 8220), Tuple.Create<System.Object, System.Int32>(ViewBag.desTaskGroupId
            
            #line default
            #line hidden
, 8220), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyID\"");

WriteLiteral(" name=\"CompanyID\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stepOrder\"");

WriteLiteral(" name=\"stepOrder\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjEmpId\"");

WriteLiteral(" name=\"ProjEmpId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8471), Tuple.Create("\"", 8497)
            
            #line 204 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 8479), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjEmpId
            
            #line default
            #line hidden
, 8479), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"FProjEmpId\"");

WriteLiteral(" name=\"FProjEmpId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8569), Tuple.Create("\"", 8596)
            
            #line 205 "..\..\Views\IsoDWGZLXDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 8577), Tuple.Create<System.Object, System.Int32>(ViewBag.FProjEmpId
            
            #line default
            #line hidden
, 8577), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsMultiSelect\"");

WriteLiteral(" name=\"IsMultiSelect\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" />\r\n            ");

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
"      \r\n            //去除换行\r\n            //alert(window.location.href)\r\n         " +
"   params = params.replace(/[\\r\\n]/g, \"\");\r\n            params = params.replace(" +
"/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n\r\n\r\n            //" +
"按钮替换\r\n            paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'" +
"-\';\r\n\r\n            //清除签发时间，默认为1900-01-01改为空\r\n            var SignTime = $(\'#Sig" +
"nTime\').val();;\r\n            if ((SignTime == \'1900-01-01\')) {\r\n                " +
"$(\'#SignTime\').val(\'\');\r\n            }\r\n            //清除接收时间，默认为1900-01-01改为空\r\n " +
"           var AcceptTime = $(\'#AcceptTime\').val();;\r\n            if ((AcceptTim" +
"e == \'1900-01-01\')) {\r\n                $(\'#AcceptTime\').val(\'\');\r\n            }\r" +
"\n            \r\n\r\n\r\n             //设置表单不可编辑样式及只读\r\n            $(\'form :input\').ad" +
"dClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\");\r\n            DomUtil.se" +
"tCtrlsReadonly(paramsObj.AllowEditControls, document.getElementById(\'IsoDWGZLXDF" +
"orm\'));\r\n\r\n            //主题，替换换行符\r\n            var ZhuTiValue = $(\'#ZhuTi\').val(" +
");\r\n            ZhuTiValue = ZhuTiValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\"" +
")\r\n            $(\'#ZhuTi\').val(ZhuTiValue);\r\n\r\n            //抄送，替换换行符\r\n         " +
"   var ChaoSongValue = $(\'#ChaoSong\').val();\r\n            ChaoSongValue = ChaoSo" +
"ngValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#ChaoSong\').va" +
"l(ChaoSongValue);\r\n\r\n            //内容及附件名称，替换换行符\r\n            var ContentsValue " +
"= $(\'#Contents\').val();\r\n            ContentsValue = ContentsValue.replace(/[\\r\\" +
"n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#Contents\').val(ContentsValue);\r\n\r\n" +
"\r\n            if (paramsObj.AllowEditControls == \"{}\") {\r\n\r\n                $(\"#" +
"ZhuTi,#ChaoSong,#Contents\").removeClass(\'jq-text-disabled\')\r\n                $(\"" +
"#ZhuTi,#ChaoSong,#Contents\").css({\r\n                    overflow: \'auto\',\r\n     " +
"           })\r\n            }\r\n            else {\r\n                $(\'#ZhuTi,#Cha" +
"oSong,#Contents\').removeClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n" +
"                $(\"#ZhuTi,#ChaoSong,#Contents\").css({\r\n                    overf" +
"low: \'auto\',\r\n                    background: \'#efefef\',\r\n                    co" +
"lor: \'#898787\'\r\n                })\r\n\r\n            }\r\n\r\n            \r\n           " +
" //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile) {\r\n                window.J" +
"inQuMobile.FormEnd();\r\n            }\r\n        }\r\n\r\n        \r\n        /*---------" +
"--------------------------------------------------------------------------------" +
"-----*/\r\n        //表单验证交互\r\n\r\n        function validateFormBegin() {\r\n           " +
" var formVali = $(\'#IsoDWGZLXDForm\').validate({\r\n                rules: {\r\n     " +
"               ProjectName: \'required\',//项目名称\r\n                    ColAttVal2:\'r" +
"equired\',//变更原因\r\n                    ModifyContent: { //修改内容\r\n                  " +
"      required: false,\r\n                        maxlength: 500\r\n                " +
"    },\r\n                    FormNote: { // 监理单位意见\r\n                        requi" +
"red: false,\r\n                        maxlength: 500\r\n                    }\r\n    " +
"            },\r\n                messages: {\r\n                    ProjectName: \'请" +
"输入项目名称 \',\r\n                    ColAttVal2:\'请输入变更原因\',\r\n                    Modify" +
"Content: {\r\n                        maxlength: \'内容长度必须介于0-500之间\'\r\n              " +
"      },\r\n                    FormNote: {\r\n                        maxlength: \'内" +
"容长度必须介于0-500之间\'\r\n                    }\r\n                }\r\n            });\r\n    " +
"        //if ($(\'#ProjName\').val() == \'\') {\r\n            //    $.alert(\'请选择项目！\')" +
"\r\n            //    return false;\r\n            //}\r\n            isValidate = for" +
"mVali.form();\r\n            if (isValidate) {\r\n                var formData = Dom" +
"Util.serialize(document.getElementById(\'IsoDWGZLXDForm\'));//json or =&\r\n        " +
"        console.log(formData)\r\n                //告诉移动端页面初始完完毕\r\n                i" +
"f (window.JinQuMobile) {\r\n                    window.JinQuMobile.validateFormEnd" +
"(JSON.stringify(formData));\r\n                }\r\n            }\r\n        }\r\n\r\n    " +
"    /*--------------------------------------------------------------------------" +
"--------------------*/\r\n        //不走验证流程\r\n        function noValidateFormBegin()" +
" {\r\n            var formData = DomUtil.serialize(document.getElementById(\'IsoDWG" +
"ZLXDForm\'));//json or =&\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQ" +
"uMobile) {\r\n                window.JinQuMobile.validateFormEnd(JSON.stringify(fo" +
"rmData));\r\n            }\r\n        }\r\n\r\n        function getProjInfo() {\r\n       " +
"     var projInfo = {\r\n                mainProjId: $(\'#mainProjId\').val(),\r\n    " +
"            projId: $(\'#projId\').val(),\r\n                projPhaseId: $(\'#projPh" +
"aseId\').val(),\r\n                desTaskGroupId: $(\'#desTaskGroupId\').val(),\r\n   " +
"             ProjEmpId: $(\'#ProjEmpId\').val(),\r\n                ProjPhaseEmpName" +
": $(\'#ProjPhaseEmpName\').val(),\r\n                FProjEmpId: $(\'#FProjEmpId\').va" +
"l(),\r\n                FProjEmpName: $(\'#FProjEmpName\').val()\r\n            };\r\n  " +
"          if (window.JinQuMobile) {\r\n                window.JinQuMobile.projInfo" +
"(JSON.stringify(projInfo));\r\n            }\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
