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
    
    #line 3 "..\..\Views\BussCustomerRemember\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussCustomerRemember/info.cshtml")]
    public partial class _Views_BussCustomerRemember_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussCustomerRemember>
    {
        public _Views_BussCustomerRemember_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

        JQ.form.submitInit({
            formid: 'BussCustomerRememberForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
        });
 

 

    var selectValue = """";
    var selectText = """";

    JQ.combobox.common({
        id: 'ProjID',
        toolbar: '#tbProj',//工具栏的容器ID
        url: '");

            
            #line 24 "..\..\Views\BussCustomerRemember\info.cshtml"
         Write(Url.Action("combogridJson", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
        panelWidth: 550,
        panelHeight: 400,
        valueField: 'Id',
        textField: 'ProjNumber',
        editable: false,
        multiple: false,
        pagination: true,//是否分页
        queryParams:
            {
                projId: '");

            
            #line 34 "..\..\Views\BussCustomerRemember\info.cshtml"
                    Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(@"',
                Words: $(""#fullNameSearchProj"").val()
            },
        columns: [[
            { title: '项目编号', field: 'ProjNumber', width: 100, align: 'center', sortable: true },
            { title: '项目名称', field: 'ProjName', width: 260, align: 'center', sortable: true },
            { title: '项目阶段', field: 'PhaseName', width: 140, align: 'center', sortable: true }
        ]],
        onSelect: function (val, row) {
            $(""#ProjName"").text(row.ProjName);
            selectValue = row.Id;
            selectText = row.ProjName;
        },
        onLoadSuccess: function () {
            $(""#ProjName"").text('");

            
            #line 48 "..\..\Views\BussCustomerRemember\info.cshtml"
                            Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral(@"');
            var selectRow = $(this).combogrid('grid').datagrid('getSelected');
            if (selectRow != undefined) {
                selectText = $(this).combogrid('getText');
            }
            if (selectRow == undefined) {
                $(this).combogrid('setText', selectText);
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
                    $dg: $(""#ProjID"").combogrid('grid'),
                    loadingType: 'datagrid',
                    queryParams:
                             {
                                 projId: '");

            
            #line 71 "..\..\Views\BussCustomerRemember\info.cshtml"
                                     Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                                 Words: $(\"#fullNameSearchProj\").val()\r\n     " +
"                        }\r\n                });\r\n        }\r\n\r\n    });\r\n</script>\r" +
"\n");

            
            #line 79 "..\..\Views\BussCustomerRemember\info.cshtml"
 using (Html.BeginForm("save", "BussCustomerRemember", FormMethod.Post, new { @area = "Bussiness", @id = "BussCustomerRememberForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 81 "..\..\Views\BussCustomerRemember\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 81 "..\..\Views\BussCustomerRemember\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">顾客名称</th>\r\n            <td");

WriteLiteral(" width=\"38%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 87 "..\..\Views\BussCustomerRemember\info.cshtml"
           Write(ViewBag.CustName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">\r\n                客户类别\r\n            </th>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 93 "..\..\Views\BussCustomerRemember\info.cshtml"
           Write(ViewBag.TypeName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" style=\"width:15%;\"");

WriteLiteral(">\r\n                项目编号\r\n            </th>\r\n            <td");

WriteLiteral(" style=\"width:35%;\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"required:true,multiple:false\"");

WriteLiteral(" style=\"width:99%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3494), Tuple.Create("\"", 3552)
            
            #line 102 "..\..\Views\BussCustomerRemember\info.cshtml"
                                        , Tuple.Create(Tuple.Create("", 3502), Tuple.Create<System.Object, System.Int32>(@Model.ProjID==0 ? "" :@Model.ProjID.ToString()
            
            #line default
            #line hidden
, 3502), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" style=\"width:15%;\"");

WriteLiteral(">\r\n                项目名称(自动获取)\r\n            </th>\r\n            <td");

WriteLiteral(" style=\"width:35%;\"");

WriteLiteral(">\r\n                <label");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral("></label>\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">记事时间</th>\r\n            <td");

WriteLiteral(" width=\"38%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RememberTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3968), Tuple.Create("\"", 3995)
            
            #line 115 "..\..\Views\BussCustomerRemember\info.cshtml"
                                , Tuple.Create(Tuple.Create("", 3976), Tuple.Create<System.Object, System.Int32>(Model.RememberTime
            
            #line default
            #line hidden
, 3976), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">记事类别</th>\r\n            <td");

WriteLiteral(" width=\"38%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 119 "..\..\Views\BussCustomerRemember\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "RememberType",
               isRequired = true,
               engName = "LogProperty",
               width = "98%",
               ids = Model.RememberType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">记事人员</th>\r\n            <td");

WriteLiteral(" width=\"38%\"");

WriteLiteral(">\r\n");

WriteLiteral("                 ");

            
            #line 134 "..\..\Views\BussCustomerRemember\info.cshtml"
            Write(ViewBag.EmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">记事部门</th>\r\n            <td");

WriteLiteral(" width=\"38%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 138 "..\..\Views\BussCustomerRemember\info.cshtml"
           Write(ViewBag.DeptName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"12%\"");

WriteLiteral(">备注说明</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"RememberNote\"");

WriteLiteral(" style=\"width:98%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4932), Tuple.Create("\"", 4959)
            
            #line 144 "..\..\Views\BussCustomerRemember\info.cshtml"
                                                                                 , Tuple.Create(Tuple.Create("", 4940), Tuple.Create<System.Object, System.Int32>(Model.RememberNote
            
            #line default
            #line hidden
, 4940), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>\r\n      " +
"          上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 154 "..\..\Views\BussCustomerRemember\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 154 "..\..\Views\BussCustomerRemember\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BussCustRemember";
                    uploader.Name = "uploadfile1";
                    
            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\BussCustomerRemember\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 159 "..\..\Views\BussCustomerRemember\info.cshtml"
                                                                                                   
                
            
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

WriteLiteral(" JQWhereOptions=\"{ Key: \'ProjName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

            
            #line 167 "..\..\Views\BussCustomerRemember\info.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591