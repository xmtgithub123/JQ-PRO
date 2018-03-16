"use strict"
if (typeof (YChart) == "undefined") {
    YChart = {};
}
if (typeof (YChart.FormBuilder) == "undefined") {
    YChart.FormBuilder = {};
}
YChart.FormBuilder = function (options) {
    this.contentIFrame = options.contentIFrame;
    this.contentIFrameUrl = options.contentIFrameUrl;
    if (options.onSelectionChange) {
        this.onSelectionChange = options.onSelectionChange;
    }
    this.options = {};
    this.dialog = options.dialogs;
    this.api = options.api;
    this.properties = [];
    this.hiddens = [];
    this.initControls();
}

YChart.FormBuilder.prototype.readTableXml = function (xmlString) {
    if (!xmlString) {
        return;
    }
    function readXml(_this) {
        var _document = this.contentWindow.document;
        var xml = YChart.loadXml(xmlString);
        var width = xml.documentElement.getAttribute("pageWidth");
        //设置页面宽度
        var _div = _document.createElement("div");
        _div.style.backgroundColor = "#ffffff";
        _div.style.border = "1px solid #EFEFEF";
        _div.style.margin = "10px 0px 50px 10px";
        _div.style.position = "relative";
        //_div.setAttribute("contenteditable", "true");
        _div.style.outline = "none";
        _div.style.padding = "40px 60px 40px 60px";
        _div.style.float = "left";
        //if (width == "auto") {
        //    _div.style.padding = "20px";
        //}
        //else {
        //    _div.style.padding = "20px 0px 20px 0px";
        //    _div.style.width = (YChart.parseFloat(width) / 15) + "px";
        //}
        _document.body.appendChild(_div);
        var pC = _document.createElement("p", null, null, { display: "inline" });
        _div.appendChild(pC);
        var _table = _document.createElement("table");
        _table.style.margin = "auto";
        _table.style.borderCollapse = "collapse";
        _table.style.border = "1px solid #888888";
        _table.style.outline = "none";
        pC.appendChild(_table);
        var xColumns = YChart.selectNodes(xml, "Table/Columns/Column");
        if (xColumns.length > 0) {
            var _tr = _document.createElement("tr");
            _table.appendChild(_tr);
            _tr.style.visibility = "hidden";
            for (var i = 0; i < xColumns.length; i++) {
                var cell = _tr.insertCell(-1);
                cell.setAttribute("width", (YChart.parseFloat(xColumns[i].getAttribute("w")) / 15) + "px");
            }
        }
        var xTrs = YChart.selectNodes(xml, "Table/Tr");
        for (var i = 0; i < xTrs.length; i++) {
            var _tr = _document.createElement("tr");
            _table.appendChild(_tr);
            var _height = YChart.parseFloat(xTrs[i].getAttribute("height")) / 15;
            var xTcs = YChart.selectNodes(xTrs[i], "Td");
            for (var j = 0; j < xTcs.length; j++) {
                var _td = _document.createElement("td");
                _td.setAttribute("contenteditable", "true");
                //_td.setAttribute("id", "td_" + i + "_" + j);
                _td.setAttribute("ycelement", "drop_td");
                _tr.appendChild(_td);
                _td.style.border = "1px solid #444444";
                _td.style.overflow = "hidden";
                _td.style.wordBreak = "break-all";
                _td.style.outline = "none";
                var colSpan = parseInt(xTcs[j].getAttribute("colSpan")) || 1;
                if (colSpan > 1) {
                    _td.setAttribute("colSpan", colSpan);
                }
                _td.setAttribute("width", (YChart.parseFloat(xTcs[j].getAttribute("width")) / 15 + colSpan - 1) + "px");
                _td.style.height = _height + "px";
                _this.initDropElement(_td, _this);
                if (xTcs[j].getAttribute("rowSpan")) {
                    _td.setAttribute("rowSpan", parseInt(xTcs[j].getAttribute("rowSpan")));
                }
                if (xTcs[j].getAttribute("vAlign")) {
                    var vAlign = xTcs[j].getAttribute("vAlign");
                    if (vAlign == "center") {
                        _td.style.verticalAlign = "middle";
                    }
                    else {
                        _td.style.verticalAlign = vAlign;
                    }
                }
                //取出文字
                var isHasContent = false;
                var xPs = YChart.selectNodes(xTcs[j], "P");
                for (var k = 0; k < xPs.length; k++) {
                    //验证是否有span元素
                    var spans = YChart.selectNodes(xPs[k], "Span");
                    if (spans.length == 0 && xPs[k].textContent == "" && !isHasContent) {
                        continue;
                    }
                    isHasContent = true;
                    var p = _document.createElement("p");
                    //p.setAttribute("id", "p_" + i + "_" + j + "_" + k);
                    _td.appendChild(p);
                    if (xPs[k].getAttribute("textAlign")) {
                        p.style.textAlign = xPs[k].getAttribute("textAlign");
                    }
                    if (xPs[k].getAttribute("fontSize")) {
                        var fontSize = YChart.parseFloat(xPs[k].getAttribute("fontSize"));
                        if (fontSize > 0) {
                            p.style.fontSize = (fontSize * 2 / 3) + "px";
                        }
                    }
                    if (xPs[k].getAttribute("fontWeight")) {
                        p.style.fontWeight = xPs[k].getAttribute("fontWeight");
                    }
                    if (xPs[k].getAttribute("fontFamily")) {
                        p.style.fontFamily = xPs[k].getAttribute("fontFamily");
                    }
                    if (spans.length == 0) {
                        p.appendChild(document.createTextNode(xPs[k].textContent));
                    }
                    else {
                        for (var l = 0; l < spans.length; l++) {
                            var span = _document.createElement("span");
                            //span.setAttribute("id", "span_" + i + "_" + j + "_" + k + "_" + l);
                            p.appendChild(span);
                            span.appendChild(document.createTextNode(spans[l].textContent));
                            if (spans[l].getAttribute("fontSize")) {
                                var fontSize = YChart.parseFloat(spans[l].getAttribute("fontSize"));
                                if (fontSize > 0) {
                                    span.style.fontSize = (fontSize * 2 / 3) + "px";
                                }
                            }
                            if (spans[l].getAttribute("fontWeight")) {
                                span.style.fontWeight = spans[l].getAttribute("fontWeight");
                            }
                            if (spans[l].getAttribute("fontFamily")) {
                                span.style.fontFamily = spans[l].getAttribute("fontFamily");
                            }
                        }
                    }
                }
                //if (_td.childNodes.length == 0) {
                //    _td.appendChild(YChart.createElement("p", null, null, YChart.fillChar));
                //}
            }
            if (i == 0) {
                _table.setAttribute("width", _tr.offsetWidth);
            }
        }
    }
    this.initContentIframe(readXml);
}

YChart.FormBuilder.prototype.setFormData = function (html, xmlString) {
    var xmlDocument = YChart.loadXml(xmlString);
    this.options.table = xmlDocument.documentElement.getAttribute("primaryTable");
    this.options.processID = xmlDocument.documentElement.getAttribute("processTemplateID");
    this.options.processVersionID = xmlDocument.documentElement.getAttribute("processTemplateVersionID");
    var xOptionItems = YChart.selectNodes(xmlDocument, "Root/Options/Option");
    for (var i = 0; i < xOptionItems.length; i++) {
        var _name = xOptionItems[i].getAttribute("name");
        if (_name == "relation") {
            this.options[_name] = {};
            YChart.parseToObject(this.options[_name], xOptionItems[i]);
            this.options.relation = [this.options.relation.item];
        }
        else {
            this.options[_name] = xOptionItems[i].textContent;
        }
    }
    this.isUseUploadControl = xmlDocument.documentElement.getAttribute("isUseUploadControl");
    this.initContentIframe(function (_this) {
        this.contentWindow.document.body.innerHTML = html;
        //初始化事件
        YChart.recuriseChildNodes(this.contentWindow.document.body, function (element) {
            if (element.tagName && element.getAttribute("ycelement")) {
                var yc = element.getAttribute("ycelement");
                if (yc == "drop_td") {
                    _this.initDropElement(element, _this);
                }
                else {
                    var control = _this.findControl(yc);
                    if (control) {
                        control.init.call(element);
                    }
                }
            }
        });
        var xProperties = YChart.selectNodes(xmlDocument, "Root/Properties/Property");
        if (xProperties && xProperties.length > 0) {
            _this.setProperties(xProperties);
        }
        var xHiddens = YChart.selectNodes(xmlDocument, "Root/Hiddens/Hidden");
        if (xHiddens && xHiddens.length > 0) {
            for (var i = 0; i < xHiddens.length; i++) {
                var _hidden = {};
                YChart.parseToObject(_hidden, xHiddens[i]);
                _this.hiddens.push(_hidden);
            }
        }
    });
}

YChart.FormBuilder.prototype.setProperties = function (properties) {
    var _document = this.contentIFrame.contentWindow.document;
    for (var i = 0; i < properties.length; i++) {
        var property = {};
        YChart.parseToObject(property, properties[i]);
        property.element = _document.getElementById(property.elementName);
        if (!property.element) {
            continue;
        }
        delete property.elementName;
        this.properties.push(property);
    }
}

