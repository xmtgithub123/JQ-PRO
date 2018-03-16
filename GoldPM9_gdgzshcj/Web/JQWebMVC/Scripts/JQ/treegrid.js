var JQ = JQ || {};
$.fn.treegrid.defaults.view.onLoadSuccess = function (datas, options, grid) {
    //设置为加载完成
    this.setAttribute("remoteloaded", "1");
    //加载进度条
    JQ.Flow.loadFlowProgress.call(this, datas, grid);
    //控制表单中表格的可编辑状态
    options.onFlowLoadSuccess && options.onFlowLoadSuccess.call(this, grid);
}
JQ.treegrid = {
    treegrid: function (parames) {
        parames = $.extend({
            JQID: null,
            idField: 'id',
            treeField: 'text',
            url: null,
            pagination: false,//是否分页
            rownumbers: true,//是否显示行号
            fit: true,//自否自适应
            striped: true, //奇偶行是否区分
            JQEnableFilter: false,//是否启用表头字段搜索
            checkOnSelect: false,//是否包含check
            singleSelect: true,//是否单选
            toolbar: null,//工具栏的容器ID
            border: 0,//边框
            pageList: [5, 10, 15, 20, 30, 50, 100, 200],
            pageSize: 20,
        }, parames);
        JQ.datagrid.genderToolButton(parames);
        var loadSuccess = parames.onLoadSuccess,
            beforeLoad = parames.onBeforeLoad,
            $dg = JQ.tools.findCurControl(parames.JQID);//$("#" + parames.JQID);
        parames.onLoadSuccess = function (row, data) {//加载成功事件
            if (JQ.tools.isExitsFunction(loadSuccess)) {//如果有自定义的先运行自定义的函数
                loadSuccess(row, data);
            }
            JQ.treegrid.setCellFiles(parames);

            //var groupArr = JQ.treegrid.getGroupFileds({ id: parames.JQID });
            //if (JQ.tools.isNotEmpty(groupArr)) {//如果有合并单元格属性执行该函数
            //    $dg.datagrid("autoMergeCells", groupArr);
            //}
            //$("[JQPanel='linkAjax']").find("a[isAjax='true']").click(function (event) {
            //    var url = $(this).attr('url');
            //    if (JQ.tools.isNotEmpty(url)) {
            //        JQ.tools.ajax({
            //            url: url,
            //            succesCollBack: function () {
            //                if (JQ.tools.isNotEmpty(parames.JQID)) {
            //                    switch (parames.JQLoadingType) {
            //                        case "treegrid":
            //                            $('#' + parames.JQID).treegrid('reload');
            //                            break;
            //                        default:
            //                            $('#' + parames.JQID).datagrid('reload');
            //                            break;
            //                    }
            //                }
            //            }
            //        });
            //        event.stopPropagation();
            //    }
            //});//如果有ajaxA链接注册事件
        }
        parames.onBeforeLoad = function (row, param) {//请求之前事件
            if (JQ.tools.isExitsFunction(beforeLoad)) {//如果有自定义的先运行自定义的函数
                beforeLoad(param);
            }
            JQ.datagrid.selectFiled({ $dg: $dg, arr: parames.JQFields, queryParams: param, loadingType: 'treegrid' });
        }
        $dg.treegrid(parames);
        //if (parames.JQEnableFilter) {
        //    $dg.treegrid('enableFilter', []);
        //}
    },

    //设置附件
    setCellFiles: function (parames) {
        parames = $.extend({
            JQID: null,
            JQPrimaryID: null,


        }, parames);
        var $dg = JQ.tools.findCurControl(parames.JQID);
        var fcol = $dg.treegrid("options").frozenColumns,
            columns = $dg.treegrid("options").columns,
            arr = new Array(),
            isFileds = false,
            AttachRefTable = null,
            backJson = null,
            $div = null,
            ids = null,
            rows = $dg.treegrid("getData");
        var rootRows = [];

        if (!isFileds && JQ.tools.isNotEmpty(columns)) {
            $(columns).each(function (index) {
                for (var i = 0; i < columns[index].length; i++) {
                    if (columns[index][i].field == 'JQFiles' && JQ.tools.isNotEmpty(columns[index][i].JQRefTable)) {
                        isFileds = true;
                        AttachRefTable = columns[index][i].JQRefTable;
                        break;
                    }
                }
            });
        }
        if (!isFileds && JQ.tools.isNotEmpty(fcol)) {
            $(fcol).each(function (index) {
                for (var i = 0; i < fcol[index].length; i++) {
                    if (fcol[index][i].field == 'JQFiles' && JQ.tools.isNotEmpty(fcol[index][i].JQRefTable)) {
                        isFileds = true;
                        AttachRefTable = fcol[index][i].JQRefTable;
                        break;
                    }
                }
            });
        }
        if (isFileds) {
            if (!JQ.tools.isNotEmpty(AttachRefTable)) {
                alert('未设置AttachRefTable的值!!!');
                return;
            }
            if (parames.JQPrimaryID == null) {
                alert('未设置JQPrimaryID的值,附件无法正常显示!!!');
                return;
            }
            getrootRows(rows);//重新遍历获取 子父节点
            rows = rootRows;

            var url = window.top.rootPath;
            if (opener != null) {
                url = opener.location.href;
            }
            ids = JQ.tools.arrParseStr(rows, ',', parames.JQPrimaryID);
            JQ.tools.ajax({
                doBackResult: false,
                data: { AttachRefTable: AttachRefTable, AttachRefIDs: ids },
                //url: window.top.rootPath + 'Core/usercontrol/jsonByRefIDs',//获取的地址为完整地址，会出现404错误
                url: url + 'Core/usercontrol/jsonByRefIDs',//获取来源地址

                succesCollBack: function (back) {

                    backJson = back;
                    for (var i = 0; i < rows.length; i++) {
                        var pid = rows[i][parames.JQPrimaryID];
                        var arr = JQ.tools.arrFind(backJson, 'AttachRefID', pid);

                        try {
                            $div = $("tr[node-id='" + pid + "'] td[field='JQFiles']").find("div");
                        } catch (e) {
                            $div = null;
                        }
                        if ($div != null && $div.size() == 1) {
                            if (JQ.tools.isNotEmpty(arr)) {
                                $div.html("<a refID='" + pid + "' JQPanel='filesDialog' class='fa fa-cloud-download'>" + arr.length + "</a>");
                            }
                            else {
                                $div.html("<span style='color:gray'>无</span>");                            
                                if ($("#IsShowFile").val()=="1")
                                    $div.html("");
                            }
                        }
                    };
                    $("a[JQPanel='filesDialog']").each(function () {
                        var $this = $(this),
                            refID = $this.attr("refID");
                        $(this).tooltip({
                            content: $(genderStr(refID)),
                            deltaY: 5,
                            onShow: function () {
                                var t = $(this);
                                t.tooltip('tip').css({
                                    backgroundColor: '#f6f6f6',
                                    borderColor: '#0092fc',
                                    padding: 0
                                });
                                t.tooltip('tip').unbind().bind('mouseenter', function () {
                                    t.tooltip('show');
                                }).bind('mouseleave', function () {
                                    t.tooltip('hide');
                                });
                            }
                        })
                    });
                }
            });
        }

        function genderStr(pid) {
            var arr = JQ.tools.arrFind(backJson, 'AttachRefID', pid);
            var str = '<div style="max-height:200px;overflow-y:auto;overflow-x:hidden;"><table class="table">';
            str += "<thead>";
            if (JQ.tools.isNotEmpty(arr)) {
                for (var i = 0; i < arr.length; i++) {
                    str += '<tr style="text-align:left;">';
                    str += '<td><a class="fa fa-book" target="_blank" href="' + arr[i].UrL + '"> ' + arr[i].FileName + '</a></td>';
                    str += '</tr>';
                }
            }
            str += "</thead>";
            str += '</table></div>';
            return str;

        }


        function getrootRows(rows) {

            $.each(rows, function (i, e) {
                rootRows.push(e);
                if (e.children != undefined && e.children.length != 0) {
                    getrootRows(e.children);
                }
            })
        }
    },

};