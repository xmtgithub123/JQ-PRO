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
    
    #line 2 "..\..\Views\IsoTechnologyChange\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/IsoTechnologyChange/info.cshtml")]
    public partial class _Views_IsoTechnologyChange_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.IsoTechnologyChange>
    {
        public _Views_IsoTechnologyChange_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'IsoTechnologyChangeForm',//formid提交需要用到
        buttonTypes: ['exportForm', 'close'],//默认按钮
        docName: 'IsoTechnologyChange',
        ExportName: '技术档案修改申请单',
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        }, flow: {
            flowNodeID: """);

            
            #line 13 "..\..\Views\IsoTechnologyChange\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            flowMultiSignID: \"");

            
            #line 14 "..\..\Views\IsoTechnologyChange\info.cshtml"
                          Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 15 "..\..\Views\IsoTechnologyChange\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"ISO,ISO.FlowP" +
"rocessor.IsoTechnologyChangeFlow\",\r\n            refID: \"");

            
            #line 18 "..\..\Views\IsoTechnologyChange\info.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"IsoTechnologyChange\",\r\n            dataCreator: \"");

            
            #line 20 "..\..\Views\IsoTechnologyChange\info.cshtml"
                      Write(ViewBag.CreateEmpId );

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        },\r\n        onBefore: function (resultArr) {\r\n            resultArr.pu" +
"sh({ Key: \"Permission\", Value: \'");

            
            #line 23 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                   Write(ViewBag.Permission);

            
            #line default
            #line hidden
WriteLiteral(@"' });
        }
    });

    //确定选择回调
    var _ProjCallBack = function (row) {
        var data = row[0];
        $(""#ProjId"").val(data.Id);
        $(""#ProjName"").textbox(""setValue"", data.ProjName);
        $(""#ProjNumber"").textbox(""setValue"", data.ProjNumber);
        $(""#ProjPhaseName"").val(data.ProjPhaseName);
        $(""#ProjPhaseId"").val(data.ProjPhaseId);
    }

    //选择项目
    function SelectMainProject() {
        JQ.dialog.dialog({
            title: ""选择项目"",
            url: '");

            
            #line 41 "..\..\Views\IsoTechnologyChange\info.cshtml"
             Write(Url.Action("ChooseProjectList1", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=&TaskGroupId=' + $(""#ProjId"").val(),
            width: '1024',
            height: '100%',
            JQID: 'ProjTable',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-list',
        });
    }

    $(':checkbox[type=""checkbox""]').each(function () {
        $(this).click(function () {
            var ck1 = $(""#check1"").is("":checked"") == true ? ""修改"" : ""无效"";
            var ck2 = $(""#check2"").is("":checked"") == true ? ""替换"" : ""无效"";
            var ck3 = $(""#check3"").is("":checked"") == true ? ""作废"" : ""无效"";
            debugger;
            var ckVal = """";
            ckVal = '□修改      □替换      □作废'.replace('□' + ck1, '☑' + ck1);
            ckVal = ckVal.replace('□' + ck3, '☑' + ck3);
            ckVal = ckVal.replace('□' + ck3, '☑' + ck3);
            $('#checkOutput').val(ckVal);
            debugger;
            $(""#checkVal"").val(ck1 + "","" + ck2 + "","" + ck3);

            //if ($(this).attr('checked')) {



            //    $(this).attr('checked', 'checked');
            //    $('#checkVal').val($(this).next().text());
            //    var replaceText = $('#checkVal').val();
            //    $('#checkOutput').val('□修改      □替换      □作废'.replace('□' + replaceText, '☑' + replaceText));
            //}
        });
    });

    $(function () {
        $('#checkOutput').val('□修改      □替换      □作废');
        $('td label').each(function () {
            if ('");

            
            #line 79 "..\..\Views\IsoTechnologyChange\info.cshtml"
            Write(Model.ApplicationType);

            
            #line default
            #line hidden
WriteLiteral("\'.indexOf($(this).text()) > -1) {\r\n                $(this).click();\r\n            " +
"        $(\'#checkOutput\').val($(\'#checkOutput\').val().replace(\'□\' + $(this).text" +
"(), \'☑\' + $(this).text()));\r\n            }\r\n        })\r\n    })\r\n</script>\r\n");

            
            #line 86 "..\..\Views\IsoTechnologyChange\info.cshtml"
 using (Html.BeginForm("save", "IsoTechnologyChange", FormMethod.Post, new { @area = "Iso", @id = "IsoTechnologyChangeForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 88 "..\..\Views\IsoTechnologyChange\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 88 "..\..\Views\IsoTechnologyChange\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" style=\"width:100%;text-align:center;font-size:20px;font-weight:bold;margin-top:1" +
"0px;\"");

WriteLiteral(">\r\n            <a>技术档案修改、替换、作废申请单</a>\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:left;margin-left:20px;margin-top:10px;\"");

WriteLiteral(">\r\n            表<label>");

            
            #line 94 "..\..\Views\IsoTechnologyChange\info.cshtml"
               Write(Model.TableNumber);

            
            #line default
            #line hidden
WriteLiteral("</label><input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"TableNumber\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4035), Tuple.Create("\"", 4061)
            
            #line 94 "..\..\Views\IsoTechnologyChange\info.cshtml"
             , Tuple.Create(Tuple.Create("", 4043), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 4043), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <div");

WriteLiteral(" style=\"float:right;margin-right:20px;margin-top:10px;margin-bottom:10px;\"");

WriteLiteral(">\r\n            编号：<input");

WriteLiteral(" id=\"Number\"");

WriteLiteral(" name=\"Number\"");

WriteLiteral(" style=\"width:140px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4288), Tuple.Create("\"", 4309)
            
            #line 97 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                            , Tuple.Create(Tuple.Create("", 4296), Tuple.Create<System.Object, System.Int32>(Model.Number
            
            #line default
            #line hidden
, 4296), false)
);

WriteLiteral(" />\r\n        </div>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ProjNumber\"");

WriteLiteral(" id=\"ProjNumber\"");

WriteLiteral(" style=\"width:60%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4537), Tuple.Create("\"", 4562)
            
            #line 102 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                   , Tuple.Create(Tuple.Create("", 4545), Tuple.Create<System.Object, System.Int32>(Model.ProjNumber
            
            #line default
            #line hidden
, 4545), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseId\"");

WriteLiteral(" id=\"ProjPhaseId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4640), Tuple.Create("\"", 4666)
            
            #line 103 "..\..\Views\IsoTechnologyChange\info.cshtml"
, Tuple.Create(Tuple.Create("", 4648), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseId
            
            #line default
            #line hidden
, 4648), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ProjPhaseName\"");

WriteLiteral(" id=\"ProjPhaseName\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4748), Tuple.Create("\"", 4776)
            
            #line 104 "..\..\Views\IsoTechnologyChange\info.cshtml"
    , Tuple.Create(Tuple.Create("", 4756), Tuple.Create<System.Object, System.Int32>(Model.ProjPhaseName
            
            #line default
            #line hidden
, 4756), false)
);

