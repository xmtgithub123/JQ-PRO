"use strict"
if (typeof (YChart) == "undefined") {
    var YChart = {};
}

YChart.Tab = function (options) {
    if (typeof (options.container) == "string") {
        options.container = document.getElementById(options.container);
    }
    if (!options.container) {
        return;
    }
    this.headWidth = options.headWidth || 160;
    if (options.fontSize) {
        this.fontSize = options.fontSize;
    }
    if (options.onSelect) {
        this.onSelect = options.onSelect;
    }
    this.header = YChart.createElement("div", { className: "y_tab_header" });
    options.container.appendChild(this.header);
    this.body = YChart.createElement("div", { className: "y_tab_body" });
    options.container.appendChild(this.body);
    this.body.style.overflowY = "auto";
    this.body.style.height = (options.container.clientHeight - this.header.offsetHeight - (options.offsetHeight || 0)) + "px";
    this.selected = null;
    this.tabs = [];
    if (options.tabs && options.tabs.length > 0) {
        for (var i = 0; i < options.tabs.length; i++) {
            this.appendTab(options.tabs[i]);
        }
    }
    if (options.defaultSelected) {
        this.select(options.defaultSelected);
    }
    else if (this.selected == null && this.tabs.length > 0) {
        this.select(this.tabs[0].name);
    }
}

YChart.Tab.prototype.appendTab = function (options) {
    if (this.isExists(options.name)) {
        return;
    }
    options._ui = YChart.createElement("div", { className: "y_tab_head" }, { width: this.headWidth + "px" });
    this.header.appendChild(options._ui);
    var headtitle = YChart.createElement("div", { className: "y_tab_head_title", title: options.title }, { float: "left" }, options.title);
    headtitle.style.width = (options.closable != false ? (this.headWidth - 23) : this.headWidth) + "px";
    if (this.fontSize) {
        headtitle.style.fontSize = this.fontSize + "px";
    }
    options._ui.appendChild(headtitle);
    var _this = this;
    this.tabs.push(options);
    var tabName = options.name;
    if (options.closable != false) {
        var btnClose = YChart.createElement("div", { className: "fa fa-times-circle y_tab_head_close" }, { float: "right" });
        options._ui.appendChild(btnClose);
        btnClose.onclick = function () {
            YChart.stopBubble();
            _this.remove(tabName);
        }
    }
    options._ui.onclick = function () {
        _this.select(tabName);
    }
    options._index = this.tabs.length;
}

YChart.Tab.prototype.select = function (tabName) {
    for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].name == tabName) {
            if (this.selected) {
                this.selected.scrollTop = this.selected._content.parentNode.scrollTop;
                YChart.removeClass(this.selected._ui, "y_tab_active");
                this.selected._content && (this.selected._content.style.display = "none");
            }
            YChart.addClass(this.tabs[i]._ui, "y_tab_active");
            this.selected = this.tabs[i];
            if (this.tabs[i]._content) {
                this.tabs[i]._content.style.display = "";
                this.tabs[i]._content.parentNode.scrollTop = this.tabs[i].scrollTop;
            }
            else {
                if (this.tabs[i].url) {
                    var tabBody = YChart.createElement("div", null, { height: "100%", width: "100%" });
                    this.body.appendChild(tabBody);
                    var div = YChart.createElement("div", null, { position: "relative", top: "0px", left: "0px", height: "100%", width: "100%", backgroundColor: "#FEFEFE" });
                    tabBody.appendChild(div);
                    var _div = YChart.createElement("div", null, { position: "absolute", top: "40%", width: "100%", textAlign: "center", color: "#888888" });
                    div.appendChild(_div);
                    _div.appendChild(YChart.createElement("span", { className: "fa fa-refresh fa-spin" }));
                    _div.appendChild(YChart.createElement("span", null, { marginLeft: "3px", WebkitUserSelect: "none", cursor: "default", display: "inline-block" }, "正在加载中"));
                    this.tabs[i]._content = tabBody;
                    var _iframe = YChart.createElement("iframe");
                    _iframe.setAttribute("frameborder", "0");
                    _iframe.style.visibility = "hidden";
                    _iframe.style.height = this.body.clientHeight + "px";
                    _iframe.style.width = "100%";
                    tabBody.appendChild(_iframe);
                    _iframe.setAttribute("src", this.tabs[i].url);
                    _iframe.onload = function () {
                        this.style.visibility = "visible";
                        this.parentNode.removeChild(this.previousSibling);
                        this.onload = null;
                    }
                }
                else if (this.tabs[i].element) {
                    var element = this.tabs[i].element;
                    if (typeof (element) == "string") {
                        element = document.getElementById(element);
                        if (!element) {
                            continue;
                        }
                    }
                    var tabBody = YChart.createElement("div", null, { height: "100%", width: "100%" });
                    this.body.appendChild(tabBody);
                    element.style.display = "";
                    tabBody.appendChild(element);
                    this.tabs[i]._content = tabBody;
                }
                else if (this.tabs[i].onLoad) {
                    var tabBody = YChart.createElement("div", null, { height: "100%", width: "100%", backgroundColor: "#FFFFFF" });
                    this.body.appendChild(tabBody);
                    this.tabs[i]._content = tabBody;
                    this.tabs[i].onLoad(tabBody);
                }
            }
            if (this.onSelect) {
                this.onSelect(tabName);
            }
        }
    }
}

YChart.Tab.prototype.remove = function (tabName) {
    var index = -1;
    for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].name == tabName) {
            if (this.selected == this.tabs[i]) {
                index = i;
                this.selected = null;
            }
            YChart.remove(this.tabs[i]._ui);
            this.tabs[i]._content && YChart.remove(this.tabs[i]._content);
            this.tabs.splice(i, 1);
            i--;
            continue;
        }
        this.tabs[i]._index = i;
    }
    if (index == -1 || this.tabs.length == 0) {
        return;
    }
    if (this.tabs.length - 1 > index) {
        this.select(this.tabs[index].name);
    }
    else {
        this.select(this.tabs[this.tabs.length - 1].name);
    }
}

YChart.Tab.prototype.isExists = function (tabName) {
    for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].name == tabName) {
            return true;
        }
    }
    return false;
}