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
    
    #line 6 "..\..\Views\SSManageMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/SSManageMobile/info.cshtml")]
    public partial class _Views_SSManageMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.SSManage>
    {
        public _Views_SSManageMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\SSManageMobile\info.cshtml"
  
    Layout = "~/Areas/Core/Views/Mobile/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    ");

WriteLiteral("\r\n\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(@">
        .aui-btn {
            padding: 10px 15px;
            font-size: 13px;
        }

        .aui-btn-row:after {
            border: 0;
        }

        .set-disabled {
            pointer-events: none !important;
        }

        .error {
            color: red;
            font-size: 14px;
        }

        .hidden {
            display: none;
        }

        .aui-input-hook {
            line-height: 34px;
            text-align: left;
            padding-left: 20px;
            background: #efefef;
        }

        .aui-display-hook {
            display: none;
            width: 30%;
            float: right;
            font-size: 13px;
            line-height: 33px;
            margin-right: 6px;
            padding: 3px 6px;
        }

        .aui-input-width {
            width: 98%;
        }

        .table-select-show tr td {
            padding: 5px !important;
            font-size: small;
            text-align: center;
        }

        .table-select-show tr td input {
            padding: 5px;
            width: 60px;
            margin-bottom: 0;
        }

        .jq-text-disabled {
            color: #898787;
            width: 98%;
            pointer-events: none;
            background-color: #efefef !important;
            border: 0;
            font-size: 13px;
        }
        #dialog .checkbox {
            margin: 0;
        }
    </style>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"aui-content\"");

WriteLiteral(" style=\"overflow:hidden\"");

WriteLiteral(">\r\n        <form");

WriteLiteral(" id=\"SSManageForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1905), Tuple.Create("\"", 1922)
            
            #line 84 "..\..\Views\SSManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 1913), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 1913), false)
);

WriteLiteral(" />\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">项目名称</label>\r\n                \r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"empid\"");

WriteLiteral(" name=\"empid\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2159), Tuple.Create("\"", 2198)
            
            #line 90 "..\..\Views\SSManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2167), Tuple.Create<System.Object, System.Int32>(Request.QueryString["empid"]
            
            #line default
            #line hidden
, 2167), false)
);

WriteLiteral(" />\r\n\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EngineeringID\"");

WriteLiteral(" name=\"EngineeringID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2282), Tuple.Create("\"", 2312)
            
            #line 92 "..\..\Views\SSManageMobile\info.cshtml"
     , Tuple.Create(Tuple.Create("", 2290), Tuple.Create<System.Object, System.Int32>(Model.EngineeringID
            
            #line default
            #line hidden
, 2290), false)
);

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"table-responsive\"");

WriteLiteral(" style=\"overflow:hidden\"");

WriteLiteral(">\r\n                    <table");

WriteLiteral(" class=\"table table-select-show table-bordered\"");

WriteLiteral(" id=\"dgGrid\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral(">\r\n                        <thead>\r\n                            <tr>\r\n           " +
"                     <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">项目编号</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">项目名称</th>\r\n                              \r\n                            </tr>\r\n  " +
"                      </thead>\r\n                        <tbody");

WriteLiteral(" id=\"gridListShow\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral(">\r\n                            <tr>\r\n                                <td>");

            
            #line 104 "..\..\Views\SSManageMobile\info.cshtml"
                               Write(ViewBag.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                <td>");

            
            #line 105 "..\..\Views\SSManageMobile\info.cshtml"
                               Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                            </tr>\r\n                        </tbody>\r\n     " +
"               </table>\r\n                </div>\r\n\r\n                <script");

WriteLiteral(" id=\"listTp2\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(">\r\n                    {{~it:appendselectArrayInfoData:index}}\r\n                 " +
"   <tr>\r\n                        <td>\r\n                        \r\n               " +
"             ");

WriteLiteral("\r\n                            <span id=\"ProjNumber\" name=\"ProjNumber\">{{=appendse" +
"lectArrayInfoData.ProjNumber}}</span>\r\n                        </td>\r\n          " +
"              <td>\r\n                            ");

WriteLiteral(@"
                            <span id=""ProjName"" name=""ProjName"">{{=appendselectArrayInfoData.ProjName}}</span>
                        </td>
                        
                    </tr>
                    {{~}}
                </script>
            </div>

            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;是否制定实施方案</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"IsSSFA_i\"");

WriteLiteral("  id=\"IsSSFA_i\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4297), Tuple.Create("\"", 4318)
            
            #line 131 "..\..\Views\SSManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4305), Tuple.Create<System.Object, System.Int32>(Model.IsSSFA
            
            #line default
            #line hidden
, 4305), false)
);

