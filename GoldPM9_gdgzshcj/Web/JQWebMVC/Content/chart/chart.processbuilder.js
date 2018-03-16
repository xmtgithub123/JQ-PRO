/*  流程绘图工具包（V 1.0.0.0） */
"use strict"
if (typeof (YChart) == "undefined") {
    window.YChart = {};
}
if (typeof (YChart.Core) == "undefined") {
    YChart.Core = {};
}
if (typeof (YChart.Shape) == "undefined") {
    YChart.Shape = {};
}

YChart.ns = "http://www.w3.org/2000/svg";

//对齐拖动
YChart.snapDrag = true;

//初始化
YChart.Process = function (option) {
    //定义形状
    this.shapes = [];
    //定义流程变量
    this.variables = [];
    if (option.onSave) {
        this.onSave = option.onSave;
    }
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
    this.dialog = option.dialog;
    this.isViewMode = option.isViewMode;
    this.viewport = option.viewport;
    var that = this;
    //开始加载
    function beginLoad() {
        if (option.remote) {
            YChart.ajax({
                url: option.remote.url,
                type: option.method || "POST",
                data: option.remote.data,
                dataType: option.remote.dataType || "json",
                success: function (result) {
                    that.data = result;
                    ready();
                },
                error: function () {
                    alert("加载数据出错！");
                }
            });
        }
        else {
            ready();
        }
    }

    //初始化加载
    function ready() {
        var viewport;
        if (!that.isViewMode) {
            //加载左边工具栏
            var lframe = YChart.createElement("div", { className: "yc_toolbar_l" });
            document.body.appendChild(lframe);
            if (window.parent && typeof (window.parent.hideDrawPanel) == "function") {
                lframe.appendChild(YChart.toolbarButton("fa-reply-all", "回退", "", window.parent.hideDrawPanel));
            }
            else {
                lframe.appendChild(YChart.toolbarButton("fa-refresh", "刷新", "", function () {
                    window.location.href = window.location.href;
                }));
            }
            YChart.addClass(lframe.childNodes[0], "yc_toolbar_btn1");
            initLToolbar(lframe);
            var width = document.documentElement.clientWidth - lframe.offsetWidth;
            //加载右上边的内容
            var rframe = YChart.createElement("div", null, { width: width + "px", height: "100%", float: "left" });
            document.body.appendChild(rframe);
            var rframe_toolbar = YChart.createElement("div", { className: "yc_rtoolbar" }, { width: rframe.clientWidth + "px" });
            rframe.appendChild(rframe_toolbar);
            initRToolBar(rframe_toolbar);
            var height = rframe.clientHeight - rframe_toolbar.offsetHeight - 1;
            var rframe_content = YChart.createElement("div", { className: "yc_r_content" }, { width: "180px", height: height + "px" });
            rframe.appendChild(rframe_content);
            viewport = YChart.createElement("div", { className: "yc_viewport" }, { height: height + "px", width: (rframe.clientWidth - rframe_content.offsetWidth) + "px", overflow: "hidden", backgroundColor: "#F5F5F5" });
            rframe.insertBefore(viewport, rframe_content);
        }
        else {
            viewport = that.viewport;
            that.choosedShape = "reset";
        }
        var rframe_canvas = document.createElementNS(YChart.ns, "svg");
        rframe_canvas.setAttributeNS(null, "class", "yc_canvas yc_canvas_grid");
        rframe_canvas.style.backgroundColor = "#FFFFFF";
        rframe_canvas.style.left = "60px";
        rframe_canvas.style.top = "60px";
        rframe_canvas.setAttributeNS(null, "width", viewport.clientWidth - 120);
        rframe_canvas.setAttributeNS(null, "height", viewport.clientHeight - 120);
        rframe_canvas.setAttributeNS(null, "viewBox", "0 0 " + (viewport.clientWidth - 120) + " " + (viewport.clientHeight - 120));
        that.canvas = rframe_canvas;
        viewport.appendChild(rframe_canvas);
        initCanvas();
        if (!that.isViewMode) {
            initRContent(rframe_content);
        }
        document.body.style.overflow = "hidden";
        window.onresize = function () {
            rframe.style.width = (document.documentElement.clientWidth - lframe.offsetWidth) + "px";
            rframe_toolbar.style.width = rframe.clientWidth + "px";
            var cHeight = rframe.clientHeight - rframe_toolbar.offsetHeight;
            viewport.style.height = cHeight + "px";
            rframe_content.style.height = cHeight + "px";
            viewport.style.width = (rframe.clientWidth - rframe_content.offsetWidth) + "px";
        }
        loadData();
        //that.log("流程设计器初始化完成，开始、结束节点插入完成");
    }

    //初始化左边工具栏
    function initLToolbar(leftFrame) {
        //加载工具按钮
        var div_1 = YChart.createElement("div");
        leftFrame.appendChild(div_1);
        var reset = YChart.toolbarButton("fa-mouse-pointer", "复位", "reset", toggleButton);
        div_1.appendChild(reset);
        div_1.appendChild(YChart.toolbarButton("fa-calendar-o", "步骤", "activity", toggleButton));
        //div_1.appendChild(YChart.toolbarButton(decodeURI("%EF%81%A5"), "线条", "line", toggleButton));
        //div_1.appendChild(YChart.toolbarButton(decodeURI("%EF%80%8A"), "分叉", "", toggleButton));
        //div_1.appendChild(YChart.toolbarButton(decodeURI("%EF%83%81"), "其他", "", toggleButton));
        //div_1.appendChild(YChart.toolbarButton(decodeURI("%EF%83%A0"), "其他", "", toggleButton));
        //div_1.appendChild(YChart.toolbarButton(decodeURI("%EF%80%93"), "其他", "", toggleButton));
        YChart.addClass(div_1.childNodes[0], "yc_toolbar_btn_active");
        function toggleButton() {
            for (var i = 0; i < div_1.childNodes.length; i++) {
                if (div_1.childNodes[i] == this) {
                    YChart.addClass(div_1.childNodes[i], "yc_toolbar_btn_active");
                    that.choosedShape = this.getAttribute("shape");
                }
                else {
                    YChart.removeClass(div_1.childNodes[i], "yc_toolbar_btn_active");
                }
            }
        }
        that.resetChooseShape = function () {
            reset.click();
        }
        reset.click();
    }

    //初始化右上边工具栏
    function initRToolBar(rtoolbar) {
        //var span = YChart.createElement("span", null, { marginLeft: "5px" });
        //YChart.setText(span, "X:");
        //rtoolbar.appendChild(span);
        //var xSpan = YChart.createElement("span", { className: "yc_rtoolbar_poslabel" });
        //YChart.setText(xSpan, "0");
        //rtoolbar.appendChild(xSpan);
        //span = YChart.createElement("span", null, { marginLeft: "5px" });
        //YChart.setText(span, "Y:");
        //rtoolbar.appendChild(span);
        //var ySpan = YChart.createElement("span", { className: "yc_rtoolbar_poslabel" });
        //YChart.setText(ySpan, "0");
        //rtoolbar.appendChild(ySpan);
        //that.setCoordinate = function (coordinate) {
        //    YChart.setText(xSpan, coordinate.x);
        //    YChart.setText(ySpan, coordinate.y);
        //}
        var button = YChart.createElement("div", { className: "yc_rtoolbar_btn fa fa-save", title: "保存" });
        button.onclick = function () {
            that.onSave && that.onSave(that.getSaveData());
        }
        rtoolbar.appendChild(button);
        //button = YChart.createElement("div", { className: "yc_rtoolbar_btn yc_icon", data_icon: decodeURI("%EF%80%8A"), title: "整理" });
        //button.onclick = function () {
        //    var start;
        //    for (var i = 0; i < that.shapes.length; i++) {
        //        if (that.shapes[i].symbol == "start") {
        //            start = that.shapes[i];
        //        }
        //    }
        //    if (!start) {
        //        that.log("未找到开始节点");
        //        return;
        //    }
        //    that.log("整理完成");
        //};
        //rtoolbar.appendChild(button);
        button = YChart.createElement("div", { className: "yc_rtoolbar_btn fa fa-trash-o", title: "清理" });
        button.onclick = function () {
            YChart.Dialog.confirm("提示", "确定要清理所有元素吗？", function (cmd) {
                if (cmd) {
                    for (var i = 0; i < that.shapes.length; i++) {
                        that.shapes[i].remove();
                        that.shapes.splice(i, 1);
                        i--;
                    }
                    var start = new YChart.Shape.Start(that.canvas, { x: 30, y: 30 });
                    that.appendShape(start);
                    var end = new YChart.Shape.End(that.canvas, { x: 80, y: 30 });
                    that.appendShape(end);
                }
            });
        }
        rtoolbar.appendChild(button);
        if (window.parent && typeof (window.parent.hideDrawPanel) == "function") {
            button = YChart.createElement("div", { className: "yc_rtoolbar_btn fa fa-refresh", title: "刷新" });
            button.onclick = function () {
                window.location.reload();
            }
            rtoolbar.appendChild(button);
        }
        button = YChart.createElement("div", { className: "yc_rtoolbar_btn fa fa-flag", title: "发布" });
        button.onclick = function () {
            var options = that.dialog.publish;
            options.api = {
                getSaveData: function () {
                    return that.getSaveData();
                },
                afterSave: function () {
                    window.parent.refreshGrid && window.parent.refreshGrid();
                    window.parent.hideDrawPanel && window.parent.hideDrawPanel();
                }
            };
            new YChart.Dialog(options);
        }
        rtoolbar.appendChild(button);
        button = YChart.createElement("div", {
            className: "yc_rtoolbar_btn fa fa-crop", title: "适应画布大小"
        });
        button.onclick = function () {
            var minLeft = undefined, minTop = undefined, maxBottom = undefined, maxRight = undefined;
            for (var i = 0; i < that.shapes.length; i++) {
                if (!that.shapes[i].symbol || that.shapes[i].symbol == "transition") {
                    continue;
                }
                if (minLeft != undefined) {
                    var left = that.shapes[i].getCoord("x", "left");
                    var right = that.shapes[i].getCoord("x", "right");
                    var top = that.shapes[i].getCoord("y", "top");
                    var bottom = that.shapes[i].getCoord("y", "bottom");
                    if (minLeft > left) {
                        minLeft = left;
                    }
                    if (minTop > top) {
                        minTop = top;
                    }
                    if (maxRight < right) {
                        maxRight = right;
                    }
                    if (maxBottom < bottom) {
                        maxBottom = bottom;
                    }
                }
                else {
                    minLeft = that.shapes[i].getCoord("x", "left");
                    maxRight = that.shapes[i].getCoord("x", "right");
                    minTop = that.shapes[i].getCoord("y", "top");
                    maxBottom = that.shapes[i].getCoord("y", "bottom");
                }
            }
            that.changeCanvasSize(maxRight + minLeft, maxBottom + minTop);
        }
        rtoolbar.appendChild(button);
    }

    //初始化右下边工具栏
    function initRContent(content) {
        content.style.fontSize = "12px";
        var div_right = YChart.createElement("div", null, { marginTop: "15px", marginLeft: "10px" });
        content.appendChild(div_right);
        if (that.data) {
            var div_name = YChart.createElement("div");
            div_name.appendChild(document.createTextNode(that.data.name));
            div_right.appendChild(div_name);
        }
        var div = YChart.createElement("div", null, { marginTop: "5px" });
        div_right.appendChild(div);
        div_right.style.width = (content.clientWidth - 20) + "px";
        div.appendChild(document.createTextNode("画布尺寸"));
        div = YChart.createElement("div", null, { marginLeft: "23px" });
        div_right.appendChild(div);
        div.appendChild(document.createTextNode("宽度："));
        var w = YChart.createElement("input", { className: "yc_textbox", type: "number" });
        w.value = that.canvas.clientWidth;
        w.onchange = function () {
            if (!isNaN(this.value)) {
                that.canvas.setAttributeNS(null, "width", this.value);
                var viewBox = that.canvas.getAttributeNS(null, "viewBox").split(" ");
                that.canvas.setAttributeNS(null, "viewBox", "0 0 " + this.value + " " + viewBox[3]);
            }
            else {
                this.value = that.canvas.clientWidth;
            }
        }
        div.appendChild(w);
        div = YChart.createElement("div", null, { marginLeft: "23px" });
        div_right.appendChild(div);
        div.appendChild(document.createTextNode("高度："));
        var h = YChart.createElement("input", { className: "yc_textbox", type: "number" });
        h.value = that.canvas.clientHeight;
        h.onchange = function () {
            if (!isNaN(this.value)) {
                that.canvas.setAttributeNS(null, "height", this.value);
                var viewBox = that.canvas.getAttributeNS(null, "viewBox").split(" ");
                that.canvas.setAttributeNS(null, "viewBox", "0 0 " + viewBox[2] + " " + this.value);
            }
            else {
                this.value = that.canvas.clientHeight;
            }
        }
        div.appendChild(h);
        that.changeCanvasSize = function (width, height) {
            if (!height || isNaN(height)) {
                height = that.canvas.getAttributeNS(null, "height");
            }
            if (!width || isNaN(width)) {
                width = that.canvas.getAttributeNS(null, "width");
            }
            that.canvas.setAttributeNS(null, "height", height);
            that.canvas.setAttributeNS(null, "width", width);
            that.canvas.setAttributeNS(null, "viewBox", "0 0 " + width + " " + height);
            w.value = width;
            h.value = height;
        }
        div = YChart.createElement("div", null, { marginTop: "5px", display: "none" });
        div.appendChild(document.createTextNode("绘图比例："));
        var ds = YChart.createElement("input", { className: "yc_textbox", type: "number", step: "5" }, { marginRight: "2px" });
        ds.value = 100;
        ds.onchange = function () {
            if (isNaN(this.value)) {
                return;
            }
            var v = parseFloat(this.value);
            if (v < 0) {
                return;
            }
            var height = that.canvas.offsetHeight * 100 / v;
            var width = that.canvas.offsetWidth * 100 / v;
            that.canvas.setAttributeNS(null, "viewBox", " 0 0 " + width + " " + height)
        }
        div.appendChild(ds);
        div.appendChild(document.createTextNode("%"));
        div_right.appendChild(div);
        div = YChart.createElement("div", null, { marginTop: "5px", width: "100%", float: "left" });
        div_right.appendChild(div);
        var sdiv = YChart.createElement("div", null, { height: "18px", lineHeight: "18px", float: "left", cursor: "default", display: "block" });
        div.appendChild(sdiv);
        sdiv.onclick = function () {
            this.childNodes[0].childNodes[0].checked = !this.childNodes[0].childNodes[0].checked;
            this.childNodes[0].childNodes[0].onchange();
        }
        var dd = YChart.createElement("div", null, { float: "left" });
        sdiv.appendChild(dd);
        var cb = YChart.createElement("input", { type: "checkbox", checked: true });
        dd.appendChild(cb);
        cb.onclick = function (event) {
            YChart.stopBubble(event);
        }
        cb.onchange = function () {
            if (this.checked) {
                YChart.removeClass(that.canvas, "yc_canvas_nogrid");
                YChart.addClass(that.canvas, "yc_canvas_grid");
            }
            else {
                YChart.addClass(that.canvas, "yc_canvas_nogrid");
                YChart.removeClass(that.canvas, "yc_canvas_grid");
            }
        }
        sdiv.appendChild(document.createTextNode("显示网格线"));
        div = YChart.createElement("div", null, { marginTop: "5px", width: "100%", float: "left" });
        div_right.appendChild(div);
        sdiv = YChart.createElement("div", null, { height: "18px", lineHeight: "18px", float: "left", cursor: "default", display: "block" });
        div.appendChild(sdiv);
        sdiv.onclick = function () {
            this.childNodes[0].childNodes[0].checked = !this.childNodes[0].childNodes[0].checked;
            this.childNodes[0].childNodes[0].onchange();
        }
        dd = YChart.createElement("div", null, { float: "left" });
        sdiv.appendChild(dd);
        cb = YChart.createElement("input", { type: "checkbox", checked: YChart.snapDrag });
        dd.appendChild(cb);
        cb.onclick = function (event) {
            YChart.stopBubble(event);
        }
        cb.onchange = function () {
            YChart.snapDrag = this.checked;
        }
        sdiv.appendChild(document.createTextNode("对齐拖动"));
        //添加按钮属性设置
        var btnProperty = YChart.createElement("div", { className: "yc_btn2" }, { width: (div_right.clientWidth - 2) + "px" }, "属性设置");
        div_right.appendChild(btnProperty);
        btnProperty.onclick = function () {
            var options = that.dialog.process;
            options.api = {
                getSource: function () {
                    return { name: that.name, variables: that.variables };
                },
                saveData: function (data) {
                    for (var i = 0; i < that.variables.length; i++) {
                        that.variables.splice(i, 1);
                        i--;
                    }
                    for (var i = 0; i < data.variables.length; i++) {
                        that.variables.push(data.variables[i]);
                    }
                }
            };
            new YChart.Dialog(options);
        }
    }

    function initCanvas() {
        //创建定义
        var defs = document.createElementNS(YChart.ns, "defs");
        that.canvas.appendChild(defs);
        //加上阴影滤镜
        var shadow_m1 = document.createElementNS(YChart.ns, "filter");
        shadow_m1.setAttributeNS(null, "id", "shadow_m1");
        defs.appendChild(shadow_m1);
        shadow_m1.appendChild(YChart.createElementNS("feOffset", { result: "offOut", "in": "SourceGraphic", dx: "1", dy: "1" }));
        shadow_m1.appendChild(YChart.createElementNS("feGaussianBlur", { result: "blurOut", "in": "offOut", stdDeviation: "2" }));
        shadow_m1.appendChild(YChart.createElementNS("feBlend", { "in": "SourceGraphic", "in2": "blurOut", mode: "normal" }));
        document.body.onkeyup = function (event) {
            if (that.selected) {
                switch (event.keyCode) {
                    case 46:
                        that.removeSelected();
                        break;
                }
            }
        }
        document.body.onkeydown = function (event) {
            if (that.selected) {
                switch (event.keyCode) {
                    case 38:
                        //向上
                        that.selected.move("y", -5);
                        break;
                    case 39:
                        //向右
                        that.selected.move("x", 5);
                        break;
                    case 40:
                        //向下
                        that.selected.move("y", 5);
                        break;
                    case 37:
                        //向左
                        that.selected.move("x", -5);
                        break;
                }
            }
        }
        YChart.attachEvent(that.canvas, "mousedown", function (event) {
            YChart.onMouseDown(event, that);
        });
        //YChart.attachEvent(that.canvas, "mousemove", function (event) {
        //    that.setCoordinate(that.getPosition(event));
        //});
        YChart.attachEvent(that.canvas, "click", function (event) {
            if (YChart.isReleased && that.selected) {
                that.unselect(that.selected);
            }
        });
        //初始化时插入开始、结束节点
        var start = new YChart.Shape.Start(that.canvas, { x: 30, y: 30, isViewMode: that.isViewMode });
        that.appendShape(start);
        var end = new YChart.Shape.End(that.canvas, { x: 80, y: 30, isViewMode: that.isViewMode });
        that.appendShape(end);
    }

    //加载数据
    function loadData() {
        if (!that.data || !that.data.xml) {
            return;
        }
        that.name = that.data.name;
        var xmlDocument = YChart.loadXml(that.data.xml);
        if (!that.isViewMode) {
            that.changeCanvasSize(parseFloat(xmlDocument.documentElement.getAttribute("width")), parseFloat(xmlDocument.documentElement.getAttribute("height")));
            if (xmlDocument.documentElement.getAttribute("left")) {
                that.canvas.style.left = xmlDocument.documentElement.getAttribute("left");
            }
            if (xmlDocument.documentElement.getAttribute("top")) {
                that.canvas.style.top = xmlDocument.documentElement.getAttribute("top");
            }
        }
        else {
            var height = parseFloat(xmlDocument.documentElement.getAttribute("height"));
            var width = parseFloat(xmlDocument.documentElement.getAttribute("width"));
            that.canvas.setAttributeNS(null, "height", height);
            that.canvas.setAttributeNS(null, "width", width);
            that.canvas.setAttributeNS(null, "viewBox", "0 0 " + width + " " + height);
            if (height > that.canvas.parentNode.clientHeight) {
                that.canvas.style.top = "30px";
            }
            else {
                height = (that.canvas.parentNode.clientHeight - height) / 2.6;
                if (height < 20) {
                    height = 20;
                }
                that.canvas.style.top = height + "px";
            }
            if (width > that.canvas.parentNode.clientWidth) {
                that.canvas.style.left = "30px";
            }
            else {
                width = (that.canvas.parentNode.clientWidth - width) / 2;
                if (width < 20) {
                    width = 20;
                }
                that.canvas.style.left = width + "px";
            }
        }
        var start = YChart.selectSingleNode(xmlDocument, "process/start");
        if (start) {
            var shape_start = that.findShape("start");
            shape_start.generatePoints(false);
            shape_start.moveTo(parseFloat(start.getAttribute("ui_cx")), parseFloat(start.getAttribute("ui_cy")), parseFloat(start.getAttribute("ui_r")));
        }
        var end = YChart.selectSingleNode(xmlDocument, "process/end");
        if (end) {
            var shape_end = that.findShape("end");
            shape_end.generatePoints(false);
            shape_end.moveTo(parseFloat(end.getAttribute("ui_cx")), parseFloat(end.getAttribute("ui_cy")), parseFloat(end.getAttribute("ui_r")));
        }
        var activities = YChart.selectNodes(xmlDocument, "process/activity|process/signactivity");
        for (var i = 0; i < activities.length; i++) {
            //加入activity
            var activity = new YChart.Shape.Activity(that.canvas, {
                x: parseFloat(activities[i].getAttribute("ui_x")),
                y: parseFloat(activities[i].getAttribute("ui_y")),
                displayName: activities[i].getAttribute("displayname"),
                isAppend: "1",
                isViewMode: that.isViewMode
            });
            activity.name = activities[i].getAttribute("name");
            if (activities[i].tagName == "signactivity") {
                activity.mode = "2";
                activity.processMode = activities[i].getAttribute("processmode");
            }
            else {
                activity.mode = "1";
            }
            that.appendShape(activity);
            activity.generatePoints(false);
            activity.isUseLaunchDepartment = activities[i].getAttribute("isUseLaunchDepartment") || "0";
            activity.isUseCurrentDepartment = activities[i].getAttribute("isUseCurrentDepartment") || "0";
            activity.users = activities[i].getAttribute("users") || "";
            activity.roles = activities[i].getAttribute("roles") || "";
            activity.actorMode = activities[i].getAttribute("actorMode") || "1";
            activity.actorVariableName = activities[i].getAttribute("actorVariableName") || "";
            activity.getActorSystemVariableMode = activities[i].getAttribute("getActorSystemVariableMode") || "1";
            if (that.data.steps) {
                for (var j = 0; j < that.data.steps.length; j++) {
                    if (that.data.steps[j].Name == activity.name) {
                        activity.ui.style.fill = that.data.steps[j].Color;
                        activity.setTip(that.data.steps[j].ActorsInfo, that.data.steps[j].StartTime, that.data.steps[j].EndTime);
                    }
                }
            }
            //var variables = YChart.selectNodes(activities[i], "variable");
            //for (var j = 0; j < variables.length; j++) {
            //    activity.variables.push({
            //        name: variables[j].getAttribute("name"),
            //        type: variables[j].getAttribute("type"),
            //        value: variables[j].getAttribute("value") || null
            //    });
            //}
            var backSteps = YChart.selectNodes(activities[i], "backsteps/backstep");
            for (var j = 0; j < backSteps.length; j++) {
                activity.backSteps.push(backSteps[j].textContent);
            }
            var virtualTransitions = YChart.selectNodes(activities[i], "transition[@virtual]");
            for (var j = 0; j < virtualTransitions.length; j++) {
                var vt = {
                    id: (j + 1),
                    name: virtualTransitions[j].getAttribute("name"),
                    type: virtualTransitions[j].getAttribute("type")
                };
                if (vt.type == "1") {
                    vt.toName = virtualTransitions[j].getAttribute("to");
                    vt.expression = virtualTransitions[j].getAttribute("expression");
                }
                else if (vt.type == "2") {
                    vt.variableName = virtualTransitions[j].getAttribute("variableName");
                }
                activity.virtualTransitions.push(vt);
            }
        }
        var transitions = YChart.selectNodes(xmlDocument, "//transition[not(@virtual)]");
        for (var i = 0; i < transitions.length; i++) {
            YChart.Shape.Transition.insert(that, that.findShape(transitions[i].getAttribute("from"))[transitions[i].getAttribute("from_position") + "Point"], that.findShape(transitions[i].getAttribute("to"))[transitions[i].getAttribute("to_position") + "Point"], transitions[i].getAttribute("name"), transitions[i].getAttribute("expression"));
        }
        //处理虚拟线条
        for (var i = 0; i < that.shapes.length; i++) {
            if (!that.shapes[i].virtualTransitions) {
                continue;
            }
            for (var j = 0; j < that.shapes[i].virtualTransitions.length; j++) {
                if (that.shapes[i].virtualTransitions[j].type == "1" && that.shapes[i].virtualTransitions[j].toName) {
                    that.shapes[i].virtualTransitions[j].to = that.findShape(that.shapes[i].virtualTransitions[j].toName);
                }
            }
        }
        //for (var i = 0; i < activities.length; i++) {
        //    var decisions = YChart.selectNodes(activities[i], "decision");
        //    if (decisions.length == 0) {
        //        continue;
        //    }
        //    var activity = that.findShape(activities[i].getAttribute("name"));
        //    var lt = activity.getLeaveTransitions();
        //    for (var j = 0; j < decisions.length; j++) {
        //        var target = decisions[j].getAttribute("target");
        //        if (!target) {
        //            continue;
        //        }
        //        var isProcess = 0;
        //        for (var k = 0; k < lt.length; k++) {
        //            if (lt[k].name == target) {
        //                activity.decisions.push({
        //                    expression: decisions[j].getAttribute("expression"),
        //                    target: lt[k]
        //                });
        //                isProcess = 1;
        //                break;
        //            }
        //        }
        //        if (isProcess == 1) {
        //            continue;
        //        }
        //        if (activity.virtualTransitions) {
        //            for (var k = 0; k < activity.virtualTransitions.length; k++) {
        //                if (activity.virtualTransitions[k].name == target) {
        //                    activity.decisions.push({
        //                        expression: decisions[j].getAttribute("expression"),
        //                        target: activity.virtualTransitions[k]
        //                    });
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
        var variables = YChart.selectNodes(xmlDocument, "process/variables/variable");
        for (var i = 0; i < variables.length; i++) {
            var v = { name: variables[i].getAttribute("name"), type: variables[i].getAttribute("type"), value: variables[i].getAttribute("value") || "" }
            that.variables.push(v);
            //that.displayProcessVariable(v);
        }
    }
}

