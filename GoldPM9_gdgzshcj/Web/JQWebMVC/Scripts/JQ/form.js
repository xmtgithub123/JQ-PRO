"use strict"
var JQ = JQ || {};
//初始化form
JQ.form = {
    //初始化提交表单
    submitInit: function (parames) {
        parames = $.extend({
            buttonTypes: []
        }, parames);
        parames = $.extend(parames, JQ.tools.findDialogInfo());
        if (parames.$div.size() <= 0) {
            alert('无法获取弹出框的唯一标识!!!');
            return;
        }
        var $panel = parames.$divPrev;
        if ($panel.attr("loaded")) {
            return;
        }
        $panel.attr("loaded", "1");
        var $dialogP = parames.$div.find("[JQPanel='dialogButtonPanel']");// $("#" +$div.attr("id") + " ");
        if (parames.beforeButtons) {
            for (var i in parames.beforeButtons) {
                $("<a>", {
                    "class": "easyui-linkbutton",
                    "data-options": "plain:true,iconCls:'fa " + parames.beforeButtons[i].icon + "'" + (("disabled" in parames.beforeButtons[i]) ? (",disabled:" + parames.beforeButtons[i].disabled) : "") + "",
                    text: parames.beforeButtons[i].text,
                    click: parames.beforeButtons[i].onClick,
                    id: parames.beforeButtons[i].id
                }).appendTo($panel);
            }
        }
        if (parames.buttonTypes.indexOf('submit') > -1) {
            $("<a>", {
                'JQPermissionName': 'submit',
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-save'",
                text: '保存',
                click: function () {
                    JQ.form.submit(parames);
                }
            }).appendTo($panel);
        };
        if (parames.flow) {
            parames.flow.formID = parames.formid;
            parames.flow.$panel = $panel;
            parames.beforeFormSubmit && (parames.flow.beforeFormSubmit = parames.beforeFormSubmit);
            parames.flow.onSubmited = parames.succesCollBack;
            if (parames.onBeforeSubmit) {
                parames.flow.onBeforeSubmit = parames.onBeforeSubmit
            }
            parames.flow = new JQ.Flow(parames.flow);
        }
        if (parames.buttonTypes.indexOf('close') > -1) {
            $("<a>", {
                'class': "easyui-linkbutton",
                'data-options': "plain:true,iconCls:'fa fa-close'",
                text: '关闭',
                click: function () {
                    JQ.dialog.dialogClose(parames);
                }
            }).appendTo($panel);
        };
        if (parames.buttonTypes.indexOf('exportForm') > -1) {
            var genderid = JQ.tools.getMathNo();
            $("<div id='" + genderid + "' style='width:120px;'></div>").appendTo($panel);
            $("<div>", {
                text: '导出PDF',
                iconCls: 'fa fa-file-pdf-o',
                click: function () {
                    parames.IsWord = false;
                    JQ.form.exportForm(parames);
                }
            }).prependTo($("#" + genderid));
            $("<div>", {
                'class': "menu-sep"
            }).prependTo($("#" + genderid));
            $("<div>", {
                text: '导出WORD',
                iconCls: 'fa fa-file-word-o',
                click: function () {
                    parames.IsWord = true;
                    JQ.form.exportForm(parames);
                }
            }).prependTo($("#" + genderid));
            $("<a>", {
                'class': "easyui-menubutton",
                'data-options': "plain:true,menu:'#" + genderid + "'",
                text: '导出表单'
            }).appendTo($panel);
        };
        if (parames.buttons && parames.buttons.length > 0) {
            for (var i = 0; i < parames.buttons.length; i++) {
                $("<a>", {
                    "class": "easyui-linkbutton",
                    "data-options": "plain:true,iconCls:'fa " + parames.buttons[i].icon + "'",
                    text: parames.buttons[i].text,
                    click: parames.buttons[i].onClick
                }).appendTo($panel);
            }
        };
        if ($dialogP && $dialogP.size() == 1) {
            $panel.append($dialogP.html());
            $dialogP.remove();
        };
        if (JQ.tools.isNotEmpty($panel.html())) {
            $panel.show();
        }
        $.parser.parse($panel);
        if (parames.onInit) {
            parames.onInit($panel);
        }
        if (parames.flow) {
            parames.flow.load();
            delete parames.flow;
        }
        parames.$div.dialog("resize");
    },
    //提交表单
    submit: function (parames) {
        parames = $.extend({
            extend: null,//json格式
            $form: null,
            formid: null,
            succesCollBack: null
        }, parames);
        if (parames.$form == null) {
            parames.$form = JQ.tools.findCurControl(parames.formid); //$("#" + parames.formid);
        }
        if (parames.$form == null || parames.$form.size() != 1) {
            alert('无法获取form对象!!!');
            return;
        }
        window.top.$.messager.progress();
        parames.$form.form('submit', {
            onSubmit: function (p) {
                var isvali = $(this).form('validate');// 返回false终止表单提交
                if (!isvali) {
                    window.top.$.messager.progress('close');
                }
                if (isvali && parames.beforeFormSubmit) {
                    isvali = parames.beforeFormSubmit.call();
                    if (isvali === false) {
                        window.top.$.messager.progress('close');
                    }
                }
                if (JQ.tools.isJson(parames.extend) && parames.extend.length > 0) {
                    for (var i in parames.extend) {
                        p[i] = parames.extend[i];
                    }
                }
                return isvali;
            },
            success: function (data) {
                parames.backData = data;
                JQ.tools.requestBackResult(parames);
                window.top.$.messager.progress('close');
            }
        });
    },

    exportForm: function (parames) {

        ToForm(parames);
        function ToForm(parames) {
            var docName = parames.docName;
            var resultArr = [];
            if (parames.onBefore)//设置导出前设置相关的值
            {
                parames.onBefore(resultArr);
            }
            var $form = JQ.tools.findCurControl(parames.formid);
            var o = {};
            var a = $form.serializeArray();
            var result = [];
            //console.log(a);
            $.each(a, function () {

                if (this.name == "SubQualifiedDirectory") {
                    return;
                }
                if (this.name == "$uplohad$_cache_y12$t1m") {
                    return;
                }
                if ($("#" + this.name).attr("comboname") == undefined) {
                    if (o[this.name] !== undefined) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                }
                else {
                    o[this.name] = $("#" + this.name).combotree("getText");
                }
                result.push({ Key: this.name, Value: (o[this.name] || '') });
            });

            $form.find("[bookmark]").each(function () {

                if ($(this).is("table")) {
                    if ($(this).attr("class") == "datagrid-f")
                        result.push({ Key: $(this).attr("id"), Value: ChangeToTable($("#" + $(this).attr("id"))) });
                }
                else {

                    if ($(this).is("tbody")) {
                        result.push({ Key: this.getAttribute("bookmark"), Value: $.trim($(this).html()) });//table tr
                    } else {
                        result.push({ Key: this.getAttribute("bookmark"), Value: $.trim($(this).text()) });
                    }
                }
            })

            $form.find("[treeGrid]").each(function () {
                if ($(this).attr("class") == "datagrid-f")
                    result.push({ Key: $(this).attr("id"), Value: TreeGridChangeToTable($("#" + $(this).attr("id"))) });
            })

            $form.find("[combotreemark]").each(function () {
                var tagget = this.getAttribute("combotreemark");
                var combotree = $(this).find("select[id='" + tagget + "']");
                var txt = combotree.combotree('getText');
                var array = txt.split(',');
                var returntxt = '';
                for (var i = 0; i < array.length; i++) {
                    if (returntxt != '') returntxt += ',';
                    returntxt += array[i] + " ☑";
                }

                result.push({ Key: tagget, Value: returntxt });
            })

            $form.find("[checkboxmark]").each(function () {
                var tagget = this.getAttribute("checkboxmark");
                var returntxt = "□";
                var t = $("#" + tagget).attr("checked");
                if (t == "checked") {
                    returntxt = "☑";
                }

                result.push({ Key: tagget, Value: returntxt });
            });

            $form.find("[checkboxlistmark]").each(function () {
                var tagget = this.getAttribute("id");
                var returntxt = "□";
                var t = $(this).attr("checked");
                if (t == "checked") {
                    returntxt = "☑";
                }
                result.push({ Key: tagget, Value: returntxt });
            });

            var ExportName = parames.ExportName;
            if (ExportName == undefined) ExportName = "";

            var IsWord = parames.IsWord;
            if (IsWord == undefined) IsWord = true;

            var f = $('<form action="' + window.top.rootPath + 'Core/usercontrol/ToDoc?docName=' + docName + '&ExportName=' + ExportName + '&IsWord=' + IsWord + '" method="post" id="fmByExportDoc"></form>');
            var i = $('<input type="hidden" id="MarkList" name="MarkList" />');
            try {
                if (resultArr.length > 0) {
                    for (var ie = 0; ie < resultArr.length; ie++) {
                        if (resultArr[ie].Key && resultArr[ie].Value) {
                            result.push(resultArr[ie]);
                        }
                    }
                }
                i.val(JSON.stringify(result));
                i.appendTo(f);
                f.appendTo(document.body).submit();
                f.remove();
                if (parames.onAfter)//导出后设置相关的值
                {
                    parames.onAfter();
                }
            }
            catch (e) {
                window.top.$.messager.progress('close');
            }
        }
        function ChangeToTable(printDatagrid) {
            var tableString = '<table cellspacing="0" style="font-size:13px;border-collapse:collapse;width:97%;"">';
            var frozenColumns = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象
            var frozenColumnsField = printDatagrid.datagrid("getColumnFields", true);
            var columns = printDatagrid.datagrid("options").columns;    // 得到columns对象
            var columnsColumnsField = printDatagrid.datagrid("getColumnFields");
            var HeadColumn = [];//所有title中field
            for (var i = 0; i < frozenColumnsField.length; i++) {
                HeadColumn.push(frozenColumnsField[i]);
            }
            for (var i = 0; i < columnsColumnsField.length; i++) {
                HeadColumn.push(columnsColumnsField[i]);
            }
            var nameList = new Array(HeadColumn.length);

            // 载入title
            if (typeof columns != 'undefined' && columns != '') {
                $(columns).each(function (index) {
                    tableString += '\n<tr>';
                    if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                        for (var i = 0; i < frozenColumns[index].length; ++i) {
                            if (!frozenColumns[index][i].hidden) {
                                tableString += '\n<th style="font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;" width="' + frozenColumns[index][i].width + '"';
                                if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                                    tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                                }
                                if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                                    tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                                }
                                if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = frozenColumns[index][i];
                                    }//nameList.push(frozenColumns[index][i]);
                                }
                                tableString += '>' + frozenColumns[0][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    tableString += "<th width='0pt' style='display: none;'></th>";
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = frozenColumns[index][i];
                                    }
                                } else {
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(frozenColumns[index][i].field)] = undefined;
                                    }
                                }
                                //if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                //    nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = undefined;
                                //}
                            }
                        }
                    }
                    for (var i = 0; i < columns[index].length; ++i) {
                        if (!columns[index][i].hidden) {
                            var width = typeof (columns[index][i].exportWidth) == "undefined" ? (typeof (columns[index][i].width) != "undefinded" ? columns[index][i].width : 120) : columns[index][i].exportWidth;
                            tableString += '\n<th style="font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;" width="' + width + '"';
                            if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                                tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                            }
                            if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                                tableString += ' colspan="' + columns[index][i].colspan + '"';
                            }
                            if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = columns[index][i];
                                }
                                //nameList.push(columns[index][i]);
                            }
                            tableString += '>' + columns[index][i].title + '</th>';
                        } else {
                            if (index > 0) {
                                tableString += "<th width='0pt' style='display: none;'></th>";
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = columns[index][i];
                                }
                            } else {
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = undefined;
                                }
                            }
                            //if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                            //    nameList[HeadColumn.indexOf(columns[index][i].field)] = undefined;
                            //}
                        }
                    }
                    tableString += '\n</tr>';
                });
            }
            // 载入内容
            var rows = printDatagrid.datagrid("getRows"); // 这段代码是获取当前页的所有行
            for (var i = 0; i < rows.length; ++i) {

                tableString += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    if (nameList[j] == undefined) {
                        continue;
                    }
                    var e = nameList[j].field.lastIndexOf('_0');
                    if (nameList[j].hidden) {
                        tableString += '<td  style="border:0.5pt solid windowtext;display: none;width=\'0pt\'"></td>';
                    } else {
                        tableString += '\n<td';
                        if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                            tableString += ' style="border:0.5pt solid windowtext;padding:2px;text-align:' + nameList[j].align + ';"';
                        }
                        tableString += '>';
                        if (e + 2 == nameList[j].field.length) {
                            tableString += rows[i][nameList[j].field.substring(0, e)];
                        }
                        else {
                            //var field = rows[i][nameList[j].field] == "1900-01-01T00:00:00" ? "" : rows[i][nameList[j].field]

                            var sValue = rows[i][nameList[j].field] + '';

                            if (sValue.match(R_ISO8601_STR)) {
                                sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                            }

                            tableString += sValue;
                        }
                        tableString += '</td>';
                    }
                }
                tableString += '\n</tr>';
            }
            tableString += '\n</table>';
            return tableString;
        }
        function TreeGridChangeToTable(printDatagrid) {
            var tableString = '<table cellspacing="0" style="font-size:13px;border-collapse:collapse;width:97%;"">';
            var frozenColumns = printDatagrid.treegrid("options").frozenColumns;  // 得到frozenColumns对象
            var frozenColumnsField = printDatagrid.treegrid("getColumnFields", true);
            var columns = printDatagrid.treegrid("options").columns;    // 得到columns对象
            var columnsColumnsField = printDatagrid.treegrid("getColumnFields");
            var HeadColumn = [];//所有title中field
            for (var i = 0; i < frozenColumnsField.length; i++) {
                HeadColumn.push(frozenColumnsField[i]);
            }
            for (var i = 0; i < columnsColumnsField.length; i++) {
                HeadColumn.push(columnsColumnsField[i]);
            }
            var nameList = new Array(HeadColumn.length);

            // 载入title
            if (typeof columns != 'undefined' && columns != '') {
                $(columns).each(function (index) {
                    tableString += '\n<tr>';
                    if (typeof frozenColumns != 'undefined' && typeof frozenColumns[index] != 'undefined') {
                        for (var i = 0; i < frozenColumns[index].length; ++i) {
                            if (!frozenColumns[index][i].hidden) {
                                tableString += '\n<th style="font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;" width="' + frozenColumns[index][i].width + '"';
                                if (typeof frozenColumns[index][i].rowspan != 'undefined' && frozenColumns[index][i].rowspan > 1) {
                                    tableString += ' rowspan="' + frozenColumns[index][i].rowspan + '"';
                                }
                                if (typeof frozenColumns[index][i].colspan != 'undefined' && frozenColumns[index][i].colspan > 1) {
                                    tableString += ' colspan="' + frozenColumns[index][i].colspan + '"';
                                }
                                if (typeof frozenColumns[index][i].field != 'undefined' && frozenColumns[index][i].field != '') {
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = frozenColumns[index][i];
                                    }//nameList.push(frozenColumns[index][i]);
                                }
                                tableString += '>' + frozenColumns[0][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    tableString += "<th width='0pt' style='display: none;'></th>";
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = frozenColumns[index][i];
                                    }
                                } else {
                                    if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(frozenColumns[index][i].field)] = undefined;
                                    }
                                }
                                //if (HeadColumn.indexOf(frozenColumns[index][i].field) > -1) {
                                //    nameList[HeadColumn.indexOf(frozenColumns[index][i].field)] = undefined;
                                //}
                            }
                        }
                    }
                    for (var i = 0; i < columns[index].length; ++i) {
                        if (!columns[index][i].hidden) {
                            var width = typeof (columns[index][i].exportWidth) == "undefined" ? (typeof (columns[index][i].width) != "undefinded" ? columns[index][i].width : 120) : columns[index][i].exportWidth;
                            tableString += '\n<th style="font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;" width="' + width + '"';
                            if (typeof columns[index][i].rowspan != 'undefined' && columns[index][i].rowspan > 1) {
                                tableString += ' rowspan="' + columns[index][i].rowspan + '"';
                            }
                            if (typeof columns[index][i].colspan != 'undefined' && columns[index][i].colspan > 1) {
                                tableString += ' colspan="' + columns[index][i].colspan + '"';
                            }
                            if (typeof columns[index][i].field != 'undefined' && columns[index][i].field != '') {
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = columns[index][i];
                                }
                                //nameList.push(columns[index][i]);
                            }
                            tableString += '>' + columns[index][i].title + '</th>';
                        } else {
                            if (index > 0) {
                                tableString += "<th width='0pt' style='display: none;'></th>";
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = columns[index][i];
                                }
                            } else {
                                if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                                    nameList[HeadColumn.indexOf(columns[index][i].field)] = undefined;
                                }
                            }
                            //if (HeadColumn.indexOf(columns[index][i].field) > -1) {
                            //    nameList[HeadColumn.indexOf(columns[index][i].field)] = undefined;
                            //}
                        }
                    }
                    tableString += '\n</tr>';
                });
            }
            // 载入内容

            var rows = printDatagrid.treegrid("getData"); // 这段代码是获取当前页的所有行

            tableString = GetRowInfo(rows, tableString, nameList);

            tableString += '\n</table>';

            return tableString;
        }

        function GetRowInfo(rows, tableString, nameList)
        {
            for (var i = 0; i < rows.length; ++i) {
                tableString += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    if (nameList[j] == undefined) {
                        continue;
                    }
                    var e = nameList[j].field.lastIndexOf('_0');
                    if (nameList[j].hidden) {
                        tableString += '<td  style="border:0.5pt solid windowtext;display: none;width=\'0pt\'"></td>';
                    } else {
                        tableString += '\n<td';
                        if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                            tableString += ' style="border:0.5pt solid windowtext;padding:2px;text-align:' + nameList[j].align + ';"';
                        }
                        tableString += '>';
                        if (e + 2 == nameList[j].field.length) {
                            tableString += rows[i][nameList[j].field.substring(0, e)];
                        }
                        else {
                            var sValue = rows[i][nameList[j].field] + '';

                            if (sValue.match(R_ISO8601_STR)) {
                                sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                            }

                            tableString += sValue;
                        }
                        tableString += '</td>';
                    }
                }
                tableString += '\n</tr>';
                if (rows[i].children) {
                    var row = rows[i].children;
                    tableString = RecursionInfo(row, tableString, nameList);
                }
            }
            return tableString;
        }
        function RecursionInfo(rows, tableString, nameList) {
            for (var i = 0; i < rows.length; ++i) {
                tableString += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    if (nameList[j] == undefined) {
                        continue;
                    }
                    var e = nameList[j].field.lastIndexOf('_0');
                    if (nameList[j].hidden) {
                        tableString += '<td  style="border:0.5pt solid windowtext;display: none;width=\'0pt\'"></td>';
                    } else {
                        tableString += '\n<td';
                        if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                            tableString += ' style="border:0.5pt solid windowtext;padding:2px;text-align:' + nameList[j].align + ';"';
                        }
                        tableString += '>';
                        if (e + 2 == nameList[j].field.length) {
                            tableString += rows[i][nameList[j].field.substring(0, e)];
                        }
                        else {
                            var sValue = rows[i][nameList[j].field] + '';

                            if (sValue.match(R_ISO8601_STR)) {
                                sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                            }

                            tableString += sValue;
                        }
                        tableString += '</td>';
                    }
                }
                tableString += '\n</tr>';
            }

            return tableString;
        }
    },
};
JQ.Flow = function (options) {
    this.$form = $("#" + options.formID);
    if (this.$form.length == 0) {
        $("<a>", { "class": "easyui-linkbutton", "data-options": "plain:true,iconCls:'fa fa-question-circle'", text: "未找到Form", disabled: true }).appendTo(options.$panel);
        return;
    }
    this.$form.attr({ action: options.url, method: "post" });
    var temp = 0;
    while (document.getElementById("_flowware_block_" + temp)) {
        temp++;
    }
    this.$menu = $("<div id=\"_flowware_block_" + temp + "\"></div>").appendTo(this.$form);
    this.$source = $("<a>", { "class": "easyui-menubutton", 'data-options': "plain:true,iconCls:'fa fa-flag',menu:'#_flowware_block_" + temp + "'", text: "流程操作", disabled: true }).appendTo(options.$panel);
    this.url = options.url;
    this.refID = options.refID;
    if (options.filterSteps) {
        this.filterSteps = options.filterSteps;
    }
    this.bridgeID = options.bridgeID;
    this.guid = options.guid;
    this.refTable = options.refTable;
    this.instance = options.instance;
    this.isShowSave = options.isShowSave;//是否显示保存
    if (options.disabled == true) {
        this.disabled = true;
    }
    if (options.getStepUsers) {
        this.getStepUsers = options.getStepUsers;
    }
    //JQ.Flow.registerFlow(window[this.instance]);
    this.processor = options.processor;
    this.flowNodeID = options.flowNodeID;
    if (options.dataCreator) {
        this.dataCreator = parseInt(options.dataCreator);
    }
    this.flowMultiSignID = options.flowMultiSignID || 0;
    if (options.beforeFormSubmit) {
        this.beforeFormSubmit = options.beforeFormSubmit;
    }
    if (options.onSubmited) {
        this.onSubmited = options.onSubmited;
    }
    if (options.onLoaded) {
        this.onLoaded = options.onLoaded;
    }
    this.$form[0].style.position = "relative";
    this.showMask();
    window[this.instance] = this;
    if (options.onInit) {
        options.onInit.call(this);
    }
}

