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
    
    #line 5 "..\..\Views\OaStampUseMobile\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaStampUseMobile/info.cshtml")]
    public partial class _Views_OaStampUseMobile_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaStampUse>
    {
        public _Views_OaStampUseMobile_info_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaStampUseMobile\info.cshtml"
  
    Layout = "~/Areas/Core/Views/Mobile/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n\r\n    .aui-btn {\r\n        padding: 10px 15px;\r\n        font-size: 13px;\r\n    }" +
"\r\n\r\n    .aui-btn-row:after {\r\n        border: 0;\r\n    }\r\n\r\n    .p-15 {\r\n        " +
"padding: 15px;\r\n    }\r\n\r\n\r\n    .set-disabled {\r\n        pointer-events: none !im" +
"portant\r\n    }\r\n\r\n    .error {\r\n        color: red;\r\n        font-size: 14px;\r\n " +
"       display: inline-block;\r\n    }\r\n\r\n    .hidden {\r\n        display: none;\r\n " +
"   }\r\n\r\n    .aui-input-hook {\r\n        line-height: 34px;\r\n        text-align: l" +
"eft;\r\n        padding-left: 20px;\r\n        background: #efefef;\r\n    }\r\n\r\n    .a" +
"ui-display-hook {\r\n        display: none;\r\n        width: 30%;\r\n        float: r" +
"ight;\r\n        font-size: 13px;\r\n        line-height: 33px;\r\n        margin-righ" +
"t: 6px;\r\n        padding: 3px 6px;\r\n    }\r\n\r\n    .aui-input-width {\r\n        wid" +
"th: 98%;\r\n    }\r\n\r\n    .hiddenShow {\r\n        height: 0;\r\n    }\r\n\r\n    .aui-inpu" +
"t-row label.aui-input-addon {\r\n        min-width: 90px;\r\n    }\r\n\r\n    .set-selec" +
"ted-icon select {\r\n        float: right;\r\n        margin-right: 5px;\r\n        bo" +
"rder: 0;\r\n        margin-bottom: 0;\r\n    }\r\n\r\n    .set-selected-icon i {\r\n      " +
"  position: absolute;\r\n        right: 25px;\r\n        top: 15px;\r\n    }\r\n\r\n    .a" +
"ui-input-row-position {\r\n        position: relative;\r\n        display: table;\r\n " +
"       padding: 6px 0;\r\n    }\r\n\r\n    .aui-input-row label.aui-input-addon {\r\n   " +
"     font-size: 14px;\r\n    }\r\n\r\n    .jq-text-disabled {\r\n        color: #898787;" +
"\r\n        width: 98%;\r\n        pointer-events: none;\r\n        background-color: " +
"#efefef !important;\r\n        border: 0;\r\n        font-size: 13px;\r\n    }\r\n     ." +
"aui-dialog {\r\n         z-index: 999;\r\n     }\r\n     .error-border {\r\n         bor" +
"der: 1px solid red !important;\r\n     }\r\n     \r\n</style>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" class=\"aui-content\"");

WriteLiteral(">\r\n        <form");

WriteLiteral(" id=\"OaStampUseMobileForm\"");

WriteLiteral(" class=\"aui-form\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;编号</label>\r\n                <input");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2208), Tuple.Create("\"", 2229)
            
            #line 114 "..\..\Views\OaStampUseMobile\info.cshtml"
      , Tuple.Create(Tuple.Create("", 2216), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 2216), false)
);

WriteLiteral("  />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"Id\"");

WriteLiteral(" name=\"Id\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2351), Tuple.Create("\"", 2368)
            
            #line 117 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2359), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 2359), false)
);

WriteLiteral(" />\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;申请人</label>\r\n                <input");

WriteLiteral(" name=\"CreatorEmpName_EmpName\"");

WriteLiteral(" id=\"CreatorEmpName_EmpName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input set-disabled\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2569), Tuple.Create("\"", 2598)
            
            #line 119 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 2577), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 2577), false)
);

WriteLiteral(" placeholder=\"请输入申请人\"");

WriteLiteral(" />\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;是否外借</label>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"iswjValue\"");

