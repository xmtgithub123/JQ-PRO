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
    
    #line 5 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoBCRWTZDMobile/info.cshtml")]
    public partial class _Views_IsoBCRWTZDMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoBCRWTZD>
    {
        public _Views_IsoBCRWTZDMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
  
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

WriteLiteral(" id=\"IsoBCRWTZDForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n               表 <label>");

            
            #line 117 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                   Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2408), Tuple.Create("\"", 2434)
            
            #line 117 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
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

WriteLiteral("  validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2703), Tuple.Create("\"", 2724)
            
            #line 118 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                                                                                                                                                                               , Tuple.Create(Tuple.Create("", 2711), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2711), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2918), Tuple.Create("\"", 2935)
            
            #line 122 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2926), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 2926), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3027), Tuple.Create("\"", 3052)
            
            #line 123 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 3035), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 3035), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3339), Tuple.Create("\"", 3362)
            
            #line 128 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3347), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 3347), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 129 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                        Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;阶段</label>\r\n                <input");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3660), Tuple.Create("\"", 3688)
            
            #line 133 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 3668), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 3668), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;主体设计部门</label>\r\n                <input");

WriteLiteral(" name=\"ProjDeptName\"");

WriteLiteral(" id=\"ProjDeptName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3920), Tuple.Create("\"", 3949)
            
            #line 137 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 3928), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjDeptName
            
            #line default
            #line hidden
, 3928), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目类型</label>\r\n                <input");

WriteLiteral(" name=\"ColAttType1Name\"");

WriteLiteral(" id=\"ColAttType1Name\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4185), Tuple.Create("\"", 4217)
            
            #line 141 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                        , Tuple.Create(Tuple.Create("", 4193), Tuple.Create<System.Object, System.Int32>(ViewBag.ColAttType1Name
            
            #line default
            #line hidden
, 4193), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;工程地点</label>\r\n                <input");

WriteLiteral(" name=\"ProjAreaName\"");

WriteLiteral(" id=\"ProjAreaName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4447), Tuple.Create("\"", 4476)
            
            #line 145 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 4455), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjAreaName
            
            #line default
            #line hidden
, 4455), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;管理等级</label>\r\n                <input");

WriteLiteral(" name=\"ManageClass\"");

WriteLiteral(" id=\"ManageClass\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4704), Tuple.Create("\"", 4730)
            
            #line 149 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 4712), Tuple.Create<System.Object, System.Int32>(Model.ManageClass
            
            #line default
            #line hidden
, 4712), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;客户单位</label>\r\n                <input");

WriteLiteral(" name=\"CompanyName\"");

WriteLiteral(" id=\"CompanyName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4958), Tuple.Create("\"", 4986)
            
            #line 153 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 4966), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyName
            
            #line default
            #line hidden
, 4966), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;联系人</label>\r\n                <input");

WriteLiteral(" name=\"CompanyLinkMan\"");

WriteLiteral(" id=\"CompanyLinkMan\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5219), Tuple.Create("\"", 5250)
            
            #line 157 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                      , Tuple.Create(Tuple.Create("", 5227), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyLinkMan
            
            #line default
            #line hidden
, 5227), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;电  话</label>\r\n                <input");

WriteLiteral(" name=\"CompanyTel\"");

WriteLiteral(" id=\"CompanyTel\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5476), Tuple.Create("\"", 5503)
            
            #line 161 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 5484), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyTel
            
            #line default
            #line hidden
, 5484), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;任务创建依据</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"TaskPursuant\"");

WriteLiteral(" name=\"TaskPursuant\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5737), Tuple.Create("\"", 5764)
            
            #line 165 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 5745), Tuple.Create<System.Object, System.Int32>(Model.TaskPursuant
            
            #line default
            #line hidden
, 5745), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(" id=\"TaskPursuantNames\"");

WriteLiteral("></div>\r\n");

