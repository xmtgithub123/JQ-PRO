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
    
    #line 4 "..\..\Views\BussCustomer\info.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/BussCustomer/info.cshtml")]
    public partial class _Views_BussCustomer_info_cshtml : System.Web.Mvc.WebViewPage<DataModel.Models.BussCustomer>
    {
        public _Views_BussCustomer_info_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n\r\n");

WriteLiteral("<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(@">
        JQ.form.submitInit({
            formid: 'BussCustomerForm',//formid提交需要用到
            buttonTypes: ['submit', 'close'],//默认按钮
            succesCollBack: function (data) {//成功回调函数,data为服务器返回值
                JQ.datagrid.autoRefdatagrid();//刷新上一级数据表格，必须在close窗体前调用
                JQ.dialog.dialogClose();//关闭窗体放在最后一步执行，以避免找不到事件源
            },
            beforeFormSubmit: function () {
                var result = true;
                var CustName = $(""#CustName"").textbox(""getValue"");
                $.ajax({
                    url: '");

            
            #line 17 "..\..\Views\BussCustomer\info.cshtml"
                     Write(Url.Action("CheckCustomerExists", "BussCustomer",new { @area="Bussiness"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                    data: \'TypeID=");

            
            #line 18 "..\..\Views\BussCustomer\info.cshtml"
                             Write(Model.TypeID);

            
            #line default
            #line hidden
WriteLiteral("&CustName=\'+CustName+\'&Id=");

            
            #line 18 "..\..\Views\BussCustomer\info.cshtml"
                                                                    Write(Model.Id);

            
            #line default
            #line hidden
WriteLiteral(@"',
                    async: false,
                    success: function (data) {
                        if (data != ""0"") {
                            result = false;
                            JQ.dialog.info(""客户名称--["" + CustName + ""]--已存在"");
                        }
                    }
                });
                return result;
            }
        });
   
</script>
");

            
            #line 32 "..\..\Views\BussCustomer\info.cshtml"
 using (Html.BeginForm("save", "BussCustomer", FormMethod.Post, new { @area = "Bussiness", @id = "BussCustomerForm" }))
{
    
            
            #line default
            #line hidden
            
            #line 34 "..\..\Views\BussCustomer\info.cshtml"
Write(Html.HiddenFor(m => m.TypeID));

            
            #line default
            #line hidden
            
            #line 34 "..\..\Views\BussCustomer\info.cshtml"
                                  
    
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\BussCustomer\info.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\BussCustomer\info.cshtml"
                              

            
            #line default
            #line hidden
WriteLiteral("    <table");

WriteLiteral(" class=\"table table-bordered\"");

WriteLiteral(">\r\n\r\n        <tr>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户类别</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 42 "..\..\Views\BussCustomer\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "CustType",
               isRequired = true,
               engName = "CustType",
               width = "98%",
               ids = Model.CustType.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户单位名称</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" id=\"CustName\"");

WriteLiteral(" name=\"CustName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"required:true\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2071), Tuple.Create("\"", 2094)
            
            #line 53 "..\..\Views\BussCustomer\info.cshtml"
                                                                             , Tuple.Create(Tuple.Create("", 2079), Tuple.Create<System.Object, System.Int32>(Model.CustName
            
            #line default
            #line hidden
, 2079), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户地区</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 60 "..\..\Views\BussCustomer\info.cshtml"
           Write(BaseData.getBase(new Params()
           {
               controlName = "CustAddressID",
               isRequired = true,
               engName = "CustAddressID",
               width = "98%",
               ids = Model.CustAddressID.ToString()
           }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">办公地址</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustAddress\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2703), Tuple.Create("\"", 2729)
            
            #line 71 "..\..\Views\BussCustomer\info.cshtml"
                                      , Tuple.Create(Tuple.Create("", 2711), Tuple.Create<System.Object, System.Int32>(Model.CustAddress
            
            #line default
            #line hidden
, 2711), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">法定代表人</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustChineseName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 2970), Tuple.Create("\"", 3000)
            
            #line 79 "..\..\Views\BussCustomer\info.cshtml"
                                         , Tuple.Create(Tuple.Create("", 2978), Tuple.Create<System.Object, System.Int32>(Model.CustChineseName
            
            #line default
            #line hidden
, 2978), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户邮编</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustPost\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,50]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3199), Tuple.Create("\"", 3222)
            
            #line 83 "..\..\Views\BussCustomer\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 3207), Tuple.Create<System.Object, System.Int32>(Model.CustPost
            
            #line default
            #line hidden
, 3207), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户电话</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustTel\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3452), Tuple.Create("\"", 3474)
            
            #line 90 "..\..\Views\BussCustomer\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 3460), Tuple.Create<System.Object, System.Int32>(Model.CustTel
            
            #line default
            #line hidden
, 3460), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">电子邮箱</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustEmail\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,500]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3675), Tuple.Create("\"", 3699)
            
            #line 94 "..\..\Views\BussCustomer\info.cshtml"
                                   , Tuple.Create(Tuple.Create("", 3683), Tuple.Create<System.Object, System.Int32>(Model.CustEmail
            
            #line default
            #line hidden
, 3683), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户传真</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustFax\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,100]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3929), Tuple.Create("\"", 3951)
            
            #line 101 "..\..\Views\BussCustomer\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 3937), Tuple.Create<System.Object, System.Int32>(Model.CustFax
            
            #line default
            #line hidden
, 3937), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">客户网址</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustWeb\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,200]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4152), Tuple.Create("\"", 4174)
            
            #line 106 "..\..\Views\BussCustomer\info.cshtml"
                                 , Tuple.Create(Tuple.Create("", 4160), Tuple.Create<System.Object, System.Int32>(Model.CustWeb
            
            #line default
            #line hidden
, 4160), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">开户银行</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustBankName\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4412), Tuple.Create("\"", 4439)
            
            #line 114 "..\..\Views\BussCustomer\info.cshtml"
                                       , Tuple.Create(Tuple.Create("", 4420), Tuple.Create<System.Object, System.Int32>(Model.CustBankName
            
            #line default
            #line hidden
, 4420), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">银行帐号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustBankAccount\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4647), Tuple.Create("\"", 4677)
            
            #line 118 "..\..\Views\BussCustomer\info.cshtml"
                                          , Tuple.Create(Tuple.Create("", 4655), Tuple.Create<System.Object, System.Int32>(Model.CustBankAccount
            
            #line default
            #line hidden
, 4655), false)
);

WriteLiteral(" />\r\n            </td>\r\n        </tr>\r\n\r\n\r\n\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">税号</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustBankTariff\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4919), Tuple.Create("\"", 4948)
            
            #line 128 "..\..\Views\BussCustomer\info.cshtml"
                                         , Tuple.Create(Tuple.Create("", 4927), Tuple.Create<System.Object, System.Int32>(Model.CustBankTariff
            
            #line default
            #line hidden
, 4927), false)
);