WriteLiteral(" id=\"iswjValue\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2829), Tuple.Create("\"", 2848)
            
            #line 124 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 2837), Tuple.Create<System.Object, System.Int32>(Model.IsWJ
            
            #line default
            #line hidden
, 2837), false)
);

WriteLiteral("/>\r\n                <select");

WriteLiteral(" id=\"IsWJ\"");

WriteLiteral(" name=\"IsWJ\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" >\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                </select>\r\n                <input");

WriteLiteral(" name=\"StampUseDate\"");

WriteLiteral(" id=\"StampUseDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3159), Tuple.Create("\"", 3186)
            
            #line 129 "..\..\Views\OaStampUseMobile\info.cshtml"
                     , Tuple.Create(Tuple.Create("", 3167), Tuple.Create<System.Object, System.Int32>(Model.StampUseDate
            
            #line default
            #line hidden
, 3167), false)
);

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;项目</label>\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"multiple:false\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" style=\"width:100%;height:40px\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3465), Tuple.Create("\"", 3523)
            
            #line 133 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                                      , Tuple.Create(Tuple.Create("", 3473), Tuple.Create<System.Object, System.Int32>(@Model.ProjId==0 ? "" :@Model.ProjId.ToString()
            
            #line default
            #line hidden
, 3473), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Words\"");

WriteLiteral(" name=\"Words\"");

WriteLiteral(" />\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral("  id=\"projNameText\"");

WriteLiteral(" style=\"width:98%;display: inline-block;\"");

WriteLiteral(">");

            
            #line 135 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                                                                    Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjIDName\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral("  style=\"width:60%;text-overflow: ellipsis;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3842), Tuple.Create("\"", 3867)
            
            #line 137 "..\..\Views\OaStampUseMobile\info.cshtml"
                                            , Tuple.Create(Tuple.Create("", 3850), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjName
            
            #line default
            #line hidden
, 3850), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjI\"");

WriteLiteral(" name=\"ProjI\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3933), Tuple.Create("\"", 3954)
            
            #line 138 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 3941), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 3941), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjIName\"");

WriteLiteral(" name=\"ProjIName\"");

WriteAttribute("value", Tuple.Create("value=\"", 4028), Tuple.Create("\"", 4050)
            
            #line 139 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4035), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 4035), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" name=\"ProjNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4126), Tuple.Create("\"", 4153)
            
            #line 140 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 4134), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjNumber
            
            #line default
            #line hidden
, 4134), false)
);

WriteLiteral(" />\r\n                ");

WriteLiteral("\r\n            </div>\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;印章名称</label>\r\n                <div");

WriteLiteral(" class=\"aui-input-block\"");

WriteLiteral(" style=\"width:60%;display: inline-block;\"");

WriteLiteral(" id=\"stampNameText\"");

WriteLiteral(">");

            
            #line 146 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                                                                     Write(Model.StampID > 0 ? Model.FK_OaStampUse_StampID.StampName : "");

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                <input");

WriteLiteral(" name=\"StampName\"");

WriteLiteral(" id=\"StampName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create("  value=\"", 4794), Tuple.Create("\"", 4862)
            
            #line 147 "..\..\Views\OaStampUseMobile\info.cshtml"
                , Tuple.Create(Tuple.Create("", 4803), Tuple.Create<System.Object, System.Int32>(Model.StampID>0?Model.FK_OaStampUse_StampID.StampName:""
            
            #line default
            #line hidden
, 4803), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" name=\"StampID\"");

WriteLiteral(" id=\"StampID\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5011), Tuple.Create("\"", 5033)
            
            #line 148 "..\..\Views\OaStampUseMobile\info.cshtml"
          , Tuple.Create(Tuple.Create("", 5019), Tuple.Create<System.Object, System.Int32>(Model.StampID
            
            #line default
            #line hidden
, 5019), false)
);

WriteLiteral(" style=\"width:60%;margin-right:15px;text-overflow: ellipsis;\"");

WriteLiteral(" />\r\n                ");

WriteLiteral("\r\n                <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" id=\"selectProjNameBtn\"");

WriteLiteral(" namne=\"selectProjNameBtn\"");

WriteLiteral(" class=\"aui-btn aui-btn-block aui-btn-success p-0\"");

