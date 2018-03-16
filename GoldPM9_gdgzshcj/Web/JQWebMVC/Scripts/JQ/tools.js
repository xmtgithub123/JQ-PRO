
var JQ = JQ || {};
//金曲工具
JQ.tools = {
    //获取弹出框的div信息
    findDialogInfo: function () {
        $div = window.top.$(".rwpdialogdiv:last");
        if ($div.size() > 0) {
            return {
                $div: $div,
                $divPrev: $div.prev(),
                divid: $div.attr("id"),
                parentid: $div.attr("parentid"),
                iframeID: $div.attr("iframeID"),
                loadingType: $div.attr("loadingType"),
                dgID: $div.attr("dgID")
            };
        }
        else {
            return null;
        }
    },

    //获取当前的控件避免ID冲突
    findCurControl: function (id) {
        var info = JQ.tools.findDialogInfo();
        if (JQ.tools.isJson(info) && JQ.tools.isNotEmpty(info.divid)) {
            if (info.$div.find("#" + id).size() > 0) {
                return info.$div.find("#" + id);
            }
        }
        return $("#" + id);
    },

    //已处理返回值的ajax
    ajax: function (parames) {
        parames = $.extend({
            type: 'POST',
            dataType: 'json',
            cache: false,
            async: true,
            data: null,
            url: null,
            progressWith: window.top,
            succesCloseProgress: true, // 执行成功后关闭进度遮罩
            succesCollBack: null,
            doBackResult: true,//是否自动处理返回值，只有后台返回doResult对象才有效
            //sender: null // 触发ajax操作的jquery对象，如按钮
        }, parames);
        if (!JQ.tools.isNotEmpty(parames.url)) {
            alert('URL地址不能为空!!!');
            return;
        };
        //$(parames.sender).attr({ "disabled": "disabled" });
        parames.progressWith.$.messager.progress();
        $.ajax({
            type: parames.type,
            url: parames.url,
            cache: parames.cache,
            dataType: parames.dataType,
            async: parames.async,
            data: parames.data,
            success: function (backJson) {
                parames.backData = backJson;
                if (parames.doBackResult) {
                    JQ.tools.requestBackResult(parames);
                }
                else {
                    if (JQ.tools.isExitsFunction(parames.succesCollBack)) {

                        try {
                            parames.succesCollBack(backJson);
                            if (parames.succesCloseProgress) {
                                //$(parames.sender).attr({ "disabled": "" });
                                parames.progressWith.$.messager.progress('close');
                            }
                        }
                        catch (e) {
                            console.log("执行回调失败，请联系管理员！！！" + e.message);
                            parames.progressWith.$.messager.progress('close');
                        }
                    }
                }
            },
            error: function (e, a, r) {
                window.top.JQ.dialog.error("服务器繁忙，请联系管理员！！！");
                //$(parames.sender).attr({ "disabled": "" });
                parames.progressWith.$.messager.progress('close');
            }
        });
    },

    //判断字符串或数组是否非空如果不为空返回true
    isNotEmpty: function (str) {
        if (str && str != null && str != "null") {
            if (isNaN(str)) {
                return str.length > 0;
            }
            else {
                return str > 0;
            }
            return true;
        }
        else {
            return false;
        }
    },

    //判断函数是否存在如果不为空返回true
    isExitsFunction: function (fun) {
        try {
            if (typeof (eval(fun)) == "function") {
                return true;
            }
        }
        catch (e) {

        }
        return false;
    },

    //是否为json对象
    isJson: function (obj) {
        var isjson = typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length;
        return isjson;
    },

    //判断是否为数组并且数据不能为空
    isArray: function (obj) {
        var isok = Object.prototype.toString.call(obj) === '[object Array]';
        if (isok && obj != null && obj.length > 0) {
            return true;
        }
        else {
            return false;
        }
    },

    //转换成数字类型
    parseInt: function (obj, defaultVal) {
        if (!isNaN(obj)) {
            try {
                return window.parseInt(obj, 10);
            }
            catch (e) {
                return window.parseInt(defaultVal, 10);
            }
        }
        else {
            return window.parseInt(defaultVal, 10);
        }
    },

    //处理远程请求返回的结果
    requestBackResult: function (parames) {
        parames = $.extend({
            backData: null,
            succesCollBack: null,
            progressWith: window.top,
            succesCloseProgress: true // 执行成功后关闭进度遮罩
        }, parames);
        var backJson = {
        };
        if (JQ.tools.isJson(parames.backData)) {
            backJson = parames.backData
        }
        else {
            try {
                backJson = eval("(" + parames.backData + ")");
            } catch (e) {
                backJson.stateType = 3;
            }
        }
        switch (backJson.stateType) {
            case 0://成功
                //alert('操作成功');
                if (typeof (backJson.stateMsg) == "string") {
                    window.top.JQ.dialog.show(JQ.tools.isNotEmpty(backJson.stateMsg) ? backJson.stateMsg : "操作成功！！！");
                }

                if (JQ.tools.isExitsFunction(parames.succesCollBack)) {
                    // parames.succesCollBack(backJson);
                    try {
                        parames.succesCollBack(backJson);
                        if (parames.succesCloseProgress) {
                            //$(parames.sender).attr({ "disabled": "" });
                            parames.progressWith.$.messager.progress('close');
                        }
                    }
                    catch (e) {
                        console.log("执行回调失败，请联系管理员！！！" + e.message);
                        parames.progressWith.$.messager.progress('close');
                    }
                } else {
                    parames.progressWith.$.messager.progress('close');
                }
                break;
            case 1://操作失败
                window.top.JQ.dialog.error(JQ.tools.isNotEmpty(backJson.stateMsg) ? backJson.stateMsg : "服务器繁忙，请联系管理员！！！");
                parames.progressWith.$.messager.progress('close');
                break;
            case 2://验证失败
                var errorInfo = backJson.validationResults.length > 0 ? backJson.validationResults[0].ErrorMessage : "验证失败";
                window.top.JQ.dialog.warning(errorInfo);
                parames.progressWith.$.messager.progress('close');
                break;
            case 3://验证失败
                window.top.JQ.dialog.error("服务器繁忙，请联系管理员！！！");
                parames.progressWith.$.messager.progress('close');
                break;
        };
    },

    //格式化时间
    TimeFormatter: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        /*json格式时间转js时间格式*/
        value = value.substr(1, value.length - 2);
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }
        var val = dateValue.format("yyyy-mm-dd HH:MM");
        return val.substr(11, 5);
    },

    //格式化日期
    DateTimeFormatter: function (value, rec, index) {
        if (!JQ.tools.isNotEmpty(value)) {
            return "";
        }
        /*json格式时间转js时间格式*/
        value = value.substr(1, value.length - 2);
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }

        return dateValue.format("yyyy-mm-dd");
    },

    DateTimeFormatterByT: function (value, rec, index) {

        if (JQ.tools.isNotEmpty(value)) {
            value = value.replace("T", " ").substring(0, 10);

            if (value.substring(0, 4) <= 1900)
                value = "";
        }
        return value;
    },

    DateTimeFormatterByHH: function (value, rec, index) {

        if (JQ.tools.isNotEmpty(value)) {
            value = value.replace("T", " ").substring(0, 13);

            if (value.substring(0, 4) <= 1900)
                value = "";
        }
        return value;
    },

    DateTimeFormatterByH: function (value, rec, index) {
        if (JQ.tools.isNotEmpty(value)) {
            value = value.replace("T", " ");

            if (value.substring(0, 4) <= 1900)
                value = "";
        }
        return value;
    },

    //得到根目录
    getRootPath: function () {
        //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
        var curWwwPath = window.document.location.href;
        //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        //获取主机地址，如： http://localhost:8083
        var localhostPaht = curWwwPath.substring(0, pos);
        //获取带"/"的项目名，如：/uimcardprj
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
        return (localhostPaht + projectName);
    },

    //得到随机数
    getMathNo: function () {
        var d = new Date();
        var sp = "";
        var month = d.getMonth() + 1;
        var strDate = d.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var math = Math.floor(Math.random() * (1000000 + 1));
        var cd = d.getHours() + sp + d.getMinutes() + sp + d.getSeconds() + sp + d.getMilliseconds();
        return math + sp + cd;
    },

    //正规获取url的参数值
    getRequests: function (name) {
        debugger;
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var locations = window.location.href;
        var r = locations.substr(locations.indexOf("\?") + 1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    },

    //导出excel
    exportExcel: function (parames, type) {

        JQ.tools.exportNewExcel(parames, type);
        return;
        window.top.$.messager.progress();
        var printDatagrid = JQ.tools.findCurControl(parames.JQID);// $("#" + parames.JQID);
        var tableString = '<table cellspacing="0" class="pb">';
        var option, data, fcol, columns;
        //pagination
        switch (parames.JQLoadingType) {
            case "datagrid":
                option = printDatagrid.datagrid("options");
                data = printDatagrid.datagrid("getData");
                fcol = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                columns = printDatagrid.datagrid("options").columns;    // 得到columns对象
                break;
            case "treegrid":
                option = printDatagrid.treegrid("options")
                data = printDatagrid.treegrid("getData");
                fcol = printDatagrid.treegrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                columns = printDatagrid.treegrid("options").columns;    // 得到columns对象
                break;
        }
        var queryParams = option.queryParams;
        var url = option.url;
        var exc = option.JQExcludeCol;
        var nameList = new Array();
        var maxCol = getMaxCol(fcol);
        // 载入title
        computeRows(exc, fcol, 0);
        computeRows(exc, columns, maxCol);
        tableString += loadHeaderStr(exc, columns, fcol, maxCol, nameList);

        //// 载入内容
        var rows = {
        };
        if (option.pagination) {
            if (type == 0) {
                queryParams.rows = option.pageSize;
                queryParams.page = option.pageNumber;
            }
            else if (type == 1) {
                queryParams.rows = data.total;//获取总行数
                queryParams.page = 1;
            }
            if (parames.fields != undefined) queryParams.fields = parames.fields;

            var sortName = printDatagrid.attr("sortName");
            var sortOrder = printDatagrid.attr("sortOrder");

            if (sortName != undefined) queryParams.sort = sortName;
            if (sortOrder != undefined) queryParams.order = sortOrder;
        }
        $.ajax({
            type: 'POST',
            dataType: 'json',
            cache: false,
            async: true,
            url: url,
            data: queryParams,
            success: function (backJson) {
                if (option.pagination) {
                    if (type == 1) {
                        printDatagrid.datagrid({
                            data: backJson
                        });
                        if (parames.JQLoadingType == "datagrid")
                            data = printDatagrid.datagrid("getData");
                        else if (parames.JQLoadingType == "treegrid")
                            data = printDatagrid.treegrid("getData");
                    }
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                else {
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                tableString += '\n</table>';
                var f = $('<form action="' + JQ.tools.getRootPath() + '/Core/usercontrol/exportExcel" method="post" id="fmByExportExcel"></form>');
                var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
                var l = $('<input type="hidden" id="txtName" name="txtName" value="导出文件" />');
                try {
                    i.val(tableString);
                    i.appendTo(f);
                    l.appendTo(f);
                    f.appendTo(document.body).submit();
                    f.remove();
                }
                catch (e) {
                    window.top.$.messager.progress('close');
                }
                window.top.$.messager.progress('close');
            },
            error: function () {
                window.top.JQ.dialog.error("请求数据源失败，请联系管理员！！！");
                window.top.$.messager.progress('close');
            }
        });

        //计算冻结列的最大数，在冻结列有多行跨列的情况下需要为正常列计算增量
        function getMaxCol(fcol) {
            var nums = 0;
            if (JQ.tools.isNotEmpty(fcol)) {
                $(fcol).each(function (index) {
                    if (fcol[index].length > nums) {
                        nums = fcol[index].length;
                    }
                });
            }
            return nums;
        }

        //计算头部的行为需要排除的列做准备[addnums需要增量值，如果有冻结列则正常的列数又会从0计算，需要加上增量，保证排除的列正常]
        function computeRows(exclude, cols, addnums) {
            if (JQ.tools.isNotEmpty(exclude) && JQ.tools.isNotEmpty(cols)) {
                for (var i = 0; i < exclude.length; i++) {
                    var num = exclude[i];
                    $(cols).each(function (index) {
                        for (var j = 0; j < cols[index].length; j++) {
                            var colspan = 0;
                            if (JQ.tools.isNotEmpty(cols[index][j].colspan) && cols[index][j].colspan > 1) {
                                colspan = cols[index][j].colspan;
                            }
                            else {
                                colspan = 1;
                            }
                            cols[index][j].min = j > 0 ? cols[index][j - 1].max : 0 + addnums;
                            cols[index][j].max = (cols[index][j].min + colspan);
                        }
                    });
                };
                for (var i = 0; i < exclude.length; i++) {
                    var num = exclude[i];
                    $(cols).each(function (index) {
                        for (var j = 0; j < cols[index].length; j++) {
                            var min = cols[index][j].min;
                            var max = cols[index][j].max;
                            if (num > min && num <= max) {
                                if (JQ.tools.isNotEmpty(cols[index][j].colspan)) {
                                    cols[index][j].colspan1 = cols[index][j].colspan - 1;
                                }
                                else {
                                    cols[index][j].colspan1 = 0;
                                }
                            }
                        }
                    });
                }
            }
        }

        //拼接头部字符考虑跨列跨行[addnums需要增量值，如果有冻结列则正常的列数又会从0计算，需要加上增量，保证排除的列正常]
        function loadHeaderStr(exclude, cols, fcol, addnums, nameList) {
            var str = '';
            if (JQ.tools.isNotEmpty(cols)) {
                $(cols).each(function (index) {
                    str += '\n<tr>';
                    if (JQ.tools.isNotEmpty(fcol)) {
                        for (var i = 0; i < fcol[index].length; ++i) {
                            if (JQ.tools.isNotEmpty(exclude) && exclude.indexOf(i + 1) > -1 && fcol[index][i].colspan1 <= 0) {
                                continue;
                            }
                            if (!fcol[index][i].hidden) {
                                str += '\n<th width="' + fcol[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(fcol[index][i].rowspan) && fcol[index][i].rowspan > 1) {
                                    str += ' rowspan="' + fcol[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].colspan) && fcol[index][i].colspan > 1) {
                                    str += ' colspan="' + fcol[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].field)) {
                                    nameList.push(fcol[index][i]);
                                }
                                str += '>' + fcol[index][i].title + '</th>';
                            }
                        }
                    }
                    if (JQ.tools.isNotEmpty(cols)) {
                        for (var i = 0; i < cols[index].length; ++i) {
                            if (JQ.tools.isNotEmpty(exclude) && exclude.indexOf(i + 1 + addnums) > -1 && cols[index][i].colspan1 <= 0) {
                                continue;
                            }
                            if (!cols[index][i].hidden) {
                                str += '\n<th width="' + cols[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(cols[index][i].rowspan) && cols[index][i].rowspan > 1) {
                                    str += ' rowspan="' + cols[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].colspan) && cols[index][i].colspan > 1) {
                                    str += ' colspan="' + cols[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].field)) {
                                    nameList.push(cols[index][i]);
                                }
                                str += '>' + cols[index][i].title + '</th>';
                            }
                        }
                    }
                    str += '\n</tr>';
                });
            }
            return str;
        }


        function loadrows(rows, num) {

            var str = '';
            for (var i = 0; i < rows.length; ++i) {
                str += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    var e = nameList[j].field.lastIndexOf('_0');
                    str += '\n<td';
                    if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                        str += ' style="text-align:' + nameList[j].align + ';"';
                    }
                    str += '>';
                    for (var k = 0; k < num; k++) {
                        str += (j <= 0 ? '&nbsp;&nbsp;' : '');
                    }

                    if (e + 2 == nameList[j].field.length) {
                        var sValue = rows[i][nameList[j].field.substring(0, e)] + '';
                        if (sValue.match(R_ISO8601_STR)) {
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                        }
                        str += sValue;
                    }
                    else {

                        var field = nameList[j].field;
                        if (typeof (nameList[j].exportfield) != "undefined") {
                            field = nameList[j].exportfield;
                        }
                        var sValue = rows[i][field] + '';

                        if (sValue.match(R_ISO8601_STR)) {
                            if ($("#ExportType").val() == "1") {
                                sValue = JQ.tools.DateTimeFormatterByT(sValue)
                            }
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');

                        }
                        str += sValue;
                    }
                    str += '</td>';
                }
                str += '\n</tr>';
                if (JQ.tools.isNotEmpty(rows[i].children)) {

                    str += loadrows(rows[i].children, num + 1);
                }
            }
            return str;
        }
    },

    //导出EXCEL  吴豪杰
    exportNewExcel: function (parames, type) {
        debugger;
        window.top.$.messager.progress();
        var printDatagrid = JQ.tools.findCurControl(parames.JQID);// $("#" + parames.JQID);
        var tableString = '<table cellspacing="0" class="pb">';
        var option, data, fcol, fcolFields, columns, columnsFields, headerColumn = [];
        //pagination

        switch (parames.JQLoadingType) {
            case "datagrid":
                option = printDatagrid.datagrid("options");
                data = printDatagrid.datagrid("getData");
                fcol = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                fcolFields = printDatagrid.datagrid("getColumnFields", true);
                columns = printDatagrid.datagrid("options").columns;    // 得到columns对象
                columnsFields = printDatagrid.datagrid("getColumnFields");
                break;
            case "treegrid":
                option = printDatagrid.treegrid("options")

                data = {
                    rows: printDatagrid.treegrid("getData"),
                    total: $(printDatagrid.treegrid("getPager")).pagination("options").total,
                };
                fcol = printDatagrid.treegrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                fcolFields = printDatagrid.treegrid("getColumnFields", true);
                columns = printDatagrid.treegrid("options").columns;    // 得到columns对象
                columnsFields = printDatagrid.treegrid("getColumnFields");
                break;
        }

        var queryParams = option.queryParams;
        var exc = option.JQExcludeCol ? option.JQExcludeCol : [];
        var excField = [];//排查列的 field
        getExcludeField(fcolFields, columnsFields, exc);
        var url = option.url;
        var nameList = new Array(headerColumn.length);

        tableString += loadHeaderStr(fcol, columns, nameList, exc);

        //// 载入内容
        var rows = {
        };
        if (option.pagination) {
            if (type == 0) {
                queryParams.rows = option.pageSize;
                queryParams.page = option.pageNumber;
            }
            else if (type == 1) {
                queryParams.id = 0;
                queryParams.rows = data.total;//获取总行数
                queryParams.page = 1;
            }
            if (parames.fields != undefined) queryParams.fields = parames.fields;

            var sortName = printDatagrid.attr("sortName");
            var sortOrder = printDatagrid.attr("sortOrder");

            if (sortName != undefined) queryParams.sort = sortName;
            if (sortOrder != undefined) queryParams.order = sortOrder;
        }

        $.ajax({
            type: 'POST',
            dataType: 'json',
            cache: false,
            async: true,
            url: url,
            data: queryParams,
            success: function (backJson) {
                if (option.pagination) {
                    if (type == 1) {
                        //printDatagrid.datagrid({
                        //    data: backJson
                        //});

                        var $disDiv = $("<div style='display:none;'></div>");
                        var $disTable = $("<table></table>");
                        $disDiv.appendTo(window.top.$("body"));
                        $disTable.appendTo($disDiv);
                        var newOption;
                        newOption = $.extend(newOption, option);
                        newOption.pagination = false;
                        if (parames.JQLoadingType == "datagrid") {
                            newOption.url = null;
                            newOption.toolbar = null;
                            newOption.data = backJson;
                            $disTable.datagrid(newOption);
                            data = $disTable.datagrid("getData");
                        }
                        else if (parames.JQLoadingType == "treegrid") {
                            newOption.url = null;
                            newOption.toolbar = null;
                            newOption.data = backJson;
                            $disTable.treegrid(newOption);
                            data = $disTable.treegrid("getData");
                            //data = $disTable.treegrid("getData");
                        }
                        $disDiv.remove();
                        //if (parames.JQLoadingType == "datagrid")
                        //    data = printDatagrid.datagrid("getData");
                        //else if (parames.JQLoadingType == "treegrid")
                        //    data = printDatagrid.treegrid("getData");
                    }
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                else {
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                tableString += '\n</table>';
                var f = $('<form action="' + window.top.rootPath + '/Core/usercontrol/exportExcel" method="post" id="fmByExportExcel"></form>');
                var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
                var l = $('<input type="hidden" id="txtName" name="txtName" value="导出文件" />');
                try {
                    i.val(tableString);
                    i.appendTo(f);
                    l.appendTo(f);
                    f.appendTo(document.body).submit();
                    f.remove();
                }
                catch (e) {
                    window.top.$.messager.progress('close');
                }
                window.top.$.messager.progress('close');
            },
            error: function () {
                window.top.JQ.dialog.error("请求数据源失败，请联系管理员！！！");
                window.top.$.messager.progress('close');
            }
        });

        //设置总列数、以及排除列
        function getExcludeField(fcolFields, columnsFields, exc) {
            for (var i = 0; i < fcolFields.length; i++) {
                headerColumn.push(fcolFields[i]);
            }
            for (var i = 0; i < columnsFields.length; i++) {
                headerColumn.push(columnsFields[i]);
            }
            for (var i = 0; i < exc.length; i++) {
                try {
                    excField.push(headerColumn[exc[i] - 1]);
                } catch (e) {

                }
            }
        }

        //拼接头部字符考虑跨列跨行[addnums需要增量值，如果有冻结列则正常的列数又会从0计算，需要加上增量，保证排除的列正常]
        function loadHeaderStr(fcol, cols, exclude) {
            var maxColum, str = "";
            if (typeof (fcol) != 'undefined') {
                maxColum = fcol;
            }
            if (typeof (cols) != 'undefined') {
                maxColum = fcol.length > cols.length ? fcol : cols;
            }

            if (JQ.tools.isNotEmpty(maxColum)) {
                $(maxColum).each(function (index) {
                    str += '<tr>';
                    if (JQ.tools.isNotEmpty(fcol) && JQ.tools.isNotEmpty(fcol[index])) {
                        for (var i = 0; i < fcol[index].length; ++i) {
                            if (JQ.tools.isNotEmpty(excField) && JQ.tools.isNotEmpty(fcol[index]) && excField.indexOf(fcol[index][i].field) > -1) {
                                continue;
                            }
                            if (!fcol[index][i].hidden) {
                                str += '\n<th width="' + fcol[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(fcol[index][i].rowspan) && fcol[index][i].rowspan > 1) {
                                    str += ' rowspan="' + fcol[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].colspan) && fcol[index][i].colspan > 1) {
                                    str += ' colspan="' + fcol[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].field)) {
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = fcol[index][i];
                                    }
                                    //nameList.push(fcol[index][i]);
                                }
                                str += '>' + fcol[index][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    str += "<th width='0'></th>";
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = fcol[index][i];
                                    }
                                } else {
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = undefined;
                                    }
                                }
                            }
                        }

                    }
                    if (JQ.tools.isNotEmpty(cols) && JQ.tools.isNotEmpty(cols[index])) {
                        for (var i = 0; i < cols[index].length; ++i) {

                            if (JQ.tools.isNotEmpty(excField) && JQ.tools.isNotEmpty(cols[index]) && excField.indexOf(cols[index][i].field) > -1) {
                                continue;
                            }
                            if (!cols[index][i].hidden) {
                                str += '\n<th width="' + cols[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(cols[index][i].rowspan) && cols[index][i].rowspan > 1) {
                                    str += ' rowspan="' + cols[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].colspan) && cols[index][i].colspan > 1) {
                                    str += ' colspan="' + cols[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].field)) {
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = cols[index][i];
                                    }
                                    //nameList.push(cols[index][i]);
                                }
                                str += '>' + cols[index][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    str += "<th width='0'></th>";
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = cols[index][i];
                                    }
                                } else {
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = undefined;
                                    }
                                }

                                //nameList = JQ.tools.delArr(nameList, cols[index][i].field);
                            }
                        }
                    }
                    str += '\n</tr>';
                });
            }
            return str;
        }

        function loadrows(rows, num) {

            var str = '';
            for (var i = 0; i < rows.length; ++i) {
                str += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    if (nameList[j] == undefined) {
                        continue;
                    }
                    var e = nameList[j].field.lastIndexOf('_0');
                    str += '\n<td';
                    if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                        str += ' style="text-align:' + nameList[j].align + ';"';
                    }
                    str += '>';
                    for (var k = 0; k < num; k++) {
                        str += (j <= 0 ? '&nbsp;&nbsp;' : '');
                    }

                    if (e + 2 == nameList[j].field.length) {
                        var sValue = rows[i][nameList[j].field.substring(0, e)] + '';
                        if (sValue.match(R_ISO8601_STR)) {
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                        }
                        str += sValue;
                    }
                    else {

                        var field = nameList[j].field;
                        if (typeof (nameList[j].exportfield) != "undefined") {
                            field = nameList[j].exportfield;
                        }
                        var sValue = rows[i][field] + '';

                        if (sValue.match(R_ISO8601_STR)) {
                            if ($("#ExportType").val() == "1") {
                                sValue = JQ.tools.DateTimeFormatterByT(sValue)
                            }
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');

                        }
                        str += sValue;
                    }
                    str += '</td>';
                }
                str += '\n</tr>';
                if (JQ.tools.isNotEmpty(rows[i].children)) {

                    str += loadrows(rows[i].children, num + 1);
                }
            }
            return str;
        }
    },
    //导出EXCEL  吴豪杰
    exportNewExcel1: function (parames, type) {
        debugger;
        window.top.$.messager.progress();
        var printDatagrid = JQ.tools.findCurControl(parames.JQID);// $("#" + parames.JQID);
        var tableString = '<table cellspacing="0" class="pb">';
        var option, data, fcol, fcolFields, columns, columnsFields, headerColumn = [];
        //pagination

        switch (parames.JQLoadingType) {
            case "datagrid":
                option = printDatagrid.datagrid("options");
                data = printDatagrid.datagrid("getData");
                fcol = printDatagrid.datagrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                fcolFields = printDatagrid.datagrid("getColumnFields", true);
                columns = printDatagrid.datagrid("options").columns;    // 得到columns对象
                columnsFields = ["TaskName", "TaskEmpName", "DatePlanStart", "DatePlanFinish", "TaskNote", "FlowName", "ck"];// printDatagrid.datagrid("getColumnFields");
                break;
            case "treegrid":
                option = printDatagrid.treegrid("options")

                data = {
                    rows: printDatagrid.treegrid("getData"),
                    total: $(printDatagrid.treegrid("getPager")).pagination("options").total,
                };
                fcol = printDatagrid.treegrid("options").frozenColumns;  // 得到frozenColumns对象（冻结的列）
                fcolFields = printDatagrid.treegrid("getColumnFields", true);
                columns = printDatagrid.treegrid("options").columns;    // 得到columns对象
                columnsFields = printDatagrid.treegrid("getColumnFields");
                break;
        }

        var queryParams = option.queryParams;
        var exc = option.JQExcludeCol ? option.JQExcludeCol : [];
        var excField = ['Id'];//排查列的 field
        getExcludeField(fcolFields, columnsFields, exc);
        var url = option.url;
        var nameList = new Array(headerColumn.length);
        tableString += loadHeaderStr(fcol, columns, nameList, exc).replace('<th width="undefined">undefined</th>', '');

        //// 载入内容
        var rows = {
        };
        if (option.pagination) {
            if (type == 0) {
                queryParams.rows = option.pageSize;
                queryParams.page = option.pageNumber;
            }
            else if (type == 1) {
                queryParams.id = 0;
                queryParams.rows = data.total;//获取总行数
                queryParams.page = 1;
            }
            if (parames.fields != undefined) queryParams.fields = parames.fields;

            var sortName = printDatagrid.attr("sortName");
            var sortOrder = printDatagrid.attr("sortOrder");

            if (sortName != undefined) queryParams.sort = sortName;
            if (sortOrder != undefined) queryParams.order = sortOrder;
        }

        $.ajax({
            type: 'POST',
            dataType: 'json',
            cache: false,
            async: true,
            url: url,
            data: queryParams,
            success: function (backJson) {
                if (option.pagination) {
                    if (type == 1) {
                        //printDatagrid.datagrid({
                        //    data: backJson
                        //});

                        var $disDiv = $("<div style='display:none;'></div>");
                        var $disTable = $("<table></table>");
                        $disDiv.appendTo(window.top.$("body"));
                        $disTable.appendTo($disDiv);
                        var newOption;
                        newOption = $.extend(newOption, option);
                        newOption.pagination = false;
                        if (parames.JQLoadingType == "datagrid") {
                            newOption.url = null;
                            newOption.toolbar = null;
                            newOption.data = backJson;
                            $disTable.datagrid(newOption);
                            data = $disTable.datagrid("getData");
                        }
                        else if (parames.JQLoadingType == "treegrid") {
                            newOption.url = null;
                            newOption.toolbar = null;
                            newOption.data = backJson;
                            $disTable.treegrid(newOption);
                            data = $disTable.treegrid("getData");
                            //data = $disTable.treegrid("getData");
                        }
                        $disDiv.remove();
                        //if (parames.JQLoadingType == "datagrid")
                        //    data = printDatagrid.datagrid("getData");
                        //else if (parames.JQLoadingType == "treegrid")
                        //    data = printDatagrid.treegrid("getData");
                    }
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                else {
                    tableString += loadrows(data.rows == undefined ? data : data.rows, 0);
                }
                tableString += '\n</table>';
                var f = $('<form action="' + window.top.rootPath + '/Core/usercontrol/exportExcel" method="post" id="fmByExportExcel"></form>');
                var i = $('<input type="hidden" id="txtContent" name="txtContent" />');
                var l = $('<input type="hidden" id="txtName" name="txtName" value="导出文件" />');
                try {
                    i.val(tableString);
                    i.appendTo(f);
                    l.appendTo(f);
                    f.appendTo(document.body).submit();
                    f.remove();
                }
                catch (e) {
                    window.top.$.messager.progress('close');
                }
                window.top.$.messager.progress('close');
            },
            error: function () {
                window.top.JQ.dialog.error("请求数据源失败，请联系管理员！！！");
                window.top.$.messager.progress('close');
            }
        });

        //设置总列数、以及排除列
        function getExcludeField(fcolFields, columnsFields, exc) {
            for (var i = 0; i < fcolFields.length; i++) {
                headerColumn.push(fcolFields[i]);
            }
            for (var i = 0; i < columnsFields.length; i++) {
                headerColumn.push(columnsFields[i]);
            }
            for (var i = 0; i < exc.length; i++) {
                try {
                    excField.push(headerColumn[exc[i] - 1]);
                } catch (e) {

                }
            }
        }

        //拼接头部字符考虑跨列跨行[addnums需要增量值，如果有冻结列则正常的列数又会从0计算，需要加上增量，保证排除的列正常]
        function loadHeaderStr(fcol, cols, exclude) {
            var maxColum, str = "";
            if (typeof (fcol) != 'undefined') {
                maxColum = fcol;
            }
            if (typeof (cols) != 'undefined') {
                maxColum = fcol.length > cols.length ? fcol : cols;
            }

            if (JQ.tools.isNotEmpty(maxColum)) {
                $(maxColum).each(function (index) {
                    str += '<tr>';
                    if (JQ.tools.isNotEmpty(fcol) && JQ.tools.isNotEmpty(fcol[index])) {
                        for (var i = 0; i < fcol[index].length; ++i) {
                            if (JQ.tools.isNotEmpty(excField) && JQ.tools.isNotEmpty(fcol[index]) && excField.indexOf(fcol[index][i].field) > -1) {
                                continue;
                            }
                            if (!fcol[index][i].hidden) {
                                str += '\n<th width="' + fcol[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(fcol[index][i].rowspan) && fcol[index][i].rowspan > 1) {
                                    str += ' rowspan="' + fcol[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].colspan) && fcol[index][i].colspan > 1) {
                                    str += ' colspan="' + fcol[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(fcol[index][i].field)) {
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = fcol[index][i];
                                    }
                                    //nameList.push(fcol[index][i]);
                                }
                                str += '>' + fcol[index][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    str += "<th width='0'></th>";
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = fcol[index][i];
                                    }
                                } else {
                                    if (headerColumn.indexOf(fcol[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(fcol[index][i].field)] = undefined;
                                    }
                                }
                            }
                        }

                    }
                    if (JQ.tools.isNotEmpty(cols) && JQ.tools.isNotEmpty(cols[index])) {
                        for (var i = 0; i < cols[index].length; ++i) {

                            if (JQ.tools.isNotEmpty(excField) && JQ.tools.isNotEmpty(cols[index]) && excField.indexOf(cols[index][i].field) > -1) {
                                continue;
                            }
                            if (!cols[index][i].hidden) {
                                str += '\n<th width="' + cols[index][i].width + '"';
                                if (JQ.tools.isNotEmpty(cols[index][i].rowspan) && cols[index][i].rowspan > 1) {
                                    str += ' rowspan="' + cols[index][i].rowspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].colspan) && cols[index][i].colspan > 1) {
                                    str += ' colspan="' + cols[index][i].colspan + '"';
                                }
                                if (JQ.tools.isNotEmpty(cols[index][i].field)) {
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = cols[index][i];
                                    }
                                    //nameList.push(cols[index][i]);
                                }
                                str += '>' + cols[index][i].title + '</th>';
                            } else {
                                if (index > 0) {
                                    str += "<th width='0'></th>";
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = cols[index][i];
                                    }
                                } else {
                                    if (headerColumn.indexOf(cols[index][i].field) > -1) {
                                        nameList[headerColumn.indexOf(cols[index][i].field)] = undefined;
                                    }
                                }

                                //nameList = JQ.tools.delArr(nameList, cols[index][i].field);
                            }
                        }
                    }
                    str += '\n</tr>';
                });
            }
            return str;
        }

        function loadrows(rows, num) {

            var str = '';
            for (var i = 0; i < rows.length; ++i) {
                str += '\n<tr>';
                for (var j = 0; j < nameList.length; ++j) {
                    if (nameList[j] == undefined) {
                        continue;
                    }
                    var e = nameList[j].field.lastIndexOf('_0');
                    str += '\n<td';
                    if (nameList[j].align != 'undefined' && nameList[j].align != '') {
                        str += ' style="text-align:' + nameList[j].align + ';"';
                    }
                    str += '>';
                    for (var k = 0; k < num; k++) {
                        str += (j <= 0 ? '&nbsp;&nbsp;' : '');
                    }

                    if (e + 2 == nameList[j].field.length) {
                        var sValue = rows[i][nameList[j].field.substring(0, e)] + '';
                        if (sValue.match(R_ISO8601_STR)) {
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');
                        }
                        str += sValue;
                    }
                    else {

                        var field = nameList[j].field;
                        if (typeof (nameList[j].exportfield) != "undefined") {
                            field = nameList[j].exportfield;
                        }
                        var sValue = rows[i][field] + '';

                        if (sValue.match(R_ISO8601_STR)) {
                            if ($("#ExportType").val() == "1") {
                                sValue = JQ.tools.DateTimeFormatterByT(sValue)
                            }
                            sValue = sValue.replace(/T00:00:00/, "").replace(/1900-01-01/, "").replace('/', '-').replace('T', '　');

                        }
                        str += sValue;
                    }
                    str += '</td>';
                }
                str += '\n</tr>';
                if (JQ.tools.isNotEmpty(rows[i].children)) {

                    str += loadrows(rows[i].children, num + 1);
                }
            }
            return str.replace(new RegExp(/(undefined)/g), '');
        }
    },
    //解码
    htmlDecode: function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&amp;/g, "&");
        s = s.replace(/&quot;/g, "\"");
        s = s.replace(/&copy;/g, "©");
        s = s.replace(/&reg;/g, "®");
        s = s.replace(/&times;/g, "×");
        s = s.replace(/&divide;/g, "÷");
        s = s.replace(/&#39;/g, "'");
        return s;
    },

    //数组转成字段串
    arrParseStr: function (arr, char, field) {
        if (JQ.tools.isArray(arr)) {
            if (JQ.tools.isNotEmpty(field)) {
                var str = '';
                for (var i = 0; i < arr.length; i++) {
                    if (JQ.tools.isNotEmpty(arr[i][field])) {
                        str += i > 0 ? char + arr[i][field] : arr[i][field];
                    }
                }
                return str;
            }
            else {
                return arr.join(char)
            }
        }
        return null;
    },

    //从数组中获取指定的数组(数组里的值必须是json)
    arrFind: function (arr, key, val) {
        var result = new Array();
        if (JQ.tools.isArray(arr)) {
            for (var i = 0; i < arr.length; i++) {
                if (arr[i][key] == val) {
                    result.push(arr[i]);
                }
            }
        }
        return result;
    },

    //从数组中获取指定的元素(数组里的值必须是json) 返回一纬数组
    arrFindS: function (arr, key) {
        var result = new Array();
        if (JQ.tools.isArray(arr)) {
            for (var i = 0; i < arr.length; i++) {
                for (var j in arr[i]) {
                    if (j == key) {
                        result.push(arr[i][j]);
                    }
                }
            }
        }
        return result;
    },

    //删除数组中的元素
    delArr: function (arr, val, field) {
        var newArr = new Array();
        if (JQ.tools.isArray(arr)) {
            var isok = JQ.tools.isNotEmpty(field);
            for (var i = 0; i < arr.length; i++) {
                if (isok) {
                    if (arr[i][field].toString() != val.toString()) {
                        newArr.push(arr[i]);
                    }
                }
                else {
                    if (arr[i].toString() != val.toString()) {
                        newArr.push(arr[i]);
                    }
                }
            }
        }
        return newArr;
    },

    //去除数组重复，只对一维数据有效
    removeArrCF: function (arr) {
        var newArr = new Array();
        if (JQ.tools.isArray(arr)) {
            for (var i = 0; i < arr.length; i++) {
                if (newArr.indexOf(arr[i]) <= -1) {
                    newArr.push(arr[i]);
                }
            }
        }
        return newArr;
    },

    getFileSizeText: function (size) {
        if (!size && size != 0) {
            return "";
        }
        var st = "";
        if (size >= 1024 * 1024 * 1024) {
            st = (size / 1024 / 1024 / 1024).toFixed(2) + "GB";
        } else if (size >= 1024 * 1024) {
            st = (size / 1024 / 1024).toFixed(2) + "MB";
        } else if (size >= 1024) {
            st = (size / 1024).toFixed(2) + "KB";
        } else {
            st = size + "B";
        }
        return st;
    },
    //去掉首尾分隔符 输入",1," 返回"1"
    trimSplitChar: function (str, splitChar) {
        str = $.trim(str);
        if (splitChar == undefined)
            return str;

        //去掉首分隔符号
        if (str.indexOf(splitChar) == 0)
            str = str.substring(str.indexOf(splitChar) + 1, str.length);
        //去掉末尾隔符号
        if (str.lastIndexOf(splitChar) == str.length - 1)
            str = str.substring(0, str.lastIndexOf(splitChar));
        return str;
    },
    //获取json格式数组的指定值的name 如多选阶段后，获取多个阶段名称
    //输入arrValue="59,60," , InSplitChar=",", OutSplitChar="、", filterValue="0", arrSource="arr", arrKeyValue="EmpID", arrKeyName="EmpName"
    //返回 str="可研、初设"
    strGetValName: function (arrSource, arrKeyValue, arrKeyName, filterValue, value, InSplitChar, OutSplitChar) {
        value = JQ.tools.trimSplitChar(value, InSplitChar);
        if (value == filterValue) {
            return;
        }

        var str = "";
        var arr = value.split(InSplitChar);
        for (var m = 0; m < arr.length; m++) {
            for (var i = 0; i < arrSource.length; i++) {
                if (arrSource[i][arrKeyValue] == arr[m]) {
                    str += arrSource[i][arrKeyName] + OutSplitChar;
                    break;
                }
            }
        }
        return JQ.tools.trimSplitChar(str, OutSplitChar);
    },
    fixHeight: function (percent) {
        return (document.body.clientHeight) * percent;
    },
    fixWidth: function (percent) {
        return (document.body.clientWidth - 5) * percent;
    },
    DisabledCtrlFrom: function (element, excludeNames) {
        // 禁用某标签中的所有控件
        $(element).find("input.combogrid-f,input.textbox-f,input.datebox-f,a.easyui-linkbutton,div[fileuploader],div[jqpanel],select,textarea").each(function () {
            JQ.tools.DisabledCtrl.call(this, excludeNames);
        });
    },
    DisabledCtrl: function (excludeNames) {
        if (this.tagName == "INPUT" || this.tagName == "SELECT" || this.tagName == "TEXTAREA") {
            // 排除禁用的控件名称列表
            if (excludeNames != "") {
                if (this.getAttribute("textboxname") && excludeNames.indexOf("," + this.getAttribute("textboxname") + ",") > -1) {
                    //包含input的textboxname，无需变为不可编辑
                    return;
                }
                else if (this.getAttribute("name") && excludeNames.indexOf("," + this.getAttribute("name") + ",") > -1) {
                    //包含input的name，无需变为不可编辑
                    return;
                }
                else if (this.getAttribute("id") && excludeNames.indexOf("," + this.getAttribute("id") + ",") > -1) {
                    //包含input的id，无需变为不可编辑
                    return;
                }
            }
            if (this.className.indexOf("textbox-f") > -1) {
                if (this.getAttribute("JQControl")) {
                    var c = this.getAttribute("JQControl");
                    if (c == "SelectEmp") {
                        var t = $(this).textbox({ editable: false }).textbox("disableValidation").next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" });
                        t.children(":eq(0)").css({ display: "none" });
                        t.children(":eq(1)").css({ backgroundColor: "transparent", paddingLeft: "0px", color: "#000000" });
                    }
                }
                else if (this.className.indexOf("easyui-datebox") > -1) {
                    //处理easyui的datebox
                    var $next = $(this).next().css({ border: "none", cursor: "default" });
                    var text = $(this).textbox("getText");
                    if (text) {
                        $next[0].parentNode.appendChild(document.createTextNode(text));
                    }
                    JQ.Flow.removeNode($next[0].childNodes[0]);
                    JQ.Flow.removeNode($next[0].childNodes[0]);
                    $next[0].parentNode.appendChild($next[0].childNodes[0]);
                    $next[0].remove();
                }
                else if (this.className.indexOf("combotree-f") > -1 || this.className.indexOf("combogrid-f") > -1 || this.className.indexOf("combobox-f") > -1) {
                    //处理easyui的combo类型的
                    $next = $(this).next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" });
                    $next[0].childNodes[0].style.visibility = "hidden";
                    $next[0].childNodes[1].style.paddingLeft = "0px";
                    $next[0].childNodes[1].style.color = "#000000";
                    $next[0].childNodes[1].style.backgroundColor = "transparent";
                    $next[0].childNodes[1].setAttribute("disabled", "disabled");
                }
                else {
                    //处理easyui的textbox、numberbox
                    $(this).textbox({ editable: false }).textbox("disableValidation").next().css({ border: "none", boxShadow: "none", backgroundColor: "transparent" }).children(0).css({ color: "#000000", paddingLeft: "0px", backgroundColor: "transparent", textAlign: "inherit" });
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
            }
            else if (this.tagName == "TEXTAREA") {
                this.style.resize = "none";
                this.style.border = "none";
                this.style.boxShadow = "none";
                this.style.outline = "none";
                this.style.backgroundColor = "transparent";
                this.style.color = "#000000";
            }
        }
        else if (this.tagName == "DIV" && this.getAttribute("jqpanel") && excludeNames.indexOf("," + this.getAttribute("id") + ",") == -1) {
            //工具栏
            this.style.display = "none";
        }
        else if (this.tagName == "DIV" && this.getAttribute("fileuploader") && excludeNames.indexOf("," + this.getAttribute("id") + ",") == -1) {
            //上传控件
            JQ.Flow.removeNode(document.getElementById(this.getAttribute("id") + "_upload"));
            JQ.Flow.removeNode(document.getElementById(this.getAttribute("id") + "_delete"));
        }
        else if (this.tagName == "A" && (excludeNames.indexOf("," + this.getAttribute("name") + ",") == -1)) {
            this.style.display = "none";
        }
    },

     //  获取人名首字母
    CreateEmpNameWithPY: function (empName) {
        var py = pinyin.getFullChars(empName).toUpperCase();
        return py != undefined ? '{0}:{1}'.format(py.substring(0, 1), empName) : empName; //.toSBC()
    },

    // 获取人名排除字母
    GetEmpNameWithoutPY: function (empName) {
        var result = "";
        var myregexp = /(.*):(.*)/;
        var match = myregexp.exec(empName);
        if (match != null) {
            result = match[2];
        } else {
            result = empName;
        }
        return result;
    }

};
