﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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
    
    #line 4 "..\..\Views\Decision\ContractTypeDecision.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Decision/ContractTypeDecision.cshtml")]
    public partial class _Views_Decision_ContractTypeDecision_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Decision_ContractTypeDecision_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Decision\ContractTypeDecision.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\Decision\ContractTypeDecision.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 193), Tuple.Create("\"", 245)
            
            #line 8 "..\..\Views\Decision\ContractTypeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 199), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/echarts/echarts.js")
            
            #line default
            #line hidden
, 199), false)
);

WriteLiteral("></script>\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        //$(\"#chartshape\").buttonset().find(\":radio\").change(function () {\r\n  " +
"      //    if ($(this).val() == 0) {//表示为图表\r\n        //        $(\"#monthMain\")." +
"show();\r\n        //    }\r\n        //    else {\r\n        //        $(\"#monthMain\"" +
").hide();\r\n        //    }\r\n        //});\r\n        var rwpTemp = {};\r\n        va" +
"r rwpTemp = {};\r\n        rwpTemp.yheight = 200;\r\n        //月度统计表格\r\n        rwpTe" +
"mp.option = {};\r\n        rwpTemp.myChart = null;\r\n        rwpTemp.myChartData = " +
"null;\r\n\r\n\r\n        //加载图标控件\r\n        rwpTemp.loadEcharts = function () {\r\n      " +
"      //rwpTemp.yheight = 80;\r\n\r\n            rwpTemp.getChartData();\r\n          " +
"  //合同大小月度统计\r\n            var dom = document.getElementById(\'monthNumber\');\r\n   " +
"         rwpTemp.myChart = require(\'echarts\').init(dom);\r\n\r\n            //合同大小年度" +
"统计\r\n            dom = document.getElementById(\'monthFee\');\r\n            rwpTemp." +
"YearChart = require(\'echarts\').init(dom);\r\n\r\n            //合同年度数量分析\r\n           " +
" dom = document.getElementById(\'YearNumber\');\r\n            rwpTemp.NumberChart =" +
" require(\'echarts\').init(dom);\r\n\r\n            //年度金额分析\r\n            dom = docume" +
"nt.getElementById(\'YearFee\');\r\n            rwpTemp.YearFeeChart = require(\'echar" +
"ts\').init(dom);\r\n\r\n\r\n            rwpTemp.setOption();\r\n        };\r\n\r\n        //设" +
"置图标高度\r\n        rwpTemp.SetMain = function (height) {\r\n            //  var minWid" +
"th = 850  ;\r\n            $(\"#monthNumber\").css({ height: $(\"#divContainer\").heig" +
"ht() / 4, padding: \'2px\' });\r\n            $(\"#monthFee\").css({ height: $(\"#divCo" +
"ntainer\").height() / 4, padding: \'2px\' });\r\n            $(\"#YearNumber\").css({ h" +
"eight: $(\"#divContainer\").height() / 4, padding: \'2px\' });\r\n            $(\"#Year" +
"Fee\").css({ height: $(\"#divContainer\").height() / 4, padding: \'2px\' });\r\n       " +
" };\r\n\r\n\r\n        //绑定图表数据\r\n        rwpTemp.setOption = function () {\r\n\r\n        " +
"    if (rwpTemp.myChartData != null) {\r\n                BindCharData(rwpTemp.opt" +
"ion, rwpTemp.myChartData, rwpTemp.myChart, $(\'#txtYear\').val() + \'年 合同分类金额月度分析\')" +
";\r\n            }\r\n            //年度统计\r\n            if (rwpTemp.YearChart != null)" +
" {\r\n                BindCharData(rwpTemp.option, rwpTemp.YearChartData, rwpTemp." +
"YearChart, $(\'#txtYear\').val() + \'年 合同分类数量月度分析\');\r\n            }\r\n\r\n            " +
"//合同年度数量分析\r\n            if (rwpTemp.NumberChart != null) {\r\n                Bind" +
"CharData(rwpTemp.option, rwpTemp.NumberChartData, rwpTemp.NumberChart, $(\'#txtYe" +
"ar\').val() + \'年 合同分类金额年度分析\');\r\n            }\r\n\r\n            //年度金额分析\r\n          " +
"  if (rwpTemp.YearFeeChart != null) {\r\n                BindCharData(rwpTemp.opti" +
"on, rwpTemp.YearFeeChartData, rwpTemp.YearFeeChart, $(\'#txtYear\').val() + \'年 合同分" +
"类数量年度分析\');\r\n            }\r\n        };\r\n\r\n        //绑定图表数据\r\n        function Bind" +
"CharData(option, ChartData, char, tile) {\r\n            for (var i = 0; i < optio" +
"n.series.length; i++) {\r\n                option.series[i].data = ChartData.data[" +
"i];\r\n            }\r\n            option.xAxis[0].data = ChartData.legendData;\r\n  " +
"          option.title.subtext = tile;\r\n            char.setOption(option);\r\n   " +
"         char.hideLoading();\r\n        }\r\n\r\n        var chartData;\r\n        //设置图" +
"标数据\r\n        rwpTemp.getChartData = function () {\r\n            debugger;\r\n      " +
"      //加载月度数据 \r\n            var url = \"");

            
            #line 101 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                   Write(Url.Action("ContractTypeMonthFee", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            LoadData(url, $(\'#txtYear\').val(),\"1\");\r\n            rwpTemp.myCh" +
