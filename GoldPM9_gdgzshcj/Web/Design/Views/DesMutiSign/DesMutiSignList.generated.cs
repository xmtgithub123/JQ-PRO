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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesMutiSign/DesMutiSignList.cshtml")]
    public partial class _Views_DesMutiSign_DesMutiSignList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesMutiSign_DesMutiSignList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n</script>\r\n\r\n<div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    ");

WriteLiteral("\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"TaskCheckListDiv\"");

WriteLiteral(" style=\"width:100%;height:100%;\"");

WriteLiteral(">\r\n            <table");

WriteAttribute("id", Tuple.Create(" id=\"", 452), Tuple.Create("\"", 495)
, Tuple.Create(Tuple.Create("", 457), Tuple.Create("TaskDesMutiSignList_", 457), true)
            
            #line 12 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
, Tuple.Create(Tuple.Create("", 477), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 477), false)
);

WriteLiteral("></table>\r\n            <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
                $(function () {
                    window.showDesMutiSignGrid = showDesMutiSignGrid;

                    showDesMutiSignGrid();
                })

                function showDesMutiSignGrid() {
                    JQ.datagrid.datagrid({
                        JQPrimaryID: 'Id',
                        idField: 'Id',//主键的字段
                        JQID: 'TaskDesMutiSignList_");

            
            #line 24 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                                              Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\',//数据表格ID\r\n                        JQLoadingType: \'datagrid\',//数据表格类型 [datagrid," +
"datagrid]\r\n                        JQFields: [\"Id\"],//追加的字段名\r\n                  " +
"      url: \'");

            
            #line 27 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                         Write(Url.Action("json"));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                        queryParams: { taskId: \'");

            
            #line 28 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                                           Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral("\' },\r\n                        pagination: false,\r\n                        rownumb" +
"ers: true,\r\n                        fit: true,\r\n                        JQMergeC" +
"ells: { fields: [\'MutiSignTitle\', \'JQFiles\', \'CreationTime\', \'statusName\', \'Id\']" +
", Only: \'Id\' },\r\n                        columns: [[\r\n                          " +
"  { title: \'会签标题\', field: \'MutiSignTitle\', width: 200, align: \'center\', sortable" +
": false },\r\n\r\n                              {\r\n                                 " +
" field: \'JQFiles\', title: \'会签文件\', align: \'center\', width: 80, JQIsExclude: true," +
" JQExcludeFlag: \"grid_files\", JQRefTable: \'DesMutiSign\', formatter: function (va" +
"lue, row) {\r\n                                      return \"<span id=\\\"grid_files" +
"_\" + row.Id + \"\\\"></span>\"\r\n                                  }\r\n               " +
"               },\r\n                            { title: \'提出时间\', field: \'Creation" +
"Time\', width: 100, align: \'center\', sortable: false, formatter: JQ.tools.DateTim" +
"eFormatterByT },\r\n                            {\r\n                               " +
" title: \'会签状态\', field: \'statusName\', width: 100, align: \'center\', sortable: fals" +
"e, formatter: function (value, row, index) {\r\n                                  " +
"  return \"<a href=\'#\' onclick=\'showProgress(\" + row.ApproveFlowID + \")\' >\" + val" +
"ue + \"</a>\";\r\n                                }\r\n                            },\r" +
"\n                            {\r\n                                title: \'会签人\', fi" +
"eld: \'RecEmpName\', width: 120, align: \'center\', sortable: false, formatter: func" +
"tion (value, row, index) {\r\n                                    return \"<\" + row" +
".RecSpecName + \">\" + row.RecEmpName;\r\n                                }\r\n       " +
"                     },\r\n                            {\r\n                        " +
"        title: \'会签意见\', field: \'RecContent\', width: 100, align: \'center\', sortabl" +
"e: false, formatter: function (value, row, index) {\r\n                           " +
"         if (row.RecStatus > 0) {\r\n                                        retur" +
"n row.RecStatus == \"2\" ? \"【同意】\" : \"【不同意】\" + row.RecContent;\r\n                   " +
"                 }\r\n                                    else {\r\n                " +
"                        return \"\";\r\n                                    }\r\n     " +
"                           }\r\n                            },\r\n                  " +
"          { title: \'处理日期\', field: \'RecDealDate\', width: 100, align: \'center\', so" +
"rtable: false, formatter: JQ.tools.DateTimeFormatterByT },\r\n");

            
            #line 63 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                               

                                if (Request.QueryString["View"] ==null)
                                {

            
            #line default
            #line hidden
WriteLiteral("                                    ");

WriteLiteral("\r\n                                        {\r\n                                    " +
"        title: \'操作\', field: \'Id\', width: 80, align: \'center\', formatter: functio" +
"n (value, row, index) {\r\n                                                return " +
"\"<a");

WriteLiteral(" id=\\");

WriteLiteral("\"btnDel\\\" href=\\\"#\\\" rootID=\" + row.RecId + \" >删除</a>  \";\r\n                      " +
"                      }\r\n                                        }\r\n            " +
"                        ");

WriteLiteral("\r\n");

            
            #line 74 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                                }
                            
            
            #line default
            #line hidden
WriteLiteral(@"
                            
                        ]],
                        onLoadSuccess: function (data) {
                            //删除样式
                            $.each(data.rows, function (i, e) {
                                $(""a[rootID='"" + e.RecId + ""']"").linkbutton({
                                    iconCls: 'fa fa-trash',
                                    onClick: function () {
                                        deleteFunc(e.Id);
                                    }
                                })
                            });
                        }
                    });
                }

                function deleteFunc(Id) {
                    return JQ.dialog.confirm(""你确定要删除吗?"", function () {
                        JQ.tools.ajax({
                            url: '");

            
            #line 95 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                             Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"?id=' + Id,
                            succesCollBack: function () {
                                showDesMutiSignGrid();
                            }
                        });
                    })
                }

                function showProgress(flowID) {
                    debugger;
                    if (flowID == undefined || flowID == 0) {
                        JQ.dialog.show(""缺少关键流程数据，操作失败！！！"");
                        return false;
                    }
                    JQ.dialog.dialog({
                        title: '查看设计进度',
                        url: """);

            
            #line 111 "..\..\Views\DesMutiSign\DesMutiSignList.cshtml"
                          Write(Url.Action("Progress", "PubFlow", new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?flowID="" + flowID,
                        width: 800,
                        height: 600,
                        iconCls: ""fa fa-list""
                    });
                    JQ.Flow.stopBubble();
                }
            </script>
        </div>

    </div>
</div>");

        }
    }
}
#pragma warning restore 1591