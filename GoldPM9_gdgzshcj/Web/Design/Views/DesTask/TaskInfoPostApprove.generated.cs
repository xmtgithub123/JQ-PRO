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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DesTask/TaskInfoPostApprove.cshtml")]
    public partial class _Views_DesTask_TaskInfoPostApprove_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DesTask_TaskInfoPostApprove_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    #TaskInfoPerson td {\r\n        font-size: 16px;\r\n        height: 30px;\r\n   " +
" }\r\n\r\n    .FlowNodeClass td {\r\n        font-size: 12px !important;\r\n        heig" +
"ht: 20px !important;\r\n        border: 1px solid #D3D3D3;\r\n    }\r\n\r\n    .btn-medi" +
"um {\r\n        padding: 5px 15px;\r\n        color: white !important;\r\n        widt" +
"h: 100px;\r\n    }\r\n\r\n    .NextSpan {\r\n        padding: 5px 5px;\r\n        font-siz" +
"e: 18px;\r\n    }\r\n\r\n    .pagefoot {\r\n        text-align: center;\r\n        padding" +
": 5px;\r\n        height: auto;\r\n    }\r\n\r\n        .pagefoot .l-btn-text {\r\n       " +
"     vertical-align: middle !important;\r\n            font-size: 14px;\r\n        }" +
"\r\n\r\n        .pagefoot .btn-blue {\r\n            background: -webkit-linear-gradie" +
"nt(top,#67c2ff 0,#3d93cf 100%);\r\n            background: -moz-linear-gradient(to" +
"p,#67c2ff 0,#3d93cf 100%);\r\n            background: -o-linear-gradient(top,#67c2" +
"ff 0,#3d93cf 100%);\r\n            background: linear-gradient(top,#67c2ff 0,#3d93" +
"cf 100%);\r\n            border: none;\r\n        }\r\n\r\n        .pagefoot .btn-green " +
"{\r\n            background: -webkit-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r" +
"\n            background: -moz-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n    " +
"        background: -o-linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n           " +
" background: linear-gradient(top,#6cd2b7 0,#69a898 100%);\r\n            border: n" +
"one;\r\n        }\r\n\r\n        .pagefoot .btn-gray {\r\n            background: -webki" +
"t-linear-gradient(top,#bebebe 0,#808080 100%);\r\n            background: -moz-lin" +
"ear-gradient(top,#bebebe 0,#808080 100%);\r\n            background: -o-linear-gra" +
"dient(top,#bebebe 0,#808080 100%);\r\n            background: linear-gradient(top," +
"#bebebe 0,#808080 100%);\r\n            border: none;\r\n        }\r\n\r\n        .pagef" +
"oot .btn-blue:hover {\r\n            background: #3d93cf;\r\n        }\r\n\r\n        .p" +
"agefoot .btn-green:hover {\r\n            background: #69a898;\r\n        }\r\n\r\n     " +
"   .pagefoot .btn-gray:hover {\r\n            background: #808080;\r\n        }\r\n</s" +
"tyle>\r\n\r\n\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteAttribute("src", Tuple.Create(" src=\"", 2066), Tuple.Create("\"", 2111)
            
            #line 74 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
, Tuple.Create(Tuple.Create("", 2072), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Scripts/JQ/DesFlow.js")
            
            #line default
            #line hidden
, 2072), false)
);

WriteLiteral("></script>\r\n\r\n<div");

WriteLiteral(" id=\"TaskInfoPostApprove\"");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"easyui-layout\"");

WriteLiteral(" data-options=\"fit:true\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" data-options=\"region:\'north\',border:false\"");

WriteLiteral(" style=\"border-bottom-width:1px;overflow:hidden;display:none;\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" style=\"height:30px;padding:5px 0px;\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" style=\"float:left;margin-left:5px;\"");

WriteLiteral(">\r\n                        校审流程：\r\n                        <div");

WriteLiteral(" id=\"FlowNodeView\"");

WriteLiteral(" class=\"approve-progress\"");

WriteLiteral("></div>\r\n                    </div>\r\n                    ");

