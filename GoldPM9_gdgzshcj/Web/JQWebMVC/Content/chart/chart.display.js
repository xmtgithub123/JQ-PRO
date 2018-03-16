//流程客户端调用包 V1.0.0
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

//是否鼠标松开
YChart.isReleased = true;

//当鼠标摁下时触发的事件
YChart.onMouseDown = function (event, shape) {
    YChart.isReleased = false;
    YChart.attachEvent((shape.canvas || shape.ui.parentNode), "mouseup", YChart.onMouseUp);
    document.onselectstart = function () { return false; };
    setTimeout(function () {
        if (YChart.isReleased) {
            document.onselectstart = null;
            return;
        }
        YChart.moved = 1;
        shape.beginMove(event);
    }, 100);
    YChart.stopBubble(event);
}

//当鼠标松开时触发的事件
YChart.onMouseUp = function (event) {
    YChart.isReleased = true;
    if (YChart.moved == 1) {
        setTimeout(function () {
            YChart.moved = 0;
        }, 50);
    }
    YChart.removeEvent(this, "mouseup", YChart.onMouseUp);
}

YChart.Display = function (option) {
    if (!option.container || !option.remote) {
        return;
    }
    if (typeof (option.container) == "string") {
        option.container = document.getElementById(option.container);
        if (!option.container) {
            return;
        }
    }
    if (!option.container) {
        return;
    }
    this.containter = option.container;
    var that = this;
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
    this.shapes = [];
    function beginLoad() {
        YChart.ajax({
            url: option.remote.url,
            type: option.method || "POST",
            data: option.remote.data,
            success: function (result) {
                that.data = result;
                loadData();
            },
            error: function (err) {

            }
        });
    }

    function loadData() {
        var divTitle = YChart.createElement("div", { className: "ycd_title" }, null, that.data.Name + "[" + that.data.StatusText + "]");
        that.containter.appendChild(divTitle);
        var divViewport = YChart.createElement("div", { className: "ycd_viewport" }, { height: (that.containter.clientHeight - divTitle.offsetHeight) + "px", width: that.containter.clientWidth + "px" });
        that.containter.appendChild(divViewport);
        var divCanvas = YChart.createElementNS("svg", { className: "ycd_canvas ycd_canvas_grid" });
        divCanvas.style.backgroundColor = "#FFFFFF";
        divCanvas.style.top = "5px";
        divCanvas.style.left = "5px";
        divViewport.appendChild(divCanvas);
        divCanvas.setAttributeNS(null, "height", that.data.Height || divViewport.clientHeight);
        divCanvas.setAttributeNS(null, "width", that.data.Width || divViewport.clientWidth);
        that.canvas = divCanvas;
        YChart.attachEvent(divCanvas, "mousedown", function (event) {
            YChart.onMouseDown(event, that);
        });
        //获取出开始节点
        for (var i = 0; i < that.data.Activities.length; i++) {
            if (that.data.Activities[i].Symbol == "start") {
                //定位位置
                //console.log(that.data.Activities[i]);
                that.shapes.push(new YChart.Start({
                    x: that.data.Activities[i].UI_CX,
                    y: that.data.Activities[i].UI_CY,
                    r: that.data.Activities[i].UI_R,
                    canvas: that.canvas,
                    displayName: that.data.Activities[i].DisplayName,
                    name: that.data.Activities[i].Name,
                    color: getColor(that.data.Activities[i].Status)
                }));
            }
            else if (that.data.Activities[i].Symbol == "end") {
                that.shapes.push(new YChart.End({
                    x: that.data.Activities[i].UI_CX,
                    y: that.data.Activities[i].UI_CY,
                    r: that.data.Activities[i].UI_R,
                    canvas: that.canvas,
                    displayName: that.data.Activities[i].DisplayName,
                    name: that.data.Activities[i].Name,
                    color: getColor(that.data.Activities[i].Status)
                }));
            }
            else if (that.data.Activities[i].Symbol == "activity" || that.data.Activities[i].Symbol == "signactivity") {
                that.shapes.push(new YChart.Activity({
                    x: that.data.Activities[i].UI_X,
                    y: that.data.Activities[i].UI_Y,
                    height: that.data.Activities[i].UI_Height,
                    width: that.data.Activities[i].UI_Width,
                    canvas: that.canvas,
                    displayName: that.data.Activities[i].DisplayName,
                    name: that.data.Activities[i].Name,
                    color: getColor(that.data.Activities[i].Status)
                }));
            }
        }
        for (var i = 0; i < that.data.Activities.length; i++) {
            if (that.data.Activities[i].Transitions && (that.data.Activities[i].Symbol == "start" || that.data.Activities[i].Symbol == "end" || that.data.Activities[i].Symbol == "activity" || that.data.Activities[i].Symbol == "signactivity")) {
                for (var j = 0; j < that.data.Activities[i].Transitions.length; j++) {
                    if (that.data.Activities[i].Transitions[j].IsVirtual) {
                        continue;
                    }
                    new YChart.Transition({ from: that.findShape(that.data.Activities[i].Transitions[j].From), to: that.findShape(that.data.Activities[i].Transitions[j].To), fromPosition: that.data.Activities[i].Transitions[j].From_Position, toPosition: that.data.Activities[i].Transitions[j].To_Position, canvas: that.canvas });
                }
            }
        }


    }

    function getColor(status) {
        switch (status) {
            case 0:
                //未激活
                return "#F5F5F5";
                break;
            case 1:
                //已激活
                return "#FFE68C";
                break;
            case 2:
                //挂起
                return "#D6D8E9";
                break;
            case 3:
                //忽略
                return "#CCCEDB";
                break;
            case 4:
                //已完成
                return "#438EB9";
                break;
            case 5:
                //跳过
                return "#008000";
                break;
        }
    }
}

