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
    
    #line 6 "..\..\Views\SafeManageMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/SafeManageMobile/info.cshtml")]
    public partial class _Views_SafeManageMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.SafeManage>
    {
        public _Views_SafeManageMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\SafeManageMobile\info.cshtml"
  
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

WriteLiteral(" id=\"SafeManageForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1909), Tuple.Create("\"", 1926)
            
            #line 84 "..\..\Views\SafeManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 1917), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 1917), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 2163), Tuple.Create("\"", 2202)
            
            #line 90 "..\..\Views\SafeManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2171), Tuple.Create<System.Object, System.Int32>(Request.QueryString["empid"]
            
            #line default
            #line hidden
, 2171), false)
);

WriteLiteral(" />\r\n\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EngineeringID\"");

WriteLiteral(" name=\"EngineeringID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2286), Tuple.Create("\"", 2316)
            
            #line 92 "..\..\Views\SafeManageMobile\info.cshtml"
     , Tuple.Create(Tuple.Create("", 2294), Tuple.Create<System.Object, System.Int32>(Model.EngineeringID
            
            #line default
            #line hidden
, 2294), false)
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

            
            #line 104 "..\..\Views\SafeManageMobile\info.cshtml"
                               Write(ViewBag.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                                <td>");

            
            #line 105 "..\..\Views\SafeManageMobile\info.cshtml"
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

WriteLiteral(">&nbsp;&nbsp;是否内审</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"IsNS_i\"");

WriteLiteral(" id=\"IsNS_i\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4294), Tuple.Create("\"", 4313)
            
            #line 132 "..\..\Views\SafeManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4302), Tuple.Create<System.Object, System.Int32>(Model.IsNS
            
            #line default
            #line hidden
, 4302), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 4727), Tuple.Create("\"", 4746)
            
            #line 140 "..\..\Views\SafeManageMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4735), Tuple.Create<System.Object, System.Int32>(Model.IsWS
            
            #line default
            #line hidden
, 4735), false)
);

WriteLiteral(" />\r\n                <select");

WriteLiteral(" id=\"IsWS\"");

WriteLiteral(" name=\"IsWS\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4834), Tuple.Create("\"", 4853)
            
            #line 141 "..\..\Views\SafeManageMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 4842), Tuple.Create<System.Object, System.Int32>(Model.IsWS
            
            #line default
            #line hidden
, 4842), false)
);

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                </select>\r\n            </div>\r\n\r\n         \r\n\r\n      " +
"      ");

WriteLiteral("\r\n        </form>\r\n      \r\n        </div>\r\n\r\n\r\n    <script>\r\n        var _webconf" +
"ig = {\r\n            serverUrl: \'");

            
            #line 159 "..\..\Views\SafeManageMobile\info.cshtml"
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

            
            #line 178 "..\..\Views\SafeManageMobile\info.cshtml"
               Write(Url.Action("ProjectPlanListJsonAll", "DesTask", new { @area= "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?InTaskGroupId=");

            
            #line 178 "..\..\Views\SafeManageMobile\info.cshtml"
                                                                                                            Write(Model.EngineeringID);

            
            #line default
            #line hidden
WriteLiteral("\", function (_rData) {\r\n                var rowData = JSON.parse(_rData);\r\n      " +
"          console.log(rowData)\r\n                if (rowData.rows.length > 0) {\r\n" +
"                    var interText = doT.template($(\"#listTp2\").text())\r\n        " +
"            $(\"#gridListShow\").html(interText(rowData.rows));\r\n                 " +
"  \r\n                }\r\n            })\r\n\r\n           / $(\'#IsSSFA\').val($(\'#IsSSF" +
"A_i\').val()) //是否制定实施方案\r\n            $(\'#IsNS\').val($(\'#IsNS_i\').val()) //是否内审\r\n" +
"            $(\'#IsWS\').val($(\'#IsWS_i\').val()) //是否外审\r\n            \r\n\r\n\r\n       " +
"     params = params.replace(/[\\r\\n]/g, \"\");//去除换行\r\n            params = params." +
"replace(/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n          " +
"  paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n           " +
" //paramsObj.AllowEditControls = paramsObj.AllowEditControls\r\n            //    " +
".replace(\"EquipId\", \"btnChooseEquipment\");\r\n\r\n          \r\n\r\n\r\n            $(\'for" +
"m :input\').addClass(\'jq-text-disabled\');\r\n            DomUtil.setCtrlsReadonly(p" +
"aramsObj.AllowEditControls, document.getElementById(\'SafeManageForm\'));\r\n       " +
"     //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile) {\r\n                wind" +
"ow.JinQuMobile.FormEnd();\r\n            }\r\n        }\r\n        //表单验证交互\r\n        f" +
"unction validateFormBegin() {\r\n            //验证表单内容\r\n           \r\n            va" +
"r formVali = $(\'#SafeManageForm\').validate({\r\n                //rules: {\r\n      " +
"          //    EquipGetCustomer: \'required\'\r\n                //},\r\n            " +
"    //messages: {\r\n                //    EquipGetCustomer: \'请输入外委单位\'\r\n          " +
"      //}\r\n            });\r\n           \r\n            isValidate = formVali.form(" +
");\r\n            var formData = DomUtil.serialize(document.getElementById(\'SafeMa" +
"nageForm\'));//json or\r\n            console.log(formData)\r\n                //aler" +
"t(JSON.stringify(formData))\r\n                //告诉移动端页面初始完完毕\r\n                if " +
"(window.JinQuMobile) {\r\n                    window.JinQuMobile.validateFormEnd(J" +
"SON.stringify(formData));\r\n                }\r\n            }\r\n            \r\n     " +
"   \r\n        //不走验证流程\r\n        function noValidateFormBegin(params) {\r\n         " +
"   var formData = DomUtil.serialize(document.getElementById(\'SafeManageForm\'));/" +
"/json or =&\r\n            //alert(JSON.stringify(formData))\r\n            //告诉移动端页" +
"面验证完毕\r\n            if (window.JinQuMobile) {\r\n                window.JinQuMobile" +
".validateFormEnd(JSON.stringify(formData));\r\n            }\r\n        }\r\n\r\n    </s" +
"cript>\r\n");

});

        }
    }
}
#pragma warning restore 1591
