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
    
    #line 4 "..\..\Views\BussContract\list_gc.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContract/list_gc.cshtml")]
    public partial class _Views_BussContract_list_gc_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussContract_list_gc_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussContract\list_gc.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n        var queryParams={\r\n                CustID: \"");

            
            #line 10 "..\..\Views\BussContract\list_gc.cshtml"
                    Write(ViewData["CustID"]);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                CustName : \"\"\r\n            };\r\n        $(function () {\r\n     " +
"       if ((");

            
            #line 14 "..\..\Views\BussContract\list_gc.cshtml"
            Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral(").indexOf(\"add\")==-1) {\r\n                $(\"#AddChildCon\").hide();\r\n            }" +
"\r\n\r\n");

            
            #line 18 "..\..\Views\BussContract\list_gc.cshtml"
            
            
            #line default
            #line hidden
            
            #line 18 "..\..\Views\BussContract\list_gc.cshtml"
              
                if (Request.QueryString["projectID"] != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    ");

WriteLiteral("queryParams.ProjID=\"");

            
            #line 21 "..\..\Views\BussContract\list_gc.cshtml"
                                          Write(JQ.Util.TypeParse.parse<int>(Request.QueryString["projectID"]) );

            
            #line default
            #line hidden
WriteLiteral("\";");

WriteLiteral("\r\n");

            
            #line 22 "..\..\Views\BussContract\list_gc.cshtml"
                }

                if (Request.QueryString["from"] == "projectcenter")
                {

            
            #line default
            #line hidden
WriteLiteral("                     ");

WriteLiteral("var buttons=[];");

WriteLiteral("\r\n");

            
            #line 27 "..\..\Views\BussContract\list_gc.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                     ");

WriteLiteral("var buttons=[\'add\', \'del\', \'export\', \'moreSearch\'];");

WriteLiteral("\r\n");

            
            #line 31 "..\..\Views\BussContract\list_gc.cshtml"
                }
            
            
            #line default
            #line hidden