//通过name获取出相应的形状
YChart.Process.prototype.findShape = function (shapeName) {
    for (var i = 0; i < this.shapes.length; i++) {
        if (this.shapes[i].name == shapeName) {
            return this.shapes[i];
        }
    }
    return null;
}

//开始移动
YChart.Process.prototype.beginMove = function (event) {
    if (!this.choosedShape) {
        return;
    }
    var that = this;
    if (this.choosedShape == "reset") {
        if (!event.altKey && !this.isViewMode) {
            return;
        }
        document.body.style.cursor = "hand";
        this.canvas.style.cursor = "hand";
        YChart.adorner = { x: parseFloat(this.canvas.style.left.replace("px", "")), y: parseFloat(this.canvas.style.top.replace("px", "")), initX: event.clientX, initY: event.clientY, chart: this };
        YChart.attachEvent(document.body, "mousemove", YChart.Process.OnMouseMove);
        YChart.attachEvent(document.body, "mouseup", YChart.Process.OnMouseUpOrLeave);
        YChart.attachEvent(document.body, "mouseleave", YChart.Process.OnMouseUpOrLeave);
    }
    else if (this.choosedShape == "activity") {
        var activity = new YChart.Shape.Activity(this.canvas, this.getPosition(event));
        this.appendShape(activity);
        this.resetChooseShape();
        activity.beginMove(event);
    }
}

