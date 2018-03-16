if (typeof (YChart) == "undefined") {
    YChart = {};
}
YChart.FormList = function (options) {
    var xml = YChart.loadXml(options.xmlContent);
    this.keywords = options.txtKeywords;
    this.gridColumns = [{ field: "ck", checkbox: true, align: "left", halign: "center" }];
    this.gridFields = [];
    this.conditions = [];
    this.tableName = options.tableName;
    this.initGridColumns(xml);
    this.initKeywords(xml);
    this.initConditions(xml, options.normalCondition, options.seniorCondition);
    this.referenceField = options.referenceField;
    this.referenceID = options.referenceID;
}

YChart.FormList.prototype.initKeywords = function (xml) {
    if (!this.keywords) {
        return;
    }
    var xKeywords = YChart.selectSingleNode(xml, "Root/Keywords");
    if (!xKeywords) {
        return;
    }
    if (xKeywords.getAttribute("isShow") == "0") {
        YChart.remove(this.keywords);
        delete this.keywords;
        return;
    }
    if (xKeywords.getAttribute("placeHolder")) {
        this.keywords.setAttribute("placeHolder", xKeywords.getAttribute("placeHolder"));
    }
    var xColumns = YChart.selectNodes(xKeywords, "Column");
    this.keywordsFilterColumns = [];
    for (var i = 0; i < xColumns.length; i++) {
        this.keywordsFilterColumns.push(xColumns[i].textContent);
    }
}

YChart.FormList.prototype.initGridColumns = function (xml) {
    var xColumns = YChart.selectNodes(xml, "Root/Columns/Column");
    if (xColumns.length == 0) {
        return;
    }
    //设置列
    for (var i = 0; i < xColumns.length; i++) {
        var column = {
            field: xColumns[i].getAttribute("dataField"),
            title: xColumns[i].getAttribute("name"),
            width: xColumns[i].getAttribute("width"),
            halign: "center",
            align: xColumns[i].getAttribute("textAlign")
        }
        this.gridColumns.push(column);
        this.gridFields.push({ name: column.field, formatter: xColumns[i].getAttribute("formatter"), dataType: xColumns[i].getAttribute("dataType") || "" });
    }
}

YChart.FormList.prototype.initConditions = function (xml, _normal, _senior) {
    var xConditions = YChart.selectNodes(xml, "Root/Conditions/Condition");
    if (xConditions.length == 0) {
        return;
    }
    for (var i = 0; i < xConditions.length; i++) {
        switch (xConditions[i].getAttribute("type")) {
            case "textbox":
                var txt = YChart.createElement("input", { type: "text", className: "yc_query_textbox" });
                if (xConditions[i].getAttribute("isShowInSenior") == "0") {
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, null, xConditions[i].getAttribute("name")));
                    txt.style.marginTop = "5px";
                    _normal.appendChild(txt);
                }
                else {
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { lineHeight: "26px" }, xConditions[i].getAttribute("name")));
                    _senior.appendChild(txt);
                }
                this.conditions.push({ dataField: xConditions[i].getAttribute("dataField"), type: "textbox", element: txt });
                break;
            case "checkbox":
            case "select":
            case "radio":
                var _select = YChart.createElement("select");
                _select.className = "yc_query_select";
                if (xConditions[i].getAttribute("isShowInSenior") == "0") {
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, null, xConditions[i].getAttribute("name")));
                    _normal.appendChild(_select);
                    _select.style.marginTop = "5px";
                }
                else {
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { lineHeight: "26px" }, xConditions[i].getAttribute("name")));
                    _senior.appendChild(_select);
                }
                var option = document.createElement("option");
                option.appendChild(document.createTextNode("全部"));
                _select.appendChild(option);
                var _items = YChart.selectNodes(xConditions[i], "item");
                for (var j = 0; j < _items.length; j++) {
                    option = document.createElement("option");
                    option.appendChild(document.createTextNode(_items[j].getAttribute("name")));
                    if (_items[j].getAttribute("value") == "") {
                        option.setAttribute("value", _items[j].getAttribute("name"));
                    }
                    else {
                        option.setAttribute("value", _items[j].getAttribute("value"));
                    }
                    _select.appendChild(option);
                }
                this.conditions.push({ dataField: xConditions[i].getAttribute("dataField"), type: xConditions[i].getAttribute("type"), element: _select });
                break;
            case "datebox":
                var txtMinDate = YChart.createElement("input", { type: "text", className: "yc_query_textbox yc_datebox", readonly: "readonly" }, { width: "90px" });
                var txtMaxDate = YChart.createElement("input", { type: "text", className: "yc_query_textbox yc_datebox", readonly: "readonly" }, { width: "90px" });
                if (xConditions[i].getAttribute("isShowInSenior") == "0") {
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, null, xConditions[i].getAttribute("name")));
                    txtMinDate.style.marginTop = "5px";
                    _normal.appendChild(txtMinDate);
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { marginRight: "2px", marginLeft: "4px" }, "-"));
                    txtMaxDate.style.marginTop = "5px";
                    _normal.appendChild(txtMaxDate);
                }
                else {
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { lineHeight: "26px" }, xConditions[i].getAttribute("name")));
                    _senior.appendChild(txtMinDate);
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { marginRight: "2px", marginLeft: "4px", lineHeight: "26px" }, "-"));
                    _senior.appendChild(txtMaxDate);
                }
                YChart.DateBox.init(txtMinDate, { maxDateElement: txtMaxDate });
                YChart.DateBox.init(txtMaxDate, { minDateElement: txtMinDate });
                this.conditions.push({ dataField: xConditions[i].getAttribute("dataField"), type: "datebox", minElement: txtMinDate, maxElement: txtMaxDate });
                break;
            case "numberbox":
                var txtMin = YChart.createElement("input", { type: "text", className: "yc_query_textbox" }, { width: "40px" });
                var txtMax = YChart.createElement("input", { type: "text", className: "yc_query_textbox" }, { width: "40px" });
                if (xConditions[i].getAttribute("isShowInSenior") == "0") {
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, null, xConditions[i].getAttribute("name")));
                    txtMin.style.marginTop = "5px";
                    _normal.appendChild(txtMin);
                    _normal.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { marginRight: "2px", marginLeft: "4px" }, "-"));
                    txtMax.style.marginTop = "5px";
                    _normal.appendChild(txtMax);
                }
                else {
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { lineHeight: "26px" }, xConditions[i].getAttribute("name")));
                    _senior.appendChild(txtMin);
                    _senior.appendChild(YChart.createElement("div", { className: "yc_query_text" }, { marginRight: "2px", marginLeft: "4px", lineHeight: "26px" }, "-"));
                    _senior.appendChild(txtMax);
                }
                YChart.registerNumberBox({
                    element: txtMin,
                    precision: parseInt(xConditions[i].getAttribute("precision")),
                    defaultValue: ""
                });
                YChart.registerNumberBox({
                    element: txtMax,
                    precision: parseInt(xConditions[i].getAttribute("precision")),
                    defaultValue: ""
                });
                this.conditions.push({ dataField: xConditions[i].getAttribute("dataField"), type: "numberbox", minElement: txtMin, maxElement: txtMax });
                break;
        }
    }
}

