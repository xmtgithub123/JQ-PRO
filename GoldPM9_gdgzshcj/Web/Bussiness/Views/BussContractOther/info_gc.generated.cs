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
    
    #line 2 "..\..\Views\BussContractOther\info_gc.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractOther/info_gc.cshtml")]
    public partial class _Views_BussContractOther_info_gc_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussContractOther>
    {
        public _Views_BussContractOther_info_gc_cshtml()
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
            JQ.dialog.dialoCJlose();//关闭窗体放在最后一步执行，以避免找不到事件源
        },
        beforeFormSubmit: function () {
            var result = true;
            var ConNumber = $(""#ConNumber"").textbox(""getValue"");
            $.ajax({
                url: '");

            
            #line 15 "..\..\Views\BussContractOther\info_gc.cshtml"
                 Write(Url.Action("CheckConNumberExists", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\',\r\n                data: \'TypeID=");

            
            #line 16 "..\..\Views\BussContractOther\info_gc.cshtml"
                         Write(Model.ConTypeID);

            
            #line default
            #line hidden
WriteLiteral("&ConNumber=\' + ConNumber + \'&Id=");

            
            #line 16 "..\..\Views\BussContractOther\info_gc.cshtml"
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

            
            #line 29 "..\..\Views\BussContractOther\info_gc.cshtml"
   Write(Model.CustID);

            
            #line default
            #line hidden
WriteLiteral(">0) {\r\n        $(\'#cg\').combogrid(\'setValue\',");

            
            #line 30 "..\..\Views\BussContractOther\info_gc.cshtml"
                                 Write(Model.CustID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n\r\n    var UrlAddres=\"\";\r\n    if ( ");

            
            #line 34 "..\..\Views\BussContractOther\info_gc.cshtml"
    Write(Model.ConTypeID);

            
            #line default
            #line hidden
WriteLiteral("==1) {\r\n        UrlAddres= \'");

            
            #line 35 "..\..\Views\BussContractOther\info_gc.cshtml"
               Write(Url.Action("GetContractSubJson", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\';\r\n    }else {\r\n        UrlAddres=  \'");

            
            #line 37 "..\..\Views\BussContractOther\info_gc.cshtml"
                Write(Url.Action("GetContractJson", "BussContractOther", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=GC\';\r\n    }\r\n\r\n    $(\'#cn\').combogrid({\r\n        panelWidth: 650,\r\n " +
"       panelHeight: 400,\r\n        idField: \'Id\',        //ID字段\r\n        textFiel" +
"d: \'ConName\',    //显示的字段\r\n        url: UrlAddres,\r\n        fitColumns: true,\r\n  " +
"      striped: true,\r\n        editable: true,\r\n        pagination: true,        " +
"   //是否分页\r\n        rownumbers: true,           //序号\r\n        collapsible: false," +
"         //是否可折叠的\r\n        fit: true,                  //自动大小\r\n        method: \'" +
"post\',\r\n        columns: [[\r\n            { field: \'ConName\', title: \'合同名称\', widt" +
"h: 150 },\r\n            { field: \'ConNumber\', title: \'合同编号\', width: 150 },\r\n     " +
"       { field: \'CustName\', title: \'客户名称\', width: 150 },\r\n            { field: \'" +
"ConDate\', title: \'签订日期\', width: 150, formatter: JQ.tools.DateTimeFormatterByT }," +
"\r\n        ]],\r\n        keyHandler: {\r\n            up: function () {             " +
"  //【向上键】押下处理\r\n                //取得选中行\r\n                var selected = $(\'#cn\')." +
"combogrid(\'grid\').datagrid(\'getSelected\');\r\n                if (selected) {\r\n   " +
"                 //取得选中行的rowIndex\r\n                    var index = $(\'#cn\').comb" +
"ogrid(\'grid\').datagrid(\'getRowIndex\', selected);\r\n                    //向上移动到第一行" +
"为止\r\n                    if (index > 0) {\r\n                        $(\'#cn\').combo" +
"grid(\'grid\').datagrid(\'selectRow\', index - 1);\r\n                    }\r\n         " +
"       } else {\r\n                    var rows = $(\'#cn\').combogrid(\'grid\').datag" +
"rid(\'getRows\');\r\n                    $(\'#cn\').combogrid(\'grid\').datagrid(\'select" +
"Row\', rows.length - 1);\r\n                }\r\n            },\r\n            down: fu" +
"nction () {             //【向下键】押下处理\r\n                //取得选中行\r\n                va" +
"r selected = $(\'#cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n              " +
"  if (selected) {\r\n                    //取得选中行的rowIndex\r\n                    var" +
" index = $(\'#cn\').combogrid(\'grid\').datagrid(\'getRowIndex\', selected);\r\n        " +
"            //向下移动到当页最后一行为止\r\n                    if (index < $(\'#cn\').combogrid(" +
"\'grid\').datagrid(\'getData\').rows.length - 1) {\r\n                        $(\'#cn\')" +
".combogrid(\'grid\').datagrid(\'selectRow\', index + 1);\r\n                    }\r\n   " +
"             } else {\r\n                    $(\'#cn\').combogrid(\'grid\').datagrid(\'" +
"selectRow\', 0);\r\n                }\r\n            },\r\n            enter: function " +
"() {             //【回车键】押下处理\r\n                //设置【性别】文本框的内容为选中行的的性别字段内容\r\n      " +
"          var row = $(\'#cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n       " +
"         //选中后让下拉表格消失\r\n                $(\'#cn\').combogrid(\'hidePanel\');\r\n       " +
"     },\r\n            query: function (keyword) {     //【动态搜索】处理\r\n               " +
" //设置查询参数\r\n                var queryParams = $(\'#cn\').combogrid(\"grid\").datagrid" +
"(\'options\').queryParams;\r\n                queryParams.cnkeyword = keyword;\r\n    " +
"            $(\'#cn\').combogrid(\"grid\").datagrid(\'options\').queryParams = queryPa" +
"rams;\r\n                //重新加载\r\n                var pager = $(\'#cn\').combogrid(\'g" +
"rid\').datagrid(\'getPager\');\r\n                pager.pagination(\'refresh\');\r\n     " +
"           pager.pagination(\'select\', 1);\r\n                $(\'#cn\').combogrid(\"g" +
"rid\").datagrid(\"reload\");\r\n                $(\'#cn\').combogrid(\"setValue\", keywor" +
"d);                    //将查询条件存入隐藏域\r\n                $(\'#cnKeyword\').val(keyword" +
");\r\n            }\r\n        },\r\n        onSelect: function () {\r\n            var " +
"row = $(\'#cn\').combogrid(\'grid\').datagrid(\'getSelected\');\r\n        }\r\n    });\r\n\r" +
"\n    if (");

            
            #line 115 "..\..\Views\BussContractOther\info_gc.cshtml"
   Write(Model.ConID);

            
            #line default
            #line hidden
WriteLiteral(">0) {\r\n        $(\'#cn\').combogrid(\'setValue\',");

            
            #line 116 "..\..\Views\BussContractOther\info_gc.cshtml"
                                 Write(Model.ConID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n    }\r\n\r\n</script>\r\n");

            
            #line 120 "..\..\Views\BussContractOther\info_gc.cshtml"
 using (Html.BeginForm("save", "BussContractOther", FormMethod.Post, new { @area = "Bussiness", @id = "BussContractOtherForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 122 "..\..\Views\BussContractOther\info_gc.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 122 "..\..\Views\BussContractOther\info_gc.cshtml"
                              

            
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

WriteLiteral(">\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">\r\n                合同名称\r\n                <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"ConTypeID\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5481), Tuple.Create("\"", 5505)
            
            #line 129 "..\..\Views\BussContractOther\info_gc.cshtml"
, Tuple.Create(Tuple.Create("", 5489), Tuple.Create<System.Object, System.Int32>(Model.ConTypeID
            
            #line default
            #line hidden
, 5489), false)
);

WriteLiteral(" />\r\n            </th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConrName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteLiteral(" data-options=\"required:true\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5695), Tuple.Create("\"", 5718)
            
            #line 132 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                               , Tuple.Create(Tuple.Create("", 5703), Tuple.Create<System.Object, System.Int32>(Model.ConrName
            
            #line default
            #line hidden
, 5703), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 5963), Tuple.Create("\"", 5987)
            
            #line 136 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                                               , Tuple.Create(Tuple.Create("", 5971), Tuple.Create<System.Object, System.Int32>(Model.ConNumber
            
            #line default
            #line hidden
, 5971), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户单位</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustID\"");

WriteLiteral(" name=\"CustID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6179), Tuple.Create("\"", 6238)
            
            #line 143 "..\..\Views\BussContractOther\info_gc.cshtml"
, Tuple.Create(Tuple.Create("", 6187), Tuple.Create<System.Object, System.Int32>(Model.CustID == 0 ? "" : Model.CustID.ToString()
            
            #line default
            #line hidden
, 6187), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6310), Tuple.Create("\"", 6333)
            
            #line 144 "..\..\Views\BussContractOther\info_gc.cshtml"
, Tuple.Create(Tuple.Create("", 6318), Tuple.Create<System.Object, System.Int32>(Model.CustName
            
            #line default
            #line hidden
, 6318), false)
);

WriteLiteral(">\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    $(function () {\r\n\r\n                        var requestUrl " +
"= \'");

            
            #line 148 "..\..\Views\BussContractOther\info_gc.cshtml"
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


                                $(""#CustBankName"").textbox(""setValue"",item.CustBankName);
                                $(""#CustBankAccount"").textbox(""setValue"",item.CustBankAccount);
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

            
            #line 175 "..\..\Views\BussContractOther\info_gc.cshtml"
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

WriteAttribute("value", Tuple.Create(" value=\"", 8235), Tuple.Create("\"", 8262)
            
            #line 189 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                        , Tuple.Create(Tuple.Create("", 8243), Tuple.Create<System.Object, System.Int32>(Model.CustBankName
            
            #line default
            #line hidden
, 8243), false)
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

WriteAttribute("value", Tuple.Create(" value=\"", 8492), Tuple.Create("\"", 8522)
            
            #line 194 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                              , Tuple.Create(Tuple.Create("", 8500), Tuple.Create<System.Object, System.Int32>(Model.CustBankAccount
            
            #line default
            #line hidden
, 8500), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同金额(元)</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" data-options=\"precision: 2\"");

WriteLiteral(" validtype=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 8787), Tuple.Create("\"", 8808)
            
            #line 201 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                                 , Tuple.Create(Tuple.Create("", 8795), Tuple.Create<System.Object, System.Int32>(Model.ConFee
            
            #line default
            #line hidden
, 8795), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">结算金额(元)</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ConrBalanceFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" data-options=\"precision: 2\"");

WriteLiteral(" validtype=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 9050), Tuple.Create("\"", 9079)
            
            #line 205 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                                         , Tuple.Create(Tuple.Create("", 9058), Tuple.Create<System.Object, System.Int32>(Model.ConrBalanceFee
            
            #line default
            #line hidden
, 9058), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同履行方式</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 211 "..\..\Views\BussContractOther\info_gc.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "ConOtherFulfilType",
               isRequired = true,
               engName = "ConDealWays",
               width = "98%",
               ids = Model.ConOtherFulfilType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">合同类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 222 "..\..\Views\BussContractOther\info_gc.cshtml"
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

WriteAttribute("value", Tuple.Create(" value=\"", 10103), Tuple.Create("\"", 10134)
            
            #line 236 "..\..\Views\BussContractOther\info_gc.cshtml"
                                        , Tuple.Create(Tuple.Create("", 10111), Tuple.Create<System.Object, System.Int32>(Model.ConOtherSignDate
            
            #line default
            #line hidden
, 10111), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

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

WriteAttribute("value", Tuple.Create(" value=\"", 10494), Tuple.Create("\"", 10517)
            
            #line 247 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                                          , Tuple.Create(Tuple.Create("", 10502), Tuple.Create<System.Object, System.Int32>(Model.ConrNote
            
            #line default
            #line hidden
, 10502), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">经办人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 254 "..\..\Views\BussContractOther\info_gc.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">部门名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 258 "..\..\Views\BussContractOther\info_gc.cshtml"
           Write(Model.CreatorDepName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记日期</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 265 "..\..\Views\BussContractOther\info_gc.cshtml"
           Write(Model.CreationTime.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n             " +
"   上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 273 "..\..\Views\BussContractOther\info_gc.cshtml"
                
            
            #line default
            #line hidden
            
            #line 273 "..\..\Views\BussContractOther\info_gc.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BussContractOther";
                    uploader.Name = "uploadfile1";
                    
            
            #line default
            #line hidden
            
            #line 278 "..\..\Views\BussContractOther\info_gc.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 278 "..\..\Views\BussContractOther\info_gc.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 283 "..\..\Views\BussContractOther\info_gc.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591