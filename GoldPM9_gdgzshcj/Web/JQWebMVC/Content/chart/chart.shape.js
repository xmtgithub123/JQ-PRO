/*
    dependences: core,tab,dialog,menu
*/
"use strict"
if (typeof (YChart) == "undefined") {
    window.YChart = {};
}
if (typeof (YChart.Shape == "undefined")) {
    YChart.Shape = {};
}

YChart.Shape.setCircleTextLocation = function (ui, text) {
    text.setAttributeNS(null, "x", ui.cx.baseVal.value - text.clientWidth / 2 + 1);
    text.setAttributeNS(null, "y", ui.cy.baseVal.value + text.clientHeight / 2 - 3);
}

YChart.Shape.setVirtualTransitions = function (shape, virtualTransitions) {
    for (var i = 0; i < shape.virtualTransitions.length; i++) {
        shape.virtualTransitions.splice(i, 1);
        i--;
    }
    for (var i = 0; i < virtualTransitions.length; i++) {
        if (virtualTransitions[i].type == "1") {
            if (!virtualTransitions[i].toName) {
                continue;
            }
            var to = shape.chart.findShape(virtualTransitions[i].toName);
            if (!to) {
                continue;
            }
            virtualTransitions[i].to = to;
        }
        shape.virtualTransitions.push(virtualTransitions[i]);
    }
}

YChart.Shape.setActivityDisplayName = function (textUI, text) {
    var texts = text.split("\n");
    YChart.clear(textUI);
    var height = 0;
    for (var i = 0; i < texts.length; i++) {
        if (texts[i] == "") {
            continue;
        }
        var text = document.createElementNS(YChart.ns, "text");
        text.setAttributeNS(null, "class", "yc_text_name");
        text.setAttributeNS(null, "x", "0");
        text.appendChild(document.createTextNode(texts[i]));
        textUI.appendChild(text);
        height += text.getBBox().height;
        text.setAttributeNS(null, "y", height);
        height += 1;
    }
    if (textUI.childNodes.length > 0) {
        var maxWidth = textUI.getBBox().width;
        for (var i = 0; i < textUI.childNodes.length; i++) {
            if (maxWidth > textUI.childNodes[i].clientWidth) {
                textUI.childNodes[i].setAttributeNS(null, "x", (maxWidth - textUI.childNodes[i].clientWidth) / 2);
            }
        }
    }
}

//定义开始元素
YChart.Shape.Start = function (canvas, option) {
    this.symbol = "start";
    this.name = "start";
    this.displayName = "开始";
    this.ui = document.createElementNS(YChart.ns, "circle");
    this.ui.setAttributeNS(null, "class", "yc_start");
    this.ui.setAttributeNS(null, "cx", option.x);
    this.ui.setAttributeNS(null, "cy", option.y);
    this.ui.setAttributeNS(null, "r", 20);
    canvas.appendChild(this.ui);
    this.text = document.createElementNS(YChart.ns, "text");
    this.text.appendChild(document.createTextNode("开始"));
    this.text.setAttributeNS(null, "class", "yc_text_name");
    canvas.appendChild(this.text);
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
    var that = this;
    this.virtualTransitions = [];
    if (option.isViewMode == true) {
        return;
    }
    YChart.attachEvent([this.ui, this.text], "mousedown", function (event) {
        YChart.onMouseDown(event, that);
    });
    YChart.attachEvent([this.ui, this.text], "click", function (event) {
        that.chart.select(that);
        YChart.stopBubble(event);
    });
    YChart.attachEvent([this.ui, this.text], "dblclick", function () {
        setTimeout(function () {
            YChart.Shape.Start.onMouseUpOrLeave();
        }, 150);
        var options = that.chart.dialog.node;
        options.title = "开始";
        options.api = {
            getSource: function () {
                return that;
            },
            getShapes: function () {
                return that.chart.shapes;
            },
            saveData: function (data) {
                if (that.chart.isExistsName(data.name, that)) {
                    YChart.Dialog.alert("保存失败", "名称{" + data.name + "}已经存在");
                    return false;
                }
                //设置名称
                that.name == data.name;
                YChart.Shape.setVirtualTransitions(that, data.virtualTransitions);
            }
        };
        new YChart.Dialog(options);
        YChart.stopBubble();
    });
}

YChart.Shape.Start.prototype.beginMove = function (event) {
    YChart.adorner = { x: parseFloat(this.ui.getAttributeNS(null, "cx")), y: parseFloat(this.ui.getAttributeNS(null, "cy")), initX: event.clientX, initY: event.clientY, start: this };
    YChart.attachEvent(this.ui.parentNode, "mousemove", YChart.Shape.Start.onMouseMove);
    YChart.attachEvent(this.ui.parentNode, "mouseup", YChart.Shape.Start.onMouseUpOrLeave);
    YChart.attachEvent(this.ui.parentNode, "mouseleave", YChart.Shape.Start.onMouseUpOrLeave);
    this.chart.select(this);
}

YChart.Shape.Start.onMouseMove = function (event) {
    if (!YChart.adorner) {
        return;
    }
    if (!YChart.adorner.moving) {
        YChart.adorner.moving = document.createElementNS(YChart.ns, "circle");
        YChart.adorner.moving.setAttributeNS(null, "cx", YChart.adorner.start.ui.getAttributeNS(null, "cx"));
        YChart.adorner.moving.setAttributeNS(null, "cy", YChart.adorner.start.ui.getAttributeNS(null, "cy"));
        YChart.adorner.moving.setAttributeNS(null, "r", 18);
        YChart.adorner.moving.setAttributeNS(null, "class", "yc_start_moving");
        YChart.adorner.start.ui.parentNode.appendChild(YChart.adorner.moving);
    }
    var temp = YChart.adorner.x + event.clientX - YChart.adorner.initX;
    if (temp > 20 && temp + 20 < YChart.adorner.start.chart.canvas.clientWidth) {
        if (YChart.snapDrag) {
            var s = temp % 10;
            if (s != 0) {
                if (s < 5) {
                    temp = temp - s
                }
                else {
                    temp = temp + 10 - s;
                }
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "cx", temp);
    }
    temp = YChart.adorner.y + event.clientY - YChart.adorner.initY;
    if (temp > 20 && temp + 20 < YChart.adorner.start.chart.canvas.clientHeight) {
        if (YChart.snapDrag) {
            var s = temp % 10;
            if (s < 5) {
                temp = temp - s
            }
            else {
                temp = temp + 10 - s;
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "cy", temp);
    }
}

YChart.Shape.Start.onMouseUpOrLeave = function (event) {
    if (YChart.adorner) {
        YChart.removeEvent(YChart.adorner.start.ui.parentNode, "mousemove", YChart.Shape.Start.onMouseMove);
        YChart.removeEvent(YChart.adorner.start.ui.parentNode, "mouseup", YChart.Shape.Start.onMouseUpOrLeave);
        YChart.removeEvent(YChart.adorner.start.ui.parentNode, "mouseleave", YChart.Shape.Start.onMouseUpOrLeave);
        if (YChart.adorner.moving) {
            var x = parseFloat(YChart.adorner.moving.getAttributeNS(null, "cx"));
            YChart.adorner.start.ui.setAttributeNS(null, "cx", x);
            var y = parseFloat(YChart.adorner.moving.getAttributeNS(null, "cy"));
            YChart.adorner.start.ui.setAttributeNS(null, "cy", y);
            YChart.remove(YChart.adorner.moving);
            YChart.Shape.setCircleTextLocation(YChart.adorner.start.ui, YChart.adorner.start.text);
            if (YChart.adorner.start.topPoint) {
                var r = parseFloat(YChart.adorner.start.ui.getAttributeNS(null, "r"));
                YChart.adorner.start.topPoint.setPosition(x, y - r);
                YChart.adorner.start.rightPoint.setPosition(x + r, y);
                YChart.adorner.start.bottomPoint.setPosition(x, y + r);
                YChart.adorner.start.leftPoint.setPosition(x - r, y);
            }
            delete YChart.adorner.moving;
        }
        delete YChart.adorner;
        document.onselectstart = null;
    }
}

YChart.Shape.Start.prototype.select = function () {
    this.showPoints();
}

YChart.Shape.Start.prototype.unselect = function () {
    this.hidePoints();
}

YChart.Shape.Start.prototype.generatePoints = function (visible) {
    if (this.topPoint) {
        return;
    }
    var x = parseFloat(this.ui.getAttributeNS(null, "cx"));
    var y = parseFloat(this.ui.getAttributeNS(null, "cy"));
    var r = parseFloat(this.ui.getAttributeNS(null, "r"));
    //上
    this.topPoint = new YChart.Shape.Activity.Point({ x: x, y: y - r, source: this, position: "top" });
    this.ui.parentNode.appendChild(this.topPoint.ui);
    //右
    this.rightPoint = new YChart.Shape.Activity.Point({ x: x + r, y: y, source: this, position: "right" });
    this.ui.parentNode.appendChild(this.rightPoint.ui);
    //下
    this.bottomPoint = new YChart.Shape.Activity.Point({ x: x, y: y + r, source: this, position: "bottom" });
    this.ui.parentNode.appendChild(this.bottomPoint.ui);
    //左
    this.leftPoint = new YChart.Shape.Activity.Point({ x: x - r, y: y, source: this, position: "left" });
    this.ui.parentNode.appendChild(this.leftPoint.ui);
    if (visible === false) {
        this.hidePoints();
    }
}

YChart.Shape.Start.prototype.showPoints = function () {
    if (!this.topPoint) {
        this.generatePoints();
    }
    else {
        this.topPoint.show();
        this.topPoint.toFront();
        this.rightPoint.show();
        this.rightPoint.toFront();
        this.bottomPoint.show();
        this.bottomPoint.toFront();
        this.leftPoint.show();
        this.leftPoint.toFront();
    }
}

YChart.Shape.Start.prototype.hidePoints = function () {
    if (this.topPoint) {
        this.topPoint.hide();
        this.rightPoint.hide();
        this.bottomPoint.hide();
        this.leftPoint.hide();
    }
}

YChart.Shape.Start.prototype.getDefaultLeaveTransition = function () {
    var lts = this.getLeaveTransitions();
    for (var i = 0; i < lts.length; i++) {
        if (lts[i].isDefault) {
            return lts[i];
        }
    }
}

YChart.Shape.Start.prototype.getLeaveTransitions = function () {
    if (!this.topPoint) {
        return [];
    }
    return this.topPoint.toTransitions.concat(this.rightPoint.toTransitions.concat(this.bottomPoint.toTransitions.concat(this.leftPoint.toTransitions)));
}

YChart.Shape.Start.prototype.move = function (axis, step) {
    if (axis == "x") {
        var s = this.ui.cx.baseVal.value % 5;
        if (s == 0) {
            this.ui.cx.baseVal.value = this.ui.cx.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.cx.baseVal.value = this.ui.cx.baseVal.value - s;
            }
            else {
                this.ui.cx.baseVal.value = this.ui.cx.baseVal.value + 5 - s;
            }
        }
    }
    else if (axis == "y") {
        var s = this.ui.cy.baseVal.value % 5;
        if (s == 0) {
            this.ui.cy.baseVal.value = this.ui.cy.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.cy.baseVal.value = this.ui.cy.baseVal.value - s;
            }
            else {
                this.ui.cy.baseVal.value = this.ui.cy.baseVal.value + 5 - s;
            }
        }
    }
    var x = this.ui.cx.baseVal.value, y = this.ui.cy.baseVal.value;
    if (this.topPoint) {
        var r = parseFloat(this.ui.getAttributeNS(null, "r"));
        this.topPoint.setPosition(x, y - r);
        this.rightPoint.setPosition(x + r, y);
        this.bottomPoint.setPosition(x, y + r);
        this.leftPoint.setPosition(x - r, y);
    }
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
}

