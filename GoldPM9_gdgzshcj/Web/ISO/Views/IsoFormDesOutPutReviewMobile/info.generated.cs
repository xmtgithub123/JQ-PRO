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
    
    #line 5 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormDesOutPutReviewMobile/info.cshtml")]
    public partial class _Views_IsoFormDesOutPutReviewMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormDesOutPutReviewMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
  
    Layout = "~/Areas/Core/Views/Mobile/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .aui-btn {\r\n        padding: 10px 15px;\r\n        font-size: 13px;\r\n    }\r\n" +
"\r\n    .aui-btn-row:after {\r\n        border: 0;\r\n    }\r\n\r\n    .p-15 {\r\n        pa" +
"dding: 15px;\r\n    }\r\n\r\n\r\n    .set-disabled {\r\n        pointer-events: none !impo" +
"rtant\r\n    }\r\n\r\n    .error {\r\n        color: red;\r\n        font-size: 14px;\r\n   " +
"     display: inline-block;\r\n    }\r\n\r\n    .hidden {\r\n        display: none;\r\n   " +
" }\r\n\r\n    .aui-input-hook {\r\n        line-height: 34px;\r\n        text-align: lef" +
"t;\r\n        padding-left: 20px;\r\n        background: #efefef;\r\n    }\r\n\r\n    .aui" +
"-display-hook {\r\n        display: none;\r\n        width: 30%;\r\n        float: rig" +
"ht;\r\n        font-size: 13px;\r\n        line-height: 33px;\r\n        margin-right:" +
" 6px;\r\n        padding: 3px 6px;\r\n    }\r\n\r\n    .aui-input-width {\r\n        width" +
": 98%;\r\n    }\r\n\r\n    .hiddenShow {\r\n        height: 0;\r\n    }\r\n\r\n    .aui-input-" +
"row label.aui-input-addon {\r\n        min-width: 90px;\r\n    }\r\n\r\n    .set-selecte" +
"d-icon select {\r\n        float: right;\r\n        margin-right: 5px;\r\n        bord" +
"er: 0;\r\n        margin-bottom: 0;\r\n    }\r\n\r\n    .set-selected-icon i {\r\n        " +
"position: absolute;\r\n        right: 25px;\r\n        top: 15px;\r\n    }\r\n\r\n    .aui" +
"-input-row-position {\r\n        position: relative;\r\n        display: table;\r\n   " +
"     padding: 6px 0;\r\n    }\r\n\r\n    .aui-input-row label.aui-input-addon {\r\n     " +
"   font-size: 14px;\r\n    }\r\n\r\n    .jq-text-disabled {\r\n        color: #898787;\r\n" +
"        width: 98%;\r\n        pointer-events: none;\r\n        background-color: #e" +
"fefef !important;\r\n        border: 0;\r\n        font-size: 13px;\r\n    }\r\n</style>" +
"\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"aui-content\"");

WriteLiteral(">\r\n        <form");

WriteLiteral(" id=\"IsoFormDesOutPutReviewForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"p-15\"");

WriteLiteral(">\r\n                <b");

WriteLiteral(" class=\"aui-input-addon aui-text-danger\"");

WriteLiteral(">*</b>\r\n                编号<input");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" style=\"display:inline-block;width: 50%;text-overflow:ellipsis;border: 1px solid " +
"#dbdbdb; border-radius: 6px;padding: 3px 6px;margin:0 3px;text-align:center;\"");

WriteLiteral(" class=\"form-control\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2254), Tuple.Create("\"", 2281)
            
            #line 106 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                                                                                                                                                                                                   , Tuple.Create(Tuple.Create("", 2262), Tuple.Create<System.Object, System.Int32>(ViewData["Number"]
            
            #line default
            #line hidden
, 2262), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"JsonRows\"");

WriteLiteral(" id=\"JsonRows\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"FormID\"");

WriteLiteral(" name=\"FormID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2548), Tuple.Create("\"", 2571)
            
            #line 111 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2556), Tuple.Create<System.Object, System.Int32>(Model.FormID
            
            #line default
            #line hidden
