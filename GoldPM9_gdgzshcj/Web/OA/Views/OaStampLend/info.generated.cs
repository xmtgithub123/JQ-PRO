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
    
    #line 2 "..\..\Views\OaStampLend\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaStampLend/info.cshtml")]
    public partial class _Views_OaStampLend_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaStampLend>
    {
        public _Views_OaStampLend_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'OaStampLendForm\',//formid提交需要用到\r\n  " +
"      buttonTypes: ");

            
            #line 6 "..\..\Views\OaStampLend\info.cshtml"
                Write(Html.Raw(ViewBag.Permission));

            
            #line default
            #line hidden
WriteLiteral(@" ,//默认按钮
        //buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });

    $('#StampID').combogrid({
        panelWidth: 500,
        panelHeight: 300,
        value: '");

            
            #line 17 "..\..\Views\OaStampLend\info.cshtml"
           Write(Model.StampID);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        idField: \'Id\',\r\n        textField: \'StampName\',\r\n        url: \'");

            
            #line 20 "..\..\Views\OaStampLend\info.cshtml"
         Write(Url.Action("FilterJson", "OaStampUse", new { @area = "OA" }));

            
            #line default
            #line hidden
WriteLiteral("\',//请求的地址\r\n        editable: true,\r\n        pagination: true,           //是否分页\r\n " +
"       rownumbers: true,           //序号\r\n        collapsible: false,         //是" +
"否可折叠的\r\n        prompt: \'请选择用章\',\r\n        fit: true,\r\n        method: \'post\',\r\n  " +
"      columns: [[\r\n            { title: \'用章名称\', field: \'StampName\', width: 300, " +
"align: \'left\' },\r\n        ]],\r\n        keyHandler: {\r\n            up: function (" +
") {               //【向上键】押下处理\r\n                var selected = $(\'#StampID\').comb" +
"ogrid(\'grid\').datagrid(\'getSelected\');\r\n                if (selected) {\r\n       " +
"             var index = $(\'#StampID\').combogrid(\'grid\').datagrid(\'getRowIndex\'," +
" selected);\r\n                    if (index > 0) {\r\n                        $(\'#S" +
"tampID\').combogrid(\'grid\').datagrid(\'selectRow\', index - 1);\r\n                  " +
"  }\r\n                }\r\n                else {\r\n                    var rows = $" +
"(\'#StampID\').combogrid(\'grid\').datagrid(\'getRows\');\r\n                    $(\'#Sta" +
"mpID\').combogrid(\'grid\').datagrid(\'selectRow\', rows.length - 1);\r\n              " +
"  }\r\n            },\r\n            down: function () {             //【向下键】押下处理\r\n  " +
"              //取得选中行\r\n                var selected = $(\'#StampID\').combogrid(\'g" +
"rid\').datagrid(\'getSelected\');\r\n                if (selected) {\r\n               " +
"     var index = $(\'#StampID\').combogrid(\'grid\').datagrid(\'getRowIndex\', selecte" +
"d);\r\n                    if (index < $(\'#StampID\').combogrid(\'grid\').datagrid(\'g" +
"etData\').rows.length - 1) {\r\n                        $(\'#StampID\').combogrid(\'gr" +
"id\').datagrid(\'selectRow\', index + 1);\r\n                    }\r\n                }" +
" else {\r\n                    $(\'#StampID\').combogrid(\'grid\').datagrid(\'selectRow" +
"\', 0);\r\n                }\r\n            },\r\n            enter: function () {     " +
"        //【回车键】押下处理\r\n                var row = $(\'#StampID\').combogrid(\'grid\').d" +
"atagrid(\'getSelected\');\r\n                $(\'#StampID\').combogrid(\'hidePanel\');\r\n" +
"            },\r\n            query: function (Words) {     //【动态搜索】处理\r\n          " +
"      //设置查询参数\r\n                var queryPara = $(\'#StampID\').combogrid(\"grid\")." +
"datagrid(\'options\').queryParams;\r\n                queryPara.Words = Words;\r\n    " +
"            $(\'#StampID\').combogrid(\"grid\").datagrid(\'options\').queryParams = qu" +
"eryPara;\r\n                //重新加载\r\n                var pager = $(\'#StampID\').comb" +
"ogrid(\'grid\').datagrid(\'getPager\');\r\n                pager.pagination(\'refresh\')" +
";\r\n                pager.pagination(\'select\', 1);\r\n                $(\'#StampID\')" +
".combogrid(\"grid\").datagrid(\"reload\");\r\n                //$(\'#StampID\').combogri" +
"d(\"setValue\", Words);                    //将查询条件存入隐藏域\r\n                //$(\'#Wor" +
"ds\').val(Words);\r\n            }\r\n        },\r\n        onLoadSuccess: function () " +
"{\r\n            if ($(\"#StampID\").combogrid(\'getValue\') == \"0\") {\r\n              " +
"  $(this).combogrid(\'setValue\', \"\");\r\n            }\r\n        }\r\n    });\r\n</scrip" +
"t>\r\n");

            
            #line 82 "..\..\Views\OaStampLend\info.cshtml"
 using (Html.BeginForm("save", "OaStampLend", FormMethod.Post, new { @area = "Oa", @id = "OaStampLendForm"}))
{
    
            
            #line default
            #line hidden
            
            #line 84 "..\..\Views\OaStampLend\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 84 "..\..\Views\OaStampLend\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">印章名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"OaStampUseId\"");

WriteLiteral(" id=\"OaStampUseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3971), Tuple.Create("\"", 4000)
            
            #line 90 "..\..\Views\OaStampLend\info.cshtml"
  , Tuple.Create(Tuple.Create("", 3979), Tuple.Create<System.Object, System.Int32>(ViewBag.OaStampUseId
            
            #line default
            #line hidden
, 3979), false)
);

WriteLiteral(" />\r\n                <select");

WriteLiteral(" id=\"StampID\"");

WriteLiteral(" name=\"StampID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-combogrid\"");

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral("></select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">借用日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampLendDate\"");

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4350), Tuple.Create("\"", 4378)
            
            #line 95 "..\..\Views\OaStampLend\info.cshtml"
                                                , Tuple.Create(Tuple.Create("", 4358), Tuple.Create<System.Object, System.Int32>(Model.StampLendDate
            
            #line default
            #line hidden
, 4358), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请理由</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampLendNote\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true,readonly:true\"");

WriteLiteral(" validtype=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4669), Tuple.Create("\"", 4697)
            
            #line 102 "..\..\Views\OaStampLend\info.cshtml"
                                                                                              , Tuple.Create(Tuple.Create("", 4677), Tuple.Create<System.Object, System.Int32>(Model.StampLendNote
            
            #line default
            #line hidden
, 4677), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">归还人姓名</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampReturnEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4967), Tuple.Create("\"", 5000)
            
            #line 109 "..\..\Views\OaStampLend\info.cshtml"
                                                                        , Tuple.Create(Tuple.Create("", 4975), Tuple.Create<System.Object, System.Int32>(Model.StampReturnEmpName
            
            #line default
            #line hidden
, 4975), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">归还日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampFactReturnDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5239), Tuple.Create("\"", 5273)
            
            #line 113 "..\..\Views\OaStampLend\info.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 5247), Tuple.Create<System.Object, System.Int32>(Model.StampFactReturnDate
            
            #line default
            #line hidden
, 5247), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CreatorEmpName\"");

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5518), Tuple.Create("\"", 5547)
            
            #line 120 "..\..\Views\OaStampLend\info.cshtml"
                                                 , Tuple.Create(Tuple.Create("", 5526), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 5526), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CreationTime\"");

WriteLiteral(" data-options=\"readonly:true\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5760), Tuple.Create("\"", 5787)
            
            #line 124 "..\..\Views\OaStampLend\info.cshtml"
                                               , Tuple.Create(Tuple.Create("", 5768), Tuple.Create<System.Object, System.Int32>(Model.CreationTime
            
            #line default
            #line hidden
, 5768), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        ");

WriteLiteral("\r\n    </table>\r\n");

            
            #line 143 "..\..\Views\OaStampLend\info.cshtml"

}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591