YChart.Shape.Start.prototype.moveTo = function (cx, cy, r) {
    this.ui.setAttributeNS(null, "cx", cx);
    this.ui.setAttributeNS(null, "cy", cy);
    r = parseFloat(this.ui.getAttributeNS(null, "r"));
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
    if (this.topPoint) {
        this.topPoint.setPosition(cx, cy - r);
        this.rightPoint.setPosition(cx + r, cy);
        this.bottomPoint.setPosition(cx, cy + r);
        this.leftPoint.setPosition(cx - r, cy);
    }
}

YChart.Shape.Start.prototype.getCoord = function (axis, position) {
    position = position.toLowerCase();
    if (axis == "x" || axis == "X") {
        if (position == "left") {
            return this.ui.cx.baseVal.value - this.ui.r.baseVal.value;
        }
        else if (position == "right") {
            return this.ui.cx.baseVal.value + this.ui.r.baseVal.value;
        }
    }
    else if (axis == "y" || axis == "Y") {
        if (position == "top") {
            return this.ui.cy.baseVal.value - this.ui.r.baseVal.value;
        }
        else if (position == "bottom") {
            return this.ui.cy.baseVal.value + this.ui.r.baseVal.value;
        }
    }
    return 0;
}

YChart.Shape.Start.prototype.remove = function () {
    YChart.remove(this.ui);
    if (this.topPoint) {
        this.topPoint.remove();
        this.leftPoint.remove();
        this.rightPoint.remove();
        this.bottomPoint.remove();
    }
    YChart.remove(this.text);
}

//定义结束元素
YChart.Shape.End = function (canvas, option) {
    this.symbol = "end";
    this.name = "end";
    this.displayName = "结束";
    this.ui = document.createElementNS(YChart.ns, "circle");
    this.ui.setAttributeNS(null, "class", "yc_end");
    this.ui.setAttributeNS(null, "cx", option.x);
    this.ui.setAttributeNS(null, "cy", option.y);
    this.ui.setAttributeNS(null, "r", 20);
    canvas.appendChild(this.ui);
    var that = this;
    this.text = document.createElementNS(YChart.ns, "text");
    this.text.appendChild(document.createTextNode(this.displayName));
    this.text.setAttributeNS(null, "class", "yc_text_name");
    canvas.appendChild(this.text);
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
    if (option.isViewMode == true) {
        return;
    }
    YChart.attachEvent([this.ui, this.text], "mousedown", function (event) {
        YChart.onMouseDown(event, that);
    });
    YChart.attachEvent([this.ui, this.text], "click", function (event) {
        that.chart.select(that);
        YChart.stopBubble(event);
    });
    YChart.attachEvent([this.ui, this.text], "dblclick", function () {
        setTimeout(function () {
            YChart.Shape.End.onMouseUpOrLeave();
        }, 150);
        var options = that.chart.dialog.node;
        options.title = "结束";
        options.api = {
            getSource: function () {
                return that;
            },
            getShapes: function () {
                return that.chart.shapes;
            },
            saveData: function (data) {
                if (that.chart.isExistsName(data.name, that)) {
                    YChart.Dialog.alert("保存失败", "名称{" + data.name + "}已经存在");
                    return false;
                }
                //设置名称
                that.name == data.name;
            }
        };
        new YChart.Dialog(options);
        YChart.stopBubble();
    });
}

YChart.Shape.End.prototype.beginMove = function (event) {
    YChart.adorner = { x: parseFloat(this.ui.getAttributeNS(null, "cx")), y: parseFloat(this.ui.getAttributeNS(null, "cy")), initX: event.clientX, initY: event.clientY, end: this };
    YChart.attachEvent(this.ui.parentNode, "mousemove", YChart.Shape.End.onMouseMove);
    YChart.attachEvent(this.ui.parentNode, "mouseup", YChart.Shape.End.onMouseUpOrLeave);
    YChart.attachEvent(this.ui.parentNode, "mouseleave", YChart.Shape.End.onMouseUpOrLeave);
    this.chart.select(this);
}

YChart.Shape.End.onMouseMove = function (event) {
    if (!YChart.adorner) {
        return;
    }
    if (!YChart.adorner.moving) {
        YChart.adorner.moving = document.createElementNS(YChart.ns, "circle");
        YChart.adorner.moving.setAttributeNS(null, "cx", YChart.adorner.end.ui.getAttributeNS(null, "cx"));
        YChart.adorner.moving.setAttributeNS(null, "cy", YChart.adorner.end.ui.getAttributeNS(null, "cy"));
        YChart.adorner.moving.setAttributeNS(null, "r", 18);
        YChart.adorner.moving.setAttributeNS(null, "class", "yc_end_moving");
        YChart.adorner.end.ui.parentNode.appendChild(YChart.adorner.moving);
    }
    var temp = YChart.adorner.x + event.clientX - YChart.adorner.initX;
    if (temp > 20 && temp + 20 < YChart.adorner.end.chart.canvas.clientWidth) {
        if (YChart.snapDrag) {
            var s = temp % 10;
            if (s != 0) {
                if (s < 5) {
                    temp = temp - s
                }
                else {
                    temp = temp + 10 - s;
                }
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "cx", temp);
    }
    temp = YChart.adorner.y + event.clientY - YChart.adorner.initY;
    if (temp > 20 && temp + 20 < YChart.adorner.end.chart.canvas.clientHeight) {
        if (YChart.snapDrag) {
            var s = temp % 10;
            if (s < 5) {
                temp = temp - s
            }
            else {
                temp = temp + 10 - s;
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "cy", temp);
    }
}

YChart.Shape.End.onMouseUpOrLeave = function (event) {
    if (!YChart.adorner || !YChart.adorner.moving) {
        return;
    }
    if (YChart.adorner) {
        YChart.removeEvent(YChart.adorner.end.ui.parentNode, "mousemove", YChart.Shape.End.onMouseMove);
        YChart.removeEvent(YChart.adorner.end.ui.parentNode, "mouseup", YChart.Shape.End.onMouseUpOrLeave);
        YChart.removeEvent(YChart.adorner.end.ui.parentNode, "mouseleave", YChart.Shape.End.onMouseUpOrLeave);
        if (YChart.adorner.moving) {
            var x = parseFloat(YChart.adorner.moving.getAttributeNS(null, "cx"));
            YChart.adorner.end.ui.setAttributeNS(null, "cx", x);
            var y = parseFloat(YChart.adorner.moving.getAttributeNS(null, "cy"));
            YChart.adorner.end.ui.setAttributeNS(null, "cy", y);
            YChart.remove(YChart.adorner.moving);
            YChart.Shape.setCircleTextLocation(YChart.adorner.end.ui, YChart.adorner.end.text);
            if (YChart.adorner.end.topPoint) {
                var r = parseFloat(YChart.adorner.end.ui.getAttributeNS(null, "r"));
                YChart.adorner.end.topPoint.setPosition(x, y - r);
                YChart.adorner.end.rightPoint.setPosition(x + r, y);
                YChart.adorner.end.bottomPoint.setPosition(x, y + r);
                YChart.adorner.end.leftPoint.setPosition(x - r, y);
            }
            delete YChart.adorner.moving;
        }
        document.onselectstart = null;
        delete YChart.adorner;
    }
}

YChart.Shape.End.prototype.select = function () {
    this.showPoints();
}

YChart.Shape.End.prototype.unselect = function () {
    this.hidePoints();
}

YChart.Shape.End.prototype.generatePoints = function (visible) {
    if (this.topPoint) {
        return;
    }
    var x = parseFloat(this.ui.getAttributeNS(null, "cx"));
    var y = parseFloat(this.ui.getAttributeNS(null, "cy"));
    var r = parseFloat(this.ui.getAttributeNS(null, "r"));
    //上
    this.topPoint = new YChart.Shape.Activity.Point({ x: x, y: y - r, source: this, position: "top" });
    this.ui.parentNode.appendChild(this.topPoint.ui);
    //右
    this.rightPoint = new YChart.Shape.Activity.Point({ x: x + r, y: y, source: this, position: "right" });
    this.ui.parentNode.appendChild(this.rightPoint.ui);
    //下
    this.bottomPoint = new YChart.Shape.Activity.Point({ x: x, y: y + r, source: this, position: "bottom" });
    this.ui.parentNode.appendChild(this.bottomPoint.ui);
    //左
    this.leftPoint = new YChart.Shape.Activity.Point({ x: x - r, y: y, source: this, position: "left" });
    this.ui.parentNode.appendChild(this.leftPoint.ui);
    if (visible === false) {
        this.hidePoints();
    }
}

YChart.Shape.End.prototype.showPoints = function () {
    if (!this.topPoint) {
        this.generatePoints();
    }
    else {
        this.topPoint.show();
        this.topPoint.toFront();
        this.rightPoint.show();
        this.rightPoint.toFront();
        this.bottomPoint.show();
        this.bottomPoint.toFront();
        this.leftPoint.show();
        this.leftPoint.toFront();
    }
}

YChart.Shape.End.prototype.hidePoints = function () {
    if (this.topPoint) {
        this.topPoint.hide();
        this.rightPoint.hide();
        this.bottomPoint.hide();
        this.leftPoint.hide();
    }
}

YChart.Shape.End.prototype.getCoord = function (axis, position) {
    position = position.toLowerCase();
    if (axis == "x" || axis == "X") {
        if (position == "left") {
            return this.ui.cx.baseVal.value - this.ui.r.baseVal.value;
        }
        else if (position == "right") {
            return this.ui.cx.baseVal.value + this.ui.r.baseVal.value;
        }
    }
    else if (axis == "y" || axis == "Y") {
        if (position == "top") {
            return this.ui.cy.baseVal.value - this.ui.r.baseVal.value;
        }
        else if (position == "bottom") {
            return this.ui.cy.baseVal.value + this.ui.r.baseVal.value;
        }
    }
    return 0;
}

YChart.Shape.End.prototype.move = function (axis, step) {
    if (axis == "x") {
        var s = this.ui.cx.baseVal.value % 5;
        if (s == 0) {
            this.ui.cx.baseVal.value = this.ui.cx.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.cx.baseVal.value = this.ui.cx.baseVal.value - s;
            }
            else {
                this.ui.cx.baseVal.value = this.ui.cx.baseVal.value + 5 - s;
            }
        }
    }
    else if (axis == "y") {
        var s = this.ui.cy.baseVal.value % 5;
        if (s == 0) {
            this.ui.cy.baseVal.value = this.ui.cy.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.cy.baseVal.value = this.ui.cy.baseVal.value - s;
            }
            else {
                this.ui.cy.baseVal.value = this.ui.cy.baseVal.value + 5 - s;
            }
        }
    }
    var x = this.ui.cx.baseVal.value, y = this.ui.cy.baseVal.value;
    if (this.topPoint) {
        var r = parseFloat(this.ui.getAttributeNS(null, "r"));
        this.topPoint.setPosition(x, y - r);
        this.rightPoint.setPosition(x + r, y);
        this.bottomPoint.setPosition(x, y + r);
        this.leftPoint.setPosition(x - r, y);
    }
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
}

