﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Decision/SynthesizeDecision.cshtml")]
    public partial class _Views_Decision_SynthesizeDecision_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Decision_SynthesizeDecision_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Decision\SynthesizeDecision.cshtml"
  
    Layout = null;

            
            #line default
            #line hidden
WriteLiteral("\r\n<html>\r\n<head>\r\n    <meta");

WriteLiteral(" charset=\"UTF-8\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" http-equiv=\"X-UA-Compatible\"");

WriteLiteral(" content=\"IE=edge\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"description\"");

WriteLiteral(" content=\"\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1\"");

WriteLiteral(">\r\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width,minimum-scale=1.0,maximum-scale=1.0,user-scalable=no" +
"\"");

WriteLiteral(" />\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 428), Tuple.Create("\"", 499)
            
            #line 12 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 435), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/fontawesome/css/font-awesome.min.css")
            
            #line default
            #line hidden
, 435), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 548), Tuple.Create("\"", 595)
            
            #line 13 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 555), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/projhome.css")
            
            #line default
            #line hidden
, 555), false)
);

WriteLiteral(" />\r\n    <title></title>\r\n</head>\r\n<body");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"height: 40px; background-color: #F8F8F8; position: relative; overflow: hi" +
"dden; border-bottom: 1px solid #D2D2D2;\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" style=\"position: absolute; bottom: 0px;\"");

WriteLiteral(">\r\n            <ul");

WriteLiteral(" id=\"divTabHead\"");

WriteLiteral(" class=\"jq_tab_header\"");

WriteLiteral("></ul>\r\n        </div>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"divTabContent\"");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(" style=\"border-bottom: 1px solid #D2D2D2;\"");

WriteLiteral("></div>\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1118), Tuple.Create("\"", 1165)
            
            #line 25 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1124), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jquery.min.js")
            
            #line default
            #line hidden
, 1124), false)
);

WriteLiteral("></script>\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1212), Tuple.Create("\"", 1266)
            
            #line 26 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1218), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/jquery.easyui.min.js")
            
            #line default
            #line hidden
, 1218), false)
);

WriteLiteral("></script>\r\n    <link");

WriteAttribute("href", Tuple.Create(" href=\"", 1288), Tuple.Create("\"", 1345)
            
            #line 27 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1295), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/themes/base/easyui.css")
            
            #line default
            #line hidden
, 1295), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n    <script");

WriteAttribute("src", Tuple.Create(" src=\"", 1379), Tuple.Create("\"", 1422)
            
            #line 28 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1385), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/JQ/tab.js")
            
            #line default
            #line hidden
, 1385), false)
);

WriteLiteral("></script>\r\n    <link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1461), Tuple.Create("\"", 1513)
            
            #line 29 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1468), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/dhtmlx.css")
            
            #line default
            #line hidden
, 1468), false)
);

WriteLiteral(" />\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 1553), Tuple.Create("\"", 1603)
            
            #line 30 "..\..\Views\Decision\SynthesizeDecision.cshtml"
, Tuple.Create(Tuple.Create("", 1559), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/dhtmlx/dhtmlx.js")
            
            #line default
            #line hidden
, 1559), false)
);

WriteLiteral("></script>\r\n\r\n\r\n</body>\r\n</html>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    window.tabID = \"");

            
            #line 37 "..\..\Views\Decision\SynthesizeDecision.cshtml"
                Write(Request.QueryString["a1b2c3wjpid"] ?? "");

            
            #line default
            #line hidden
WriteLiteral("\";\r\n    window.top.updateTitleByID(window.tabID, \"合同分析\");\r\n    $.parser.onComplet" +
"e = function () {\r\n        debugger;\r\n        var tabs = [{ name: \"contractAmoun" +
"t\", title: \"合同大小数量月度分析\", baseUrl: \"");

            
            #line 41 "..\..\Views\Decision\SynthesizeDecision.cshtml"
                                                                         Write(Url.Action("ContractDecision", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\" }];\r\n         tabs.push({ name: \"subcontract\", title: \"合同收费付款分析\", baseUrl: \"");

            
            #line 42 "..\..\Views\Decision\SynthesizeDecision.cshtml"
                                                                   Write(Url.Action("ContractFeeDecision", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\" });\r\n        // tabs.push({ name: \"contract\", title: \"合同总额分类比例分析\", baseUrl: \"");

            
            #line 43 "..\..\Views\Decision\SynthesizeDecision.cshtml"
                                                                    Write(Url.Action("list", "busscontract", new { @area = "bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\" });\r\n         tabs.push({ name: \"design\", title: \"合同分类分析\", baseUrl: \"");

            
            #line 44 "..\..\Views\Decision\SynthesizeDecision.cshtml"
                                                            Write(Url.Action("ContractTypeDecision", "Decision", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral("\" });\r\n      \r\n         var cTab = new Tab({\r\n             head: document.getElem" +
"entById(\"divTabHead\"),\r\n             body: document.getElementById(\"divTabConten" +
"t\"),\r\n             tabs: tabs,\r\n             onSelected: function (tab) {\r\n     " +
"            debugger;\r\n                 reloadTab(cTab, tab);\r\n             }\r\n " +
"        });\r\n         var toSelect;\r\n         var lastChoosed = null;\r\n     }\r\n\r" +
"\n     function reloadTab(cTab, tab) {\r\n         var url = getTablUrl(tab, { proj" +
"ID: 1 });\r\n         //switch (tab.name) {\r\n         //    case \"contractAmount\":" +
"\r\n         //        url = getTablUrl(tab, { projID: 1 });\r\n         //        b" +
"reak;\r\n         //    case \"subcontract\":\r\n         //        url = getTablUrl(t" +
"ab, { projID: 1 });\r\n         //        break;\r\n\r\n         //}\r\n         if (url" +
" && url != tab.url) {\r\n             tab.url = url;\r\n             cTab.refreshSel" +
"ected();\r\n         }\r\n     }\r\n\r\n     function getTablUrl(tab, params) {\r\n       " +
"  var str = \"\";\r\n         var index = 0;\r\n         params.from = \"projectcenter\"" +
";\r\n         for (var i in params) {\r\n             if (index == 0 && (tab.baseUrl" +
" && tab.baseUrl.indexOf(\"?\") == -1)) {\r\n                 str += \"?\";\r\n          " +
"   }\r\n             else {\r\n                 str += \"&\";\r\n             }\r\n       " +
"      index++;\r\n             str += i + \"=\" + params[i];\r\n         }\r\n         s" +
"witch (tab.name) {\r\n             case \"contractAmount\":\r\n             case \"cont" +
"ract\":\r\n             case \"subcontract\":\r\n             case \"design\":\r\n         " +
"    case \"quality\":\r\n             case \"event\":\r\n             case \"archive\":\r\n " +
"            case \"gantt\":\r\n             case \"progress\":\r\n                 //cas" +
"e \"info\":\r\n                 return tab.baseUrl + str;\r\n                 break;\r\n" +
"         }\r\n     }\r\n\r\n     window.redirect = function (projectID, taskGroupID) {" +
"\r\n         //var data=$(\"#\r\n     }\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
