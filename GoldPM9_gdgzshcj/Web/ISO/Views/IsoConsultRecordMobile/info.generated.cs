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
    
    #line 5 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoConsultRecordMobile/info.cshtml")]
    public partial class _Views_IsoConsultRecordMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoConsultRecord>
    {
        public _Views_IsoConsultRecordMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
  
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

WriteLiteral(" id=\"IsoConsultRecordForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n               表 <label>");

            
            #line 117 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                   Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2420), Tuple.Create("\"", 2446)
            
            #line 117 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                 , Tuple.Create(Tuple.Create("", 2428), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 2428), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 2735), Tuple.Create("\"", 2756)
            
            #line 118 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                                                                                                                                                                                                   , Tuple.Create(Tuple.Create("", 2743), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2743), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2950), Tuple.Create("\"", 2967)
            
            #line 122 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2958), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 2958), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3059), Tuple.Create("\"", 3084)
            
            #line 123 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 3067), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 3067), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseId\"");

WriteLiteral(" id=\"ProjPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3223), Tuple.Create("\"", 3249)
            
            #line 124 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3231), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 3231), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3331), Tuple.Create("\"", 3359)
            
            #line 125 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
    , Tuple.Create(Tuple.Create("", 3339), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 3339), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjId\"");

WriteLiteral(" name=\"ProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3561), Tuple.Create("\"", 3582)
            
            #line 129 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3569), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 3569), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3672), Tuple.Create("\"", 3695)
            
            #line 130 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3680), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 3680), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 131 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                        Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;洽商事项</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"Consultation\"");

WriteLiteral(" id=\"Consultation\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 135 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                                                                                                                                                 Write(Model.Consultation);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;建设单位</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"BuildCompany\"");

WriteLiteral(" name=\"BuildCompany\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4327), Tuple.Create("\"", 4354)
            
            #line 139 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 4335), Tuple.Create<System.Object, System.Int32>(Model.BuildCompany
            
            #line default
            #line hidden
, 4335), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 140 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                        Write(Model.BuildCompany);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;监理单位</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"SupervisonCompany\"");

WriteLiteral(" name=\"SupervisonCompany\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4668), Tuple.Create("\"", 4700)
            
            #line 144 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 4676), Tuple.Create<System.Object, System.Int32>(Model.SupervisonCompany
            
            #line default
            #line hidden
, 4676), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 145 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                        Write(Model.SupervisonCompany);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;施工单位</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ConstructionCompany\"");

WriteLiteral(" name=\"ConstructionCompany\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5023), Tuple.Create("\"", 5057)
            
            #line 149 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                  , Tuple.Create(Tuple.Create("", 5031), Tuple.Create<System.Object, System.Int32>(Model.ConstructionCompany
            
            #line default
            #line hidden
, 5031), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(">");

            
            #line 150 "..\..\Views\IsoConsultRecordMobile\info.cshtml"
                                        Write(Model.ConstructionCompany);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </div>\r\n\r\n\r\n\r\n            ");

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
"       //页面初始化\r\n        function initFormBegin(params) {\r\n\r\n            //去除换行\r\n" +
"            //alert(window.location.href)\r\n            params = params.replace(/" +
"[\\r\\n]/g, \"\");\r\n            params = params.replace(/\\s/g, \"\");\r\n            var" +
" paramsObj = JSON.parse(params);\r\n\r\n            //按钮替换\r\n            paramsObj.Al" +
"lowEditControls = paramsObj.AllowEditControls || \'-\';\r\n\r\n            //设置表单不可编辑样" +
"式及只读\r\n            $(\'form :input\').addClass(\'jq-text-disabled\').attr(\"readonly\"," +
" \"readonly\");\r\n            DomUtil.setCtrlsReadonly(paramsObj.AllowEditControls," +
" document.getElementById(\'IsoConsultRecordForm\'));\r\n\r\n            //洽商事项，替换换行符\r\n" +
"            var ConsultationValue = $(\'#Consultation\').val();\r\n            Consu" +
"ltationValue = ConsultationValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n    " +
"        $(\'#Consultation\').val(ConsultationValue);\r\n\r\n            $(\'#Consultati" +
"on\').removeClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n            $" +
"(\"#Consultation\").css({\r\n                overflow: \'auto\',\r\n                back" +
"ground: \'#efefef\',\r\n                color: \'#898787\'\r\n            })\r\n\r\n        " +
"    $(\'input[type=\"checkbox\"]\').attr(\"disabled\", \"disabled\");\r\n            $(\'in" +
"put[type=\"radio\"]\').attr(\"disabled\", \"disabled\");\r\n            //告诉移动端页面初始完完毕\r\n " +
"           if (window.JinQuMobile) {\r\n                window.JinQuMobile.FormEnd" +
"();\r\n            }\r\n        }\r\n\r\n        \r\n        /*---------------------------" +
"-------------------------------------------------------------------*/\r\n        /" +
"/表单验证交互\r\n\r\n        function validateFormBegin() {\r\n            var formVali = $(" +
"\'#IsoConsultRecordForm\').validate({\r\n                //rules: {\r\n               " +
" //    Number:\'required\',//编号\r\n                //    ProjectName: \'required\',//项" +
"目名称\r\n                //    ColAttVal2:\'required\',//变更原因\r\n                //    M" +
"odifyContent: { //修改内容\r\n                //        required: false,\r\n            " +
"    //        maxlength: 500\r\n                //    },\r\n                //    Fo" +
"rmNote: { // 监理单位意见\r\n                //        required: false,\r\n               " +
" //        maxlength: 500\r\n                //    }\r\n                //},\r\n      " +
"          //messages: {\r\n                //    Number:\'请输入编号\',\r\n                " +
"//    ProjectName: \'请输入项目名称 \',\r\n                //    ColAttVal2:\'请输入变更原因\',\r\n   " +
"             //    ModifyContent: {\r\n                //        maxlength: \'内容长度必" +
"须介于0-500之间\'\r\n                //    },\r\n                //    FormNote: {\r\n      " +
"          //        maxlength: \'内容长度必须介于0-500之间\'\r\n                //    }\r\n     " +
"           //}\r\n            });\r\n            isValidate = formVali.form();\r\n\r\n  " +
"          if (isValidate) {\r\n                var formData = DomUtil.serialize(do" +
"cument.getElementById(\'IsoConsultRecordForm\'));//json or =&\r\n                con" +
"sole.log(formData)\r\n                //告诉移动端页面初始完完毕\r\n                if (window.J" +
"inQuMobile) {\r\n                    window.JinQuMobile.validateFormEnd(JSON.strin" +
"gify(formData));\r\n                }\r\n            }\r\n        }\r\n\r\n        /*-----" +
"--------------------------------------------------------------------------------" +
"---------*/\r\n        //不走验证流程\r\n        function noValidateFormBegin() {\r\n       " +
"     var formData = DomUtil.serialize(document.getElementById(\'IsoConsultRecordF" +
"orm\'));//json or =&\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobi" +
"le) {\r\n                window.JinQuMobile.validateFormEnd(JSON.stringify(formDat" +
"a));\r\n            }\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
