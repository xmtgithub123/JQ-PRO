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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/basedata/info.cshtml")]
    public partial class _Views_basedata_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BaseData>
    {
        public _Views_basedata_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral(@"<script>
    JQ.form.submitInit({
        formid: 'ajaxform',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
    $('#ParentID').combotree({
        url: '");

            
            #line 12 "..\..\Views\basedata\info.cshtml"
         Write(Url.Action("girdjson", "basedata", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("?isContains=true&baseID=");

            
            #line 12 "..\..\Views\basedata\info.cshtml"
                                                                                            Write(ViewBag.parentid);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        required: true,\r\n        onLoadSuccess: function (node, data) {\r\n    " +
"        if (\'");

            
            #line 15 "..\..\Views\basedata\info.cshtml"
            Write(ViewBag.parentid);

            
            #line default
            #line hidden
WriteLiteral("\' > 0) {\r\n                $(\'#ParentID\').combotree(\'setValue\', \'");

            
            #line 16 "..\..\Views\basedata\info.cshtml"
                                                 Write(ViewBag.parentid);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            }\r\n        }\r\n    });\r\n</script>\r\n");

            
            #line 21 "..\..\Views\basedata\info.cshtml"
 using (Html.BeginForm("save", "basedata", FormMethod.Post, new { @id = "ajaxform" }))
{
    
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\basedata\info.cshtml"
Write(Html.HiddenFor(m => m.BaseID));

            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\basedata\info.cshtml"
                                  

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-hover table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                上级节点：\r\n            </th>\r\n            <td>\r\n                <i" +
"nput");

WriteLiteral(" name=\"ParentID\"");

WriteLiteral(" id=\"ParentID\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:80%\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1119), Tuple.Create("\"", 1144)
            
            #line 30 "..\..\Views\basedata\info.cshtml"
                     , Tuple.Create(Tuple.Create("", 1127), Tuple.Create<System.Object, System.Int32>(ViewBag.parentid
            
            #line default
            #line hidden
, 1127), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(" style=\"width: 100px;\"");

WriteLiteral(">\r\n                数据名称：\r\n            </th>\r\n            <td>\r\n                <i" +
"nput");

WriteLiteral(" name=\"BaseName\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1401), Tuple.Create("\"", 1424)
            
            #line 38 "..\..\Views\basedata\info.cshtml"
                  , Tuple.Create(Tuple.Create("", 1409), Tuple.Create<System.Object, System.Int32>(Model.BaseName
            
            #line default
            #line hidden
, 1409), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                扩展属性1：\r\n            </th>\r\n            <td>\r\n                <" +
"input");

WriteLiteral(" name=\"BaseAtt1\"");

WriteLiteral(" id=\"urlParam\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1663), Tuple.Create("\"", 1686)
            
            #line 46 "..\..\Views\basedata\info.cshtml"
   , Tuple.Create(Tuple.Create("", 1671), Tuple.Create<System.Object, System.Int32>(Model.BaseAtt1
            
            #line default
            #line hidden
, 1671), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                扩展属性2：\r\n            </th>\r\n            <td>\r\n                <" +
"input");

WriteLiteral(" name=\"BaseAtt2\"");

WriteLiteral(" id=\"urlParam\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1925), Tuple.Create("\"", 1948)
            
            #line 54 "..\..\Views\basedata\info.cshtml"
   , Tuple.Create(Tuple.Create("", 1933), Tuple.Create<System.Object, System.Int32>(Model.BaseAtt2
            
            #line default
            #line hidden
, 1933), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                扩展属性3：\r\n            </th>\r\n            <td>\r\n                <" +
"input");

WriteLiteral(" name=\"BaseAtt3\"");

WriteLiteral(" id=\"urlParam\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2187), Tuple.Create("\"", 2210)
            
            #line 62 "..\..\Views\basedata\info.cshtml"
   , Tuple.Create(Tuple.Create("", 2195), Tuple.Create<System.Object, System.Int32>(Model.BaseAtt3
            
            #line default
            #line hidden
, 2195), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                扩展属性4：\r\n            </th>\r\n            <td>\r\n                <" +
"input");

WriteLiteral(" name=\"BaseAtt4\"");

WriteLiteral(" id=\"urlParam\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2449), Tuple.Create("\"", 2472)
            
            #line 70 "..\..\Views\basedata\info.cshtml"
   , Tuple.Create(Tuple.Create("", 2457), Tuple.Create<System.Object, System.Int32>(Model.BaseAtt4
            
            #line default
            #line hidden
, 2457), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"info\"");

WriteLiteral(">\r\n                扩展属性5：\r\n            </th>\r\n            <td>\r\n                <" +
"input");

WriteLiteral(" name=\"BaseAtt5\"");

WriteLiteral(" id=\"urlParam\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2711), Tuple.Create("\"", 2734)
            
            #line 78 "..\..\Views\basedata\info.cshtml"
   , Tuple.Create(Tuple.Create("", 2719), Tuple.Create<System.Object, System.Int32>(Model.BaseAtt5
            
            #line default
            #line hidden
, 2719), false)
);

WriteLiteral(" style=\"width:95%\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n    </table>\r\n");

            
            #line 83 "..\..\Views\basedata\info.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591