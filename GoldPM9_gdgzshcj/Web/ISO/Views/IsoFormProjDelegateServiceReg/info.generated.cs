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
    
    #line 2 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoFormProjDelegateServiceReg/info.cshtml")]
    public partial class _Views_IsoFormProjDelegateServiceReg_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoForm>
    {
        public _Views_IsoFormProjDelegateServiceReg_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    JQ.form.submitInit({\r\n        formid: \'IsoFormProjDelegateServiceRegForm\'," +
"//formid提交需要用到\r\n        buttonTypes: ");

            
            #line 6 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                Write(Html.Raw(ViewBag.Permission));

            
            #line default
            #line hidden
WriteLiteral(@",//权限按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
    });
    $(""#FormDate"").datebox({ readonly: true })
    $(""#MaterialName"").textbox({ required: true })
    
    var selectValue="""";
    var selectText="""";

    JQ.combobox.common({
        id: 'ProjID',
        toolbar: '#tbProj',//工具栏的容器ID
        url: '");

            
            #line 21 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
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

            
            #line 31 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
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

            
            #line 49 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                           Write(ViewBag.PhaseID);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjEmpName\").text(\'");

            
            #line 50 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                               Write(ViewBag.ProjEmpName);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjNumber\").text(\'");

            
            #line 51 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                              Write(ViewBag.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#ProjEmpId\").text(\'");

            
            #line 52 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
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

            
            #line 78 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                    Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                                 Words:$(\"#fullNameSearchProj\").val()\r\n      " +
"                       }\r\n                });\r\n        }\r\n\r\n    });\r\n\r\n\r\n");

WriteLiteral("    ");

            
            #line 87 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
Write(Html.Raw(ViewBag.Upload));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n");

            
            #line 90 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
 using (Html.BeginForm("save", "IsoFormProjDelegateServiceReg", FormMethod.Post, new { @area = "Iso", @id = "IsoFormProjDelegateServiceRegForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 92 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
Write(Html.HiddenFor(m => m.FormID));

            
            #line default
            #line hidden
            
            #line 92 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                  

            
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

WriteLiteral(">工代登记</span>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目名称\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"required:true,multiple:false\"");

WriteLiteral(" style=\"width:99%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3835), Tuple.Create("\"", 3893)
            
            #line 106 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                        , Tuple.Create(Tuple.Create("", 3843), Tuple.Create<System.Object, System.Int32>(@Model.ProjID==0 ? "" :@Model.ProjID.ToString()
            
            #line default
            #line hidden
, 3843), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Words\"");

WriteLiteral(" name=\"Words\"");

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目编号\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral("></label>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                设计阶段\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"PhaseID\"");

WriteLiteral("></label>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目负责人\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"ProjEmpName\"");

WriteLiteral("></label>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                登记日期\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 137 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
           Write(Model.CreationTime.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                登记人\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 145 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                服务开始时间\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ColAttDate1\"");

WriteLiteral(" name=\"ColAttDate1\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5333), Tuple.Create("\"", 5359)
            
            #line 154 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                  , Tuple.Create(Tuple.Create("", 5341), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate1
            
            #line default
            #line hidden
, 5341), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                服务结束时间\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ColAttDate2\"");

WriteLiteral(" name=\"ColAttDate2\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5593), Tuple.Create("\"", 5619)
            
            #line 160 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                  , Tuple.Create(Tuple.Create("", 5601), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate2
            
            #line default
            #line hidden
, 5601), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 5919), Tuple.Create("\"", 5942)
            
            #line 168 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 5927), Tuple.Create<System.Object, System.Int32>(Model.FormNote
            
            #line default
            #line hidden
, 5927), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 176 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 176 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                  
    var uploader = new DataModel.FileUploader();
    uploader.RefID = Model.FormID;
    uploader.RefTable = "IsoForm";
    uploader.Name = "uploadfile1";
        
            
            #line default
            #line hidden
            
            #line 181 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
    Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 181 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
                                                                                       
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <div");

WriteLiteral(" id=\"tbProj\"");

WriteLiteral(" style=\"padding:5px;height:auto; display:none;\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchProj\"");

WriteLiteral(" name=\"Words\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" jqwhereoptions=\"{ Key: \'ProjName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

            
            #line 189 "..\..\Views\IsoFormProjDelegateServiceReg\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
