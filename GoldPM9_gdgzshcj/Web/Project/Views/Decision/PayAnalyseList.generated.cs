﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.36373
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
    
    #line 4 "..\..\Views\Decision\PayAnalyseList.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Decision/PayAnalyseList.cshtml")]
    public partial class _Views_Decision_PayAnalyseList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Decision_PayAnalyseList_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Decision\PayAnalyseList.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 6 "..\..\Views\Decision\PayAnalyseList.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 193), Tuple.Create("\"", 245)
            
            #line 8 "..\..\Views\Decision\PayAnalyseList.cshtml"
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
"null;\r\n\r\n        //年度完成情况\r\n        rwpTemp.YearChart = null;\r\n        rwpTemp.Ye" +
"arChartData = null;\r\n\r\n        //阶段完成情况\r\n        rwpTemp.PhaseChart = null;\r\n   " +
"     rwpTemp.PhaseChartData = null;\r\n\r\n        //专业室完成情况\r\n         rwpTemp.DepOp" +
"tion  = {};\r\n        //rwpTemp.DepChart = null;\r\n        rwpTemp.DepChartData = " +
"null;\r\n\r\n        \r\n\r\n\r\n        //加载图标控件\r\n        rwpTemp.loadEcharts = function " +
"() {\r\n            //rwpTemp.yheight = 80;\r\n\r\n            rwpTemp.getChartData();" +
"\r\n            //合同大小月度统计\r\n            var dom = document.getElementById(\'monthNu" +
"mber\');\r\n            rwpTemp.myChart = require(\'echarts\').init(dom);\r\n\r\n        " +
"    //合同大小年度统计\r\n            dom = document.getElementById(\'YearNumber\');\r\n      " +
"      rwpTemp.YearChart = require(\'echarts\').init(dom);\r\n\r\n            //合同年度数量分" +
"析\r\n            //dom = document.getElementById(\'PhaseNumber\');\r\n            //rw" +
"pTemp.PhaseChart = require(\'echarts\').init(dom);\r\n\r\n            //年度金额分析\r\n      " +
"      //dom = document.getElementById(\'DepNumber\');\r\n            //rwpTemp.DepCh" +
"art = require(\'echarts\').init(dom);\r\n\r\n\r\n            rwpTemp.setOption();\r\n     " +
"   };\r\n\r\n        //设置图标高度\r\n        rwpTemp.SetMain = function (height) {\r\n      " +
"      //  var minWidth = 850  ;\r\n            $(\"#monthNumber\").css({ height: $(\"" +
"#divContainer\").height() / 3, padding: \'2px\' });\r\n            $(\"#YearNumber\").c" +
"ss({ height: $(\"#divContainer\").height() / 3, padding: \'2px\' });\r\n            //" +
"$(\"#PhaseNumber\").css({ height: $(\"#divContainer\").height() / 4, padding: \'2px\' " +
"});\r\n            $(\"#DepNumber\").css({ height: $(\"#divContainer\").height() / 2, " +
"padding: \'2px\' });\r\n        };\r\n\r\n\r\n        //绑定图表数据\r\n        rwpTemp.setOption " +
"= function () {\r\n\r\n            if (rwpTemp.myChartData != null) {\r\n             " +
"   BindCharData(rwpTemp.option, rwpTemp.myChartData, rwpTemp.myChart, $(\'#txtYea" +
"r\').val() + \'年 月度部门产值统计分析\');\r\n            }\r\n            //年度统计\r\n            if " +
"(rwpTemp.YearChart != null) {\r\n                BindCharData(rwpTemp.option, rwpT" +
"emp.YearChartData, rwpTemp.YearChart, $(\'#txtYear\').val() + \'年 年度部门产值统计分析\');\r\n  " +
"          }\r\n\r\n          \r\n\r\n            //年度金额分析\r\n            var myDepChart = " +
"require(\'echarts\').init( document.getElementById(\'DepNumber\'));// 图表初始化的地方，在页面中要" +
"有一个地方来显示图表，他的ID是main  \r\n            rwpTemp.DepOption = getOptionByArray(rwpTemp" +
".DepChartData, \"部门产值\");//得到option图形  \r\n            myDepChart.setOption(rwpTemp." +
"DepOption); //显示图形  \r\n        };\r\n\r\n        //绑定图表数据\r\n        function BindCharD" +
"ata(option, ChartData, char, tile) {\r\n            for (var i = 0; i < option.ser" +
"ies.length; i++) {\r\n                option.series[i].data = ChartData.data[i];\r\n" +
"            }\r\n            option.xAxis[0].data = ChartData.legendData;\r\n       " +
"     option.title.subtext = tile;\r\n            char.setOption(option);\r\n        " +
"    char.hideLoading();\r\n        }\r\n\r\n\r\n        //绑定图表数据\r\n        //function Bin" +
"dSerieCharData(option, ChartData, char, tile) {\r\n        //    char.setOption({\r" +
"\n \r\n        //        series: functionName(ChartData)\r\n        //    })\r\n       " +
" //}\r\n\r\n        function functionName(data) {\r\n            var serie = [];\r\n    " +
"        for (var i = 0; i < data.data.length; i++) {\r\n                var item =" +
" {\r\n                    name: data.dataName[i],\r\n                    type: \'bar\'" +
",\r\n                    data: data.data[i],\r\n                    itemStyle: {\r\n  " +
"                      normal: {\r\n                            label: {\r\n         " +
"                       show: true,\r\n                                position: \'t" +
"op\',\r\n                                formatter: \"{c}\"\r\n                        " +
"    }\r\n                        }\r\n                    }\r\n                }\r\n    " +
"         \r\n                serie.push(item);\r\n            };\r\n            return" +
" serie;\r\n        }\r\n\r\n        function getOptionByArray(json, reportName) {\r\n   " +
"         var DepOption = {\r\n                title: {\r\n                    text: " +
"\'\',\r\n                    x: \'center\'\r\n                },\r\n                toolti" +
"p: {\r\n                    trigger: \'item\',\r\n                    formatter: \"{a} " +
"<br/>{b} : {c}\"\r\n                },\r\n                legend: {\r\n                " +
"  //  orient: \'vertical\',\r\n                    //x:\'bottom\',\r\n                  " +
"  data: json.dataName\r\n                },\r\n                toolbox: {\r\n         " +
"           show: true,\r\n                    orient: \'vertical\',\r\n               " +
"     feature: {\r\n                        mark: { show: true },\r\n                " +
"        dataView: { show: true, readOnly: false },\r\n                        magi" +
"cType: { show: true, type: [\'line\', \'bar\'] },\r\n                        restore: " +
"{ show: true },\r\n                        saveAsImage: { show: true }\r\n          " +
"          }\r\n                },\r\n                calculable: true,\r\n            " +
"    yAxis: [\r\n                    {\r\n                        type: \'value\'\r\n\r\n  " +
"                  }\r\n                ],\r\n                xAxis: [\r\n             " +
"       {\r\n                        type: \'category\',\r\n                        dat" +
"a: json.legendData// [\'1月\', \'2月\', \'3月\', \'4月\', \'5月\', \'6月\', \'7月\', \'8月\', \'9月\', \'10月" +
"\', \'11月\', \'12月\']\r\n                    }\r\n                ],\r\n                ser" +
"ies: functionName(rwpTemp.DepChartData)\r\n            };\r\n            return DepO" +
"ption;\r\n        }\r\n\r\n        var chartData;\r\n        //设置图标数据\r\n        rwpTemp.g" +
"etChartData = function () {\r\n            debugger;\r\n            //加载月度数据 \r\n     " +
"       var url = \"");

            
            #line 189 "..\..\Views\Decision\PayAnalyseList.cshtml"
                   Write(Url.Action("PayMonthReport", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            LoadData(url, $(\'#txtYear\').val() );\r\n            rwpTemp.myChart" +
"Data = chartData;\r\n            //月度数据\r\n             url = \"");

            
            #line 193 "..\..\Views\Decision\PayAnalyseList.cshtml"
                Write(Url.Action("PayYearReport", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            LoadData(url, $(\'#txtYear\').val() );\r\n            rwpTemp.YearCha" +
"rtData = chartData\r\n            // LoadData(url, rwpTemp.YearChartData);\r\n      " +
"      //年度数量分析\r\n         ");

WriteLiteral("\r\n\r\n            //年度金额分析\r\n           url = \"");

            
            #line 203 "..\..\Views\Decision\PayAnalyseList.cshtml"
              Write(Url.Action("PayEmpManMonthReport", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@""";
            LoadData(url, $('#txtYear').val()  );
            rwpTemp.DepChartData = chartData; 

        }


        //后台加载数据type 1 :获取合同金额，2 获取合同数量
        function LoadData(url, Year ) {
            $.ajax({
                type: ""post"",
                async: false, //同步执行  
                url: url,
                dataType: ""json"", //返回数据形式为json  
                data: {
                   
                    StartDate: Year,
                    EndDate: Year,
                    ConYear: Year
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

            
            #line 239 "..\..\Views\Decision\PayAnalyseList.cshtml"
                                 Write(this.Request.Headers["Host"]);

            
            #line default
            #line hidden
WriteLiteral("\" + \"");

            
            #line 239 "..\..\Views\Decision\PayAnalyseList.cshtml"
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
"cument.getElementById(\'YearNumber\'));\r\n                 rwpTemp.YearChart.showLo" +
"ading({\r\n                     text: \'正在努力加载中...\'\r\n                 });\r\n        " +
"  \r\n\r\n                 var myDepChart = ec.init(document.getElementById(\'DepNumb" +
"er\'));// 图表初始化的地方，在页面中要有一个地方来显示图表，他的ID是main  \r\n                 rwpTemp.DepOptio" +
"n = getOptionByArray(rwpTemp.DepChartData, \"部门产值\");//得到option图形  \r\n             " +
"    myDepChart.setOption(rwpTemp.DepOption); //显示图形  \r\n\r\n\r\n                 rwpT" +
"emp.option = {\r\n\r\n                     title: {\r\n                         text: " +
"\' \',\r\n                         x: \'center\',\r\n                         subtext: $" +
"(\'#txtYear\').val() + \'年 合同分类月度分析\'\r\n                     },\r\n                    " +
" tooltip: {\r\n                         trigger: \'axis\'\r\n                     },\r\n" +
"                     legend: {\r\n\r\n                         data: [\'输变电室\', \'配电自动化" +
"室\', \'配电室\', \'技经室\']\r\n                     },\r\n                     toolbox: {\r\n   " +
"                      show: true,\r\n                         orient: \'vertical\',\r" +
"\n                         feature: {\r\n                             mark: { show:" +
" true },\r\n                             dataView: { show: true, readOnly: false }" +
",\r\n                             magicType: { show: true, type: [\'line\', \'bar\'] }" +
",\r\n                             restore: { show: true },\r\n                      " +
"       saveAsImage: { show: true }\r\n                         }\r\n                " +
"     },\r\n                     calculable: true,\r\n                     yAxis: [\r\n" +
"                         {\r\n                             type: \'value\'\r\n\r\n      " +
"                   }\r\n                     ],\r\n                     xAxis: [\r\n  " +
"                       {\r\n                             type: \'category\',\r\n      " +
"                       data: null// [\'1月\', \'2月\', \'3月\', \'4月\', \'5月\', \'6月\', \'7月\', \'" +
"8月\', \'9月\', \'10月\', \'11月\', \'12月\']\r\n                         }\r\n                   " +
"  ],\r\n                     series: [\r\n                         {\r\n              " +
"               name: \'输变电室\',\r\n                             type: \'bar\',\r\n       " +
"                      data: null,\r\n                             itemStyle: {\r\n  " +
"                               normal: {\r\n                                     l" +
"abel: {\r\n                                         show: true,\r\n                 " +
"                        position: \'top\',\r\n                                      " +
"   formatter: \"{c}\"\r\n                                     }\r\n                   " +
"              }\r\n                             }\r\n                         },\r\n  " +
"                        {\r\n                              name: \'配电自动化室\',\r\n      " +
"                        type: \'bar\',\r\n                              data: null,\r" +
"\n                              itemStyle: {\r\n                                  n" +
"ormal: {\r\n                                      label: {\r\n                      " +
"                    show: true,\r\n                                          posit" +
"ion: \'top\',\r\n                                          formatter: \"{c}\"\r\n       " +
"                               }\r\n                                  }\r\n         " +
"                     }\r\n                          },\r\n                          " +
" {\r\n                               name: \'配电室\',\r\n                               " +
"type: \'bar\',\r\n                               data: null,\r\n                      " +
"         itemStyle: {\r\n                                   normal: {\r\n           " +
"                            label: {\r\n                                          " +
" show: true,\r\n                                           position: \'top\',\r\n     " +
"                                      formatter: \"{c}\"\r\n                        " +
"               }\r\n                                   }\r\n                        " +
"       }\r\n                           },\r\n                            {\r\n        " +
"                        name: \'技经室\',\r\n                                type: \'bar" +
"\',\r\n                                data: null,\r\n                               " +
" itemStyle: {\r\n                                    normal: {\r\n                  " +
"                      label: {\r\n                                            show" +
": true,\r\n                                            position: \'top\',\r\n         " +
"                                   formatter: \"{c}\"\r\n                           " +
"             }\r\n                                    }\r\n                         " +
"       }\r\n                            } \r\n                     ]\r\n              " +
"   };\r\n\r\n                  \r\n\r\n                 rwpTemp.setOption();\r\n          " +
"   }\r\n        );\r\n    </script>\r\n\r\n\r\n\r\n");

WriteLiteral("    ");

            
            #line 380 "..\..\Views\Decision\PayAnalyseList.cshtml"
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

WriteLiteral(" style=\"height: 100%;width:100%; overflow-x: auto\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"+zoom: 1; height:800px;overflow: auto\"");

WriteLiteral(" id=\"divContainer\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" id=\"monthNumber\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n            <div");

WriteLiteral(" id=\"YearNumber\"");

WriteLiteral(" style=\"width: 98%;\"");

WriteLiteral("></div>");

WriteLiteral("\r\n           ");

WriteLiteral("\r\n            <div");

WriteLiteral(" id=\"DepNumber\"");

WriteLiteral(" style=\"width: 3000px; \"");

WriteLiteral("></div>");

WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591