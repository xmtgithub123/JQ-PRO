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
    
    #line 5 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoContractMobile/SubFeeFact.cshtml")]
    public partial class _Views_IsoContractMobile_SubFeeFact_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoContractMobile_SubFeeFact_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
  
    Layout = "~/Areas/Core/Views/Mobile/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n        .aui-btn {\r\n            padding: 10px 15px;\r\n            font-size: 13" +
"px;\r\n        }\r\n\r\n        .aui-btn-row:after {\r\n            border: 0;\r\n        " +
"}\r\n\r\n        .jq-text-disabled {\r\n            color: #898787;\r\n            width" +
": 98%;\r\n            pointer-events: none;\r\n            background-color: #efefef" +
" !important;\r\n            border: 0;\r\n            font-size: 13px;\r\n        }\r\n\r" +
"\n        .set-disabled {\r\n            pointer-events: none !important\r\n        }" +
"\r\n\r\n        .error {\r\n            color: red;\r\n            font-size: 14px;\r\n   " +
"         display: block;\r\n        }\r\n\r\n        .hidden {\r\n            display: n" +
"one;\r\n        }\r\n\r\n        .aui-input-hook {\r\n            line-height: 34px;\r\n  " +
"          text-align: left;\r\n            padding-left: 20px;\r\n            backgr" +
"ound: #efefef;\r\n        }\r\n\r\n        .aui-display-hook {\r\n            display: n" +
"one;\r\n            width: 30%;\r\n            float: right;\r\n            font-size:" +
" 13px;\r\n            line-height: 33px;\r\n            margin-right: 6px;\r\n        " +
"    padding: 3px 6px;\r\n        }\r\n\r\n        .aui-input-width {\r\n            widt" +
"h: 98%;\r\n        }\r\n\r\n        .hiddenShow {\r\n            height: 0;\r\n        }\r\n" +
"\r\n        .aui-input-row label.aui-input-addon {\r\n            min-width: 90px;\r\n" +
"        }\r\n\r\n        .set-selected-icon select {\r\n            float: right;\r\n   " +
"         margin-right: 5px;\r\n            border: 0;\r\n            margin-bottom: " +
"0;\r\n        }\r\n\r\n        .set-selected-icon i {\r\n            position: absolute;" +
"\r\n            right: 25px;\r\n            top: 15px;\r\n        }\r\n        /*.aui-in" +
"put-row input, .aui-input-row select, .set-selected-icon .aui-input-addon {\r\n   " +
"         color: #9e9e9e;\r\n        }*/\r\n\r\n        .aui-input-row-position {\r\n    " +
"        position: relative;\r\n            display: table;\r\n            padding: 6" +
"px 0;\r\n        }\r\n\r\n        .aui-input-row label.aui-input-addon {\r\n            " +
"font-size: 14px;\r\n        }\r\n\r\n        .error-border {\r\n            border: 1px " +
"solid red !important;\r\n        }\r\n\r\n        .aui-input-check-disable {\r\n        " +
"    font-size: 12px !important;\r\n            font-weight: normal !important;\r\n  " +
"      }\r\n\r\n            .aui-input-check-disable label {\r\n                line-he" +
"ight: 45px;\r\n                margin-right: 15px;\r\n            }\r\n\r\n            ." +
"aui-input-check-disable input[type=\"checkbox\"] {\r\n                width: auto;\r\n" +
"                line-height: 45px;\r\n            }\r\n    </style>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"aui-content\"");

WriteLiteral(" style=\"overflow-x:hidden\"");

WriteLiteral(">\r\n        <form");

WriteLiteral(" id=\"BussSubFeeFactForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"FGS\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"FormID\"");

WriteLiteral(" name=\"FormID\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2964), Tuple.Create("\"", 2985)
            
            #line 124 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
, Tuple.Create(Tuple.Create("", 2972), Tuple.Create<System.Object, System.Int32>(Model.FormID
            
            #line default
            #line hidden
, 2972), false)
);