YChart.Shape.End.prototype.moveTo = function (cx, cy, r) {
    this.ui.setAttributeNS(null, "cx", cx);
    this.ui.setAttributeNS(null, "cy", cy);
    //if (r) {
    //    this.ui.setAttributeNS(null, "r", r);
    //}
    //else {
    r = parseFloat(this.ui.getAttributeNS(null, "r"));
    //}
    //重定位文字
    YChart.Shape.setCircleTextLocation(this.ui, this.text);
    if (this.topPoint) {
        this.topPoint.setPosition(cx, cy - r);
        this.rightPoint.setPosition(cx + r, cy);
        this.bottomPoint.setPosition(cx, cy + r);
        this.leftPoint.setPosition(cx - r, cy);
    }
}

YChart.Shape.End.prototype.getLeaveTransitions = function () {
    if (!this.topPoint) {
        return [];
    }
    return this.topPoint.toTransitions.concat(this.rightPoint.toTransitions.concat(this.bottomPoint.toTransitions.concat(this.leftPoint.toTransitions)));
}

YChart.Shape.End.prototype.remove = function () {
    YChart.remove(this.ui);
    if (this.topPoint) {
        this.topPoint.remove();
        this.leftPoint.remove();
        this.rightPoint.remove();
        this.bottomPoint.remove();
    }
    YChart.remove(this.text);
}

/*
    定义活动元素
    @canvas: 画布元素
    @option: 选项
*/
YChart.Shape.Activity = function (canvas, option) {
    this.ui = document.createElementNS(YChart.ns, "rect");
    this.ui.setAttributeNS(null, "x", option.x);
    this.ui.setAttributeNS(null, "y", option.y);
    this.ui.setAttributeNS(null, "height", 40);
    this.ui.setAttributeNS(null, "width", 70);
    this.ui.setAttributeNS(null, "class", "yc_rect");
    if (!("isAppend" in option)) {
        this.ui.setAttributeNS(null, "n", "1");
    }
    canvas.appendChild(this.ui);
    var i = 1;
    while (document.getElementById("rect_" + i)) {
        i++;
    }
    this.ui.setAttributeNS(null, "id", "rect_" + i);
    if (option.name) {
        this.name = option.name;
    }
    else {
        this.name = "step" + i;
    }
    this.text = document.createElementNS(YChart.ns, "g");
    canvas.appendChild(this.text);
    if (!option.displayName) {
        option.displayName = "步骤" + i;
    }
    this.displayName = option.displayName;
    YChart.Shape.setActivityDisplayName(this.text, this.displayName);
    this.setTextLocation();
    var that = this;
    //1：普通 2：会签
    this.mode = "1";
    this.virtualTransitions = [];
    //this.variables = [];
    //this.decisions = [];
    this.backSteps = [];
    this.ui.setAttributeNS(null, "id", "rect_" + i);
    this.symbol = "activity";
    this.users = "";
    this.roles = "";
    this.isUseLaunchDepartment = "0";
    this.isUseCurrentDepartment = "0";
    if (option.isViewMode == true) {
        //是否为视图模式
        return;
    }
    YChart.attachEvent([this.ui, this.text], "mousedown", function (event) {
        if (event.which == 1) {
            //鼠标左键
            YChart.onMouseDown(event, that);
        }
    });
    YChart.attachEvent([this.ui, this.text], "click", function (event) {
        YChart.stopBubble(event);
        if (event.which == 1) {
            //鼠标左键
            that.chart.select(that);
        }
    });
    //加上双击事件
    YChart.attachEvent([this.ui, this.text], "dblclick", function (ev) {
        setTimeout(function () {
            YChart.Shape.Activity.onMouseUpOrLeave();
        }, 150);
        var options = that.chart.dialog.activity;
        options.title = "步骤属性";
        options.api = {
            getSource: function () {
                return that;
            },
            getShapes: function () {
                return that.chart.shapes;
            },
            saveData: function (data) {
                if (that.chart.isExistsName(data.name, that)) {
                    YChart.Dialog.alert("保存失败", "名称{" + data.name + "}已经存在");
                    return false;
                }
                //设置名称
                that.name == data.name;
                if (data.displayName != that.displayName) {
                    that.displayName = data.displayName;
                    YChart.Shape.setActivityDisplayName(that.text, data.displayName);
                    that.setTextLocation();
                }
                YChart.Shape.setVirtualTransitions(that, data.virtualTransitions);
                that.mode = data.mode;
                if (data.mode == "2") {
                    //多人处理
                    that.processMode = data.processMode;
                }
                else if (that.processMode) {
                    delete that.processMode;
                }
                that.actorMode = data.actorMode;
                that.actorVariableName = data.actorVariableName;
                that.getActorSystemVariableMode = data.getActorSystemVariableMode;
                for (var i = 0; i < that.backSteps.length; i++) {
                    that.backSteps.splice(i, 1);
                    i--;
                }
                for (var i = 0; i < data.backSteps.length; i++) {
                    that.backSteps.push(data.backSteps[i]);
                }
                that.users = data.users;
                that.roles = data.roles;
                that.isUseLaunchDepartment = data.isUseLaunchDepartment;
                that.isUseCurrentDepartment = data.isUseCurrentDepartment;
            }
        };
        new YChart.Dialog(options);
        YChart.stopBubble();
    });
}

YChart.Shape.Activity.prototype.select = function () {
    YChart.addClass(this.ui, "yc_rect_act");
    this.showPoints();
}

YChart.Shape.Activity.prototype.unselect = function () {
    YChart.removeClass(this.ui, "yc_rect_act");
    this.hidePoints();
}

YChart.Shape.Activity.prototype.generatePoints = function (visible) {
    if (this.topPoint) {
        return;
    }
    var x = parseFloat(this.ui.getAttributeNS(null, "x"));
    var y = parseFloat(this.ui.getAttributeNS(null, "y"));
    var width = parseFloat(this.ui.getAttributeNS(null, "width"));
    var height = parseFloat(this.ui.getAttributeNS(null, "height"));
    //上
    this.topPoint = new YChart.Shape.Activity.Point({ x: x + width / 2, y: y, source: this, position: "top" });
    this.ui.parentNode.appendChild(this.topPoint.ui);
    //右
    this.rightPoint = new YChart.Shape.Activity.Point({ x: x + width, y: y + height / 2, source: this, position: "right" });
    this.ui.parentNode.appendChild(this.rightPoint.ui);
    //下
    this.bottomPoint = new YChart.Shape.Activity.Point({ x: x + width / 2, y: y + height, source: this, position: "bottom" });
    this.ui.parentNode.appendChild(this.bottomPoint.ui);
    //左
    this.leftPoint = new YChart.Shape.Activity.Point({ x: x, y: y + height / 2, source: this, position: "left" });
    this.ui.parentNode.appendChild(this.leftPoint.ui);
    if (visible === false) {
        this.hidePoints();
    }
}

YChart.Shape.Activity.prototype.showPoints = function () {
    if (!this.topPoint) {
        this.generatePoints();
    }
    else {
        this.topPoint.show();
        this.leftPoint.show();
        this.rightPoint.show();
        this.bottomPoint.show();
    }
}

YChart.Shape.Activity.prototype.hidePoints = function () {
    if (this.topPoint) {
        this.topPoint.hide();
    }
    if (this.leftPoint) {
        this.leftPoint.hide();
    }
    if (this.rightPoint) {
        this.rightPoint.hide();
    }
    if (this.bottomPoint) {
        this.bottomPoint.hide();
    }
}

YChart.Shape.Activity.prototype.toFront = function () {
    this.ui.parentNode.appendChild(this.ui);
    if (this.topPoint) {
        this.topPoint.toFront();
        this.rightPoint.toFront();
        this.bottomPoint.toFront();
        this.leftPoint.toFront();
    }
}

YChart.Shape.Activity.prototype.getDefaultLeaveTransition = function () {
    var lts = this.getLeaveTransitions();
    for (var i = 0; i < lts.length; i++) {
        if (lts[i].isDefault) {
            return lts[i];
        }
    }
}

YChart.Shape.Activity.prototype.toBack = function () {
    this.ui.parentNode.insertBefore(this.ui, this.ui.parentNode.firstChild);
    if (this.topPoint) {
        this.topPoint.toBack();
        this.rightPoint.toBack();
        this.bottomPoint.toBack();
        this.leftPoint.toBack();
    }
}

YChart.Shape.Activity.prototype.remove = function () {
    YChart.remove(this.ui);
    if (this.topPoint) {
        this.topPoint.remove();
        this.leftPoint.remove();
        this.rightPoint.remove();
        this.bottomPoint.remove();
    }
    YChart.remove(this.text);
}

YChart.Shape.Activity.prototype.getLeaveTransitions = function () {
    if (!this.topPoint) {
        return [];
    }
    return this.topPoint.toTransitions.concat(this.rightPoint.toTransitions.concat(this.bottomPoint.toTransitions.concat(this.leftPoint.toTransitions)));
}

YChart.Shape.Activity.prototype.getCoord = function (axis, position) {
    position = position.toLowerCase();
    if (axis == "x" || axis == "X") {
        if (position == "left") {
            return this.ui.x.baseVal.value;
        }
        else if (position == "right") {
            return this.ui.x.baseVal.value + parseFloat(this.ui.getAttributeNS(null, "width"));
        }
    }
    else if (axis == "y" || axis == "Y") {
        if (position == "top") {
            return this.ui.y.baseVal.value;
        }
        else if (position == "bottom") {
            return this.ui.y.baseVal.value + parseFloat(this.ui.getAttributeNS(null, "height"));
        }
    }
    return 0;
}

YChart.Shape.Activity.prototype.beginMove = function (event) {
    YChart.adorner = { x: parseFloat(this.ui.getAttributeNS(null, "x")), y: parseFloat(this.ui.getAttributeNS(null, "y")), initX: event.clientX, initY: event.clientY, rect: this };
    YChart.attachEvent(this.ui.parentNode, "mousemove", YChart.Shape.Activity.onMouseMove);
    YChart.attachEvent(this.ui.parentNode, "mouseup", YChart.Shape.Activity.onMouseUpOrLeave);
    YChart.attachEvent(this.ui.parentNode, "mouseleave", YChart.Shape.Activity.onMouseUpOrLeave);
}

