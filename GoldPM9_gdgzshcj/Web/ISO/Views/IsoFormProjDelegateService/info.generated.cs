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
    
    #line 2 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjDelegateService/info.cshtml")]
    public partial class _Views_IsoFormProjDelegateService_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormProjDelegateService_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'IsoFormProjDelegateServiceForm\',//f" +
"ormid提交需要用到\r\n        docName: \'IsoFormProjDelegateService\',\r\n        ExportName:" +
" \'项目工代\',\r\n        buttonTypes: ");

            
            #line 8 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                Write(Html.Raw(ViewBag.ExportPermission));

            
            #line default
            #line hidden
WriteLiteral(",//默认按钮\r\n        succesCollBack: function (data) {//成功回调函数,data为服务器返回值\r\n         " +
"   JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用\r\n            JQ.dialo" +
"g.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源\r\n        }, flow: {\r\n            flowNo" +
"deID: \"");

            
            #line 13 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 14 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.IsoFormProjDelegateService\",\r\n            onInit: function () {\r\n      " +
"          _flowInstance = this;\r\n            },\r\n            refID: \"");

            
            #line 20 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                Write(Model.FormID);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"IsoFormProjDelegateService\"\r\n");

WriteLiteral("            ");

            
            #line 22 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
       Write(Html.Raw(ViewBag.editPermission));

            
            #line default
            #line hidden
WriteLiteral("\r\n        }\r\n    });\r\n\r\n");

WriteLiteral("    ");

            
            #line 26 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
Write(Html.Raw(ViewBag.submitPermission));

            
            #line default
            #line hidden
