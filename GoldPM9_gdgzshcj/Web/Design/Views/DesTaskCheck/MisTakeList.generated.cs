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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTaskCheck/MisTakeList.cshtml")]
    public partial class _Views_DesTaskCheck_MisTakeList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTaskCheck_MisTakeList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<div");

WriteLiteral(" id=\"MisTakeListDiv\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"overflow:hidden;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"height:30px;padding:5px 0px;\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-plus\'\"");

WriteLiteral(" id=\"MisTake\"");

WriteLiteral(">添加到校审意见</a>\r\n            <span");

WriteLiteral(" style=\"margin-left:8px;font-size:13px\"");

WriteLiteral("> 已选择<strong");

WriteLiteral(" style=\"margin:0px 5px;\"");

WriteLiteral(" id=\"selectMisTake\"");

WriteLiteral(" data-misTake=\"\"");

WriteLiteral(">0</strong>项</span>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"TaskMisTakeListDiv\"");

WriteLiteral(" style=\"width:100%;height:100%;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"TaskMisTakeList\"");

WriteLiteral("></table>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    var _selectId = [];

    $(""#MisTake"").click(function () {
        if (_selectId.length == 0) {
            JQ.dialog.show(""请至少选择一项！！！"");
            return false;
        }

        JQ.dialog.confirm(""你确定将此"" + _selectId.length + ""项，添加到校审意见中嘛？"", function () {
            JQ.tools.ajax({
                url: '");

            
            #line 27 "..\..\Views\DesTaskCheck\MisTakeList.cshtml"
                 Write(Url.Action("AddToDesCheck"));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                data: { taskId: \'");

            
            #line 28 "..\..\Views\DesTaskCheck\MisTakeList.cshtml"
                            Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral(@"', isoCheckIds: _selectId.join(',') },
                succesCollBack: function (param) {
                    if (param.stateValue > 0) {
                        var info = JQ.tools.findDialogInfo();
                        if (typeof (window[""TabSelect_"" + info.divid]) == ""function"") {
                            window[""TabSelect_"" + info.divid](""校审意见"");
                        }
                    }
                }
            })
        })
    });

    function showSelectCount() {
        $(""#selectMisTake"", ""#MisTakeListDiv"").text(_selectId.length);
    }

    function showTaskMisTakeGrid() {
        JQ.datagrid.datagrid({
            idField: 'Id',//主键的字段
            JQID: 'TaskMisTakeList',//数据表格ID
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,datagrid]
            JQFields: [""Id""],//追加的字段名
            url: '");

            
            #line 51 "..\..\Views\DesTaskCheck\MisTakeList.cshtml"
             Write(Url.Action("MisTakeJson"));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            queryParams: { taskId: \'");

            
            #line 52 "..\..\Views\DesTaskCheck\MisTakeList.cshtml"
                               Write(ViewBag.taskId);

            
            #line default
            #line hidden
WriteLiteral("\' },\r\n            pagination: false,\r\n            rownumbers: true,\r\n\r\n          " +
"  checkOnSelect: true,\r\n            fit: true,\r\n            columns: [[\r\n       " +
"         {field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true,},\r\n  " +
"              { title: \'角色\', field: \'CheckTypeName\', width: \'12%\', align: \'cente" +
"r\', },\r\n                { title: \'差错内容\', field: \'CheckItem\', width: \'50%\', align" +
": \'left\', },\r\n                { title: \'备注\', field: \'CheckNote\', width: \'20%\', a" +
"lign: \'left\', },\r\n                {\r\n                    title: \'是否添加\', field: \'" +
"IsCreate\', width: \'10%\', align: \'left\', formatter: function (value, row, idnex) " +
"{\r\n                        if (value == \"1\") {\r\n                            retu" +
"rn \"已添加\";\r\n                        }\r\n                        else {\r\n          " +
"                  return \"\";\r\n                        }\r\n                    }\r\n" +
"                },\r\n            ]],\r\n            onCheck: function (index, row) " +
"{\r\n                if (_selectId.indexOf(row.Id) == -1) {\r\n                    _" +
"selectId.push(row.Id);\r\n                }\r\n                showSelectCount();\r\n " +
"           },\r\n            onUncheck: function (index, row) {\r\n                _" +
"selectId = Enumerable.From(_selectId).Where(\"x=>x!=\" + row.Id + \"\").ToArray();\r\n" +
"                showSelectCount();\r\n            },\r\n            onCheckAll: func" +
"tion (rows) {\r\n                $.each(rows, function (i, e) {\r\n                 " +
"   if (_selectId.indexOf(e.Id) == -1) {\r\n                        _selectId.push(" +
"e.Id);\r\n                    }\r\n                });\r\n                showSelectCo" +
"unt();\r\n            },\r\n            onUncheckAll: function (rows) {\r\n           " +
"     var _unSelect = Enumerable.From(rows).Select(\"x=>x.Id\").ToArray();\r\n       " +
"         _selectId = Enumerable.From(_selectId).Where(function (i) {\r\n          " +
"          return _unSelect.indexOf(i) == -1;\r\n                }).ToArray();\r\n   " +
"             showSelectCount();\r\n            },\r\n            onLoadSuccess:funct" +
"ion (data) {\r\n                //if (data.total > 0) {\r\n                //    $.e" +
"ach(data.rows, function (i, e) {\r\n                //        if (e.IsCreate == \"1" +
"\") {\r\n                //            var checkB = $(\"#TaskMisTakeList\").datagrid(" +
"\"getPanel\").find(\".datagrid-btable\").find(\"td[field=\\\"ck\\\"]\");\r\n                " +
"//            $(checkB[i]).empty();\r\n                //            $(\"<div class" +
"=\'datagrid-header-check\' title=\'已添加\'><i class=\'fa fa-check\' ></i></div>\").append" +
"To($(checkB[i]));\r\n                //        }\r\n                //    })\r\n      " +
"          //}\r\n            }\r\n        });\r\n    }\r\n\r\n    // 页面初始化\r\n    function M" +
"isTakeListPageInfo() {\r\n        if (\'");

            
            #line 115 "..\..\Views\DesTaskCheck\MisTakeList.cshtml"
        Write(ViewBag.from);

            
            #line default
            #line hidden
WriteLiteral(@"' == ""TaskInfoApprove"") {
            $(""#MisTake"", ""#MisTakeListDiv"").show();
        } else {
            $(""#MisTake"", ""#MisTakeListDiv"").hide();
        }
    }

    $(function () {
        // 页面初始化
        MisTakeListPageInfo();

        // 显示排查模板
        showTaskMisTakeGrid();
    });

</script>
");

        }
    }
}
#pragma warning restore 1591
