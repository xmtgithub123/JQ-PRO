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
    
    #line 2 "..\..\Views\IsoZYZDJYB\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoZYZDJYB/info.cshtml")]
    public partial class _Views_IsoZYZDJYB_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoZYZDJYB>
    {
        public _Views_IsoZYZDJYB_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'IsoZYZDJYBForm',//formid提交需要用到
        docName: 'IsoZYZDJYB',
        ExportName: '专业指导纪要表',
        buttonTypes: ['close', 'exportForm'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }, flow: {
            flowNodeID: """);

            
            #line 13 "..\..\Views\IsoZYZDJYB\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 14 "..\..\Views\IsoZYZDJYB\info.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 15 "..\..\Views\IsoZYZDJYB\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.IsoZYZDJYBFlow\",\r\n            refID: \"");

            
            #line 18 "..\..\Views\IsoZYZDJYB\info.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"IsoZYZDJYB\",\r\n            dataCreator: \"");

            
            #line 20 "..\..\Views\IsoZYZDJYB\info.cshtml"
                      Write(ViewBag.CurrEmpID);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        },onBefore: function (resultArr) {\r\n            resultArr.push({ Key: " +
"\"Permission\", Value: ");

            
            #line 22 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                  Write(ViewBag.Permission);

            
            #line default
            #line hidden
WriteLiteral(" });\r\n        }\r\n    });\r\n\r\n    $(function () {\r\n        SetCheck(\"IsPS\", \'");

            
            #line 27 "..\..\Views\IsoZYZDJYB\info.cshtml"
                     Write(Model.IsReview);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n        SetCheck(\"IsYZ\", \'");

            
            #line 28 "..\..\Views\IsoZYZDJYB\info.cshtml"
                     Write(Model.IsValidate);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n        SetCheck(\"IsTY\", \'");

            
            #line 29 "..\..\Views\IsoZYZDJYB\info.cshtml"
                     Write(Model.IsTYGD);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n    });\r\n\r\n    //选择项目\r\n    function SelectMainProject() {\r\n        JQ.dialog" +
".dialog({\r\n            title: \"选择项目\",\r\n            url: \'");

            
            #line 36 "..\..\Views\IsoZYZDJYB\info.cshtml"
             Write(Url.Action("ChooseProjectList1", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=&TaskGroupId=\' + $(\"#ProjId\").val(),\r\n            width: \'1024\',\r\n  " +
"          height: \'100%\',\r\n            JQID: \'ProjTable\',\r\n            JQLoading" +
"Type: \'datagrid\',\r\n            iconCls: \'fa fa-list\',\r\n        });\r\n    }\r\n\r\n   " +
" //确定选择回调\r\n    var _ProjCallBack = function (row) {\r\n        var data = row[0];\r" +
"\n        $(\"#ProjId\").val(data.Id);\r\n        $(\"#ProjName\").textbox(\"setValue\", " +
"data.ProjName);\r\n        $(\"#ProjNumber\").textbox(\"setValue\", data.ProjNumber);\r" +
"\n        $(\"#ProjPhaseName\").textbox(\"setValue\",data.ProjPhaseName);\r\n        $(" +
"\"#ProjPhaseId\").val(data.ProjPhaseId);\r\n    }\r\n\r\n    //选中控件，隐藏控件，id，name，是否单选\r\n " +
"   function Checked(ckControl, hidCon, value, valName, isOnlyName) {\r\n        if" +
" ($(\"#\" + ckControl).attr(\"checked\")) {\r\n            if (isOnlyName != undefined" +
") {\r\n                $(\':checkbox[name=\' + isOnlyName + \']\').removeAttr(\'checked" +
"\');\r\n                $(\"#\" + ckControl).attr(\'checked\', \'checked\');\r\n           " +
" }\r\n            switch (hidCon) {\r\n                default:\r\n                   " +
" $(\"#\" + hidCon).val(value);\r\n                    break;\r\n            }\r\n       " +
" } else {\r\n            switch (hidCon) {\r\n                default:\r\n            " +
"        $(\"#\" + hidCon).val(\"\");\r\n                    break;\r\n            }\r\n   " +
"     }\r\n    }\r\n\r\n    function SetCheck(contId, paras) {\r\n        $(\':checkbox[na" +
"me=\' + contId + \']\').removeAttr(\'checked\');\r\n        var paraArr = Enumerable.Fr" +
"om(paras.split(\',\')).Where(\"x=>x!=\'\'\").Select(\"x=>x\").ToArray();\r\n        $.each" +
"(paraArr, function (index, data) {\r\n            $(\"#\" + contId + paraArr[index])" +
".attr(\"checked\", \"checked\");\r\n        })\r\n\r\n        var val = \"0\";\r\n        if (" +
"paraArr.length == 0) {\r\n            val = \"0\";\r\n        } else {\r\n            va" +
"l = paraArr[0];\r\n        }\r\n        switch (contId) {\r\n            default:\r\n   " +
"             switch (val) {\r\n                    case \"1\":\r\n                    " +
"    $(\"#\" + contId + \"_Ex\").val(\"☑是  □否\");\r\n                        break;\r\n    " +
"                case \"2\":\r\n                        $(\"#\" + contId + \"_Ex\").val(\"" +
"□是  ☑否\");\r\n                        break;\r\n                    default:\r\n       " +
"                 $(\"#\" + contId + \"_Ex\").val(\"□是  □否\");\r\n                       " +
" break;\r\n                }\r\n                break;\r\n        }\r\n    }\r\n</script>\r" +
"\n");

            
            #line 106 "..\..\Views\IsoZYZDJYB\info.cshtml"
 using (Html.BeginForm("save", "IsoZYZDJYB", FormMethod.Post, new { @area = "Iso", @id = "IsoZYZDJYBForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 108 "..\..\Views\IsoZYZDJYB\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 108 "..\..\Views\IsoZYZDJYB\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:20px;font-weight:bold;margin-top:1" +
"0px;\"");

WriteLiteral(">\r\n            <a>专业设计指导纪要表</a>\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:left;margin-left:20px;margin-top:10px;\"");

WriteLiteral(">\r\n            表<label>");

            
            #line 114 "..\..\Views\IsoZYZDJYB\info.cshtml"
               Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4380), Tuple.Create("\"", 4406)
            
            #line 114 "..\..\Views\IsoZYZDJYB\info.cshtml"
             , Tuple.Create(Tuple.Create("", 4388), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 4388), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:right;margin-right:20px;margin-top:10px;margin-bottom:10px;\"");

WriteLiteral(">\r\n            编号<input");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" style=\"width:140px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4632), Tuple.Create("\"", 4653)
            
            #line 117 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 4640), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 4640), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">项目编号</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" style=\"width:60%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("  data-options=\"required:true,editable:false\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4926), Tuple.Create("\"", 4951)
            
            #line 122 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                                                , Tuple.Create(Tuple.Create("", 4934), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 4934), false)
);

WriteLiteral(" />\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnChoice\"");

WriteLiteral(" id=\"btnChoice\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(" onclick=\"SelectMainProject()\"");

WriteLiteral(">选择项目</a>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">项目名称</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjId\"");

WriteLiteral(" name=\"ProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5301), Tuple.Create("\"", 5322)
            
            #line 127 "..\..\Views\IsoZYZDJYB\info.cshtml"
, Tuple.Create(Tuple.Create("", 5309), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 5309), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,editable:false\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5491), Tuple.Create("\"", 5514)
            
            #line 128 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                                           , Tuple.Create(Tuple.Create("", 5499), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 5499), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>管理等级</th>\r\n " +
"           <td>\r\n                <input");

WriteLiteral(" name=\"Level\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5715), Tuple.Create("\"", 5735)
            
            #line 134 "..\..\Views\IsoZYZDJYB\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 5723), Tuple.Create<System.Object, System.Int32>(Model.Level
            
            #line default
            #line hidden
, 5723), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th>设计阶段</th>\r\n            <td>\r\n            " +
"    <input");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"editable:false\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5964), Tuple.Create("\"", 5992)
            
            #line 138 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                                       , Tuple.Create(Tuple.Create("", 5972), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 5972), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseId\"");

WriteLiteral(" id=\"ProjPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6070), Tuple.Create("\"", 6096)
            
            #line 139 "..\..\Views\IsoZYZDJYB\info.cshtml"
, Tuple.Create(Tuple.Create("", 6078), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 6078), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>专业</th>\r\n " +
"           <td>\r\n                <input");

WriteLiteral(" name=\"SpecName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6301), Tuple.Create("\"", 6324)
            
            #line 146 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                  , Tuple.Create(Tuple.Create("", 6309), Tuple.Create<System.Object, System.Int32>(Model.SpecName
            
            #line default
            #line hidden
, 6309), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th>专业负责人</th>\r\n            <td>\r\n           " +
"     <input");

WriteLiteral(" name=\"SpecEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6504), Tuple.Create("\"", 6530)
            
            #line 150 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                     , Tuple.Create(Tuple.Create("", 6512), Tuple.Create<System.Object, System.Int32>(Model.SpecEmpName
            
            #line default
            #line hidden
, 6512), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>应收集的特殊基础资料" +
"，项目所在地的质量、安全、环境标准规定及资格备案要求</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BAYQ\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6820), Tuple.Create("\"", 6839)
            
            #line 157 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 6828), Tuple.Create<System.Object, System.Int32>(Model.BAYQ
            
            #line default
            #line hidden
, 6828), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>专业设计原则和设计标准，" +
"主要技术要求及技术参数</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"JSCS\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7114), Tuple.Create("\"", 7133)
            
            #line 163 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 7122), Tuple.Create<System.Object, System.Int32>(Model.JSCS
            
            #line default
            #line hidden
, 7122), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>质量安全风险点及主要技术" +
"措施</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"AQCS\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7399), Tuple.Create("\"", 7418)
            
            #line 169 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 7407), Tuple.Create<System.Object, System.Int32>(Model.AQCS
            
            #line default
            #line hidden
, 7407), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>技术创新要点</th>\r" +
"\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CXYD\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7676), Tuple.Create("\"", 7695)
            
            #line 175 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 7684), Tuple.Create<System.Object, System.Int32>(Model.CXYD
            
            #line default
            #line hidden
, 7684), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>审定人需要签署的施工图<" +
"/th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"XQSSGT\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7960), Tuple.Create("\"", 7981)
            
            #line 181 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 7968), Tuple.Create<System.Object, System.Int32>(Model.XQSSGT
            
            #line default
            #line hidden
, 7968), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>计算书的编制内容及采用的" +
"计算软件</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"JSRJ\"");

WriteLiteral(" style=\"width:98%;height:100px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8249), Tuple.Create("\"", 8268)
            
            #line 187 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 8257), Tuple.Create<System.Object, System.Int32>(Model.JSRJ
            
            #line default
            #line hidden
, 8257), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      其他\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" style=\"display:block;float:left;margin-left:5px;\"");

WriteLiteral(">是否需要专业设计评审？</span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsPS1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsPS\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsReview\',\'1\',\'\',\'IsPS\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsPS1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">是</label>\r\n                </span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsPS2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsPS\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsReview\',\'2\',\'\',\'IsPS\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsPS2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">否</label>\r\n                </span>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsReview\"");

WriteLiteral(" name=\"IsReview\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9223), Tuple.Create("\"", 9246)
            
            #line 204 "..\..\Views\IsoZYZDJYB\info.cshtml"
, Tuple.Create(Tuple.Create("", 9231), Tuple.Create<System.Object, System.Int32>(Model.IsReview
            
            #line default
            #line hidden
, 9231), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsPS_Ex\"");

WriteLiteral(" name=\"IsPS_Ex\"");

WriteLiteral(" />\r\n                <div");

WriteLiteral(" style=\"clear:both;\"");

WriteLiteral("></div>\r\n                <span");

WriteLiteral(" style=\"display:block;float:left;margin-left:5px;\"");

WriteLiteral(">是否进行特殊验证？</span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsYZ1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsYZ\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsValidate\',\'1\',\'\',\'IsYZ\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsYZ1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">是</label>\r\n                </span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsYZ2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsYZ\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsValidate\',\'2\',\'\',\'IsYZ\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsYZ2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">否</label>\r\n                </span>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsValidate\"");

WriteLiteral(" name=\"IsValidate\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10190), Tuple.Create("\"", 10215)
            
            #line 216 "..\..\Views\IsoZYZDJYB\info.cshtml"
, Tuple.Create(Tuple.Create("", 10198), Tuple.Create<System.Object, System.Int32>(Model.IsValidate
            
            #line default
            #line hidden
, 10198), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsYZ_Ex\"");

WriteLiteral(" name=\"IsYZ_Ex\"");

WriteLiteral(" />\r\n                <div");

WriteLiteral(" style=\"clear:both;\"");

WriteLiteral("></div>\r\n                <span");

WriteLiteral(" style=\"display:block;float:left;margin-left:5px;\"");

WriteLiteral(">是否需要编制统一技术规定？</span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsTY1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsTY\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsTYGD\',\'1\',\'\',\'IsTY\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsTY1\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">是</label>\r\n                </span>\r\n                <span");

WriteLiteral(" style=\"display:block;width:50px;float:left;\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" id=\"IsTY2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(" name=\"IsTY\"");

WriteLiteral(" onclick=\"Checked(this.id,\'IsTYGD\',\'2\',\'\',\'IsTY\')\"");

WriteLiteral(" />\r\n                    <label");

WriteLiteral(" for=\"IsTY2\"");

WriteLiteral(" style=\"vertical-align:middle;\"");

WriteLiteral(">否</label>\r\n                </span>\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsTYGD\"");

WriteLiteral(" name=\"IsTYGD\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11147), Tuple.Create("\"", 11168)
            
            #line 228 "..\..\Views\IsoZYZDJYB\info.cshtml"
, Tuple.Create(Tuple.Create("", 11155), Tuple.Create<System.Object, System.Int32>(Model.IsTYGD
            
            #line default
            #line hidden
, 11155), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"IsTY_Ex\"");

WriteLiteral(" name=\"IsTY_Ex\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 241 "..\..\Views\IsoZYZDJYB\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 241 "..\..\Views\IsoZYZDJYB\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "IsoZYZDJYB";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 246 "..\..\Views\IsoZYZDJYB\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 246 "..\..\Views\IsoZYZDJYB\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 251 "..\..\Views\IsoZYZDJYB\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591