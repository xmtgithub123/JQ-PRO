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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseRestDay/list.cshtml")]
    public partial class _Views_BaseRestDay_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BaseRestDay_list_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<html>\r\n<head>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 25), Tuple.Create("\"", 96)
            
            #line 3 "..\..\Views\BaseRestDay\list.cshtml"
, Tuple.Create(Tuple.Create("", 32), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/dhtmlxscheduler.css")
            
            #line default
            #line hidden
, 32), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" media=\"screen\"");

WriteLiteral(">\r\n        html, body {\r\n            margin: 0px;\r\n            padding: 0px;\r\n   " +
"         height: 100%;\r\n            overflow: hidden;\r\n        }\r\n    </style>\r\n" +
"</head>\r\n<body>\r\n    <div");

WriteLiteral(" id=\"scheduler_here\"");

WriteLiteral(" class=\"dhx_cal_container\"");

WriteLiteral(" style=\'width:100%; height:100%;\'");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"dhx_cal_navline\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"dhx_cal_prev_button\"");

WriteLiteral(">&nbsp;</div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_next_button\"");

WriteLiteral(">&nbsp;</div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_today_button\"");

WriteLiteral("></div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_date\"");

WriteLiteral("></div>\r\n            ");

WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dhx_cal_header\"");

WriteLiteral(">\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dhx_cal_data\"");

WriteLiteral(">\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1219), Tuple.Create("\"", 1266)
            
            #line 32 "..\..\Views\BaseRestDay\list.cshtml"
, Tuple.Create(Tuple.Create("", 1225), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jquery.min.js")
            
            #line default
            #line hidden
, 1225), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1309), Tuple.Create("\"", 1378)
            
            #line 33 "..\..\Views\BaseRestDay\list.cshtml"
, Tuple.Create(Tuple.Create("", 1315), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/dhtmlxscheduler.js")
            
            #line default
            #line hidden
, 1315), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1421), Tuple.Create("\"", 1502)
            
            #line 34 "..\..\Views\BaseRestDay\list.cshtml"
, Tuple.Create(Tuple.Create("", 1427), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/ext/dhtmlxscheduler_minical.js")
            
            #line default
            #line hidden
, 1427), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1545), Tuple.Create("\"", 1608)
            
            #line 35 "..\..\Views\BaseRestDay\list.cshtml"
, Tuple.Create(Tuple.Create("", 1551), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/locale_cn.js")
            
            #line default
            #line hidden
, 1551), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    window.onload = function () {
        var step = 15;
        var tempID = 0;
        var format = scheduler.date ;
        var isSaved = false;
        //scheduler.config.hour_size_px = (60 / step) * 22;

        //scheduler.config.time_step = 15;
        scheduler.config.details_on_create = false;
        scheduler.config.details_on_dblclick = true;
        scheduler.config.xml_date = ""%Y-%m-%d"";
        var datetimeformat = scheduler.date.date_to_str(""%Y-%m-%d"");


        scheduler.showLightbox = function (eventID) {
            isSaved = false;
            var params = ""?id="" + eventID  ;
            if (eventID < 0) {
                var event = scheduler.getEvent(eventID);
                if (event == null) {
                    return false;
                }
                params += ""&startTime="" + datetimeformat(event.start_date) + ""&endTime="" + datetimeformat(event.end_date)  ;
            }
            $.ajax({
                url: """);

            
            #line 62 "..\..\Views\BaseRestDay\list.cshtml"
                 Write(Url.Action("save", "BaseRestDay", new { @area= "Core" }));

            
            #line default
            #line hidden
WriteLiteral(@""" + params,
                type: ""POST"",
                success: function () {

                }
            });
            if (eventID < 0) {
                scheduler.deleteEvent(eventID);
            }
            scheduler.endLightbox(false);
            scheduler.clearAll();
            load();
            return false;
        }
        scheduler.attachEvent(""onSchedulerReady"", function () {
            scheduler.templates.event_bar_date = function (start, end, ev) {
                return ""• "";
            };
        });
        function load(){
            scheduler.load(""");

            
            #line 82 "..\..\Views\BaseRestDay\list.cshtml"
                        Write(Url.Action("GetData", "BaseRestDay", new { @area="Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?startTime="" + datetimeformat(scheduler._min_date) + ""&endTime="" + datetimeformat(scheduler._max_date), ""json"", function () {
                //加载数据完成的事件
            });
        }

        scheduler.attachEvent(""onViewChange"", function () {
            load();
        });

        scheduler.uid = function () {
            return --tempID;
        }
        scheduler.init(""scheduler_here"", new Date(""");

            
            #line 94 "..\..\Views\BaseRestDay\list.cshtml"
                                               Write(DateTime.Now.ToString("yyyy/MM/dd"));

            
            #line default
            #line hidden
WriteLiteral("\"), \"month\");\r\n    }\r\n\r\n\r\n</script>");

        }
    }
}
#pragma warning restore 1591