YChart.FormBuilder.prototype.initContentIframe = function (callback) {
    var _this = this;
    //清除掉原先设置的src
    this.contentIFrame.setAttribute("src", "");
    this.contentIFrame.setAttribute("src", this.contentIFrameUrl);
    this.contentIFrame.onload = function () {
        if (callback) {
            callback.call(this, _this);
        }
        this.contentWindow.document.body.onkeyup = function (ev) {
            if (ev.keyCode == 13) {
                //var selection = this.ownerDocument.defaultView.getSelection();
                //var _element = selection.extentNode;
                //if (!_element) {
                //    return;
                //}
                //if (_element.tagName) {
                //    var p = YChart.createElement("p", null, { display: "inline" }, YChart.fillChar);
                //    YChart.insertAfter(p, _element.childNodes[selection.extentOffset]);
                //    YChart.remove(_element.childNodes[selection.extentOffset]);
                //    var range = document.createRange();
                //    range.selectNodeContents(p);
                //    range.setStart(p, 0);
                //    range.setEnd(p, 0);
                //    selection.removeAllRanges();
                //    selection.addRange(range);
                //}
            }
            else if (ev.keyCode == 8 || ev.keyCode == 46) {
                var selection = this.ownerDocument.defaultView.getSelection();
                //if (!selection.anchorNode.tagName && selection.anchorNode.parentNode && selection.anchorNode.parentNode.tagName == "P" && selection.anchorNode == selection.extentNode && selection.anchorOffset == 0 && selection.extentOffset == selection.anchorNode.nodeValue.length) {
                //    var p = selection.anchorNode.parentNode;
                //    YChart.clear(p);
                //    p.appendChild(document.createTextNode(YChart.fillChar));
                //    var range = document.createRange();
                //    range.selectNodeContents(p);
                //    range.setStart(p, 0);
                //    range.setEnd(p, 0);
                //    selection.removeAllRanges();
                //    selection.addRange(range);
                //    ev.returnValue = false;
                //}
            }
        }
        this.contentWindow.document.body.onpaste = function (ev) {
            ev.preventDefault();
            var text = null;
            if (window.clipboardData && clipboardData.setData) {
                text = window.clipboardData.getData("text");
            } else {
                text = (ev.originalEvent || ev).clipboardData.getData("text/plain");
            }
            ev.target.ownerDocument.execCommand("insertText", false, text);
        }
        this.contentWindow.document.onselectionchange = function () {
            //获取出选中的范围
            var selection = this.defaultView.getSelection();
            _this.setChoosed(null);
            //console.log(selection);
            //if (selection.rangeCount == 0) {
            //    return;
            //}
            //var range = selection.getRangeAt(0);
            //if ((range.startContainer.nodeType == 3 || range.startContainer.nodeType == 1) && range.startOffset == 0 && range.endContainer.nodeType == 1 && range.endOffset == 0) {
            //    var td = YChart.FormBuilder.findTDElement(range.startContainer);
            //    if (!td) {
            //        return;
            //    }
            //    if (td.childNodes.length == 1) {
            //        var _e = range.startContainer.parentNode;
            //        range = document.createRange();
            //        range.selectNodeContents(_e);
            //        range.setStart(_e, 0);
            //        range.setEnd(_e, 1);
            //        selection.removeAllRanges();
            //        selection.addRange(range);
            //    }
            //}
            //else {
            //    var td = YChart.FormBuilder.findTDElement(range.startContainer);
            //    if (!td) {
            //        return;
            //    }
            //    if (td.childNodes.length == 0 || (td.childNodes.length == 1 && td.childNodes[0].tagName && td.childNodes[0].tagName == "BR")) {
            //        YChart.clear(td);
            //        var p = YChart.createElement("p", null, null, YChart.fillChar);
            //        td.appendChild(p);
            //        range = document.createRange();
            //        range.selectNodeContents(p);
            //        range.setStart(p, 0);
            //        range.setEnd(p, 1);
            //        selection.removeAllRanges();
            //        selection.addRange(range);
            //    }
            //    else if (td.childNodes.length == 1 && td.childNodes[0].nodeType == 1 && td.childNodes[0].tagName == "P" && td.childNodes[0].childNodes.length == 0) {
            //        td.childNodes[0].style.display = "";
            //        td.childNodes[0].appendChild(document.createTextNode(YChart.fillChar));
            //        range = document.createRange();
            //        range.selectNodeContents(td.childNodes[0]);
            //        range.setStart(td.childNodes[0], 0);
            //        range.setEnd(td.childNodes[0], 1);
            //        selection.removeAllRanges();
            //        selection.addRange(range);
            //    }
            //}
            var nodes = YChart.getSelectionNodes.call(this, selection);
            _this.selectNodes = nodes;
            if (_this.onSelectionChange) {
                var data = {};
                var computeHistory = [];
                for (var i = 0; i < nodes.length; i++) {
                    var style;
                    if (nodes[i].tagName) {
                        var isIn = false;
                        for (var j = 0; j < computeHistory.length; j++) {
                            if (computeHistory[j] == nodes[i]) {
                                isIn = true;
                                break;
                            }
                        }
                        if (isIn) {
                            continue;
                        }
                        var node = this.defaultView.getComputedStyle(nodes[i]);
                        style = this.defaultView.getComputedStyle(nodes[i]);
                        computeHistory.push(nodes[i]);
                    }
                    else {
                        var isIn = false;
                        for (var j = 0; j < computeHistory.length; j++) {
                            if (computeHistory[j] == nodes[i].parentNode) {
                                isIn = true;
                                break;
                            }
                        }
                        if (isIn) {
                            continue;
                        }
                        style = this.defaultView.getComputedStyle(YChart.FormBuilder.findStyleElement(nodes[i]));
                        computeHistory.push(nodes[i].parentNode);
                    }
                    if (i == 0) {
                        //第一次读取出原来的值
                        data.textAlign = style.textAlign;
                        //data.verticalAlign = style.verticalAlign;
                        data.fontWeight = style.fontWeight;
                        data.textDecoration = style.textDecoration;
                        data.fontStyle = style.fontStyle;
                        data.fontSize = style.fontSize;
                        data.fontFamily = style.fontFamily;
                        data.color = style.color;
                        var td = YChart.FormBuilder.findTDElement(nodes[i]);
                        if (td) {
                            if (!td.style.verticalAlign) {
                                data.verticalAlign = "start";
                            }
                            else {
                                data.verticalAlign = td.style.verticalAlign;
                            }
                        }
                    }
                    else {
                        YChart.FormBuilder.setStyle(data, style, "textAlign", "start");
                        //YChart.FormBuilder.setStyle(data, style, "verticalAlign", "start");
                        YChart.FormBuilder.setStyle(data, style, "fontWeight", "normal");
                        YChart.FormBuilder.setStyle(data, style, "textDecoration", "none");
                        YChart.FormBuilder.setStyle(data, style, "fontStyle", "normal");
                        YChart.FormBuilder.setStyle(data, style, "fontSize", "12px");
                        YChart.FormBuilder.setStyle(data, style, "fontFamily", "宋体");
                        YChart.FormBuilder.setStyle(data, style, "color", "#000000");
                        if (data.verticalAlign != "start") {
                            var td = YChart.FormBuilder.findTDElement(nodes[i]);
                            var verticalAlign;
                            if (!td || !td.style.verticalAlign) {
                                verticalAlign = "start";
                            }
                            else {
                                verticalAlign = td.style.verticalAlign;
                            }
                            if (data.verticalAlign != verticalAlign) {
                                data.verticalAlign = "start";
                            }
                        }
                    }
                }
                computeHistory = null;
                _this.onSelectionChange(data);
            }
        }
    };
}

//获取保存的数据
YChart.FormBuilder.prototype.getSaveData = function (isToXml) {
    if (!this.contentIFrame.contentWindow) {
        return;
    }
    var result = {};
    if (isToXml) {
        var xmlDocument = YChart.loadXml("<Options></Options>");
        for (var item in this.options) {
            var xItem = xmlDocument.createElement("Option");
            xItem.setAttribute("name", item);
            if (item == "relation") {
                YChart.parseToXml(xItem, this.options[item]);
                if (this.options[item].length > 0 && this.options[item][0].children && this.options[item][0].children.length > 0) {
                    xItem.setAttribute("isTreeList", "1");
                }
                else {
                    xItem.setAttribute("isTreeList", "0");
                }
            }
            else {
                xItem.textContent = this.options[item];
            }
            xmlDocument.documentElement.appendChild(xItem);
        }
        result.options = xmlDocument.documentElement.innerHTML;
        xmlDocument = YChart.loadXml("<Properties></Properties>");
        for (var i = 0; i < this.properties.length; i++) {
            if (!this.properties[i].element || !this.properties[i].element.getAttribute("id") || !this.properties[i].element.ownerDocument.getElementById(this.properties[i].element.getAttribute("id"))) {
                continue;
            }
            var xData = xmlDocument.createElement("Property");
            xmlDocument.documentElement.appendChild(xData);
            var element = this.properties[i].element;
            delete this.properties[i].element
            this.properties[i].elementName = element.getAttribute("id");
            YChart.parseToXml(xData, this.properties[i]);
            delete this.properties[i].elementName;
            this.properties[i].element = element;
        }
        result.properties = xmlDocument.documentElement.innerHTML;
        xmlDocument = YChart.loadXml("<Hiddens></Hiddens>");
        for (var i = 0; i < this.hiddens.length; i++) {
            var xData = xmlDocument.createElement("Hidden");
            xmlDocument.documentElement.appendChild(xData);
            YChart.parseToXml(xData, this.hiddens[i]);
        }
        result.hiddens = xmlDocument.documentElement.innerHTML;
    }
    else {
        //result.options = this.options;
        result.properties = [];
        for (var i = 0; i < this.properties.length; i++) {
            if (!this.properties[i].element || !this.properties[i].element.getAttribute("id")) {
                continue;
            }
            var property = YChart.clone(this.properties[i]);
            property.elementName = this.properties[i].element.getAttribute("id");
            result.properties.push(property);
        }
        result.hiddens = this.hiddens;
    }
    var lastChoosed = formBuilder.lastChoosed;
    formBuilder.setChoosed(null);
    var _body = this.contentIFrame.contentWindow.document.body;
    for (var i = 0; i < _body.childNodes.length; i++) {
        if (_body.childNodes[i].tagName && _body.childNodes[i].tagName == "DIV") {
            result.html = _body.childNodes[i].outerHTML;
            result.width = _body.childNodes[i].offsetWidth;
            break;
        }
    }
    formBuilder.setChoosed(lastChoosed);
    return result;
}

