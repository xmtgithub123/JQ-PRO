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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/TaskAttachProgress.cshtml")]
    public partial class _Views_DesTask_TaskAttachProgress_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_TaskAttachProgress_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<link");

WriteAttribute("href", Tuple.Create(" href=\"", 7), Tuple.Create("\"", 64)
            
            #line 2 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 14), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/uploader/jq.upload.css")
            
            #line default
            #line hidden
, 14), false)
);

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" />\r\n\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    #TaskAttachProgressDiv .tree-title {\r\n        height: auto;\r\n    }\r\n\r\n    " +
".FlowNodeClass td {\r\n        font-size: 12px !important;\r\n        height: 20px !" +
"important;\r\n        border: 1px solid #D3D3D3;\r\n    }\r\n</style>\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(@">
    .jq-panel .list-group-item {
        border-bottom: 1px solid #dbdbdb;
        padding: 10px 15px;
    }

    .jq-panel-heading {
        font-weight: bold;
    }

    .jq-btn-block {
        width: auto;
    }

    .jq-btn {
        padding: 2px 12px;
    }

    .jq-panel .list-group-item span.item-title {
        display: inline-block;
        float: left;
        width: 60px;
        text-align: left;
        margin-right: 5px;
    }

    .jq-panel .list-group-item p.item-text {
        padding-left: 70px;
        /* margin: -15px 0; */
        text-indent: -0px;
        color: #848484;
    }
</style>

<div");

WriteLiteral(" id=\"TaskAttachProgressDiv\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"border-right-width:1px;border-bottom-width:1px;overflow:hidden\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" style=\"height:30px;padding:5px 0px;\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" style=\"float:left;margin-left:5px;\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" id=\"downFile\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-download fa-lg\'\"");

WriteLiteral(">下载</a>\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" id=\"reloadFiles_files\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-repeat fa-lg\',plain:true\"");

WriteLiteral(">刷新</a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n" +
"            <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(" style=\"border-right-width:1px;\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" id=\"_desupload_$_1_temp\"");

WriteLiteral("></span>\r\n                <table");

WriteLiteral(" id=\"TaskAttachProgress_files\"");

WriteLiteral("></table>\r\n                <div");

WriteLiteral(" id=\"TaskAttachProgress_rigthMenu\"");

WriteLiteral(" data-options=\"onClick:TaskAttachProgress_rigthMenuHandler\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" style=\"width:100px;display: none;\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" id=\"showAttachSplitFiles\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-object-ungroup\'\"");

WriteLiteral(">拆图文件</div>\r\n                    <div");

WriteLiteral(" id=\"showAttachHistory\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-history\'\"");

WriteLiteral(">历史版本</div>\r\n                    <div");

WriteLiteral(" id=\"showAttachInfo\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-info-circle\'\"");

WriteLiteral(">文件属性</div>\r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" data-options=\"region:\'south\',border:false,split:true,title:\'工具面板\',collapsible:tr" +
"ue\"");

WriteLiteral(" style=\"height:350px\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" id=\"TaskAttachProgress_TaskTab\"");

WriteLiteral(" class=\"easyui-tabs\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" title=\"校审意见\"");

WriteAttribute("url", Tuple.Create(" url=\"", 2804), Tuple.Create("\"", 2917)
            
            #line 71 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 2810), Tuple.Create<System.Object, System.Int32>(Url.Action("TaskCheckList","DesTaskCheck")
            
            #line default
            #line hidden
, 2810), false)
, Tuple.Create(Tuple.Create("", 2853), Tuple.Create("?taskId=", 2853), true)
            
            #line 71 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
              , Tuple.Create(Tuple.Create("", 2861), Tuple.Create<System.Object, System.Int32>(ViewBag.TaskModel.Id
            
            #line default
            #line hidden
, 2861), false)
, Tuple.Create(Tuple.Create("", 2882), Tuple.Create("&divid=", 2882), true)
            
            #line 71 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                          , Tuple.Create(Tuple.Create("", 2889), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 2889), false)