WriteLiteral(@"
    $(""#FormDate"").datebox({ readonly: true })
    $(""#MaterialName"").textbox({ required: true })
 
    var selectValue="""";
    var selectText="""";

    JQ.combobox.common({
        id: 'ProjID',
        toolbar: '#tbProj',//工具栏的容器ID
        url: '");

            
            #line 36 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
         Write(Url.Action("combogridJson", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
        panelWidth: 600,
        panelHeight: 400,
        valueField: 'Id',
        textField: 'ProjName',
        editable: false,
        multiple: false,
        pagination: true,//是否分页
        queryParams:
            {
                projId:'");

            
            #line 46 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                   Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(@"',
                Words:$(""#fullNameSearchProj"").val()
            },
        columns: [[
            { title: '项目编号', field: 'ProjNumber', width: 100, align: 'center', sortable: true },
            { title: '项目名称', field: 'ProjName', width: 260, align: 'center', sortable: true },
            { title: '项目阶段', field: 'PhaseName', width: 140, align: 'center', sortable: true }
        ]],
        onSelect: function (val, row) {
           
            $(""#ProjNumber"").text(row.ProjNumber);
            $(""#PhaseID"").text(row.PhaseName);
            $(""#ProjEmpName"").text(row.ProjEmpName);

            selectValue=row.Id;
            selectText=row.ProjName;
        },
        onLoadSuccess: function () {
            $(""#PhaseID"").text('");

            
            #line 64 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                           Write(ViewBag.PhaseID);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjEmpName\").text(\'");

            
            #line 65 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                               Write(ViewBag.ProjEmpName);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjNumber\").text(\'");

            
            #line 66 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                              Write(ViewBag.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjEmpId\").text(\'");

            
            #line 67 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                             Write(ViewBag.ProjEmpId);

            
            #line default
            #line hidden
WriteLiteral(@"');
            var selectRow=$(this).combogrid('grid').datagrid('getSelected');
            if(selectRow!=undefined)
            {
                selectText=$(this).combogrid('getText');
            }
            if(selectRow==undefined)
            {
                $(this).combogrid('setText',selectText);
            }
        }

    });


    $(""#fullNameSearchProj"").textbox({
        buttonText: '筛选',
        buttonIcon: 'fa fa-search',
        height: 25,
        prompt: '项目名称、项目编号',
        onClickButton: function () {
            JQ.datagrid.searchGrid(
                {
                    $dg:$(""#ProjID"").combogrid('grid'),
                    loadingType: 'datagrid',
                    queryParams:
                             {
                                 projId:'");

            
            #line 94 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                    Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                                 Words:$(\"#fullNameSearchProj\").val()\r\n      " +
"                       }\r\n                });\r\n        }\r\n\r\n    });\r\n\r\n\r\n    fun" +
"ction downfile() {\r\n        ");

WriteLiteral(@"

        var parm;
        $.each(row, function (i, n) {
            if (i == 0) {
                parm = ""id="" + n;

            } else {
                parm += ""&id="" + n;
            }
        });

        debugger;
        //return;

        Url = '");

            
            #line 122 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
          Write(Url.Action("DownFile", "ArchElecFile", new { @area = "Archive" }));

            
            #line default
            #line hidden
WriteLiteral(@"?';
        downurlfile(Url + parm);
        //$('#ElecFileGrid').datagrid('reload');
    }

    function downurlfile(url) {
        var _a = document.createElement(""a"");
        _a.setAttribute(""href"", url);
        document.body.appendChild(_a);
        _a.click();
        document.body.removeChild(_a);
    }
</script>

");

            
            #line 136 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
 using (Html.BeginForm("save", "IsoFormProjDelegateService", FormMethod.Post, new { @area = "Iso", @id = "IsoFormProjDelegateServiceForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 138 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 138 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                  

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr");

WriteLiteral(" style=\"border:none;\"");

WriteLiteral(">\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" style=\"border:none;text-align:center;\"");

WriteLiteral(">\r\n                <span");

WriteLiteral(" style=\"font-weight:bold;font-size:large;\"");

WriteLiteral(">项目工代</span>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目名称\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"required:true,multiple:false\"");

WriteLiteral(" style=\"width:99%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5369), Tuple.Create("\"", 5427)
            
            #line 154 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                        , Tuple.Create(Tuple.Create("", 5377), Tuple.Create<System.Object, System.Int32>(@Model.ProjID==0 ? "" :@Model.ProjID.ToString()
            
            #line default
            #line hidden
, 5377), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjIDName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5487), Tuple.Create("\"", 5512)
            
            #line 155 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
, Tuple.Create(Tuple.Create("", 5495), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjName
            
            #line default
            #line hidden
, 5495), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目编号\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"ProjNumber\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral("></label>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                设计阶段\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"PhaseID\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"PhaseID\"");

WriteLiteral("></label>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目负责人\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"ProjEmpName\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"ProjEmpName\"");

WriteLiteral("></label>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                申请日期\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"CreationTime\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 185 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
           Write(Model.CreationTime.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                申请人\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" bookmark=\"CreatorEmpName\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 193 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                申请原因\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FormReason\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" data-options=\"required:true,multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,500]\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7098), Tuple.Create("\"", 7123)
            
            #line 202 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                                                                                                         , Tuple.Create(Tuple.Create("", 7106), Tuple.Create<System.Object, System.Int32>(Model.FormReason
            
            #line default
            #line hidden
, 7106), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                备注\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FormNote\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,500]\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7423), Tuple.Create("\"", 7446)
            
            #line 210 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 7431), Tuple.Create<System.Object, System.Int32>(Model.FormNote
            
            #line default
            #line hidden
, 7431), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 223 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 223 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                  
    var uploader = new DataModel.FileUploader();
    uploader.RefID = Model.FormID;
    uploader.RefTable = "IsoForm";
    uploader.Name = "uploadfile1";
    
            
            #line default
            #line hidden
            
            #line 228 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 228 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n        </tr>\r\n    </table>\r\n");

            
            #line 234 "..\..\Views\IsoFormProjDelegateService\info.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" id=\"tbProj\"");

WriteLiteral(" style=\"padding:5px;height:auto; display:none;\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchProj\"");

WriteLiteral(" name=\"Words\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'ProjName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

            
            #line 238 "..\..\Views\IsoFormProjDelegateService\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