WriteLiteral("\r\n            JQ.treegrid.treegrid({\r\n                pagination: true,\r\n        " +
"        JQButtonTypes: buttons,//需要显示的按钮\r\n                JQAddUrl: \'");

            
            #line 36 "..\..\Views\BussContract\list_gc.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 37 "..\..\Views\BussContract\list_gc.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 38 "..\..\Views\BussContract\list_gc.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                idField: 'Keyid',
                treeField: 'ConNumber',
                JQID: 'BussContractGrid',//数据表格ID
                JQDialogTitle: '收款合同',//弹出窗体标题
                JQWidth: '1024',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'treegrid',//数据表格类型 [datagrid,treegrid]
                singleSelect: true,
                JQExcludeCol: [1,14,15,16,17,18],//导出execl排除的列
                JQFields: [""Id""],//追加的字段名
                url: '");

            
            #line 50 "..\..\Views\BussContract\list_gc.cshtml"
                 Write(Url.Action("json", "BussContract", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC&Footer=true\',//请求的地址\r\n                queryParams: queryParams,\r\n" +
"                checkOnSelect: true,//是否包含check\r\n                toolbar: \'#Buss" +
"ContractPanel\',//工具栏的容器ID\r\n                showFooter: true,\r\n                no" +
"wrap: false,\r\n                frozenColumns: [[\r\n                    { field: \'c" +
"k\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n                    {" +
" title: \'合同编号\', field: \'ConNumber\', width: \"180px\", halign: \'center\', align: \'le" +
"ft\', sortable: true },\r\n                    {\r\n                        title: \'合" +
"同名称\', field: \'ConName\', width: \"20%\", halign: \'center\', align: \'left\', sortable:" +
" true, formatter: function (item, data) {\r\n                            return \"<" +
"span title=\'\" + item + \"\'>\" + item + \"</span>\";\r\n                        }\r\n    " +
"                },\r\n                ]],\r\n                columns: [[\r\n          " +
"          { title: \'客户名称\', field: \'CustName\', width: \"10%\", halign: \'center\', al" +
"ign: \'left\', sortable: true },\r\n                    {\r\n                        t" +
"itle: \'签订状态\', field: \'ConStatus\', width: \"80px\", halign: \'center\', align: \'cente" +
"r\', sortable: true, formatter: function (item, row) {\r\n                         " +
"   return row.ConStatusShow;\r\n                        }\r\n                    },\r" +
"\n                    { title: \'签订日期\', field: \'ConDate\', width: \"80px\", halign: \'" +
"center\', align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterB" +
"yT },\r\n                    {\r\n                        title: \'合同金额\', field: \'New" +
"ConFee\', width: \"100px\", halign: \'center\', align: \'right\', formatter: function (" +
"value, row, index) {\r\n                            if (row.Footer) {\r\n           " +
"                     return row.NewConFee;\r\n                            }\r\n     " +
"                       if (row.FatherID == 0) {\r\n                               " +
" if (row.ConFulfilType == \'");

            
            #line 79 "..\..\Views\BussContract\list_gc.cshtml"
                                                      Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n                                    if (row.children.length == 0) {\r\n      " +
"                                  row.NewConFee = row.SumConFee;\r\n              " +
"                          return \"<div><span>\" + row.SumConFee + \"</span></div>\"" +
";\r\n                                    } else {\r\n                               " +
"         row.NewConFee = row.SumConFee + \"(\" + row.ConFee + \")\";\r\n              " +
"                          return \"<div title=\\\"开口合同、红色金额为主合同金额\\\"><span>\" + row.S" +
"umConFee + \"</span><span style=\\\"color:red;\\\">（\" + row.ConFee + \"）</span></div>\"" +
";\r\n                                    }\r\n\r\n                                } el" +
"se {\r\n                                    if (row.children.length == 0) {\r\n     " +
"                                   var showPan = \"\";\r\n                          " +
"              if (row.SumConBalanceFee == 0) {\r\n                                " +
"            row.NewConFee = row.SumConFee;\r\n                                    " +
"        showPan = \"<div><span>\" + row.ConFee + \"</span></div>\";\r\n               " +
"                         } else {\r\n                                            r" +
"ow.NewConFee = row.SumConBalanceFee;\r\n                                          " +
"  showPan = \"<div><span>\" + row.SumConBalanceFee + \"</span></div>\";\r\n           " +
"                             }\r\n                                        return s" +
"howPan;\r\n                                    } else {\r\n                         " +
"               var showPan = \"\";\r\n                                        if (ro" +
"w.SumConBalanceFee == 0) {\r\n                                            row.NewC" +
"onFee = row.SumConFee + \"(\" + row.ConFee + \")\";\r\n                               " +
"             showPan = \"<div title=\\\"闭口合同、红色金额为主合同金额\\\"><span>\" + row.SumConFee +" +
" \"</span><span style=\\\"color:red;\\\">（\" + row.ConFee + \"）</span></div>\";\r\n       " +
"                                 } else {\r\n                                     " +
"       row.NewConFee = row.SumConBalanceFee + \"(\" + row.ConBalanceFee + \")\";\r\n  " +
"                                          showPan = \"<div title=\\\"闭口合同、红色金额为主合同金" +
"额\\\"><span>\" + row.SumConBalanceFee + \"</span><span style=\\\"color:red;\\\">（\" + row" +
".ConBalanceFee + \"）</span></div>\";\r\n                                        }\r\n " +
"                                       return showPan;\r\n                        " +
"            }\r\n                                }\r\n                            } " +
"else {\r\n                                if (row.ConFulfilType == \'");

            
            #line 112 "..\..\Views\BussContract\list_gc.cshtml"
                                                      Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n                                    row.NewConFee = row.ConFee;\r\n          " +
"                          return \"\" + row.ConFee + \"\";\r\n                        " +
"        }\r\n                                else {\r\n                             " +
"       if (row.ConBalanceFee == 0) {\r\n                                        ro" +
"w.NewConFee = row.SumConFee;\r\n                                    } else {\r\n    " +
"                                    row.NewConFee = row.ConBalanceFee;\r\n        " +
"                            }\r\n                                    return \"\" + r" +
"ow.NewConFee + \"\";\r\n                                }\r\n                         " +
"   }\r\n                        }\r\n\r\n                    },\r\n                    {" +
"\r\n                        title: \'已收款\', field: \'FeeFact\', width: \"100px\", halign" +
": \'center\', align: \'right\', formatter: function (value, row, index) {\r\n         " +
"                   if (row.Footer) {\r\n                                return row" +
".FeeFact;\r\n                            }\r\n                            if (row.Fa" +
"therID == 0) { return row.FeeFact; }\r\n                            else { return " +
"\"\"; }\r\n                        }\r\n                    },\r\n                    {\r" +
"\n                        title: \'未收款\', field: \'NoFee\', width: \"100px\", halign: \'" +
"center\', align: \'right\', formatter: function (value, row, index) {\r\n            " +
"                if (row.Footer) {\r\n                                return row.Ne" +
"wConFee - row.FeeFact;\r\n                            }\r\n                         " +
"   if (row.FatherID == 0) {\r\n                                if (row.ConFulfilTy" +
"pe == \'");

            
            #line 143 "..\..\Views\BussContract\list_gc.cshtml"
                                                      Write((int)DataModel.ConDealWays.开口);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n                                    return \"\" + (row.SumConFee - row.FeeFac" +
