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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/ProjectProgressInfoOrg.cshtml")]
    public partial class _Views_DesTask_ProjectProgressInfoOrg_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_ProjectProgressInfoOrg_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 22), Tuple.Create("\"", 61)
, Tuple.Create(Tuple.Create("", 29), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jointjs/rappid.min.css")
, 29), false)
);

WriteLiteral(" />\r\n\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(@">
    #paper {
        position: relative;
        width: 100%;
        height: 100%;
        overflow: auto;
    }

    .link .link-tools {
        display: none;
    }

    .link {
        pointer-events: none;
    }

    .element {
        cursor: initial;
    }

        .element .btn.des > text {
            cursor: pointer;
        }

         .element .btn.prt > text {
            cursor: pointer;
        } .element .btn.pub > text {
            cursor: pointer;
        }
</style>

<div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"border-bottom-width:1px;overflow:hidden\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"height:30px;padding:5px\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" style=\"float:left;margin-left:5px;\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" id=\"btnReloadMember\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-repeat\'\"");

WriteLiteral(" onclick=\"btnReloadMember()\"");

WriteLiteral(">刷新</a>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-search-minus\'\"");

WriteLiteral(" onclick=\"zoom(-0.1)\"");

WriteLiteral(">缩小</a>\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-search-plus\'\"");

WriteLiteral(" onclick=\"zoom(0.1)\"");

WriteLiteral(">放大</a>\r\n                显示方式：\r\n                <input");

WriteLiteral(" id=\"orgchart-direction\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" style=\"margin-right:10px;\"");

WriteLiteral(">&nbsp;</span>\r\n            </div>\r\n            <div");

WriteLiteral(" style=\"float:right;margin-right:5px;margin-top:7px;\"");

WriteLiteral(">\r\n                节点状态：\r\n                <span");

WriteLiteral(" class=\"node-example node-status-0-bg\"");

WriteLiteral(">未启动</span>\r\n                <span");

WriteLiteral(" class=\"node-example node-status-1-bg\"");

WriteLiteral(">已安排</span>\r\n                <span");

WriteLiteral(" class=\"node-example node-status-2-bg\"");

WriteLiteral(">进行中</span>\r\n                <span");

WriteLiteral(" class=\"node-example node-status-3-bg\"");

WriteLiteral(">已完成</span>\r\n                <span");

WriteLiteral(" class=\"node-example node-status-4-bg\"");

WriteLiteral(">已停止</span>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"paper\"");

WriteLiteral(" class=\"paper\"");

WriteLiteral("></div>\r\n    </div>\r\n</div>\r\n\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 2088), Tuple.Create("\"", 2125)
, Tuple.Create(Tuple.Create("", 2094), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jointjs/lodash.min.js")
, 2094), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 2145), Tuple.Create("\"", 2184)
, Tuple.Create(Tuple.Create("", 2151), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jointjs/backbone-min.js")
, 2151), false)
);

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 2204), Tuple.Create("\"", 2241)
, Tuple.Create(Tuple.Create("", 2210), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/jointjs/rappid.min.js")
, 2210), false)
);