var flowInfo = "";

JQ.Flow.prototype.load = function () {
    if (this.disabled === true) {
        if (this.isShowSave != false) {
            $("<a href=\"javascript:void(0)\">暂存</a>").insertBefore(this.$source).linkbutton({
                plain: true, iconCls: "fa fa-save", onClick: function () { this.submit({ action: "save" }); }
            });
        }
        this.$source.remove();
        this.hideMask();
        this.onLoaded && this.onLoaded.call(this);
        return;
    }

    var _this = this;
    this.isLoading = 1;
    $.ajax({
        url: this.url,
        type: "POST",
        data: { _refID: this.refID, _refTable: this.refTable, _flowNodeID: this.flowNodeID, _flowMultiSignID: this.flowMultiSignID, _action: "load" },
        success: function (result) {
            //console.log(result);
            _this.defaultNote = result.DefaultNote;
            onLoadSuccess(result);
            _this.onLoaded && _this.onLoaded.call(_this);
            _this.hideMask();
            _this.isLoading = 0;
        }
    });
    function onLoadSuccess(result) {
        if (result.Result == false) {
            JQ.dialog.error(result.Message);
            return;
        }
        _this.flowID = result.FlowID;
        _this.flowName = result.FlowName;
        _this.progressUrl = result.ProgressUrl;
        _this.date = result.Date;
        _this.userName = result.UserName;
        _this.stepOrder = result.StepOrder;
        _this.isNew = result.IsNew;
        _this.agentUserName = result.AgentEmpName;

        flowInfo = _this;
        if (!result.IsNew) {
            if (_this.flowNodeID == 0 && result.FlowNodeID > 0) {
                _this.flowNodeID = result.FlowNodeID;
            }
            if (_this.flowMultiSignID == 0 && result.FlowMultiSignID > 0) {
                _this.flowMultiSignID = result.FlowMultiSignID;
            }
        }

        if (result.IsFinished === true || result.IsFlowFinished === true || (_this.dataCreator && _this.flowID == 0 && _this.dataCreator != result.UserID)) {
            //当前节点已经完成
            loadFinished(result)
        }
        else {
            loadUnFinsh(result);
        }
        _this.setControlEditale();
        _this.setSignControl();
        loadSignDatas(result.SignDatas, result.FlowNodeID.toString(), result.IsFinished, result.IsNew);
    }

    function loadFinished(result) {
        if (result.FlowID == 0) {
            _this.allowEditControls = "";
        }
        else {
            _this.allowEditControls = result.AllowEditControls;
        }
        _this.$menu.remove();
        if (result.FlowID > 0) {
            $("<a href=\"javascript:void(0)\" title=\"点击查看流程进度\" style=\"color:#166992\"></a>").text((result.NodeName || result.FlowName)).insertAfter(_this.$source).linkbutton({
                plain: true,
                iconCls: "fa fa-flag",
                onClick: function () {
                    if (_this.flowID == 0) {
                        return;
                    }
                    JQ.Flow.showProgressDialog(_this.flowName, _this.progressUrl + _this.flowID);
                }
            });
        }
        if (result.IsFlowFinished === true) {
            if (_this.isShowSave != false && result.AllowEditControls) {
                $("<a href=\"javascript:void(0)\">保存</a>").insertBefore(_this.$source).linkbutton({
                    plain: true, iconCls: "fa fa-save", onClick: function () { _this.submit({ action: "save" }); }
                });
            }
            if (result.IsAllowUndo) {
                $("<a href=\"javascript:void(0)\">撤销</a>").insertBefore(_this.$source).linkbutton({
                    plain: true, iconCls: "fa fa-undo", onClick: function () {
                        _this.dialogID = JQ.dialog.dialog({
                            title: _this.flowName + " 撤销",
                            url: result.ActionUrl + "?action=undo&refID=" + _this.refID + "&refTable=" + encodeURIComponent(_this.refTable) + "&flowNodeID=" + _this.flowNodeID + "&instance=" + encodeURIComponent(_this.instance) + "&service=" + encodeURIComponent(_this.url) + "&flowMultiSignID=" + _this.flowMultiSignID + "&guid=" + (_this.guid || ""),
                            width: 400,
                            height: 360,
                            iconCls: 'fa fa-flag'
                        });
                    }
                });
            }
        }
        _this.$source.remove();
    }

    function loadUnFinsh(result) {
        _this.allowEditControls = result.AllowEditControls;
        _this.signControls = result.SignControls;
        _this.$source.find("span span:eq(0)").text(result.NodeName);
        if (result.NextAction > 0) {
            _this.$menu.menu("appendItem", { text: "审批提交", name: "next", iconCls: "fa fa-toggle-right" });
        }
        if (result.BackAction > 0) {
            _this.$menu.menu("appendItem", { text: "审批退回", name: "back", iconCls: "fa fa-toggle-left" });
        }
        if (result.changeAction > 0) {
            _this.$menu.menu("appendItem", { text: "换人处理", name: "change", iconCls: "fa fa-user" });
        }
        if (result.FinishAction > 0) {
            _this.$menu.menu("appendItem", { text: "直接完成", name: "finish", iconCls: "fa fa-check-circle" });
        }
        if (result.RejectAction > 0) {
            _this.$menu.menu("appendItem", { text: "直接否决", name: "reject", iconCls: "fa fa-times-circle" });
        }
        if (!result.IsNew) {
            _this.$menu.menu("appendItem", { text: "工作移交", name: "transfer", iconCls: "fa fa-street-view" });
        }
        //if (result.FlowID > 0) {
        _this.$menu.menu("appendItem", { text: "流程进度", name: "progress", iconCls: "fa fa-list" });
        //}
        _this.$source.menubutton("enable");
        if (_this.isShowSave != false) {
            //判定是否要显示
            $("<a href=\"javascript:void(0)\">暂存</a>").insertBefore(_this.$source).linkbutton({
                plain: true, iconCls: "fa fa-save", onClick: function () { _this.submit({ action: "save" }); }
            });
        }
        if (result.LastNote) {
            var div = JQ.Flow.createElement("div", null, { maxWidth: "220px", minWidth: "100px" });
            var notes = result.LastNote.split('\n');
            for (var i in notes) {
                div.appendChild(JQ.Flow.createElement("p", null, null, notes[i]));
            }
            $("<span class=\"fa fa-bell\" style=\"margin:0px 10px 0px 5px;display:inline-block\"></span>").insertAfter(_this.$source).tooltip({
                content: div.outerHTML,
                trackMouse: true,
                position: "bottom",
                onShow: function () { $(this).tooltip("tip").css({ backgroundColor: "#FFFFFF", borderColor: "#66ccff" }); }
            });
        }
        _this.actionUrl = result.ActionUrl;
        _this.nodeName = result.NodeName;
        _this.$menu.menu({
            onClick: function (item) {
                if (item.name == "progress") {
                    if (_this.isNew) {
                        JQ.Flow.showProgressDialog(_this.flowName, _this.progressUrl + _this.refTable);
                    }
                    else {
                        JQ.Flow.showProgressDialog(_this.flowName, _this.progressUrl + _this.flowID);
                    }
                }
                else {
                    if (!_this.validate("1", "load" + item.name)) {
                        return;
                    }
                    _this.dialogID = JQ.dialog.dialog({
                        title: _this.nodeName + " " + item.text,
                        url: _this.actionUrl + "?action=" + item.name + "&refID=" + _this.refID + "&refTable=" + encodeURIComponent(_this.refTable) + "&flowNodeID=" + _this.flowNodeID + "&instance=" + encodeURIComponent(_this.instance) + "&service=" + encodeURIComponent(_this.url) + "&flowMultiSignID=" + _this.flowMultiSignID + "&guid=" + (_this.guid || ""),
                        width: 400,
                        height: 360,
                        iconCls: 'fa fa-flag',
                        onClose: function () {
                            if (_this.setNote) {
                                try {
                                    _this.setNote($("#" + _this.dialogID).find("[flowflag='_flow_note']").textbox("getText"));
                                } catch (e) {

                                }
                            }
                        }
                    });
                }
            }
        });
    }

    function loadSignDatas(signDatas, flowNodeID, isFinished, isNew) {
        if (!signDatas) {
            return;
        }
        var xml = GoldPM.loadXml(signDatas);
        debugger;
        //var _signControl = document.getElementById(xml.documentElement.getAttribute("SignControlID"));
        var _signControl = _this.$form.find("#" + xml.documentElement.getAttribute("SignControlID"))[0];
        if (!_signControl) {
            return;
        }
        var signNotes = GoldPM.selectNodes(xml, "Root/SignNote");
        if (signNotes.length == 0) {
            return;
        }
        var table = null;
        if (_signControl.tagName == "TR") {
            table = _signControl.parentNode;
            var row = null;
            var table = _signControl.parentNode;
            for (var i in signNotes) {
                if (i == 0) {
                    row = _signControl;
                }
                else {
                    row = table.insertRow(row.rowIndex + 1);
                }
            }
        }
        else {
            table = JQ.Flow.createElement("table", { className: "table table-bordered" }, { cursor: "default" });
            _signControl.appendChild(table);
        }
        var row = null, cell0 = null, cell1 = null, templateHTML = null;
        for (var i in signNotes) {
            if (_signControl.tagName == "TR") {
                if (i == 0) {
                    row = _signControl;
                    templateHTML = row.innerHTML;
                }
                else {
                    row = table.insertRow(row.rowIndex + 1);
                    row.innerHTML = templateHTML;
                }
                cell0 = row.cells[0];
                cell1 = row.cells[1];
            }
            else {
                row = table.insertRow(-1);
                cell0 = JQ.Flow.createElement("th", null, { width: "10%" });
                row.appendChild(cell0);
                cell1 = JQ.Flow.createElement("td", null, { width: "90%" });
                row.appendChild(cell1);
            }
            cell0.appendChild(document.createTextNode(signNotes[i].getAttribute("Name")));
            if (signNotes[i].getAttribute("TypeID") == "-1") {
                cell0.appendChild(document.createTextNode("（会签）"));
            }
            var nodeName = signNotes[i].getAttribute("Name");
            var order = signNotes[i].getAttribute("Order") || "";
            if (order) {
                order = "_" + order;
            }
            //验证是否为当前节点
            if (!isNew && !isFinished && signNotes[i].getAttribute("ID") == flowNodeID && (_this.flowMultiSignID == 0 || (_this.flowMultiSignID.toString() == signNotes[i].getAttribute("MultiSignID")))) {
                //可编辑
                var txt = JQ.Flow.createElement("textarea", { className: "easyui-textbox" }, { width: "100%", height: "80px" });
                cell1.appendChild(txt);
                txt.appendChild(document.createTextNode(signNotes[i].textContent));
                $(txt).textbox({
                    multiline: true
                }).textbox("textbox").attr("bookmark", nodeName + order + "_Note");
                _this.getNote = function () {
                    return $(txt).textbox("getText");
                }
                _this.setNote = function (text) {
                    $(txt).textbox("setText", text);
                };
                var div = JQ.Flow.createElement("div", null, { textAlign: "right", float: "left", width: "100%", marginTop: "3px" });
                cell1.appendChild(div);
                div.appendChild(JQ.Flow.createElement("span", { bookmark: nodeName + order + "_EmpName" }, null, _this.userName + (_this.agentUserName ? ("(" + _this.agentUserName + ")") : "")));
                //div.appendChild(JQ.Flow.createElement("span", { bookmark: nodeName + order + "_Date" }, { marginLeft: "5px" }, _this.date));
            }
            else {
                //加载老数据
                if (signNotes[i].textContent) {
                    cell1.appendChild(JQ.Flow.createElement("textarea", { bookmark: nodeName + order + "_Note", readonly: "readonly" }, { height: "45px", width: "100%", resize: "none", border: "none", backgroundColor: "transparent", boxShadow: "none", outline: "none", cursor: "default", color: "#000000", fontSize: "12px" }, signNotes[i].textContent));
                }
                if (signNotes[i].getAttribute("EmpID")) {
                    var div = JQ.Flow.createElement("div", null, { textAlign: "right", float: "left", width: "100%" });
                    cell1.appendChild(div);
                    div.appendChild(JQ.Flow.createElement("span", { bookmark: nodeName + order + "_EmpName" }, null, signNotes[i].getAttribute("EmpName") + (signNotes[i].getAttribute("AgentEmpName") ? ("(" + signNotes[i].getAttribute("AgentEmpName") + ")") : "")));
                    div.appendChild(JQ.Flow.createElement("span", { bookmark: nodeName + order + "_Date" }, { marginLeft: "5px" }, signNotes[i].getAttribute("Date").substr(0, 10)));
                }
            }
        }
    }
}

