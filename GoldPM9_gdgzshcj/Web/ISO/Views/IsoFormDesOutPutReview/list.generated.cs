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
    
    #line 4 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormDesOutPutReview/list.cshtml")]
    public partial class _Views_IsoFormDesOutPutReview_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormDesOutPutReview_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
                     Write(Url.Action("json", "IsoFormDesOutPutReview", new { @area="Iso"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
                    Write(Url.Action("add","IsoFormDesOutPutReview",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
                     Write(Url.Action("edit", "IsoFormDesOutPutReview",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
                    Write(Url.Action("del", "IsoFormDesOutPutReview", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var EmpId=\'");

            
            #line 11 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
              Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var xml = \'\';\r\n        $(function () {\r\n            JQ.datagrid.datag" +
"rid({\r\n                JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮" +
"\r\n                JQAddUrl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl," +
"//编辑的路径\r\n                JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID:" +
" \'FormID\',//主键的字段\r\n                JQID: \'IsoFormDesOutPutReviewGrid\',//数据表格ID\r\n" +
"                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'950\',//弹" +
"出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'" +
"datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r" +
"\n                checkOnSelect: true,//是否包含check\r\n                JQExcludeCol: " +
"[1,8, 9],//导出execl排除的列\r\n                toolbar: \'#IsoFormDesOutPutReviewPanel\'," +
"//工具栏的容器ID\r\n                singleSelect: false,\r\n                columns: [[\r\n " +
"               { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true" +
" },\r\n                {\r\n                    title: \'编号\', field: \'Number\', width:" +
" \"8%\", align: \'left\', sortable: true\r\n                },\r\n\r\n                {\r\n " +
"                   title: \'项目编号\', field: \'ProjNumber\', width: \"14%\", align: \'lef" +
"t\', sortable: true\r\n                },\r\n                  {\r\n                   " +
"   title: \'项目名称\', field: \'ProjName\', width: \"25%\", align: \'left\', sortable: true" +
"\r\n                  },\r\n                  {\r\n                      title: \'评审主持人" +
"\', field: \'ReviewName\', width: \"15%\", align: \'center\', sortable: true\r\n         " +
"         },\r\n                  {\r\n                      title: \'登记人\', field: \'Cr" +
"eatorEmpName\', width: \"10%\", align: \'center\', sortable: true\r\n                  " +
"},\r\n                {\r\n                    title: \'登记时间\', field: \'FormDate\', wid" +
"th: \"10%\", align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatte" +
"rByT\r\n                },\r\n                {\r\n                    field: \"FlowPro" +
"gress\", title: \"流程进度\", align: \"center\", halign: \"center\", width: \"8%\", formatter" +
": JQ.Flow.showFlowProgress(\"IsoFormDesOutPutReviewGrid\", \"IsoFormDesOutPutReview" +
"\", \"FormID\")\r\n                },\r\n                {\r\n                    field:\'" +
"info\',title:\'操作\',align:\'center\',width:\'6%\',\r\n                    formatter:funct" +
"ion(val,row)\r\n                    {\r\n                        if (row.FlowStatus " +
"== \"0\")\r\n                        {\r\n                            return \'<a class" +
"=\"easyui-linkbutton\" onclick=\"Info(\'+row.FormID+\')\">修改</a>\';\r\n                  " +
"      }\r\n                        else\r\n                        {\r\n              " +
"              if (row.FlowStatus == \"1\")\r\n                            {\r\n       " +
"                         return \'<a class=\"easyui-linkbutton\" onclick=\"Info(\' + " +
"row.FormID + \')\" ck=\"1\">处理</a>\';\r\n                            }\r\n               " +
"             else\r\n                            {\r\n                              " +
"  return \'<a class=\"easyui-linkbutton\" onclick=\"Info(\' + row.FormID + \')\" ck=\"1\"" +
">查看</a>\';\r\n                            }\r\n                        }\r\n           " +
"         }\r\n                }\r\n\r\n                ]],\r\n                onClickRow" +
": function (rowIndex, rowData) {\r\n\r\n                    if (rowData.FlowStatus !" +
"= \"0\")//处于流程中的记录不允许选中（删除）\r\n                    {\r\n                        $(this" +
").datagrid(\'unselectRow\', rowIndex);\r\n                    }\r\n                },\r" +
"\n                onLoadSuccess: function (row) {\r\n\r\n                    //禁用全选（避" +
"免失误删除）\r\n                    $(\"div.datagrid-header-check input[type=\'checkbox\']\"" +
").attr(\"disabled\", true);\r\n                    $(\'a[ck=\"1\"]\').parent().parent()." +
"parent().find(\'input[type=\"checkbox\"]\').attr(\"disabled\", true);\r\n               " +
" }\r\n            });\r\n            function setTextAlign(filed, align) {\r\n        " +
"        $(\"tr[class=\'datagrid-header-row\']\").find(\"td[field=\'\" + filed + \"\']\").c" +
"hildren().removeAttr(\"style\").attr(\"style\", align);\r\n            }\r\n            " +
"setTextAlign(\"ProjName\", \"text-align:center\");\r\n            setTextAlign(\"ProjNu" +
"mber\", \"text-align:center\");\r\n            $(\"#JQSearch\").textbox({\r\n            " +
"    buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n            " +
"    height: 25,\r\n                prompt: \'项目编号、项目名称\',\r\n                onClickBu" +
"tton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n              " +
"          {\r\n                            dgID: \'IsoFormDesOutPutReviewGrid\',\r\n  " +
"                          loadingType: \'datagrid\',\r\n                            " +
"queryParams: {\r\n                                Keys: $(\"#JQSearch\").val(),\r\n   " +
"                             startDate: $(\"#startDate\").datebox(\'getValue\'),\r\n  " +
"                              endDate:$(\"#endDate\").datebox(\'getValue\')\r\n       " +
"                     }\r\n                        });\r\n                }\r\n        " +
"    });\r\n        });\r\n\r\n        function Info(FormID)\r\n        {\r\n            JQ" +
".dialog.dialog({\r\n                title: \'设计输出编辑\',\r\n                width: \'950\'" +
",\r\n                height: \'700\',\r\n                url: editUrl + \'?id=\' + FormI" +
"D,\r\n                onClose:function()\r\n                {\r\n                    $" +
"(\"#IsoFormDesOutPutReviewGrid\").datagrid(\"reload\");\r\n                }\r\n        " +
"    });\r\n        }\r\n    </script>\r\n");

            
            #line 130 "..\..\Views\IsoFormDesOutPutReview\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormDesOutPutReviewGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormDesOutPutReviewPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;border:none;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" style=\"display:inline-block;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"margin-left:20px;\"");

WriteLiteral(">登记时间：</span>\r\n            <input");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" id=\"startDate\"");

WriteLiteral(" name=\"startDate\"");

WriteLiteral(" style=\"width:120px;\"");

WriteLiteral(" prompt=\"开始时间\"");

WriteLiteral(" />\r\n            ---\r\n            <input");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" id=\"endDate\"");

WriteLiteral(" name=\"endDate\"");

WriteLiteral(" style=\"width:120px;\"");

WriteLiteral(" prompt=\"结束时间\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591