WriteLiteral("/>\r\n                <select");

WriteLiteral(" id=\"IsSSFA\"");

WriteLiteral(" name=\"IsSSFA\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral("  style=\"width:98%;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;技术成本预算</label>\r\n                <input");

WriteLiteral(" name=\"TechNum\"");

WriteLiteral(" id=\"TechNum\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4758), Tuple.Create("\"", 4780)
            
            #line 140 "..\..\Views\SSManageMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 4766), Tuple.Create<System.Object, System.Int32>(Model.TechNum
            
            #line default
            #line hidden
, 4766), false)
);

WriteLiteral(" />\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;是否内审</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"IsNS_i\"");

WriteLiteral(" id=\"IsNS_i\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4984), Tuple.Create("\"", 5003)
            
            #line 145 "..\..\Views\SSManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4992), Tuple.Create<System.Object, System.Int32>(Model.IsNS
            
            #line default
            #line hidden
, 4992), false)
);

WriteLiteral(" />\r\n                <select");

WriteLiteral(" id=\"IsNS\"");

WriteLiteral(" name=\"IsNS\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;是否外审</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"IsWS_i\"");

WriteLiteral(" id=\"IsWS_i\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5417), Tuple.Create("\"", 5436)
            
            #line 153 "..\..\Views\SSManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 5425), Tuple.Create<System.Object, System.Int32>(Model.IsWS
            
            #line default
            #line hidden
, 5425), false)
);

WriteLiteral(" />\r\n                <select");

WriteLiteral(" id=\"IsWS\"");

WriteLiteral(" name=\"IsWS\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5524), Tuple.Create("\"", 5543)
            
            #line 154 "..\..\Views\SSManageMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 5532), Tuple.Create<System.Object, System.Int32>(Model.IsWS
            
            #line default
            #line hidden
, 5532), false)
);

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;地点</label>\r\n                <input");

WriteLiteral(" name=\"Address\"");

WriteLiteral(" id=\"Address\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5888), Tuple.Create("\"", 5910)
            
            #line 162 "..\..\Views\SSManageMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 5896), Tuple.Create<System.Object, System.Int32>(Model.Address
            
            #line default
            #line hidden
, 5896), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;级别</label>\r\n                <input");

WriteLiteral(" name=\"Level\"");

