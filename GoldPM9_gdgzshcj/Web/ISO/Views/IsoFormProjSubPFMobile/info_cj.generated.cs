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
    
    #line 5 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjSubPFMobile/info_cj.cshtml")]
    public partial class _Views_IsoFormProjSubPFMobile_info_cj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormProjSubPFMobile_info_cj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
  
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

WriteLiteral(" id=\"IsoFormProjSubPFForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" id=\"divXml\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">");

            
            #line 123 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                                  Write(ViewBag.contentXml);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"CJ\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"FormID\"");

WriteLiteral(" name=\"FormID\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3048), Tuple.Create("\"", 3069)
            
            #line 125 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
, Tuple.Create(Tuple.Create("", 3056), Tuple.Create<System.Object, System.Int32>(Model.FormID
            
            #line default
            #line hidden
, 3056), false)
);

WriteLiteral(" />\r\n\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目外委信息：</label>\r\n            </div>\r\n            <div");

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
                            <th>外委编号</th>
                            <th>外委名称</th>
                            <th>项目编号</th>
                            <th>项目名称</th>
                            <th>外委时间</th>
                            <th>登记时间</th>

                        </tr>
                    </thead>
                    <tbody");

WriteLiteral(" id=\"gridList\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral("></tbody>\r\n                </table>\r\n            </div>\r\n\r\n            <script");

WriteLiteral(" id=\"listTpl\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                {{~it:appendProjectGetInfoData:index}}
                <tr id=""tr_{{=index}}"">

                    <td>
                        <div class=""checkbox"" style=""margin:0"">
                            <input type=""checkbox"" name=""subBox"" value="""" />
                        </div>
                    </td>
                    <td>
                        {{=appendProjectGetInfoData.SubNumber}}
                    </td>
                    <td>
                        {{=appendProjectGetInfoData.SubName}}
                    </td>
                    <td>
                        {{=appendProjectGetInfoData.ProjNumber}}
                    </td>
                    <td>
                        {{=appendProjectGetInfoData.ProjName}}
                    </td>
                    <td>
                        {{=(Date.jsonStringToDate(appendProjectGetInfoData.ColAttDate1)).getFullYear() =='1900' ? '':(Date.jsonStringToDate(appendProjectGetInfoData.ColAttDate1)).format(""yyyy-MM-dd"")}}
                        ");

WriteLiteral(@"
                    </td>
                    <td>
                        {{=(Date.jsonStringToDate(appendProjectGetInfoData.CreationTime)).getFullYear() =='1900' ? '':(Date.jsonStringToDate(appendProjectGetInfoData.CreationTime)).format(""yyyy-MM-dd"")}}
                    </td>
                </tr>
                {{~}}
            </script>
            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;合同编号：</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"ConSubNumber\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"ConSubNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5847), Tuple.Create("\"", 5876)
            
            #line 187 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                  , Tuple.Create(Tuple.Create("", 5855), Tuple.Create<System.Object, System.Int32>(ViewBag.ConSubNumber
            
            #line default
            #line hidden
, 5855), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;合同名称：</label>\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(" id=\"ConSubNameText\"");

WriteLiteral("></div>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ConSubName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"ConSubName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6178), Tuple.Create("\"", 6205)
            
            #line 192 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                , Tuple.Create(Tuple.Create("", 6186), Tuple.Create<System.Object, System.Int32>(ViewBag.ConSubName
            
            #line default
            #line hidden
, 6186), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;合同金额：</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"ConSubFee\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"ConSubFee\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6430), Tuple.Create("\"", 6456)
            
            #line 196 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
            , Tuple.Create(Tuple.Create("", 6438), Tuple.Create<System.Object, System.Int32>(ViewBag.ConSubFee
            
            #line default
            #line hidden
, 6438), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;合作方式：</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"HZSJName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"HZSJName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6679), Tuple.Create("\"", 6704)
            
            #line 200 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
          , Tuple.Create(Tuple.Create("", 6687), Tuple.Create<System.Object, System.Int32>(ViewBag.HZSJName
            
            #line default
            #line hidden
, 6687), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;分包合作单位：</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"CustName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6929), Tuple.Create("\"", 6954)
            
            #line 204 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
          , Tuple.Create(Tuple.Create("", 6937), Tuple.Create<System.Object, System.Int32>(ViewBag.CustName
            
            #line default
            #line hidden
, 6937), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;填报单位：</label>\r\n                <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" id=\"CompanyName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" name=\"CompanyName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7183), Tuple.Create("\"", 7211)
            
            #line 208 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                , Tuple.Create(Tuple.Create("", 7191), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyName
            
            #line default
            #line hidden
, 7191), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row aui-input-check-disable\"");

WriteLiteral(" id=\"WFXD\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;是否为我方选定：</label>\r\n");

WriteLiteral("                ");

            
            #line 212 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
           Write(Html.CheckBoxList("items"));

            
            #line default
            #line hidden
WriteLiteral(";\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;分包合作项目服务情况：</label>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row aui-input-check-disable\"");

WriteLiteral(" style=\"width:100%;\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" id=\"FWJD\"");

WriteLiteral(">&nbsp;&nbsp;&nbsp;&nbsp;(1)服务进度 ");

            
            #line 218 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                                          Write(Html.CheckBoxList("itemsA"));

            
            #line default
            #line hidden
WriteLiteral(";</div>\r\n                <div");

WriteLiteral(" id=\"SJZL\"");

WriteLiteral(">&nbsp;&nbsp;&nbsp;&nbsp;(2)设计质量 ");

            
            #line 219 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                                          Write(Html.CheckBoxList("itemsB"));

            
            #line default
            #line hidden
WriteLiteral(";</div>\r\n                <div");

WriteLiteral(" id=\"FWTD\"");

WriteLiteral(">&nbsp;&nbsp;&nbsp;&nbsp;(3)服务态度 ");

            
            #line 220 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                                          Write(Html.CheckBoxList("itemsC"));

            
            #line default
            #line hidden
WriteLiteral(";</div>\r\n                <div");

WriteLiteral(" id=\"ZJPJ\"");

WriteLiteral(">&nbsp;&nbsp;&nbsp;&nbsp;(4)总体评价 ");

            
            #line 221 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                                          Write(Html.CheckBoxList("itemsD"));

            
            #line default
            #line hidden
WriteLiteral(";</div>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;备注</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"BZ\"");

WriteLiteral(" id=\"BZ\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral("></textarea>\r\n            </div>\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjSubIDs\"");

WriteLiteral(" name=\"ProjSubIDs\"");

WriteLiteral(" />\r\n\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stritems\"");

WriteLiteral(" name=\"stritems\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stritemsA\"");

WriteLiteral(" name=\"stritemsA\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stritemsB\"");

WriteLiteral(" name=\"stritemsB\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stritemsC\"");

WriteLiteral(" name=\"stritemsC\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stritemsD\"");

WriteLiteral(" name=\"stritemsD\"");

WriteLiteral(" value=\"\"");

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" name=\"ProjName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8915), Tuple.Create("\"", 8940)
            
            #line 234 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
, Tuple.Create(Tuple.Create("", 8923), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjName
            
            #line default
            #line hidden
, 8923), false)
);

WriteLiteral(" />\r\n            ");

WriteLiteral("\r\n        </form>\r\n    </div>\r\n\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        $(function () {
            if (window.JinQuMobile == undefined) {
                //initFormBegin(JSON.stringify({

                //    ""SignControls"": """",
                //    ""AllowEditControls"":""{}""
                //}));
                //initFormBegin();
            }
        })


        //表单初始化
        function initFormBegin(params) {
           //alert(params)
            //获取数据

            if(");

            
            #line 260 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
          Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n            {\r\n                $(\"#ProjSubIDs\").val(");

            
            #line 262 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
                                Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            }\r\n\r\n            $.post(\'");

            
            #line 265 "..\..\Views\IsoFormProjSubPFMobile\info_cj.cshtml"
               Write(Url.Action("GetProjSubList", "ProjSub", new { @area= "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?CompanyType=CJ\" + \"\", {\r\n                ProjSubIDs: $(\"#ProjSubIDs\").val()" +
"\r\n            }, function (_rData) {\r\n                console.log(JSON.parse(_rD" +
"ata))\r\n                //datas = JSON.parse(_rData)\r\n                var appendP" +
"rojectGetInfoData = JSON.parse(_rData).rows\r\n                if (appendProjectGe" +
"tInfoData.length > 0) {\r\n                    var interText = doT.template($(\"#li" +
"stTpl\").text())\r\n                    $(\"#gridList\").html(interText(appendProject" +
"GetInfoData));\r\n                }\r\n                else {\r\n                    c" +
"onsole.log(\'暂无数据\')\r\n                }\r\n            })\r\n\r\n            var GoldPM " +
"= {\r\n                loadXml: function (xmlContent) {\r\n                    var x" +
"mlDoc;\r\n                    if (\"ActiveXObject\" in window) {\r\n                  " +
"      xmlDoc = new ActiveXObject(\"Microsoft.XMLDOM\");\r\n                        x" +
"mlDoc.async = \"false\";\r\n                        xmlDoc.loadXML(xmlContent);\r\n   " +
"                 }\r\n                    else if (window.DOMParser) {\r\n          " +
"              parser = new DOMParser();\r\n                        xmlDoc = parser" +
".parseFromString(xmlContent, \"text/xml\");\r\n                    }\r\n              " +
"      return xmlDoc;\r\n                },\r\n                selectNodes: function " +
"(node, xpath) {\r\n                    if (\"ActiveXObject\" in window) {\r\n         " +
"               return node.selectNodes(xpath);\r\n                    }\r\n         " +
"           else {\r\n                        var xpe = new XPathEvaluator();\r\n    " +
"                    var nsResolver = xpe.createNSResolver(node.ownerDocument == " +
"null ? node.documentElement : node.ownerDocument.documentElement);\r\n            " +
"            var result = xpe.evaluate(xpath, node, nsResolver, 0, null);\r\n      " +
"                  var found = [];\r\n                        var res;\r\n           " +
"             while (res = result.iterateNext())\r\n                            fou" +
"nd.push(res);\r\n                        return found;\r\n                    }\r\n   " +
"             }\r\n            }\r\n            var flag = decodeURIComponent($(\"#div" +
"Xml\").text().trim());\r\n            if (flag) {\r\n                var xml = GoldPM" +
".loadXml(flag);\r\n                var items = GoldPM.selectNodes(xml, \"Root/Item\"" +
");\r\n                for (var i = 0; i < items.length; i++) {\r\n                  " +
"  var name = items[i].getAttribute(\"name\");\r\n                    var text = item" +
"s[i].text || items[i].innerHTML;\r\n                    $(\"#\" + name).val(text);\r\n" +
"                }\r\n            }\r\n\r\n            params = params.replace(/[\\r\\n]/" +
"g, \"\");//去除换行\r\n            params = params.replace(/\\s/g, \"\");\r\n            var " +
"paramsObj = JSON.parse(params);\r\n            paramsObj.AllowEditControls = param" +
"sObj.AllowEditControls || \'-\';\r\n\r\n            $(\'#ConSubNameText\').text($(\'#ConS" +
"ubName\').val())\r\n\r\n\r\n\r\n\r\n            //设置表单不可编辑样式及只读\r\n            $(\'form :input" +
"\').addClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\");\r\n            DomUt" +
"il.setCtrlsReadonly(paramsObj.AllowEditControls, document.getElementById(\'IsoFor" +
"mProjSubPFForm\'));\r\n\r\n            $(\'#ConSubNumber,#ConSubFee,#HZSJName,#CustNam" +
"e,#CompanyName\').addClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\");\r\n\r\n\r" +
"\n            if (paramsObj.AllowEditControls == \'{}\') {\r\n                // aler" +
"t(\'ok\')\r\n                $(\'input[name=\"items\"]\').removeClass(\'jq-text-disabled\'" +
").attr(\"disabled\", false)\r\n                $(\'input[name=\"itemsA\"]\').removeClass" +
"(\'jq-text-disabled\').attr(\"disabled\", false)\r\n                $(\'input[name=\"ite" +
"msB\"]\').removeClass(\'jq-text-disabled\').attr(\"disabled\", false)\r\n               " +
" $(\'input[name=\"itemsC\"]\').removeClass(\'jq-text-disabled\').attr(\"disabled\", fals" +
"e)\r\n                $(\'input[name=\"itemsD\"]\').removeClass(\'jq-text-disabled\').at" +
"tr(\"disabled\", false)\r\n\r\n            }\r\n            else {\r\n                // a" +
"lert(\'ok1\')\r\n                $(\'input[name=\"items\"]\').addClass(\'jq-text-disabled" +
"\').attr(\"disabled\", \"disabled\")\r\n                $(\'input[name=\"itemsA\"]\').addCl" +
"ass(\'jq-text-disabled\').attr(\"disabled\", \"disabled\")\r\n                $(\'input[n" +
"ame=\"itemsB\"]\').addClass(\'jq-text-disabled\').attr(\"disabled\", \"disabled\")\r\n     " +
"           $(\'input[name=\"itemsC\"]\').addClass(\'jq-text-disabled\').attr(\"disabled" +
"\", \"disabled\")\r\n                $(\'input[name=\"itemsD\"]\').addClass(\'jq-text-disa" +
"bled\').attr(\"disabled\", \"disabled\")\r\n\r\n            }\r\n\r\n            //备注，替换换行符\r\n" +
"            var BZValue = $(\'#BZ\').val();\r\n            BZValue = BZValue.replace" +
"(/[\\r\\n]/g, \"\")\r\n            $(\'#BZ\').val(BZValue);\r\n\r\n            if (paramsObj" +
".AllowEditControls == \"{}\") {\r\n                $(\'#BZ\').removeClass(\'jq-text-dis" +
"abled\').addClass(\'aui-input\')\r\n                $(\"#BZ\").css({\r\n                 " +
"   overflow: \'auto\',\r\n\r\n                })\r\n            }\r\n            else {\r\n " +
"               $(\'#BZ\').removeClass(\'jq-text-disabled\').attr(\"readonly\", \"readon" +
"ly\")\r\n                $(\"#BZ\").css({\r\n                    overflow: \'auto\',\r\n   " +
"                 background: \'#efefef\',\r\n                    color: \'#898787\'\r\n " +
"               })\r\n\r\n            }\r\n            //告诉移动端页面初始完完毕\r\n            if (" +
"window.JinQuMobile) {\r\n                window.JinQuMobile.FormEnd();\r\n          " +
"  }\r\n        }\r\n        /*------------------------------------------------------" +
"----------------------------------------*/\r\n        //表单验证交互\r\n        function v" +
"alidateFormBegin() {\r\n            var formVali = $(\'#IsoFormProjSubPFForm\').vali" +
"date({\r\n                rules: {\r\n\r\n                },\r\n                messages" +
": {\r\n\r\n                }\r\n            });\r\n            isValidate = formVali.for" +
"m();\r\n            if (isValidate) {\r\n\r\n\r\n                //1\r\n                va" +
"r wfxdIdArr = [];\r\n                $(\'#WFXD input[type=\"checkbox\"]:checked\').eac" +
"h(function (i, e) {\r\n                    console.log($(this).val())\r\n           " +
"         wfxdIdArr.push($(this).val())\r\n                })\r\n                $(\'#" +
"stritems\').val(wfxdIdArr)\r\n\r\n                //2\r\n                var fwjdIdArr " +
"= [];\r\n                $(\'#FWJD input[type=\"checkbox\"]:checked\').each(function (" +
"i, e) {\r\n                    fwjdIdArr.push($(this).val())\r\n                })\r\n" +
"                $(\'#stritemsA\').val(fwjdIdArr)\r\n\r\n                //3\r\n         " +
"       var sjzlIdArr = [];\r\n                $(\'#SJZL input[type=\"checkbox\"]:chec" +
"ked\').each(function (i, e) {\r\n                    sjzlIdArr.push($(this).val())\r" +
"\n                })\r\n                $(\'#stritemsB\').val(fwjdIdArr)\r\n\r\n         " +
"       //4\r\n                var fwtdIdArr = [];\r\n                $(\'#FWTD input[" +
"type=\"checkbox\"]:checked\').each(function (i, e) {\r\n                    fwtdIdArr" +
".push($(this).val())\r\n                })\r\n                $(\'#stritemsC\').val(fw" +
"tdIdArr)\r\n\r\n                //5\r\n                var ztpjIdArr = [];\r\n          " +
"      $(\'#ZJPJ input[type=\"checkbox\"]:checked\').each(function (i, e) {\r\n        " +
"            ztpjIdArr.push($(this).val())\r\n                })\r\n                $" +
"(\'#stritemsD\').val(ztpjIdArr)\r\n\r\n                var formData = DomUtil.serializ" +
"e(document.getElementById(\'IsoFormProjSubPFForm\'));//json or =&\r\n               " +
" console.log(formData)\r\n                //告诉移动端页面初始完完毕\r\n                if (wind" +
"ow.JinQuMobile) {\r\n                    window.JinQuMobile.validateFormEnd(JSON.s" +
"tringify(formData));\r\n                }\r\n            }\r\n        }\r\n\r\n\r\n        /" +
"*-------------------------------------------------------------------------------" +
"---------------*/\r\n        //不走验证流程\r\n        function noValidateFormBegin(params" +
") {\r\n\r\n            //1\r\n            var wfxdIdArr = [];\r\n            $(\'#WFXD in" +
"put[type=\"checkbox\"]:checked\').each(function (i, e) {\r\n                console.l" +
"og($(this).val())\r\n                wfxdIdArr.push($(this).val())\r\n            })" +
"\r\n            $(\'#stritems\').val(wfxdIdArr)\r\n\r\n            //2\r\n            var " +
"fwjdIdArr = [];\r\n            $(\'#FWJD input[type=\"checkbox\"]:checked\').each(func" +
"tion (i, e) {\r\n                fwjdIdArr.push($(this).val())\r\n            })\r\n  " +
"          $(\'#stritemsA\').val(fwjdIdArr)\r\n\r\n            //3\r\n            var sjz" +
"lIdArr = [];\r\n            $(\'#SJZL input[type=\"checkbox\"]:checked\').each(functio" +
"n (i, e) {\r\n                sjzlIdArr.push($(this).val())\r\n            })\r\n     " +
"       $(\'#stritemsB\').val(fwjdIdArr)\r\n\r\n            //4\r\n            var fwtdId" +
"Arr = [];\r\n            $(\'#FWTD input[type=\"checkbox\"]:checked\').each(function (" +
"i, e) {\r\n                fwtdIdArr.push($(this).val())\r\n            })\r\n        " +
"    $(\'#stritemsC\').val(fwtdIdArr)\r\n\r\n            //5\r\n            var ztpjIdArr" +
" = [];\r\n            $(\'#ZJPJ input[type=\"checkbox\"]:checked\').each(function (i, " +
"e) {\r\n                ztpjIdArr.push($(this).val())\r\n            })\r\n           " +
" $(\'#stritemsD\').val(ztpjIdArr)\r\n\r\n            var formData = DomUtil.serialize(" +
"document.getElementById(\'IsoFormProjSubPFForm\'));//json or =&\r\n            //告诉移" +
"动端页面验证完毕\r\n\r\n            if (window.JinQuMobile) {\r\n                window.JinQuM" +
"obile.validateFormEnd(JSON.stringify(formData));\r\n            }\r\n        }\r\n\r\n  " +
"  </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
