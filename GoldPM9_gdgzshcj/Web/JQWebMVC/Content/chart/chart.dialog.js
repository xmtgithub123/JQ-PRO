"use strict"
if (typeof (YChart) == "undefined") {
    window.YChart = {};
}
YChart.Dialog = function (options) {
    if (options.isTop == false) {
        this.parent = window;
        this.id = YChart.getRandomID();
    }
    else {
        this.parent = window.top;
        this.id = "top_" + YChart.getRandomID();
    }
    var _dialog = YChart.createElement("div", { id: "yc_dialog_" + this.id }, { boxShadow: " 0 0 20px 6px #C1C1C1", position: "absolute" });
    _dialog.style.width = options.width + "px";
    this.parent.document.body.appendChild(_dialog);
    var _width = this.parent.document.documentElement.clientWidth;
    if (_width > options.width) {
        //水平居中
        _dialog.style.left = (_width - options.width) / 2 + "px";
    }
    var _title = YChart.createElement("div", null, { height: "38px", backgroundColor: "#FAFAFA", width: "100%", lineHeight: "38px", borderBottom: "1px solid #EAEAEA", fontSize: "14px", color: "#222", cursor: "default", textIndent: "6px" });
    _dialog.appendChild(_title);
    YChart.attachEvent(_title, "mousedown", function (event) {
        YChart.onMouseDown(event, this, function (event) {
            //开始移动
            YChart.adorner = { initX: event.clientX, initY: event.clientY, ui: (event.target || event.srcElement).parentNode, target: event.target || event.srcElement };
            YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", YChart.Dialog.onMouseMove);
            YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", YChart.Dialog.onMouseUpOrLeave);
            YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", YChart.Dialog.onMouseUpOrLeave);
        });
    });
    if (options.icon) {
        _title.appendChild(YChart.createElement("span", { className: "fa " + options.icon }, { marginRight: "3px", fontSize: "14px" }))
    }
    if (options.title) {
        _title.appendChild(this.parent.document.createTextNode(options.title));
    }
    var _title_right = YChart.createElement("div", null, { float: "right", marginRight: "10px" });
    _title.appendChild(_title_right);
    var title_right1 = YChart.createElement("span", { className: "fa fa-close" }, { cursor: "pointer", lineHeight: "32px" });
    var onBeforeClose = options.onBeforeClose;
    title_right1.onclick = function () {
        if (this.parentNode.parentNode.parentNode.getAttribute("mask")) {
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
        }
        onBeforeClose && onBeforeClose();
        YChart.remove(this.parentNode.parentNode.parentNode);
    }
    _title_right.appendChild(title_right1);
    this.close = function () {
        title_right1.click();
    }
    var _content = YChart.createElement("div", null, { height: options.height + "px", width: options.width + "px", backgroundColor: "#FFFFFF" });
    _dialog.appendChild(_content);
    var _height = this.parent.document.documentElement.clientHeight;
    if (_height > _dialog.offsetHeight) {
        _dialog.style.top = (_height - _dialog.offsetHeight) / 2.6 + "px";
    }
    if (options.mask != false) {
        YChart.Dialog.showMask(this.parent);
        _dialog.setAttribute("mask", "1");
    }
    _dialog.style.zIndex = 1000 + YChart.Dialog.getCounter(this.parent) * 2 - 1;
    if (options.url) {
        var _c1 = YChart.createElement("div", null, { position: "relative", top: "0px", left: "0px", height: "100%", width: "100%", backgroundColor: "#FEFEFE" });
        _content.appendChild(_c1);
        var _c2 = YChart.createElement("div", null, { position: "absolute", top: "40%", width: "100%", textAlign: "center", color: "#888888" });
        _c1.appendChild(_c2);
        _c2.appendChild(YChart.createElement("span", { className: "fa fa-refresh fa-spin" }, { display: "inline-block" }));
        _c2.appendChild(YChart.createElement("span", null, { marginLeft: "3px", cursor: "default" }, "正在加载中"));
        var _iframe = YChart.createElement("iframe");
        _iframe.setAttribute("frameborder", "0");
        _iframe.style.height = "100%";
        _iframe.style.width = "100%";
        _content.appendChild(_iframe);
        if (options.api) {
            _iframe.contentWindow.frameElement.api = options.api;
        }
        _iframe.contentWindow.frameElement.closeDialog = function () {
            title_right1.click();
        };
        _iframe.setAttribute("src", options.url);
        var onLoaded = options.onLoaded;
        YChart.attachEvent(_iframe, "load", function () {
            if (this.previousSibling) {
                this.parentNode.removeChild(this.previousSibling);
            }
            this.style.visibility = "visible";
            this.onload = null;
            onLoaded && onLoaded(this);
            if (this.contentWindow.onDialogLoaded) {
                this.contentWindow.onDialogLoaded();
            }
        });
        _iframe.style.visibility = "hidden";
    }
    else if (options.element) {
        _content.appendChild(options.element);
        options.onLoadContent && options.onLoadContent();
    }
    delete this.parent;
}