JQ.Flow.showProgressDialog = function (title, url) {
    JQ.dialog.dialog({
        title: decodeURIComponent(title),
        url: url,
        width: 950,
        height: 650,
        iconCls: "fa fa-list"
    });
}

JQ.Flow.prototype.validate = function (data, action) {
    var isValidate = this.$form.form("validate");
    if (!isValidate) {
        return false;
    }
    if (this.beforeFormSubmit) {
        isValidate = this.beforeFormSubmit(data, action);
        if (isValidate === false) {
            return false;
        }
    }
    return isValidate != false;
}

JQ.Flow.prototype.submit = function (options) {
    if (this.$form.length == 0) {
        JQ.dialog.error("提交错误！");
    }
    var result = true;
    if (this.onBeforeSubmit) {
        result = this.onBeforeSubmit("2", options.action);
        if (!result) {
            return;
        }
    }
    result = this.validate("2", options.action);
    if (!result) {
        return;
    }
    var pwin = window.top.$.messager.progress();
    var _this = this;
    appendHidden("_refID", this.refID);
    appendHidden("_refTable", this.refTable);
    appendHidden("_flowNodeID", this.flowNodeID);
    appendHidden("_flowMultiSignID", this.flowMultiSignID);
    appendHidden("_processor", this.processor);
    if (this.bridgeID) {
        appendHidden("_bridgeID", this.bridgeID);
    }
    if (this.guid) {
        appendHidden("_guid", this.guid);
    }
    for (var i in options) {
        appendHidden("_" + i, options[i]);
    }
    if (options.action == "save" && this.getNote) {
        appendHidden("_note", this.getNote());
    }
    this.$form.form("submit", {
        success: function (data) {
            data = JQ.Flow.parseJson(data);
            if (data.Result === false) {
                window.top.$.messager.progress("close");
                JQ.dialog.error(data.Message);
                return;
            }
            window.top && window.top.refreshToDoForm && window.top.refreshToDoForm();
            if (_this.dialogID) {
                $("#" + _this.dialogID).dialog("close");
            }
            if (_this.onSubmited) {
                _this.onSubmited.call(_this);
            }
            pwin.window("close");
        }
    });
    function appendHidden(name, value) {
        var temp = document.getElementsByName(name);
        if (temp.length == 0) {
            temp = document.createElement("input");
            temp.setAttribute("type", "hidden");
            temp.setAttribute("name", name);
            temp.setAttribute("value", value);
            _this.$form[0].appendChild(temp);
        }
        else {
            temp[0].setAttribute("value", value);
        }
    }
}

