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
    
    #line 2 "..\..\Views\DesTask\EditTask.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/EditTask.cshtml")]
    public partial class _Views_DesTask_EditTask_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.DesTaskGantt>
    {
        public _Views_DesTask_EditTask_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

    JQ.form.submitInit({
        formid: 'SaveDesTaskGanttForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            window.top.execute(""个人中心"", ""refreshtaskGantt"");
            JQ.dialog.dialogClose();
            try
            {
                window.top.refresh();
            }
            catch(e){
            }
        }
    });

    $(""#bit"").val(");

            
            #line 21 "..\..\Views\DesTask\EditTask.cshtml"
             Write(ViewBag.bit);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    $(\"#Duration\").attr(\"readonly\", \"readonly\");\r\n    function onSelect(date)" +
" {  \r\n        var dt1 = $(\"#ParentDatePlanStart\").datebox(\"getValue\");\r\n        " +
"var dt2 = $(\"#ParentDatePlanFinish\").datebox(\"getValue\");\r\n\r\n        var dt3 = $" +
"(\"#DatePlanStart\").datebox(\"getValue\");\r\n        var dt4 = $(\"#DatePlanFinish\")." +
"datebox(\"getValue\");\r\n\r\n        if (dt3 != \"\" && dt1 != \"\") {\r\n            var t" +
"1 = parser(dt1);\r\n            var t3 = parser(dt3);\r\n\r\n            if (t1 > t3) " +
"{\r\n                $(\"#DatePlanStart\").datebox(\"setValue\", \"\");\r\n               " +
" JQ.dialog.error(\"关键节点的开始时间不能早于父节点的开始时间！\");\r\n                return false;\r\n    " +
"        }\r\n        }\r\n\r\n        if (dt3 != \"\" && dt2 != \"\") {\r\n            var t" +
"3 = parser(dt3);\r\n            var t2 = parser(dt2);\r\n\r\n            if (t3 > t2) " +
"{\r\n\r\n                $(\"#DatePlanStart\").datebox(\"setValue\", \"\");\r\n             " +
"   JQ.dialog.error(\"关键节点的开始时间不能晚于父节点的结束时间！\");\r\n                return false;\r\n  " +
"          }\r\n        }\r\n\r\n        if (dt4 != \"\" && dt2 != \"\") {\r\n            var" +
" t2 = parser(dt2);\r\n            var t4 = parser(dt4);\r\n\r\n            if (t2 < t4" +
") {\r\n\r\n                $(\"#DatePlanFinish\").datebox(\"setValue\", \"\");\r\n          " +
"      JQ.dialog.error(\"关键节点的结束时间不能晚于父节点的结束时间！\");\r\n                return false;\r" +
"\n            }\r\n        }\r\n\r\n        if (dt4 != \"\" && dt1 != \"\") {\r\n            " +
"var t4 = parser(dt4);\r\n            var t1 = parser(dt1);\r\n\r\n            if (t4 <" +
" t1) {\r\n\r\n                $(\"#DatePlanFinish\").datebox(\"setValue\", \"\");\r\n       " +
"         JQ.dialog.error(\"关键节点的结束时间不能早于父节点的开始时间！\");\r\n                return fals" +
"e;\r\n            }\r\n        }\r\n\r\n\r\n        if (dt3 != \"\" && dt4 != \"\") {\r\n       " +
"     var t3 = parser(dt3);\r\n            var t4 = parser(dt4);\r\n            if (t" +
"3 <= t4) {\r\n                var result = calculateDays(t3, t4);\r\n               " +
" // debugger;\r\n                $(\"#Duration\").numberbox(\'setValue\', result );\r\n " +
"           }\r\n            else {\r\n                JQ.dialog.error(\"开始时间不能大于完成时间！" +
"\");\r\n                return false;\r\n            }\r\n        }\r\n    }\r\n    //funct" +
"ion calculateDays(dateBegin, dateEnd) {\r\n    //    var sDate = new Date(dateBegi" +
"n);\r\n    //    var eDate = new Date(dateEnd);\r\n    //    var weekToDay = eDate.g" +
"etDay();\r\n         \r\n        \r\n    //    var fen = ((eDate.getTime() - sDate.get" +
"Time()) / 1000) / 60;\r\n    //    var distance = parseInt(fen / (24 * 60)); //相隔d" +
"istance天\r\n    //    return distance;\r\n    //};\r\n\r\n    function calculateDays(dat" +
"eBegin, dateEnd) {\r\n        var sDate = new Date(dateBegin);\r\n        var eDate " +
"= new Date(dateEnd);\r\n        var newSDate ;\r\n        var newEDate ;\r\n        de" +
"bugger;\r\n        //开始时间判断\r\n        var weekToDay = sDate.getDay();\r\n        if(w" +
"eekToDay==6)\r\n        {\r\n            newSDate= new Date( addDate(sDate,2));\r\n   " +
"        \r\n        }\r\n        else if(weekToDay==0)\r\n        {\r\n            newSD" +
"ate= new Date( addDate(sDate,1));\r\n           \r\n        }\r\n        else\r\n       " +
" {\r\n            newSDate=sDate;\r\n        }\r\n        //结束时间判断\r\n        weekToDay " +
"= eDate.getDay();\r\n        if(weekToDay==6)\r\n        {\r\n            newEDate= ne" +
"w Date( addDate(eDate,-1));\r\n           \r\n        }\r\n        else if(weekToDay==" +
"0)\r\n        {\r\n            newEDate= new Date( addDate(eDate,-2));\r\n           \r" +
"\n        }\r\n        else\r\n        {\r\n            newEDate=eDate;\r\n        }\r\n\r\n " +
"       var pos = new Date(newEDate);\r\n        newEDate.setHours(23, 59, 59, 999)" +
";\r\n        newSDate.setHours(23, 59, 59, 999);\r\n     //   alert(newEDate);  aler" +
"t(newSDate);\r\n        var fen = ((newEDate.getTime() - newSDate.getTime()) / 100" +
"0) / 60;\r\n        var distance = parseInt(fen / (24 * 60)); //相隔distance天\r\n     " +
"    \r\n        return distance+1;\r\n    };\r\n\r\n\r\n    function addDate(date,days){ \r" +
"\n        var d=new Date(date); \r\n        d.setDate(d.getDate()+days); \r\n        " +
"var m=d.getMonth()+1; \r\n        return d.getFullYear()+\'-\'+m+\'-\'+d.getDate(); \r\n" +
"    } \r\n\r\n    JQ.combotree.selEmp({ id: \'ManageEmpId\', width: \"98%\" });\r\n\r\n    $" +
"(function(){\r\n        JQ.Flow.processControl.call(document.getElementById(\"Paren" +
"tDatePlanStart\"),\"DatePlanStart\",false);\r\n        JQ.Flow.processControl.call(do" +
"cument.getElementById(\"ParentDatePlanFinish\"),\"DatePlanStart\",false);\r\n\r\n       " +
" if(");

            
            #line 166 "..\..\Views\DesTask\EditTask.cshtml"
       Write(ViewBag.bit);

            
            #line default
            #line hidden
WriteLiteral(@"==3)
        {
            setTimeout(function(){
                JQ.Flow.processControl.call(document.getElementById(""ParentDatePlanStart""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""ParentDatePlanFinish""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""DatePlanStart""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""DatePlanFinish""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""name""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""Duration""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""KeyPointType""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""ManageEmpId""),""DatePlanStart"",false);

            },100);
        }
        else{
            setTimeout(function(){
                JQ.Flow.processControl.call(document.getElementById(""ParentDatePlanStart""),""DatePlanStart"",false);
                JQ.Flow.processControl.call(document.getElementById(""ParentDatePlanFinish""),""DatePlanStart"",false);

            },100);
        }
    });

    if(");

            
            #line 189 "..\..\Views\DesTask\EditTask.cshtml"
  Write(ViewData["ManageEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("!=0&&");

            
            #line 189 "..\..\Views\DesTask\EditTask.cshtml"
                                Write(ViewData["ManageEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral("!=0)\r\n    {\r\n        $(\"#ManageEmpId\").combotree(\'setValue\',");

            
            #line 191 "..\..\Views\DesTask\EditTask.cshtml"
                                          Write(ViewData["ManageEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("+\"-\"+ ");

            
            #line 191 "..\..\Views\DesTask\EditTask.cshtml"
                                                                         Write(ViewData["ManageEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral(@");
    }

    function parser(s) 
    {
        if (!s) return new Date();
        var ss = s.split('-');
        var y = parseInt(ss[0], 10);
        var m = parseInt(ss[1], 10);
        var d = parseInt(ss[2], 10);
        if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
            return new Date(y, m - 1, d);
        } else {
            return new Date();
        }
    }
</script>

");

            
            #line 209 "..\..\Views\DesTask\EditTask.cshtml"
 using (Html.BeginForm("SaveTask", "DesTask", FormMethod.Post, new { @area = "Design", @id = "SaveDesTaskGanttForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 211 "..\..\Views\DesTask\EditTask.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 211 "..\..\Views\DesTask\EditTask.cshtml"
                              


            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">父节点开始时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ParentDatePlanStart\"");

WriteLiteral(" id=\"ParentDatePlanStart\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"editable: false\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7125), Tuple.Create("\"", 7161)
            
            #line 217 "..\..\Views\DesTask\EditTask.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 7133), Tuple.Create<System.Object, System.Int32>(ViewBag.ParentDatePlanStart
            
            #line default
            #line hidden
, 7133), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 7406), Tuple.Create("\"", 7443)
            
            #line 221 "..\..\Views\DesTask\EditTask.cshtml"
                                                                            , Tuple.Create(Tuple.Create("", 7414), Tuple.Create<System.Object, System.Int32>(ViewBag.ParentDatePlanFinish
            
            #line default
            #line hidden
, 7414), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">任务名称</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"name\"");

WriteLiteral(" name=\"name\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7708), Tuple.Create("\"", 7727)
            
            #line 227 "..\..\Views\DesTask\EditTask.cshtml"
                                                                      , Tuple.Create(Tuple.Create("", 7716), Tuple.Create<System.Object, System.Int32>(Model.Name
            
            #line default
            #line hidden
, 7716), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">相关责任人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ManageEmpId\"");

WriteLiteral(" name=\"ManageEmpId\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 7910), Tuple.Create("\"", 7938)
            
            #line 233 "..\..\Views\DesTask\EditTask.cshtml"
, Tuple.Create(Tuple.Create("", 7920), Tuple.Create<System.Object, System.Int32>(Model.ManageEmpId
            
            #line default
            #line hidden
, 7920), false)
);

WriteLiteral(" data-options=\"required:true,editable: false\"");

WriteLiteral("></select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">关键节点</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 237 "..\..\Views\DesTask\EditTask.cshtml"
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

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral(" data-options=\"onSelect:onSelect,required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8675), Tuple.Create("\"", 8703)
            
            #line 250 "..\..\Views\DesTask\EditTask.cshtml"
                                                                                                                       , Tuple.Create(Tuple.Create("", 8683), Tuple.Create<System.Object, System.Int32>(Model.DatePlanStart
            
            #line default
            #line hidden
, 8683), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 8970), Tuple.Create("\"", 8999)
            
            #line 254 "..\..\Views\DesTask\EditTask.cshtml"
                                                                                                     , Tuple.Create(Tuple.Create("", 8978), Tuple.Create<System.Object, System.Int32>(Model.DatePlanFinish
            
            #line default
            #line hidden
, 8978), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 9251), Tuple.Create("\"", 9274)
            
            #line 260 "..\..\Views\DesTask\EditTask.cshtml"
                                                         , Tuple.Create(Tuple.Create("", 9259), Tuple.Create<System.Object, System.Int32>(Model.Duration
            
            #line default
            #line hidden
, 9259), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">完成比例</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Progress\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"min:0,max:100,precision:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9491), Tuple.Create("\"", 9514)
            
            #line 264 "..\..\Views\DesTask\EditTask.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 9499), Tuple.Create<System.Object, System.Int32>(Model.Progress
            
            #line default
            #line hidden
, 9499), false)
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

            
            #line 271 "..\..\Views\DesTask\EditTask.cshtml"
                                                                                                                                                       Write(Model.Description);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n  " +
"              上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 279 "..\..\Views\DesTask\EditTask.cshtml"
                
            
            #line default
            #line hidden
            
            #line 279 "..\..\Views\DesTask\EditTask.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "DesTaskGantt";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 284 "..\..\Views\DesTask\EditTask.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 284 "..\..\Views\DesTask\EditTask.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"bit\"");

WriteLiteral(" name=\"bit\"");

WriteLiteral(" />\r\n");

            
            #line 290 "..\..\Views\DesTask\EditTask.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
