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
    
    #line 4 "..\..\Views\ModelVolCatalog\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ModelVolCatalog/list.cshtml")]
    public partial class _Views_ModelVolCatalog_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ModelVolCatalog_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ModelVolCatalog\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\ModelVolCatalog\list.cshtml"
                     Write(Url.Action("json", "ModelVolCatalog", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\ModelVolCatalog\list.cshtml"
                    Write(Url.Action("add", "ModelVolCatalog", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\ModelVolCatalog\list.cshtml"
                     Write(Url.Action("edit", "ModelVolCatalog", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 10 "..\..\Views\ModelVolCatalog\list.cshtml"
                    Write(Url.Action("del", "ModelVolCatalog", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAdd" +
"Url: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n              " +
"  JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n        " +
"        JQID: \'ModelVolCatalogGrid\',//数据表格ID\r\n                JQDialogTitle: \'任务" +
"模板信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight" +
": \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,t" +
"reegrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                url: reques" +
"tUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                J" +
"QExcludeCol: [1, 7],//导出execl排除的列\r\n                toolbar: \'#ModelVolCatalogPan" +
"el\',//工具栏的容器ID\r\n                columns: [[\r\n                  { field: \'ck\', al" +
"ign: \'center\', checkbox: true, JQIsExclude: false },\r\n\t\t{ title: \'阶段\', field: \'P" +
"haseIDName\', width: \"15%\", align: \'center\', sortable: false },\r\n        { title:" +
" \'专业\', field: \'SpecialIDName\', width: \"15%\", align: \'center\', sortable: false }," +
"\r\n\t\t{ title: \'编号\', field: \'ModelVolNumber\', width: \"15%\", align: \'center\', sorta" +
"ble: true },\r\n\t\t{ title: \'名称\', field: \'ModelVolName\', width: \"25%\", align: \'cent" +
"er\', sortable: true },\r\n\t\t{ title: \'备注\', field: \'ModelVolNote\', width: \"20%\", al" +
"ign: \'center\', sortable: true },\r\n        {\r\n            title: \'操作\', field: \'Ed" +
"it\', width: \'5%\', align: \'center\',\r\n            formatter: function (val,row) {\r" +
"\n                return \'<a class=\"easyui-linkbutton\" onclick=\"EditOrReadingInfo" +
"(\'+row.Id+\')\">修改</a>\'\r\n            }\r\n        }\r\n                ]]\r\n           " +
" });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n " +
"               buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n       " +
"         prompt: \'编号、名称\',\r\n                queryOptions: { \'Key\': \'ModelVolNumbe" +
"r,ModelVolName\', \'Contract\': \'like\' },\r\n                onClickButton: function " +
"() {\r\n                    JQ.datagrid.searchGrid(\r\n                        {\r\n  " +
"                          dgID: \'ModelVolCatalogGrid\',\r\n                        " +
"    loadingType: \'datagrid\',\r\n                            queryParams: null\r\n   " +
"                     });\r\n                }\r\n            });\r\n        });\r\n\r\n   " +
"     //查看或者编辑  任务模板信息\r\n        function EditOrReadingInfo(Id)\r\n        {\r\n      " +
"      JQ.dialog.dialog({\r\n                title: \'任务模板信息\',\r\n                widt" +
"h: \'800\',\r\n                height: \'600\',\r\n                url: editUrl + \'?id=\'" +
" + Id,\r\n                onClose:function()\r\n                {\r\n                 " +
"   $(\"#ModelVolCatalogGrid\").datagrid(\"reload\");\r\n                }\r\n           " +
" });\r\n        }\r\n    </script>\r\n");

            
            #line 75 "..\..\Views\ModelVolCatalog\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"ModelVolCatalogGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"ModelVolCatalogPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n");

WriteLiteral("        ");

            
            #line 84 "..\..\Views\ModelVolCatalog\list.cshtml"
   Write(BaseData.getBases(
                                                                 new Params() { isMult = true, engName = "ProjectPhase", queryOptions = "{ Key:'PhaseID', Contract:'in',filedType:'Int'}" },
                                                         new Params() { isMult = true, engName = "Special", queryOptions = "{ Key:'SpecialID', Contract:'in',filedType:'Int'}" }
           ));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'ModelVolNumber,ModelVolName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
