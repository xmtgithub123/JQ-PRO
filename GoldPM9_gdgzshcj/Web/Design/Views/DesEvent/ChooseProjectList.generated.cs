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
    
    #line 1 "..\..\Views\DesEvent\ChooseProjectList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesEvent/ChooseProjectList.cshtml")]
    public partial class _Views_DesEvent_ChooseProjectList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesEvent_ChooseProjectList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 3 "..\..\Views\DesEvent\ChooseProjectList.cshtml"
                 Write(Url.Action("ProjectPlanListJsonAll", "DesTask",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=");

            
            #line 3 "..\..\Views\DesEvent\ChooseProjectList.cshtml"
                                                                                                    Write(ViewBag.CompanyType);

            
            #line default
            #line hidden
WriteLiteral("&FormMenu=ProjectPlanList&TaskGroupId=");

            
            #line 3 "..\..\Views\DesEvent\ChooseProjectList.cshtml"
                                                                                                                                                              Write(Request.QueryString["TaskGroupId"]);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    var params = {\r\n    };\r\n\r\n    JQ.datagrid.datagrid({\r\n        JQPrimaryID" +
": \'Id\',//主键的字段\r\n        JQID: \'DesTaskGrid\',//数据表格ID\r\n        JQDialogTitle: \'信息" +
"\',//弹出窗体标题\r\n        JQWidth: \'768\',//弹出窗体宽\r\n        JQHeight: \'100%\',//弹出窗体高\r\n  " +
"      JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,datagrid]\r\n        JQFields: " +
"[\"Id\"],//追加的字段名\r\n        url: requestUrl,//请求的地址\r\n        checkOnSelect: false,\r" +
"\n        selectOnCheck: false,\r\n        singleSelect: true,\r\n        queryParams" +
": params,\r\n        toolbar: \'#DesTaskPanel\',//工具栏的容器ID\r\n        JQExcludeCol: [1" +
", 9],\r\n        columns: [[\r\n            { field: \'ck\', align: \'center\', checkbox" +
": true, JQIsExclude: true },\r\n            {\r\n                title: \'状态\', field:" +
" \'TaskGroupStatusName\', width: 40, align: \'center\', formatter: function (value, " +
"row, index) {\r\n                    var icon = \"\";\r\n                    var title" +
" = \"\";\r\n                    switch (row.TaskGroupStatus) {\r\n                    " +
"    case 0:\r\n                            title = \"未启动\";\r\n                       " +
"     icon = \"fa-circle-o\";\r\n                            break;\r\n                " +
"        case 1:\r\n                            title = \"已轮到\";\r\n                   " +
"         icon = \"fa-dot-circle-o\";\r\n                            break;\r\n        " +
"                case 2:\r\n                            title = \"进行中\";\r\n           " +
"                 icon = \"fa-play-circle\";\r\n                            break;\r\n " +
"                       case 3:\r\n                            title = \"已完成\";\r\n    " +
"                        icon = \"fa-check-circle\";\r\n                            b" +
"reak;\r\n                        case 4:\r\n                            title = \"已停止" +
"\";\r\n                            icon = \"fa-minus-circle\";\r\n                     " +
"       break;\r\n                    }\r\n                    row.TaskGroupStatusNam" +
"e = title;\r\n                    return \"<i class=\\\"fa \" + icon + \"\\\" title=\\\"\" +" +
" title + \"\\\"></i>\";\r\n                }\r\n            },\r\n            { title: \'项目" +
"编号\', field: \'ProjNumber\', width: \'10%\', align: \'center\', sortable: true },\r\n    " +
"        { title: \'项目名称\', field: \'ProjName\', width: \'30%\', align: \'left\', sortabl" +
"e: true },\r\n            { title: \'阶段\', field: \'ProjPhaseName\', width: \'10%\', ali" +
"gn: \'center\', sortable: true },\r\n            { title: \'负责人\', field: \'ProjPhaseEm" +
"pName\', width: \'10%\', align: \'center\', sortable: true },\r\n            { title: \'" +
"计划开始\', field: \'DatePlanStart\', width: \'10%\', align: \'center\', sortable: true, fo" +
"rmatter: JQ.tools.DateTimeFormatterByT },\r\n            { title: \'计划结束\', field: \'" +
"DatePlanFinish\', width: \'10%\', align: \'center\', sortable: true, formatter: JQ.to" +
"ols.DateTimeFormatterByT },\r\n        ]],\r\n        onClickRow: function (rowIndex" +
", rowData) {\r\n            $(\"#DesTaskGrid\").datagrid(\'unselectRow\', rowIndex);\r\n" +
"        },\r\n        onCheckAll: function (rows) {\r\n            $(\"#DesTaskGrid\")" +
".datagrid(\"getPanel\").find(\"input[type=\'checkbox\'][name=\'ck\']\").each(function (i" +
", CheckB) {\r\n                if ($(CheckB).is(\":hidden\")) {\r\n                   " +
" $(\"#DesTaskGrid\").datagrid(\'uncheckRow\', i);\r\n                }\r\n            })" +
";\r\n        }\r\n    });\r\n\r\n    $(\"#JQSearch\").textbox({\r\n        buttonText: \'筛选\'," +
"\r\n        buttonIcon: \'fa fa-search\',\r\n        height: 25,\r\n        prompt: \'项目名" +
"称、编号\',\r\n        onClickButton: function () {\r\n            JQ.datagrid.searchGrid" +
"(\r\n                {\r\n                    dgID: \'DesTaskGrid\',\r\n                " +
"    loadingType: \'datagrid\',\r\n                    queryParams: params\r\n         " +
"       });\r\n        }\r\n    });\r\n\r\n    function Sure() {\r\n        var rows = $(\'#" +
"DesTaskGrid\').datagrid(\'getChecked\');\r\n        if (rows.length < 1) {\r\n         " +
"   window.top.JQ.dialog.warning(\'您必须选择至少一项！！！\');\r\n            return;\r\n        }" +
"\r\n        for (var rowindex = 0; rowindex < rows.length; rowindex++) {\r\n        " +
"    insert(rows[rowindex]);\r\n        }\r\n        JQ.dialog.dialogClose();\r\n    }\r" +
"\n</script>\r\n\r\n<table");

WriteLiteral(" id=\"DesTaskGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"DesTaskPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-check-circle\'\"");

WriteLiteral(" onclick=\"Sure()\"");

WriteLiteral(">确定</a>\r\n    </span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'p.ProjNumber,p.ProjName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n</div>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591