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
    
    #line 4 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaEquipment/listOaEquipment.cshtml")]
    public partial class _Views_OaEquipment_listOaEquipment_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaEquipment_listOaEquipment_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var requestUrl = \'");

            
            #line 7 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
                 Write(Url.Action("jsonOaEquipment", "OaEquipment", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("?EquipOrOffice=1\',\r\n           addUrl = \'");

            
            #line 8 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
                Write(Url.Action("add","OaEquipment",new {@area="Oa" }));

            
            #line default
            #line hidden
WriteLiteral("?EquipOrOffice=1\',\r\n           editUrl = \'");

            
            #line 9 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
                 Write(Url.Action("edit", "OaEquipment",new {@area="Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n           delUrl = \'");

            
            #line 10 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
                Write(Url.Action("del", "OaEquipment", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    $(function () {\r\n        JQ.datagrid.datagrid({\r\n            JQButtonType" +
"s: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n            JQAddUrl: addUrl, //添" +
"加的路径\r\n            JQEditUrl: editUrl,//编辑的路径\r\n            JQDelUrl: delUrl, //删除" +
"的路径\r\n            JQPrimaryID: \'Id\',//主键的字段\r\n            JQID: \'OaEquipmentGrid\'," +
"//数据表格ID\r\n            JQDialogTitle: \'办公用品信息\',//弹出窗体标题\r\n            JQWidth: \'10" +
"00\',//弹出窗体宽\r\n            JQHeight: \'800\',//弹出窗体高\r\n            JQLoadingType: \'da" +
"tagrid\',//数据表格类型 [datagrid,treegrid]\r\n            JQFields: [\"Id\"],//追加的字段名\r\n   " +
"         url: requestUrl,//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n " +
"           toolbar: \'#OaEquipmentPanel\',//工具栏的容器ID\r\n            JQExcludeCol: [1" +
", 7, 8],//导出execl排除的列\r\n            columns: [[\r\n                { field: \'ck\', a" +
"lign: \'center\', checkbox: true, JQIsExclude: true },\r\n                { title: \'" +
"办公用品编号\', field: \'EquipNumber\', width: \"20%\", align: \'center\', sortable: true },\r" +
"\n                { title: \'办公用品名称\', field: \'EquipName\', width: \"20%\", align: \'ce" +
"nter\', sortable: true },\r\n                { title: \'规格\', field: \'EquipModel\', wi" +
"dth: \"10%\", align: \'center\', sortable: true },\r\n                { title: \'总量\', f" +
"ield: \'EquipTotalCount\', width: \"10%\", align: \'center\', sortable: true },\r\n     " +
"           { title: \'库存\', field: \'SJKC\', width: \"10%\", align: \'center\', sortable" +
": true },\r\n                { title: \'登记员\', field: \'CreatorEmpName\', width: \"10%\"" +
", align: \'center\', sortable: true },\r\n                {\r\n                    tit" +
"le: \'使用情况\', field: \'使用情况\', width: \"10%\", align: \'center\', sortable: false, forma" +
"tter: function (value, row) {\r\n                        return \"<a href=\'javascri" +
"pt:UsageList(\" + row.Id + \")\' class=\'easyui-linkbutton\'>使用情况</a>\";\r\n            " +
"        }\r\n                },\r\n                {\r\n                    field: \'JQ" +
"Files\', title: \'附件\', align: \'center\', width: \"5%\", JQIsExclude: true, JQExcludeF" +
"lag: \"grid_files\", JQRefTable: \'OaEquipment\', formatter:\r\n                      " +
"function (value, row) {\r\n                          return \"<span id=\\\"grid_files" +
"_\" + row.Id + \"\\\"></span>\"\r\n                      }\r\n                }\r\n        " +
"    ]],\r\n            onBeforeCheck: function (rowIndex, rowData) {\r\n            " +
"    if ($(\".datagrid-row[datagrid-row-index=\" + rowIndex + \"] input[type=\'checkb" +
"ox\']\").attr(\"disabled\") == \"disabled\") {\r\n                    return false;\r\n   " +
"             }\r\n            },\r\n            onCheckAll: function (rowData) {\r\n  " +
"              for (var i = 0; i < rowData.length; i++) {\r\n                    if" +
" ($(\".datagrid-row[datagrid-row-index=\" + i + \"] input[type=\'checkbox\']\").attr(\"" +
"disabled\") == \"disabled\") {\r\n                        $(\".datagrid-row[datagrid-r" +
"ow-index=\" + i + \"] input[type=\'checkbox\']\")[0].checked = false;\r\n              " +
"      }\r\n                }\r\n            }\r\n        });\r\n        $(\"#JQSearch\").t" +
"extbox({\r\n            buttonText: \'筛选\',\r\n            buttonIcon: \'fa fa-search\'," +
"\r\n            height: 25,\r\n            prompt: \'办公用品名称\',\r\n            onClickBut" +
"ton: function () {\r\n                JQ.datagrid.searchGrid(\r\n                   " +
" {\r\n                        dgID: \'OaEquipmentGrid\',\r\n                        lo" +
"adingType: \'datagrid\',\r\n                        queryParams: null\r\n             " +
"       });\r\n            }\r\n        });\r\n    });\r\n\r\n    var chooseUrl = \'");

            
            #line 77 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
                Write(Url.Action("OaUsageList", "OaEquipment", new { @area = "Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    function UsageList(id) {\r\n        JQ.dialog.dialog({\r\n            title: " +
"\"使用情况查看\",\r\n            url: chooseUrl + \"?EquId=\" + id + \"\",\r\n            width:" +
" \'800\',\r\n            height: \'600\'\r\n        });\r\n    }\r\n    </script>\r\n");

            
            #line 87 "..\..\Views\OaEquipment\listOaEquipment.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"OaEquipmentGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"OaEquipmentPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EquipName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591