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
    
    #line 4 "..\..\Views\BussContractSub\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractSub/list.cshtml")]
    public partial class _Views_BussContractSub_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussContractSub_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussContractSub\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\BussContractSub\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n\r\n");

            
            #line 10 "..\..\Views\BussContractSub\list.cshtml"
            
            
            #line default
            #line hidden
            
            #line 10 "..\..\Views\BussContractSub\list.cshtml"
              
                if (Request.QueryString["from"] == "projectcenter")
                {

            
            #line default
            #line hidden
WriteLiteral("                    ");

WriteLiteral("var buttons=[];");

WriteLiteral("\r\n");

            
            #line 14 "..\..\Views\BussContractSub\list.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    ");

WriteLiteral("var buttons=[\'add\', \'edit\', \'del\', \'export\', \'moreSearch\'];");

WriteLiteral("\r\n");

            
            #line 18 "..\..\Views\BussContractSub\list.cshtml"
                }
            
            
            #line default
            #line hidden
WriteLiteral("\r\n            JQ.datagrid.datagrid({\r\n                JQButtonTypes: buttons,//需要" +
"显示的按钮\r\n                JQAddUrl: \'");

            
            #line 22 "..\..\Views\BussContractSub\list.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=FGS\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 23 "..\..\Views\BussContractSub\list.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=FGS\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 24 "..\..\Views\BussContractSub\list.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussContractSubGrid',//数据表格ID
                JQDialogTitle: '付款合同',//弹出窗体标题
                JQWidth: '1024',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                JQExcludeCol: [1, 14, 15, 16],//导出execl排除的列
                url: '");

            
            #line 32 "..\..\Views\BussContractSub\list.cshtml"
                 Write(Url.Action("json","BussContractSub",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=FGS&Footer=true\',//请求的地址\r\n                queryParams: { projectID:");

            
            #line 33 "..\..\Views\BussContractSub\list.cshtml"
                                     Write(Request.Params["projectID"]==null?"0":Request.Params["projectID"]);

            
            #line default
            #line hidden
WriteLiteral(",ConSubCustId:");

            
            #line 33 "..\..\Views\BussContractSub\list.cshtml"
                                                                                                                       Write(Request.Params["ConSubCustId"] ==null?"0":Request.Params["ConSubCustId"]);

            
            #line default
            #line hidden
WriteLiteral(" },\r\n                checkOnSelect: true,//是否包含check\r\n                toolbar: \'#" +
"BussContractSubPanel\',//工具栏的容器ID\r\n                showFooter:true,\r\n            " +
"    columns: [[\r\n                    { field: \'ck\', align: \'center\', checkbox: t" +
"rue, JQIsExclude: true },\r\n\t\t\t\t  \t{ title: \'外委合同编号\', field: \'ConSubNumber\', widt" +
"h: \'140px\', halign: \'center\', align: \'left\', sortable: true },\r\n\t\t            { " +
"title: \'外委合同名称\', field: \'ConSubName\', width: \'20%\', align: \'left\', halign: \'cent" +
"er\', sortable: true },\r\n                    { title: \'外委合同类型\', field: \'ConSubTyp" +
"eName\', width: \'80px\', align: \'center\' },\r\n                    { title: \'外委合同状态\'" +
", field: \'ConSubStatusName\', width: \'80px\', align: \'center\'},\r\n                 " +
"   { title: \'外委合同单位\', field: \'CustName\', width: \'15%\', align: \'left\', halign: \'c" +
"enter\', sortable: true },\r\n                    { title: \'签订日期\', field: \'ConSubDa" +
"te\', width: \'80px\', align: \'center\', sortable: true, formatter: JQ.tools.DateTim" +
"eFormatterByT },\r\n                    { title: \'合同金额(元)\', field: \'ConSubFee\', wi" +
"dth: \'100px\', align: \'right\', halign: \'center\', sortable: true },\r\n             " +
"       { title: \'已付款(元)\', field: \'AlreadySumFeeMoney\', width: \'100px\', align: \'r" +
"ight\', halign: \'center\'},\r\n                    { title: \'未付款(元)\', field: \'UnPay\'" +
", width: \'100px\', align: \'right\', halign: \'center\' },\r\n                    { tit" +
"le: \'已收票(元)\', field: \'AlreadySumInvoiceMoney\', width: \'100px\', align: \'right\', h" +
"align: \'center\' },\r\n                      {\r\n                          field: \'J" +
"QFiles\', title: \'附件\', align: \'center\', width: \"40px\", JQIsExclude: true, JQExclu" +
"deFlag: \"grid_files\", JQRefTable: \'BussContractSub\', formatter: function (value," +
" row) {\r\n                              if(row.Footer){\r\n                        " +
"          return \"\";\r\n                              }\r\n                         " +
"     else{\r\n                                  return \"<span id=\\\"grid_files_\" + " +
"row.Id + \"\\\"></span>\"\r\n                              }\r\n                        " +
"  }\r\n                      },\r\n\r\n                   {\r\n                       fi" +
"eld: \"FlowProgress\", title: \"流程进度\", align: \"left\", halign: \"center\", width: \"100" +
"px\", formatter: function (value, row, index) {\r\n                           if(ro" +
"w.Footer){\r\n                               return \"\";\r\n                         " +
"  } else {\r\n                               return JQ.Flow.showProgress(\"FlowID\"," +
" \"FlowName\", \"FlowStatusID\", \"FlowStatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId" +
"\", \"");

            
            #line 65 "..\..\Views\BussContractSub\list.cshtml"
                                                                                                                                                    Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral("\")(value,row);\r\n                           }\r\n                       }\r\n         " +
"          },\r\n                    {\r\n                        field: \'opt\', title" +
": \'操作\', width: \"40\", align: \'center\', JQIsExclude: true,\r\n                      " +
"  formatter: function (value, row, index) {\r\n                            if(row." +
"Footer){\r\n                                return \"\";\r\n                          " +
"  }\r\n                            var title = \"查看\";\r\n                            " +
"if (row._EditStatus == 1) {\r\n                                title = \"修改\";\r\n    " +
"                        }\r\n                            else if (row._EditStatus " +
"== 2) {\r\n                                title = \"处理\";\r\n                        " +
"    }\r\n                            return \"<a href=\\\"#\\\" class=\\\"easyui-linkbutt" +
"on\\\"  style=\\\"line-height:22px;\\\" onclick=\\\"ShowBussContractSubInfoDialogue(\'\" +" +
" row.Id + \"\')\\\">\"+title+\"</>\";\r\n                        }\r\n                    }" +
"\r\n                ]],\r\n                onBeforeCheck: function (rowIndex, rowDat" +
"a) {\r\n                    return rowData._AllowCheck;\r\n                },\r\n     " +
"           onClickRow: function (rowIndex, rowData) {\r\n                    if (!" +
"rowData._AllowCheck) {\r\n                        $(this).datagrid(\'unselectRow\', " +
"rowIndex);\r\n                    }\r\n                },\r\n                onLoadSuc" +
"cess: function (data) {\r\n                    $(\"div.datagrid-header-check input[" +
"type=\'checkbox\']\").attr(\"style\", \"display:none\");\r\n                    var rowVi" +
"ews = $(\"#BussContractSubGrid\").parent().find(\".datagrid-view2 .datagrid-body tr" +
".datagrid-row\");\r\n                    for (var i = 0; i < data.rows.length; i++)" +
" {\r\n                        if (!data.rows[i]._AllowCheck) {\r\n                  " +
"          rowViews.filter(\"[datagrid-row-index=\'\" + i + \"\']\").find(\"td[field=\'ck" +
"\'] :checkbox\").prop(\"disabled\", \"disabled\");\r\n                        }\r\n       " +
"             }\r\n                }\r\n            });\r\n            $(\"#JQSearch\").t" +
"extbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-" +
"search\',\r\n                height: 25,\r\n                prompt: \'外委合同编号、外委合同名称、外委" +
"合同单位\',\r\n                queryOptions: { \'Key\': \'ConSubNumber,ConSubName\', \'Contr" +
"act\': \'like\' },\r\n                onClickButton: function () {\r\n                 " +
"   JQ.datagrid.searchGrid(\r\n                        {\r\n                         " +
"   dgID: \'BussContractSubGrid\',\r\n                            loadingType: \'datag" +
"rid\',\r\n                            queryParams:  { projectID:");

            
            #line 115 "..\..\Views\BussContractSub\list.cshtml"
                                                  Write(Request.Params["projectID"]==null?"0":Request.Params["projectID"]);

            
            #line default
            #line hidden
WriteLiteral(",ConSubCustId:");

            
            #line 115 "..\..\Views\BussContractSub\list.cshtml"
                                                                                                                                    Write(Request.Params["ConSubCustId"] ==null?"0":Request.Params["ConSubCustId"]);

            
            #line default
            #line hidden
WriteLiteral(@" }
                        });
                }
            });
            function DialogFK(id) {
                var divid = getMathNo();
                if (window.top.$(""#"" + divid).size() <= 0) {
                    window.top.$(""body"").append(""<div class='rwpdialogdiv' id='"" + divid + ""'></div>"");
                }
                var paraJosn = {
                    title: ""<b>合同付费</b>"",
                    content: '<iframe id=""linkMan"" frameborder=""0"" scrolling=""no"" style=""width:100%;height:99%"" src=""");

            
            #line 126 "..\..\Views\BussContractSub\list.cshtml"
                                                                                                                Write(Url.Action("list", "BussCustomerLinkMan"));

            
            #line default
            #line hidden
WriteLiteral("?CustID=\' + id + \'\"></iframe>\',\r\n                    width: \'1100\',\r\n            " +
"        height: \'100%\',\r\n                    resizable: false,\r\n                " +
"    maximizable: false,\r\n                    collapsible: false,\r\n              " +
"      modal: true,\r\n                    onClose: function () {\r\n                " +
"        $(\'#BussCustomerGrid\').datagrid(\'reload\');\r\n                        wind" +
"ow.top.$(\"#\" + divid).parent().remove();\r\n                    }\r\n               " +
" };\r\n                var dialog = window.top.$(\"#\" + divid).dialog(paraJosn);\r\n " +
"           }\r\n            function getMathNo() {\r\n                var d = new Da" +
"te();\r\n                var sp = \"\";\r\n                var month = d.getMonth() + " +
"1;\r\n                var strDate = d.getDate();\r\n                if (month >= 1 &" +
"& month <= 9) {\r\n                    month = \"0\" + month;\r\n                }\r\n  " +
"              if (strDate >= 0 && strDate <= 9) {\r\n                    strDate =" +
" \"0\" + strDate;\r\n                }\r\n                var math = Math.floor(Math.r" +
"andom() * (1000000 + 1));\r\n                var cd = d.getHours() + sp + d.getMin" +
"utes() + sp + d.getSeconds() + sp + d.getMilliseconds();\r\n                return" +
" math + sp + cd;\r\n            };\r\n        });\r\n        window.refreshGrid = func" +
"tion () {\r\n            $(\"#JQSearch\").textbox(\"button\").click();\r\n        }\r\n\r\n " +
"       function ShowBussContractSubInfoDialogue(Id) {\r\n            JQ.dialog.dia" +
"log({\r\n                title: \"付款合同明细\",\r\n                url: \'");

            
            #line 163 "..\..\Views\BussContractSub\list.cshtml"
                 Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=FGS&Id=\' + Id,\r\n                width: \'1024\',\r\n                heig" +
"ht: \'100%\'\r\n            });\r\n        }\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussContractSubGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussContractSubPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 175 "..\..\Views\BussContractSub\list.cshtml"
       Write(BaseData.getBases(new Params()
       {
           isMult = true,
           controlName = "ConSubType1",
           isRequired = false,
           engName = "ConSubType",
           queryOptions = "{'Key':'ConSubType','Contract':'in','filedType':'Int'}",
           width = "150px",

       }, new Params()
       {
           isMult = true,
           controlName = "ConSubStatus1",
           isRequired = false,
           engName = "ConSubStatus",
           queryOptions = "{'Key':'ConSubStatus','Contract':'in','filedType':'Int'}",
           width = "150px",
       },
        new Params()
        {
            isMult = true,
            controlName = "ConSubCategory1",
            isRequired = false,
            engName = "ConSubCategory",
            queryOptions = "{'Key':'ConSubCategory','Contract':'in','filedType':'Int'}",
            width = "150px",


        }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n        <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'txtLike\', Contract:\'like\'}\"");

WriteLiteral(" style=\"width:350px;\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            签订日期：<input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConSubDateS\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConSubDateE\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n            <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n            归档盒号：<input");

WriteLiteral(" id=\"ArchNumber\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'归档盒号\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ArchNumber\', Contract:\'like\',filedType:\'string\' }\"");

WriteLiteral(">\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
