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
    
    #line 4 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjDelegateService/list.cshtml")]
    public partial class _Views_IsoFormProjDelegateService_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjDelegateService_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                     Write(Url.Action("json", "IsoFormProjDelegateService", new { @area = "ISO" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                    Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                     Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                    Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var xml = \'\';\r\n        var EmpId = \'");

            
            #line 12 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'FormID\',//主键的字段\r\n    " +
"            JQID: \'IsoFormProjDelegateServiceGrid\',//数据表格ID\r\n                JQD" +
"ialogTitle: \'项目工代信息\',//弹出窗体标题\r\n                JQWidth: \'960\',//弹出窗体宽\r\n         " +
"       JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表" +
"格类型 [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r\n              " +
"  checkOnSelect: true,//是否包含check\r\n                toolbar: \'#IsoFormProjDelegat" +
"eServicePanel\',//工具栏的容器ID\r\n                JQExcludeCol: [1, 9, 10, 11],//导出exec" +
"l排除的列\r\n                singleSelect: false,\r\n                columns: [[\r\n      " +
"            {\r\n                    field: \'ck\', align: \'center\', checkbox: true," +
" JQIsExclude: true\r\n\r\n                  },\r\n                  {\r\n               " +
"       title: \'项目名称\', field: \'ProjName\', width: \"22%\", align: \'left\', sortable: " +
"false\r\n                  },\r\n                  {\r\n                      title: \'" +
"项目编号\', field: \'ProjNumber\', width: \"12%\", align: \'left\', sortable: false\r\n      " +
"            },\r\n                  {\r\n                      title: \'项目负责人\', field" +
": \'ProjEmpName\', width: \"8%\", align: \'center\', sortable: false\r\n                " +
"  },\r\n                  {\r\n                      title: \'阶段\', field: \'ProjPhaseI" +
"ds\', width: \"13%\", align: \'center\', sortable: false\r\n                  },\r\n     " +
"             {\r\n                      title: \'申请人\', field: \'CreatorEmpName\', wid" +
"th: \"8%\", align: \'center\', sortable: true\r\n                  },\r\n               " +
"   {\r\n                      title: \'申请日期\', field: \'CreationTime\', width: \"8%\", a" +
"lign: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT \r\n     " +
"             },\r\n                  {\r\n                      title: \'申请原因\', field" +
": \'FormReason\', width: \"10%\", align: \'center\', sortable: true\r\n                 " +
" }\r\n                   ,\r\n                  {\r\n                      field: \"Flo" +
"wProgress\", title: \"流程进度\", align: \"left\", halign: \"center\", width: \"8%\", formatt" +
"er: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"FlowStatusID\", \"FlowStatusName\"," +
" \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 58 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
                                                                                                                                                                                                                                   Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                  },\r\n                \r\n                  {\r\n                " +
"      field: \'opt\', title: \'操作\', width: \"40\", align: \'center\', JQIsExclude: true" +
",\r\n                      formatter: function (value, row, index) {\r\n            " +
"              console.log(row);\r\n                          var title = \"查看\";\r\n  " +
"                        if (row._EditStatus == 1) {\r\n                           " +
"   title = \"修改\";\r\n                          }\r\n                          else if" +
" (row._EditStatus == 2) {\r\n                              title = \"处理\"; \r\n       " +
"                   }\r\n                          return \'<a class=\"easyui-linkbut" +
"ton\" onclick=\"ReadingOrEdit(\' + row.FormID + \')\">\' + title + \'</a>\';\r\n          " +
"            }\r\n                  },\r\n                { title: \'FlowStatus\', fiel" +
"d: \'FlowStatus\', halign: \'center\', align: \'left\', hidden: true },\r\n             " +
"     //,\r\n                  //{\r\n                  //    field: \'Info\', title: \'" +
"操作\', align: \'center\', width: \'5%\',\r\n                  //    formatter:function(v" +
"al,row)\r\n                  //    {\r\n                  //        if (row.CreatorE" +
"mpId == EmpId && row.FlowStatus == \"0\")\r\n                  //        {\r\n        " +
"          //            return \'<a class=\"easyui-linkbutton\" onclick=\"ReadingOrE" +
"dit(\'+row.FormID+\')\">修改</a>\';\r\n                  //        }\r\n                  " +
"//        else\r\n                  //        {\r\n                  //            r" +
"eturn \'<a class=\"easyui-linkbutton\" onclick=\"ReadingOrEdit(\' + row.FormID + \')\" " +
"ck=\"1\">查看</a>\';\r\n                  //        }\r\n                          \r\n    " +
"              //    }\r\n                  //}\r\n                ]],\r\n             " +
"   onBeforeCheck: function (rowIndex, rowData) {\r\n                    return row" +
"Data._AllowCheck;\r\n                },\r\n                onClickRow: function (row" +
"Index, rowData) {\r\n                    if (!rowData._AllowCheck) {\r\n            " +
"            $(this).datagrid(\'unselectRow\', rowIndex);\r\n                    }\r\n " +
"               },\r\n                onLoadSuccess: function (data) {\r\n           " +
"         $(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"style\", \"di" +
"splay:none\");\r\n                    var rowViews = $(\"#IsoFormProjDelegateService" +
"Grid\").parent().find(\".datagrid-view2 .datagrid-body tr.datagrid-row\");\r\n       " +
"             for (var i = 0; i < data.rows.length; i++) {\r\n                     " +
"   if (!data.rows[i]._AllowCheck) {\r\n                            rowViews.filter" +
"(\"[datagrid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"disa" +
"bled\", \"disabled\");\r\n                        }\r\n                    }\r\n         " +
"       }\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n               " +
" buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n               " +
" height: 25,\r\n                prompt: \'项目名称、项目编号、申请原因\',\r\n                onClick" +
"Button: function () {\r\n                    JQ.datagrid.searchGrid(\r\n            " +
"            {\r\n                            dgID: \'IsoFormProjDelegateServiceGrid" +
"\',\r\n                            loadingType: \'datagrid\',\r\n                      " +
"      queryParams: {\r\n                                KeyJQSearch: $(\"#JQSearch\"" +
").val(),\r\n                                 //ProjectPhase: $(\'#ProjectPhase\').te" +
"xtbox(\'getValue\'),\r\n                            }\r\n                        });\r\n" +
"                }\r\n            });\r\n        });\r\n        //查看、维护     项目工代信息\r\n   " +
"     function ReadingOrEdit(FormID)\r\n        {\r\n            JQ.dialog.dialog({\r\n" +
"                title: \'项目工代信息\',\r\n                width: \'960\',\r\n               " +
" height: \'700\',\r\n                url: editUrl + \'?id=\' + FormID,\r\n              " +
"  onClose:function()\r\n                {\r\n                    $(\"#IsoFormProjDele" +
"gateServiceGrid\").datagrid(\"reload\");\r\n                }\r\n            });\r\n     " +
"   }\r\n    </script>\r\n");

            
            #line 144 "..\..\Views\IsoFormProjDelegateService\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormProjDelegateServiceGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormProjDelegateServicePanel\"");

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
