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
    
    #line 2 "..\..\Views\ProjManage\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProjManage/info.cshtml")]
    public partial class _Views_ProjManage_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ProjManage>
    {
        public _Views_ProjManage_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    JQ.form.submitInit({
        formid: 'ProjManageForm',//formid提交需要用到
        buttonTypes: ['close'],//默认按钮
        beforeFormSubmit: function () {
            var datas = $(""#ProjTable"").datagrid(""getData"");
            if (datas.rows.length == 0) {
                JQ.dialog.warning(""请先选择项目!"");
                return false;
            }
        },
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            //JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            //JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            var _tempFrame=window.top.document.getElementById(""");

            
            #line 17 "..\..\Views\ProjManage\info.cshtml"
                                                           Write(Request.QueryString["iframeID"]);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n            if(_tempFrame){\r\n                _tempFrame.contentWindow.refres" +
"hGrid();\r\n            }\r\n            _flowInstance.$form.parent().dialog(\"close\"" +
");\r\n        },\r\n        flow: {\r\n            flowNodeID: \"");

            
            #line 24 "..\..\Views\ProjManage\info.cshtml"
                     Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            url: \"");

            
            #line 25 "..\..\Views\ProjManage\info.cshtml"
              Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            instance: \"_flowInstance\",\r\n            processor: \"Engineering,E" +
"ngineering.FlowProcessor.ProjProcessor\",\r\n            onInit: function () {\r\n   " +
"             _flowInstance = this;\r\n            },\r\n            refID: \"");

            
            #line 31 "..\..\Views\ProjManage\info.cshtml"
                Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n            refTable: \"ProjManage\",\r\n            dataCreator: \"");

            
            #line 33 "..\..\Views\ProjManage\info.cshtml"
                      Write(ViewBag.CreatorEmpId);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n        }\r\n    });\r\n</script>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
                        var insert = function (row) {
                            $('#ProjTable').datagrid('loadData', { total: 0, rows: [] });
                            $('#ProjTable').datagrid('appendRow', {
                                ProjNumber: row.ProjNumber,
                                ProjName: row.ProjName
                            });
                            $(""#EngineeringID"").val(row.Id);
                        }

                        $(function () {
                            debugger;

                            console.log(""Model.EngineeringID:");

            
            #line 50 "..\..\Views\ProjManage\info.cshtml"
                                                         Write(Model.EngineeringID);

            
            #line default
            #line hidden
WriteLiteral(@""");

                            $('#ProjTable').datagrid({
                                JQPrimaryID: 'Id',//主键的字段
                                JQID: 'ProjTable',//数据表格ID
                                JQLoadingType: 'datagrid',//数据表格类型 [datagrid,treegrid]
                                //url: '");

            
            #line 56 "..\..\Views\ProjManage\info.cshtml"
                                   Write(Url.Action("ProjectPlanListJsonAll", "DesTask", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?InTaskGroupId=");

            
            #line 56 "..\..\Views\ProjManage\info.cshtml"
                                                                                                                             Write(Model.EngineeringID);

            
            #line default
            #line hidden
WriteLiteral(@"',
                                toolbar: '#ProjTablePanel',//工具栏的容器ID
                                pagination: false,
                                columns: [[
                                    { field: 'ck', align: 'center', checkbox: true, JQIsExclude: true },
                                    { field: 'ProjNumber', title: '项目编号', halign: 'center', width: ""30%"", },
                                    { field: 'ProjName', title: '项目名称', halign: 'center', width: '65%', },
                                ]],
                            });
                            $('#ProjTable').datagrid('insertRow', {
                                index: 0,	// 索引从0开始
                                row: {
                                    ProjNumber: '");

            
            #line 68 "..\..\Views\ProjManage\info.cshtml"
                                            Write(ViewBag.ProjNumber);

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                                    ProjName: \'");

            
            #line 69 "..\..\Views\ProjManage\info.cshtml"
                                          Write(ViewBag.ProjName);

            
            #line default
            #line hidden
WriteLiteral(@"'
                                }
                            });

                        });

                        //返回回调刷新
                        var _ProjCallBack = function (param) {
                            console.log(""param:"" + param);
                            console.log(""EngineeringID:"" + $(""#EngineeringID"").val());

                            $(""#EngineeringID"").val(param);

                            $(""#ProjTable"").datagrid({
                                url: '");

            
            #line 83 "..\..\Views\ProjManage\info.cshtml"
                                 Write(Url.Action("ProjectPlanListJsonAll", "DesTask", new { @area = "Design" }));

            
            #line default
            #line hidden
WriteLiteral("?InTaskGroupId=\' + $(\"#EngineeringID\").val(),\r\n                            });\r\n " +
"                       }\r\n\r\n                        //选择工程\r\n                    " +
"    function selectEng() {\r\n                           // window.open(\'");

            
            #line 89 "..\..\Views\ProjManage\info.cshtml"
                                      Write(Url.Action("ChooseProjectList", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?TaskGroupId=' + $(""#EngineeringID"").val()','','width=530,height=200,location=no,menubar=no,status=no,scrollbars=no,resizable=no top=300,left=500')
                            // 暂存项目信息
                            //Save_Proj();
                            JQ.dialog.dialog({
                                title: ""选择项目"",
                                url: '");

            
            #line 94 "..\..\Views\ProjManage\info.cshtml"
                                 Write(Url.Action("ChooseProjectList", "EmpManage", new { @area = "Engineering" }));

            
            #line default
            #line hidden
WriteLiteral(@"?TaskGroupId=' + $(""#EngineeringID"").val(),
                                width: '1024',
                                height: '100%',
                                JQID: 'ProjTable',
                                JQLoadingType: 'datagrid',
                                iconCls: 'fa fa-list',

                            });
                        }

                        //删除
                        function DelEng() {
                            //Save_Proj();
                            var row = $('#ProjTable').datagrid('getSelections');
                            if (row.length < 1) {
                                window.top.JQ.dialog.warning('您必须选择至少一项！！！');
                            }
                            else {
                                $(""#EngineeringID"").val(0);
                                $('#ProjTable').datagrid('loadData', { total: 0, rows: [] });
                            }
                        }
</script>
");

            
            #line 117 "..\..\Views\ProjManage\info.cshtml"
 using (Html.BeginForm("save", "ProjManage", FormMethod.Post, new { @area = "Engineering", @id = "ProjManageForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 119 "..\..\Views\ProjManage\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 119 "..\..\Views\ProjManage\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                项目名称\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"EngineeringID\"");

WriteLiteral(" name=\"EngineeringID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6112), Tuple.Create("\"", 6142)
            
            #line 124 "..\..\Views\ProjManage\info.cshtml"
     , Tuple.Create(Tuple.Create("", 6120), Tuple.Create<System.Object, System.Int32>(Model.EngineeringID
            
            #line default
            #line hidden
, 6120), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"empid\"");

WriteLiteral(" name=\"empid\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6208), Tuple.Create("\"", 6247)
            
            #line 125 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 6216), Tuple.Create<System.Object, System.Int32>(Request.QueryString["empid"]
            
            #line default
            #line hidden
, 6216), false)
);

WriteLiteral(" />\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"ProjTable\"");

WriteLiteral("></table>\r\n                <div");

WriteLiteral(" id=\"ProjTablePanel\"");

WriteLiteral(" JQPanel=\"searchPanel\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n                    <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" name=\"selectEng\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"selectEng()\"");

WriteLiteral(">选择项目</a>\r\n                        <a");

WriteLiteral(" name=\"DelEng\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-minus-circle\'\"");

WriteLiteral(" onclick=\"DelEng()\"");

WriteLiteral(">删除</a>\r\n                    </span>\r\n                   \r\n                </div>" +
"\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">成本指标</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Cost\"");

WriteLiteral(" style=\"width:98%;height:100px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7053), Tuple.Create("\"", 7072)
            
            #line 141 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 7061), Tuple.Create<System.Object, System.Int32>(Model.Cost
            
            #line default
            #line hidden
, 7061), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">安全目标</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Safe\"");

WriteLiteral(" style=\"width:98%;height:100px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7339), Tuple.Create("\"", 7358)
            
            #line 147 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 7347), Tuple.Create<System.Object, System.Int32>(Model.Safe
            
            #line default
            #line hidden
, 7347), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">工期目标</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Finished\"");

WriteLiteral(" style=\"width:98%;height:100px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7629), Tuple.Create("\"", 7652)
            
            #line 153 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 7637), Tuple.Create<System.Object, System.Int32>(Model.Finished
            
            #line default
            #line hidden
, 7637), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">资料目标</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Doc\"");

WriteLiteral(" style=\"width:98%;height:100px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7918), Tuple.Create("\"", 7936)
            
            #line 159 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 7926), Tuple.Create<System.Object, System.Int32>(Model.Doc
            
            #line default
            #line hidden
, 7926), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">特殊要求</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"Note\"");

WriteLiteral(" style=\"width:98%;height:100px\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8203), Tuple.Create("\"", 8222)
            
            #line 165 "..\..\Views\ProjManage\info.cshtml"
, Tuple.Create(Tuple.Create("", 8211), Tuple.Create<System.Object, System.Int32>(Model.Note
            
            #line default
            #line hidden
, 8211), false)
);

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n          " +
"      上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 173 "..\..\Views\ProjManage\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 173 "..\..\Views\ProjManage\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "ProjManage";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 178 "..\..\Views\ProjManage\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 178 "..\..\Views\ProjManage\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 183 "..\..\Views\ProjManage\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
