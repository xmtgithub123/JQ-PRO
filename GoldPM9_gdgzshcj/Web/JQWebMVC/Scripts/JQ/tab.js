var Tab = function (options) { 
    if (!options.head || !options.body) {
        return;
    }
    this.head = options.head;
    this.body = options.body;
    if (options.onSelected) {
        this.onSelected = options.onSelected;
    }
    this.tabs = [];

    if (options.tabs) {
        for (var i in options.tabs) {
            this.appendTab(options.tabs[i]);
        }
        if (this.defaultSelected) {
            this.select(this.defaultSelected);
        }
        else if (this.selected == null && this.tabs.length > 0) {
            this.select(this.tabs[0].name);
        }
    }
}

Tab.prototype.appendTab = function (options) {
    if (this.isExists(options.name)) {
        return;
    }
    options._ui = document.createElement("li");
    options._ui.appendChild(document.createTextNode(options.title));
    this.head.appendChild(options._ui);
    var _this = this;
    this.tabs.push(options);
    var tabName = options.name;
    options._ui.onclick = function () {
        _this.select(tabName);
    }
    if (options.selected === true) {
        this.defaultSelected = options.name;
    }
    options.scrollTop = 0;
    options._index = this.tabs.length;
}

Tab.prototype.select = function (tabName) { 
    for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].name == tabName) {
            if (this.selected) {
                this.selected._ui.className = "";
                if (this.selected._content) {
                    this.selected._content.style.display = "none"
                    this.selected.scrollTop = this.selected._content.parentNode.scrollTop;
                }
            }
            this.tabs[i]._ui.className = "jq_tab_active";
            this.selected = this.tabs[i];
            if (this.tabs[i]._content) {
                this.tabs[i]._content.parentNode.scrollTop = this.tabs[i].scrollTop;
                this.tabs[i]._content.style.display = "";
            }
            else {
                this.refreshSelected();
            }
            if (this.onSelected) {
                this.onSelected(this.selected);
            }
        }
    }
}

Tab.prototype.getSelected = function () {
    return this.selected;
}

Tab.prototype.refreshSelected = function () {
    if (this.selected.url) {
        if (!this.selected._content) {
            var tabBody = Tab.createElement("div", null, { height: "100%", width: "100%", overflowY: "hidden" });
            this.body.appendChild(tabBody);
            this.selected._content = tabBody;
        }
        else {
            this.selected._content.innerHTML = "";
        }
        var div = Tab.createElement("div", null, { position: "relative", top: "0px", left: "0px", height: "100%", width: "100%", backgroundColor: "#FEFEFE" });
        this.selected._content.appendChild(div);
        var _div = Tab.createElement("div", null, { position: "absolute", top: "40%", width: "100%", textAlign: "center", color: "#888888" });
        div.appendChild(_div);
        _div.appendChild(Tab.createElement("span", { className: "fa fa-refresh fa-spin" }, { display: "inline-block" }));
        _div.appendChild(Tab.createElement("span", null, { marginLeft: "3px" }, "正在加载中"));
        var _iframe = Tab.createElement("iframe");
        _iframe.setAttribute("frameborder", "0");
        _iframe.style.height = "100%";
        _iframe.style.width = "100%";
        this.selected._content.appendChild(_iframe);
        _iframe.setAttribute("src", this.selected.url);
        _iframe.onload = function () {
            this.parentNode.removeChild(this.previousSibling);
            this.onload = null;
        }
    }
    else if (this.selected.element) {
        if (!this.selected._content) {
            var tabBody = Tab.createElement("div", null, { height: "100%", width: "100%" });
            this.body.appendChild(tabBody);
            this.selected._content = tabBody;
            document.getElementById(this.selected.element).style.display = "";
            this.selected._content.appendChild(document.getElementById(this.selected.element));
        }
    }
    else if (this.selected.onLoad) {
        if (!this.selected._content) {
            var tabBody = Tab.createElement("div", null, { height: "100%", width: "100%", backgroundColor: "#FFFFFF" });
            this.body.appendChild(tabBody);
            this.selected._content = tabBody;
        }
        this.selected.onLoad(this.selected._content);
    }
}

Tab.prototype.isExists = function (tabName) {
    for (var i = 0; i < this.tabs.length; i++) {
        if (this.tabs[i].name == tabName) {
            return true;
        }
    }
    return false;
}

Tab.createElement = function (tagName, attributes, styles, text) {
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