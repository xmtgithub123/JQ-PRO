//工具包
"use strict"
if (typeof (YChart) == "undefined") {
    window.YChart = {};
}
if (typeof (YChart.ns == "undefined")) {
    YChart.ns = "http://www.w3.org/2000/svg";
}

//取消冒泡
YChart.stopBubble = function (e) {
    var e = e ? e : window.event;
    if (e.stopPropagation) {
        e.stopPropagation();
    }
    else {
        e.cancelBubble = true();
    }
}

YChart.preventDefault = function (e) {
    e.preventDefault ? e.preventDefault() : e.returnValue = false;
}
//设置元素样式
YChart.setElementStyles = function (element, styles) {
    for (var style in styles) {
        element.style[style] = styles[style];
    }
}

//创建元素
YChart.createElement = function (tagName, attributes, styles, text) {
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
    if (text != null && text != undefined) {
        v.appendChild(document.createTextNode(text));
    }
    return v;
}

//创建SVG元素
YChart.createElementNS = function (tagName, attributes, styles) {
    var v = document.createElementNS(YChart.ns, tagName);
    if (attributes) {
        for (var a in attributes) {
            if (attributes[a]) {
                if (a == "className") {
                    v.setAttributeNS(null, "class", attributes[a]);
                }
                else {
                    v.setAttributeNS(null, a, attributes[a]);
                }
            }
        }
    }
    if (styles) {
        for (var s in styles) {
            v.style[s] = styles[s];
        }
    }
    return v;
}

//获取元素中的文本
YChart.getText = function (element) {
    return element.textContent || element.innerText;
}

//设置元素中的文本
YChart.setText = function (element, text) {
    element.innerHTML = "";
    element.appendChild(document.createTextNode(text));
}

//附加事件
YChart.attachEvent = function (src, eventName, func) {
    if (src instanceof Array) {
        for (var i in src) {
            if (src[i].attachEvent) {
                src[i].attachEvent("on" + eventName, func);
            }
            else {
                src[i].addEventListener(eventName, func, false);
            }
        }
    }
    else {
        if (src.attachEvent) {
            src.attachEvent("on" + eventName, func);
        }
        else {
            src.addEventListener(eventName, func, false);
        }
    }
}

//移除事件
YChart.removeEvent = function (src, eventName, func) {
    if (src.detachEvent) {
        src.detachEvent("on" + eventName, func);
    }
    else {
        src.removeEventListener(eventName, func, false);
    }
}

//是否为SVG元素
YChart.isSVGElement = function (obj) {
    if (obj.tagName == "rect" || obj.tagName == "path" || obj.tagName == "g" || obj.tagName == "circle" || obj.tagName == "ellipse" || obj.tagName == "line" || obj.tagName == "polyline" || obj.tagName == "polygon" || obj.tagName == "svg") {
        return true;
    }
    return false;
}

//添加样式
YChart.addClass = function (element, className) {
    if (YChart.isSVGElement(element)) {
        var temp = YChart.trim(element.getAttributeNS(null, "class"));
        if ((" " + temp + " ").indexOf(" " + className + " ") == -1) {
            element.setAttributeNS(null, "class", temp + (temp == "" ? "" : " ") + className);
        }
    }
    else {
        if ((" " + element.className + " ").indexOf(" " + className + " ") == -1) {
            element.className = YChart.trim(element.className) + (element.className == "" ? "" : " ") + className;
        }
    }
}

//移除样式
YChart.removeClass = function (obj, className) {
    if (YChart.isSVGElement(obj)) {
        var s = obj.getAttributeNS(null, "class").split(" ");
        var a = [];
        for (var i = 0; i < s.length; i++) {
            if (s[i] == className || s[i] == "") {
                continue;
            }
            a.push(s[i]);
        }
        obj.setAttributeNS(null, "class", a.join(" "));
    }
    else {
        var s = obj.className.split(" ");
        var a = [];
        for (var i = 0; i < s.length; i++) {
            if (s[i] == className || s[i] == "") {
                continue;
            }
            a.push(s[i]);
        }
        obj.className = a.join(" ");
    }
}

//获取选中项的文本
YChart.getSelectedText = function (select) {
    for (var i = 0; i < select.options.length; i++) {
        if (select.options[i].selected) {
            return YChart.getText(select.options[i]);
        }
    }
    return "";
}

