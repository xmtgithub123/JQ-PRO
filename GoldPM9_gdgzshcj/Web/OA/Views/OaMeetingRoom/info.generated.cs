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
    
    #line 2 "..\..\Views\OaMeetingRoom\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaMeetingRoom/info.cshtml")]
    public partial class _Views_OaMeetingRoom_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaMeetingRoom>
    {
        public _Views_OaMeetingRoom_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'OaMeetingRoomForm\',//formid提交需要用到\r\n" +
"        buttonTypes: ");

            
            #line 6 "..\..\Views\OaMeetingRoom\info.cshtml"
                Write(Html.Raw(ViewBag.Permission));

            
            #line default
            #line hidden
WriteLiteral(@" ,//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            //JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            //JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源

            var tab = $('#mainTab').tabs('getSelected');
            var index = $('#mainTab').tabs('getTabIndex', tab);

            $(""#mainTab"").find(""iframe"")[index].contentWindow.refreshGrid();
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
</script>
");

            
            #line 19 "..\..\Views\OaMeetingRoom\info.cshtml"
 using (Html.BeginForm("save", "OaMeetingRoom", FormMethod.Post, new { @area = "Oa", @id = "OaMeetingRoomForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\OaMeetingRoom\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 21 "..\..\Views\OaMeetingRoom\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">会议室名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RoomName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1136), Tuple.Create("\"", 1159)
            
            #line 27 "..\..\Views\OaMeetingRoom\info.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 1144), Tuple.Create<System.Object, System.Int32>(Model.RoomName
            
            #line default
            #line hidden
, 1144), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">会议室容纳人数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RoomPeoLength\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteLiteral(" data-options=\"min:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1393), Tuple.Create("\"", 1421)
            
            #line 31 "..\..\Views\OaMeetingRoom\info.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 1401), Tuple.Create<System.Object, System.Int32>(Model.RoomPeoLength
            
            #line default
            #line hidden
, 1401), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">会议面积</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RoomArea\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteLiteral(" data-options=\"precision:2,min:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1690), Tuple.Create("\"", 1713)
            
            #line 38 "..\..\Views\OaMeetingRoom\info.cshtml"
                                                                        , Tuple.Create(Tuple.Create("", 1698), Tuple.Create<System.Object, System.Int32>(Model.RoomArea
            
            #line default
            #line hidden
, 1698), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">会议室状态</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 42 "..\..\Views\OaMeetingRoom\info.cshtml"
           Write(BaseData.getBase(new Params()
                     {
                         controlName = "RoomStatus",
                         isRequired = true,
                         engName = "MeetinfRoomState",
                         width = "98%",
                         ids = Model.RoomStatus.ToString()
                     }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                说明\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RoomNote\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:98%;height:60px;\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2430), Tuple.Create("\"", 2453)
            
            #line 57 "..\..\Views\OaMeetingRoom\info.cshtml"
                                                  , Tuple.Create(Tuple.Create("", 2438), Tuple.Create<System.Object, System.Int32>(Model.RoomNote
            
            #line default
            #line hidden
, 2438), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 65 "..\..\Views\OaMeetingRoom\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 65 "..\..\Views\OaMeetingRoom\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaMeetingRoom";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 70 "..\..\Views\OaMeetingRoom\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 70 "..\..\Views\OaMeetingRoom\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 75 "..\..\Views\OaMeetingRoom\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
