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
    
    #line 2 "..\..\Views\BaseEmpAgen\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BaseEmpAgen/info.cshtml")]
    public partial class _Views_BaseEmpAgen_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseEmpAgen>
    {
        public _Views_BaseEmpAgen_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'BaseEmpAgenForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
    JQ.combobox.selQtEmp({
        id: 'ToEmpID'
    });
    //常用combobox
    JQ.combobox.common({
        id: 'AgenFlowRefTable',
        toolbar: '#tbFlow',//工具栏的容器ID
        url: '");

            
            #line 19 "..\..\Views\BaseEmpAgen\info.cshtml"
          Write(Url.Action("GetModelList", "PubFlow", new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
        panelWidth: 550,
        panelHeight: 320,
        valueField: 'ModelRefTable',
        textField: 'ModelName',
        //disabled: true,
        multiple: true,
        pagination: true,//是否分页
        JQSearch: {
            id: 'fullNameSearchFlow',
            prompt: '流程名称',
            $panel: $(""#tbFlow"")
        },
        columns: [[
                     { field: 'Id', hidden: true },
                     { title: '模版名称', field: 'ModelName', width: 120, align: 'center', sortable: true },
                     { title: '关联表名', field: 'ModelRefTable', width: 160, align: 'center', sortable: true }]],
    });

    JQ.combobox.common({
        id: 'AgenMenu',
        //toolbar: '#tbMenu',//工具栏的容器ID
        url: '");

            
            #line 41 "..\..\Views\BaseEmpAgen\info.cshtml"
         Write(Url.Action("PermitTopjson", "menu",new {@area="Core", @OrderLen=2 }));

            
            #line default
            #line hidden
WriteLiteral(@"',
        panelWidth: 550,
        panelHeight: 320,
        valueField: 'MenuNameEng',
        textField: 'MenuName',
        editable: false,
        //disabled:true,
        multiple: true,
        pagination: false,//是否分页
        //JQSearch: {
        //    id: 'fullNameSearchMenu',
        //    prompt: '菜单名称'
        //},
        columns: [[
                   { field: 'MenuNameEng', hidden: true },
                   { title: '菜单名称', field: 'MenuName', width: 250, align: 'left' },
                 ]],
    });

    //默认combox 不能编写
</script>
");

            
            #line 62 "..\..\Views\BaseEmpAgen\info.cshtml"
 using (Html.BeginForm("save", "BaseEmpAgen", FormMethod.Post, new { @area = "Core", @id = "BaseEmpAgenForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\BaseEmpAgen\info.cshtml"
Write(Html.HiddenFor(m => m.BaseEmpAgenID));

            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\BaseEmpAgen\info.cshtml"
                                         

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FromEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2498), Tuple.Create("\"", 2524)
            
            #line 69 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 2506), Tuple.Create<System.Object, System.Int32>(Model.FromEmpName
            
            #line default
            #line hidden
, 2506), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">代理人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"ToEmpID\"");

WriteLiteral(" name=\"ToEmpID\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 2668), Tuple.Create("\"", 2692)
            
            #line 73 "..\..\Views\BaseEmpAgen\info.cshtml"
, Tuple.Create(Tuple.Create("", 2678), Tuple.Create<System.Object, System.Int32>(Model.ToEmpID
            
            #line default
            #line hidden
, 2678), false)
);

WriteLiteral(" data-options=\"required:true,editable: false\"");

WriteLiteral("></select>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"AgenType\"");

WriteLiteral(" name=\"AgenType\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" data-options=\"editable:false,valueField:\'label\',textField: \'value\',data: [{label" +
": \'0\',value: \'全部权限\'},{label: \'1\',value: \'菜单权限\'},{label: \'2\',value: \'流程权限\'}]\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3144), Tuple.Create("\"", 3167)
            
            #line 79 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                                                                                                                                                                             , Tuple.Create(Tuple.Create("", 3152), Tuple.Create<System.Object, System.Int32>(Model.AgenType
            
            #line default
            #line hidden
, 3152), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral("></th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                \r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            " +
"<th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托流程</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"AgenFlowRefTable\"");

WriteLiteral(" name=\"AgenFlowRefTable\"");

WriteLiteral(" data-options=\"multiple:true,multiline:true\"");

WriteLiteral(" style=\"width:98%; height:40px;\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3575), Tuple.Create("\"", 3606)
            
            #line 89 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                                                                   , Tuple.Create(Tuple.Create("", 3583), Tuple.Create<System.Object, System.Int32>(Model.AgenFlowRefTable
            
            #line default
            #line hidden
, 3583), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托菜单</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"AgenMenu\"");

WriteLiteral(" name=\"AgenMenu\"");

WriteLiteral(" data-options=\"multiple:true,multiline:true\"");

WriteLiteral(" style=\"width:98%; height:40px;\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3896), Tuple.Create("\"", 3919)
            
            #line 95 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                                                   , Tuple.Create(Tuple.Create("", 3904), Tuple.Create<System.Object, System.Int32>(Model.AgenMenu
            
            #line default
            #line hidden
, 3904), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托开始日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateLower\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,16]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4179), Tuple.Create("\"", 4203)
            
            #line 101 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 4187), Tuple.Create<System.Object, System.Int32>(Model.DateLower
            
            #line default
            #line hidden
, 4187), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托结束日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateUpper\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,16]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4434), Tuple.Create("\"", 4458)
            
            #line 105 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 4442), Tuple.Create<System.Object, System.Int32>(Model.DateUpper
            
            #line default
            #line hidden
, 4442), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">委托备注</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"AgenNote\"");

WriteLiteral(" style=\"width:98%; height:60px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4745), Tuple.Create("\"", 4768)
            
            #line 111 "..\..\Views\BaseEmpAgen\info.cshtml"
                                                                                , Tuple.Create(Tuple.Create("", 4753), Tuple.Create<System.Object, System.Int32>(Model.AgenNote
            
            #line default
            #line hidden
, 4753), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"tbMenu\"");

WriteLiteral(" style=\"padding:5px;height:auto; display:none;\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchMenu\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'MenuName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"tbFlow\"");

WriteLiteral(" style=\"padding:5px;height:auto; display:none;\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchFlow\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'ModelName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

            
            #line 121 "..\..\Views\BaseEmpAgen\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591