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
    
    #line 4 "..\..\Views\ArchElecProject\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchElecProject/list.cshtml")]
    public partial class _Views_ArchElecProject_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ArchElecProject_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ArchElecProject\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\ArchElecProject\list.cshtml"
                     Write(Url.Action("json", "ArchElecProject",new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'export\', \'moreSearch\'],//需要显示的按钮\r\n                JQPrimaryID: " +
"\'Id\',//主键的字段\r\n                JQID: \'ArchElecProjectGrid\',//数据表格ID\r\n            " +
"    JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n      " +
"          JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//" +
"数据表格类型 [datagrid,treegrid]\r\n                JQFields: [\"Id\", \"ProjectId\"],//追加的字" +
"段名\r\n                url: requestUrl,//请求的地址\r\n                checkOnSelect: true" +
",//是否包含check\r\n                JQIsSearch: true,\r\n                fitColumns: tru" +
"e,\r\n                nowrap: false,\r\n                JQExcludeCol: [9],\r\n        " +
"        toolbar: \'#ArchElecProjectPanel\',//工具栏的容器ID\r\n                columns: [[" +
"\r\n\t\t            { title: \'项目编号\', field: \'ElecNumber\', width: 100, align: \'center" +
"\', sortable: true },\r\n\t\t            { title: \'项目名称\', field: \'ElecName\', width: 1" +
"00, align: \'center\', sortable: true },\r\n\t\t            { title: \'立项时间\', field: \'D" +
"ateCreate\', width: 100, align: \'center\', sortable: true, formatter: JQ.tools.Dat" +
"eTimeFormatterByT },\r\n\t\t            { title: \'顾客名称\', field: \'CustName\', width: 1" +
"00, align: \'center\', sortable: true },\r\n\t\t            { title: \'项目经理\', field: \'P" +
"rojEmpName\', width: 100, align: \'center\', sortable: true },\r\n\t\t            { tit" +
"le: \'项目阶段\', field: \'PhaseName\', width: 100, align: \'center\', sortable: true },\r\n" +
"                    { title: \'类别\', field: \'AttTypeName2\', JQfield: \'FK_ArchElecP" +
"roject_AttTypeID2.BaseName as AttTypeName2\', width: 100, align: \'center\', sortab" +
"le: true },\r\n                    { title: \'性质\', field: \'AttTypeName3\', JQfield: " +
"\'FK_ArchElecProject_AttTypeID3.BaseName as AttTypeName3\', width: 100, align: \'ce" +
"nter\', sortable: true },\r\n                     {\r\n                         field" +
": \'ProjectId\', title: \'操作\', width: 100, align: \'center\', JQIsExclude: true,\r\n   " +
"                      formatter: function (value, row, index) {\r\n               " +
"              var html = \"<a href=\\\"javascript:Handler(\" + row.Id + \",\" + row.Pr" +
"ojectId + \")\\\">归档</a>\";\r\n                             return html;\r\n            " +
"             }\r\n                     }\r\n                ]]\r\n            });\r\n\r\n " +
"           $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n        " +
"        buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n              " +
"  prompt: \'项目编号、项目名称\',\r\n                onClickButton: function () {\r\n          " +
"          JQ.datagrid.searchGrid(\r\n                        {\r\n                  " +
"          dgID: \'ArchElecProjectGrid\',\r\n                            loadingType:" +
" \'datagrid\',\r\n                            queryParams: null\r\n                   " +
"     });\r\n                }\r\n            });\r\n        });\r\n\r\n        function Ha" +
"ndler(ArchProjId, ProjID) {\r\n            JQ.dialog.dialog({\r\n                tit" +
"le: \"归档\",\r\n                url: \'");

            
            #line 63 "..\..\Views\ArchElecProject\list.cshtml"
                 Write(Url.Action("treelist", "ArchElecProject",new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?Id=' + ArchProjId + '&ProjID=' + ProjID,
                width: '1200',
                height: '600',
                JQID: 'ArchElecProjectGrid',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus'
            });
        }
    </script>
");

WriteLiteral("    ");

            
            #line 72 "..\..\Views\ArchElecProject\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"ArchElecProjectGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"ArchElecProjectPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'ElecNumber,ElecName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 84 "..\..\Views\ArchElecProject\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjectType", queryOptions = "{ Key:'AttTypeID2', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 85 "..\..\Views\ArchElecProject\list.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjProperty", queryOptions = "{ Key:'AttTypeID3', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"hide\"");

WriteLiteral(" value=\"114\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'ElecType\', \'Contract\':\'=\',filedType:\'Int\' }\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591