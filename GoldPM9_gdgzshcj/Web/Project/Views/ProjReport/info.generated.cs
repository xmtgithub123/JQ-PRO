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
    
    #line 2 "..\..\Views\ProjReport\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProjReport/info.cshtml")]
    public partial class _Views_ProjReport_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ProjSub>
    {
        public _Views_ProjReport_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'ProjSubForm',//formid提交需要用到
            buttonTypes: ['close'],//默认按钮
            beforeFormSubmit: function () {
                debugger;
                var _Id = $(""#Id"").val();
                var _SubNumber = $(""#SubNumber"").val();
                var bit=0;
                $.ajax({
                    doBackResult: false,
                    url: '");

            
            #line 15 "..\..\Views\ProjReport\info.cshtml"
                     Write(Url.Action("GetSubNumberCount", "ProjSub"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                    data: { Number: _SubNumber, Id: _Id },
                    async: false,
                    success: function (data) {
                        bit=data
                    }
                })
                if (bit > 0) {
                    JQ.dialog.warning('存在相同的外委项目编号:[' + _SubNumber + ""]"");
                    return false;
                }
            },
            flow: {
                flowNodeID: """);

            
            #line 28 "..\..\Views\ProjReport\info.cshtml"
                         Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                flowMultiSignID: \"");

            
            #line 29 "..\..\Views\ProjReport\info.cshtml"
                              Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                url: \"");

            
            #line 30 "..\..\Views\ProjReport\info.cshtml"
                  Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                instance: \"_flowInstance\",\r\n                processor: \"Proje" +
"ct,Project.FlowProcessor.ProjSubProcessor\",\r\n                refID: \"");

            
            #line 33 "..\..\Views\ProjReport\info.cshtml"
                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@""",
                refTable: ""ProjSub""
            },
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                var tab = $('#mainTab').tabs('getSelected');
                var index = $('#mainTab').tabs('getTabIndex', tab);
                $(""#mainTab"").find(""iframe"")[index].contentWindow.refreshGrid();
                _flowInstance.$form.parent().dialog(""close"");
            }
        });
        //外委负责人
        JQ.combotree.selEmp({ id: 'SubEmpId', width: '98%'});
        //选人员
        if(");

            
            #line 46 "..\..\Views\ProjReport\info.cshtml"
      Write(ViewData["SubEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("!=0&&");

            
            #line 46 "..\..\Views\ProjReport\info.cshtml"
                                 Write(ViewData["SubEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral("!=0)\r\n        {\r\n            $(\"#SubEmpId\").combotree(\'setValue\',");

            
            #line 48 "..\..\Views\ProjReport\info.cshtml"
                                           Write(ViewData["SubEmpId1"]);

            
            #line default
            #line hidden
WriteLiteral("+\"-\"+ ");

            
            #line 48 "..\..\Views\ProjReport\info.cshtml"
                                                                       Write(ViewData["SubEmpId2"]);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n        $(\"#projId\").val(");

            
            #line 50 "..\..\Views\ProjReport\info.cshtml"
                    Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n\r\n        if(");

            
            #line 52 "..\..\Views\ProjReport\info.cshtml"
      Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n            $(\"#ProjInfo\").val(\'");

            
            #line 54 "..\..\Views\ProjReport\info.cshtml"
                           Write(ViewBag.ProjInfo);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n        }\r\n    });\r\n\r\n    function SelectProjectInfo() {\r\n        JQ.dialog." +
"dialog({\r\n            title: \"选择项目信息\",\r\n            url: \'");

            
            #line 61 "..\..\Views\ProjReport\info.cshtml"
             Write(Url.Action("ListInfo", "Project", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"',
            width: '1024',
            height: '100%',
            JQID: 'ShowProjectTreeGridList',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }

    JQ.combobox.common({
        id: 'CustomerID',
        toolbar: '#tbCustomer',//工具栏的容器ID
        url: '");

            
            #line 73 "..\..\Views\ProjReport\info.cshtml"
         Write(Url.Action("GetCustomerList", "BussCustomer", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"',
        panelWidth: 650,
        panelHeight: 320,
        valueField: 'Id',
        textField: 'CustName',
        editable: false,
        multiple: false,
        pagination: true,//是否分页
        queryParams:{TypeID:1},
        JQSearch: {
            id: 'fullNameSearchCustomer',
            prompt: '客户名称',
            $panel: $(""#tbCustomer"")
        },
        columns: [[
                    { field: 'Id', hidden: true },
                    { field: 'CustName', title: '单位名称', width: 150 },
                    { field: 'CustAddress', title: '单位地址', width: 400 }
        ]],
    });

    var _ProjSubBack = function (param) {
        var json = JSON.parse(param);
        $(""#projId"").val(json.Id);
        $(""#ProjInfo"").textbox('setText', ""项目编号：""+json.ProjNumber+"",项目名称:""+json.ProjName);
    }
</script>
");

            
            #line 100 "..\..\Views\ProjReport\info.cshtml"
 using (Html.BeginForm("save", "ProjSub", FormMethod.Post, new { @area = "Project", @id = "ProjSubForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 102 "..\..\Views\ProjReport\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 102 "..\..\Views\ProjReport\info.cshtml"
                              


            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" style=\"padding: 15px; font-size: 20px; text-align: center; font-weight: bold; bo" +
"rder: none;\"");

WriteLiteral(">外委项目申请表</td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral("></th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("></td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委项目编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" id=\"SubNumber\"");

WriteLiteral(" name=\"SubNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4410), Tuple.Create("\"", 4434)
            
            #line 112 "..\..\Views\ProjReport\info.cshtml"
                                                                                          , Tuple.Create(Tuple.Create("", 4418), Tuple.Create<System.Object, System.Int32>(Model.SubNumber
            
            #line default
            #line hidden
, 4418), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请部门</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 117 "..\..\Views\ProjReport\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "CreatorDepId",
               isRequired = true,
               engName = "department",
               width = "40%",
               ids = Model.CreatorDepId.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral(" &nbsp;登记人\r\n                <input");

WriteLiteral(" name=\"CreatorEmpName\"");

WriteLiteral(" style=\"width:30%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,readonly:true\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4988), Tuple.Create("\"", 5017)
            
            #line 125 "..\..\Views\ProjReport\info.cshtml"
                                                                                  , Tuple.Create(Tuple.Create("", 4996), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 4996), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"CreationTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5197), Tuple.Create("\"", 5224)
            
            #line 128 "..\..\Views\ProjReport\info.cshtml"
                                            , Tuple.Create(Tuple.Create("", 5205), Tuple.Create<System.Object, System.Int32>(Model.CreationTime
            
            #line default
            #line hidden
, 5205), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" rowspan=\"2\"");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目信息</th>\r\n            <th");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" style=\"text-align:left\"");

WriteLiteral("><a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnAdd\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectProjectInfo()\"");

WriteLiteral(">选择项目信息</a></th>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" style=\"text-align:left\"");

WriteLiteral("><input");

WriteLiteral(" id=\"ProjInfo\"");

WriteLiteral(" name=\"ProjInfo\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:100%;\"");

WriteLiteral(" data-options=\"required:true,readonly:true\"");

WriteLiteral(" /></th>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委项目名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"SubName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5904), Tuple.Create("\"", 5926)
            
            #line 139 "..\..\Views\ProjReport\info.cshtml"
                                             , Tuple.Create(Tuple.Create("", 5912), Tuple.Create<System.Object, System.Int32>(Model.SubName
            
            #line default
            #line hidden
, 5912), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委项目阶段</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 142 "..\..\Views\ProjReport\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "SubPhase",
               isRequired = true,
               engName = "ProjectPhase",
               width = "98%",
               ids = Model.SubPhase.ToString(),
               isMult = true
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委专业</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 156 "..\..\Views\ProjReport\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "SubSpecial",
               isRequired = true,
               engName = "Special",
               width = "98%",
               ids = Model.SubSpecial.ToString(),
               isMult = true
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委负责人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"SubEmpId\"");

WriteLiteral(" name=\"SubEmpId\"");

WriteLiteral("></select>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">提供产品类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 174 "..\..\Views\ProjReport\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ColAttType1",
               isRequired = true,
               engName = "ProjSubProductType",
               width = "98%",
               ids = Model.ColAttType1.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委性质</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 185 "..\..\Views\ProjReport\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "SubType",
               isRequired = true,
               engName = "ProjSubType",
               width = "98%",
               ids = Model.SubType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate1\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7876), Tuple.Create("\"", 7902)
            
            #line 198 "..\..\Views\ProjReport\info.cshtml"
                               , Tuple.Create(Tuple.Create("", 7884), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate1
            
            #line default
            #line hidden
, 7884), false)
);

WriteLiteral(" />\r\n\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">综合评审时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate2\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8103), Tuple.Create("\"", 8129)
            
            #line 203 "..\..\Views\ProjReport\info.cshtml"
                               , Tuple.Create(Tuple.Create("", 8111), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate2
            
            #line default
            #line hidden
, 8111), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">审核时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"ColAttDate3\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8337), Tuple.Create("\"", 8363)
            
            #line 208 "..\..\Views\ProjReport\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 8345), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate3
            
            #line default
            #line hidden
, 8345), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">成品提交时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"ColAttDate4\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8530), Tuple.Create("\"", 8556)
            
            #line 210 "..\..\Views\ProjReport\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 8538), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate4
            
            #line default
            #line hidden
, 8538), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th>确认外委单位</th>\r\n            <" +
"td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustomerID\"");

WriteLiteral(" name=\"CustomerID\"");

WriteLiteral(" data-options=\"required:true,multiple:false\"");

WriteLiteral(" style=\"width:99%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8774), Tuple.Create("\"", 8842)
            
            #line 215 "..\..\Views\ProjReport\info.cshtml"
                                                , Tuple.Create(Tuple.Create("", 8782), Tuple.Create<System.Object, System.Int32>(@Model.ColAttType2==0 ? "" :@Model.ColAttType2.ToString()
            
            #line default
            #line hidden
, 8782), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委原因</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubReason\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 221 "..\..\Views\ProjReport\info.cshtml"
                                                                                                                                                     Write(Model.SubReason);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委内容</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubContent\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 227 "..\..\Views\ProjReport\info.cshtml"
                                                                                                                                                      Write(Model.SubContent);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">验收要求</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubAcceptance\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 233 "..\..\Views\ProjReport\info.cshtml"
                                                                                                                                                         Write(Model.SubAcceptance);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ColAttVal1\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 239 "..\..\Views\ProjReport\info.cshtml"
                                                                                                                                                      Write(Model.ColAttVal1);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n  " +
"              上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 247 "..\..\Views\ProjReport\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 247 "..\..\Views\ProjReport\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "ProjSub";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 252 "..\..\Views\ProjReport\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 252 "..\..\Views\ProjReport\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 257 "..\..\Views\ProjReport\info.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" id=\"tbCustomer\"");

WriteLiteral(" style=\"padding:5px;height:auto; display:none;\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" id=\"fullNameSearchCustomer\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'CustName\', \'Contract\': \'like\' }\"");

WriteLiteral(" />\r\n    </div>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"projId\"");

WriteLiteral(" name=\"projId\"");

WriteLiteral(" />\r\n");

            
            #line 262 "..\..\Views\ProjReport\info.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
