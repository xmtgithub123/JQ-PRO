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
    
    #line 2 "..\..\Views\ProjSub\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ProjSub/info.cshtml")]
    public partial class _Views_ProjSub_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.ProjSub>
    {
        public _Views_ProjSub_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    var  btnPer=");

            
            #line 5 "..\..\Views\ProjSub\info.cshtml"
           Write(Html.Raw(ViewBag.permission));

            
            #line default
            #line hidden
WriteLiteral(@";
    $(function () {
        JQ.form.submitInit({
            formid: 'ProjSubForm',//formid提交需要用到
            docName: 'projsub',
            ExportName: '外委项目申请表',
            buttonTypes: [ 'exportForm','close'],//默认按钮
            beforeFormSubmit: function (a,b) {
                var _Id = $(""#Id"").val();
                var _SubNumber = $(""#SubNumber"").val();
                var bit=0;
                $.ajax({
                    doBackResult: false,
                    url: '");

            
            #line 18 "..\..\Views\ProjSub\info.cshtml"
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
                if($(""#stepOrder"").val()==""2""&&b!=""loadback""&&b!=""back"")
                {
                    var  _c= $(""#CustomerID"").combobox('getValue');
                    if(_c=="""")
                    {
                        JQ.dialog.warning(""请先确认外委单位!"");
                        return false;
                    }
                }
            },
            flow: {
                flowNodeID: """);

            
            #line 40 "..\..\Views\ProjSub\info.cshtml"
                         Write(Request.QueryString["flowNodeID"]==null? 0 : int.Parse(Request.QueryString["flowNodeID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                flowMultiSignID: \"");

            
            #line 41 "..\..\Views\ProjSub\info.cshtml"
                              Write(Request.QueryString["flowMultiSignID"] ==null? 0 : int.Parse(Request.QueryString["flowMultiSignID"]));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                url: \"");

            
            #line 42 "..\..\Views\ProjSub\info.cshtml"
                  Write(Url.Action("FlowWidget", "PubFlow",new { @area="Core"} ));

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                instance: \"_flowInstance\",\r\n                processor: \"Proje" +
"ct,Project.FlowProcessor.ProjSubProcessor\",\r\n                refID: \"");

            
            #line 45 "..\..\Views\ProjSub\info.cshtml"
                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral("\",\r\n                refTable: \"ProjSub\",\r\n                dataCreator:\"");

            
            #line 47 "..\..\Views\ProjSub\info.cshtml"
                         Write(ViewBag.CreatorEmpId );

            
            #line default
            #line hidden
WriteLiteral(@""",
                onLoaded:function(){
                    $(""#stepOrder"").val(this.stepOrder);
                }
            },
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                var tab = $('#mainTab').tabs('getSelected');
                var index = $('#mainTab').tabs('getTabIndex', tab);
                $(""#mainTab"").find(""iframe"")[index].contentWindow.refreshGrid();
                _flowInstance.$form.parent().dialog(""close"");
            },
            onBefore: function (resultArr) {
                var bit=""0"";
                if($.inArray(""allDown"", btnPer)!=-1){
                    bit=""1"";
                }
                resultArr.push({ Key: ""Permission"", Value: bit});
            }
        });
        //外委负责人
        JQ.combobox.selEmp({ id: 'SubEmpId', width:'98%' });
        //选人员
        $(""#projId"").val(");

            
            #line 69 "..\..\Views\ProjSub\info.cshtml"
                    Write(Model.ProjID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n\r\n        if(");

            
            #line 71 "..\..\Views\ProjSub\info.cshtml"
      Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n            $(\"#ProjInfo\").val(\'");

            
            #line 73 "..\..\Views\ProjSub\info.cshtml"
                           Write(ViewBag.ProjInfo);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n\r\n        }\r\n    });\r\n\r\n    function SelectProjectInfo() {\r\n        JQ.dialo" +
"g.dialog({\r\n            title: \"选择项目信息\",\r\n            url: \'");

            
            #line 81 "..\..\Views\ProjSub\info.cshtml"
             Write(Url.Action("ListInfo", "Project", new { @area = "Project" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=FGS',
            width: '1024',
            height: '100%',
            JQID: 'ShowProjectTreeGridList',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }
    var selectValue="""";
    var selectText="""";

    JQ.combobox.common({
        id: 'CustomerID',
        toolbar: '#tbCustomer',//工具栏的容器ID
        url: '");

            
            #line 95 "..\..\Views\ProjSub\info.cshtml"
         Write(Url.Action("GetCustomerList", "BussCustomer", new { @area= "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n        panelWidth: 650,\r\n        panelHeight: 320,\r\n        valueField: \'Id\'" +
",\r\n        textField: \'CustName\',\r\n        editable: false,\r\n        multiple: f" +
"alse,\r\n        pagination: true,//是否分页\r\n        queryParams:{TypeID:1,CustID:");

            
            #line 103 "..\..\Views\ProjSub\info.cshtml"
                                Write(Model.ColAttType2);

            
            #line default
            #line hidden
WriteLiteral(@"},
        columns: [[
                    { field: 'Id', hidden: true },
                    { field: 'CustName', title: '单位名称', width: 150 },
                    { field: 'CustAddress', title: '单位地址', width: 400 }
        ]],
        onSelect: function (val, row) {
            selectValue=row.Id;
            selectText=row.CustName;
        },

        onLoadSuccess: function () {
            var selectRow=$(this).combogrid('grid').datagrid('getSelected');
            if(selectRow!=undefined)
            {
                selectText=$(this).combogrid('getText');
            }
            if(selectRow==undefined)
            {
                $(this).combogrid('setText',selectText);
            }
        }
    });
    $(""#fullNameSearchCustomer"").textbox({
        buttonText: '筛选',
        buttonIcon: 'fa fa-search',
        height: 25,
        prompt: '客户名称',
        onClickButton: function () {
            JQ.datagrid.searchGrid(
                {
                    $dg:$(""#CustomerID"").combogrid('grid'),
                    loadingType: 'datagrid',
                    queryParams:
                             {
                                 TypeID:0,
                                 CustID:'");

            
            #line 139 "..\..\Views\ProjSub\info.cshtml"
                                    Write(Model.ColAttType2);

            
            #line default
            #line hidden
WriteLiteral(@"',
                                 keyWord:$(""#fullNameSearchCustomer"").val()
                             }
                });
        }

    });
    var _ProjSubBack = function (param) {
        var json = JSON.parse(param);
        $(""#projId"").val(json.Id);
        $(""#ProjInfo"").textbox('setText', ""项目编号：""+json.ProjNumber+"",项目名称:""+json.ProjName);
    }
</script>
");

            
            #line 152 "..\..\Views\ProjSub\info.cshtml"
 using (Html.BeginForm("save", "ProjSub", FormMethod.Post, new { @area = "Project", @id = "ProjSubForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 154 "..\..\Views\ProjSub\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 154 "..\..\Views\ProjSub\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"CompanyType\"");

WriteLiteral(" name=\"CompanyType\"");

WriteLiteral(" value=\"FGS\"");

WriteLiteral(" />\r\n");

WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <td");

WriteLiteral(" colspan=\"4\"");

WriteLiteral(" style=\"padding: 15px; font-size: 20px; text-align: center; font-weight: bold; bo" +
"rder: none;\"");

WriteLiteral(">外委项目申请表</td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral("></th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"TableNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6239), Tuple.Create("\"", 6265)
            
            #line 162 "..\..\Views\ProjSub\info.cshtml"
                                                                              , Tuple.Create(Tuple.Create("", 6247), Tuple.Create<System.Object, System.Int32>(Model.TableNumber
            
            #line default
            #line hidden
, 6247), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委项目编号</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" id=\"SubNumber\"");

WriteLiteral(" name=\"SubNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validtype=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6479), Tuple.Create("\"", 6503)
            
            #line 164 "..\..\Views\ProjSub\info.cshtml"
                                                                                          , Tuple.Create(Tuple.Create("", 6487), Tuple.Create<System.Object, System.Int32>(Model.SubNumber
            
            #line default
            #line hidden
, 6487), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">申请部门</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 169 "..\..\Views\ProjSub\info.cshtml"
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

WriteAttribute("value", Tuple.Create(" value=\"", 7057), Tuple.Create("\"", 7086)
            
            #line 177 "..\..\Views\ProjSub\info.cshtml"
                                                                                  , Tuple.Create(Tuple.Create("", 7065), Tuple.Create<System.Object, System.Int32>(Model.CreatorEmpName
            
            #line default
            #line hidden
, 7065), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">登记时间</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"CreationTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7266), Tuple.Create("\"", 7293)
            
            #line 180 "..\..\Views\ProjSub\info.cshtml"
                                            , Tuple.Create(Tuple.Create("", 7274), Tuple.Create<System.Object, System.Int32>(Model.CreationTime
            
            #line default
            #line hidden
, 7274), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" rowspan=\"2\"");

WriteLiteral(" width=\"15%\"");

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

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委项目名称</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"SubName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validtype=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 7973), Tuple.Create("\"", 7995)
            
            #line 191 "..\..\Views\ProjSub\info.cshtml"
                                             , Tuple.Create(Tuple.Create("", 7981), Tuple.Create<System.Object, System.Int32>(Model.SubName
            
            #line default
            #line hidden
, 7981), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委项目阶段</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 194 "..\..\Views\ProjSub\info.cshtml"
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

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委专业</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 208 "..\..\Views\ProjSub\info.cshtml"
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

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委负责人</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"SubEmpId\"");

WriteLiteral(" name=\"SubEmpId\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">提供产品类别</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 226 "..\..\Views\ProjSub\info.cshtml"
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

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委性质</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 237 "..\..\Views\ProjSub\info.cshtml"
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
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">合作方式</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 251 "..\..\Views\ProjSub\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "SubHZSJ",
               isRequired = true,
               engName = "HZSJ",
               width = "98%",
               ids = Model.SubHZSJ.ToString(),
               isMult = true
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">评审结果</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                记录合格放名录\r\n");

WriteLiteral("                ");

            
            #line 265 "..\..\Views\ProjSub\info.cshtml"
           Write(Html.CheckBoxList("SubQualifiedDirectory"));

            
            #line default
            #line hidden
WriteLiteral(";\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委时间</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate1\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10541), Tuple.Create("\"", 10567)
            
            #line 271 "..\..\Views\ProjSub\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 10549), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate1
            
            #line default
            #line hidden
, 10549), false)
);

WriteLiteral(" />\r\n\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">综合评审时间</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"ColAttDate2\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 10768), Tuple.Create("\"", 10794)
            
            #line 276 "..\..\Views\ProjSub\info.cshtml"
                              , Tuple.Create(Tuple.Create("", 10776), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate2
            
            #line default
            #line hidden
, 10776), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">审核时间</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"ColAttDate3\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11002), Tuple.Create("\"", 11028)
            
            #line 281 "..\..\Views\ProjSub\info.cshtml"
                                          , Tuple.Create(Tuple.Create("", 11010), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate3
            
            #line default
            #line hidden
, 11010), false)
);

WriteLiteral(" /></td>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">成品提交时间</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"ColAttDate4\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11195), Tuple.Create("\"", 11221)
            
            #line 283 "..\..\Views\ProjSub\info.cshtml"
                                          , Tuple.Create(Tuple.Create("", 11203), Tuple.Create<System.Object, System.Int32>(Model.ColAttDate4
            
            #line default
            #line hidden
, 11203), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">预估金额</th>\r\n            <td");

WriteLiteral(" width=\"35%\"");

WriteLiteral("><input");

WriteLiteral(" name=\"YGMoney\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberbox\"");

WriteLiteral(" data-options=\"precision:2,min:0\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11426), Tuple.Create("\"", 11448)
            
            #line 287 "..\..\Views\ProjSub\info.cshtml"
                                                     , Tuple.Create(Tuple.Create("", 11434), Tuple.Create<System.Object, System.Int32>(Model.YGMoney
            
            #line default
            #line hidden
, 11434), false)
);

WriteLiteral(" /></td>\r\n        </tr>\r\n        <tr>\r\n            <th>确认外委单位</th>\r\n            <" +
"td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustomerID\"");

WriteLiteral(" name=\"CustomerID\"");

WriteLiteral(" style=\"width:98%;\"");

WriteAttribute("value", Tuple.Create(" value=\"", 11622), Tuple.Create("\"", 11685)
            
            #line 292 "..\..\Views\ProjSub\info.cshtml"
   , Tuple.Create(Tuple.Create("", 11630), Tuple.Create<System.Object, System.Int32>(Model.ColAttType2==0?"":Model.ColAttType2.ToString()
            
            #line default
            #line hidden
, 11630), false)
);

WriteLiteral(" />\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(">\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    $(function () {\r\n                        $(\"#CustomerID\")." +
"combobox({\r\n                            url: \'");

            
            #line 297 "..\..\Views\ProjSub\info.cshtml"
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
                            onLoadSuccess:function () {
                                if ($(""#CustomerID"").combobox(""getText"")=="""") {
                                    $(""#CustomerID"").combobox(""setText"",$(""#CustName"").val());
                                }
                            }
                        })

                    })
                </script>
            </td>
        </tr>
        <tr>
            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委原因</th>\r\n            <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubReason\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 318 "..\..\Views\ProjSub\info.cshtml"
                                                                                                                                                    Write(Model.SubReason);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">外委内容</th>\r\n            <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubContent\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 324 "..\..\Views\ProjSub\info.cshtml"
                                                                                                                                                     Write(Model.SubContent);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">验收要求</th>\r\n            <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"SubAcceptance\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 330 "..\..\Views\ProjSub\info.cshtml"
                                                                                                                                                        Write(Model.SubAcceptance);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"15%\"");

WriteLiteral(">备注</th>\r\n            <td");

WriteLiteral(" width=\"85%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <textarea");

WriteLiteral(" name=\"ColAttVal1\"");

WriteLiteral(" style=\"width:99%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteLiteral(">");

            
            #line 336 "..\..\Views\ProjSub\info.cshtml"
                                                                                                                                                     Write(Model.ColAttVal1);

            
            #line default
            #line hidden
WriteLiteral("</textarea>\r\n            </td>\r\n        </tr>\r\n\r\n        <tr");

WriteLiteral(" id=\"SignTd\"");

WriteLiteral(">\r\n            <th></th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral("></td>\r\n        </tr>\r\n        <tr>\r\n            <th>\r\n                上传附件\r\n    " +
"        </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 349 "..\..\Views\ProjSub\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 349 "..\..\Views\ProjSub\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "ProjSub";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 354 "..\..\Views\ProjSub\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 354 "..\..\Views\ProjSub\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 359 "..\..\Views\ProjSub\info.cshtml"


            
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

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"stepOrder\"");

WriteLiteral(" name=\"stepOrder\"");

WriteLiteral(" />\r\n");

            
            #line 365 "..\..\Views\ProjSub\info.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
