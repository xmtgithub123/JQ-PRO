var JQ = JQ || {};
//金曲grid
$.fn.datagrid.defaults.view.onLoadSuccess = function (datas, options, grid) {
    //设置为加载完成
    this.setAttribute("remoteloaded", "1");
    //加载进度条
    JQ.Flow.loadFlowProgress.call(this, datas, grid);

    //控制表单中表格的可编辑状态
    options.onFlowLoadSuccess && options.onFlowLoadSuccess.call(this, grid);
}
JQ.datagrid = {
    //数据表格
    datagrid: function (parames) {
        parames = $.extend({
            JQID: null,
            JQSearch: null,//配置表格搜索
            JQIsSearch: false,//是否加载数据之前就获取搜索参数值，可在搜索控件中添加默认值
            JQEnableFilter: false,//是否启用表头字段搜索
            url: null,
            nowrap: false,//当true不换行。默认true
            pagination: true,//是否分页
            rownumbers: true,//是否显示行号
            fit: true,//自否自适应
            fitColumns: false,
            striped: true, //奇偶行是否区分
            checkOnSelect: false,//是否包含check
            singleSelect: false,//是否单选
            toolbar: null,//工具栏的容器ID
            border: 0,//边框
            pageList: [5, 10, 15, 20, 30, 50, 100, 200],
            pageSize: 20
        }, parames);
        //个人设置默认分页数
        if (window.top.window.PageSize != undefined) {
            parames.pageSize = window.top.PageSize.toInt();
            parames.pageList = Enumerable.From(parames.pageList).Union(parames.pageSize).Distinct().ToArray();
        }
        if (parames.JQIsSearch) {
            parames.queryParams = JQ.datagrid.getSearchJson(parames);
        }
        JQ.datagrid.genderToolButton(parames);
        var loadSuccess = parames.onLoadSuccess,
            beforeLoad = parames.onBeforeLoad,
            $dg = JQ.tools.findCurControl(parames.JQID);
        parames.onLoadSuccess = function (rows, data) {//加载成功事件
            if (JQ.tools.isExitsFunction(loadSuccess)) {//如果有自定义的先运行自定义的函数
                loadSuccess(rows, data);
            }
            JQ.datagrid.setCellFiles(parames);
            JQ.datagrid.mergeCells(parames);//如果有合并单元格属性执行该函数,必须放在setCellFiles方法之后(setCellFiles方法会更新rows倒置合并被还原)
        }
        parames.onBeforeLoad = function (param) {//请求之前事件
            if (JQ.tools.isExitsFunction(beforeLoad)) {//如果有自定义的先运行自定义的函数
                beforeLoad(param);
            }
            JQ.datagrid.selectFiled({ $dg: $dg, arr: parames.JQFields, queryParams: param });
        }
        parames.onSortColumn = function (sort, order) {
            var dg = JQ.tools.findCurControl(parames.JQID);
            dg.attr("sortName", sort);
            dg.attr("sortOrder", order);
        }
        $dg.datagrid(parames);
        //if (parames.JQEnableFilter) {
        //    $dg.datagrid('enableFilter', []);
        //}
        JQ.datagrid.doCellTip(parames);
        if (JQ.tools.isJson(parames.JQSearch)) {
            parames.JQSearch.dgID = parames.JQID;
            JQ.textbox.search(parames.JQSearch);
        }
    },

    //单元格显示
    doCellTip: function (parames) {
        parames = $.extend({
            JQID: null,
            onlyShowInterrupt: true
        }, parames);
        var $dg = JQ.tools.findCurControl(parames.JQID);
        $dg.datagrid('doCellTip', {
            onlyShowInterrupt: parames.onlyShowInterrupt,
            position: 'bottom',
            maxWidth: '350px',
            tipStyler: {
                backgroundColor: '#ffffff',
                borderColor: '#0092fc',
                padding: '10px',
                color: '#5c5c5c',
                'font-size': '9pt'
            }
        });
    },

    //合并单元格
    mergeCells: function (parames) {

        parames = $.extend({
            JQID: null
        }, parames);
        var $dg = JQ.tools.findCurControl(parames.JQID),
            JQMergeCells,
            rows = $dg.datagrid("getRows");;
        try {
            if ($dg.datagrid("options").JQMergeCells) {
                JQMergeCells = $dg.datagrid("options").JQMergeCells;
            } else {
                if (parames.JQMergeCells) {
                    JQMergeCells = parames.JQMergeCells;
                }
            }
        }
        catch (e) {
            JQMergeCells = null;
        }
        if (JQ.tools.isJson(JQMergeCells)) {
            if (JQ.tools.isNotEmpty(JQMergeCells.fields)) {
                if (JQ.tools.isNotEmpty(JQMergeCells.Only)) {
                    var merges = [];
                    for (var i = 0; i < rows.length; i++) {
                        if (i == 0) {
                            merges.push({ index: i, rowspan: 1, val: rows[i][JQMergeCells.Only] });
                        }
                        else {
                            if (merges[merges.length - 1].val == rows[i][JQMergeCells.Only]) {
                                merges[merges.length - 1].rowspan += 1;
                            }
                            else {
                                merges.push({ index: i, rowspan: 1, val: rows[i][JQMergeCells.Only] });
                            }
                        }
                    }
                    for (var i = 0; i < merges.length; i++) {
                        if (merges[i].rowspan > 1) {
                            for (var j = 0; j < JQMergeCells.fields.length; j++) {
                                $dg.datagrid('mergeCells', {
                                    index: merges[i].index,
                                    rowspan: merges[i].rowspan,
                                    field: JQMergeCells.fields[j]
                                });
                            }
                        }
                    }
                }
                else {
                    $dg.datagrid("autoMergeCells", JQMergeCells.fields);
                }
            }
        }
    },

    //设置附件
    setCellFiles: function (parames) {
        parames = $.extend({
            JQID: null,
            JQPrimaryID: null
        }, parames);
        var $dg = JQ.tools.findCurControl(parames.JQID);
        var fcol = $dg.datagrid("options").frozenColumns,
            columns = $dg.datagrid("options").columns,
            arr = new Array(),
            isFileds = false,
            AttachRefTable = null,
            backJson = null,
            $div = null,
            ids = null,
            rows = $dg.datagrid("getRows");

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
                            $div = $("td[field='JQFiles']").eq(i + 1).find("div");
                        } catch (e) {
                            $div = null;
                        }
                        if ($div != null && $div.size() == 1) {
                            if (JQ.tools.isNotEmpty(arr)) {
                                $div.html("<a refID='" + pid + "' JQPanel='filesDialog' class='fa fa-cloud-download'>" + arr.length + "</a>");
                            }
                            else {
                                $div.html("<span style='color:gray'>无</span>");
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
    },

    //得到搜索json值
    getSearchJson: function (parames) {
        parames = $.extend({
            dgID: null,
            loadingType: 'datagrid',
            queryParams: null,
            $panel: null,
        }, parames);
        var queryInfo = { Filter: [] },
            $control;   //所有查询条件
        if (parames.$panel && parames.$panel != null && parames.$panel.size() == 1) {
            $control = parames.$panel.find("[JQWhereOptions]");
        }
        else {
            $control = $("[JQWhereOptions]");
        }
        $control.each(function (a, b) {
            var $c = $(this),
                queryOpt = $c.attr("JQWhereOptions"),
                val,
                arr = new Array();

            switch (this.tagName) {
                case "INPUT":
                    if (this.className.indexOf("easyui") > -1) {
                        val = $c.textbox("getValue");
                    }
                    else {
                        val = $c.val();
                    }
                    break;
                case "SELECT":
                    if (this.className.indexOf("combotree") > -1) {
                        //val = JQ.combotree.geCheckedVals({
                        //    treeID: this.id
                        //});
                        var list = [];
                        var s = '';
                        list = $(this).combotree('getValues');
                        for (var i = 0; i < list.length; i++) {
                            if (s != '') s += ',';
                            s += list[i];
                        }
                        val = s;
                    }
                    else if (this.className.indexOf("combogrid") > -1) {
                        var idField = $c.combogrid("options").idField;
                        var sels = $c.combogrid('grid').datagrid('getSelections');
                        if (sels && sels != null && sels.length > 0) {
                            val = '';
                            for (var i in sels) {
                                val += ((i > 0 ? ',' : '') + sels[i][idField]);
                            }
                        }
                    }
                    else if (this.className.indexOf("easyui-combobox") > -1) {
                        val = $c.combobox("getValues");
                        if (val && val != null && val.length > 0) {
                            val = val.join(",")
                        }
                        else {
                            val = "";
                        }
                    }
                    else if (this.className.indexOf("combobox-f combo-f textbox-f") > -1) {
                        val = $c.combobox("getValues");
                        if (val && val != null && val.length > 0) {
                            val = val.join(",")
                        }
                        else {
                            val = "";
                        }
                    }
                    else {
                        val = $c.val();
                    }
                    break;
            }
            if (queryOpt && val && val.length > 0) {
                queryOpt = eval("(" + queryOpt + ")");
                if (JQ.tools.isJson(queryOpt)) {
                    arr.push(queryOpt)
                }
                else if (JQ.tools.isArray(queryOpt)) {
                    arr = queryOpt;
                }
                else {
                    arr == null;
                }
                if (JQ.tools.isArray(arr)) {
                    for (var i = 0; i < arr.length; i++) {
                        arr[i].Value = val;
                    }
                }
                queryInfo.Filter.push({ isGroup: false, list: arr });
            }
        });
        if (!JQ.tools.isJson(parames.queryParams)) {
            parames.queryParams = {};
        }
        parames.queryParams.queryInfo = JSON.stringify(queryInfo.Filter);

        return parames.queryParams;
    },

    //搜索数据表格
    searchGrid: function (parames) {
        parames = $.extend({
            dgID: null,
            $dg: null,
            loadingType: 'datagrid',
            queryParams: null,
            $panel: null,
            setParamsFunc: function () {

            }
        }, parames);
        var json = JQ.datagrid.getSearchJson(parames);
        if (parames.setParamsFunc && typeof parames.setParamsFunc == "function") {
            parames.setParamsFunc(json);
        }
        if (JQ.tools.isNotEmpty(parames.dgID)) {
            parames.$dg = JQ.tools.findCurControl(parames.dgID);//$('#' + parames.dgID);
        }
        switch (parames.loadingType) {
            case 'datagrid':
                parames.$dg.datagrid('load', json);
                break;
            case 'treegrid':
                parames.$dg.treegrid('load', json);
                break;
        }

    },

    //为列表拼装需要查询的字段
    selectFiled: function (parames) {
        parames = $.extend({
            $dg: null,
            arr: null,
            queryParams: null,
            loadingType: 'datagrid'
        }, parames);
        var arr = new Array(), option;
        if (parames.$dg == null || parames.$dg.size() != 1) {
            alert("无法获取$dg!!!");
        }
        switch (parames.loadingType) {
            case 'datagrid':
                option = parames.$dg.datagrid('options');
                break;
            case 'treegrid':
                option = parames.$dg.treegrid('options');
                break;
        }

        if (JQ.tools.isJson(option)) {
            if (JQ.tools.isNotEmpty(parames.arr)) {
                for (var i = 0; i < parames.arr.length; i++) {
                    arr.push(parames.arr[i]);
                }
            }
            if (JQ.tools.isNotEmpty(option.columns)) {
                var columns = option.columns;
                $(columns).each(function (index) {
                    for (var i = 0; i < columns[index].length; i++) {
                        var field = columns[index][i]["field"],
                            bool = columns[index][i]["JQIsExclude"],
                            JQfield = columns[index][i]["JQfield"];
                        if (!JQ.tools.isNotEmpty(JQfield)) {
                            JQfield = field;
                        }
                        if (JQ.tools.isNotEmpty(JQfield) && !bool) {
                            if (arr.indexOf(JQfield) <= -1) {
                                arr.push(JQfield);
                            }
                        }
                    }
                });
            }
            if (JQ.tools.isNotEmpty(option.frozenColumns)) {
                var frozenColumns = option.frozenColumns;
                $(frozenColumns).each(function (index) {
                    for (var i = 0; i < frozenColumns[index].length; i++) {
                        var field = frozenColumns[index][i]["field"],
                            bool = frozenColumns[index][i]["JQIsExclude"],
                            JQfield = frozenColumns[index][i]["JQfield"];
                        if (!JQ.tools.isNotEmpty(JQfield)) {
                            JQfield = field;
                        }
                        if (JQ.tools.isNotEmpty(JQfield) && !bool) {
                            if (arr.indexOf(JQfield) <= -1) {
                                arr.push(JQfield);
                            }
                        }
                    }
                });
            };
            if (!JQ.tools.isJson(parames.queryParams)) {
                parames.queryParams = {
                };
            }
            parames.queryParams.fields = arr;
        }
        else {
            alert("无法获取option或columns!!!");
        }
    },

    //工具栏的编辑
    toolbarEdit: function (parames) {
        parames = $.extend({
            JQLoadingType: '',
            JQID: '',
            JQPrimaryID: '',
            OnJQEditClick: null
        }, parames);
        parames.JQRows = JQ.datagrid.getSelctionsRows(parames);

        if (!JQ.tools.isArray(parames.JQRows)) {
            JQ.dialog.warning('"未选择数据行!!!"');
            return false;
        }
        if (parames.JQRows[0].hasOwnProperty('RowNoEdit')) {
            JQ.dialog.warning('该行无法编辑，想要编辑此行请删除该行中的RowNoEdit属性！！！');
            return false;
        }

        if (parames.url.indexOf("?") <= -1) {
            var lastRow = parames.JQRows.length == 0 ? parames.JQRows[0] : parames.JQRows[parames.JQRows.length - 1];
            parames.url += '?id=' + lastRow[parames.JQPrimaryID];
        }
        else {
            var lastRow = parames.JQRows.length == 0 ? parames.JQRows[0] : parames.JQRows[parames.JQRows.length - 1];
            parames.url += '&id=' + lastRow[parames.JQPrimaryID];
        }
        if (JQ.tools.isJson(parames.OnJQEditClick)) {
            parames.JQField = parames.OnJQEditClick.JQField;
            if (JQ.tools.isArray(parames.JQField)) {
                var retrunJson = JQ.datagrid.getSelectionsValue(parames);
                for (var i in retrunJson[0]) {
                    parames.url += "&" + i + "=" + retrunJson[i]
                }
            };
            if (JQ.tools.isExitsFunction(parames.OnJQEditClick.JQCollBack)) {
                funtionJson = parames.OnJQEditClick.JQCollBack();
                if (JQ.tools.isArray(funtionJson)) {
                    for (var i = 0; i < funtionJson.length; i++) {
                        parames.url += "&" + funtionJson[i].key + "=" + funtionJson[i].val;
                    }
                }
            };
        }

        JQ.dialog.dialog(parames)
    },

    //工具栏的删除
    toolbarDel: function (parames) {
        parames = $.extend({
            JQLoadingType: '',
            JQID: '',
            JQPrimaryID: '',
            JQDelUrl: ''
        }, parames);
        var row = JQ.datagrid.getSelctionsRows(parames),
            $dg = JQ.tools.findCurControl(parames.JQID);
        if (row.length <= 0) {
            window.top.JQ.dialog.warning('您必须选择至少一项进行删除！！！');
        }
        else {
            if (row[0].hasOwnProperty('RowNoDel')) {
                window.top.JQ.dialog.warning('该行不能被删除~');
            }
            else {
                window.top.JQ.dialog.confirm("您是否确定要删除该数据？", function () {
                    var parm;
                    $.each(row, function (i, n) {
                        if (i == 0) {
                            parm = "id=" + n[parames.JQPrimaryID];

                        } else {
                            parm += "&id=" + n[parames.JQPrimaryID];
                        }
                    });
                    JQ.tools.ajax({
                        url: parames.JQDelUrl,
                        data: parm,
                        succesCollBack: function () {
                            switch (parames.JQLoadingType) {
                                case "treegrid":
                                    $dg.treegrid('reload');
                                    break;
                                default:
                                    $dg.datagrid('reload');
                                    break;
                            }
                        }

                    });
                }, null)
            }

        }
    },

    //工具栏的高级搜索
    toolbarMoreSearch: function (parames) {
        parames = $.extend({
            JQLoadingType: '',
            JQID: ''
        }, parames);
        var info = JQ.tools.findDialogInfo(),
            $c,
            $d;
        if (JQ.tools.isJson(info) && JQ.tools.isNotEmpty(info.divid)) {
            $c = info.$div.find(".moreSearchPanel");
            $d = info.$div.find("#" + parames.JQID);
        }
        else {
            $c = $(".moreSearchPanel");
            $d = $('#' + parames.JQID);
        }
        if ($c.is(":hidden")) {
            $c.show(500, function () {
                switch (parames.JQLoadingType) {
                    case "treegrid":
                        $d.treegrid('resize');
                        break;
                    default:
                        $d.datagrid('resize');
                        break;
                }
            });
        }
        else {
            $c.hide(500, function () {
                switch (parames.JQLoadingType) {
                    case "treegrid":
                        $d.treegrid('resize');
                        break;
                    default:
                        $d.datagrid('resize');
                        break;
                }
            });
        }
    },

    //刷新表格数据
    refdatagrid: function (parames) {
        parames = $.extend({
            dgID: null,
            loadingType: null,
            divid: null,
            parentid: null,
            iframeID: null
        }, parames);
        if (JQ.tools.isNotEmpty(parames.dgID)) {
            var $dg;
            if (JQ.tools.isNotEmpty(parames.iframeID)) {
                $dg = window.top.document.getElementById(parames.iframeID).contentWindow.$('#' + parames.dgID);
            }
            else if (JQ.tools.isNotEmpty(parames.parentid)) {
                $dg = window.top.$("#" + parames.parentid).find('#' + parames.dgID);
            }
            else {
                $dg = window.top.$('#' + parames.dgID);
            }
            if ($dg && $dg != null && $dg.size() == 1) {
                switch (parames.loadingType) {
                    case "treegrid":
                        $dg.treegrid('reload');
                        break;
                    default:
                        $dg.datagrid('reload');
                        break;
                }
            }
            else {
                // 暂时先注释alert，影响用户体验
                //alert('无法获取刷新源!!!');
                console.log('无法获取刷新源!!!');
            }
        }
    },

    //无参数刷新表格数据
    autoRefdatagrid: function () {
        var parames = JQ.tools.findDialogInfo();
        JQ.datagrid.refdatagrid(parames);
    },

    //生成toolBar的按钮
    genderToolButton: function (parames) {
        parames = $.extend({
            JQButtonTypes: [],
            JQAddUrl: '',
            OnJQAddClick: null,
            JQEditUrl: '',
            JQDelUrl: '',
            JQPrimaryID: 'id',
            JQID: 'dgid',
            JQDialogTitle: '弹出明细',
            JQWidth: '650',
            JQHeight: '85%',
            JQLoadingType: 'datagrid',
            JQButtonPanleID: 'toolbarPanel',
            JQCustomSearch: null,
            onClose: null
        }, parames);
        var info = JQ.tools.findDialogInfo(),
            panel,
            $dg,
            genderid;
        if (JQ.tools.isJson(info) && JQ.tools.isNotEmpty(info.divid)) {
            panel = info.$div.find("[JQPanel='" + parames.JQButtonPanleID + "']");
            $dg = info.$div.find("#" + parames.JQID);
        }
        else {
            panel = $("[JQPanel='" + parames.JQButtonPanleID + "']");
            $dg = $("#" + parames.JQID);
        }
        //var $dg = $("#" + parames.JQID);
        if ((parames.JQButtonTypes.indexOf('export') > -1 || JQ.tools.isJson(parames.JQCustomSearch)) && panel.find("a[jqpermissionname='export']").length == 0) {
            genderid = JQ.tools.getMathNo();
            $("<div id='" + genderid + "' style='width:120px;'></div>").appendTo(panel);
            var fgu = false;
            if (parames.JQButtonTypes.indexOf('export') > -1) {
                $("<div>", {
                    text: '导出当前页',
                    iconCls: 'fa fa-gbp',
                    click: function () {
                        JQ.datagrid.selectFiled({ $dg: $dg, arr: parames.JQFields, queryParams: parames });
                        JQ.tools.exportExcel(parames, 0);
                    }
                }).prependTo($("#" + genderid));
                $("<div>", {
                    'class': "menu-sep"
                }).prependTo($("#" + genderid));
                $("<div>", {
                    text: '导出全部页',
                    iconCls: 'fa fa-gbp',
                    click: function () {
                        JQ.datagrid.selectFiled({ $dg: $dg, arr: parames.JQFields, queryParams: parames });
                        JQ.tools.exportExcel(parames, 1);
                    }
                }).prependTo($("#" + genderid));
                fgu = true;
            }
            if (JQ.tools.isNotEmpty(parames.JQCustomSearch)) {
                if (fgu) {
                    $("<div>", {
                        'class': "menu-sep"
                    }).prependTo($("#" + genderid));
                }
                $("<div>", {
                    text: '自定义查询',
                    iconCls: 'fa fa-search',
                    click: function () {
                        JQ.datagrid.customSearch(parames);
                    }
                }).prependTo($("#" + genderid));
            }
            $("<a>", {
                'JQPermissionName': 'export',
                'class': "easyui-menubutton",
                'data-options': "plain:true,menu:'#" + genderid + "'",
                text: '工具'
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('refreshByTree') > -1) {
            $("<a>", {
                'JQPermissionName': 'refresh',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-refresh'",
                text: '刷新',
                click: function () {
                    $dg.treegrid("reload");
                }
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('refreshByGrid') > -1) {
            $("<a>", {
                'JQPermissionName': 'refresh',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-refresh'",
                text: '刷新',
                click: function () {
                    $dg.datagrid("reload");
                }
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('del') > -1 && panel.find("a[jqpermissionname='del']").length == 0) {
            $("<a>", {
                'JQPermissionName': 'del',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-trash'",
                text: '删除',
                click: function () {
                    JQ.datagrid.toolbarDel(parames);
                }
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('edit') > -1 && panel.find("a[jqpermissionname='edit']").length == 0) {
            $("<a>", {
                'JQPermissionName': 'edit',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-pencil'",
                text: '修改',
                click: function () {
                    JQ.datagrid.toolbarEdit({
                        title: "编辑" + parames.JQDialogTitle,
                        url: parames.JQEditUrl,
                        width: parames.JQWidth,
                        height: parames.JQHeight,
                        iconCls: 'fa fa-pencil',
                        JQID: parames.JQID,
                        JQLoadingType: parames.JQLoadingType,
                        JQPrimaryID: parames.JQPrimaryID,
                        OnJQEditClick: parames.OnJQEditClick,
                        onClose: parames.onClose
                    });
                }
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('add') > -1 && panel.find("a[jqpermissionname='add']").length == 0) {
            $("<a>", {
                'JQPermissionName': 'add',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-plus'",
                text: '新增',
                click: function () {
                    var url = parames.JQAddUrl,
                        retrunJson,
                        funtionJson;
                    if (JQ.tools.isJson(parames.OnJQAddClick)) {
                        if (url.indexOf("?") <= -1) {
                            url += '?JQ=JQ';
                        }
                        if (JQ.tools.isArray(parames.OnJQAddClick.JQField)) {
                            parames.JQField = parames.OnJQAddClick.JQField;
                            parames.JQRows = JQ.datagrid.getSelctionsRows(parames);
                            if (!JQ.tools.isArray(parames.JQRows)) {
                                JQ.dialog.warning('"未选择数据行!!!"');
                                return false;
                            }
                            retrunJson = JQ.datagrid.getSelectionsValue(parames);
                            for (var i in retrunJson[0]) {
                                url += "&" + i + "=" + retrunJson[i]
                            }
                        }
                        if (JQ.tools.isExitsFunction(parames.OnJQAddClick.JQCollBack)) {
                            funtionJson = parames.OnJQAddClick.JQCollBack();
                            if (JQ.tools.isArray(funtionJson)) {
                                for (var i = 0; i < funtionJson.length; i++) {
                                    url += "&" + funtionJson[i].key + "=" + funtionJson[i].val;
                                }
                            }
                        };
                    }
                    JQ.dialog.dialog({
                        title: "添加" + parames.JQDialogTitle,
                        url: url,
                        width: parames.JQWidth,
                        height: parames.JQHeight,
                        JQID: parames.JQID,
                        JQLoadingType: parames.JQLoadingType,
                        iconCls: 'fa fa-plus',
                        onClose: parames.onClose
                    });
                }
            }).prependTo(panel);
        }
        if (parames.JQButtonTypes.indexOf('moreSearch') > -1 && panel.find("a[jqpermissionname='moreSearch']").length == 0) {
            $("<a>", {
                'class': "moreSearchButton easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-search-plus'",
                text: '更多',
                click: function () {
                    JQ.datagrid.toolbarMoreSearch({
                        JQID: parames.JQID,
                        JQLoadingType: parames.JQLoadingType
                    });
                }
            }).prependTo(panel);
        }
        $.parser.parse(panel);
    },

    //自定义查询
    customSearch: function (parames) {
        parames = $.extend({
            JQID: '',
            JQLoadingType: 'datagrid',
            JQCustomSearch: null
        }, parames);
        if (!JQ.tools.isNotEmpty(parames.JQCustomSearch)) {
            alert("未发现自定义字段集合")
        }
        if (!JQ.tools.isNotEmpty(parames.JQID)) {
            alert("未发现JQID")
        }
        if (!JQ.tools.isNotEmpty(parames.JQLoadingType)) {
            alert("未发现JQLoadingType")
        }
        JQ.dialog.dialog({
            istoolbar: false,
            iconCls: 'fa fa-search',
            title: '自定义查询条件列表',
            width: '760',
            height: '85%',
            JQLoadingType: parames.JQLoadingType,
            JQID: parames.JQID,
            url: JQ.tools.getRootPath() + '/Core/usercontrol/customSearch?parames=' + JSON.stringify(parames.JQCustomSearch)
        });
    },

    //获取选中的行数据
    getSelctionsRows: function (parames) {
        parames = $.extend({
            JQID: 'dgid',
            JQLoadingType: 'datagrid'
        }, parames);
        var rows,
            $dg = JQ.tools.findCurControl(parames.JQID);
        if (parames.JQLoadingType == 'treegrid') {
            rows = $dg.treegrid('getSelections');
        }
        else {
            rows = $dg.datagrid('getSelections');
        }
        return rows;
    },

    //得到选中行的指定字段值
    getSelectionsValue: function (parames) {
        parames = $.extend({
            JQID: null,
            JQLoadingType: 'datagrid',
            JQRows: null,
            JQField: null
        }, parames);
        if (!JQ.tools.isArray(parames.JQRows)) {
            parames.JQRows = JQ.datagrid.getSelctionsRows(parames);
        }
        var resutlArr = new Array();
        if (JQ.tools.isArray(parames.JQRows)) {
            if (JQ.tools.isArray(parames.JQField)) {
                for (var j = 0; j < parames.JQRows.length; j++) {
                    var resultJson = {};
                    for (var i = 0; i < parames.JQField.length; i++) {
                        var key = parames.JQField[i];
                        var val = parames.JQRows[j][key];
                        resultJson[key] = val;
                    }
                    resutlArr.push(resultJson);
                }
            }
            else {
                resutlArr = parames.JQRows;
            }
        }
        return resutlArr;
    },

    //得到指定行的指定字段值
    getDataRows: function (parames) {
        parames = $.extend({
            JQID: null,
            JQLoadingType: 'datagrid',
            JQField: null,
            JQJson: null
        }, parames);
        var appendArr = new Array(),
            $d = JQ.tools.findCurControl(parames.JQID),//$("#" + parames.JQID),
            rows = $d.datagrid('getRows'),
            resultArr = new Array();
        if (JQ.tools.isJson(parames.JQJson)) {
            if (JQ.tools.isArray(parames.JQJson.index)) {
                for (var i = 0; i < parames.JQJson.index.length; i++) {
                    if (rows.length >= parames.JQJson.index[i]) {
                        appendArr.push(rows[(parames.JQJson.index[i] - 1)]);
                    }
                }
            }
            if (JQ.tools.isJson(parames.JQJson.primarys) && JQ.tools.isNotEmpty(parames.JQJson.primarys.field) && JQ.tools.isArray(parames.JQJson.primarys.ids)) {
                for (var i = 0; i < parames.JQJson.primarys.ids.length; i++) {
                    var s1 = JQ.tools.arrFind(rows, parames.JQJson.primarys.field, parames.JQJson.primarys.ids[i]);
                    if (JQ.tools.isArray(s1)) {
                        for (var j = 0; j < s1.length; j++) {
                            appendArr.push(s1[i]);
                        }
                    }
                }
            }
            if (parames.JQJson.selects) {
                var s2 = JQ.datagrid.getSelctionsRows(parames);
                if (JQ.tools.isArray(s2)) {
                    for (var j = 0; j < s2.length; j++) {
                        appendArr.push(s2[j]);
                    }
                }
            }
        }
        if (JQ.tools.isArray(parames.JQField)) {
            for (var j = 0; j < appendArr.length; j++) {
                var resultJson = {};
                for (var i = 0; i < parames.JQField.length; i++) {
                    var key = parames.JQField[i];
                    var val = appendArr[j][key];
                    resultJson[key] = val;
                }
                resultArr.push(resultJson);
            }
        }
        else {
            resultArr = appendArr;
        }
        return resultArr;
    }
};

//合并单元格扩展
$.extend($.fn.datagrid.methods, {
    autoMergeCells: function (jq, fields) {
        return jq.each(function () {
            var target = $(this);
            if (!fields) {
                fields = target.datagrid("getColumnFields");
            }
            var rows = target.datagrid("getRows");
            var i = 0,
                j = 0,
                temp = {
                };
            for (i; i < rows.length; i++) {
                var row = rows[i];
                j = 0;
                for (j; j < fields.length; j++) {
                    var field = fields[j];
                    var tf = temp[field];
                    if (!tf) {
                        tf = temp[field] = {
                        };
                        tf[row[field]] = [i];
                    } else {
                        var tfv = tf[row[field]];
                        if (tfv) {
                            tfv.push(i);
                        } else {
                            tfv = tf[row[field]] = [i];
                        }
                    }
                }
            }
            $.each(temp, function (field, colunm) {
                $.each(colunm, function () {
                    var group = this;

                    if (group.length > 1) {
                        var before,
                            after,
                            megerIndex = group[0];
                        for (var i = 0; i < group.length; i++) {
                            before = group[i];
                            after = group[i + 1];
                            if (after && (after - before) == 1) {
                                continue;
                            }
                            var rowspan = before - megerIndex + 1;
                            if (rowspan > 1) {
                                target.datagrid('mergeCells', {
                                    index: megerIndex,
                                    field: field,
                                    rowspan: rowspan
                                });
                            }
                            if (after && (after - before) != 1) {
                                megerIndex = after;
                            }
                        }
                    }
                });
            });
        });
    }
});

$.extend($.fn.datagrid.methods, {
    //onlyShowInterrupt	string	是否只有在文字被截断时才显示tip，默认值为false，即所有单元格都显示tip。
    //specialShowFields	Array	需要特殊定义显示的列，比如要求鼠标经过name列时提示standName列(可以是隐藏列)的内容,specialShowFields参数可以传入：[{field:'name',showField:'standName'}]。
    //position	string	tip的位置，可以为top,botom,right,left。
    //minWidth	string	tip的最小宽度(IE7+)。
    //maxWidth	string	tip的最大宽度(IE7+)。
    //width	string	tip的宽度，例如'200px'。
    //tipStyler	object	tip内容的样式，注意要符合jquery css函数的要求。
    //contentStyler	object	整个tip的样式，注意要符合jquery css函数的要求。
    /**
     * 开打提示功能（基于1.3.3+版本）
     * @param {} jq
     * @param {} params 提示消息框的样式
     * @return {}
     */
    doCellTip: function (jq, params) {
        function showTip(showParams, td, e, dg) {
            //无文本，不提示。
            if ($(td).text() == "") return;
            params = params || {
            };
            var options = dg.data('datagrid');
            var styler = 'style="';
            if (showParams.width) {
                styler = styler + "width:" + showParams.width + ";";
            }
            if (showParams.maxWidth) {
                styler = styler + "max-width:" + showParams.maxWidth + ";";
            }
            if (showParams.minWidth) {
                styler = styler + "min-width:" + showParams.minWidth + ";";
            }
            styler = styler + '"';
            showParams.content = '<div class="tipcontent" ' + styler + '>' + showParams.content + '</div>';
            $(td).tooltip({
                content: showParams.content,
                trackMouse: true,
                position: params.position,
                onHide: function () {
                    $(this).tooltip('destroy');
                },
                onShow: function () {
                    var tip = $(this).tooltip('tip');
                    if (showParams.tipStyler) {
                        tip.css(showParams.tipStyler);
                    }
                    if (showParams.contentStyler) {
                        tip.find('div.tipcontent').css(showParams.contentStyler);
                    }
                }
            }).tooltip('show');
        };
        return jq.each(function () {
            var grid = $(this);
            var options = $(this).data('datagrid');
            if (!options.tooltip) {
                var panel = grid.datagrid('getPanel').panel('panel');
                panel.find('.datagrid-body').each(function () {
                    var delegateEle = $(this).find('> div.datagrid-body-inner').length ? $(this).find('> div.datagrid-body-inner')[0] : this;
                    $(delegateEle).undelegate('td', 'mouseover').undelegate('td', 'mouseout').undelegate('td', 'mousemove').delegate('td[field]', {
                        'mouseover': function (e) {
                            //if($(this).attr('field')===undefined) return;
                            if (params.showFields) {
                                var isIn = false, field = this.getAttribute("field");
                                for (var i in params.showFields) {
                                    if (params.showFields[i] == field) {
                                        isIn = true;
                                        break;
                                    }
                                }
                                if (!isIn) {
                                    return;
                                }
                            }
                            var that = this;
                            var setField = null;
                            if (params.specialShowFields && params.specialShowFields.sort) {
                                for (var i = 0; i < params.specialShowFields.length; i++) {
                                    if (params.specialShowFields[i].field == $(this).attr('field')) {
                                        setField = params.specialShowFields[i];
                                    }
                                }
                            }
                            if (setField == null) {
                                options.factContent = $(this).find('>div').clone().css({ 'margin-left': '-5000px', 'width': 'auto', 'display': 'inline', 'position': 'absolute' }).appendTo('body');
                                var factContentWidth = options.factContent.width();
                                params.content = $(this).text().replace(/\n/g, "<br />");
                                if (params.onlyShowInterrupt) {
                                    if (factContentWidth > $(this).width()) {
                                        showTip(params, this, e, grid);
                                    }
                                } else {
                                    showTip(params, this, e, grid);
                                }
                            } else {
                                panel.find('.datagrid-body').each(function () {
                                    var trs = $(this).find('tr[datagrid-row-index="' + $(that).parent().attr('datagrid-row-index') + '"]');
                                    trs.each(function () {
                                        var td = $(this).find('> td[field="' + setField.showField + '"]');
                                        if (td.length) {
                                            params.content = td.text().replace(/\n/g, "<br />");
                                        }
                                    });
                                });
                                showTip(params, this, e, grid);
                            }
                        },
                        'mouseout': function (e) {
                            if (options.factContent) {
                                options.factContent.remove();
                                options.factContent = null;
                            }
                        }
                    });
                });
            }
        });
    },
    /**
     * 关闭消息提示功能（基于1.3.3版本）
     * @param {} jq
     * @return {}
     */
    cancelCellTip: function (jq) {
        return jq.each(function () {
            var data = $(this).data('datagrid');
            if (data.factContent) {
                data.factContent.remove();
                data.factContent = null;
            }
            var panel = $(this).datagrid('getPanel').panel('panel');
            panel.find('.datagrid-body').undelegate('td', 'mouseover').undelegate('td', 'mouseout').undelegate('td', 'mousemove')
        });
    }
});