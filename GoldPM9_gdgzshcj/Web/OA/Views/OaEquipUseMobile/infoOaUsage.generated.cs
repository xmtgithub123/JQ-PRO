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
    
    #line 6 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaEquipUseMobile/infoOaUsage.cshtml")]
    public partial class _Views_OaEquipUseMobile_infoOaUsage_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaEquipUse>
    {
        public _Views_OaEquipUseMobile_infoOaUsage_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 2 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
  
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
            
            #line 84 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
, Tuple.Create(Tuple.Create("", 1917), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 1917), false)
);

WriteLiteral(" />\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EquipId\"");

WriteLiteral(" name=\"EquipId\"");

WriteLiteral(" />\r\n          ");

WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;办公用品</label>\r\n                <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"btnChooseEquipment\"");

WriteLiteral(" namne=\"btnChooseEquipment\"");

WriteLiteral(" class=\"aui-btn aui-btn-block aui-btn-success p-0\"");

WriteLiteral(" style=\"width:40%;\"");

WriteLiteral(" onclick=\"EquipmentChooseBegin();\"");

WriteLiteral(" value=\"选择用品\"");

WriteLiteral(" />\r\n                <br />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EquipId\"");

WriteLiteral(" name=\"EquipId\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EquipOrOffice\"");

WriteLiteral(" name=\"EquipOrOffice\"");

WriteLiteral(" value=\'1\'");

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

WriteLiteral(">办公用品编号</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">办公用品名称</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">规格</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">领用数量</th>\r\n                                <th");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">操作</th>\r\n                            </tr>\r\n                        </thead>\r\n  " +
"                      <tbody");

WriteLiteral(" id=\"gridListShow\"");

WriteLiteral(" style=\"font-size:12px;\"");

WriteLiteral("></tbody>\r\n                    </table>\r\n                </div>\r\n\r\n              " +
"  <script");

