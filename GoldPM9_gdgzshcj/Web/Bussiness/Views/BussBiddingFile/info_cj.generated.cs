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
    
    #line 2 "..\..\Views\BussBiddingFile\info_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingFile/info_cj.cshtml")]
    public partial class _Views_BussBiddingFile_info_cj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.Project>
    {
        public _Views_BussBiddingFile_info_cj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteAttribute("src", Tuple.Create(" src=\"", 56), Tuple.Create("\"", 116)
            
            #line 3 "..\..\Views\BussBiddingFile\info_cj.cshtml"
, Tuple.Create(Tuple.Create("", 62), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jQueryGantt/libs/i18nJs.js")
            
            #line default
            #line hidden
, 62), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'BussBiddingFileForm',//formid提交需要用到
            buttonTypes: ['close'],//默认按钮
            beforeFormSubmit: function () {

                var _Id = $(""#Id"").val();
                var _ProjName = $(""#ProjName"").val();

                var dt1 = $(""#DatePlanStart"").datebox(""getValue"");
                var dt2 = $(""#DatePlanFinish"").datebox(""getValue"");
                var t1 = strparser(dt1);
                var t2 = strparser(dt2);
                if (t1 <= t2) {
                    var result = calculateDays(dt1, dt2);
                    $(""#Duration"").val(result);
                }
                else {
                    JQ.dialog.error(""开始时间不能大于完成时间！"");
                }
            },
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                var _tempFrame=window.top.document.getElementById(""");

            
            #line 27 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                               Write(Request.QueryString["iframeID"]);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n                if(_tempFrame){\r\n                    _tempFrame.contentWindo" +
"w.refreshGrid();\r\n                }\r\n                JQ.dialog.dialogClose();//关" +
"闭窗体放在最后一步执行，以避免找不到事件源\r\n            },flow:{\r\n                flowNodeID: \"");

            
            #line 33 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                         Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                flowMultiSignID: \"");

            
            #line 34 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                              Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                url: \"");

            
            #line 35 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                  Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                instance: \"_flowInstance\",\r\n                processor: \"Bussi" +
"ness,Bussiness.FlowProcessor.BussBiddingFile\",\r\n                onInit: function" +
" () {\r\n                    _flowInstance = this;\r\n                },\r\n          " +
"      refID: \"");

            
            #line 41 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                refTable: \"BussBiddingFile_CJ\",\r\n                dataCreator:" +
" \"");

            
            #line 43 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                          Write(ViewBag.CreatorEmpId);

            
            #line default
            #line hidden
WriteLiteral(@""",
                getStepUsers: function (step, action) {
                    if (step.Order == 2) {
                        var swlxrId = $(""#BusinessEmpId"").combotree(""getValue"");
                        var swlxr = $(""#BusinessEmpId"").combotree(""getText"");
                        var jslxrId = $(""#ProjEmpId"").combotree(""getValue"");
                        var jslxr = $(""#ProjEmpId"").combotree(""getText"");
                        var usersId = """";
                        var usersName = """";
                        if (swlxrId != """") {
                            usersId += swlxrId.split('-')[0] + "","";
                            usersName += swlxr + "","";
                        }
                        if (jslxrId != """") {
                            usersId += jslxrId.split('-')[0];
                            usersName += jslxr;
                        }

                        if (usersId!="""") {
                            return { choosedUserNames: usersName, choosedUsers: usersId };
                        }
                    }
                }
            }
        });


        if(");

            
            #line 70 "..\..\Views\BussBiddingFile\info_cj.cshtml"
      Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n            $(\"#BiddingBatch\").val(\'");

            
            #line 72 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                               Write(ViewBag.BiddingBatch);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#BiddingNumber\").val(\'");

            
            #line 73 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                Write(ViewBag.BiddingNumber);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#PackageNumber\").val(\'");

            
            #line 74 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                Write(ViewBag.PackageNumber);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#EngineeringName\").val(\'");

            
            #line 75 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                  Write(ViewBag.EngineeringName);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#BiddingId\").val(");

            
            #line 76 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                           Write(Model.ColAttType7);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#BiddingInfoID\").val(");

            
            #line 77 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                               Write(Model.ColAttType8);

            
            #line default
            #line hidden
WriteLiteral(");\r\n            $(\"#Duration\").val(");

            
            #line 78 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                          Write(Model.ColAttType11);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n    });\r\n\r\n    function SelectBiddingInfoPackage() {\r\n        JQ.d" +
"ialog.dialog({\r\n            title: \"选择投标信息\",\r\n            url: \'");

            
            #line 85 "..\..\Views\BussBiddingFile\info_cj.cshtml"
             Write(Url.Action("ListInfo", "BussBiddingInfo", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=CJ&IsFilter=0',
            width: '1024',
            height: '100%',
            JQID: 'BiddingInfoInfoGrid',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }

    var _BussBiddingPackageBack = function (row) {
        debugger;
        $(""#BiddingId"").val(row.Id);
        $(""#EngineeringName"").textbox('setText', row.EngineeringName);
        $(""#BiddingNumber"").textbox('setText', row.BiddingNumber);
        $(""#ProjEmpId"").combotree(""setValue"",row.BiddingManageID+""-""+row.BiddingManageDeptId);
        $(""#BusinessEmpId"").combotree(""setValue"",row.Delegator+""-""+row.DelegatorDeptId);
    }
    JQ.combotree.selEmp({ id: 'ProjEmpId',width:""98%"" });
    JQ.combotree.selEmp({ id: 'BusinessEmpId',width:""98%"" });
    JQ.combotree.selEmp({ id: 'TechnologyEmpId',width:""98%"" });


    if(");

            
            #line 107 "..\..\Views\BussBiddingFile\info_cj.cshtml"
  Write(ViewData["ProjEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("!=0&&");

            
            #line 107 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                              Write(ViewData["ProjEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral("!=0)\r\n    {\r\n        debugger;\r\n        $(\"#ProjEmpId\").combotree(\'setValue\',");

            
            #line 110 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                        Write(ViewData["ProjEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("+\"-\"+ ");

            
            #line 110 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                                     Write(ViewData["ProjEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n    if(");

            
            #line 112 "..\..\Views\BussBiddingFile\info_cj.cshtml"
  Write(ViewData["BusinessEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("!=0&&");

            
            #line 112 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                  Write(ViewData["BusinessEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral("!=0)\r\n    {\r\n        $(\"#BusinessEmpId\").combotree(\'setValue\',");

            
            #line 114 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                            Write(ViewData["BusinessEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("+\"-\"+ ");

            
            #line 114 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                                             Write(ViewData["BusinessEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n    if(");

            
            #line 116 "..\..\Views\BussBiddingFile\info_cj.cshtml"
  Write(ViewData["TechnologyEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("!=0&&");

            
            #line 116 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                    Write(ViewData["TechnologyEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral("!=0)\r\n    {\r\n        $(\"#TechnologyEmpId\").combotree(\'setValue\',");

            
            #line 118 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                              Write(ViewData["TechnologyEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("+\"-\"+ ");

            
            #line 118 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                                                 Write(ViewData["TechnologyEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n\r\n    function onSelect(date) {\r\n\r\n        var dt1 = $(\"#DatePlanStart" +
"\").datebox(\"getValue\");\r\n        var dt2 = $(\"#DatePlanFinish\").datebox(\"getValu" +
"e\");\r\n        if (dt1 != \"\" && dt2 != \"\") {\r\n            var t1 = strparser(dt1)" +
";\r\n            var t2 = strparser(dt2);\r\n            if (t1 <= t2) {\r\n          " +
"      var result = calculateDays(dt1, dt2);\r\n                $(\"#Duration\").val(" +
"result);\r\n            }\r\n            else {\r\n                JQ.dialog.error(\"开始" +
"时间不能大于完成时间！\");\r\n            }\r\n        }\r\n    }\r\n    function calculateDays(date" +
"Begin, dateEnd) {\r\n        var sDate = new Date(dateBegin);\r\n        var eDate =" +
" new Date(dateEnd);\r\n        var distance = sDate.distanceInWorkingDays(eDate);\r" +
"\n        return distance;\r\n    };\r\n\r\n\r\n    Date.prototype.distanceInWorkingDays " +
"= function (toDate) {\r\n        var pos = new Date(this.getTime());\r\n        pos." +
"setHours(23, 59, 59, 999);\r\n        var days = 0;\r\n        var nd = new Date(toD" +
"ate.getTime());\r\n        nd.setHours(23, 59, 59, 999);\r\n        var end = nd.get" +
"Time();\r\n        while (pos.getTime() <= end) {\r\n            days = days + (isHo" +
"liday(pos) ? 0 : 1);\r\n            pos.setDate(pos.getDate() + 1);\r\n        }\r\n  " +
"      return days;\r\n    };\r\n\r\n    function strparser(s)\r\n    {\r\n        if (!s) " +
"return new Date();\r\n        var ss = s.split(\'-\');\r\n        var y = parseInt(ss[" +
"0], 10);\r\n        var m = parseInt(ss[1], 10);\r\n        var d = parseInt(ss[2], " +
"10);\r\n        if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {\r\n            return new" +
" Date(y, m - 1, d);\r\n        } else {\r\n            return new Date();\r\n        }" +
"\r\n    }\r\n</script>\r\n");

            
            #line 173 "..\..\Views\BussBiddingFile\info_cj.cshtml"
 using (Html.BeginForm("save", "BussBiddingFile", FormMethod.Post, new { @area = "Bussiness", @id = "BussBiddingFileForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 175 "..\..\Views\BussBiddingFile\info_cj.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 175 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"CJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"BiddingNumber\"");

WriteLiteral(" name=\"BiddingNumber\"");

WriteLiteral(" style=\"width:50%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,editable:false\"");

WriteLiteral(" />\r\n                <a");

WriteLiteral(" id=\"selectBidding\"");

WriteLiteral(" name=\"selectBidding\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectBiddingInfoPackage()\"");

WriteLiteral(">选择投标项目</a>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"EngineeringName\"");

WriteLiteral(" name=\"EngineeringName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,editable:false\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标文件名</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8108), Tuple.Create("\"", 8131)
            
            #line 192 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 8116), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 8116), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">技术联系人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ProjEmpId\"");

WriteLiteral(" name=\"ProjEmpId\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 8310), Tuple.Create("\"", 8336)
            
            #line 198 "..\..\Views\BussBiddingFile\info_cj.cshtml"
, Tuple.Create(Tuple.Create("", 8320), Tuple.Create<System.Object, System.Int32>(Model.ProjEmpId
            
            #line default
            #line hidden
, 8320), false)
);

WriteLiteral(" data-options=\"required:true,editable: false,readonly:true\"");

WriteLiteral("></select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">商务联系人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"BusinessEmpId\"");

WriteLiteral(" name=\"BusinessEmpId\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 8560), Tuple.Create("\"", 8588)
            
            #line 202 "..\..\Views\BussBiddingFile\info_cj.cshtml"
, Tuple.Create(Tuple.Create("", 8570), Tuple.Create<System.Object, System.Int32>(Model.ColAttType9
            
            #line default
            #line hidden
, 8570), false)
);

WriteLiteral(" data-options=\"required:true,editable: false,readonly:true\"");

WriteLiteral("></select>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">计划开始时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"DatePlanStart\"");

WriteLiteral(" name=\"DatePlanStart\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8913), Tuple.Create("\"", 8941)
            
            #line 208 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                              , Tuple.Create(Tuple.Create("", 8921), Tuple.Create<System.Object, System.Int32>(Model.DatePlanStart
            
            #line default
            #line hidden
, 8921), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">计划完成时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"DatePlanFinish\"");

WriteLiteral(" name=\"DatePlanFinish\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9173), Tuple.Create("\"", 9202)
            
            #line 212 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 9181), Tuple.Create<System.Object, System.Int32>(Model.DatePlanFinish
            
            #line default
            #line hidden
, 9181), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注说明</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 218 "..\..\Views\BussBiddingFile\info_cj.cshtml"
           Write(Html.TextAreaFor(model => Model.ProjNote, new { @style = "width:98%;height:80px" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 226 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                
            
            #line default
            #line hidden
            
            #line 226 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "Project";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 231 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 231 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"BiddingId\"");

WriteLiteral(" name=\"BiddingId\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"BiddingInfoID\"");

WriteLiteral(" name=\"BiddingInfoID\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Duration\"");

WriteLiteral(" name=\"Duration\"");

WriteLiteral(" />\r\n");

            
            #line 239 "..\..\Views\BussBiddingFile\info_cj.cshtml"
                    }
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