WriteLiteral(" />\r\n            </td>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">业务关系建立时间</th>\r\n            <td");

WriteLiteral(" width=\"40%\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustDate\"");

WriteLiteral(" style=\"width:98%;\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" validType=\"dateISO\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5146), Tuple.Create("\"", 5169)
            
            #line 132 "..\..\Views\BussCustomer\info.cshtml"
                            , Tuple.Create(Tuple.Create("", 5154), Tuple.Create<System.Object, System.Int32>(Model.CustDate
            
            #line default
            #line hidden
, 5154), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n        <tr>\r\n            <th");

WriteLiteral(" width=\"10%\"");

WriteLiteral(">备注说明</th>\r\n            <td");

WriteLiteral(" width=\"90%\"");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n                <input");

WriteLiteral(" name=\"CustNote\"");

WriteLiteral(" style=\"width:98%;height:80px;\"");

WriteLiteral(" class=\"easyui-textbox\"");

WriteLiteral(" data-options=\"multiline:true\"");

WriteLiteral(" validType=\"length[0,1000]\"");

WriteAttribute("value", Tuple.Create(" value=\"", 5457), Tuple.Create("\"", 5480)
            
            #line 140 "..\..\Views\BussCustomer\info.cshtml"
                                                                             , Tuple.Create(Tuple.Create("", 5465), Tuple.Create<System.Object, System.Int32>(Model.CustNote
            
            #line default
            #line hidden
, 5465), false)
);

WriteLiteral(" />\r\n            </td>\r\n\r\n        </tr>\r\n\r\n\r\n        <tr>\r\n            <th>\r\n    " +
"            上传附件\r\n            </th>\r\n            <td");

WriteLiteral(" colspan=\"3\"");

WriteLiteral(">\r\n");

            
            #line 151 "..\..\Views\BussCustomer\info.cshtml"
                
            
            #line default
            #line hidden
            
            #line 151 "..\..\Views\BussCustomer\info.cshtml"
                  
                    var uploader = new DataModel.FileUploader();
                    uploader.RefID = Model.Id;
                    uploader.RefTable = "Customer";
                    uploader.Name = "uploadfile1";
                    
            
            #line default
            #line hidden
            
            #line 156 "..\..\Views\BussCustomer\info.cshtml"
                Write(Html.Partial("~/Areas/Core/Views/UserControl/FileUploader.cshtml", uploader));

            
            #line default
            #line hidden
            
            #line 156 "..\..\Views\BussCustomer\info.cshtml"
                                                                                                   
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");

            
            #line 161 "..\..\Views\BussCustomer\info.cshtml"

                    }

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