YChart.Display.prototype.findShape = function (shapeName) {
    for (var i = 0; i < this.shapes.length; i++) {
        if (this.shapes[i].name == shapeName) {
            return this.shapes[i];
        }
    }
}

YChart.Display.prototype.beginMove = function (event) {
    document.body.style.cursor = "hand";
    this.canvas.style.cursor = "hand";
    YChart.adorner = { x: parseFloat(this.canvas.style.left.replace("px", "")), y: parseFloat(this.canvas.style.top.replace("px", "")), initX: event.clientX, initY: event.clientY, canvas: this.canvas };
    YChart.attachEvent(document.body, "mousemove", YChart.Display.onMouseMove);
    YChart.attachEvent(document.body, "mouseup", YChart.Display.onMouseUpOrLeave);
    YChart.attachEvent(document.body, "mouseleave", YChart.Display.onMouseUpOrLeave);
}

YChart.Display.onMouseMove = function (event) {
    var temp = YChart.adorner.x + event.clientX - YChart.adorner.initX;
    if (temp + YChart.adorner.canvas.offsetWidth > 50 && temp + 50 < YChart.adorner.canvas.parentNode.clientWidth) {
        YChart.adorner.canvas.style.left = temp + "px";
    }
    temp = YChart.adorner.y + event.clientY - YChart.adorner.initY;
    if (temp + YChart.adorner.canvas.offsetHeight > 50 && temp + 50 < YChart.adorner.canvas.parentNode.clientHeight) {
        YChart.adorner.canvas.style.top = temp + "px";
    }
}

YChart.Display.onMouseUpOrLeave = function () {
    document.body.style.cursor = "default";
    YChart.adorner.canvas.style.cursor = "default";
    YChart.adorner.canvasOffsetHeight = YChart.adorner.canvas.offsetTop;
    YChart.adorner.canvasOffsetWidth = YChart.adorner.canvas.offsetLeft;
    YChart.removeEvent(document.body, "mousemove", YChart.Display.onMouseMove);
    YChart.removeEvent(document.body, "mouseup", YChart.Display.onMouseUpOrLeave);
    YChart.removeEvent(document.body, "mouseleave", YChart.Display.onMouseUpOrLeave);
    delete YChart.adorner;
}

//开始
YChart.Start = function (option) {
    if (!option.canvas) {
        return;
    }
    this.symbol = "start";
    this.name = "start";
    this.ui = YChart.createElementNS("circle", { className: "ycd_start", cx: option.x, cy: option.y, r: option.r || 20 });
    option.canvas.appendChild(this.ui);
    if (option.color) {
        this.ui.style.fill = option.color;
    }
    if (option.displayName) {
        this.text = YChart.createElementNS("text");
        this.text.appendChild(document.createTextNode(option.displayName));
        this.text.setAttributeNS(null, "class", "ycd_text_name");
        option.canvas.appendChild(this.text);
        this.text.setAttributeNS(null, "x", (option.x + (-this.text.offsetWidth) / 2));
        this.text.setAttributeNS(null, "y", (option.y + this.text.offsetHeight / 3));
    }
}

//获取点坐标
YChart.Start.prototype.getPointCoord = function (position) {
    var x = parseFloat(this.ui.getAttributeNS(null, "cx"));
    var y = parseFloat(this.ui.getAttributeNS(null, "cy"));
    var r = parseFloat(this.ui.getAttributeNS(null, "r"));
    if (position == "top") {
        return { x: x, y: y - r };
    }
    else if (position == "right") {
        return { x: x + r, y: y };
    }
    else if (position == "bottom") {
        return { x: x, y: y + r };
    }
    else if (position == "left") {
        return { x: x - r, y: y };
    }
}