WriteLiteral(" id=\"Level\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6124), Tuple.Create("\"", 6144)
            
            #line 166 "..\..\Views\SSManageMobile\info.cshtml"
    , Tuple.Create(Tuple.Create("", 6132), Tuple.Create<System.Object, System.Int32>(Model.Level
            
            #line default
            #line hidden
, 6132), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;技术创新计划</label>\r\n                <textarea");

WriteLiteral(" rows=\"2\"");

WriteLiteral(" name=\"TechPlan\"");

WriteLiteral(" id=\"TechPlan\"");

WriteLiteral(" style=\"width:98%;overflow:auto;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(">");

            
            #line 170 "..\..\Views\SSManageMobile\info.cshtml"
                                                                                                               Write(Model.TechPlan);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n\r\n            ");

WriteLiteral("\r\n        </form>\r\n      \r\n        </div>\r\n\r\n\r\n    <script>\r\n        var _webconf" +
"ig = {\r\n            serverUrl: \'");

            
            #line 183 "..\..\Views\SSManageMobile\info.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral(@"'
        };
        var datas;
        $(function () {
            if (window.JinQuMobile == undefined) {
                $('.hidden').css('display', 'block')
                //validateFormBegin();
                 initFormBegin(JSON.stringify({
                     ""AllowEditControls"": ""{}""
                }));
            }


        });
        //表单初始化交互
        function initFormBegin(params) {
            //编辑界面显示设备明细
           //alert(params)
            console.log(params)
            $.post('");

            
            #line 202 "..\..\Views\SSManageMobile\info.cshtml"
               Write(Url.Action("ProjectPlanListJsonAll", "DesTask", new { @area= "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?InTaskGroupId=");

            
            #line 202 "..\..\Views\SSManageMobile\info.cshtml"
                                                                                                            Write(Model.EngineeringID);

            
            #line default
            #line hidden
WriteLiteral("\", function (_rData) {\r\n                var rowData = JSON.parse(_rData);\r\n      " +
"          console.log(rowData)\r\n                if (rowData.rows.length > 0) {\r\n" +
"                    var interText = doT.template($(\"#listTp2\").text())\r\n        " +
"            $(\"#gridListShow\").html(interText(rowData.rows));\r\n                 " +
"  \r\n                }\r\n            })\r\n\r\n            $(\'#IsSSFA\').val($(\'#IsSSFA" +
"_i\').val()) //是否制定实施方案\r\n            $(\'#IsNS\').val($(\'#IsNS_i\').val()) //是否内审\r\n " +
"           $(\'#IsWS\').val($(\'#IsWS_i\').val()) //是否外审\r\n            \r\n\r\n\r\n        " +
"    params = params.replace(/[\\r\\n]/g, \"\");//去除换行\r\n            params = params.r" +
"eplace(/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n           " +
" paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n            " +
"//paramsObj.AllowEditControls = paramsObj.AllowEditControls\r\n            //    ." +
"replace(\"EquipId\", \"btnChooseEquipment\");\r\n\r\n            //技术创新计划，替换换行符\r\n       " +
"     var TechPlanValue = $(\'#TechPlan\').val();\r\n            TechPlanValue = Tech" +
"PlanValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            $(\'#TechPlan\')." +
"val(TechPlanValue);\r\n\r\n\r\n            $(\'form :input\').addClass(\'jq-text-disabled" +
"\');\r\n            DomUtil.setCtrlsReadonly(paramsObj.AllowEditControls, document." +
"getElementById(\'SSManageForm\'));\r\n            //告诉移动端页面初始完完毕\r\n            if (wi" +
"ndow.JinQuMobile) {\r\n                window.JinQuMobile.FormEnd();\r\n            " +
"}\r\n        }\r\n        //表单验证交互\r\n        function validateFormBegin() {\r\n        " +
"    //验证表单内容\r\n           \r\n            var formVali = $(\'#SSManageForm\').validat" +
"e({\r\n                //rules: {\r\n                //    EquipGetCustomer: \'requir" +
"ed\'\r\n                //},\r\n                //messages: {\r\n                //    " +
"EquipGetCustomer: \'请输入外委单位\'\r\n                //}\r\n            });\r\n           \r\n" +
"            isValidate = formVali.form();\r\n            var formData = DomUtil.se" +
"rialize(document.getElementById(\'SSManageForm\'));//json or\r\n            console." +
"log(formData)\r\n                //alert(JSON.stringify(formData))\r\n              " +
"  //告诉移动端页面初始完完毕\r\n                if (window.JinQuMobile) {\r\n                   " +
" window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n                " +
"}\r\n            }\r\n            \r\n        \r\n        //不走验证流程\r\n        function noV" +
"alidateFormBegin(params) {\r\n            var formData = DomUtil.serialize(documen" +
"t.getElementById(\'SSManageForm\'));//json or =&\r\n            //alert(JSON.stringi" +
"fy(formData))\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {\r" +
"\n                window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n" +
"            }\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