WriteLiteral("></script>\r\n\r\n<script>\r\n    var _webconfig = {\r\n        serverUrl: \'");

            
            #line 66 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
               Write(Url.Content("~"));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n    };\r\n\r\n    var member = function (name, rank, other, background, prtbg, pub" +
"bg, arcbg, _data) {\r\n        console.log(arguments);\r\n        var cell = new joi" +
"nt.shapes.org.Member({\r\n            markup: other == \"*\" ?\r\n                [\r\n " +
"               \'<g class=\"rotatable\">\',\r\n                    \'<g class=\"scalable" +
"\">\',\r\n                        \'<rect class=\"card\" markupType=\"\' + _data.Type + \'" +
"\" markupId=\"\' + _data.Id + \'\"/>\',\r\n                    \'</g>\',\r\n                " +
"    \'<text class=\"nodename\" markupType=\"\' + _data.Type + \'\" markupId=\"\' + _data." +
"Id + \'\">\' + name + \'</text>\',\r\n                    \'<text class=\"noderank\" marku" +
"pType=\"\' + _data.Type + \'\" markupId=\"\' + _data.Id + \'\">\' + rank + \'</text>\',\r\n  " +
"                  \'<g class=\"btn des\"><rect class=\"des\" taskId=\"\' + _data.Id + \'" +
"\" /><text class=\"des\" taskId=\"\' + _data.Id + \'\">校审</text></g>\',\r\n               " +
"     \'<g class=\"btn prt\"><rect class=\"prt\" taskId=\"\' + _data.Id + \'\" /><text cla" +
"ss=\"prt\" taskId=\"\' + _data.Id + \'\">出版</text></g>\',\r\n                    \'<g clas" +
"s=\"btn pub\" style=\"display:none;\"><rect class=\"pub\" taskId=\"\' + _data.Id + \'\" />" +
"<text class=\"pub\"  taskId=\"\' + _data.Id + \'\">交付</text></g>\',\r\n                  " +
"  //\'<g class=\"btn arc\"><rect class=\"arc\"/><text class=\"arc\">归档</text></g>\',\r\n  " +
"              \'</g>\'\r\n                ].join(\'\') :\r\n                [\r\n         " +
"       \'<g class=\"rotatable\">\',\r\n                    \'<g class=\"scalable\">\',\r\n  " +
"                      \'<rect class=\"card\" markupType=\"\' + _data.Type + \'\" markup" +
"Id=\"\' + _data.Id + \'\"/>\',\r\n                    \'</g>\',\r\n                    \'<te" +
"xt class=\"nodename\" markupType=\"\' + _data.Type + \'\" markupId=\"\' + _data.Id + \'\">" +
"\' + name + \'</text>\',\r\n                    \'<text class=\"noderank\" markupType=\"\'" +
" + _data.Type + \'\" markupId=\"\' + _data.Id + \'\">\' + rank + \'</text>\',\r\n          " +
"          \'<text class=\"nodeother\" markupType=\"\' + _data.Type + \'\" markupId=\"\' +" +
" _data.Id + \'\">\' + other + \'</text>\',\r\n                \'</g>\'\r\n                ]" +
".join(\'\'),\r\n            size: { width: 200, height: 72 },\r\n            attrs: {\r" +
"\n                \'.card\': { fill: background, \'stroke-width\': 0 },\r\n            " +
"    \'.nodename\': { fill: \"#000\", \'font-size\': 16, \'font-weight\': 600, x: 5, y: 2" +
"0 },\r\n                \'.noderank\': { fill: \"#000\", \'font-size\': 12, x: 5, y: 40 " +
"},\r\n                \'.nodeother\': { fill: \"#000\", \'font-size\': 12, x: 5, y: 60 }" +
",\r\n                \'.btn>rect\': { height: 20, width: 40, rx: 2, ry: 2, fill: \'tr" +
"ansparent\', stroke: \'#333\', \'stroke-width\': 1 },\r\n                \'.btn.des\': { " +
"\'ref-x\': 5, \'ref-y\': 46, \'ref\': \'.card\' },\r\n                \'.btn.des>rect\': { f" +
"ill: background },\r\n                \'.btn.des>text\': { fill: \"#000\", \'font-size\'" +
": 12, \'font-weight\': 200, stroke: \"#000\", x: 9, y: 15 },\r\n                \'.btn." +
"prt\': { \'ref-x\': 55, \'ref-y\': 46, \'ref\': \'.card\' },\r\n                \'.btn.prt>r" +
"ect\': { fill: prtbg },\r\n                \'.btn.prt>text\': { fill: \"#000\", \'font-s" +
"ize\': 12, \'font-weight\': 0, stroke: \"#000\", x: 9, y: 15 },\r\n                \'.bt" +
"n.pub\': { \'ref-x\': 105, \'ref-y\': 46, \'ref\': \'.card\' },\r\n                \'.btn.pu" +
"b>rect\': { fill: pubbg },\r\n                \'.btn.pub>text\': { fill: \"#000\", \'fon" +
"t-size\': 12, \'font-weight\': 0, stroke: \"#000\", x: 9, y: 15 },\r\n                \'" +
".btn.arc\': { \'ref-x\': 155, \'ref-y\': 46, \'ref\': \'.card\' },\r\n                \'.btn" +
".arc>rect\': { fill: arcbg },\r\n                \'.btn.arc>text\': { fill: \"#000\", \'" +
"font-size\': 12, \'font-weight\': 0, stroke: \"#000\", x: 9, y: 15 },\r\n            }\r" +
"\n        });\r\n\r\n\r\n        return cell;\r\n    };\r\n\r\n    function link(source, targ" +
"et) {\r\n\r\n        //var cell = new joint.shapes.org.Arrow({\r\n        var cell = n" +
"ew joint.dia.Link({\r\n            source: { id: source.id },\r\n            target:" +
" { id: target.id }\r\n        });\r\n\r\n        return cell;\r\n    }\r\n\r\n    var graph " +
"= new joint.dia.Graph();\r\n\r\n    var paper = new joint.dia.Paper({\r\n        //el:" +
" $(\'#paper\'),\r\n        width: 1000,\r\n        height: 1000,\r\n        gridSize: 1," +
"\r\n        model: graph,\r\n        interactive: false,\r\n        defaultLink: new j" +
"oint.shapes.org.Arrow()\r\n    });\r\n\r\n    var paperScroller = new joint.ui.PaperSc" +
"roller({\r\n        paper: paper,\r\n        autoResizePaper: true\r\n    });\r\n\r\n    p" +
"aper.on(\'blank:pointerdown\', paperScroller.startPanning);\r\n    paperScroller.$el" +
".css({ width: \'100%\', height: \'100%\' }).appendTo(\'#paper\');\r\n    paperScroller.z" +
"oom(0);\r\n\r\n    var _zoom = 0.0;\r\n    function zoom(n) {\r\n        _zoom += (n);\r\n" +
"        if (_zoom < -0.5) {\r\n            _zoom = -0.5;\r\n            return;\r\n   " +
"     } else if (_zoom > 2) {\r\n            _zoom = 2;\r\n            return;\r\n     " +
"   }\r\n        //console.log(_zoom.toFixed(1));\r\n        paperScroller.zoom(n);\r\n" +
"    }\r\n\r\n    var graphLayout = new joint.layout.TreeLayout({\r\n        graph: gra" +
"ph,\r\n        direction: \'R\',\r\n        //gap: 80,\r\n        //siblingGap: 40\r\n    " +
"});\r\n\r\n    paper.on(\'cell:pointerup\', function (cellView, evt, x, y) {\r\n        " +
"if (V(evt.target).hasClass(\'des\')) {\r\n            // 点击校审按钮\r\n            var tas" +
"kId = V(evt.target).attr(\"taskId\");\r\n            JQ.dialog.dialog({\r\n           " +
"     //title: \"查看设计任务：[{0}]\".format(\"\"),\r\n                title: \"查看设计任务\",\r\n    " +
"            url: \'");

            
            #line 182 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
                 Write(Url.Action("TaskAttachProgress", "DesTask"));

            
            #line default
            #line hidden