YChart.Start.prototype.getCoord = function (position) {
    if (position == "left") {
        return this.ui.cx.baseVal.value - this.ui.r.baseVal.value;
    }
    else if (position == "right") {
        return this.ui.cx.baseVal.value + this.ui.r.baseVal.value;
    }
    else if (position == "top") {
        return this.ui.cy.baseVal.value - this.ui.r.baseVal.value;
    }
    else if (position == "bottom") {
        return this.ui.cy.baseVal.value + this.ui.r.baseVal.value;
    }

}

//结束
YChart.End = function (option) {
    if (!option.canvas) {
        return;
    }
    this.symbol = "end";
    this.name = "end";
    this.ui = YChart.createElementNS("circle", { className: "ycd_end", cx: option.x, cy: option.y, r: option.r || 20 });
    option.canvas.appendChild(this.ui);
    if (option.color) {
        this.ui.style.fill = option.color;
    }
    if (option.displayName) {
        this.text = YChart.createElementNS("text");
        this.text.appendChild(document.createTextNode(option.displayName));
        this.text.setAttributeNS(null, "class", "ycd_text_name");
        option.canvas.appendChild(this.text);
        this.text.setAttributeNS(null, "x", (option.x + (-this.text.offsetWidth) / 2));
        this.text.setAttributeNS(null, "y", (option.y + this.text.offsetHeight / 3));
    }
}

YChart.End.prototype.getPointCoord = function (position) {
    var x = parseFloat(this.ui.getAttributeNS(null, "cx"));
    var y = parseFloat(this.ui.getAttributeNS(null, "cy"));
    var r = parseFloat(this.ui.getAttributeNS(null, "r"));
    if (position == "top") {
        return { x: x, y: y - r };
    }
    else if (position == "right") {
        return { x: x + r, y: y };
    }
    else if (position == "bottom") {
        return { x: x, y: y + r };
    }
    else if (position == "left") {
        return { x: x - r, y: y };
    }
}

YChart.End.prototype.getCoord = function (position) {
    if (position == "left") {
        return this.ui.cx.baseVal.value - this.ui.r.baseVal.value;
    }
    else if (position == "right") {
        return this.ui.cx.baseVal.value + this.ui.r.baseVal.value;
    }
    else if (position == "top") {
        return this.ui.cy.baseVal.value - this.ui.r.baseVal.value;
    }
    else if (position == "bottom") {
        return this.ui.cy.baseVal.value + this.ui.r.baseVal.value;
    }

}
//活动
YChart.Activity = function (option) {
    if (!option.canvas) {
        return;
    }
    this.symbol = "activity";
    this.name = option.name;
    this.ui = YChart.createElementNS("rect", { x: option.x, y: option.y, className: "ycd_rect", height: 40, width: 70 });
    option.canvas.appendChild(this.ui);
    if (option.color) {
        this.ui.style.fill = option.color;
    }
    //显示文本
    if (option.displayName) {
        this.text = YChart.createElementNS("text", { className: "ycd_text_name" });
        this.text.appendChild(document.createTextNode(option.displayName));
        option.canvas.appendChild(this.text);
        this.text.setAttributeNS(null, "x", (option.x + (70 - this.text.offsetWidth) / 2));
        this.text.setAttributeNS(null, "y", (option.y + (45 - this.text.offsetHeight) / 2 + this.text.offsetHeight - 4));
    }
}

YChart.Activity.prototype.getPointCoord = function (position) {
    var x = parseFloat(this.ui.getAttributeNS(null, "x"));
    var y = parseFloat(this.ui.getAttributeNS(null, "y"));
    var width = parseFloat(this.ui.getAttributeNS(null, "width"));
    var height = parseFloat(this.ui.getAttributeNS(null, "height"));
    if (position == "top") {
        return { x: x + width / 2, y: y };
    }
    else if (position == "right") {
        return { x: x + width, y: y + height / 2 };
    }
    else if (position == "bottom") {
        return { x: x + width / 2, y: y + height };
    }
    else if (position == "left") {
        return { x: x, y: y + height / 2 };
    }
}

