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
    
    #line 4 "..\..\Views\IsoAbstract\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoAbstract/list.cshtml")]
    public partial class _Views_IsoAbstract_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoAbstract_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoAbstract\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 8 "..\..\Views\IsoAbstract\list.cshtml"
                     Write(Url.Action("json", "IsoAbstract", new { @area="ISO"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               editUrl = \'");

            
            #line 9 "..\..\Views\IsoAbstract\list.cshtml"
                     Write(Url.Action("edit", "IsoAbstract", new {@area= "ISO" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'moreSearch\'],//需要显示的按钮\r\n                JQIsSearch: true,\r\n    " +
"            JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'tableGrid\',//数据表格I" +
"D\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n                JQWidth: \'1024\'" +
",//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQLoadingTyp" +
"e: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQFields: [\"Id\"],//追" +
"加的字段名\r\n                url: requestUrl,//请求的地址\r\n                checkOnSelect: t" +
"rue,//是否包含check\r\n                toolbar: \'#tablePanel\',//工具栏的容器ID\r\n            " +
"    JQExcludeCol: [1, 9],\r\n                JQIsSearch: true,//是否加载数据之前就获取搜索参数值，可" +
"在搜索控件中添加默认值(默认为false)\r\n                columns: [[\r\n                  { field: \'" +
"ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                  { " +
"title: \'项目编号\', field: \'ProjNumber\', width: \"10%\", halign: \'center\', align: \'left" +
"\', sortable: true },\r\n                  { title: \'项目名称\', field: \'ProjName\', widt" +
"h: \"15%\", halign: \'center\', align: \'left\', sortable: true },\r\n\t              { t" +
"itle: \'阶段\', field: \'PhaseName\', width: \"10%\", halign: \'center\', align: \'left\', s" +
"ortable: true },\r\n                  { title: \'专业\', field: \'SpecialName\', width: " +
"\"10%\", halign: \'center\', align: \'left\', sortable: true },\r\n                  { t" +
"itle: \'卷册信息\', field: \'TaskName\', width: \"10%\", halign: \'center\', align: \'left\', " +
"sortable: true },\r\n                  { title: \'错误类型\', field: \'ErrorTypeName\', wi" +
"dth: \"80\", halign: \'center\', align: \'center\', sortable: true },\r\n               " +
"   { title: \'错误描述\', field: \'CheckNote\', width: \"15%\", halign: \'center\', align: \'" +
"left\', sortable: true },\r\n                  { title: \'设计人\', field: \'DesignEmpNam" +
"e\', width: 80, halign: \'center\', align: \'center\', sortable: true },\r\n           " +
"       { title: \'提出人\', field: \'CheckEmpName\', width: 80, halign: \'center\', align" +
": \'center\', sortable: true }\r\n                ]],\r\n                queryParams: " +
"{\r\n                    \'ModelIsoCheckID\': \'-1\'\r\n                }\r\n            }" +
");\r\n\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n " +
"               buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n       " +
"         prompt: \'错误描述\',\r\n                onClickButton: function () {\r\n        " +
"            JQ.datagrid.searchGrid(\r\n                        {\r\n                " +
"            dgID: \'tableGrid\',\r\n                            loadingType: \'datagr" +
"id\',\r\n                            queryParams: {\r\n                              " +
"  \'ModelIsoCheckID\': \'-1\'\r\n                            }\r\n                      " +
"  });\r\n                }\r\n            });\r\n        });\r\n\r\n\r\n\r\n        function s" +
"etExtract() {\r\n            var selections = $(\"#tableGrid\").datagrid(\"getSelecti" +
"ons\");\r\n            if (selections.length < 1) {\r\n                window.top.JQ." +
"dialog.warning(\'您必须选择至少一项进行差错模板导入！！！\');\r\n                return;\r\n            }\r" +
"\n            var idSet = \"\";\r\n            for (var i = 0; i < selections.length;" +
" i++) {\r\n                if (i != 0) {\r\n                    idSet += \",\";\r\n     " +
"           }\r\n                idSet += selections[i].Id;\r\n            }\r\n       " +
"     var confirmed = function () {\r\n                window.top.$.messager.progre" +
"ss();\r\n                $.ajax({\r\n                    url: \"");

            
            #line 79 "..\..\Views\IsoAbstract\list.cshtml"
                      Write(Url.Action("setExtract", "IsoAbstract", new { @area= "ISO" }));

            
            #line default
            #line hidden
WriteLiteral(@""",
                    type: ""POST"",
                    data: { idSet: idSet },
                    success: function (result) {
                        if (result.Result == false) {
                            JQ.dialog.alert(""操作失败！"");
                            return;
                        }
                        window.refreshGrid();
                    }, complete: function () {
                        window.top.$.messager.progress(""close"");
                    }
                });
            };
            JQ.dialog.confirm(""确定要将选中的消息导入到差错模板吗？"", confirmed);
        }

        window.refreshGrid = function () {
            JQ.datagrid.searchGrid(
                       {
                           dgID: 'tableGrid',
                           loadingType: 'datagrid',
                           queryParams: {
                               'ModelIsoCheckID': '-1'
                           }
                       });
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"tableGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"tablePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-check-circle\'\"");

WriteLiteral(" onclick=\"setExtract()\"");

WriteLiteral(">导入模板库</a>\r\n        </span>\r\n        &nbsp;&nbsp;<span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>&nbsp;&nbsp;&nbsp;\r\n        <select");

WriteLiteral(" id=\"isStartSearch\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" JQWhereOptions=\"[{ Key:\'IsExtract\', Contract:\'=\',filedType:\'Int\'}]\"");

WriteLiteral(">\r\n            <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(" selected=\"selected\"");

WriteLiteral(">未提取</option>\r\n            <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">已提取</option>\r\n        </select>&nbsp;\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'CheckNote\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 123 "..\..\Views\IsoAbstract\list.cshtml"
       Write(BaseData.getBases(
           new Params() { isMult = true, engName = "ProjectPhase", queryOptions = "{ Key:'PhaseID', Contract:'in',filedType:'Int'}", }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("            ");

            
            #line 126 "..\..\Views\IsoAbstract\list.cshtml"
       Write(BaseData.getBases(
           new Params() { isMult = true, engName = "Special", queryOptions = "{ Key:'SpecialID', Contract:'in',filedType:'Int'}", }));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

WriteLiteral("            ");

            
            #line 129 "..\..\Views\IsoAbstract\list.cshtml"
       Write(BaseData.getBaseDataSystem(
           new Params() { isMult = true, engName = "DesignErrorType", queryOptions = "{ Key:'CheckErrTypeID', Contract:'in',filedType:'Int'}", }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