, Tuple.Create(Tuple.Create("", 2907), Tuple.Create("&View=true", 2907), true)
);

WriteLiteral(" data-options=\"selected:true\"");

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" title=\"会签记录\"");

WriteAttribute("url", Tuple.Create(" url=\"", 2993), Tuple.Create("\"", 3107)
            
            #line 72 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 2999), Tuple.Create<System.Object, System.Int32>(Url.Action("DesMutiSignList","DesMutiSign")
            
            #line default
            #line hidden
, 2999), false)
, Tuple.Create(Tuple.Create("", 3043), Tuple.Create("?taskId=", 3043), true)
            
            #line 72 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
               , Tuple.Create(Tuple.Create("", 3051), Tuple.Create<System.Object, System.Int32>(ViewBag.TaskModel.Id
            
            #line default
            #line hidden
, 3051), false)
, Tuple.Create(Tuple.Create("", 3072), Tuple.Create("&divid=", 3072), true)
            
            #line 72 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                           , Tuple.Create(Tuple.Create("", 3079), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 3079), false)
, Tuple.Create(Tuple.Create("", 3097), Tuple.Create("&View=true", 3097), true)
);

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" title=\"提资记录\"");

WriteAttribute("url", Tuple.Create(" url=\"", 3154), Tuple.Create("\"", 3265)
            
            #line 73 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 3160), Tuple.Create<System.Object, System.Int32>(Url.Action("DesSpecExchList", "DesExch")
            
            #line default
            #line hidden
, 3160), false)
, Tuple.Create(Tuple.Create("", 3201), Tuple.Create("?taskId=", 3201), true)
            
            #line 73 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
            , Tuple.Create(Tuple.Create("", 3209), Tuple.Create<System.Object, System.Int32>(ViewBag.TaskModel.Id
            
            #line default
            #line hidden
, 3209), false)
, Tuple.Create(Tuple.Create("", 3230), Tuple.Create("&divid=", 3230), true)
            
            #line 73 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                        , Tuple.Create(Tuple.Create("", 3237), Tuple.Create<System.Object, System.Int32>(ViewBag._DialogId
            
            #line default
            #line hidden
, 3237), false)
, Tuple.Create(Tuple.Create("", 3255), Tuple.Create("&View=true", 3255), true)
);

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" title=\"备注\"");

WriteAttribute("url", Tuple.Create(" url=\"", 3310), Tuple.Create("\"", 3342)
            
            #line 74 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 3316), Tuple.Create<System.Object, System.Int32>(ViewBag.AltHtml
            
            #line default
            #line hidden
, 3316), false)
, Tuple.Create(Tuple.Create("", 3332), Tuple.Create("&View=true", 3332), true)
);

WriteLiteral("></div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n" +
"    <div");

WriteLiteral(" data-options=\"region:\'east\',border:false,title:\'详情面板\',collapsible:true\"");

WriteLiteral(" style=\"width:300px\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"margin:10px 10px \"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"jq-panel jq-panel-default\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"jq-panel-heading\"");

WriteLiteral(">项目详情</div>\r\n                <ul");

WriteLiteral(" class=\"list-group\"");

WriteLiteral(">\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">项目名称:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 84 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                              Write(Html.Raw(ViewBag.ProjModel.ProjName));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">项目编号:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 85 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                              Write(Html.Raw(ViewBag.ProjModel.ProjNumber));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">项目阶段:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 86 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.TaskPhaseName);

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">负责人员:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 87 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                              Write(ViewBag.ProjModel.ProjEmpName);

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">计划开始:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 88 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.ProjModel.DatePlanStart.Year == 1900 ? "" : ViewBag.ProjModel.DatePlanStart.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">计划结束:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 89 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.ProjModel.DatePlanFinish.Year == 1900 ? "" : ViewBag.ProjModel.DatePlanFinish.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                </ul>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"jq-panel jq-panel-default\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"jq-panel-heading\"");