, 2556), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <b");

WriteLiteral(" class=\"aui-input-addon aui-text-danger\"");

WriteLiteral(">*</b>\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目名称</label>\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"required:true,multiple:false\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" style=\"width:98%;height:26px;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2914), Tuple.Create("\"", 2972)
            
            #line 116 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                                                                  , Tuple.Create(Tuple.Create("", 2922), Tuple.Create<System.Object, System.Int32>(@Model.ProjID==0 ? "" :@Model.ProjID.ToString()
            
            #line default
            #line hidden
, 2922), false)
);

WriteLiteral(" />\r\n                ");

WriteLiteral("\r\n                <textarea");

WriteLiteral(" rows=\"1\"");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" style=\"width:98%;overflow:auto\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(">");

            
            #line 118 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                                                                                                              Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n                ");

WriteLiteral("\r\n                ");

WriteLiteral("\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设计阶段</label>\r\n                <input");

WriteLiteral(" name=\"PhaseName\"");

WriteLiteral(" id=\"PhaseName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3859), Tuple.Create("\"", 3881)
            
            #line 124 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
            , Tuple.Create(Tuple.Create("", 3867), Tuple.Create<System.Object, System.Int32>(ViewBag.Phase
            
            #line default
            #line hidden
, 3867), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目编号</label>\r\n                <input");

WriteLiteral(" name=\"ProjNum\"");

WriteLiteral(" id=\"ProjNum\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4101), Tuple.Create("\"", 4128)
            
            #line 128 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 4109), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjNumber
            
            #line default
            #line hidden
, 4109), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"PhaseName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4187), Tuple.Create("\"", 4209)
            
            #line 129 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4195), Tuple.Create<System.Object, System.Int32>(ViewBag.Phase
            
            #line default
            #line hidden
, 4195), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4269), Tuple.Create("\"", 4296)
            
            #line 130 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4277), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjNumber
            
            #line default
            #line hidden
, 4277), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"RecordMan\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4355), Tuple.Create("\"", 4383)
            
            #line 131 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4363), Tuple.Create<System.Object, System.Int32>(ViewData["EmpName"]
            
            #line default
            #line hidden
, 4363), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;评审主持人</label>\r\n                <input");

WriteLiteral(" name=\"ReviewName\"");

WriteLiteral(" id=\"ReviewName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\'", 4610), Tuple.Create("\'", 4641)
            
            #line 135 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
              , Tuple.Create(Tuple.Create("", 4618), Tuple.Create<System.Object, System.Int32>(ViewData["ReviewName"]
            
            #line default
            #line hidden
, 4618), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;记录人</label>\r\n                <input");

WriteLiteral(" name=\"EmpName\"");

WriteLiteral(" id=\"EmpName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\'", 4860), Tuple.Create("\'", 4888)
            
            #line 139 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
        , Tuple.Create(Tuple.Create("", 4868), Tuple.Create<System.Object, System.Int32>(ViewData["EmpName"]
            
            #line default
            #line hidden
, 4868), false)
);

WriteLiteral("/>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;评审时间</label>\r\n                <input");

WriteLiteral(" name=\"FormDate\"");

WriteLiteral(" id=\"FormDate\"");

WriteLiteral(" type=\"date\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5109), Tuple.Create("\"", 5155)
            
            #line 143 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 5117), Tuple.Create<System.Object, System.Int32>(Model.FormDate.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 5117), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;参加评审人员</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Person\"");

WriteLiteral(" id=\"Person\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ReviewPerson\"");

WriteLiteral(" id=\"ReviewPerson\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5474), Tuple.Create("\"", 5507)
            
            #line 148 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 5482), Tuple.Create<System.Object, System.Int32>(ViewData["ReviewPerson"]
            
            #line default
            #line hidden
, 5482), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ReviewPersonName\"");

WriteLiteral(" id=\"ReviewPersonName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(" id=\"ReviewPersonNameText\"");

WriteLiteral(" style=\"width:60%;display:inline-block;\"");