//在鼠标移动过程中
YChart.Process.OnMouseMove = function (event) {
    //var temp = YChart.adorner.x + event.clientX - YChart.adorner.initX;
    //if (temp > 0 && temp + YChart.adorner.chart.canvas.clientWidth < YChart.adorner.chart.canvas.parentNode.clientWidth) {
    //    YChart.adorner.chart.canvas.style.left = temp + "px";
    //}
    //temp = YChart.adorner.y + event.clientY - YChart.adorner.initY;
    //if (temp > 0 && temp + YChart.adorner.chart.canvas.clientHeight < YChart.adorner.chart.canvas.parentNode.clientHeight) {
    //    YChart.adorner.chart.canvas.style.top = temp + "px";
    //}
    YChart.adorner.chart.canvas.style.left = (YChart.adorner.x + event.clientX - YChart.adorner.initX) + "px";
    YChart.adorner.chart.canvas.style.top = (YChart.adorner.y + event.clientY - YChart.adorner.initY) + "px";
}

//鼠标松开或离开作用范围
YChart.Process.OnMouseUpOrLeave = function (event) {
    document.body.style.cursor = "default";
    YChart.adorner.chart.canvas.style.cursor = "default";
    YChart.removeEvent(document.body, "mousemove", YChart.Process.OnMouseMove);
    YChart.removeEvent(document.body, "mouseup", YChart.Process.OnMouseUpOrLeave);
    YChart.removeEvent(document.body, "mouseleave", YChart.Process.OnMouseUpOrLeave);
    delete YChart.adorner;
}

