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
    
    #line 4 "..\..\Views\IsoForm\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoForm/list.cshtml")]
    public partial class _Views_IsoForm_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_IsoForm_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\IsoForm\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\t var requestUrl = \'");

            
            #line 7 "..\..\Views\IsoForm\list.cshtml"
               Write(Url.Action("json", "IsoForm",new { @area="Iso"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\IsoForm\list.cshtml"
                 Write(Url.Action("add","IsoForm",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\IsoForm\list.cshtml"
                  Write(Url.Action("edit", "IsoForm",new {@area="Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\IsoForm\list.cshtml"
                 Write(Url.Action("del", "IsoForm", new { @area = "Iso" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            JQ.treegrid.treegrid({\r\n                " +
"JQButtonTypes: [\'add\', \'edit\',\'del\', \'export\'],//需要显示的按钮\r\n                JQAddU" +
"rl: addUrl, //添加的路径\r\n                JQEditUrl: editUrl,//编辑的路径\r\n               " +
" JQDelUrl: delUrl, //删除的路径\r\n                JQPrimaryID: \'FormID\',//主键的字段\r\n     " +
"           JQID: \'IsoFormGrid\',//数据表格ID\r\n                JQDialogTitle: \'信息\',//弹" +
"出窗体标题\r\n                JQWidth: \'1024\',//弹出窗体宽\r\n                JQHeight: \'100%\'" +
",//弹出窗体高\r\n                JQLoadingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]" +
"\r\n                JQFields: [\"FormID\"],//追加的字段名\r\n                url: requestUrl" +
",//请求的地址\r\n                checkOnSelect: true,//是否包含check\r\n                toolb" +
"ar: \'#IsoFormPanel\',//工具栏的容器ID\r\n                columns: [[\r\n                  {" +
" field: \'ck\', align: \'center\', checkbox: true, JQIsExclude: true },\r\n\t\t\t\t  \t\t{ t" +
"itle: \'自增\', field: \'FormID\', width: 100, align: \'center\', sortable: true  },\t\t{ " +
"title: \'工程\', field: \'ProjID\', width: 100, align: \'center\', sortable: true  },\t\t{" +
" title: \'子项\', field: \'EngID\', width: 100, align: \'center\', sortable: true  },\t\t{" +
" title: \'阶段\', field: \'PhaseID\', width: 100, align: \'center\', sortable: true  },\t" +
"\t{ title: \'专业\', field: \'SpecID\', width: 100, align: \'center\', sortable: true  }," +
"\t\t{ title: \'任务分组\', field: \'TaskGroupID\', width: 100, align: \'center\', sortable: " +
"true  },\t\t{ title: \'调整节点ID\', field: \'TaskID\', width: 100, align: \'center\', sorta" +
"ble: true  },\t\t{ title: \'类别\', field: \'FormTypeID\', width: 100, align: \'center\', " +
"sortable: true  },\r\n\t\t{ title: \'表单日期\', field: \'FormDate\', width: 100, align: \'ce" +
"nter\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: " +
"\'表单名称\', field: \'FormName\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ " +
"title: \'原因\', field: \'FormReason\', width: 100, align: \'center\', sortable: true  }" +
",\r\n\t\t{ title: \'备注\', field: \'FormNote\', width: 100, align: \'center\', sortable: tr" +
"ue  },\r\n\t\t{ title: \'链接\', field: \'FormLinkURL\', width: 100, align: \'center\', sort" +
"able: true  },\r\n\t\t{ title: \'扩展类别\', field: \'ColAttType1\', width: 100, align: \'cen" +
"ter\', sortable: true  },\t\t{ title: \'扩展类别\', field: \'ColAttType2\', width: 100, ali" +
"gn: \'center\', sortable: true  },\t\t{ title: \'扩展类别\', field: \'ColAttType3\', width: " +
"100, align: \'center\', sortable: true  },\t\t{ title: \'扩展类别\', field: \'ColAttType4\'," +
" width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'扩展类别\', field: \'Col" +
"AttType5\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'扩展值\', fi" +
"eld: \'ColAttVal1\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'" +
"扩展值2\', field: \'ColAttVal2\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{" +
" title: \'扩展值\', field: \'ColAttVal3\', width: 100, align: \'center\', sortable: true " +
" },\r\n\t\t{ title: \'扩展值\', field: \'ColAttVal4\', width: 100, align: \'center\', sortabl" +
"e: true  },\r\n\t\t{ title: \'扩展值\', field: \'ColAttVal5\', width: 100, align: \'center\'," +
" sortable: true  },\r\n\t\t{ title: \'扩展时间1\', field: \'ColAttDate1\', width: 100, align" +
": \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ ti" +
"tle: \'扩展时间2\', field: \'ColAttDate2\', width: 100, align: \'center\', sortable: true " +
", formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'扩展时间3\', field: \'ColAt" +
"tDate3\', width: 100, align: \'center\', sortable: true , formatter: JQ.tools.DateT" +
"imeFormatterByT },\r\n\t\t{ title: \'扩展时间4\', field: \'ColAttDate4\', width: 100, align:" +
" \'center\', sortable: true , formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ tit" +
"le: \'扩展时间5\', field: \'ColAttDate5\', width: 100, align: \'center\', sortable: true ," +
" formatter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'扩展\', field: \'FormCtlXm" +
"l\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'关联表名\', field: \'" +
"RefTable\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'关联ID\', f" +
"ield: \'RefID\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'流程ID" +
"\', field: \'FlowID\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: " +
"\'审批结束时间\', field: \'FlowTime\', width: 100, align: \'center\', sortable: true , forma" +
"tter: JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'最后修改时间\', field: \'LastModific" +
"ationTime\', width: 100, align: \'center\', sortable: true , formatter: JQ.tools.Da" +
"teTimeFormatterByT },\r\n\t\t{ title: \'最后修改人ID\', field: \'LastModifierEmpId\', width: " +
"100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'最后修改人姓名\', field: \'LastModi" +
"fierEmpName\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'创建时间\'" +
", field: \'CreationTime\', width: 100, align: \'center\', sortable: true , formatter" +
": JQ.tools.DateTimeFormatterByT },\r\n\t\t{ title: \'创建人ID\', field: \'CreatorEmpId\', w" +
"idth: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'创建人姓名\', field: \'Crea" +
"torEmpName\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title: \'代理人ID\'" +
", field: \'AgenEmpId\', width: 100, align: \'center\', sortable: true  },\r\n\t\t{ title" +
": \'代理人姓名\', field: \'AgenEmpName\', width: 100, align: \'center\', sortable: true  }," +
"\r\n\t\t{ title: \'创建部门\', field: \'CreatorDepId\', width: 100, align: \'center\', sortabl" +
"e: true  },\r\n\t\t{ title: \'创建部门姓名\', field: \'CreatorDepName\', width: 100, align: \'c" +
"enter\', sortable: true  },\r\n                 { field: \'JQFiles\', title: \'附件\', al" +
"ign: \'center\', width: 80, JQIsExclude: true, JQRefTable: \'IsoForm\' }\r\n          " +
"      ]]\r\n            });\r\n            $(\"#JQSearch\").textbox({\r\n               " +
" buttonText: \'筛选\',\r\n                buttonIcon: \'fa fa-search\',\r\n               " +
" height: 25,\r\n                prompt: \'筛选字段\',\r\n                queryOptions: { \'" +
"Key\': \'FormName\', \'Contract\': \'like\' },\r\n                onClickButton: function" +
" () {\r\n                    JQ.treegrid.searchGrid(\r\n                        {\r\n " +
"                           dgID: \'IsoFormGrid\',\r\n                            loa" +
"dingType: \'datagrid\',\r\n                            queryParams: null\r\n          " +
"              });\r\n                }\r\n            });\r\n        });\r\n    </script" +
">\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"IsoFormGrid\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"IsoFormPanel\"");

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