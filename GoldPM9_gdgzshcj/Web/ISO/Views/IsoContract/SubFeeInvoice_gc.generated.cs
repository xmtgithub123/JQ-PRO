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
    
    #line 2 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoContract/SubFeeInvoice_gc.cshtml")]
    public partial class _Views_IsoContract_SubFeeInvoice_gc_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoContract_SubFeeInvoice_gc_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var x = 0;\r\n    var requestconsuburl = \'");

            
            #line 6 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                       Write(Url.Action("jsonFeeSubInvoive", "BussContractSub", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\';\r\n    var requestprojsuburl = \'");

            
            #line 7 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                        Write(Url.Action("json", "ProjSub", new { @area= "Project" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\';\r\n\r\n    $(function () {\r\n        $(\"#Items\").hide();\r\n        JQ" +
".form.submitInit({\r\n            formid: \'BussSubFeeInvoiveForm\',//formid提交需要用到\r\n" +
"            buttonTypes: [\'submit\', \'close\'],//默认按钮\r\n            succesCollBack:" +
" function (data) {//成功回调函数,data为服务器返回值\r\n                var tab = $(\'#mainTab\')." +
"tabs(\'getSelected\');\r\n                var index = $(\'#mainTab\').tabs(\'getTabInde" +
"x\', tab);\r\n                var link = $(\"#mainTab\").find(\"iframe\")[index].conten" +
"tWindow.document.getElementById(\"LinkMan\");\r\n                if (window.top.docu" +
"ment.getElementById(\"linkMan\")!=null) {\r\n                    JQ.datagrid.autoRef" +
"datagrid();\r\n                }\r\n                else {\r\n                    if (" +
"link != null) {\r\n                        link.contentWindow.location.reload();\r\n" +
"                    }\r\n                }\r\n                JQ.dialog.dialogClose(" +
");//关闭窗体放在最后一步执行，以避免找不到事件源\r\n            },\r\n            beforeFormSubmit : funct" +
"ion () {\r\n                var xml = GoldPM.loadXml(\"<Root></Root>\");\r\n          " +
"      try {\r\n                    var datas = $(\"#SubFeeInvoiceGrid\").datagrid(\"g" +
"etData\");\r\n                    if (datas.total == 0) {\r\n                        " +
"alert(\"请先输外委合同信息!\");\r\n                        return false;\r\n                   " +
" }\r\n                    recuriseData(datas.rows, xml.documentElement);\r\n        " +
"            $(\"#strxml\").val(xml.documentElement.outerHTML);\r\n                }\r" +
"\n                catch (e) {\r\n                    alert(\"请先输入正确的信息!\");\r\n        " +
"            return false;\r\n                }\r\n            }\r\n        });\r\n\r\n    " +
"    $(\"#SubFeeInvoiceGrid\").datagrid({\r\n            idField: \'Id\',//主键的字段\r\n     " +
"       singleSelect: false,\r\n            pagination: false,\r\n            url: \'");

            
            #line 50 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
             Write(Url.Action("GetJsonList", "BussSubFeeInvoice", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',//请求的地址\r\n\r\n            queryParams: { FormTableID: ");

            
            #line 52 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                                   Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(" },\r\n            rownumbers: true,\r\n            toolbar: \'#SubFeeInvoicePanel\',//" +
"工具栏的容器ID\r\n            columns: [[\r\n               { field: \'ck\', align: \'center\'" +
", checkbox: true, JQIsExclude: true },\r\n                {\r\n                    t" +
"itle: \'外委合同信息\', field: \'ConSubId\', width: 180, align: \'center\', formatter: funct" +
"ion (value, rowData) {\r\n                        return \"<input type=\\\"text\\\" id=" +
"\\\"c_ConSubId\" + rowData.Id + \"\\\" name=\\\"c_ConSubId\" + rowData.Id + \"\\\"  style=\\\"" +
"width:98%;text-align:left; \\\"  />\";\r\n                    }\r\n                },\r\n" +
"                {\r\n                    title: \'外委项目信息\', field: \'ProjSubId\', widt" +
"h: 180, align: \'center\', formatter: function (value, rowData) {\r\n               " +
"         return \"<input type=\\\"text\\\" id=\\\"c_ProjSubId\" + rowData.Id + \"\\\"   nam" +
"e=\\\"c_ProjSubId\" + rowData.Id + \"\\\"   style=\\\"width:98%;text-align:left; \\\"    /" +
">\";\r\n                    }\r\n                },\r\n                {\r\n             " +
"       title: \'外委合同金额\', field: \'ConSubFee\', width: 80, align: \'right\', formatter" +
": function (value, rowData) {\r\n                        return \"<input type=\\\"tex" +
"t\\\" id=\\\"c_ConSubFee\" + rowData.Id + \"\\\"    name=\\\"c_ConSubFee\" + rowData.Id + \"" +
"\\\" style=\\\"width:90%;text-align:right;\\\"  disabled=\\\"disabled\\\"  />\";\r\n         " +
"           }\r\n                },\r\n                {\r\n                    title: " +
"\'已收票金额\', field: \'AlreadySumInvoiceMoney\', width: 80, align: \'right\', formatter: " +
"function (value, rowData) {\r\n                        return \"<input type=\\\"text\\" +
"\" id=\\\"c_AlreadySumInvoiceMoney\" + rowData.Id + \"\\\"  name=\\\"c_AlreadySumInvoiceM" +
"oney\" + rowData.Id + \"\\\" style=\\\"width:90%;text-align:right;\\\"  disabled=\\\"disab" +
"led\\\" />\";\r\n                    }\r\n                },\r\n                {\r\n      " +
"              title: \'本次收票金额\', field: \'SubFeeInvoiceMoney\', width: 90, align: \'c" +
"enter\', formatter: function (value, rowData) {\r\n                        return \"" +
"<input type=\\\"text\\\" id=\\\"c_SubFeeInvoiceMoney\" + rowData.Id + \"\\\"   name=\\\"c_Su" +
"bFeeInvoiceMoney\" + rowData.Id + \"\\\"  validType=\\\"intOrFloat\\\" style=\\\"width:90%" +
";text-align:right;\\\"    />\";\r\n                    }\r\n                },\r\n       " +
"         {\r\n                    title: \'本次收票日期\', field: \'SubFeeInvoiceDate\', wid" +
"th: 100, align: \'center\', formatter: function (value, rowData) {\r\n              " +
"          return \"<input type=\\\"text\\\" id=\\\"c_SubFeeInvoiceDate\" + rowData.Id + " +
"\"\\\" name=\\\"c_SubFeeInvoiceDate\" + rowData.Id + \"\\\" validType=\\\"dateISO\\\" style=\\" +
"\"width:100%;text-align:left; \\\"  />\";\r\n                    }\r\n                }," +
"\r\n               {\r\n                   title: \'累计比例\', field: \'TotalRatio\', width" +
": 80, align: \'center\', formatter: function (value, rowData) {\r\n                 " +
"      return \"<input type=\\\"text\\\" id=\\\"c_TotalRatio\" + rowData.Id + \"\\\"   name=" +
"\\\"c_TotalRatio\" + rowData.Id + \"\\\" style=\\\"width:90%;text-align:right;\\\"    />\";" +
"\r\n                   }\r\n               },\r\n               {\r\n                   " +
"title: \'收票类型结清\', field: \'SubFeeInvoiceType\', width: 100, align: \'center\', format" +
"ter: function (value, rowData) {\r\n                       var html = \"<select id=" +
"\\\"c_SubFeeInvoiceType\" + rowData.Id + \"\\\" class=\\\"InputSelect\\\" style=\\\"width:92" +
"px\\\">\";\r\n                       $(\"#Items option\").each(function () {\r\n         " +
"                  var _key = $(this).attr(\"value\");\r\n                           " +
"var _value = $(this).text();\r\n                           if(_key==decodeURICompo" +
"nent(value).replace(/\\+/g, \" \"))\r\n                           {\r\n                " +
"               html += \"<option selected=\\\"selected\\\" value=\" + _key + \">\" + _va" +
"lue + \"</option>\";\r\n                           }\r\n                           els" +
"e{\r\n                               html += \"<option value=\" + _key + \">\" + _valu" +
"e + \"</option>\";\r\n                           }\r\n                       });\r\n    " +
"                   html += \"</select>\";\r\n                       return html;\r\n  " +
"                 }\r\n               }\r\n            ]],\r\n            onLoadSuccess" +
":function(data){\r\n                $.each(data.rows, function(i, n){\r\n           " +
"         var rowIndex=n.Id;\r\n                    var consubID=n.ConSubId;\r\n     " +
"               var _consubid = \"c_ConSubId\" + rowIndex;\r\n                    (fu" +
"nction ($_consubid) {\r\n                        JQ.combobox.common({\r\n           " +
"                 id: $_consubid,\r\n                            toolbar: \'#tbConSu" +
"b\',//工具栏的容器ID\r\n                            url: requestconsuburl,\r\n             " +
"               panelWidth: 550,\r\n                            panelHeight: 320,\r\n" +
"                            valueField: \'Id\',\r\n                            textF" +
"ield: \'ConSubName\',\r\n                            multiple: false,\r\n             " +
"               pagination: true,//是否分页\r\n                            JQSearch: {\r" +
"\n                                id: \'fullNameSearchConSub\',\r\n                  " +
"              prompt: \'外委合同名称\',\r\n                                $panel: $(\"#tbC" +
"onSub\")//搜索的容器，可以不传\r\n                            },\r\n                           " +
" columns: [[\r\n                                         { field: \'Id\', hidden: tr" +
"ue },\r\n                                         { title: \'外委合同编号\', field: \'ConSu" +
"bNumber\', width: 120, align: \'left\', sortable: true },\r\n                        " +
"                 { title: \'外委合同名称\', field: \'ConSubName\', width: 120, align: \'lef" +
"t\', sortable: true },\r\n                                         { title: \'外包合同类型" +
"\', field: \'ConSubTypeName\', width: 120, align: \'left\', sortable: true },\r\n      " +
"                                   { title: \'外包合同类别\', field: \'ConSubCategoryName" +
"\', width: 120, align: \'left\', sortable: true },\r\n                            ]]," +
"\r\n                            onSelect: function (rowIndex, rowData) {\r\n        " +
"                        var array = _consubid.split(\'c_ConSubId\');\r\n            " +
"                    var y = array[1];\r\n                                $(\"#c_Con" +
"SubFee\" + y).val(rowData.ConSubFee);\r\n                                $(\"#c_Alre" +
"adySumInvoiceMoney\" + y).val(rowData.AlreadySumInvoiceMoney);\r\n                 " +
"           },\r\n                            onLoadSuccess:function() {\r\n         " +
"                       $(\"#\"+$_consubid).combogrid(\'setValue\',consubID);\r\n\r\n    " +
"                            var consubName=$(\"#\"+$_consubid).combogrid(\'getText\'" +
");\r\n                                if(!isNaN(consubName)){\r\n                   " +
"                 $(\"#\"+$_consubid).combogrid(\'setText\',n.ConSubName);\r\n         " +
"                       }\r\n                            }\r\n                       " +
" });\r\n                    }(_consubid));\r\n\r\n                    var projsubID=  " +
"n.ProjSubId\r\n                    var _projsubid = \"c_ProjSubId\" + rowIndex;\r\n   " +
"                 (function ($_projsubid) {\r\n                        JQ.combobox." +
"common({\r\n                            id: $_projsubid,\r\n                        " +
"    toolbar: \'#tbProjSub\',//工具栏的容器ID\r\n                            url: requestpr" +
"ojsuburl,\r\n                            panelWidth: 550,\r\n                       " +
"     panelHeight: 320,\r\n                            valueField: \'Id\',\r\n         " +
"                   textField: \'SubNumber\',\r\n                            multiple" +
": false,\r\n                            pagination: true,//是否分页\r\n                 " +
"           queryParams:{ConSubID:n.ConSubId},\r\n                            JQSea" +
"rch: {\r\n                                id: \'fullNameSearchProjSub\',\r\n          " +
"                      prompt: \'外委项目名称\',\r\n                                $panel:" +
" $(\"#tbProjSub\")//搜索的容器，可以不传\r\n                            },\r\n                  " +
"          columns: [[\r\n                                         { field: \'Id\', h" +
"idden: true },\r\n                                         { title: \'外委项目编号\', fiel" +
"d: \'SubNumber\', width: 120, align: \'left\', sortable: true },\r\n                  " +
"                       { title: \'外委项目名称\', field: \'SubName\', width: 120, align: \'" +
"left\', sortable: true },\r\n                                         { title: \'项目编" +
"号\', field: \'ProjNumber\', width: 120, align: \'left\', sortable: true },\r\n         " +
"                                { title: \'项目名称\', field: \'ProjName\', width: 120, " +
"align: \'left\', sortable: true },\r\n                                         { tit" +
"le: \'外委性质\', field: \'SubTypeName\', width: 120, align: \'left\', sortable: true },\r\n" +
"                            ]],\r\n                            onLoadSuccess:funct" +
"ion() {\r\n                                if(projsubID!=0){\r\n                    " +
"                $(\"#\"+$_projsubid).combogrid(\'setValue\',projsubID);\r\n           " +
"                         var projsubName=$(\"#\"+$_projsubid).combogrid(\'getText\')" +
";\r\n                                    if(!isNaN(projsubName)){\r\n               " +
"                         $(\"#\"+$_consubid).combogrid(\'setText\',n.SubName);\r\n    " +
"                                }\r\n                                }\r\n          " +
"                  }\r\n                        });\r\n                    }(_projsub" +
"id));\r\n\r\n                    var _subFeeFactMoney = \"c_SubFeeInvoiceMoney\" + row" +
"Index;\r\n                    (function ($_subFeeFactMoney) {\r\n                   " +
"     $(\'#\' + $_subFeeFactMoney).textbox({\r\n                            inputEven" +
"ts: $.extend({}, $.fn.textbox.defaults.inputEvents, {\r\n                         " +
"       keyup: function (e) {\r\n                                    var array = _s" +
"ubFeeFactMoney.split(\'c_SubFeeInvoiceMoney\');\r\n                                 " +
"   var y = array[1];\r\n                                    var t1 = $(\"#c_ConSubF" +
"ee\" + y).val();\r\n                                    var t2 = $(\"#c_AlreadySumIn" +
"voiceMoney\" + y).val();\r\n                                    var t3 = $(this).va" +
"l();\r\n                                    var s1 = (isNaN(parseFloat(t1)) == fal" +
"se ? parseFloat(t1) : 0) + (isNaN(parseFloat(t1)) == false ? parseFloat(t1) : 0)" +
";\r\n                                    var s2 = (isNaN(parseFloat(t2)) == false " +
"? parseFloat(t2) : 0) + (isNaN(parseFloat(t2)) == false ? parseFloat(t2) : 0);\r\n" +
"                                    var s3 = (isNaN(parseFloat(t3)) == false ? p" +
"arseFloat(t3) : 0) + (isNaN(parseFloat(t3)) == false ? parseFloat(t3) : 0);\r\n   " +
"                                 var s4 = 0;\r\n                                  " +
"  if (s1 != 0) {\r\n                                        s4 = Math.round(((s2 +" +
" s3) / s1) * 100);\r\n                                        s4 = isNaN(parseFloa" +
"t(s4)) == false ? parseFloat(s4) : 0\r\n                                    }\r\n   " +
"                                 $(\"#c_TotalRatio\"+y).textbox(\'setValue\',s4);\r\n " +
"                               }\r\n                            })\r\n              " +
"          })\r\n                    }(_subFeeFactMoney))\r\n\r\n                    $(" +
"\"#c_ConSubFee\"+rowIndex).textbox();\r\n                    $(\"#c_ConSubFee\"+rowInd" +
"ex).textbox(\'setValue\',n.ConSubFee);\r\n\r\n                    $(\"#c_AlreadySumInvo" +
"iceMoney\" + rowIndex).textbox();\r\n                    $(\"#c_AlreadySumInvoiceMon" +
"ey\" + rowIndex).textbox(\"setValue\", n.AlreadySumInvoiceMoney);\r\n\r\n              " +
"      $(\"#c_SubFeeInvoiceMoney\"+rowIndex).textbox();\r\n                    $(\"#c_" +
"SubFeeInvoiceMoney\"+rowIndex).textbox(\'setValue\',n.SubFeeInvoiceMoney);\r\n\r\n     " +
"               $(\"#c_SubFeeInvoiceDate\" + rowIndex).datebox({required:true });\r\n" +
"                    $(\"#c_SubFeeInvoiceDate\" + rowIndex).datebox(\"setValue\", n.S" +
"ubFeeInvoiceDate);\r\n\r\n                    $(\"#c_TotalRatio\"+rowIndex).textbox();" +
"\r\n                    $(\"#c_TotalRatio\"+rowIndex).textbox(\'setValue\',n.TotalRati" +
"o);\r\n                });\r\n            }\r\n        });\r\n\r\n\r\n    });\r\n\r\n    functio" +
"n AddRow() {\r\n        $(\"#consubId\").val(0);\r\n        x--;\r\n        var _data = " +
"{ \"Id\": x, \"ConSubId\": \"0\", \"ProjSubId\": \"0\", \"ConSubFee\": \"0\", \"AlreadySumInvoi" +
"ceMoney\": \"0\", \"SubFeeInvoiceMoney\": \"0\", \"SubFeeInvoiceDate\": \"\", \"TotalRatio\":" +
" \"0\", \"IsSettle\": 0 }\r\n        $(\'#SubFeeInvoiceGrid\').datagrid(\'appendRow\', _da" +
"ta);\r\n\r\n        var _consubid = \"c_ConSubId\" + x;\r\n        (function ($_consubid" +
") {\r\n            JQ.combobox.common({\r\n                id: $_consubid,\r\n        " +
"        toolbar: \'#tbConSub\',//工具栏的容器ID\r\n                url: requestconsuburl,\r" +
"\n                panelWidth: 550,\r\n                panelHeight: 320,\r\n          " +
"      valueField: \'Id\',\r\n                textField: \'ConSubName\',\r\n             " +
"   multiple: false,\r\n                pagination: true,//是否分页\r\n                JQ" +
"Search: {\r\n                    id: \'fullNameSearchConSub\',\r\n                    " +
"prompt: \'外委合同名称\',\r\n                    $panel: $(\"#tbConSub\")//搜索的容器，可以不传\r\n     " +
"           },\r\n                columns: [[\r\n                             { field" +
": \'Id\', hidden: true },\r\n                             { title: \'外委合同编号\', field: " +
"\'ConSubNumber\', width: 120, align: \'left\', sortable: true },\r\n                  " +
"           { title: \'外委合同名称\', field: \'ConSubName\', width: 120, align: \'left\', so" +
"rtable: true },\r\n                             { title: \'外包合同类型\', field: \'ConSubT" +
"ypeName\', width: 120, align: \'left\', sortable: true },\r\n                        " +
"     { title: \'外包合同类别\', field: \'ConSubCategoryName\', width: 120, align: \'left\', " +
"sortable: true },\r\n                ]],\r\n                onSelect: function (rowI" +
"ndex, rowData) {\r\n                    debugger;\r\n                    var array =" +
" _consubid.split(\'c_ConSubId\');\r\n                    var y = array[1];\r\n        " +
"            $(\"#c_ConSubFee\" + y).val(rowData.ConSubFee);\r\n                    $" +
"(\"#c_AlreadySumInvoiceMoney\" + y).val(rowData.AlreadySumInvoiceMoney);\r\n        " +
"            $(\"#consubId\").val(rowData.Id);\r\n\r\n                    var _projsubi" +
"d = \"c_ProjSubId\" + y;\r\n                    (function ($_projsubid) {\r\n         " +
"               JQ.combobox.common({\r\n                            id: $_projsubid" +
",\r\n                            toolbar: \'#tbProjSub\',//工具栏的容器ID\r\n               " +
"             url: requestprojsuburl,\r\n                            panelWidth: 55" +
"0,\r\n                            panelHeight: 320,\r\n                            v" +
"alueField: \'Id\',\r\n                            textField: \'SubNumber\',\r\n         " +
"                   queryParams:{ConSubID:$(\"#consubId\").val()},\r\n               " +
"             multiple: false,\r\n                            pagination: true,//是否" +
"分页\r\n                            JQSearch: {\r\n                                id:" +
" \'fullNameSearchProjSub\',\r\n                                prompt: \'外委合同编号,外委合同名" +
"称\',\r\n                                $panel: $(\"#tbProjSub\")//搜索的容器，可以不传\r\n      " +
"                      },\r\n                            columns: [[\r\n             " +
"                            { field: \'Id\', hidden: true },\r\n                    " +
"                     { title: \'外委项目编号\', field: \'SubNumber\', width: 120, align: \'" +
"left\', sortable: true },\r\n                                         { title: \'外委项" +
"目名称\', field: \'SubName\', width: 120, align: \'left\', sortable: true },\r\n          " +
"                               { title: \'项目编号\', field: \'ProjNumber\', width: 120," +
" align: \'left\', sortable: true },\r\n                                         { ti" +
"tle: \'项目名称\', field: \'ProjName\', width: 120, align: \'left\', sortable: true },\r\n  " +
"                                       { title: \'外委性质\', field: \'SubTypeName\', wi" +
"dth: 120, align: \'left\', sortable: true },\r\n                            ]]\r\n    " +
"                    });\r\n                    }(_projsubid))\r\n                }\r\n" +
"            });\r\n        }(_consubid));\r\n\r\n        var _projsubid = \"c_ProjSubId" +
"\" + x;\r\n        (function ($_projsubid) {\r\n\r\n            JQ.combobox.common({\r\n " +
"               id: $_projsubid,\r\n                toolbar: \'#tbProjSub\',//工具栏的容器I" +
"D\r\n                url: requestprojsuburl,\r\n                panelWidth: 550,\r\n  " +
"              panelHeight: 320,\r\n                valueField: \'Id\',\r\n            " +
"    textField: \'SubNumber\',\r\n                multiple: false,\r\n                p" +
"agination: true,//是否分页\r\n                JQSearch: {\r\n                    id: \'fu" +
"llNameSearchProjSub\',\r\n                    prompt: \'外委项目编号,外委项目名称\',\r\n           " +
"         $panel: $(\"#tbProjSub\")//搜索的容器，可以不传\r\n                },\r\n              " +
"  columns: [[\r\n                             { field: \'Id\', hidden: true },\r\n    " +
"                         { title: \'外委项目编号\', field: \'SubNumber\', width: 120, alig" +
"n: \'left\', sortable: true },\r\n                             { title: \'外委项目名称\', fi" +
"eld: \'SubName\', width: 120, align: \'left\', sortable: true },\r\n                  " +
"           { title: \'项目编号\', field: \'ProjNumber\', width: 120, align: \'left\', sort" +
"able: true },\r\n                             { title: \'项目名称\', field: \'ProjName\', " +
"width: 120, align: \'left\', sortable: true },\r\n                             { tit" +
"le: \'外委性质\', field: \'SubTypeName\', width: 120, align: \'left\', sortable: true },\r\n" +
"                ]]\r\n            });\r\n        }(_projsubid))\r\n\r\n        var _subF" +
"eeFactMoney = \"c_SubFeeInvoiceMoney\" + x;\r\n        (function ($_subFeeFactMoney)" +
" {\r\n            $(\'#\' + $_subFeeFactMoney).textbox({\r\n                inputEvent" +
"s: $.extend({}, $.fn.textbox.defaults.inputEvents, {\r\n                    keyup:" +
" function (e) {\r\n                        var array = _subFeeFactMoney.split(\'c_S" +
"ubFeeInvoiceMoney\');\r\n                        var y = array[1];\r\n               " +
"         var t1 = $(\"#c_ConSubFee\" + y).val();\r\n                        var t2 =" +
" $(\"#c_AlreadySumInvoiceMoney\" + y).val();\r\n                        var t3 = $(t" +
"his).val();\r\n                        var s1 = (isNaN(parseFloat(t1)) == false ? " +
"parseFloat(t1) : 0) + (isNaN(parseFloat(t1)) == false ? parseFloat(t1) : 0);\r\n  " +
"                      var s2 = (isNaN(parseFloat(t2)) == false ? parseFloat(t2) " +
": 0) + (isNaN(parseFloat(t2)) == false ? parseFloat(t2) : 0);\r\n                 " +
"       var s3 = (isNaN(parseFloat(t3)) == false ? parseFloat(t3) : 0) + (isNaN(p" +
"arseFloat(t3)) == false ? parseFloat(t3) : 0);\r\n                        var s4 =" +
" 0;\r\n                        if (s1 != 0) {\r\n                            s4 = Ma" +
"th.round(((s2 + s3) / s1) * 100);\r\n                            s4 = isNaN(parseF" +
"loat(s4)) == false ? parseFloat(s4) : 0\r\n                        }\r\n\r\n          " +
"              $(\"#c_TotalRatio\" + y).val(s4);\r\n                    }\r\n          " +
"      })\r\n            })\r\n        }(_subFeeFactMoney))\r\n\r\n        var t=(new Dat" +
"e()).format(\"yyyy-MM-dd\");\r\n        $(\"#c_SubFeeInvoiceDate\" + x).datebox({requi" +
"red:true });\r\n        $(\"#c_SubFeeInvoiceDate\" + x).datebox(\'setValue\',t);\r\n    " +
"}\r\n\r\n    function DeleteRow() {\r\n        var rows = $(\'#SubFeeInvoiceGrid\').data" +
"grid(\"getSelections\");\r\n        var copyRows = [];\r\n        for (var j = 0; j < " +
"rows.length; j++) {\r\n            copyRows.push(rows[j]);\r\n        }\r\n        for" +
" (var i = 0; i < copyRows.length; i++) {\r\n            var index = $(\'#SubFeeInvo" +
"iceGrid\').datagrid(\'getRowIndex\', copyRows[i]);\r\n            $(\'#SubFeeInvoiceGr" +
"id\').datagrid(\'deleteRow\', index);\r\n        }\r\n    }\r\n\r\n    function recuriseDat" +
"a(data, parentNode) {\r\n        for (var i = 0; i < data.length; i++) {\r\n        " +
"    var node = parentNode.ownerDocument.createElement(\"Item\");\r\n            node" +
".setAttribute(\"Id\", data[i].Id);\r\n            node.setAttribute(\"ConSubId\", $(\"#" +
"c_ConSubId\" + data[i].Id).combogrid(\'grid\').datagrid(\'getSelected\').Id);\r\n      " +
"      node.setAttribute(\"ProjSubId\", $(\"#c_ProjSubId\" + data[i].Id).combogrid(\'g" +
"rid\').datagrid(\'getSelected\') == null ? 0 : $(\"#c_ProjSubId\" + data[i].Id).combo" +
"grid(\'grid\').datagrid(\'getSelected\').Id);\r\n            node.setAttribute(\"ConSub" +
"Fee\", $(\"#c_ConSubFee\" + data[i].Id).val());\r\n            node.setAttribute(\"Alr" +
"eadySumInvoiceMoney\", $(\"#c_AlreadySumInvoiceMoney\" + data[i].Id).val());\r\n     " +
"       node.setAttribute(\"SubFeeInvoiceMoney\", $(\"#c_SubFeeInvoiceMoney\" + data[" +
"i].Id).val());\r\n            node.setAttribute(\"SubFeeInvoiceDate\", $(\"#c_SubFeeI" +
"nvoiceDate\" + data[i].Id).datebox(\'getValue\'));\r\n            node.setAttribute(\"" +
"TotalRatio\", $(\"#c_TotalRatio\" + data[i].Id).val());\r\n            node.setAttrib" +
"ute(\"SubFeeInvoiceType\", $(\"#c_SubFeeInvoiceType\" + data[i].Id).val());\r\n       " +
"     parentNode.appendChild(node);\r\n            if (data[i].children && data[i]." +
"children.length > 0) {\r\n                recuriseData(data[i].children, node);\r\n " +
"           }\r\n        }\r\n    }\r\n</script>\r\n");

            
            #line 400 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
 using (Html.BeginForm("saveSubFeeInvoice", "IsoContract", FormMethod.Post, new { @area = "ISO", @id = "BussSubFeeInvoiveForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 402 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 402 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                                  
    
            
            #line default
            #line hidden
            
            #line 403 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
Write(Html.DropDownList("Items"));

            
            #line default
            #line hidden
            
            #line 403 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                               ;


            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"GC\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(" style=\"border:none;\"");

WriteLiteral(">付款合同收票登记</td>\r\n        </tr>\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"SubFeeInvoiceGrid\"");

WriteLiteral("></table>\r\n                <div");

WriteLiteral(" id=\"SubFeeInvoicePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"AddRow()\"");

WriteLiteral(">新增</a>\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" onclick=\"DeleteRow()\"");

WriteLiteral(">删除</a>\r\n                    </span>\r\n                </div>\r\n\r\n            </td>" +
"\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" width=\"100%\"");

WriteLiteral(">\r\n\r\n");

            
            #line 428 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                
            
            #line default
            #line hidden
            
            #line 428 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.FormID;
                    uploader.RefTable = "IsoForm";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 433 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 433 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"tbConSub\"");

WriteLiteral(" style=\"padding:5px;height:auto;display:none\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchConSub\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"tbProjSub\"");

WriteLiteral(" style=\"padding:5px;height:auto;display:none\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchProjSub\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'txtLike\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"strxml\"");

WriteLiteral(" name=\"strxml\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"consubId\"");

WriteLiteral(" name=\"consubId\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />\r\n");

            
            #line 446 "..\..\Views\IsoContract\SubFeeInvoice_gc.cshtml"
                    }
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
