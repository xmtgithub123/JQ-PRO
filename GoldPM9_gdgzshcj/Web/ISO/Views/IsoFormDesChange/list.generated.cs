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
    
    #line 4 "..\..\Views\IsoFormDesChange\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormDesChange/list.cshtml")]
    public partial class _Views_IsoFormDesChange_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormDesChange_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormDesChange\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoFormDesChange\list.cshtml"
                     Write(Url.Action("json", "IsoFormDesChange", new { @area="ISO"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\IsoFormDesChange\list.cshtml"
                    Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoFormDesChange\list.cshtml"
                     Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\IsoFormDesChange\list.cshtml"
                    Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var EmpId = \'");

            
            #line 11 "..\..\Views\IsoFormDesChange\list.cshtml"
                Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        var xml = \'\';\r\n        $(function () {\r\n            JQ.datagrid.datag" +
"rid({\r\n                JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮" +
"\r\n                JQAddUrl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl," +
"//编辑的路径\r\n                JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID:" +
" \'FormID\',//主键的字段\r\n                JQID: \'IsoFormDesChangeGid\',//数据表格ID\r\n       " +
"         JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'960\',//弹出窗体宽\r\n " +
"               JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagri" +
"d\',//数据表格类型 [datagrid,treegrid]\r\n                url: requestUrl,//请求的地址\r\n      " +
"          JQExcludeCol: [1, 9,10],//导出execl排除的列\r\n                checkOnSelect: " +
"true,//是否包含check\r\n                toolbar: \'#IsoFormDesChangePanel\',//工具栏的容器ID\r\n" +
"                singleSelect: false,\r\n                columns: [[\r\n             " +
"       { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n   " +
"                 {\r\n                        title: \'编号\', field: \'Number\', width:" +
" \"8%\", align: \'left\', sortable: true\r\n                    },\r\n                  " +
"{\r\n                      title: \'项目编号\', field: \'ProjNumber\', width: \"10%\", align" +
": \'left\', sortable: true\r\n                  },\r\n                  {\r\n           " +
"           title: \'项目名称\', field: \'ProjName\', width: \"20%\", align: \'left\', sortab" +
"le: true\r\n                  },\r\n                  {\r\n                      title" +
": \'发起人\', field: \'CreatorEmpName\', width: \"6%\", align: \'center\', sortable: true\r\n" +
"                  },\r\n                  {\r\n                      title: \'变更性质\', " +
"field: \'DesignChange\', width: \"14%\", align: \'center\', sortable: true\r\n          " +
"        },\r\n\r\n                {\r\n                    title: \'变更日期\', field: \'Form" +
"Date\', width: \"8%\", align: \'center\', sortable: true, formatter: JQ.tools.DateTim" +
"eFormatterByT\r\n                },\r\n                {\r\n                    title:" +
" \'变更原因\', field: \'DesignReason\', width: \"16%\", align: \'left\', sortable: true\r\n   " +
"             },\r\n                  {\r\n                      field: \"FlowProgress" +
"\", title: \"流程进度\", align: \"center\", halign: \"center\", width: \"8%\", formatter: JQ." +
"Flow.showFlowProgress(\"IsoFormDesChangeGid\", \"IsoFormDesChange\", \"FormID\")\r\n    " +
"              },\r\n                  {\r\n                      field: \'Info\', titl" +
"e: \'操作\', align: \'center\', width: \'6%\',\r\n                      formatter:function" +
"(val,row)\r\n                      {\r\n                          if (row.FlowStatus" +
" == \"0\") {\r\n                              return \'<a class=\"easyui-linkbutton\" o" +
"nclick=\"Info(\' + row.FormID + \')\">修改</a>\';\r\n                          }\r\n       " +
"                   else {\r\n                              if (row.FlowStatus == \"" +
"1\")\r\n                              {\r\n                                  return \'" +
"<a class=\"easyui-linkbutton\" onclick=\"Info(\' + row.FormID + \')\" ck=\"1\">处理</a>\';\r" +
"\n                              }\r\n                              else\r\n          " +
"                    {\r\n                                  return \'<a class=\"easyu" +
"i-linkbutton\" onclick=\"Info(\' + row.FormID + \')\" ck=\"1\">查看</a>\';\r\n              " +
"                }\r\n                          }\r\n                      }\r\n       " +
"           }\r\n                ]],\r\n                onClickRow: function (rowInde" +
"x, rowData) {\r\n\r\n                    if (rowData.FlowStatus != \"0\")//处于流程中的记录不允许" +
"选中（删除）\r\n                    {\r\n                        $(this).datagrid(\'unselec" +
"tRow\', rowIndex);\r\n                    }\r\n                    //else {\r\n        " +
"            //    if (rowData.CreatorEmpId != EmpId)//不是自己发起的、不处于审批中\r\n          " +
"          //    {\r\n                    //        $(this).datagrid(\'unselectRow\'," +
" rowIndex);\r\n                    //    }\r\n                    //}\r\n             " +
"   },\r\n                onLoadSuccess: function (row) {\r\n\r\n                    //" +
"禁用全选（避免失误删除）\r\n                    $(\"div.datagrid-header-check input[type=\'check" +
"box\']\").attr(\"disabled\", true);\r\n                    $(\'a[ck=\"1\"]\').parent().par" +
"ent().parent().find(\'input[type=\"checkbox\"]\').attr(\"disabled\", true);\r\n         " +
"       }\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n               " +
" buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n               " +
" height: 25,\r\n                prompt: \'项目编号、项目名称\',\r\n                onClickButto" +
"n: function () {\r\n                    JQ.datagrid.searchGrid(\r\n                 " +
"       {\r\n                            dgID: \'IsoFormDesChangeGid\',\r\n            " +
"                loadingType: \'datagrid\',\r\n                            queryParam" +
"s: {\r\n                                Keys: $(\"#JQSearch\").val(),\r\n             " +
"                   startDate: $(\"#startDate\").datebox(\'getValue\'),\r\n            " +
"                    endDate: $(\"#endDate\").datebox(\'getValue\')\r\n                " +
"            }\r\n                        });\r\n                }\r\n            });\r\n" +
"        });\r\n\r\n        function Info(Id)\r\n        {\r\n            JQ.dialog.dialo" +
"g({\r\n                title: \'设计变更信息\',\r\n                width: \'1000\',\r\n         " +
"       height: \'700\',\r\n                url: editUrl + \'?id=\' + Id,\r\n            " +
"    onClose:function()\r\n                {\r\n                    $(\"#IsoFormDesCha" +
"ngeGid\").datagrid(\"reload\");\r\n                }\r\n            });\r\n        }\r\n   " +
" </script>\r\n");

            
            #line 131 "..\..\Views\IsoFormDesChange\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormDesChangeGid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormDesChangePanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;border:none;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(" style=\"float:left;padding-bottom:5px;\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" style=\"float:left;border:none;padding-bottom:5px;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"margin-left:20px;\"");

WriteLiteral(">变更时间：</span>\r\n            <input");

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

WriteLiteral(" />\r\n        </div>\r\n        \r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591