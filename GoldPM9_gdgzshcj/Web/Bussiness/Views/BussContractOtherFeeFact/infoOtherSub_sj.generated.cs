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
    
    #line 2 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussContractOtherFeeFact/infoOtherSub_sj.cshtml")]
    public partial class _Views_BussContractOtherFeeFact_infoOtherSub_sj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussContractOtherFeeFact>
    {
        public _Views_BussContractOtherFeeFact_infoOtherSub_sj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">

        JQ.form.submitInit({
            formid: 'BussContractOtherFeeFactForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            },
            beforeFormSubmit: function () {
                accept();
            }
        });


    $(""#ConOtherFeeFactGrid"").datagrid({
        iconCls: 'icon-edit',
        multiSelect: false,
        rownumbers: true,
        fitColumns: true,
        //selectOnCheck: true,
        //checkOnSelect: true,jsonOtherFee
        toolbar: '#toolBar',
        url: '");

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
         Write(Url.Action("jsonOtherFeeFactAdd", "BussContractOtherFeeFact", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("?Id=");

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                                                                                        Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\' + \"&ConOtherID=");

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                                                                                                                  Write(Model.RefID);

            
            #line default
            #line hidden
WriteLiteral("\" + \"&RefTable=");

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                                                                                                                                             Write(Model.RefTable);

            
            #line default
            #line hidden
WriteLiteral("\" + \"&FactTypeID=");

            
            #line 26 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                                                                                                                                                                             Write(Model.FactTypeID);

            
            #line default
            #line hidden
WriteLiteral(@""",//请求的地址
        columns: [[
                    {

                        title: '合同名称', field: 'RefID', width: 180, align: 'center',
                        formatter: function (value, row) {
                            return row.ConrName;
                        },
                        editor: {
                            type: ""combogrid"",
                            options: {
                                panelWidth: 650,
                                idField: 'Id',
                                textField: 'ConrName',
                                url: '");

            
            #line 40 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                 Write(Url.Action("json", "BussContractOther"));

            
            #line default
            #line hidden
WriteLiteral("?CompanyType=SJ&ConTypeID=1\',  // ?ConTypeID=0\r\n                                q" +
"ueryParams:{},\r\n                                pagination: true,\r\n             " +
"                   nowrap: false,//是否换行\r\n                                striped" +
": true,\r\n                                method: \'post\',\r\n                      " +
"          singleSelect: true,\r\n                                required: true,\r\n" +
"                                columns: [[\r\n                                   " +
"         { field: \'Id\', title: \'合同编号\', width: 100, hidden: true, halign: \'center" +
"\' },\r\n                                            { field: \'ConrName\', title: \'合" +
"同名称\', width: 230, halign: \'center\' },\r\n                                         " +
"   { field: \'ConNumber\', title: \'合同编号\', width: 150, halign: \'center\' },\r\n       " +
"                                     { field: \'CustName\', title: \'客户单位\', width: " +
"150, halign: \'center\' },\r\n                                           { field: \'C" +
"onFee\', title: \'合同金额（元）\', width: 100, align: \'right\' }\r\n                        " +
"        ]],\r\n                                keyHandler: {\r\n                    " +
"                query: function (keyword) {\r\n                                   " +
"     var _params = $(this).combogrid(\"grid\").datagrid(\"options\").queryParams;\r\n " +
"                                       _params.Filter = keyword;\r\n              " +
"                          $(this).combogrid(\"grid\").datagrid(\'load\', _params);\r\n" +
"                                        $(this).combogrid(\"setValue\", keyword);\r" +
"\n                                    }\r\n                                },\r\n    " +
"                            onSelect: function (rowIndex, rowData) {\r\n\r\n        " +
"                            var ConNumber = $(\'#ConOtherFeeFactGrid\').datagrid(\'" +
"getEditor\', { index: editIndex, field: \'ConNumber\' });\r\n                        " +
"            $(ConNumber.target).textbox(\'setValue\', rowData.ConNumber);\r\n       " +
"                             var confee = $(\'#ConOtherFeeFactGrid\').datagrid(\'ge" +
"tEditor\', { index: editIndex, field: \'ConFee\' });\r\n                             " +
"       $(confee.target).textbox(\'setValue\', rowData.ConFee);\r\n                  " +
"                  var SumFeeMoney = $(\'#ConOtherFeeFactGrid\').datagrid(\'getEdito" +
"r\', { index: editIndex, field: \'SumFeeMoney\' });\r\n                              " +
"      $(SumFeeMoney.target).textbox(\'setValue\', rowData.SumFeeMoney == null ? 0 " +
": rowData.SumFeeMoney);\r\n\r\n                                    GetPercent(editIn" +
"dex, rowData);\r\n                                }\r\n                            }" +
"\r\n                        }\r\n                    },\r\n                           " +
"     {\r\n                                    title: \'合同编号\', field: \'ConNumber\', w" +
"idth: 100, align: \'center\', editor:\r\n                                      { typ" +
"e: \'textbox\', options: { disabled: true } }\r\n                                },\r" +
"\n                                { title: \'合同金额\', field: \'ConFee\', width: 100, a" +
"lign: \'right\', editor: { type: \'textbox\', options: { disabled: true } } },\r\n    " +
"                            { title: \'已付款金额\', field: \'SumFeeMoney\', width: 100, " +
"align: \'right\', editor: { type: \'textbox\', options: { disabled: true } } },//, e" +
"ditor: \'label\'\r\n                                {\r\n                             " +
"       title: \'本次付款金额\', field: \'FeeMoney\', width: 100, align: \'right\', editor: {" +
"\r\n                                        type: \'numberspinner\', options: {\r\n   " +
"                                         precision: 2, required: true, onChange:" +
" function (_new, _old) {\r\n\r\n                                                if (" +
"editIndex != undefined) {\r\n\r\n                                                   " +
" var SumFeeMoney = $(\'#ConOtherFeeFactGrid\').datagrid(\'getEditor\', { index: edit" +
"Index, field: \'SumFeeMoney\' });\r\n                                               " +
"     var SumFeeMoney = $(SumFeeMoney.target).textbox(\'getValue\');\r\n             " +
"                                       var FeeMoney = $(\'#ConOtherFeeFactGrid\')." +
"datagrid(\'getEditor\', { index: editIndex, field: \'FeeMoney\' });\r\n               " +
"                                     var FeeMoney = $(FeeMoney.target).textbox(\'" +
"getValue\');\r\n\r\n                                                    var ConFee = " +
"$(\'#ConOtherFeeFactGrid\').datagrid(\'getEditor\', { index: editIndex, field: \'ConF" +
"ee\' });\r\n                                                    var ConFee = $(ConF" +
"ee.target).textbox(\'getValue\');\r\n\r\n                                             " +
"       if (ConFee <= 0) {\r\n                                                     " +
"   var FeeMoney = $(\'#ConOtherFeeFactGrid\').datagrid(\'getEditor\', { index: editI" +
"ndex, field: \'FeeMoney\' });\r\n                                                   " +
"     $(FeeMoney.target).textbox(\'setValue\', \"0\");\r\n                             " +
"                           return;\r\n                                            " +
"        }\r\n\r\n                                                    if (FeeMoney < " +
"0) {\r\n                                                        var FeeMoney = $(\'" +
"#ConOtherFeeFactGrid\').datagrid(\'getEditor\', { index: editIndex, field: \'FeeMone" +
"y\' });\r\n                                                        $(FeeMoney.targe" +
"t).textbox(\'setValue\', \"0\");\r\n                                                  " +
"      return;\r\n                                                    }\r\n\r\n\r\n      " +
"                                              var Scale = $(\'#ConOtherFeeFactGri" +
"d\').datagrid(\'getEditor\', { index: editIndex, field: \'Scale\' });\r\n              " +
"                                      $(Scale.target).textbox(\'setValue\', ((pars" +
"eFloat(SumFeeMoney) + parseFloat(FeeMoney)) / parseFloat(ConFee) * 100).toFixed(" +
"2) + \"%\");\r\n                                                }\r\n                 " +
"                           }\r\n                                        }\r\n       " +
"                             }\r\n                                },\r\n            " +
"                    { title: \'本次付款日期\', field: \'FeerDate\', width: 100, align: \'ce" +
"nter\', formatter: JQ.tools.DateTimeFormatterByT, editor: { type: \'datebox\', opti" +
"ons: { validType: [\'dateISO\'], required: true } } },\r\n                          " +
"      { title: \'累计比例\', field: \'Scale\', width: 80, align: \'center\', editor: { typ" +
"e: \'textbox\', options: { disabled: true } } },\r\n                                " +
"//{ title: \'是否结清\', field: \'IsSettle\', width: 100, align: \'center\', JQIsExclude: " +
"true }\r\n\r\n        ]], onClickRow: onDblClickRow\r\n    });\r\n\r\n\r\n    function GetPe" +
"rcent(rowIndex, rowData) {\r\n        ////获取合同金额\r\n        var ConFee = $(\'#ConOthe" +
"rFeeFactGrid\').datagrid(\'getEditor\', { index: rowIndex, field: \'ConFee\' });\r\n\r\n " +
"       ////获取已收款\r\n        var SumFeeMoney = $(\'#ConOtherFeeFactGrid\').datagrid(\'" +
"getEditor\', { index: rowIndex, field: \'SumFeeMoney\' });\r\n        $(SumFeeMoney.t" +
"arget).textbox(\'setValue\', rowData.SumFeeMoney == null ? 0 : rowData.SumFeeMoney" +
");\r\n        ////获取本次收款金额\r\n        var FeeMoney = $(\'#ConOtherFeeFactGrid\').datag" +
"rid(\'getEditor\', { index: rowIndex, field: \'FeeMoney\' });\r\n        $(FeeMoney.ta" +
"rget).textbox(\'setValue\', rowData.FeeMoney == null ? 0 : rowData.FeeMoney);\r\n\r\n " +
"       var Scale = $(\'#ConOtherFeeFactGrid\').datagrid(\'getEditor\', { index: rowI" +
"ndex, field: \'Scale\' });\r\n        if ($(ConFee.target).val() == 0) {\r\n          " +
"  $(Scale.target).textbox(\'setValue\', \"0%\");\r\n        }\r\n        else {\r\n       " +
"     $(Scale.target).textbox(\'setValue\', ((parseFloat($(SumFeeMoney.target).val(" +
")) + parseFloat($(FeeMoney.target).val())) / parseFloat($(ConFee.target).val()) " +
"* 100).toFixed(2) + \"%\");\r\n            var SumFeeMoney = $(\'#ConOtherFeeFactGrid" +
"\').datagrid(\'getEditor\', { index: rowIndex, field: \'SumFeeMoney\' });\r\n          " +
"  if (parseFloat($(ConFee.target).val()) - parseFloat($(SumFeeMoney.target).val(" +
")) > 0) {\r\n                //$(FeeMoney.target).numberbox({ prompt: ($(ConFee.ta" +
"rget).val() - $(SumFeeMoney.target).val()) });\r\n            }\r\n        }\r\n    }\r" +
"\n\r\n    var editIndex = undefined;\r\n    function endEditing() {\r\n        if (edit" +
"Index == undefined) { return true }\r\n        if ($(\'#ConOtherFeeFactGrid\').datag" +
"rid(\'validateRow\', editIndex)) {\r\n            var edCnName = $(\'#ConOtherFeeFact" +
"Grid\').datagrid(\'getEditor\', { index: editIndex, field: \'RefID\' });\r\n           " +
" var CnName = $(edCnName.target).combogrid(\'getText\');\r\n            var RefID = " +
"$(edCnName.target).combogrid(\'getValue\');\r\n            $(\'#ConOtherFeeFactGrid\')" +
".datagrid(\'getRows\')[editIndex][\'RefID\'] = RefID;\r\n            $(\'#ConOtherFeeFa" +
"ctGrid\').datagrid(\'getRows\')[editIndex][\'ConrName\'] = CnName;\r\n            var r" +
"ow = $(edCnName.target).combogrid(\'grid\').datagrid(\'getSelected\');\r\n\r\n          " +
"  $(\'#ConOtherFeeFactGrid\').datagrid(\'getRows\')[editIndex][\'ConNumber\'] = row.Co" +
"nNumber;\r\n            $(\'#ConOtherFeeFactGrid\').datagrid(\'getRows\')[editIndex][\'" +
"ConFee\'] = row.ConFee;\r\n            $(\'#ConOtherFeeFactGrid\').datagrid(\'getRows\'" +
")[editIndex][\'SumFeeMoney\'] = row.SumFeeMoney;\r\n\r\n            // $(\'#ConOtherFee" +
"FactGrid\').datagrid(\'getRows\')[editIndex][\'text\'] = row.text;\r\n\r\n            $(\'" +
"#ConOtherFeeFactGrid\').datagrid(\'endEdit\', editIndex);\r\n            editIndex = " +
"undefined;\r\n            getRows();\r\n            return true;\r\n        } else {\r\n" +
"            return false;\r\n        }\r\n    }\r\n\r\n    function onDblClickRow(index)" +
" {\r\n        if (editIndex != index) {\r\n            if (endEditing()) {\r\n        " +
"        $(\'#ConOtherFeeFactGrid\').datagrid(\'selectRow\', index)\r\n                " +
"        .datagrid(\'beginEdit\', index);\r\n                editIndex = index;\r\n\r\n  " +
"              getRows();\r\n            } else {\r\n                $(\'#ConOtherFeeF" +
"actGrid\').datagrid(\'selectRow\', editIndex);\r\n                //getEditors();\r\n  " +
"          }\r\n        }\r\n    }\r\n    function append() {\r\n        if (endEditing()" +
") {\r\n            $(\'#ConOtherFeeFactGrid\').datagrid(\'appendRow\', { Id: 0, FeerDa" +
"te: \'");

            
            #line 192 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                                                           Write(DateTime.Now);

            
            #line default
            #line hidden
WriteLiteral(@"' });
            editIndex = $('#ConOtherFeeFactGrid').datagrid('getRows').length - 1;
            $('#ConOtherFeeFactGrid').datagrid('selectRow', editIndex)
                    .datagrid('beginEdit', editIndex);
            getRows();
        }
        else {
            JQ.dialog.warning('请将当前信息填写完整，再添加记录');
        }
    }
    function removexit() {
        if (editIndex == undefined) { return }
        $('#ConOtherFeeFactGrid').datagrid('cancelEdit', editIndex)
                .datagrid('deleteRow', editIndex);
        editIndex = undefined;
        if ($(""#ConOtherFeeFactGrid"").datagrid(""getRows"").length > 0) {
            getRows();
        }
    }
    function accept() {
        if (endEditing()) {
            $('#ConOtherFeeFactGrid').datagrid('acceptChanges');
            getRows();
            return true;
        }
        else {
            JQ.dialog.warning('请将信息填写完整');
            return false;
        }
    }
    function getRows() {
        var rows = $('#ConOtherFeeFactGrid').datagrid('getRows');
        $(""#JsonRows"").val(JSON.stringify(rows));
    }

    var beforeFormSubmit = function () {
        accept();
    }



</script>
");

            
            #line 234 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
 using (Html.BeginForm("saveOtherFeeFact", "BussContractOtherFeeFact", FormMethod.Post, new { @area = "Bussiness", @id = "BussContractOtherFeeFactForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 236 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 236 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                              
    
            
            #line default
            #line hidden
            
            #line 237 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
Write(Html.HiddenFor(m => m.FactTypeID));

            
            #line default
            #line hidden
            
            #line 237 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                      
    
            
            #line default
            #line hidden
            
            #line 238 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
Write(Html.HiddenFor(m => m.RefTable));

            
            #line default
            #line hidden
            
            #line 238 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
                                    


            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"SJ\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"JsonRows\"");

WriteLiteral(" id=\"JsonRows\"");

WriteLiteral(" />\r\n");

            
            #line 242 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">付款</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"ConOtherFeeFactGrid\"");

WriteLiteral(" class=\"easyui-datagrid\"");

WriteLiteral(" style=\"width:98%;height:auto\"");

WriteLiteral("></table>\r\n\r\n                <div");

WriteLiteral(" id=\"toolBar\"");

WriteLiteral(" style=\"height:auto\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"easyui-linkbutton l-btn l-btn-small l-btn-plain\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-plus\',plain:true\"");

WriteLiteral(" onclick=\"append()\"");

WriteLiteral(">新增</a>\r\n                    <a");

WriteLiteral(" href=\"javascript:void(0)\"");

WriteLiteral(" class=\"easyui-linkbutton l-btn l-btn-small l-btn-plain\"");

WriteLiteral(" data-options=\"iconCls:\'fa fa-trash\',plain:true\"");

WriteLiteral(" onclick=\"removexit()\"");

WriteLiteral(">删除</a>\r\n                </div>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n" +
"            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 258 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
           Write(Model.CreatorEmpName);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">登记时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 262 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
           Write(Model.CreationTime.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n        ");

WriteLiteral("\r\n        ");

WriteLiteral("\r\n    </table>\r\n");

            
            #line 288 "..\..\Views\BussContractOtherFeeFact\infoOtherSub_sj.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591