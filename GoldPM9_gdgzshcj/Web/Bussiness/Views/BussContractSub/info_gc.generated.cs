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
    
    #line 2 "..\..\Views\BussContractSub\info_gc.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractSub/info_gc.cshtml")]
    public partial class _Views_BussContractSub_info_gc_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussContractSub>
    {
        public _Views_BussContractSub_info_gc_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n    $(function () {\r\n        $(\"#ProjSubIDs\").val(\'");

            
            #line 5 "..\..\Views\BussContractSub\info_gc.cshtml"
                         Write(ViewBag.ProjSubIDs);

            
            #line default
            #line hidden
WriteLiteral(@"');
        JQ.form.submitInit({
            formid: 'BussContractSubForm',//formid提交需要用到
            docName: 'BussContractSub',
            buttonTypes: ['exportForm', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                var tab = $('#mainTab').tabs('getSelected');
                var index = $('#mainTab').tabs('getTabIndex', tab);
                $(""#mainTab"").find(""iframe"")[index].contentWindow.refreshGrid();
                _flowInstance.$form.parent().dialog(""close"");
            },
            beforeFormSubmit: function () {
                var _Id = $(""#Id"").val();
                var _ConSubNumber = $(""#ConSubNumber"").textbox('getText');
                var bit;
                //判断编号重名
                $.ajax({
                    doBackResult: false,
                    url: '");

            
            #line 23 "..\..\Views\BussContractSub\info_gc.cshtml"
                     Write(Url.Action("GetConSubCodeCount", "BussContractSub"));

            
            #line default
            #line hidden
WriteLiteral(@"',
                    data: { ConSubNumber: _ConSubNumber, Id: _Id },
                    async: false,
                    success: function (data) {
                        bit = data
                    }
                })
                if (bit > 0) {
                    JQ.dialog.warning('存在相同的外委合同编号:[' + _ConSubNumber + ""]"");
                    return false;
                }
            },
            flow: {
                flowNodeID: """);

            
            #line 36 "..\..\Views\BussContractSub\info_gc.cshtml"
                         Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                flowMultiSignID: \"");

            
            #line 37 "..\..\Views\BussContractSub\info_gc.cshtml"
                              Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                url: \"");

            
            #line 38 "..\..\Views\BussContractSub\info_gc.cshtml"
                  Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                instance: \"_flowInstance\",\r\n                processor: \"Bussi" +
"ness,Bussiness.FlowProcessor.BussContractSub\",\r\n                refID: \"");

            
            #line 41 "..\..\Views\BussContractSub\info_gc.cshtml"
                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                refTable: \"BussContractSub_GC\",\r\n                dataCreator:" +
" \"");

            
            #line 43 "..\..\Views\BussContractSub\info_gc.cshtml"
                          Write(ViewBag.CreatorEmpId );

            
            #line default
            #line hidden
WriteLiteral(@"""
            },
            onBefore: function () {
                $(""#ProjSubTableGrid"").datagrid('hideColumn', ""ck"");
            },
            onAfter: function () {
                $(""#ProjSubTableGrid"").datagrid('showColumn', ""ck"");
            },
        });

        $('#ProjSubTableGrid').datagrid({
            JQPrimaryID: 'Id',//主键的字段
            JQID: 'ProjSubTableGrid',//数据表格ID
            JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
            url: '");

            
            #line 57 "..\..\Views\BussContractSub\info_gc.cshtml"
             Write(Url.Action("GetProjSubList", "BussContractSub", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"',
            toolbar: '#ProjSubTablePanel',//工具栏的容器ID
            pagination: false,
            queryParams: { ProjSubIDs: $(""#ProjSubIDs"").val() },
            columns: [[
                     { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
                     { title: '外委项目编号', field: 'SubNumber', width: 100, halign: 'center', align: 'left', sortable: true, exportWidth: 100 },
                     { title: '外委项目名称', field: 'SubName', width: 160, halign: 'center', align: 'left', sortable: true, exportWidth: 100 },
                     { title: '项目编号', field: 'ProjNumber', width: 100, halign: 'center', align: 'left', sortable: true, exportWidth: 80 },
                     { title: '项目名称', field: 'ProjName', width: 250, halign: 'center', align: 'left', sortable: true, exportWidth: 80 },
                     { title: '外委单位', field: 'CustName', width: 200, halign: 'center', align: 'left', sortable: true, exportWidth: 80 }
            ]],
        });

        JQ.combobox.selEmp({
            id: 'CreateEmpId'
        });
    });

    function SelectProjSub() {
        JQ.dialog.dialog({
            title: ""选择项目外委"",
            url: '");

            
            #line 79 "..\..\Views\BussContractSub\info_gc.cshtml"
             Write(Url.Action("selectprojsub", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=GC&typeID=1&FilterConSubID=1',
            width: '1024',
            height: '100%',
            JQID: 'ProjSubTableGrid',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }
    var _ProjCallBack = function (param) {
        var _ProjSubIDs = $(""#ProjSubIDs"").val().split("","");
        for (var i = 0; i < param.length; i++) {
            if (_ProjSubIDs.indexOf(param[i]) < 0) { _ProjSubIDs.push(param[i]); }
        }
        $(""#ProjSubIDs"").val(_ProjSubIDs);
        $(""#ProjSubTableGrid"").datagrid({
            url: '");

            
            #line 94 "..\..\Views\BussContractSub\info_gc.cshtml"
             Write(Url.Action("GetProjSubList", "ProjSub", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=GC',
            queryParams: { ProjSubIDs: $(""#ProjSubIDs"").val() },
        });
    }


    function DelProjSub() {
        var updateIds = """";
        var rows = $('#ProjSubTableGrid').datagrid(""getSelections"");
        var _ProjSubIDs = $(""#ProjSubIDs"").val().split("","");
        for (var j = 0; j < rows.length; j++) {
            var index = $('#ProjSubTableGrid').datagrid('getRowIndex', rows[j]);
            var _ProjSubId = rows[j].Id;
            var delIndex = _ProjSubIDs.indexOf(_ProjSubId);
            if (delIndex > -1) {
                _ProjSubIDs.splice(delIndex, 1);
                $(""#ProjSubIDs"").val(_ProjSubIDs);
            }
            updateIds += _ProjSubId;
            $('#ProjSubTableGrid').datagrid('deleteRow', index);
        }
        if (updateIds.length > 0)
        {
            $.ajax({
                doBackResult: false,
                url: """);

            
            #line 119 "..\..\Views\BussContractSub\info_gc.cshtml"
                  Write(Url.Action("UpdateProjSubByConSubIDs", "ProjSub", new { @area= "Project" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\",\r\n                data: { Ids: updateIds }\r\n            })\r\n    " +
"    }\r\n    }\r\n</script>\r\n\r\n");

            
            #line 126 "..\..\Views\BussContractSub\info_gc.cshtml"
 using (Html.BeginForm("save", "BussContractSub", FormMethod.Post, new { @area = "Bussiness", @id = "BussContractSubForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 128 "..\..\Views\BussContractSub\info_gc.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 128 "..\..\Views\BussContractSub\info_gc.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"GC\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th>\r\n                外委项目名称\r\n            </th>\r\n   " +
"         <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"ProjSubTableGrid\"");

WriteLiteral(" bookmark=\"DetailGrid\"");

WriteLiteral("></table>\r\n                <div");

WriteLiteral(" id=\"ProjSubTablePanel\"");

WriteLiteral(" jqpanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" jqpanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"SelectProjSub\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectProjSub()\"");

WriteLiteral(">选择外委项目</a>\r\n                        <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"DelProjSub\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" onclick=\"DelProjSub()\"");

WriteLiteral(">删除</a>\r\n                    </span>\r\n                </div>\r\n            </td>\r\n" +
"        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ConSubNumber\"");

WriteLiteral(" name=\"ConSubNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6989), Tuple.Create("\"", 7016)
            
            #line 148 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                                    , Tuple.Create(Tuple.Create("", 6997), Tuple.Create<System.Object, System.Int32>(Model.ConSubNumber
            
            #line default
            #line hidden
, 6997), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConSubName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7249), Tuple.Create("\"", 7274)
            
            #line 152 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 7257), Tuple.Create<System.Object, System.Int32>(Model.ConSubName
            
            #line default
            #line hidden
, 7257), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同类型</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 159 "..\..\Views\BussContractSub\info_gc.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConSubType",
               isRequired = true,
               engName = "ConSubType",
               width = "98%",
               ids = Model.ConSubType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">签订状态</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 171 "..\..\Views\BussContractSub\info_gc.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConSubStatus",
               isRequired = true,
               engName = "ConSubStatus",
               width = "98%",
               ids = Model.ConSubStatus.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">顾客名称</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ConSubCustId\"");

WriteLiteral(" name=\"ConSubCustId\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8247), Tuple.Create("\"", 8312)
            
            #line 184 "..\..\Views\BussContractSub\info_gc.cshtml"
        , Tuple.Create(Tuple.Create("", 8255), Tuple.Create<System.Object, System.Int32>(Model.ConSubCustId==0?"":Model.ConSubCustId.ToString()
            
            #line default
            #line hidden
, 8255), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(">\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    $(function () {\r\n                        $(\"#ConSubCustId\"" +
").combobox({\r\n                            url: \'");

            
            #line 189 "..\..\Views\BussContractSub\info_gc.cshtml"
                             Write(Url.Action("GetSubCustomerListCombobox", "BussCustomer", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"',
                            valueField: 'Id',
                            textField: 'CustName',
                            filter:function (q,row) {
                                var opts = $(this).combobox('options');
                                return row[opts.textField].indexOf(q) > -1;
                            },
                            onLoadSuccess: function (data) {
                                console.log(data);
                                if ($(""#ConSubCustId"").combobox(""getText"") == """") {
                                    $(""#ConSubCustId"").combobox(""setText"", $(""#CustName"").val());
                                }
                            }
                        })

                    })
                </script>
            </td>
        </tr>
        <tr>
            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同金额</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConSubFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteLiteral(" data-options=\"precision:2\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9690), Tuple.Create("\"", 9714)
            
            #line 211 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                   , Tuple.Create(Tuple.Create("", 9698), Tuple.Create<System.Object, System.Int32>(Model.ConSubFee
            
            #line default
            #line hidden
, 9698), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 216 "..\..\Views\BussContractSub\info_gc.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConSubCategory",
               isRequired = true,
               engName = "ConSubCategory",
               width = "98%",
               ids = Model.ConSubCategory.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记人员</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" id=\"CreateEmpId\"");

WriteLiteral(" name=\"CreateEmpId\"");

WriteAttribute("JQvalue", Tuple.Create(" JQvalue=\"", 10279), Tuple.Create("\"", 10307)
            
            #line 230 "..\..\Views\BussContractSub\info_gc.cshtml"
, Tuple.Create(Tuple.Create("", 10289), Tuple.Create<System.Object, System.Int32>(Model.CreateEmpId
            
            #line default
            #line hidden
, 10289), false)
);

WriteLiteral("></select>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CreateDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10510), Tuple.Create("\"", 10535)
            
            #line 234 "..\..\Views\BussContractSub\info_gc.cshtml"
                             , Tuple.Create(Tuple.Create("", 10518), Tuple.Create<System.Object, System.Int32>(Model.CreateDate
            
            #line default
            #line hidden
, 10518), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">签订日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConSubDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10760), Tuple.Create("\"", 10785)
            
            #line 240 "..\..\Views\BussContractSub\info_gc.cshtml"
                             , Tuple.Create(Tuple.Create("", 10768), Tuple.Create<System.Object, System.Int32>(Model.ConSubDate
            
            #line default
            #line hidden
, 10768), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">归档盒号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ArchNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10988), Tuple.Create("\"", 11013)
            
            #line 245 "..\..\Views\BussContractSub\info_gc.cshtml"
                                  , Tuple.Create(Tuple.Create("", 10996), Tuple.Create<System.Object, System.Int32>(Model.ArchNumber
            
            #line default
            #line hidden
, 10996), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">付款条件</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n\r\n                <textarea");

WriteLiteral(" name=\"Condition\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 252 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                                                                                                     Write(Model.Condition);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">外委合同备注</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ConSubNote\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteLiteral(">");

            
            #line 259 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                                                                                                      Write(Model.ConSubNote);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" style=\"width:98%\"");

WriteLiteral(">\r\n");

            
            #line 272 "..\..\Views\BussContractSub\info_gc.cshtml"
                
            
            #line default
            #line hidden
            
            #line 272 "..\..\Views\BussContractSub\info_gc.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BussContractSub";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 277 "..\..\Views\BussContractSub\info_gc.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 277 "..\..\Views\BussContractSub\info_gc.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

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

WriteLiteral(" id=\"ProjSubIDs\"");

WriteLiteral(" name=\"ProjSubIDs\"");

WriteLiteral(" />\r\n");

            
            #line 286 "..\..\Views\BussContractSub\info_gc.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