//设置页面的可编辑控件
JQ.Flow.prototype.setControlEditale = function () {
    if (this.allowEditControls == "{}") {
        return;
    }
    var controls = (this.allowEditControls || "").split(";");
    var grids = [];
    var names = ",";
    for (var i = 0; i < controls.length; i++) {
        var match = /^([\w\W]*)\{([\w\W]*)\}$/g.exec(controls[i]);
        if (match && match.length > 0) {
            var s = controls[i];
            controls.splice(i, 1);
            i--;
            grids.push({ name: match[1], fields: "," + match[2] + "," });
        }
        else {
            names += controls[i] + ",";
        }
    }
    this.setGridEditable(grids);
    this.$form.find("input[textboxname],select[textboxname],textarea[textboxname],div[fileuploader],a[name],textarea,:checkbox,:radio").each(function () {
        JQ.Flow.processControl.call(this, names, false);
    });
}

//设置表格里面的控件是否可编辑
JQ.Flow.prototype.setGridEditable = function (grids) {
    this.grids = grids;
    var setGrid = function (gridtype) {
        var tableID = this.getAttribute("id");
        //console.log("invoke setGrid " + tableID + " " + gridtype);
        //console.log();
        //获取出当前tabled的fields
        if (gridtype == "datagrid") {
            var fields = null;
            for (var i in grids) {
                if (grids[i].name == tableID) {
                    fields = grids[i].fields;
                    break;
                }
            }
            if (!fields) {
                fields = "";
            }
            $(this).datagrid("getPanel").find(".datagrid-body td[field]").each(function () {
                if (fields.indexOf("," + this.getAttribute("field") + ",") > -1) {
                    return;
                }
                $(this).find("input,select,textarea,a").each(function () {
                    //console.log(this);
                    JQ.Flow.processControl.call(this, "", true);
                });
            });
        }
    }

    //获取出所有的easyui的datagrid
    this.$form.find("table.datagrid-f").each(function () {
        if (this.getAttribute("remoteloaded")) {
            setGrid.call(this, "datagrid");
        }
        var opts = $(this).datagrid("options");
        opts.onFlowLoadSuccess = setGrid;
        //opts.onFlowUpdateRow = setGridRow;
    });
}

