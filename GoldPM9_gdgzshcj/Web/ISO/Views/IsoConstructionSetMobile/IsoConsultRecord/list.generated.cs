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
    
    #line 4 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoConstructionSetMobile/IsoConsultRecord/list.cshtml")]
    public partial class _Views_IsoConstructionSetMobile_IsoConsultRecord_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoConstructionSetMobile_IsoConsultRecord_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
               Write(Url.Action("json", "IsoConsultRecord",new { @area="Iso"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
                 Write(Url.Action("add","IsoConsultRecord",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
                  Write(Url.Action("edit", "IsoConsultRecord",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
                 Write(Url.Action("del", "IsoConsultRecord", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n        " +
"        JQID: \'IsoConsultRecordGrid\',//数据表格ID\r\n                JQDialogTitle: \'信" +
"息\',//弹出窗体标题\r\n                JQWidth: \'1024\',//弹出窗体宽\r\n                JQHeight: " +
"\'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,tre" +
"egrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                JQSearch: {\r\n" +
"                    id: \'JQSearch\',\r\n                    prompt: \'项目编号、项目名称\',\r\n " +
"                   loadingType: \'datagrid\',//默认值为datagrid可以不传\r\n                }" +
",\r\n                url: requestUrl,//请求的地址\r\n                checkOnSelect: true," +
"//是否包含check\r\n                JQExcludeCol: [1,8,9,10],//导出execl排除的列\r\n           " +
"     fitColumns: false,\r\n                nowrap: false,\r\n                toolbar" +
": \'#IsoConsultRecordPanel\',//工具栏的容器ID\r\n                columns: [[\r\n            " +
"        { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n  " +
"                  { title: \'项目编号\', field: \'ProjNumber\', width: 100, align: \'cent" +
"er\', sortable: true },\r\n                    { title: \'项目名称\', field: \'ProjName\', " +
"width: 300, align: \'center\', sortable: true },\r\n                    { title: \'洽商" +
"事项\', field: \'Consultation\', width: 100, align: \'center\', sortable: true },\r\n    " +
"                { title: \'建设单位\', field: \'BuildCompany\', width: 100, align: \'cent" +
"er\', sortable: true },\r\n                    { title: \'监管单位\', field: \'SupervisonC" +
"ompany\', width: 100, align: \'center\', sortable: true },\r\n                    { t" +
"itle: \'施工单位\', field: \'ConstructionCompany\', width: 100, align: \'center\', sortabl" +
"e: true },\r\n                    { field: \'JQFiles\', title: \'附件\', align: \'center\'" +
", width: 80, JQIsExclude: true, JQRefTable: \'IsoConsultRecord\' },\r\n             " +
"       {\r\n                        field: \"FlowProgress\", title: \"流程进度\", align: \"" +
"left\", halign: \"center\", width: \"8%\", formatter: JQ.Flow.showProgress(\"FlowID\", " +
"\"FlowName\", \"FlowStatusID\", \"FlowStatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\"" +
", \"");

            
            #line 45 "..\..\Views\IsoConstructionSetMobile\IsoConsultRecord\list.cshtml"
                                                                                                                                                                                                                                     Write(ViewBag.EmpId);

            
            #line default
            #line hidden
WriteLiteral("\")\r\n                    },\r\n                    {\r\n                        field:" +
" \'opt\', title: \'操作\', width: \"40\", align: \'center\', JQIsExclude: true,\r\n         " +
"               formatter: function (value, row, index) {\r\n                      " +
"      var title = \"查看\";\r\n                            if (row._EditStatus == 1) {" +
"\r\n                                title = \"修改\";\r\n                            }\r\n" +
"                            else if (row._EditStatus == 2) {\r\n                  " +
"              title = \"处理\";\r\n                            }\r\n                    " +
"        return \"<a class=\'easyui-linkbutton\' onclick=\'Look(\" + row.Id + \")\'>\" + " +
"title + \"</a>\";\r\n                        }\r\n                    }\r\n             " +
"   ]],\r\n                onBeforeCheck: function (rowIndex, rowData) {\r\n         " +
"           return rowData._AllowCheck;\r\n                },\r\n                onCl" +
"ickRow: function (rowIndex, rowData) {\r\n                    if (!rowData._AllowC" +
"heck) {\r\n                        $(this).datagrid(\'unselectRow\', rowIndex);\r\n   " +
"                 }\r\n                },\r\n                onLoadSuccess: function " +
"(data) {\r\n                    $(\"div.datagrid-header-check input[type=\'checkbox\'" +
"]\").attr(\"style\", \"display:none\");\r\n                    var rowViews = $(\"#IsoCo" +
"nsultRecordGrid\").parent().find(\".datagrid-view2 .datagrid-body tr.datagrid-row\"" +
");\r\n                    for (var i = 0; i < data.rows.length; i++) {\r\n          " +
"              if (!data.rows[i]._AllowCheck) {\r\n                            rowV" +
"iews.filter(\"[datagrid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck\'] :checkbox\")" +
".prop(\"disabled\", \"disabled\");\r\n                        }\r\n                    }" +
"\r\n                }\r\n            });\r\n        });\r\n\r\n        function Look(val) " +
"{\r\n            JQ.dialog.dialog({\r\n                title: \"施工洽商记录\",\r\n           " +
"     url: editUrl + \"?Id=\" + val,\r\n                width: \'1000\',\r\n             " +
"   height: \'100%\',\r\n                JQLoadingType: \'datagrid\',\r\n                " +
"iconCls: \'fa fa-search\',\r\n                onClose: function () {\r\n              " +
"      $(\"#IsoConsultRecordGrid\").datagrid(\"reload\");\r\n                }\r\n\r\n     " +
"       });\r\n        }\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoConsultRecordGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoConsultRecordPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral("  JQWhereOptions=\"{ \'Key\': \'ProjNumber,ProjName\', \'Contract\': \'like\' }\"");

WriteLiteral("/>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