YChart.Shape.Activity.onMouseMove = function (event) {
    if (!YChart.adorner.moving) {
        if (YChart.adorner.rect.ui.getAttributeNS(null, "n")) {
            YChart.adorner.moving = YChart.adorner.rect.ui;
        }
        else {
            YChart.adorner.rect.chart.select(YChart.adorner.rect);
            YChart.adorner.moving = document.createElementNS(YChart.ns, "rect");
            YChart.adorner.moving.setAttributeNS(null, "x", YChart.adorner.rect.ui.getAttributeNS(null, "x"));
            YChart.adorner.moving.setAttributeNS(null, "y", YChart.adorner.rect.ui.getAttributeNS(null, "y"));
            YChart.adorner.moving.setAttributeNS(null, "height", 40);
            YChart.adorner.moving.setAttributeNS(null, "width", 70);
            YChart.adorner.moving.setAttributeNS(null, "class", "yc_rect_moving");
            YChart.adorner.rect.ui.parentNode.appendChild(YChart.adorner.moving);
        }
    }
    var x = YChart.adorner.x + event.clientX - YChart.adorner.initX;
    if (x > 1 && x + 80 < YChart.adorner.rect.chart.canvas.clientWidth) {
        if (YChart.snapDrag) {
            var s = x % 10;
            if (s < 5) {
                x = x - s
            }
            else {
                x = x + 10 - s;
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "x", x);
    }
    var y = YChart.adorner.y + event.clientY - YChart.adorner.initY;
    if (y > 1 && y + 40 < YChart.adorner.rect.chart.canvas.clientHeight) {
        if (YChart.snapDrag) {
            var s = y % 10;
            if (s < 5) {
                y = y - s
            }
            else {
                y = y + 10 - s;
            }
        }
        YChart.adorner.moving.setAttributeNS(null, "y", y);
    }
    if (YChart.adorner.moving.getAttributeNS(null, "n")) {
        YChart.adorner.rect.setTextLocation();
    }
}

YChart.Shape.Activity.onMouseUpOrLeave = function (event) {
    if (YChart.adorner) {
        YChart.removeEvent(YChart.adorner.rect.ui.parentNode, "mousemove", YChart.Shape.Activity.onMouseMove);
        YChart.removeEvent(YChart.adorner.rect.ui.parentNode, "mouseup", YChart.Shape.Activity.onMouseUpOrLeave);
        YChart.removeEvent(YChart.adorner.rect.ui.parentNode, "mouseleave", YChart.Shape.Activity.onMouseUpOrLeave);
        if (YChart.adorner.moving) {
            var x = parseFloat(YChart.adorner.moving.getAttributeNS(null, "x"));
            YChart.adorner.rect.ui.setAttributeNS(null, "x", x);
            var y = parseFloat(YChart.adorner.moving.getAttributeNS(null, "y"));
            YChart.adorner.rect.ui.setAttributeNS(null, "y", y);
            YChart.adorner.rect.setTextLocation();
            if (YChart.adorner.moving.getAttributeNS(null, "n")) {
                YChart.adorner.moving.removeAttributeNS(null, "n");
                YChart.adorner.rect.chart.select(YChart.adorner.rect);
            }
            else if (YChart.adorner.moving) {
                YChart.remove(YChart.adorner.moving);
            }
            if (YChart.adorner.rect.topPoint) {
                var width = parseFloat(YChart.adorner.rect.ui.getAttributeNS(null, "width"));
                var height = parseFloat(YChart.adorner.rect.ui.getAttributeNS(null, "height"));
                YChart.adorner.rect.topPoint.setPosition(x + width / 2, y);
                YChart.adorner.rect.topPoint.toFront();
                YChart.adorner.rect.rightPoint.setPosition(x + width, y + height / 2);
                YChart.adorner.rect.rightPoint.toFront();
                YChart.adorner.rect.bottomPoint.setPosition(x + width / 2, y + height);
                YChart.adorner.rect.bottomPoint.toFront();
                YChart.adorner.rect.leftPoint.setPosition(x, y + height / 2);
                YChart.adorner.rect.leftPoint.toFront();
            }
            delete YChart.adorner.moving;
        }
        document.onselectstart = null;
        delete YChart.adorner;
    }
}

YChart.Shape.Activity.prototype.move = function (axis, step) {
    if (axis == "x") {
        var s = this.ui.x.baseVal.value % 5;
        if (s == 0) {
            this.ui.x.baseVal.value = this.ui.x.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.x.baseVal.value = this.ui.x.baseVal.value - s;
            }
            else {
                this.ui.x.baseVal.value = this.ui.x.baseVal.value + 5 - s;
            }
        }
    }
    else if (axis == "y") {
        var s = this.ui.y.baseVal.value % 5;
        if (s == 0) {
            this.ui.y.baseVal.value = this.ui.y.baseVal.value + step;
        }
        else {
            if (step < 0) {
                this.ui.y.baseVal.value = this.ui.y.baseVal.value - s;
            }
            else {
                this.ui.y.baseVal.value = this.ui.y.baseVal.value + 5 - s;
            }
        }
    }
    var x = this.ui.x.baseVal.value, y = this.ui.y.baseVal.value;
    if (this.topPoint) {
        var width = parseFloat(this.ui.getAttributeNS(null, "width")), height = parseFloat(this.ui.getAttributeNS(null, "height"));
        this.topPoint.setPosition(x + width / 2, y);
        this.rightPoint.setPosition(x + width, y + height / 2);
        this.bottomPoint.setPosition(x + width / 2, y + height);
        this.leftPoint.setPosition(x, y + height / 2);
    }
    this.setTextLocation();
}

YChart.Shape.Activity.prototype.setTextLocation = function () {
    var bbox = this.text.getBBox();
    var x = this.ui.x.baseVal.value + (this.ui.width.baseVal.value - bbox.width) / 2;
    var y = this.ui.y.baseVal.value + (this.ui.height.baseVal.value - bbox.height) / 2 - 3;
    this.text.setAttributeNS(null, "transform", "translate(" + x + "," + y + ")");
}

YChart.Shape.Activity.prototype.setTip = function (tipInfo, startTime, endTime) {
    //显示人员
    var xActors = YChart.selectNodes(YChart.loadXml(tipInfo), "Root/Actor");
    if (xActors.length == 0) {
        return;
    }
    var tipUI = YChart.createElement("div", null, { position: "absolute" });
    document.body.appendChild(tipUI);
    var div_1 = YChart.createElement("div", null, { width: "152px", backgroundColor: "#92D0FF", opacity: 0.8, position: "absolute" });
    var div_2 = YChart.createElement("div", null, { width: "140px", border: "1px solid #4BB1FF", position: "absolute", fontSize: "12px", padding: "5px", cursor: "default" });
    tipUI.appendChild(div_1);
    tipUI.appendChild(div_2);
    if (this.mode == "1" || xActors.length == 1) {
        //单人
        for (var i = 0; i < xActors.length; i++) {
            var div_name = YChart.createElement("div", null, null, xActors[i].getAttribute("name"));
            div_2.appendChild(div_name);
            if (i != 0) {
                div_name.style.marginTop = "3px";
            }
            div_2.appendChild(YChart.createElement("div", null, null, "轮到：" + startTime));
            if (xActors[i].getAttribute("isFinished") == "1") {
                div_2.appendChild(YChart.createElement("div", null, null, "完成：" + endTime));
            }
            else {
                div_name.appendChild(YChart.createElement("span", null, { color: "#444444", marginLeft: "4px" }, "未处理"));
            }
        }
    }
    else if (this.mode == "2") {
        //多人
        div_2.appendChild(YChart.createElement("div", null, null, "轮到：" + startTime));
        for (var i = 0; i < xActors.length; i++) {
            var div_name = YChart.createElement("div", null, { marginTop: "3px" }, xActors[i].getAttribute("name"));
            div_2.appendChild(div_name);
            if (xActors[i].getAttribute("isFinished") == "1") {
                div_2.appendChild(YChart.createElement("div", null, null, "完成：" + xActors[i].getAttribute("time")));
            }
            else {
                div_name.appendChild(YChart.createElement("span", null, { color: "#666666", marginLeft: "4px" }, "未处理"));
            }
        }
    }
    div_1.style.height = div_2.offsetHeight + "px";
    div_2.style.marginTop = "-100%";
    tipUI.style.display = "none";
    YChart.attachEvent([this.ui, this.text], "mouseenter", function (ev) {
        tipUI.style.display = "";
        tipUI.style.left = (ev.clientX + 12) + "px";
        tipUI.style.top = (ev.clientY + 10) + "px";
    });
    YChart.attachEvent([this.ui, this.text], "mouseleave", function () {
        tipUI.style.display = "none";
    });
    YChart.attachEvent([this.ui, this.text], "mousemove", function (ev) {
        tipUI.style.left = (ev.clientX + 12) + "px";
        tipUI.style.top = (ev.clientY + 10) + "px";
    });
}

//定义元素与元素之间的接点
YChart.Shape.Activity.Point = function (option) {
    this.symbol = "activitypoint";
    var point = document.createElementNS(YChart.ns, "circle");
    point.setAttributeNS(null, "class", "yc_rect_point");
    point.setAttributeNS(null, "cx", option.x);
    point.setAttributeNS(null, "cy", option.y);
    point.setAttributeNS(null, "r", 4);
    this.ui = point;
    this.position = option.position;
    this.source = option.source;
    var that = this;
    if (this.source.symbol != "end") {
        YChart.attachEvent(this.ui, "mousedown", function (event) {
            YChart.onMouseDown(event, that);
        });
    }
    point.style.cursor = "Crosshair";
    this.fromTransitions = [];
    this.toTransitions = [];
    if (option.isViewMode == true) {
        return;
    }
    YChart.attachEvent(this.ui, "mouseover", function (event) {
        if (YChart.adorner && YChart.adorner.transition) {
            YChart.addClass(this, "yc_rect_point_over");
            YChart.adorner.refPoint = that;
        }
    });
    YChart.attachEvent(this.ui, "mouseout", function (event) {
        if (YChart.adorner && YChart.adorner.transition) {
            YChart.removeClass(this, "yc_rect_point_over");
            YChart.adorner.refPoint = null;
        }
    });
    YChart.attachEvent(this.ui, "click", function (event) {
        YChart.stopBubble(event);
    });
}

YChart.Shape.Activity.Point.prototype.hide = function () {
    this.ui.style.display = "none";
}

YChart.Shape.Activity.Point.prototype.show = function () {
    this.toFront();
    this.ui.style.display = "";
}

YChart.Shape.Activity.Point.prototype.toFront = function () {
    YChart.toFront(this.ui);
}

YChart.Shape.Activity.Point.prototype.toBack = function () {
    this.ui.parentNode.insertBefore(this.ui, this.ui.parentNode.firstChild);
}

YChart.Shape.Activity.Point.prototype.remove = function () {
    YChart.remove(this.ui);
    if (this.toTransition) {
        this.toTransition.setFromActivityPoint(null);
    }
    if (this.fromTransition) {
        this.fromTransition.setToActivityPoint(null);
    }
}

YChart.Shape.Activity.Point.prototype.addFromTransition = function (transition) {
    this.fromTransitions.push(transition);
}

YChart.Shape.Activity.Point.prototype.removeFromTransition = function (transition) {
    for (var i = 0; i < this.fromTransitions.length; i++) {
        if (transition == this.fromTransitions[i]) {
            this.fromTransitions.splice(i, 1);
            break;
        }
    }
}

YChart.Shape.Activity.Point.prototype.addToTransition = function (transition) {
    this.toTransitions.push(transition);
}

YChart.Shape.Activity.Point.prototype.removeToTransition = function (transition) {
    for (var i = 0; i < this.toTransitions.length; i++) {
        if (transition == this.toTransitions[i]) {
            this.toTransitions.splice(i, 1);
            break;
        }
    }
}

YChart.Shape.Activity.Point.prototype.setPosition = function (x, y) {
    this.ui.setAttributeNS(null, "cx", x);
    this.ui.setAttributeNS(null, "cy", y);
    for (var i = 0; i < this.toTransitions.length; i++) {
        this.toTransitions[i].reGenerateLines();
    }
    for (var i = 0; i < this.fromTransitions.length; i++) {
        this.fromTransitions[i].reGenerateLines();
    }
}