YChart.Activity.prototype.getCoord = function (position) {
    if (position == "left") {
        return this.ui.x.baseVal.value;
    }
    else if (position == "right") {
        return this.ui.x.baseVal.value + parseFloat(this.ui.getAttributeNS(null, "width"));
    }
    else if (position == "top") {
        return this.ui.y.baseVal.value;
    }
    else if (position == "bottom") {
        return this.ui.y.baseVal.value + parseFloat(this.ui.getAttributeNS(null, "height"));
    }
}

YChart.Transition = function (option) {
    if (!option.from || !option.to || !option.fromPosition || !option.toPosition) {
        return;
    }
    var fromCoord = option.from.getPointCoord(option.fromPosition);
    if (!fromCoord) {
        return;
    }
    var toCoord = option.to.getPointCoord(option.toPosition);
    if (!toCoord) {
        return;
    }
    this.x1 = fromCoord.x;
    this.y1 = fromCoord.y;
    this.x2 = toCoord.x;
    this.y2 = toCoord.y;
    this.lines = [];
    this.fromPoint = YChart.createElementNS("circle", { className: "ycd_t_point", r: "4" });
    //生成开始点
    var d = "";
    if (option.fromPosition == "top") {
        this.fromPoint.setAttributeNS(null, "cx", this.x1);
        this.fromPoint.setAttributeNS(null, "cy", this.y1 - 6);
        this.y1 = this.y1 - 1;
        d = YChart.Transition.getArrowPath("bottomtoup", this.x2, this.y2 - 4);
    }
    else if (option.fromPosition == "right") {
        this.fromPoint.setAttributeNS(null, "cx", this.x1 + 6);
        this.fromPoint.setAttributeNS(null, "cy", this.y1);
        this.x1 = this.x1 + 1;
        d = YChart.Transition.getArrowPath("righttoleft", this.x2 + 5, this.y2);
    }
    else if (option.fromPosition == "bottom") {
        this.fromPoint.setAttributeNS(null, "cx", this.x1);
        this.fromPoint.setAttributeNS(null, "cy", this.y1 + 6);
        this.y1 = this.y1 + 1;
        d = YChart.Transition.getArrowPath("uptobottom", this.x2, this.y2 + 5);
    }
    else if (option.fromPosition == "left") {
        this.fromPoint.setAttributeNS(null, "cx", this.x1 - 6);
        this.fromPoint.setAttributeNS(null, "cy", this.y1);
        this.x1 = this.x1 - 1;
        d = YChart.Transition.getArrowPath("lefttoright", this.x2 - 5, this.y2);
    }
    option.canvas.appendChild(this.fromPoint);
    this.fromPosition = option.fromPosition;
    this.toPosition = option.toPosition;
    this.to = option.to;
    this.from = option.from;
    //生成箭头
    //this.toPoint = YChart.createElementNS("path", { d: d });
    //option.canvas.appendChild(this.toPoint);
    this.generateLines();
    this.generateToPointArrow();
}

//生成线
YChart.Transition.prototype.generateLines = function () {
    var points;
    switch (this.fromPosition) {
        case "top":
            points = this.processFromTopConnection(this.x1, this.y1, this.x2, this.y2, this.toPosition);
            break;
        case "right":
            points = this.processFromRightConnection(this.x1, this.y1, this.x2, this.y2, this.toPosition);
            break;
        case "bottom":
            points = this.processFromBottomConnection(this.x1, this.y1, this.x2, this.y2, this.toPosition);
            break;
        case "left":
            points = this.processFromLeftConnection(this.x1, this.y1, this.x2, this.y2, this.toPosition);
            break;
    }
    console.log(points);
    if (points && points.length > 1) {
        for (var i = 1; i < points.length; i++) {
            this.lines.push(new YChart.Transition.Line(this.fromPoint.parentNode, { x1: points[i - 1].x, y1: points[i - 1].y, x2: points[i].x, y2: points[i].y }));
        }
    }
}

