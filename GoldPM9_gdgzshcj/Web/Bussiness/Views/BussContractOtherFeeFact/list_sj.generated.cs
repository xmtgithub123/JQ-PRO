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
    
    #line 4 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractOtherFeeFact/list_sj.cshtml")]
    public partial class _Views_BussContractOtherFeeFact_list_sj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussContractOtherFeeFact_list_sj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'],//需要显示的按钮\r\n        " +
"        JQAddUrl: \'");

            
            #line 10 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 11 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 12 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussContractOtherFeeFactGrid',//数据表格ID
                JQDialogTitle: '其它合同收款',//弹出窗体标题
                JQWidth: '1024',//弹出窗体宽
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

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                 Write(Url.Action("json","BussContractOtherFeeFact",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=SJ',//请求的地址
                checkOnSelect: true,//是否包含check
                toolbar: '#BussContractOtherFeeFactPanel',//工具栏的容器ID
                columns: [[
                  { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
        { title: '合同编号', field: 'ConNumber', width: ""15%"", align: 'left', sortable: false },
        { title: '合同名称', field: 'ConrName', width: ""15%"", align: 'left', sortable: false },
        { title: '客户单位', field: 'CustName', width: ""14%"", align: 'left', sortable: false },
        //{ title: '合同金额(元)', field: 'ConFee', width: ""9%"", align: 'right', sortable: true },
         {
             title: '合同金额（元）', field: 'ConFee', width: ""9%"", align: 'right', sortable: false, formatter: function (value, row, index) {
                 if (row.ConOtherFulfilType == '");

            
            #line 37 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                                            Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral(@"') {
                     var value = ""<div><span>"" + row.ConFee + ""</span></div>"";
                     row.ConFee = value;//更新数据行数据
                     return value;
                 } else {
                     var value = ""<div><span>"" + row.ConrBalanceFee + ""</span></div>"";
                     row.ConFee = value;//更新数据行数据
                     return value;
                 }
             }
         },
            { title: '已收款金额(元)', field: 'SumFeeMoney', width: ""9%"", align: 'right', sortable: false },
            { title: '本次收款金额(元)', field: 'FeeMoney', width: ""9%"", align: 'right', sortable: false },
            //{ title: '未收款金额(元)', field: 'NoFeeMoney', width: ""9%"", align: 'right', sortable: true },
            {
                title: '未收款金额（元）', field: 'NoFeeMoney', width: ""9%"", align: 'right', sortable: false, formatter: function (value, row, index) {
                    if (row.ConOtherFulfilType == '");

            
            #line 53 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                                               Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral(@"') {
                        var value =  row.NoFeeMoney  ;
                        if (value <= 0) {
                            value = 0;
                        }
                        row.NoFeeMoney = value;//更新数据行数据
                        return value;
                    } else {
                        var value =  row.NoConrBalanceFeeMoney  ;
                        if (value <= 0) {
                            value = 0;
                        }
                        row.NoFeeMoney = value;//更新数据行数据
                        return value;
                    }
                }
            },
            { title: '本次收款日期', field: 'FeerDate', width: ""8%"", align: 'center', formatter: JQ.tools.DateTimeFormatterByT },
                    {
                        title: '累计收款比例', field: 'Scale', width: ""8%"", align: 'center', sortable: false, formatter: function (value, row, index) {
                            if (row.ConOtherFulfilType == '");

            
            #line 73 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
                                                       Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n                                var value = \"<div><span>\" + row.Scale + \"</" +
"span></div>\";\r\n                                row.Scale = value;//更新数据行数据\r\n    " +
"                            return value;\r\n                            } else {\r" +
"\n                                var value = \"<div><span>\" + row.ScaleConrBalanc" +
"eFee + \"</span></div>\";\r\n                                row.Scale = value;//更新数" +
"据行数据\r\n                                return value;\r\n                           " +
" }\r\n                        }\r\n                    },\r\n                ]]\r\n     " +
"       });\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛" +
"选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n " +
"               prompt: \'合同编号、合同名称、客户单位\',\r\n                //queryOptions: { \'Key" +
"\': \'ConNumber,ConrName,CustName\', \'Contract\': \'like\' },\r\n                queryOp" +
"tions: { \'Key\': \'FK_BussContractOtherFeeFact_RefID.ConrName,FK_BussContractOther" +
"FeeFact_RefID.CustName,FK_BussContractOtherFeeFact_RefID.ConNumber\', \'Contract\':" +
" \'like\' },\r\n                onClickButton: function () {\r\n                    JQ" +
".datagrid.searchGrid(\r\n                        {\r\n                            dg" +
"ID: \'BussContractOtherFeeFactGrid\',\r\n                            loadingType: \'d" +
"atagrid\',\r\n                            KeyJQSearch: $(\"#JQSearch\").val(),\r\n     " +
"                       //ProjectPhase: $(\'#\').textbox(\'getValue\'),\r\n            " +
"            });\r\n                }\r\n            });\r\n        });\r\n    </script>\r" +
"\n");

WriteLiteral("    ");

            
            #line 105 "..\..\Views\BussContractOtherFeeFact\list_sj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussContractOtherFeeFactGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussContractOtherFeeFactPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        ");

WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'FK_BussContractOtherFeeFact_RefID.ConrName,FK_BussContrac" +
"tOtherFeeFact_RefID.ConNumber,FK_BussContractOtherFeeFact_RefID.CustName\', Contr" +
"act:\'like\'}\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'收款日期开始\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'FeerDate\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'收款日期结束\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'FeerDate\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