YChart.FormBuilder.prototype.setSelecctionStyle = function (styleName, value, operation) {
    if (!this.selectNodes) {
        return;
    }
    if (this.selectNodes.length == 1) {
        YChart.FormBuilder.setElementStyle(this.selectNodes[0], styleName, value, operation);
    }
    else {
        var records = [];
        for (var i = 0; i < this.selectNodes.length; i++) {
            YChart.FormBuilder.setElementStyle(this.selectNodes[i], styleName, value, operation, records);
        }
    }
}

YChart.FormBuilder.prototype.initLayoutTable = function (options) {
    var table = _document.createElement("table", { className: "y_auto_table", cellpadding: "0", cellspacing: "0" });
    this.paper.appendChild(table);
    var isMouseDown = false;
    var isMouseReleased = false;
    var tablePosition = YChart.FormBuilder.getPosition(table);
    var resize = { mode: "", target: null, initSize: 0, initX: 0, initY: 0 };
    var merge = {
        rowIndex1: -1, cellIndex1: -1, rowIndex2: -1, cellIndex2: -1, enable: false,
        reset: function () {
            if (merge.rowIndex2 == -1) {
                return;
            }
            if (merge.rowIndex1 < merge.rowIndex2) {
                for (var i = merge.rowIndex1; i <= merge.rowIndex2; i++) {
                    if (merge.cellIndex1 < merge.cellIndex2) {
                        for (var j = merge.cellIndex1; j <= merge.cellIndex2; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "";
                        }
                    }
                    else {
                        for (var j = merge.cellIndex2; j <= merge.cellIndex1; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "";
                        }
                    }
                }
            }
            else {
                for (var i = merge.rowIndex2; i <= merge.rowIndex1; i++) {
                    if (merge.cellIndex1 < merge.cellIndex2) {
                        for (var j = merge.cellIndex1; j <= merge.cellIndex2; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "";
                        }
                    }
                    else {
                        for (var j = merge.cellIndex2; j <= merge.cellIndex1; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "";
                        }
                    }
                }
            }
        },
        set: function (rowIndex, cellIndex) {
            if (rowIndex == merge.rowIndex1 && cellIndex == merge.cellIndex11) {
                return;
            }
            merge.rowIndex2 = rowIndex;
            merge.cellIndex2 = cellIndex;
            if (merge.rowIndex1 < merge.rowIndex2) {
                for (var i = merge.rowIndex1; i <= merge.rowIndex2; i++) {
                    if (merge.cellIndex1 < merge.cellIndex2) {
                        for (var j = merge.cellIndex1; j <= merge.cellIndex2; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "#EFEFEF";
                        }
                    }
                    else {
                        for (var j = merge.cellIndex2; j <= merge.cellIndex1; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "#EFEFEF";
                        }
                    }
                }
            }
            else {
                for (var i = merge.rowIndex2; i <= merge.rowIndex1; i++) {
                    if (merge.cellIndex1 < merge.cellIndex2) {
                        for (var j = merge.cellIndex1; j <= merge.cellIndex2; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "#EFEFEF";
                        }
                    }
                    else {
                        for (var j = merge.cellIndex2; j <= merge.cellIndex1; j++) {
                            table.rows[i].cells[j].style.backgroundColor = "#EFEFEF";
                        }
                    }
                }
            }
        },
        clear: function () {
            merge.reset();
            merge.rowIndex1 = -1;
            merge.cellIndex1 = -1;
            merge.rowIndex2 = -1;
            merge.cellIndex2 = -1;
        },
        merge: function () {
            if (merge.rowIndex1 < merge.rowIndex2) {
                if (merge.cellIndex1 < merge.cellIndex2) {
                    table.rows[merge.rowIndex1].cells[merge.cellIndex1].setAttribute("rowSpan", merge.rowIndex2 - merge.rowIndex1 + 1);
                    table.rows[merge.rowIndex1].cells[merge.cellIndex1].setAttribute("colSpan", merge.cellIndex2 - merge.cellIndex1 + 1);
                    for (var i = merge.rowIndex2; i >= merge.rowIndex1; i--) {
                        for (var j = merge.cellIndex2; j >= merge.cellIndex1; j--) {
                            if (i == merge.rowIndex1 && j == merge.cellIndex1) {
                                continue;
                            }
                            YChart.remove(table.rows[i].cells[j]);
                        }
                    }
                }
                else {
                    table.rows[merge.rowIndex1].cells[merge.cellIndex2].setAttribute("rowSpan", merge.rowIndex2 - merge.rowIndex1 + 1);
                    table.rows[merge.rowIndex1].cells[merge.cellIndex2].setAttribute("colSpan", merge.cellIndex1 - merge.cellIndex2 + 1);
                    for (var i = merge.rowIndex2; i >= merge.rowIndex1; i--) {
                        for (var j = merge.cellIndex1; j >= merge.cellIndex2; j--) {
                            if (i == merge.rowIndex1 && j == merge.cellIndex2) {
                                continue;
                            }
                            YChart.remove(table.rows[i].cells[j]);
                        }
                    }
                }
            }
            else {
                if (merge.cellIndex1 < merge.cellIndex2) {
                    table.rows[merge.rowIndex2].cells[merge.cellIndex1].setAttribute("rowSpan", merge.rowIndex1 - merge.rowIndex2 + 1);
                    table.rows[merge.rowIndex2].cells[merge.cellIndex1].setAttribute("colSpan", merge.cellIndex2 - merge.cellIndex1 + 1);
                    for (var i = merge.rowIndex1; i >= merge.rowIndex2; i--) {
                        for (var j = merge.cellIndex2; j >= merge.cellIndex1; j--) {
                            if (i == merge.rowIndex2 && j == merge.cellIndex1) {
                                continue;
                            }
                            YChart.remove(table.rows[i].cells[j]);
                        }
                    }
                }
                else {
                    table.rows[merge.rowIndex2].cells[merge.cellIndex2].setAttribute("rowSpan", merge.rowIndex1 - merge.rowIndex2 + 1);
                    table.rows[merge.rowIndex2].cells[merge.cellIndex2].setAttribute("colSpan", merge.cellIndex1 - merge.cellIndex2 + 1);
                    for (var i = merge.rowIndex1; i >= merge.rowIndex2; i--) {
                        for (var j = merge.cellIndex1; j >= merge.cellIndex2; j--) {
                            if (i == merge.rowIndex2 && j == merge.cellIndex2) {
                                continue;
                            }
                            YChart.remove(table.rows[i].cells[j]);
                        }
                    }
                }
            }
        }
    };
    var menu = new YChart.Menu({
        items: [
            {
                name: "mergecell",
                text: "合并单元格",
                onClick: function () {
                    merge.merge();
                }
            },
            {
                name: "setwidth", text: "尺寸", onClick: function () { }
            }
        ]
    });
    function initResze() {
        resize.mode = "";
        resize.target = null;
    }

    function setResize(mode, target) {
        resize.mode = mode;
        resize.target = target;
        if (mode == "col") {
            resize.initSize = target.clientWidth;
        }
        else if (mode == "row") {
            resize.initSize = target.clientHeight;
        }
    }

    var onLayoutTableMouseOver = function (event) {
        if (isMouseDown) {
            if (merge.enable == true) {
                merge.reset();
                merge.set(event.srcElement.parentNode.rowIndex, event.srcElement.cellIndex);
            }
            return;
        }
        var realX = event.clientX - tablePosition.x;
        var realY = event.clientY - tablePosition.y;
        if (realX - event.srcElement.offsetLeft <= 3) {
            if (event.srcElement.cellIndex == 0) {
                event.srcElement.style.cursor = "text";
                initResze();
            }
            else {
                event.srcElement.style.cursor = "col-resize";
                setResize("col", event.srcElement.previousSibling);
            }
        }
        else if (realY - event.srcElement.offsetTop <= 3) {
            if (event.srcElement.parentNode.rowIndex == 0) {
                event.srcElement.style.cursor = "text";
                initResze();
            }
            else {
                event.srcElement.style.cursor = "row-resize";
                setResize("row", event.srcElement.parentNode.previousSibling.cells[event.srcElement.cellIndex]);
            }
        }
        else if (realX - (event.srcElement.offsetLeft + event.srcElement.clientWidth) >= -3) {
            event.srcElement.style.cursor = "col-resize";
            setResize("col", event.srcElement);
        }
        else if (realY - (event.srcElement.offsetTop + event.srcElement.clientHeight) >= -3) {
            event.srcElement.style.cursor = "row-resize";
            setResize("row", event.srcElement);
        }
        else {
            event.srcElement.style.cursor = "text";
            initResze();
        }
    }
    var _paper = this.paper;
    var onLayoutTableMouseDown = function (event) {
        if (event.which != 1) {
            return;
        }
        isMouseReleased = false;
        YChart.attachEvent(_paper, "mouseup", onCheckLayoutMouseUpOrLeave);
        setTimeout(function () {
            if (isMouseReleased) {
                return;
            }
            isMouseDown = true;
            if (resize.mode != "") {
                resize.initX = event.clientX;
                resize.initY = event.clientY;
                YChart.attachEvent(_paper, "mousemove", onPaperMouseMove);
            }
            else {
                //拖动时选中单元格
                merge.rowIndex1 = event.srcElement.parentNode.rowIndex;
                merge.cellIndex1 = event.srcElement.cellIndex;
                merge.enable = true;
            }
            YChart.attachEvent(_paper, "mouseup", onLayoutMouseUpOrLeave);
            YChart.attachEvent(_paper, "mouseleave", onLayoutMouseUpOrLeave);
        }, 100);
    }

    var onPaperMouseMove = function (event) {
        if (resize.mode == "col") {
            var _width = resize.initSize + event.clientX - resize.initX;
            if (_width > 50) {
                var _table = resize.target.parentNode.parentNode;
                for (var i = 0; i < _table.rows.length; i++) {
                    _table.rows[i].cells[resize.target.cellIndex].style.width = _width + "px";
                }
            }
        }
        else if (resize.mode == "row") {
            var _height = resize.initSize + event.clientY - resize.initY;
            if (_height > 15) {
                var _table = resize.target.parentNode.parentNode;
                var rowIndex = resize.target.parentNode.rowIndex;
                for (var i = 0; i < _table.rows[rowIndex].cells.length; i++) {
                    _table.rows[rowIndex].cells[i].style.height = _height + "px";
                }
            }
        }
    }

    var onLayoutMouseUpOrLeave = function (event) {
        isMouseDown = false;
        if (resize.mode != "") {
            initResze();
            YChart.removeEvent(this, "mousemove", onPaperMouseMove);
        }
        else if (merge.enable) {
            merge.enable = false;
            YChart.attachEvent(this, "mousedown", clearMerge);
        }
        YChart.removeEvent(this, "mouseup", onLayoutMouseUpOrLeave);
        YChart.removeEvent(this, "mouseleave", onLayoutMouseUpOrLeave);
    }

    var onCheckLayoutMouseUpOrLeave = function () {
        isMouseReleased = true;
        YChart.removeEvent(this, "mouseup", onCheckLayoutMouseUpOrLeave);
    }

    var clearMerge = function (event) {
        if (event.which != 1) {
            return;
        }
        merge.clear();
        YChart.removeEvent(this, "mousedown", clearMerge);
    }

    for (var i = 0; i < options.rows; i++) {
        var tr = table.insertRow(-1);
        for (var j = 0; j < options.columns; j++) {
            var td = tr.insertCell(-1);
            td.style.height = YChart.FormBuilder.getHeight(options.rowHeight) + "px";
            td.style.width = YChart.FormBuilder.getWidth(options.columnWidth) + "px";
            td.setAttribute("contenteditable", "true");
            td.setAttribute("spellcheck", "false");
            YChart.attachEvent(td, "mousemove", onLayoutTableMouseOver);
            YChart.attachEvent(td, "mousedown", onLayoutTableMouseDown);
            YChart.attachEvent(td, "contextmenu", function (event) {
                YChart.preventDefault(event);
                menu.show({ x: event.clientX, y: event.clientY });
            });
        }
    }

}