//获取选中项的值
YChart.getSelectedValue = function (select) {
    return YChart.getSelectedAttribute(select, "value") || "";
}

//获取选中项的属性
YChart.getSelectedAttribute = function (select, attributeName) {
    for (var i = 0; i < select.options.length; i++) {
        if (select.options[i].selected) {
            return select.options[i].getAttribute(attributeName);
        }
    }
}

//获取选中项的属性
YChart.getSelectedOption = function (select) {
    for (var i = 0; i < select.options.length; i++) {
        if (select.options[i].selected) {
            return select.options[i];
        }
    }
}

//设置选中的文本
YChart.setSelectedByText = function (select, text) {
    for (var i = 0; i < select.options.length; i++) {
        if (YChart.getText(select.options[i]) == text) {
            select.options[i].selected = true;
            break;
        }
    }
}

//设置选中的值
YChart.setSelectedByValue = function (select, value) {
    return YChart.setSelectedByAttribute(select, "value", value);
}

//设置选中的某属性
YChart.setSelectedByAttribute = function (select, attributeName, value) {
    for (var i = 0; i < select.options.length; i++) {
        if (select.options[i].getAttribute(attributeName) == value) {
            select.options[i].selected = true;
            break;
        }
    }
}

//加载XML
YChart.loadXml = function (xmlContent) {
    var xmlDoc;
    if ("ActiveXObject" in window) {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.loadXML(xmlContent);
    }
    else if (document.implementation && document.implementation.createDocument) {
        var domParser = new DOMParser();
        xmlDoc = domParser.parseFromString(xmlContent, "text/xml");
    } else {
        alert('您的浏览器不支持该系统脚本解析XML！');
    }
    return xmlDoc;
}

//在表格行中插入单元格
YChart.insertCell = function (tr, attributes, styles) {
    var v = tr.insertCell(-1);
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
    return v;
}

//调用Ajax
YChart.ajax = function (cfg) {
    if (!cfg.url) {
        return;
    }
    if (!cfg.type) {
        cfg.type = "get";
    }
    if (cfg.async != false && cfg.async != true) {
        cfg.async = true;
    }
    var xmlHttp;
    if (window.ActiveXObject) {
        xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    else if (window.XMLHttpRequest) {
        xmlHttp = new XMLHttpRequest();
    }
    else {
        alert("您的浏览器不支持异步数据访问操作");
        return;
    }
    if (!cfg.dataType) {
        cfg.dataType = "";
    }
    else {
        cfg.dataType = cfg.dataType.toLowerCase();
    }
    if (cfg.isShowMask == true) {
        YChart.showAjaxMask();
    }
    else if (cfg.isShowTopMask == true) {
        YChart.showAjaxMask(true);
    }
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            if (cfg.onAjaxReady) {
                cfg.onAjaxReady.call(xmlHttp);
            }
            if (cfg.isShowMask == true) {
                YChart.hideAjaxMask();
            }
            else if (cfg.isShowTopMask == true) {
                YChart.hideAjaxMask(true);
            }
            if (xmlHttp.status == 200) {
                if (cfg.success) {
                    var contentType = xmlHttp.getResponseHeader("Content-Type");
                    if (contentType == null) {
                        cfg.success(null);
                    }
                    else if (contentType.indexOf("application/json") > -1 || cfg.dataType == "json") {
                        cfg.success(YChart.parseJson(xmlHttp.responseText));
                    }
                    else if (contentType.indexOf("application/xml") > -1 || cfg.dataType == "xml") {
                        cfg.success(YChart.loadXml(xmlHttp.responseText));
                    }
                    else {
                        cfg.success(xmlHttp.responseText);
                    }
                }
            }
            else if (xmlHttp.status == 600) {
                window.top.location.href = xmlHttp.statusText;
            }
            else {
                if (cfg.error) {
                    cfg.error(xmlHttp.responseText);
                }
            }
            if (cfg.complete) {
                if (cfg.complete) {
                    cfg.complete();
                }
            }
        }
    }
    xmlHttp.open(cfg.type, cfg.url, cfg.async);
    if (cfg.data) {
        if (typeof (cfg.data) == "object") {
            var temp = "", flag = 0;
            for (var i in cfg.data) {
                if (flag == 1) {
                    temp += "&";
                }
                else {
                    flag = 1;
                }
                temp += i + "=" + encodeURIComponent(cfg.data[i]);
            }
            cfg.data = temp;
        }
        //Cherish.xmlHttp.setRequestHeader("Accept", "*/*");
        xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
        //Cherish.xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
        xmlHttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
    }
    if (cfg.onAjaxSend) {
        cfg.onAjaxSend.call(xmlHttp);
    }
    xmlHttp.send(cfg.data);
}