YChart.Dialog.setting = {
    isCrossForm: true,
    theme: "default"
};

YChart.Dialog.close = function (dialogID) {
    if (!dialogID) {
        return;
    }
    var dialog;
    if (dialogID.indexOf("top_") == 0) {
        dialog = window.top.document.getElementById("yc_dialog_" + dialogID);
    }
    else {
        dialog = document.getElementById("yc_dialog_" + dialogID);
    }
    if (dialog) {
        dialog.childNodes[0].childNodes[2].childNodes[0].click();
    }
}

YChart.Dialog.addCounter = function (_window) {
    if (!_window._ychart_counter) {
        _window._ychart_counter = 1;
    }
    else {
        _window._ychart_counter++;
    }
}

YChart.Dialog.minusCounter = function (_window) {
    if (!_window._ychart_counter) {
        return;
    }
    _window._ychart_counter--;
}

YChart.Dialog.getCounter = function (_window) {
    if (!_window._ychart_counter) {
        return 0;
    }
    return _window._ychart_counter;
}

YChart.Dialog.onMouseMove = function (event) {
    var left = parseFloat(YChart.adorner.ui.style.left.replace("px", "")) + event.clientX - YChart.adorner.initX;
    if (left < 0) {
        left = 0;
    }
    else if ((left + YChart.adorner.ui.offsetWidth) > this.ownerDocument.documentElement.clientWidth) {
        left = this.ownerDocument.documentElement.clientWidth - YChart.adorner.ui.offsetWidth;
    }
    YChart.adorner.ui.style.left = left + "px";
    var top = parseFloat(YChart.adorner.ui.style.top.replace("px", "")) + event.clientY - YChart.adorner.initY;
    if (top < 0) {
        top = 0;
    }
    else if ((top + YChart.adorner.ui.offsetHeight) > this.ownerDocument.documentElement.clientHeight) {
        top = this.ownerDocument.documentElement.clientHeight - YChart.adorner.ui.offsetHeight;
    }
    YChart.adorner.ui.style.top = top + "px";
    YChart.adorner.initX = event.clientX;
    YChart.adorner.initY = event.clientY;
}

YChart.Dialog.onMouseUpOrLeave = function (event) {
    if (!YChart.adorner) {
        return;
    }
    delete YChart.adorner;
    YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", YChart.Dialog.onMouseMove);
    YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", YChart.Dialog.onMouseUpOrLeave);
    YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", YChart.Dialog.onMouseUpOrLeave);
}

YChart.Dialog.showMask = function (_window) {
    var div = _window.document.getElementById("yc_div_mask");
    if (!div) {
        div = YChart.createElement("div", { id: "yc_div_mask" }, { zIndex: 1000, position: "absolute", top: "0px", width: "100%", left: "0px", height: "100%", backgroundColor: "#92CDDE", opacity: 0.2 });
        _window.document.body.appendChild(div);
        div.style.display = "";
    }
    else if (div.style.display == "") {
        div.style.zIndex = parseInt(div.style.zIndex) + 2;
    }
    else {
        div.style.display = "";
    }
    YChart.Dialog.addCounter(_window);
}

YChart.Dialog.hideMask = function (_window) {
    var div = _window.document.getElementById("yc_div_mask");
    if (div) {
        if (YChart.Dialog.getCounter(_window) == 1) {
            div.style.display = "none";
        }
        else {
            div.style.zIndex = parseInt(div.style.zIndex) - 2;
        }
        YChart.Dialog.minusCounter(_window);
    }
}