WriteLiteral(" />\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"table-responsive\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(" id=\"ProjPhaseGrid\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral(">\r\n                    <thead>\r\n                        <tr>\r\n                   " +
"         <th>\r\n                                <div");

WriteLiteral(" class=\"checkAll\"");

WriteLiteral(">\r\n                                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"name\"");

WriteLiteral(" value=\"\"");

WriteLiteral(@" />
                                </div>
                            </th>
                            <th>外委合同信息</th>
                            <th>外委项目信息</th>
                            <th>外委单位信息</th>
                            <th>外委合同金额</th>
                            <th>已付款金额</th>
                            <th>本次付款金额</th>
                            <th>本次付款日期</th>
                            <th>累计比例</th>
                            <th>是否结清</th>
                        </tr>
                    </thead>
                    <tbody");

WriteLiteral(" id=\"gridList\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral("></tbody>\r\n                </table>\r\n            </div>\r\n\r\n            <script");

WriteLiteral(" id=\"listTpl\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(">\r\n                {{~it:appendInfoData:index}}\r\n                <tr id=\"tr_{{=in" +
"dex}}\">\r\n\r\n                    <td>\r\n                        <div class=\"checkbo" +
"x\" style=\"margin:0\">\r\n                            <input type=\"checkbox\" name=\"s" +
"ubBox\" value=\"\" />\r\n                        </div>\r\n                    </td>\r\n " +
"                   <td>\r\n                        <input type=\"hidden\" name=\"c_Co" +
"nSubId-{{=index+1}}\" class=\"aui-input\" id=\"c_ConSubId-{{=index+1}}\" value=\"{{=ap" +
"pendInfoData.ConSubId}}\" />\r\n                        {{=appendInfoData.ConSubNam" +
"e}}\r\n                    </td>\r\n                    <td>\r\n                      " +
"  <input type=\"hidden\" name=\"c_ProjSubId-{{=index+1}}\" class=\"aui-input\" id=\"c_P" +
"rojSubId-{{=index+1}}\" value=\"{{=appendInfoData.ProjSubId}}\" />\r\n               " +
"        {{=appendInfoData.ProjSubId == 0 ? \'\':\'appendInfoData.ProjName\'}}\r\n     " +
"               </td>\r\n                    <td>\r\n                        <input t" +
"ype=\"hidden\" name=\"c_SKDW-{{=index+1}}\" class=\"aui-input\" id=\"c_SKDW-{{=index+1}" +
"}\" value=\"{{=appendInfoData.SKDW}}\" />\r\n                        {{=appendInfoDat" +
"a.SKDW}}\r\n                    </td>\r\n                    <td>\r\n                 " +
"       <input type=\"hidden\" name=\"c_ConSubFee-{{=index+1}}\" class=\"aui-input\" id" +
"=\"c_ConSubFee-{{=index+1}}\" value=\"{{=appendInfoData.ConSubFee}}\" />\r\n          " +
"              {{=appendInfoData.ConSubFee}}\r\n                    </td>\r\n        " +
"            <td>\r\n                        <input type=\"hidden\" name=\"c_AlreadySu" +
"mFeeMoney-{{=index+1}}\" class=\"aui-input\" id=\"c_AlreadySumFeeMoney-{{=index+1}}\"" +
" value=\"{{=appendInfoData.AlreadySumFeeMoney}}\" />\r\n                        {{=a" +
"ppendInfoData.AlreadySumFeeMoney}}\r\n                    </td>\r\n                 " +
"   <td>\r\n                        <input type=\"hidden\" name=\"c_SubFeeFactMoney-{{" +
"=index+1}}\" class=\"aui-input\" id=\"c_SubFeeFactMoney-{{=index+1}}\" value=\"{{=appe" +
"ndInfoData.SubFeeFactMoney}}\" />\r\n                        {{=appendInfoData.SubF" +
"eeFactMoney}}\r\n                    </td>\r\n                    <td>\r\n            " +
"            <input type=\"hidden\" name=\"c_SubFeeFactDate-{{=index+1}}\" class=\"aui" +
"-input\" id=\"c_SubFeeFactDate-{{=index+1}}\" value=\"{{=appendInfoData.SubFeeFactDa" +
"te}}\" />\r\n                        {{=(Date.jsonStringToDate(appendInfoData.SubFe" +
"eFactDate)).getFullYear() ==\'1900\' ? \'\':(Date.jsonStringToDate(appendInfoData.Su" +
"bFeeFactDate)).format(\"yyyy-MM-dd\")}}\r\n                    </td>\r\n              " +
"      <td>\r\n                        <input type=\"hidden\" name=\"c_TotalRatio-{{=i" +
"ndex+1}}\" class=\"aui-input\" id=\"c_TotalRatio-{{=index+1}}\" value=\"{{=appendInfoD" +
"ata.TotalRatio}}\" />\r\n                        {{=appendInfoData.TotalRatio}}\r\n  " +
"                  </td>\r\n                    <td>\r\n                        <inpu" +
"t type=\"hidden\" name=\"c_IsSettle-{{=index+1}}\" class=\"aui-input\" id=\"c_IsSettle-" +
"{{=index+1}}\" value=\"{{=appendInfoData.IsSettle}}\" />\r\n                        {" +
"{=appendInfoData.IsSettle}}\r\n                    </td>\r\n                </tr>\r\n " +
"               {{~}}\r\n            </script>\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strxml\"");

WriteLiteral(" name=\"strxml\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"consubId\"");

WriteLiteral(" name=\"consubId\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n            ");

WriteLiteral("\r\n        </form>\r\n    </div>\r\n\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        var _webconfig = {\r\n            serverUrl: \'");

            
            #line 211 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n            };\r\n\r\n        $(function () {\r\n\r\n            if (window.JinQuMobil" +
"e == undefined) {\r\n                initFormBegin(JSON.stringify({\r\n             " +
"       \"Result\": true,\r\n                    \"Message\": null,\r\n                  " +
"  \"NodeName\": \"发起审批\",\r\n                    \"AllowEditControls\": \"ProjNumber;Proj" +
"Name;ProjEmpId;ProjTypeID;ColAttType2;ProjAreaID;ColAttType1;ProjPropertyID;Proj" +
"VoltID;TaskBasisName;TaskBasisNumber;CustName;ProjTaskSource;ProjDepId;ProjJoinD" +
"epIds;CreatorEmpName;DateCreate;DatePlanStart;DatePlanFinish;ProjTaskSource;Proj" +
"FeeSource;DatePlanDeliver;ProjDemand;ProjNoteOther;btnAddUnit;btnRemoveUnit;Uplo" +
"adFile1;btnChoice;btnInput;ProjPhaseGrid{ProjNumber,ProjName,ProjEmpId,ProjTypeI" +
"D,DatePlanStart,DatePlanFinish,ProjPhaseIds,ProjFee,ProjDemand,ProjNoteOther,Pro" +
"jVoltID,ColAttType3,ColAttVal3,ColAttVal4,ColAttVal5,ProjPhaseInfo};ColAttType4;" +
"ProjTaskContent;ProjCostGrid{ModelTempFee,ModelFactFee};btnImportUnit;btnDownImp" +
"ortExcel;CustID;CustLinkId;CustLinkTel;CustLinkWeb;ColAttVal1\",\r\n               " +
"     \"SignControls\": \"\",\r\n                    \"AllowEditControls\":\"\"\r\n\r\n        " +
"        }));\r\n                //initFormBegin();\r\n            }\r\n\r\n        })\r\n\r" +
"\n\r\n        //表单初始化\r\n        function initFormBegin(params) {\r\n            params" +
" = params.replace(/[\\r\\n]/g, \"\");//去除换行\r\n            params = params.replace(/\\s" +
"/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n            paramsObj" +
".AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n\r\n           //获取数据\r\n " +
"           $.post(\'");

            
            #line 240 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
               Write(Url.Action("GetJsonList", "BussSubFeeFact", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?CompanyType=FGS\" + \"\", {\r\n                SubFeeFactId:  ");

            
            #line 241 "..\..\Views\IsoContractMobile\SubFeeFact.cshtml"
                          Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(",\r\n            }, function (_rData) {\r\n               // alert(_rData)\r\n         " +
"       console.log(JSON.parse(_rData))\r\n                //datas = JSON.parse(_rD" +
"ata)\r\n                var appendInfoData = JSON.parse(_rData).rows\r\n            " +
"    if (appendInfoData.length > 0) {\r\n                    var interText = doT.te" +
"mplate($(\"#listTpl\").text())\r\n                    $(\"#gridList\").html(interText(" +
"appendInfoData));\r\n                }\r\n                else {\r\n                  " +
"  console.log(\'暂无数据\')\r\n                }\r\n            })\r\n\r\n            var Gold" +
"PM = {\r\n                loadXml: function (xmlContent) {\r\n                    va" +
"r xmlDoc;\r\n                    if (\"ActiveXObject\" in window) {\r\n               " +
"         xmlDoc = new ActiveXObject(\"Microsoft.XMLDOM\");\r\n                      " +
"  xmlDoc.async = \"false\";\r\n                        xmlDoc.loadXML(xmlContent);\r\n" +
"                    }\r\n                    else if (window.DOMParser) {\r\n       " +
"                 parser = new DOMParser();\r\n                        xmlDoc = par" +
"ser.parseFromString(xmlContent, \"text/xml\");\r\n                    }\r\n           " +
"         return xmlDoc;\r\n                },\r\n                selectNodes: functi" +
"on (node, xpath) {\r\n                    if (\"ActiveXObject\" in window) {\r\n      " +
"                  return node.selectNodes(xpath);\r\n                    }\r\n      " +
"              else {\r\n                        var xpe = new XPathEvaluator();\r\n " +
"                       var nsResolver = xpe.createNSResolver(node.ownerDocument " +
"== null ? node.documentElement : node.ownerDocument.documentElement);\r\n         " +
"               var result = xpe.evaluate(xpath, node, nsResolver, 0, null);\r\n   " +
"                     var found = [];\r\n                        var res;\r\n        " +
"                while (res = result.iterateNext())\r\n                            " +
"found.push(res);\r\n                        return found;\r\n                    }\r\n" +
"                }\r\n            }\r\n            var flag = decodeURIComponent($(\"#" +
"divXml\").text().trim());\r\n            if (flag) {\r\n                var xml = Gol" +
"dPM.loadXml(flag);\r\n                var items = GoldPM.selectNodes(xml, \"Root/It" +
"em\");\r\n                for (var i = 0; i < items.length; i++) {\r\n               " +
"     var name = items[i].getAttribute(\"name\");\r\n                    var text = i" +
"tems[i].text || items[i].innerHTML;\r\n                    $(\"#\" + name).val(text)" +
";\r\n                }\r\n            }\r\n\r\n            var xml = GoldPM.loadXml(\"<Ro" +
"ot></Root>\");\r\n            $(\"#strxml\").val(xml.documentElement.outerHTML);\r\n\r\n " +
"           //设置表单不可编辑样式及只读\r\n            $(\'form :input\').addClass(\'jq-text-disab" +
"led\').attr(\"readonly\", \"readonly\");\r\n            DomUtil.setCtrlsReadonly(params" +
"Obj.AllowEditControls, document.getElementById(\'BussSubFeeFactForm\'));\r\n\r\n      " +
"      $(\'#FormNote\').removeClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\"" +
")\r\n            $(\"#FormNote\").css({\r\n                overflow: \'auto\',\r\n        " +
"        background: \'#efefef\',\r\n                color: \'#898787\'\r\n            })" +
"\r\n\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile) {\r\n         " +
"       window.JinQuMobile.FormEnd();\r\n            }\r\n        }\r\n        /*------" +
"--------------------------------------------------------------------------------" +
"--------*/\r\n        //表单验证交互\r\n        function validateFormBegin() {\r\n          " +
"  var formVali = $(\'#BussSubFeeFactForm\').validate({\r\n                rules: {\r\n" +
"\r\n                },\r\n                messages: {\r\n\r\n                }\r\n        " +
"    });\r\n            isValidate = formVali.form();\r\n            if (isValidate) " +
"{\r\n                \r\n                var formData = DomUtil.serialize(document.g" +
"etElementById(\'BussSubFeeFactForm\'));//json or =&\r\n                console.log(f" +
"ormData)\r\n                //告诉移动端页面初始完完毕\r\n                if (window.JinQuMobile" +
") {\r\n                    window.JinQuMobile.validateFormEnd(JSON.stringify(formD" +
"ata));\r\n                }\r\n            }\r\n        }\r\n\r\n        /*---------------" +
"-------------------------------------------------------------------------------*" +
"/\r\n        //不走验证流程\r\n        function noValidateFormBegin(params) {\r\n           " +
" var formData = DomUtil.serialize(document.getElementById(\'BussSubFeeFactForm\'))" +
";//json or =&\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {\r" +
"\n                window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n" +
"            }\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591