WriteLiteral("\r\n                </div>\r\n            </div>\r\n            <div");

WriteLiteral(" data-options=\"region:\'center\',border:false\"");

WriteLiteral(" style=\"border-right-width:1px;\"");

WriteLiteral(">\r\n                <table");

WriteLiteral(" id=\"TaskInfoPostApprove_files\"");

WriteLiteral("></table>\r\n                <script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n                    var _Qualification=");

            
            #line 98 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                  Write(Html.Raw(ViewBag.Qualification));

            
            #line default
            #line hidden
WriteLiteral("; // 普通生产流程人员资格列表\r\n\r\n                    $(function () {\r\n                       " +
" $(\'#TaskInfoPostApprove_files\').treegrid({\r\n                            url: \'");

            
            #line 102 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                             Write(Url.Action("TaskInfoGetDesTaskAttachSelDataJson"));

            
            #line default
            #line hidden
WriteLiteral("?TaskId=\'+");

            
            #line 102 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                                                         Write(ViewBag.TaskModel.Id);

            
            #line default
            #line hidden
WriteLiteral(@",
                            idField: 'AttachID',
                            treeField: 'AttachName',
                            border: 0,
                            fit: true,
                            lines: true,
                            animate: true,
                            rownumbers:true,
                            queryParams:{AttachIds:'");

            
            #line 110 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                               Write(Html.Raw(ViewBag.AttachIds));

            
            #line default
            #line hidden
WriteLiteral("\'},\r\n                            //checkOnSelect:false,\r\n                        " +
"    selectOnCheck:true,\r\n                            checkOnSelect:true,\r\n      " +
"                      singleSelect:true,\r\n                            columns: [" +
"[\r\n                                { title: \'附件信息\', field: \'AttachName\', width: " +
"550, align: \'left\',halign:\'center\',formatter:function (value,row,index) {\r\n     " +
"                               var FileName=\"<div style=\\\"float:left\\\">\"+row.Att" +
"achName+\"</div>\";\r\n                                    return FileName;\r\n       " +
"                         } },  { title: \"版本\", field: \"AttachVer\", width: 50, ali" +
"gn: \"center\", halign: \"center\",formatter:function (value,row,index) {\r\n         " +
"                           if(row.AttachExt==\".\"){\r\n                            " +
"            return \"\";\r\n                                    }else {\r\n           " +
"                             return value;\r\n                                    " +
"}\r\n                                } },\r\n                                {\r\n    " +
"                                title: \"大小\", field: \"AttachSize\", width: 76, ali" +
"gn: \"center\", halign: \"center\", formatter: function (value, row,index) {\r\n      " +
"                                  if(row.AttachExt==\".\"){\r\n                     " +
"                       return \"\";\r\n                                        }else" +
" {\r\n                                            return JQ.tools.getFileSizeText(" +
"value);\r\n                                        }\r\n                            " +
"        }\r\n                                },\r\n                                {" +
"\r\n                                    title: \"修改时间\", field: \"AttachDateChange\", " +
"width: 90, align: \"center\", halign: \"center\", formatter: function (value, rowDat" +
"a,index) {\r\n                                        return JQ.tools.DateTimeForm" +
"atterByT(rowData.AttachDateChange);\r\n                                    }\r\n    " +
"                            },\r\n                                {\r\n             " +
"                       title: \"上传日期\", field: \"AttachDateUpload\", width: 90, alig" +
"n: \"center\", halign: \"center\", formatter: function (value, rowData,index) {\r\n   " +
"                                     return JQ.tools.DateTimeFormatterByT(rowDat" +
"a.AttachDateUpload);\r\n                                    }\r\n                   " +
"             },\r\n                                {\r\n                            " +
"        title: \"操作\", field: \"AttachID\", width: 80, align: \"center\", halign: \"cen" +
"ter\", formatter: function (value, rowData,index) {\r\n                            " +
"            return \"<a id=\\\"btnDel\\\" href=\\\"#\\\" rootID=\"+rowData.AttachID+\" >排除<" +
"/a>  \";\r\n                                    }\r\n                                " +
"}\r\n                            ]],\r\n                            onLoadSuccess:fu" +
"nction (row,data) {\r\n                                if (data.total>0) {\r\n      " +
"                              $.each(data.rows,function (i,e) {\r\n               " +
"                         if (e.AttachExt==\".\") {\r\n                              " +
"              if ( $(\"#TaskInfoPostApprove_files\").treegrid(\"getChildren\",e.Atta" +
"chID).length==0) {\r\n                                                $(\"#TaskInfo" +
"PostApprove_files\").treegrid(\"update\",{id:e.AttachID,row:{iconCls:\"fa-folder\"}})" +
";\r\n                                                $(\"span[class=\'tree-icon tree" +
"-file fa-folder\']\").removeClass(\"tree-file\");\r\n                                 " +
"               //$(\"#TaskInfoDiv > .tree-icon,#TaskInfoDiv > .tree-file,#TaskInf" +
"oDiv > .fa-folder\").removeClass(\"tree-file\");\r\n                                 " +
"           }\r\n                                        }\r\n                       " +
"                 //删除样式\r\n                                        $(\"a[rootID=\'\"+" +
"e.AttachID+\"\']\").linkbutton({\r\n                                            iconC" +
"ls: \'fa fa-ban\',\r\n                                            onClick:function (" +
") {\r\n                                                deleteFunc(e.AttachID);\r\n  " +
"                                          }\r\n                                   " +
"     })\r\n\r\n                                    })\r\n\r\n                           " +
"         //显示节点信息\r\n                                    showNodeInfo();\r\n        " +
"                        }\r\n                            },\r\n                     " +
"       onClickRow: function (rowIndex, rowData) {\r\n                             " +
"   $(\"#TaskInfoPostApprove_files\").datagrid(\'unselectRow\', rowIndex);\r\n         " +
"                   }\r\n                        })\r\n                    });\r\n\r\n   " +
"                 function deleteFunc(AttachID) {\r\n                        //retu" +
"rn JQ.dialog.confirm(\"你确定要删除吗?\",function (){\r\n                            var pa" +
"rentNode=$(\"#TaskInfoPostApprove_files\").treegrid(\"getParent\",AttachID);\r\n      " +
"                      $(\"#TaskInfoPostApprove_files\").treegrid(\"remove\",AttachID" +
");\r\n                            //判断父节点\r\n                            if (parentN" +
"ode!=null) {\r\n                                if ($(\"#TaskInfoPostApprove_files\"" +
").treegrid(\"getChildren\",parentNode.AttachID).length==0) {\r\n                    " +
"                $(\"#TaskInfoPostApprove_files\").treegrid(\"update\", { id: parentN" +
"ode.AttachID, row: { iconCls: \"fa-folder\" } });\r\n\r\n                             " +
"       var panel= $(\"#TaskInfoPostApprove_files\").treegrid(\"getPanel\");\r\n       " +
"                             var delHtml=panel.find(\"tr[node-id=\'\"+parentNode.At" +
"tachID+\"\'] a[id=\'btnDel\']\");\r\n                                    delHtml.linkbu" +
"tton({\r\n                                        iconCls: \'fa fa-trash\',\r\n       " +
"                                 onClick:function () {\r\n                        " +
"                    deleteFunc(parentNode.AttachID);\r\n                          " +
"              }\r\n                                    });\r\n\r\n\r\n                  " +
"                  $(\"span[class=\'tree-icon tree-file fa-folder\']\").removeClass(\"" +
"tree-file\");\r\n                                }\r\n                            }\r\n" +
"                        //});\r\n                    }\r\n                </script>\r" +
"\n            </div>\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" data-options=\"region:\'south\',border:true\"");

WriteLiteral(" style=\"overflow:hidden;\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"pagefoot\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton btn-medium btn-gray\"");

WriteLiteral(" id=\"btnClose\"");

WriteLiteral(">取消</a>&nbsp;&nbsp;&nbsp;&nbsp;\r\n            <a");

WriteLiteral(" href=\"javascript:void(0);\"");

WriteLiteral(" class=\"easyui-linkbutton btn-medium btn-blue\"");

WriteLiteral(" id=\"btnNext\"");

WriteLiteral(">确定</a>\r\n        </div>\r\n    </div>\r\n</div>\r\n<script");

WriteLiteral(" type=\"text/javascript\"");

WriteLiteral(">\r\n\r\n    function  showNodeInfo() {\r\n\r\n        var NodeInfo=JSON.parse(\'");

            
            #line 220 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                            Write(Html.Raw(ViewBag.TaskModel.TaskFlowModelJson));

            
            #line default
            #line hidden
WriteLiteral("\').root.item;\r\n\r\n        var FlowNodeData=Enumerable.From(NodeInfo).OrderBy(\"x=>x" +
".rownum\").ToArray();\r\n\r\n        var SpecID=");

            
            #line 224 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
              Write(ViewBag.TaskModel.TaskSpecId);

            
            #line default
            #line hidden
WriteLiteral(@";//当前专业

        $('#FlowNodeView').empty();
        $.each(FlowNodeData,function (ic,ec) {
            getEmpDrop(SpecID,ec,$('#FlowNodeView'));
        });

        //JQ.DesFlow.show({
        //    Ctl:$('#FlowNodeView'),
        //    FlowData:NodeInfo,
        //});

    }

    //设置drop
    function getEmpDrop(SpecID,e,$item,rootId) {
        //获取资格人员
        var EmpData=null;
        if (SpecID == 0) {
            // 汇总的设计人员 = 项目负责人 + 各专业负可选责人 + 各专业设计人员
            if (e.FlowNodeTypeID == '");

            
            #line 244 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                 Write((int)DataModel.NodeType.设计);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n                // 获取项目负责人\r\n                var _pdata = [];\r\n             " +
"   _pdata.push({\r\n                    EmpID : ");

            
            #line 248 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                       Write(ViewBag.projEmpId);

            
            #line default
            #line hidden
WriteLiteral(",\r\n                    EmpName : \'");

            
            #line 249 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                           Write(@Html.Raw(ViewBag.projEmpName));

            
            #line default
            #line hidden
WriteLiteral("\'\r\n                });\r\n\r\n                // 获取专业负责人 + 各专业设计人员，\r\n                " +
"var _sdata = Enumerable.From(_Qualification).Where(\"x=>x.QualificationSpecID!=-1" +
"&&(x.QualificationValue==");

            
            #line 253 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                                                                                     Write((int)DataModel.NodeType.专业);

            
            #line default
            #line hidden
WriteLiteral(" || x.QualificationValue==");

            
            #line 253 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                                                                                                                                            Write((int)DataModel.NodeType.设计);

            
            #line default
            #line hidden
WriteLiteral(")\").Select(\"x=>{EmpID:x.EmpID,EmpName:x.EmpName}\").ToArray();\r\n\r\n                " +
"// 合并上述人员\r\n                EmpData = Enumerable.From(_sdata).Union(_pdata).Disti" +
"nct(\"x=>x.EmpID\").ToArray();\r\n            } else {\r\n                // 汇总的校审人员 =" +
" 所有专业可选校审人员\r\n                EmpData = Enumerable.From(_Qualification).Where(\"x=" +
">x.QualificationSpecID!=-1&&x.QualificationValue==\" +e.FlowNodeTypeID +\"\").Selec" +
"t(\"x=>{EmpID:x.EmpID,EmpName:x.EmpName}\").Distinct(\"x=>x.EmpID\").ToArray();\r\n   " +
"         }\r\n        } else {\r\n            // 非汇总的设校审批人员默认从资格配置获取\r\n            Em" +
"pData = Enumerable.From(_Qualification).Where(\"x=>x.QualificationSpecID==\" + Spe" +
"cID + \"&&x.QualificationValue==\" +e.FlowNodeTypeID +\"\").Select(\"x=>{EmpID:x.EmpI" +
"D,EmpName:x.EmpName}\").ToArray();\r\n        }\r\n        var $select= $(\"<select id" +
"=\\\"empDrop_\"+SpecID+\"_\"+e.ID+\"\\\" style=\\\"width:70px; margin-right:4px;\\\"></selec" +
"t>\");\r\n        $select.appendTo($item);\r\n        $select.combobox({\r\n           " +
" valueField: \'EmpID\',\r\n            textField: \'EmpName\',\r\n            data: EmpD" +
"ata,\r\n            width: \'95px\',\r\n            panelMaxHeight: \'360px\',\r\n        " +
"    required: true,\r\n            editable:false,\r\n        });\r\n        var $comb" +
"o = $select.next();\r\n        // $combo.addClass(\"combo-node node-status-\" + e.Fl" +
"owNodeStatus);\r\n        $combo.addClass(\"combo-node\");\r\n        $(\"<span style=\\" +
"\"line-height: 27px; padding-left:4px;\\\">\"+e.FlowNodeName+\":</span>\").prependTo($" +
"combo);\r\n\r\n        if (e.hasOwnProperty(\"FlowNodeEmpID\")&&e.FlowNodeEmpID!=0) {\r" +
"\n            $select.combobox(\"setValue\",e.FlowNodeEmpID);\r\n        }\r\n\r\n       " +
" if (e.FlowNodeTypeID == \'");

            
            #line 285 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                             Write((int)DataModel.NodeType.设计);

            
            #line default
            #line hidden
WriteLiteral("\') {\r\n            $select.combobox(\"readonly\", true);\r\n            $combo.find(\"." +
"textbox-addon-right\").remove();\r\n        }\r\n        else {\r\n            $select." +
"combobox(\"readonly\", false);\r\n        }\r\n        ");

WriteLiteral("\r\n    }\r\n\r\n    $(\"#btnClose\", $(\"#TaskInfoPostApprove\")).click(function() {\r\n    " +
"    window.top.$(\"#");

            
            #line 300 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                  Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\").dialog(\"close\");\r\n    });\r\n\r\n    $(\"#btnNext\", $(\"#TaskInfoPostApprove\")).clic" +
"k(function () {\r\n\r\n        //判断是否有文件\r\n        var result=false;\r\n        var roo" +
"tNode=$(\"#TaskInfoPostApprove_files\").treegrid(\"getRoots\");\r\n        if (rootNod" +
"e==null||rootNode.length==0) {\r\n            JQ.dialog.show(\"没有可提交的文件！！！\");\r\n    " +
"        return false;\r\n        }\r\n        //仅判断是否有文件\r\n        $.each(rootNode,fu" +
"nction (i,e) {\r\n            if (result) {\r\n                return false;\r\n      " +
"      }\r\n            if (e.AttachExt!=\".\") {\r\n                result=true;\r\n    " +
"            return false;\r\n            }\r\n            var childNodes=$(\"#TaskInf" +
"oPostApprove_files\").treegrid(\"getChildren\",e.AttachID);\r\n            if (childN" +
"odes!=null&&childNodes.length>0) {\r\n                $.each(childNodes,function(i" +
"c,ec){\r\n                    if (ec.AttachExt!=\".\") {\r\n                        re" +
"sult=true;\r\n                        return false;\r\n                    }\r\n      " +
"          })\r\n            }\r\n        })\r\n\r\n        if (!result) {\r\n            J" +
"Q.dialog.show(\"没有可提交的文件！！！\");\r\n            return false;\r\n        }\r\n\r\n        v" +
"ar addRow=[];\r\n        $.each(rootNode,function (i,e) {\r\n            addRow.push" +
"(e.AttachID);\r\n            var childNodes=$(\"#TaskInfoPostApprove_files\").treegr" +
"id(\"getChildren\",e.AttachID);\r\n            if (childNodes!=null&&childNodes.leng" +
"th>0) {\r\n                $.each(childNodes,function(ic,ec){\r\n                   " +
" addRow.push(ec.AttachID);\r\n                })\r\n            }\r\n        });\r\n\r\n  " +
"      //JQ.dialog.confirm(\'你确定要提交下一步吗?\',function () {\r\n        //post 备注数据\r\n    " +
"    var info = JQ.tools.findDialogInfo();\r\n        if (typeof(window[\"DesignInfo" +
"Save_\"+info.parentid])==\"function\") {\r\n            var PostResult=window[\"Design" +
"InfoSave_\"+info.parentid]();\r\n            if(!PostResult)  return false;\r\n      " +
"  }\r\n\r\n        //post 更新校审意见数据\r\n        if (typeof(window[\"NextDesignPost_\" + in" +
"fo.parentid])==\"function\") {\r\n            var PostResult=window[\"NextDesignPost_" +
"\" + info.parentid](addRow.join(\',\'));\r\n            //更新校审 状态\r\n            if (ty" +
"peof(window[\"FormCheckRefresh_\" + info.parentid])==\"function\") {\r\n              " +
"  window[\"FormCheckRefresh_\" + info.parentid]();\r\n            }\r\n            if(" +
"!PostResult)  return false;\r\n        }\r\n\r\n        // 获取选人的值\r\n        JQ.tools.aj" +
"ax({\r\n            url: \'");

            
            #line 368 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
             Write(Url.Action("TaskInfoSavePostApprove"));

            
            #line default
            #line hidden
WriteLiteral("?TaskId=\' + ");

            
            #line 368 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                               Write(ViewBag.TaskModel.Id);

            
            #line default
            #line hidden
WriteLiteral(@",
            data: {
                AttachIds:addRow.join(','),
                SpecPlanData:JSON.stringify(GetEmpValue($(""#FlowNodeView""))),
            },
            succesCollBack: function () {
                if (typeof(window.top[""_DialogCallback_");

            
            #line 374 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                  Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\"]) == \"function\") {\r\n                    //console.log(\"_DialogCallback_");

            
            #line 375 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                              Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\");\r\n                    window.top[\"_DialogCallback_");

            
            #line 376 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                           Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\"]();\r\n                    window.top.$(\"#");

            
            #line 377 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                              Write(ViewBag._DialogId);

            
            #line default
            #line hidden
WriteLiteral("\").dialog(\"close\");\r\n                }\r\n            }\r\n        });\r\n        //});" +
"\r\n    });\r\n\r\n    function GetEmpValue($div) {\r\n        var jsonData=JSON.parse(\'" +
"");

            
            #line 385 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                            Write(Html.Raw(ViewBag.TaskModel.TaskFlowModelJson));

            
            #line default
            #line hidden
WriteLiteral(@"').root.item;
        //var NodeJson = Node.TaskFlowModelJson;
        //var jsonData=JSON.parse(NodeJson).hasOwnProperty(""root"")?(JSON.parse(NodeJson).root.item):JSON.parse(NodeJson);
        if ($div&&jsonData.length>0) {
            $.each(jsonData,function(i,e){
                $thisselect=$div.find(""select[id$='_""+e.ID+""']"");
                e.FlowNodeEmpID=$thisselect.combobox(""getValue"")==null?0:$thisselect.combobox(""getValue"");
                e.FlowNodeEmpName=$thisselect.combobox(""getText"")==null?0:$thisselect.combobox(""getText"");
                if (e.FlowNodeStatus == 0) {
                    // 节点状态: 0 未安排（灰） 1 已安排（黄） 2 进行中（绿色） 3 完成（蓝色） 4 停止（红色） 5 回退（橙色）
                    if (Node.TaskSpecId != 0 && Node.TaskLevelType != ");

            
            #line 395 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                                  Write((int)DBSql.Design.TaskLevelType.层级父);

            
            #line default
            #line hidden
WriteLiteral(" && e.FlowNodeTypeID == \"");

            
            #line 395 "..\..\Views\DesTask\TaskInfoPostApprove.cshtml"
                                                                                                                                 Write((int)DataModel.NodeType.设计);

            
            #line default
            #line hidden
WriteLiteral("\") {\r\n                        e.FlowNodeStatus = 2;\r\n                    } else {" +
"\r\n                        e.FlowNodeStatus = 1;\r\n                    }\r\n        " +
"        }\r\n            })\r\n        }\r\n        return jsonData;\r\n    }\r\n</script>" +
"\r\n");

        }
    }
}
#pragma warning restore 1591