YChart.Dialog.confirm = function (title, message, callback) {
    var _parent = window.top;
    var mb = _parent.document.getElementById("yc_messageboxconfirm");
    if (!mb) {
        mb = YChart.createElement("div", { id: "yc_messageboxconfirm" }, { height: "150px", width: "280px", zIndex: 9900, position: "absolute", display: "none" });
        _parent.document.body.appendChild(mb);
    }
    if (mb.style.display != "") {
        YChart.Dialog.showMask(_parent);
        mb.style.display = "";
        var mb_border = YChart.createElement("div", null, { backgroundColor: "#E5E5E5", opacity: 0.5, width: "280px", height: "150px", zIndex: 9901 });
        mb.appendChild(mb_border);
        var mb_content = YChart.createElement("div", null, { margin: "-145px 0px 0px 5px", zIndex: 9902, position: "absolute" });
        mb.appendChild(mb_content);
        var mb_title = YChart.createElement("div", null, { width: "270px", height: "26px", backgroundColor: "#E8E8E8", lineHeight: "28px", fontSize: "16px" });
        mb_content.appendChild(mb_title);
        var mb_title_1 = YChart.createElement("div", null, { float: "left", textIndent: "10px", color: "#444444", cursor: "default" });
        mb_title.appendChild(mb_title_1);
        mb_title_1.appendChild(document.createTextNode(title || "提示信息"));
        var adorner = { x: 0, y: 0, x1: 0, y1: 0 };
        YChart.attachEvent(mb_title, "mousedown", function (event) {
            YChart.onMouseDown(event, {
                ui: mb,
                beginMove: function (event) {
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", onmousemove);
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", onmouseuporleave);
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", onmouseuporleave);
                    adorner.x = event.clientX;
                    adorner.y = event.clientY;
                    adorner.x1 = mb.offsetLeft;
                    adorner.y1 = mb.offsetTop;
                }
            });
        });
        var onmousemove = function (event) {
            var temp = event.clientX - adorner.x + adorner.x1;
            if (temp < 0) {
                temp = 0;
            }
            else if (temp > ((event.target || event.srcElement).ownerDocument.documentElement.clientWidth - mb.offsetWidth)) {
                temp = event.screenWidth - mb.offsetWidth;
            }
            mb.style.left = temp + "px";
            var temp = event.clientY - adorner.y + adorner.y1;
            if (temp < 0) {
                temp = 0;
            }
            else if (temp > ((event.target || event.srcElement).ownerDocument.documentElement.clientHeight - mb.offsetHeight)) {
                temp = event.screenHeight - mb.offsetHeight;
            }
            mb.style.top = temp + "px";
        }
        var onmouseuporleave = function (event) {
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", onmousemove);
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", onmouseuporleave);
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", onmouseuporleave);
        }
        var mb_title_2 = YChart.createElement("div", { className: "fa fa-close" }, { float: "right", cursor: "pointer", color: "#686868", width: "20px", marginTop: "5px" });
        mb_title_2.onclick = function () {
            if (callback) {
                callback(false, "close");
            }
            this.parentNode.parentNode.parentNode.style.display = "none";
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
            YChart.clear(this.parentNode.parentNode.parentNode);
        };
        mb_title.appendChild(mb_title_2);
        var msg = YChart.createElement("div", null, { border: "1px solid #F5F5F5", width: "268px", height: "110px", backgroundColor: "#FFFFFF" });
        mb_content.appendChild(msg);
        var c11 = YChart.createElement("div", null, { width: "248px", textAlign: "left", height: "50px", verticalAlign: "middle", float: "left", color: "#444444", padding: "12px 10px 10px 10px", fontSize: "14px", cursor: "default" });
        msg.appendChild(c11);
        c11.appendChild(document.createTextNode(message || "提示信息"));
        c11 = YChart.createElement("div", null, { float: "left", marginLeft: "44px" });
        msg.appendChild(c11);
        var btn = YChart.createElement("div", null, { height: "30px", width: "80px", backgroundColor: "#EFEFEF", textAlign: "center", lineHeight: "30px", color: "#444444", float: "left", cursor: "pointer" });
        YChart.setText(btn, "确定");
        c11.appendChild(btn);
        btn.onclick = function () {
            if (callback) {
                callback(true);
            }
            this.parentNode.parentNode.parentNode.parentNode.style.display = "none";
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
            YChart.clear(this.parentNode.parentNode.parentNode.parentNode);
        }
        btn = YChart.createElement("div", null, { marginLeft: "10px", height: "30px", width: "80px", backgroundColor: "#EFEFEF", textAlign: "center", lineHeight: "30px", color: "#444444", float: "left", cursor: "pointer" });
        YChart.setText(btn, "取消");
        btn.onclick = function () {
            if (callback) {
                callback(false);
            }
            this.parentNode.parentNode.parentNode.parentNode.style.display = "none";
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
            YChart.clear(this.parentNode.parentNode.parentNode.parentNode);
        }
        c11.appendChild(btn);
    }
    else {
        YChart.setText(mb.childNodes[1].childNodes[0].childNodes[0], title || "提示");
        YChart.setText(mb.childNodes[1].childNodes[1].childNodes[0], message || "提示信息");
    }
    mb.style.left = ((_parent.document.documentElement.clientWidth - mb.offsetWidth) / 2) + "px";
    mb.style.top = ((_parent.document.documentElement.clientHeight - mb.offsetHeight) / 2.6) + "px";
}

