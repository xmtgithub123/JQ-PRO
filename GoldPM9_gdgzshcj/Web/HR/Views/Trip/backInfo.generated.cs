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
    
    #line 2 "..\..\Views\Trip\backInfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Trip/backInfo.cshtml")]
    public partial class _Views_Trip_backInfo_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.Trip>
    {
        public _Views_Trip_backInfo_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'TripForm',//formid提交需要用到
        buttonTypes: ['close', 'exportForm'],//默认按钮
        docName: 'Trip',
        ExportName: '出差报销单',
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }, flow: {
            flowNodeID: """);

            
            #line 13 "..\..\Views\Trip\backInfo.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 14 "..\..\Views\Trip\backInfo.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 15 "..\..\Views\Trip\backInfo.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"Hr,Hr.FlowPro" +
"cessor.TripBack\",\r\n            onInit: function () {\r\n                _flowInsta" +
"nce = this;\r\n            },\r\n            refID: \"");

            
            #line 21 "..\..\Views\Trip\backInfo.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"HrCCBXD\",\r\n            dataCreator: \"");

            
            #line 23 "..\..\Views\Trip\backInfo.cshtml"
                      Write(ViewBag.CreatorEmpId );

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        },\r\n        beforeFormSubmit: function () {\r\n\r\n        },\r\n        onB" +
"efore: function (resultArr) {\r\n            resultArr.push({ Key: \"Permission\", V" +
"alue: \'");

            
            #line 29 "..\..\Views\Trip\backInfo.cshtml"
                                                   Write(ViewBag.isExport);

            
            #line default
            #line hidden
WriteLiteral("\' });\r\n        }\r\n    });\r\n\r\n</script>\r\n");

            
            #line 34 "..\..\Views\Trip\backInfo.cshtml"
 using (Html.BeginForm("save", "Trip", FormMethod.Post, new { @area = "Hr", @id = "TripForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\Trip\backInfo.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\Trip\backInfo.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:20px;font-weight:bold;margin-top:1" +
"0px;\"");

WriteLiteral(">\r\n            <a>出差申请单</a>\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:right;margin-right:20px;margin-top:10px;margin-bottom:10px;\"");

WriteLiteral(">\r\n            申请日期：");

            
            #line 42 "..\..\Views\Trip\backInfo.cshtml"
            Write(Model.ApplicationDate.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n        <tr>\r\n            <th");

WriteLiteral(" style=\"width:10%;\"");

WriteLiteral(">出差人</th>\r\n            <td");

WriteLiteral(" style=\"width:40%;\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 47 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" style=\"width:10%;\"");

WriteLiteral(">出差天数</th>\r\n            <td");

WriteLiteral(" style=\"width:40%;\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 51 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.Days);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>出差起始日期</th>\r\n" +
"            <td>\r\n");

WriteLiteral("                ");

            
            #line 58 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.StartDate.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th>出差结束日期</th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 62 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.EndDate.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>出差省市与地区</th>\r" +
"\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 69 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.TripReceive);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>出差事由</th>\r\n  " +
"          <td");

WriteLiteral(" height=\"150px\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 76 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.TripReason);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>配合类型</th>\r\n  " +
"          <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 83 "..\..\Views\Trip\backInfo.cshtml"
           Write(Model.TripType);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>实际天数</th>\r\n  " +
"          <td><input");

WriteLiteral(" name=\"SJTS\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" data-options=\"min:0,precision:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2980), Tuple.Create("\"", 2999)
            
            #line 89 "..\..\Views\Trip\backInfo.cshtml"
                    , Tuple.Create(Tuple.Create("", 2988), Tuple.Create<System.Object, System.Int32>(Model.SJTS
            
            #line default
            #line hidden
, 2988), false)
);

WriteLiteral(" /></td>\r\n            <th>补贴属性</th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 92 "..\..\Views\Trip\backInfo.cshtml"
           Write(BaseData.getBase(new Params()
                {
                    isMult = false,
                    controlName = "BTSX",
                    isRequired = true,
                    engName = "BTSX",
                    width = "98%",
                    ids=Model.BTSX.ToString()
                }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>补贴费用</th>\r\n  " +
"          <td><input");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" name=\"BTFY\"");

WriteLiteral(" data-options=\"min:0,precision:2\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3553), Tuple.Create("\"", 3572)
            
            #line 106 "..\..\Views\Trip\backInfo.cshtml"
                    , Tuple.Create(Tuple.Create("", 3561), Tuple.Create<System.Object, System.Int32>(Model.BTFY
            
            #line default
            #line hidden
, 3561), false)
);

WriteLiteral(" /></td>\r\n            <th>补贴方式</th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 109 "..\..\Views\Trip\backInfo.cshtml"
           Write(BaseData.getBase(new Params()
                {
                    isMult = false,
                    controlName = "BTFS",
                    isRequired = true,
                    engName = "BTFS",
                    width = "98%",
                    ids=Model.BTFS.ToString()
                }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 129 "..\..\Views\Trip\backInfo.cshtml"
                
            
            #line default
            #line hidden
            
            #line 129 "..\..\Views\Trip\backInfo.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "Trip";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 134 "..\..\Views\Trip\backInfo.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 134 "..\..\Views\Trip\backInfo.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 139 "..\..\Views\Trip\backInfo.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