WriteLiteral("                ");

            
            #line 167 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
           Write(BaseData.getBaseSelect(new Params()
               {
                   controlName = "TaskPursuant_cn",
                   engName = "TaskPursuant",
                   width = "98%;",
                   ids = Model.TaskPursuant.ToString(),
                   isRequired = true
               }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;任务内容</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"TaskContents1\"");

WriteLiteral(" id=\"TaskContents1\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 179 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                                                                                                                                                   Write(Model.TaskContents1);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;创建任务时<br />&nbsp;&nbsp;可以明确的<br />&nbsp;&nbsp;分包或合作<br />&nbsp;&nbsp" +
";设计单位及<br />&nbsp;&nbsp;设计内容</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"TaskContents2\"");

WriteLiteral(" id=\"TaskContents2\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 183 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                                                                                                                                                   Write(Model.TaskContents2);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;顾客对项目<br />&nbsp;&nbsp;进度的要求</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"TaskContents3\"");

WriteLiteral(" id=\"TaskContents3\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 187 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                                                                                                                                                   Write(Model.TaskContents3);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;备注</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"Contents\"");

WriteLiteral(" id=\"Contents\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 191 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
                                                                                                                                                         Write(Model.Contents);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projId\"");

WriteLiteral(" name=\"projId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7688), Tuple.Create("\"", 7709)
            
            #line 194 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 7696), Tuple.Create<System.Object, System.Int32>(Model.ProjID
            
            #line default
            #line hidden
, 7696), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projPhaseId\"");

WriteLiteral(" name=\"projPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7783), Tuple.Create("\"", 7809)
            
            #line 195 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 7791), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 7791), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"desTaskGroupId\"");

WriteLiteral(" name=\"desTaskGroupId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7889), Tuple.Create("\"", 7927)
            
            #line 196 "..\..\Views\IsoBCRWTZDMobile\info.cshtml"
  , Tuple.Create(Tuple.Create("", 7897), Tuple.Create<System.Object, System.Int32>(Request.Params["taskGroupId"]
            
            #line default
            #line hidden
, 7897), false)
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

WriteLiteral(" id=\"IsMultiSelect\"");

