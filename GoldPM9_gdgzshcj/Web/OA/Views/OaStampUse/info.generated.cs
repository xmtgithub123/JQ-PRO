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
    
    #line 2 "..\..\Views\OaStampUse\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaStampUse/info.cshtml")]
    public partial class _Views_OaStampUse_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaStampUse>
    {
        public _Views_OaStampUse_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    var old_originalSteps = new Array();
    JQ.form.submitInit({
        formid: 'OaStampUseForm',//formid提交需要用到
        buttonTypes: ['close', 'exportForm'],//默认按钮
        docName: 'StamUse',
        ExportName: '印鉴申请单',
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }
        ,
        flow: {
            flowNodeID: """);

            
            #line 16 "..\..\Views\OaStampUse\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 17 "..\..\Views\OaStampUse\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"OA,OA.FlowPro" +
"cessor.OaStampUseFlow\",\r\n            refID: \"");

            
            #line 20 "..\..\Views\OaStampUse\info.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@""",
            refTable: ""OaStampUse"",
            isShowSave: false,
            filterSteps: function (originalSteps) {
                var orderStep = 0;

                old_originalSteps = originalSteps.concat();

                if (this.stepOrder == 1) {
                    if (stampType == ""技术类"") {

                    } else {
                        if ('");

            
            #line 32 "..\..\Views\OaStampUse\info.cshtml"
                        Write(ViewBag.StampType);

            
            #line default
            #line hidden
WriteLiteral("\' == \'技术类\') {\r\n                            orderStep = 2;\r\n                      " +
"  } else {\r\n                            if (\'");

            
            #line 35 "..\..\Views\OaStampUse\info.cshtml"
                            Write(ViewBag.isExist);

            
            #line default
            #line hidden
WriteLiteral(@"' == '1') {
                                orderStep = 3;
                            } else {
                                orderStep = 2;
                            }
                        }
                    }
                } else if (this.stepOrder == 2) {
                    if ('");

            
            #line 43 "..\..\Views\OaStampUse\info.cshtml"
                    Write(ViewBag.StampType);

            
            #line default
            #line hidden
WriteLiteral(@"' == '技术类') {
                        orderStep = 5;
                    }
                }

                var steps = Enumerable.From(old_originalSteps).Where(""x => x.Order == "" + orderStep + """").FirstOrDefault();

                if (steps != undefined) {
                    originalSteps.splice(0, originalSteps.length);
                    if ('");

            
            #line 52 "..\..\Views\OaStampUse\info.cshtml"
                    Write(ViewBag.StampType);

            
            #line default
            #line hidden
WriteLiteral(@"' == '技术类') {
                        steps.Users = [{""ID"":719,""Name"":""伍翠萍""}];
                    }
                    originalSteps.push(steps);
                }
            },
            beforeFormSubmit: function () {
                var projN = $('#ProjID').combobox('getText');
                var projName = $('#ProjIName').val();
                if (projN != projName)
                {
                    $('#ProjIName').val(projN);
                    $('#ProjI').val(0);
                }
            }
        },
        onBefore: function (resultArr) {
            resultArr.push({ Key: ""Permission"", Value: '");

            
            #line 69 "..\..\Views\OaStampUse\info.cshtml"
                                                   Write(ViewBag.isExport);

            
            #line default
            #line hidden
WriteLiteral(@"' });
        }
    });

    var stampType = '';
    var stampName = '';
    //返回回调刷新
    var _ProjCallBackSingLe = function (param) {
        $(""#StampID"").val(param[0]);
        stampType = param[1];
        stampName = param[2];
        $('#stampName').textbox(""setValue"", stampName);
        if (stampType == ""技术类"") {
            $(""#IsJSL"").show();
        } else {
            $(""#IsJSL"").hide();
        }

    }

    var _ProjCallBackFlowModelID = function (param) {

    }

    var selectText = """";

    JQ.combobox.common({
        id: 'ProjID',
        toolbar: '#tbProj',//工具栏的容器ID
        url: '");

            
            #line 98 "..\..\Views\OaStampUse\info.cshtml"
         Write(Url.Action("combogridJson", "Project",new { @area="Project"}));

            
            #line default
            #line hidden
WriteLiteral(@"',//请求的地址
        panelWidth: 900,
        panelHeight: 400,
        valueField: 'Id',
        textField: 'ProjName',
        //editable: false,
        multiple: false,
        pagination: true,//是否分页
        queryParams:
            {
                projId: '");

            
            #line 108 "..\..\Views\OaStampUse\info.cshtml"
                    Write(Model.ProjId);

            
            #line default
            #line hidden
WriteLiteral(@"',
                Words: $(""#fullNameSearchProj"").val()
            },
        columns: [[
            { title: '项目编号', field: 'ProjNumber', width: 150, align: 'center', sortable: true },
            { title: '项目名称', field: 'ProjName', width: 400, align: 'center', sortable: true },
            { title: '项目阶段', field: 'PhaseName', width: 300, align: 'center', sortable: true }
        ]],
        onSelect: function (val, row) {
            $(""#ProjI"").val(row.Id);
            $(""#ProjIName"").val(row.ProjName);
            $(""#ProjNumber"").val(row.ProjNumber);
            selectText = row.ProjName;

        },
        onLoadSuccess: function () {
            if ('");

            
            #line 124 "..\..\Views\OaStampUse\info.cshtml"
            Write(ViewBag.StampType);

            
            #line default
            #line hidden
WriteLiteral(@"' == ""技术类"") {
                $(""#IsJSL"").show();
            } else {
                $(""#IsJSL"").hide();
            }

            var selectRow = $(this).combogrid('grid').datagrid('getSelected');

            if (selectRow != undefined) {
                selectText = $(this).combogrid('getText');
            }
            else{
                $(this).combogrid('setText', selectText);
            }

            if (Number('");

            
            #line 139 "..\..\Views\OaStampUse\info.cshtml"
                   Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\') > 0) {\r\n                $(this).combogrid(\'setText\', \'");

            
            #line 140 "..\..\Views\OaStampUse\info.cshtml"
                                         Write(Model.ProjName);

            
            #line default
            #line hidden
WriteLiteral(@"');
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

            
            #line 157 "..\..\Views\OaStampUse\info.cshtml"
                            Write(Model.ProjId);

            
            #line default
            #line hidden
WriteLiteral(@"',
                        Words: $(""#fullNameSearchProj"").val()
                    }
                });
        }
    });

    $(""#stampName"").textbox({
        onClickButton: function () {
            JQ.dialog.dialog({
                title: ""选择用章"",
                url: '");

            
            #line 168 "..\..\Views\OaStampUse\info.cshtml"
                 Write(Url.Action("FilterList", "OaStampUse", new { @area = "oa" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                width: \'1000\',\r\n                height: \'100%\',\r\n            " +
"});\r\n        }\r\n    });\r\n\r\n</script>\r\n");

            
            #line 176 "..\..\Views\OaStampUse\info.cshtml"
 using (Html.BeginForm("save", "OaStampUse", FormMethod.Post, new { @area = "Oa", @id = "OaStampUseForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 178 "..\..\Views\OaStampUse\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 178 "..\..\Views\OaStampUse\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:20px;font-weight:bold;margin-top:1" +
"0px;\"");

WriteLiteral(">\r\n            <a>印章申请登记表</a>\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:right;margin-right:20px;margin-top:10px;margin-bottom:10px;\"");

WriteLiteral(">\r\n            编号<input");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" style=\"width:140px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6501), Tuple.Create("\"", 6522)
            
            #line 184 "..\..\Views\OaStampUse\info.cshtml"
                                           , Tuple.Create(Tuple.Create("", 6509), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 6509), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">申请人</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"s\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteLiteral(" data-options=\"readonly:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6753), Tuple.Create("\"", 6782)
            
            #line 189 "..\..\Views\OaStampUse\info.cshtml"
                                                       , Tuple.Create(Tuple.Create("", 6761), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 6761), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">是否外借</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <select");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" id=\"IsWJ\"");

WriteLiteral(" name=\"IsWJ\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6945), Tuple.Create("\"", 6964)
            
            #line 193 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 6953), Tuple.Create<System.Object, System.Int32>(Model.IsWJ
            
            #line default
            #line hidden
, 6953), false)
);

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(">否</option>\r\n                    <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">是</option>\r\n                </select>\r\n                <input");

WriteLiteral(" name=\"StampUseDate\"");

WriteLiteral(" id=\"StampUseDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7188), Tuple.Create("\"", 7215)
            
            #line 197 "..\..\Views\OaStampUse\info.cshtml"
                     , Tuple.Create(Tuple.Create("", 7196), Tuple.Create<System.Object, System.Int32>(Model.StampUseDate
            
            #line default
            #line hidden
, 7196), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" height=\"40px\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"ProjID\"");

WriteLiteral(" name=\"ProjID\"");

WriteLiteral(" data-options=\"multiple:false\"");

WriteLiteral(" style=\"width:100%;height:40px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7503), Tuple.Create("\"", 7561)
            
            #line 205 "..\..\Views\OaStampUse\info.cshtml"
                                      , Tuple.Create(Tuple.Create("", 7511), Tuple.Create<System.Object, System.Int32>(@Model.ProjId==0 ? "" :@Model.ProjId.ToString()
            
            #line default
            #line hidden
, 7511), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"Words\"");

WriteLiteral(" name=\"Words\"");

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjIDName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7686), Tuple.Create("\"", 7711)
            
            #line 207 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 7694), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjName
            
            #line default
            #line hidden
, 7694), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjI\"");

WriteLiteral(" name=\"ProjI\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7777), Tuple.Create("\"", 7798)
            
            #line 208 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 7785), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 7785), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjIName\"");

WriteLiteral(" name=\"ProjIName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7872), Tuple.Create("\"", 7895)
            
            #line 209 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 7880), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 7880), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" name=\"ProjNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7971), Tuple.Create("\"", 7998)
            
            #line 210 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 7979), Tuple.Create<System.Object, System.Int32>(ViewBag.ProjNumber
            
            #line default
            #line hidden
, 7979), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                印章名称\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" style=\"width:100%;\"");

WriteLiteral(" id=\"stampName\"");

WriteLiteral(" name=\"stampName\"");

WriteLiteral(" data-options=\"buttonText:\'选择印章\',buttonIcon:\'l-btn-icon fa fa-search\',prompt:\'请选择" +
"印章...\',editable:false\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8353), Tuple.Create("\"", 8420)
            
            #line 218 "..\..\Views\OaStampUse\info.cshtml"
                                                                                                                                 , Tuple.Create(Tuple.Create("", 8361), Tuple.Create<System.Object, System.Int32>(Model.StampID>0?Model.FK_OaStampUse_StampID.StampName:""
            
            #line default
            #line hidden
, 8361), false)
);

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampID\"");

WriteLiteral(" id=\"StampID\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8488), Tuple.Create("\"", 8510)
            
            #line 219 "..\..\Views\OaStampUse\info.cshtml"
, Tuple.Create(Tuple.Create("", 8496), Tuple.Create<System.Object, System.Int32>(Model.StampID
            
            #line default
            #line hidden
, 8496), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请理由</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"StampEmpesult\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8786), Tuple.Create("\"", 8814)
            
            #line 225 "..\..\Views\OaStampUse\info.cshtml"
                                                                                 , Tuple.Create(Tuple.Create("", 8794), Tuple.Create<System.Object, System.Int32>(Model.StampEmpesult
            
            #line default
            #line hidden
, 8794), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"IsJSL\"");

WriteLiteral(">\r\n            <th>份数</th>\r\n            <td><input");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" id=\"FS\"");

WriteLiteral(" name=\"FS\"");

WriteLiteral(" data-options=\"min:0,precision:0\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9021), Tuple.Create("\"", 9038)
            
            #line 230 "..\..\Views\OaStampUse\info.cshtml"
                                             , Tuple.Create(Tuple.Create("", 9029), Tuple.Create<System.Object, System.Int32>(Model.FS
            
            #line default
            #line hidden
, 9029), false)
);

WriteLiteral(" /></td>\r\n            <th>文件签收人</th>\r\n            <td><input");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" id=\"WJQSR\"");

WriteLiteral(" name=\"WJQSR\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9165), Tuple.Create("\"", 9185)
            
            #line 232 "..\..\Views\OaStampUse\info.cshtml"
                , Tuple.Create(Tuple.Create("", 9173), Tuple.Create<System.Object, System.Int32>(Model.WJQSR
            
            #line default
            #line hidden
, 9173), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"remark\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9438), Tuple.Create("\"", 9459)
            
            #line 237 "..\..\Views\OaStampUse\info.cshtml"
                                                                          , Tuple.Create(Tuple.Create("", 9446), Tuple.Create<System.Object, System.Int32>(Model.remark
            
            #line default
            #line hidden
, 9446), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 250 "..\..\Views\OaStampUse\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 250 "..\..\Views\OaStampUse\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaStampUse";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 255 "..\..\Views\OaStampUse\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 255 "..\..\Views\OaStampUse\info.cshtml"
                                                                                                   
                
            
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

            
            #line 263 "..\..\Views\OaStampUse\info.cshtml"
                    }
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
