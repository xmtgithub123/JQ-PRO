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
    
    #line 2 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoContract/SubFeeFact_sj.cshtml")]
    public partial class _Views_IsoContract_SubFeeFact_sj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoContract_SubFeeFact_sj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var x = 0;\r\n    var requestconsuburl = \'");

            
            #line 6 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                       Write(Url.Action("jsonSubFeeFact", "BussContractSub", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\';\r\n    var requestprojsuburl = \'");

            
            #line 7 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                        Write(Url.Action("json", "ProjSub", new { @area= "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=SJ';
    JQ.form.submitInit({
        formid: ""BussSubFeeFactForm"",//formid提交需要用到
        buttonTypes: [""close""],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeFormSubmit: function () {
            var xml = GoldPM.loadXml(""<Root></Root>"");
            try {
                var datas = $(""#SubFeeFactGrid"").datagrid(""getData"");
                if (datas.total == 0) {
                    alert(""请先输外委合同信息!"");
                    return false;
                }
                recuriseData(datas.rows, xml.documentElement);
                $(""#strxml"").val(xml.documentElement.outerHTML);

            }
            catch (e) {
                alert(""请先输入正确的信息!"");
                return false;
            }
        },
        flow: {
            flowNodeID: """);

            
            #line 33 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 34 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 35 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.SubFeeFact\",\r\n            refID: \"");

            
            #line 38 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"SubFeeFact_SJ\",\r\n            isShowSave:\"");

            
            #line 40 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                    Write(ViewBag.AllowSave);

            
            #line default
            #line hidden
WriteLiteral("\"==\"1\",\r\n            dataCreator:\"");

            
            #line 41 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                     Write(Model.CreatorEmpId );

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        }\r\n    });\r\n\r\n    $(\"#SubFeeFactGrid\").datagrid({\r\n        idField: \'I" +
"d\',//主键的字段\r\n        singleSelect: false,\r\n        pagination: false,\r\n        ur" +
"l: \"");

            
            #line 49 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
          Write(Url.Action("GetJsonList", "BussSubFeeFact", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ\",//请求的地址\r\n\r\n        queryParams: { FormTableID: ");

            
            #line 51 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                               Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral(" },\r\n        rownumbers: true,\r\n        toolbar: \'#SubFeeFactPanel\',//工具栏的容器ID\r\n " +
"       columns: [[\r\n           { field: \'ck\', align: \'center\', checkbox: true, J" +
"QIsExclude: true },\r\n            {\r\n                title: \'外委合同信息\', field: \'Con" +
"SubId\', width: 160, align: \'center\', formatter: function (value, rowData) {\r\n   " +
"                 return \"<input type=\\\"text\\\" id=\\\"c_ConSubId\" + rowData.Id + \"\\" +
"\"     name=\\\"c_ConSubId\" + rowData.Id + \"\\\"  style=\\\"width:98%;text-align:left; " +
"\\\"  />\";\r\n                }\r\n            },\r\n            {\r\n                titl" +
"e: \'外委项目信息\', field: \'ProjSubId\', width: 160, align: \'center\', formatter: functio" +
"n (value, rowData) {\r\n                    return \"<input type=\\\"text\\\" id=\\\"c_Pr" +
"ojSubId\" + rowData.Id + \"\\\"   name=\\\"c_ProjSubId\" + rowData.Id + \"\\\"   style=\\\"w" +
"idth:98%;text-align:left; \\\"    />\";\r\n                }\r\n            },\r\n       " +
"     {\r\n                title: \'外委单位信息\', field: \'SKDW\', width: 160, align: \'cent" +
"er\', formatter: function (value, rowData) {\r\n                    return \"<input " +
"type=\\\"text\\\" id=\\\"c_SKDW\" + rowData.Id + \"\\\"   name=\\\"c_SKDW\" + rowData.Id + \"\\" +
"\"   style=\\\"width:98%;text-align:left; \\\" value=\\\"\" + value + \"\\\"    />\";\r\n     " +
"           }\r\n            },\r\n            {\r\n                title: \'外委合同金额\', fi" +
"eld: \'ConSubFee\', width: 80, align: \'right\', formatter: function (value, rowData" +
") {\r\n                    return \"<input type=\\\"text\\\" id=\\\"c_ConSubFee\" + rowDat" +
"a.Id + \"\\\"    name=\\\"c_ConSubFee\" + rowData.Id + \"\\\" style=\\\"width:90%;text-alig" +
"n:right;\\\"  disabled=\\\"disabled\\\"  />\";\r\n                }\r\n            },\r\n    " +
"        {\r\n                title: \'已付款金额\', field: \'AlreadySumFeeMoney\', width: 8" +
"0, align: \'right\', formatter: function (value, rowData) {\r\n                    r" +
"eturn \"<input type=\\\"text\\\" id=\\\"c_AlreadySumFeeMoney\" + rowData.Id + \"\\\"  name=" +
"\\\"c_AlreadySumFeeMoney\" + rowData.Id + \"\\\" style=\\\"width:90%;text-align:right;\\\"" +
"  disabled=\\\"disabled\\\" />\";\r\n                }\r\n            },\r\n            {\r\n" +
"                title: \'本次付款金额\', field: \'SubFeeFactMoney\', width: 80, align: \'ce" +
"nter\', formatter: function (value, rowData) {\r\n                    return \"<inpu" +
"t type=\\\"text\\\" id=\\\"c_SubFeeFactMoney\" + rowData.Id + \"\\\"   name=\\\"c_SubFeeFact" +
"Money\" + rowData.Id + \"\\\"  validType=\\\"intOrFloat\\\" style=\\\"width:90%;text-align" +
":right;\\\"    />\";\r\n                }\r\n            },\r\n            {\r\n           " +
"     title: \'本次付款日期\', field: \'SubFeeFactDate\', width: 120, align: \'center\', form" +
"atter: function (value, rowData) {\r\n                    return \"<input type=\\\"te" +
"xt\\\" id=\\\"c_SubFeeFactDate\" + rowData.Id + \"\\\" name=\\\"c_SubFeeFactDate\" + rowDat" +
"a.Id + \"\\\" validType=\\\"dateISO\\\" style=\\\"width:98%;text-align:left; \\\"  />\";\r\n  " +
"              }\r\n            },\r\n            {\r\n                title: \'累计比例\', f" +
"ield: \'TotalRatio\', width: 80, align: \'center\', formatter: function (value, rowD" +
"ata) {\r\n                    return \"<input type=\\\"text\\\" id=\\\"c_TotalRatio\" + ro" +
"wData.Id + \"\\\"   name=\\\"c_TotalRatio\" + rowData.Id + \"\\\" style=\\\"width:90%;text-" +
"align:right;\\\"    />\";\r\n                }\r\n            },\r\n            {\r\n      " +
"          title: \'是否结清\', field: \'IsSettle\', width: 100, align: \'center\', formatt" +
"er: function (value, rowData) {\r\n                    var strHtml = \"<select id=\\" +
"\"c_IsSettle\" + rowData.Id + \"\\\" class=\\\"InputSelect\\\" refID=\\\"c_IsSettle\" + rowD" +
"ata.Id + \"\\\" style=\\\"width:90px\\\">\";\r\n                    strHtml += \"<option se" +
"lected=\\\"selected\\\" value=\\\"0\\\">未结清</option>\";\r\n                    strHtml += \"" +
"<option  value=\\\"1\\\">已结清</option>\";\r\n                    strHtml += \"</select>\";" +
"\r\n                    return strHtml;\r\n                }\r\n            }\r\n       " +
" ]],\r\n        onLoadSuccess:function(data){\r\n\r\n            $.each(data.rows, fun" +
"ction(i, n){\r\n                debugger;\r\n                var rowIndex=n.Id;\r\n   " +
"             var consubID=n.ConSubId;\r\n                var _consubid = \"c_ConSub" +
"Id\" + rowIndex;\r\n                (function ($_consubid) {\r\n                    J" +
"Q.combobox.common({\r\n                        id: $_consubid,\r\n                  " +
"      toolbar: \'#tbConSub\',//工具栏的容器ID\r\n                        url: requestconsu" +
"burl,\r\n                        panelWidth: 550,\r\n                        panelHe" +
"ight: 320,\r\n                        valueField: \'Id\',\r\n                        t" +
"extField: \'ConSubName\',\r\n                        multiple: false,\r\n             " +
"           pagination: true,//是否分页\r\n                        JQSearch: {\r\n       " +
"                     id: \'fullNameSearchConSub\',\r\n                            pr" +
"ompt: \'外委合同名称\',\r\n                            $panel: $(\"#tbConSub\")//搜索的容器，可以不传\r" +
"\n                        },\r\n                        columns: [[\r\n              " +
"                       { field: \'Id\', hidden: true },\r\n                         " +
"            { title: \'外委合同编号\', field: \'ConSubNumber\', width: 120, align: \'left\'," +
" sortable: true },\r\n                                     { title: \'外委合同名称\', fiel" +
"d: \'ConSubName\', width: 120, align: \'left\', sortable: true },\r\n                 " +
"                    { title: \'外包合同类型\', field: \'ConSubTypeName\', width: 120, alig" +
"n: \'left\', sortable: true },\r\n                                     { title: \'外包合" +
"同类别\', field: \'ConSubCategoryName\', width: 120, align: \'left\', sortable: true },\r" +
"\n                        ]],\r\n                        onSelect: function (rowInd" +
"ex, rowData) {\r\n                            var array = _consubid.split(\'c_ConSu" +
"bId\');\r\n                            var y = array[1];\r\n                         " +
"   $(\"#c_ConSubFee\" + y).val(rowData.ConSubFee);\r\n                            $(" +
"\"#c_AlreadySumFeeMoney\" + y).val(rowData.AlreadySumFeeMoney);\r\n                 " +
"       },\r\n                        onLoadSuccess:function() {\r\n                 " +
"           $(\"#\"+$_consubid).combogrid(\'setValue\',consubID);\r\n\r\n                " +
"            var consubName=$(\"#\"+$_consubid).combogrid(\'getText\');\r\n            " +
"                if(!isNaN(consubName)){\r\n                                $(\"#\"+$" +
"_consubid).combogrid(\'setText\',n.ConSubName);\r\n                            }\r\n  " +
"                      }\r\n                    });\r\n                }(_consubid));" +
"\r\n\r\n                var projsubID=  n.ProjSubId\r\n                var _projsubid " +
"= \"c_ProjSubId\" + rowIndex;\r\n                (function ($_projsubid) {\r\n        " +
"            JQ.combobox.common({\r\n                        id: $_projsubid,\r\n    " +
"                    toolbar: \'#tbProjSub\',//工具栏的容器ID\r\n                        ur" +
"l: requestprojsuburl,\r\n                        panelWidth: 550,\r\n               " +
"         panelHeight: 320,\r\n                        valueField: \'Id\',\r\n         " +
"               textField: \'SubNumber\',\r\n                        multiple: false," +
"\r\n                        pagination: true,//是否分页\r\n                        query" +
"Params:{ConSubID:n.ConSubId},\r\n                        JQSearch: {\r\n            " +
"                id: \'fullNameSearchProjSub\',\r\n                            prompt" +
": \'外委项目名称\',\r\n                            $panel: $(\"#tbProjSub\")//搜索的容器，可以不传\r\n  " +
"                      },\r\n                        columns: [[\r\n                 " +
"                    { field: \'Id\', hidden: true },\r\n                            " +
"         { title: \'外委项目编号\', field: \'SubNumber\', width: 120, align: \'left\', sorta" +
"ble: true },\r\n                                     { title: \'外委项目名称\', field: \'Su" +
"bName\', width: 120, align: \'left\', sortable: true },\r\n                          " +
"           { title: \'项目编号\', field: \'ProjNumber\', width: 120, align: \'left\', sort" +
"able: true },\r\n                                     { title: \'项目名称\', field: \'Pro" +
"jName\', width: 120, align: \'left\', sortable: true },\r\n                          " +
"           { title: \'外委性质\', field: \'SubTypeName\', width: 120, align: \'left\', sor" +
"table: true },\r\n                        ]],\r\n                        onLoadSucce" +
"ss:function() {\r\n                            if(projsubID!=0){\r\n                " +
"                $(\"#\"+$_projsubid).combogrid(\'setValue\',projsubID);\r\n           " +
"                     var projsubName=$(\"#\"+$_projsubid).combogrid(\'getText\');\r\n " +
"                               if(!isNaN(projsubName)){\r\n                       " +
"             $(\"#\"+$_consubid).combogrid(\'setText\',n.SubName);\r\n                " +
"                }\r\n                            }\r\n                        }\r\n   " +
"                 });\r\n                }(_projsubid))\r\n\r\n\r\n                var _s" +
"ubFeeFactMoney = \"c_SubFeeFactMoney\" + rowIndex;\r\n                (function ($_s" +
"ubFeeFactMoney) {\r\n                    $(\'#\' + $_subFeeFactMoney).textbox({\r\n   " +
"                     inputEvents: $.extend({}, $.fn.textbox.defaults.inputEvents" +
", {\r\n                            keyup: function (e) {\r\n                        " +
"        var array = _subFeeFactMoney.split(\'c_SubFeeFactMoney\');\r\n              " +
"                  var y = array[1];\r\n                                var t1 = $(" +
"\"#c_ConSubFee\" + y).val();\r\n                                var t2 = $(\"#c_Alrea" +
"dySumFeeMoney\" + y).val();\r\n                                var t3 = $(this).val" +
"();\r\n                                var s1 = (isNaN(parseFloat(t1)) == false ? " +
"parseFloat(t1) : 0) + (isNaN(parseFloat(t1)) == false ? parseFloat(t1) : 0);\r\n  " +
"                              var s2 = (isNaN(parseFloat(t2)) == false ? parseFl" +
"oat(t2) : 0) + (isNaN(parseFloat(t2)) == false ? parseFloat(t2) : 0);\r\n         " +
"                       var s3 = (isNaN(parseFloat(t3)) == false ? parseFloat(t3)" +
" : 0) + (isNaN(parseFloat(t3)) == false ? parseFloat(t3) : 0);\r\n                " +
"                var s4 = 0;\r\n                                if (s1 != 0) {\r\n   " +
"                                 s4 = Math.round(((s2 + s3) / s1) * 100);\r\n     " +
"                               s4 = isNaN(parseFloat(s4)) == false ? parseFloat(" +
"s4) : 0\r\n                                }\r\n                                $(\"#" +
"c_TotalRatio\"+id).textbox(\'setValue\',s4);\r\n                            }\r\n      " +
"                  })\r\n                    })\r\n                }(_subFeeFactMoney" +
"))\r\n\r\n                $(\"#c_ConSubFee\"+rowIndex).textbox();\r\n                $(\"" +
"#c_ConSubFee\"+rowIndex).textbox(\'setValue\',n.ConSubFee);\r\n\r\n                $(\"#" +
"c_AlreadySumFeeMoney\"+rowIndex).textbox();\r\n                $(\"#c_AlreadySumFeeM" +
"oney\"+rowIndex).textbox(\'setValue\',n.AlreadySumFeeMoney);\r\n\r\n                $(\"" +
"#c_SubFeeFactMoney\"+rowIndex).textbox()\r\n                $(\"#c_SubFeeFactMoney\"+" +
"rowIndex).textbox(\'setValue\',n.SubFeeFactMoney);\r\n\r\n                $(\"#c_SubFee" +
"FactDate\" + rowIndex).datebox({required:true });\r\n                $(\"#c_SubFeeFa" +
"ctDate\" + rowIndex).datebox(\"setValue\", n.SubFeeFactDate);\r\n\r\n                $(" +
"\"#c_TotalRatio\"+rowIndex).textbox();\r\n                $(\"#c_TotalRatio\"+rowIndex" +
").textbox(\'setValue\',n.TotalRatio);\r\n            })\r\n        }\r\n    });\r\n\r\n    f" +
"unction AddRow() {\r\n\r\n        $(\"#consubId\").val(0);\r\n        x--;\r\n        var " +
"_data = { \"Id\": x, \"ConSubId\": \"0\", \"ProjSubId\": \"0\",\"SKDW\":\"\", \"ConSubFee\": \"0\"" +
", \"AlreadySumFeeMoney\": \"0\", \"SubFeeFactMoney\": \"0\", \"SubFeeFactDate\": \"\", \"Tota" +
"lRatio\": \"0\", \"IsSettle\": 0 }\r\n        $(\'#SubFeeFactGrid\').datagrid(\'appendRow\'" +
", _data);\r\n\r\n        var _consubid = \"c_ConSubId\" + x;\r\n        (function ($_con" +
"subid) {\r\n            JQ.combobox.common({\r\n                id: $_consubid,\r\n   " +
"             toolbar: \'#tbConSub\',//工具栏的容器ID\r\n                url: requestconsub" +
"url,\r\n                panelWidth: 550,\r\n                panelHeight: 320,\r\n     " +
"           valueField: \'Id\',\r\n                textField: \'ConSubName\',\r\n        " +
"        multiple: false,\r\n                pagination: true,//是否分页\r\n             " +
"   JQSearch: {\r\n                    id: \'fullNameSearchConSub\',\r\n               " +
"     prompt: \'外委合同编号,外委合同名称\',\r\n                    $panel: $(\"#tbConSub\")//搜索的容器" +
"，可以不传\r\n                },\r\n                columns: [[\r\n                        " +
"     { field: \'Id\', hidden: true },\r\n                             { title: \'外委合同" +
"编号\', field: \'ConSubNumber\', width: 120, align: \'left\', sortable: true },\r\n      " +
"                       { title: \'外委合同名称\', field: \'ConSubName\', width: 120, align" +
": \'left\', sortable: true },\r\n                             { title: \'外包合同类型\', fie" +
"ld: \'ConSubTypeName\', width: 120, align: \'left\', sortable: true },\r\n            " +
"                 { title: \'外包合同类别\', field: \'ConSubCategoryName\', width: 120, ali" +
"gn: \'left\', sortable: true },\r\n                ]],\r\n                onSelect: fu" +
"nction (rowIndex, rowData) {\r\n                    var array = _consubid.split(\'c" +
"_ConSubId\');\r\n                    var y = array[1];\r\n                    $(\"#c_C" +
"onSubFee\" + y).val(rowData.ConSubFee);\r\n                    $(\"#c_AlreadySumFeeM" +
"oney\" + y).val(rowData.AlreadySumFeeMoney);\r\n                    $(\"#consubId\")." +
"val(rowData.Id);\r\n\r\n                    var _projsubid = \"c_ProjSubId\" + y;\r\n   " +
"                 (function ($_projsubid) {\r\n                        JQ.combobox." +
"common({\r\n                            id: $_projsubid,\r\n                        " +
"    toolbar: \'#tbProjSub\',//工具栏的容器ID\r\n                            url: requestpr" +
"ojsuburl,\r\n                            panelWidth: 550,\r\n                       " +
"     panelHeight: 320,\r\n                            valueField: \'Id\',\r\n         " +
"                   textField: \'SubNumber\',\r\n                            queryPar" +
"ams:{ConSubID:$(\"#consubId\").val()},\r\n                            multiple: fals" +
"e,\r\n                            pagination: true,//是否分页\r\n                       " +
"     JQSearch: {\r\n                                id: \'fullNameSearchProjSub\',\r\n" +
"                                prompt: \'外委项目名称\',\r\n                             " +
"   $panel: $(\"#tbProjSub\")//搜索的容器，可以不传\r\n                            },\r\n        " +
"                    columns: [[\r\n                                         { fiel" +
"d: \'Id\', hidden: true },\r\n                                         { title: \'外委项" +
"目编号\', field: \'SubNumber\', width: 120, align: \'left\', sortable: true },\r\n        " +
"                                 { title: \'外委项目名称\', field: \'SubName\', width: 120" +
", align: \'left\', sortable: true },\r\n                                         { t" +
"itle: \'项目编号\', field: \'ProjNumber\', width: 120, align: \'left\', sortable: true },\r" +
"\n                                         { title: \'项目名称\', field: \'ProjName\', wi" +
"dth: 120, align: \'left\', sortable: true },\r\n                                    " +
"     { title: \'外委性质\', field: \'SubTypeName\', width: 120, align: \'left\', sortable:" +
" true },\r\n                            ]]\r\n                        });\r\n         " +
"           }(_projsubid))\r\n                }\r\n            });\r\n        }(_consub" +
"id));\r\n\r\n        var _projsubid = \"c_ProjSubId\" + x;\r\n        (function ($_projs" +
"ubid) {\r\n            JQ.combobox.common({\r\n                id: $_projsubid,\r\n   " +
"             toolbar: \'#tbProjSub\',//工具栏的容器ID\r\n                url: requestprojs" +
"uburl,\r\n                panelWidth: 550,\r\n                panelHeight: 320,\r\n   " +
"             valueField: \'Id\',\r\n                textField: \'SubNumber\',\r\n       " +
"         multiple: false,\r\n                pagination: true,//是否分页\r\n            " +
"    JQSearch: {\r\n                    id: \'fullNameSearchProjSub\',\r\n             " +
"       prompt: \'外委项目编号,外委项目名称\',\r\n                    $panel: $(\"#tbProjSub\")//搜索" +
"的容器，可以不传\r\n                },\r\n                columns: [[\r\n                     " +
"        { field: \'Id\', hidden: true },\r\n                             { title: \'外" +
"委项目编号\', field: \'SubNumber\', width: 120, align: \'left\', sortable: true },\r\n      " +
"                       { title: \'外委项目名称\', field: \'SubName\', width: 120, align: \'" +
"left\', sortable: true },\r\n                             { title: \'项目编号\', field: \'" +
"ProjNumber\', width: 120, align: \'left\', sortable: true },\r\n                     " +
"        { title: \'项目名称\', field: \'ProjName\', width: 120, align: \'left\', sortable:" +
" true },\r\n                             { title: \'外委性质\', field: \'SubTypeName\', wi" +
"dth: 120, align: \'left\', sortable: true },\r\n                ]]\r\n            });\r" +
"\n        }(_projsubid))\r\n\r\n        var _subFeeFactMoney = \"c_SubFeeFactMoney\" + " +
"x;\r\n        (function ($_subFeeFactMoney) {\r\n            $(\'#\' + $_subFeeFactMon" +
"ey).textbox({\r\n                inputEvents: $.extend({}, $.fn.textbox.defaults.i" +
"nputEvents, {\r\n                    keyup: function (e) {\r\n                      " +
"  var array = _subFeeFactMoney.split(\'c_SubFeeFactMoney\');\r\n                    " +
"    var y = array[1];\r\n                        var t1 = $(\"#c_ConSubFee\" + y).va" +
"l();\r\n                        var t2 = $(\"#c_AlreadySumFeeMoney\" + y).val();\r\n  " +
"                      var t3 = $(this).val();\r\n                        var s1 = " +
"(isNaN(parseFloat(t1)) == false ? parseFloat(t1) : 0) + (isNaN(parseFloat(t1)) =" +
"= false ? parseFloat(t1) : 0);\r\n                        var s2 = (isNaN(parseFlo" +
"at(t2)) == false ? parseFloat(t2) : 0) + (isNaN(parseFloat(t2)) == false ? parse" +
"Float(t2) : 0);\r\n                        var s3 = (isNaN(parseFloat(t3)) == fals" +
"e ? parseFloat(t3) : 0) + (isNaN(parseFloat(t3)) == false ? parseFloat(t3) : 0);" +
"\r\n                        var s4 = 0;\r\n                        if (s1 != 0) {\r\n " +
"                           s4 = Math.round(((s2 + s3) / s1) * 100);\r\n           " +
"                 s4 = isNaN(parseFloat(s4)) == false ? parseFloat(s4) : 0\r\n     " +
"                   }\r\n\r\n                        $(\"#c_TotalRatio\" + y).val(s4);\r" +
"\n                    }\r\n                })\r\n            })\r\n        }(_subFeeFac" +
"tMoney))\r\n\r\n        var t=(new Date()).format(\"yyyy-MM-dd\");\r\n        $(\"#c_SubF" +
"eeFactDate\" + x).datebox({required:true });\r\n        $(\"#c_SubFeeFactDate\" + x)." +
"datebox(\'setValue\',t);\r\n    }\r\n\r\n    function DeleteRow() {\r\n        var rows = " +
"$(\'#SubFeeFactGrid\').datagrid(\"getSelections\");\r\n        var copyRows = [];\r\n   " +
"     for (var j = 0; j < rows.length; j++) {\r\n            copyRows.push(rows[j])" +
";\r\n        }\r\n        for (var i = 0; i < copyRows.length; i++) {\r\n            v" +
"ar index = $(\'#SubFeeFactGrid\').datagrid(\'getRowIndex\', copyRows[i]);\r\n         " +
"   $(\'#SubFeeFactGrid\').datagrid(\'deleteRow\', index);\r\n        }\r\n    }\r\n\r\n    f" +
"unction recuriseData(data, parentNode) {\r\n        for (var i = 0; i < data.lengt" +
"h; i++) {\r\n            var node = parentNode.ownerDocument.createElement(\"Item\")" +
";\r\n            node.setAttribute(\"Id\", data[i].Id);\r\n            node.setAttribu" +
"te(\"ConSubId\", $(\"#c_ConSubId\" + data[i].Id).combogrid(\'grid\').datagrid(\'getSele" +
"cted\').Id);\r\n            node.setAttribute(\"ProjSubId\", $(\"#c_ProjSubId\" + data[" +
"i].Id).combogrid(\'grid\').datagrid(\'getSelected\') == null ? 0 : $(\"#c_ProjSubId\" " +
"+ data[i].Id).combogrid(\'grid\').datagrid(\'getSelected\').Id);\r\n            node.s" +
"etAttribute(\"ConSubFee\", $(\"#c_ConSubFee\" + data[i].Id).val());\r\n            nod" +
"e.setAttribute(\"AlreadySumFeeMoney\", $(\"#c_AlreadySumFeeMoney\" + data[i].Id).val" +
"());\r\n            node.setAttribute(\"SubFeeFactMoney\", $(\"#c_SubFeeFactMoney\" + " +
"data[i].Id).val());\r\n            node.setAttribute(\"SubFeeFactDate\", $(\"#c_SubFe" +
"eFactDate\" + data[i].Id).datebox(\'getValue\'));\r\n            node.setAttribute(\"T" +
"otalRatio\", $(\"#c_TotalRatio\" + data[i].Id).val());\r\n            node.setAttribu" +
"te(\"IsSettle\", $(\"#c_IsSettle\" + data[i].Id).val());\r\n            parentNode.app" +
"endChild(node);\r\n            if (data[i].children && data[i].children.length > 0" +
") {\r\n                recuriseData(data[i].children, node);\r\n            }\r\n     " +
"   }\r\n    }\r\n\r\n</script>\r\n");

            
            #line 395 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
 using (Html.BeginForm("saveSubFeeFact", "IsoContract", FormMethod.Post, new { @area = "ISO", @id = "BussSubFeeFactForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 397 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 397 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                                  

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"SJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(" style=\"border:none;\"");

WriteLiteral(">付款合同付款登记</td>\r\n        </tr>\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"SubFeeFactGrid\"");

WriteLiteral("></table>\r\n                <div");

WriteLiteral(" id=\"SubFeeFactPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" name=\"btnAddRow\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"AddRow()\"");

WriteLiteral(">新增</a>\r\n                        <a");

WriteLiteral(" name=\"btnDeleteRow\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" onclick=\"DeleteRow()\"");

WriteLiteral(">删除</a>\r\n                    </span>\r\n                </div>\r\n            </td>\r\n" +
"        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" width=\"100%\"");

WriteLiteral(">\r\n");

            
            #line 419 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                
            
            #line default
            #line hidden
            
            #line 419 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.FormID;
                    uploader.RefTable = "IsoForm";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 424 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 424 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"
                                                                                                   
                
            
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

            
            #line 437 "..\..\Views\IsoContract\SubFeeFact_sj.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
