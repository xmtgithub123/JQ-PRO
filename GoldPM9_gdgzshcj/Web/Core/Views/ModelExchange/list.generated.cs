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
    
    #line 4 "..\..\Views\ModelExchange\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ModelExchange/list.cshtml")]
    public partial class _Views_ModelExchange_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ModelExchange_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ModelExchange\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAddU" +
"rl: \'");

            
            #line 10 "..\..\Views\ModelExchange\list.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 11 "..\..\Views\ModelExchange\list.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 12 "..\..\Views\ModelExchange\list.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'ModelExchangeGrid',//数据表格ID
                JQDialogTitle: '提资模板信息',//弹出窗体标题
                JQWidth: '768',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                //JQCustomSearch: [  //自定义搜索的字段
                //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
                //],
                JQExcludeCol: [1, 7],//导出execl排除的列
                //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
                //JQFields: ["""", """"],//追加的字段名
                url: '");

            
            #line 26 "..\..\Views\ModelExchange\list.cshtml"
                 Write(Url.Action("json","ModelExchange",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                toolb" +
"ar: \'#ModelExchangePanel\',//工具栏的容器ID\r\n                columns: [[\r\n             " +
"     { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t\t\t " +
" \t\t//{ title: \'自增\', field: \'Id\', width: 100, align: \'center\', sortable: true },\r" +
"\n                        { title: \'阶段\', field: \'ModelExchangePhaseID\', width: \"1" +
"2%\", align: \'center\', sortable: true },\r\n                        { title: \'专业\', " +
"field: \'ModelExchangeSpecID\', width: \"12%\", align: \'center\', sortable: true },\r\n" +
"                        { title: \'资料名称\', field: \'ModelExchangeName\', width: \"20%" +
"\", align: \'center\', sortable: true },\r\n\t\t{ title: \'资料内容\', field: \'ModelExchangeC" +
"ontent\', width: \"22%\", align: \'center\', sortable: true },\r\n\t     \t { title: \'接收专" +
"业\', field: \'ReciveSpecName\', width: \"25%\", align: \'center\', sortable: false },\r\n" +
"             {\r\n                 title:\'操作\',field:\'edit\',width:\'5%\',align:\'cente" +
"r\',\r\n                     formatter:function(val,row)\r\n                     {\r\n " +
"                        return \'<a  class=\"easyui-linkbutton\" onclick=\"EditOrRea" +
"dingInfo(\'+row.Id+\')\">修改</a>\'\r\n                     }\r\n             }\r\n         " +
"       ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n              " +
"  buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n              " +
"  height: 25,\r\n                prompt: \'资料名称,资料内容\',\r\n                queryOption" +
"s: { \'Key\': \'ModelExchangeName,ModelExchangeContent\', \'Contract\': \'like\' },\r\n   " +
"             onClickButton: function () {\r\n                    JQ.datagrid.searc" +
"hGrid(\r\n                        {\r\n                            dgID: \'ModelExcha" +
"ngeGrid\',\r\n                            loadingType: \'datagrid\',\r\n               " +
"             queryParams: {\r\n                                KeyJQSearch: $(\"#JQ" +
"Search\").val(),\r\n                                //ProjectPhase: $(\'#ProjectPhas" +
"e\').textbox(\'getValue\'),\r\n                                //Special: $(\'#Special" +
"\').textbox(\'getValue\')\r\n                            }\r\n                        }" +
");\r\n                }\r\n            });\r\n        });\r\n\r\n        // 查看或者编辑  提资模板信息" +
"\r\n        function EditOrReadingInfo(Id)\r\n        {\r\n            JQ.dialog.dialo" +
"g({\r\n                title: \'提资模板信息\',\r\n                height: \'600\',\r\n         " +
"       width: \'800\',\r\n                url: \'");

            
            #line 74 "..\..\Views\ModelExchange\list.cshtml"
                 Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + Id,\r\n                onClose:function()\r\n                {\r\n             " +
"       $(\"#ModelExchangeGrid\").datagrid(\"reload\");\r\n                }\r\n         " +
"   });\r\n        }\r\n    </script>\r\n");

WriteLiteral("    ");

            
            #line 82 "..\..\Views\ModelExchange\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"ModelExchangeGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"ModelExchangePanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n");

WriteLiteral("        ");

            
            #line 91 "..\..\Views\ModelExchange\list.cshtml"
   Write(BaseData.getBases(
                                                      new Params() { isMult = true, engName = "ProjectPhase", queryOptions = "{ Key:'ModelExchangePhaseID', Contract:'in',filedType:'Int'}" },
                                             new Params() { isMult = true, engName = "Special", queryOptions = "{ Key:'ModelExchangeSpecID', Contract:'in',filedType:'Int'}" }
           ));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'ModelExchangeName,ModelExchangeContent\', \'Contract\': \'li" +
"ke\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591