"artData = chartData;\r\n            //月度数据\r\n             url = \"");

            
            #line 105 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                Write(Url.Action("ContractTypeMonthFee", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            LoadData(url, $(\'#txtYear\').val(), \"2\");\r\n            rwpTemp.Yea" +
"rChartData = chartData\r\n            // LoadData(url, rwpTemp.YearChartData);\r\n  " +
"          //年度数量分析\r\n             url = \"");

            
            #line 110 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                Write(Url.Action("ContractTypeFeeYear", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            LoadData(url, $(\'#txtYear\').val(), \"1\");\r\n            rwpTemp.Num" +
"berChartData = chartData;\r\n\r\n            //年度金额分析\r\n             url = \"");

            
            #line 115 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                Write(Url.Action("ContractTypeFeeYear", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@""";
            LoadData(url, $('#txtYear').val(), ""2"");
            rwpTemp.YearFeeChartData = chartData;

        }


        //后台加载数据type 1 :获取合同金额，2 获取合同数量
        function LoadData(url, Year,type) {
            $.ajax({
                type: ""post"",
                async: false, //同步执行  
                url: url,
                dataType: ""json"", //返回数据形式为json  
                data: {
                    ContractYear: Year,
                    type: type
                },
                success: function (result) {
                    if (result) {
                        chartData = result;
                    }
                },
                error: function (errorMsg) {
                    myChart.hideLoading();
                }
            });
        }


        //配置图表控件
        require.config({
            paths: {
                //  echarts: ""http://localhost/JQWebMVC/Scripts/echarts""
                echarts: ""http://"" + """);

            
            #line 149 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                                 Write(this.Request.Headers["Host"]);

            
            #line default
            #line hidden
WriteLiteral("\" + \"");

            
            #line 149 "..\..\Views\Decision\ContractTypeDecision.cshtml"
                                                                    Write(Url.Content("~/Scripts/echarts"));

            
            #line default
            #line hidden
WriteLiteral("\"\r\n            }\r\n        });\r\n\r\n        //初设图标控件主入口\r\n        require(\r\n\r\n       " +
"     [\r\n                \'echarts\',\r\n                \'echarts/chart/bar\',\r\n      " +
"          \'echarts/chart/line\',\r\n                \'echarts/chart/map\'\r\n          " +
"  ],\r\n             function (ec) {\r\n                 debugger;\r\n                " +
" rwpTemp.getChartData();\r\n                 rwpTemp.SetMain($(\"#divContainer\").he" +
"ight());\r\n                 rwpTemp.myChart = ec.init(document.getElementById(\'mo" +
"nthNumber\'));\r\n                 rwpTemp.myChart.showLoading({\r\n                 " +
"    text: \'正在努力加载中...\'\r\n                 });\r\n\r\n                 // rwpTemp.SetM" +
"ain($(\"#divContent\").height());\r\n                 rwpTemp.YearChart = ec.init(do" +
"cument.getElementById(\'monthFee\'));\r\n                 rwpTemp.YearChart.showLoad" +
"ing({\r\n                     text: \'正在努力加载中...\'\r\n                 });\r\n          " +
"       // rwpTemp.SetMain($(\"#divContent\").height());\r\n                 rwpTemp." +
"NumberChart = ec.init(document.getElementById(\'YearNumber\'));\r\n                 " +
"rwpTemp.NumberChart.showLoading({\r\n                     text: \'正在努力加载中...\'\r\n    " +
"             });\r\n                 // 年度金额分析\r\n                 rwpTemp.YearFeeCh" +
"art = ec.init(document.getElementById(\'YearFee\'));\r\n                 rwpTemp.Yea" +
"rFeeChart.showLoading({\r\n                     text: \'正在努力加载中...\'\r\n              " +
"   });\r\n\r\n                 rwpTemp.option = {\r\n\r\n                     title: {\r\n" +
"                         text: \' \',\r\n                         x: \'center\',\r\n    " +
"                     subtext: $(\'#txtYear\').val() + \'年 合同分类月度分析\'\r\n              " +
"       },\r\n                     tooltip: {\r\n                         trigger: \'a" +
"xis\'\r\n                     },\r\n                     legend: {\r\n\r\n               " +
"          data: [\'设计合同\', \'总包合同\', \'监理合同\', \'其他合同\']\r\n                     },\r\n     " +
"                toolbox: {\r\n                         show: true,\r\n              " +
"           orient: \'vertical\',\r\n                         feature: {\r\n           " +
"                  mark: { show: true },\r\n                             dataView: " +
"{ show: true, readOnly: false },\r\n                             magicType: { show" +
": true, type: [\'line\', \'bar\'] },\r\n                             restore: { show: " +
"true },\r\n                             saveAsImage: { show: true }\r\n             " +
"            }\r\n                     },\r\n                     calculable: true,\r\n" +
"                     yAxis: [\r\n                         {\r\n                     " +
"        type: \'value\'\r\n\r\n                         }\r\n                     ],\r\n  " +
"                   xAxis: [\r\n                         {\r\n                       " +
"      type: \'category\',\r\n                             data: null// [\'1月\', \'2月\', " +
"\'3月\', \'4月\', \'5月\', \'6月\', \'7月\', \'8月\', \'9月\', \'10月\', \'11月\', \'12月\']\r\n                " +
"         }\r\n                     ],\r\n                     series: [\r\n           " +
"              {\r\n                             name: \'设计合同\',\r\n                   " +
"          type: \'bar\',\r\n                             data: null,\r\n              " +
"               itemStyle: {\r\n                                 normal: {\r\n       " +
"                              label: {\r\n                                        " +
" show: true,\r\n                                         position: \'top\',\r\n       " +
"                                  formatter: \"{c}\"\r\n                            " +
"         }\r\n                                 }\r\n                             }\r\n" +
"                         },\r\n                          {\r\n                      " +
"        name: \'总包合同\',\r\n                              type: \'bar\',\r\n             " +
"                 data: null,\r\n                              itemStyle: {\r\n      " +
"                            normal: {\r\n                                      lab" +
"el: {\r\n                                          show: true,\r\n                  " +
"                        position: \'top\',\r\n                                      " +
"    formatter: \"{c}\"\r\n                                      }\r\n                 " +
"                 }\r\n                              }\r\n                          }" +
",\r\n                           {\r\n                               name: \'监理合同\',\r\n " +
"                              type: \'bar\',\r\n                               data:" +
" null,\r\n                               itemStyle: {\r\n                           " +
"        normal: {\r\n                                       label: {\r\n            " +
"                               show: true,\r\n                                    " +
"       position: \'top\',\r\n                                           formatter: \"" +
"{c}\"\r\n                                       }\r\n                                " +
"   }\r\n                               }\r\n                           },\r\n         " +
"                  {\r\n                               name: \'其他合同\',\r\n             " +
"                  type: \'bar\',\r\n                               data: null,\r\n    " +
"                           itemStyle: {\r\n                                   norm" +
"al: {\r\n                                       label: {\r\n                        " +
"                   show: true,\r\n                                           posit" +
"ion: \'top\',\r\n                                           formatter: \"{c}\"\r\n      " +
"                                 }\r\n                                   }\r\n      " +
"                         }\r\n                           }\r\n                     ]" +
"\r\n                 };\r\n\r\n\r\n\r\n                 rwpTemp.setOption();\r\n            " +
" }\r\n        );\r\n    </script>\r\n\r\n\r\n\r\n");

WriteLiteral("    ");

            
            #line 294 "..\..\Views\Decision\ContractTypeDecision.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"divToolbar\"");

WriteLiteral(" class=\"toolbar\"");

WriteLiteral(" style=\"border-bottom: 1px solid  #d6d6d6;\"");

WriteLiteral(">\r\n        <div>\r\n\r\n            <div");

WriteLiteral(" id=\"divSearch\"");

WriteLiteral(" class=\"showFullSearch\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" border=\"0\"");

WriteLiteral(" cellpadding=\"0\"");

WriteLiteral(" cellspacing=\"5\"");

WriteLiteral(" style=\"height: 100%\"");

WriteLiteral(">\r\n                    <tr>\r\n                        <td>立项日期：\r\n                 " +
"       </td>\r\n                        <td>\r\n                            <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"bbit-dp-input\"");

WriteLiteral(" style=\"width: 70px; height: 25px;\"");

WriteLiteral(" id=\"txtYear\"");

WriteLiteral(" value=\"2016\"");

WriteLiteral(" />\r\n                        </td>\r\n                        <td>\r\n               " +
"             <input");

WriteLiteral(" name=\"sbtnFilter\"");

WriteLiteral(" runat=\"server\"");

WriteLiteral(" class=\"InputBttn\"");

WriteLiteral(" id=\"sbtnFilter\"");

WriteLiteral(" style=\"width: 52px; padding-left: 18px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"筛选\"");

WriteLiteral(" onclick=\"rwpTemp.loadEcharts()\"");

WriteLiteral(" />\r\n                        </td>\r\n                    </tr>\r\n                </" +
"table>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"clear\"");

WriteLiteral("></div>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"divContent\"");

WriteLiteral(" class=\"divfix\"");

WriteLiteral(" style=\"height: 100%; overflow-y: auto\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"+zoom: 1; height: 1000px;\"");

WriteLiteral(" id=\"divContainer\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" id=\"monthNumber\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n            <div");

WriteLiteral(" id=\"monthFee\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n            <div");

WriteLiteral(" id=\"YearNumber\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n            <div");

WriteLiteral(" id=\"YearFee\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
