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
    
    #line 4 "..\..\Views\IsoSJTJTGD\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoSJTJTGD/list.cshtml")]
    public partial class _Views_IsoSJTJTGD_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoSJTJTGD_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoSJTJTGD\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoSJTJTGD\list.cshtml"
               Write(Url.Action("json", "IsoSJTJTGD",new { @area="Iso"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\IsoSJTJTGD\list.cshtml"
                 Write(Url.Action("add","IsoSJTJTGD",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\IsoSJTJTGD\list.cshtml"
                  Write(Url.Action("edit", "IsoSJTJTGD",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\IsoSJTJTGD\list.cshtml"
                 Write(Url.Action("del", "IsoSJTJTGD", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\',\'del\', \'export\'],//需要显示的按钮\r\n                JQAddU" +
"rl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n               " +
" JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n         " +
"       JQID: \'IsoSJTJTGDGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹出" +
"窗体标题\r\n                JQWidth: \'1024\',//弹出窗体宽\r\n                JQHeight: \'100%\'," +
"//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r" +
"\n                JQFields: [\"Id\"],//追加的字段名\r\n\t\t\t\tJQSearch: {\r\n                   " +
" id: \'JQSearch\',\r\n                    prompt: \'项目编号、项目名称\',\r\n                    " +
"loadingType: \'datagrid\',//默认值为datagrid可以不传\r\n                },\r\n                " +
"url: requestUrl,//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n      " +
"          fitColumns: false,\r\n                JQExcludeCol: [1,10,11,12],//导出exe" +
"cl排除的列\r\n                nowrap: false,\r\n                toolbar: \'#IsoSJTJTGDPan" +
"el\',//工具栏的容器ID\r\n                columns: [[\r\n                    { field: \'ck\', " +
"align: \'center\', checkbox: true, JQIsExclude: true },\r\n                    { tit" +
"le: \'项目编号\', field: \'ProjNumber\', width: 100, align: \'center\', sortable: true  }," +
"\r\n                    { title: \'项目名称\', field: \'ProjName\', width: 300, align: \'ce" +
"nter\', sortable: true  },\r\n                    { title: \'提供专业\', field: \'TGSpecNa" +
"me\', width: 100, align: \'center\', sortable: true  },\r\n                    { titl" +
"e: \'提供专业负责人\', field: \'TGSpecHeader\', width: 100, align: \'center\', sortable: true" +
"  },\r\n                    { title: \'提供日期\', field: \'TGTime\', width: 100, align: \'" +
"center\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n         " +
"           { title: \'接受专业\', field: \'JSSpecName\', width: 100, align: \'center\', so" +
"rtable: true  },\r\n                    { title: \'接受专业负责人\', field: \'JSSpecHeader\'," +
" width: 100, align: \'center\', sortable: true  },\r\n                    { title: \'" +
"接受日期\', field: \'JSTime\', width: 100, align: \'center\', sortable: true , formatter:" +
" JQ.tools.DateTimeFormatterByT },\r\n                    {\r\n                      " +
"  field: \"FlowProgress\", title: \"流程进度\", align: \"left\", halign: \"center\", width: " +
"\"8%\", formatter: JQ.Flow.showProgress(\"FlowID\", \"FlowName\", \"FlowStatusID\", \"Flo" +
"wStatusName\", \"FlowTurnedEmpIDs\", \"CreatorEmpId\", \"");

            
            #line 46 "..\..\Views\IsoSJTJTGD\list.cshtml"
                                                                                                                                                                                                                                     Write(ViewBag.CurrEmpID);

            
            #line default
            #line hidden
WriteLiteral(@""")
                    },
                    { field: 'JQFiles', title: '附件', align: 'center', width: 80, JQIsExclude: true, JQRefTable: 'IsoSJTJTGD' },
                    {
                        field: 'opt', title: '操作', width: ""40"", align: 'center', JQIsExclude: true,
                        formatter: function (value, row, index) {
                            var title = ""查看"";
                            if (row._EditStatus == 1) {
                                title = ""修改"";
                            }
                            else if (row._EditStatus == 2) {
                                title = ""处理"";
                            }
                            return ""<a class='easyui-linkbutton' onclick='Look("" + row.Id + "")'>"" + title + ""</a>"";
                        }
                    }
                ]]
            });
        });

        function Look(val) {
            JQ.dialog.dialog({
                title: ""设计条件提供单"",
                url: editUrl + ""?Id="" + val,
                width: '1000',
                height: '100%',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-search',
                onClose: function () {
                    $(""#IsoSJTJTGDGrid"").datagrid(""reload"");
                }
            });
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoSJTJTGDGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoSJTJTGDPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral("  JQWhereOptions=\"{ \'Key\': \'ProjNumber,ProjName\', \'Contract\': \'like\' }\"");

WriteLiteral("/>\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591