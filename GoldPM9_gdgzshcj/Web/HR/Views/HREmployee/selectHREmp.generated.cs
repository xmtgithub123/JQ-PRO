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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/HREmployee/selectHREmp.cshtml")]
    public partial class _Views_HREmployee_selectHREmp_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_HREmployee_selectHREmp_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.datagrid.datagrid({
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'HREmployeeGrid',//数据表格ID
            JQDialogTitle: '信息',//弹出窗体标题
            JQWidth: '768',//弹出窗体宽
            JQHeight: '100%',//弹出窗体高
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            JQFields: [""Id""],//追加的字段名
            JQSearch: {
                id: 'JQSearch',
                prompt: '筛选字段',
                loadingType: 'datagrid',//默认值为datagrid可以不传
            },
            url: '");

            
            #line 18 "..\..\Views\HREmployee\selectHREmp.cshtml"
             Write(Url.Action("json", "HREmployee",new { @area="Hr"}));

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
            fitColumns: false,
            nowrap: false,
            singleSelect:true,
            toolbar: '#HREmployeePanel',//工具栏的容器ID
            columns: [[
                { title: '部门', field: 'DepName', width: 100, align: 'center', sortable: true },
                { title: '姓名', field: 'EmpName', width: 100, align: 'center', sortable: true },
                { title: '入职日期', field: 'EmpJoinDate', width: 100, align: 'center', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },
                { title: '性别', field: 'EmpSexName', width: 100, align: 'center', sortable: true },
                { title: '手机', field: 'EmpPhoneNumber', width: 100, align: 'center', sortable: true },
                { title: '年休天数', field: 'VacationDays1', width: 100, align: 'center', sortable: true },
                { title: '最高学历', field: 'LastEducationName', width: 100, align: 'center', sortable: true }
            ]]
        });
    });

    function closeDia() {
        JQ.dialog.dialogClose();
    }

    function sure() {

        var rows = $('#HREmployeeGrid').datagrid('getSelections');
        if (rows.length < 1) {
            window.top.JQ.dialog.warning('您必须选择至少一项！！！');
            return;
        }
        _SureEmp(rows[0]);
        JQ.dialog.dialogClose();
    }
</script>
<table");

WriteLiteral(" id=\"HREmployeeGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"HREmployeePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnSure\"");

WriteLiteral(" id=\"btnSure\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"sure()\"");

WriteLiteral(">确定</a>\r\n        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnCancel\"");

WriteLiteral(" id=\"btnCancel\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-close\'\"");

WriteLiteral(" onclick=\"closeDia()\"");

WriteLiteral(">取消</a>\r\n    </span>\r\n\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EmpName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">\r\n    </div>\r\n</div>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