JQ.Flow.prototype.setGridRowControl = function (table, id) {
    if (typeof (table) == "string") {
        table = document.getElementById(table);
    }
    if (!table) {
        return;
    }
    var tableID = table.getAttribute("id");
    var fields = null;
    for (var i in this.grids) {
        if (this.grids[i].name == tableID) {
            fields = this.grids[i].fields;
            break;
        }
    }
    if (!fields) {
        fields = "";
    }
    $(table).datagrid("getPanel").find(".datagrid-body tr[datagrid-row-index='" + $(table).datagrid("getRowIndex", id) + "'] td[field]").each(function () {
        if (fields.indexOf("," + this.getAttribute("field") + ",") > -1) {
            return;
        }
        $(this).find("input.combogrid-f,input.textbox-f,select,input.datebox-f,textarea").each(function () {
            JQ.Flow.processControl.call(this, "", true);
        });
    });
}
JQ.Flow.processControl = function (names, isGridControl) {
    if (this.tagName == "INPUT" || this.tagName == "SELECT" || this.tagName == "TEXTAREA") {
        //处理input
        if (!isGridControl) {
            if (this.getAttribute("textboxname") && names.indexOf("," + this.getAttribute("textboxname") + ",") > -1) {
                //包含input的textboxname，无需变为不可编辑
                return;
            }
            else if (this.getAttribute("name") && names.indexOf("," + this.getAttribute("name") + ",") > -1) {
                //包含input的name，无需变为不可编辑
                return;
            }
            else if (this.getAttribute("id") && names.indexOf("," + this.getAttribute("id") + ",") > -1) {
                //包含input的id，无需变为不可编辑
                return;
            }
            else if (this.tagName == "TEXTAREA" && this.parentNode && this.parentNode.className.indexOf("easyui-fluid") > -1) {
                return;
            }


        }
        this.setAttribute("flowdisabled", "1");
        if (this.className.indexOf("textbox-f") > -1) {
            if (this.getAttribute("JQControl")) {
                var c = this.getAttribute("JQControl");
                if (c == "SelectEmp") {
                    var t = $(this).textbox({ editable: false }).textbox("disableValidation").next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" });
                    t.children(":eq(0)").css({ display: "none" });
                    t.children(":eq(1)").css({ backgroundColor: "transparent", paddingLeft: "0px", color: "#000000" });
                }
            }
            else if (this.className.indexOf("datebox-f") > -1 || this.className.indexOf("datetimebox-f") > -1) {
                //处理easyui的datebox
                var next = $(this).next().css({ border: "none", cursor: "default", backgroundColor: "transparent" })[0];
                next.childNodes[0].style.visibility = "hidden";
                next.childNodes[1].style.backgroundColor = "transparent";
                next.childNodes[1].setAttribute("readonly", "readonly");
                next.childNodes[1].setAttribute("disabled", "disabled");
                next.childNodes[1].onfocus = null;
                next.childNodes[1].style.boxShadow = "none";
                if (isGridControl) {
                    next.style.width = "auto";
                    next.childNodes[1].style.width = "100%";
                    next.childNodes[1].style.textAlign = "center";
                }
                else {
                    next.childNodes[1].paddingLeft = "0px";
                    next.childNodes[1].style.color = "#000000";
                }
                var classNames = next.childNodes[1].className.split(" ");
                for (var k = 0; k < classNames.length; k++) {
                    if (classNames[k] == "validatebox-text" || classNames[k] == "validatebox-invalid") {
                        classNames.splice(k, 1);
                        k--;
                    }
                }
                next.childNodes[1].className = classNames.join(" ");
            }
            else if (this.className.indexOf("combotree-f") > -1 || this.className.indexOf("combogrid-f") > -1 || this.className.indexOf("combobox-f") > -1) {
                //处理easyui的combo类型的
                var $next = $(this).next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" });
                $next[0].childNodes[0].style.visibility = "hidden";
                $next[0].childNodes[1].style.paddingLeft = "0px";
                $next[0].childNodes[1].style.color = "#000000";
                $next[0].childNodes[1].style.backgroundColor = "transparent";
                var classNames = $next[0].childNodes[1].className.split(" ");
                for (var k = 0; k < classNames.length; k++) {
                    if (classNames[k] == "validatebox-text" || classNames[k] == "validatebox-invalid") {
                        classNames.splice(k, 1);
                        k--;
                    }
                }
                $next[0].childNodes[1].className = classNames.join(" ");
                $next[0].childNodes[1].setAttribute("disabled", "disabled");
                if (isGridControl) {
                    if ($next[0].childNodes[1].value != "" && !$next.find(":hidden").val()) {
                        $next[0].childNodes[1].value = "";
                        $next[0].childNodes[1].setAttribute("placeholder", "");
                    }
                } else {
                    if ($next[0].childNodes[1].value != "" && !$next.find(":hidden[name='" + this.getAttribute("comboname") + "']").val()) {
                        $next[0].childNodes[1].value = "";
                        $next[0].childNodes[1].setAttribute("placeholder", "");
                    }
                }
            }
            else if (this.className.indexOf("numberspinner-f") > -1) {
                var $next = $(this).next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" });
                $next[0].childNodes[0].style.visibility = "hidden";
                $next[0].childNodes[1].style.paddingLeft = "0px";
                $next[0].childNodes[1].style.color = "#000000";
                $next[0].childNodes[1].style.backgroundColor = "transparent";
                $next[0].childNodes[1].setAttribute("readonly", "readonly");
                var classNames = $next[0].childNodes[1].className.split(" ");
                for (var k = 0; k < classNames.length; k++) {
                    if (classNames[k] == "validatebox-text" || classNames[k] == "validatebox-invalid") {
                        classNames.splice(k, 1);
                        k--;
                    }
                }
                $next[0].childNodes[1].className = classNames.join(" ");
            }
            else {
                //处理easyui的textbox、numberbox
                $(this).textbox({ editable: false }).textbox("disableValidation").next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" }).children(0).css({ color: "#000000", paddingLeft: "0px", backgroundColor: "transparent", textAlign: "inherit" });

                //判断文本框是否有按钮，有按钮的话隐藏
                if ($(this).textbox("button").length > 0) {
                    $(this).textbox("button").hide();
                }

            }
        }
        else if (this.tagName == "SELECT") {
            this.parentNode.appendChild(document.createTextNode($(this).find("option:selected").text()));
            this.style.display = "none";
        }
        else if (this.tagName == "INPUT") {
            this.style.color = "#000000";
            this.style.paddingLeft = "0px";
            this.style.backgroundColor = "transparent";
            this.style.border = "none";
            this.style.boxShadow = "none";
            this.setAttribute("readonly", "readonly");
            this.style.cursor = "default";

            if (this.type == "checkbox") {
                this.setAttribute("disabled", "disabled");
            }
            if (this.type == "radio") {
                this.setAttribute("disabled", "disabled");
            }
        }
        else if (this.tagName == "TEXTAREA") {
            this.style.resize = "none";
            this.style.border = "none";
            this.style.boxShadow = "none";
            this.style.outline = "none";
            this.style.backgroundColor = "transparent";
            this.style.color = "#000000";
            this.setAttribute("readonly", "readonly");
            this.style.cursor = "default";
        }

    }
    else if (this.tagName == "DIV" && this.getAttribute("fileuploader") && names.indexOf("," + this.getAttribute("fileuploader") + ",") == -1) {
        //上传控件
        this.setAttribute("flowdisabled", "1");
        JQ.Flow.removeNode(document.getElementById("upload_" + this.getAttribute("uniqueid")));
        JQ.Flow.removeNode(document.getElementById("delete_" + this.getAttribute("uniqueid")));
    }
    else if (this.tagName == "A" && (names.indexOf("," + this.getAttribute("name") + ",") == -1 && !isGridControl)) {
        this.setAttribute("flowdisabled", "1");
        this.style.display = "none";
    }
}

