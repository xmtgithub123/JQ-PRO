"use strict"
if (typeof (YChart) == "undefined") {
    var YChart = {};
}

YChart.Grid = function (options) {
    var _width = options.width || options.container.clientWidth;
    var _height = options.height || options.container.clientHeight;
    var div = YChart.createElement("div", null, { width: (_width - 1) + "px", height: "28px", backgroundColor: "#F5F5F5", borderTop: "1px solid #EFEFEF", borderRight: "1px solid #EFEFEF", borderBottom: "1px solid #EFEFEF" });
    options.container.appendChild(div);
    this.idField = options.idField;
    this.checkbox = options.checkbox;
    this.singleChoose = options.singleChoose;
    this.choosed = [];
    var t_head = YChart.createElement("table", null, { borderCollapse: "collapse", height: "100%" });
    this.columns = options.columns;
    this.head = t_head;
    div.appendChild(t_head);
    var t_head_tr = t_head.insertRow(-1);
    if (this.checkbox == true) {
        var tc = t_head_tr.insertCell(-1);
        tc.style.width = "30px";
        tc.style.textAlign = "center";
        tc.style.cursor = "default";
        tc.style.borderLeft = "1px solid #EFEFEF";
        tc.style.borderBottom = "1px solid #EFEFEF";
        if (this.singleChoose != true) {
            var checkbox = YChart.createElement("input", { type: "checkbox" });
            tc.appendChild(checkbox);
            checkbox.onchange = function () {
                var _tb = this.parentNode.parentNode.parentNode;
                if (_tb.tagName == "TBODY") {
                    _tb = _tb.parentNode;
                }
                _tb = _tb.parentNode.nextSibling.childNodes[0];
                for (var i = 0; i < _tb.rows.length; i++) {
                    _tb.rows[i].cells[0].childNodes[0].checked = this.checked;
                    YChart.fireEvent(_tb.rows[i].cells[0].childNodes[0], "change");
                }
            };
        }
    }
    for (var i = 0; i < options.columns.length; i++) {
        var t_head_tc = t_head_tr.insertCell(-1);
        YChart.setText(t_head_tc, options.columns[i].title);
        t_head_tc.style.width = (options.columns[i].width || 30) + "px";
        t_head_tc.style.textAlign = "center";
        t_head_tc.style.cursor = "default";
        t_head_tc.style.borderLeft = "1px solid #EFEFEF";
        t_head_tc.style.borderBottom = "1px solid #EFEFEF";
    }
    div = YChart.createElement("div", null, { width: (_width - 2) + "px", height: (_height - 30) + "px", overflowY: "auto", borderLeft: "1px solid #EFEFEF", borderRight: "1px solid #EFEFEF", borderBottom: "1px solid #EFEFEF" });
    options.container.appendChild(div);
    var t_body = YChart.createElement("table", null, { borderCollapse: "collapse", tableLayout: "fixed", margin: "-1px 0px 0px -1px" });
    this.body = t_body;
    div.appendChild(t_body);
    if (options.datas && YChart.isArray(options.datas)) {
        this.datas = options.datas;
        this.setDatas(this.datas);
    }
    else {
        this.datas = [];
    }
}

YChart.Grid.prototype.setDatas = function (datas) {
    this.clear();
    for (var i = 0; i < datas.length; i++) {
        this.datas.push(datas[i]);
        var tr = this.body.insertRow(-1);
        this.fillRow(tr, datas[i]);
    }
}

//清楚表格中的内容
YChart.Grid.prototype.clear = function () {
    for (var i = 0; i < this.body.rows.length; i++) {
        this.body.deleteRow(0);
        i--;
    }
    this.datas = [];
}

//添加数据
YChart.Grid.prototype.appendData = function (data) {
    var tr = this.body.insertRow(-1);
    this.fillRow(tr, data);
    this.datas.push(data);
    return tr;
}

//插入数据
YChart.Grid.prototype.insertData = function (data, index) {
    if (index > this.body.rows.length) {
        index = -1;
    }
    var tr = this.body.insertRow(index);
    this.fillRow(tr, data);
    if (index == -1) {
        this.datas.push(data);
    }
    else {
        this.datas.splice(index, 0, data);
    }
    return tr;
}

