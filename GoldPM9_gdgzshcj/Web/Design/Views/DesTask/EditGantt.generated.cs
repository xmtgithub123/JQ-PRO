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
    
    #line 2 "..\..\Views\DesTask\EditGantt.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/EditGantt.cshtml")]
    public partial class _Views_DesTask_EditGantt_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.DesTaskGantt>
    {
        public _Views_DesTask_EditGantt_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteAttribute("src", Tuple.Create(" src=\"", 61), Tuple.Create("\"", 121)
            
            #line 3 "..\..\Views\DesTask\EditGantt.cshtml"
, Tuple.Create(Tuple.Create("", 67), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jQueryGantt/libs/i18nJs.js")
            
            #line default
            #line hidden
, 67), false)
);

WriteLiteral("></script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'SaveDesTaskGanttForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            window.top.refresh();
            JQ.dialog.dialogClose();
        }
    });
    $(""#projId"").val(");

            
            #line 13 "..\..\Views\DesTask\EditGantt.cshtml"
                Write(ViewBag.projid);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    $(\"#parentID\").val(");

            
            #line 14 "..\..\Views\DesTask\EditGantt.cshtml"
                  Write(ViewBag.parentID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n\r\n    $(\"#Duration\").attr(\"readonly\", \"readonly\");\r\n    $(\"#DatePlanStart\").a" +
"ttr(\"editable\", \"false\");\r\n    $(\"#DatePlanFinish\").attr(\"editable\", \"false\");\r\n" +
"\r\n    function onSelect(date) {\r\n        debugger;\r\n        var dt1 = $(\"#Parent" +
"DatePlanStart\").datebox(\"getValue\");\r\n        var dt2 = $(\"#ParentDatePlanFinish" +
"\").datebox(\"getValue\");\r\n\r\n        var dt3 = $(\"#DatePlanStart\").datebox(\"getVal" +
"ue\");\r\n        var dt4 = $(\"#DatePlanFinish\").datebox(\"getValue\");\r\n\r\n        if" +
" (dt3 != \"\" && dt1 != \"\") {\r\n            var t1 = parser(dt1);\r\n            var " +
"t3 = parser(dt3);\r\n\r\n            if (t1 > t3) {\r\n                $(\"#DatePlanSta" +
"rt\").datebox(\"setValue\", \"\");\r\n                JQ.dialog.error(\"关键节点的开始时间不能早于父节点" +
"的开始时间！\");\r\n                return false;\r\n            }\r\n        }\r\n\r\n        if" +
" (dt3 != \"\" && dt2 != \"\") {\r\n            var t3 = parser(dt3);\r\n            var " +
"t2 = parser(dt2);\r\n\r\n            if (t3 > t2) {\r\n\r\n                $(\"#DatePlanS" +
"tart\").datebox(\"setValue\", \"\");\r\n                JQ.dialog.error(\"关键节点的开始时间不能晚于父" +
"节点的结束时间！\");\r\n                return false;\r\n            }\r\n        }\r\n\r\n        " +
"if (dt4 != \"\" && dt2 != \"\") {\r\n            var t2 = parser(dt2);\r\n            va" +
"r t4 = parser(dt4);\r\n\r\n            if (t2 < t4) {\r\n\r\n                $(\"#DatePla" +
"nFinish\").datebox(\"setValue\", \"\");\r\n                JQ.dialog.error(\"关键节点的结束时间不能" +
"晚于父节点的结束时间！\");\r\n                return false;\r\n            }\r\n        }\r\n\r\n     " +
"   if (dt4 != \"\" && dt1 != \"\") {\r\n            var t4 = parser(dt4);\r\n           " +
" var t1 = parser(dt1);\r\n\r\n            if (t4 < t1) {\r\n\r\n                $(\"#Date" +
"PlanFinish\").datebox(\"setValue\", \"\");\r\n                JQ.dialog.error(\"关键节点的结束时" +
"间不能早于父节点的开始时间！\");\r\n                return false;\r\n            }\r\n        }\r\n\r\n\r\n" +
"        if (dt3 != \"\" && dt4 != \"\") {\r\n            var t3 = parser(dt3);\r\n      " +
"      var t4 = parser(dt4);\r\n            if (t3 <= t4) {\r\n                var re" +
"sult = calculateDays(t3, t4);\r\n               // debugger;\r\n                $(\"#" +
"Duration\").numberbox(\'setValue\', result );\r\n            }\r\n            else {\r\n " +
"               JQ.dialog.error(\"开始时间不能大于完成时间！\");\r\n                return false;\r" +
"\n            }\r\n        }\r\n    }\r\n    //function calculateDays(dateBegin, dateEn" +
"d) {\r\n    //    var sDate = new Date(dateBegin);\r\n    //    var eDate = new Date" +
"(dateEnd);\r\n\r\n    //    var weekToDay = sDate.getDate();\r\n    //    alert(weekTo" +
"Day);\r\n         \r\n\r\n    //    var distance = sDate.distanceInWorkingDays(eDate);" +
"\r\n    //    return distance;\r\n    //};\r\n\r\n    function calculateDays(dateBegin, " +
"dateEnd) {\r\n        var sDate = new Date(dateBegin);\r\n        var eDate = new Da" +
"te(dateEnd);\r\n        var newSDate;\r\n        var newEDate;\r\n        debugger;\r\n " +
"       //开始时间判断\r\n        var weekToDay = sDate.getDay();\r\n        if (weekToDay " +
"== 6) {\r\n            newSDate = new Date(addDate(sDate, 2));\r\n\r\n        }\r\n     " +
"   else if (weekToDay == 0) {\r\n            newSDate = new Date(addDate(sDate, 1)" +
");\r\n\r\n        }\r\n        else {\r\n            newSDate = sDate;\r\n        }\r\n     " +
"   //结束时间判断\r\n        weekToDay = eDate.getDay();\r\n        if (weekToDay == 6) {\r" +
"\n            newEDate = new Date(addDate(eDate, -1));\r\n          //  alert(newED" +
"ate);\r\n        }\r\n        else if (weekToDay == 0) {\r\n            newEDate = new" +
" Date(addDate(eDate, -2));\r\n\r\n        }\r\n        else {\r\n            newEDate = " +
"eDate;\r\n        }\r\n\r\n        //  alert(weekToDay);\r\n       \r\n        var fen = (" +
"(newEDate.getTime() - newSDate.getTime()) / 1000) / 60;\r\n        var distance = " +
"parseInt(fen / (24 * 60)); //相隔distance天\r\n        return distance+1;\r\n    };\r\n\r\n" +
"    function addDate(date, days) {\r\n        var d = new Date(date);\r\n        d.s" +
"etDate(d.getDate() + days);\r\n        var m = d.getMonth() + 1;\r\n        return d" +
".getFullYear() + \'-\' + m + \'-\' + d.getDate();\r\n    }\r\n\r\n    Date.prototype.dista" +
"nceInWorkingDays = function (toDate) {\r\n        var pos = new Date(this.getTime(" +
"));\r\n        pos.setHours(23, 59, 59, 999);\r\n        var days = 0;\r\n        var " +
"nd = new Date(toDate.getTime());\r\n        nd.setHours(23, 59, 59, 999);\r\n       " +
" var end = nd.getTime();\r\n        while (pos.getTime() <= end) {\r\n            da" +
"ys = days + (isHoliday(pos) ? 0 : 1);\r\n          //  days = days +   1 ;\r\n      " +
"      pos.setDate(pos.getDate() + 1);\r\n        }\r\n        return days;\r\n    };\r\n" +
"\r\n    JQ.combotree.selEmp({ id: \'ManageEmpId\', width: \"98%\" });\r\n    $(\"#ParentD" +
"atePlanStart\").attr(\"readonly\", \"readonly\");\r\n    $(\"#ParentDatePlanFinish\").att" +
"r(\"readonly\", \"readonly\");\r\n\r\n    function parser(s) {\r\n        if (!s) return n" +
"ew Date();\r\n        var ss = s.split(\'-\');\r\n        var y = parseInt(ss[0], 10);" +
"\r\n        var m = parseInt(ss[1], 10);\r\n        var d = parseInt(ss[2], 10);\r\n  " +
"      if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {\r\n            return new Date(y," +
" m - 1, d);\r\n        } else {\r\n            return new Date();\r\n        }\r\n    }\r" +
"\n</script>\r\n\r\n");

            
            #line 182 "..\..\Views\DesTask\EditGantt.cshtml"
 using (Html.BeginForm("SaveGantt", "DesTask", FormMethod.Post, new { @area = "Design", @id = "SaveDesTaskGanttForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 184 "..\..\Views\DesTask\EditGantt.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 184 "..\..\Views\DesTask\EditGantt.cshtml"
                              


            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">父节点开始时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ParentDatePlanStart\"");

WriteLiteral(" id=\"ParentDatePlanStart\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"editable: false\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5864), Tuple.Create("\"", 5900)
            
            #line 191 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 5872), Tuple.Create<System.Object, System.Int32>(ViewBag.ParentDatePlanStart
            
            #line default
            #line hidden
, 5872), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">父节点完成时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ParentDatePlanFinish\"");

WriteLiteral(" id=\"ParentDatePlanFinish\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"editable:false\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6145), Tuple.Create("\"", 6182)
            
            #line 195 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                            , Tuple.Create(Tuple.Create("", 6153), Tuple.Create<System.Object, System.Int32>(ViewBag.ParentDatePlanFinish
            
            #line default
            #line hidden
, 6153), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">任务名称</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"name\"");