"t).toFixed(2) + \"\";\r\n                                } else {\r\n                 " +
"                   return \"\" + (row.SumConBalanceFee - row.FeeFact).toFixed(2);\r" +
"\n                                }\r\n                            } else {\r\n      " +
"                          return \"\";\r\n                            }\r\n           " +
"             }\r\n                    },\r\n                    {\r\n                 " +
"       title: \'开票款\', field: \'FeeInvoice\', width: \"100px\", halign: \'center\', alig" +
"n: \'right\', formatter: function (value, row, index) {\r\n                         " +
"   if (row.Footer) {\r\n                                return row.FeeInvoice;\r\n  " +
"                          }\r\n                            if (row.FatherID == 0) " +
"{ return row.FeeInvoice; }\r\n                            else { return \"\"; }\r\n   " +
"                     }\r\n                    },\r\n                    {\r\n         " +
"               title: \'分包付款\', field: \'FBFK\', width: \"100px\", halign: \'center\', a" +
"lign: \'right\', formatter: function (value, row, index) {\r\n                      " +
"      if (row.Footer) {\r\n                                return 0;\r\n            " +
"                }\r\n                            row.FBFK = 0;\r\n                  " +
"          return row.FBFK;\r\n                        }\r\n                    },\r\n " +
"                   {\r\n                        title: \'是否结清\', field: \'ConIsFeeFin" +
"ished\', width: \"60px\", halign: \'center\', align: \'center\', formatter: function (v" +
"alue, row, index) {\r\n                            if (row.Footer) {\r\n            " +
"                    return \"\";\r\n                            }\r\n                 " +
"           return value;\r\n                        }\r\n                    },\r\n   " +
"                 {\r\n                        title: \'是否分包\', field: \'ConStreamNumb" +
"er\', width: \"60px\", halign: \'center\', align: \'center\', formatter: function (valu" +
"e, row, index) {\r\n                            if (row.Footer) {\r\n               " +
"                 return \"\";\r\n                            }\r\n                    " +
"        if (row.ConStreamNumber != \"\" && row.ConStreamNumber == 1) {\r\n          " +
"                      row.ConStreamNumber = \"是\";\r\n                            }\r" +
"\n                            else {\r\n                                row.ConStre" +
"amNumber = \"否\";\r\n                            }\r\n                            retu" +
"rn row.ConStreamNumber;\r\n                        }\r\n                    },\r\n    " +
"                {\r\n                        title: \'付款合同\', field: \'tt\', width: \"7" +
"0px\", halign: \'center\', align: \'center\', formatter: function (value, row, index)" +
" {\r\n                            if (row.Footer) {\r\n                             " +
"   return \"\";\r\n                            }\r\n                            if (ro" +
"w.FatherID == 0) {\r\n                                return \"<a href=\\\"#\\\" class=" +
"\\\"easyui-linkbutton\\\"  style=\\\"line-height:22px;\\\" onclick=\\\"OpenConSub(\'\" + row" +
".ProjId + \"\',\'\" + row.EngineeringNumber + \"\')\\\">付款合同</>\";\r\n                     " +
"       }\r\n                        }\r\n                    },\r\n                   " +
" {\r\n                        title: \'收款开票\', field: \'Id\', width: \"70px\", halign: \'" +
"center\', align: \'center\', JQIsExclude: true, formatter: function (value, row, in" +
"dex) {\r\n                            if (row.Footer) {\r\n                         " +
"       return \"\";\r\n                            }\r\n                            if" +
" (row.FatherID == 0) {\r\n                                return \"<a href=\\\"#\\\" cl" +
"ass=\\\"easyui-linkbutton\\\"  style=\\\"line-height:22px;\\\" onclick=\\\"FeeListView(\" +" +
" row.Id + \")\\\">收款开票</>\";\r\n                            } else {\r\n                " +
"                return \"\";\r\n                            }\r\n\r\n                   " +
"     }\r\n                    },\r\n                    {\r\n                        f" +
"ield: \'JQFiles\', title: \'附件\', align: \'center\', width: \"40px\", JQIsExclude: true," +
" JQExcludeFlag: \"grid_files\", JQRefTable: \'BussContract\', formatter: function (v" +
"alue, row) {\r\n                            return \"<span id=\\\"grid_files_\" + row." +
"Id + \"\\\"></span>\"\r\n                        }\r\n                    },\r\n          " +
"          {\r\n                        field: \"FlowProgress\", title: \"流程进度\", align" +
": \"left\", halign: \"center\", width: \"100px\", formatter: function (value, row, ind" +
"ex) {\r\n                            if (row.Footer) {\r\n                          " +
"      return \"\";\r\n                            } else {\r\n                        " +
"        return JQ.Flow.showProgress(\"FlowIDD\", \"FlowName\", \"FlowStatusID\", \"Flow" +
"StatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 226 "..\..\Views\BussContract\list_gc.cshtml"
                                                                                                                                                      Write(ViewBag.CurrentEmpID);

            
            #line default
            #line hidden