//开始移动
YChart.Shape.Activity.Point.prototype.beginMove = function (event) {
    var x = parseFloat(this.ui.getAttributeNS(null, "cx"));
    var y = parseFloat(this.ui.getAttributeNS(null, "cy"));
    var toTransition = new YChart.Shape.Transition(this, { x: x, y: y });
    this.source.chart.appendShape(toTransition);
    this.source.chart.select(toTransition);
    toTransition.beginMove(event, "to");
    this.addToTransition(toTransition);
}

//定义连接线
YChart.Shape.Transition = function (fromActivityPoint, option) {
    this.fromPoint = document.createElementNS(YChart.ns, "circle");
    this.fromPoint.setAttributeNS(null, "class", "yc_t_point");
    this.fromPoint.setAttributeNS(null, "r", 4);
    this.ui = this.fromPoint;
    this.x1 = option.x;
    this.y1 = option.y;
    this.x2 = option.x;
    this.y2 = option.y;
    fromActivityPoint.ui.parentNode.appendChild(this.fromPoint);
    var d = "";
    if (fromActivityPoint.position == "top") {
        this.y1 = this.y1 - 1;
        this.fromPoint.setAttributeNS(null, "cx", option.x);
        this.fromPoint.setAttributeNS(null, "cy", option.y - 6);
        d = YChart.Shape.Transition.getArrowPath("bottomtoup", this.x2, this.y2 - 4);
    }
    else if (fromActivityPoint.position == "right") {
        this.x1 = this.x1 + 1;
        this.fromPoint.setAttributeNS(null, "cx", option.x + 6);
        this.fromPoint.setAttributeNS(null, "cy", option.y);
        d = YChart.Shape.Transition.getArrowPath("righttoleft", this.x2 + 5, this.y2);
    }
    else if (fromActivityPoint.position == "bottom") {
        this.y1 = this.y1 + 1;
        this.fromPoint.setAttributeNS(null, "cx", option.x);
        this.fromPoint.setAttributeNS(null, "cy", option.y + 6);
        d = YChart.Shape.Transition.getArrowPath("uptobottom", this.x2, this.y2 + 5);
    }
    else if (fromActivityPoint.position == "left") {
        this.x1 = this.x1 - 1;
        this.fromPoint.setAttributeNS(null, "cx", option.x - 6);
        this.fromPoint.setAttributeNS(null, "cy", option.y);
        d = YChart.Shape.Transition.getArrowPath("lefttoright", this.x2 - 5, this.y2);
    }
    this.fromActivityPoint = fromActivityPoint;
    this.lines = [];
    //绘制箭头
    this.toPoint = document.createElementNS(YChart.ns, "path");
    this.toPoint.setAttributeNS(null, "d", d);
    fromActivityPoint.ui.parentNode.appendChild(this.toPoint);
    if (option.name) {
        this.fromPoint.setAttributeNS(null, "id", option.name);
        this.name = option.name;
    }
    else {
        var i = 1;
        while (document.getElementById("line" + i)) {
            i++;
        }
        this.fromPoint.setAttributeNS(null, "id", "line" + i);
        this.name = "line" + i;
    }
    this.expression = option.expression || "";
    var that = this;
    this.isDefault = this.fromActivityPoint.source.getDefaultLeaveTransition() ? false : true;
    if (option.isViewMode == true) {
        return;
    }
    YChart.attachEvent([this.fromPoint, this.toPoint], "click", function (event) {
        that.chart.select(that);
        YChart.stopBubble(event);
    });
    YChart.attachEvent([this.fromPoint, this.toPoint], "mousedown", function (event) {
        YChart.onMouseDown(event, that);
    });
    YChart.attachEvent([this.fromPoint, this.toPoint], "dblclick", function () {
        setTimeout(function () {
            YChart.Shape.Transition.onMouseUpOrLeave();
        }, 150);
        var options = that.chart.dialog.transition;
        options.api = {
            getSource: function () {
                return that;
            },
            saveData: function (data) {
                if (that.chart.isExistsName(data.name, that)) {
                    YChart.Dialog.alert("保存失败", "名称{" + data.name + "}已经存在");
                    return false;
                }
                that.name == data.name;
                that.expression = data.expression;
                if (that.fromActivityPoint) {
                    that.isDefault = data.isDefault;
                    if (that.isDefault) {
                        //如果为默认先
                        var leaves = that.fromActivityPoint.source.getLeaveTransitions();
                        for (var i = 0; i < leaves.length; i++) {
                            if (leaves[i] == that) {
                                continue;
                            }
                            if (leaves[i].isDefault) {
                                leaves[i].isDefault = false;
                            }
                        }
                    }
                }
                else {
                    that.isDefault = false;
                }
            }
        };
        new YChart.Dialog(options);
        YChart.stopBubble();
    });
}

YChart.Shape.Transition.insert = function (chart, fromActivityPoint, toActivityPoint, name, expression) {
    //var getRightPoint
    if (!fromActivityPoint || !toActivityPoint) {
        return;
    }
    var transition = new YChart.Shape.Transition(fromActivityPoint, { x: fromActivityPoint.ui.getAttributeNS(null, "cx"), y: fromActivityPoint.ui.getAttributeNS(null, "cy"), name: name, expression: expression, isViewMode: chart.isViewMode });
    chart.appendShape(transition);
    fromActivityPoint.addToTransition(transition);
    transition.setToActivityPoint(toActivityPoint);
    transition.reGenerateLines();
}

YChart.Shape.Transition.prototype.beginMove = function (event, flag) {
    this.chart.select(this);
    YChart.adorner = { initX: event.clientX, initY: event.clientY, transition: this, moved: 0 };
    if (flag) {
        YChart.adorner.flag = flag;
    }
    else if (event.srcElement == this.fromPoint) {
        YChart.adorner.flag = "from";
    }
    else if (event.srcElement == this.toPoint) {
        YChart.adorner.flag = "to";
    }
    if (YChart.adorner.flag == "from") {
        YChart.adorner.x = this.x1;
        YChart.adorner.y = this.y1;
    }
    else if (YChart.adorner.flag == "to") {
        YChart.adorner.x = this.x2;
        YChart.adorner.y = this.y2;
    }
    else { }
    YChart.attachEvent(this.fromPoint.parentNode, "mousemove", YChart.Shape.Transition.onMouseMove);
    YChart.attachEvent(this.fromPoint.parentNode, "mouseup", YChart.Shape.Transition.onMouseUpOrLeave);
    YChart.attachEvent(this.fromPoint.parentNode, "mouseleave", YChart.Shape.Transition.onMouseUpOrLeave);
}

YChart.Shape.Transition.prototype.clearLines = function () {
    for (var i = 0; i < this.lines.length; i++) {
        this.lines[i].remove();
        //YChart.remove(this.lines[i]);
    }
    this.lines = [];
}

YChart.Shape.Transition.prototype.firstMove = function () {
    for (var i = 0; i < this.chart.shapes.length; i++) {
        if (this.chart.shapes[i].showPoints) {
            this.chart.shapes[i].showPoints();
        }
    }
    YChart.toBack(this.toPoint);
    for (var i = 0; i < this.lines.length; i++) {
        this.lines[i].toBack();
    }
    if (YChart.adorner.flag == "from") {
        if (this.fromActivityPoint) {
            this.fromActivityPoint.removeToTransition(this);
        }
        this.setFromActivityPoint(null);
    }
    else if (YChart.adorner.flag == "to") {
        if (this.toActivityPoint) {
            this.toActivityPoint.removeFromTransition(this);
        }
        this.setToActivityPoint(null);
    }
    else { }
}

YChart.Shape.Transition.onMouseMove = function (event) {
    if (YChart.adorner.moved == 0) {
        YChart.adorner.transition.firstMove();
        YChart.adorner.moved = 1;
    }
    if (YChart.adorner.refPoint) {
        if (YChart.adorner.flag == "from") {
            YChart.adorner.transition.x1 = YChart.adorner.refPoint.ui.cx.baseVal.value;
        }
        else if (YChart.adorner.flag == "to") {
            YChart.adorner.transition.x2 = YChart.adorner.refPoint.ui.cx.baseVal.value;
        }
        if (YChart.adorner.flag == "from") {
            YChart.adorner.transition.y1 = YChart.adorner.refPoint.ui.cy.baseVal.value;
            YChart.adorner.transition.setFromActivityPoint(YChart.adorner.refPoint);
        }
        else if (YChart.adorner.flag == "to") {
            YChart.adorner.transition.y2 = YChart.adorner.refPoint.ui.cy.baseVal.value;
            YChart.adorner.transition.setToActivityPoint(YChart.adorner.refPoint);
        }
    }
    else {
        var temp = YChart.adorner.x + event.clientX - YChart.adorner.initX;
        if (temp < 1) {
            temp = 1;
        }
        if (YChart.adorner.flag == "from") {
            YChart.adorner.transition.x1 = temp;
        }
        else if (YChart.adorner.flag == "to") {
            YChart.adorner.transition.x2 = temp;
        }
        temp = YChart.adorner.y + event.clientY - YChart.adorner.initY;
        if (temp < 1) {
            temp = 1;
        }
        if (YChart.adorner.flag == "from") {
            YChart.adorner.transition.y1 = temp;
            YChart.adorner.transition.setFromActivityPoint(null);
            if (YChart.adorner.transition.fromActivityPoint) {
                YChart.adorner.transition.fromActivityPoint.removeToTransition(YChart.adorner.transition);
            }
        }
        else if (YChart.adorner.flag == "to") {
            YChart.adorner.transition.y2 = temp;
            YChart.adorner.transition.setToActivityPoint(null);
            if (YChart.adorner.transition.toActivityPoint) {
                YChart.adorner.transition.toActivityPoint.removeFromTransition(YChart.adorner.transition);
            }
        }
    }
    YChart.adorner.transition.clearLines();
    YChart.adorner.transition.generateLines(true);
    //if (YChart.adorner.flag == "from") {
    YChart.adorner.transition.locateFromPoint();
    //}
    //else if (YChart.adorner.flag == "to") {
    YChart.adorner.transition.generateToPointArrow();
    //}
}

YChart.Shape.Transition.onMouseUpOrLeave = function (event) {
    if (YChart.adorner) {
        document.onselectstart = null;
        YChart.removeEvent(YChart.adorner.transition.toPoint.parentNode, "mousemove", YChart.Shape.Transition.onMouseMove);
        YChart.removeEvent(YChart.adorner.transition.toPoint.parentNode, "mouseup", YChart.Shape.Transition.onMouseUpOrLeave);
        YChart.removeEvent(YChart.adorner.transition.toPoint.parentNode, "mouseleave", YChart.Shape.Transition.onMouseUpOrLeave);
        for (var i = 0; i < YChart.adorner.transition.chart.shapes.length; i++) {
            if (YChart.adorner.transition.chart.shapes[i].hidePoints) {
                YChart.adorner.transition.chart.shapes[i].hidePoints();
            }
        }
        for (var i = 0; i < YChart.adorner.transition.lines.length; i++) {
            YChart.adorner.transition.lines[i].toFront();
        }
        if (YChart.adorner.refPoint) {
            YChart.removeClass(YChart.adorner.refPoint.ui, "yc_rect_point_over");
        }
        YChart.toFront(YChart.adorner.transition.fromPoint, YChart.adorner.transition.toPoint);
        delete YChart.adorner;
    }
}

