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
    
    #line 4 "..\..\Views\DesDelivery\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesDelivery/list.cshtml")]
    public partial class _Views_DesDelivery_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesDelivery_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\DesDelivery\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t var requestUrl = \'");

            
            #line 7 "..\..\Views\DesDelivery\list.cshtml"
               Write(Url.Action("json", "DesDelivery",new { @area="Design"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\DesDelivery\list.cshtml"
                 Write(Url.Action("add","DesDelivery",new {@area="Design" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\DesDelivery\list.cshtml"
                  Write(Url.Action("edit", "DesDelivery",new {@area="Design" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\DesDelivery\list.cshtml"
                 Write(Url.Action("del", "DesDelivery", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\',\'del\', \'export\'],//需要显示的按钮\r\n                JQAddU" +
"rl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n               " +
" JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'DeliveryID\',//主键的字段\r\n " +
"               JQID: \'DesDeliveryGrid\',//数据表格ID\r\n                JQDialogTitle: " +
"\'信息\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight:" +
" \'100%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,da" +
"tagrid]\r\n                JQFields: [\"DeliveryID\"],//追加的字段名\r\n                url:" +
" requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n          " +
"      toolbar: \'#DesDeliveryPanel\',//工具栏的容器ID\r\n                columns: [[\r\n    " +
"              { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true " +
"},\r\n\t\t\t\t  \t\t{ title: \'---交付单---\', field: \'DeliveryID\', width: 100, align: \'cente" +
"r\', sortable: true  },\r\n\t\t{ title: \'工程\', field: \'ProjID\', width: 100, align: \'ce" +
"nter\', sortable: true  },\r\n\t\t{ title: \'工程编号\', field: \'ProjNumber\', width: 100, a" +
"lign: \'center\', sortable: true  },\r\n\t\t{ title: \'工程名称\', field: \'ProjName\', width:" +
" 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'交付日期\', field: \'DeliveryDa" +
"te\', width: 100, align: \'center\', sortable: true , formatter: JQ.tools.DateTimeF" +
"ormatterByT },\r\n\t\t{ title: \'交付方式\', field: \'DeliveryTypeID\', width: 100, align: \'" +
"center\', sortable: true  },\r\n\t\t{ title: \'交付专业\', field: \'DeliveryPhase\', width: 1" +
"00, align: \'center\', sortable: true  },\r\n\t\t{ title: \'交付阶段\', field: \'DeliverySpec" +
"\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'交付卷册\', field: \'D" +
"eliveryVol\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'设计人\', " +
"field: \'DeliveryDesignEmpName\', width: 100, align: \'center\', sortable: true  },\r" +
"\n\t\t{ title: \'晒图日期\', field: \'DeliveryBlueDate\', width: 100, align: \'center\', sort" +
"able: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'接收单位\', fie" +
"ld: \'DeliveryCompany\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ titl" +
"e: \'接收人\', field: \'DeliveryRecEmpName\', width: 100, align: \'center\', sortable: tr" +
"ue  },\r\n\t\t{ title: \'份数\', field: \'DeliveryDetailNum\', width: 100, align: \'center\'" +
", sortable: true  },\r\n\t\t{ title: \'交付备注\', field: \'DeliveryNote\', width: 100, alig" +
"n: \'center\', sortable: true  },\r\n                 { field: \'JQFiles\', title: \'附件" +
"\', align: \'center\', width: 80, JQIsExclude: true, JQRefTable: \'DesDelivery\' }\r\n " +
"               ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n      " +
"          buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n      " +
"          height: 25,\r\n                prompt: \'筛选字段\',\r\n                queryOpt" +
"ions: { \'Key\': \'ProjNumber\', \'Contract\': \'like\' },\r\n                onClickButto" +
"n: function () {\r\n                    JQ.datagrid.searchGrid(\r\n                 " +
"       {\r\n                            dgID: \'DesDeliveryGrid\',\r\n                " +
"            loadingType: \'datagrid\',\r\n                            queryParams: n" +
"ull\r\n                        });\r\n                }\r\n            });\r\n        })" +
";\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"DesDeliveryGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"DesDeliveryPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            \r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