WriteLiteral(" style=\"width:32%;float:right;margin-right:15px;\"");

WriteLiteral(" onclick=\"SetStampSelectBegin();\"");

WriteLiteral(" value=\"选择印章\"");

WriteLiteral(" />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;申请理由</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"StampEmpesult\"");

WriteLiteral(" id=\"StampEmpesult\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 154 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                                                                                                                                   Write(Model.StampEmpesult);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n\r\n\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(" id=\"IsJSL\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;份数</label>\r\n                <input");

WriteLiteral(" name=\"FS\"");

WriteLiteral(" id=\"FS\"");

WriteLiteral(" type=\"number\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6051), Tuple.Create("\"", 6068)
            
            #line 160 "..\..\Views\OaStampUseMobile\info.cshtml"
, Tuple.Create(Tuple.Create("", 6059), Tuple.Create<System.Object, System.Int32>(Model.FS
            
            #line default
            #line hidden
, 6059), false)
);

WriteLiteral(" />\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;文件签收人</label>\r\n                <input");

WriteLiteral(" name=\"WJQSR\"");

WriteLiteral(" id=\"WJQSR\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"aui-input\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6224), Tuple.Create("\"", 6244)
            
            #line 162 "..\..\Views\OaStampUseMobile\info.cshtml"
    , Tuple.Create(Tuple.Create("", 6232), Tuple.Create<System.Object, System.Int32>(Model.WJQSR
            
            #line default
            #line hidden
, 6232), false)
);

WriteLiteral("  />\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"aui-input-row\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" class=\"aui-input-addon\"");

WriteLiteral(">&nbsp;&nbsp;备注</label>\r\n                <textarea");

WriteLiteral(" rows=\"6\"");

WriteLiteral(" name=\"remark\"");

