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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/ProjectProgressInfo.cshtml")]
    public partial class _Views_DesTask_ProjectProgressInfo_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_ProjectProgressInfo_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"ProjectProgressInfoTab\"");

WriteLiteral(" class=\"easyui-tabs\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" title=\"节点进度\"");

WriteAttribute("href", Tuple.Create(" href=\"", 223), Tuple.Create("\"", 334)
            
            #line 11 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
, Tuple.Create(Tuple.Create("", 230), Tuple.Create<System.Object, System.Int32>(Url.Action("ProjectProgressInfoOrg", "DesTask")
            
            #line default
            #line hidden
, 230), false)
, Tuple.Create(Tuple.Create("", 278), Tuple.Create("?projID=", 278), true)
            
            #line 11 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
         , Tuple.Create(Tuple.Create("", 286), Tuple.Create<System.Object, System.Int32>(ViewBag.projID
            
            #line default
            #line hidden
, 286), false)
, Tuple.Create(Tuple.Create("", 301), Tuple.Create("&taskGroupId=", 301), true)
            
            #line 11 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
                                     , Tuple.Create(Tuple.Create("", 314), Tuple.Create<System.Object, System.Int32>(ViewBag.taskGroupId
            
            #line default
            #line hidden
, 314), false)
);

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" title=\"策划进度\"");

WriteAttribute("href", Tuple.Create(" href=\"", 369), Tuple.Create("\"", 481)
            
            #line 12 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
, Tuple.Create(Tuple.Create("", 376), Tuple.Create<System.Object, System.Int32>(Url.Action("ProjectProgressInfoPlan", "DesTask")
            
            #line default
            #line hidden
, 376), false)
, Tuple.Create(Tuple.Create("", 425), Tuple.Create("?projID=", 425), true)
            
            #line 12 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
          , Tuple.Create(Tuple.Create("", 433), Tuple.Create<System.Object, System.Int32>(ViewBag.projID
            
            #line default
            #line hidden
, 433), false)
, Tuple.Create(Tuple.Create("", 448), Tuple.Create("&taskGroupId=", 448), true)
            
            #line 12 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
                                      , Tuple.Create(Tuple.Create("", 461), Tuple.Create<System.Object, System.Int32>(ViewBag.taskGroupId
            
            #line default
            #line hidden
, 461), false)
);

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" title=\"文件进度\"");

WriteAttribute("href", Tuple.Create(" href=\"", 516), Tuple.Create("\"", 639)
            
            #line 13 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
, Tuple.Create(Tuple.Create("", 523), Tuple.Create<System.Object, System.Int32>(Url.Action("ProjectProgressInfoAttach", "DesTask")
            
            #line default
            #line hidden
, 523), false)
, Tuple.Create(Tuple.Create("", 574), Tuple.Create("?projID=", 574), true)
            
            #line 13 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
            , Tuple.Create(Tuple.Create("", 582), Tuple.Create<System.Object, System.Int32>(ViewBag.projID
            
            #line default
            #line hidden
, 582), false)
, Tuple.Create(Tuple.Create("", 597), Tuple.Create("&taskGroupId=", 597), true)
            
            #line 13 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
                                        , Tuple.Create(Tuple.Create("", 610), Tuple.Create<System.Object, System.Int32>(ViewBag.taskGroupId
            
            #line default
            #line hidden
, 610), false)
, Tuple.Create(Tuple.Create("", 630), Tuple.Create("&taskId=0", 630), true)
);

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" title=\"提资进度\"");

WriteAttribute("href", Tuple.Create(" href=\"", 674), Tuple.Create("\"", 786)
            
            #line 14 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
, Tuple.Create(Tuple.Create("", 681), Tuple.Create<System.Object, System.Int32>(Url.Action("ProjectProgressInfoExch", "DesExch")
            
            #line default
            #line hidden
, 681), false)
, Tuple.Create(Tuple.Create("", 730), Tuple.Create("?projID=", 730), true)
            
            #line 14 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
          , Tuple.Create(Tuple.Create("", 738), Tuple.Create<System.Object, System.Int32>(ViewBag.projID
            
            #line default
            #line hidden
, 738), false)
, Tuple.Create(Tuple.Create("", 753), Tuple.Create("&taskGroupId=", 753), true)
            
            #line 14 "..\..\Views\DesTask\ProjectProgressInfo.cshtml"
                                      , Tuple.Create(Tuple.Create("", 766), Tuple.Create<System.Object, System.Int32>(ViewBag.taskGroupId
            
            #line default
            #line hidden
, 766), false)
);

WriteLiteral("></div>\r\n    </div>\r\n");

});

        }
    }
}
#pragma warning restore 1591