YChart.Grid.prototype.removeRow = function (rowID) {
    for (var i = 0; i < this.datas.length; i++) {
        if (this.datas[i][this.idField] == rowID) {
            this.datas.splice(i, 1);
            for (var j = 0; j < this.choosed.length; j++) {
                if (this.choosed[j].getAttribute("rowid") == rowID.toString()) {
                    this.choosed.splice(j, 1);
                    break;
                }
            }
            for (var j = 0; j < this.body.rows.length; j++) {
                if (this.body.rows[j].getAttribute("rowid") == rowID.toString()) {
                    this.body.deleteRow(j);
                    break;
                }
            }
            break;
        }
    }
}

YChart.Grid.prototype.getRow = function (rowID) {
    for (var i = 0; i < this.body.rows.length; i++) {
        if (this.body.rows[i].getAttribute("rowid") == rowID.toString()) {
            return this.body.rows[i];
        }
    }
}

YChart.Grid.prototype.getRowData = function (rowID) {
    for (var i = 0; i < this.datas.length; i++) {
        if (this.datas[i][this.idField] == rowID) {
            return this.datas[i];
        }
    }
}

//填充行
YChart.Grid.prototype.fillRow = function (row, rowData) {
    if (this.idField && rowData[this.idField] != undefined) {
        row.setAttribute("rowid", rowData[this.idField]);
    }
    if (this.checkbox == true) {
        //显示复选框
        var tc = row.insertCell(-1);
        tc.style.height = "30px";
        tc.style.width = "30px";
        tc.style.border = "1px solid #EFEFEF";
        tc.style.textAlign = "center";
        var checkbox = YChart.createElement("input", { type: "checkbox" });
        tc.appendChild(checkbox);
        var _this = this;
        checkbox.onchange = function () {
            if (this.checked) {
                if (_this.singleChoose) {
                    for (var i = 0; i < _this.choosed.length; i++) {
                        _this.choosed[i].childNodes[0].childNodes[0].checked = false;
                        _this.choosed.splice(i, 1);
                        i--;
                    }
                }
                _this.choosed.push(this.parentNode.parentNode);
            }
            else {
                var _row = this.parentNode.parentNode;
                for (var i = 0; i < _this.choosed.length; i++) {
                    if (_this.choosed[i] == _row) {
                        _this.choosed.splice(i, 1);
                        break;
                    }
                }
            }
        }
    }
    for (var i = 0; i < this.columns.length; i++) {
        var tc = row.insertCell(-1);
        tc.style.height = "30px";
        tc.style.border = "1px solid #EFEFEF";
        var div = document.createElement("div");
        div.style.width = ((this.columns[i].width || 30) - 6) + "px";
        div.style.padding = "3px";
        div.style.overflow = "hidden";
        tc.appendChild(div);
        if (this.columns[i].field) {
            if (this.columns[i].formatter) {
                this.columns[i].formatter(div, rowData);
            }
            else {
                YChart.setText(div, rowData[this.columns[i].field]);
            }
        }
        if (this.columns[i].align) {
            div.style.textAlign = this.columns[i].align || "left";
        }
        div.style.wordBreak = "break-all";
        if (this.columns[i].create) {
            this.columns[i].create(tc, rowData);
        }
    }
}

YChart.Grid.prototype.deleteData = function (rowID) {
    if (!this.idField) {
        return;
    }
    for (var i = 0; i < this.body.rows.length; i++) {
        var _attr = this.body.rows[i].getAttribute("rowid");
        if (_attr != null && _attr == rowID) {
            this.body.deleteRow(i);
            break;
        }
    }
    for (var i = 0; i < this.datas.length; i++) {
        if (this.datas[i][this.idField] == rowID) {
            this.datas.splice(i, 1);
            break;
        }
    }
}

YChart.Grid.prototype.getRowID = function (row) {
    return row.getAttribute("rowid");
}