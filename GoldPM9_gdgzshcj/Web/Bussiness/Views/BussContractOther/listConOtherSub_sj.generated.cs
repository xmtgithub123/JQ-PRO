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
    
    #line 6 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractOther/listConOtherSub_sj.cshtml")]
    public partial class _Views_BussContractOther_listConOtherSub_sj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseEmployee>
    {
        public _Views_BussContractOther_listConOtherSub_sj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
  
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

            
            #line 12 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                      Write(Url.Action("addSub"));

            
            #line default
            #line hidden
WriteLiteral("\' + \'?CompanyType=SJ&ConTypeID=1\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 13 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                       Write(Url.Action("editSub"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 14 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussContractOtherSubGrid',//数据表格ID
                JQDialogTitle: '其它付款合同信息',//弹出窗体标题
                JQWidth: '1024',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                //JQCustomSearch: [  //自定义搜索的字段
                //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
                //],
                 JQExcludeCol: [1, 9, 11],//导出execl排除的列
                //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
                //JQFields: ["""", """"],//追加的字段名
                 url: '");

            
            #line 28 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                  Write(Url.Action("json", "BussContractOther", new { @area = "Bussiness", @ConTypeID = 1 }));

            
            #line default
            #line hidden
WriteLiteral("&CompanyType=SJ&Footer=true\',//请求的地址\r\n                checkOnSelect: true,//是否包含c" +
"heck\r\n                toolbar: \'#BussContractOtherPanel\',//工具栏的容器ID\r\n           " +
"     showFooter: true,\r\n                columns: [[\r\n        { field: \'ck\', alig" +
"n: \'center\', checkbox: true, JQIsExclude: true },\r\n        { title: \'合同编号\', fiel" +
"d: \'ConNumber\', width: \"10%\", align: \'left\', sortable: true },\r\n        { title:" +
" \'合同名称\', field: \'ConrName\', width: \"14%\", align: \'left\', sortable: true },\r\n    " +
"    { title: \'客户单位\', field: \'CustName\', width: \"14%\", align: \'left\', sortable: t" +
"rue },\r\n        { title: \'合同金额（元）\', field: \'ConFee\', width: \"8%\", align: \'right\'" +
", sortable: true },\r\n        { title: \'已付款（元）\', field: \'SumFeeMoney\', width: \"8%" +
"\", align: \'right\', sortable: false },\r\n        { title: \'未付款（元）\', field: \'NoFeeM" +
"oney\', width: \"8%\", align: \'right\', sortable: false },\r\n        { title: \'已收票（元）" +
"\', field: \'SumInvoiceMoney\', width: \"8%\", align: \'right\', sortable: false },\r\n  " +
"      { field: \'JQFiles\', title: \'附件\', align: \'center\', width: \"6%\", JQIsExclude" +
": true, JQRefTable: \'BussContractOther\' },\r\n          {\r\n              title: \'是" +
"否结清\', field: \'ConIsFeeFinished\', width: \"8%\", align: \'center\', formatter: functi" +
"on (val, row) {\r\n                  if (row.Footer) {\r\n                      retu" +
"rn \"\";\r\n                  }\r\n                  return val == \"已结清\" ? \"<span styl" +
"e=\'color:green\'>已结清</span>\" : \"<span style=\'color:red\'>未结清</span>\";\r\n           " +
"   }\r\n          },\r\n        ////{ title: \'审批进度\', field: \'\', width: 100, align: \'" +
"center\', sortable: true },\r\n                    {\r\n                        field" +
": \'opt\', title: \'操作\', width: \"12%\", align: \'center\',\r\n                        fo" +
"rmatter: function (value, row, index) {\r\n                            if (row.Foo" +
"ter) {\r\n                                return \"\";\r\n                            " +
"}\r\n                            return \"<a href=\\\"javascript:void(0)\\\"  JQPermiss" +
"ionName=\\\"add\\\" id=\\\"mb\" + index + \"\\\" dataid=\\\"\" + row.Id + \"\\\" flag=\\\"menubutt" +
"on\\\"   >操作</a>\";\r\n                        }\r\n                    },\r\n           " +
"     ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n                " +
"buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                " +
"height: 25,\r\n                prompt: \'合同名称，合同编号，客户单位\',\r\n                //queryO" +
"ptions: { \'Key\': \'ConNumber,ConrName,CustName\', \'Contract\': \'like\' },\r\n         " +
"       onClickButton: function () {\r\n                    JQ.datagrid.searchGrid(" +
"\r\n                        {\r\n                            dgID: \'BussContractOthe" +
"rSubGrid\',\r\n                            loadingType: \'datagrid\',\r\n              " +
"              queryParams: {\r\n                                KeyJQSearch: $(\"#J" +
"QSearch\").val(),\r\n                            }\r\n                        });\r\n  " +
"              }\r\n            });\r\n        });\r\n\r\n        $.extend($.fn.datagrid." +
"defaults.view, {\r\n            onAfterRender: function () {\r\n                $(\"a" +
"[flag=\'menubutton\']\").mouseover(function () {\r\n                    document.getE" +
"lementById(\"mm3\").setAttribute(\"rowid\", this.getAttribute(\"dataid\"));\r\n         " +
"       }).menubutton({\r\n                    iconCls: \'fa fa-edit\',\r\n            " +
"        menu: \'#mm3\'\r\n                });\r\n            }\r\n        });\r\n\r\n       " +
" function menuHandler(item) {\r\n            var key = $(\"#mm3\").attr(\"rowid\");\r\n " +
"           switch (item.text) {\r\n                case \"付款\":\r\n                   " +
" JQ.dialog.dialog({\r\n                        title: \"查看其他付款合同信息\",\r\n             " +
"           url: \'");

            
            #line 98 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                         Write(Url.Action("AddConOtherFee"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?CompanyType=SJ&Id=' + key,
                        width: '1024',
                        height: '100%',
                        JQID: 'BussContractOtherSubGrid',
                        JQLoadingType: 'datagrid',
                        iconCls: 'fa fa-plus'
                    });
                    break;
                case ""查看"":
                    JQ.dialog.dialog({
                        title: ""查看信息"",
                        url: '");

            
            #line 109 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
                         Write(Url.Action("editSub"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?CompanyType=SJ&Id=' + key,
                        width: '1024',
                        height: '100%',
                        JQID: 'BussContractOtherSubGrid',
                        JQLoadingType: 'datagrid',
                        iconCls: 'fa fa-plus'
                    });
                    break;
                default:
                    break;
            }
        }
    </script>
");

WriteLiteral("    ");

            
            #line 122 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussContractOtherSubGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussContractOtherPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        &nbsp;\r\n        ");

WriteLiteral("\r\n\r\n");

WriteLiteral("        ");

            
            #line 139 "..\..\Views\BussContractOther\listConOtherSub_sj.cshtml"
   Write(BaseData.getBases(
                          new Params() { isMult = false, engName = "ConStatus", queryOptions = "{ Key:'ConOtherStatus', Contract:'in',filedType:'Int'}" },
                                      new Params() { isMult = false, engName = "ContractType", queryOptions = "{ Key:'ConOtherType', Contract:'in',filedType:'Int'}" }
                                ));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'登记日期开始\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'CreationTime\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'登记日期结束\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'CreationTime\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"mm3\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"div1\"");

WriteLiteral(">付款</div>\r\n        <div");

WriteLiteral(" id=\"div2\"");

WriteLiteral(">查看</div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