YChart.ajaxSubmit = function (options) {
    var form;
    if (typeof (options.form) == "string") {
        form = document.getElementById(options.form);
        if (!form) {
            var temp = document.getElementsByName(options.form);
            if (temp.length > 0) {
                form = temp[0];
            }
        }
    }
    else {
        form = options.form;
    }
    if (!form) {
        return;
    }
    var data = "";
    YChart.recuriseChildNodes(form, function (element) {
        if (!element.tagName) {
            return;
        }
        var name = element.getAttribute("name");
        if (!name) {
            name = element.getAttribute("id");
        }
        if (!name) {
            return;
        }
        switch (element.tagName) {
            case "INPUT":
                switch (element.getAttribute("type")) {
                    case "hidden":
                    case "text":
                    case "number":
                    case "password":
                        if (data != "") {
                            data += "&";
                        }
                        data += encodeURIComponent(name) + "=" + encodeURIComponent(element.value);
                        break;
                    case "checkbox":
                    case "radio":
                        if (element.checked) {
                            if (data != "") {
                                data += "&";
                            }
                            data += encodeURIComponent(name) + "=" + encodeURIComponent(element.getAttribute("value") || "");
                        }
                        break;
                }
                break;
            case "SELECT":
                for (var i = 0; i < element.childNodes.length; i++) {
                    if (element.childNodes[i].tagName && (element.childNodes[i].selected || element.childNodes[i].getAttribute("selected") == "selected")) {
                        if (data != "") {
                            data += "&";
                        }
                        data += encodeURIComponent(name) + "=" + encodeURIComponent(element.childNodes[i].getAttribute("value"));
                        break;
                    }
                }
                break;
            case "TEXTAREA":
                if (data != "") {
                    data += "&";
                }
                data += encodeURIComponent(name) + "=" + encodeURIComponent(element.value);
                break;
        }
    });
    delete options.form;
    if (options.externalData) {
        if (typeof (options.externalData) == "string") {
            if (data != "") {
                data += "&";
            }
            data += options.externalData;
        }
        else if (YChart.isArray(options.externalData)) {
            for (var i = 0; i < options.externalData.length; i++) {
                if (data != "") {
                    data += "&";
                }
                data += encodeURIComponent(options.externalData[i].name) + "=" + encodeURIComponent(options.externalData[i].value);
            }
        }
        else if (typeof (options.externalData) == "object") {
            for (var i in options.externalData) {
                if (data != "") {
                    data += "&";
                }
                data += encodeURIComponent(i) + "=" + encodeURIComponent(options.externalData[i]);
            }
        }
    }
    options.data = data.replace(/%20/g, "+");
    YChart.ajax(options);
}

YChart.recuriseChildNodes = function (parent, callback) {
    for (var i = 0; i < parent.childNodes.length; i++) {
        var result = callback(parent.childNodes[i]);
        if (result === true || result === false) {
            return result;
        }
        if (!parent.childNodes[i]) {
            i--;
            continue;
        }
        if (parent.childNodes[i].childNodes.length > 0) {
            if (YChart.recuriseChildNodes(parent.childNodes[i], callback) == false) {
                return false;
            }
        }
    }
}

//将json字符串转化为js对象

YChart.parseJson = function (data) {
    return (new Function("return " + data))();
}

YChart.parseFloat = function (value) {
    var s = parseFloat(value);
    if (isNaN(s)) {
        return 0;
    }
    return s;
}

//选中单个XML节点
YChart.selectSingleNode = function (node, xpath) {
    if ("ActiveXObject" in window) {
        return node.selectSingleNode(xpath);
    }
    else {
        var x = YChart.selectNodes(node, xpath)
        if (!x || x.length < 1) return null;
        return x[0];
    }
}

//选中多个XML节点
YChart.selectNodes = function (node, xpath) {
    if ("ActiveXObject" in window) {
        return node.selectNodes(xpath);
    }
    else {
        var xpe = new XPathEvaluator();
        var nsResolver = xpe.createNSResolver(node.ownerDocument == null ? node.documentElement : node.ownerDocument.documentElement);
        var result = xpe.evaluate(xpath, node, nsResolver, 0, null);
        var found = [];
        var res;
        while (res = result.iterateNext())
            found.push(res);
        return found;
    }
}