//获取位置
YChart.Process.prototype.getPosition = function (e) {
    var rect = this.canvas.getBoundingClientRect();
    return { x: e.clientX - rect.left, y: e.clientY - rect.top };
}

//验证名字是否已经存在
YChart.Process.prototype.isExistsName = function (name, exclueShape) {
    for (var i = 0; i < this.shapes.length; i++) {
        if (exclueShape && this.shapes[i] == exclueShape) {
            continue;
        }
        if (this.shapes[i].name == name) {
            return true;
        }
    }
    return false;
}

//获取出按钮（现在缺少可拖动）
YChart.toolbarButton = function (icon, title, shape, onclick) {
    var div = YChart.createElement("div", { className: "yc_toolbar_btn fa " + icon, title: title, shape: shape });
    if (onclick) {
        div.onclick = onclick;
    }
    return div;
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

//选中元素
YChart.Process.prototype.select = function (shape) {
    if (this.selected) {
        this.selected.unselect();
    }
    this.selected = shape;
    shape.select();
}

//取消选中元素
YChart.Process.prototype.unselect = function (shape) {
    shape.unselect();
    this.selected = null;
}

//获取元素
YChart.Process.prototype.getShape = function (ui) {
    for (var i = 0; i < this.shapes.length; i++) {
        if (this.shapes[i].ui == ui) {
            return this.shapes[i];
        }
    }
}

//添加元素
YChart.Process.prototype.appendShape = function (shape) {
    shape.chart = this;
    this.shapes.push(shape);
}

//移除被选中的元素
YChart.Process.prototype.removeSelected = function (shape) {
    if (!this.selected) {
        return;
    }
    this.selected.remove();
    for (var i = 0; i < this.shapes.length; i++) {
        if (this.shapes[i] == this.selected) {
            this.shapes.splice(i, 1);
        }
    }
}

YChart.Process.prototype.getSaveData = function () {
    var xmlDocument = YChart.loadXml("<process></process>");
    xmlDocument.documentElement.setAttribute("height", this.canvas.getAttributeNS(null, "height"));
    xmlDocument.documentElement.setAttribute("width", this.canvas.getAttributeNS(null, "width"));
    xmlDocument.documentElement.setAttribute("top", this.canvas.style.top);
    xmlDocument.documentElement.setAttribute("left", this.canvas.style.left);
    var start = this.findShape("start");
    processActivity(start);
    var end = this.findShape("end");
    var endElement = xmlDocument.createElement("end");
    endElement.setAttribute("name", end.name);
    endElement.setAttribute("displayname", end.displayName);
    endElement.setAttribute("ui_cx", end.ui.getAttributeNS(null, "cx"));
    endElement.setAttribute("ui_cy", end.ui.getAttributeNS(null, "cy"));
    endElement.setAttribute("ui_r", end.ui.getAttributeNS(null, "r"));
    xmlDocument.documentElement.appendChild(endElement);
    for (var i = 0; i < this.shapes.length; i++) {
        if (this.shapes[i].isProcessed) {
            delete this.shapes[i].isProcessed;
        }
    }
    var variables = xmlDocument.createElement("variables");
    xmlDocument.documentElement.appendChild(variables);
    //添加流程变量
    for (var i = 0; i < this.variables.length; i++) {
        var variableElment = xmlDocument.createElement("variable");
        variableElment.setAttribute("name", YChart.trim(this.variables[i].name));
        variableElment.setAttribute("type", this.variables[i].type);
        if (this.variables[i].value) {
            variableElment.setAttribute("value", this.variables[i].value);
        }
        variables.appendChild(variableElment);
    }
    function processActivity(shape) {
        if (shape.symbol == "end" || shape.isProcessed == true) {
            return;
        }
        var tag = "";
        shape.isProcessed = true;
        if (shape.symbol == "activity" && shape.mode == "2") {
            tag = "signactivity";
        }
        else {
            tag = shape.symbol;
        }
        var element = xmlDocument.createElement(tag);
        xmlDocument.documentElement.appendChild(element);
        if (tag == "signactivity") {
            element.setAttribute("processmode", shape.processMode);
        }
        element.setAttribute("name", shape.name);
        element.setAttribute("displayname", shape.displayName);
        if (shape.symbol == "activity") {
            element.setAttribute("ui_x", shape.ui.getAttributeNS(null, "x"));
            element.setAttribute("ui_y", shape.ui.getAttributeNS(null, "y"));
            element.setAttribute("ui_height", shape.ui.getAttributeNS(null, "height"));
            element.setAttribute("ui_width", shape.ui.getAttributeNS(null, "width"));
            element.setAttribute("isUseLaunchDepartment", shape.isUseLaunchDepartment);
            element.setAttribute("isUseCurrentDepartment", shape.isUseCurrentDepartment);
        }
        else if (shape.symbol == "start" || shape.symbol == "end") {
            element.setAttribute("ui_cx", shape.ui.getAttributeNS(null, "cx"));
            element.setAttribute("ui_cy", shape.ui.getAttributeNS(null, "cy"));
            element.setAttribute("ui_r", shape.ui.getAttributeNS(null, "r"));
        }
        var leaveTransitions = shape.getLeaveTransitions();
        for (var i = 0; i < leaveTransitions.length; i++) {
            var tElement = xmlDocument.createElement("transition");
            tElement.setAttribute("name", leaveTransitions[i].name);
            tElement.setAttribute("from", element.getAttribute("name"));
            tElement.setAttribute("to", (leaveTransitions[i].toActivityPoint ? leaveTransitions[i].toActivityPoint.source.name : ""));
            tElement.setAttribute("expression", leaveTransitions[i].expression);
            tElement.setAttribute("from_position", leaveTransitions[i].fromActivityPoint.position);
            tElement.setAttribute("to_position", (leaveTransitions[i].toActivityPoint ? leaveTransitions[i].toActivityPoint.position : ""));
            tElement.setAttribute("isDefault", leaveTransitions[i].isDefault ? "1" : "0");
            element.appendChild(tElement);
        }
        if (shape.users) {
            element.setAttribute("users", shape.users);
        }
        if (shape.roles) {
            element.setAttribute("roles", shape.roles);
        }
        if (shape.actorMode) {
            element.setAttribute("actorMode", shape.actorMode);
            element.setAttribute("actorVariableName", shape.actorVariableName);
            element.setAttribute("getActorSystemVariableMode", shape.getActorSystemVariableMode);
        }
        if (shape.backSteps && shape.backSteps.length > 0) {
            var bElement = xmlDocument.createElement("backsteps");
            element.appendChild(bElement)
            for (var i = 0; i < shape.backSteps.length; i++) {
                if (!shape.chart.findShape(shape.backSteps[i])) {
                    continue;
                }
                var bsElement = xmlDocument.createElement("backstep");
                bsElement.textContent = shape.backSteps[i];
                bElement.appendChild(bsElement)
            }
        }
        //if (shape.variables) {
        //    for (var i in shape.variables) {
        //        var tElement = xmlDocument.createElement("variable");
        //        tElement.setAttribute("name", shape.variables[i].name);
        //        tElement.setAttribute("value", shape.variables[i].value);
        //        tElement.setAttribute("type", shape.variables[i].type);
        //        element.appendChild(tElement);
        //    }
        //}
        if (shape.virtualTransitions) {
            for (var i = 0; i < shape.virtualTransitions.length; i++) {
                //debugger;
                //if (!shape.virtualTransitions[i].name) {
                //    continue;
                //}
                var tElement = xmlDocument.createElement("transition");
                if (shape.virtualTransitions[i].type == "1") {
                    //节点
                    if (!shape.virtualTransitions[i].to) {
                        continue;
                    }
                    tElement.setAttribute("to", shape.virtualTransitions[i].to.name);
                    tElement.setAttribute("expression", YChart.trim(shape.virtualTransitions[i].expression));
                }
                else {
                    //变量
                    if (shape.virtualTransitions[i].variableName == "") {
                        continue;
                    }
                    tElement.setAttribute("variablename", shape.virtualTransitions[i].variableName);
                }
                tElement.setAttribute("name", YChart.trim(shape.virtualTransitions[i].name));
                //tElement.setAttribute("from", shape.virtualTransitions[i].from.name);
                tElement.setAttribute("type", shape.virtualTransitions[i].type);
                tElement.setAttribute("virtual", "1");
                element.appendChild(tElement);
            }
        }
        for (var i in leaveTransitions) {
            if (leaveTransitions[i].toActivityPoint) {
                processActivity(leaveTransitions[i].toActivityPoint.source);
            }
        }
        //if (shape.decisions) {
        //    for (var i in shape.decisions) {
        //        if (!shape.decisions[i].target) {
        //            return;
        //        }
        //        var dElement = xmlDocument.createElement("decision");
        //        dElement.setAttribute("expression", shape.decisions[i].expression);
        //        dElement.setAttribute("target", shape.decisions[i].target.name);
        //        element.appendChild(dElement);
        //    }
        //}
    }
    return xmlDocument.documentElement.outerHTML;
}

//定义一些常用选项
YChart.Data = {
    activityMode: [{ id: 1, label: "普通" }, {
        id: 2, label: "会签"
    }],
    //variableType: ["string", "decimal", "List<string>", "List<decimal>"],
    transitionDefault: [{ id: 1, label: "是" }, {
        id: 0, label: "否"
    }]
}

//工具
YChart.Utility = {
    insertCell: function (tr, attributes, styles) {
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
}