WriteLiteral(">任务详情</div>\r\n                <ul");

WriteLiteral(" class=\"list-group\"");

WriteLiteral(">\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">任务名称:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 95 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                              Write(Html.Raw(ViewBag.TaskModel.TaskName));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">所属专业:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 96 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.TaskSpecName);

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">执行人员:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 97 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.TaskModel.TaskEmpName);

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">计划开始:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral("> ");

            
            #line 98 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                                Write(ViewBag.TaskModel.DatePlanStart.Year == 1900 ? "" : ViewBag.TaskModel.DatePlanStart.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                    <li");

WriteLiteral(" class=\"list-group-item clearfix\"");

WriteLiteral("><span");

WriteLiteral(" class=\"item-title\"");

WriteLiteral(">计划完成:</span><p");

WriteLiteral(" class=\"item-text\"");

WriteLiteral(">");

            
            #line 99 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                               Write(ViewBag.TaskModel.DatePlanFinish.Year == 1900 ? "" : ViewBag.TaskModel.DatePlanFinish.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</p></li>\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r" +
"\n</div>\r\n\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 6052), Tuple.Create("\"", 6096)
            
            #line 106 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 6058), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/doT.min.js")
            
            #line default
            #line hidden
, 6058), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 6139), Tuple.Create("\"", 6187)
            
            #line 107 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 6145), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/touchSlider.js")
            
            #line default
            #line hidden
, 6145), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 6230), Tuple.Create("\"", 6282)
            
            #line 108 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 6236), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/uploader/upload.js")
            
            #line default
            #line hidden
, 6236), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 6325), Tuple.Create("\"", 6384)
            
            #line 109 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 6331), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/uploader/jq.taskUpload.js")
            
            #line default
            #line hidden
, 6331), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n<script");

WriteAttribute("src", Tuple.Create(" src=\"", 6427), Tuple.Create("\"", 6474)
            
            #line 110 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
, Tuple.Create(Tuple.Create("", 6433), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/JQ/DesFlow.js")
            
            #line default
            #line hidden
, 6433), false)
);

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral("></script>\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    var _TaskAttachProgressDesignUpload = new JQ.taskUpload();\r\n    var _Tas" +
"kAttachProgressTaskStatus = \'");

            
            #line 115 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                    Write(ViewBag.TaskModel.TaskStatus);

            
            #line default
            #line hidden
WriteLiteral("\';\r\n    var _TaskAttachProgress_CurrentUserId = \'");

            
            #line 116 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                        Write(ViewBag.EmpID);

            
            #line default
            #line hidden
WriteLiteral(@"';

    // 刷新文件列表
    $(""#reloadFiles_files"", $(""#TaskAttachProgressDiv"")).click(function () {
        //_dialogClose = false;
        $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""reload"");
        $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""unselectAll"");
    });

</script>