YChart.FormBuilder.prototype.initDragElements = function (elements) {
    for (var i = 0; i < elements.length; i++) {
        elements[i].setAttribute("draggable", true);
        YChart.attachEvent(elements[i], "dragstart", function (ev) {
            ev.effectAllowed = "none";
            ev.dataTransfer.setData("Text", (ev.srcElement || ev.target).getAttribute("control"));
        });
    }
}

YChart.FormBuilder.prototype.initDropElement = function (element, _this) {
    //YChart.attachEvent(element, "dragover", function (ev) {
    //    ev.preventDefault();
    //});
    YChart.attachEvent(element, "drop", function (ev) {
        if (YChart.FormBuilder.isDropElement(ev.target)) {
            ev.dataTransfer.clearData("Text");
            return;
        }
        var control = _this.findControl(ev.dataTransfer.getData("Text"));
        if (control) {
            var dropui = control.drop(ev.target, ev);
            if (dropui) {
                control.init.call(dropui);
            }
        }
        ev.dataTransfer.clearData("Text");
        ev.preventDefault();
    });
}

//产生不重复的控件ID
YChart.FormBuilder.prototype.generateID = function (element, prefix) {
    var i = 1;
    while (element.ownerDocument.getElementById(prefix + i.toString())) {
        i++;
    }
    element.setAttribute("id", prefix + i.toString());
    element.setAttribute("name", prefix + i.toString());
    this.removeProperty(prefix + i.toString());
}

//设置当前选中的控件
YChart.FormBuilder.prototype.setChoosed = function (element) {
    if (this.lastChoosed) {
        for (var i = 0; i < this.lastChoosed.length; i++) {
            YChart.removeClass(this.lastChoosed[i], "yc_choosed");
        }
    }
    if (element) {
        if (YChart.isArray(element)) {
            for (var i = 0; i < element.length; i++) {
                YChart.addClass(element[i], "yc_choosed");
            }
            this.lastChoosed = element;
        }
        else {
            YChart.addClass(element, "yc_choosed");
            this.lastChoosed = [element];
        }
    }
    else {
        this.lastChoosed = null;
    }
}

//移除当前选中的控件
YChart.FormBuilder.prototype.removeChoosed = function () {
    for (var i = 0; i < this.lastChoosed.length; i++) {
        for (var j = 0; j < this.properties.length; j++) {
            if (this.properties[j].element == this.lastChoosed[i]) {
                this.properties.splice(j, 1);
                break;
            }
        }
        this.lastChoosed[i].parentNode.removeChild(this.lastChoosed[i]);
    }
}

YChart.FormBuilder.prototype.getPropertyDefault = function (element) {
    var result;
    for (var i = 0; i < this.properties.length; i++) {
        if (this.properties[i].element == element) {
            result = this.properties[i];
            break;
        }
    }
    var tableName = "";
    var storageMode = "";
    if (result.inlineTableName) {
        //获取出所属的inlinetable
        for (var i = 0; i < this.properties.length; i++) {
            if (this.properties[i].element.getAttribute("id") == result.inlineTableName) {
                tableName = this.properties[i].table;
                storageMode = this.properties[i].storageMode;
                break;
            }
        }
    }
    if (!tableName) {
        tableName = this.options.table;
        storageMode = this.options.storageMode;
    }
    if (!result) {
        result = { element: element };
    }
    return { source: result, columns: this.api.getColumns(storageMode, tableName), processTemplateID: this.options.processID, processTemplateVersionID: this.options.processVersionID };
}

YChart.FormBuilder.prototype.isExistsDefault = function (element, name, isHidden) {
    if (!isHidden) {
        for (var i = 0; i < this.hiddens.length; i++) {
            if (this.hiddens[i].name == name) {
                return false;
            }
        }
    }
    var temp = element.ownerDocument.getElementById(name);
    if (!temp || temp == element) {
        return false;
    }
    else {
        return true;
    }
}

//移除属性
YChart.FormBuilder.prototype.removeProperty = function (elementName) {
    for (var i = 0; i < this.properties.length; i++) {
        if (this.properties[i].element.getAttribute("name") == elementName || this.properties[i].elementName == elementName) {
            this.properties.splice(i, 1);
            break;
        }
    }
}

YChart.FormBuilder.prototype.setPropertyDefault = function (element, property) {
    var isIn = false;
    for (var i = 0; i < this.properties.length; i++) {
        if (this.properties[i].element == element) {
            isIn = true;
            property.element = element;
            property.type = this.properties[i].type;
            if (this.properties[i].inlineTableName) {
                property.inlineTableName = this.properties[i].inlineTableName;
            }
            this.properties[i] = property;
            break;
        }
    }
    if (!isIn) {
        property.element = element;
        this.properties.push(property);
    }
}

