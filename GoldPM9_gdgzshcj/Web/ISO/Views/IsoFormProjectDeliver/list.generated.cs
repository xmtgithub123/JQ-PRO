﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
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
    
    #line 4 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjectDeliver/list.cshtml")]
    public partial class _Views_IsoFormProjectDeliver_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjectDeliver_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
                     Write(Url.Action("json", "IsoFormProjectDeliver", new { @area = "ISO" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
                    Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
                     Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
                    Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var xml = \'\';\r\n        var EmpId = \'");

            
            #line 12 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
                Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'FormID\',//主键的字段\r\n    " +
"            JQID: \'IsoFormProjectDeliverGrid\',//数据表格ID\r\n                JQDialog" +
"Title: \'项目交付登记信息\',//弹出窗体标题\r\n                JQWidth: \'960\',//弹出窗体宽\r\n            " +
"    JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型" +
" [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r\n                c" +
"heckOnSelect: true,//是否包含check\r\n                toolbar: \'#IsoFormProjectDeliver" +
"Panel\',//工具栏的容器ID\r\n                JQExcludeCol: [1, 10],//导出execl排除的列\r\n        " +
"        singleSelect: false,\r\n                columns: [[\r\n                  { f" +
"ield: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n             " +
"     {\r\n                      title: \'项目名称\', field: \'ProjName\', width: \"20%\", al" +
"ign: \'left\', sortable: false\r\n                  },\r\n                  {\r\n       " +
"               title: \'项目编号\', field: \'ProjNumber\', width: \"10%\", align: \'left\', " +
"sortable: false\r\n                  },\r\n                  {\r\n                    " +
"  title: \'项目负责人\', field: \'ProjEmpName\', width: \"8%\", align: \'center\', sortable: " +
"false\r\n                  },\r\n                  {\r\n                      title: \'" +
"项目阶段\', field: \'ProjPhaseIds\', width: \"12%\", align: \'center\', sortable: false\r\n  " +
"                },\r\n                  {\r\n                      title: \'发放人\', fie" +
"ld: \'ColAttVal3\', width: \"8%\", align: \'center\', sortable: true\r\n                " +
"  },\r\n                  {\r\n                      title: \'发放日期\', field: \'ColAttDa" +
"te1\', width: \"8%\", align: \'center\', sortable: true, formatter: JQ.tools.DateTime" +
"FormatterByT\r\n                  },\r\n                  {\r\n                      t" +
"itle: \'交付方式\', field: \'ColAttVal1\', width: \"12%\", align: \'center\', sortable: true" +
"\r\n                  },\r\n                  {\r\n                      title: \'交付单号\'" +
", field: \'ColAttVal2\', width: \"13%\", align: \'center\', sortable: true\r\n          " +
"        },\r\n                  {\r\n                      title: \'操作\', field: \'info" +
"\', width: \'5%\', align: \'center\',\r\n                      formatter:function(val,r" +
"ow)\r\n                      {\r\n                          if (row.CreatorEmpId == " +
"EmpId)\r\n                          {\r\n                              return \'<a cl" +
"ass=\"easyui-linkbutton\" onclick=\"ReadingOrEditInfo(\'+row.FormID+\')\">修改</a>\'\r\n   " +
"                       }\r\n                          else\r\n                      " +
"    {\r\n                              return \'<a class=\"easyui-linkbutton\" onclic" +
"k=\"ReadingOrEditInfo(\'+row.FormID+\')\" ck=\"1\">查看</a>\'\r\n                          " +
"}\r\n                      }\r\n                  }\r\n                ]],\r\n          " +
"      onClickRow: function (rowIndex, rowData) {\r\n\r\n                        if (" +
"rowData.CreatorEmpId != EmpId)//不是自己发起的\r\n                        {\r\n            " +
"                $(this).datagrid(\'unselectRow\', rowIndex);\r\n                    " +
"    }\r\n                },\r\n                onLoadSuccess: function (row) { \r\n   " +
"                 //$(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"d" +
"isabled\", true);//禁用全选（避免失误删除）\r\n                    $(\"div.datagrid-header-check" +
" input[type=\'checkbox\']\").attr(\"style\", \"display:none\");\r\n                    $(" +
"\'a[ck=\"1\"]\').parent().parent().parent().find(\'input[type=\"checkbox\"]\').attr(\"dis" +
"abled\", true);\r\n                }\r\n            });\r\n            $(\"#JQSearch\").t" +
"extbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-" +
"search\',\r\n                height: 25,\r\n                prompt: \'项目名称、项目编号、说明\',\r\n" +
"                onClickButton: function () {\r\n                    JQ.datagrid.se" +
"archGrid(\r\n                        {\r\n                            dgID: \'IsoForm" +
"ProjectDeliverGrid\',\r\n                            loadingType: \'datagrid\',\r\n    " +
"                        queryParams: {\r\n                                KeyJQSea" +
"rch: $(\"#JQSearch\").val(),\r\n                                \r\n                  " +
"              //ProjectPhase: $(\'#ProjectPhase\').combotree(\"getValues\"),\r\n      " +
"                      }\r\n                        });\r\n                }\r\n       " +
"     });\r\n        });\r\n\r\n        function ReadingOrEditInfo(FormID)\r\n        {\r\n" +
"            JQ.dialog.dialog({\r\n                title: \'项目交付信息\',\r\n              " +
"  height: \'700\',\r\n                width: \'1000\',\r\n                url: editUrl +" +
" \"?id=\" + FormID,\r\n                onClose:function()\r\n                {\r\n      " +
"              $(\"#IsoFormProjectDeliverGrid\").datagrid(\"reload\");\r\n             " +
"   }\r\n            });\r\n        }\r\n    </script>\r\n");

            
            #line 118 "..\..\Views\IsoFormProjectDeliver\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormProjectDeliverGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormProjectDeliverPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n        ");

WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
