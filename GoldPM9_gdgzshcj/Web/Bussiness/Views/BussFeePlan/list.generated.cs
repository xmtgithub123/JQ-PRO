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
    
    #line 4 "..\..\Views\BussFeePlan\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussFeePlan/list.cshtml")]
    public partial class _Views_BussFeePlan_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_BussFeePlan_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\BussFeePlan\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        $(function () {\r\n            JQ.treegrid.treegrid({\r\n                J" +
"QButtonTypes: [\'add\', \'edit\', \'export\'],//需要显示的按钮\r\n                JQAddUrl: \'");

            
            #line 10 "..\..\Views\BussFeePlan\list.cshtml"
                      Write(Url.Action("add"));

            
            #line default
            #line hidden
WriteLiteral("\',//添加的路径\r\n                JQEditUrl: \'");

            
            #line 11 "..\..\Views\BussFeePlan\list.cshtml"
                       Write(Url.Action("edit"));

            
            #line default
            #line hidden
WriteLiteral("\',//编辑的路径\r\n                JQDelUrl: \'");

            
            #line 12 "..\..\Views\BussFeePlan\list.cshtml"
                      Write(Url.Action("del"));

            
            #line default
            #line hidden
WriteLiteral(@"',//删除的路径
                JQPrimaryID: 'Id',//主键的字段
                JQID: 'BussFeePlanGrid',//数据表格ID
                JQDialogTitle: '信息',//弹出窗体标题
                JQWidth: '768',//弹出窗体宽
                JQHeight: '100%',//弹出窗体高
                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                //JQCustomSearch: [  //自定义搜索的字段
                    //{ value: 'DepartmentName', key: '人员部门', type: 'string' }//type支持三种类型string,datetime,combox,默认为string,combox必须提供engname
                //],
                //JQExcludeCol: [1, 4, 10],//导出execl排除的列
                //JQMergeCells: { fields: ['DepartmentName', 'EmpName', 'EmpLogin'], Only: 'DepartmentName' },//当字段值相同时会自动的跨行(只对相邻有效)
                //JQEnableFilter: true,//是否启用表格字段检索，只支持当页数据
                //JQFields: ["""", """"],//追加的字段名
                url: '");

            
            #line 26 "..\..\Views\BussFeePlan\list.cshtml"
                 Write(Url.Action("json","BussFeePlan",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                toolb" +
"ar: \'#BussFeePlanPanel\',//工具栏的容器ID\r\n                columns: [[\r\n               " +
"   { field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t\t\t  \t" +
"\t{ title: \'自增\', field: \'Id\', width: 100, align: \'center\', sortable: true  },\r\n\t\t" +
"{ title: \'计划收款金额(元)\', field: \'FeeMoney\', width: 100, align: \'center\', sortable: " +
"true  },\r\n\t\t{ title: \'类型\', field: \'FeeType\', width: 100, align: \'center\', sortab" +
"le: true  },\r\n\t\t{ title: \'项目ID\', field: \'PorjID\', width: 100, align: \'center\', s" +
"ortable: true  },\r\n\t\t{ title: \'收款合同ID\', field: \'ConID\', width: 100, align: \'cent" +
"er\', sortable: true  },\t\t{ title: \'备注说明\', field: \'FeeNote\', width: 100, align: \'" +
"center\', sortable: true  },\r\n\t\t{ title: \'计划收款日期\', field: \'FeeDate\', width: 100, " +
"align: \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t" +
"\t{ title: \'是否完成\', field: \'IsFinished\', width: 100, align: \'center\', sortable: tr" +
"ue  },\r\n\t\t{ title: \'关联表ID\', field: \'FormTableID\', width: 100, align: \'center\', s" +
"ortable: true  },\r\n\t\t{ title: \'扩展类别1\', field: \'ColAttType1\', width: 100, align: " +
"\'center\', sortable: true  },\r\n\t\t{ title: \'扩展类别2\', field: \'ColAttType2\', width: 1" +
"00, align: \'center\', sortable: true  },\r\n\t\t{ title: \'扩展时间1\', field: \'ColAttDate1" +
"\', width: 100, align: \'center\', sortable: true , formatter: JQ.tools.DateTimeFor" +
"matterByT },\r\n\t\t{ title: \'扩展时间2\', field: \'ColAttDate2\', width: 100, align: \'cent" +
"er\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'扩" +
"展系数1\', field: \'ColAttFloat1\', width: 100, align: \'center\', sortable: true  },\r\n\t" +
"\t{ title: \'扩展系数2\', field: \'ColAttFloat2\', width: 100, align: \'center\', sortable:" +
" true  },\r\n\t\t{ title: \'最后修改时间\', field: \'LastModificationTime\', width: 100, align" +
": \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ ti" +
"tle: \'最后修改人ID\', field: \'LastModifierEmpId\', width: 100, align: \'center\', sortabl" +
"e: true  },\r\n\t\t{ title: \'最后修改人姓名\', field: \'LastModifierEmpName\', width: 100, ali" +
"gn: \'center\', sortable: true  },\r\n\t\t{ title: \'创建时间\', field: \'CreationTime\', widt" +
"h: 100, align: \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatterB" +
"yT },\r\n\t\t{ title: \'创建人ID\', field: \'CreatorEmpId\', width: 100, align: \'center\', s" +
"ortable: true  },\r\n\t\t{ title: \'创建人姓名\', field: \'CreatorEmpName\', width: 100, alig" +
"n: \'center\', sortable: true  },\r\n\t\t{ title: \'流程ID\', field: \'FlowID\', width: 100," +
" align: \'center\', sortable: true  },\r\n\t\t{ title: \'审批结束时间\', field: \'FlowTime\', wi" +
"dth: 100, align: \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatte" +
"rByT },\r\n\t\t{ title: \'代理人ID\', field: \'AgenEmpId\', width: 100, align: \'center\', so" +
"rtable: true  },\r\n\t\t{ title: \'代理人姓名\', field: \'AgenEmpName\', width: 100, align: \'" +
"center\', sortable: true  },\r\n\t\t{ title: \'创建部门\', field: \'CreatorDepId\', width: 10" +
"0, align: \'center\', sortable: true  },\r\n\t\t{ title: \'创建部门姓名\', field: \'CreatorDepN" +
"ame\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'删除人员\', field:" +
" \'DeleterEmpId\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'删除" +
"人员姓名\', field: \'DeleterEmpName\', width: 100, align: \'center\', sortable: true  },\r" +
"\n\t\t{ title: \'删除日期\', field: \'DeletionTime\', width: 100, align: \'center\', sortable" +
": true , formatter: JQ.tools.DateTimeFormatterByT },\r\n                 { field: " +
"\'JQFiles\', title: \'附件\', align: \'center\', width: 80, JQIsExclude: true, JQRefTabl" +
"e: \'userInfo\' }\r\n                ]]\r\n            });\r\n            $(\"#JQSearch\")" +
".textbox({\r\n                buttonText: \'筛选\',\r\n                buttonIcon: \'fa f" +
"a-search\',\r\n                height: 25,\r\n                prompt: \'筛选字段\',\r\n      " +
"          queryOptions: { \'Key\': \'FeeNote\', \'Contract\': \'like\' },\r\n             " +
"   onClickButton: function () {\r\n                    JQ.treegrid.searchGrid(\r\n  " +
"                      {\r\n                            dgID: \'BussFeePlanGrid\',\r\n " +
"                           loadingType: \'datagrid\',\r\n                           " +
" queryParams: null\r\n                        });\r\n                }\r\n            " +
"});\r\n        });\r\n    </script>\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"BussFeePlanGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"BussFeePlanPanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n\r\n        </span>\r\n\r\n        <input");

WriteLiteral(" id=\"JQSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" />\r\n\r\n    </div>\r\n");

});

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