//初始化控件集
YChart.FormBuilder.prototype.initControls = function () {
    this.controls = [];
    var _this = this;
    function getWidth(_element) {
        var width = _element.clientWidth - 40;
        if (width < 40) {
            width = 40;
        }
        return width;
    }

    function setInlineTable(_parent, _property) {
        var inlineTableName = YChart.FormBuilder.getInlineTableName(_parent);
        if (inlineTableName) {
            _property.inlineTableName = inlineTableName;
        }
    }
    this.registerControl("label", {
        drop: function (parent) {
            var label = YChart.createElement("span", { ycelement: "label", allowdrop: "0", contenteditable: "false" }, { display: "inline-table", cursor: "default", WebkitUserSelect: "none", border: "1px solid transparent", marginLeft: "5px" }, "[ ]");
            parent.appendChild(label);
            YChart.FormBuilder.removeRoundEmptyElement(label);
            _this.generateID(label, "label");
            var property = {
                element: label,
                type: "label",
            }
            setInlineTable(parent, property);
            _this.properties.push(property);
            return label;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.label;
                var label = this;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(label);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(label, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            label.setAttribute("name", data.name);
                            label.setAttribute("id", data.name);
                        }
                        YChart.clear(label);
                        if (data.property.text == "") {
                            label.appendChild(document.createTextNode("[ ]"));
                        }
                        else {
                            label.appendChild(document.createTextNode(data.property.text));
                        }
                        label.style.textIndent = data.style.textIndent + "px";
                        label.style.margin = data.style.margin;
                        _this.setPropertyDefault(label, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("textbox", {
        drop: function (parent) {
            var txt = YChart.createElement("input", { type: "text", className: "yc_textbox", readonly: "readonly", ycelement: "textbox", allowdrop: "0" }, { width: getWidth(parent) + "px", marginLeft: "5px" });
            parent.appendChild(txt);
            YChart.FormBuilder.removeRoundEmptyElement(txt);
            _this.generateID(txt, "textbox");
            var property = {
                element: txt,
                type: "text"
            }
            setInlineTable(parent, property);
            _this.properties.push(property);
            return txt;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.textbox;
                var textbox = this;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(textbox);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(textbox, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            textbox.setAttribute("name", data.name);
                            textbox.setAttribute("id", data.name);
                        }
                        textbox.style.width = data.style.width + "px";
                        textbox.style.padding = data.style.padding;
                        textbox.style.margin = data.style.margin;
                        _this.setPropertyDefault(textbox, data.property);
                    }
                }
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("textarea", {
        drop: function (parent) {
            var height = parent.clientHeight - 40;
            if (height < 40) {
                height = 40;
            }
            var ta = YChart.createElement("textarea", { className: "yc_textbox", readonly: "readonly", ycelement: "textarea", allowdrop: "0" }, { width: getWidth(parent) + "px", height: height + "px", marginLeft: "5px" });
            parent.appendChild(ta);
            YChart.FormBuilder.removeRoundEmptyElement(ta);
            _this.generateID(ta, "textarea");
            var property = {
                element: ta,
                type: "textarea"
            }
            setInlineTable(parent, property);
            _this.properties.push(property);
            return ta;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.textarea;
                var textarea = ev.target;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(textarea);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(textarea, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            textarea.setAttribute("name", data.name);
                            textarea.setAttribute("id", data.name);
                        }
                        textarea.style.width = data.style.width + "px";
                        textarea.style.height = data.style.height + "px";
                        textarea.style.padding = data.style.padding;
                        textarea.style.margin = data.style.margin;
                        _this.setPropertyDefault(textarea, data.property);
                    }
                }
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("radio", {
        drop: function (parent) {
            var nameIndex = 0;
            while (parent.ownerDocument.getElementsByName("radio_" + nameIndex).length > 0) {
                nameIndex++;
            }
            var div = YChart.createElement("div", { contenteditable: "false", ycelement: "radio", allowdrop: "0" }, { position: "relative", display: "inline-table", border: "1px solid transparent", marginLeft: "5px", WebkitUserSelect: "none" });
            parent.appendChild(div);
            if (parent.tagName && parent.tagName == "P") {
                parent.style.display = "inline";
            }
            YChart.FormBuilder.removeRoundEmptyElement(div);
            _this.generateID(div, "radio");
            var property = {
                element: div, type: "radio", groupName: "radio_" + nameIndex, items: [{ id: 1, name: "选项1", value: "1", isChecked: "1" }, { id: 2, name: "选项2", value: "2", isChecked: "0" }], rowAmount: 0
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            YChart.FormBuilder.generateRadios(div, property);
            return div;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.radio;
                var radio = ev.target;
                while (radio.getAttribute("ycelement") != "radio" && radio.nodeType == 1 && radio.tagName != "BODY") {
                    radio = radio.parentNode;
                }
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(radio);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(radio, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            radio.setAttribute("name", data.name);
                            radio.setAttribute("id", data.name);
                        }
                        YChart.FormBuilder.generateRadios(radio, data.property);
                        radio.style.margin = data.style.margin;
                        _this.setPropertyDefault(radio, data.property);
                    }
                }
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("checkbox", {
        drop: function (parent) {
            var nameIndex = 0;
            while (parent.ownerDocument.getElementsByName("checkbox_" + nameIndex).length > 0) {
                nameIndex++;
            }
            var div = YChart.createElement("div", { contenteditable: "false", ycelement: "checkbox", allowdrop: "0" }, { position: "relative", display: "inline-table", border: "1px solid transparent", marginLeft: "5px", WebkitUserSelect: "none" });
            parent.appendChild(div);
            if (parent.tagName && parent.tagName == "P") {
                parent.style.display = "inline";
            }
            YChart.FormBuilder.removeRoundEmptyElement(div);
            _this.generateID(div, "checkbox");
            var property = {
                element: div, groupName: "checkbox_" + nameIndex, type: "checkbox", items: [{ id: 1, name: "选项1", value: "1", isChecked: "0" }, { id: 2, name: "选项2", value: "2", isChecked: "0" }], rowAmount: 0
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            YChart.FormBuilder.generateCheckBoxes(div, property);
            return div;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.checkbox;
                var checkbox = ev.target;
                while (checkbox.getAttribute("ycelement") != "checkbox" && checkbox.nodeType == 1 && checkbox.tagName != "BODY") {
                    checkbox = checkbox.parentNode;
                }
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(checkbox);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(checkbox, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            checkbox.setAttribute("name", data.name);
                            checkbox.setAttribute("id", data.name);
                        }
                        YChart.FormBuilder.generateCheckBoxes(checkbox, data.property);
                        checkbox.style.margin = data.style.margin;
                        _this.setPropertyDefault(checkbox, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("select", {
        drop: function (parent) {
            var _select = YChart.createElement("select", { className: "yc_select", ycelement: "select", allowdrop: "0" }, { marginLeft: "5px" });
            parent.appendChild(_select);
            YChart.FormBuilder.removeRoundEmptyElement(_select);
            _this.generateID(_select, "select");
            var property = {
                element: _select,
                type: "select",
                items: [{ id: 1, name: "选项1", value: "1", isSelected: "0" }, { id: 2, name: "选项2", value: "2", isSelected: "0" }]
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            YChart.FormBuilder.generateSelect(_select, property);
            return _select;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.select;
                var select = ev.target;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(select);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(select, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            select.setAttribute("name", data.name);
                            select.setAttribute("id", data.name);
                        }
                        select.style.width = data.style.width + "px";
                        select.style.margin = data.style.margin;
                        YChart.FormBuilder.generateSelect(select, data.property);
                        _this.setPropertyDefault(select, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("datebox", {
        drop: function (parent) {
            var txt = YChart.createElement("input", { type: "text", className: "yc_textbox yc_datebox", readonly: "readonly", ycelement: "datebox", allowdrop: "0" }, { width: getWidth(parent) + "px", marginLeft: "5px" });
            parent.appendChild(txt);
            YChart.FormBuilder.removeRoundEmptyElement(txt);
            _this.generateID(txt, "datebox");
            var property = {
                element: txt,
                type: "datebox"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            return txt;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.datebox;
                var datebox = ev.target;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(datebox);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(datebox, name);
                    },
                    getDateBoxes: function () {
                        var result = [];
                        for (var i = 0; i < _this.properties.length; i++) {
                            if (_this.properties[i].element && typeof (_this.properties[i].element.getAttribute) == "function" && (_this.properties[i].element == datebox || _this.properties[i].element.getAttribute("ycelement") != "datebox")) {
                                continue;
                            }
                            result.push(_this.properties[i].element.getAttribute("id"));
                        }
                        return result;
                    },
                    saveData: function (data) {
                        if (data.name) {
                            datebox.setAttribute("name", data.name);
                            datebox.setAttribute("id", data.name);
                        }
                        datebox.style.width = data.style.width + "px";
                        datebox.style.padding = data.style.padding;
                        datebox.style.margin = data.style.margin;
                        _this.setPropertyDefault(datebox, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("numberbox", {
        drop: function (parent) {
            var txt = YChart.createElement("input", { type: "text", className: "yc_textbox", readonly: "readonly", ycelement: "numberbox", allowdrop: "0" }, { width: getWidth(parent) + "px", marginLeft: "5px", textAlign: "right", WebkitUserSelect: "none" });
            parent.appendChild(txt);
            YChart.FormBuilder.removeRoundEmptyElement(txt);
            txt.value = "0";
            _this.generateID(txt, "numberbox");
            var property = {
                element: txt,
                type: "numberbox"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            return txt;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.numberbox;
                var numberbox = ev.target;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(numberbox);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(numberbox, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            numberbox.setAttribute("name", data.name);
                            numberbox.setAttribute("id", data.name);
                        }
                        numberbox.style.width = data.style.width + "px";
                        numberbox.style.padding = data.style.padding;
                        numberbox.style.margin = data.style.margin;
                        var str = "0";
                        for (var i = 0; i < data.property.precision; i++) {
                            if (i == 0) {
                                str += ".";
                            }
                            str += "0";
                        }
                        numberbox.value = str;
                        _this.setPropertyDefault(numberbox, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("panel", {
        drop: function (parent, ev) {
            var _td;
            if (YChart.FormBuilder.isPanelElement(parent)) {
                _td = YChart.FormBuilder.findTDElement(parent);
                if (!_td) {
                    ev.dataTransfer.clearData("Text");
                    ev.preventDefault();
                    return;
                }
            }
            var width;
            if (_td) {
                width = getWidth(_td);
            }
            else {
                width = getWidth(parent);
            }
            var panel = YChart.createElement("div", { className: "yc_control_panel", ycelement: "panel" }, { border: "1px dashed #BBBBBB", width: width + "px", height: "auto", overflow: "hidden", marginLeft: "5px 0px 5px 5px", position: "relative" });
            if (_td) {
                _td.appendChild(panel);
            }
            else {
                parent.appendChild(panel);
            }
            var p = document.createElement("p");
            panel.appendChild(p);
            p.appendChild(document.createTextNode(YChart.fillChar));
            YChart.FormBuilder.removeRoundEmptyElement(panel);
            _this.generateID(panel, "panel");
            var property = {
                element: panel,
                type: "panel"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            return panel;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                //修复事件莫名丢失的问题
                var ye = ev.target.getAttribute("ycelement");
                if (ye == "label" || ye == "textbox" || ye == "textarea" || ye == "radio" || ye == "checkbox" || ye == "select" || ye == "datebox" || ye == "numberbox") {
                    _this.findControl(ye).init.call(ev.target);
                    ev.target.click();
                }
                if (ye && ye != "panel") {
                    YChart.stopBubble(ev);
                    return;
                }
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                if (ev.target.getAttribute("ycelement") != "panel") {
                    return;
                }
                var options = _this.dialog.panel;
                var panel = ev.target;
                options.api = {
                    getSource: function () {
                        return { element: panel };
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(panel, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            panel.setAttribute("name", data.name);
                            panel.setAttribute("id", data.name);
                        }
                        panel.style.width = data.style.width + "px";
                        panel.style.padding = data.style.padding;
                        panel.style.margin = data.style.margin;
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("table", {
        drop: function (parent, ev) {
            var _td;
            if (YChart.FormBuilder.isTableElement(parent)) {
                _td = YChart.FormBuilder.findTDElement(parent);
                if (!_td) {
                    ev.dataTransfer.clearData("Text");
                    ev.preventDefault();
                    return;
                }
            }
            var width;
            if (_td) {
                width = _td.clientWidth - 20;
                if (width < 40) {
                    width = 40;
                }
            }
            else {
                width = getWidth(parent);
            }
            var table = YChart.createElement("div", { className: "yc_control_panel", ycelement: "table", contenteditable: "false" }, { border: "2px dashed #BBBBBB", minWidth: "100px", minHeight: "30px", height: "auto", overflow: "hidden", margin: "5px 0px 5px 5px", position: "relative", display: "inline-block" });
            if (_td) {
                _td.appendChild(table);
            }
            else {
                parent.appendChild(table);
            }
            YChart.FormBuilder.removeRoundEmptyElement(table);
            _this.generateID(table, "table");
            var property = {
                element: table,
                columns: [],
                buttons: ["add", "delete"],
                type: "table"
            };
            YChart.FormBuilder.generateTable(table, property.buttons);
            setInlineTable(parent, property);
            _this.properties.push(property);
            return table;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.table;
                var table = ev.target;
                while (table.getAttribute("ycelement") != "table" && table.nodeType == 1 && table.tagName != "BODY") {
                    table = table.parentNode;
                }
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(table);
                    },
                    getDataTables: function (storageMode) {
                        return _this.api.getTables(storageMode);
                    },
                    getDataColumns: function (storageMode, tableName) {
                        return _this.api.getColumns(storageMode, tableName);
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(table, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            var lastName = table.getAttribute("name");
                            if (lastName != data.name) {
                                table.setAttribute("name", data.name);
                                table.setAttribute("id", data.name);
                                for (var i = 0; i < _this.properties.length; i++) {
                                    if (_this.properties[i].inlineTableName == lastName) {
                                        _this.properties[i].inlineTableName = data.name;
                                    }
                                }
                            }
                        }
                        table.style.margin = data.style.margin;
                        _this.setPropertyDefault(table, data.property);
                        var _divTitle = table.childNodes[0];
                        YChart.clear(_divTitle);
                        _divTitle.appendChild(document.createTextNode(data.property.title));
                        var _table = table.childNodes[2];
                        //设置表头和表宽度
                        for (var i = 0; i < data.property.columns.length; i++) {
                            var cell;
                            for (var j = 0; j < _table.rows[0].cells.length; j++) {
                                if (_table.rows[0].cells[j].getAttribute("columnid") == data.property.columns[i].id.toString()) {
                                    cell = _table.rows[0].cells[j];
                                    break;
                                }
                            }
                            if (!cell) {
                                continue;
                            }
                            YChart.clear(cell);
                            cell.appendChild(document.createTextNode(data.property.columns[i].name));
                            cell.style.width = data.property.columns[i].width + "px";
                        }
                        YChart.FormBuilder.generateTableToolbar(_table, data.property.buttons);
                    },
                    addColumn: function (data) {
                        //添加新列
                        var _table = table.childNodes[2];
                        var _tr0 = _table.rows[0];
                        var _tr1 = _table.rows[1];
                        var _td = document.createElement("th");
                        _tr0.appendChild(_td);
                        _td.setAttribute("columnid", data.id);
                        _td.style.width = data.width + "px";
                        if (data.name) {
                            _td.appendChild(document.createTextNode(data.name));
                        }
                        _td = document.createElement("td");
                        _tr1.appendChild(_td);
                        _td.setAttribute("contenteditable", "true");
                        _td.setAttribute("ycelement", "drop_inlinetd");
                        //修改属性
                        _td.style.backgroundColor = "#FFFFFF";
                        _this.getPropertyDefault(table).source.columns.push(data);
                        //显示选择框
                        if (!_table.getAttribute("showcheckbox")) {
                            _table.setAttribute("showcheckbox", "1");
                            _td = document.createElement("th");
                            YChart.insertBefore(_td, _tr0.cells[0]);
                            _td.appendChild(YChart.createElement("input", { type: "checkbox" }));
                            _td.style.textAlign = "center";
                            _td.style.width = "30px";
                            _td = document.createElement("td");
                            YChart.insertBefore(_td, _tr1.cells[0]);
                            _td.style.textAlign = "center";
                            _td.appendChild(YChart.createElement("input", { type: "checkbox" }));
                        }
                        YChart.FormBuilder.generateTableToolbar(_table);
                    },
                    removeColumn: function (columnID) {
                        var _table = table.childNodes[2];
                        var _tr0 = _table.rows[0];
                        for (var i = 0; i < _tr0.cells.length; i++) {
                            if (_tr0.cells[i].getAttribute("columnid") == columnID) {
                                _tr0.deleteCell(i);
                                _table.rows[1].deleteCell(i);
                                break;
                            }
                        }
                        var columns = _this.getPropertyDefault(table).source.columns;
                        for (var i = 0; i < columns.length; i++) {
                            if (columns[i].id == columnID) {
                                columns.splice(i, 1);
                                break;
                            }
                        }
                        if (_tr0.cells.length == 1) {
                            YChart.clear(_tr0);
                            YChart.clear(_table.rows[1]);
                            _table.removeAttribute("showcheckbox");
                        }
                        YChart.FormBuilder.generateTableToolbar(_table);
                    },
                    moveUpColumn: function (data) {
                        var _table = table.childNodes[1];
                        var _tr0 = _table.rows[0];
                        for (var i = 0; i < _tr0.cells.length; i++) {
                            if (_tr0.cells[i].getAttribute("columnid") == data.id.toString()) {
                                if (i == 0) {
                                    return;
                                }
                                var _newCell = document.createElement("th");
                                _newCell.setAttribute("columnid", data.id.toString());
                                YChart.insertBefore(_newCell, _tr0.cells[i - 1]);
                                _newCell.appendChild(document.createTextNode(data.name));
                                _newCell.style.width = data.width + "px";
                                _newCell = _table.rows[1].insertCell(i - 1);
                                _newCell.setAttribute("contenteditable", "true");
                                _newCell.setAttribute("ycelement", "drop_inlinetd");
                                _newCell.style.backgroundColor = "#FFFFFF";
                                for (var j = 0; j < _table.rows[1].cells[i + 1].childNodes.length; j++) {
                                    _newCell.appendChild(_table.rows[1].cells[i + 1].childNodes[j]);
                                }
                                _tr0.deleteCell(i + 1);
                                _table.rows[1].deleteCell(i + 1);
                                var columns = _this.getPropertyDefault(table).source.columns;
                                for (var j = 0; j < columns.length; j++) {
                                    if (columns[j].id == data.id) {
                                        if (j == 0) {
                                            break;
                                        }
                                        columns.splice(j, 1);
                                        columns.splice(j - 1, 0, data);
                                        break;
                                    }
                                }
                                return;
                            }
                        }
                    },
                    moveDownColumn: function (data) {
                        var _table = table.childNodes[1];
                        var _tr0 = _table.rows[0];
                        for (var i = 0; i < _tr0.cells.length; i++) {
                            if (_tr0.cells[i].getAttribute("columnid") == data.id.toString()) {
                                if (i == _tr0.cells.length - 1) {
                                    return;
                                }
                                var _newCell = document.createElement("th");
                                _newCell.setAttribute("columnid", data.id.toString());
                                YChart.insertAfter(_newCell, _tr0.cells[i + 1]);
                                _newCell.appendChild(document.createTextNode(data.name));
                                _newCell.style.width = data.width + "px";
                                _newCell = _table.rows[1].insertCell(i + 1);
                                _newCell.setAttribute("contenteditable", "true");
                                _newCell.setAttribute("ycelement", "drop_inlinetd");
                                _newCell.style.backgroundColor = "#FFFFFF";
                                for (var j = 0; j < _table.rows[1].cells[i].childNodes.length; j++) {
                                    _newCell.appendChild(_table.rows[1].cells[i].childNodes[j]);
                                }
                                _tr0.deleteCell(i);
                                _table.rows[1].deleteCell(i);
                                var columns = _this.getPropertyDefault(table).source.columns;
                                for (var j = 0; j < columns.length; j++) {
                                    if (columns[j].id == data.id) {
                                        if (j == columns.length - 1) {
                                            break;
                                        }
                                        columns.splice(j, 1);
                                        columns.splice(j + 1, 0, data);
                                        break;
                                    }
                                }
                                return;
                            }
                        }
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("map", {
        drop: function (parent, ev) {
            var _td;
            if (YChart.FormBuilder.isPanelElement(parent)) {
                _td = YChart.FormBuilder.findTDElement(ev.target);
                if (!_td) {
                    ev.dataTransfer.clearData("Text");
                    ev.preventDefault();
                    return;
                }
            }
            var width;
            if (_td) {
                width = getWidth(_td);
            }
            else {
                width = getWidth(parent);
            }
            var map = YChart.createElement("div", { className: "yc_control_panel", ycelement: "map" }, { border: "1px dashed #BBBBBB", width: width + "px", height: "auto", overflow: "hidden", marginLeft: "5px", position: "relative" });
            var txt = YChart.createElement("input", { type: "text", className: "yc_textbox", readonly: "readonly", allowdrop: "0" }, { width: (width - 10) + "px" });
            var img = YChart.createElement("img", { src: "http://api.map.baidu.com/staticimage/v2?ak=3uTjQV5cdGbZlngI7Q8m3ijpoW4zgYfO&center=120.06496,31.723543&markers=120.06496,31.723543&markerStyles=m&width=" + width + "&height=460&zoom=16&copyright=1" });
            var allmap = YChart.createElement("div", { className: "yc_control_allmap", marginTop: "5px" });
            allmap.appendChild(img);
            map.appendChild(txt);
            map.appendChild(YChart.createElement("input", { type: "hidden" }));
            map.appendChild(allmap);
            if (_td) {
                _td.appendChild(map);
            }
            else {
                parent.appendChild(map);
            }
            YChart.FormBuilder.removeRoundEmptyElement(map);
            _this.generateID(map, "map");
            _this.generateID(txt, "mapsuggest");
            _this.generateID(allmap, "mapspace");
            var property = {
                element: map,
                type: "map"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            return map;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.map;
                var map = ev.target;
                while (map.getAttribute("ycelement") != "map" && map.nodeType == 1 && map.tagName != "BODY") {
                    map = map.parentNode;
                }
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(map);;
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(map, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            map.setAttribute("name", data.name);
                            map.setAttribute("id", data.name);
                        }
                        map.style.width = data.style.width + "px";
                        map.childNodes[0].style.width = (data.style.width - 10) + 'px';
                        map.style.padding = data.style.padding;
                        map.style.margin = data.style.margin;
                        _this.setPropertyDefault(map, data.property);
                    }
                };
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
    this.registerControl("uploader", {
        drop: function (parent, ev) {
            var _parent;
            if (YChart.FormBuilder.isTableElement(parent)) {
                _parent = YChart.formBuilder.findTDElement(parent);
                if (!_parent) {
                    ev.dataTransfer.clearData("Text");
                    ev.preventDefault();
                    return;
                }
                else {
                    parent = _parent;
                }
            }
            var uploader = YChart.createElement("div", { className: "yc_control_panel", ycelement: "uploader", contenteditable: "false" }, { border: "2px dashed #BBBBBB", minWidth: "100px", width: getWidth(parent) + "px", minHeight: "30px", height: "auto", overflow: "hidden", margin: "5px 0px 5px 5px", position: "relative", display: "inline-block" });
            parent.appendChild(uploader);
            YChart.FormBuilder.removeRoundEmptyElement(uploader);
            _this.generateID(uploader, "uploader");
            var property = {
                element: uploader,
                type: "uploader"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            YChart.FormBuilder.generateUploader(uploader, property);
            return uploader;
        },
        init: function () {

        }
    });
    this.registerControl("signbox", {
        drop: function (parent, ev) {
            var _td;
            if (YChart.FormBuilder.isPanelElement(parent)) {
                _td = YChart.FormBuilder.findTDElement(parent);
                if (!_td) {
                    ev.dataTransfer.clearData("Text");
                    ev.preventDefault();
                    return;
                }
            }
            var signbox = YChart.createElement("div", { className: "yc_control_panel", ycelement: "signbox", contenteditable: "false" }, { border: "2px dashed #BBBBBB", minWidth: "50px", minHeight: "50px", height: "auto", overflow: "hidden", marginLeft: "5px 0px 5px 5px", position: "relative", display: "inline-block" });
            if (_td) {
                _td.appendChild(signbox);
            }
            else {
                parent.appendChild(signbox);
            }
            YChart.FormBuilder.removeRoundEmptyElement(signbox);
            _this.generateID(signbox, "signbox");
            var property = {
                element: signbox,
                type: "signbox"
            };
            setInlineTable(parent, property);
            _this.properties.push(property);
            return signbox;
        },
        init: function () {
            YChart.attachEvent(this, "click", function (ev) {
                _this.setChoosed(this);
                YChart.stopBubble(ev);
            });
            YChart.attachEvent(this, "dblclick", function (ev) {
                var options = _this.dialog.signbox;
                var signbox = ev.target;
                options.api = {
                    getSource: function () {
                        return _this.getPropertyDefault(signbox);;
                    },
                    isExists: function (name) {
                        return _this.isExistsDefault(signbox, name);
                    },
                    saveData: function (data) {
                        if (data.name) {
                            signbox.setAttribute("name", data.name);
                            signbox.setAttribute("id", data.name);
                        }
                        _this.setPropertyDefault(signbox, data.property);
                    }
                }
                new YChart.Dialog(options);
                YChart.stopBubble(ev);
            });
        }
    });
}

//注册控件
YChart.FormBuilder.prototype.registerControl = function (controlName, events) {
    for (var i = 0; i < this.controls.length; i++) {
        if (controlName == this.controls[i].name) {
            return;
        }
    }
    this.controls.push({ name: controlName, events: events });
}

//获取出控件
YChart.FormBuilder.prototype.findControl = function (controlName) {
    for (var i = 0; i < this.controls.length; i++) {
        if (controlName == this.controls[i].name) {
            return this.controls[i].events;
        }
    }
}

YChart.FormBuilder.getPosition = function (element) {
    var pos = { x: element.offsetLeft, y: element.offsetTop };
    if (element.offsetParent != null) {
        var temp = YChart.FormBuilder.getPosition(element.offsetParent);
        pos.x += temp.x;
        pos.y += temp.y;
    }
    return pos;
}

YChart.FormBuilder.getDPI = function () {
    var result = {};
    if (window.screen.deviceXDPI) {
        result.xDPI = window.screen.deviceXDPI;
        result.yDPI = window.screen.deviceYDPI;
    }
    else {
        var tmpNode = document.createElement("div");
        tmpNode.style.width = "1in";
        tmpNode.style.height = "1in";
        tmpNode.style.position = "absolute";
        tmpNode.style.left = "0px";
        tmpNode.style.top = "0px";
        tmpNode.style.zIndex = "99";
        tmpNode.style.visibility = "hidden";
        document.body.appendChild(tmpNode);
        result.xDPI = parseInt(tmpNode.offsetWidth);
        result.yDPI = parseInt(tmpNode.offsetHeight);
        tmpNode.parentNode.removeChild(tmpNode);
    }
    return result;
}

YChart.FormBuilder.DPI = YChart.FormBuilder.getDPI();

YChart.FormBuilder.getHeight = function (height) {
    return height * YChart.FormBuilder.DPI.yDPI / 25.4;
}

YChart.FormBuilder.getWidth = function (width) {
    return width * YChart.FormBuilder.DPI.xDPI / 25.4;
}

YChart.FormBuilder.findTDElement = function (element) {
    while (element && element.tagName != "BODY") {
        if ((element.tagName == "TD" || element.tagName == "TH") && (element.getAttribute("ycelement") == "drop_td" || element.getAttribute("ycelement") == "drop_inlinetd")) {
            return element;
        }
        element = element.parentNode;
    }
}

YChart.FormBuilder.findInlineTableElement = function (element) {
    while (element && element.nodeType == 1 && element.tagName != "BODY") {
        if (element.getAttribute("ycelement") == "table") {
            return element;
        }
        element = element.parentNode;
    }
}

YChart.FormBuilder.getInlineTableName = function (element) {
    while (element && element.nodeType == 1 && element.tagName != "BODY") {
        if (element.tagName == "DIV" && element.getAttribute("ycelement") == "table") {
            return element.getAttribute("ID");
        }
        element = element.parentNode;
    }
}

YChart.FormBuilder.findStyleElement = function (element) {
    if (element.tagName == "P" || element.tagName == "TD") {
        return element;
    }
    while (element && element.parentNode != document.documentElement) {
        if (element.parentNode && (element.parentNode.tagName == "P" || element.parentNode.tagName == "TD")) {
            return element.parentNode;
        }
        element = element.parentNode;
    }
}

YChart.getSelectionNodes = function (selection) {
    var _status = 0;
    var nodes = [];
    if (selection.anchorNode && selection.anchorNode == selection.extentNode) {
        nodes.push(selection.anchorNode);
    }
    else {
        recuriseNodes(this.body, function (node, _parent) {
            if (node == selection.anchorNode || node == selection.extentNode) {
                if (_status == 0) {
                    //第一个选中开始的节点
                    _status = 1;
                }
                else if (_status == 1) {
                    //最后一个节点，不继续处理
                    _status = 2;
                }
            }
            if (_status == 1 || _status == 2) {
                nodes.push(node);
            }
        });
    }
    return nodes;

    function recuriseNodes(parent, callback) {
        if (_status == 2 || !parent.tagName) {
            return;
        }
        for (var i = 0; i < parent.childNodes.length; i++) {
            if (_status == 2) {
                return;
            }
            callback(parent.childNodes[i], parent);
            if (_status == 2) {
                return;
            }
            if (parent.childNodes[i].childNodes.length > 0) {
                recuriseNodes(parent.childNodes[i], callback);
            }
        }
    }
}

YChart.FormBuilder.setStyle = function (data, style, attributeName, defaultValue) {
    if (data[attributeName] == defaultValue) {
        return;
    }
    if (data[attributeName] != style[attributeName]) {
        data[attributeName] = defaultValue;
    }
}

YChart.FormBuilder.isDropElement = function (element) {
    while (element && element.tagName != "BODY") {
        if (element.tagName && element.getAttribute("allowdrop") == "0") {
            return true;
        }
        element = element.parentNode;
    }
    return false;
}

YChart.FormBuilder.isPanelElement = function (element) {
    while (element && element.tagName != "BODY") {
        if (element.tagName && element.getAttribute("ycelement") == "panel") {
            return true;
        }
        element = element.parentNode;
    }
    return false;
}

YChart.FormBuilder.isTableElement = function (element) {
    while (element && element.tagName != "BODY") {
        if (element.tagName && element.getAttribute("ycelement") == "table") {
            return true;
        }
        element = element.parentNode;
    }
    return false;
}

YChart.FormBuilder.setElementStyle = function (element, styleName, value, operation, records) {
    var targetElement;
    if (styleName == "verticalAlign") {
        targetElement = YChart.FormBuilder.findTDElement(element);
    }
    else {
        targetElement = YChart.FormBuilder.findStyleElement(element);
    }
    if (targetElement) {
        if (records) {
            for (var i = 0; i < records.length; i++) {
                if (records[i] == targetElement) {
                    return;
                }
            }
        }
        if (styleName == "textDecoration") {
            var styles = targetElement.style.textDecoration ? targetElement.style.textDecoration.split(" ") : [];
            if (operation == "add") {
                var isIn = false;
                for (var i = 0; i < styles.length; i++) {
                    if (styles[i] == value) {
                        isIn = true;
                    }
                }
                if (!isIn) {
                    styles.push(value);
                }
            }
            else if (operation == "remove") {
                for (var i = 0; i < styles.length; i++) {
                    if (styles[i] == value) {
                        styles.splice(i, 1);
                        break;
                    }
                }
            }
            targetElement.style.textDecoration = styles.join(" ");
        } else {
            targetElement.style[styleName] = value;
        }
        records && records.push(targetElement);
    }
}

YChart.FormBuilder.generateRadios = function (radioElement, property) {
    YChart.clear(radioElement);
    if (property.baseDataID) {
        radioElement.appendChild(YChart.createElement("input", { id: property.groupName + "_" + property.baseDataID, name: property.groupName, type: "radio", value: +property.baseDataID }));
        radioElement.appendChild(YChart.createElement("label", { "for": property.groupName + "_" + property.baseDataID }, null, "[" + property.baseDataName + "]"));
    }
    else {
        if (property.rowAmount == 0 || property.items.length <= property.rowAmount) {
            //直接平铺
            for (var i = 0; i < property.items.length; i++) {
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "radio", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (i != 0 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (i != property.items.length - 1 && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                radioElement.appendChild(r);
                radioElement.appendChild(l);
            }
        }
        else {
            var amount = 0;
            var _tb = YChart.createElement("table", null, { borderCollapse: "collapse", width: "auto", height: "auto" });
            radioElement.appendChild(_tb);
            var _tr;
            var lastRowIndex = (property.items.length % property.rowAmount == 0 ? (property.items.length / property.rowAmount) : parseInt((property.items.length / property.rowAmount) + 1)) - 1;
            for (var i = 0; i < property.items.length; i++) {
                if (amount == 0 || amount == property.rowAmount) {
                    _tr = YChart.createElement("tr");
                    _tb.appendChild(_tr);
                    amount = 0;
                }
                amount++;
                var _tc = _tr.insertCell(-1);
                if (_tr.rowIndex != 0 && property.paddingTop > 0) {
                    _tc.style.paddingTop = property.paddingTop + "px";
                }
                if (_tr.rowIndex != lastRowIndex && property.paddingBottom > 0) {
                    _tc.style.paddingBottom = property.paddingBottom + "px";
                }
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "radio", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (amount != 1 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (amount != property.rowAmount && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                _tc.appendChild(r);
                _tc.appendChild(l);
            }
            amount--;
            if (_tr) {
                while (amount < property.rowAmount) {
                    _tr.insertCell(-1);
                    amount++;
                }
            }
        }
    }
}

YChart.FormBuilder.generateCheckBoxes = function (checkBoxElement, property) {
    YChart.clear(checkBoxElement);
    if (property.baseDataID) {
        checkBoxElement.appendChild(YChart.createElement("input", { id: property.groupName + "_" + property.baseDataID, name: property.groupName, type: "checkbox", value: +property.baseDataID }));
        checkBoxElement.appendChild(YChart.createElement("label", { "for": property.groupName + "_" + property.baseDataID }, null, "[" + property.baseDataName + "]"));
    }
    else {
        if (property.rowAmount == 0 || property.items.length <= property.rowAmount) {
            for (var i = 0; i < property.items.length; i++) {
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "checkbox", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (i != 0 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (i != property.items.length - 1 && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                checkBoxElement.appendChild(r);
                checkBoxElement.appendChild(l);
            }
        }
        else {
            var amount = 0;
            var _tb = YChart.createElement("table", null, { borderCollapse: "collapse", width: "auto", height: "auto" });
            checkBoxElement.appendChild(_tb);
            var _tr;
            var lastRowIndex = (property.items.length % property.rowAmount == 0 ? (property.items.length / property.rowAmount) : parseInt((property.items.length / property.rowAmount) + 1)) - 1;
            for (var i = 0; i < property.items.length; i++) {
                if (amount == 0 || amount == property.rowAmount) {
                    _tr = YChart.createElement("tr");
                    _tb.appendChild(_tr);
                    amount = 0;
                }
                amount++;
                var _tc = _tr.insertCell(-1);
                if (_tr.rowIndex != 0 && property.paddingTop > 0) {
                    _tc.style.paddingTop = property.paddingTop + "px";
                }
                if (_tr.rowIndex != lastRowIndex && property.paddingBottom > 0) {
                    _tc.style.paddingBottom = property.paddingBottom + "px";
                }
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "checkbox", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (amount != 1 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (amount != property.rowAmount && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                _tc.appendChild(r);
                _tc.appendChild(l);
            }
            amount--;
            if (_tr) {
                while (amount < property.rowAmount) {
                    _tr.insertCell(-1);
                    amount++;
                }
            }
        }
    }
}

YChart.FormBuilder.generateTable = function (table, buttons) {
    //加入table
    var _div = document.createElement("div");
    table.appendChild(_div);
    _div.style.textAlign = "center";
    _div.style.fontSize = "18px";
    _div.style.fontWeight = "bolder";
    _div = document.createElement("div");
    _div.style.display = "none";
    _div.className = "yc_inlinetable_toolbar";
    table.appendChild(_div);
    var _table = document.createElement("table");
    _table.className = "yc_inlinetable";
    table.appendChild(_table);
    //插入两个空行
    var _tr0 = document.createElement("tr");
    _table.appendChild(_tr0);
    var _tr1 = document.createElement("tr");
    _table.appendChild(_tr1);
    YChart.FormBuilder.generateTableToolbar(_table, buttons)
}

YChart.FormBuilder.generateTableToolbar = function (tableElement, buttons) {
    var _toolbar = tableElement.previousSibling;
    if (tableElement.rows[0].cells.length == 0) {
        _toolbar.style.display = "none";
    }
    else {
        _toolbar.style.display = "";
        _toolbar.style.width = (tableElement.clientWidth - 1) + "px";
    }
    if (buttons) {
        if (_toolbar.previousSibling.textContent == "") {
            _toolbar.style.marginTop = "0px";
        }
        else {
            _toolbar.style.marginTop = "3px";
        }
        YChart.clear(_toolbar);
        for (var i = 0; i < buttons.length; i++) {
            //添加按钮
            var btn = YChart.createElement("div", { className: "yc_inlinetable_toolbar_btn" });
            var _span1 = document.createElement("span");
            btn.appendChild(_span1);
            var _span2 = document.createElement("span");
            btn.appendChild(_span2);
            _span2.style.marginLeft = "5px";
            switch (buttons[i]) {
                case "add":
                    _span1.className = "fa fa-plus";
                    _span2.appendChild(document.createTextNode("添加"));
                    break;
                case "delete":
                    _span1.className = "fa fa-minus";
                    _span2.appendChild(document.createTextNode("删除"));
                    break;
                case "moveup":
                    _span1.className = "fa fa-arrow-up";
                    _span2.appendChild(document.createTextNode("上移"));
                    break;
                case "movedown":
                    _span1.className = "fa fa-arrow-down";
                    _span2.appendChild(document.createTextNode("下移"));
                    break;
            }
            _toolbar.appendChild(btn);
            btn.setAttribute("yc_action", buttons[i]);
        }
    }
}

YChart.FormBuilder.generateSelect = function (selectElement, property) {
    YChart.clear(selectElement);
    if (property.baseDataID) {
        var option = document.createElement("option");
        option.setAttribute("value", property.baseDataID);
        option.appendChild(document.createTextNode("[" + property.baseDataName + "]"));
        selectElement.appendChild(option);
    }
    else {
        for (var i = 0; i < property.items.length; i++) {
            var option = document.createElement("option");
            option.setAttribute("value", property.items[i].value);
            option.appendChild(document.createTextNode(property.items[i].name));
            selectElement.appendChild(option);
        }
    }
}

YChart.FormBuilder.removeRoundEmptyElement = function (element) {
    var temp = element.previousSibling;
    while (temp && !(temp.tagName == "DIV" && temp.getAttribute("ycelement") == "panel") && YChart.isEmptyElement(temp)) {
        YChart.remove(temp);
        temp = element.previousSibling;
    }
    temp = element.nextSibling;
    while (temp && !(temp.tagName == "DIV" && temp.getAttribute("ycelement") == "panel") && YChart.isEmptyElement(temp)) {
        YChart.remove(temp);
        temp = element.nextSibling;
    }
}

YChart.FormBuilder.generateUploader = function (element, property) {
    var divToolbar = YChart.createElement("div", { className: "yc_inlinetable_toolbar" }, { width: "100%" });
    element.appendChild(divToolbar);
    var btnUpload = YChart.createElement("div", { className: "yc_inlinetable_toolbar_btn", yc_action: "add" });
    btnUpload.appendChild(YChart.createElement("span", { className: "fa fa-upload" }));
    btnUpload.appendChild(YChart.createElement("span", null, { marginLeft: "5px" }, "上传"));
    divToolbar.appendChild(btnUpload);
    var btnDownload = YChart.createElement("div", { className: "yc_inlinetable_toolbar_btn", yc_action: "download" });
    btnDownload.appendChild(YChart.createElement("span", { className: "fa fa-download" }));
    btnDownload.appendChild(YChart.createElement("span", null, { marginLeft: "5px" }, "下载"));
    divToolbar.appendChild(btnDownload);
    var btnDelete = YChart.createElement("div", { className: "yc_inlinetable_toolbar_btn", yc_action: "download" });
    btnDelete.appendChild(YChart.createElement("span", { className: "fa fa-trash" }));
    btnDelete.appendChild(YChart.createElement("span", null, { marginLeft: "5px" }, "删除"));
    divToolbar.appendChild(btnDelete);
    var divBody = YChart.createElement("div", null, { border: "1px solid #666666", width: "100%", height: "50px" });
    element.appendChild(divBody);
    //<div class="yc_inlinetable_toolbar" style="margin-top: 0px; width: 91px;"><div class="yc_inlinetable_toolbar_btn" yc_action="add"><span class="fa fa-plus"></span><span style="margin-left: 5px;">添加</span></div><div class="yc_inlinetable_toolbar_btn" yc_action="delete"><span class="fa fa-minus"></span><span style="margin-left: 5px;">删除</span></div></div>
}