WriteLiteral(" style=\"width:99%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6437), Tuple.Create("\"", 6456)
            
            #line 201 "..\..\Views\DesTask\EditGantt.cshtml"
                                                            , Tuple.Create(Tuple.Create("", 6445), Tuple.Create<System.Object, System.Int32>(Model.Name
            
            #line default
            #line hidden
, 6445), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">相关责任人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ManageEmpId\"");

WriteLiteral(" name=\"ManageEmpId\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 6641), Tuple.Create("\"", 6669)
            
            #line 208 "..\..\Views\DesTask\EditGantt.cshtml"
, Tuple.Create(Tuple.Create("", 6651), Tuple.Create<System.Object, System.Int32>(Model.ManageEmpId
            
            #line default
            #line hidden
, 6651), false)
);

WriteLiteral(" data-options=\"required:true,editable: false\"");

WriteLiteral("></select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">关键节点</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 212 "..\..\Views\DesTask\EditGantt.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "KeyPointType",
               engName = "KeyPointType",
               width = "98%",
               isRequired = true,
               ids = Model.KeyPointType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">开始时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DatePlanStart\"");

WriteLiteral(" id=\"DatePlanStart\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" data-options=\"onSelect:onSelect,required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7386), Tuple.Create("\"", 7414)
            
            #line 225 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                                                   , Tuple.Create(Tuple.Create("", 7394), Tuple.Create<System.Object, System.Int32>(Model.DatePlanStart
            
            #line default
            #line hidden
