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
    
    #line 6 "..\..\Views\OaEquipUseMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaEquipUseMobile/info.cshtml")]
    public partial class _Views_OaEquipUseMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaEquipUse>
    {
        public _Views_OaEquipUseMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\OaEquipUseMobile\info.cshtml"
  
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

WriteLiteral(" id=\"OaEquipUseForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1909), Tuple.Create("\"", 1926)
            
            #line 84 "..\..\Views\OaEquipUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 1917), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 1917), false)
);

WriteLiteral(" />\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;设备</label>\r\n                <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnChooseEquipment\"");

WriteLiteral(" namne=\"btnChooseEquipment\"");

WriteLiteral(" class=\"aui-btn aui-btn-block aui-btn-success p-0\"");

WriteLiteral(" style=\"width:40%;\"");

WriteLiteral(" onclick=\"EquipmentChooseBegin();\"");

WriteLiteral(" value=\"选择设备\"");

WriteLiteral(" />\r\n                <br />\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EquipId\"");

WriteLiteral(" name=\"EquipId\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EquipOrOffice\"");

WriteLiteral(" name=\"EquipOrOffice\"");

WriteAttribute("value", Tuple.Create(" value=\'", 2474), Tuple.Create("\'", 2519)
            
            #line 93 "..\..\Views\OaEquipUseMobile\info.cshtml"
    , Tuple.Create(Tuple.Create("", 2482), Tuple.Create<System.Object, System.Int32>(Request.QueryString["EquipOrOffice"]
            
            #line default
            #line hidden
, 2482), false)
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

WriteLiteral(">设备编号</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">设备名称</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">设备规格</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">数量</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">操作</th>\r\n                            </tr>\r\n                        </thead>\r\n  " +
"                      <tbody");

WriteLiteral(" id=\"gridListShow\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral("></tbody>\r\n                    </table>\r\n                </div>\r\n                " +
"\r\n                <script");

WriteLiteral(" id=\"listTp2\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(">\r\n                    {{~it:appendselectArrayInfoData:index}}\r\n                 " +
"   <tr>\r\n                        <td>\r\n                           ");

WriteLiteral(@"
                            <span class=""hidden"" name=""EquipId_Id"" id=""EquipId_Id"">{{=appendselectArrayInfoData.EquipId}}</span>
                            <input type=""hidden"" name=""E_Id"" id=""E_Id"" value=""{{=appendselectArrayInfoData.Id}}"" />
                         
                            <span id=""EquipNumber"" name=""EquipNumber"">{{=appendselectArrayInfoData.EquipNumber}}</span>
                        </td>
                        <td>
                            ");

WriteLiteral("\r\n                            <span id=\"EquipName\" name=\"EquipName\">{{=appendsele" +
"ctArrayInfoData.EquipName}}</span>\r\n                        </td>\r\n             " +
"           <td>\r\n                            ");

WriteLiteral(@"
                            <span id=""EquipModel"" name=""EquipModel"">{{=appendselectArrayInfoData.EquipModel}}</span>
                        </td>
                        <td>
                              <input type=""hidden"" name=""E_SJKC"" value=""{{=appendselectArrayInfoData.EquipTotalCount}}"" />
                             <input type=""number"" id=""EquipTotalCount"" name=""EquipTotalCount"" value=""{{=appendselectArrayInfoData.EquipTotalCount}}"" style=""width:60px;padding:0;text-align:center;"" />
                            ");

WriteLiteral(@"
                        </td>
                        <td style=""margin:0 auto;text-align:center"">
                            <input type=""button"" name=""DeleteRowBtn"" id=""DeleteRowBtn_{{=index}}"" class=""delete-btn aui-btn aui-btn-block aui-btn-success p-0"" value=""删除"" onclick=""removeTrInfo(this);"" style=""width:auto;margin:0"" />
                            ");

WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n                    {" +
"{~}}\r\n                </script>\r\n            </div>\r\n          \r\n            <di" +
"v");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <b");

WriteLiteral(" class=\"aui-input-addon aui-text-danger\"");

WriteLiteral(">*</b>\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;领用时间</label>\r\n                <input");

WriteLiteral(" name=\"EquipLendDate\"");

WriteLiteral(" id=\"EquipLendDate\"");

WriteLiteral(" type=\"date\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6099), Tuple.Create("\"", 6150)
            
            #line 144 "..\..\Views\OaEquipUseMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 6107), Tuple.Create<System.Object, System.Int32>(Model.EquipLendDate.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 6107), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <b");

WriteLiteral(" class=\"aui-input-addon aui-text-danger\"");

WriteLiteral(">*</b>\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;归还时间</label>\r\n                <input");

WriteLiteral(" name=\"EquipBackDate\"");

WriteLiteral(" id=\"EquipBackDate\"");

WriteLiteral(" type=\"date\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6448), Tuple.Create("\"", 6499)
            
            #line 149 "..\..\Views\OaEquipUseMobile\info.cshtml"
                    , Tuple.Create(Tuple.Create("", 6456), Tuple.Create<System.Object, System.Int32>(Model.EquipBackDate.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 6456), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;备注</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"EquipLendNote\"");

WriteLiteral(" id=\"EquipLendNote\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 153 "..\..\Views\OaEquipUseMobile\info.cshtml"
                                                                                                                                                                   Write(Model.EquipLendNote);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            ");

WriteLiteral("\r\n        </form>\r\n        <div");

WriteLiteral(" class=\"aui-dialog aui-hidden\"");

WriteLiteral(" id=\"dialog\"");

WriteLiteral(" style=\"top:50%;\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-dialog-header\"");

WriteLiteral(">选择设备</div>\r\n            <div");

WriteLiteral(" class=\"aui-dialog-body aui-text-left\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"aui-card aui-noborder\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"table-responsive\"");

WriteLiteral(">\r\n                        <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n                            <thead>\r\n                                <tr>\r\n   " +
"                                 <th>\r\n                                        <" +
"div");

WriteLiteral(" class=\"checkAll\"");

WriteLiteral(">\r\n                                            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"name\"");

WriteLiteral(" value=\"\"");

WriteLiteral(@" />
                                        </div>
                                    </th>
                                    <th>编号</th>
                                    <th>名称</th>
                                    <th>规格</th>
                                    <th>总量</th>
                                    <th>库存</th>
                                </tr>
                            </thead>
                            <tbody");

WriteLiteral(" id=\"gridList\"");

WriteLiteral(" style=\"max-height:250px;overflow-y:auto;margin-bottom:0;\"");

WriteLiteral("></tbody>\r\n                        </table>\r\n                    </div>\r\n\r\n\r\n    " +
"                <!--模板-->\r\n                    <script");

WriteLiteral(" id=\"listTpl\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                        {{~it:appendEquipGetInfoData:index}}

                        <tr id=""tr_{{=index}}"">

                            <td>
                                <div class=""checkbox"">
                                    <input type=""checkbox"" name=""subBox"" value="""" />
                                </div>
                            </td>
                            <td>
                                {{=appendEquipGetInfoData.EquipNumber}}
                            </td>
                            <td>
                                {{=appendEquipGetInfoData.EquipName}}
                            </td>
                            <td>
                                {{=appendEquipGetInfoData.EquipModel}}
                            </td>
                            <td>
                                {{=appendEquipGetInfoData.EquipTotalCount}}
                            </td>
                            <td>
                                {{=appendEquipGetInfoData.SJKC}}
                            </td>
                            <td class=""hidden"">{{=appendEquipGetInfoData.Id}}</td>

                        </tr>
                        {{~}}
                    </script>
                </div>
            </div>
            <div");

WriteLiteral(" class=\"aui-dialog-footer\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"aui-dialog-btn aui-text-danger\"");

WriteLiteral(" tapmode");

WriteLiteral(" onclick=\"cancel()\"");

WriteLiteral(">取消</div>\r\n                <div");

WriteLiteral(" class=\"aui-dialog-btn aui-text-info\"");

WriteLiteral(" tapmode");

WriteLiteral(" onclick=\"confirm()\"");

WriteLiteral(">确定</div>\r\n            </div>\r\n        </div>\r\n        </div>\r\n\r\n\r\n\r\n    <script>" +
"\r\n        var _webconfig = {\r\n            serverUrl: \'");

            
            #line 228 "..\..\Views\OaEquipUseMobile\info.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral(@"'
        };
        hasFloatNumber = 0;
        var datas,equipjsondatas;
        $(function () {
            if (window.JinQuMobile == undefined) {
                //$('.hidden').css('display', 'block')
                //validateFormBegin();
                //initFormBegin()
                 initFormBegin(JSON.stringify({
                     ""AllowEditControls"": ""{}""
                }));
            }


        });
        //表单初始化交互
        function initFormBegin(params) {
            //console.log(params)
            //alert(1);
            //编辑界面显示设备明细
            $.post('");

            
            #line 249 "..\..\Views\OaEquipUseMobile\info.cshtml"
               Write(Url.Action("json", "OaEquipStock", new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?EquipOrOffice=\" + \'");

            
            #line 249 "..\..\Views\OaEquipUseMobile\info.cshtml"
                                                                                              Write(Request.QueryString["EquipOrOffice"]);

            
            #line default
            #line hidden
WriteLiteral("\' + \"&EquipFormID=");

            
            #line 249 "..\..\Views\OaEquipUseMobile\info.cshtml"
                                                                                                                                                     Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("&EquipFormType=OaEquipUse\", function (_rData) {\r\n                //alert(_rData)\r" +
"\n                var rowData = JSON.parse(_rData);\r\n                console.log(" +
"rowData)\r\n                datas = rowData;\r\n                if (rowData.rows.len" +
"gth > 0) {\r\n                    $.each(rowData.rows, function (idx, ele) {\r\n    " +
"                    ele.EquipTotalCount = ele.Number;\r\n                    });\r\n" +
"                    var interText = doT.template($(\"#listTp2\").text())\r\n        " +
"            $(\"#gridListShow\").html(interText(rowData.rows));\r\n                 " +
"   if ($(\"#btnChooseEquipment\").hasClass(\'jq-text-disabled\')) {\r\n               " +
"         $(\".delete-btn\").addClass(\"jq-text-disabled\");\r\n                       " +
" $(\'input[name=\"EquipTotalCount\"]\').addClass(\"jq-text-disabled\").attr(\"readonly\"" +
", \"readonly\");\r\n                    }\r\n                }\r\n            })\r\n      " +
"      //alert(2);\r\n\r\n            var url = _webconfig.serverUrl + \'OaEquipment/j" +
"son?EquIds=\';\r\n            $.ajax({\r\n                type: \'POST\',\r\n            " +
"    url: url,\r\n                data: \'\',\r\n                success: function (dat" +
"a) {\r\n                    equipjsondatas = JSON.parse(data).rows\r\n              " +
"  }\r\n            })\r\n\r\n\r\n            params = params.replace(/[\\r\\n]/g, \"\");//去除" +
"换行\r\n            params = params.replace(/\\s/g, \"\");\r\n            var paramsObj =" +
" JSON.parse(params);\r\n            paramsObj.AllowEditControls = paramsObj.AllowE" +
"ditControls || \'-\';\r\n            paramsObj.AllowEditControls = paramsObj.AllowEd" +
"itControls\r\n                .replace(\"EquipmentID\", \"btnChooseEquipment\");\r\n\r\n  " +
"          //清除时间审批时，默认为1900-01-01改为空\r\n            var EquipLendDate = $(\'#EquipL" +
"endDate\').val(); //申请时间\r\n            var EquipBackDate = $(\'#EquipBackDate\').val" +
"(); //采购时间\r\n\r\n            if ((EquipLendDate == \'1900-01-01\')) {\r\n              " +
"  $(\'#EquipLendDate\').val(\'\');\r\n            }\r\n            if ((EquipBackDate ==" +
" \'1900-01-01\')) {\r\n                $(\'#EquipBackDate\').val(\'\');\r\n            }\r\n" +
"            //alert(3);\r\n\r\n            \r\n\r\n            //任务内容，替换换行符\r\n           " +
" var EquipLendNoteValue = $(\'#EquipLendNote\').val();\r\n            EquipLendNoteV" +
"alue = EquipLendNoteValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n           " +
" $(\'#EquipLendNote\').val(EquipLendNoteValue);\r\n\r\n\r\n            $(\'form :input\')." +
"addClass(\'jq-text-disabled\');\r\n            DomUtil.setCtrlsReadonly(paramsObj.Al" +
"lowEditControls, document.getElementById(\'OaEquipUseForm\'));\r\n\r\n            //al" +
"ert(4);\r\n            //☆ 对aui-input-block的特殊处理\r\n            if (((paramsObj.Allo" +
"wEditControls).indexOf(\'EquipLendNote\')) != -1) {\r\n                $(\'#EquipLend" +
"Note\').removeClass(\'jq-text-disabled\')\r\n                $(\"#EquipLendNote\").css(" +
"{\r\n                    overflow: \'auto\',\r\n                  \r\n                })" +
"\r\n            }\r\n            else {\r\n                $(\'#EquipLendNote\').removeC" +
"lass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n                $(\"#Equip" +
"LendNote\").css({\r\n                    overflow: \'auto\',\r\n                    bac" +
"kground: \'#efefef\',\r\n                    color: \'#898787\'\r\n                })\r\n\r" +
"\n            }\r\n\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQuMobile" +
") {\r\n                window.JinQuMobile.FormEnd();\r\n            }\r\n        }\r\n  " +
"      //表单验证交互\r\n        function validateFormBegin() {\r\n            //验证表单内容\r\n  " +
"     \r\n            var formVali = $(\'#OaEquipUseForm\').validate({\r\n             " +
"   rules: {\r\n                    EquipLendDate: \'required\',\r\n                   " +
" EquipBackDate: \'required\'\r\n                },\r\n                messages: {\r\n   " +
"                 EquipLendDate: \'请输入领用时间\',\r\n                    EquipBackDate: \'" +
"请输入归还时间\'\r\n                }\r\n            });\r\n            \r\n            var hasF" +
"loatNumber = 0;\r\n            var $gridTr = $(\"#gridListShow\").find(\"tr\");\r\n     " +
"      // console.log(equipjsondatas)\r\n            $.each($gridTr, function (inde" +
"x, ele) {\r\n                var number = $(ele).find(\"#EquipTotalCount\").val();\r\n" +
"                if (number.indexOf(\'.\') > 0) {\r\n                    $.alert(\'请确认" +
"设备数量，不能为小数\');\r\n                    hasFloatNumber = 1;\r\n                    retu" +
"rn false;\r\n                }\r\n                if (number == \'\') {\r\n             " +
"       $.alert(\'请输入设备数量！！！\');\r\n                    hasFloatNumber = 1;\r\n        " +
"            return false;\r\n                }\r\n              \r\n            })\r\n  " +
"          for (var i = 0; i < equipjsondatas.length; i++) {\r\n                var" +
" info = equipjsondatas[i];\r\n                $.each($gridTr, function (index, ele" +
") {\r\n                    var number = $(ele).find(\"#EquipTotalCount\").val();\r\n  " +
"                  var count = info.SJKC;\r\n                   \r\n                 " +
"   if (info.Id == $(ele).find(\"#EquipId_Id\").text()) {\r\n                        " +
"//alert(number + \'=\' + count)\r\n                        if (number > count) {\r\n  " +
"                          $.alert(\'领用数量超过库存量\');\r\n                            $(e" +
"le).find(\"#EquipTotalCount\").val(\'\');\r\n                            hasFloatNumbe" +
"r = 1;\r\n                            return false;\r\n                        }\r\n\r\n" +
"                    }\r\n                })\r\n            }\r\n            isValidate" +
" = formVali.form();\r\n          //  alert(hasFloatNumber)\r\n            if (hasFlo" +
"atNumber == 0) {\r\n                if (isValidate) {\r\n                    var gri" +
"dData = [];\r\n                    $.each($gridTr, function (index, ele) {\r\n      " +
"                  gridData.push({\r\n                            Id: -1,\r\n        " +
"                    EquipId: $(ele).find(\"#EquipId_Id\").text(),\r\n               " +
"             EquipNumber: $(ele).find(\"#EquipNumber\").text(),\r\n                 " +
"           EquipName: $(ele).find(\"#EquipName\").text(),\r\n                       " +
"     EquipModel: $(ele).find(\"#EquipModel\").text(),\r\n                           " +
" SJKC: $(ele).find(\"#E_SJKC\").val(),\r\n                            Number: $(ele)" +
".find(\"#EquipTotalCount\").val()\r\n                        })\r\n                   " +
" })\r\n                    if (gridData.length == 0) {\r\n                        $." +
"alert(\'请选择要领用的设备！\');\r\n                        return false;\r\n                   " +
" }\r\n                    $(\"#EquipId\").val(JSON.stringify(gridData));\r\n          " +
"          var formData = DomUtil.serialize(document.getElementById(\'OaEquipUseFo" +
"rm\'));//json or\r\n                    formData.EquipLendDate = $(\'#EquipLendDate\'" +
").val();\r\n                    formData.EquipBackDate = $(\'#EquipBackDate\').val()" +
";\r\n                    console.log(formData)\r\n                    //告诉移动端页面初始完完毕" +
"\r\n                    //alert(JSON.stringify(formData))\r\n                    if " +
"(window.JinQuMobile) {\r\n                        window.JinQuMobile.validateFormE" +
"nd(JSON.stringify(formData));\r\n                    }\r\n                }\r\n       " +
"     }\r\n           \r\n\r\n\r\n           \r\n           \r\n            \r\n        }\r\n\r\n  " +
"      //不走验证流程\r\n        function noValidateFormBegin(params) {\r\n          \r\n    " +
"        var gridData = [];\r\n            var $gridTr = $(\"#gridListShow\").find(\"t" +
"r\");\r\n            //console.log($gridTr)\r\n            $.each($gridTr, function (" +
"index, ele) {\r\n              \r\n                gridData.push({\r\n                " +
"    Id: $(ele).find(\"#E_Id\").val(),\r\n                    EquipId: $(ele).find(\"#" +
"EquipId_Id\").text(),\r\n                    EquipNumber: $(ele).find(\"#EquipNumber" +
"\").text(),\r\n                    EquipName: $(ele).find(\"#EquipName\").text(),\r\n  " +
"               \r\n                    EquipModel: $(ele).find(\"#EquipModel\").text" +
"(),\r\n                    Number: $(ele).find(\"#EquipTotalCount\").val()\r\n        " +
"        })\r\n            })\r\n            \r\n            $(\"#EquipId\").val(JSON.str" +
"ingify(gridData));\r\n            var formData = DomUtil.serialize(document.getEle" +
"mentById(\'OaEquipUseForm\'));//json or =&\r\n            formData.EquipLendDate = $" +
"(\'#EquipLendDate\').val();\r\n            formData.EquipBackDate = $(\'#EquipBackDat" +
"e\').val();\r\n            //console.log(formData)\r\n           // alert(JSON.string" +
"ify(formData))\r\n            //告诉移动端页面验证完毕\r\n            if (window.JinQuMobile) {" +
"\r\n                window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r" +
"\n            }\r\n        }\r\n\r\n        //选择设备\r\n        function EquipmentChooseBeg" +
"in() {\r\n            var url = _webconfig.serverUrl + \'OaEquipment/json?EquIds=\';" +
"\r\n            $.ajax({\r\n                type: \'POST\',\r\n                url: url," +
"\r\n                data: \'\',\r\n                success: function (data) {\r\n       " +
"             // alert(data)\r\n                    var appendEquipGetInfoData = JS" +
"ON.parse(data).rows\r\n                    console.log(appendEquipGetInfoData)\r\n  " +
"                  if (appendEquipGetInfoData.length > 0) {\r\n                    " +
"    var interText = doT.template($(\"#listTpl\").text())\r\n                        " +
"$(\"#gridList\").html(interText(appendEquipGetInfoData));\r\n                       " +
" $(\'body\').append(\'<div class=\"aui-mask\"></div>\');\r\n                        $(\"." +
"aui-dialog.aui-hidden\").removeClass(\'aui-hidden\');\r\n\r\n\r\n                        " +
"$(\'#gridList tr\').each(function (index, value) {\r\n                            va" +
"r trID = \'#tr_\' + index;\r\n                            $(\'.checkAll input[type=\"c" +
"heckbox\"]\').click(function () {\r\n                                $(trID).find(\'." +
"checkbox input[name=\"subBox\"]\').prop(\"checked\", this.checked)\r\n                 " +
"           })\r\n                            var allLen = $(\'.checkbox input[name=" +
"\"subBox\"]\').length;\r\n                            $(\'.checkbox input[name=\"subBox" +
"\"]\').each(function (index, value) {\r\n                                $(this).bin" +
"d(\'click\', function (i) {\r\n                                    var selectedLen =" +
" $(\'.checkbox input[name=\"subBox\"]:checked\').length;\r\n                          " +
"          if (selectedLen == allLen) {\r\n                                        " +
"$(\'.checkAll input[type=\"checkbox\"]\').prop(\"checked\", true)\r\n                   " +
"                 }\r\n                                    else {\r\n                " +
"                        $(\'.checkAll input[type=\"checkbox\"]\').prop(\"checked\", fa" +
"lse)\r\n                                    }\r\n                                })\r" +
"\n                            })\r\n                        })\r\n                   " +
" }\r\n                }\r\n            })\r\n        }\r\n        //信息弹出框---------------" +
"--------------------------------------------------------\r\n        function cance" +
"l() {\r\n            $(\'div\').removeClass(\"aui-mask\")\r\n            $(\'.aui-dialog\'" +
").addClass(\"aui-hidden\")\r\n        }\r\n        function confirm() {\r\n            v" +
"ar checkValueLength = $(\".checkbox input[name=\'subBox\']:checked\").length;\r\n\r\n   " +
"         if (checkValueLength > 0) {\r\n                $(\'div\').removeClass(\'aui-" +
"mask\');\r\n                $(\'.aui-dialog\').addClass(\"aui-hidden\");\r\n             " +
"   var selectArray = [];\r\n                $(\".checkbox input[name=\'subBox\']:chec" +
"ked\").closest(\"tr\").each(function (i, e) {\r\n                    var selectString" +
" = $(this).children().text().trim();\r\n                    var arr = selectString" +
".split(/[ |,]/)\r\n                    console.log(arr)\r\n                    for (" +
"var i = 0; i < arr.length; i++) {\r\n                        if (arr[i].length ===" +
" 0) {\r\n                            arr.splice(i, 1)\r\n                        }\r\n" +
"                    }\r\n                    selectArray.push({ EquipNumber: arr[0" +
"], EquipName: arr[1], EquipModel: arr[2], EquipTotalCount: arr[3], EquipId: arr[" +
"5] })\r\n                    console.log(selectArray)\r\n                    var app" +
"endselectArrayInfoData = selectArray;\r\n                    if (appendselectArray" +
"InfoData.length > 0) {\r\n                        var interText = doT.template($(\"" +
"#listTp2\").text())\r\n                        $(\"#gridListShow\").html(interText(ap" +
"pendselectArrayInfoData));\r\n\r\n                    }\r\n                })\r\n       " +
"     }\r\n            else {\r\n                $.alert(\'请选择设备\');\r\n            }\r\n  " +
"      }\r\n        function removeTrInfo(e) {\r\n            $(e).parent().parent()." +
"remove();\r\n        }\r\n\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591
