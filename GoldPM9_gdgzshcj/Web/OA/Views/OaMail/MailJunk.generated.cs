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
    
    #line 4 "..\..\Views\OaMail\MailJunk.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaMail/MailJunk.cshtml")]
    public partial class _Views_OaMail_MailJunk_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaMail_MailJunk_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaMail\MailJunk.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\OaMail\MailJunk.cshtml"
                     Write(Url.Action("mailjunkjson", "OaMail",new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                " +
"JQButtonTypes: [ \'export\'],//需要显示的按钮\r\n                JQPrimaryID: \'Id\',//主键的字段\r" +
"\n                JQID: \'OaMailGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息" +
"\',//弹出窗体标题\r\n                JQWidth: \'768\',//弹出窗体宽\r\n                JQHeight: \'1" +
"00%\',//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treeg" +
"rid]\r\n                JQFields: [\"Id\"],//追加的字段名\r\n                JQExcludeCol: [" +
"0, 1, 2, 7],//导出execl排除的列\r\n                url: requestUrl,//请求的地址\r\n            " +
"    checkOnSelect: true,//是否包含check\r\n                fitColumns: true,\r\n        " +
"        nowrap: false,\r\n                toolbar: \'#OaMailPanel\',//工具栏的容器ID\r\n    " +
"            columns: [[\r\n                            { field: \'ck\', align: \'cent" +
"er\', checkbox: true, JQIsExclude: true },\r\n                            {\r\n      " +
"                          title: \'状态\', field: \'MailFlag\', width: 40, align: \'cen" +
"ter\', formatter: function (v) {\r\n                                    return \"<di" +
"v class=\'\" + v + \"\'/>\";\r\n                                }\r\n                    " +
"        },\r\n                            { title: \'收件人\', field: \'MailEmpName\', wi" +
"dth: 200, align: \'center\', sortable: true },\r\n\t\t                    { title: \'发送" +
"时间\', field: \'MailDate\', width: 100, align: \'center\', sortable: true, formatter: " +
"JQ.tools.DateTimeFormatterByT },\r\n                            {\r\n               " +
"                 field: \'MailTitle\', title: \'标题\', width: 300, align: \'center\',\r\n" +
"                                formatter: function (value, row, index) {\r\n     " +
"                               return \"<a href=\\\"javascript:Handler(\" + row.Id +" +
" \")\\\">\" + value + \"</a>\";\r\n                                }\r\n                  " +
"          },\r\n\t\t                    { title: \'内容\', field: \'MailNote\', width: 100" +
", align: \'center\', sortable: true },\r\n                            { field: \'JQFi" +
"les\', title: \'附件\', align: \'center\', width: 60, JQIsExclude: true, JQRefTable: \'O" +
"aMail\' }\r\n                ]]\r\n            });\r\n            $(\"#txtCondtion\").tex" +
"tbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-se" +
"arch\',\r\n                height: 25,\r\n                prompt: \'标题\',\r\n            " +
"    onClickButton: function () {\r\n                    JQ.datagrid.searchGrid(\r\n " +
"                       {\r\n                            dgID: \'OaMailGrid\',\r\n     " +
"                       loadingType: \'datagrid\',\r\n                            que" +
"ryParams: {\r\n                                txtCondtion: $(\'#txtCondtion\').text" +
"box(\'getValue\'),\r\n                                DateLower: $(\'#DateLower\').tex" +
"tbox(\'getValue\'),\r\n                                DateUpper: $(\'#DateUpper\').te" +
"xtbox(\'getValue\')\r\n                            }\r\n                        });\r\n " +
"               }\r\n            });\r\n        });\r\n\r\n        function ToUrl(TypeID)" +
" {\r\n            var row = $(\"#OaMailGrid\").datagrid(\"getSelections\");\r\n         " +
"   if (row.length <= 0) {\r\n                JQ.dialog.info(\"请选择\");\r\n             " +
"   return;\r\n            }\r\n            var MailID = row[0][\"Id\"];\r\n            /" +
"/debugger;\r\n            window.location.href = \'");

            
            #line 71 "..\..\Views\OaMail\MailJunk.cshtml"
                               Write(Url.Action("MailWrite", "OaMail",new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral(@"?MailID=' + MailID + ""&TypeID="" + TypeID;
        }

        function del(DelType) {
            var row = $(""#OaMailGrid"").datagrid(""getSelections"");
            if (row.length <= 0) {
                JQ.dialog.info(""请选择"");
                return;
            }

            var parm;
            $.each(row, function (i, n) {
                var Url = """";
                if (n[""IsSend""] == 0) {
                    Url = '");

            
            #line 85 "..\..\Views\OaMail\MailJunk.cshtml"
                      Write(Url.Action("del", "OaMail", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("?DelType=true&id=\' + n[\"Id\"];\r\n                } else {\r\n                    Url " +
"= \'");

            
            #line 87 "..\..\Views\OaMail\MailJunk.cshtml"
                      Write(Url.Action("del", "OaMailRead", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral(@"?DelType=true&id=' + n[""Id""];
                }

                JQ.tools.ajax({
                    doBackResult: true,
                    url: Url
                });
            });
            $('#OaMailGrid').datagrid('reload');
        }
        function resume(DelType) {
            var row = $(""#OaMailGrid"").datagrid(""getSelections"");
            if (row.length <= 0) {
                JQ.dialog.info(""请选择"");
                return;
            }

            var parm;
            $.each(row, function (i, n) {
                var Url = """";
                if (n[""IsSend""] == 0) {
                    Url = '");

            
            #line 108 "..\..\Views\OaMail\MailJunk.cshtml"
                      Write(Url.Action("del", "OaMail", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("?IsResum=true&id=\' + n[\"Id\"];\r\n                } else {\r\n                    Url " +
"= \'");

            
            #line 110 "..\..\Views\OaMail\MailJunk.cshtml"
                      Write(Url.Action("del", "OaMailRead", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral(@"?IsResum=true&id=' + n[""Id""];
                }

                JQ.tools.ajax({
                    doBackResult: true,
                    url: Url
                });
            });
            $('#OaMailGrid').datagrid('reload');
        }

        function Handler(rowid) {
            JQ.dialog.dialog({
                title: ""查看"",
                url: '");

            
            #line 124 "..\..\Views\OaMail\MailJunk.cshtml"
                 Write(Url.Action("edit", "OaMail", new { @area= "Oa" }));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?ReceiveFlag=0&Id=' + rowid,
                width: '800',
                height: '600',
                JQID: 'OaMailGrid',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus'
            });
        }
    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"OaMailGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"OaMailPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-undo\'\"");

WriteLiteral(" onclick=\"ToUrl(1)\"");

WriteLiteral(">转发</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-envelope\'\"");

WriteLiteral(" onclick=\"resume(true)\"");

WriteLiteral(">恢复</a>\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-remove (alias)\'\"");

WriteLiteral(" onclick=\"del(true)\"");

WriteLiteral(">彻底删除</a>\r\n        </span>\r\n        <input");

WriteLiteral(" id=\"DateLower\"");

WriteLiteral(" name=\"DateLower\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'发送开始时间\'\"");

WriteLiteral(">\r\n        至\r\n        <input");

WriteLiteral(" id=\"DateUpper\"");

WriteLiteral(" name=\"DateUpper\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'发送结束时间\'\"");

WriteLiteral(">&nbsp;\r\n        <input");

WriteLiteral(" id=\"txtCondtion\"");

WriteLiteral(" name=\"txtCondtion\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
