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
    
    #line 4 "..\..\Views\BussContractModel\list_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractModel/list_cj.cshtml")]
    public partial class _Views_BussContractModel_list_cj_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussContractModel_list_cj_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussContractModel\list_cj.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.datagrid.datagrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'del\', \'export\'],//需要显示的按钮\r\n                JQAddU" +
"rl: \'");

            
            #line 10 "..\..\Views\BussContractModel\list_cj.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 11 "..\..\Views\BussContractModel\list_cj.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 12 "..\..\Views\BussContractModel\list_cj.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussContractModelGrid',//数据表格ID
                JQDialogTitle: '合同模板信息',//弹出窗体标题
                JQWidth: '1024',//弹出窗体宽
                JQHeight: '90%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                //JQCustomSearch: [  //自定义搜索的字段
                //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
                //],
                JQExcludeCol: [1, 6],//导出execl排除的列
                //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
                //JQFields: ["""", """"],//追加的字段名
                url: '");

            
            #line 26 "..\..\Views\BussContractModel\list_cj.cshtml"
                 Write(Url.Action("json","BussContractModel",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=CJ',//请求的地址
                checkOnSelect: true,//是否包含check
                toolbar: '#BussContractModelPanel',//工具栏的容器ID
                columns: [[
        { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
		{ title: '合同模板名称', field: 'ConModelName', width: ""30%"", align: 'center', sortable: true },
		{ title: '合同模板备注', field: 'ConModelNote', width: ""36%"", align: 'center', sortable: true },
		{ title: '上传人', field: 'CreatorEmpName', width: ""10%"", align: 'center', sortable: true },
		{ title: '上传时间', field: 'CreationTime', width: ""10%"", align: 'center', sortable: true, formatter: JQ.tools.DateTimeFormatterByT },

                 { field: 'JQFiles', title: '附件', align: 'center', width: ""10%"", JQIsExclude: true, JQRefTable: 'BussContractModel' }
                ]]
            });
            $(""#JQSearch"").textbox({
                buttonText: '筛选',
                buttonIcon: 'fa fa-search',
                height: 25,
                prompt: '合同模板名称',
                queryOptions: { 'Key': 'ConModelName', 'Contract': 'like' },
                onClickButton: function () {
                    JQ.datagrid.searchGrid(
                        {
                            dgID: 'BussContractModelGrid',
                            loadingType: 'datagrid',
                            queryParams: null
                        });
                }
            });
        });
    </script>
");

WriteLiteral("    ");

            
            #line 56 "..\..\Views\BussContractModel\list_cj.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussContractModelGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussContractModelPanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n        &nbsp;\r\n        <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'上传开始\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'CreationTime\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n        至\r\n        <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'上传结束\'\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'CreationTime\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key:\'ConModelName\', Contract:\'like\'}\"");

WriteLiteral(">\r\n    </div>\r\n");

});

WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
