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
    
    #line 4 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjSubYS/list_cj.cshtml")]
    public partial class _Views_IsoFormProjSubYS_list_cj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoFormProjSubYS_list_cj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 8 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                     Write(Url.Action("GetJsonList", "IsoFormProjSubYS", new { @area="Iso"}));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',\r\n               addUrl = \'");

            
            #line 9 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                    Write(Url.Action("add","IsoFormProjSubYS",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',\r\n               editUrl = \'");

            
            #line 10 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                     Write(Url.Action("edit", "IsoFormProjSubYS",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',\r\n               delUrl = \'");

            
            #line 11 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                    Write(Url.Action("del", "IsoFormProjSubYS", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n              " +
"  JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n     " +
"           JQAddUrl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径" +
"\r\n                JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'FormI" +
"D\',//主键的字段\r\n                JQID: \'IsoFormProjSubYSGrid\',//数据表格ID\r\n             " +
"   JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'1024\',//弹出窗体宽\r\n      " +
"          JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//" +
"数据表格类型 [datagrid,treegrid]\r\n                JQFields: [\"FormID\"],//追加的字段名\r\n     " +
"           url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含ch" +
"eck\r\n                toolbar: \'#IsoFormProjSubYSPanel\',//工具栏的容器ID\r\n             " +
"   singleSelect: true,\r\n                JQExcludeCol: [1, 10, 11, 12],//导出execl排" +
"除的列\r\n                nowrap:true,\r\n                columns: [[\r\n                " +
" { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n         " +
"         { title: \'外委项目编号\', field: \'SubNumber\', width: \"10%\", align: \'left\' },\r\n" +
"                  { title: \'外委项目名称\', field: \'SubName\', width: \"15%\", align: \'lef" +
"t\' },\r\n                  { title: \'外委合同编号\', field: \'ConSubNumber2\', width: \"10%\"" +
", align: \'left\' },\r\n                  { title: \'外委合同名称\', field: \'ConSubName2\', w" +
"idth: \"15%\", align: \'left\'},\r\n                  { title: \'外委单位\', field: \'custNam" +
"e\', width: \"15%\", align: \'left\' },\r\n                  { title: \'验收结论\', field: \'Y" +
"SQKJL\', width: \"10%\", align: \'left\' },\r\n                  { title: \'登记时间\', field" +
": \'ColAttDate1\', width: \"80\", align: \'center\', formatter: JQ.tools.DateTimeForma" +
"tterByT },\r\n                  { title: \'验收时间\', field: \'ColAttDate2\', width: \"80\"" +
", align: \'center\', formatter: JQ.tools.DateTimeFormatterByT },\r\n                " +
"  {\r\n                      field: \'JQFiles\', title: \'附件\', align: \'center\', width" +
": \"40px\", JQIsExclude: true, JQExcludeFlag: \"grid_files\", JQRefTable: \'IsoForm\'," +
" formatter: function (value, row) {\r\n                          return \"<span id=" +
"\\\"grid_files_\" + row.FormID + \"\\\"></span>\"\r\n                      }\r\n           " +
"       },\r\n                 {\r\n                     field: \"FlowProgress\", title" +
": \"流程进度\", align: \"left\", halign: \"center\", width: \"8%\", formatter: JQ.Flow.showP" +
"rogress(\"FlowID\", \"FlowName\", \"FlowStatusID\", \"FlowStatusName\", \"FlowTurnedEmpID" +
"s\", \"CreatorEmpId\", \"");

            
            #line 48 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                                                                                                                                                                                                                                  Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                 },\r\n                  {\r\n                      field: \"opt\"," +
" title: \"操作\", align: \"center\", halign: \"center\", width: \"40\",\r\n                 " +
"     formatter: function (value, row, index) {\r\n                          var ti" +
"tle = \"查看\";\r\n                          if (row._EditStatus == 1) {\r\n            " +
"                  title = \"修改\";\r\n                          }\r\n                  " +
"        else if (row._EditStatus == 2) {\r\n                              title = " +
"\"处理\";\r\n                          }\r\n                          return \'<a class=\"" +
"easyui-linkbutton\" onclick=\"ShowInfoYSDialogue(\' + row.FormID + \')\">\' + title + " +
"\'</a>\';\r\n                      }\r\n                  }\r\n                ]],\r\n    " +
"            onBeforeCheck: function (rowIndex, rowData) {\r\n                    r" +
"eturn rowData._AllowCheck;\r\n                },\r\n                onClickRow: func" +
"tion (rowIndex, rowData) {\r\n                    if (!rowData._AllowCheck) {\r\n   " +
"                     $(this).datagrid(\'unselectRow\', rowIndex);\r\n               " +
"     }\r\n                },\r\n                onLoadSuccess: function (row) {\r\n   " +
"                 $(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"sty" +
"le\", \"display:none\");\r\n                    var rowViews = $(\"#IsoFormProjSubYSGr" +
"id\").parent().find(\".datagrid-view2 .datagrid-body tr.datagrid-row\");\r\n         " +
"           for (var i = 0; i < row.rows.length; i++) {\r\n                        " +
"if (!row.rows[i]._AllowCheck) {\r\n                            rowViews.filter(\"[d" +
"atagrid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\").prop(\"disabled" +
"\", \"disabled\");\r\n                        }\r\n                    }\r\n             " +
"   }\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n                but" +
"tonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                hei" +
"ght: 25,\r\n                prompt: \'外委项目名称、外委项目编号、外委合同名称、外委合同编号、外委单位\',\r\n         " +
"       onClickButton: function () {\r\n                    JQ.datagrid.searchGrid(" +
"\r\n                        {\r\n                            dgID: \'IsoFormProjSubYS" +
"Grid\',\r\n                            loadingType: \'datagrid\'\r\n                   " +
"     });\r\n                }\r\n            });\r\n        });\r\n\r\n        function Sh" +
"owInfoYSDialogue(Id) {\r\n            JQ.dialog.dialog({\r\n                title: \"" +
"项目外委验收\",\r\n                url: \'");

            
            #line 100 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
                 Write(Url.Action("edit", "IsoFormProjSubYS",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ&Id=\' + Id,\r\n                width: \'1024\',\r\n                heigh" +
"t: \'100%\'\r\n            });\r\n        }\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormProjSubYSGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormProjSubYSPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 112 "..\..\Views\IsoFormProjSubYS\list_cj.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjSubType", queryOptions = "{ Key:'ProjSubTypeState', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            登记时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate1S\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate1E\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            验收时间\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate2S\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ColAttDate2E\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