//去除字符串前后空格
YChart.trim = function (str) {
    return str.replace(/(^\s*)|(\s*$)/g, "")
}

//将元素上移置最前面
YChart.toFront = function () {
    for (var i = 0; i < arguments.length; i++) {
        arguments[i].parentNode.appendChild(arguments[i]);
    }
}

//将元素下移至最后面
YChart.toBack = function () {
    for (var i = 0; i < arguments.length; i++) {
        arguments[i].parentNode.insertBefore(arguments[i], arguments[i].parentNode.firstChild);
    }

}

//清除元素中的所有内容
YChart.clear = function (element) {
    for (var i = 0; i < element.childNodes.length; i++) {
        element.removeChild(element.childNodes[i]);
        i--;
    }
}

YChart.insertAfter = function (newElement, targetElement) {
    var parent = targetElement.parentNode;
    if (targetElement.nextSibling) {
        parent.insertBefore(newElement, targetElement.nextSibling);
    } else {
        parent.appendChild(newElement);
    }
}

YChart.insertBefore = function (newElement, targetElement) {
    targetElement.parentNode.insertBefore(newElement, targetElement);
}

YChart.getFirstChild = function (element) {
    for (var i = 0; i < element.childNodes.length; i++) {
        if (element.childNodes[i].tagName) {
            return element.childNodes[i];
        }
    }
}

YChart.remove = function (element) {
    element.parentNode.removeChild(element);
}
//是否鼠标松开
YChart.isReleased = true;

//当鼠标摁下时触发的事件
YChart.onMouseDown = function (event, shape, beginMove) {
    YChart.isReleased = false;
    YChart.attachEvent((beginMove ? shape : (shape.canvas || shape.ui.parentNode)), "mouseup", YChart.onMouseUp);
    document.onselectstart = function () { return false; };
    YChart.mousedowntimeout = setTimeout(function () {
        if (YChart.mousedowntimeout) {
            delete YChart.mousedowntimeout;
        }
        if (YChart.isReleased) {
            document.onselectstart = null;
            return;
        }
        beginMove ? beginMove(event) : shape.beginMove(event);
    }, 130);
    YChart.stopBubble(event);
}
//当鼠标松开时触发的事件
YChart.onMouseUp = function (event) {
    if (YChart.mousedowntimeout) {
        clearTimeout(YChart.mousedowntimeout);
        delete YChart.mousedowntimeout;
    }
    YChart.isReleased = true;
    YChart.removeEvent(this, "mouseup", YChart.onMouseUp);
}
YChart.preventDefault = function (e) {
    e.preventDefault ? e.preventDefault() : e.returnValue = false;
}
YChart.isArray = function (object) {
    return object && typeof object === "object" && typeof object.length === "number" && typeof object.splice === "function" && !(object.propertyIsEnumerable("length"));
}
YChart.parseDateValue = function (v) { if (!v) { return ""; } return new Date(parseInt(/^\/Date\(([0-9]*)\)\/$/.exec(v)[1])); };
YChart.parseDateText = function (date, format) {
    return YChart.getDateText(YChart.parseDateValue(date), format);
};
YChart.supplyString = function (content, digit) {
    if (!digit) { digit = 2; } content = content.toString();
    while (content.length < digit) { content = "0" + content; }
    return content;
};
YChart.getDateText = function (date, format) {
    if (!date) {
        return "";
    }
    if (!format) { format = "yyyy-MM-dd"; }
    if (format == "yyyy-MM-dd") {
        return date.getFullYear().toString() + "-" + YChart.supplyString((date.getMonth() + 1)) + "-" + YChart.supplyString(date.getDate());
    }
    else if (format == "yyyy-MM-dd HH:mm") {
        return date.getFullYear().toString() + "-" + YChart.supplyString((date.getMonth() + 1)) + "-" + YChart.supplyString(date.getDate()) + " " + YChart.supplyString(date.getHours()) + ":" + YChart.supplyString(date.getMinutes());
    }
    else if (format == "HH:mm") {
        return YChart.supplyString(date.getHours()) + ":" + YChart.supplyString(date.getMinutes());
    }
};