<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

    // 打开模态对话框
    function TaskAttachProgress_showModelDialog(title, url, width, height, onclose) {
        var _TaskAttachProgress_DialogdivId = JQ.dialog.dialog({
            title: decodeURIComponent(title),
            url: url,
            width: width,
            height: height,
            JQID: """",
            JQLoadingType: """",
            iconCls: ""fa fa-table"",
            onClose: onclose
        });
        JQ.Flow.stopBubble();

        // 刷新列表
        window.top[""_DialogCallback_"" +_TaskAttachProgress_DialogdivId] = function () {
            $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""clearSelections"");
            $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""clearChecked"");
            $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""reload"");
        }
    }

    // 下载文件
    $(""#downFile"", $(""#TaskAttachProgressDiv"")).click(function () {
        if ($(this).linkbutton('options').disabled == false) {
            TaskAttachProgress_showModelDialog(
                ""批量下载"",
                '");

            
            #line 156 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
            Write(Url.Action("TaskAttach", "DesTask"));

            
            #line default
            #line hidden
WriteLiteral("?TaskId=");

            
            #line 156 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                         Write(ViewBag.TaskModel.Id);

            
            #line default
            #line hidden
WriteLiteral(@"',
                1200,
                800
            );
        }
    });
    

    // 刷新文件列表
    $(""#reloadFiles"", $(""#TaskAttachProgressDiv"")).click(function () {
        $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""clearSelections"");
        $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""clearChecked"");
        $(""#TaskAttachProgress_files"", $(""#TaskAttachProgressDiv"")).treegrid(""reload"");
    });

    // 初始化附件列表
    function TaskAttachProgressInit() {
        _TaskAttachProgressDesignUpload.CreateFolderUrl='");

            
            #line 173 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                    Write(Url.Action("TaskCreateFolder", "ProcessFile", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("?AttchId=0\';\r\n        _TaskAttachProgressDesignUpload.DeleteFileUrl=\'");

            
            #line 174 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                   Write(Url.Action("Delete", "ProcessFile", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        _TaskAttachProgressDesignUpload.requireurl = \'");

            
            #line 175 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                 Write(Url.Action("TaskInfoGetDesTaskAttachDataJson"));

            
            #line default
            #line hidden
WriteLiteral("?TaskId=\'+");

            
            #line 175 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                                                                          Write(ViewBag.TaskModel.Id);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        _TaskAttachProgressDesignUpload.uploadurl = \"");

            
            #line 176 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                 Write(Url.Action("Upload", "ProcessFile", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n        _TaskAttachProgressDesignUpload.downloadUrl = \"");

            
            #line 177 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                   Write(Url.Action("Download", "ProcessFile", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n        _TaskAttachProgressDesignUpload.cancelUrl = \"");

            
            #line 178 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                 Write(Url.Action("CancelUpload", "ProcessFile", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n        _TaskAttachProgressDesignUpload.flash_swf_url=\"");

            
            #line 179 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                   Write(Url.Content("~/Scripts/uploader/upload.swf"));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n        _TaskAttachProgressDesignUpload.silverlight_xap_url= \"");

            
            #line 180 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                                                          Write(Url.Content("~/Scripts/uploader/upload.xap"));

            
            #line default
            #line hidden
WriteLiteral("\";\r\n        _TaskAttachProgressDesignUpload.init($(\"#TaskAttachProgress_files\", $" +
"(\"#TaskAttachProgressDiv\")),{\r\n            rownumbers:true,\r\n            frozenC" +
"olumns: [[\r\n                //{ field: \'ck\', align: \'center\', checkbox: true, JQ" +
"IsExclude: true},\r\n                { title: \'附件信息\', field: \'AttachName\', width: " +
"350, align: \'left\',halign:\'center\',formatter:function (value,row,index) {\r\n     " +
"               var FileName=\"\";\r\n                    if (row.AttachExt==\".\") {\r\n" +
"                        FileName=\"<div style=\\\"float:left;cursor:pointer\\\"  oncl" +
"ick=\\\"expandFun(\"+row.AttachID+\")\\\" ><strong>\"+row.AttachName+\"</strong></div>\";" +
"\r\n                    }else {\r\n                        FileName=\"<div><a href=\\\"" +
"\"+_TaskAttachProgressDesignUpload.buildDownloadUrl(row.AttachID, { type: \'attach" +
"\' })+\"\\\" target=\'_blank\' >\"+value+\"</a>\";\r\n                        if (row.Attac" +
"hID <= 0) {\r\n                            FileName+=\"<div class=\\\"jq_upfile_s\\\" s" +
"tyle=\\\"float:right\\\"><div id=\\\"p_\" + row.FileID + \"\\\" class=\\\"jq_upfile_s1\\\"><di" +
"v class=\\\"jq_upfile_s2\\\"></div><div class=\\\"jq_upfile_s3\\\">0%</div></div><div id" +
"=\\\"r_\" + row.FileID + \"\\\" class=\\\"fa fa-times-circle jq_upfile_s4\\\" title=\\\"取消\\\"" +
"></div></div>\"\r\n                        }\r\n                        FileName+=\"</" +
"div>\";\r\n                    }\r\n                    return FileName;\r\n           " +
"     } },\r\n            ]],\r\n            columns: [[\r\n                { title: \'校" +
"审流程\', field: \'12\', width: 600, align: \'left\',formatter:function (value,row,index" +
") {\r\n                    return \"<div class=\\\"swipe\\\"><div id=\\\"FlowNode_files_\"" +
"+row.AttachID+\"\\\" class=\\\"approve-progress\\\"></div></div>\"\r\n                } }," +
"\r\n                { title: \"版本\", field: \"AttachVer\", width: 50, align: \"center\"," +
" halign: \"center\",formatter:function (value, row,index) {\r\n                    i" +
"f(row.AttachExt==\".\"){\r\n                        return \"\";\r\n                    " +
"}else {\r\n\r\n                        return value;\r\n                    }\r\n       " +
"         } },\r\n                {\r\n                    title: \"大小\", field: \"Attac" +
"hSize\", width: 76, align: \"center\", halign: \"center\", formatter: function (value" +
", row,index) {\r\n                        if(row.AttachExt==\".\"){\r\n               " +
"             return \"\";\r\n                        }else {\r\n                      " +
"      return JQ.tools.getFileSizeText(value);\r\n                        }\r\n      " +
"              }},\r\n                {\r\n                    title: \"修改时间\", field: " +
"\"AttachDateChange\", width: 120, align: \"center\", halign: \"center\", formatter: fu" +
"nction (value, rowData,index) {\r\n                        if (rowData.AttachID <=" +
" 0) {\r\n                            return value;\r\n                        }\r\n   " +
"                     else if (rowData.UploadStatus) {\r\n                         " +
"   return JQ.tools.DateTimeFormatterByT(rowData.AttachDateChange);\r\n            " +
"            }\r\n                        else {\r\n                            retur" +
"n JQ.tools.DateTimeFormatterByT(rowData.AttachDateChange);\r\n                    " +
"    }\r\n                    }\r\n                },\r\n                {\r\n           " +
"         title: \"上传日期\", field: \"AttachDateUpload\", width: 90, align: \"center\", h" +
"align: \"center\", formatter: function (value, rowData,index) {\r\n                 " +
"       if (rowData.AttachID <= 0) {\r\n                            return \"\";\r\n   " +
"                     }\r\n                        else if (rowData.UploadStatus) {" +
"\r\n                            return JQ.tools.DateTimeFormatterByT(rowData.Attac" +
"hDateUpload);\r\n                        }\r\n                        else {\r\n      " +
"                      return JQ.tools.DateTimeFormatterByT(rowData.AttachDateUpl" +
"oad);\r\n                        }\r\n                    }\r\n                }\r\n    " +
"        ]],\r\n            onLoadSuccess:function (row,data) {\r\n                if" +
" (data.total>0) {\r\n                    $.each(data.rows,function (i,e) {\r\n      " +
"                  var jsonNode = [];\r\n                        if (e.AttachFlowNo" +
"deJson!=\"\") {\r\n                            jsonNode=JSON.parse(e.AttachFlowNodeJ" +
"son).root.item;\r\n\r\n                            if (e.AttachExt!=\".\") {\r\n        " +
"                        var $table=$(\"div[id=\'FlowNode_files_\"+e.AttachID+\"\']\", " +
"$(\"#TaskAttachProgressDiv\"));\r\n                                if (e.AttachFlowN" +
"odeJson!=\"\") {\r\n                                    JQ.DesFlow.show({\r\n         " +
"                               Ctl:$table,\r\n                                    " +
"    FlowData:jsonNode,\r\n                                    });\r\n               " +
"                 }\r\n                            }\r\n                        }\r\n  " +
"                  });\r\n                }\r\n            },\r\n            onContextM" +
"enu: function (e, row) {\r\n                if (row == null) return;\r\n            " +
"    $.data(document.body, \"selectTempData\", row);//把临时数据存在缓存中\r\n                /" +
"/ 显示右键菜单\r\n                var $m = $(\'#TaskAttachProgress_rigthMenu\');\r\n        " +
"        $m.menu(\'show\', {\r\n                    left: e.pageX,\r\n                 " +
"   top: e.pageY\r\n                });\r\n                e.preventDefault();\r\n     " +
"       }\r\n        });\r\n    }\r\n\r\n    function TaskAttachProgress_rigthMenuHandler" +
"(item) {\r\n        var data = $.data(document.body, \"selectTempData\");//获取临时数据\r\n " +
"       //var data = $(\"#TaskInfo_files\", $(\"#TaskInfoDiv\")).treegrid(\"getSelecte" +
"d\");\r\n        if (JQ.tools.isJson(data)) {\r\n            switch (item.id) {\r\n    " +
"            case \"showAttachSplitFiles\":\r\n                    TaskAttachProgress" +
"_showModelDialog(\r\n                        \"拆图文件：{0}\".format(data.AttachName),\r\n" +
"                        \'");

            
            #line 288 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                    Write(Url.Action("TaskAttachSplitFilesList"));

            
            #line default
            #line hidden
WriteLiteral(@"?AttachID=' + data.AttachID + '&AttachVer=' + data.AttachVer,
                        800,
                        700
                    );
                    break;
                case ""showAttachHistory"":
                    TaskAttachProgress_showModelDialog(
                        ""历史版本：{0}"".format(data.AttachName),
                        '");

            
            #line 296 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                    Write(Url.Action("AttachHistory", "BaseAttach", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?AttachID=' + data.AttachID,
                        600,
                        600
                    );
                    break;
                case ""showAttachInfo"":
                    TaskAttachProgress_showModelDialog(
                        ""文件属性：{0}"".format(data.AttachName),
                        '");

            
            #line 304 "..\..\Views\DesTask\TaskAttachProgress.cshtml"
                    Write(Url.Action("AttachInfo", "BaseAttach", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral(@"?AttachID=' + data.AttachID,
                        500,
                        500
                    );
                    break;
            }
        }
        //$.data(document.body, ""selectTempData"", null);//清空临时数据
        return false;
    }

    // 初始化底部选项卡
    function TaskInfoApproveTabInit() {
        $(""#TaskAttachProgress_TaskTab"", $(""#TaskAttachProgressDiv"")).tabs({
            onSelect:function (title,index) {
                //加载页面
                var selectTab= $(""#TaskAttachProgress_TaskTab"", $(""#TaskAttachProgressDiv"")).tabs(""getTab"",index);
                var isRefresh=true;
                if (selectTab.find(""div[id='TaskCheckListDiv']"").length>0) {
                    isRefresh= false;//校审建议
                }
                if (selectTab.find(""form[id='DesignInfoForm']"").length>0) {
                    isRefresh= false;//备注
                }
                if (isRefresh) {
                    $('#TaskAttachProgress_TaskTab').tabs('update', {
                        tab: selectTab,
                        options: {
                            href: $(selectTab).attr(""url""),  // 新内容的URL
                        }
                    });
                }
            }
        });
        $(""#TaskAttachProgress_TaskTab"", $(""#TaskAttachProgressDiv"")).tabs(""select"",0);
    }

    $(function () {
        // 初始化附件列表
        TaskAttachProgressInit();

        TaskInfoApproveTabInit();
    });
</script>
");

        }
    }
}
#pragma warning restore 1591