YChart.Shape.Transition.prototype.select = function () {
    this.fromPoint.setAttributeNS(null, "filter", "url(#shadow_m1)");
    this.toPoint.setAttributeNS(null, "filter", "url(#shadow_m1)");
    for (var i = 0; i < this.lines.length; i++) {
        //this.lines[i].ui.setAttributeNS(null, "filter", "url(#shadow_m1)");
        this.lines[i].ui.setAttributeNS(null, "stroke", "#EFEFEF");
    }
}

YChart.Shape.Transition.prototype.unselect = function () {
    this.fromPoint.removeAttributeNS(null, "filter");
    this.toPoint.removeAttributeNS(null, "filter");
    for (var i = 0; i < this.lines.length; i++) {
        //this.lines[i].ui.removeAttributeNS(null, "filter");
        this.lines[i].ui.removeAttributeNS(null, "stroke");
    }
}

YChart.Shape.Transition.prototype.locateFromPoint = function () {
    var direction = "";
    if (this.lines.length > 0) {
        direction = YChart.Shape.Transition.getLineStatus(this.lines[0].ui).direction;
    }
    else {
        direction = "lefttoright";
    }
    switch (direction) {
        case "uptobottom":
            //this.y1 = this.y1 - 1;
            this.fromPoint.setAttributeNS(null, "cx", this.x1);
            this.fromPoint.setAttributeNS(null, "cy", this.y1 + 5);
            break;
        case "lefttoright":
            //this.x1 = this.x1 + 1;
            this.fromPoint.setAttributeNS(null, "cx", this.x1 + 5);
            this.fromPoint.setAttributeNS(null, "cy", this.y1);
            break;
        case "bottomtoup":
            //this.y1 = this.y1 + 1;
            this.fromPoint.setAttributeNS(null, "cx", this.x1);
            this.fromPoint.setAttributeNS(null, "cy", this.y1 - 5);
            break;
        case "righttoleft":
            //this.x1 = this.x1 - 1;
            this.fromPoint.setAttributeNS(null, "cx", this.x1 - 5);
            this.fromPoint.setAttributeNS(null, "cy", this.y1);
            break;
    }
}

YChart.Shape.Transition.prototype.remove = function () {
    if (this.toActivityPoint) {
        this.toActivityPoint.removeFromTransition(this);
    }
    if (this.fromActivityPoint) {
        this.fromActivityPoint.removeToTransition(this);
    }
    this.clearLines();
    //YChart.remove(this.ui);
    YChart.remove(this.fromPoint);
    YChart.remove(this.toPoint);
}

YChart.Shape.Transition.prototype.toFront = function () {
    for (var line in this.lines) {
        this.lines[i].toFront();
    }
    YChart.toFront(this.fromPoint, this.toPoint);
}

YChart.Shape.Transition.prototype.generateToPointArrow = function (offsetX, offsetY) {
    var direction;
    if (this.lines.length == 0) {
        if (this.toActivityPoint) {
            direction = YChart.Shape.Transition.getDirectionByPosition(this.toActivityPoint.position);
        }
        else if (this.fromActivityPoint) {
            direction = YChart.Shape.Transition.getDirectionByPosition(this.fromActivityPoint.position);
        }
        else {
            direction = "lefttoright";
        }
    }
    else {
        direction = YChart.Shape.Transition.getLineStatus(this.lines[this.lines.length - 1].ui).direction;
    }
    var x = this.x2, y = this.y2;
    if (offsetX) {
        x = x + offsetX;
    }
    if (offsetY) {
        y = y + offsetY;
    }
    var path = YChart.Shape.Transition.getArrowPath(direction, x, y);
    if (path) {
        this.toPoint.setAttributeNS(null, "d", path);
    }
}

YChart.Shape.Transition.getArrowPath = function (direction, x, y) {
    switch (direction) {
        case "uptobottom":
            return "M " + (x - 5) + "," + (y - 14) + " L " + (x + 5) + "," + (y - 14) + " L " + x + "," + (y + 1) + " L " + (x - 5) + "," + (y - 14);
        case "lefttoright":
            return "M " + (x - 14) + "," + (y - 5) + " L " + (x - 14) + "," + (y + 5) + " L " + (x + 1) + "," + y + " L " + (x - 14) + "," + (y - 5);
        case "bottomtoup":
            return "M " + (x - 5) + "," + (y + 14) + " L " + (x + 5) + "," + (y + 14) + " L " + x + "," + (y - 1) + " L " + (x - 5) + "," + (y + 14);
        case "righttoleft":
            return "M " + (x + 14) + "," + (y - 5) + " L " + (x + 14) + "," + (y + 5) + " L " + (x - 1) + "," + y + " L " + (x + 14) + "," + (y - 5);
    }
    return null;
}

YChart.Shape.Transition.getDirectionByPosition = function (position) {
    switch (position) {
        case "top":
            return "uptobottom";
        case "right":
            return "lefttoright";
        case "bottom":
            return "botttomtoup";
        case "left":
            return "righttoleft";
    }
    return "lefttoright";
}

YChart.Shape.Transition.prototype.setFromActivityPoint = function (rectPoint) {
    if (this.fromActivityPoint == rectPoint) {
        return;
    }
    this.fromActivityPoint = rectPoint;
    if (rectPoint) {
        rectPoint.addToTransition(this);
    }
}

YChart.Shape.Transition.prototype.setToActivityPoint = function (rectPoint) {
    if (this.toActivityPoint == rectPoint) {
        return;
    }
    this.toActivityPoint = rectPoint;
    if (rectPoint) {
        rectPoint.addFromTransition(this);
    }
}

YChart.Shape.Transition.prototype.reGenerateLines = function () {
    if (this.fromActivityPoint) {
        this.x1 = this.fromActivityPoint.ui.cx.baseVal.value;
        this.y1 = this.fromActivityPoint.ui.cy.baseVal.value;
    }
    if (this.toActivityPoint) {
        this.x2 = this.toActivityPoint.ui.cx.baseVal.value;
        this.y2 = this.toActivityPoint.ui.cy.baseVal.value;
    }
    this.clearLines();
    this.generateLines(false);
    YChart.toFront(this.fromPoint);
    this.locateFromPoint();
    this.generateToPointArrow();
}

YChart.Shape.Transition.prototype.generateLines = function (isFocus) {
    var points;
    if (this.fromActivityPoint) {
        switch (this.fromActivityPoint.position) {
            case "top":
                points = this.processFromTopConnection(this.x1, this.y1, this.x2, this.y2);
                break;
            case "right":
                points = this.processFromRightConnection(this.x1, this.y1, this.x2, this.y2);
                break;
            case "bottom":
                points = this.processFromBottomConnection(this.x1, this.y1, this.x2, this.y2);
                break;
            case "left":
                points = this.processFromLeftConnection(this.x1, this.y1, this.x2, this.y2);
                break;
        }
    }
    else if (this.toActivityPoint) {
        points = [];
        points.push({ x: this.x1, y: this.y1 });
        switch (this.toActivityPoint.position) {
            case "top":
                points.push({ x: this.x2, y: this.y2 - 20 });
                points.push({ x: this.x2, y: this.y2 });
                points = YChart.Shape.Transition.mergePoints(points, "v");
                break;
            case "right":
                points.push({ x: this.x2 + 20, y: this.y2 });
                points.push({ x: this.x2, y: this.y2 });
                points = YChart.Shape.Transition.mergePoints(points, "h");
                break;
            case "bottom":
                points.push({ x: this.x2, y: this.y2 + 20 });
                points.push({ x: this.x2, y: this.y2 });
                points = YChart.Shape.Transition.mergePoints(points, "v");
                break;
            case "left":
                points.push({ x: this.x2 - 20, y: this.y2 });
                points.push({ x: this.x2, y: this.y2 });
                points = YChart.Shape.Transition.mergePoints(points, "h");
                break;
        }
    }
    else {
        points = [];
        points.push({ x: this.x1, y: this.y1 });
        points.push({ x: this.x2, y: this.y2 });
        points = YChart.Shape.Transition.mergePoints(points, "h");
    }
    if (points && points.length > 1) {
        for (var i = 1; i < points.length; i++) {
            this.lines.push(new YChart.Shape.Transition.Line(this.fromPoint.parentNode, { x1: points[i - 1].x, y1: points[i - 1].y, x2: points[i].x, y2: points[i].y, isFocus: isFocus }));
        }
    }
}

YChart.Shape.Transition.getDirection = function (x1, y1, x2, y2) {
    if (x1 == x2) {
        if (y1 > y2) {
            return "top";
        }
        else {
            return "bottom";
        }
    }
    else if (y1 == y2) {
        if (x1 > x2) {
            return "left";
        }
        else {
            return "right"
        }
    }
    else if (x2 - x1 > 0 && y2 - y1 > 0) {
        return "rightbottom";
    }
    else if (x2 - x1 < 0 && y2 - y1 > 0) {
        return "leftbottom";
    }
    else if (x2 - x1 > 0 && y2 - y1 < 0) {
        return "righttop";
    }
    else if (x2 - x1 < 0 && y2 - y1 < 0) {
        return "lefttop";
    }
    else {
        return "origin";
    }
}

YChart.Shape.Transition.getLineStatus = function (line) {
    var result = { type: "", direction: "" };
    if (line.x1.baseVal.value == line.x2.baseVal.value) {
        result.type = "horizontal";
        if (line.y1.baseVal.value > line.y2.baseVal.value) {
            //从下往上
            result.direction = "bottomtoup";
        }
        else {
            //从上往下
            result.direction = "uptobottom";
        }
    }
    if (line.y1.baseVal.value == line.y2.baseVal.value) {
        result.type = "vertical";
        if (line.x1.baseVal.value > line.x2.baseVal.value) {
            //从右向左
            result.direction = "righttoleft";
        }
        else {
            //从左向右
            result.direction = "lefttoright";
        }
    }
    return result;
}