YChart.isNumber = function (v) {
    if (/^[-]?[0-9]+[.]?[0-9]*$/.test(v)) {
        return true;
    }
    return false;
}

YChart.parseInt = function (v, defaultValue) {
    v = parseInt(v);
    if (isNaN(v)) {
        return defaultValue || 0;
    }
    return v;
}

YChart.registerNumberBox = function (options) {
    if (!options.element) {
        return;
    }
    if (YChart.isArray(options.element)) {
        for (var i = 0; i < options.element.length; i++) {
            options.element[i].style.textAlign = "right";
        }
    }
    else {
        options.element.style.textAlign = "right";
    }
    if (options.precision) {
        options.precision = YChart.parseInt(options.precision);
    }
    else {
        options.precision = 0;
    }
    YChart.attachEvent(options.element, "keydown", function (event) {
        if ((event.keyCode >= 48 && event.keyCode <= 57) || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 45 || (options.precision > 0 && (event.keyCode == 190 || event.keyCode == 110)) || event.keyCode == 189 || event.keyCode == 109 || event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 9) {
            //允许输入
        }
        else {
            //不允许输入
            event.returnValue = false;
        }
    });
    YChart.attachEvent(options.element, "change", function (event) {
        if (this.value == "" && options.defaultValue == "") {
            return;
        }
        //验证输入值是否符合条件
        var value = parseFloat(this.value);
        if (isNaN(value)) {
            value = parseFloat(options.defaultValue) || 0;
        }
        value = parseFloat(value.toFixed(options.precision));
        if (YChart.isNumber(options.min) && value < options.min) {
            this.value = options.min;
            return;
        }
        if (YChart.isNumber(options.max) && value > options.max) {
            this.value = options.max;
            return;
        }
        this.value = value;
    });
}

YChart.parseToXml = function (xNode, obj, name) {
    if (obj == null || obj == undefined) {
        return;
    }
    if (YChart.isArray(obj)) {
        if (name) {
            var xChild = xNode.ownerDocument.createElement(name);
            xNode.appendChild(xChild);
            xChild.setAttribute("_isArray", "1");
            for (var i = 0; i < obj.length; i++) {
                var _xChild = xNode.ownerDocument.createElement("item");
                xChild.appendChild(_xChild);
                YChart.parseToXml(_xChild, obj[i]);
            }
        }
        else {
            xNode.setAttribute("_isArray", "1");
            for (var i = 0; i < obj.length; i++) {
                var _xChild = xNode.ownerDocument.createElement("item");
                xNode.appendChild(_xChild);
                YChart.parseToXml(_xChild, obj[i]);
            }
        }
    }
    else if (typeof (obj) == "object") {
        if (name) {
            var xChild = xNode.ownerDocument.createElement(name);
            xNode.appendChild(xChild);
            for (var i in obj) {
                YChart.parseToXml(xChild, obj[i], i);
            }
        }
        else {
            for (var i in obj) {
                YChart.parseToXml(xNode, obj[i], i);
            }
        }
    }
    else {
        if (!name) {
            name = "_value";
        }
        xNode.setAttribute(name, obj.toString());
    }
}

YChart.parseToObject = function (obj, xNode, name) {
    if (!obj) {
        return;
    }
    if (xNode.attributes.length == 1 && xNode.getAttribute("_isArray") == "1") {
        obj[xNode.localName] = [];
        for (var i = 0; i < xNode.childNodes.length; i++) {
            if (xNode.childNodes[i].attributes.length == 1 && xNode.childNodes[i].getAttribute("_value")) {
                obj[xNode.localName].push(xNode.childNodes[i].getAttribute("_value"));
            }
            else {
                var _obj = {};
                YChart.parseToObject(_obj, xNode.childNodes[i]);
                obj[xNode.localName].push(_obj);
            }
        }
    }
    else {
        var _obj;
        if (name) {
            obj[name] = {};
            _obj = obj[name];
        }
        else {
            _obj = obj;
        }
        for (var i = 0; i < xNode.attributes.length; i++) {
            _obj[xNode.attributes[i].name] = xNode.attributes[i].value;
        }
        for (var i = 0; i < xNode.childNodes.length; i++) {
            YChart.parseToObject(_obj, xNode.childNodes[i], xNode.childNodes[i].localName);
        }
    }
}
YChart.fillChar = "\u200B";
YChart.isEmptyElement = function (node) {
    if (node.nodeType == 3) {
        if (YChart.isWhitespace(node)) {
            return 1;
        }
        return 0;
    }
    if (node.nodeType != 1 || node.tagName == "INPUT" || node.tagName == "TEXTAREA" || node.tagName == "SELECT") {
        return 0;
    }
    node = node.firstChild;
    while (node) {
        if ((node.nodeType == 1 && !YChart.isEmptyElement(node)) || (node.nodeType == 3 && !YChart.isWhitespace(node))) {
            return 0;
        }
        node = node.nextSibling;
    }
    return 1;
}

