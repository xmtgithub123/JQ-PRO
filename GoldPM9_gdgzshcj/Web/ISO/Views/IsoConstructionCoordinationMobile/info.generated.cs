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
    
    #line 5 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoConstructionCoordinationMobile/info.cshtml")]
    public partial class _Views_IsoConstructionCoordinationMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoConstructionCoordination>
    {
        public _Views_IsoConstructionCoordinationMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
  
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

WriteLiteral(" id=\"IsoConstructionCoordinationForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n               表 <label>");

            
            #line 117 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                   Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2442), Tuple.Create("\"", 2468)
            
            #line 117 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                 , Tuple.Create(Tuple.Create("", 2450), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 2450), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 2757), Tuple.Create("\"", 2778)
            
            #line 118 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                                                                                                                                                                                                   , Tuple.Create(Tuple.Create("", 2765), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2765), false)
);

WriteLiteral(" />\r\n            </div>\r\n           \r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjId\"");

WriteLiteral(" name=\"ProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2993), Tuple.Create("\"", 3014)
            
            #line 123 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3001), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 3001), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3074), Tuple.Create("\"", 3091)
            
            #line 124 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3082), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 3082), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3181), Tuple.Create("\"", 3204)
            
            #line 125 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3189), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 3189), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 126 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                        Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;配合日期</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Date\"");

WriteLiteral(" id=\"Date\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"CoordinationDate\"");

WriteLiteral(" name=\"CoordinationDate\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3573), Tuple.Create("\"", 3629)
            
            #line 131 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                          , Tuple.Create(Tuple.Create("", 3581), Tuple.Create<System.Object, System.Int32>(ViewBag.CoordinationDate.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 3581), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3855), Tuple.Create("\"", 3880)
            
            #line 135 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 3863), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 3863), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseId\"");

WriteLiteral(" id=\"ProjPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4019), Tuple.Create("\"", 4045)
            
            #line 136 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4027), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 4027), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4127), Tuple.Create("\"", 4155)
            
            #line 137 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
    , Tuple.Create(Tuple.Create("", 4135), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 4135), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;专业室</label>\r\n                <input");

WriteLiteral(" name=\"Special\"");

WriteLiteral(" id=\"Special\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Specialty_i\"");

WriteLiteral(" name=\"Specialty_i\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4453), Tuple.Create("\"", 4477)
            
            #line 142 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 4461), Tuple.Create<System.Object, System.Int32>(Model.Specialty
            
            #line default
            #line hidden
, 4461), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("                ");

            
            #line 143 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
           Write(BaseData.getBaseSelect(new Params()
               {
                   isMult = false,
                   controlName = "Specialty",
                   isRequired = true,
                   engName = "Special",
                   width = "98%",
                   ids = Model.Specialty.ToString()
               }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;天气</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"Wather\"");

WriteLiteral(" name=\"Wather\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5035), Tuple.Create("\"", 5056)
            
            #line 155 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
      , Tuple.Create(Tuple.Create("", 5043), Tuple.Create<System.Object, System.Int32>(Model.Wather
            
            #line default
            #line hidden
, 5043), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;施工单位</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ConstructCompany\"");

WriteLiteral(" name=\"ConstructCompany\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5296), Tuple.Create("\"", 5327)
            
            #line 159 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                            , Tuple.Create(Tuple.Create("", 5304), Tuple.Create<System.Object, System.Int32>(Model.ConstructCompany
            
            #line default
            #line hidden
, 5304), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 160 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                        Write(Model.ConstructCompany);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;配合人员</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Person\"");

WriteLiteral(" id=\"Person\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5623), Tuple.Create("\"", 5653)
            
            #line 164 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 5631), Tuple.Create<System.Object, System.Int32>(Model.CoordinationIds
            
            #line default
            #line hidden
, 5631), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 165 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                        Write(Model.CoordinationIds);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row aui-input-check-disable\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;配合类型</label>\r\n                <p>施工交底&nbsp;&nbsp;<input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check1\"");

WriteLiteral(" />&nbsp;&nbsp;施工配合&nbsp;&nbsp;<input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check2\"");

WriteLiteral(" /></p>\r\n                <p></p>\r\n                <p>施工例会&nbsp;&nbsp;<input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check3\"");

WriteLiteral(" />&nbsp;&nbsp;工程验收&nbsp;&nbsp;<input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check4\"");

