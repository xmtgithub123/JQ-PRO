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
    
    #line 2 "..\..\Views\IsoFormArchive\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormArchive/info.cshtml")]
    public partial class _Views_IsoFormArchive_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormArchive_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'IsoFormArchiveForm',//formid提交需要用到
        docName: 'da',
        ExportName : '档案申请表',
        buttonTypes: ['close', 'exportForm'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            try {
                var _tempFrame=window.top.document.getElementById(""");

            
            #line 11 "..\..\Views\IsoFormArchive\info.cshtml"
                                                               Write(Request.QueryString["iframeID"]);

            
            #line default
            #line hidden
WriteLiteral(@""");
                if(_tempFrame){
                    _tempFrame.contentWindow.refreshGrid();
                }
            } catch (e) { }
            _flowInstance.$form.parent().dialog(""close"");
        },
        flow: {
            flowNodeID: """);

            
            #line 19 "..\..\Views\IsoFormArchive\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 20 "..\..\Views\IsoFormArchive\info.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 21 "..\..\Views\IsoFormArchive\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.IsoFormArchiveFlow\",\r\n            refID: \"");

            
            #line 24 "..\..\Views\IsoFormArchive\info.cshtml"
                Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(@""",
            refTable: ""IsoFormArchive""
        }
    });
    JQ.datagrid.datagrid({
        JQPrimaryID: 'Id',//主键的字段
        JQID: 'ElecFileGrid',//数据表格ID
        JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
        JQFields: [""Id"", ""ArchProjId"", ""ArchElecFileId""],//追加的字段名
        url: '");

            
            #line 33 "..\..\Views\IsoFormArchive\info.cshtml"
         Write(Url.Action("json", "ArchElecFile",new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral("?ids=\' + \"");

            
            #line 33 "..\..\Views\IsoFormArchive\info.cshtml"
                                                                             Write(Model.ColAttVal1);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        checkOnSelect: true,\r\n        fitColumns: true,\r\n        nowrap: fals" +
"e,\r\n        pagination: false,\r\n        JQMergeCells: { fields: [\'ElecNumber\', \'" +
"ProjEmpName\'], Only: \'ElecNumber\' },\r\n        columns: [[\r\n                { tit" +
"le: \'项目编号\', field: \'ElecNumber\', JQfield: \'FK_ArchElecFile_ArchProjId.ElecNumber" +
"\', width: 100, align: \'center\' },\r\n                //{ title: \'项目名称\', field: \'El" +
"ecName\', JQfield: \'FK_ArchElecFile_ArchProjId.ElecName\', width: 100, align: \'cen" +
"ter\' },\r\n                { title: \'项目经理\', field: \'ProjEmpName\', JQfield: \'FK_Arc" +
"hElecFile_ArchProjId.ProjEmpName\', width: 100, align: \'center\' },\r\n             " +
"   //{ title: \'项目阶段\', field: \'PhaseName\', JQfield: \'FK_ArchElecFile_ArchProjId.P" +
"haseName\', width: 100, align: \'center\' },\r\n                { title: \'文件名称\', fiel" +
"d: \'ElecFileName\', width: 200, align: \'center\', sortable: true },\r\n             " +
"   //{ title: \'文件类型\', field: \'ElecExt\', width: 100, align: \'center\', sortable: t" +
"rue },\r\n                 {\r\n                     title: \"文件大小\", field: \"ElecSize" +
"\", width: \"100\", align: \"right\", halign: \"center\", formatter: function (value, r" +
"ow) {\r\n                         row.ElecSize = getFileSizeText(value)\r\n         " +
"                return row.ElecSize;\r\n                     }\r\n                 }" +
",\r\n                //{ title: \'文件大小\', field: \'ElecSize\', width: 100, align: \'cen" +
"ter\', sortable: true },\r\n                { title: \'上传时间\', field: \'CreationTime\'," +
" width: 100, align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormat" +
"terByT },\r\n                { title: \'上传人\', field: \'CreatorEmpName\', width: 100, " +
"align: \'center\', sortable: true },\r\n        ]], onLoadSuccess: function (data) {" +
"\r\n            if (data != null && data.rows.length > 0) {\r\n                $(\"#P" +
"rojID\").val(data.rows[0][\"ArchProjId\"]);\r\n            }\r\n        }\r\n    });\r\n   " +
" function getFileSizeText(size) {\r\n        if (!size && size != 0) {\r\n          " +
"  return \"\";\r\n        }\r\n        var st = \"\";\r\n        if (size >= 1024 * 1024 *" +
" 1024) {\r\n            st = (size / 1024 / 1024 / 1024).toFixed(2) + \"GB\";\r\n     " +
"   } else if (size >= 1024 * 1024) {\r\n            st = (size / 1024 / 1024).toFi" +
"xed(2) + \"MB\";\r\n        } else if (size >= 1024) {\r\n            st = (size / 102" +
"4).toFixed(2) + \"KB\";\r\n        } else {\r\n            st = size + \"B\";\r\n        }" +
"\r\n        return st;\r\n    }\r\n    function downfile() {\r\n        var row = \"");

            
            #line 78 "..\..\Views\IsoFormArchive\info.cshtml"
              Write(Model.ColAttVal1);

            
            #line default
            #line hidden
WriteLiteral(@""".split(',');
        if (row.length <= 0) {
            return;
        }

        var parm;
        $.each(row, function (i, n) {
            if (i == 0) {
                parm = ""id="" + n;

            } else {
                parm += ""&id="" + n;
            }
        });

        debugger;
        //return;

        Url = '");

            
            #line 96 "..\..\Views\IsoFormArchive\info.cshtml"
          Write(Url.Action("DownFile", "ArchElecFile", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"?';
        downurlfile(Url + parm);
        $('#ElecFileGrid').datagrid('reload');
    }

    function downurlfile(url) {
        var _a = document.createElement(""a"");
        _a.setAttribute(""href"", url);
        document.body.appendChild(_a);
        _a.click();
        document.body.removeChild(_a);
    }


</script>
");

            
            #line 111 "..\..\Views\IsoFormArchive\info.cshtml"
 using (Html.BeginForm("save", "IsoFormArchive", FormMethod.Post, new { @area = "Iso", @id = "IsoFormArchiveForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 113 "..\..\Views\IsoFormArchive\info.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 113 "..\..\Views\IsoFormArchive\info.cshtml"
                                  


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" JQPanel=\'dialogButtonPanel\'");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-arrow-up\'\"");

WriteLiteral(" JQPermissionName=\"a\"");

WriteLiteral(" onclick=\"downfile()\"");

WriteLiteral(">打包下载</a>\r\n    </div>\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" style=\"padding: 15px; font-size: 20px; text-align: center; font-weight: bold; bo" +
"rder: none;\"");

WriteLiteral(">\r\n                档案申请单\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ColAttVal1\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5336), Tuple.Create("\"", 5361)
            
            #line 122 "..\..\Views\IsoFormArchive\info.cshtml"
, Tuple.Create(Tuple.Create("", 5344), Tuple.Create<System.Object, System.Int32>(Model.ColAttVal1
            
            #line default
            #line hidden
, 5344), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"CreatorEmpName\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 128 "..\..\Views\IsoFormArchive\info.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("  bookmark=\"FormDate\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 132 "..\..\Views\IsoFormArchive\info.cshtml"
           Write(Model.FormDate);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请原因</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FormReason\"");

WriteLiteral(" id=\"FormReason\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5986), Tuple.Create("\"", 6011)
            
            #line 138 "..\..\Views\IsoFormArchive\info.cshtml"
                                                                                                           , Tuple.Create(Tuple.Create("", 5994), Tuple.Create<System.Object, System.Int32>(Model.FormReason
            
            #line default
            #line hidden
, 5994), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <table");

WriteLiteral(" id=\"ElecFileGrid\"");

WriteLiteral(" style=\"height:500px; min-height:300px\"");

WriteLiteral(" bookmark=\"ElecFileGrid\"");

WriteLiteral("></table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" />\r\n");

            
            #line 148 "..\..\Views\IsoFormArchive\info.cshtml"
}

            
            #line default
            #line hidden
            
            #line 149 "..\..\Views\IsoFormArchive\info.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591