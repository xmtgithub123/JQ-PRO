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
    
    #line 2 "..\..\Views\OaCarUse\Createinfo.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/OaCarUse/Createinfo.cshtml")]
    public partial class _Views_OaCarUse_Createinfo_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.OaCarUse>
    {
        public _Views_OaCarUse_Createinfo_cshtml()
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
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        flow: {
            flowNodeID: """);

            
            #line 12 "..\..\Views\OaCarUse\Createinfo.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 13 "..\..\Views\OaCarUse\Createinfo.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 14 "..\..\Views\OaCarUse\Createinfo.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"OA,OA.FlowPro" +
"cessor.OaCarUseFlow\",\r\n            refID: \"");

            
            #line 17 "..\..\Views\OaCarUse\Createinfo.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            dataCreator: \"");

            
            #line 18 "..\..\Views\OaCarUse\Createinfo.cshtml"
                      Write(ViewBag.CreatorEmpId);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            permission: \'");

            
            #line 19 "..\..\Views\OaCarUse\Createinfo.cshtml"
                    Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            refTable: \"CarUse\",\r\n            beforeFormSubmit: function (mode" +
", action) {\r\n                if (!window[_uploader_unique_id].isUploaded) {\r\n   " +
"                 JQ.dialog.warning(\"还存在未上传完成的文件，无法保存！\");\r\n                    re" +
"turn false;\r\n                }\r\n\r\n                var plan = $(\'#DateArrivePlan\'" +
").datetimebox(\'getValue\')\r\n                var arr = $(\'#DateLeavePlan\').datetim" +
"ebox(\'getValue\');\r\n                if (arr > plan) {\r\n                    $.mess" +
"ager.alert(\"提示\", \"出车时间应小于归队时间\");\r\n                    return false;\r\n           " +
"     }\r\n            },\r\n            onInit: function () {\r\n\r\n            }\r\n    " +
"    }\r\n    });\r\n\r\n\r\n\r\n    JQ.combobox.selEmpEx({\r\n        id: \'UseLeaderEmp\',\r\n " +
"   });\r\n    JQ.textbox.selEmp({\r\n        id: \'UsePeople\',\r\n        setID: \'UsePe" +
"ople\'\r\n    });\r\n    var _CarCallBack = function (param) {\r\n        var BackInfo " +
"= param;\r\n        var carid = BackInfo[\"Id\"];\r\n        var carName = BackInfo[\"C" +
"arName\"];\r\n        var carNumber = BackInfo[\"CarNumber\"];\r\n        var carInfo =" +
" carName + \"[\" + carNumber + \"]\";\r\n        //$(\"#CarID\").val(carid);\r\n        $(" +
"\"input[name=\'CarName\']\").val(carInfo);\r\n        $(\"#hCarID\").val(carid);\r\n    }\r" +
"\n    function Setcar(useid) {\r\n        var leave = $(\"input[name=\'DateLeavePlan\'" +
"]\").val();\r\n        var arrive = $(\"input[name=\'DateArrivePlan\']\").val()\r\n      " +
"  if (leave == \" 00:00:00\" || leave == \"\") {\r\n            JQ.dialog.error(\"请先选择计" +
"划出车时间！\");\r\n            return;\r\n        }\r\n        if (arrive == \" 00:00:00\" || " +
"arrive == \"\") {\r\n            JQ.dialog.error(\"请先选择计划归队时间！\");\r\n            return" +
";\r\n        }\r\n        JQ.dialog.dialog({\r\n            title: \"选择车辆\",\r\n          " +
"  url: \'");

            
            #line 72 "..\..\Views\OaCarUse\Createinfo.cshtml"
             Write(Url.Action("selectcar"));

            
            #line default
            #line hidden
WriteLiteral(@"?useid=' + useid + '&leave=' + leave + '&arrive=' + arrive,
            width: '1024',
            height: '100%',
            JQID: 'CarTable',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-list',

        });
    }
    // $(""#UseLeaderEmp"").combobox(""setvalue"", """");

    $(document).ready(function () {
        debugger
        if ('");

            
            #line 85 "..\..\Views\OaCarUse\Createinfo.cshtml"
        Write(Model.IsNeedDriver);

            
            #line default
            #line hidden
WriteLiteral(@"' == ""1"")
            $('#yes').attr('checked', 'checked');
        else
            $('#no').attr('checked', 'checked');

        $('input[name=needDriver]').click(function () {
            $(this).attr('checked', 'checked').siblings().removeAttr('checked');
            debugger
            $('#IsNeedDriver').val(0);
            if (this.id == ""yes"")
                $('#IsNeedDriver').val(1);
        });


    })

    function myformatter(date) {
        return date.format('yyyy-MM-dd hh') == '1900-01-01 00' ? """" : date.format('yyyy-MM-dd hh');
        //return date.format('yyyy-MM-dd hh');
    }
</script>
");

            
            #line 106 "..\..\Views\OaCarUse\Createinfo.cshtml"
 using (Html.BeginForm("save", "OaCarUse", FormMethod.Post, new { @area = "Oa", @id = "OaCarUseForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 108 "..\..\Views\OaCarUse\Createinfo.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 108 "..\..\Views\OaCarUse\Createinfo.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                用途\r\n                ");

WriteLiteral("\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"hFormType\"");

WriteLiteral(" id=\"hFormType\"");

WriteLiteral(" value=\"SetCar\"");

WriteLiteral(" />\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePurpose\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4350), Tuple.Create("\"", 4375)
            
            #line 117 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 4358), Tuple.Create<System.Object, System.Int32>(Model.UsePurpose
            
            #line default
            #line hidden
, 4358), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">去向</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UsePlace\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4604), Tuple.Create("\"", 4627)
            
            #line 124 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                  , Tuple.Create(Tuple.Create("", 4612), Tuple.Create<System.Object, System.Int32>(Model.UsePlace
            
            #line default
            #line hidden
, 4612), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">计划出车时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateLeavePlan\"");

WriteLiteral(" id=\"DateLeavePlan\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4862), Tuple.Create("\"", 4890)
            
            #line 131 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                    , Tuple.Create(Tuple.Create("", 4870), Tuple.Create<System.Object, System.Int32>(Model.DateLeavePlan
            
            #line default
            #line hidden
, 4870), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">计划归队时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateArrivePlan\"");

WriteLiteral(" id=\"DateArrivePlan\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datetimebox\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5161), Tuple.Create("\"", 5190)
            
            #line 137 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                      , Tuple.Create(Tuple.Create("", 5169), Tuple.Create<System.Object, System.Int32>(Model.DateArrivePlan
            
            #line default
            #line hidden
, 5169), false)
);

WriteLiteral(" data-options=\"formatter:myformatter\"");

WriteLiteral("/>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">带车责任人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseLeaderEmp\"");

WriteLiteral(" id=\"UseLeaderEmp\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5429), Tuple.Create("\"", 5456)
            
            #line 143 "..\..\Views\OaCarUse\Createinfo.cshtml"
       , Tuple.Create(Tuple.Create("", 5437), Tuple.Create<System.Object, System.Int32>(Model.UseLeaderEmp
            
            #line default
            #line hidden
, 5437), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">同车人员</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n\r\n                <input");

WriteLiteral(" name=\"UsePeople\"");

WriteLiteral(" id=\"UsePeople\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5682), Tuple.Create("\"", 5706)
            
            #line 151 "..\..\Views\OaCarUse\Createinfo.cshtml"
                           , Tuple.Create(Tuple.Create("", 5690), Tuple.Create<System.Object, System.Int32>(Model.UsePeople
            
            #line default
            #line hidden
, 5690), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">乘车人数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"UsePeopleNum\"");

WriteLiteral(" name=\"UsePeopleNum\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" style=\"width:80px;\"");

WriteLiteral("\r\n                       required=\"required\"");

WriteLiteral(" data-options=\"min:0,max:40,editable:true\"");

WriteLiteral(" validType=\"number\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6043), Tuple.Create("\"", 6070)
            
            #line 158 "..\..\Views\OaCarUse\Createinfo.cshtml"
                               , Tuple.Create(Tuple.Create("", 6051), Tuple.Create<System.Object, System.Int32>(Model.UsePeopleNum
            
            #line default
            #line hidden
, 6051), false)
);

WriteLiteral(" />\r\n\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">是否需要司机</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                是 <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"needDriver\"");

WriteLiteral(" id=\"yes\"");

WriteLiteral("/>\r\n                否 <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" name=\"needDriver\"");

WriteLiteral(" id=\"no\"");

WriteLiteral("/>\r\n");

WriteLiteral("                ");

            
            #line 168 "..\..\Views\OaCarUse\Createinfo.cshtml"
           Write(Html.Hidden("IsNeedDriver",Model.IsNeedDriver));

            
            #line default
            #line hidden
WriteLiteral("\r\n                ");

WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                派车信息\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"hCarID\"");

WriteLiteral(" id=\"hCarID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6685), Tuple.Create("\"", 6705)
            
            #line 175 "..\..\Views\OaCarUse\Createinfo.cshtml"
, Tuple.Create(Tuple.Create("", 6693), Tuple.Create<System.Object, System.Int32>(Model.CarID
            
            #line default
            #line hidden
, 6693), false)
);

WriteLiteral(" />\r\n\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CarName\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" style=\"width:70%;\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6856), Tuple.Create("\"", 6886)
            
            #line 179 "..\..\Views\OaCarUse\Createinfo.cshtml"
                       , Tuple.Create(Tuple.Create("", 6864), Tuple.Create<System.Object, System.Int32>(ViewData["CarInfo"]
            
            #line default
            #line hidden
, 6864), false)
);

WriteLiteral(" />\r\n\r\n                <b");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" name=\"selectCar\"");

WriteAttribute("onclick", Tuple.Create(" onclick=\"", 7002), Tuple.Create("\"", 7029)
, Tuple.Create(Tuple.Create("", 7012), Tuple.Create("Setcar(", 7012), true)
            
            #line 181 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 7019), Tuple.Create<System.Object, System.Int32>(Model.Id
            
            #line default
            #line hidden
, 7019), false)
, Tuple.Create(Tuple.Create("", 7028), Tuple.Create(")", 7028), true)
);

WriteLiteral(">选择车辆</b>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">司机</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"UseCarDriver\"");

WriteLiteral(" style=\"width:50%;\"");

WriteLiteral(" name=\"UseCarDriver\"");

WriteLiteral(" value=\"\"");

WriteLiteral(">\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseNote\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7493), Tuple.Create("\"", 7515)
            
            #line 195 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                 , Tuple.Create(Tuple.Create("", 7501), Tuple.Create<System.Object, System.Int32>(Model.UseNote
            
            #line default
            #line hidden
, 7501), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n\r\n");

            
            #line 204 "..\..\Views\OaCarUse\Createinfo.cshtml"
                
            
            #line default
            #line hidden
            
            #line 204 "..\..\Views\OaCarUse\Createinfo.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "OaCarUse";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 209 "..\..\Views\OaCarUse\Createinfo.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 209 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <table");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">出车时公里数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"KmLeave\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,25]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8297), Tuple.Create("\"", 8319)
            
            #line 221 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                , Tuple.Create(Tuple.Create("", 8305), Tuple.Create<System.Object, System.Int32>(Model.KmLeave
            
            #line default
            #line hidden
, 8305), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">归队时公里数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"KmArrive\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,25]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8520), Tuple.Create("\"", 8543)
            
            #line 225 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                 , Tuple.Create(Tuple.Create("", 8528), Tuple.Create<System.Object, System.Int32>(Model.KmArrive
            
            #line default
            #line hidden
, 8528), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">实际出车时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateLeave\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8776), Tuple.Create("\"", 8800)
            
            #line 232 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                  , Tuple.Create(Tuple.Create("", 8784), Tuple.Create<System.Object, System.Int32>(Model.DateLeave
            
            #line default
            #line hidden
, 8784), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">实际归队时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"DateArrive\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9003), Tuple.Create("\"", 9028)
            
            #line 236 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                   , Tuple.Create(Tuple.Create("", 9011), Tuple.Create<System.Object, System.Int32>(Model.DateArrive
            
            #line default
            #line hidden
, 9011), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">过桥过路停车费(元)</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCarFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9271), Tuple.Create("\"", 9295)
            
            #line 243 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                        , Tuple.Create(Tuple.Create("", 9279), Tuple.Create<System.Object, System.Int32>(Model.UseCarFee
            
            #line default
            #line hidden
, 9279), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">报销单据张数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCarFeeInvoice\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9510), Tuple.Create("\"", 9541)
            
            #line 247 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                               , Tuple.Create(Tuple.Create("", 9518), Tuple.Create<System.Object, System.Int32>(Model.UseCarFeeInvoice
            
            #line default
            #line hidden
, 9518), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">驾驶员</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("></td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">检查备注</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCheckNote\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9849), Tuple.Create("\"", 9876)
            
            #line 256 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                      , Tuple.Create(Tuple.Create("", 9857), Tuple.Create<System.Object, System.Int32>(Model.UseCheckNote
            
            #line default
            #line hidden
, 9857), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">用户检查所属部门ID</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCheckInDepId\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10125), Tuple.Create("\"", 10155)
            
            #line 263 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 10133), Tuple.Create<System.Object, System.Int32>(Model.UseCheckInDepId
            
            #line default
            #line hidden
, 10133), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">用户检查 用户ID</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCheckInEmpId\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,10]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10372), Tuple.Create("\"", 10402)
            
            #line 267 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                             , Tuple.Create(Tuple.Create("", 10380), Tuple.Create<System.Object, System.Int32>(Model.UseCheckInEmpId
            
            #line default
            #line hidden
, 10380), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">用户检查 用户名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCheckInEmpName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10646), Tuple.Create("\"", 10678)
            
            #line 274 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                         , Tuple.Create(Tuple.Create("", 10654), Tuple.Create<System.Object, System.Int32>(Model.UseCheckInEmpName
            
            #line default
            #line hidden
, 10654), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">用户检查 登记日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"UseCheckInDateTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10892), Tuple.Create("\"", 10925)
            
            #line 278 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                          , Tuple.Create(Tuple.Create("", 10900), Tuple.Create<System.Object, System.Int32>(Model.UseCheckInDateTime
            
            #line default
            #line hidden
, 10900), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">里程数</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Distance\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11160), Tuple.Create("\"", 11183)
            
            #line 285 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                      , Tuple.Create(Tuple.Create("", 11168), Tuple.Create<System.Object, System.Int32>(Model.Distance
            
            #line default
            #line hidden
, 11168), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">耗油量</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"OilQuantity\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11390), Tuple.Create("\"", 11416)
            
            #line 289 "..\..\Views\OaCarUse\Createinfo.cshtml"
                                         , Tuple.Create(Tuple.Create("", 11398), Tuple.Create<System.Object, System.Int32>(Model.OilQuantity
            
            #line default
            #line hidden
, 11398), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n    </table>\r\n");

            
            #line 294 "..\..\Views\OaCarUse\Createinfo.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