WriteLiteral(" /></p>\r\n                <p></p>\r\n                <p>其他&nbsp;&nbsp;<input");

WriteLiteral(" type=\"radio\"");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check5\"");

WriteLiteral(" /></p>\r\n\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"checkVal\"");

WriteLiteral(" name=\"checkVal\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6422), Tuple.Create("\"", 6453)
            
            #line 175 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 6430), Tuple.Create<System.Object, System.Int32>(Model.CoordinationType
            
            #line default
            #line hidden
, 6430), false)
);

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"checkOutput\"");

WriteLiteral(" name=\"checkOutput\"");

WriteLiteral(">\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;配合内容</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"CoordinationValue\"");

WriteLiteral(" id=\"CoordinationValue\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 180 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                                                                                                                                                           Write(Model.CoordinationValue);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设计变更</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"DesignChange\"");

WriteLiteral(" id=\"DesignChange\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 184 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                                                                                                                                                 Write(Model.DesignChange);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;问题反馈</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"Problem\"");

WriteLiteral(" id=\"Problem\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 188 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                                                                                                                                       Write(Model.Problem);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;附件</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"Attachment\"");

WriteLiteral(" id=\"Attachment\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 192 "..\..\Views\IsoConstructionCoordinationMobile\info.cshtml"
                                                                                                                                                             Write(Model.Attachment);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n\r\n\r\n\r\n            ");

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
"       //页面初始化\r\n        function initFormBegin(params) {\r\n\r\n            $(\'#Spec" +
"ialty\').val($(\'#Specialty_i\').val())\r\n            SetCheck();\r\n            //去除换" +
"行\r\n            \r\n            params = params.replace(/[\\r\\n]/g, \"\");\r\n          " +
"  params = params.replace(/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(pa" +
"rams);\r\n\r\n\r\n            //按钮替换\r\n            paramsObj.AllowEditControls = params" +
"Obj.AllowEditControls || \'-\';\r\n\r\n            //清除时间审批时，默认为1900-01-01改为空\r\n       " +
"     var CoordinationDate = $(\'#CoordinationDate\').val();;\r\n            if ((Coo" +
"rdinationDate == \'1900-01-01\')) {\r\n                $(\'#CoordinationDate\').val(\'\'" +
");\r\n            }\r\n\r\n             //设置表单不可编辑样式及只读\r\n            $(\'form :input\')." +
"addClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\");\r\n            DomUtil." +
"setCtrlsReadonly(paramsObj.AllowEditControls, document.getElementById(\'IsoConstr" +
"uctionCoordinationForm\'));\r\n\r\n            //配合内容，替换换行符\r\n            var Coordina" +
"tionValueValue = $(\'#CoordinationValue\').val();\r\n            CoordinationValueVa" +
"lue = CoordinationValueValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n        " +
"    $(\'#CoordinationValue\').val(CoordinationValueValue);\r\n\r\n            //设计变更，替" +
"换换行符\r\n            var DesignChangeValue = $(\'#DesignChange\').val();\r\n           " +
" DesignChangeValue = DesignChangeValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")" +
"\r\n            $(\'#DesignChange\').val(DesignChangeValue);\r\n\r\n            //问题反馈，替" +
"换换行符\r\n            var ProblemValue = $(\'#Problem\').val();\r\n            ProblemVa" +
"lue = ProblemValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#Pr" +
"oblem\').val(ProblemValue);\r\n\r\n            //附件，替换换行符\r\n            var Attachment" +
"Value = $(\'#Attachment\').val();\r\n            AttachmentValue = AttachmentValue.r" +
"eplace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#Attachment\').val(Attac" +
"hmentValue);\r\n            \r\n            $(\"#CoordinationValue,#DesignChange,#Pro" +
"blem,#Attachment\").removeClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r" +
"\n            $(\"#CoordinationValue,#DesignChange,#Problem,#Attachment\").css({\r\n " +
"               overflow: \'auto\',\r\n                background: \'#efefef\',\r\n      " +
"          color: \'#898787\'\r\n            })\r\n            $(\'input[type=\"checkbox\"" +
"]\').attr(\"disabled\", \"disabled\");\r\n            $(\'input[type=\"radio\"]\').attr(\"di" +
"sabled\", \"disabled\");\r\n\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQ" +
"uMobile) {\r\n                window.JinQuMobile.FormEnd();\r\n            }\r\n      " +
"  }\r\n\r\n        function SetCheck() {\r\n            var checkIndex = $(\'#checkVal\'" +
").val()\r\n            switch (checkIndex) {\r\n                case \"施工交底\": $(\'#che" +
"ck1\').attr(\'checked\', \'checked\');\r\n                    $(\'#checkOutput\').val(\'施工" +
"交底☑  施工配合□  施工例会□ 工程验收□ 其他□\');\r\n                    break;\r\n                case" +
" \"施工配合\": $(\'#check2\').attr(\'checked\', \'checked\');\r\n                    $(\'#check" +
"Output\').val(\'施工交底□  施工配合☑  施工例会□ 工程验收□ 其他□\');\r\n                    break;\r\n    " +
"            case \"施工例会\": $(\'#check3\').attr(\'checked\', \'checked\');\r\n             " +
"       $(\'#checkOutput\').val(\'施工交底□  施工配合□  施工例会☑ 工程验收□ 其他□\');\r\n                " +
"    break;\r\n                case \"工程验收\": $(\'#check4\').attr(\'checked\', \'checked\')" +
";\r\n                    $(\'#checkOutput\').val(\'施工交底□  施工配合□  施工例会□ 工程验收☑ 其他□\');\r\n" +
"                    break;\r\n                case \"其他\": $(\'#check5\').attr(\'checke" +
"d\', \'checked\');\r\n                    $(\'#checkOutput\').val(\'施工交底□  施工配合□  施工例会□ " +
"工程验收□ 其他☑\');\r\n                    break;\r\n                default:\r\n            " +
"        $(\'#checkOutput\').val(\'施工交底□  施工配合□  施工例会□ 工程验收□ 其他□\');\r\n            }\r\n" +
"        }\r\n        /*-----------------------------------------------------------" +
"-----------------------------------*/\r\n        //表单验证交互\r\n\r\n        function vali" +
"dateFormBegin() {\r\n            var formVali = $(\'#IsoConstructionCoordinationFor" +
"m\').validate({\r\n                //rules: {\r\n                //    Number:\'requir" +
"ed\',//编号\r\n                //    ProjectName: \'required\',//项目名称\r\n                " +
"//    ColAttVal2:\'required\',//变更原因\r\n                //    ModifyContent: { //修改内" +
"容\r\n                //        required: false,\r\n                //        maxleng" +
"th: 500\r\n                //    },\r\n                //    FormNote: { // 监理单位意见\r\n" +
"                //        required: false,\r\n                //        maxlength:" +
" 500\r\n                //    }\r\n                //},\r\n                //messages:" +
" {\r\n                //    Number:\'请输入编号\',\r\n                //    ProjectName: \'请" +
"输入项目名称 \',\r\n                //    ColAttVal2:\'请输入变更原因\',\r\n                //    Mo" +
"difyContent: {\r\n                //        maxlength: \'内容长度必须介于0-500之间\'\r\n        " +
"        //    },\r\n                //    FormNote: {\r\n                //        m" +
"axlength: \'内容长度必须介于0-500之间\'\r\n                //    }\r\n                //}\r\n     " +
"       });\r\n            //if ($(\'#ProjName\').val() == \'\') {\r\n            //    $" +
".alert(\'请选择项目！\')\r\n            //    return false;\r\n            //}\r\n            " +
"isValidate = formVali.form();\r\n            if (isValidate) {\r\n                va" +
"r formData = DomUtil.serialize(document.getElementById(\'IsoConstructionCoordinat" +
"ionForm\'));//json or =&\r\n                console.log(formData)\r\n                " +
"//告诉移动端页面初始完完毕\r\n                if (window.JinQuMobile) {\r\n                    w" +
"indow.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n                }\r" +
"\n            }\r\n        }\r\n\r\n        /*-----------------------------------------" +
"-----------------------------------------------------*/\r\n        //不走验证流程\r\n     " +
"   function noValidateFormBegin() {\r\n            var formData = DomUtil.serializ" +
"e(document.getElementById(\'IsoConstructionCoordinationForm\'));//json or =&\r\n    " +
"        //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {\r\n                wi" +
"ndow.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n            }\r\n    " +
"    }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