WriteLiteral("></div>\r\n                <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"useLeaderBtn\"");

WriteLiteral(" namne=\"useLeaderBtn\"");

WriteLiteral(" class=\"aui-btn aui-btn-block aui-btn-success p-0\"");

WriteLiteral(" value=\"选择人员\"");

WriteLiteral(" style=\"width:32%;float:right;margin-right:8px;\"");

WriteLiteral(" onclick=\"usePeopleFormBegin();\"");

WriteLiteral(" />\r\n            </div>\r\n            <div");

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
                            <th>评审对象</th>
                            <th>评审内容</th>
                            <th>评审结果</th>
                            <th>评审意见</th>
                            <th>专业</th>
                        </tr>
                    </thead>
                    <tbody");

WriteLiteral(" id=\"gridList\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral("></tbody>\r\n                </table>\r\n            </div>\r\n\r\n            <script");

WriteLiteral(" id=\"listTpl\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                {{~it:appendReviewTargetInfoData:index}}
                <tr id=""tr_{{=index}}"">

                    <td>
                        <input type=""hidden"" value=""{{=appendReviewTargetInfoData.FormID}}"" />
                        <input type=""hidden"" value=""{{=appendReviewTargetInfoData.RefID}}"" />
                        <input type=""hidden"" value=""{{=appendReviewTargetInfoData.SpecialId}}"" />
                        <div class=""checkbox"" style=""margin:0"">
                            <input type=""checkbox"" name=""subBox"" value="""" />
                        </div>
                    </td>
                    <td>
                        {{=appendReviewTargetInfoData.ReviewTarget}}
                    </td>
                    <td>
                        {{=appendReviewTargetInfoData.ReviewContent}}
                    </td>
                    <td>
                        {{=appendReviewTargetInfoData.ReviewResult}}
                    </td>
                    <td>
                        {{=appendReviewTargetInfoData.ReviewSugg}}
                    </td>
                    <td>
                        {{=appendReviewTargetInfoData.SpecialName}}
                    </td>
                </tr>
                {{~}}
            </script>
            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;评审结论</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"FormReason\"");

WriteLiteral(" id=\"FormReason\"");

WriteLiteral(" style=\"width:98%;overflow:auto;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(">");

            
            #line 208 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                                                                                                                   Write(Model.FormReason);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            ");

WriteLiteral("\r\n        </form>\r\n    </div>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        var datas;\r\n        var _webconfig = {\r\n            serverUrl: \'");

            
            #line 219 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral(@"'
        };
        $(function () {
            if (window.JinQuMobile == undefined) {
                initFormBegin(JSON.stringify({
                    ""Result"": true,
                    ""Message"": null,
                    ""NodeName"": null,
                    ""AllowEditControls"": ""{}"",
                    ""SignControls"": ""DesignName(签名)"",
                   
                    //""AllowEditControls"":""""

                }));
            //initFormBegin();
            }

        })

        //页面初始化
        function initFormBegin(params) {

            //alert(params)
            //获取数据
            var text = '");

            
            #line 243 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                   Write(ViewData["ReviewPerson"]);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n            //参加评审人员\r\n            var SubSpecialArr = text.split(\',\');\r\n     " +
"       var SubSpecialList,jsonDataList,depId,empName,empNameArr = [];\r\n         " +
"   var url = _webconfig.serverUrl + \'/Core/usercontrol/employeeByDepJson?isDialo" +
"g=false\';\r\n            $.ajax({\r\n                type: \'POST\',\r\n                " +
"url: url,\r\n                data: \'\',\r\n                success: function (data) {" +
"\r\n                    var jsonData = JSON.parse(data)[0].children;\r\n            " +
"        for (var i = 0; i < SubSpecialArr.length; i++) {\r\n                      " +
"  SubSpecialList = SubSpecialArr[i]\r\n                       // console.log(SubSp" +
"ecialArr[i].split(\'-\'))\r\n                        depId = SubSpecialArr[i].split(" +
"\'-\')[0];\r\n                        empName = SubSpecialArr[i].split(\'-\')[1];\r\n   " +
"                     for (var j = 0; j < jsonData.length; j++) {\r\n              " +
"              //console.log(jsonData[j])\r\n                            jsonDataLi" +
"st = jsonData[j]\r\n                            if (empName == jsonDataList.id) {\r" +
"\n                                var empArr = jsonDataList.children;\r\n          " +
"                      for (var k = 0; k < empArr.length; k++) {\r\n               " +
"                     //console.log(empArr[k])\r\n                                 " +
"   if (depId == empArr[k].empid) {\r\n                                        cons" +
"ole.log(empArr[k].text)\r\n                                        empNameArr.push" +
"(empArr[k].text)\r\n                                    }\r\n                       " +
"         }\r\n                            }\r\n                            \r\n       " +
"                 }\r\n                    }\r\n                    console.log(empNa" +
"meArr)\r\n                    $(\'#ReviewPersonNameText\').text(empNameArr.toString(" +
"))\r\n                    $(\'#ReviewPersonName\').val(empNameArr.toString())\r\n     " +
"           }\r\n            })\r\n\r\n            //清除设计评审时机，默认为1900-01-01改为空\r\n       " +
"     var FormDate = $(\'#FormDate\').val();;\r\n            if ((FormDate == \'1900-0" +
"1-01\')) {\r\n                $(\'#FormDate\').val(\'\');\r\n            }\r\n\r\n\r\n         " +
"   $.post(\'");

            
            #line 288 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
               Write(Url.Action("ReviewJson", "IsoFormNode", new { @area= "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?FormID=");

            
            #line 288 "..\..\Views\IsoFormDesOutPutReviewMobile\info.cshtml"
                                                                                          Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral("\" , function (_rData) {\r\n                //console.log(JSON.parse(_rData))\r\n     " +
"           datas = JSON.parse(_rData).rows\r\n                var appendReviewTarg" +
"etInfoData = JSON.parse(_rData).rows\r\n                if (appendReviewTargetInfo" +
"Data.length > 0) {\r\n                    var interText = doT.template($(\"#listTpl" +
"\").text())\r\n                    $(\"#gridList\").html(interText(appendReviewTarget" +
"InfoData));\r\n                }\r\n                else {\r\n                    cons" +
"ole.log(\'暂无数据\')\r\n                }\r\n            })\r\n\r\n\r\n\r\n            //验收情况记录\t，" +
"替换换行符\r\n            var FormReasonValue = $(\'#FormReason\').val();\r\n            Fo" +
"rmReasonValue = FormReasonValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n     " +
"       $(\'#FormReason\').val(FormReasonValue);\r\n\r\n            //去除换行\r\n           " +
" params = params.replace(/[\\r\\n]/g, \"\");\r\n            params = params.replace(/\\" +
"s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n\r\n            //按钮替换" +
"\r\n            paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r" +
"\n            paramsObj.AllowEditControls = paramsObj.AllowEditControls.replace(\"" +
"ProjID\", \"selectProjNameBtn\").replace(\'ReviewPerson\',\'useLeaderBtn\');\r\n\r\n       " +
"     //设置表单不可编辑样式及只读\r\n            $(\'form :input\').addClass(\'jq-text-disabled\')." +
"attr(\"readonly\", \"readonly\");\r\n            DomUtil.setCtrlsReadonly(paramsObj.Al" +
"lowEditControls, document.getElementById(\'IsoFormDesOutPutReviewForm\'));\r\n\r\n    " +
"       \r\n\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile) {\r\n  " +
"              window.JinQuMobile.FormEnd();\r\n            }\r\n        }\r\n\r\n       " +
" /*-----------------------------------------------------------------------------" +
"-----------------*/\r\n        //表单验证交互\r\n\r\n        function validateFormBegin() {\r" +
"\n            var formVali = $(\'#IsoFormDesOutPutReviewForm\').validate({\r\n       " +
"         rules: {\r\n                    ProjectName: \'required\',//项目名称\r\n         " +
"           Number: \'required\',//编号\r\n\r\n                    FormReason: {\r\n       " +
"                 required: false,\r\n                        maxlength: 500\r\n     " +
"               }\r\n\r\n                },\r\n                messages: {\r\n           " +
"         ProjectName: \'请输入项目名称\',\r\n                    Number: \'请输入编号\',\r\n        " +
"            FormReason: {\r\n                        maxlength: \'内容长度必须介于0-500之间\'\r" +
"\n                    }\r\n                }\r\n            });\r\n            if ($(\'#" +
"ProjectName\').val() == \'\') {\r\n                $.alert(\'请选择项目！\')\r\n               " +
" return false;\r\n            }\r\n            isValidate = formVali.form();\r\n\r\n    " +
"        $(\'#JsonRows\').val(JSON.stringify(datas))\r\n\r\n            if (isValidate)" +
" {\r\n                var formData = DomUtil.serialize(document.getElementById(\'Is" +
"oFormDesOutPutReviewForm\'));//json or =&\r\n                console.log(formData)\r" +
"\n                //告诉移动端页面初始完完毕\r\n                if (window.JinQuMobile) {\r\n    " +
"                window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n " +
"               }\r\n            }\r\n        }\r\n\r\n        /*------------------------" +
"----------------------------------------------------------------------*/\r\n      " +
"  //不走验证流程\r\n        function noValidateFormBegin() {\r\n            var formData =" +
" DomUtil.serialize(document.getElementById(\'IsoFormDesOutPutReviewForm\'));//json" +
" or =&\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {\r\n      " +
"          window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n       " +
"     }\r\n        }\r\n\r\n        function SetProjectSelectBegin() {\r\n            var" +
" selemp = {\r\n                ProjID: $(\'#ProjID\').val(),\r\n                ProjNa" +
"me: $(\'#ProjectName\').val()\r\n            }\r\n            if (window.JinQuMobile) " +
"{\r\n                window.JinQuMobile.ProjectSelectBegin(JSON.stringify(selemp)," +
" \'SetProjectSelectEnd\');\r\n            }\r\n        }\r\n        function SetProjectS" +
"electEnd(emp) {\r\n            alert(emp)\r\n            emp = JSON.parse(emp);\r\n\r\n " +
"           $(\'#ProjectName\').val(emp.ProjName);\r\n            $(\'#PhaseName\').val" +
"(emp.ProjPhaseName)\r\n            $(\'#ProjNum\').val(emp.ProjNumber)\r\n            " +
"$(\'#ProjID\').val(emp.ProjPhaseId);\r\n            $(\'#ProjectNameText\').text(emp.P" +
"rojName);\r\n        }\r\n\r\n        function usePeopleFormBegin() {\r\n            var" +
" selectUsePeople = {\r\n                useName: $(\'#ReviewPerson\').val(),\r\n      " +
"          isSingleSelect: 1  //0：单选   1：多选\r\n                //UsePeopleID: $(\"#U" +
"sePeopleID\").val()\r\n            }\r\n            if (window.JinQuMobile) {\r\n      " +
"          window.JinQuMobile.usePeopleSingleSelectBegin(JSON.stringify(selectUse" +
"People), \'usePeopleFormEnd\');\r\n            }\r\n        }\r\n       \r\n        //正确版本" +
"\r\n        function usePeopleFormEnd(emp) {\r\n            //alert(emp)\r\n          " +
"  emp = JSON.parse(emp)\r\n            var empArr = [];\r\n            var empId = [" +
"];\r\n            emp.forEach(function (e, i) {\r\n                empArr.push(e.Emp" +
"Name)\r\n                empId.push(e.EmpID+\'-\'+e.EmpDepID)\r\n            })\r\n     " +
"      // alert(empId)\r\n            $(\'#ReviewPersonName\').val(empArr);\r\n        " +
"    $(\'#ReviewPersonNameText\').text(empArr);\r\n            $(\'#ReviewPerson\').val" +
"(empId);\r\n            //alert(empId)\r\n        }\r\n       \r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
