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
    
    #line 4 "..\..\Views\ModelIsoCheck\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ModelIsoCheck/list.cshtml")]
    public partial class _Views_ModelIsoCheck_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ModelIsoCheck_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ModelIsoCheck\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\ModelIsoCheck\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 9 "..\..\Views\ModelIsoCheck\list.cshtml"
                     Write(Url.Action("json", "ModelIsoCheck",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 10 "..\..\Views\ModelIsoCheck\list.cshtml"
                    Write(Url.Action("add","ModelIsoCheck",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 11 "..\..\Views\ModelIsoCheck\list.cshtml"
                     Write(Url.Action("edit", "ModelIsoCheck",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               delUrl = \'");

            
            #line 12 "..\..\Views\ModelIsoCheck\list.cshtml"
                    Write(Url.Action("del", "ModelIsoCheck", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n              " +
"  JQButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n     " +
"           JQAddUrl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径" +
"\r\n                JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',/" +
"/主键的字段\r\n                JQID: \'ModelIsoCheckGrid\',//数据表格ID\r\n                JQDi" +
"alogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n              " +
"  JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [" +
"datagrid,treegrid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                u" +
"rl: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n       " +
"         toolbar: \'#ModelIsoCheckPanel\',//工具栏的容器ID\r\n                JQExcludeCol" +
": [1,8],\r\n                JQIsSearch: true,//是否加载数据之前就获取搜索参数值，可在搜索控件中添加默认值(默认为fa" +
"lse)\r\n                columns: [[\r\n                  { field: \'ck\', align: \'cent" +
"er\', checkbox: true, JQIsExclude: true },\r\n\t              { title: \'阶段\', field: " +
"\'PhaseName\', width: 100, align: \'left\', sortable: true },\r\n                  { t" +
"itle: \'专业\', field: \'SpecialName\', width: 100, align: \'left\', sortable: true },  " +
"            \r\n                  { title: \'错误类型\', field: \'ErrorTypeName\', width: " +
"100, align: \'left\', sortable: true },\r\n                  { title: \'差错内容\', field:" +
" \'CheckItem\', width: 300, align: \'left\', sortable: true },\r\n                  { " +
"title: \'备注\', field: \'CheckNote\', width: 300, align: \'left\', sortable: true },\r\n " +
"               {\r\n                    title: \'状态\', field: \'DeleterEmpId\', width:" +
" \"5%\", align: \'center\', sortable: true,\r\n                    formatter: function" +
" (val, row) {\r\n                        return val == \"停用\" ? \"<span style=\'color:" +
"red\'>停用</span>\" : \"<span style=\'color:green\'>启用</span>\";\r\n                    }\r" +
"\n                },\r\n                ]],\r\n                //queryParams: {\r\n    " +
"            //    queryInfo: \'[{\"Key\":\"DeleterEmpId\",\"Contract\":\"=\",\"filedType\":" +
"\"Int\",\"Value\":\"0\"}]\'\r\n                //}\r\n            });\r\n            $(\"#JQSe" +
"arch\").textbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon:" +
" \'fa fa-search\',\r\n                height: 25,\r\n                prompt: \'差错内容\',\r\n" +
"                onClickButton: function () {\r\n                    JQ.datagrid.se" +
"archGrid(\r\n                        {\r\n                            dgID: \'ModelIs" +
"oCheckGrid\',\r\n                            loadingType: \'datagrid\',\r\n            " +
"                queryParams: {\r\n                                //ProjectPhases:" +
" $(\'#ProjectPhase\').combotree(\"getValues\"),\r\n                            }\r\n    " +
"                    });\r\n                }\r\n            });\r\n            $(\'#isS" +
"tartSearch\').combobox({\r\n                editable:false ,\r\n                onSel" +
"ect: function () {\r\n                    var ss = document.getElementsByClassName" +
"(\"textbox-button textbox-button-right l-btn l-btn-small\");\r\n                    " +
"$(ss).click();\r\n                }\r\n            });\r\n        });\r\n\r\n    </script>" +
"\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"ModelIsoCheckGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"ModelIsoCheckPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n        ");

WriteLiteral("\r\n      \r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'CheckItem\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 87 "..\..\Views\ModelIsoCheck\list.cshtml"
       Write(BaseData.getBases(
           new Params() { isMult = true, engName = "ProjectPhase", queryOptions = "{ Key:'PhaseID', Contract:'in',filedType:'Int'}", }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("            ");

            
            #line 90 "..\..\Views\ModelIsoCheck\list.cshtml"
       Write(BaseData.getBases(
           new Params() { isMult = true, engName = "Special", queryOptions = "{ Key:'SpecialID', Contract:'in',filedType:'Int'}", }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n            ");

WriteLiteral("\r\n            <select");

WriteLiteral(" id=\"isStartSearch\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" jqwhereoptions=\"[{ Key:\'DeleterEmpId\', Contract:\'=\',filedType:\'Int\'}]\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"\"");

WriteLiteral(">所有状态</option>\r\n                <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(" selected=\"selected\"");

WriteLiteral(">启用</option>\r\n                <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">停用</option>\r\n            </select>&nbsp;\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591