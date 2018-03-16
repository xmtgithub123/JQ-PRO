var idTmr;
function getExplorer() {
    var explorer = window.navigator.userAgent;
    //IE
    if (explorer.indexOf("MSIE") >= 0) {
        return 'ie';
    }
        //火狐  
    else if (explorer.indexOf("Firefox") >= 0) {
        return 'Firefox';
    }
        //谷歌  
    else if (explorer.indexOf("Chrome") >= 0) {
        return 'Chrome';
    }
        //欧鹏  
    else if (explorer.indexOf("Opera") >= 0) {
        return 'Opera';
    }
        //赛飞  
    else if (explorer.indexOf("Safari") >= 0) {
        return 'Safari';
    }
}

function Export(tableid, title) {
    if (getExplorer() == 'ie') {
        var curTbl = document.getElementById(tableid);
        var oXL = new ActiveXObject("Excel.Application");
        var oWB = oXL.Workbooks.Add();
        var xlsheet = oWB.Worksheets(1);
        var sel = document.body.createTextRange();
        sel.moveToElementText(curTbl);
        sel.select();
        sel.execCommand("Copy");
        xlsheet.Paste();
        oXL.Visible = true;

        try {
            var fname = oXL.Application.GetSaveAsFilename(title, "Excel Spreadsheets (*.xls), *.xls");
        } catch (e) {
            print("捕获信息： " + e);
        } finally {
            oWB.SaveAs(fname);
            oWB.Close(savechanges = false);
            oXL.Quit();
            oXL = null;
            idTmr = window.setInterval("Cleanup();", 1);
        }

    }
    else {
        tableToExcel(tableid, title)
    }
}


function Cleanup() {
    window.clearInterval(idTmr);
    CollectGarbage();
}
var tableToExcel = (function () {
    var css = '<style type="text/css">' +
              '.tbBorder{border:0.5pt solid #000;}' +
              '.width{width:auto;}' +
              '</style>';

    var uri = 'data:application/vnd.ms-excel;base64,',
            template = '<html><head><meta charset="UTF-8">' + css + '</head><body>{0}</body></html>',
            base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
            format = function (s, c) {
                return s.replace(/{(\w+)}/g,
                        function (m, p) { return c[p]; })
            }
    return function (table, name) {
        //var ctx = $("#"+table).parent().parent().html();
        var ctx = getRowsHtml(table);
        window.location.href = uri + base64(template.replace("{0}", ctx))
    }
})()

