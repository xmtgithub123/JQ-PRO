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
    
    #line 1 "..\..\Views\EmpManage\weeklist.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/EmpManage/weeklist.cshtml")]
    public partial class _Views_EmpManage_weeklist_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_EmpManage_weeklist_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<input");

WriteLiteral(" id=\"currentempid\"");

WriteAttribute("value", Tuple.Create(" value=\"", 40), Tuple.Create("\"", 69)
            
            #line 2 "..\..\Views\EmpManage\weeklist.cshtml"
, Tuple.Create(Tuple.Create("", 48), Tuple.Create<System.Object, System.Int32>(ViewBag.CurrentEmpID
            
            #line default
            #line hidden
, 48), false)
);

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(" />\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 4 "..\..\Views\EmpManage\weeklist.cshtml"
                 Write(Url.Action("weekjson", "EmpManage",new { @area="Engineering"}));

            
            #line default
            #line hidden
WriteLiteral("?EngID=");

            
            #line 4 "..\..\Views\EmpManage\weeklist.cshtml"
                                                                                       Write(Request["taskGroupId"]);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n           addUrl = \'");

            
            #line 5 "..\..\Views\EmpManage\weeklist.cshtml"
                Write(Url.Action("weekadd", "EmpManage",new { @area= "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral("?EngID=");

            
            #line 5 "..\..\Views\EmpManage\weeklist.cshtml"
                                                                                       Write(Request["taskGroupId"]);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n\r\n    $(function () {\r\n        JQ.datagrid.datagrid({\r\n            JQButtonTy" +
"pes: [\'add\', \'export\'],//需要显示的按钮\r\n            JQAddUrl: addUrl, //添加的路径\r\n       " +
"     JQPrimaryID: \'Id\',//主键的字段\r\n            idField: \'Id\',\r\n            JQID: \'W" +
"eeklyGrid\',//数据表格ID\r\n            JQDialogTitle: \'信息\',//弹出窗体标题\r\n            JQWid" +
"th: \'768\',//弹出窗体宽\r\n            JQHeight: \'100%\',//弹出窗体高\r\n            JQLoadingTy" +
"pe: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n            JQExcludeCol: [7, 8],//" +
"导出execl排除的列\r\n            JQSearch: {\r\n                id: \'JQSearch\',\r\n         " +
"       prompt: \'周报标题\',\r\n                loadingType: \'datagrid\',//默认值为datagrid可以" +
"不传\r\n            },\r\n            url: requestUrl,//请求的地址\r\n            checkOnSele" +
"ct: true,//是否包含check\r\n            fitColumns: false,\r\n            nowrap: false," +
"\r\n            toolbar: \'#WeeklyPanel\',//工具栏的容器ID\r\n            columns: [[\r\n     " +
"           { title: \'标题\', field: \'Title\', width: 120, align: \'center\', sortable:" +
" true },\r\n                {\r\n                    title: \'内容\', field: \'Detail\', w" +
"idth: 250, align: \'center\', sortable: true, formatter: function (value, row, ind" +
"ex) {\r\n                        return \'<div style=\"text-align:left;\">\' + value +" +
" \'</div>\';\r\n                    }\r\n                },\r\n                { title: " +
"\'开始日期\', field: \'StartTime\', width: 120, align: \'center\', sortable: true },\r\n    " +
"            { title: \'结束日期\', field: \'EndTime\', width: 120, align: \'center\', sort" +
"able: true },\r\n                { title: \'创建人\', field: \'CreateEmpName\', width: 12" +
"0, align: \'center\', sortable: true },\r\n                { title: \'创建时间\', field: \'" +
"CreateTime\', width: 120, align: \'center\', sortable: true },\r\n                { f" +
"ield: \'JQFiles\', title: \'附件\', align: \'center\', width: 80, JQIsExclude: true, JQR" +
"efTable: \'Weekly\' },\r\n                {\r\n                    title: \'操作\', field:" +
" \'Id\', width: 80, align: \'center\', sortable: true, formatter: function (value, r" +
"ow, index) {\r\n                        var html = \'\';\r\n\r\n                        " +
"if (row.CreateEmpId == $(\'#currentempid\').val()) {\r\n                            " +
"html += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"weeke" +
"dit(\' + row.Id + \',\' + row.TaskGroupId + \')\">修改</a>\';\r\n                         " +
"   html += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;\" onclick=\"we" +
"ekdel(\' + row.Id + \')\">删除</a>\';\r\n                        } else {\r\n             " +
"               html += \'<a class=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;c" +
"olor: gray;\" disabled=\"true\">修改</a>\';\r\n                            html += \'<a c" +
"lass=\"easyui-linkbutton\" style=\"margin:0 5px 0 5px;color: gray;\" disabled=\"true\"" +
">删除</a>\';\r\n                        }\r\n\r\n                        return html;\r\n  " +
"                  }\r\n                }\r\n            ]]\r\n        });\r\n    });\r\n\r\n" +
"    function weekedit(id, taskgroupid) {\r\n        JQ.dialog.dialog({\r\n          " +
"  title: \'编辑信息\',\r\n            url: \'");

            
            #line 63 "..\..\Views\EmpManage\weeklist.cshtml"
             Write(Url.Action("weekedit", "EmpManage", new { @area = "engineering" }));

            
            #line default
            #line hidden
WriteLiteral("?id=\' + id + \'&EngID=");

            
            #line 63 "..\..\Views\EmpManage\weeklist.cshtml"
                                                                                                     Write(Request["taskGroupId"]);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            width: \'768\',\r\n            height: \'100%\',\r\n            JQID: \'Pr" +
"ojTable\',\r\n            JQLoadingType: \'datagrid\',\r\n            iconCls: \'fa fa-l" +
"ist\',\r\n        });\r\n    }\r\n\r\n    function weekdel(id) {\r\n        $.post(\'");

            
            #line 73 "..\..\Views\EmpManage\weeklist.cshtml"
           Write(Url.Action("weekdel", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"', { id: id }, function (result) {
            $('#WeeklyGrid').datagrid('reload');
            if (result.stateValue > 0) {
                JQ.dialog.show('操作成功');
            } else {
                JQ.dialog.show('操作失败');
            }
        });
    }

    function _WeekCallBack() {
        $('#WeeklyGrid').datagrid('reload');
    }
</script>
<table");

WriteLiteral(" id=\"WeeklyGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"WeeklyPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n    </span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591