YChart.FormList.prototype.getContitionData = function () {
    var xml = YChart.loadXml("<Root></Root>");
    if (this.keywords) {
        var xKeywords = xml.createElement("Keywords");
        xml.documentElement.appendChild(xKeywords);
        var xValue = xml.createElement("Value");
        xValue.textContent = this.keywords.value;
        xKeywords.appendChild(xValue);
        var xColumns = xml.createElement("Columns");
        xKeywords.appendChild(xColumns);
        if (this.keywordsFilterColumns && this.keywordsFilterColumns.length > 0) {
            for (var i = 0; i < this.keywordsFilterColumns.length; i++) {
                var xColumn = xml.createElement("Column");
                xColumn.textContent = this.keywordsFilterColumns[i];
                xColumns.appendChild(xColumn);
            }
        }
    }
    for (var i = 0; i < this.conditions.length; i++) {
        var xCondition = xml.createElement("Condition");
        xml.documentElement.appendChild(xCondition);
        xCondition.setAttribute("dataField", this.conditions[i].dataField);
        xCondition.setAttribute("type", this.conditions[i].type);
        switch (this.conditions[i].type) {
            case "select":
            case "checkbox":
            case "radio":
                xCondition.setAttribute("text", YChart.getSelectedText(this.conditions[i].element));
                xCondition.setAttribute("value", YChart.getSelectedValue(this.conditions[i].element));
                break;
            case "textbox":
                xCondition.setAttribute("value", this.conditions[i].element.value);
                break;
            case "datebox":
                xCondition.setAttribute("minValue", this.conditions[i].minElement.value);
                xCondition.setAttribute("maxValue", this.conditions[i].maxElement.value);
                break;
            case "numberbox":
                xCondition.setAttribute("minValue", this.conditions[i].minElement.value);
                xCondition.setAttribute("maxValue", this.conditions[i].maxElement.value);
                break;
        }
    }
    if (this.referenceField && this.referenceID) {
        var xReference = xml.createElement("Condition");
        xml.documentElement.appendChild(xReference);
        xReference.setAttribute("dataField", this.referenceField);
        xReference.setAttribute("type", "reference");
        xReference.setAttribute("value", this.referenceID);
    }
    xml.documentElement.setAttribute("table", this.tableName);
    var xColumns = xml.createElement("Columns");
    xml.documentElement.appendChild(xColumns);
    for (var i = 0; i < this.gridFields.length; i++) {
        var xColumn = xml.createElement("Column");
        xColumns.appendChild(xColumn);
        xColumn.setAttribute("dataType", this.gridFields[i].dataType);
        xColumn.setAttribute("formatter", this.gridFields[i].formatter);
        xColumn.textContent = this.gridFields[i].name;
    }
    return xml.documentElement.outerHTML;
}