WriteLiteral(@""")(value, row);
                            }
                        }
                    },
                    {
                        field: ""opt"", title: ""操作"", align: ""center"", halign: ""center"", width: ""40px"", formatter: function (value, row, index) {
                            if (row.Footer) {
                                return """";
                            }
                            var title = ""查看"";
                            if (row._EditStatus == 1) {
                                title = ""修改"";
                            }
                            else if (row._EditStatus == 2) {
                                title = ""处理"";
                            }
                            return ' <a href=\""#\"" class=\""easyui-linkbutton\""  style=\""line-height:22px;\"" onclick=""EditCon(' + row.Id + ')"">' + title + '</a>';
                        }
                    }

                ]],
                onLoadSuccess: function (rowIndex, rowDatas) {
                    if ((");

            
            #line 248 "..\..\Views\BussContract\list_gc.cshtml"
                    Write(Html.Raw(ViewBag.Feepermission));

            
            #line default
            #line hidden
WriteLiteral(").indexOf(\"ContractFee\") == -1) {\r\n                        $(\"#BussContractGrid\")" +
".treegrid(\"hideColumn\", \"Id\");\r\n                    }\r\n\r\n                    var" +
" isShowCKAll = true;\r\n\r\n                    var data = rowDatas.rows;\r\n         " +
"           var $rows = $(\"#BussContractGrid\").parent().find(\".datagrid-view1 .da" +
"tagrid-body tr.datagrid-row\");\r\n                    for (var i = 0; i < data.len" +
"gth; i++) {\r\n\r\n                        if (data[i].FlowStatusID == 2 || data[i]." +
"FlowStatusID == 3) {\r\n                            var gRow = $rows.filter(\"[node" +
"-id=\'\" + data[i].Id + \"\']\");\r\n                            gRow.find(\"td[field=\'c" +
"k\']\").find(\"input[type=\'checkbox\']\").hide();\r\n                            isShow" +
"CKAll = false;\r\n                        }\r\n\r\n                        for (var j " +
"= 0; j < data[i].children.length; j++) {\r\n                            if (data[i" +
"].children[j].FlowStatusID == 2 || data[i].children[j].FlowStatusID == 3) {\r\n   " +
"                             var cgRow = $rows.filter(\"[node-id=\'\" + data[i].chi" +
"ldren[j].Id + \"\']\");\r\n                                cgRow.find(\"td[field=\'ck\']" +
"\").find(\"input[type=\'checkbox\']\").hide();\r\n                                isSho" +
"wCKAll = false;\r\n                            }\r\n                        }\r\n     " +
"               }\r\n                    if (isShowCKAll) {\r\n                      " +
"  $(\"div.datagrid-header-check input[type=\'checkbox\']\").attr(\"style\", \"display:n" +
"one\");\r\n                    }\r\n\r\n                    var htmlFooter = $(\"#BussCo" +
"ntractGrid\").datagrid(\"getPanel\").find(\".datagrid-footer\").find(\"tr\");\r\n        " +
"            htmlFooter.find(\"td[field=\\\"ConNumber\\\"]\").find(\".datagrid-cell\").em" +
"pty();\r\n                    htmlFooter.find(\"td[field=\\\"ConName\\\"]\").find(\".data" +
"grid-cell\").empty();\r\n                    htmlFooter.find(\"td[field=\\\"JQFiles\\\"]" +
"\").find(\".datagrid-cell\").empty();\r\n                    htmlFooter.find(\"td[fiel" +
"d=\\\"FlowProgress\\\"]\").find(\".datagrid-cell\").empty();\r\n                }\r\n      " +
"      });\r\n\r\n            $(\"#JQSearch\").textbox({\r\n                buttonText: \'" +
"筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n" +
"                prompt: \'合同编号、合同名称、客户名称\',\r\n                onClickButton: functi" +
"on () {\r\n                    ResGrid();\r\n                }\r\n            });\r\n\r\n " +
"           $(\"#ConIsFeeFinished\").combobox({\r\n                onChange: function" +
" () {\r\n                    ResGrid();\r\n                }\r\n            });\r\n\r\n   " +
"         $(\"#ConStatus1\").combotree({\r\n                onChange: function () {\r\n" +
"                   ResGrid();\r\n                }\r\n            });\r\n\r\n           " +
" $(\"#ConFulfilType1\").combotree({\r\n                onChange: function () {\r\n    " +
"               ResGrid();\r\n                }\r\n            });\r\n\r\n            $(\"" +
"#SKZT\").combobox({\r\n                onChange: function () {\r\n                   " +
" ResGrid();\r\n                }\r\n            });\r\n        });\r\n\r\n        function" +
" ResGrid() {\r\n            JQ.datagrid.searchGrid({\r\n                dgID: \'BussC" +
"ontractGrid\',\r\n                loadingType: \'treegrid\',\r\n                queryPa" +
"rams: queryParams,\r\n                setParamsFunc: function (paras) {\r\n         " +
"           var queryInfo = $.parseJSON(paras.queryInfo);\r\n\r\n                    " +
"$.each(queryInfo, function (index, item) {\r\n                        $.each(item." +
"list, function (index, data) {\r\n                            if (data.Key == \"Fee" +
"Fact\") {\r\n                                debugger;\r\n                           " +
"     data.Key = \"isnull((select Sum(FeeMoney) from BussFeeFact bf where bf.ConID" +
"=c.Id and bf.DeleterEmpId=0),0)\";\r\n                                if (data.Valu" +
"e == \"0\") {\r\n                                    data.Contract = \"<=\";\r\n        " +
"                        } else {\r\n                                    data.Value" +
" = \"0\";\r\n                                    data.Contract = \">\";\r\n             " +
"                   }\r\n                            }\r\n                        });" +
"\r\n                    });\r\n                    paras.queryInfo = JSON.stringify(" +
"queryInfo);\r\n                }\r\n            });\r\n        }\r\n\r\n        function F" +
"eeListView(ConID) {\r\n            JQ.dialog.dialog({\r\n                title: \"收款开" +
"票\",\r\n                url: \'");

            
            #line 349 "..\..\Views\BussContract\list_gc.cshtml"
                 Write(Url.Action("FeeDetails"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?ConID=' + ConID,
                width: '1024',
                height: '100%',
                JQID: 'BussContractGrid',
                JQLoadingType: 'treegrid',
                iconCls: 'fa fa-jpy',
                onClose:function () {
                    $(""#BussContractGrid"").treegrid(""reload"");
                }
            });
        }

        function OpenConSub(projId,EngineeringNumber){
            JQ.dialog.dialog({
                title: ""付款合同"",
                url: '");

            
            #line 364 "..\..\Views\BussContract\list_gc.cshtml"
                 Write(Url.Action("conSubList", "BussContractSub", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"?projectID='+projId+'&EngineeringNumber='+EngineeringNumber+'',
                width: '1024',
                height: '100%',
                JQID: 'BussContractGrid',
                JQLoadingType: 'treegrid',
                iconCls: 'fa fa-jpy',
                onClose:function () {
                    $(""#BussContractGrid"").treegrid(""reload"");
                }
            });
        }


        function AddCon() {
            var row = $('#BussContractGrid').treegrid('getSelections');
            if (row.length < 1) {
                window.top.JQ.dialog.warning('您必须选择至少一项进行操作！！！');
            }
            else {

                if (row[0]['FatherID'] != '0') {
                    window.top.JQ.dialog.warning('子合同下无法创建子合同！！！');
                    return;
                }

                JQ.dialog.dialog({
                    title: ""添加子合同"",
                    url: '");

            
            #line 391 "..\..\Views\BussContract\list_gc.cshtml"
                     Write(Url.Action("AddChild"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?CompanyType=GC&parentId=' + row[0]['Keyid'],
                    width: '1024',
                    height: '100%',
                    JQID: 'BussContractGrid',
                    JQLoadingType: 'treegrid',
                    iconCls: 'fa fa-plus'
                });
            }
        }

        function EditCon(id) {
            if(id){
                JQ.dialog.dialog({
                    title: ""编辑收款合同"",
                    url: '");

            
            #line 405 "..\..\Views\BussContract\list_gc.cshtml"
                     Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?CompanyType=GC&id=' + id,
                    width: '1024',
                    height: '100%',
                    JQID: 'BussContractGrid',
                    JQLoadingType: 'treegrid',
                    iconCls: 'fa fa-plus'
                });
            }
        }

        var _ConList_CallBack = function () {
            $(""#BussContractGrid"").datagrid(""reload"");
        }

        window.refreshGrid = function () {
            $(""#JQSearch"").textbox(""button"").click();
        }
    </script>
");

WriteLiteral("    ");

            
            #line 423 "..\..\Views\BussContract\list_gc.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussContractGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussContractPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n");

            
            #line 429 "..\..\Views\BussContract\list_gc.cshtml"
            
            
            #line default
            #line hidden
            
            #line 429 "..\..\Views\BussContract\list_gc.cshtml"
             if (Request.QueryString["from"] != "projectcenter")
            {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteLiteral(" id=\"AddChildCon\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"AddCon()\"");

WriteLiteral(">新增子合同</a>\r\n");

WriteLiteral("                <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n");

            
            #line 433 "..\..\Views\BussContract\list_gc.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </span>\r\n        <strong>单位：元</strong>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:400px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConName,ConNumber,CustName\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n            <span>\r\n                履行方式：");

            
            #line 440 "..\..\Views\BussContract\list_gc.cshtml"
                Write(BaseData.getBases(new Params()
                {
                    isMult = true,
                    controlName = "ConFulfilType1",
                    isRequired = false,
                    engName = "ConDealWays",
                    queryOptions = "{'Key':'ConFulfilType','Contract':'in','filedType':'Int'}",
                    width = "150px",
                }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </span>\r\n\r\n            <span>\r\n                签订状态：");

            
            #line 452 "..\..\Views\BussContract\list_gc.cshtml"
                Write(BaseData.getBases(new Params()
                {
                    isMult = true,
                    controlName = "ConStatus1",
                    isRequired = false,
                    engName = "ConStatus",
                    queryOptions = "{'Key':'ConStatus','Contract':'in','filedType':'Int'}",
                    width = "120px",
                }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </span>\r\n            <span>\r\n                是否结清：<select");

WriteLiteral(" name=\"ConIsFeeFinished\"");

WriteLiteral(" id=\"ConIsFeeFinished\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" editable=\"false\"");

WriteLiteral(" style=\'font-size:12px;width:80px;\'");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConIsFeeFinished\', Contract:\'=\',filedType:\'Int\' }\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"\"");

WriteLiteral(">请选择</option>\r\n                    <option");

WriteLiteral(" value=\'1\'");

WriteLiteral(">是</option>\r\n                    <option");

WriteLiteral(" value=\'0\'");

WriteLiteral(">否</option>\r\n                </select>\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;\"");

WriteLiteral(">\r\n                收款状态：<select");

WriteLiteral(" name=\"SKZT\"");

WriteLiteral(" id=\"SKZT\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" editable=\"false\"");

WriteLiteral(" style=\'font-size:12px;width:80px;\'");

WriteLiteral(" JQWhereOptions=\"{ Key:\'FeeFact\', Contract:\'=\',filedType:\'Int\' }\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"\"");

WriteLiteral(">请选择</option>\r\n                    <option");

WriteLiteral(" value=\'1\'");

WriteLiteral(">已收</option>\r\n                    <option");

WriteLiteral(" value=\'0\'");

WriteLiteral(">未收</option>\r\n                </select>\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;\"");

WriteLiteral(">\r\n                签订时间：<input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'签订日期开始\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConDate\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n                至\r\n                <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'签订日期结束\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConDate\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;\"");

WriteLiteral(">\r\n                合同金额：<input");

WriteLiteral(" id=\"ConFeeLower\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'合同金额\',min:0,precision:2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConFee\', Contract:\'>=\',filedType:\'decimal\' }\"");

WriteLiteral(">\r\n                ~\r\n                <input");

WriteLiteral(" id=\"ConFeeUpper\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'合同金额\',min:0,precision:2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'ConFee\', Contract:\'<=\',filedType:\'decimal\' }\"");

WriteLiteral(">\r\n            </span>\r\n            <span");

WriteLiteral(" style=\"padding-left:10px;display:none;\"");

WriteLiteral(">\r\n                保证金：<input");

WriteLiteral(" id=\"BZJLower\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'保证金金额\',min:0,precision:2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BZJ\', Contract:\'>=\',filedType:\'double\' }\"");

WriteLiteral(">\r\n                ~\r\n                <input");

WriteLiteral(" id=\"BZJUpper\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'保证金金额\',min:0,precision:2\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'BZJ\', Contract:\'<=\',filedType:\'double\' }\"");

WriteLiteral(">\r\n            </span>\r\n        </div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
