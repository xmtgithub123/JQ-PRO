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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Scheduler/Index.cshtml")]
    public partial class _Views_Scheduler_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Scheduler_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<html>\r\n<head>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 25), Tuple.Create("\"", 96)
            
            #line 3 "..\..\Views\Scheduler\Index.cshtml"
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

WriteLiteral("></div>\r\n            <div");

WriteLiteral(" class=\"dhx_minical_icon\"");

WriteLiteral(" id=\"dhx_minical_icon\"");

WriteLiteral(" onclick=\"show_minical()\"");

WriteLiteral(">&nbsp;</div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_tab\"");

WriteLiteral(" name=\"day_tab\"");

WriteLiteral(" style=\"right:204px;\"");

WriteLiteral("></div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_tab\"");

WriteLiteral(" name=\"week_tab\"");

WriteLiteral(" style=\"right:140px;\"");

WriteLiteral("></div>\r\n            <div");

WriteLiteral(" class=\"dhx_cal_tab\"");

WriteLiteral(" name=\"month_tab\"");

WriteLiteral(" style=\"right:76px;\"");

WriteLiteral("></div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dhx_cal_header\"");

WriteLiteral(">\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"dhx_cal_data\"");

WriteLiteral(">\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1215), Tuple.Create("\"", 1262)
            
            #line 32 "..\..\Views\Scheduler\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1221), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jquery.min.js")
            
            #line default
            #line hidden
, 1221), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1305), Tuple.Create("\"", 1374)
            
            #line 33 "..\..\Views\Scheduler\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1311), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/dhtmlxscheduler.js")
            
            #line default
            #line hidden
, 1311), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1417), Tuple.Create("\"", 1498)
            
            #line 34 "..\..\Views\Scheduler\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1423), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/ext/dhtmlxscheduler_minical.js")
            
            #line default
            #line hidden
, 1423), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1541), Tuple.Create("\"", 1604)
            
            #line 35 "..\..\Views\Scheduler\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1547), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/scheduler/locale_cn.js")
            
            #line default
            #line hidden
, 1547), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    window.onload = function () {\r\n        var step = 15;\r\n        var tempID " +
"= 0;\r\n        var format = scheduler.date.date_to_str(\"%H:%i\");\r\n        var isS" +
"aved = false;\r\n        scheduler.config.hour_size_px = (60 / step) * 22;\r\n      " +
"  scheduler.templates.hour_scale = function (date) {\r\n            html = \"\";\r\n  " +
"          var hour = date.getHours();\r\n            for (var i = 0; i < 60 / step" +
"; i++) {\r\n                if (hour < 8 || hour > 17) {\r\n                    html" +
" += \"<div style=\'height:22px;line-height:22px;background-color:#EFEFEF\'>\" + form" +
"at(date) + \"</div>\";\r\n                }\r\n                else {\r\n               " +
"     html += \"<div style=\'height:22px;line-height:22px;\'>\" + format(date) + \"</d" +
"iv>\";\r\n                }\r\n                date = scheduler.date.add(date, step, " +
"\"minute\");\r\n            }\r\n            return html;\r\n        }\r\n        schedule" +
"r.config.time_step = 15;\r\n        scheduler.config.details_on_create = true;\r\n  " +
"      scheduler.config.details_on_dblclick = true;\r\n        scheduler.config.xml" +
"_date = \"%Y-%m-%d %H:%i\";\r\n        var datetimeformat = scheduler.date.date_to_s" +
"tr(\"%Y-%m-%d %H:%i\");\r\n        scheduler.attachEvent(\"onEventCreated\", function " +
"(eventID) {\r\n            var event = scheduler.getEvent(eventID);\r\n            i" +
"f (event == null || eventID > 0) {\r\n                return false;\r\n            }" +
"\r\n            scheduler.setEvent(eventID, {\r\n                start_date: event.s" +
"tart_date,\r\n                end_date: scheduler.date.add(event.start_date, 30, \"" +
"minute\"),\r\n                text: event.text\r\n            });\r\n            schedu" +
"ler.updateEvent(eventID);\r\n        });\r\n        ");

WriteLiteral("\r\n        scheduler.showLightbox = function (eventID) {\r\n            isSaved = fa" +
"lse;\r\n            var params = \"?id=\" + eventID + \"&parentID=");

            
            #line 100 "..\..\Views\Scheduler\Index.cshtml"
                                                   Write(Request.QueryString["a1b2c3wjpid"]);

            
            #line default
            #line hidden
