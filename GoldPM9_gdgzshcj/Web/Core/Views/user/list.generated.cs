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
    
    #line 4 "..\..\Views\user\list.cshtml"
    using JQ.Web;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/user/list.cshtml")]
    public partial class _Views_user_list_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_user_list_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\user\list.cshtml"
  
    Layout = "~/Areas/Core/Views/Shared/_Layout.cshtml";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

DefineSection("head", () => {

WriteLiteral("\r\n    <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n        var requestUrl = \'");

            
            #line 7 "..\..\Views\user\list.cshtml"
                     Write(Url.Action("json", "user",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            addUrl = \'");

            
            #line 8 "..\..\Views\user\list.cshtml"
                 Write(Url.Action("add","user",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            editUrl = \'");

            
            #line 9 "..\..\Views\user\list.cshtml"
                  Write(Url.Action("edit", "user",new {@area="Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n            delUrl = \'");

            
            #line 10 "..\..\Views\user\list.cshtml"
                 Write(Url.Action("del", "user", new { @area = "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\';\r\n        $(function () {\r\n            //数据表格\r\n            JQ.datagrid.datagrid" +
"({\r\n                JQAddUrl: addUrl,//添加的路径\r\n                JQIsSearch: true,/" +
"/是否加载数据之前就获取搜索参数值，可在搜索控件中添加默认值(默认为false)\r\n                JQEditUrl: editUrl,//编" +
"辑的路径\r\n                JQButtonTypes: [\'add\', \'edit\', \'export\', \'moreSearch\'],//需" +
"要显示的按钮\r\n                JQPrimaryID: \'EmpID\',//主键的字段\r\n                JQID: \'tt\'" +
",//数据表格ID\r\n                JQDialogTitle: \'用户信息\',//弹出窗体标题\r\n                JQWid" +
"th: \'760\',//弹出窗体宽\r\n                JQHeight: \'500\',//弹出窗体高\r\n                JQLo" +
"adingType: \'datagrid\',//数据表格类型 [datagrid,treegrid]\r\n                JQExcludeCol" +
": [5,6],//导出execl排除的列\r\n                JQMergeCells: { fields: [\'DepartmentName\'" +
"], Only: \'DepartmentName\' },//当字段值相同时会自动的跨行(只对相邻有效)\r\n                JQFields: [" +
"\"EmpID\"],//追加的字段名\r\n                JQSearch: {\r\n                    id: \'fullNam" +
"eSearch\',\r\n                    prompt: \'姓名、角色\',\r\n                    loadingType" +
": \'datagrid\',//默认值为datagrid可以不传\r\n                    $panel: $(\"#tb\")//搜索的容器，可以不" +
"传\r\n                },\r\n                url: requestUrl,//请求的地址\r\n                " +
"checkOnSelect: false,//是否包含check\r\n                fitColumns: true,\r\n           " +
"     nowrap: false,\r\n                toolbar: \'#tb\',//工具栏的容器ID\r\n                " +
"columns: [[\r\n                  { title: \'所属部门\', field: \'DepartmentName\', width: " +
"\"10%\", align: \'center\', sortable: true },\r\n                  { title: \'姓名\', fiel" +
"d: \'EmpName\', width: \"10%\", align: \'center\', sortable: true },\r\n                " +
"  { title: \'用户名\', field: \'EmpLogin\', width: \"10%\", align: \'center\', sortable: tr" +
"ue },\r\n                  { title: \'角色\', field: \'RoleName\', width: \"45%\", align: " +
"\'center\' },\r\n                  {\r\n                      title: \'状态\', field: \'Emp" +
"IsDeleted\', width: \"10%\", align: \'center\', formatter: function (val, row) {\r\n   " +
"                       var value = val > 0 ? \"<span style=\'color:gray\'>停用</span>" +
"\" : \"<span style=\'color:green\'>启用</span>\";\r\n                          //row.EmpI" +
"sDeleted = value;//更新数据行数据\r\n                          return value;\r\n           " +
"           }\r\n                  },\r\n                  {\r\n                      t" +
"itle: \'签名\', field: \'Sign\', JQIsExclude: true, width: \"130px\", align: \'center\', f" +
"ormatter: function (val, row) {\r\n                          var value = \"<img src" +
"=\'../../SignImages/\" + row.EmpLogin + \".png\' onerror=\\\"javascript:this.src=\'\'\\\" " +
" style=\'width:100px; height:40px;\' alt=\'无\'>\";\r\n                          return " +
"value;\r\n                      }\r\n                  },\r\n                ]],\r\n    " +
"            onRowContextMenu: function (e, index, row) { //右键时触发事件\r\n            " +
"        var $m = $(\'#userMenu\'),\r\n                        itemState = $m.menu(\'g" +
"etItem\', $(\'#setState\')[0]),\r\n                        text = row.EmpIsDeleted ==" +
" 1 ? \'启用\' : \'停用\';\r\n                    $.data(document.body, \"selectTempData\", r" +
"ow);//把临时数据存在缓存中\r\n                    $m.menu(\'setText\', {\r\n                    " +
"    target: itemState.target,\r\n                        text: text\r\n             " +
"       });\r\n                    $m.menu(\'show\', {\r\n                        left:" +
" e.pageX,\r\n                        top: e.pageY\r\n                    });\r\n      " +
"              e.preventDefault();\r\n                }\r\n            });\r\n         " +
"   \r\n\r\n            $(\"#httpUpload\").click(function () {\r\n                var div" +
"id = getMathNo();\r\n                if (window.top.$(\"#\" + divid).size() <= 0) {\r" +
"\n                    window.top.$(\"body\").append(\"<div class=\'rwpdialogdiv\' id=\'" +
"\" + divid + \"\'></div>\");\r\n                }\r\n                var paraJosn = {\r\n " +
"                   iconCls: \'fa fa-upload\',\r\n                    title: \"<b>手写签名" +
"文件上传</b>\",\r\n                    content: \'<iframe id=\"uploadIframe\" frameborder=" +
"\"0\" scrolling=\"no\" style=\"width:100%;height:99%\" src=\"");

            
            #line 83 "..\..\Views\user\list.cshtml"
                                                                                                                     Write(Url.Action("uploadByHttp", "usercontrol", new { @area = "Core", }));

            
            #line default
            #line hidden
WriteLiteral(@"?AttachRefID=0&AttachRefTable=ElecFile""></iframe>',
                    width: '608',
                    height: '353',
                    resizable: false,
                    maximizable: false,
                    collapsible: false,
                    modal: true,
                    onClose: function () {
                        Url = '");

            
            #line 91 "..\..\Views\user\list.cshtml"
                          Write(Url.Action("signMove", "user", new { @area = "Core", }));

            
            #line default
            #line hidden
WriteLiteral(@"';
                        JQ.tools.ajax({
                            doBackResult: true,
                            url: Url,
                        });
                        $('#tt').datagrid('reload');
                        window.top.$(""#"" + divid).parent().remove();
                    }
                };
                var dialog = window.top.$(""#"" + divid).dialog(paraJosn);
            });
        });
        function menuHandler(item) {
            var data = $.data(document.body, ""selectTempData"");//获取临时数据
            if (JQ.tools.isJson(data)) {
                switch (item.id) {
                    case ""setState"":
                        JQ.tools.ajax({
                            url: delUrl + '?EmpID=' + data.EmpID + '&isDel=' + (data.EmpIsDeleted == 1 ? '0' : '1'),
                            succesCollBack: function () {
                                $('#tt').datagrid('reload');
                            }
                        });
                        break;
                    case ""moveUp"":
                        JQ.tools.ajax({
                            url: '");

            
            #line 117 "..\..\Views\user\list.cshtml"
                             Write(Url.Action("UpdateEmpOrder", "user",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?EmpID=' + data.EmpID + '&OrderType=1',
                            succesCollBack: function () {
                                $('#tt').datagrid('reload');
                            }
                        });
                        break;
                    case ""moveDown"":
                        JQ.tools.ajax({
                            url: '");

            
            #line 125 "..\..\Views\user\list.cshtml"
                             Write(Url.Action("UpdateEmpOrder", "user",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?EmpID=' + data.EmpID + '&OrderType=2',
                            succesCollBack: function () {
                                $('#tt').datagrid('reload');
                            }
                        });
                        break;
                    case ""DefaultPass"":
                        JQ.tools.ajax({
                            url: '");

            
            #line 133 "..\..\Views\user\list.cshtml"
                             Write(Url.Action("DefaultPass", "user",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?EmpID=' + data.EmpID,
                            succesCollBack: function () {
                                $('#tt').datagrid('reload');
                            }
                        });
                        break;
                    case ""ToEmp"":
                        JQ.dialog.confirm(""确定要切换到此账户吗？"", function () {
                            JQ.tools.ajax({
                                url: '");

            
            #line 142 "..\..\Views\user\list.cshtml"
                                 Write(Url.Action("ToEmp", "user",new { @area="Core"}));

            
            #line default
            #line hidden
WriteLiteral(@"?EmpID=' + data.EmpID,
                                succesCollBack: function (d) {
                                    window.top.location.href = d.url;
                                }
                            });
                        });
                        break;
                }
            }
            $.data(document.body, ""selectTempData"", null);//清空临时数据
            return false;
        }
        function getMathNo() {
            var d = new Date();
            var sp = """";
            var month = d.getMonth() + 1;
            var strDate = d.getDate();
            if (month >= 1 && month <= 9) {
                month = ""0"" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = ""0"" + strDate;
            }
            var math = Math.floor(Math.random() * (1000000 + 1));
            var cd = d.getHours() + sp + d.getMinutes() + sp + d.getSeconds() + sp + d.getMilliseconds();
            return math + sp + cd;
        };
        //同步部门人员
        function SynchDeptEmp() {
            JQ.dialog.confirm(""确定要同步部门人员到RTX中吗？"", function () {
                JQ.tools.ajax({
                    url: '");

            
            #line 173 "..\..\Views\user\list.cshtml"
                      Write(Url.Action("LinkSynchDeptEmp", "user", new { @area= "Core" }));

            
            #line default
            #line hidden
WriteLiteral("\',\r\n                    \r\n                    succesCollBack: function (data) {\r\n" +
"                    }\r\n                });\r\n            }, null);\r\n        };\r\n " +
"   </script>\r\n");

WriteLiteral("    ");

            
            #line 181 "..\..\Views\user\list.cshtml"
Write(RenderPage("~/Areas/Core/Views/Shared/_Init.cshtml"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n");

DefineSection("Body_Content", () => {

WriteLiteral("\r\n    <table");

WriteLiteral(" id=\"tt\"");

WriteLiteral("></table>\r\n    <div");

WriteLiteral(" id=\"tb\"");

WriteLiteral(" style=\"padding:5px;height:auto;\"");

WriteLiteral(">\r\n        <span");

WriteLiteral(" JQPanel=\"toolbarPanel\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-plus\'\"");

WriteLiteral(" id=\"httpUpload\"");

WriteLiteral(">签名上传</a>\r\n            <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton\"");

WriteLiteral(" data-options=\"plain:true,iconCls:\'fa fa-play-circle\'\"");

WriteLiteral(" style=\"display:none;\"");

WriteLiteral(" onclick=\"SynchDeptEmp()\"");

WriteLiteral(">同步部门人员</a>\r\n        </span>\r\n        <span");

WriteLiteral(" class=\'datagrid-btn-separator\'");

WriteLiteral(" style=\'vertical-align: middle; height: 22px;display:inline-block;float:none\'");

WriteLiteral("></span>\r\n");

WriteLiteral("        ");

            
            #line 192 "..\..\Views\user\list.cshtml"
   Write(BaseData.getBases(new Params() { isMult = true, engName = "department", queryOptions = "{ Key:'EmpDepID', Contract:'in',filedType:'Int'}" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <input");

WriteLiteral(" id=\"fullNameSearch\"");

WriteLiteral(" style=\"width:250px;\"");

WriteLiteral(" JQWhereOptions=\"{ Key: \'EmpName,RoleName\', \'Contract\': \'like\' }\"");

WriteLiteral("    />\r\n        <div");

WriteLiteral(" class=\"moreSearchPanel\"");

WriteLiteral(">         \r\n            <select");

WriteLiteral(" id=\"isStartSearch\"");

WriteLiteral(" class=\"easyui-combobox\"");

WriteLiteral(" JQWhereOptions=\"[{ Key:\'EmpIsDeleted\', Contract:\'=\',filedType:\'Bool\'}]\"");

WriteLiteral(">\r\n                <option");

WriteLiteral(" value=\"\"");

WriteLiteral(">所有状态</option>\r\n                <option");

WriteLiteral(" value=\"0\"");

WriteLiteral(" selected=\"selected\"");

WriteLiteral(">启用</option>\r\n                <option");

WriteLiteral(" value=\"1\"");

WriteLiteral(">停用</option>\r\n            </select>&nbsp;\r\n            <input");

WriteLiteral(" id=\"startTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'生日开始\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'EmpBirthDate\', Contract:\'>=\',filedType:\'Date\' }\"");

WriteLiteral(">\r\n            至\r\n            <input");

WriteLiteral(" id=\"endTime\"");

WriteLiteral(" type=\"text\"");

WriteLiteral(" class=\"easyui-datebox\"");

WriteLiteral(" style=\"width:110px\"");

WriteLiteral(" data-options=\"prompt: \'生日结束\'\"");

WriteLiteral(" JQWhereOptions=\"{ Key:\'EmpBirthDate\', Contract:\'<=\',filedType:\'Date\' }\"");

WriteLiteral(">&nbsp;\r\n        </div>\r\n    </div>\r\n \r\n    <div");

WriteLiteral(" id=\"userMenu\"");

WriteLiteral(" class=\"easyui-menu\"");

WriteLiteral(" data-options=\"onClick:menuHandler\"");

WriteLiteral(" style=\"width:100px;display: none;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" id=\"setState\"");

WriteLiteral("></div>\r\n        <div");

WriteLiteral(" id=\"DefaultPass\"");

WriteLiteral(">恢复密码</div>\r\n        <div");

WriteLiteral(" id=\"moveUp\"");

WriteLiteral(">上移</div>\r\n        <div");

WriteLiteral(" id=\"moveDown\"");

WriteLiteral(">下移</div>\r\n        <div");

WriteLiteral(" id=\"ToEmp\"");

WriteLiteral(">切换登录用户</div>\r\n    </div>\r\n");

});

WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591