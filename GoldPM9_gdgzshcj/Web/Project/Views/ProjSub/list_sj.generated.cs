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
    
    #line 4 "..\..\Views\ProjSub\list_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProjSub/list_sj.cshtml")]
    public partial class _Views_ProjSub_list_sj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ProjSub_list_sj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ProjSub\list_sj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\ProjSub\list_sj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n        " +
"        JQAddUrl: \'");

            
            #line 11 "..\..\Views\ProjSub\list_sj.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 12 "..\..\Views\ProjSub\list_sj.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 13 "..\..\Views\ProjSub\list_sj.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'ProjSubGrid',//数据表格ID
                JQDialogTitle: '信息',//弹出窗体标题
                JQWidth: '768',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                singleSelect: false,
                JQExcludeCol: [1,12,13,14],//导出execl排除的列
                url: '");

            
            #line 22 "..\..\Views\ProjSub\list_sj.cshtml"
                 Write(Url.Action("json","ProjSub",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n      " +
"          queryParams: { IsAudit: \"0\" },\r\n                toolbar: \'#ProjSubPane" +
"l\',//工具栏的容器ID\r\n                columns: [[\r\n                  { field: \'ck\', ali" +
"gn: \'center\', checkbox: true, JQIsExclude: true },\r\n                  { title: \'" +
"项目编号\', field: \'ProjNumber\', width: \"8%\", align: \'left\', sortable: true },\r\n     " +
"             { title: \'项目名称\', field: \'ProjName\', width: \"12%\", align: \'left\', so" +
"rtable: true },\r\n                  { title: \'外委项目编号\', field: \'SubNumber\', width:" +
" \"8%\", align: \'left\', sortable: true },\r\n                  { title: \'外委项目名称\', fi" +
"eld: \'SubName\', width: \"12%\", align: \'left\', sortable: true },\r\n                " +
"  { title: \'外委性质\', field: \'SubTypeName\', width: \"80\", align: \'center\', sortable:" +
" true },\r\n                  {\r\n                      title: \'合同是否签订\', field: \'Co" +
"nSubID\', width: \"80\", align: \'center\', sortable: true, formatter: function (valu" +
"e, row) {\r\n                          if (row.ConSubID > 0) {\r\n                  " +
"            row.ConSubID = \"是\";\r\n                          } else {\r\n           " +
"                   row.ConSubID = \"否\";\r\n                          }\r\n           " +
"               return row.ConSubID;\r\n                      }\r\n                  " +
"},\r\n                  { title: \'外委负责人\', field: \'SubEmpName\', width: \"80\", align:" +
" \'center\', sortable: true },\r\n                  { title: \'外委时间\', field: \'ColAttD" +
"ate1\', width: \"80\", align: \'center\', sortable: true, formatter: JQ.tools.DateTim" +
"eFormatterByT },\r\n                  { title: \'登记时间\', field: \'CreationTime\', widt" +
"h: \"80\", align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterB" +
"yT },\r\n                  { title: \'外委单位\', field: \'CustName\', width: \"12%\", align" +
": \'left\', sortable: true }, {\r\n                      field: \'JQFiles\', title: \'附" +
"件\', align: \'center\', width: \"40px\", JQIsExclude: true, JQExcludeFlag: \"grid_file" +
"s\", JQRefTable: \'ProjSub\', formatter: function (value, row) {\r\n                 " +
"         return \"<span id=\\\"grid_files_\" + row.Id + \"\\\"></span>\"\r\n              " +
"        }\r\n                  },\r\n                   {\r\n                       fi" +
"eld: \"FlowProgress\", title: \"流程进度\", align: \"left\", halign: \"center\", width: \"8%\"" +
", formatter: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"FlowStatusID\", \"FlowSta" +
"tusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 52 "..\..\Views\ProjSub\list_sj.cshtml"
                                                                                                                                                                                                                                    Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                  },\r\n                  {\r\n                      field: \"opt\"" +
", title: \"操作\", align: \"center\", halign: \"center\", width: \"40\",\r\n                " +
"      formatter: function (value, row, index) {\r\n                          var t" +
"itle = \"查看\";\r\n                          if (row._EditStatus == 1) {\r\n           " +
"                   title = \"修改\";\r\n                          }\r\n                 " +
"         else if (row._EditStatus == 2) {\r\n                              title =" +
" \"处理\";\r\n                          }\r\n                          return \'<a class=" +
"\"easyui-linkbutton\" onclick=\"ShowDetailsinfoDialogue(\' + row.Id + \')\">\' + title " +
"+ \'</a>\';\r\n                      }\r\n                  }\r\n                ]],\r\n  " +
"              onBeforeCheck: function (rowIndex, rowData) {\r\n                   " +
" return rowData._AllowCheck;\r\n                },\r\n                onClickRow: fu" +
"nction (rowIndex, rowData) {\r\n                    if (!rowData._AllowCheck) {\r\n " +
"                       $(this).datagrid(\'unselectRow\', rowIndex);\r\n             " +
"       }\r\n                },\r\n                onLoadSuccess: function (data) {\r\n" +
"                    $(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"" +
"style\", \"display:none\");\r\n                    var rowViews = $(\"#ProjSubGrid\").p" +
"arent().find(\".datagrid-view2 .datagrid-body tr.datagrid-row\");\r\n               " +
"     for (var i = 0; i < data.rows.length; i++) {\r\n                        if (!" +
"data.rows[i]._AllowCheck) {\r\n                            rowViews.filter(\"[datag" +
"rid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"disabled\", \"" +
"disabled\");\r\n                        }\r\n                    }\r\n                }" +
"\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonT" +
"ext: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height:" +
" 25,\r\n                prompt: \'项目编号,项目名称,外委项目编号,外委项目名称,外委单位\',\r\n                o" +
"nClickButton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n      " +
"                  {\r\n                            dgID: \'ProjSubGrid\',\r\n         " +
"                   loadingType: \'datagrid\'\r\n                        });\r\n       " +
"         }\r\n            });\r\n        });\r\n\r\n        function ShowDetailsinfoDial" +
"ogue(id) {\r\n            JQ.dialog.dialog({\r\n                title: \"查看项目外委明细\",\r\n" +
"                url: \'");

            
            #line 104 "..\..\Views\ProjSub\list_sj.cshtml"
                 Write(Url.Action("edit", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=SJ&id=' + id,
                width: '768',
                height: '100%',
                JQID: 'TendersInfoCost',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus',

            });
        }

        window.refreshGrid = function () {
            $(""#JQSearch"").textbox(""button"").click();
        }
    </script>

");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"ProjSubGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"ProjSubPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 125 "..\..\Views\ProjSub\list_sj.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjSubType", queryOptions = "{ Key:'ProjSubTypeState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;\"");

WriteLiteral(">\r\n                外委时间\r\n                <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate1S\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n                至\r\n                <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate1E\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;\"");

WriteLiteral(">\r\n                登记时间\r\n                <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CreationTimeS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n                至\r\n                <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CreationTimeE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            </span>\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
