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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/usercontrol/customSearchDialog.cshtml")]
    public partial class _Views_usercontrol_customSearchDialog_cshtml : System.Web.Mvc.WebViewPage<Core.Controllers.CSDialog>
    {
        public _Views_usercontrol_customSearchDialog_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral(@"<script>
    var JQCS = JQCS || {};
    JQCS.changesDialog = function (target) {
        var $td = $(""#csdTD""),
            val = $(""#customTableByDialog"").find(""select[name='zdn']"").val();
        JQCS.genderVal($td, val);
    };
    JQCS.saveDialog = function () {
        var $t = $(""#customTableByDialog""),
        ljfval = $t.find(""select[name='ljf']"").val(),
        zdnval = $t.find(""select[name='zdn']"").val(),
        ysfval = $t.find(""select[name='ysf']"").val(),
        json = JQCS.getJson(zdnval),
        zdntext = json.key,
        jgzJosn = JQCS.getJGZVal($t, 'zdn');
        JQCS.editRow(");

            
            #line 17 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                Write(Model.index);

            
            #line default
            #line hidden
WriteLiteral(@", { ljfval: ljfval, zdnval: zdnval, zdntext: zdntext, ysfval: ysfval, jgztext: jgzJosn.text, jgzval: jgzJosn.val,filedType:json.type });
        JQ.dialog.dialogClose();
    };
    JQCS.setJGZVal = function ($t) {
        var json = JQCS.getJson($t.find(""select[name='zdn']"").val()),
            $jgz = $t.find(""#jgz"");
        switch (json.type) {
            case ""combox"":
                $jgz.combotree('setValues', [");

            
            #line 25 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                        Write(Model.jgz);

            
            #line default
            #line hidden
WriteLiteral("]);\r\n                break;\r\n            case \"datetime\":\r\n                $jgz.d" +
"atebox(\'setValue\', \'");

            
            #line 28 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                     Write(Model.jgz);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n                break;\r\n            default:\r\n                $jgz.textbox(\'" +
"setValue\', \'");

            
            #line 31 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                     Write(Model.jgz);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n                break;\r\n        }\r\n    };\r\n    JQCS.setDialogValues = functi" +
"on () {\r\n        var $t = $(\"#customTableByDialog\");\r\n        $t.find(\"select[na" +
"me=\'ljf\']\").val(\'");

            
            #line 37 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                      Write(Model.ljf);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n        $t.find(\"select[name=\'zdn\']\").val(\'");

            
            #line 38 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                      Write(Model.zdn);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n        $t.find(\"select[name=\'ysf\']\").val(JQ.tools.htmlDecode(\'");

            
            #line 39 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                                                          Write(Model.ysf);

            
            #line default
            #line hidden
WriteLiteral(@"'));
    };

    JQ.form.submitInit({});
    $('#customTableByDialog select[name=""zdn""]').each(function (index) {
        var arr = JQCS.getArr()
        for (var i = 0; i < arr.length; i++) {
            $(this).append(""<option value='"" + arr[i].value + ""'>"" + arr[i].key + ""</option>"");
        }
    });
    JQCS.genderVal($(""#csdTD""), '");

            
            #line 49 "..\..\Views\usercontrol\customSearchDialog.cshtml"
                            Write(Model.zdn);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n    JQCS.setDialogValues();\r\n    setTimeout(function () {\r\n        JQCS.setJ" +
"GZVal($(\"#customTableByDialog\"));\r\n    }, 100);\r\n</script>\r\n<div");

WriteLiteral(" JQPanel=\"dialogButtonPanel\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-save\'\"");

WriteLiteral(" onclick=\"JQCS.saveDialog()\"");

WriteLiteral(">确定</a>\r\n</div>\r\n<table");

WriteLiteral(" id=\"customTableByDialog\"");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n    <tr>\r\n        <td");

WriteLiteral(" style=\"width:90px;\"");

WriteLiteral(">\r\n            链接符\r\n        </td>\r\n        <td>\r\n            <select");

WriteLiteral(" name=\"ljf\"");

WriteLiteral(" style=\"width:75%\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"and\"");

WriteLiteral(">and</option>\r\n                <option");

WriteLiteral(" value=\"or\"");

WriteLiteral(">or</option>\r\n            </select>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        " +
"<td>\r\n            字段名\r\n        </td>\r\n        <td>\r\n            <select");

WriteLiteral(" name=\"zdn\"");

WriteLiteral(" onchange=\"JQCS.changesDialog(this)\"");

WriteLiteral(" style=\"width:75%\"");

WriteLiteral("></select>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <td>\r\n            运处符\r\n  " +
"      </td>\r\n        <td>\r\n            <select");

WriteLiteral(" name=\"ysf\"");

WriteLiteral(" style=\"width:75%\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"=\"");

WriteLiteral(">=</option>\r\n                <option");

WriteLiteral(" value=\">=\"");

WriteLiteral(">>=</option>\r\n                <option");

WriteLiteral(" value=\">\"");

WriteLiteral(">></option>\r\n                <option");

WriteLiteral(" value=\"<=\"");

WriteLiteral("><=</option>\r\n                <option");

WriteLiteral(" value=\"<\"");

WriteLiteral("><</option>\r\n                <option");

WriteLiteral(" value=\"like\"");

WriteLiteral(">like</option>\r\n                <option");

WriteLiteral(" value=\"in\"");

WriteLiteral(">in</option>\r\n            </select>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        " +
"<td>\r\n            结果值\r\n        </td>\r\n        <td");

WriteLiteral(" id=\"csdTD\"");

WriteLiteral("></td>\r\n    </tr>\r\n</table>");

        }
    }
}
#pragma warning restore 1591
