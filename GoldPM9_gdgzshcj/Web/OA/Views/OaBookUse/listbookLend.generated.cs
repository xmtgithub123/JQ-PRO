﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
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
    
    #line 4 "..\..\Views\OaBookUse\listbookLend.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaBookUse/listbookLend.cshtml")]
    public partial class _Views_OaBookUse_listbookLend_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaBookUse_listbookLend_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaBookUse\listbookLend.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            $(\".dialog-toolbar :last\").hide();\r\n\r\n   " +
"         $(\"#JQSearch\").textbox({\r\n                buttonText: \'筛选\',\r\n          " +
"      buttonIcon: \'fa fa-search\',\r\n                height: 25,\r\n                " +
"prompt: \'请输入图书名称或编号\',\r\n                onClickButton: function () {\r\n           " +
"         refreshGrid();\r\n                }\r\n            });\r\n\r\n            $.ext" +
"end($.fn.datagrid.defaults.view, {\r\n                onAfterRender: function () {" +
"\r\n                    $(\"a[flag=\'menubutton\']\").mouseover(function () {\r\n       " +
"                 document.getElementById(\"mm3\").setAttribute(\"rowid\", this.getAt" +
"tribute(\"dataid\"));\r\n                        document.getElementById(\"mm3\").setA" +
"ttribute(\"rowCount\", this.getAttribute(\"dataCount\"));\r\n\r\n                    })." +
"menubutton({\r\n                        iconCls: \'icon-edit\',\r\n                   " +
"     menu: \'#mm3\'\r\n                    });\r\n                }\r\n            });\r\n" +
"\r\n            JQ.datagrid.datagrid({\r\n                JQButtonTypes: [],//需要显示的按" +
"钮\r\n                JQPrimaryID: \'Id\',//主键的字段\r\n                JQID: \'OaBookGrid\'" +
",//数据表格ID\r\n                JQDialogTitle: \'选择图书\',//弹出窗体标题\r\n                JQWid" +
"th: \'1024\',//弹出窗体宽\r\n                JQHeight: \'100%\',//弹出窗体高\r\n                JQ" +
"LoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                singleSele" +
"ct: false,\r\n                //JQCustomSearch: [  //自定义搜索的字段\r\n                //{" +
" value: \'DepartmentName\', key: \'人员部门\', type: \'string\' }//type支持三种类型string,dateti" +
"me,combox,默认为string,combox必须提供engname\r\n                //],\r\n                //J" +
"QExcludeCol: [1, 4, 10],//导出execl排除的列\r\n                //JQMergeCells: { fields:" +
" [\'DepartmentName\', \'EmpName\', \'EmpLogin\'], Only: \'DepartmentName\' },//当字段值相同时会自" +
"动的跨行(只对相邻有效)\r\n                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据\r\n     " +
"           JQFields: [\"Id\"],//追加的字段名\r\n                url: \'");

            
            #line 49 "..\..\Views\OaBookUse\listbookLend.cshtml"
                 Write(Url.Action("json", "OaBook", new { @area = "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                toolb" +
"ar: \'#OaBookPanel\',//工具栏的容器ID\r\n              \r\n                columns: [[\r\n    " +
"                  { title: \'图书类别\', field: \'BookTypeName\', width: \"10%\", align: \'" +
"center\', sortable: true },\r\n                        { title: \'图书名称\', field: \'Boo" +
"kName\', width: \"20%\", align: \'center\', sortable: true },\r\n                      " +
"  { title: \'图书编号\', field: \'BookNameNumber\', width: \"15%\", align: \'center\', sorta" +
"ble: true },\r\n                         { title: \'出版商\', field: \'BookPublisher\', w" +
"idth: \"15%\", align: \'center\', sortable: true },\r\n                          { tit" +
"le: \'作者\', field: \'BookAuthor\', width: \"10%\", align: \'center\', sortable: true },\r" +
"\n                            { title: \'图书数量\', field: \'BookQuantity\', width: \"10%" +
"\", align: \'center\', sortable: true },\r\n                           { title: \'可借数量" +
"\', field: \'Count\', width: \"10%\", align: \'center\', sortable: true },\r\n           " +
"                {\r\n                               field: \'opt\', title: \'操作\', wid" +
"th: \"10%\", align: \'center\',\r\n                               formatter: function " +
"(value, row, index) {\r\n                                   if (row.Count > 0) {\r\n" +
"                                       return \"<a href=\\\"javascript:void(0)\\\" id" +
"=\\\"mb\" + index + \"\\\" dataid=\\\"\" + row.Id + \"\\\" dataCount=\\\"\" + row.Count + \"\\\" f" +
"lag=\\\"menubutton\\\"   >操作</a>\";\r\n                                   } else {\r\n   " +
"                                    //return \"无法借阅\";\r\n                          " +
"             return \'<a btn  class=\"easyui-linkbutton\"  data-options=\"iconCls:\\\'" +
"fa fa-check\\\'\"   onclick=\"borrHistory(\' + row.Id + \')\">历史借阅</a>\';\r\n             " +
"                      }\r\n                               }\r\n                     " +
"      }\r\n                ]],\r\n                queryParams: getQueryParameters()," +
"\r\n                onLoadSuccess: function (value, row, index) {\r\n               " +
"     $(\"a[btn]\").linkbutton({ iconCls: \'fa fa-edit\' });\r\n                }\r\n    " +
"        });\r\n             \r\n\r\n            $(\"#JQSearch\").textbox({\r\n            " +
"    buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n            " +
"    height: 25,\r\n                prompt: \'图书编号、图书名称\',\r\n                onClickBu" +
"tton: function () {\r\n                    refreshGrid();\r\n                }\r\n    " +
"        });\r\n\r\n        });\r\n\r\n\r\n\r\n\r\n        //历史借阅\r\n        function borrHistory" +
"(Id) {\r\n            JQ.dialog.dialog({\r\n                title: \"历史借阅记录\",\r\n      " +
"          url: \'");

            
            #line 99 "..\..\Views\OaBookUse\listbookLend.cshtml"
                 Write(Url.Action("BookHistoryByBook"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?id=' + Id,
                width: '900',
                height: '100%',
                JQID: 'OaBookGrid',
                JQLoadingType: 'datagrid',
                iconCls: 'fa fa-plus'
            });
        }

        window.refreshGrid = function () {
            var query = getQueryParameters();
            query.bookType = $(""#BookType"").combotree(""getValues"");
            $(""#OaBookGrid"").datagrid(""load"", query);
        }

        function getQueryParameters() {
            return { text: $(""#JQSearch"").textbox(""getText"") };
        }

        function selectProj() {
            debugger;
            _BookBorrCallBack();
            JQ.dialog.dialogClose();
        }

        function menuHandler(item) {
            var key = $(""#mm3"").attr(""rowid"");
            var rowCount = $(""#mm3"").attr(""rowCount"");
            switch (item.text) {
                case ""借阅"":
                    JQ.dialog.dialog({
                        title: ""借阅"",
                        url: '");

            
            #line 131 "..\..\Views\OaBookUse\listbookLend.cshtml"
                         Write(Url.Action("listBookLendInfo"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?id=' + key + '&rowCount=' + rowCount,
                        width: '950',
                        height: '100%',
                        JQID: 'OaBookGrid',
                        JQLoadingType: 'datagrid',
                        iconCls: 'fa fa-plus'
                    });
                    break;
                case ""历史借阅"":
                    JQ.dialog.dialog({
                        title: ""历史借阅"",
                        url: '");

            
            #line 142 "..\..\Views\OaBookUse\listbookLend.cshtml"
                         Write(Url.Action("BookHistoryByBook"));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?id=' + key,
                        width: '900',
                        height: '100%',
                        JQID: 'OaBookGrid',
                        JQLoadingType: 'datagrid',
                        iconCls: 'fa fa-plus'
                    });
                    break;
                default:
                    break;
            }
        }

    </script>
");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"OaBookGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"OaBookPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        </span>\r\n");

WriteLiteral("        ");

            
            #line 163 "..\..\Views\OaBookUse\listbookLend.cshtml"
   Write(BaseData.getBases(new Params() { isMult = true, engName = "BookType", controlName = "BookType" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n    </div>\r\n    <div");

WriteLiteral(" id=\"mm3\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"div1\"");

WriteLiteral(">借阅</div>\r\n        <div");

WriteLiteral(" id=\"div1\"");

WriteLiteral(">历史借阅</div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591