WriteLiteral(@"?Id=' + taskId,
                width: 1200,
                height: 800,
                onClose: function () {

                }
            });
        } else if (V(evt.target).hasClass('prt')) {
            // 点击出版按钮
            var taskId = V(evt.target).attr(""taskId"");
            JQ.dialog.dialog({
                //title: ""查看设计任务：[{0}]"".format(""""),
                title: ""查看出版"",
                url: '");

            
            #line 195 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
                 Write(Url.Action("infoForPrint", "IsoFormProjectPublish", new { @area = "iso" }));

            
            #line default
            #line hidden
WriteLiteral(@"?Id=' + taskId,
                width: 240,
                height: 200,
                onClose: function () {

                }
            });

        } else if (V(evt.target).hasClass('pub')) {
            // 点击交付按钮
            var taskId = V(evt.target).attr(""taskId"");
            JQ.dialog.dialog({
                //title: ""查看设计任务：[{0}]"".format(""""),
                title: ""查看交付"",
                url: '");

            
            #line 209 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
                 Write(Url.Action("infoForPublish", "IsoFormProjectDeliver", new { @area = "iso" }));

            
            #line default
            #line hidden
WriteLiteral("?Id=\' + taskId,\r\n                width: 240,\r\n                height: 200,\r\n     " +
"           onClose: function () {\r\n\r\n                }\r\n            });\r\n       " +
" } else if (V(evt.target).hasClass(\'arc\')) {\r\n            // 点击归档按钮\r\n\r\n        }" +
" else if (V(evt.target).node.nodeName == \'text\') {\r\n            // 点击节点\r\n       " +
"     var markupType = V(evt.target).attr(\"markupType\");\r\n            var markupI" +
"d = V(evt.target).attr(\"markupId\");\r\n            //clickMember(markupType, marku" +
"pId);\r\n        } else if (V(evt.target).node.nodeName == \'rect\') {\r\n            " +
"// 点击节点\r\n            var markupType = V(evt.target).attr(\"markupType\");\r\n       " +
"     var markupId = V(evt.target).attr(\"markupId\");\r\n            //clickMember(m" +
"arkupType, markupId);\r\n        }\r\n    });\r\n\r\n    $(\'#orgchart-direction\').combob" +
"ox({\r\n        valueField: \'value\',\r\n        textField: \'content\',\r\n        data:" +
" [\r\n            { value: \'R\', content: \'从左到右\' },\r\n            { value: \'B\', cont" +
"ent: \'从上到下\' },\r\n        ],\r\n        onSelect: function (option) {\r\n            /" +
"/console.log(option);\r\n            _.invoke(graph.getElements(), \'set\', \'directi" +
"on\', option.value);\r\n            graphLayout.layout();\r\n            paperScrolle" +
"r.centerContent();\r\n        }\r\n    });\r\n    $(\'#orgchart-direction\').combobox(\'s" +
"etValue\', \'R\');\r\n\r\n\r\n    var bgcolor = [\r\n        \"#d8d8d8\",\r\n        \"#f3d963\"," +
"\r\n        \"#5adcbb\",\r\n        \"#6fc0e8\",\r\n        \"#ea8a8a\"\r\n    ];\r\n\r\n    var m" +
"embers = [];\r\n\r\n    var connections = [];\r\n\r\n    var ii = 0;\r\n\r\n    function rel" +
"oadMember() {\r\n\r\n        $(\"#btnReloadMember\").linkbutton().linkbutton(\"disable\"" +
");\r\n\r\n        members = [];\r\n        connections = [];\r\n        ii = 0;\r\n\r\n     " +
"   $.ajax({\r\n            url: \'");

            
            #line 272 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
             Write(Url.Action("ProjectProgressInfoOrgGetListJson", "DesTask", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?projID=");

            
            #line 272 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
                                                                                                          Write(ViewBag.projID);

            
            #line default
            #line hidden
WriteLiteral("&taskGroupId=");

            
            #line 272 "..\..\Views\DesTask\ProjectProgressInfoOrg.cshtml"
                                                                                                                                      Write(ViewBag.taskGroupId);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            type: \"POST\",\r\n            data: {},\r\n            success: functi" +
"on (result) {\r\n                var rows = JSON.parse(result).rows;\r\n            " +
"    if (rows.length > 0) {\r\n                    //console.log(rows);\r\n          " +
"          var row = Enumerable.From(rows).Where(\"x => x.ParentId == \'P0\'\").First" +
"OrDefault();\r\n                    //console.log(row);\r\n                    membe" +
"rs.push(member(GetMemberName(row), GetFinishDate(row), GetMemberOther(row), GetM" +
"emberColor(row), GetMemberPrtbg(row), GetMemberPubbg(row), GetMemberArcbg(row), " +
"row).position(100, 350))\r\n                    CreateMemberLink(rows, row, ii);\r\n" +
"                }\r\n\r\n                graph.resetCells(members.concat(connections" +
"));\r\n                _.invoke(graph.getElements(), \'set\', \'direction\', $(\'#orgch" +
"art-direction\').combobox(\'getValue\'));\r\n                graphLayout.layout();\r\n " +
"               paperScroller.centerContent();\r\n\r\n                setTimeout(func" +
"tion () {\r\n                    $(\"#btnReloadMember\").linkbutton(\"enable\");\r\n    " +
"            }, 500);\r\n            }\r\n        });\r\n    }\r\n\r\n    function CreateMe" +
"mberLink(rows, root, fi) {\r\n        Enumerable.From(rows).Where(function (x) { r" +
"eturn x.ParentId == root.RefId }).OrderBy(\"x => x.Order1\").ThenBy(\"x => x.Order2" +
"\")\r\n            .ForEach(function (row, index) {\r\n                ii++;\r\n       " +
"         members.push(member(GetMemberName(row), GetFinishDate(row), GetMemberOt" +
"her(row), GetMemberColor(row), GetMemberPrtbg(row), GetMemberPubbg(row), GetMemb" +
"erArcbg(row), row));\r\n                connections.push(link(members[fi], members" +
"[ii]));\r\n                CreateMemberLink(rows, row, ii);\r\n            }\r\n      " +
"  );\r\n    }\r\n\r\n    function GetMemberName(row) {\r\n        if (row.Type == \"Proje" +
"ct\") {\r\n            return \"{0}\".format(\"项目\");\r\n        } else {\r\n            re" +
"turn \"{0}\".format(row.Name);\r\n        }\r\n    }\r\n\r\n    function GetFinishDate(row" +
") {\r\n        var rd = \"\";\r\n        var DatePlanFinish = Date.jsonStringToDate(ro" +
"w.DatePlanFinish);\r\n        var DateActualFinish = Date.jsonStringToDate(row.Dat" +
"eActualFinish);\r\n        if (DateActualFinish.getFullYear() > 1900) {\r\n         " +
"   rd = (row.EmpId == 0 ? \"\" : row.EmpName + \" | \") + \"实际完成：{0}\".format(DateActu" +
"alFinish.format(\"yyyy-MM-dd\"));\r\n        } else if (DatePlanFinish.getFullYear()" +
" > 1900) {\r\n            rd = (row.EmpId == 0 ? \"\" : row.EmpName + \" | \") + \"计划完成" +
"：{0}\".format(DatePlanFinish.format(\"yyyy-MM-dd\"));\r\n        } else {\r\n          " +
"  rd = (row.EmpId == 0 ? \"\" : row.EmpName + \" | \");\r\n        }\r\n        return r" +
"d;\r\n    }\r\n\r\n    function GetMemberOther(row) {\r\n        if (row.Type == \"Projec" +
"t\") {\r\n            return row.Name.getByteLengthString(24, \"...\");\r\n        } el" +
"se if (row.Type == \"TaskPath\" || row.Type == \"Task\") {\r\n            return \"*\"\r\n" +
"        } else {\r\n            return \"\";\r\n        }\r\n    }\r\n\r\n    function GetMe" +
"mberColor(row) {\r\n        var rd = \"\";\r\n        var DatePlanFinish = Date.jsonSt" +
"ringToDate(row.DatePlanFinish);\r\n        var DateActualFinish = Date.jsonStringT" +
"oDate(row.DateActualFinish);\r\n        switch (row.Status) {\r\n            case 0:" +
"\r\n                rd = bgcolor[row.Status];\r\n                break;\r\n           " +
" case 1:\r\n                rd = bgcolor[row.Status];\r\n                break;\r\n   " +
"         case 2:\r\n                rd = bgcolor[row.Status];\r\n                bre" +
"ak;\r\n            case 3:\r\n                rd = bgcolor[row.Status];\r\n           " +
"     break;\r\n            case 4:\r\n                rd = bgcolor[row.Status];\r\n   " +
"             break;\r\n        }\r\n        //}\r\n        return rd;\r\n    }\r\n\r\n    fu" +
"nction GetMemberPrtbg(row) {\r\n        var rd = \"\";\r\n        var DateForPrint = D" +
"ate.jsonStringToDate(row.DateForPrint);\r\n        if (DateForPrint.getFullYear() " +
"== 1900) {\r\n            rd = bgcolor[0];\r\n        } else {\r\n            rd = bgc" +
"olor[3];\r\n        }\r\n        return rd;\r\n    }\r\n\r\n    function GetMemberPubbg(ro" +
"w) {\r\n        var rd = \"\";\r\n        var DateForPublish = Date.jsonStringToDate(r" +
"ow.DateForPublish);\r\n        if (DateForPublish.getFullYear() == 1900) {\r\n      " +
"      rd = bgcolor[0];\r\n        } else {\r\n            rd = bgcolor[3];\r\n        " +
"}\r\n        return rd;\r\n    }\r\n\r\n    function GetMemberArcbg(row) {\r\n        var " +
"rd = \"\";\r\n        var DateForArchive = Date.jsonStringToDate(row.DateForArchive)" +
";\r\n        if (DateForArchive.getFullYear() == 1900) {\r\n            rd = bgcolor" +
"[0];\r\n        } else {\r\n            rd = bgcolor[3];\r\n        }\r\n        return " +
"rd;\r\n    }\r\n\r\n    function btnReloadMember() {\r\n        reloadMember();\r\n    }\r\n" +
"\r\n    $(function () {\r\n        reloadMember();\r\n    });\r\n\r\n    function clickMem" +
"ber(active, id) {\r\n        var text = \"节点详情\";\r\n        var url = \"\";\r\n        sw" +
"itch (active) {\r\n            case \"Project\":\r\n                //  项目查看页面\r\n      " +
"          url = _webconfig.serverUrl + \'Project/Project/ProjInfoOnlyRead?id=\' + " +
"id;\r\n                JQ.dialog.dialog({\r\n                    title: \'{0}\'.format" +
"(text),\r\n                    url: url,\r\n                    width: 1024,\r\n      " +
"              height: 900,\r\n                    onClose: function () {\r\n        " +
"            }\r\n                });\r\n                break;\r\n            case \"Pr" +
"ojPhase\":\r\n                //  阶段查看页面\r\n                url = _webconfig.serverUr" +
"l + \'Design/DesTask/TaskPhaseInfo?id=\' + id;\r\n                JQ.dialog.dialog({" +
"\r\n                    title: \'{0}\'.format(text),\r\n                    url: url,\r" +
"\n                    width: 500,\r\n                    height: 500,\r\n            " +
"        onClose: function () {\r\n                    }\r\n                });\r\n    " +
"            break;\r\n            case \"TaskGroup\":\r\n                //  分组查看页面\r\n " +
"               url = _webconfig.serverUrl + \'Design/DesTask/TaskGroupInfo?id=\' +" +
" id;\r\n                JQ.dialog.dialog({\r\n                    title: \'{0}\'.forma" +
"t(text),\r\n                    url: url,\r\n                    width: 500,\r\n      " +
"              height: 500,\r\n                    onClose: function () {\r\n        " +
"            }\r\n                });\r\n                break;\r\n            case \"Ta" +
"skSpec\":\r\n                //  专业查看页面\r\n                url = _webconfig.serverUrl" +
" + \'Design/DesTask/TaskSpecInfo?id=\' + id;\r\n                JQ.dialog.dialog({\r\n" +
"                    title: \'{0}\'.format(text),\r\n                    url: url,\r\n " +
"                   width: 500,\r\n                    height: 500,\r\n              " +
"      onClose: function () {\r\n                    }\r\n                });\r\n      " +
"          break;\r\n            case \"TaskPath\":\r\n            case \"Task\":\r\n      " +
"          //  任务查看页面\r\n                url = _webconfig.serverUrl + \'Design/DesTa" +
"sk/TaskInfo?from=Remind&Id=\' + id;\r\n                JQ.dialog.dialog({\r\n        " +
"            title: \'{0}\'.format(text),\r\n                    url: url,\r\n         " +
"           width: 1200,\r\n                    height: 900,\r\n                    o" +
"nClose: function () {\r\n                    }\r\n                });\r\n             " +
"   break;\r\n            case \"MutiSign\":\r\n                //  会签查看页面\r\n           " +
"     url = _webconfig.serverUrl + \'Design/DesMutiSign/Edit?from=Remind&Id=\' + id" +
";\r\n                JQ.dialog.dialog({\r\n                    title: \'{0}\'.format(t" +
"ext),\r\n                    url: url,\r\n                    width: 800,\r\n         " +
"           height: 800,\r\n                    onClose: function () {\r\n           " +
"         }\r\n                });\r\n                break;\r\n        }\r\n        retu" +
"rn btn;\r\n    }\r\n\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