JQ.Flow.disableControl = function (element) {
    JQ.Flow.processControl.call(element, "", false);
}

//设置页面的签名控件
JQ.Flow.prototype.setSignControl = function () {
    var signControls = (this.signControls || "").split(",");
    try {
        for (var i in signControls) {
            var match = /^([\w\W]*)\(([\w\W]*)\)$/g.exec(signControls[i]);
            if (match && match.length > 0) {
                var $ctrl = this.$form.find("input[textboxname='" + match[1] + "'],textarea[textboxname='" + match[1] + "']");
                if ($ctrl.length > 0) {
                    var tages = match[2].split("+");
                    var txts = [];
                    for (var j in tages) {
                        switch (tages[j]) {
                            case "签名":
                                txts.push(this.userName);
                                break;
                            case "日期":
                                txts.push(this.date.substring(0, 10));
                                break;
                            case "日期+时间":
                                txts.push(this.date);
                                break;
                        }
                    }
                    $ctrl.textbox("setText", txts.join(" ")).textbox({ editable: false }).textbox("disableValidation").next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" }).children(0).css({
                        color: "#000000", paddingLeft: "0px", backgroundColor: "transparent"
                    });
                }
            }
        }
    }
    catch (err) {
    }
}

JQ.Flow.prototype.showMask = function () {
    this.maskDiv0 = JQ.Flow.createElement("div", null, {
        backgroundColor: "#EFEFEF", opacity: 0.8, position: "absolute", top: "0px", left: "0px", height: "100%", width: "100%"
    });
    this.$form[0].appendChild(this.maskDiv0);
    this.maskDiv1 = JQ.Flow.createElement("div", null, {
        position: "absolute", top: "40%", left: "0px", width: "100%", textAlign: "center", color: "#888888"
    });
    this.$form[0].appendChild(this.maskDiv1);
    this.maskDiv1.appendChild(JQ.Flow.createElement("span", { className: "panel-loading" }));
    this.maskDiv1.appendChild(JQ.Flow.createElement("span", null, { marginLeft: "3px" }, "正在加载中"));
}

