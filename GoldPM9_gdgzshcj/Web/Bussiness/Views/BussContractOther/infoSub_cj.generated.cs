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
    
    #line 2 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractOther/infoSub_cj.cshtml")]
    public partial class _Views_BussContractOther_infoSub_cj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussContractOther>
    {
        public _Views_BussContractOther_infoSub_cj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

    JQ.form.submitInit({
        formid: 'BussContractOtherForm',//formid提交需要用到
        buttonTypes: ['submit', 'close'],//默认按钮
        succesCollBack: function (data) {//成功回调函数,data为服务器返回值
            JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
            JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeFormSubmit: function () {
            var result = true;
            var ConNumber = $(""#ConNumber"").textbox(""getValue"");
            $.ajax({
                url: '");

            
            #line 16 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                 Write(Url.Action("CheckConNumberExists", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                data: \'TypeID=");

            
            #line 17 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                         Write(Model.ConTypeID);

            
            #line default
            #line hidden
WriteLiteral("&ConNumber=\' + ConNumber + \'&Id=");

            
            #line 17 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                                         Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@"',
                async: false,
                success: function (data) {
                    if (data != ""0"") {
                        result = false;
                        JQ.dialog.info(""其它合同--["" + ConNumber + ""]--编号已存在"");
                    }
                }
            });
            return result;
        }
    });

    if (");

            
            #line 30 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
   Write(Model.CustID);

            
            #line default
            #line hidden
WriteLiteral(">0) {\r\n        $(\'#cg\').combogrid(\'setValue\',");

            
            #line 31 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                 Write(Model.CustID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n\r\n        var UrlAddres=\"\";\r\n        if ( ");

            
            #line 35 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
        Write(Model.ConTypeID);

            
            #line default
            #line hidden
WriteLiteral("==1) {\r\n            UrlAddres = \'");

            
            #line 36 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                    Write(Url.Action("GetContractSubJson", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\';\r\n        } else {\r\n            UrlAddres = \'");

            
            #line 38 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                    Write(Url.Action("GetContractJson", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=CJ\';\r\n        }\r\n\r\n        $(\'#cn\').combogrid({\r\n            panelWi" +
"dth: 650,\r\n            panelHeight: 400,\r\n            idField: \'Id\',        //ID" +
"字段\r\n            textField: \'ConName\',    //显示的字段\r\n            url: UrlAddres,\r\n " +
"           fitColumns: true,\r\n            striped: true,\r\n            editable: " +
"true,\r\n            pagination: true,           //是否分页\r\n            rownumbers: t" +
"rue,           //序号\r\n            collapsible: false,         //是否可折叠的\r\n         " +
"   fit: true,                  //自动大小\r\n            method: \'post\',\r\n            " +
"columns: [[\r\n                { field: \'ConName\', title: \'合同名称\', width: 150 },\r\n " +
"               { field: \'ConNumber\', title: \'合同编号\', width: 150 },\r\n             " +
"   { field: \'CustName\', title: \'客户名称\', width: 150 },\r\n                { field: \'" +
"ConDate\', title: \'签订日期\', width: 150, formatter: JQ.tools.DateTimeFormatterByT }," +
"\r\n            ]],\r\n            keyHandler: {\r\n                up: function () { " +
"              //【向上键】押下处理\r\n                    //取得选中行\r\n                    var " +
"selected = $(\'#cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n                " +
"    if (selected) {\r\n                        //取得选中行的rowIndex\r\n                 " +
"       var index = $(\'#cn\').combogrid(\'grid\').datagrid(\'getRowIndex\', selected);" +
"\r\n                        //向上移动到第一行为止\r\n                        if (index > 0) {" +
"\r\n                            $(\'#cn\').combogrid(\'grid\').datagrid(\'selectRow\', i" +
"ndex - 1);\r\n                        }\r\n                    } else {\r\n           " +
"             var rows = $(\'#cn\').combogrid(\'grid\').datagrid(\'getRows\');\r\n       " +
"                 $(\'#cn\').combogrid(\'grid\').datagrid(\'selectRow\', rows.length - " +
"1);\r\n                    }\r\n                },\r\n                down: function (" +
") {             //【向下键】押下处理\r\n                    //取得选中行\r\n                    va" +
"r selected = $(\'#cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n              " +
"      if (selected) {\r\n                        //取得选中行的rowIndex\r\n               " +
"         var index = $(\'#cn\').combogrid(\'grid\').datagrid(\'getRowIndex\', selected" +
");\r\n                        //向下移动到当页最后一行为止\r\n                        if (index <" +
" $(\'#cn\').combogrid(\'grid\').datagrid(\'getData\').rows.length - 1) {\r\n            " +
"                $(\'#cn\').combogrid(\'grid\').datagrid(\'selectRow\', index + 1);\r\n  " +
"                      }\r\n                    } else {\r\n                        $" +
"(\'#cn\').combogrid(\'grid\').datagrid(\'selectRow\', 0);\r\n                    }\r\n    " +
"            },\r\n                enter: function () {             //【回车键】押下处理\r\n  " +
"                  //设置【性别】文本框的内容为选中行的的性别字段内容\r\n                    var row = $(\'#" +
"cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n                    //选中后让下拉表格消" +
"失\r\n                    $(\'#cn\').combogrid(\'hidePanel\');\r\n                },\r\n   " +
"             query: function (keyword) {     //【动态搜索】处理\r\n                    //设" +
"置查询参数\r\n                    var queryParams = $(\'#cn\').combogrid(\"grid\").datagrid" +
"(\'options\').queryParams;\r\n                    queryParams.cnkeyword = keyword;\r\n" +
"                    $(\'#cn\').combogrid(\"grid\").datagrid(\'options\').queryParams =" +
" queryParams;\r\n                    //重新加载\r\n                    var pager = $(\'#c" +
"n\').combogrid(\'grid\').datagrid(\'getPager\');\r\n                    pager.paginatio" +
"n(\'refresh\');\r\n                    pager.pagination(\'select\', 1);\r\n             " +
"       $(\'#cn\').combogrid(\"grid\").datagrid(\"reload\");\r\n                    $(\'#c" +
"n\').combogrid(\"setValue\", keyword);                    //将查询条件存入隐藏域\r\n           " +
"         $(\'#cnKeyword\').val(keyword);\r\n                }\r\n            },\r\n     " +
"       onSelect: function () {\r\n                var row = $(\'#cn\').combogrid(\'gr" +
"id\').datagrid(\'getSelected\');\r\n            }\r\n        });\r\n\r\n        if (");

            
            #line 116 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
       Write(Model.ConID);

            
            #line default
            #line hidden
WriteLiteral(">0) {\r\n            $(\'#cn\').combogrid(\'setValue\',");

            
            #line 117 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                     Write(Model.ConID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n\r\n</script>\r\n");

            
            #line 121 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
 using (Html.BeginForm("save", "BussContractOther", FormMethod.Post, new { @area = "Bussiness", @id = "BussContractOtherForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 123 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 123 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"CJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                合同名称\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ConTypeID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5802), Tuple.Create("\"", 5826)
            
            #line 130 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
, Tuple.Create(Tuple.Create("", 5810), Tuple.Create<System.Object, System.Int32>(Model.ConTypeID
            
            #line default
            #line hidden
, 5810), false)
);

WriteLiteral(" />\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConrName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6016), Tuple.Create("\"", 6039)
            
            #line 133 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 6024), Tuple.Create<System.Object, System.Int32>(Model.ConrName
            
            #line default
            #line hidden
, 6024), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConNumber\"");

WriteLiteral(" id=\"ConNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6284), Tuple.Create("\"", 6308)
            
            #line 137 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                                               , Tuple.Create(Tuple.Create("", 6292), Tuple.Create<System.Object, System.Int32>(Model.ConNumber
            
            #line default
            #line hidden
, 6292), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户单位</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustID\"");

