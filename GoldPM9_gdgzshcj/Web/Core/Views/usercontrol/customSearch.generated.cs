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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/usercontrol/customSearch.cshtml")]
    public partial class _Views_usercontrol_customSearch_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_usercontrol_customSearch_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<style>\r\n    .cByCS {\r\n        cursor: pointer;\r\n    }\r\n</style>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    var urlTreeJson = \"");

            
            #line 8 "..\..\Views\usercontrol\customSearch.cshtml"
                  Write(Url.Action("treejson", "basedata",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n    var urlCSD = \'");

            
            #line 9 "..\..\Views\usercontrol\customSearch.cshtml"
             Write(Url.Action("customSearchDialog"));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    var JQCS = {\r\n        changes: function (target) {\r\n            var $tr =" +
" $(target).parent().parent(),\r\n                $td = $tr.find(\"td:eq(4)\"),\r\n    " +
"            val = $tr.find(\"select[name=\'zdn\']\").val();\r\n            JQCS.gender" +
"Val($td, val, \'jgz\');\r\n        },\r\n        insertRow: function (target) {\r\n     " +
"       var $tr = $(target).parent().parent(),\r\n                ljfval = $tr.find" +
"(\"select[name=\'ljf\']\").val(),\r\n                zdnval = $tr.find(\"select[name=\'z" +
"dn\']\").val(),\r\n                ysfval = $tr.find(\"select[name=\'ysf\']\").val(),\r\n " +
"               json = JQCS.getJson(zdnval),\r\n                zdntext = json.key," +
"\r\n                jgzval = JQCS.getJGZVal($tr);\r\n            if (!JQ.tools.isJso" +
"n(jgzval) || !JQ.tools.isNotEmpty(jgzval.val) || !JQ.tools.isNotEmpty(jgzval.tex" +
"t)) {\r\n                JQ.dialog.info(\"请输入结果值!!!\");\r\n                return;\r\n  " +
"          }\r\n            JQCS.removeVal($tr);\r\n            var str = \"<tr style=" +
"\'text-align:center;\'>\"\r\n                    + \"<td><input name=\'ck\' type=\'checkb" +
"ox\' name=\'ck\'></td>\"\r\n                    + \"<td>\" + ljfval + \"</td>\"\r\n         " +
"           + \"<td>\" + zdntext + \"</td>\"\r\n                    + \"<td>\" + ysfval +" +
" \"</td>\"\r\n                    + \"<td>\" + jgzval.text + \"</td>\"\r\n                " +
"    + \"<td>未分组</td>\"\r\n                    + \"<td>\"\r\n                    + \"<inpu" +
"t type=\'hidden\' name=\'lfjval\' value=\'\" + ljfval + \"\' />\"\r\n                    + " +
"\"<input type=\'hidden\' name=\'zdntext\' value=\'\" + zdntext + \"\' />\"\r\n              " +
"      + \"<input type=\'hidden\' name=\'zdnval\' value=\'\" + zdnval + \"\' />\"\r\n        " +
"            + \"<input type=\'hidden\' name=\'ysfval\' value=\'\" + ysfval + \"\' />\"\r\n  " +
"                  + \"<input type=\'hidden\' name=\'jgztext\' value=\'\" + jgzval.text " +
"+ \"\' />\"\r\n                    + \"<input type=\'hidden\' name=\'jgzval\' value=\'\" + j" +
"gzval.val + \"\' />\"\r\n                    + \"<input type=\'hidden\' name=\'filedType\'" +
" value=\'\" + json.type + \"\' />\"\r\n                    + \"<span class=\'fa fa-trash " +
"cByCS\' onclick=\'JQCS.delRow(this)\' />&nbsp;&nbsp;&nbsp;\"\r\n                    + " +
"\"<span class=\'fa fa-pencil cByCS\' onclick=\'JQCS.dialogEdit(this)\' />\"\r\n         " +
"           + \"</td>\"\r\n                    + \"</tr>\"\r\n            $(\"#customTable" +
" tr:eq(0)\").after(str);\r\n\r\n        },\r\n        editRow: function (index, json) {" +
"\r\n            var str = \"<td><input name=\'ck\' type=\'checkbox\' name=\'ck\'></td>\"\r\n" +
"                    + \"<td>\" + json.ljfval + \"</td>\"\r\n                    + \"<td" +
">\" + json.zdntext + \"</td>\"\r\n                    + \"<td>\" + json.ysfval + \"</td>" +
"\"\r\n                    + \"<td>\" + json.jgztext + \"</td>\"\r\n                    + " +
"\"<td>未分组</td>\"\r\n                    + \"<td>\"\r\n                    + \"<input type" +
"=\'hidden\' name=\'lfjval\' value=\'\" + json.ljfval + \"\' />\"\r\n                    + \"" +
"<input type=\'hidden\' name=\'zdntext\' value=\'\" + json.zdntext + \"\' />\"\r\n          " +
"          + \"<input type=\'hidden\' name=\'zdnval\' value=\'\" + json.zdnval + \"\' />\"\r" +
"\n                    + \"<input type=\'hidden\' name=\'ysfval\' value=\'\" + json.ysfva" +
"l + \"\' />\"\r\n                    + \"<input type=\'hidden\' name=\'jgztext\' value=\'\" " +
"+ json.jgztext + \"\' />\"\r\n                    + \"<input type=\'hidden\' name=\'jgzva" +
"l\' value=\'\" + json.jgzval + \"\' />\"\r\n                    + \"<input type=\'hidden\' " +
"name=\'filedType\' value=\'\" + json.filedType + \"\' />\"\r\n                    + \"<spa" +
"n class=\'fa fa-trash cByCS\' onclick=\'JQCS.delRow(this)\' />&nbsp;&nbsp;&nbsp;\"\r\n " +
"                   + \"<span class=\'fa fa-pencil cByCS\' onclick=\'JQCS.dialogEdit(" +
"this)\' />\"\r\n                    + \"</td>\";\r\n            $(\"#customTable tr:eq(\" " +
"+ index + \")\").html(str);\r\n        },\r\n        delRow: function (target) {\r\n    " +
"        $(target).parent().parent().remove();\r\n        },\r\n        dialogEdit: f" +
"unction (target) {\r\n            var j = JQCS.getValues($(target).parent().parent" +
"());\r\n            JQ.dialog.dialog({\r\n                width: 450,\r\n             " +
"   height: \'60%\',\r\n                url: urlCSD + \'?ljf=\' + j.ljfval + \'&zdn=\' + " +
"j.zdnval + \'&ysf=\' + j.ysfval + \'&jgz=\' + j.jgzval + \'&index=\' + $(target).paren" +
"t().parent().index(),\r\n                iconCls: \'fa fa-pencil\',\r\n               " +
" title: \'编辑自定义搜索\'\r\n\r\n            });\r\n        },\r\n        getValues: function ($" +
"tr) {\r\n            var ljfval = $tr.find(\"input[name=\'lfjval\']\").val();\r\n       " +
"     var zdntext = $tr.find(\"input[name=\'zdntext\']\").val();\r\n            var zdn" +
"val = $tr.find(\"input[name=\'zdnval\']\").val();\r\n            var jgztext = $tr.fin" +
"d(\"input[name=\'jgztext\']\").val();\r\n            var jgzval = $tr.find(\"input[name" +
"=\'jgzval\']\").val();\r\n            var ysfval = $tr.find(\"input[name=\'ysfval\']\").v" +
"al();\r\n\r\n            return { ljfval: ljfval, zdntext: zdntext, zdnval: zdnval, " +
"jgztext: jgztext, jgzval: jgzval, ysfval: ysfval }\r\n        },\r\n        genderVa" +
"l: function ($td, val) {\r\n            var json = JQCS.getJson(val),\r\n           " +
"     control;\r\n            switch (json.type) {\r\n                case \"datetime\"" +
":\r\n                    control = $(\"<input>\", {\r\n                        \'id\': \'" +
"jgz\',\r\n                        \'name\': \'jgz\',\r\n                        \'type\': \'" +
"text\',\r\n                        \'width\': \'98%\',\r\n                        \'height" +
"\': \'30\',\r\n                        \'class\': \'easyui-datebox\'\r\n                   " +
" });\r\n                    break;\r\n                case \"combox\":\r\n              " +
"      var url = urlTreeJson + \"?engName=\" + json.engname;\r\n                    c" +
"ontrol = $(\"<select>\", {\r\n                        \'id\': \'jgz\',\r\n                " +
"        \'name\': \'jgz\',\r\n                        \'width\': \'98%\',\r\n               " +
"         \'height\': \'30\',\r\n                        \'class\': \'easyui-combotree\',\r\n" +
"                        \'data-options\': \"valueField:\'id\',textField:\'text\',url:\'\"" +
" + url + \"\',multiple:true\"\r\n                    });\r\n                    break;\r" +
"\n                default:\r\n                    control = $(\"<input>\", {\r\n       " +
"                 \'id\': \'jgz\',\r\n                        \'name\': \'jgz\',\r\n         " +
"               \'width\': \'98%\',\r\n                        \'height\': \'30\',\r\n       " +
"                 \'type\': \'text\',\r\n                        \'class\': \'easyui-textb" +
"ox\'\r\n                    });\r\n                    break;\r\n            }\r\n       " +
"     $td.html(\"\");\r\n            control.prependTo($td);\r\n            $.parser.pa" +
"rse($td);\r\n        },\r\n        getArr: function () {\r\n            return eval(\"(" +
"\" + $(\"#customerSearchHidden\").val() + \")\");\r\n        },\r\n        getJson: funct" +
"ion (val) {\r\n            var arr = JQCS.getArr();\r\n            for (var i = 0; i" +
" < arr.length; i++) {\r\n                if (arr[i].value == val) {\r\n             " +
"       return arr[i];\r\n                }\r\n            }\r\n            return null" +
";\r\n        },\r\n        getJGZVal: function ($t) {\r\n            var json = JQCS.g" +
"etJson($t.find(\"select[name=\'zdn\']\").val()),\r\n                $jgz = $t.find(\"#j" +
"gz\");\r\n            switch (json.type) {\r\n                case \"combox\":\r\n       " +
"             return {\r\n                        text: JQ.combotree.geCheckedTexts" +
"({ $tree: $jgz }),\r\n                        val: JQ.combotree.geCheckedVals({ $t" +
"ree: $jgz })\r\n                    };\r\n                    break;\r\n              " +
"  case \"datetime\":\r\n                    return {\r\n                        text: " +
"$jgz.textbox(\"options\").value,\r\n                        val: $jgz.textbox(\"optio" +
"ns\").value\r\n                    };\r\n                    break;\r\n                " +
"default:\r\n                    return {\r\n                        text: $jgz.textb" +
"ox(\"options\").value,\r\n                        val: $jgz.textbox(\"options\").value" +
"\r\n                    };\r\n                    break;\r\n            }\r\n        },\r" +
"\n        removeVal: function ($t) {\r\n            var json = JQCS.getJson($t.find" +
"(\"select[name=\'zdn\']\").val()),\r\n               $jgz = $t.find(\"#jgz\");\r\n        " +
"    switch (json.type) {\r\n                case \"combox\":\r\n                    $j" +
"gz.combotree(\'clear\');\r\n                    break;\r\n                case \"dateti" +
"me\":\r\n                    $jgz.datebox(\'setValue\', \'\');\r\n                    bre" +
"ak;\r\n                default:\r\n                    $jgz.textbox(\"setValue\", \'\')\r" +
"\n                    break;\r\n            }\r\n        },\r\n        groupRow: functi" +
"on () {\r\n            var maxNums = parseInt(JQCS.getMaxGroup(), 10) + 1;\r\n      " +
"      $(\":checkbox[name=\'ck\']:checked\").each(function () {\r\n                var " +
"$tr = $(this).parent().parent();\r\n                $tr.addClass(\"info\");\r\n       " +
"         $tr.find(\"td\").eq(5).text(\"组\" + maxNums);\r\n                $(this).remo" +
"veAttr(\"checked\");\r\n            });\r\n\r\n        },\r\n        cancelRroupRow: funct" +
"ion () {\r\n            $(\":checkbox[name=\'ck\']:checked\").each(function () {\r\n    " +
"            var $tr = $(this).parent().parent();\r\n                $tr.removeClas" +
"s(\"info\");\r\n                $tr.find(\"td\").eq(5).text(\"未分组\");\r\n                $" +
"(this).removeAttr(\"checked\");\r\n            });\r\n        },\r\n        getMaxGroup:" +
" function () {\r\n            var nums = 0;\r\n            $(\"#customTable\").find(\"t" +
"r:gt(0)\").each(function () {\r\n                var v = $(this).find(\"td:eq(5)\").t" +
"ext();\r\n                if (JQ.tools.isNotEmpty(v) && v != \'未分组\') {\r\n           " +
"         v = v.substring(1, v.length);\r\n                    if (!isNaN(v) && v >" +
" nums) {\r\n                        nums = v;\r\n                    }\r\n            " +
"    }\r\n            });\r\n            return nums;\r\n        },\r\n        parseType:" +
" function (str) {\r\n            var result = \'\';\r\n            switch (str) {\r\n   " +
"             case \"datetime\":\r\n                    result = \"Date\";\r\n           " +
"         break;\r\n                case \"combox\":\r\n                    result = \"I" +
"nt\";\r\n                    break;\r\n                default:\r\n                    " +
"result = \"String\";\r\n                    break;\r\n            }\r\n            retur" +
"n result;\r\n        },\r\n        customerSearch: function () {\r\n            var re" +
"sult = [], groupArr = [], arr = [];;\r\n            $(\"#customTable :checkbox[name" +
"=\'ck\']\").each(function () {\r\n                var $tr = $(this).parent().parent()" +
"\r\n                lfjval = $tr.find(\"input[name=\'lfjval\']\").val(),\r\n            " +
"    zdnval = $tr.find(\"input[name=\'zdnval\']\").val(),\r\n                ysfval = $" +
"tr.find(\"input[name=\'ysfval\']\").val(),\r\n                jgzval = $tr.find(\"input" +
"[name=\'jgzval\']\").val(),\r\n                filedType = $tr.find(\"input[name=\'file" +
"dType\']\").val();;\r\n                arr.push({\r\n                    lfjval: lfjva" +
"l,\r\n                    zdnval: zdnval,\r\n                    ysfval: ysfval,\r\n  " +
"                  jgzval: jgzval,\r\n                    group: $tr.find(\"td:eq(5)" +
"\").text(),\r\n                    filedType: filedType\r\n                });\r\n     " +
"       });\r\n            if (JQ.tools.isArray(arr)) {\r\n                for (var i" +
" = 0; i < arr.length; i++) {\r\n                    if (arr[i].group == \'未分组\') {\r\n" +
"                        result.push({\r\n                            isGroup: fals" +
"e, list: [{\r\n                                Key: arr[i].zdnval,\r\n              " +
"                  Contract: arr[i].ysfval,\r\n                                ljf:" +
" arr[i].lfjval,\r\n                                filedType: JQCS.parseType(arr[i" +
"].filedType),\r\n                                Value: arr[i].jgzval\r\n           " +
"                 }]\r\n                        });\r\n                    }\r\n       " +
"             else {\r\n                        if (groupArr.indexOf(arr[i].group) " +
"<= -1) {\r\n                            groupArr.push(arr[i].group);\r\n            " +
"            }\r\n                    }\r\n                }\r\n                if (JQ." +
"tools.isArray(groupArr)) {\r\n                    for (var i = 0; i < groupArr.len" +
"gth; i++) {\r\n                        var newArr = JQ.tools.arrFind(arr, \"group\"," +
" groupArr[i]);\r\n                        if (JQ.tools.isArray(newArr)) {\r\n       " +
"                     var newArr1 = [], groupLJF = newArr[0].lfjval;\r\n           " +
"                 for (var j = 0; j < newArr.length; j++) {\r\n                    " +
"            newArr1.push({\r\n                                    Key: newArr[j].z" +
"dnval,\r\n                                    Contract: newArr[j].ysfval,\r\n       " +
"                             ljf: (j <= 0 ? \"\" : newArr[j].lfjval),\r\n           " +
"                         filedType: JQCS.parseType(newArr[i][filedType]),\r\n     " +
"                               Value: newArr[j].jgzval\r\n                        " +
"        });\r\n                            }\r\n                            result.p" +
"ush({ isGroup: true, ljf: groupLJF, list: newArr1 });\r\n                        }" +
"\r\n                    }\r\n                }\r\n            }\r\n            var param" +
"es = JQ.tools.findDialogInfo(),\r\n                $dg;\r\n            if (JQ.tools." +
"isNotEmpty(parames.iframeID)) {\r\n                $dg = window.top.document.getEl" +
"ementById(parames.iframeID).contentWindow.$(\'#\' + parames.dgID);\r\n            }\r" +
"\n            else if (JQ.tools.isNotEmpty(parames.parentid)) {\r\n                " +
"$dg = window.top.$(\"#\" + parames.parentid).find(\'#\' + parames.dgID);\r\n          " +
"  }\r\n            else {\r\n                $dg = window.top.$(\'#\' + parames.dgID);" +
"\r\n            }\r\n            if ($dg && $dg != null && $dg.size() == 1) {\r\n     " +
"           switch (parames.loadingType) {\r\n                    case \'datagrid\':\r" +
"\n                        $dg.datagrid(\'load\', { queryInfo: JSON.stringify(result" +
") });\r\n                        break;\r\n                    case \'treegrid\':\r\n   " +
"                     $dg.treegrid(\'load\', { queryInfo: JSON.stringify(result) })" +
";\r\n                        break;\r\n                }\r\n                JQ.dialog." +
"dialogClose();\r\n            }\r\n            else {\r\n                alert(\'无法获取刷新" +
"源!!!\');\r\n            }\r\n        }\r\n    };\r\n    $(\'select[name=\"zdn\"]\').each(func" +
"tion (index) {\r\n        var arr = JQCS.getArr()\r\n        for (var i = 0; i < arr" +
".length; i++) {\r\n            $(this).append(\"<option value=\'\" + arr[i].value + \"" +
"\'>\" + arr[i].key + \"</option>\");\r\n        }\r\n    });\r\n    var $tr = $(\"#customTa" +
"ble\").find(\"tr:last\"),\r\n        $td = $tr.find(\"td:eq(4)\");\r\n    val = $tr.find(" +
"\"select[name=\'zdn\']\").val();\r\n    JQCS.genderVal($td, val);\r\n</script>\r\n<div");

WriteLiteral(" id=\"tbcustomerSearch\"");

WriteLiteral(" style=\"padding-bottom:3px; padding-top:3px;\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-magic\'\"");

WriteLiteral(" onclick=\"JQCS.groupRow()\"");

WriteLiteral(">组合</a>\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-gavel\'\"");

WriteLiteral(" onclick=\"JQCS.cancelRroupRow()\"");

WriteLiteral(">取消组合</a>\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-search\'\"");

WriteLiteral(" onclick=\"JQCS.customerSearch()\"");

WriteLiteral(">搜索</a>\r\n</div>\r\n<table");

WriteLiteral(" id=\"customTable\"");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n    <tr");

WriteLiteral(" style=\"text-align:center; background-color:#eaeaea;height:20px; border-color:#80" +
"8080;font-weight:bolder;\"");

WriteLiteral(">\r\n        <td");

WriteLiteral(" style=\"width:30px;\"");

WriteLiteral(">\r\n            选择\r\n        </td>\r\n        <td");

WriteLiteral(" style=\"width:60px;\"");

WriteLiteral(">\r\n            链接符\r\n        </td>\r\n        <td");

WriteLiteral(" style=\"width:70px;\"");

WriteLiteral(">\r\n            字段名\r\n        </td>\r\n        <td");

WriteLiteral(" style=\"width:60px;\"");

WriteLiteral(">\r\n            运处符\r\n        </td>\r\n        <td>\r\n            结果值\r\n        </td>\r\n" +
"        <td");

WriteLiteral(" style=\"width:70px;\"");

WriteLiteral(">\r\n            分组\r\n        </td>\r\n        <td");

WriteLiteral(" style=\"width:65px;\"");

WriteLiteral(">\r\n            操作\r\n        </td>\r\n    </tr>\r\n    <tr");

WriteLiteral(" style=\"text-align:center;\"");

WriteLiteral(">\r\n        <td></td>\r\n        <td>\r\n            <select");

WriteLiteral(" name=\"ljf\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"and\"");

WriteLiteral(">and</option>\r\n                <option");

WriteLiteral(" value=\"or\"");

WriteLiteral(">or</option>\r\n            </select>\r\n        </td>\r\n        <td>\r\n            <se" +
"lect");

WriteLiteral(" name=\"zdn\"");

WriteLiteral(" onchange=\"JQCS.changes(this)\"");

WriteLiteral("></select>\r\n        </td>\r\n        <td>\r\n            <select");

WriteLiteral(" name=\"ysf\"");

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

WriteLiteral(">in</option>\r\n            </select>\r\n        </td>\r\n        <td></td>\r\n        <t" +
"d></td>\r\n        <td>\r\n            <span");

WriteLiteral(" class=\'fa fa-save cByCS\'");

WriteLiteral(" onclick=\'JQCS.insertRow(this,0)\'");

WriteLiteral(" />\r\n        </td>\r\n    </tr>\r\n</table>\r\n<input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"customerSearchHidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 16423), Tuple.Create("\"", 16447)
            
            #line 385 "..\..\Views\usercontrol\customSearch.cshtml"
, Tuple.Create(Tuple.Create("", 16431), Tuple.Create<System.Object, System.Int32>(ViewBag.parames
            
            #line default
            #line hidden
, 16431), false)
);

WriteLiteral(" />\r\n");

        }
    }
}
#pragma warning restore 1591