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
    
    #line 4 "..\..\Views\BaseEmpAgen\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseEmpAgen/list.cshtml")]
    public partial class _Views_BaseEmpAgen_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BaseEmpAgen_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BaseEmpAgen\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\BaseEmpAgen\list.cshtml"
                     Write(Url.Action("json", "BaseEmpAgen",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n               addUrl = \'");

            
            #line 8 "..\..\Views\BaseEmpAgen\list.cshtml"
                    Write(Url.Action("add","BaseEmpAgen",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\'],//需要显示的按钮\r\n                JQAddUrl: addUrl, //添加的路径\r\n   " +
"             JQPrimaryID: \'BaseEmpAgenID\',//主键的字段\r\n                JQID: \'BaseEm" +
"pAgenGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出窗体标题\r\n             " +
"   JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'600\',//弹出窗体高\r\n            " +
"    JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQFi" +
"elds: [\"BaseEmpAgenID\"],//追加的字段名\r\n                url: requestUrl,//请求的地址\r\n     " +
"           fitColumns: true,\r\n                checkOnSelect: true,//是否包含check\r\n " +
"               toolbar: \'#BaseEmpAgenPanel\',//工具栏的容器ID\r\n                columns:" +
" [[\r\n                        { field: \'ck\', align: \'center\', checkbox: true, JQI" +
"sExclude: true },\r\n\t\t                { title: \'委托人姓名\', field: \'FromEmpName\', wid" +
"th: 100, align: \'center\', sortable: true },\r\n\t\t                { title: \'代理人姓名\'," +
" field: \'ToEmpName\', width: 100, align: \'center\', sortable: true },\r\n\t\t         " +
"       { title: \'委托流程\', field: \'AgenFlowName\', width: 100, align: \'center\', sort" +
"able: true },\r\n\t\t                { title: \'委托菜单\', field: \'AgenMenuName\', width: " +
"100, align: \'center\', sortable: true },\r\n\t\t                { title: \'创建日期\', fiel" +
"d: \'DateCreate\', width: 100, align: \'center\', sortable: true, formatter: JQ.tool" +
"s.DateTimeFormatterByT },\r\n\t\t                { title: \'委托开始日期\', field: \'DateLowe" +
"r\', width: 100, align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFor" +
"matterByT },\r\n\t\t                { title: \'委托结束日期\', field: \'DateUpper\', width: 10" +
"0, align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },\r" +
"\n\t\t                { title: \'委托备注\', field: \'AgenNote\', width: 100, align: \'cente" +
"r\', sortable: true },\r\n                        {\r\n                            ti" +
"tle: \'委托类别\', field: \'AgenType\', width: 100, align: \'center\', formatter: function" +
" (val, row) {\r\n                                return val == 0 ? \"<span style=\'c" +
"olor:gray\'>全部权限</span>\" : (val == 1 ? \"<span style=\'color:green\'>菜单权限</span>\" : " +
"\"<span style=\'color:green\'>流程权限</span>\");\r\n                            }\r\n      " +
"                  },\r\n                        {\r\n                            tit" +
"le: \'委托状态\', field: \'AgenStatus\', width: 100, align: \'center\', formatter: functio" +
"n (val, row) {\r\n                                var dateValue0 = new Date(JQ.too" +
"ls.DateTimeFormatterByT(row.DateUpper));                             \r\n         " +
"                       var dateValue1 = new Date(\"");

            
            #line 42 "..\..\Views\BaseEmpAgen\list.cshtml"
                                                      Write(ViewBag.NowDate);

            
            #line default
            #line hidden
WriteLiteral(@""");
                                var s = dateValue0 < dateValue1;
                                var result = val;
                                if (s) result = 1;
                                return result == 0 ? ""<span style='color:green'>正常</span>"" : ""<span style='color:gray'>失效</span>"";
                             }
                         },
                ]]
            });
            $(""#JQSearch"").textbox({
                buttonText: '筛选',
                buttonIcon: 'fa fa-search',
                height: 25,
                prompt: '姓名',
                onClickButton: function () {
                    JQ.datagrid.searchGrid(
                        {
                            dgID: 'BaseEmpAgenGrid',
                            loadingType: 'datagrid',
                            queryParams: null
                        });
                }
            });
        });

        function save() {
            var data = $('#BaseEmpAgenGrid').datagrid('getSelected');

            if (!JQ.tools.isNotEmpty(data)) {
                var url = '");

            
            #line 71 "..\..\Views\BaseEmpAgen\list.cshtml"
                      Write(Url.Action("del", "BaseEmpAgen", new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"';
                JQ.tools.ajax({
                    doBackResult: true,
                    url: url,
                    data: 'BaseEmpAgenID=' + data.BaseEmpAgenID,
                    succesCollBack: function () {
                        $('#BaseEmpAgenGrid').datagrid('reload');
                    }
                });
            }
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BaseEmpAgenGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BaseEmpAgenPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"save()\"");

WriteLiteral(">权限回收</a>\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'FromEmpName,ToEmpName\', \'Contract\': \'like\' }\"");

WriteLiteral("/>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