WriteLiteral(@""";
            var isNew = scheduler.getUserData(eventID, ""isNew"");
            if (eventID < 0) {
                var event = scheduler.getEvent(eventID);
                if (event == null) {
                    return false;
                }
                params += ""&startTime="" + datetimeformat(event.start_date) + ""&endTime="" + datetimeformat(event.end_date) + ""&content="" + encodeURIComponent(event.text);
            }
            window.top.JQ.dialog.dialog({
                title: (eventID < 0 ? ""日程 - 新建"" : ""日程 - 编辑""),
                height: 500,
                width: 600,
                url: """);

            
            #line 113 "..\..\Views\Scheduler\Index.cshtml"
                  Write(Url.Action("Edit", "Scheduler",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral(@""" + params,
                onClose: function (datas) {
                    //如果是新加的，则去掉
                    if (eventID < 0) {
                        scheduler.deleteEvent(eventID);
                    }
                    if (!isSaved) {
                        scheduler.endLightbox(false);
                    }
                }
            });
            return false;
        }
        scheduler.attachEvent(""onViewChange"", function () {
            $(""#scheduler_here>div.dhx_cal_data"")[0].scrollTop = 700;
            scheduler.load(""");

            
            #line 128 "..\..\Views\Scheduler\Index.cshtml"
                        Write(Url.Action("GetData", "Scheduler",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral(@"?startTime="" + datetimeformat(scheduler._min_date) + ""&endTime="" + datetimeformat(scheduler._max_date), ""json"", function () {
                //加载数据完成的事件
            });
        });
        scheduler.templates.event_text = function (start, end, event) {
            var texts = event.text.split(""\n"");
            var div = document.createElement(""div"");
            for (var i in texts) {
                var p = document.createElement(""p"");
                p.style.margin = ""1px 0px 1px 0px"";
                p.appendChild(document.createTextNode(texts[i]));
                div.appendChild(p);
            }
            return div.innerHTML;
        }
        scheduler.attachEvent(""onEventChanged"", function (id, event) {
            $.ajax({
                url: """);

            
            #line 145 "..\..\Views\Scheduler\Index.cshtml"
                 Write(Url.Action("SaveData","Scheduler",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral(@""",
                type: ""POST"",
                data: { id: event.id, startTime: datetimeformat(event.start_date), endTime: datetimeformat(event.end_date), content: event.text },
                success: function () {
                }
            });
        });
        scheduler.attachEvent(""onEventDeleted"", function (id) {
            deleteSchedulerData(id);
        });
        scheduler.uid = function () {
            return --tempID;
        }
        scheduler.init(""scheduler_here"", new Date(""");

            
            #line 158 "..\..\Views\Scheduler\Index.cshtml"
                                               Write(DateTime.Now.ToString("yyyy/MM/dd"));

            
            #line default
            #line hidden
WriteLiteral(@"""), ""week"");
    }

    var afterSchedulerSave = function (data, originalEventID) {
        if (!data) {
            return;
        }
        isSaved = true;
        if (originalEventID <= 0) {
            scheduler.changeEventId(originalEventID, data.ID);
        }
        //setTimeout(function () {
        //    alert(1);
        //    scheduler.addEvent({
        //        start_date: ""25-08-2016 09:00"",
        //        end_date: ""26-08-2016 12:00"",
        //        text: ""Meeting"",
        //    });
        //}, 5000);
        scheduler.setEvent(data.ID, {
            start_date: data.start_date,
            end_date: data.end_date,
            text: data.Content
        });
        scheduler.updateEvent(data.ID);
    }

    var deleteSchedulerData = function (eventID, isRemove) {
        if (eventID <= 0) {
            return;
        }
        $.ajax({
            url: """);

            
            #line 190 "..\..\Views\Scheduler\Index.cshtml"
             Write(Url.Action("DeleteData","Scheduler",new { @area="OA" }));

            
            #line default
            #line hidden
WriteLiteral(@""",
            type: ""POST"",
            data: { id: eventID },
            async: false,
            success: function () {
                if (isRemove) {
                    scheduler.deleteEvent(eventID);
                }
            }
        });
        scheduler.endLightbox(false);
    }

    function show_minical() {
        if (scheduler.isCalendarVisible())
            scheduler.destroyCalendar();
        else
            scheduler.renderCalendar({
                position: ""dhx_minical_icon"",
                date: scheduler._date,
                navigation: true,
                handler: function (date, calendar) {
                    scheduler.setCurrentView(date);
                    scheduler.destroyCalendar()
                }
            });
    }
</script>");

        }
    }
}
#pragma warning restore 1591