YChart.Shape.Transition.prototype.processFromTopConnection = function (x1, y1, x2, y2) {
    //先处理向上20px
    if (this.toActivityPoint) {
        switch (this.toActivityPoint.position) {
            case "top":
                var y = 0;
                if (y2 < y1) {
                    y = y2 - 20;
                }
                else {
                    y = y1 - 20;
                }
                var x = 0;
                if (x2 < x1) {
                    //在左边
                    if (y2 < y1) {
                        //在上面 to向右绕过
                        var x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                        if (x <= x1) {
                            x = x1;
                        }
                    }
                    else {
                        //在下面 from向左绕过
                        x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                        if (x >= x2) {
                            x = x2;
                        }
                    }
                }
                else {
                    //在右边
                    if (y2 < y1) {
                        y = y2 - 20;
                        //在上面 to向左绕过
                        x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                        if (x >= x1) {
                            x = x1;
                        }
                    }
                    else {
                        //在下面 from向右绕过
                        x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                        if (x <= x2) {
                            x = x2;
                        }
                    }
                }
                return YChart.Shape.Transition.mergePoints([{ x: x1, y: y1 }, { x: x1, y: y1 - 20 }, { x: x, y: y }, { x: x2, y: y2 - 20 }, { x: x2, y: y2 }], "h");
                break;
            case "right":
                var points = [{ x: x1, y: y1 }];
                var x = 0, y = 0, extend1_y = y1 - 20, extend2_x = x2 + 20;
                if (y1 - y2 > 10) {
                    //在上方
                    if (x1 - x2 > 10) {
                        //在左方，可以直连
                        y = y2;
                        x = x1;
                        extend1_y = 0;
                        extend2_x = 0;
                    }
                    else {
                        x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                        var vDistance = y1 - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            //从缝隙中穿过
                            y = y1 - vDistance / 2;
                        }
                        else {
                            if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = y1 - 20;
                            }
                        }
                        extend1_y = y;
                    }
                }
                else {
                    //在下方
                    var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        x = x2 + hDistance / 2;
                        y = y1 - 20;
                        extend2_x = 0;
                    }
                    else {
                        if (x2 > this.fromActivityPoint.source.getCoord("x", "right")) {
                            x = x2 + 20;
                        }
                        else {
                            x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                        }
                        extend2_x = 0;
                        if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                            y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                        }
                        else {
                            y = y1 - 20;
                        }
                        extend1_y = y;
                    }
                }
                if (extend1_y != 0) {
                    points.push({ x: x1, y: extend1_y });
                }
                points.push({ x: x, y: y });
                if (extend2_x != 0) {
                    points.push({ x: extend2_x, y: y2 });
                }
                points.push({ x: x2, y: y2 });
                return YChart.Shape.Transition.mergePoints(points, "h");
                break;
            case "bottom":
                var points = [{ x: x1, y: y1 }];
                var x = 0, y = 0, extend1_y = y1 - 20, extend2_y = y2 + 20;
                var vDistance = this.fromActivityPoint.source.getCoord("y", "top") - this.toActivityPoint.source.getCoord("y", "bottom");
                if (vDistance > 10) {
                    y = this.fromActivityPoint.source.getCoord("y", "top") - vDistance / 2;
                    extend1_y = 0;
                    extend2_y = 0;
                    x = x1;
                }
                else {
                    if (x2 < x1) {
                        //在左边
                        var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                        y = y1 - 20;
                        if (hDistance > 10) {
                            x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        }
                        else {
                            x = this.toActivityPoint.source.getCoord("x", "left");
                            if (x < this.fromActivityPoint.source.getCoord("x", "left")) {
                                x = x - 20;
                            }
                            else {
                                x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                                extend1_y = y;
                            }
                        }
                    }
                    else {
                        //在右边
                        var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                        y = y1 - 20;
                        if (hDistance > 10) {
                            x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        }
                        else {
                            x = this.toActivityPoint.source.getCoord("x", "right");
                            if (x > this.fromActivityPoint.source.getCoord("x", "right")) {
                                x = x + 20;
                            }
                            else {
                                x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                                extend1_y = y;
                            }
                        }
                    }
                }
                if (extend1_y != 0) {
                    points.push({ x: x1, y: extend1_y });
                }
                points.push({ x: x, y: y });
                if (extend2_y != 0) {
                    points.push({ x: x2, y: extend2_y });
                }
                points.push({ x: x2, y: y2 });
                return YChart.Shape.Transition.mergePoints(points, "h");
                break;
            case "left":
                var points = [{ x: x1, y: y1 }];
                var x = 0, y = 0, extend1_y = y1 - 20, extend2_x = x2 - 20;
                if (y1 - y2 > 10) {
                    //在上方
                    if (x2 - x1 > 10) {
                        //在右方，可以直连
                        y = y2;
                        x = x1;
                        extend1_y = 0;
                        extend2_x = 0;
                    }
                    else {
                        x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                        var vDistance = y1 - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            //从缝隙中穿过
                            y = y1 - vDistance / 2;
                        }
                        else {
                            if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = y1 - 20;
                            }
                        }
                        extend1_y = y;
                    }
                }
                else {
                    //在下方
                    var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        y = y1 - 20;
                        extend2_x = 0;
                    }
                    else {
                        if (x2 < this.fromActivityPoint.source.getCoord("x", "left")) {
                            x = x2 - 20;
                        }
                        else {
                            x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                        }
                        extend2_x = 0;
                        if (y1 > this.toActivityPoint.source.getCoord("y", "top")) {
                            y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                        }
                        else {
                            y = y1 - 20;
                        }
                        extend1_y = y;
                    }
                }
                if (extend1_y != 0) {
                    points.push({ x: x1, y: extend1_y });
                }
                points.push({ x: x, y: y });
                if (extend2_x != 0) {
                    points.push({ x: extend2_x, y: y2 });
                }
                points.push({ x: x2, y: y2 });
                return YChart.Shape.Transition.mergePoints(points, "h");
                break;
        }
    }
    else {
        var points = [{ x: x1, y: y1 }];
        points.push({ x: x1, y: y1 - 20 });
        points.push({ x: x2, y: y2 });
        return YChart.Shape.Transition.mergePoints(points, "h");
    }
}

YChart.Shape.Transition.prototype.processFromRightConnection = function (x1, y1, x2, y2) {
    var points = [{ x: x1, y: y1 }];
    if (this.toActivityPoint) {
        var x = 0, y = 0, extend1_x = x1 + 20, temp;
        switch (this.toActivityPoint.position) {
            case "top":
                var extend2_y = y2 - 20;
                if (y2 - y1 < 10) {
                    var vDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right")
                    //在上方
                    if (vDistance > 10) {
                        x = x1 + vDistance / 2;
                        extend1_x = 0;
                        y = y1;
                    }
                    else {
                        if (x1 > this.toActivityPoint.source.getCoord("x", "right")) {
                            x = x1 + 20;
                        }
                        else {
                            x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                        }
                        if (this.toActivityPoint.source.getCoord("y", "top") < this.fromActivityPoint.source.getCoord("y", "top")) {
                            y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                        }
                        else {
                            y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                        }
                        extend1_x = x;
                        extend1_y = y;
                    }
                }
                else {
                    //在下方
                    if (x2 - x1 > 10) {
                        x = x2;
                        y = y1;
                        extend1_x = 0;
                        extend2_y = 0;
                    }
                    else {
                        var hDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (hDistance > 10) {
                            y = y2 - hDistance / 2;
                            x = x1 + 20;
                        }
                        else {
                            y = this.fromActivityPoint.source.getCoord("y", "top");
                            if (y < y2) {
                                y = y - 20;
                            }
                            else {
                                y = y2 - 20;
                            }
                            x = x2;
                        }
                        extend2_y = y;
                    }
                }
                if (extend2_y != 0) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "right":
                var extend2_x = x2 + 20;
                if (y1 > y2) {
                    if (x2 >= x1) {
                        //在右边
                        y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                        if (y < y1) {
                            y = y1;
                        }
                        if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                            x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                        }
                        else {
                            x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                        }
                    }
                    else {
                        //在左边
                        x = x1 + 20;
                        y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                        if (y > y2) {
                            y = y2;
                            extend2_x = 0;
                        }
                    }
                }
                else {
                    if (x2 >= x1) {
                        //在右边
                        y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                        if (y > y1) {
                            y = y1;
                        }
                        if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                            x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                        }
                        else {
                            x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                        }
                    }
                    else {
                        //在左边
                        x = x1 + 20;
                        y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                        if (y < y2) {
                            y = y2;
                            extend2_x = 0;
                        }
                    }
                }
                if (extend2_x != 0) {
                    temp = { x: extend2_x, y: y2 };
                }
                break;
            case "bottom":
                var extend2_y = y2 + 20;
                if (x2 - x1 > 10 && y2 < y1) {
                    y = y1;
                    x = x2;
                    extend1_x = null;
                    extend2_y = null;
                }
                else {
                    var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        //有间隙可走
                        if (y1 - this.toActivityPoint.source.getCoord("y", "bottom") < 10) {
                            y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                            extend1_x = x;
                        }
                    }
                    else {
                        var vDistance = this.fromActivityPoint.source.getCoord("y", "top") - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            y = this.fromActivityPoint.source.getCoord("y", "top") - vDistance / 2;
                            x = extend1_x;
                            extend2_y = y;
                        }
                        else {
                            if (this.fromActivityPoint.source.getCoord("x", "right") > this.toActivityPoint.source.getCoord("x", "right")) {
                                x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            extend1_x = x;
                            if (this.fromActivityPoint.source.getCoord("y", "bottom") > this.toActivityPoint.source.getCoord("y", "bottom")) {
                                y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                                extend2_y = null;
                            }
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "left":
                var extend2_x = x2 - 20;
                var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                if (hDistance > 10) {
                    x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                    y = y2;
                    extend1_x = x;
                    extend2_x = null;
                }
                else {
                    if (y1 > y2) {
                        //在上面
                        var vDistance = this.fromActivityPoint.source.getCoord("y", "top") - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            y = this.fromActivityPoint.source.getCoord("y", "top") - vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.fromActivityPoint.source.getCoord("y", "top") > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            if (this.fromActivityPoint.source.getCoord("x", "right") > this.toActivityPoint.source.getCoord("x", "right")) {
                                x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            extend1_x = x;
                        }
                    }
                    else {
                        //在下面
                        var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.fromActivityPoint.source.getCoord("y", "bottom") > this.toActivityPoint.source.getCoord("y", "bottom")) {
                                y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            if (this.fromActivityPoint.source.getCoord("x", "right") > this.toActivityPoint.source.getCoord("x", "right")) {
                                x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            extend1_x = x;
                        }
                    }
                }
                if (extend2_x != null) {
                    temp = { x: extend2_x, y: y2 };
                }
                break;
        }
        if (extend1_x != 0 && extend1_x != null) {
            points.push({ x: extend1_x, y: y1 });
        }
        points.push({ x: x, y: y });
        if (temp)
        { points.push(temp); }
    }
    else {
        points.push({ x: x1 + 20, y: y1 });
    }
    points.push({ x: x2, y: y2 });
    return YChart.Shape.Transition.mergePoints(points, "v");
}

