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
    
    #line 4 "..\..\Views\ArchProjectFolder\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ArchProjectFolder/list.cshtml")]
    public partial class _Views_ArchProjectFolder_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_ArchProjectFolder_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\ArchProjectFolder\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            $(function () {\r\n                var myur" +
"ls = \'");

            
            #line 9 "..\..\Views\ArchProjectFolder\list.cshtml"
                         Write(Url.Action("treejson", "ArchProjectFolder", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n                myurls = myurls + \'?ProjID=");

            
            #line 10 "..\..\Views\ArchProjectFolder\list.cshtml"
                                      Write(ViewBag.ProjID);

            
            #line default
            #line hidden
WriteLiteral(@"';
                $('#lefttree').treegrid({
                    url: myurls,
                    idField: 'id',
                    treeField: 'text',
                    border: 0,
                    toolbar: '#tbleft',
                    fit: true,
                    animate: true,
                    singleSelect: true,
                    columns: [[
                       { title: '<b>选择节点</b>', field: 'text', width: 240, align: 'left' }
                    ]],
                    onLoadSuccess: function (row, data) {
                        //FileBtnShow(true);
                    },
                    onSelect: function (node) {
                        $(""#AttID"").val(node.id);
                        $('#ArchShareFolderFileGrid').datagrid('load', {
                            FolderId: node.id
                        });
                        ");

WriteLiteral(@"
                    }
                });

                JQ.datagrid.datagrid({
                    JQIsSearch: true,
                    JQPrimaryID: 'Id',//主键的字段
                    JQID: 'ArchShareFolderFileGrid',//数据表格ID
                    JQDialogTitle: '文件信息',//弹出窗体标题
                    JQWidth: '600',//弹出窗体宽
                    JQHeight: '600',//弹出窗体高
                    JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                    JQFields: [""Id""],//追加的字段名
                    url: '");

            
            #line 47 "..\..\Views\ArchProjectFolder\list.cshtml"
                     Write(Url.Action("json", "ArchProjectFolderFile", new { @area="Archive"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                    checkOnSelect: true,//是否包含check\r\n                 " +
"   fitColumns: true,\r\n                    nowrap: false,\r\n                    to" +
"olbar: \'#ArchShareFolderFilePanel\',//工具栏的容器ID\r\n                    columns: [[\r\n" +
"                            { field: \'ck\', align: \'center\', checkbox: true, JQIs" +
"Exclude: true },\r\n                            { title: \'文件名称\', field: \'FileName\'" +
", width: 100, align: \'center\', sortable: true },\r\n                            { " +
"title: \'文件描述\', field: \'FileRemark\', width: 100, align: \'center\', sortable: true " +
"},\r\n                            { title: \'创建时间\', field: \'CreationTime\', width: 1" +
"00, align: \'center\', sortable: true, formatter: JQ.tools.DateTimeFormatterByT }," +
"\r\n                            { title: \'创建人姓名\', field: \'CreatorEmpName\', width: " +
"100, align: \'center\', sortable: true },\r\n                            { title: \'最" +
"后修改时间\', field: \'LastModificationTime\', width: 100, align: \'center\', sortable: tr" +
"ue, formatter: JQ.tools.DateTimeFormatterByT },\r\n                            { t" +
"itle: \'最后修改人姓名\', field: \'LastModifierEmpName\', width: 100, align: \'center\', sort" +
"able: true },\r\n                            {\r\n                                fi" +
"eld: \'JQFiles\', title: \'附件\', align: \'center\', width: 80, JQIsExclude: true, JQEx" +
"cludeFlag: \"grid_files\", JQRefTable: \'ArchProjectFolderFile\', formatter: functio" +
"n (value, row) {\r\n                                      return \"<span id=\\\"grid_" +
"files_\" + row.Id + \"\\\"></span>\"\r\n                                  }\r\n          " +
"                  },\r\n\r\n\r\n                    ]]\r\n                });\r\n         " +
"       $(\"#JQSearch\").textbox({\r\n                    buttonText: \'筛选\',\r\n        " +
"            buttonIcon: \'fa fa-search\',\r\n                    height: 25,\r\n      " +
"              prompt: \'文件名称\',\r\n                    onClickButton: function () {\r" +
"\n                        JQ.datagrid.searchGrid(\r\n                            {\r" +
"\n                                dgID: \'ArchShareFolderFileGrid\',\r\n             " +
"                   loadingType: \'datagrid\',\r\n                                que" +
"ryParams: { FolderId: $(\"#AttID\").val() }\r\n                            });\r\n    " +
"                }\r\n                });\r\n            });\r\n        });\r\n\r\n        " +
"function savefile(flag) {\r\n            if (flag == 1) {\r\n                var ite" +
"m = $(\"#lefttree\").treegrid(\"getSelected\");\r\n                if (item==null) {\r\n" +
"                    JQ.dialog.info(\"请选择\");\r\n                    return;\r\n       " +
"         }\r\n                var id = item.id;\r\n                JQ.dialog.dialog(" +
"{\r\n                    title: \"添加文件\",\r\n                    url: \'");

            
            #line 96 "..\..\Views\ArchProjectFolder\list.cshtml"
                     Write(Url.Action("add", "ArchProjectFolderFile", new {@area="Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?FolderId=' + id,
                    width: '600',
                    height: '600',
                    iconCls: 'fa fa-plus',
                    onClose: function () {
                        $(""#ArchShareFolderFileGrid"").datagrid('reload');
                    }
                });
                return;
            }
            var row = $(""#ArchShareFolderFileGrid"").datagrid(""getSelections"");
            if (row.length <= 0) {
                JQ.dialog.info(""请选择"");
                return;
            }
            var id = row[0].Id;
            if (flag == 2) {
                JQ.dialog.dialog({
                    title: ""修改文件"",
                    url: '");

            
            #line 115 "..\..\Views\ArchProjectFolder\list.cshtml"
                     Write(Url.Action("edit", "ArchProjectFolderFile", new {@area="Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"' + '?Id=' + id,
                    width: '600',
                    height: '600',
                    iconCls: 'fa fa-plus',
                    onClose: function () {
                        $(""#ArchShareFolderFileGrid"").datagrid('reload');
                    }
                });
            }
            else if (flag == 3) {
                var parm;
                $.each(row, function (i, n) {
                    if (i == 0) {
                        parm = ""id="" + n[""Id""];

                    } else {
                        parm += ""&id="" + n[""Id""];
                    }
                });

                JQ.tools.ajax({
                    doBackResult: true,
                    url: '");

            
            #line 137 "..\..\Views\ArchProjectFolder\list.cshtml"
                     Write(Url.Action("del", "ArchProjectFolderFile", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"',
                    data: parm,
                    async:false
                });
                $('#ArchShareFolderFileGrid').datagrid('reload');
                return;
            }
        }

        function downurl(url) {
            var _a = document.createElement(""a"");
            _a.setAttribute(""href"", url);
            document.body.appendChild(_a);
            _a.click();
            document.body.removeChild(_a);
        }

        function down() {
            var row = $(""#ArchShareFolderFileGrid"").datagrid(""getSelections"");
            if (row.length <= 0) {
                JQ.dialog.info(""请选择"");
                return;
            }

            var parm;
            $.each(row, function (i, n) {
                if (i == 0) {
                    parm = ""id="" + n[""Id""];

                } else {
                    parm += ""&id="" + n[""Id""];
                }
            });
            Url = '");

            
            #line 170 "..\..\Views\ArchProjectFolder\list.cshtml"
              Write(Url.Action("DownFile", "ArchProjectFolderFile", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"?';
            downurl(Url + parm);
            $('#ArchShareFolderFileGrid').datagrid('reload');
        }

        function FileBtnShow(b) {
            if (b) {
                $(""#fileadd"").hide();
                $(""#fileedit"").hide();
                $(""#filedel"").hide();
            }
            else {
                $(""#fileadd"").show();
                $(""#fileedit"").show();
                $(""#filedel"").show();
            }
        }
    </script>
");

WriteLiteral("    ");

            
            #line 188 "..\..\Views\ArchProjectFolder\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"treePanel\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" style=\"overflow: hidden\"");

WriteLiteral(" fit=\"true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" data-options=\"region:\'west\',split:false\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"lefttree\"");

WriteLiteral("></table>\r\n        </div>\r\n        <div");

WriteLiteral(" data-options=\"region:\'center\'\"");

WriteLiteral(" style=\"background:#eee;\"");

WriteLiteral(">\r\n            <table");

WriteLiteral(" id=\"ArchShareFolderFileGrid\"");

WriteLiteral("></table>\r\n        </div>\r\n        <div");

WriteLiteral(" id=\"tbleft\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n            ");

WriteLiteral("\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" id=\"ArchShareFolderFilePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n            <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" id=\"fileadd\"");

WriteLiteral(" onclick=\"savefile(1)\"");

WriteLiteral(">新增</a>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-pencil\'\"");

WriteLiteral(" id=\"fileedit\"");

WriteLiteral(" onclick=\"savefile(2)\"");

WriteLiteral(">修改</a>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-trash\'\"");

WriteLiteral(" id=\"filedel\"");

WriteLiteral(" onclick=\"savefile(3)\"");

WriteLiteral(">删除</a>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-pencil\'\"");

WriteLiteral(" onclick=\"down()\"");

WriteLiteral(">下载</a>\r\n            </span>\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'开始日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CreationTime\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'结束日期\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'CreationTime\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n\r\n            <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'FileName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n\r\n        </div>\r\n    </div>\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" id=\"DeleterEmpId\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'DeleterEmpId\', \'Contract\': \'=\',filedType:\'Int\' }\"");

WriteLiteral(" />\r\n    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" value=\"0\"");

WriteLiteral(" id=\"AttID\"");

WriteLiteral(" />\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