WriteLiteral(" name=\"CustID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6500), Tuple.Create("\"", 6559)
            
            #line 144 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
, Tuple.Create(Tuple.Create("", 6508), Tuple.Create<System.Object, System.Int32>(Model.CustID == 0 ? "" : Model.CustID.ToString()
            
            #line default
            #line hidden
, 6508), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6631), Tuple.Create("\"", 6654)
            
            #line 145 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
, Tuple.Create(Tuple.Create("", 6639), Tuple.Create<System.Object, System.Int32>(Model.CustName
            
            #line default
            #line hidden
, 6639), false)
);

WriteLiteral(">\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    $(function () {\r\n                        var requestUrl = " +
"\'");

            
            #line 148 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                     Write(Url.Action("GetAllCustomerListCombobox", "BussCustomer", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"';

                        $(""#CustID"").combobox({
                            url: requestUrl,
                            valueField: 'Id',
                            textField: 'CustName',
                            filter:function (q,row) {
                                var opts = $(this).combobox('options');
                                return row[opts.textField].indexOf(q) > -1;
                            },
                            onLoadSuccess: function () {
                                if ($(""#CustID"").combobox(""getText"") == """") {
                                    $(""#CustID"").combobox(""setText"", $(""#CustName"").val());
                                }
                            },onSelect:function(item){
                                console.log(item);
                                $(""#CustBankName"").textbox(""setText"",item.CustBankName);
                                $(""#CustBankAccount"").textbox(""setText"",item.CustBankAccount);
                            },onShowPanel:function(){
                                $(""#CustBankName"").textbox(""setText"","""");
                                $(""#CustBankAccount"").textbox(""setText"","""");
                            }
                        })
                    })
                </script>
            </td>
            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同状态</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 176 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConOtherStatus",
               isRequired = true,
               engName = "ConStatus",
               width = "98%",
               ids = Model.ConOtherStatus.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">单位开户行</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustBankName\"");

WriteLiteral(" name=\"CustBankName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8806), Tuple.Create("\"", 8833)
            
            #line 190 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                        , Tuple.Create(Tuple.Create("", 8814), Tuple.Create<System.Object, System.Int32>(Model.CustBankName
            
            #line default
            #line hidden
, 8814), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">银行帐号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustBankAccount\"");

