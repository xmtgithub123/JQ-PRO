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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTaskAltInfo/FeeDetailInfo.cshtml")]
    public partial class _Views_DesTaskAltInfo_FeeDetailInfo_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.DesTaskFeeDetails>
    {
        public _Views_DesTaskAltInfo_FeeDetailInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    #DesignInfo th {\r\n        text-align: center;\r\n    }\r\n\r\n    #DesignInfo td" +
" {\r\n        text-align: center;\r\n    }\r\n</style>\r\n<script>\r\n    var IsView = \'");

            
            #line 12 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
             Write(ViewBag.IsView);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    $(function () {\r\n        $(\"input\", \"#DesignInfoForm_");

            
            #line 14 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                               Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(@""").each(function (i, e) {
            if (IsView == ""1"") {
                var t = $(e).textbox({ editable: false }).next().css({
                    border: ""none"", boxShadow: ""none"", backgroundColor: ""transparent""
                });
                t.children().eq(0).css({
                    backgroundColor: ""transparent""
                })
            }
            else {
                $(e).textbox();
            }
        })
        //保存方法
        window.DesignInfoSave_");

            
            #line 28 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                         Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(" = function () {\r\n            var result = true;\r\n            if (!$(\"#DesignInfo" +
"Form_");

            
            #line 30 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                               Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\").form(\"validate\")) {\r\n                result = false;\r\n                return r" +
"esult;\r\n            }\r\n\r\n            JQ.tools.ajax({\r\n                async: fal" +
"se,//同步操作\r\n                url: \'");

            
            #line 37 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                 Write(Url.Action("FeeDesignSave"));

            
            #line default
            #line hidden
WriteLiteral("?taskId=");

            
            #line 37 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                                     Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                data: $(\"#DesignInfoForm_");

            
            #line 38 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                    Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(@""").serialize(),
                succesCollBack: function (param) {
                    if (param.stateValue > 0) {
                        resutl = true;
                    } else {
                        resutl = false;
                    }
                }
            });

            return resutl;
        }

        window.DesignInfoValid_");

            
            #line 51 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                          Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(" = function () {\r\n            return $(\"#DesignInfoForm_");

            
            #line 52 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                 Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\").form(\"validate\");\r\n        }\r\n\r\n    })\r\n\r\n\r\n</script>\r\n\r\n<form");

WriteAttribute("id", Tuple.Create(" id=\"", 1763), Tuple.Create("\"", 1801)
, Tuple.Create(Tuple.Create("", 1768), Tuple.Create("DesignInfoForm_", 1768), true)
            
            #line 60 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
, Tuple.Create(Tuple.Create("", 1783), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 1783), false)
);

WriteLiteral(">\r\n    <table");

WriteLiteral(" id=\"DesignInfo\"");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(" style=\"table-layout:fixed;\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">招标费(元)</th>\r\n            <td");

WriteLiteral(" width=\"34%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeZBF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2093), Tuple.Create("\"", 2114)
            
            #line 65 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 2101), Tuple.Create<System.Object, System.Int32>(Model.FeeZBF
            
            #line default
            #line hidden
, 2101), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">项目前期工作费(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeXMQQF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2321), Tuple.Create("\"", 2344)
            
            #line 69 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                               , Tuple.Create(Tuple.Create("", 2329), Tuple.Create<System.Object, System.Int32>(Model.FeeXMQQF
            
            #line default
            #line hidden
, 2329), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">设计费(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeSJF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2574), Tuple.Create("\"", 2595)
            
            #line 75 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 2582), Tuple.Create<System.Object, System.Int32>(Model.FeeSJF
            
            #line default
            #line hidden
, 2582), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">预算编制费(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeYSBZF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2800), Tuple.Create("\"", 2823)
            
            #line 79 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                               , Tuple.Create(Tuple.Create("", 2808), Tuple.Create<System.Object, System.Int32>(Model.FeeYSBZF
            
            #line default
            #line hidden
, 2808), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">竣工图编制费(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeJGTBZF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3059), Tuple.Create("\"", 3083)
            
            #line 85 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                                , Tuple.Create(Tuple.Create("", 3067), Tuple.Create<System.Object, System.Int32>(Model.FeeJGTBZF
            
            #line default
            #line hidden
, 3067), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">工程勘察费(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeKCF\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3286), Tuple.Create("\"", 3307)
            
            #line 89 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 3294), Tuple.Create<System.Object, System.Int32>(Model.FeeKCF
            
            #line default
            #line hidden
, 3294), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">总投资额(元)</th>\r\n            <td>\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" name=\"FeeZTZ\"");

WriteLiteral(" data-options=\"validType:\'intOrFloat\',\"");

WriteLiteral(" style=\"width:100%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3540), Tuple.Create("\"", 3561)
            
            #line 96 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 3548), Tuple.Create<System.Object, System.Int32>(Model.FeeZTZ
            
            #line default
            #line hidden
, 3548), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"16%\"");

WriteLiteral(">编辑人</th>\r\n            <td>\r\n                <span>\r\n");

WriteLiteral("                    ");

            
            #line 101 "..\..\Views\DesTaskAltInfo\FeeDetailInfo.cshtml"
               Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral(" \r\n                </span>\r\n            </td>\r\n        </tr>\r\n\r\n    </table>\r\n</f" +
"orm>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591