WriteLiteral(" id=\"remark\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"aui-input\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 166 "..\..\Views\OaStampUseMobile\info.cshtml"
                                                                                                                                                     Write(Model.remark);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </div>\r\n            \r\n            ");

WriteLiteral("\r\n        </form>\r\n\r\n        <div");

WriteLiteral(" class=\"aui-dialog aui-hidden\"");

WriteLiteral(" id=\"dialog\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-dialog-header\"");

WriteLiteral(">选择印章</div>\r\n            <div");

WriteLiteral(" class=\"aui-dialog-body aui-text-left\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"aui-card aui-noborder\"");

WriteLiteral(">\r\n                    <ul");

WriteLiteral(" class=\"aui-list-view aui-self-list-view\"");

WriteLiteral(" id=\"gridList\"");

WriteLiteral("></ul>\r\n                    <!--模板-->\r\n                    <script");

WriteLiteral(" id=\"listTpl\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                        {{~it:appendStampInfoData:index}}
                        <li class=""aui-list-view-cell aui-im"" tapmode
                            data-uid=""{{=appendStampInfoData.Id}}"">
                            <div class=""radio"">
                                ");

WriteLiteral(@"
                                <input type=""radio"" name=""optionsRadios"" id=""{{=appendStampInfoData.Id}}"" value=""{{=appendStampInfoData.Id}}_{{=appendStampInfoData.StampStatusID}}_{{=appendStampInfoData.StampName}}_{{=appendStampInfoData.StampTypeID}}"" />
                            </div>
                            <div class=""aui-img-body"">
                                <div>
                                    <label style=""display:inline""><b>印章名称：</b>{{=appendStampInfoData.StampName == '' ? '未填写': appendStampInfoData.StampName}}</label>
                                    <span class=""aui-badge aui-badge-{{=appendStampInfoData.StampStatusID == '使用中' ? 'success' : 'warning'}} pull-right"">{{=appendStampInfoData.StampStatusID}}</span>
                                </div>
                               ");

WriteLiteral(@"
                                <label><b>印章类型：</b>{{=appendStampInfoData.StampTypeID}}</label>
                                <br />
                                <span><b>保管人：</b>{{=appendStampInfoData.KeepEmpName}}</span>
                            </div>
                        </li>
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

WriteLiteral(">确定</div>\r\n            </div>\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"aui-dialog aui-hidden\"");

WriteLiteral(" id=\"dialog_pro\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"aui-dialog-header\"");

WriteLiteral(">选择项目</div>\r\n            <div");

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
                                    <th>项目编号</th>
                                    <th>项目名称</th>
                                    <th>项目阶段</th>
                                </tr>
                            </thead>
                            <tbody");

WriteLiteral(" id=\"gridList_pro\"");

WriteLiteral("></tbody>\r\n                        </table>\r\n                    </div>\r\n        " +
"            <!--模板-->\r\n                    <script");

WriteLiteral(" id=\"listTpl_pro\"");

WriteLiteral(" type=\"text/x-dot-template\"");

WriteLiteral(@">
                        {{~it:appendInfoData:index}}

                        <tr id=""tr_{{=index}}"">

                            <td>
                                <div class=""checkbox"">
                                    <input type=""checkbox"" name=""subBox"" value="""" />
                                </div>
                            </td>
                            <td>
                                {{=appendInfoData.ProjNumber}}
                            </td>
                            <td>
                                {{=appendInfoData.ProjName}}
                            </td>
                            <td>
                                {{=appendInfoData.PhaseName}}
                            </td>
                        
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

WriteLiteral(" onclick=\"confirm_pro()\"");

WriteLiteral(">确定</div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var _webconfig = {\r\n            serverUrl: \'");

            
            #line 264 "..\..\Views\OaStampUseMobile\info.cshtml"
                   Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral(@"'
        };
        $(function () {
            if (window.JinQuMobile == undefined) {
                //initFormBegin(JSON.stringify({
                //    ""AllowEditControls"": ""{}""
                //}));
               // initFormBegin();
            }

        })

        //页面初始化
        function initFormBegin(params) {

            $('#IsWJ').val($('#iswjValue').val())

            //借用类型
            $('#StampBorrowType').prepend(""<option disabled selected value>--- 请选择借用类型 ---</option>"");
            $('#StampBorrowType').change(function () {
                $(this).removeClass('error-border')
            })

            var StartDate = $('#StartDate').val();
            var EndDate = $('#EndDate').val();
            if ((StartDate == '1900-01-01')) {
                $('#StartDate').val('');
            }
            if ((EndDate == '1900-01-01')) {
                $('#EndDate').val('');
            }

            if ('");

            
            #line 296 "..\..\Views\OaStampUseMobile\info.cshtml"
            Write(ViewBag.StampType);

            
            #line default
            #line hidden
WriteLiteral("\' == \"技术类\") {\r\n                $(\"#IsJSL\").show();\r\n            } else {\r\n       " +
"         $(\"#IsJSL\").hide();\r\n            }\r\n            //去除换行\r\n            par" +
"ams = params.replace(/[\\r\\n]/g, \"\");\r\n            params = params.replace(/\\s/g," +
" \"\");\r\n\r\n            var paramsObj = JSON.parse(params);\r\n            //按钮替换\r\n  " +
"          paramsObj.AllowEditControls = paramsObj.AllowEditControls || \'-\';\r\n   " +
"         paramsObj.AllowEditControls = paramsObj.AllowEditControls.replace(\"Stam" +
"pID\", \"selectProjNameBtn\").replace(\'ProjID\',\'setProjNameBtn\');\r\n\r\n\r\n            " +
"//申请内容，替换换行符\r\n            var StampEmpesultValue = $(\'#StampEmpesult\').val();\r\n " +
"           StampEmpesultValue = StampEmpesultValue.replace(/[\\r\\n]/g, \"\").replac" +
"e(/\\s/g, \"\")\r\n            $(\'#StampEmpesult\').val(StampEmpesultValue);\r\n        " +
"    //备注，替换换行符\r\n            var remarkValue = $(\'#remark\').val();\r\n            r" +
"emarkValue = remarkValue.replace(/[\\r\\n]/g, \"\").replace(/\\s/g, \"\")\r\n            " +
"$(\'#remark\').val(remarkValue);\r\n\r\n            $(\'#StampBorrowType\').val($(\'#Stam" +
"pBorrowType_i\').val());\r\n            //设置表单不可编辑样式及只读\r\n            $(\'form :input" +
"\').addClass(\'jq-text-disabled\').attr(\"readonly\", \"readonly\");\r\n            DomUt" +
"il.setCtrlsReadonly(paramsObj.AllowEditControls, document.getElementById(\'OaStam" +
"pUseMobileForm\'));\r\n\r\n\r\n            \r\n\r\n            if (paramsObj.AllowEditContr" +
"ols == \"{}\") {\r\n                //单独设置申请人不可编辑\r\n                $(\'#CreatorEmpNam" +
"e_EmpName\').addClass(\'jq-text-disabled\');\r\n\r\n                $(\'#StampEmpesult,#" +
"remark\').removeClass(\'jq-text-disabled\');\r\n                $(\"#StampEmpesult,#re" +
"mark\").css({\r\n                    overflow: \'auto\',\r\n                })\r\n       " +
"     }\r\n            else {\r\n\r\n                $(\'#StampEmpesult,#remark\').remove" +
"Class(\'jq-text-disabled\').attr(\"readonly\", \"readonly\")\r\n                $(\"#Stam" +
"pEmpesult,#remark\").css({\r\n                    overflow: \'auto\',\r\n              " +
"      background: \'#efefef\',\r\n                    color: \'#898787\'\r\n            " +
"    })\r\n            }\r\n            //告诉移动端页面初始完完毕\r\n            if (window.JinQuM" +
"obile) {\r\n                window.JinQuMobile.FormEnd();\r\n            }\r\n        " +
"}\r\n\r\n        /*-----------------------------------------------------------------" +
"-----------------------------*/\r\n        //表单验证交互\r\n\r\n        function validateFo" +
"rmBegin() {\r\n            var formVali = $(\'#OaStampUseMobileForm\').validate({\r\n " +
"               rules: {\r\n\r\n                    StampID: \'required\',//项目名称\r\n     " +
"               StampEmpesult: {\r\n                        required: false,\r\n     " +
"                   maxlength: 500\r\n                    },\r\n                    S" +
"tampBorrowType:\'required\'\r\n\r\n                },\r\n                messages: {\r\n\r\n" +
"                    StampID: \'请输入印章名称\', //项目名称\r\n                    StampEmpesul" +
"t: {\r\n                        maxlength: \'内容长度必须介于0-500之间\'\r\n                    " +
"},\r\n                    StampBorrowType: \'请选择借用类型\'\r\n                }\r\n         " +
"   });\r\n\r\n            if ($(\'#StampName\').val() == \'\') {\r\n                $.aler" +
"t(\'请选择印章！\')\r\n                return false;\r\n            }\r\n\r\n            // 份数只能" +
"为正整数\r\n            var count = $(\'#FS\').val();\r\n            if (count < 0 || !cou" +
"nt || count.indexOf(\'.\') > 0)\r\n            {\r\n                $.alert(\'份数只能为正整数！" +
"\')\r\n                return false;\r\n            }\r\n\r\n            isValidate = for" +
"mVali.form();\r\n            if (isValidate) {\r\n                var formData = Dom" +
"Util.serialize(document.getElementById(\'OaStampUseMobileForm\'));//json or =&\r\n  " +
"              console.log(JSON.stringify(formData))\r\n                //告诉移动端页面初始" +
"完完毕\r\n                if (window.JinQuMobile) {\r\n                    window.JinQu" +
"Mobile.validateFormEnd(JSON.stringify(formData));\r\n                }\r\n          " +
"  }\r\n        }\r\n\r\n        /*----------------------------------------------------" +
"------------------------------------------*/\r\n        //不走验证流程\r\n        function" +
" noValidateFormBegin() {\r\n            var formData = DomUtil.serialize(document." +
"getElementById(\'OaStampUseMobileForm\'));//json or =&\r\n            //告诉移动端页面验证完毕\r" +
"\n            if (window.JinQuMobile) {\r\n                window.JinQuMobile.valid" +
"ateFormEnd(JSON.stringify(formData));\r\n            }\r\n        }\r\n\r\n        /*---" +
"--------------------------------------------------------------------------------" +
"-----------*/\r\n        //项目负责人 - 选择印章\r\n\r\n        function SetStampSelectBegin() " +
"{\r\n\r\n            $(\'#dialog_pro\').hide();\r\n            $(\'#dialog\').show();\r\n\r\n " +
"          // $(body).css(\'background-color\',\'red\')\r\n            var url = _webco" +
"nfig.serverUrl + \'oa/OaStampUse/FilterJson\';\r\n            $.ajax({\r\n            " +
"    type: \'POST\',\r\n                url: url,\r\n                data: \'\',\r\n       " +
"         success: function (data) {\r\n                    console.log(JSON.parse(" +
"data))\r\n                    var appendStampInfoData = JSON.parse(data).rows;\r\n  " +
"                  if (appendStampInfoData.length > 0) {\r\n                       " +
" var interText = doT.template($(\"#listTpl\").text())\r\n\r\n\r\n                       " +
" $(\"#gridList\").html(interText(appendStampInfoData));\r\n                        $" +
"(\'body\').append(\'<div class=\"aui-mask\"></div>\');\r\n                        $(\".au" +
"i-dialog.aui-hidden\").removeClass(\'aui-hidden\')\r\n                    }\r\n        " +
"            else {\r\n                        $.alert(\'暂无数据\');\r\n                  " +
"      $(\'#gridList\').html(\'\');\r\n                    }\r\n                }\r\n      " +
"      })\r\n\r\n        }\r\n\r\n        //项目 - 选择项目\r\n        function SetProjectSelectB" +
"egin() {\r\n            //alert(1)\r\n            var selemp = {\r\n                pr" +
"ojI: $(\'#ProjI\').val(),\r\n                ProjIName: $(\'#ProjIName\').val()\r\n     " +
"       }\r\n            if (window.JinQuMobile) {\r\n                window.JinQuMob" +
"ile.ProjectSelectBegin(JSON.stringify(selemp), \'SetProjectSelectEnd\');\r\n        " +
"    }\r\n        }\r\n        function SetProjectSelectEnd(emp) {\r\n            //ale" +
"rt(emp)\r\n            emp = JSON.parse(emp);\r\n            $(\"#ProjI\").val(emp.Id)" +
"\r\n            $(\'#ProjIName\').val(emp.ProjName);\r\n            $(\'#projNameText\')" +
".text(emp.ProjName)\r\n        }\r\n\r\n        //信息弹出框-------------------------------" +
"----------------------------------------\r\n        function cancel() {\r\n         " +
"   $(\'div\').removeClass(\"aui-mask\")\r\n            $(\'.aui-dialog\').addClass(\"aui-" +
"hidden\")\r\n        }\r\n        function confirm() {\r\n            var radioValueLen" +
"gth = $(\".radio input[type=\'radio\']:checked\").length;\r\n            if (radioValu" +
"eLength > 0) {\r\n                $(\'div\').removeClass(\'aui-mask\');\r\n             " +
"   $(\'.aui-dialog\').addClass(\"aui-hidden\");\r\n                var radioValue = $(" +
"\".radio input[type=\'radio\']:checked\").val();\r\n                console.log(radioV" +
"alue)\r\n\r\n                //var carId = $(\".radio input[type=\'radio\']:checked\").a" +
"ttr(\'id\');\r\n                var radioValue = radioValue.split(\'_\');\r\n           " +
"     console.log(radioValue)\r\n                if (radioValue[1] != \'使用中\') {\r\n   " +
"                 $.alert(\"该印章暂时不可用\");\r\n                    return false;\r\n      " +
"          }\r\n                if (radioValue[3] == \'技术类\') {\r\n                    " +
"$(\"#IsJSL\").show();\r\n                }\r\n                else {\r\n                " +
"    $(\"#IsJSL\").hide();\r\n                }\r\n                $(\'#StampID\').val(ra" +
"dioValue[0]);\r\n                $(\'#StampName\').val(radioValue[2])\r\n             " +
"   $(\'#stampNameText\').text(radioValue[2])\r\n\r\n            }\r\n            else {\r" +
"\n                $.alert(\'请选择印章\');\r\n            }\r\n\r\n        }\r\n    </script>\r\n");

});

        }
    }
}
#pragma warning restore 1591