WriteLiteral(" name=\"IsMultiSelect\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" />\r\n\r\n\r\n            ");

WriteLiteral("\r\n        </form>\r\n    </div>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        $(function () {\r\n            if (window.JinQuMobile == undefined) {\r" +
"\n                initFormBegin(JSON.stringify({\r\n                    \"Result\": t" +
"rue,\r\n                    \"Message\": null,\r\n                    \"NodeName\": null" +
",\r\n                    \"AllowEditControls\": \"ColAttVal1;btnChooseProject;ProjIDN" +
"ame;Number;ProjID;Words;ConstructUnit;SupplyUnit;FormDate;ColAttVal2;ModifyConte" +
"nt;DesignName;FormNote;DesChangeUpload;ColAttVal3;ProjName;DesignReason;ColAttVa" +
"l4\",\r\n                    \"SignControls\": \"DesignName(签名)\",\r\n                   " +
" \"SignControls\": null,\r\n                    //\"AllowEditControls\":\"\"\r\n\r\n        " +
"        }));\r\n                //initFormBegin();\r\n            }\r\n\r\n        })\r\n " +
"       //页面初始化\r\n        function initFormBegin(params) {\r\n\r\n            //设置多选显示" +
"名称问题\r\n            $(\'#TaskPursuant_cn\').css(\'display\', \'none\')\r\n            var " +
"TaskPursuantArr = $(\'#TaskPursuant\').val().split(\',\')\r\n            var TaskPursu" +
"antArrString = [];\r\n            for (var i = 0; i < TaskPursuantArr.length; i++)" +
" {\r\n                // console.log(depArr[i]);\r\n                $(\'#TaskPursuant" +
"_cn\').val(TaskPursuantArr[i]);\r\n                TaskPursuantArrString.push($(\'#T" +
"askPursuant_cn option:selected\').text())\r\n            }\r\n            $(\'#TaskPur" +
"suantNames\').text(TaskPursuantArrString.toString())\r\n\r\n            \r\n           " +
" //去除换行\r\n            //alert(window.location.href)\r\n            params = params." +
"replace(/[\\r\\n]/g, \"\");\r\n            params = params.replace(/\\s/g, \"\");\r\n      " +
"      var paramsObj = JSON.parse(params);\r\n\r\n\r\n            //按钮替换\r\n            p" +
"aramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n\r\n           \r" +
"\n             //设置表单不可编辑样式及只读\r\n            $(\'form :input\').addClass(\'jq-text-di" +
"sabled\').attr(\"readonly\", \"readonly\");\r\n            DomUtil.setCtrlsReadonly(par" +
"amsObj.AllowEditControls, document.getElementById(\'IsoBCRWTZDForm\'));\r\n\r\n       " +
"     //任务内容，替换换行符\r\n            var TaskContents1Value = $(\'#TaskContents1\').val(" +
");\r\n            TaskContents1Value = TaskContents1Value.replace(/[\\r\\n]/g, \"\").r" +
"eplace(/\\s/g, \"\")\r\n            $(\'#TaskContents1\').val(TaskContents1Value);\r\n\r\n " +
"           //设计单位及设计内容，替换换行符\r\n            var TaskContents2Value = $(\'#TaskConte" +
"nts2\').val();\r\n            TaskContents2Value = TaskContents2Value.replace(/[\\r\\" +
"n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#TaskContents2\').val(TaskContents2V" +
"alue);\r\n\r\n            //顾客对项目进度的要求，替换换行符\r\n            var TaskContents3Value = $" +
"(\'#TaskContents3\').val();\r\n            TaskContents3Value = TaskContents3Value.r" +
"eplace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#TaskContents3\').val(Ta" +
"skContents3Value);\r\n\r\n            //备注，替换换行符\r\n            var ContentsValue = $(" +
"\'#Contents\').val();\r\n            ContentsValue = ContentsValue.replace(/[\\r\\n]/g" +
", \"\").replace(/\\s/g, \"\")\r\n            $(\'#Contents\').val(ContentsValue);\r\n\r\n    " +
"        $(\'#TaskContents1,#TaskContents2,#TaskContents3,#Contents\').removeClass(" +
"\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n            $(\"#TaskContents1," +
"#TaskContents2,#TaskContents3,#Contents\").css({\r\n                overflow: \'auto" +
"\',\r\n                background: \'#efefef\',\r\n                color: \'#898787\'\r\n  " +
"          })\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile) {\r" +
"\n                window.JinQuMobile.FormEnd();\r\n            }\r\n        }\r\n\r\n    " +
"    /*--------------------------------------------------------------------------" +
"--------------------*/\r\n        //表单验证交互\r\n\r\n        function validateFormBegin()" +
" {\r\n            var formVali = $(\'#IsoBCRWTZDForm\').validate({\r\n                " +
"//rules: {\r\n                //    Number:\'required\',//编号\r\n                //    " +
"ProjectName: \'required\',//项目名称\r\n                //    ColAttVal2:\'required\',//变更" +
"原因\r\n                //    ModifyContent: { //修改内容\r\n                //        req" +
"uired: false,\r\n                //        maxlength: 500\r\n                //    }" +
",\r\n                //    FormNote: { // 监理单位意见\r\n                //        requir" +
"ed: false,\r\n                //        maxlength: 500\r\n                //    }\r\n " +
"               //},\r\n                //messages: {\r\n                //    Number" +
":\'请输入编号\',\r\n                //    ProjectName: \'请输入项目名称 \',\r\n                //   " +
" ColAttVal2:\'请输入变更原因\',\r\n                //    ModifyContent: {\r\n                " +
"//        maxlength: \'内容长度必须介于0-500之间\'\r\n                //    },\r\n              " +
"  //    FormNote: {\r\n                //        maxlength: \'内容长度必须介于0-500之间\'\r\n   " +
"             //    }\r\n                //}\r\n            });\r\n            //if ($(" +
"\'#ProjName\').val() == \'\') {\r\n            //    $.alert(\'请选择项目！\')\r\n            //" +
"    return false;\r\n            //}\r\n            isValidate = formVali.form();\r\n " +
"           if (isValidate) {\r\n                var formData = DomUtil.serialize(d" +
"ocument.getElementById(\'IsoBCRWTZDForm\'));//json or =&\r\n                console." +
"log(formData)\r\n                //告诉移动端页面初始完完毕\r\n                if (window.JinQuM" +
"obile) {\r\n                    window.JinQuMobile.validateFormEnd(JSON.stringify(" +
"formData));\r\n                }\r\n            }\r\n        }\r\n\r\n        /*----------" +
"--------------------------------------------------------------------------------" +
"----*/\r\n        //不走验证流程\r\n        function noValidateFormBegin() {\r\n            " +
"var formData = DomUtil.serialize(document.getElementById(\'IsoBCRWTZDForm\'));//js" +
"on or =&\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {\r\n    " +
"            window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n     " +
"       }\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