YChart.isWhitespace = function (node) {
    return !new RegExp('[^ \t\n\r' + YChart.fillChar + ']').test(node.nodeValue);
}

YChart.getPosition = function (element) {
    var pos = { y: element.offsetTop, x: element.offsetLeft };
    if (element.offsetParent != null) {
        var temp = YChart.getPosition(element.offsetParent);
        pos.x += temp.x;
        pos.y += temp.y;
    }
    return pos;
}

YChart.getAbsolutePosition = function (element) {
    var pos = YChart.getPosition(element);
    var scroll = { x: 0, y: 0 };
    while (element.parentNode && element.parentNode.tagName && element.parentNode.tagName != "HTML") {
        scroll.x += element.scrollLeft || 0;
        scroll.y += element.scrollTop || 0;
        element = element.parentNode;
    }
    pos.x = pos.x - scroll.x;
    pos.y = pos.y - scroll.y;
    return pos;
}

YChart.isDateFormat = function (content) {
    var datereg = /((^((1[8-9]\d{2})|([2-9]\d{3}))(-)(10|12|0?[13578])(-)(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(11|0?[469])(-)(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))(-)(0?2)(-)(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)(-)(0?2)(-)(29)$)|(^([3579][26]00)(-)(0?2)(-)(29)$)|(^([1][89][0][48])(-)(0?2)(-)(29)$)|(^([2-9][0-9][0][48])(-)(0?2)(-)(29)$)|(^([1][89][2468][048])(-)(0?2)(-)(29)$)|(^([2-9][0-9][2468][048])(-)(0?2)(-)(29)$)|(^([1][89][13579][26])(-)(0?2)(-)(29)$)|(^([2-9][0-9][13579][26])(-)(0?2)(-)(29)$))/;
    if (!datereg.test(content)) {
        return false;
    }
    else {
        return true;
    }
}

YChart.parseDateTime = function (str) {
    var reg = /^\/Date\(([0-9]*)\)\/$/;
    return new Date(parseInt(reg.exec(str)[1]));
}

//格式转化 例2013-08-30 12:34:22
YChart.parseDate = function (date) {
    var t = date.split(" ");
    var t1 = t[0].split("-");
    if (t1.length == 1) {
        t1 = t[0].split("/");
    }
    if (t[1]) {
        t = t[1].split(":");
    }
    else {
        t = 1;
    }
    return new Date(t1[0], parseInt(t1[1]) - 1, t1[2], t[0] || 0, t[1] || 0, t[2] || 0);
}

YChart.clone = function (obj) {
    var o;
    if (typeof (obj) == "object") {
        if (obj === null) {
            o = null;
        }
        else if (YChart.isArray(obj)) {
            o = [];
            for (var i = 0; i < obj.length; i++) {
                o.push(YChart.clone(obj[i]));
            }
        }
        else {
            if (obj.nodeType && obj.nodeName && obj.getAttribute) {
                return null;
            }
            o = {};
            for (var k in obj) {
                o[k] = YChart.clone(obj[k]);
            }
        }
    } else {
        o = obj;
    }
    return o;
}
YChart.fireEvent = function (element, eventName) {
    if (element.fireEvent) {
        var ev = document.createEventObject();
        ev.eventType = "message";
        element.fireEvent("on" + eventName, ev);
        return ev;
    }
    else {
        var ev = document.createEvent("HTMLEvents");
        ev.initEvent(eventName, true, true);
        ev.eventType = "message";
        element.dispatchEvent(ev);
        return ev;
    }
}

YChart.isInArray = function (array, data) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] == data) {
            return true;
        }
    }
    return false;
}

//将数组元素从数组中移除
YChart.removeFromArray = function (array, data) {
    for (var i = 0; i < array.length; i++) {
        if (array[i] == data) {
            array.splice(i, 1);
            return;
        }
    }
}

