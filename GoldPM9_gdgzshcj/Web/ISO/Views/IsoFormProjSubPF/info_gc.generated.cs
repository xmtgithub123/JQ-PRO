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
    
    #line 2 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjSubPF/info_gc.cshtml")]
    public partial class _Views_IsoFormProjSubPF_info_gc_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormProjSubPF_info_gc_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(function () {\r\n        JQ.form.submitInit({\r\n            formid: \'IsoFor" +
"mProjSubPFForm\',//formid提交需要用到\r\n            docName: \'isoformprojsubpf\',\r\n      " +
"      ExportName: \'项目外委评分表\',\r\n            buttonTypes: [\'exportForm\',\'close\'],//" +
"默认按钮\r\n            onBefore: function () {\r\n                $(\"#ProjSubTableGrid\"" +
").datagrid(\'hideColumn\', \"ck\");\r\n            } ,\r\n            onAfter: function " +
"() {\r\n                $(\"#ProjSubTableGrid\").datagrid(\'showColumn\', \"ck\");\r\n    " +
"        },\r\n            succesCollBack: function (data) {//成功回调函数,data为服务器返回值\r\n " +
"               JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用\r\n        " +
"        JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源\r\n            },\r\n      " +
"      beforeFormSubmit: function () {\r\n                var _Id = $(\"#ProjSubIDs\"" +
").val();\r\n                if (_Id == \"\") {\r\n                    JQ.dialog.warnin" +
"g(\"请先选择项目外委信息!\");\r\n                    return false;\r\n                }\r\n       " +
"         $(\"#strProjSubRWS\").val(JQ.Flow.htmlEncode($(\"#ProjSubRWS\").val()));\r\n " +
"               $(\"#strProjSubPercent\").val(JQ.Flow.htmlEncode($(\"#ProjSubPercent" +
"\").val()));\r\n                $(\"#strWWHZNR\").val(JQ.Flow.htmlEncode($(\"#WWHZNR\")" +
".val()));\r\n                $(\"#strRYPB\").val(JQ.Flow.htmlEncode($(\"#RYPB\").val()" +
"));\r\n                $(\"#strSJZL\").val(JQ.Flow.htmlEncode($(\"#SJZL\").val()));\r\n " +
"               $(\"#strSJNLJSSP\").val(JQ.Flow.htmlEncode($(\"#SJNLJSSP\").val()));\r" +
"\n                $(\"#strSJFWXY\").val(JQ.Flow.htmlEncode($(\"#SJFWXY\").val()));\r\n " +
"               $(\"#strSJJD\").val(JQ.Flow.htmlEncode($(\"#SJJD\").val()));\r\n       " +
"         $(\"#strSJZLTJ\").val(JQ.Flow.htmlEncode($(\"#SJZLTJ\").val()));\r\n         " +
"       $(\"#strBZ\").val(JQ.Flow.htmlEncode($(\"#BZ\").val()));\r\n                $(\"" +
"#strPSYJ\").val(JQ.Flow.htmlEncode($(\"#PSYJ\").val()));\r\n                $(\"#strZT" +
"DF\").val(JQ.Flow.htmlEncode($(\"#ZTDF\").val()));\r\n\r\n            }, flow: {\r\n     " +
"           flowNodeID: \"");

            
            #line 40 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                         Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                flowMultiSignID: \"");

            
            #line 41 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                              Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                url: \"");

            
            #line 42 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                  Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                instance: \"_flowInstance\",\r\n                processor: \"ISO,I" +
"SO.FlowProcessor.IsoFormGrade\",\r\n                onInit: function () {\r\n        " +
"            _flowInstance = this;\r\n                },\r\n                refID: \"");

            
            #line 48 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                    Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                refTable: \"IsoFormGrade_GC\",\r\n                dataCreator:\"");

            
            #line 50 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                         Write(ViewBag.CreatorEmpId );

            
            #line default
            #line hidden
WriteLiteral("\"\r\n            }\r\n        });\r\n        if(");

            
            #line 53 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
      Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n            $(\"#ProjSubIDs\").val(");

            
            #line 55 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                            Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n        $(\'#ProjSubTableGrid\').datagrid({\r\n            JQPrimaryID" +
": \'Id\',//主键的字段\r\n            JQID: \'ProjSubTable\',//数据表格ID\r\n            JQLoading" +
"Type: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n            url: \'");

            
            #line 61 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
             Write(Url.Action("GetProjSubList", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',\r\n            queryParams: { ProjSubIDs: $(\"#ProjSubIDs\").val() " +
"},\r\n            toolbar: \'#ProjSubTablePanel\',//工具栏的容器ID\r\n            pagination" +
": false,\r\n            idField:\"Id\",\r\n            columns: [[\r\n                  " +
"   { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n       " +
"              { title: \'外委项目编号\', field: \'SubNumber\', width: 100, align: \'left\', " +
"sortable: true },\r\n                     { title: \'外委项目名称\', field: \'SubName\', wid" +
"th: 160, align: \'left\', sortable: true },\r\n                     { title: \'项目编号\'," +
" field: \'ProjNumber\', width: 100, align: \'left\', sortable: true },\r\n            " +
"         { title: \'项目名称\', field: \'ProjName\', width: 250, align: \'left\', sortable" +
": true },\r\n                     { title: \'外委时间\', field: \'ColAttDate1\', width: 80" +
", align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n" +
"                     { title: \'登记时间\', field: \'CreationTime\', width: 80, align: \'" +
"center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n\r\n        " +
"    ]],\r\n\r\n        });\r\n        var flag = $(\"#divXml\").text().trim();\r\n        " +
"if (flag) {\r\n            var xml = GoldPM.loadXml(flag);\r\n            var items " +
"= GoldPM.selectNodes(xml, \"Root/Item\");\r\n            for (var i = 0; i < items.l" +
"ength; i++) {\r\n                var name = items[i].getAttribute(\"name\");\r\n      " +
"          var text = items[i].text || items[i].innerHTML;\r\n                $(\"#\"" +
" + name).val(text);\r\n            }\r\n        }\r\n    });\r\n\r\n    function SelectPro" +
"jSub() {\r\n        JQ.dialog.dialog({\r\n            title: \"选择项目外委\",\r\n            " +
"url: \'");

            
            #line 93 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
             Write(Url.Action("selectprojsub", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=GC&typeID=4&FilterConSubID=0',
            width: '1024',
            height: '100%',
            JQID: 'ProjSubTable',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }
    var _ProjCallBackByPF = function (param) {
        $(""#ProjSubIDs"").val(param);
        $(""#ProjSubTableGrid"").datagrid({
            url: '");

            
            #line 104 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
             Write(Url.Action("GetProjSubList", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',\r\n            queryParams: { ProjSubIDs: $(\"#ProjSubIDs\").val() " +
"},\r\n        });\r\n    }\r\n\r\n</script>\r\n");

            
            #line 110 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
 using (Html.BeginForm("save", "IsoFormProjSubPF", FormMethod.Post, new { @area = "Iso", @id = "IsoFormProjSubPFForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 112 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 112 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                                  

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"GC\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" style=\"padding: 15px; font-size: 20px; text-align: center; font-weight: bold; bo" +
"rder: none;\"");

WriteLiteral(">\r\n                项目外委评分\r\n                <div");

WriteLiteral(" id=\"divXml\"");

WriteLiteral(" style=\"display:none\"");

WriteLiteral(">");

            
            #line 118 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                                                  Write(ViewBag.contentXml);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n       " +
"         项目外委信息\r\n                <div");

WriteLiteral(" id=\"ProjSubTablePanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"SelectProjSub\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectProjSub()\"");

WriteLiteral(">选择外委项目</a>\r\n                    </span>\r\n                </div>\r\n            </t" +
"h>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"ProjSubTableGrid\"");

WriteLiteral(" bookmark=\"ProjSubTableGrid\"");

WriteLiteral("></table>\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委任务书名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ProjSubRWS\"");

WriteLiteral(" name=\"ProjSubRWS\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委比例</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ProjSubPercent\"");

WriteLiteral(" name=\"ProjSubPercent\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate1\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true,validType:[\'dateISO\',\'length[0,16]\']\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7148), Tuple.Create("\"", 7174)
            
            #line 148 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                                                                             , Tuple.Create(Tuple.Create("", 7156), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate1
            
            #line default
            #line hidden
, 7156), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">评分时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate2\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true,validType:[\'dateISO\',\'length[0,16]\']\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7417), Tuple.Create("\"", 7443)
            
            #line 152 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                                                                             , Tuple.Create(Tuple.Create("", 7425), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate2
            
            #line default
            #line hidden
, 7425), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合作内容</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"WWHZNR\"");

WriteLiteral(" id=\"WWHZNR\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">人员配备情况</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"RYPB\"");

WriteLiteral(" id=\"RYPB\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">设计质量</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SJZL\"");

WriteLiteral(" id=\"SJZL\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">设计能力及技术水平</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SJNLJSSP\"");

WriteLiteral(" id=\"SJNLJSSP\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">设计服务响应</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SJFWXY\"");

WriteLiteral(" id=\"SJFWXY\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                设计进度\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SJJD\"");

WriteLiteral(" id=\"SJJD\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                设计资料的提交\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SJZLTJ\"");

WriteLiteral(" id=\"SJZLTJ\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                备注\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"BZ\"");

WriteLiteral(" id=\"BZ\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                评审意见\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"PSYJ\"");

WriteLiteral(" id=\"PSYJ\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                总体得分\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ZTDF\"");

WriteLiteral(" id=\"ZTDF\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral("></textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n " +
"               上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 231 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                
            
            #line default
            #line hidden
            
            #line 231 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.FormID;
                    uploader.RefTable = "IsoForm";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 236 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 236 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjSubIDs\"");

WriteLiteral(" name=\"ProjSubIDs\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strProjSubRWS\"");

WriteLiteral(" name=\"strProjSubRWS\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strProjSubPercent\"");

WriteLiteral(" name=\"strProjSubPercent\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strWWHZNR\"");

WriteLiteral(" name=\"strWWHZNR\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strRYPB\"");

WriteLiteral(" name=\"strRYPB\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strSJZL\"");

WriteLiteral(" name=\"strSJZL\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strSJNLJSSP\"");

WriteLiteral(" name=\"strSJNLJSSP\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strSJFWXY\"");

WriteLiteral(" name=\"strSJFWXY\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strSJJD\"");

WriteLiteral(" name=\"strSJJD\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strSJZLTJ\"");

WriteLiteral(" name=\"strSJZLTJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strBZ\"");

WriteLiteral(" name=\"strBZ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strPSYJ\"");

WriteLiteral(" name=\"strPSYJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strZTDF\"");

WriteLiteral(" name=\"strZTDF\"");

WriteLiteral(" />\r\n");

            
            #line 254 "..\..\Views\IsoFormProjSubPF\info_gc.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
