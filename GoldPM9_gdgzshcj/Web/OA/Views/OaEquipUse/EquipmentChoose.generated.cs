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
    
    #line 2 "..\..\Views\OaEquipUse\EquipmentChoose.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaEquipUse/EquipmentChoose.cshtml")]
    public partial class _Views_OaEquipUse_EquipmentChoose_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_OaEquipUse_EquipmentChoose_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.datagrid.datagrid({
        JQPrimaryID: 'Id',//主键的字段
        JQID: 'OaEquipmentGrid',//数据表格ID
        JQDialogTitle: '设备选择',//弹出窗体标题
        JQWidth: '1000',//弹出窗体宽
        JQHeight: '600',//弹出窗体高
        JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
        url: '");

            
            #line 11 "..\..\Views\OaEquipUse\EquipmentChoose.cshtml"
         Write(Url.Action("json", "OaEquipment", new { @area="Oa"}));

            
            #line default
            #line hidden
WriteLiteral("\' + \'?EquIds=");

            
            #line 11 "..\..\Views\OaEquipUse\EquipmentChoose.cshtml"
                                                                           Write(Request.QueryString["EquIds"]);

            
            #line default
            #line hidden
WriteLiteral("&EquipOrOffice=");

            
            #line 11 "..\..\Views\OaEquipUse\EquipmentChoose.cshtml"
                                                                                                                        Write(Request.QueryString["EquipOrOffice"]);

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
        checkOnSelect: true,//是否包含check
        toolbar: '#OaEquipmentPanel',//工具栏的容器ID
        columns: [[
            { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
            { title: '设备编号', field: 'EquipNumber', width: 100, align: 'center', sortable: true },
		    { title: '设备名称', field: 'EquipName', width: 250, align: 'center', sortable: true },
            { title: '规格', field: 'EquipModel', width: 100, align: 'center', sortable: true },
            { title: '库存', field: 'SJKC', width: 100, align: 'center', sortable: true }
        ]]
    });

    $(""#JQSearch"").textbox({
        buttonText: '筛选',
        buttonIcon: 'fa fa-search',
        height: 25,
        prompt: '设备名称',
        queryOptions: { Key: 'EquipName', Contract: 'like' },
        onClickButton: function () {
            JQ.datagrid.searchGrid(
                {
                    dgID: 'OaEquipmentGrid',
                    loadingType: 'datagrid',
                    queryParams: null
                });
        }
    });

    function selectReivew() {
        var rows = $('#OaEquipmentGrid').datagrid('getSelections');
        if (rows.length < 1) {
            window.top.JQ.dialog.warning('您必须选择至少一项！！！');
            return;
        }
        for (var rowindex = 0; rowindex < rows.length; rowindex++) {
            insert(rows[rowindex]);
        }
        JQ.dialog.dialogClose();
    }
</script>



<table");

WriteLiteral(" id=\"OaEquipmentGrid\"");

WriteLiteral("></table>\r\n<div");

WriteLiteral(" id=\"OaEquipmentPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n    </span>\r\n    <a");

WriteLiteral(" id=\"btn\"");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-search\'\"");

WriteLiteral(" onclick=\"selectReivew()\"");

WriteLiteral(">确定</a>\r\n    <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ \'Key\': \'EquipName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n</div>\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591