YChart.showTip = function (element, text) {
    if (!element) {
        return;
    }
    var rect;
    if (element.tagName == "INPUT") {
        element.focus();
        rect = element.getBoundingClientRect();
    }
    else {
        //滚动到第一个发现滚动条的元素
        var y = element.offsetTop;
        var temp = element.parentNode;
        while (temp.tagName != "HTML" && temp.scrollHeight == temp.clientHeight) {
            y += temp.offsetTop;
            temp = temp.parentNode;
        }
        if (temp && temp.tagName != "HTML") {
            //滚动到元素的位置
            rect = YChart.getPosition(element);
            var rect1 = temp.getBoundingClientRect();
            var st = rect.y - rect1.top - temp.clientHeight + element.clientHeight + 2;
            if (temp.scrollTop < st) {
                temp.scrollTop = st;
            }
        }
        rect = element.getBoundingClientRect();
    }
    var width = element.offsetWidth;
    if (width < 30) {
        width = 30;
    }
    var _v = YChart.createElement("div", null, { position: "absolute" });
    _v.style.left = rect.left + "px";
    _v.style.top = (rect.top + rect.height) + "px";
    document.body.appendChild(_v);
    var _v1 = YChart.createElement("div", null, { width: "0px", height: "0px", borderLeft: "6px solid transparent", borderRight: "6px solid transparent", borderBottom: "6px solid #111111", marginLeft: "10px", opacity: 0.8 });
    _v.appendChild(_v1);
    var _v2 = YChart.createElement("div", null, { width: width + "px", backgroundColor: "#111111", cursor: "default", opacity: 0.8, borderRadius: "3px" }, "1");
    _v.appendChild(_v2);
    var _v3 = YChart.createElement("div", null, { lineHeight: "14px", width: (width - 4) + "px", color: "#FFFFFF", cursor: "default", fontSize: "11px", padding: "4px 3px 4px 3px", position: "absolute", top: "6px", wordBreak: "break-all" }, text);
    _v.appendChild(_v3);
    _v2.style.height = _v3.offsetHeight + "px";
    _v.onmousedown = function () {
        YChart.stopBubble();
    }
    if (rect.top + rect.height + _v.offsetHeight > document.documentElement.clientHeight) {
        _v1.style.borderBottom = "";
        _v1.style.borderTop = "6px solid #000000";
        YChart.insertAfter(_v1, _v3);
        _v3.style.top = "0px";
        _v.style.top = (rect.top - _v.offsetHeight) + "px"
    }
    var hide = function () {
        YChart.remove(_v);
        YChart.removeEvent(document.body, "mousedown", hide);
    };
    YChart.attachEvent(document.body, "mousedown", hide);
}

YChart.showAjaxMask = function (isTop) {
    var div = (isTop ? window.top.document : document).getElementById("div_ajax_ycmask");
    if (!div) {
        div = YChart.createElement("div", { id: "div_ajax_ycmask", amount: "1" }, { position: "absolute", height: "100%", width: "100%", top: "0px", left: "0px", zIndex: 2000 });
        div.appendChild(YChart.createElement("div", null, { height: "100%", width: "100%", backgroundColor: "#555555", opacity: 0.1 }));
        var _div = YChart.createElement("div", null, { marginLeft: "-38px", position: "absolute", top: "40%", left: "50%" });
        _div.appendChild(YChart.createElement("span", { className: "fa fa-spin fa-spinner" }, { fontSize: "30px", color: "#444444" }))
        div.appendChild(_div);
        (isTop ? window.top.document : document).body.appendChild(div);
    }
    else {
        div.setAttribute("amount", parseInt(div.getAttribute("amount")) + 1);
    }
    div.style.display = "";
}

YChart.hideAjaxMask = function (isTop) {
    var div = (isTop ? window.top.document : document).getElementById("div_ajax_ycmask");
    if (!div) {
        return;
    }
    var amount = parseInt(div.getAttribute("amount")) - 1;
    div.setAttribute("amount", amount);
    if (amount == 0) {
        div.style.display = "none";
    }
}
YChart.randomstr = "abcdefghjiklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
YChart.getRandomID = function () {
    var r = "";
    for (var i = 0; i < 8; i++) {
        var n = Math.random() * 61;
        r = r + YChart.randomstr.substring(n, n + 1);
    }
    return r;
}