WriteLiteral(" name=\"CustBankAccount\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9063), Tuple.Create("\"", 9093)
            
            #line 195 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                              , Tuple.Create(Tuple.Create("", 9071), Tuple.Create<System.Object, System.Int32>(Model.CustBankAccount
            
            #line default
            #line hidden
, 9071), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同金额(元)</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" data-options=\"precision:2\"");

WriteLiteral(" validtype=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9357), Tuple.Create("\"", 9378)
            
            #line 202 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                                , Tuple.Create(Tuple.Create("", 9365), Tuple.Create<System.Object, System.Int32>(Model.ConFee
            
            #line default
            #line hidden
, 9365), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 207 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConOtherType",
               isRequired = true,
               engName = "ContractType",
               width = "98%",
               ids = Model.ConOtherType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同签订日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConOtherSignDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validtype=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9991), Tuple.Create("\"", 10022)
            
            #line 221 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                         , Tuple.Create(Tuple.Create("", 9999), Tuple.Create<System.Object, System.Int32>(Model.ConOtherSignDate
            
            #line default
            #line hidden
, 9999), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral("></th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                备注\r\n      " +
"      </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConrNote\"");

WriteLiteral(" style=\"width:98%;height:80px\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,500]\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10380), Tuple.Create("\"", 10403)
            
            #line 231 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                                          , Tuple.Create(Tuple.Create("", 10388), Tuple.Create<System.Object, System.Int32>(Model.ConrNote
            
            #line default
            #line hidden
, 10388), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">经办人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 238 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">部门名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 242 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
           Write(Model.CreatorDepName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 249 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
           Write(Model.CreationTime.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 257 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                
            
            #line default
            #line hidden
            
            #line 257 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BussContractOther";
                    uploader.Name = "uploadfile1";
                    
            
            #line default
            #line hidden
            
            #line 262 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 262 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 267 "..\..\Views\BussContractOther\infoSub_cj.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
