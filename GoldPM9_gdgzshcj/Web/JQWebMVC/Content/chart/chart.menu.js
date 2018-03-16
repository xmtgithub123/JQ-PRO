"use strict"
if (typeof (YChart) == "undefined") {
    YChart = {};
}
YChart.Menu = function (options) {
    this.ui = YChart.createElement("div", { className: "yc_menu" });
    document.body.appendChild(this.ui);
    this.items = [];
    if (options.items && options.items.length > 0) {
        for (var i = 0; i < options.items.length; i++) {
            this.appendItem(options.items[i]);
        }
    }
    if (options.show) {
        this.show(options.show);
    }
    else {
        this.ui.style.display = "none";
    }
}

YChart.Menu.prototype.appendItem = function (options) {
    options._ui = YChart.createElement("div", { className: "yc_menu_item" }, null, options.text);
    this.ui.appendChild(options._ui);
    options._order = this.items.length;
    if (options.onClick) {
        options._ui.onclick = options.onClick;
    }
    this.items.push(options);
}

YChart.Menu.prototype.show = function (options) {
    this.ui.style.left = options.x + "px";
    this.ui.style.top = options.y + "px";
    this.ui.style.display = "";
    var _this = this;
    var hide = function () {
        _this.ui.style.display = "none";
        YChart.removeEvent(document.body, "click", hide);
    };
    YChart.attachEvent(document.body, "click", hide);
}