YChart.Dialog.alert = function (title, message) {
    var _parent = window.top;
    var mb = _parent.document.getElementById("yc_messageboxalert");
    if (!mb) {
        mb = YChart.createElement("div", { id: "yc_messageboxalert" }, { height: "150px", width: "280px", zIndex: 9900, position: "absolute", display: "none" });
        _parent.document.body.appendChild(mb);
    }
    if (mb.style.display != "") {
        YChart.Dialog.showMask(_parent);
        mb.style.display = "";
        var mb_border = YChart.createElement("div", null, { backgroundColor: "#E5E5E5", opacity: 0.5, width: "280px", height: "150px", zIndex: 9901 });
        mb.appendChild(mb_border);
        var mb_content = YChart.createElement("div", null, { margin: "-145px 0px 0px 5px", zIndex: 9902, position: "absolute" });
        mb.appendChild(mb_content);
        var mb_title = YChart.createElement("div", null, { width: "270px", height: "26px", backgroundColor: "#E8E8E8", lineHeight: "28px", fontSize: "16px" });
        mb_content.appendChild(mb_title);
        var mb_title_1 = YChart.createElement("div", null, { float: "left", textIndent: "10px", color: "#444444", cursor: "default", fontSize: "14px" });
        mb_title.appendChild(mb_title_1);
        mb_title_1.appendChild(document.createTextNode(title || "提示"));
        var adorner = { x: 0, y: 0, x1: 0, y1: 0 };
        mb_title.onmousedown = function (event) {
            YChart.onMouseDown(event, {
                ui: mb,
                beginMove: function (event) {
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", onmousemove);
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", onmouseuporleave);
                    YChart.attachEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", onmouseuporleave);
                    adorner.x = event.clientX;
                    adorner.y = event.clientY;
                    adorner.x1 = mb.offsetLeft;
                    adorner.y1 = mb.offsetTop;
                }
            });
        }
        var onmousemove = function (event) {
            var temp = event.clientX - adorner.x + adorner.x1;
            if (temp < 0) {
                temp = 0;
            }
            else if (temp > ((event.target || event.srcElement).ownerDocument.documentElement.clientWidth - mb.offsetWidth)) {
                temp = event.screenWidth - mb.offsetWidth;
            }
            mb.style.left = temp + "px";
            var temp = event.clientY - adorner.y + adorner.y1;
            if (temp < 0) {
                temp = 0;
            }
            else if (temp > ((event.target || event.srcElement).ownerDocument.documentElement.clientHeight - mb.offsetHeight)) {
                temp = event.screenHeight - mb.offsetHeight;
            }
            mb.style.top = temp + "px";
        }
        var onmouseuporleave = function (event) {
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mousemove", onmousemove);
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseup", onmouseuporleave);
            YChart.removeEvent((event.target || event.srcElement).ownerDocument.body, "mouseleave", onmouseuporleave);
        }
        var mb_title_2 = YChart.createElement("div", { className: "fa fa-close" }, { float: "right", cursor: "pointer", color: "#686868", width: "20px", marginTop: "5px" });
        mb_title_2.onclick = function () {
            this.parentNode.parentNode.parentNode.style.display = "none";
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
            YChart.clear(this.parentNode.parentNode.parentNode);
        };
        mb_title.appendChild(mb_title_2);
        var msg = YChart.createElement("div", null, { border: "1px solid #F5F5F5", width: "268px", height: "110px", backgroundColor: "#FFFFFF" });
        mb_content.appendChild(msg);
        var c11 = YChart.createElement("div", null, { width: "248px", textAlign: "left", height: "50px", verticalAlign: "middle", float: "left", color: "#444444", padding: "12px 10px 10px 10px", fontSize: "14px", cursor: "default" });
        msg.appendChild(c11);
        c11.appendChild(document.createTextNode(message || "提示信息"));
        c11 = YChart.createElement("div", null, { float: "left", marginLeft: "95px" });
        msg.appendChild(c11);
        var btn = YChart.createElement("div", null, { height: "30px", width: "80px", backgroundColor: "#EFEFEF", textAlign: "center", lineHeight: "30px", color: "#444444", float: "left", cursor: "pointer" });
        YChart.setText(btn, "确定");
        c11.appendChild(btn);
        btn.onclick = function () {
            this.parentNode.parentNode.parentNode.parentNode.style.display = "none";
            YChart.Dialog.hideMask(this.ownerDocument.defaultView);
            YChart.clear(this.parentNode.parentNode.parentNode.parentNode);
        }
    }
    else {
        YChart.setText(mb.childNodes[1].childNodes[0].childNodes[0], title || "提示");
        YChart.setText(mb.childNodes[1].childNodes[1].childNodes[0], message || "提示信息");
    }
    mb.style.left = ((_parent.document.documentElement.clientWidth - mb.offsetWidth) / 2) + "px";
    mb.style.top = ((_parent.document.documentElement.clientHeight - mb.offsetHeight) / 2.6) + "px";
}