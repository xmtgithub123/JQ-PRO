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
    
    #line 2 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussProjInfoRecords/noteinfo.cshtml")]
    public partial class _Views_BussProjInfoRecords_noteinfo_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ProjIRNote>
    {
        public _Views_BussProjInfoRecords_noteinfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'NoteForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        beforeFormSubmit: function () {
        },
        // 成功回调函数,data为服务器返回值
        succesCollBack: function (data) {
            // 刷新上一级数据表格，必须在close窗体前调用
            if (_WeekCallBack && typeof (_WeekCallBack) == ""function"") {
                _WeekCallBack();
            }
            JQ.datagrid.autoRefdatagrid();
            // 关闭窗体放在最后一步执行，以避免找不到事件源
            JQ.dialog.dialogClose();
        }
    });
</script>
");

            
            #line 21 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
 using (Html.BeginForm("notesave", "BussProjInfoRecords", FormMethod.Post, new { @area = "Bussiness", @id = "NoteForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" id=\'BussProjInfoRecordsId\'");

WriteLiteral(" name=\'BussProjInfoRecordsId\'");

WriteLiteral(" style=\'display:none;\'");

WriteAttribute("value", Tuple.Create(" value=\'", 892), Tuple.Create("\'", 933)
            
            #line 24 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                 , Tuple.Create(Tuple.Create("", 900), Tuple.Create<System.Object, System.Int32>(Request["BussProjInfoRecordsId"]
            
            #line default
            #line hidden
, 900), false)
);

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">标题</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Title\"");

WriteLiteral(" style=\"width:48%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteLiteral(" prompt=\"请输入标题\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1220), Tuple.Create("\"", 1240)
            
            #line 29 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                                                                          , Tuple.Create(Tuple.Create("", 1228), Tuple.Create<System.Object, System.Int32>(Model.Title
            
            #line default
            #line hidden
, 1228), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">内容</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Detail\"");

WriteLiteral(" style=\"width:98%;height:180px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1440), Tuple.Create("\"", 1461)
            
            #line 35 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
, Tuple.Create(Tuple.Create("", 1448), Tuple.Create<System.Object, System.Int32>(Model.Detail
            
            #line default
            #line hidden
, 1448), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" prompt=\"请输入内容\"");

WriteLiteral(" data-options=\"multiline:true,required:true\"");

WriteLiteral(" validType=\"length[0,5000]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">事件类型</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 41 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
           Write(JQ.Web.BaseData.getBase(new Params() {
                   controlName = "BaseDataId",
                   isRequired = true,
                   engName = "ProjEventType",
                   width = "90%",
                   ids = Model.BaseDataId.ToString(),
               }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">事件日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Time\"");

WriteLiteral(" style=\"width:150px;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2217), Tuple.Create("\"", 2270)
            
            #line 51 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 2225), Tuple.Create<System.Object, System.Int32>(Model.Time.ToString("yyyy-MM-dd HH:mm:dd")
            
            #line default
            #line hidden
, 2225), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 59 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                
            
            #line default
            #line hidden
            
            #line 59 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "ProjIRNote";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 69 "..\..\Views\BussProjInfoRecords\noteinfo.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
