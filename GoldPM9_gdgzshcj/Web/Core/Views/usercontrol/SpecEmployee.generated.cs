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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/usercontrol/SpecEmployee.cshtml")]
    public partial class _Views_usercontrol_SpecEmployee_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_usercontrol_SpecEmployee_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" JQPanel=\"dialogButtonPanel\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-search\'\"");

WriteLiteral(" id=\"btnSure\"");

WriteLiteral(">确定</a>\r\n    <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-trash\'\"");

WriteLiteral(" id=\"EmptyLnk\"");

WriteLiteral(">清除所有</a>\r\n</div>\r\n<div");

WriteLiteral(" id=\"cc\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" style=\"overflow: hidden\"");

WriteLiteral(" fit=\"true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'west\',split:false\"");

WriteLiteral(" style=\"width:220px;\"");

WriteLiteral(">\r\n        <ul");

WriteLiteral(" id=\"employeeLeftTree\"");

WriteLiteral("></ul>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\'\"");

WriteLiteral(" style=\"background:#eee;\"");

WriteLiteral(">\r\n        <ul");

WriteLiteral(" id=\"employeeRightTree\"");

WriteLiteral("></ul>\r\n    </div>\r\n</div>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    //清空
    $(""#EmptyLnk"").click(function () {
        debugger;
        var rootNodes = $(""#employeeRightTree"").tree(""getRoots"");
        var ids = [];
        for (var i = 0; i < rootNodes.length; i++) {
            ids.push(rootNodes[i].id);
        }
        $.each(ids, function (i, ne) {
            clickFun(ne, ""employeeRightTree"");
        })
    })

    $(""#btnSure"").click(function () {
        //获取本层dialog
        window.SpecEmps = [];
        var data = $(""#employeeRightTree"").tree(""getRoots"");
        
        $.each(data, function (i, e) {
            if (e.children == undefined) return true;
            if (e.children.length > 0) {
                $.each(e.children, function (ic, ec) {
                    window.SpecEmps.push({ SpecID: ec.SpecID, SpecName: ec.SpecName, EmpName: ec.text, EmpID: ec.EmpID });
                })
            }
        })
        if (window.SpecEmps.length > 0) {
            JQ.dialog.dialogClose();
        } else {
            JQ.dialog.show(""请至少选择一位！！！"");
        }

    })

    $(function () {
        showleftTree();
        showrightTree();
    })

    function showleftTree() {
        $('#employeeLeftTree').tree({
            url: '");

            
            #line 58 "..\..\Views\usercontrol\SpecEmployee.cshtml"
             Write(Url.Action("getSpecEmpList"));

            
            #line default
            #line hidden
WriteLiteral(@"',//open
            animate: true,
            onClick: function (node) {

                var parent = $(this).tree(""getParent"", node.target),
                  childs = $(this).tree(""getChildren"", node.target);
                if (childs.length == 0) {

                    clickFun(node.id, 'employeeLeftTree');
                }
                else {
                    if (node.state == ""open"") {
                        $(this).tree(""collapse"", node.target);
                    } else {
                        $(this).tree(""expand"", node.target);
                    }
                }
            },
            onLoadSuccess: function (node, data) {
                regditEvent(data, ""employeeLeftTree"");
            }
        });
    }

    function showrightTree() {
        $('#employeeRightTree').tree({
            url: '");

            
            #line 84 "..\..\Views\usercontrol\SpecEmployee.cshtml"
             Write(Url.Action("getSpecEmpList"));

            
            #line default
            #line hidden
WriteLiteral("?empids=");

            
            #line 84 "..\..\Views\usercontrol\SpecEmployee.cshtml"
                                                   Write(Html.Raw(ViewBag.empids));

            
            #line default
            #line hidden
WriteLiteral("\',//open\r\n            animate: true,\r\n            onClick: function (node) {\r\n   " +
"             var parent = $(this).tree(\"getParent\", node.target),\r\n             " +
"     childs = $(this).tree(\"getChildren\", node.target);\r\n                if (chi" +
"lds.length == 0) {\r\n                    clickFun(node.id, \'employeeRightTree\');\r" +
"\n                }\r\n                else {\r\n                    if (node.state =" +
"= \"open\") {\r\n                        $(this).tree(\"collapse\", node.target);\r\n   " +
"                 } else {\r\n                        $(this).tree(\"expand\", node.t" +
"arget);\r\n                    }\r\n                }\r\n            },\r\n            o" +
"nLoadSuccess: function (node, data) {\r\n                regditEvent(data, \"employ" +
"eeRightTree\");\r\n                //全部展开\r\n                $(\'#employeeRightTree\')." +
"tree(\"expandAll\");\r\n            }\r\n        });\r\n    }\r\n    //注册 鼠标浮动事件\r\n    func" +
"tion regditEvent(data, treeId) {\r\n        if (JQ.tools.isNotEmpty(data)) {\r\n    " +
"        var $grid = $(\"#\" + treeId);\r\n            var text, icon;\r\n            $" +
".each(data, function (i, e) {\r\n                var $NodedivID = $(\"#\" + e.domId)" +
";//行节点\r\n\r\n                //$NodedivID.attr(\'jq-employeetree\', \"{id:\" + e.id + \"" +
",text:\'\" + e.text + \"\',treeid:\'\" + treeId + \"\'}\");\r\n                $NodedivID.a" +
"ttr(\'jq-employeetree\', JSON.stringify({ \'id\': e.id, \'text\': e.text, \'treeid\': tr" +
"eeId }));\r\n                if (treeId == \'employeeLeftTree\') {\r\n                " +
"    text = \'全选\';\r\n                    icon = \'fa fa-check-circle\';\r\n            " +
"    }\r\n                else {\r\n                    text = \'移除\';\r\n               " +
"     icon = \'fa fa-trash\';\r\n                }\r\n\r\n                if (e.id.indexO" +
"f(\'T\') == 0) {\r\n                    $(\"<a>\", {\r\n                        \'class\':" +
" icon,\r\n                        \'func\': \'all\',\r\n                        style: \'" +
"float:right;margin-right:10px;display:none;height: 18px;padding: 0 2px;\',\r\n     " +
"                   text: text,\r\n                        click: function (e) {\r\n " +
"                           var j = eval(\"(\" + $(this).parent().attr(\"jq-employee" +
"tree\") + \")\");\r\n                            clickFun(j.id, treeId);\r\n           " +
"                 e.stopPropagation();\r\n                        }\r\n              " +
"      }).appendTo($NodedivID);\r\n                }\r\n            })\r\n\r\n           " +
" $(\"div[jq-employeetree]\").hover(\r\n                 function (e) {\r\n            " +
"         var j = eval(\"(\" + $(this).attr(\'jq-employeetree\') + \")\");\r\n           " +
"          var isok = j.id.indexOf(\'T\') == 0;\r\n                     if (isok) {\r\n" +
"                         $(this).find(\"a[func=\'all\']\").show(100);\r\n             " +
"        }\r\n\r\n                 },\r\n                 function (e) {\r\n             " +
"        var j = eval(\"(\" + $(this).attr(\'jq-employeetree\') + \")\");\r\n            " +
"         var isok = j.id.indexOf(\'T\') == 0;\r\n                     if (isok) {\r\n " +
"                        $(this).find(\"a[func=\'all\']\").hide(100);\r\n              " +
"       }\r\n                 }\r\n           );\r\n        }\r\n    }\r\n    //移除节点\r\n    f" +
"unction clickFun(nodeId, treeid) {\r\n\r\n        var $grid = $(\"#\" + treeid);\r\n    " +
"    var otherTreeid = treeid == \'employeeLeftTree\' ? \"employeeRightTree\" : \'empl" +
"oyeeLeftTree\';\r\n        appendNodes(nodeId, treeid, otherTreeid);\r\n\r\n\r\n        v" +
"ar CurrentNode = $grid.tree(\"find\", nodeId);\r\n        var Childs = $grid.tree(\"g" +
"etChildren\", CurrentNode.target);\r\n        if (Childs.length > 0) {\r\n           " +
" //父节点、子节点\r\n            $grid.tree(\"remove\", CurrentNode.target);\r\n        } els" +
"e {\r\n            var parentNode = $grid.tree(\"getParent\", CurrentNode.target);\r\n" +
"            $grid.tree(\"remove\", CurrentNode.target);\r\n            //判断是否还有子节点\r\n" +
"            if (parentNode == null) return;\r\n            var CChilds = $grid.tre" +
"e(\"getChildren\", parentNode.target);\r\n            if (CChilds.length == 0) {\r\n  " +
"              $grid.tree(\"remove\", parentNode.target);\r\n            }\r\n        }" +
"\r\n    }\r\n    //新增节点\r\n    function appendNodes(nodeId, treeid, addtreeid) {\r\n    " +
"    $addgrid = $(\"#\" + addtreeid);\r\n        $removegrid = $(\"#\" + treeid);\r\n\r\n  " +
"      var CurrentNode = $removegrid.tree(\"find\", nodeId);\r\n        //是否还有父节点\r\n  " +
"      var parentNode = $removegrid.tree(\"getParent\", CurrentNode.target);\r\n     " +
"   if (parentNode == null) {\r\n            //判断新增边 是否有父节点\r\n            var otherp" +
"arentNode = $addgrid.tree(\"find\", CurrentNode.id);\r\n            if (otherparentN" +
"ode == null) {\r\n                $addgrid.tree(\"append\", { data: [{ id: CurrentNo" +
"de.id, text: CurrentNode.text, SpecID: CurrentNode.SpecID, SpecName: CurrentNode" +
".SpecName, EmpID: CurrentNode.EmpID }] });\r\n                otherparentNode = $a" +
"ddgrid.tree(\"find\", CurrentNode.id);\r\n            }\r\n\r\n            var childs = " +
"$removegrid.tree(\"getChildren\", CurrentNode.target);\r\n            if (childs.len" +
"gth > 0) {\r\n                $.each(childs, function (i, e) {\r\n                  " +
"  var otherChild = $addgrid.tree(\"find\", e.id);\r\n                    if (otherCh" +
"ild == null) {\r\n                        $addgrid.tree(\"append\", { parent: otherp" +
"arentNode.target, data: [{ id: e.id, text: e.text, iconCls: e.iconCls, SpecID: e" +
".SpecID, SpecName: e.SpecName, EmpID: e.EmpID }] });\r\n                    }\r\n   " +
"             })\r\n            }\r\n\r\n        } else {\r\n            var otherparentN" +
"ode = $addgrid.tree(\"find\", parentNode.id);\r\n            if (otherparentNode == " +
"null) {\r\n                $addgrid.tree(\"append\", { data: [{ id: parentNode.id, t" +
"ext: parentNode.text, SpecID: parentNode.SpecID, SpecName: parentNode.SpecName, " +
"EmpID: parentNode.EmpID }] });\r\n                otherparentNode = $addgrid.tree(" +
"\"find\", parentNode.id);\r\n            }\r\n            $addgrid.tree(\"append\", { pa" +
"rent: otherparentNode.target, data: [{ id: CurrentNode.id, text: CurrentNode.tex" +
"t, iconCls: CurrentNode.iconCls, SpecID: CurrentNode.SpecID, SpecName: CurrentNo" +
"de.SpecName, EmpID: CurrentNode.EmpID }] });\r\n        }\r\n    }\r\n\r\n\r\n\r\n</script>");

        }
    }
}
#pragma warning restore 1591