function getRowsHtml(tableID) {
    var tabObj = $("#" + tableID).pivotgrid('options');//声明对象
    var target = $("#" + tableID).pivotgrid('targetObj').get(0);
    var data = $(target).data("pivotgrid").data;
    var span = tabObj.pivot.columns;
    var rowSpan = 1;
    if (span == null || span == undefined) {
        rowSpan = 1;
    }
    else {
        rowSpan = (parseInt(tabObj.pivot.columns.length) + 1).toString();//第一列合并单元格的个数
    }
    var columns = getColumns(target, data);//获取列数据信息
    var allColumns = [];
    var rows = getRows(target, data);
    var TableHtml = "<table style=\"width:100%;border-collapse:collapse;\">";
    TableHtml = TableHtml + "<tr>"
    TableHtml = TableHtml + "<th class=\"tbBorder\" rowspan=\"" + rowSpan + "\">" + tabObj.forzenColumnTitle + "</th>";
    if (columns != null && columns != undefined) {
        for (var index = 0; index < columns.length; index++) {
            var colSet = columns[index];
            for (var colSetsIndex = 0; colSetsIndex < colSet.length; colSetsIndex++) {
                var title = colSet[colSetsIndex].title;
                var colspan = colSet[colSetsIndex].colspan;
                if (colspan == "undefined" || colspan == null) {
                    colspan = "1";
                    allColumns.push(colSet[colSetsIndex].field);
                }
                TableHtml = TableHtml + '<th  colspan="' + colspan + '" class=\"tbBorder\" >' + title + '</th>';
            }
            if (index == columns.length - 1) {
                TableHtml = TableHtml + "</tr>"
            }
            else {
                TableHtml = TableHtml + "</tr><tr>"
            }
        }
    }
    if (rows != null && rows != undefined) {
        var parentRow = [];
        for (var rowIndex = 0; rowIndex < rows.length; rowIndex++) {
            if (rows[rowIndex]._parentId == 'undefined' || rows[rowIndex]._parentId == null) {
                TableHtml = TableHtml + "<tr>";
                TableHtml = TableHtml + '<td class=\"tbBorder\" style=\"text-align:center\"  >' + rows[rowIndex]._tree_field + '</td>';
                if (allColumns.length > 0) {
                    for (var colIndex = 0; colIndex < allColumns.length; colIndex++) {
                        TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[rowIndex][allColumns[colIndex]] + '</td>';
                    }

                }
                TableHtml = TableHtml + "</tr>";
                if (allColumns.length > 0)
                {
                    var ss = allColumns[0].split("_");
                    if (ss.length >= 2)
                    {
                        for (var newIndex = 0; newIndex < rows.length; newIndex++)//此处树状结构暂时展现2层（后期可扩展为递归）
                        {
                            if (rows[newIndex]._parentId == rows[rowIndex]._id_field) {
                                TableHtml = TableHtml + "<tr>";
                                TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[newIndex]._tree_field + '</td>';
                                if (allColumns.length > 0) {
                                    for (var colIndex = 0; colIndex < allColumns.length; colIndex++) {
                                        TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[newIndex][allColumns[colIndex]] + '</td>';
                                    }

                                }
                                TableHtml = TableHtml + "</tr>";

                            }
                            //if(ss.length>=3)
                            //{
                            //    for (var Third = 0; Third < rows.length; Third++)//此处树状结构暂时展现2层（后期可扩展为递归）
                            //    {
                            //        if (rows[Third]._parentId == rows[newIndex]._id_field) {
                            //            TableHtml = TableHtml + "<tr>";
                            //            TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[Third]._tree_field + '</td>';
                            //            if (allColumns.length > 0) {
                            //                for (var colIndex = 0; colIndex < allColumns.length; colIndex++) {
                            //                    TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[Third][allColumns[colIndex]] + '</td>';
                            //                }

                            //            }
                            //            TableHtml = TableHtml + "</tr>";

                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }
            //else {
            //    TableHtml = TableHtml + '<td class=\"tbBorder\" >' + rows[rowIndex]._tree_field + '</th>';
            //}
        }
    }
    TableHtml = TableHtml + "</table>";
    return TableHtml;

}

function getColumns(target, data) {
    if (!data) { return null; }
    var opts = $.data(target, 'pivotgrid').options;
    var columns = [];
    $.map(opts.pivot.columns, function (field, index) {
        var pcolumns = columns[index - 1];
        if (pcolumns) {
            var cc = [];
            $.map(pcolumns, function (pcol) {
                var subcol = getV1(field, pcol._field, pcol.title);
                $.map(subcol, function (v) {
                    cc.push({
                        _field: field,
                        title: v,
                        tt: pcol.tt.concat(v),
                        colspan: opts.pivot.values.length
                    });
                });
                pcol.colspan += (subcol.length - 1) * opts.pivot.values.length;
            });
            columns.push(cc);
        } else {
            var cc = [];
            $.map(getV1(field), function (v) {
                cc.push({
                    _field: field,
                    title: v,
                    tt: [v],
                    colspan: opts.pivot.values.length
                });
            });
            columns.push(cc);
        }
    });

    var cc = [];
    $.map(columns[columns.length - 1], function (col, index) {
        $.map(opts.pivot.values, function (v) {
            cc.push($.extend({}, v, {
                field: col.tt.join('_') + '_' + v.field,
                title: (v.title || v.field),
                tt: col.tt.concat(v.field),
                width: (v.width || opts.valueFieldWidth),
                align: (v.align || 'right'),
                styler: (v.styler || opts.valueStyler),
                formatter: (v.formatter || opts.valueFormatter),
                sortable: true,
                sorter: function (a, b) {
                    var v1 = parseFloat(a);
                    var v2 = parseFloat(b);
                    return v1 == v2 ? 0 : (v1 > v2 ? 1 : -1);
                }
            }))
        });
    });
    columns.push(cc);
    return columns;

    function getV1(field, pfield, pvalue) {
        var vv = [];
        $.map(data, function (row) {
            var val = String(row[field]);
            if (pfield == undefined || row[pfield] == pvalue) {
                if ($.inArray(val, vv) == -1) {
                    vv.push(val);
                }
            }
        });
        return vv;
    }

}


function getRows(target, data) {
    var opts = $.data(target, 'pivotgrid').options;

    var _idIndex = 1;
    var allRows = [];
    var topRows = [];
    $.map(opts.pivot.rows, function (field, index) {
        var pfield = opts.pivot.rows[index - 1];
        if (pfield) {
            var tmpRows = [];
            while (topRows.length) {
                var r1 = topRows.shift();
                var groups = getR1(field, r1._rows);
                $.map(groups, function (rows) {
                    var r = sumR1(rows);
                    r._rows = rows;
                    r[opts.treeField] = rows[0][field];
                    r._parentId = r1[opts.idField];
                    r[opts.idField] = _idIndex++;
                    allRows.push(r);
                    tmpRows.push(r);
                })
            }
            topRows = tmpRows;
        } else {
            var groups = getR1(field, data);
            $.map(groups, function (rows) {
                var r = sumR1(rows);
                r._rows = rows;
                r[opts.treeField] = rows[0][field];
                r[opts.idField] = _idIndex++;
                topRows.push(r);
                allRows.push(r);
            });
        }
    });
    console.log(allRows);
    return allRows;

    function sumR1(rows) {
        var r = {};
        var fields = $(target).datagrid('getColumnFields');
        $.map(fields, function (field) {
            r[field] = _sum(field);
        });
        return r;

        function _sum(field) {
            var col = $(target).datagrid('getColumnOption', field);
            var rr = $.map(rows, function (row) {
                for (var i = 0; i < opts.pivot.columns.length; i++) {
                    if (row[opts.pivot.columns[i]] != col.tt[i]) {
                        return undefined;
                    }
                }
                return row;
            });
            return opts.operators[col.op || 'sum'].call(target, rr, col.tt[col.tt.length - 1]);
        }
    }

    function getR1(field, rows) {
        var result = {};
        $.map(rows, function (row) {
            var val = row[field];
            var rr = result[val];
            if (!rr) {
                rr = [row];
            } else {
                rr.push(row);
            }
            result[val] = rr;
        });
        var groups = [];
        for (var val in result) {
            groups.push(result[val]);
        }
        return groups;
    }
}