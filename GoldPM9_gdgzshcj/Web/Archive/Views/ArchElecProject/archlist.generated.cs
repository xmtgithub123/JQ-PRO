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
    
    #line 4 "..\..\Views\ArchElecProject\archlist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchElecProject/archlist.cshtml")]
    public partial class _Views_ArchElecProject_archlist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ArchElecProject_archlist_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ArchElecProject\archlist.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\ArchElecProject\archlist.cshtml"
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
"ojectId + \")\\\">归档</a>\";\r\n                             html += \"&nbsp;&nbsp;<a hr" +
"ef=\\\"javascript:GCHandler(\" + row.Id + \",\" + row.ProjectId + \")\\\">过程归档</a>\";\r\n  " +
"                           return html;\r\n                         }\r\n           " +
"          }\r\n                ]]\r\n            });\r\n            $(\"#JQSearch\").tex" +
"tbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-se" +
"arch\',\r\n                height: 25,\r\n                prompt: \'项目编号、项目名称\',\r\n     " +
"           onClickButton: function () {\r\n                    JQ.datagrid.searchG" +
"rid(\r\n                        {\r\n                            dgID: \'ArchElecProj" +
"ectGrid\',\r\n                            loadingType: \'datagrid\',\r\n               " +
"             queryParams: null\r\n                        });\r\n                }\r\n" +
"            });\r\n        });\r\n\r\n        function Handler(ArchProjId, ProjID) {\r\n" +
"            JQ.dialog.dialog({\r\n                title: \"归档\",\r\n                ur" +
"l: \'");

            
            #line 63 "..\..\Views\ArchElecProject\archlist.cshtml"
                 Write(Url.Action("treelist", "ArchElecProject",new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?IsRead=1&Id=' + ArchProjId + '&ProjID=' + ProjID,
                width: '1200',
                height: '600',
                JQID: 'ArchElecProjectGrid',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus'
            });
        }
        //过程归档
        function GCHandler(ArchProjId, ProjID) {
            JQ.dialog.dialogIframe({
                title: ""过程归档"",
                url: '");

            
            #line 75 "..\..\Views\ArchElecProject\archlist.cshtml"
                 Write(Url.Action("list", "ArchProjectFolder", new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?IsRead=1&Id=' + ArchProjId + '&ProjID=' + ProjID,
                width: '1200',
                height: '600',
                iconCls: 'fa fa-plus'
            });
        }

        //设置目录模板
        function SetProjectTemplate() {
            var rows = $(""#ArchElecProjectGrid"").datagrid(""getSelections"");
            if (rows.length == 0) {
                JQ.dialog.info(""请选择项目！"");
                return;
            }
            var ProjID = rows[0].ProjectId;
            JQ.dialog.dialog({
                title: ""过程归档目录模板设置"",
                url: '");

            
            #line 92 "..\..\Views\ArchElecProject\archlist.cshtml"
                 Write(Url.Action("settemplate", "ArchProjectFolder", new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral("\' + \'?ProjID=\' + ProjID,\r\n                width: \'800\',\r\n                height: " +
"\'600\',\r\n                iconCls: \'fa fa-plus\'\r\n            });\r\n        }\r\n\r\n   " +
" </script>\r\n");

WriteLiteral("    ");

            
            #line 100 "..\..\Views\ArchElecProject\archlist.cshtml"
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

WriteLiteral(">\r\n            <a");

WriteLiteral(" id=\"setdirtemp_a\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SetProjectTemplate()\"");

WriteLiteral(">设置模板</a>\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'ElecNumber,ElecName\', \'Contract\': \'like\' }\"");

WriteLiteral("  JQPermissionName=\"CanEdit\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 113 "..\..\Views\ArchElecProject\archlist.cshtml"
       Write(BaseData.getBases(new Params() { isMult = true, engName = "ProjectType", queryOptions = "{ Key:'AttTypeID2', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 114 "..\..\Views\ArchElecProject\archlist.cshtml"
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