WriteLiteral(" />\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" name=\"btnChoice\"");

WriteLiteral(" id=\"btnChoice\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" onclick=\"SelectMainProject()\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(">选择项目</a>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"ProjId\"");

WriteLiteral(" name=\"ProjId\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5126), Tuple.Create("\"", 5147)
            
            #line 109 "..\..\Views\IsoTechnologyChange\info.cshtml"
, Tuple.Create(Tuple.Create("", 5134), Tuple.Create<System.Object, System.Int32>(Model.ProjId
            
            #line default
            #line hidden
, 5134), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"ProjName\"");

WriteLiteral(" name=\"ProjName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true,editable:false\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5316), Tuple.Create("\"", 5339)
            
            #line 110 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                                                           , Tuple.Create(Tuple.Create("", 5324), Tuple.Create<System.Object, System.Int32>(Model.ProjName
            
            #line default
            #line hidden
, 5324), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>档案号</th>\r\n" +
"            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"FileNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5558), Tuple.Create("\"", 5583)
            
            #line 117 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                   , Tuple.Create(Tuple.Create("", 5566), Tuple.Create<System.Object, System.Int32>(Model.FileNumber
            
            #line default
            #line hidden
, 5566), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>申请类别</th>\r\n " +
"           <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(" id=\"td\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check1\"");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" />\r\n                <label");

WriteLiteral(" for=\"check1\"");

WriteLiteral(">修改</label>\r\n                <span />\r\n                <input");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check2\"");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" />\r\n                <label");

WriteLiteral(" for=\"check2\"");

WriteLiteral(">替换</label>\r\n                <span />\r\n                <input");

WriteLiteral(" name=\"check\"");

WriteLiteral(" id=\"check3\"");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" />\r\n                <label");

WriteLiteral(" for=\"check3\"");

WriteLiteral(">作废</label>\r\n                <span />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"checkVal\"");

WriteLiteral(" name=\"checkVal\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6194), Tuple.Create("\"", 6224)
            
            #line 132 "..\..\Views\IsoTechnologyChange\info.cshtml"
, Tuple.Create(Tuple.Create("", 6202), Tuple.Create<System.Object, System.Int32>(Model.ApplicationType
            
            #line default
            #line hidden
, 6202), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"checkOutput\"");

WriteLiteral(" name=\"checkOutput\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>申请原因</th>\r\n " +
"           <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ApplicationResult\"");

WriteLiteral(" style=\"width:98%;height:90px\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,4000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6569), Tuple.Create("\"", 6601)
            
            #line 139 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                                                     , Tuple.Create(Tuple.Create("", 6577), Tuple.Create<System.Object, System.Int32>(Model.ApplicationResult
            
            #line default
            #line hidden
, 6577), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">申请单位</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ApplicationCompany\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6840), Tuple.Create("\"", 6873)
            
            #line 145 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                            , Tuple.Create(Tuple.Create("", 6848), Tuple.Create<System.Object, System.Int32>(Model.ApplicationCompany
            
            #line default
            #line hidden
, 6848), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>备注</th>\r\n   " +
"         <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Remark\"");

WriteLiteral(" style=\"width:98%;height:90px;\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,4000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7129), Tuple.Create("\"", 7150)
            
            #line 151 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                                           , Tuple.Create(Tuple.Create("", 7137), Tuple.Create<System.Object, System.Int32>(Model.Remark
            
            #line default
            #line hidden
, 7137), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 163 "..\..\Views\IsoTechnologyChange\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 163 "..\..\Views\IsoTechnologyChange\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "IsoTechnologyChange";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 168 "..\..\Views\IsoTechnologyChange\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 168 "..\..\Views\IsoTechnologyChange\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 173 "..\..\Views\IsoTechnologyChange\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
