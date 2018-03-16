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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/TaskAttachSplitFilesList.cshtml")]
    public partial class _Views_DesTask_TaskAttachSplitFilesList_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_TaskAttachSplitFilesList_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: ""splitattachform"",
        buttonTypes: [""close""],
        succesCollBack: function (data) {
            JQ.dialog.dialogClose();
        },
        buttons: [{
            icon: ""fa-download"", text: ""批量下载"", onClick: function () {
                if (!window.JinQu) {
                    JQ.dialog.info(""请在金曲客户端中使用批量下载功能！"");
                    return;
                }
                var rows = $(""#tbFiles"").datagrid(""getChecked"");
                if (rows.length == 0) {
                    JQ.dialog.info(""请至少选择一个文件！"");
                    return;
                }
                var downopt = $(""#downloadoptions"").combobox(""getValue"");
                var urls = [];
                var dirs = [];
                switch (downopt) {
                    case ""1"":
                        //下载DWG文件
                        for (var i = 0; i < rows.length; i++) {
                            urls.push(window.location.protocol + ""//"" + window.location.host + """);

            
            #line 26 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                                                            Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + rows[i].ID + ""&type=DesignSplitFile"");
                            dirs.push(rows[i].Name);
                        }
                        break;
                    case ""2"":
                        //下载DWG签名文件
                        for (var i = 0; i < rows.length; i++) {
                            if (!rows[i].Extensions) {
                                continue;
                            }
                            for (var j = 0; j < rows[i].Extensions.length; j++) {
                                if (rows[i].Extensions[j].Extension == ""DWG"") {
                                    urls.push(window.location.protocol + ""//"" + window.location.host + """);

            
            #line 38 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                                                                    Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + rows[i].Extensions[j].ID + ""&type=DesignSplitFile"");
                                    dirs.push(getName(rows[i].Name) + "".dwg"");
                                }
                            }
                        }
                        break;
                    case ""3"":
                        //下载PDF签名文件
                        for (var i = 0; i < rows.length; i++) {
                            if (!rows[i].Extensions) {
                                continue;
                            }
                            for (var j = 0; j < rows[i].Extensions.length; j++) {
                                if (rows[i].Extensions[j].Extension == ""PDF"" && rows[i].Extensions[j].IsSigned) {
                                    urls.push(window.location.protocol + ""//"" + window.location.host + """);

            
            #line 52 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                                                                    Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + rows[i].Extensions[j].ID + ""&type=DesignSplitFileSign"");
                                    dirs.push(getName(rows[i].Name) + ""（签名）.pdf"");
                                }
                            }
                        }
                        break;
                }
                if (urls.length > 0) {
                    var $btn = $(this)
                    $btn.linkbutton(""disable"");
                    window.JinQu.query(""httpDownloadMulti"", {
                        url: urls,
                        local: dirs
                    }, function (file, uploadSize, totalSize) {

                    }, function (response, request) {
                        $btn.linkbutton(""enable"");
                    }, function (error_code, error_message) {
                        $btn.linkbutton(""enable"");
                    });
                }
            }
        }],
        onInit: function ($panel) {
            $(""<select id=\""downloadoptions\""><option value=\""1\"">DWG图纸文件</option><option value=\""2\"">DWG带签名文件</option></select>"").appendTo($panel);
            $(""#downloadoptions"").combobox({
                editable: false,
                panelHeight: ""auto"",
                onSelect: function (row) {
                    debugger;
                    type = row.value;
                    $(""#tbFiles"").datagrid(""load"",{ attachID: """);

            
            #line 83 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                           Write(Request.QueryString["AttachID"]);

            
            #line default
            #line hidden
WriteLiteral("\", attachVer: \"");

            
            #line 83 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                                                                            Write(Request.QueryString["AttachVer"]);

            
            #line default
            #line hidden
WriteLiteral("\", type:type });\r\n                }\r\n            });\r\n        }\r\n    });\r\n    var" +
" type = 1;\r\n    var divFrame = document.getElementById(\"splitattachform\").parent" +
"Node;\r\n    $(\"#tbFiles\").datagrid({\r\n        url: \"");

            
            #line 91 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
          Write(Url.Action("GetSplitFiles", "DesTask",new { @area= "Design" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        queryParams: { attachID: \"");

            
            #line 92 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                              Write(Request.QueryString["AttachID"]);

            
            #line default
            #line hidden
WriteLiteral("\", attachVer: \"");

            
            #line 92 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                                                               Write(Request.QueryString["AttachVer"]);

            
            #line default
            #line hidden
WriteLiteral(@""", type:type },
        rownumbers: true,
        idField: ""ID"",
        height: divFrame.clientHeight - 2,
        width: divFrame.clientWidth - 2,
        columns: [[
              { field: ""aaa"", checkbox: true },
              {
                  title: ""文件名称"", field: ""Name"", width: ""40%"", align: ""left"", halign: ""center"", formatter: function (value, rowData) {
                      var a = document.createElement(""a"");
                      a.setAttribute(""href"", """);

            
            #line 102 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                          Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + rowData.ID + ""&type=DesignSplitFile"");
                      a.setAttribute(""onclick"", ""JQ.Flow.stopBubble();"");
                      a.appendChild(document.createTextNode(value));
                      return a.outerHTML;
                  }
              },
              {
                  title: ""文件大小"", field: ""Size"", width: ""10%"", align: ""right"", halign: ""center"", formatter: function (value) {
                      return getFileSizeText(value);
                  }
              },
              {
                  title: ""其他格式"", field: ""ID"", width: ""40%"", align: ""left"", halign: ""center"", formatter: function (value, row) {
                      if (!row.Extensions) {
                          return;
                      }
                      var text = """";
                      for (var i = 0; i < row.Extensions.length; i++) {
                          if (row.Extensions[i].IsSigned) {
                              text += ""<a href=\""");

            
            #line 121 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                             Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + row.Extensions[i].ID + ""&type=DesignSplitFileSign\"" style=\""margin-left:5px\"" onclick=\""JQ.Flow.stopBubble();\"">"" + row.Extensions[i].Extension + ""("" + getFileSizeText(row.Extensions[i].Size) + "")带签名"" + ""</a>"";
                          }
                          else {
                              text += ""<a href=\""");

            
            #line 124 "..\..\Views\DesTask\TaskAttachSplitFilesList.cshtml"
                                             Write(Url.Action("Download","ProcessFile",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?id="" + row.Extensions[i].ID + ""&type=DesignSplitFile\"" style=\""margin-left:5px\"" onclick=\""JQ.Flow.stopBubble();\"">"" + row.Extensions[i].Extension + ""("" + getFileSizeText(row.Extensions[i].Size) + "")"" + ""</a>"";
                          }
                      }
                      return text;
                  }
              }
        ]],
    });

    function getFileSizeText(size) {
        if (!size && size != 0) {
            return """";
        }
        var st = """";
        if (size >= 1024 * 1024 * 1024) {
            st = (size / 1024 / 1024 / 1024).toFixed(2) + ""GB"";
        } else if (size >= 1024 * 1024) {
            st = (size / 1024 / 1024).toFixed(2) + ""MB"";
        } else if (size >= 1024) {
            st = (size / 1024).toFixed(2) + ""KB"";
        } else {
            st = size + ""B"";
        }
        return st;
    }

    function getName(name) {
        return name.substring(0, name.indexOf("".""));
    }
</script>
<form");

WriteLiteral(" id=\"splitattachform\"");

WriteLiteral(" name=\"splitattachform\"");

WriteLiteral(">\r\n    <table");

WriteLiteral(" id=\"tbFiles\"");

WriteLiteral("></table>\r\n</form>");

        }
    }
}
#pragma warning restore 1591