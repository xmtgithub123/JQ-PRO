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
    
    #line 2 "..\..\Views\IsoDWGZLXD\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoDWGZLXD/info.cshtml")]
    public partial class _Views_IsoDWGZLXD_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoDWGZLXD>
    {
        public _Views_IsoDWGZLXD_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var btnPer =");

            
            #line 4 "..\..\Views\IsoDWGZLXD\info.cshtml"
           Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral(@";

    var old_originalSteps = new Array();

    JQ.form.submitInit({
        formid: 'IsoDWGZLXDForm',//formid提交需要用到
        buttonTypes: ['close','exportForm'],//默认按钮
        docName: 'IsoDWGZLXD',
        ExportName: '对外工作联系单',
        beforeFormSubmit: function () {
        },
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            var _tempFrame = window.top.document.getElementById(""");

            
            #line 16 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                             Write(Request.QueryString["iframeID"]);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n            if (_tempFrame) {\r\n                _tempFrame.contentWindow.refr" +
"eshGrid();\r\n            }\r\n            _flowInstance.$form.parent().dialog(\"clos" +
"e\");\r\n        },\r\n        flow: {\r\n            flowNodeID: \"");

            
            #line 23 "..\..\Views\IsoDWGZLXD\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 24 "..\..\Views\IsoDWGZLXD\info.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 25 "..\..\Views\IsoDWGZLXD\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.IsoDWGZLXDFlow\",\r\n            refID: \"");

            
            #line 28 "..\..\Views\IsoDWGZLXD\info.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"IsoDWGZLXD\",\r\n            dataCreator: \"");

            
            #line 30 "..\..\Views\IsoDWGZLXD\info.cshtml"
                      Write(ViewBag.CurrEmpID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            getStepUsers: function (step, action) {\r\n\r\n\r\n            }, filte" +
"rSteps: function (originalSteps) {\r\n                debugger;\r\n                v" +
"ar json;\r\n                old_originalSteps = originalSteps.concat();\r\n         " +
"       var nOrder = 0;\r\n                if (this.stepOrder == 1) {\r\n            " +
"        var url = window.top.rootPath;\r\n                    var _mainProjId = $(" +
"\"#mainProjId\").val();\r\n                    var _projId = $(\"#projId\").val();\r\n  " +
"                  var _projPhaseId = $(\"#projPhaseId\").val();\r\n                 " +
"   var _desTaskGroupId = $(\"#desTaskGroupId\").val();\r\n                    $.ajax" +
"({\r\n                        doBackResult: false,\r\n                        url: u" +
"rl + \'Design/DesTask/jsonByArrange\',\r\n                        data: { mainProjId" +
": _mainProjId, projId: _projId, projPhaseId: _projPhaseId, desTaskGroupId: _desT" +
"askGroupId, type: 1 },\r\n                        async: false,\r\n                 " +
"       success: function (result) {\r\n                            result = JSON.p" +
"arse(result);\r\n                            if (result.Result == true) {\r\n       " +
"                         json = JSON.parse(result.Info);\r\n                      " +
"      }\r\n                        }\r\n                    });\r\n                }\r\n" +
"                else if (this.stepOrder == 2) {\r\n                    var url = w" +
"indow.top.rootPath;\r\n                    var _mainProjId = $(\"#mainProjId\").val(" +
");\r\n                    var _projId = $(\"#projId\").val();\r\n                    v" +
"ar _projPhaseId = $(\"#projPhaseId\").val();\r\n                    var _desTaskGrou" +
"pId = $(\"#desTaskGroupId\").val();\r\n                    $.ajax({\r\n               " +
"         doBackResult: false,\r\n                        url: url + \'Design/DesTas" +
"k/jsonByArrange\',\r\n                        data: { mainProjId: _mainProjId, proj" +
"Id: _projId, projPhaseId: _projPhaseId, desTaskGroupId: _desTaskGroupId, type: 2" +
" },\r\n                        async: false,\r\n                        success: fun" +
"ction (result) {\r\n                            result = JSON.parse(result);\r\n    " +
"                        if (result.Result == true) {\r\n                          " +
"      json = JSON.parse(result.Info);\r\n                            }\r\n          " +
"              }\r\n                    })\r\n                }\r\n                else" +
" if (this.stepOrder == 3) {\r\n                    var url = window.top.rootPath;\r" +
"\n                    var _mainProjId = $(\"#mainProjId\").val();\r\n                " +
"    var _projId = $(\"#projId\").val();\r\n                    var _projPhaseId = $(" +
"\"#projPhaseId\").val();\r\n                    var _desTaskGroupId = $(\"#desTaskGro" +
"upId\").val();\r\n                    $.ajax({\r\n                        doBackResul" +
"t: false,\r\n                        url: url + \'Design/DesTask/jsonByArrange\',\r\n " +
"                       data: { mainProjId: _mainProjId, projId: _projId, projPha" +
"seId: _projPhaseId, desTaskGroupId: _desTaskGroupId, type: 3 },\r\n               " +
"         async: false,\r\n                        success: function (result) {\r\n  " +
"                          result = JSON.parse(result);\r\n                        " +
"    if (result.Result == true) {\r\n                                json = JSON.pa" +
"rse(result.Info);\r\n                            }\r\n                        }\r\n   " +
"                 })\r\n                }\r\n                else if (this.stepOrder " +
"== 4) {\r\n\r\n                    var empId = $(\"#FProjEmpId\").val();\r\n            " +
"        var empName = $(\"#FProjEmpName\").textbox(\'getValue\');\r\n                 " +
"   if (empId == \"0\") {\r\n                        empId = $(\"#ProjEmpId\").val();\r\n" +
"                        empName = $(\"#ProjPhaseEmpName\").textbox(\'getValue\');\r\n " +
"                       nOrder = 6;\r\n                    }\r\n                    j" +
"son = { defaultChoosedUser: empId, users: [{ id: empId, name: empName }] };\r\n   " +
"             }\r\n                else if (this.stepOrder == 5) {\r\n               " +
"     var empId = $(\"#ProjEmpId\").val();\r\n                    var empName = $(\"#P" +
"rojPhaseEmpName\").textbox(\'getValue\');\r\n                    json = { defaultChoo" +
"sedUser: empId, users: [{ id: empId, name: empName }] };\r\n\r\n                }\r\n " +
"               debugger;\r\n                if (nOrder == 0) {\r\n                  " +
"  nOrder = this.stepOrder + 1;\r\n                }\r\n\r\n                var steps =" +
" Enumerable.From(old_originalSteps).Where(\"x => x.Order == \" + nOrder + \"\").Firs" +
"tOrDefault();\r\n                if (steps != undefined) {\r\n                    or" +
"iginalSteps.splice(0, originalSteps.length);\r\n                    if (json != un" +
"defined) {\r\n                        if (json.defaultChoosedUser != undefined) {\r" +
"\n                            steps.DefaultChoosedUser = json.defaultChoosedUser;" +
"\r\n                            for (var i = 0; i < json.users.length; i++) {\r\n   " +
"                             steps.Users.push({ ID: json.users[i].id, Name: json" +
".users[i].name });\r\n                            }\r\n                        } els" +
"e {\r\n                            steps.Users = new Array();\r\n                   " +
"         var userName = json.choosedUserNames.split(\",\");\r\n                     " +
"       var userId = json.choosedUsers.split(\",\");\r\n                            v" +
"ar users = Array();\r\n                            for (var i = 0; i < userName.le" +
"ngth; i++) {\r\n                                steps.Users.push({ ID: userId[i], " +
"Name: userName[i] });\r\n                            }\r\n                        }\r" +
"\n                    }\r\n                    originalSteps.push(steps);\r\n        " +
"        }\r\n            },\r\n            onLoaded: function () {\r\n                " +
"$(\"#stepOrder\").val(this.stepOrder);\r\n            }\r\n        },\r\n        onBefor" +
"e: function (resultArr) {\r\n            var bit=\"0\";\r\n            if($.inArray(\"a" +
"llDown\", btnPer)!=-1){\r\n                bit=\"1\";\r\n            }\r\n            res" +
"ultArr.push({ Key: \"Permission\", Value: bit});\r\n        }\r\n    });\r\n    function" +
" SelectProjectInfo() {\r\n        JQ.dialog.dialog({\r\n            title: \"选择项目信息\"," +
"\r\n            url: \'");

            
            #line 155 "..\..\Views\IsoDWGZLXD\info.cshtml"
             Write(Url.Action("ChooseProjectList1", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=FGS&TaskGroupId=0',
            width: '1024',
            height: '100%',
            JQID: 'ShowProjectTreeGridList',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }
    var _ProjCallBack = function (param) {
        $(""#mainProjId"").val(param[0].ParentId);
        $(""#projId"").val(param[0].Id);
        $(""#projPhaseId"").val(param[0].ProjPhaseId);
        $(""#desTaskGroupId"").val(param[0].TaskGroupId);
        $(""#CompanyID"").val(param[0].CompanyID);

        $(""#ProjNumber"").textbox('setValue', param[0].ProjNumber);
        $(""#ProjName"").textbox('setValue', param[0].ProjName);
        $(""#ProjPhaseName"").textbox('setValue', param[0].ProjPhaseName);
        $(""#ProjPhaseEmpName"").textbox('setValue', param[0].ProjPhaseEmpName);
        $(""#FProjEmpName"").textbox('setValue', param[0].FProjEmpName);
        $(""#AcceptUnit"").textbox('setValue', param[0].CustName);
    }

    $(function () {
        if(");

            
            #line 179 "..\..\Views\IsoDWGZLXD\info.cshtml"
       Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n\r\n            $(\"#mainProjId\").val(");

            
            #line 182 "..\..\Views\IsoDWGZLXD\info.cshtml"
                             Write(ViewBag.MainProjId );

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#projId\").val(");

            
            #line 183 "..\..\Views\IsoDWGZLXD\info.cshtml"
                        Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#projPhaseId\").val(");

            
            #line 184 "..\..\Views\IsoDWGZLXD\info.cshtml"
                             Write(Model.ProjPhaseId);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#desTaskGroupId\").val(");

            
            #line 185 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                Write(Model.DesTaskGroupId);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#CompanyID\").val(");

            
            #line 186 "..\..\Views\IsoDWGZLXD\info.cshtml"
                           Write(Model.CompanyID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n\r\n            $(\"#ProjEmpId\").val(");

            
            #line 188 "..\..\Views\IsoDWGZLXD\info.cshtml"
                            Write(ViewBag.ProjEmpId );

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#FProjEmpId\").val(");

            
            #line 189 "..\..\Views\IsoDWGZLXD\info.cshtml"
                             Write(ViewBag.FProjEmpId);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n    })\r\n\r\n</script>\r\n");

            
            #line 194 "..\..\Views\IsoDWGZLXD\info.cshtml"
 using (Html.BeginForm("save", "IsoDWGZLXD", FormMethod.Post, new { @area = "Iso", @id = "IsoDWGZLXDForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 196 "..\..\Views\IsoDWGZLXD\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 196 "..\..\Views\IsoDWGZLXD\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:20px;font-weight:bold;margin-top:1" +
"0px;\"");

WriteLiteral(">\r\n            <a>对外工作联系单</a>\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:left;margin-left:20px;margin-top:10px;\"");

WriteLiteral(">\r\n            表<label>");

            
            #line 202 "..\..\Views\IsoDWGZLXD\info.cshtml"
               Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label> <input");

WriteLiteral(" name=\"TableNumber\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9375), Tuple.Create("\"", 9401)
            
            #line 202 "..\..\Views\IsoDWGZLXD\info.cshtml"
              , Tuple.Create(Tuple.Create("", 9383), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 9383), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:right;margin-right:20px;margin-top:10px;margin-bottom:10px;\"");

WriteLiteral(">\r\n            编号<input");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9614), Tuple.Create("\"", 9635)
            
            #line 205 "..\..\Views\IsoDWGZLXD\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 9622), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 9622), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目编号\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" style=\"width:60%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9896), Tuple.Create("\"", 9921)
            
            #line 212 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                    , Tuple.Create(Tuple.Create("", 9904), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 9904), false)
);

WriteLiteral(" data-options=\"required:true,readonly:true\"");

WriteLiteral(" />\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnChoiceProject\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectProjectInfo()\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">选择项目信息</a>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10358), Tuple.Create("\"", 10381)
            
            #line 217 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                               , Tuple.Create(Tuple.Create("", 10366), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 10366), false)
);

WriteLiteral(" data-options=\"required:true,readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">阶段</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10677), Tuple.Create("\"", 10705)
            
            #line 224 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                         , Tuple.Create(Tuple.Create("", 10685), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 10685), false)
);

WriteLiteral(" data-options=\"required:true,readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">主题</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ZhuTi\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 230 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                                                                                                                 Write(Model.ZhuTi);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">发文单位</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CompanyName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11270), Tuple.Create("\"", 11298)
            
            #line 236 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                    , Tuple.Create(Tuple.Create("", 11278), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyName
            
            #line default
            #line hidden
, 11278), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">电  话</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CompanyTel\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11500), Tuple.Create("\"", 11527)
            
            #line 240 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                   , Tuple.Create(Tuple.Create("", 11508), Tuple.Create<System.Object, System.Int32>(ViewBag.CompanyTel
            
            #line default
            #line hidden
, 11508), false)
);

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">主送</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ZhuSong\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11787), Tuple.Create("\"", 11809)
            
            #line 248 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 11795), Tuple.Create<System.Object, System.Int32>(Model.ZhuSong
            
            #line default
            #line hidden
, 11795), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">拟稿人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DrafterName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 12012), Tuple.Create("\"", 12040)
            
            #line 252 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                     , Tuple.Create(Tuple.Create("", 12020), Tuple.Create<System.Object, System.Int32>(ViewBag.DrafterName
            
            #line default
            #line hidden
, 12020), false)
);

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <Tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(" rowspan=\"3\"");

WriteLiteral(">抄送</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" rowspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ChaoSong\"");

WriteLiteral(" style=\"width:99%;height:120px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 259 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                                                                                                                     Write(Model.ChaoSong);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目负责人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ProjPhaseEmpName\"");

WriteLiteral(" id=\"ProjPhaseEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 12624), Tuple.Create("\"", 12657)
            
            #line 263 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 12632), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjPhaseEmpName
            
            #line default
            #line hidden
, 12632), false)
);

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </Tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">副项目负责人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FProjEmpName\"");

WriteLiteral(" id=\"FProjEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 12940), Tuple.Create("\"", 12969)
            
            #line 269 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                        , Tuple.Create(Tuple.Create("", 12948), Tuple.Create<System.Object, System.Int32>(ViewBag.FProjEmpName
            
            #line default
            #line hidden
, 12948), false)
);

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">签发时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"SignTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 13221), Tuple.Create("\"", 13244)
            
            #line 275 "..\..\Views\IsoDWGZLXD\info.cshtml"
                           , Tuple.Create(Tuple.Create("", 13229), Tuple.Create<System.Object, System.Int32>(Model.SignTime
            
            #line default
            #line hidden
, 13229), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">内容及附件名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"Contents\"");

WriteLiteral(" style=\"width:99%;height:120px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 281 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                                                                                                                     Write(Model.Contents);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">接受单位</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"AcceptUnit\"");

WriteLiteral(" id=\"AcceptUnit\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 13781), Tuple.Create("\"", 13806)
            
            #line 288 "..\..\Views\IsoDWGZLXD\info.cshtml"
                         , Tuple.Create(Tuple.Create("", 13789), Tuple.Create<System.Object, System.Int32>(Model.AcceptUnit
            
            #line default
            #line hidden
, 13789), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">接收人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"AcceptPerson\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 14037), Tuple.Create("\"", 14064)
            
            #line 294 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                    , Tuple.Create(Tuple.Create("", 14045), Tuple.Create<System.Object, System.Int32>(Model.AcceptPerson
            
            #line default
            #line hidden
, 14045), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">接收时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"AcceptTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 14260), Tuple.Create("\"", 14285)
            
            #line 298 "..\..\Views\IsoDWGZLXD\info.cshtml"
                             , Tuple.Create(Tuple.Create("", 14268), Tuple.Create<System.Object, System.Int32>(Model.AcceptTime
            
            #line default
            #line hidden
, 14268), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 310 "..\..\Views\IsoDWGZLXD\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 310 "..\..\Views\IsoDWGZLXD\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "IsoDWGZLXD";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 315 "..\..\Views\IsoDWGZLXD\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 315 "..\..\Views\IsoDWGZLXD\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"mainProjId\"");

WriteLiteral(" name=\"mainProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 14994), Tuple.Create("\"", 15021)
            
            #line 320 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15002), Tuple.Create<System.Object, System.Int32>(ViewBag.MainProjId
            
            #line default
            #line hidden
, 15002), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projId\"");

WriteLiteral(" name=\"projId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 15077), Tuple.Create("\"", 15098)
            
            #line 321 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15085), Tuple.Create<System.Object, System.Int32>(Model.ProjID
            
            #line default
            #line hidden
, 15085), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projPhaseId\"");

WriteLiteral(" name=\"projPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 15164), Tuple.Create("\"", 15190)
            
            #line 322 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15172), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 15172), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"desTaskGroupId\"");

WriteLiteral(" name=\"desTaskGroupId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 15262), Tuple.Create("\"", 15300)
            
            #line 323 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15270), Tuple.Create<System.Object, System.Int32>(Request.Params["taskGroupId"]
            
            #line default
            #line hidden
, 15270), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyID\"");

WriteLiteral(" name=\"CompanyID\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stepOrder\"");

WriteLiteral(" name=\"stepOrder\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjEmpId\"");

WriteLiteral(" name=\"ProjEmpId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 15504), Tuple.Create("\"", 15530)
            
            #line 326 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15512), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjEmpId
            
            #line default
            #line hidden
, 15512), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"FProjEmpId\"");

WriteLiteral(" name=\"FProjEmpId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 15594), Tuple.Create("\"", 15623)
            
            #line 327 "..\..\Views\IsoDWGZLXD\info.cshtml"
, Tuple.Create(Tuple.Create("", 15602), Tuple.Create<System.Object, System.Int32>(ViewBag.FProjEmpName
            
            #line default
            #line hidden
, 15602), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsMultiSelect\"");

WriteLiteral(" name=\"IsMultiSelect\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" />\r\n");

            
            #line 329 "..\..\Views\IsoDWGZLXD\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591