JQ.Flow.prototype.hideMask = function () {
    JQ.Flow.removeNode(this.maskDiv0);
    JQ.Flow.removeNode(this.maskDiv1);
    delete this.maskDiv0;
    delete this.maskDiv1;
}

//TODO 流程变更
JQ.Flow.prototype.changeFlow = function (refTable) {

    if (this.isLoading == 1) {
        JQ.dialog.error("流程正在初始化中，无法变更！");
        return;
    }
    this.showMask();
    this.refTable = refTable;
    this.load();
}
JQ.Flow.showFlowProgress = function (grid, refTable, refID, url) {
    if (!JQ.Flow._progress) {
        JQ.Flow._progress = {};
    }
    JQ.Flow._progress["$flow" + grid] = {
        url: url, refTable: refTable, refID: refID, ids: []
    };
    return function (value, rowData) {
        var _refID = refID && rowData[refID] || value;
        JQ.Flow._progress["$flow" + grid].ids.push(_refID);
        return JQ.Flow.createElement("span", { id: "c" + grid + "_" + _refID, className: "fa fa-spin fa-spinner" }, { display: "inline-block" }).outerHTML;
    }
};

JQ.Flow.showProgress = function (flowID, flowName, statusID, statusName, turnedEmpIDs, creator, currentUserID) {
    return function (value, rowData) {
        var span = document.createElement("span");
        if (!rowData[statusName]) {
            span.appendChild(document.createTextNode("未审批"));
            span.style.color = "#888888";
            if (rowData[creator].toString() == currentUserID.toString()) {
                rowData._EditStatus = 1;
                rowData._AllowCheck = true;
            }
            else {
                rowData._EditStatus = 0;
                rowData._AllowCheck = false;
            }
        }
        else {
            span.style.color = "#165778";
            span.appendChild(document.createTextNode(rowData[statusName]));
            span.style.cursor = "pointer";
            span.setAttribute("onclick", "JQ.Flow.showProgressDialog('" + rowData[flowName] + "', '" + (JQ.Flow.sitePath || window.top.rootPath) + "Core/PubFlow/Progress?flowID=" + rowData[flowID] + "');JQ.Flow.stopBubble();");
            if (rowData[statusID] == 3 || rowData[statusID] == 4) {
                rowData._EditStatus = 0;
            }
            else if (("," + rowData[turnedEmpIDs] + ",").indexOf("," + currentUserID.toString() + ",") > -1 && rowData[statusName].indexOf("退回") == -1) {
                //alert(rowData[turnedEmpIDs] + "," + currentUserID);
                rowData._EditStatus = 2;
            }

            else {
                rowData._EditStatus = 0;
            }



            if (rowData[statusName].indexOf("退回") == 0 && rowData[creator].toString() == currentUserID.toString()) {
                rowData._EditStatus = 1;
                rowData._AllowCheck = true;
            }
            else {
                rowData._AllowCheck = false;
            }
        }
        return span.outerHTML;
    }
}

