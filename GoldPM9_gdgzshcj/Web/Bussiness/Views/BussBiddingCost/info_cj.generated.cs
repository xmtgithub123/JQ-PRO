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
    
    #line 2 "..\..\Views\BussBiddingCost\info_cj.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussBiddingCost/info_cj.cshtml")]
    public partial class _Views_BussBiddingCost_info_cj_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussBiddingCost>
    {
        public _Views_BussBiddingCost_info_cj_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
    $(function () {
        JQ.form.submitInit({
            formid: 'BussBiddingCostForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                var _tempFrame=window.top.document.getElementById(""");

            
            #line 9 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                                               Write(Request.QueryString["iframeID"]);

            
            #line default
            #line hidden
WriteLiteral(@""");
                if(_tempFrame){
                    _tempFrame.contentWindow.refreshGrid();
                }
                //JQ.datagrid.autoRefdatagrid;//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            }
        });

        $(""#BiddingBatch"").attr('readonly', true);
        $(""#BiddingNumber"").attr('readonly', true);
        $(""#PackageNumber"").attr('readonly', true);
        $(""#ProgressName"").attr('readonly', true);
        $(""#WinTime"").attr('readonly', true);

        if(");

            
            #line 24 "..\..\Views\BussBiddingCost\info_cj.cshtml"
      Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(">0)\r\n        {\r\n            $(\"#BiddingBatch\").val(\'");

            
            #line 26 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                               Write(ViewBag.BiddingBatch);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n            $(\"#BiddingNumber\").val(\'");

            
            #line 27 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                Write(ViewBag.BiddingNumber);

            
            #line default
            #line hidden
WriteLiteral("\');\r\n\r\n            $(\"#BiddingId\").val(");

            
            #line 29 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                           Write(Model.BussBiddingInfoID);

            
            #line default
            #line hidden
WriteLiteral(");\r\n        }\r\n    });\r\n\r\n    function SelectBiddingInfoPackage() {\r\n        JQ.d" +
"ialog.dialog({\r\n            title: \"选择投标项目\",\r\n            url: \'");

            
            #line 36 "..\..\Views\BussBiddingCost\info_cj.cshtml"
             Write(Url.Action("ListInfo", "BussBiddingInfo", new { @area = "Bussiness" }));

            
            #line default
            #line hidden
WriteLiteral(@"?CompanyType=CJ&IsFilter=1',
            width: '1024',
            height: '100%',
            JQID: 'BiddingInfoInfoGrid',
            JQLoadingType: 'datagrid',
            iconCls: 'fa fa-plus',
        });
    }

    var _BussBiddingPackageBack = function (row) {
        debugger;
        $(""#BiddingId"").val(row.Id);
        $(""#EngineeringName"").textbox('setText', row.EngineeringName);
        $(""#BiddingNumber"").textbox('setText', row.BiddingNumber);
        debugger;
        if (row.BidStatus != 0) {
            $(""#ProgressName"").combotree(""setValue"", row.BidStatus);
        }
        $(""#WinTime"").datebox(""setValue"", row.BidStatusTime);
    }
</script>
");

            
            #line 57 "..\..\Views\BussBiddingCost\info_cj.cshtml"
 using (Html.BeginForm("save", "BussBiddingCost", FormMethod.Post, new { @area = "Bussiness", @id = "BussBiddingCostForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 59 "..\..\Views\BussBiddingCost\info_cj.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 59 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标编号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"BiddingNumber\"");

WriteLiteral(" name=\"BiddingNumber\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" />\r\n                <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" onclick=\"SelectBiddingInfoPackage()\"");

WriteLiteral(">选择投标项目</a>\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">项目名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"EngineeringName\"");

WriteLiteral(" name=\"EngineeringName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标状态</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                ");

WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 76 "..\..\Views\BussBiddingCost\info_cj.cshtml"
       Write(BaseData.getBase(new Params()
           {
               controlName = "ProgressName",
               isMult = false,
               isRequired = false,
               engName = "BiddingProgress",
               width = "98%"
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">中标时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"WinTime\"");

WriteLiteral(" name=\"WinTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">标书费</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TenderFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3847), Tuple.Create("\"", 3871)
            
            #line 95 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                        , Tuple.Create(Tuple.Create("", 3855), Tuple.Create<System.Object, System.Int32>(Model.TenderFee
            
            #line default
            #line hidden
, 3855), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">标书费缴纳时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TenderFeePayTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4081), Tuple.Create("\"", 4112)
            
            #line 99 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                         , Tuple.Create(Tuple.Create("", 4089), Tuple.Create<System.Object, System.Int32>(Model.TenderFeePayTime
            
            #line default
            #line hidden
, 4089), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">招标代理费</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TenderAgentFee\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4355), Tuple.Create("\"", 4384)
            
            #line 106 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                             , Tuple.Create(Tuple.Create("", 4363), Tuple.Create<System.Object, System.Int32>(Model.TenderAgentFee
            
            #line default
            #line hidden
, 4363), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">招标代理费缴纳时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"TenderAgentFeePayTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4601), Tuple.Create("\"", 4637)
            
            #line 110 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                              , Tuple.Create(Tuple.Create("", 4609), Tuple.Create<System.Object, System.Int32>(Model.TenderAgentFeePayTime
            
            #line default
            #line hidden
, 4609), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标保证金缴纳金额</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BidBondPay\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4880), Tuple.Create("\"", 4905)
            
            #line 117 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                         , Tuple.Create(Tuple.Create("", 4888), Tuple.Create<System.Object, System.Int32>(Model.BidBondPay
            
            #line default
            #line hidden
, 4888), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标保证金缴纳时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BidBondPayTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5115), Tuple.Create("\"", 5144)
            
            #line 121 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                       , Tuple.Create(Tuple.Create("", 5123), Tuple.Create<System.Object, System.Int32>(Model.BidBondPayTime
            
            #line default
            #line hidden
, 5123), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标保证金退还金额</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BidBondBack\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5388), Tuple.Create("\"", 5414)
            
            #line 128 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                          , Tuple.Create(Tuple.Create("", 5396), Tuple.Create<System.Object, System.Int32>(Model.BidBondBack
            
            #line default
            #line hidden
, 5396), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">投标保证金退还时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"BidBondBackTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5625), Tuple.Create("\"", 5655)
            
            #line 132 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                        , Tuple.Create(Tuple.Create("", 5633), Tuple.Create<System.Object, System.Int32>(Model.BidBondBackTime
            
            #line default
            #line hidden
, 5633), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">履约保证金缴纳金额</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"PerformanceBondPay\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5906), Tuple.Create("\"", 5939)
            
            #line 139 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                                 , Tuple.Create(Tuple.Create("", 5914), Tuple.Create<System.Object, System.Int32>(Model.PerformanceBondPay
            
            #line default
            #line hidden
, 5914), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">履约保证金缴纳时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"PerformanceBondPayTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6157), Tuple.Create("\"", 6194)
            
            #line 143 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                               , Tuple.Create(Tuple.Create("", 6165), Tuple.Create<System.Object, System.Int32>(Model.PerformanceBondPayTime
            
            #line default
            #line hidden
, 6165), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">履约保证金退还金额</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"PerformanceBondBack\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-numberspinner\"");

WriteLiteral(" validType=\"length[0,18]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6446), Tuple.Create("\"", 6480)
            
            #line 150 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                                  , Tuple.Create(Tuple.Create("", 6454), Tuple.Create<System.Object, System.Int32>(Model.PerformanceBondBack
            
            #line default
            #line hidden
, 6454), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">履约保证金退还时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"PerformanceBondBackTime\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"length[0,23]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 6699), Tuple.Create("\"", 6737)
            
            #line 154 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                                , Tuple.Create(Tuple.Create("", 6707), Tuple.Create<System.Object, System.Int32>(Model.PerformanceBondBackTime
            
            #line default
            #line hidden
, 6707), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注说明</th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 161 "..\..\Views\BussBiddingCost\info_cj.cshtml"
           Write(Html.TextAreaFor(model => Model.CostNote, new { @style = "width:98%;height:80px" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th>\r\n           " +
"     上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 170 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                
            
            #line default
            #line hidden
            
            #line 170 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "BussBiddingCost";
                    uploader.Name = "UploadFile1";
                    
            
            #line default
            #line hidden
            
            #line 175 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 175 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"BiddingId\"");

WriteLiteral(" name=\"BiddingId\"");

WriteLiteral(" />\r\n");

            
            #line 181 "..\..\Views\BussBiddingCost\info_cj.cshtml"
                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
