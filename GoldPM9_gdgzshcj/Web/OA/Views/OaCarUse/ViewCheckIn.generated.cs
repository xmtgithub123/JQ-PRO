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
    
    #line 3 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaCarUse/ViewCheckIn.cshtml")]
    public partial class _Views_OaCarUse_ViewCheckIn_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaCarUse>
    {
        public _Views_OaCarUse_ViewCheckIn_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'OaCarUseForm',//formid提交需要用到
        buttonTypes: ['close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            var tab = $('#mainTab').tabs('getSelected');
            var index = $('#mainTab').tabs('getTabIndex', tab);
            debugger;
            $(""#mainTab"").find(""iframe"")[index].contentWindow.refreshGrid();
            _flowInstance.$form.parent().dialog(""close"");
        },
        flow: {
        flowNodeID: """);

            
            #line 16 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                 Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        flowMultiSignID: \"");

            
            #line 17 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                      Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        url: \"");

            
            #line 18 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
          Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n        instance: \"_flowInstance\",\r\n        processor: \"OA,OA.FlowProcessor.O" +
"aCarUseFlow\",\r\n            dataCreator:\"");

            
            #line 21 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                     Write(ViewBag.CreatorEmpId);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            permission: \"");

            
            #line 22 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                    Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refID: \"");

            
            #line 23 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@""",
        refTable: ""CarUse"",
        beforeFormSubmit: function (mode, action) {
            var result = false;
            //  alert($(""#hCarID"").val());
            if ($(""#hCarID"").val() == ""0"")
            {
                $.messager.alert(""提示"", ""请选择车辆！！！"");
                return false;
            }

            $.ajax({
                url: """);

            
            #line 35 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                  Write(Url.Action("CheckCarIsUse", "OaCarUse",new { @area="Oa" }));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                type: \"POST\",\r\n            async: false,\r\n            data: {" +
" Id: \"");

            
            #line 38 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@""", hCarID: $(""#hCarID"").val() },
            // data: { hFormType: """", hCarID: """", Id: """" },
            success: function (data) {
                // alert(data);
                result = data;
            },
            complete:function(data)
            {
                // alert(1121212);
            }
        });
        if (!result) {
            $.messager.alert(""提示"", ""车辆使用中请重新选择！！！"");
        }
        return result;
    },
    onInit: function () {
        ");

WriteLiteral(@"
        }
    }
    });
    var _CarCallBack = function (param) {
        var BackInfo = param;
        var carid = BackInfo[""Id""];
        var carName = BackInfo[""CarName""];
        var carNumber = BackInfo[""CarNumber""];
        var carInfo = carName + ""["" + carNumber + ""]"";
        //$(""#CarID"").val(carid);
        $(""input[name='CarName']"").val(carInfo);
        $(""#hCarID"").val(carid);


    }
    //?lower='+$('input[name='DateLeavePlan']').val()+'&upper='+$('input[name='DateArrivePlan']').val()
    function selectcar(useid) {

        JQ.dialog.dialog({
            title: ""选择车辆"",
            url: '");

            
            #line 79 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
             Write(Url.Action("selectcar"));

            
            #line default
            #line hidden
WriteLiteral("?useid=\' + useid,\r\n            width: \'1024\',\r\n\r\n            height: \'100%\',\r\n   " +
"         JQID: \'CarTable\',\r\n            JQLoadingType: \'datagrid\',\r\n            " +
"iconCls: \'fa fa-list\',\r\n\r\n        });\r\n    }\r\n    $(function () {\r\n        if (\'" +
"");

            
            #line 90 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
        Write(Model.IsNeedDriver);

            
            #line default
            #line hidden
WriteLiteral("\' == \"1\")\r\n            $(\'#yes\').attr(\'checked\', \'checked\');\r\n        else\r\n     " +
"       $(\'#no\').attr(\'checked\', \'checked\');\r\n\r\n        $(\'#UseCarDriver\').combob" +
"ox({\r\n            url: \'");

            
            #line 96 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
             Write(Url.Action("GetDirverList", "OaCarUse", new { @area = "Oa"}));

            
            #line default
            #line hidden
WriteLiteral(@"',
            valueField: 'CarDriver',
            textField: 'CarDriver',
            panelHeight: ""auto"",
            editable: false, //不允许手动输入
        });
    });
    JQ.combobox.selEmpEx({
        id: 'UseLeaderEmp',
    });
    function myformatter(date) {
        return date.format('yyyy-MM-dd hh') == '1900-01-01 00' ? """" : date.format('yyyy-MM-dd hh');
        //return date.format('yyyy-MM-dd hh');
    }
</script>
");

            
            #line 111 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
 using (Html.BeginForm("save", "OaCarUse", FormMethod.Post, new { @area = "Oa", @id = "OaCarUseForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 113 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 113 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">用途\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"hFormType\"");

WriteLiteral(" id=\"hFormType\"");

WriteLiteral(" value=\"Finish\"");

WriteLiteral(" /></th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePurpose\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4361), Tuple.Create("\"", 4386)
            
            #line 120 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 4369), Tuple.Create<System.Object, System.Int32>(Model.UsePurpose
            
            #line default
            #line hidden
, 4369), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">去向</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePlace\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4616), Tuple.Create("\"", 4639)
            
            #line 127 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                  , Tuple.Create(Tuple.Create("", 4624), Tuple.Create<System.Object, System.Int32>(Model.UsePlace
            
            #line default
            #line hidden
, 4624), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">计划出车时间</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateLeavePlan\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4881), Tuple.Create("\"", 4909)
            
            #line 134 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                          , Tuple.Create(Tuple.Create("", 4889), Tuple.Create<System.Object, System.Int32>(Model.DateLeavePlan
            
            #line default
            #line hidden
, 4889), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">计划归队时间</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateArrivePlan\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5188), Tuple.Create("\"", 5217)
            
            #line 141 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                           , Tuple.Create(Tuple.Create("", 5196), Tuple.Create<System.Object, System.Int32>(Model.DateArrivePlan
            
            #line default
            #line hidden
, 5196), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">带车责任人</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                 <input");

WriteLiteral(" name=\"UseLeaderEmp\"");

WriteLiteral(" id=\"UseLeaderEmp\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5458), Tuple.Create("\"", 5485)
            
            #line 147 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
        , Tuple.Create(Tuple.Create("", 5466), Tuple.Create<System.Object, System.Int32>(Model.UseLeaderEmp
            
            #line default
            #line hidden
, 5466), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">同车人员</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePeople\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5718), Tuple.Create("\"", 5742)
            
            #line 154 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                   , Tuple.Create(Tuple.Create("", 5726), Tuple.Create<System.Object, System.Int32>(Model.UsePeople
            
            #line default
            #line hidden
, 5726), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">乘车人数</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePeopleNum\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5981), Tuple.Create("\"", 6008)
            
            #line 160 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                           , Tuple.Create(Tuple.Create("", 5989), Tuple.Create<System.Object, System.Int32>(Model.UsePeopleNum
            
            #line default
            #line hidden
, 5989), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否需要司机</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                是 <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"needDriver\"");

WriteLiteral(" disabled");

WriteLiteral(" id=\"yes\"");

WriteLiteral(" />\r\n                否 <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"needDriver\"");

WriteLiteral(" disabled");

WriteLiteral(" id=\"no\"");

WriteLiteral(" />\r\n");

WriteLiteral("                ");

            
            #line 169 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
           Write(Html.Hidden("IsNeedDriver", Model.IsNeedDriver));

            
            #line default
            #line hidden
WriteLiteral("\r\n                ");

WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   派车信息\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"hCarID\"");

WriteLiteral(" id=\"hCarID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6608), Tuple.Create("\"", 6628)
            
            #line 176 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
, Tuple.Create(Tuple.Create("", 6616), Tuple.Create<System.Object, System.Int32>(Model.CarID
            
            #line default
            #line hidden
, 6616), false)
);

WriteLiteral("  />\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CarName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" style=\"width:70%;\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6778), Tuple.Create("\"", 6808)
            
            #line 179 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                       , Tuple.Create(Tuple.Create("", 6786), Tuple.Create<System.Object, System.Int32>(ViewData["CarInfo"]
            
            #line default
            #line hidden
, 6786), false)
);

WriteLiteral(" readonly=\"readonly\"");

WriteLiteral("  />\r\n\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 6928), Tuple.Create("\"", 6958)
, Tuple.Create(Tuple.Create("", 6938), Tuple.Create("selectcar(", 6938), true)
            
            #line 181 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                               , Tuple.Create(Tuple.Create("", 6948), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 6948), false)
, Tuple.Create(Tuple.Create("", 6957), Tuple.Create(")", 6957), true)
);

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">选择车辆</a>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">司机</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"UseCarDriver\"");

WriteLiteral(" name=\"UseCarDriver\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7167), Tuple.Create("\"", 7194)
            
            #line 187 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
, Tuple.Create(Tuple.Create("", 7175), Tuple.Create<System.Object, System.Int32>(Model.UseCarDriver
            
            #line default
            #line hidden
, 7175), false)
);

WriteLiteral(">\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">备注</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseNote\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7421), Tuple.Create("\"", 7443)
            
            #line 194 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                 , Tuple.Create(Tuple.Create("", 7429), Tuple.Create<System.Object, System.Int32>(Model.UseNote
            
            #line default
            #line hidden
, 7429), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">实际出车时间</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateLeave\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7681), Tuple.Create("\"", 7705)
            
            #line 201 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                      , Tuple.Create(Tuple.Create("", 7689), Tuple.Create<System.Object, System.Int32>(Model.DateLeave
            
            #line default
            #line hidden
, 7689), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">实际归队时间</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateArrive\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7980), Tuple.Create("\"", 8005)
            
            #line 208 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                       , Tuple.Create(Tuple.Create("", 7988), Tuple.Create<System.Object, System.Int32>(Model.DateArrive
            
            #line default
            #line hidden
, 7988), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">出车时公里数</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"KmLeave\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("  min=\"0\"");

WriteLiteral(" validType=\"length[0,25]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8280), Tuple.Create("\"", 8302)
            
            #line 214 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                         , Tuple.Create(Tuple.Create("", 8288), Tuple.Create<System.Object, System.Int32>(Model.KmLeave
            
            #line default
            #line hidden
, 8288), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">归队时公里数</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"KmArrive\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral("  min=\"0\"");

WriteLiteral(" validType=\"length[0,25]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8542), Tuple.Create("\"", 8565)
            
            #line 220 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                          , Tuple.Create(Tuple.Create("", 8550), Tuple.Create<System.Object, System.Int32>(Model.KmArrive
            
            #line default
            #line hidden
, 8550), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">过桥过路停车费(元)</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCarFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("  min=\"0\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8818), Tuple.Create("\"", 8842)
            
            #line 227 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                                 , Tuple.Create(Tuple.Create("", 8826), Tuple.Create<System.Object, System.Int32>(Model.UseCarFee
            
            #line default
            #line hidden
, 8826), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">报销单据张数</th>\r\n             <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCarFeeInvoice\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral("  min=\"0\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9098), Tuple.Create("\"", 9129)
            
            #line 234 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                                        , Tuple.Create(Tuple.Create("", 9106), Tuple.Create<System.Object, System.Int32>(Model.UseCarFeeInvoice
            
            #line default
            #line hidden
, 9106), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n\r\n");

            
            #line 243 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                
            
            #line default
            #line hidden
            
            #line 243 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaCarUse";
                    uploader.Name = "UploadFile1";
                    uploader.AllowDelete = ViewBag.AllowDelete;
                    uploader.AllowUpload = ViewBag.AllowUpload;
                    
            
            #line default
            #line hidden
            
            #line 250 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 250 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 257 "..\..\Views\OaCarUse\ViewCheckIn.cshtml"

}

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
