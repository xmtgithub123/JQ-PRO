document.onreadystatechange = function () {
    if (this.readyState == "complete") {
        if (!document.getElementById("divtoolbar") && !document.getElementById("divcontent")) {
            return;
        }
        document.getElementById("divcontent").style.float = "left";
        document.getElementById("divcontent").style.width = "100%";
        document.getElementById("divcontent").style.overflowY = "auto";
        if (document.getElementById("divseniorqueryblock")) {
            document.getElementById("divseniorqueryblock").style.display = "none";
        }
        function size() {
            var height = document.documentElement.clientHeight - (document.getElementById("divseniorqueryblock") ? document.getElementById("divseniorqueryblock").offsetHeight : 0) - document.getElementById("divtoolbar").offsetHeight - 1;
            document.getElementById("divcontent").style.height = height + "px";
            window.onResize && window.onResize(height);
        }
        size();
        window.onresize = function () {
            size();
        }
        if (document.getElementById("div_senior_flag")) {
            document.getElementById("div_senior_flag").onclick = function () {
                var _seniorblock = document.getElementById("divseniorqueryblock");
                if (_seniorblock.style.display == "") {
                    this.className = "yc_senior_expand"
                    document.getElementById("divtoolbar").style.borderBottom = "";
                    _seniorblock.style.display = "none";
                    size();
                }
                else {
                    this.className = "yc_senior_collapse";
                    _seniorblock.style.display = "";
                    document.getElementById("divtoolbar").style.borderBottom = "1px solid transparent";
                    size();
                }
            }
            document.getElementById("divseniorqueryline").onclick = function () {
                document.getElementById("div_senior_flag").click();
            }
        }
        window.onLoaded && window.onLoaded();
    }
}
if (typeof (bpm) == "undefined") {
    window.bpm = {};
}
bpm.parseDateValue = function (v) { if (!v) { return ""; } return new Date(parseInt(/^\/Date\(([0-9]*)\)\/$/.exec(v)[1])); };
bpm.parseDateText = function (date, format) {
    return bpm.getDateText(bpm.parseDateValue(date), format);
};
bpm.supplyString = function (content, digit) {
    if (!digit) { digit = 2; } content = content.toString();
    while (content.length < digit) { content = "0" + content; }
    return content;
};
bpm.getDateText = function (date, format) {
    if (!date) {
        return "";
    }
    if (!format) { format = "yyyy-MM-dd"; }
    if (format == "yyyy-MM-dd") {
        return date.getFullYear().toString() + "-" + bpm.supplyString((date.getMonth() + 1)) + "-" + bpm.supplyString(date.getDate());
    }
    else if (format == "yyyy-MM-dd HH:mm") {
        return date.getFullYear().toString() + "-" + bpm.supplyString((date.getMonth() + 1)) + "-" + bpm.supplyString(date.getDate()) + " " + bpm.supplyString(date.getHours()) + ":" + bpm.supplyString(date.getMinutes());
    }
    else if (format == "HH:mm") {
        return bpm.supplyString(date.getHours()) + ":" + bpm.supplyString(date.getMinutes());
    }
};
bpm.stopBubble = function (e) {
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

bpm.trim = function (str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

bpm.getValue = function (elementID) {
    return document.getElementById(elementID).value;
}

bpm.getTrimValue = function (elementID) {
    return bpm.trim(document.getElementById(elementID).value);
}