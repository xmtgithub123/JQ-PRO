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
    
    #line 1 "..\..\Views\Project\ListInfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Project/ListInfo.cshtml")]
    public partial class _Views_Project_ListInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Project_ListInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.treegrid.treegrid({
            pagination: true,
            JQButtonTypes: [],//需要显示的按钮
            JQPrimaryID: 'Id',//主键的字段
            idField: 'Id',
            treeField: 'ProjName',
            JQID: 'ProjectTreeGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'treegrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: true,
            url: '");

            
            #line 17 "..\..\Views\Project\ListInfo.cshtml"
             Write(Url.Action("GetJsonList", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral("?Company=");

            
            #line 17 "..\..\Views\Project\ListInfo.cshtml"
                                                                                  Write(Request.Params["CompanyType"].ToString());

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n            checkOnSelect: true,//是否包含check\r\n            toolbar: \'#Pr" +
"ojectTreePanel\',//工具栏的容器ID\r\n            columns: [[\r\n              { field: \'ck\'" +
", align: \'center\', checkbox: true, JQIsExclude: true },\r\n              { title: " +
"\'项目名称\', field: \'ProjName\', width: \"28%\", halign: \'center\', align: \'left\', sortab" +
"le: true },\r\n              { title: \'设总\', field: \'ProjEmpName\', width: \"8%\", hal" +
"ign: \'center\', align: \'center\', sortable: true },\r\n              { title: \'项目阶段\'" +
", field: \'PhaseNames\', width: \"20%\", halign: \'center\', align: \'left\', sortable: " +
"true },\r\n              { title: \'项目类别\', field: \'ProjTypeName\', width: \"8%\", hali" +
"gn: \'center\', align: \'center\', sortable: true },\r\n              { title: \'建设性质\'," +
" field: \'ProjPropertyName\', width: \"8%\", halign: \'center\', align: \'center\', sort" +
"able: true },\r\n              { title: \'电压级别\', field: \'ProjVoltName\', width: \"8%\"" +
", halign: \'center\', align: \'center\', sortable: true },\r\n              { title: \'" +
"立项时间\', field: \'DateCreate\', width: \"8%\", halign: \'center\', align: \'center\', sort" +
"able: true, formatter: JQ.tools.DateTimeFormatterByT },\r\n              { title: " +
"\'计划完成时间\', field: \'DatePlanFinish\', width: \"8%\", halign: \'center\', align: \'center" +
"\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT }\r\n            ]],\r\n" +
"            onLoadSuccess: function (row, data) {\r\n                if (data.tota" +
"l) {\r\n                    $.each(data.rows, function (index, item) {\r\n          " +
"              $(\"input[type=\'checkbox\']\")[index + 2].style.display = \"none\";\r\n  " +
"                      var number = item.row_number;\r\n                        $(\"" +
"tr[node-id=\'\" + item.Id + \"\']\").find(\"div.datagrid-cell-rownumber\").text(number)" +
";\r\n                    });\r\n                } else {\r\n                    var gr" +
"idRoots = $(\"#ProjectTreeGrid\").treegrid(\"getRoots\");\r\n                    var F" +
"irst = 0;\r\n                    $.each(gridRoots, function (i, n) {\r\n            " +
"            var num = n.row_number;\r\n                        var rowChildren = $" +
"(\"#ProjectTreeGrid\").treegrid(\"getChildren\", n.Id);\r\n                        if " +
"(rowChildren.length > 0) {\r\n                            First += rowChildren.len" +
"gth;\r\n                            $(\"tr[node-id=\'\" + n.Id + \"\']\").find(\"div.data" +
"grid-cell-rownumber\").text(num);\r\n\r\n                            $.each(rowChildr" +
"en, function (y, k) {\r\n                                var tNum = y + num + 1;\r\n" +
"                                //$(\"tr[node-id=\'\" + k.Id + \"\']\").find(\"div.data" +
"grid-cell-rownumber\").text(num + \'-\' + (y + 1));\r\n                              " +
"  $(\"tr[node-id=\'\" + k.Id + \"\']\").find(\"div.datagrid-cell-rownumber\").text(\"\");\r" +
"\n                            })\r\n                        }\r\n                    " +
"    else {\r\n                            var RowNum = num + First;\r\n             " +
"               $(\"tr[node-id=\'\" + n.Id + \"\']\").find(\"div.datagrid-cell-rownumber" +
"\").text(num);\r\n                        }\r\n                    });\r\n             " +
"   }\r\n            },\r\n            onCheck: function (rowIndex, rowData) {\r\n     " +
"           if (rowIndex.ParentId == 0) {\r\n                    return false;\r\n   " +
"             }\r\n                else {\r\n                    $(\"#choiceIds\").val(" +
"JSON.stringify(rowIndex));\r\n                }\r\n            },\r\n            onBef" +
"oreLoad: function (row, param) {\r\n                if ($(\"#category\").val() == \"0" +
"\") {\r\n                    $(\"#ProjectTreeGrid\").treegrid(\'options\').url = \'");

            
            #line 71 "..\..\Views\Project\ListInfo.cshtml"
                                                                Write(Url.Action("GetJsonList", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral("?category=0&CompanyType=");

            
            #line 71 "..\..\Views\Project\ListInfo.cshtml"
                                                                                                                                                    Write(Request.Params["CompanyType"].ToString());

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                }\r\n                $(\"#category\").val(\"0\");\r\n            },\r\n" +
"            onBeforeExpand: function (row) {\r\n                $(\"#category\").val" +
"(\"1\");\r\n                $(this).treegrid(\'options\').url = \'");

            
            #line 77 "..\..\Views\Project\ListInfo.cshtml"
                                              Write(Url.Action("GetJsonList", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral("?category=1&CompanyType=");

            
            #line 77 "..\..\Views\Project\ListInfo.cshtml"
                                                                                                                                  Write(Request.Params["CompanyType"].ToString());

            
            #line default
            #line hidden
WriteLiteral(@"';
            }
        });

        $(""#JQSearch"").textbox({
            buttonText: '筛选',
            buttonIcon: 'fa fa-search',
            height: 25,
            prompt: '项目名称',
            onClickButton: function () {
                JQ.datagrid.searchGrid(
                    {
                        dgID: 'ProjectTreeGrid',
                        loadingType: 'treegrid',
                    });
            }
        });
    });
    function selectProj() {
        var Id = $(""#choiceIds"").val();
        if (Id == """") {
            JQ.dialog.warning(""请选择具体的项目信息!!!"");
        }
        else {
            _ProjSubBack(Id);
            JQ.dialog.dialogClose();
        }
    }

    function closedialog() {
        JQ.dialog.dialogClose();
    }

    //$.extend($.fn.treegrid.defaults.view, {
    //    onAfterRender: function (target) {
    //        var gridRoots = $(target).treegrid(""getRoots"");
    //        debugger;
    //        $.each(gridRoots, function (i, n) {
    //            debugger;
    //            var num = n.row_number;
    //            var rowChildren = $(target).treegrid(""getChildren"", n.Id);
    //            var rowNum = num + rowChildren.length;
    //            $(""tr[node-id='"" + n.Id + ""']"").find(""div.datagrid-cell-rownumber"").text(1111);
    //        });
    //    }
    //});
</script>




<table");

WriteLiteral(" id=\"ProjectTreeGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"ProjectTreePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectProj()\"");

WriteLiteral(">确定选择</a>\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"closedialog()\"");

WriteLiteral(">关闭</a>\r\n    </span>\r\n    ");

WriteLiteral("\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'txtLike\', Contract:\'like\'}\"");

WriteLiteral(" />\r\n\r\n</div>\r\n\r\n<input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"choiceIds\"");

WriteLiteral(" name=\"choiceIds\"");

WriteLiteral(" />\r\n<input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"category\"");

WriteLiteral(" name=\"category\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" />");

        }
    }
}
#pragma warning restore 1591