WriteLiteral(" id=\"listTp2\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                    {{~it:appendselectArrayInfoData:index}}
                    <tr>
                        <td>
                            <span class=""hidden"" name=""E_Id"" id=""E_Id"">{{=appendselectArrayInfoData.Id}}</span>
                            <span class=""hidden"" name=""EquipId_Id"" id=""EquipId_Id"">{{=appendselectArrayInfoData.EquipId}}</span>
                            ");

WriteLiteral("\r\n                            <span id=\"EquipNumber\" name=\"EquipNumber\">{{=append" +
"selectArrayInfoData.EquipNumber}}</span>\r\n                        </td>\r\n       " +
"                 <td>\r\n                            ");

WriteLiteral("\r\n                            <span id=\"EquipName\" name=\"EquipName\">{{=appendsele" +
"ctArrayInfoData.EquipName}}</span>\r\n                        </td>\r\n             " +
"           <td>\r\n                            ");

WriteLiteral(@"
                            <span id=""EquipModel"" name=""EquipModel"">{{=appendselectArrayInfoData.EquipModel}}</span>
                        </td>
                        <td>
                            <input type=""number"" id=""EquipTotalCount"" name=""EquipTotalCount"" value=""{{=appendselectArrayInfoData.EquipTotalCount}}"" style=""width:60px;padding:0;text-align:center"" />
                          ");

WriteLiteral(@"
                            <input type=""hidden"" id=""Equip_sjkc"" name=""Equip_sjkc"" value=""{{=appendselectArrayInfoData.SJKC}}"" style=""width:60px;padding:0;text-align:center"" />
                        </td>
                        <td style=""margin:0 auto;text-align:center"">
                            <input type=""button"" name=""DeleteRowBtn"" id=""DeleteRowBtn_{{=index}}"" class=""delete-btn aui-btn aui-btn-block aui-btn-success p-0"" value=""删除"" onclick=""removeTrInfo(this);"" style=""width:auto;margin:0;font-size:12px;"" />
                            ");

WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n                    {" +
"{~}}\r\n                </script>\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;领用时间</label>\r\n                <input");

WriteLiteral(" name=\"EquipLendDate\"");

WriteLiteral(" id=\"EquipLendDate\"");

WriteLiteral(" type=\"date\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6193), Tuple.Create("\"", 6244)
            
            #line 143 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
                    , Tuple.Create(Tuple.Create("", 6201), Tuple.Create<System.Object, System.Int32>(Model.EquipLendDate.ToString("yyyy-MM-dd")
            
            #line default
            #line hidden
, 6201), false)
);

WriteLiteral(" />\r\n            </div>\r\n         \r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;备注</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"EquipLendNote\"");

WriteLiteral(" id=\"EquipLendNote\"");

WriteLiteral(" style=\"width:98%;overflow:auto;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 148 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
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

WriteLiteral(">确定</div>\r\n            </div>\r\n        </div>\r\n\r\n\r\n\r\n        </div>\r\n\r\n\r\n\r\n    <s" +
"cript>\r\n        var _webconfig = {\r\n            serverUrl: \'");

            
            #line 226 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n        };\r\n        var EquipArray = new Array();\r\n        var chooseUrl = \'");

            
            #line 229 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
                    Write(Url.Action("EquipmentOaChoose", "OaEquipUse", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral(@"';
        var datas,equipdatas;
        $(function () {
            if (window.JinQuMobile == undefined) {
                initFormBegin(JSON.stringify({
                    ""AllowEditControls"": ""{}""
                }));
               // initFormBegin()
            }


        });
        //表单初始化交互
        function initFormBegin(params) {
           // alert(params);

            var url = _webconfig.serverUrl + '/oa/OaEquipment/jsonOaChoose?EquIds=' + EquipArray.join(',') + """";
            $.ajax({
                type: 'POST',
                url: url,
                data: '',
                success: function (data) {
                    console.log(JSON.parse(data))
                    equipdatas = JSON.parse(data)
                }
            })


            //编辑界面显示设备明细
            $.post('");

            
            #line 258 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
               Write(Url.Action("json", "OaEquipStock", new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\' + \"?EquipFormID=");

            
            #line 258 "..\..\Views\OaEquipUseMobile\infoOaUsage.cshtml"
                                                                                       Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("&EquipFormType=OaEquipUseOfficeFlow\", function (_rData) {\r\n                var ro" +
"wData = JSON.parse(_rData);\r\n                datas = rowData;\r\n               //" +
" console.log(rowData)\r\n                if (rowData.rows.length > 0) {\r\n         " +
"           $.each(rowData.rows, function (idx, ele) {\r\n                        e" +
"le.EquipTotalCount = ele.Number;\r\n                    });\r\n                    f" +
"or (var i = 0; i < rowData.rows.length; i++) {\r\n                        var rows" +
" = rowData.rows[i];\r\n                        for (var j = 0; j < equipdatas.rows" +
".length; j++) {\r\n                            var cols = equipdatas.rows[j];\r\n   " +
"                         if (rows.EquipId == cols.Id) {\r\n                       " +
"         rows.SJKC = cols.SJKC;\r\n                               // console.log(c" +
"ols.SJKC)\r\n                            }\r\n                        }\r\n           " +
"         }\r\n                    console.log(rowData.rows)\r\n                    v" +
"ar interText = doT.template($(\"#listTp2\").text())\r\n                    $(\"#gridL" +
"istShow\").html(interText(rowData.rows));\r\n\r\n\r\n                    if ($(\"#btnCho" +
"oseEquipment\").hasClass(\'jq-text-disabled\')) {\r\n                        $(\".dele" +
"te-btn\").addClass(\"jq-text-disabled\");\r\n                        $(\'input[name=\"E" +
"quipTotalCount\"]\').addClass(\"jq-text-disabled\").attr(\"readonly\", \"readonly\");\r\n " +
"                   }\r\n                }\r\n            })\r\n            //alert(2);" +
"\r\n            params = params.replace(/[\\r\\n]/g, \"\");//去除换行\r\n            params " +
"= params.replace(/\\s/g, \"\");\r\n            var paramsObj = JSON.parse(params);\r\n " +
"           paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n  " +
"          paramsObj.AllowEditControls = paramsObj.AllowEditControls\r\n           " +
"     .replace(\"btn\", \"btnChooseEquipment\");\r\n\r\n\r\n\r\n            //清除时间审批时，默认为1900" +
"-01-01改为空\r\n            var EquipLendDate = $(\'#EquipLendDate\').val(); //申请时间\r\n\r\n" +
"            if ((EquipLendDate == \'1900-01-01\')) {\r\n                $(\'#EquipLen" +
"dDate\').val(\'\');\r\n            }\r\n\r\n            //任务内容，替换换行符\r\n            var Equ" +
"ipGetNoteValue = $(\'#EquipGetNote\').val();\r\n            //alert(EquipGetNoteValu" +
"e);\r\n            if (typeof (EquipGetNoteValue) != \'undefined\' && EquipGetNoteVa" +
"lue.length > 0) {\r\n                EquipGetNoteValue = EquipGetNoteValue.replace" +
"(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            }\r\n            $(\'#EquipGetNote\'" +
").val(EquipGetNoteValue);\r\n\r\n           \r\n\r\n\r\n\r\n            $(\'form :input\').add" +
"Class(\'jq-text-disabled\');\r\n            DomUtil.setCtrlsReadonly(paramsObj.Allow" +
"EditControls, document.getElementById(\'OaEquipUseForm\'));\r\n\r\n            //告诉移动端" +
"页面初始完完毕\r\n            if (window.JinQuMobile) {\r\n                window.JinQuMobi" +
"le.FormEnd();\r\n            }\r\n        }\r\n        //表单验证交互\r\n        function vali" +
"dateFormBegin() {\r\n            //验证表单内容\r\n            var isValidate = false;\r\n  " +
"          var formVali = $(\'#OaEquipUseForm\').validate({\r\n                //rule" +
"s: {\r\n                //    EquipGetCustomer: \'required\'\r\n                //},\r\n" +
"                //messages: {\r\n                //    EquipGetCustomer: \'请输入外委单位\'" +
"\r\n                //}\r\n            });\r\n            isValidate = formVali.form()" +
";\r\n            var $gridTr = $(\"#gridListShow\").find(\"tr\");\r\n            var has" +
"FloatNumber = 0;\r\n            $.each($gridTr, function (index, ele) {\r\n         " +
"       // 归还数量应小于等于应归还数量\r\n                var kc = parseInt($(ele).find(\"#Equip_" +
"sjkc\").val());\r\n                var lysl = parseInt($(ele).find(\"#EquipTotalCoun" +
"t\").val());\r\n                var number = $(ele).find(\"#EquipTotalCount\").val();" +
"\r\n                if (lysl > kc) {\r\n                    isValidate = false;\r\n   " +
"                 $.alert(\'领用数量不允许超过库存数量！\');\r\n                    hasFloatNumber " +
"= 1;\r\n                    return false;\r\n                }\r\n\r\n                if" +
" (number == \'\' || lysl == \'0\') {\r\n                    $.alert(\'请输入办公用品数量！！！\');\r\n" +
"                    hasFloatNumber = 1;\r\n                    return false;\r\n    " +
"            }\r\n                if (number.indexOf(\'.\') > 0) {\r\n                 " +
"   $.alert(\'请确认用品数量，不能为小数\');\r\n                    hasFloatNumber = 1;\r\n         " +
"           return false;\r\n                }\r\n                if (number < 0) {\r\n" +
"                    $.alert(\'请确认用品数量，不能为负数\');\r\n                    hasFloatNumbe" +
"r = 1;\r\n                    return false;\r\n                }\r\n            })\r\n  " +
"          if (hasFloatNumber == 0) {\r\n                if (isValidate) {\r\n\r\n     " +
"               var gridData = [];\r\n\r\n                    $.each($gridTr, functio" +
"n (index, ele) {\r\n                        gridData.push({\r\n                     " +
"       Id: $(ele).find(\"#E_Id\").text(),\r\n                            EquipId: $(" +
"ele).find(\"#EquipId_Id\").text(),\r\n                            EquipNumber: $(ele" +
").find(\"#EquipNumber\").text(),\r\n                            EquipName: $(ele).fi" +
"nd(\"#EquipName\").text(),\r\n                            EquipModel: $(ele).find(\"#" +
"EquipModel\").text(),\r\n                            Number: $(ele).find(\"#EquipTot" +
"alCount\").val(),\r\n                        })\r\n                    })\r\n\r\n\r\n      " +
"              $(\"#EquipId\").val(JSON.stringify(gridData));\r\n                    " +
"//console.log($(\"#EquipId\").val())\r\n                    var formData = DomUtil.s" +
"erialize(document.getElementById(\'OaEquipUseForm\'));//json or\r\n                 " +
"   formData.EquipId = $(\"#EquipId\").val();\r\n                    //alert(JSON.str" +
"ingify(formData))\r\n                    console.log(formData)\r\n                  " +
"  //告诉移动端页面初始完完毕\r\n                    if (window.JinQuMobile) {\r\n               " +
"         window.JinQuMobile.validateFormEnd(JSON.stringify(formData));\r\n        " +
"            }\r\n                }\r\n            }\r\n           \r\n            \r\n    " +
"    }\r\n\r\n        //不走验证流程\r\n        function noValidateFormBegin(params) {\r\n     " +
"       var gridData = [];\r\n            var $gridTr = $(\"#gridListShow\").find(\"tr" +
"\");\r\n            $.each($gridTr, function (index, ele) {\r\n                gridDa" +
"ta.push({\r\n                    Id: $(ele).find(\"#E_Id\").text(),\r\n               " +
"     EquipId: $(ele).find(\"#EquipId_Id\").text(),\r\n                    EquipNumbe" +
"r: $(ele).find(\"#EquipNumber\").text(),\r\n                    EquipName: $(ele).fi" +
"nd(\"#EquipName\").text(),\r\n                    EquipModel: $(ele).find(\"#EquipMod" +
"el\").text(),\r\n                    Number: $(ele).find(\"#EquipTotalCount\").val()," +
"\r\n                })\r\n            })\r\n\r\n            $(\"#EquipId\").val(JSON.strin" +
"gify(gridData));\r\n            var formData = DomUtil.serialize(document.getEleme" +
"ntById(\'OaEquipUseForm\'));//json or\r\n            formData.EquipId = $(\"#EquipId\"" +
").val();\r\n           // alert(JSON.stringify(formData))\r\n            //告诉移动端页面验证" +
"完毕\r\n            if (window.JinQuMobile) {\r\n                window.JinQuMobile.va" +
"lidateFormEnd(JSON.stringify(formData));\r\n            }\r\n        }\r\n\r\n        //" +
"选择设备\r\n\r\n\r\n\r\n        function EquipmentChooseBegin() {\r\n            //var url = _" +
"webconfig.serverUrl + \'OaEquipment/json?EquIds=\';\r\n            var url = _webcon" +
"fig.serverUrl + \'/oa/OaEquipment/jsonOaChoose?EquIds=\' + EquipArray.join(\',\') + " +
"\"\";\r\n            $.ajax({\r\n                type: \'POST\',\r\n                url: u" +
"rl,\r\n                data: \'\',\r\n                success: function (data) {\r\n    " +
"                console.log(data)\r\n                    var appendEquipGetInfoDat" +
"a = JSON.parse(data).rows\r\n                    if (appendEquipGetInfoData.length" +
" > 0) {\r\n                        var interText = doT.template($(\"#listTpl\").text" +
"())\r\n                        $(\"#gridList\").html(interText(appendEquipGetInfoDat" +
"a));\r\n                        $(\'body\').append(\'<div class=\"aui-mask\"></div>\');\r" +
"\n                        $(\".aui-dialog.aui-hidden\").removeClass(\'aui-hidden\');\r" +
"\n\r\n\r\n                        $(\'#gridList tr\').each(function (index, value) {\r\n " +
"                           var trID = \'#tr_\' + index;\r\n                         " +
"   $(\'.checkAll input[type=\"checkbox\"]\').click(function () {\r\n                  " +
"              $(trID).find(\'.checkbox input[name=\"subBox\"]\').prop(\"checked\", thi" +
"s.checked)\r\n                            })\r\n                            var allL" +
"en = $(\'.checkbox input[name=\"subBox\"]\').length;\r\n                            $(" +
"\'.checkbox input[name=\"subBox\"]\').each(function (index, value) {\r\n              " +
"                  $(this).bind(\'click\', function (i) {\r\n                        " +
"            var selectedLen = $(\'.checkbox input[name=\"subBox\"]:checked\').length" +
";\r\n                                    if (selectedLen == allLen) {\r\n           " +
"                             $(\'.checkAll input[type=\"checkbox\"]\').prop(\"checked" +
"\", true)\r\n                                    }\r\n                               " +
"     else {\r\n                                        $(\'.checkAll input[type=\"ch" +
"eckbox\"]\').prop(\"checked\", false)\r\n                                    }\r\n      " +
"                          })\r\n                            })\r\n                  " +
"      })\r\n                    }\r\n                }\r\n            })\r\n        }\r\n\r" +
"\n\r\n\r\n        //信息弹出框------------------------------------------------------------" +
"-----------\r\n        function cancel() {\r\n            $(\'div\').removeClass(\"aui-" +
"mask\")\r\n            $(\'.aui-dialog\').addClass(\"aui-hidden\")\r\n        }\r\n        " +
"function confirm() {\r\n            var checkValueLength = $(\".checkbox input[name" +
"=\'subBox\']:checked\").length;\r\n\r\n            if (checkValueLength > 0) {\r\n       " +
"         $(\'div\').removeClass(\'aui-mask\');\r\n                $(\'.aui-dialog\').add" +
"Class(\"aui-hidden\");\r\n                var selectArray = [];\r\n                $(\"" +
".checkbox input[name=\'subBox\']:checked\").closest(\"tr\").each(function (i, e) {\r\n " +
"                   var selectString = $(this).children().text().trim();\r\n       " +
"             var arr = selectString.split(/[ |,]/)\r\n                    console." +
"log(arr)\r\n                    for (var i = 0; i < arr.length; i++) {\r\n          " +
"              if (arr[i].length === 0) {\r\n                            arr.splice" +
"(i, 1)\r\n                        }\r\n                    }\r\n                    se" +
"lectArray.push({ EquipNumber: arr[0], EquipName: arr[1], EquipModel: arr[2], SJK" +
"C: arr[4], EquipTotalCount:arr[4], EquipId: arr[5] })\r\n                    conso" +
"le.log(selectArray)\r\n                    var appendselectArrayInfoData = selectA" +
"rray;\r\n                    if (appendselectArrayInfoData.length > 0) {\r\n        " +
"                var interText = doT.template($(\"#listTp2\").text())\r\n            " +
"            $(\"#gridListShow\").html(interText(appendselectArrayInfoData));\r\n\r\n  " +
"                  }\r\n                })\r\n            }\r\n            else {\r\n    " +
"            $.alert(\'请选择设备\');\r\n            }\r\n        }\r\n        function remove" +
"TrInfo(e) {\r\n            $(e).parent().parent().remove();\r\n        }\r\n\r\n    </sc" +
"ript>\r\n");

});

        }
    }
}
#pragma warning restore 1591