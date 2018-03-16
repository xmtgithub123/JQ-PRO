"use strict"
if (typeof (YChart) == "undefined") {
    var YChart = {};
}
//引入chart.js
YChart._allScripts = document.getElementsByTagName('script');
YChart._src = YChart._allScripts[YChart._allScripts.length - 1].getAttribute("src");
YChart._scriptElement = document.createElement("script");
YChart._scriptElement.setAttribute("type", "text/javascript");
YChart._scriptElement.setAttribute("src", YChart._src.substr(0, YChart._src.lastIndexOf("/")) + "/chart.js");
YChart._allScripts[YChart._allScripts.length - 1].parentNode.insertBefore(YChart._scriptElement, YChart._allScripts[YChart._allScripts.length - 1]); delete YChart._allScripts; delete YChart._src; delete YChart._scriptElement;

YChart.Widget = function (option) {
    if (!option.container || !option.remote) {
        return;
    }
    if (typeof (option.container) == "string") {
        option.container = document.getElementById(option.container);
        if (!option.container) {
            return;
        }
    }
    if (!option.container || !option.remote) {
        return;
    }
    var that = this;
    if (option.template) {
        this.template = option.template;
    }
    if (option.currentActivityID) {
        this.currentActivityID = option.currentActivityID;
    }
    if (option.buttons && option.buttons.length > 0) {
        this.buttons = option.buttons;
    }
    this.remote = option.remote;
    this.containter = option.container;
    if (document.readyState == "complete") {
        beginLoad();
    }
    else {
        document.onreadystatechange = function () {
            if (document.readyState == "complete") {
                beginLoad();
            }
        }
    }

    function beginLoad() {
        YChart.addClass(that.containter, "ycw_container");
        var div = YChart.createElement("div", null, { width: "100%", float: "left", borderBottom: "1px solid #E1E1E1" });
        that.containter.appendChild(div);
        initButton(div);
        div = YChart.createElement("div", null, { width: "100%", float: "left", margin: "4px 0px 0px 20px", height: "30px", lineHeight: "30px", color: "#333333" });
        that.containter.appendChild(div);
        loadRoute(div);
        div = YChart.createElement("div", null, { width: "100%", float: "left", margin: "4px 0px 0px  20px" });
        div.appendChild(YChart.createElement("textarea", { placeholder: "请输入备注", className: "form-control" }, { width: (that.containter.clientWidth - 40) + "px", height: "60px", resize: "none", margin: "0px 0px 0px 0px" }));
        that.containter.appendChild(div);
    }

    function initButton(parent) {
        var btn = YChart.createElement("div", { className: "ycw_button" }, { margin: "5px 0px 4px 20px" });
        var span = YChart.createElement("span", { className: "ycw_icon ycw_icon_ok" }, { margin: "0px 2px 0px 0px" });
        btn.appendChild(span);
        btn.appendChild(document.createTextNode("提交"));
        parent.appendChild(btn);
        //按钮部分
        for (var i = 0; i < that.buttons.length; i++) {
            if (!that.buttons[i].text) {
                continue;
            }
            btn = YChart.createElement("div", { className: "ycw_button" }, { margin: "5px 0px 4px 5px" });
            span = YChart.createElement("span", null, { margin: "0px 2px 0px 0px" });
            if (that.buttons[i].icon) {
                span.className = "ycw_icon ycw_icon_" + that.buttons[i].icon;
            }
            btn.appendChild(span);
            btn.appendChild(document.createTextNode(that.buttons[i].text));
            if (that.buttons[i].click) {
                btn.onclick = that.buttons[i].click;
            }
            parent.appendChild(btn);
        }
    }

    function writeWrongText(element, text) {
        var div = YChart.createElement("div", null, { margin: "5px 0px 0px 5px", height: "22px", lineHeight: "22px", color: "#DB4E2F" });
        element.appendChild(div);
        div.appendChild(YChart.createElement("span", { className: "ycw_icon ycw_icon_wrong" }, { fontSize: "18px", float: "left" }));
        div.appendChild(YChart.createElement("span", null, { float: "left", margin: "0px 0px 0px 5px" }, text));
    }

    function loadRoute(parent) {
        if (!("template" in that) && !("currentActivityID" in that)) {
            writeWrongText(parent, "参数异常，无法准确加载出流程信息");
            return;
        }
        //显示正在加载中
        parent.appendChild(YChart.createElement("span", { className: "ycw_icon ycw_icon_loading" }, { fontSize: "18px", float: "left" }));
        parent.appendChild(YChart.createElement("span", null, { float: "left", margin: "0px 0px 0px 5px" }, "正在加载中"));
        setTimeout(function () {
            YChart.ajax({
                url: that.remote.url,
                type: that.remote.type || "post",
                data: { template: that.template },
                success: function (result) {
                    console.log(result);
                },
                error: function () {
                    YChart.clear(parent);
                    writeWrongText(parent, "服务器返回错误信息");
                }
            });
        }, 1000)
    }
}

YChart.Widget.prototype.reload = function () {
    //重新加载

}