, 7394), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">完成时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DatePlanFinish\"");

WriteLiteral(" id=\"DatePlanFinish\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteLiteral(" data-options=\"onSelect:onSelect,required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7681), Tuple.Create("\"", 7710)
            
            #line 229 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                                                     , Tuple.Create(Tuple.Create("", 7689), Tuple.Create<System.Object, System.Int32>(Model.DatePlanFinish
            
            #line default
            #line hidden
, 7689), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">持续天数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Duration\"");

WriteLiteral(" id=\"Duration\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" data-options=\"min:0,precision:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7962), Tuple.Create("\"", 7985)
            
            #line 235 "..\..\Views\DesTask\EditGantt.cshtml"
                                                         , Tuple.Create(Tuple.Create("", 7970), Tuple.Create<System.Object, System.Int32>(Model.Duration
            
            #line default
            #line hidden
, 7970), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">完成比例</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Progress\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" min=\"0.01\"");

WriteLiteral(" value=\"1\"");

WriteLiteral(" max=\"100\"");

WriteLiteral(" precision=\"2\"");

WriteAttribute("value", Tuple.Create("  value=\"", 8206), Tuple.Create("\"", 8230)
            
            #line 239 "..\..\Views\DesTask\EditGantt.cshtml"
                                                        , Tuple.Create(Tuple.Create("", 8215), Tuple.Create<System.Object, System.Int32>(Model.Progress
            
            #line default
            #line hidden
, 8215), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">其他描述</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"Description\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 246 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                                                                                                       Write(Model.Description);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n  " +
"              上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 254 "..\..\Views\DesTask\EditGantt.cshtml"
                
            
            #line default
            #line hidden
            
            #line 254 "..\..\Views\DesTask\EditGantt.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "DesTaskGantt";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 259 "..\..\Views\DesTask\EditGantt.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 259 "..\..\Views\DesTask\EditGantt.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projId\"");

WriteLiteral(" name=\"projId\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"parentID\"");

WriteLiteral(" name=\"parentID\"");

WriteLiteral(" />\r\n");

            
            #line 266 "..\..\Views\DesTask\EditGantt.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591