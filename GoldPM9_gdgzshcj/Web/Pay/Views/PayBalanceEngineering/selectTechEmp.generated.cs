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
    
    #line 2 "..\..\Views\PayBalanceEngineering\selectTechEmp.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/PayBalanceEngineering/selectTechEmp.cshtml")]
    public partial class _Views_PayBalanceEngineering_selectTechEmp_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_PayBalanceEngineering_selectTechEmp_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .rwpdialogdiv {\r\n        padding: 0px;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        $("".dialog-toolbar :last"").hide();
        JQ.datagrid.datagrid({
            JQButtonTypes: [],//需要显示的按钮
            JQID: 'EmpListGrid',//数据表格ID
            JQDialogTitle: '选择评审记录',//弹出窗体标题
            JQWidth: '1024',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            singleSelect: false,
            pagination:false,
            url: '");

            
            #line 20 "..\..\Views\PayBalanceEngineering\selectTechEmp.cshtml"
             Write(Url.Action("selectTechEmpJson", "PayBalanceEngineering", new { @area="pay"}));

            
            #line default
            #line hidden
WriteLiteral("?projId=");

            
            #line 20 "..\..\Views\PayBalanceEngineering\selectTechEmp.cshtml"
                                                                                                  Write(ViewBag.projId);

            
            #line default
            #line hidden
WriteLiteral("&PhaseId=");

            
            #line 20 "..\..\Views\PayBalanceEngineering\selectTechEmp.cshtml"
                                                                                                                          Write(ViewBag.PhaseId);

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
            checkOnSelect: true,//是否包含check
            toolbar: '#ReviewPanel',//工具栏的容器ID
            columns: [[
                { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
                { title: '任务名称', field: 'TaskName', width: 280, align: 'center', sortable: true },
                { title: '专业', field: 'SpecName', width: 200, align: 'center', sortable: true },
                { title: '节点类型', field: 'FlowNodeName', width: 200, align: 'center', sortable: true },
                { title: '人员姓名', field: 'FlowNodeEmpName', width: 200, align: 'center', sortable: true }
            ]],

        });

    });

    function selectEmpList() {
        var rows = $('#EmpListGrid').datagrid('getSelections');
        if (rows.length < 1) {
            window.top.JQ.dialog.warning('您必须选择至少一项！！！');
            return;
        }
        for (var rowindex=0;rowindex<rows.length;rowindex++)
        {
            insert(rows[rowindex]);
        }
        
        JQ.dialog.dialogClose();
    }

</script>

<table");

WriteLiteral(" id=\"EmpListGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"ReviewPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"selectEmpList()\"");

WriteLiteral(">确定选择</a>\r\n    </span>\r\n    <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n\r\n</div>\r\n\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591