YChart.Shape.Transition.prototype.processFromBottomConnection = function (x1, y1, x2, y2) {
    var points = [{ x: x1, y: y1 }];
    if (this.toActivityPoint) {
        var x = 0, y = 0, extend1_y = y1 + 20, temp;
        switch (this.toActivityPoint.position) {
            case "top":
                var extend2_y = y2 - 20;
                var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                if (vDistance > 10) {
                    y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                    extend1_y = extend2_y = y;
                    x = x2;
                }
                else {
                    if (x1 > x2) {
                        //在左边
                        var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                        if (hDistance > 10) {
                            x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                            y = extend1_y;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                                x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            if (this.fromActivityPoint.source.getCoord("y", "top") > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            extend2_y = y;
                        }
                    }
                    else {
                        //在右边
                        var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                        if (hDistance > 10) {
                            x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                            y = extend1_y;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("x", "left") > this.fromActivityPoint.source.getCoord("x", "left")) {
                                x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            else {
                                x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            if (this.fromActivityPoint.source.getCoord("y", "top") > this.toActivityPoint.source.getCoord("y", "top")) {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            extend2_y = y;
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "right":
                var extend2_x = x2 + 20;
                if (x1 - x2 > 10 && y2 - y1 > 10) {
                    x = x1;
                    y = y2;
                    extend1_y = extend2_x = null;
                }
                else {
                    var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        extend2_x = x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        y = y1;
                    }
                    else {
                        var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            extend1_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "bottom") > this.fromActivityPoint.source.getCoord("y", "bottom")) {
                                extend1_y = y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                extend1_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                                extend2_x = x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                extend2_x = x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                        }
                    }
                }
                if (extend2_x != null) {
                    temp = { x: extend2_x, y: y };
                }
                break;
            case "bottom":
                var extend2_y = y2 + 20;
                if (x1 > x2) {
                    //在左边
                    if (this.fromActivityPoint.source.getCoord("y", "bottom") > this.toActivityPoint.source.getCoord("y", "bottom")) {
                        x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                        if (x2 < x) {
                            x = x2;
                        }
                        y = extend1_y;
                    }
                    else {
                        if (x1 - this.toActivityPoint.source.getCoord("x", "right") > 10) {
                            x = x1;
                            extend1_y = y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                        }
                        else {
                            x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            y = extend1_y;
                        }
                    }
                }
                else {
                    //在右边
                    if (this.fromActivityPoint.source.getCoord("y", "bottom") > this.toActivityPoint.source.getCoord("y", "bottom")) {
                        x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                        if (x2 > x) {
                            x = x2;
                        }
                        y = extend1_y;
                    }
                    else {
                        if (this.toActivityPoint.source.getCoord("x", "left") - x1 > 10) {
                            x = x1;
                            extend1_y = y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                        }
                        else {
                            x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            y = extend1_y;
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "left":
                var extend2_x = x2 - 20;
                if (x2 - x1 >= 10 && y2 - y1 >= 10) {
                    x = x1;
                    y = y2;
                    extend1_y = extend2_x = null;
                }
                else {
                    var hDistance = this.toActivityPoint.source.getCoord("x", "left") - this.fromActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        extend2_x = x = this.fromActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        y = y1;
                    }
                    else {
                        var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            extend1_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "bottom") > this.fromActivityPoint.source.getCoord("y", "bottom")) {
                                extend1_y = y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                extend1_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "left") > this.fromActivityPoint.source.getCoord("x", "left")) {
                                extend2_x = x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            else {
                                extend2_x = x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            }
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: extend2_x, y: y2 };
                }
                break;
        }
        if (extend1_y != null) {
            points.push({ x: x1, y: extend1_y });
        }
        points.push({ x: x, y: y });
        if (temp)
        { points.push(temp); }
    }
    else {
        points.push({ x: x1, y: y1 + 20 });
    }
    points.push({ x: x2, y: y2 });
    return YChart.Shape.Transition.mergePoints(points, "h");
}

YChart.Shape.Transition.prototype.processFromLeftConnection = function (x1, y1, x2, y2) {
    var points = [{ x: x1, y: y1 }];
    if (this.toActivityPoint) {
        var x = 0, y = 0, extend1_x = x1 - 20, temp;
        switch (this.toActivityPoint.position) {
            case "top":
                var extend2_y = y2 - 20;
                if (x1 - x2 > 10 && y2 - y1 > 10) {
                    x = x2;
                    y = y1;
                    extend1_x = extend2_y = null;
                }
                else {
                    var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        extend1_x = x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        y = y1;
                    }
                    else {
                        var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            extend2_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "top") > this.fromActivityPoint.source.getCoord("y", "top")) {
                                extend2_y = y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                extend2_y = y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "left") > this.fromActivityPoint.source.getCoord("x", "left")) {
                                extend1_x = x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            else {
                                extend1_x = x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            }
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "right":
                var extend2_x = x2 + 20;
                var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                if (hDistance > 10) {
                    extend1_x = extend2_x = x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                    y = y2;
                }
                else {
                    if (y1 > y2) {
                        //在上边
                        var vDistance = this.fromActivityPoint.source.getCoord("y", "top") - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            y = this.toActivityPoint.source.getCoord("y", "top") + vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "bottom") > this.fromActivityPoint.source.getCoord("y", "bottom")) {
                                y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                                extend2_x = x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                extend2_x = x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                        }
                    }
                    else {
                        var vDistance = this.toActivityPoint.source.getCoord("y", "top") - this.fromActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            y = this.fromActivityPoint.source.getCoord("y", "bottom") + vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "top") > this.fromActivityPoint.source.getCoord("y", "top")) {
                                y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            else {
                                y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "right") > this.fromActivityPoint.source.getCoord("x", "right")) {
                                extend2_x = x = this.toActivityPoint.source.getCoord("x", "right") + 20;
                            }
                            else {
                                extend2_x = x = this.fromActivityPoint.source.getCoord("x", "right") + 20;
                            }
                        }
                    }
                }
                if (extend2_x != null) {
                    temp = { x: extend2_x, y: y2 };
                }
                break;
            case "bottom":
                var extend2_y = y2 + 20;
                if (y1 - y2 > 10 && x1 - x2 > 10) {
                    x = x2;
                    y = y1;
                    extend1_x = extend2_y = null;
                }
                else {
                    var hDistance = this.fromActivityPoint.source.getCoord("x", "left") - this.toActivityPoint.source.getCoord("x", "right");
                    if (hDistance > 10) {
                        extend1_x = x = this.toActivityPoint.source.getCoord("x", "right") + hDistance / 2;
                        y = y1;
                    }
                    else {
                        var vDistance = this.fromActivityPoint.source.getCoord("y", "top") - this.toActivityPoint.source.getCoord("y", "bottom");
                        if (vDistance > 10) {
                            extend2_y = y = this.fromActivityPoint.source.getCoord("y", "top") - vDistance / 2;
                            x = extend1_x;
                        }
                        else {
                            if (this.toActivityPoint.source.getCoord("y", "bottom") > this.fromActivityPoint.source.getCoord("y", "bottom")) {
                                extend2_y = y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            else {
                                extend2_y = y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                            }
                            if (this.toActivityPoint.source.getCoord("x", "left") > this.fromActivityPoint.source.getCoord("x", "left")) {
                                extend1_x = x = this.fromActivityPoint.source.getCoord("x", "left") - 20;
                            }
                            else {
                                extend1_x = x = this.toActivityPoint.source.getCoord("x", "left") - 20;
                            }
                        }
                    }
                }
                if (extend2_y != null) {
                    temp = { x: x2, y: extend2_y };
                }
                break;
            case "left":
                var extend2_x = x2 - 20;
                if (y1 > y2) {
                    //在上边
                    if (x2 >= x1) {
                        x = extend1_x;
                        //在右边
                        if (this.fromActivityPoint.source.getCoord("y", "top") - y2 > 10) {
                            y = y2;
                        }
                        else {
                            y = this.fromActivityPoint.source.getCoord("y", "top") - 20;
                        }
                    }
                    else {
                        //在左边
                        x = extend2_x;
                        if (y1 - this.toActivityPoint.source.getCoord("y", "bottom") > 10) {
                            y = y1;
                            extend1_x = x;
                        }
                        else {
                            y = this.toActivityPoint.source.getCoord("y", "bottom") + 20;
                        }
                    }
                }
                else {
                    if (x2 >= x1) {
                        x = extend1_x;
                        //在右边
                        if (y2 - this.fromActivityPoint.source.getCoord("y", "bottom") > 10) {
                            y = y2;
                        }
                        else {
                            y = this.fromActivityPoint.source.getCoord("y", "bottom") + 20;
                        }
                    }
                    else {
                        x = extend2_x;
                        if (this.toActivityPoint.source.getCoord("y", "top") - y1 > 10) {
                            y = y1;
                        }
                        else {
                            y = this.toActivityPoint.source.getCoord("y", "top") - 20;
                        }
                    }
                }
                if (extend2_x != null) {
                    temp = { x: extend2_x, y: y2 };
                }
                break;
        }
        if (extend1_x != null) {
            points.push({ x: extend1_x, y: y1 });
        }
        points.push({ x: x, y: y });
        if (temp)
        { points.push(temp); }
    }
    else {
        points.push({ x: x1 - 20, y: y1 });
    }
    points.push({ x: x2, y: y2 });
    return YChart.Shape.Transition.mergePoints(points, "v");
}

YChart.Shape.Transition.mergePoints = function (points, firstDirection) {
    var result = []
    if (points.length > 0) {
        result.push(points[0]);
    }
    var nextDirection = firstDirection || "h";
    for (var i = 1; i < points.length; i++) {
        if (points[i - 1].x == points[i].x && points[i - 1].y == points[i].y) {
            continue;
        }
        if (result[result.length - 1].x == points[i].x) {
            //两者在同一垂直线上
            nextDirection = "h";
            if (result.length >= 2 && result[result.length - 2].x == points[i].x) {
                //与前两个都在同一垂直线上
                if (result[result.length - 2].y < result[result.length - 1].y) {
                    //从上往下
                    if (points[i].y >= result[result.length - 1].y) {
                        result[result.length - 1].y = points[i].y;
                    }
                    else {
                        result.push({ x: points[i].x, y: points[i].y });
                    }
                }
                else {
                    //从下往上
                    if (points[i].y <= result[result.length - 1].y) {
                        result[result.length - 1].y = points[i].y;
                    }
                    else {
                        result.push({ x: points[i].x, y: points[i].y });
                    }
                }
            }
            else {
                result.push({ x: points[i].x, y: points[i].y });
            }
        }
        else if (result[result.length - 1].y == points[i].y) {
            //两者在同一水平线上
            nextDirection = "v";
            if (result.length >= 2 && result[result.length - 2].y == points[i].y) {
                //与前两个都在同一水平线上
                if (result[result.length - 2].x < result[result.length - 1].x) {
                    //从左往右
                    if (points[i].x >= result[result.length - 1].x) {
                        result[result.length - 1].x = points[i].x;
                    }
                    else {
                        result.push({ x: points[i].x, y: points[i].y });
                    }
                }
                else {
                    //从右往左
                    if (points[i].x <= result[result.length - 1].x) {
                        result[result.length - 1].x = points[i].x;
                    }
                    else {
                        result.push({ x: points[i].x, y: points[i].y });
                    }
                }
            }
            else {
                result.push({ x: points[i].x, y: points[i].y });
            }
        }
        else {
            if (nextDirection == "h") {
                result.push({ x: points[i].x, y: result[result.length - 1].y });
                nextDirection = "v";
            }
            else {
                result.push({ x: result[result.length - 1].x, y: points[i].y });
                nextDirection = "h";
            }
            result.push({ x: points[i].x, y: points[i].y });
        }
    }
    return result;
}

//定义连接线中的线条
YChart.Shape.Transition.Line = function (canvas, option) {
    var line = document.createElementNS(YChart.ns, "line");
    line.setAttributeNS(null, "class", "yc_t_line");
    line.x1.baseVal.value = option.x1;
    line.y1.baseVal.value = option.y1;
    line.x2.baseVal.value = option.x2;
    line.y2.baseVal.value = option.y2;
    if (option.isFocus) {
        //line.setAttributeNS(null, "filter", "url(#shadow_m1)");
        line.setAttributeNS(null, "stroke", "#EFEFEF");
    }
    canvas.appendChild(line);
    YChart.toBack(line);
    this.ui = line;
}

YChart.Shape.Transition.Line.prototype.remove = function () {
    YChart.remove(this.ui);
}

YChart.Shape.Transition.Line.prototype.toFront = function () {
    YChart.toFront(this.ui);
}

YChart.Shape.Transition.Line.prototype.toBack = function () {
    YChart.toBack(this.ui);
}