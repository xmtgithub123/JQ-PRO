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
    
    #line 4 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjDelegateServiceReg/list.cshtml")]
    public partial class _Views_IsoFormProjDelegateServiceReg_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjDelegateServiceReg_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
                     Write(Url.Action("json", "IsoFormProjDelegateServiceReg", new { @area = "ISO" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
                    Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
                     Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
                    Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var xml = \'\';\r\n        var EmpId = \'");

            
            #line 12 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
                Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'FormID\',//主键的字段\r\n    " +
"            JQID: \'IsoFormProjDelegateServiceRegGrid\',//数据表格ID\r\n                " +
"JQDialogTitle: \'工代登记信息\',//弹出窗体标题\r\n                JQWidth: \'960\',//弹出窗体宽\r\n      " +
"          JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//" +
"数据表格类型 [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r\n           " +
"     checkOnSelect: true,//是否包含check\r\n                toolbar: \'#IsoFormProjDele" +
"gateServiceRegPanel\',//工具栏的容器ID\r\n                JQExcludeCol: [1, 10],//导出execl" +
"排除的列\r\n                singleSelect: false,\r\n                columns: [[\r\n       " +
"           {\r\n                      field: \'ck\', align: \'center\', checkbox: true" +
", JQIsExclude: true\r\n\r\n                  },\r\n                  {\r\n              " +
"        title: \'项目名称\', field: \'ProjName\', width: \"19%\", align: \'left\', sortable:" +
" false\r\n                  },\r\n                  {\r\n                      title: " +
"\'项目编号\', field: \'ProjNumber\', width: \"12%\", align: \'left\', sortable: false\r\n     " +
"             },\r\n                  {\r\n                      title: \'项目负责人\', fiel" +
"d: \'ProjEmpName\', width: \"8%\", align: \'center\', sortable: false\r\n               " +
"   },\r\n                  {\r\n                      title: \'阶段\', field: \'ProjPhase" +
"Ids\', width: \"18%\", align: \'center\', sortable: false\r\n                  },\r\n    " +
"               {\r\n                       title: \'服务开始时间\', field: \'ColAttDate1\', " +
"width: \"8%\", align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormat" +
"terByT\r\n                   },\r\n                   {\r\n                       titl" +
"e: \'服务结束时间\', field: \'ColAttDate2\', width: \"8%\", align: \'center\', sortable: true," +
" formatter: JQ.tools.DateTimeFormatterByT\r\n                   },\r\n              " +
"    {\r\n                      title: \'登记人\', field: \'CreatorEmpName\', width: \"8%\"," +
" align: \'center\', sortable: true\r\n                  },\r\n                  {\r\n   " +
"                   title: \'登记日期\', field: \'CreationTime\', width: \"8%\", align: \'ce" +
"nter\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT\r\n               " +
"   },\r\n                  {\r\n                      title: \'操作\', field: \'Info\', wi" +
"dth: \"7%\", align: \'center\',\r\n                      formatter: function (val, row" +
") {\r\n                          if (row.CreatorEmpId == EmpId)//个人只能维护自己的\r\n      " +
"                    {\r\n                              return \'<a class=\"easyui-li" +
"nkbutton\" onclick=\"UpdateOrReadingInfo(\' + row.FormID + \')\">修改</a>\'\r\n           " +
"               }\r\n                          else {\r\n                            " +
"  return \'<a class=\"easyui-linkbutton\" onclick=\"UpdateOrReadingInfo(\' + row.Form" +
"ID + \')\">查看</a>\'\r\n                          }\r\n                      }\r\n        " +
"          }\r\n                ]]\r\n            });\r\n            $(\"#JQSearch\").tex" +
"tbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-se" +
"arch\',\r\n                height: 25,\r\n                prompt: \'项目名称、项目编号\',\r\n     " +
"           onClickButton: function () {\r\n                    JQ.datagrid.searchG" +
"rid(\r\n                        {\r\n                            dgID: \'IsoFormProjD" +
"elegateServiceRegGrid\',\r\n                            loadingType: \'datagrid\',\r\n " +
"                           queryParams: {\r\n                                KeyJQ" +
"Search: $(\"#JQSearch\").val(),\r\n                                //ProjectPhase: $" +
"(\'#ProjectPhase\').combotree(\"getValues\"),\r\n                            }\r\n      " +
"                  });\r\n                }\r\n            });\r\n        });\r\n\r\n      " +
"  //更新或者查看  工代登记信息\r\n        function UpdateOrReadingInfo(id) {\r\n            JQ.d" +
"ialog.dialog({\r\n                title: \'工代登记信息\',\r\n                width: 960,\r\n " +
"               height: 700,\r\n                url: editUrl + \'?id=\' + id,\r\n      " +
"          onClose:function()\r\n                {\r\n                    $(\"#IsoForm" +
"ProjDelegateServiceRegGrid\").datagrid(\"reload\");\r\n                }\r\n           " +
" });\r\n        }\r\n    </script>\r\n");

WriteLiteral("    ");

            
            #line 106 "..\..\Views\IsoFormProjDelegateServiceReg\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormProjDelegateServiceRegGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormProjDelegateServiceRegPanel\"");

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