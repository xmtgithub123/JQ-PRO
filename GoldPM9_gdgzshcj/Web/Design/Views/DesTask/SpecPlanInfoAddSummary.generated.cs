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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/SpecPlanInfoAddSummary.cshtml")]
    public partial class _Views_DesTask_SpecPlanInfoAddSummary_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_SpecPlanInfoAddSummary_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<style>\r\n    .datagrid-group input {\r\n        margin: 0;\r\n        padding: 0;\r\n  " +
"       width: 15px; \r\n         height: 18px; \r\n    }\r\n    \r\n\r\n    .btn-medium {\r" +
"\n        padding: 5px 15px;\r\n        color: white !important;\r\n        width: 10" +
"0px;\r\n    }\r\n\r\n    .pagefoot {\r\n        text-align: center;\r\n        padding:5px" +
";\r\n        height:auto;\r\n    }\r\n\r\n        .pagefoot .l-btn-text {\r\n            v" +
"ertical-align: middle !important;\r\n            font-size: 14px;\r\n        }\r\n\r\n  " +
"      .pagefoot .btn-blue {\r\n            background: -webkit-linear-gradient(top" +
",#67c2ff 0,#3d93cf 100%);\r\n            background: -moz-linear-gradient(top,#67c" +
"2ff 0,#3d93cf 100%);\r\n            background: -o-linear-gradient(top,#67c2ff 0,#" +
"3d93cf 100%);\r\n            background: linear-gradient(top,#67c2ff 0,#3d93cf 100" +
"%);\r\n            border: none;\r\n        }\r\n\r\n        .pagefoot .btn-green {\r\n   " +
"         background: -webkit-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n     " +
"       background: -moz-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n          " +
"  background: -o-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n            backg" +
"round: linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n            border: none;\r\n" +
"        }\r\n\r\n        .pagefoot .btn-gray {\r\n            background: -webkit-line" +
"ar-gradient(top,#bebebe 0,#808080 100%);\r\n            background: -moz-linear-gr" +
"adient(top,#bebebe 0,#808080 100%);\r\n            background: -o-linear-gradient(" +
"top,#bebebe 0,#808080 100%);\r\n            background: linear-gradient(top,#bebeb" +
"e 0,#808080 100%);\r\n            border: none;\r\n        }\r\n\r\n        .pagefoot .b" +
"tn-blue:hover {\r\n            background: #3d93cf;\r\n        }\r\n\r\n        .pagefoo" +
"t .btn-green:hover {\r\n            background: #69a898;\r\n        }\r\n\r\n        .pa" +
"gefoot .btn-gray:hover {\r\n            background: #808080;\r\n        }\r\n</style>\r" +
"\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1872), Tuple.Create("\"", 1935)
            
            #line 63 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
, Tuple.Create(Tuple.Create("", 1878), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/extension/datagrid-groupview.js")
            
            #line default
            #line hidden
, 1878), false)
);

WriteLiteral("></script>\r\n\r\n<div");

WriteLiteral(" id=\"TaskInfoSave\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n            ");

WriteLiteral("\r\n            <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(" style=\"border-right-width:1px;\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"TaskInfoPersonApprove\"");

WriteLiteral("></table>\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    var selTaskIds = [");

            
            #line 81 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
                                 Write(ViewBag.selTaskIds);

            
            #line default
            #line hidden
WriteLiteral(@"];

                    $(function () {
                        $('#TaskInfoPersonApprove').datagrid({
                            border: 0,
                            idField: 'Id',
                            fit: true,
                            rownumbers: true,
                            checkOnSelect: true,
                            nowrap: false,
                            fitColumns: true,
                            data: {rows:");

            
            #line 92 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
                                    Write(Html.Raw(ViewBag.specPlanData));

            
            #line default
            #line hidden
WriteLiteral("},\r\n                            columns: [[\r\n                                { fi" +
"eld: \'Id\', width: 50, checkbox: true },\r\n                                { field" +
": \'TaskName\', title: \'任务名称\', width: 200, align: \'left\' },\r\n                     " +
"           { field: \'TaskEmpName\', title: \'负责人\', width: 80, align: \'left\' },\r\n  " +
"                              { field: \'DatePlanStart\', title: \'计划开始\', width: 80" +
", align: \'center\', formatter: JQ.tools.DateTimeFormatterByT },\r\n                " +
"                { field: \'DatePlanFinish\', title: \'计划完成\', width: 80, align: \'cen" +
"ter\', formatter: JQ.tools.DateTimeFormatterByT }\r\n                            ]]" +
",\r\n                            groupField: \'TaskSpecName\',\r\n                    " +
"        view: groupview,\r\n                            groupFormatter: function (" +
"value, rows) {\r\n                                var gh = value + \' - \' + rows.le" +
"ngth + \' 项\'\r\n                                return gh;\r\n                       " +
"     },\r\n                            onLoadSuccess:function (row, data) {\r\n     " +
"                           for(var i = 0; i < selTaskIds.length; i++) {\r\n       " +
"                             var idex = $(\'#TaskInfoPersonApprove\').datagrid(\"ge" +
"tRowIndex\", selTaskIds[i]);\r\n                                    $(\'#TaskInfoPer" +
"sonApprove\').datagrid(\"checkRow\", idex);\r\n                                }\r\n   " +
"                             $(\'#TaskInfoPersonApprove\').datagrid(\"reload\");\r\n  " +
"                          }\r\n                        });\r\n                    })" +
";\r\n                </script>\r\n            </div>\r\n        </div>\r\n    </div>\r\n  " +
"  <div");

WriteLiteral(" data-options=\"region:\'south\',border:true\"");

WriteLiteral(" style=\"overflow:hidden;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"pagefoot\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton btn-medium btn-gray\"");

WriteLiteral(" id=\"btnClose\"");

WriteLiteral(">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;\r\n            <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton btn-medium btn-blue\"");

WriteLiteral(" id=\"btnNext\"");

WriteLiteral(">确定</a>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    $(\"#btnClose\").click(function() {\r\n        $(\"#");

            
            #line 129 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
       Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral(@""").dialog(""close"");
    });

    $(""#btnNext"").click(function () {
        var $grid=$('#TaskInfoPersonApprove');
        var getChecks=$grid.datagrid(""getChecked"");
        //if (getChecks.length==0) {
        //    JQ.dialog.warning(""请在列表中勾选要处理的项！！！"");
        //    return false;
        //}
        if (typeof(window[""_DialogCallback_");

            
            #line 139 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
                                      Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\"])==\"function\") {\r\n            window[\"_DialogCallback_");

            
            #line 140 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
                               Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\"](getChecks);            \r\n            $(\"#");

            
            #line 141 "..\..\Views\DesTask\SpecPlanInfoAddSummary.cshtml"
           Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\").dialog(\"close\");\r\n        }\r\n    })\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591