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
    
    #line 4 "..\..\Views\BussCustomer\substisticslist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussCustomer/substisticslist.cshtml")]
    public partial class _Views_BussCustomer_substisticslist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussCustomer_substisticslist_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussCustomer\substisticslist.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        $(function () {
            var startTime = $(""#startTime"").textbox('getValue');
            var endTime = $(""#endTime"").textbox('getValue');
            var ConSubStatus = $(""#ConSubStatus"").combotree('getValue');
            JQ.datagrid.datagrid({
                JQButtonTypes: ['export', 'moreSearch'],//需要显示的按钮
                // JQAddUrl: '");

            
            #line 13 "..\..\Views\BussCustomer\substisticslist.cshtml"
                         Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',//添加的路径\r\n                //JQEditUrl: \'");

            
            #line 14 "..\..\Views\BussCustomer\substisticslist.cshtml"
                         Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',//编辑的路径\r\n                //JQDelUrl: \'");

            
            #line 15 "..\..\Views\BussCustomer\substisticslist.cshtml"
                        Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussCustomerStatiGrid',//数据表格ID
                JQWidth: '900',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                //JQCustomSearch: [  //自定义搜索的字段
                //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
                //],
                JQExcludeCol: [1],//导出execl排除的列
                //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
                //JQFields: ["""", """"],//追加的字段名
                url: '");

            
            #line 28 "..\..\Views\BussCustomer\substisticslist.cshtml"
                 Write(Url.Action("SubStatisticsListjson", "BussCustomer",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                fit: " +
"true,\r\n                toolbar: \'#BussCustomerStatiPanel\',//工具栏的容器ID\r\n          " +
"      columns: [[\r\n                  { field: \'ck\', align: \'center\', checkbox: t" +
"rue, JQIsExclude: true },\r\n                 // { title: \'编号\', field: \'Id\', width" +
": 120, align: \'center\', sortable: true },\r\n\r\n        { title: \'顾客名称\', field: \'Cu" +
"stName\', width: \'60%\', align: \'center\', sortable: true },\r\n\r\n                 {\r" +
"\n                     field: \'Num\', title: \'合同个数\', align: \'center\', width: \'18%\'" +
",\r\n                     formatter: function (value, row) {\r\n                    " +
"     return \'<a class=\"easyui-linkbutton\" onclick=\"DialogN(\' + row.Id + \')\" href" +
"=\"#\" style=\"line-height:22px;\">\' + row.Num + \'</a>\';\r\n                     }\r\n\r\n" +
"                 },\r\n                 {\r\n                     field: \'Fee\', titl" +
"e: \'合同价格汇总（元）\', align: \'center\', width: \'18%\',\r\n                     formatter: " +
"function (value, row) {\r\n                         return \'<a class=\"easyui-linkb" +
"utton\" onclick=DialogN(\' + row.Id + \')  href=\"#\" style=\"line-height:22px;\">\' + r" +
"ow.Fee + \'</a>\';\r\n                     }\r\n                 }\r\n\r\n                " +
"]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n                butto" +
"nText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                heigh" +
"t: 25,\r\n                prompt: \'顾客名称\',\r\n                queryOptions: { Key: \'C" +
"ustName\', Contract: \'like\' },\r\n                onClickButton: function () {\r\n   " +
"                 JQ.datagrid.searchGrid(\r\n                        {\r\n           " +
"                 dgID: \'BussCustomerStatiGrid\',\r\n                            loa" +
"dingType: \'datagrid\',\r\n                            queryParams: {\r\n             " +
"                   startTime: $(\"#startTime\").textbox(\'getValue\'),\r\n            " +
"                    endTime: $(\"#endTime\").textbox(\'getValue\'),\r\n               " +
"                 ConSubStatus: $(\"#ConSubStatus\").combotree(\'getValue\')\r\n       " +
"                     }\r\n                        });\r\n                }\r\n        " +
"    });\r\n        });\r\n\r\n        function getMathNo() {\r\n            var d = new " +
"Date();\r\n            var sp = \"\";\r\n            var month = d.getMonth() + 1;\r\n  " +
"          var strDate = d.getDate();\r\n            if (month >= 1 && month <= 9) " +
"{\r\n                month = \"0\" + month;\r\n            }\r\n            if (strDate " +
">= 0 && strDate <= 9) {\r\n                strDate = \"0\" + strDate;\r\n            }" +
"\r\n            var math = Math.floor(Math.random() * (1000000 + 1));\r\n           " +
" var cd = d.getHours() + sp + d.getMinutes() + sp + d.getSeconds() + sp + d.getM" +
"illiseconds();\r\n            return math + sp + cd;\r\n        };\r\n        function" +
" DialogN(id) {\r\n            window.top.addTab(\'付款合同信息\', \'");

            
            #line 91 "..\..\Views\BussCustomer\substisticslist.cshtml"
                                    Write(Url.Action("list", "BussContractSub"));

            
            #line default
            #line hidden
WriteLiteral("?ConSubCustId=\' + id)\r\n        }\r\n    </script>\r\n");

WriteLiteral("    ");

            
            #line 94 "..\..\Views\BussCustomer\substisticslist.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussCustomerStatiGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussCustomerStatiPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CustName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            外委合同签订时间:&nbsp;&nbsp;\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始时间\'\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束时间\'\"");

WriteLiteral(">\r\n            &nbsp;&nbsp;&nbsp;&nbsp;\r\n            外委合同状态：\r\n");

WriteLiteral("            ");

            
            #line 114 "..\..\Views\BussCustomer\substisticslist.cshtml"
       Write(BaseData.getBases(new Params() { isMult = false, engName = "ConSubStatus" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            &nbsp;&nbsp;&nbsp;&nbsp;\r\n            外委单位性质\r\n");

WriteLiteral("            ");

            
            #line 117 "..\..\Views\BussCustomer\substisticslist.cshtml"
       Write(BaseData.getBases( new Params() { isMult = false, engName = "SubCustType" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591