//在表格中显示流程的审批进度
JQ.Flow.loadFlowProgress = function (row, gridType) {
    if (!JQ.Flow._progress || !("$flow" + this.getAttribute("id") in JQ.Flow._progress)) {
        return;
    }
    var tempid = this.getAttribute("id");
    var temp = JQ.Flow._progress["$flow" + tempid];
    var flowIDs = temp.ids;
    temp.ids = [];
    $.ajax({
        url: (temp.url || JQ.Flow.sitePath || window.top.rootPath) + "/Core/PubFlow/GetFlowStatuses",
        type: "post",
        data: {
            refTable: temp.refTable, refIDs: flowIDs.join(",")
        },
        async: false,
        success: function (result) {
            if (!result) {
                return;
            }
            var isIn = false;
            for (var i in flowIDs) {
                var span = document.getElementById("c" + tempid + "_" + flowIDs[i]);
                if (!span) {
                    continue;
                }
                isIn = false;
                for (var j in result.datas) {
                    if (result.datas[j].FlowRefID == flowIDs[i]) {
                        span.className = "";
                        span.innerHTML = "";
                        span.style.color = "#165778";
                        span.appendChild(document.createTextNode(result.datas[j].FlowStatusName));
                        span.style.cursor = "pointer";
                        span.onclick = function (_flowName, _url) {
                            return function () {
                                JQ.Flow.showProgressDialog(_flowName, _url);
                                JQ.Flow.stopBubble();
                            };
                        }(result.datas[j].FlowName, result.url + result.datas[j].Id)
                        isIn = true;
                        break;
                    }
                }
                if (!isIn) {
                    span.className = "";
                    span.innerHTML = "";
                    span.appendChild(document.createTextNode("未审批"));
                    span.style.color = "#888888";
                }
            }
            if (gridType == "datagrid") {
                ////加载数据，控制checkbox的可选状态
                //var rows = $("#" + tempid).datagrid("getRows");
                //var rowViews = $("#" + tempid).parent().find(".datagrid-view2 .datagrid-body tr.datagrid-row");
                //for (var i in rows) {
                //    var f = rows[i][temp.refID];
                //    for (var j in result.datas) {
                //        if (result.datas[j].FlowRefID == f && result.datas[j].FlowStatusName.indexOf("退回") != 0 && (result.datas[j].FlowStatusID == 2 || result.datas[j].FlowStatusID == 3)) {
                //            rowViews.filter("[datagrid-row-index='" + i + "']").find("td[field='ck'] :checkbox").prop("disabled", "disabled");
                //        }
                //    }
                //}
            } else {
                //加载数据，控制checkbox的可选状态
                var rows = $("#" + tempid).treegrid("getRoots");
                var rowViews = $("#" + tempid).treegrid("getPanel").find(".datagrid-view2 .datagrid-body tr.datagrid-row");
                for (var j in result.datas) {
                    //result.datas[j].FlowRefID
                    if (result.datas[j].FlowStatusName.indexOf("退回") != 0 && (result.datas[j].FlowStatusID == 2 || result.datas[j].FlowStatusID == 3)) {
                        rowViews.filter("[node-id='" + result.datas[j].FlowRefID + "']").find("td[field='ck'] :checkbox").prop("disabled", "disabled");
                    }
                }
            }
            JQ.Flow.onLoadFlowProgress && JQ.Flow.onLoadFlowProgress(tempid, result.datas);
        }
    });
    //var currentUserId = $("#CurrentUserID").val();
    //if (currentUserId == "") { return; }
    if (gridType == "datagrid") {
        var rows = $("#" + tempid).datagrid("getRows");
        var rowViews = $("#" + tempid).parent().find(".datagrid-view2 .datagrid-body tr.datagrid-row");
        for (var i in rows) {
            var FlowStatus = rows[i].FlowStatus;

            if (FlowStatus == "2" || FlowStatus == "-1" || FlowStatus == "3") {
                rowViews.filter("[datagrid-row-index='" + i + "']").find("td[field='ck'] :checkbox").prop("disabled", "disabled");
            }

        }
    }
}

JQ.Flow.htmlEncode = function (html) {
    var temp = document.createElement("div");
    (temp.textContent != undefined) ? (temp.textContent = html) : (temp.innerText = html);
    var output = temp.innerHTML;
    temp = null;
    return output;
}

JQ.Flow.htmlDecode = function (text) {
    var temp = document.createElement("div");
    temp.innerHTML = text;
    var output = temp.innerText || temp.textContent;
    temp = null;
    return output;
}

JQ.Flow.parseDateText = function (str) {
    var matches = /^\/Date\((-?[0-9]*)\)\/$/.exec(str);
    if (matches && matches.length > 0) {
        var text = JQ.Flow.getDateText(new Date(parseInt(matches[1])));
        if (text == "1900-01-01") {
            text = "";
        }
        return text;
    }
    else {
        return str.replace(/T/g, " ");
    }
}

JQ.Flow.parseDateTimeText = function (str) {
    var matches = /^\/Date\((-?[0-9]*)\)\/$/.exec(str);
    if (matches && matches.length > 0) {
        var text = JQ.Flow.getDateTimeText(new Date(parseInt(matches[1])));
        if (text == "1900-01-01 00:00:00") {
            text = "";
        }
        return text;
    }
    else {
        return str.replace(/T/g, " ");
    }
}

JQ.Flow.getDateText = function (date) {
    return date.getFullYear().toString() + "-" + JQ.Flow.supplyString((date.getMonth() + 1)) + "-" + JQ.Flow.supplyString(date.getDate());
}

JQ.Flow.getDateTimeText = function (date) {
    return date.getFullYear().toString() + "-" + JQ.Flow.supplyString((date.getMonth() + 1)) + "-" + JQ.Flow.supplyString(date.getDate()) + " " + JQ.Flow.supplyString(date.getHours()) + ":" + JQ.Flow.supplyString(date.getMinutes()) + ":" + JQ.Flow.supplyString(date.getSeconds());
}

JQ.Flow.parseJson = function (data) {
    return (new Function("return " + data))();
}

JQ.Flow.supplyString = function (content, digit) {
    if (!digit) {
        digit = 2;
    }
    content = content.toString();
    while (content.length < digit) {
        content = "0" + content;
    }
    return content;
}

JQ.Flow.stopBubble = function (e) {
    if (!e && window.event) {
        e = window.event;
    }
    if (!e) {
        return;
    }
    if (document.all) {
        e.cancelBubble = true;
    }
    else {
        e.stopPropagation();
    }
}

JQ.Flow.createElement = function (tagName, attributes, styles, text) {
    var v = document.createElement(tagName);
    if (attributes) {
        for (var a in attributes) {
            if (attributes[a]) {
                if (a == "className") {
                    v.className = attributes[a];
                }
                else {
                    v.setAttribute(a, attributes[a]);
                }
            }
        }
    }
    if (styles) {
        for (var s in styles) {
            v.style[s] = styles[s];
        }
    }
    if (text) {
        v.appendChild(document.createTextNode(text));
    }
    return v;
}

JQ.Flow.removeNode = function (element) {
    if (element) {
        element.parentNode.removeChild(element);
    }
}

JQ.Flow.insertAfter = function (newElement, targetElement) {
    var parent = targetElement.parentNode;
    if (parent.lastChild == targetElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement, targetElement);
    }
}