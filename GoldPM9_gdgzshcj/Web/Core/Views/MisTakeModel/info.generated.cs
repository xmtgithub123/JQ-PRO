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
    
    #line 2 "..\..\Views\MisTakeModel\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/MisTakeModel/info.cshtml")]
    public partial class _Views_MisTakeModel_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseLog>
    {
        public _Views_MisTakeModel_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'BaseLogForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
        });
    });
</script>
");

            
            #line 15 "..\..\Views\MisTakeModel\info.cshtml"
 using (Html.BeginForm("save", "BaseLog", FormMethod.Post, new { @area = "Core", @id = "BaseLogForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\MisTakeModel\info.cshtml"
Write(Html.HiddenFor(m => m.BaseLogID));

            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\MisTakeModel\info.cshtml"
                                     

            
            #line default
            #line hidden
WriteLiteral("<table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n    <tr>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">人员ID</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 23 "..\..\Views\MisTakeModel\info.cshtml"
       Write(BaseData.getBase(new Params()
            {
            controlName = "BaseLogEmpID",
            isRequired = true,
            engName = "department",
            width = "98%",
            ids = Model.BaseLogEmpID.ToString()
            }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">日志日期</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1193), Tuple.Create("\"", 1219)
            
            #line 34 "..\..\Views\MisTakeModel\info.cshtml"
                                , Tuple.Create(Tuple.Create("", 1201), Tuple.Create<System.Object, System.Int32>(Model.BaseLogDate
            
            #line default
            #line hidden
, 1201), false)
);

WriteLiteral(" />\r\n        </td>\r\n    </tr>\r\n\r\n    <tr>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">记录IP</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogIP\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1456), Tuple.Create("\"", 1480)
            
            #line 41 "..\..\Views\MisTakeModel\info.cshtml"
                                                            , Tuple.Create(Tuple.Create("", 1464), Tuple.Create<System.Object, System.Int32>(Model.BaseLogIP
            
            #line default
            #line hidden
, 1464), false)
);

WriteLiteral(" />\r\n        </td>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">日志类别</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogTypeID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1674), Tuple.Create("\"", 1702)
            
            #line 45 "..\..\Views\MisTakeModel\info.cshtml"
                                        , Tuple.Create(Tuple.Create("", 1682), Tuple.Create<System.Object, System.Int32>(Model.BaseLogTypeID
            
            #line default
            #line hidden
, 1682), false)
);

WriteLiteral(" />\r\n        </td>\r\n    </tr>\r\n\r\n    <tr>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">引用表名称</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogRefTable\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1917), Tuple.Create("\"", 1947)
            
            #line 52 "..\..\Views\MisTakeModel\info.cshtml"
                                     , Tuple.Create(Tuple.Create("", 1925), Tuple.Create<System.Object, System.Int32>(Model.BaseLogRefTable
            
            #line default
            #line hidden
, 1925), false)
);

WriteLiteral(" />\r\n        </td>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">引用表ID</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogRefID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2141), Tuple.Create("\"", 2168)
            
            #line 56 "..\..\Views\MisTakeModel\info.cshtml"
                                       , Tuple.Create(Tuple.Create("", 2149), Tuple.Create<System.Object, System.Int32>(Model.BaseLogRefID
            
            #line default
            #line hidden
, 2149), false)
);

WriteLiteral(" />\r\n        </td>\r\n    </tr>\r\n\r\n    <tr>\r\n        <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">日志内容</th>\r\n        <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" name=\"BaseLogRefHTML\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1073741823]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2388), Tuple.Create("\"", 2417)
            
            #line 63 "..\..\Views\MisTakeModel\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 2396), Tuple.Create<System.Object, System.Int32>(Model.BaseLogRefHTML
            
            #line default
            #line hidden
, 2396), false)
);

WriteLiteral(" />\r\n        </td>\r\n    </tr> \r\n</table>\r\n");

            
            #line 67 "..\..\Views\MisTakeModel\info.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