YChart.Transition.prototype.processFromTopConnection = function (x1, y1, x2, y2, toPosition) {
    //先处理向上20px
    switch (toPosition) {
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
                    var x = this.to.getCoord("right") + 20;
                    if (x <= x1) {
                        x = x1;
                    }
                }
                else {
                    //在下面 from向左绕过
                    x = this.from.getCoord("left") - 20;
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
                    x = this.to.getCoord("left") - 20;
                    if (x >= x1) {
                        x = x1;
                    }
                }
                else {
                    //在下面 from向右绕过
                    x = this.from.getCoord("right") + 20;
                    if (x <= x2) {
                        x = x2;
                    }
                }
            }
            return YChart.Transition.mergePoints([{ x: x1, y: y1 }, { x: x1, y: y1 - 20 }, { x: x, y: y }, { x: x2, y: y2 - 20 }, { x: x2, y: y2 }], "h");
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
                    x = this.to.getCoord("right") + 20;
                    var vDistance = y1 - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        //从缝隙中穿过
                        y = y1 - vDistance / 2;
                    }
                    else {
                        if (y1 > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
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
                var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                if (hDistance > 10) {
                    x = x2 + hDistance / 2;
                    y = y1 - 20;
                    extend2_x = 0;
                }
                else {
                    if (x2 > this.from.getCoord("right")) {
                        x = x2 + 20;
                    }
                    else {
                        x = this.from.getCoord("right") + 20;
                    }
                    extend2_x = 0;
                    if (y1 > this.to.getCoord("top")) {
                        y = this.to.getCoord("top") - 20;
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
            return YChart.Transition.mergePoints(points, "h");
            break;
        case "bottom":
            var points = [{ x: x1, y: y1 }];
            var x = 0, y = 0, extend1_y = y1 - 20, extend2_y = y2 + 20;
            var vDistance = this.from.getCoord("top") - this.to.getCoord("bottom");
            if (vDistance > 10) {
                y = this.from.getCoord("top") - vDistance / 2;
                extend1_y = 0;
                extend2_y = 0;
                x = x1;
            }
            else {
                if (x2 < x1) {
                    //在左边
                    var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                    y = y1 - 20;
                    if (hDistance > 10) {
                        x = this.to.getCoord("right") + hDistance / 2;
                    }
                    else {
                        x = this.to.getCoord("left");
                        if (x < this.from.getCoord("left")) {
                            x = x - 20;
                        }
                        else {
                            x = this.from.getCoord("left") - 20;
                        }
                        if (y1 > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
                            extend1_y = y;
                        }
                    }
                }
                else {
                    //在右边
                    var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
                    y = y1 - 20;
                    if (hDistance > 10) {
                        x = this.from.getCoord("right") + hDistance / 2;
                    }
                    else {
                        x = this.to.getCoord("right");
                        if (x > this.from.getCoord("right")) {
                            x = x + 20;
                        }
                        else {
                            x = this.from.getCoord("right") + 20;
                        }
                        if (y1 > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
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
            return YChart.Transition.mergePoints(points, "h");
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
                    x = this.to.getCoord("left") - 20;
                    var vDistance = y1 - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        //从缝隙中穿过
                        y = y1 - vDistance / 2;
                    }
                    else {
                        if (y1 > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
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
                var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
                if (hDistance > 10) {
                    x = this.from.getCoord("right") + hDistance / 2;
                    y = y1 - 20;
                    extend2_x = 0;
                }
                else {
                    if (x2 < this.from.getCoord("left")) {
                        x = x2 - 20;
                    }
                    else {
                        x = this.from.getCoord("left") - 20;
                    }
                    extend2_x = 0;
                    if (y1 > this.to.getCoord("top")) {
                        y = this.to.getCoord("top") - 20;
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
            return YChart.Transition.mergePoints(points, "h");
            break;
    }
}

YChart.Transition.prototype.processFromRightConnection = function (x1, y1, x2, y2, toPosition) {
    var points = [{ x: x1, y: y1 }];
    var x = 0, y = 0, extend1_x = x1 + 20, temp;
    switch (toPosition) {
        case "top":
            var extend2_y = y2 - 20;
            if (y2 - y1 < 10) {
                var vDistance = this.to.getCoord("left") - this.from.getCoord("right")
                //在上方
                if (vDistance > 10) {
                    x = x1 + vDistance / 2;
                    extend1_x = 0;
                    y = y1;
                }
                else {
                    if (x1 > this.to.getCoord("right")) {
                        x = x1 + 20;
                    }
                    else {
                        x = this.to.getCoord("right") + 20;
                    }
                    if (this.to.getCoord("top") < this.from.getCoord("top")) {
                        y = this.to.getCoord("top") - 20;
                    }
                    else {
                        y = this.from.getCoord("top") - 20;
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
                    var hDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (hDistance > 10) {
                        y = y2 - hDistance / 2;
                        x = x1 + 20;
                    }
                    else {
                        y = this.from.getCoord("top");
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
                    y = this.to.getCoord("bottom") + 20;
                    if (y < y1) {
                        y = y1;
                    }
                    if (this.to.getCoord("right") > this.from.getCoord("right")) {
                        x = this.to.getCoord("right") + 20;
                    }
                    else {
                        x = this.from.getCoord("right") + 20;
                    }
                }
                else {
                    //在左边
                    x = x1 + 20;
                    y = this.from.getCoord("top") - 20;
                    if (y > y2) {
                        y = y2;
                        extend2_x = 0;
                    }
                }
            }
            else {
                if (x2 >= x1) {
                    //在右边
                    y = this.to.getCoord("top") - 20;
                    if (y > y1) {
                        y = y1;
                    }
                    if (this.to.getCoord("right") > this.from.getCoord("right")) {
                        x = this.to.getCoord("right") + 20;
                    }
                    else {
                        x = this.from.getCoord("right") + 20;
                    }
                }
                else {
                    //在左边
                    x = x1 + 20;
                    y = this.from.getCoord("bottom") + 20;
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
                var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
                if (hDistance > 10) {
                    //有间隙可走
                    if (y1 - this.to.getCoord("bottom") < 10) {
                        y = this.to.getCoord("bottom") + 20;
                        x = this.from.getCoord("right") + hDistance / 2;
                        extend1_x = x;
                    }
                }
                else {
                    var vDistance = this.from.getCoord("top") - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        y = this.from.getCoord("top") - vDistance / 2;
                        x = extend1_x;
                        extend2_y = y;
                    }
                    else {
                        if (this.from.getCoord("right") > this.to.getCoord("right")) {
                            x = this.from.getCoord("right") + 20;
                        }
                        else {
                            x = this.to.getCoord("right") + 20;
                        }
                        extend1_x = x;
                        if (this.from.getCoord("bottom") > this.to.getCoord("bottom")) {
                            y = this.from.getCoord("bottom") + 20;
                        }
                        else {
                            y = this.to.getCoord("bottom") + 20;
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
            var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
            if (hDistance > 10) {
                x = this.from.getCoord("right") + hDistance / 2;
                y = y2;
                extend1_x = x;
                extend2_x = null;
            }
            else {
                if (y1 > y2) {
                    //在上面
                    var vDistance = this.from.getCoord("top") - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        y = this.from.getCoord("top") - vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.from.getCoord("top") > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
                        }
                        else {
                            y = this.from.getCoord("top") - 20;
                        }
                        if (this.from.getCoord("right") > this.to.getCoord("right")) {
                            x = this.from.getCoord("right") + 20;
                        }
                        else {
                            x = this.to.getCoord("right") + 20;
                        }
                        extend1_x = x;
                    }
                }
                else {
                    //在下面
                    var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (vDistance > 10) {
                        y = this.from.getCoord("bottom") + vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.from.getCoord("bottom") > this.to.getCoord("bottom")) {
                            y = this.from.getCoord("bottom") + 20;
                        }
                        else {
                            y = this.to.getCoord("bottom") + 20;
                        }
                        if (this.from.getCoord("right") > this.to.getCoord("right")) {
                            x = this.from.getCoord("right") + 20;
                        }
                        else {
                            x = this.to.getCoord("right") + 20;
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
    points.push({ x: x2, y: y2 });
    return YChart.Transition.mergePoints(points, "v");
}

YChart.Transition.prototype.processFromBottomConnection = function (x1, y1, x2, y2, toPosition) {
    var points = [{ x: x1, y: y1 }];
    var x = 0, y = 0, extend1_y = y1 + 20, temp;
    switch (toPosition) {
        case "top":
            var extend2_y = y2 - 20;
            var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
            if (vDistance > 10) {
                y = this.from.getCoord("bottom") + vDistance / 2;
                extend1_y = extend2_y = y;
                x = x2;
            }
            else {
                if (x1 > x2) {
                    //在左边
                    var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                    if (hDistance > 10) {
                        x = this.to.getCoord("right") + hDistance / 2;
                        y = extend1_y;
                    }
                    else {
                        if (this.to.getCoord("right") > this.from.getCoord("right")) {
                            x = this.to.getCoord("right") + 20;
                        }
                        else {
                            x = this.from.getCoord("right") + 20;
                        }
                        if (this.from.getCoord("top") > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
                        }
                        else {
                            y = this.from.getCoord("top") - 20;
                        }
                        extend2_y = y;
                    }
                }
                else {
                    //在右边
                    var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
                    if (hDistance > 10) {
                        x = this.from.getCoord("right") + hDistance / 2;
                        y = extend1_y;
                    }
                    else {
                        if (this.to.getCoord("left") > this.from.getCoord("left")) {
                            x = this.from.getCoord("left") - 20;
                        }
                        else {
                            x = this.to.getCoord("left") - 20;
                        }
                        if (this.from.getCoord("top") > this.to.getCoord("top")) {
                            y = this.to.getCoord("top") - 20;
                        }
                        else {
                            y = this.from.getCoord("top") - 20;
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
                var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                if (hDistance > 10) {
                    extend2_x = x = this.to.getCoord("right") + hDistance / 2;
                    y = y1;
                }
                else {
                    var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (vDistance > 10) {
                        x = this.to.getCoord("right") + 20;
                        extend1_y = y = this.from.getCoord("bottom") + vDistance / 2;
                    }
                    else {
                        if (this.to.getCoord("bottom") > this.from.getCoord("bottom")) {
                            extend1_y = y = this.to.getCoord("bottom") + 20;
                        }
                        else {
                            extend1_y = y = this.from.getCoord("bottom") + 20;
                        }
                        if (this.to.getCoord("right") > this.from.getCoord("right")) {
                            extend2_x = x = this.to.getCoord("right") + 20;
                        }
                        else {
                            extend2_x = x = this.from.getCoord("right") + 20;
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
                if (this.from.getCoord("bottom") > this.to.getCoord("bottom")) {
                    x = this.from.getCoord("left") - 20;
                    if (x2 < x) {
                        x = x2;
                    }
                    y = extend1_y;
                }
                else {
                    if (x1 - this.to.getCoord("right") > 10) {
                        x = x1;
                        extend1_y = y = this.to.getCoord("bottom") + 20;
                    }
                    else {
                        x = this.to.getCoord("right") + 20;
                        y = extend1_y;
                    }
                }
            }
            else {
                //在右边
                if (this.from.getCoord("bottom") > this.to.getCoord("bottom")) {
                    x = this.from.getCoord("right") + 20;
                    if (x2 > x) {
                        x = x2;
                    }
                    y = extend1_y;
                }
                else {
                    if (this.to.getCoord("left") - x1 > 10) {
                        x = x1;
                        extend1_y = y = this.to.getCoord("bottom") + 20;
                    }
                    else {
                        x = this.to.getCoord("left") - 20;
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
                var hDistance = this.to.getCoord("left") - this.from.getCoord("right");
                if (hDistance > 10) {
                    extend2_x = x = this.from.getCoord("right") + hDistance / 2;
                    y = y1;
                }
                else {
                    var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (vDistance > 10) {
                        x = this.to.getCoord("left") - 20;
                        extend1_y = y = this.from.getCoord("bottom") + vDistance / 2;
                    }
                    else {
                        if (this.to.getCoord("bottom") > this.from.getCoord("bottom")) {
                            extend1_y = y = this.to.getCoord("bottom") + 20;
                        }
                        else {
                            extend1_y = y = this.from.getCoord("bottom") + 20;
                        }
                        if (this.to.getCoord("left") > this.from.getCoord("left")) {
                            extend2_x = x = this.from.getCoord("left") - 20;
                        }
                        else {
                            extend2_x = x = this.to.getCoord("left") - 20;
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
    points.push({ x: x2, y: y2 });
    return YChart.Transition.mergePoints(points, "h");
}

YChart.Transition.prototype.processFromLeftConnection = function (x1, y1, x2, y2, toPosition) {
    var points = [{ x: x1, y: y1 }];
    var x = 0, y = 0, extend1_x = x1 - 20, temp;
    switch (toPosition) {
        case "top":
            var extend2_y = y2 - 20;
            if (x1 - x2 > 10 && y2 - y1 > 10) {
                x = x2;
                y = y1;
                extend1_x = extend2_y = null;
            }
            else {
                var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                if (hDistance > 10) {
                    extend1_x = x = this.to.getCoord("right") + hDistance / 2;
                    y = y1;
                }
                else {
                    var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (vDistance > 10) {
                        extend2_y = y = this.from.getCoord("bottom") + vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.to.getCoord("top") > this.from.getCoord("top")) {
                            extend2_y = y = this.from.getCoord("top") - 20;
                        }
                        else {
                            extend2_y = y = this.to.getCoord("top") - 20;
                        }
                        if (this.to.getCoord("left") > this.from.getCoord("left")) {
                            extend1_x = x = this.from.getCoord("left") - 20;
                        }
                        else {
                            extend1_x = x = this.to.getCoord("left") - 20;
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
            var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
            if (hDistance > 10) {
                extend1_x = extend2_x = x = this.to.getCoord("right") + hDistance / 2;
                y = y2;
            }
            else {
                if (y1 > y2) {
                    //在上边
                    var vDistance = this.from.getCoord("top") - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        y = this.to.getCoord("top") + vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.to.getCoord("bottom") > this.from.getCoord("bottom")) {
                            y = this.to.getCoord("bottom") + 20;
                        }
                        else {
                            y = this.from.getCoord("bottom") + 20;
                        }
                        if (this.to.getCoord("right") > this.from.getCoord("right")) {
                            extend2_x = x = this.to.getCoord("right") + 20;
                        }
                        else {
                            extend2_x = x = this.from.getCoord("right") + 20;
                        }
                    }
                }
                else {
                    var vDistance = this.to.getCoord("top") - this.from.getCoord("bottom");
                    if (vDistance > 10) {
                        y = this.from.getCoord("bottom") + vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.to.getCoord("top") > this.from.getCoord("top")) {
                            y = this.from.getCoord("top") - 20;
                        }
                        else {
                            y = this.to.getCoord("top") - 20;
                        }
                        if (this.to.getCoord("right") > this.from.getCoord("right")) {
                            extend2_x = x = this.to.getCoord("right") + 20;
                        }
                        else {
                            extend2_x = x = this.from.getCoord("right") + 20;
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
                var hDistance = this.from.getCoord("left") - this.to.getCoord("right");
                if (hDistance > 10) {
                    extend1_x = x = this.to.getCoord("right") + hDistance / 2;
                    y = y1;
                }
                else {
                    var vDistance = this.from.getCoord("top") - this.to.getCoord("bottom");
                    if (vDistance > 10) {
                        extend2_y = y = this.from.getCoord("top") - vDistance / 2;
                        x = extend1_x;
                    }
                    else {
                        if (this.to.getCoord("bottom") > this.from.getCoord("bottom")) {
                            extend2_y = y = this.to.getCoord("bottom") + 20;
                        }
                        else {
                            extend2_y = y = this.from.getCoord("bottom") + 20;
                        }
                        if (this.to.getCoord("left") > this.from.getCoord("left")) {
                            extend1_x = x = this.from.getCoord("left") - 20;
                        }
                        else {
                            extend1_x = x = this.to.getCoord("left") - 20;
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
                    if (this.from.getCoord("top") - y2 > 10) {
                        y = y2;
                    }
                    else {
                        y = this.from.getCoord("top") - 20;
                    }
                }
                else {
                    //在左边
                    x = extend2_x;
                    if (y1 - this.to.getCoord("bottom") > 10) {
                        y = y1;
                        extend1_x = x;
                    }
                    else {
                        y = this.to.getCoord("bottom") + 20;
                    }
                }
            }
            else {
                if (x2 >= x1) {
                    x = extend1_x;
                    //在右边
                    if (y2 - this.from.getCoord("bottom") > 10) {
                        y = y2;
                    }
                    else {
                        y = this.from.getCoord("bottom") + 20;
                    }
                }
                else {
                    x = extend2_x;
                    if (this.to.getCoord("top") - y1 > 10) {
                        y = y1;
                    }
                    else {
                        y = this.to.getCoord("top") - 20;
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
    points.push({ x: x2, y: y2 });
    return YChart.Transition.mergePoints(points, "v");
}

YChart.Transition.getArrowPath = function (direction, x, y) {
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

YChart.Transition.prototype.generateToPointArrow = function (offsetX, offsetY) {
    var direction;
    if (this.lines.length == 0) {
        if (this.toActivityPoint) {
            direction = YChart.Transition.getDirectionByPosition(this.toActivityPoint.position);
        }
        else if (this.fromActivityPoint) {
            direction = YChart.Transition.getDirectionByPosition(this.fromActivityPoint.position);
        }
        else {
            direction = "lefttoright";
        }
    }
    else {
        direction = YChart.Transition.getLineStatus(this.lines[this.lines.length - 1].ui).direction;
    }
    var x = this.x2, y = this.y2;
    if (offsetX) {
        x = x + offsetX;
    }
    if (offsetY) {
        y = y + offsetY;
    }
    var path = YChart.Transition.getArrowPath(direction, x, y);
    if (path) {
        this.toPoint = YChart.createElementNS("path", { d: path });
        this.fromPoint.parentNode.appendChild(this.toPoint);
    }
}

YChart.Transition.getLineStatus = function (line) {
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

YChart.Transition.mergePoints = function (points, firstDirection) {
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

YChart.Transition.Line = function (canvas, option) {
    var line = YChart.createElementNS("line", { className: "ycd_t_line" });
    line.x1.baseVal.value = option.x1;
    line.y1.baseVal.value = option.y1;
    line.x2.baseVal.value = option.x2;
    line.y2.baseVal.value = option.y2;
    canvas.appendChild(line);
    YChart.toBack(line);
    this.ui = line;
}

YChart.Transition.Line.prototype.remove = function () {
    YChart.remove(this.ui);
}

YChart.Transition.Line.prototype.toFront = function () {
    YChart.toFront(this.ui);
}

YChart.Transition.Line.prototype.toBack = function () {
    YChart.toBack(this.ui);
}