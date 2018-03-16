"use strict"
if (typeof (YChart) == "undefined") {
    window.YChart = {};
}
YChart.DateBox = {};
YChart.DateBox.init = function (element, options) {
    if (YChart.isArray(element)) {
        for (var i = 0; i < element.length; i++) {
            YChart.attachEvent(element[i], "click", function () {
                YChart.DateBox.pick(this, options);
                YChart.stopBubble();
            });
        }
    }
    else {
        YChart.attachEvent(element, "click", function () {
            YChart.DateBox.pick(this, options);
            YChart.stopBubble();
        });
    }
}
YChart.DateBox.pick = function (obj, options) {
    YChart.DateBox.target = obj;
    obj.setAttribute("readonly", true);
    if (!obj.getAttribute("init_c")) {
        obj.setAttribute("init_c", "1");
        YChart.attachEvent(obj, "keydown", function (event) {
            if ((event.keyCode || event.switch || event.charCode) == 8) {
                return false;
            }
        });
    }
    var datebox = YChart.DateBox.getDateBox();
    var content = YChart.trim(obj.value);
    if (YChart.isDateFormat(content)) {
        content = content.replace(/\//, "-");
        var index = content.indexOf("-");
        var year = content.substring(0, index);
        content = content.substring(index + 1, content.length);
        index = content.indexOf("-");
        YChart.DateBox.choosed = {
            year: parseInt(year),
            month: parseInt(content.substring(0, index) - 1),
            date: parseInt(content.substring(index + 1, content.length))
        }
    }
    else {
        var cdate = new Date();
        YChart.DateBox.choosed = {
            year: cdate.getFullYear(),
            month: cdate.getMonth(),
            date: cdate.getDate()
        }
    }
    YChart.DateBox.minDate = null;
    YChart.DateBox.maxDate = null;
    if (options) {
        if (options.minDateElement) {
            options.minDate = options.minDateElement.value;
        }
        if (options.minDate) {
            if (typeof (options.minDate) == "string") {
                try {
                    YChart.DateBox.minDate = YChart.parseDate(options.minDate);
                }
                catch (err) { }
            }
            else if (typeof (options.minDate) == "object" && options.minDate.getTime()) {
                YChart.DateBox.minDate = cfg.minDate;
            }
        }
        if (options.maxDateElement) {
            options.maxDate = options.maxDateElement.value;
        }
        if (options.maxDate) {
            if (typeof (options.maxDate) == "string") {
                try {
                    YChart.DateBox.maxDate = YChart.parseDate(options.maxDate);
                }
                catch (err) { }
            }
            else if (typeof (options.maxDate) == "object" && options.maxDate.getTime()) {
                YChart.DateBox.maxDate = options.maxDate;
            }
        }
    }
    if (!YChart.DateBox.currentYear || YChart.DateBox.currentYear != YChart.DateBox.year || YChart.DateBox.currentMonth != YChart.DateBox.month || YChart.DateBox.currentDate != YChart.DateBox.date) {
        YChart.DateBox.currentYear = YChart.DateBox.choosed.year;
        YChart.DateBox.currentMonth = YChart.DateBox.choosed.month;
        YChart.DateBox.currentDate = YChart.DateBox.choosed.date;
        YChart.DateBox.generateDates();
    }
    var position = obj.getBoundingClientRect();
    if (position.top + obj.offsetHeight + datebox.offsetHeight > document.documentElement.clientHeight) {
        datebox.style.top = (document.documentElement.clientHeight - datebox.offsetHeight) + "px";
    }
    else {
        datebox.style.top = (position.top + obj.offsetHeight) + "px";
    }
    if (position.left + datebox.offsetWidth > document.documentElement.clientWidth) {
        datebox.style.left = (document.documentElement.clientWidth - datebox.offsetWidth) + "px"
    }
    else {
        datebox.style.left = position.left + "px";
    }
    YChart.attachEvent(obj, "click", function (event) { return YChart.stopBubble(event) });
    YChart.attachEvent(document.body, "click", YChart.DateBox.hide);
}

YChart.DateBox.getDateBox = function () {
    var v = document.getElementById("_yc_datebox_container");
    if (!v) {
        v = document.createElement("div");
        v.style.position = "absolute";
        v.id = "_yc_datebox_container";
        v.style.zIndex = 1000;
        v.style.border = "1px solid #EFEFEF";
        v.style.width = "190px";
        v.style.backgroundColor = "#FFFFFF";
        v.onclick = function (event) {
            return YChart.stopBubble(event);
        }
        document.body.appendChild(v);
        YChart.DateBox.createCalendar(v);
        var v1 = YChart.createElement("div", null, { backgroundColor: "#FFFFFF", float: "left", margin: "2px 8px 5px 8px" });
        v.appendChild(v1);
        var btn = YChart.DateBox.getImitateButton("今日");
        btn.onclick = function () {
            var date = new Date();
            if ((YChart.DateBox.minDate && date < YChart.DateBox.minDate) || (YChart.DateBox.maxDate && date > YChart.DateBox.maxDate)) {
                if (YChart.DateBox.currentYear != date.getFullYear() || YChart.DateBox.currentMonth != date.getMonth()) {
                    YChart.DateBox.currentYear = date.getFullYear();
                    YChart.DateBox.currentMonth = date.getMonth();
                    YChart.DateBox.generateDates();
                }
            }
            else {
                YChart.DateBox.target.value = YChart.getDateText(date, "yyyy-MM-dd");
                YChart.DateBox.hide();
            }
        }
        btn.style.width = "80px";
        v1.appendChild(btn);
        btn = YChart.DateBox.getImitateButton("清空");
        btn.onclick = function () {
            YChart.DateBox.target.value = "";
            //Lscheduler.calendar.hide();
            //Lscheduler.calendar.target.focus();
        }
        btn.style.width = "80px";
        btn.style.marginLeft = "9px";
        v1.appendChild(btn)
    }
    else {
        v.style.display = "";
    }
    return v;
}

YChart.DateBox.hide = function () {
    document.getElementById("_yc_datebox_container").style.display = "none";
    var temp = document.getElementById("_yc_datebox_yearchoose");
    if (temp && temp.style.display == "") {
        temp.style.display = "none";
        temp.style.height = "0px";
        temp.removeAttribute("f");
    }
    else {
        temp = document.getElementById("_yc_datebox_monthchoose");
        if (temp && temp.style.display == "") {
            temp.style.display = "none";
            temp.style.height = "0px";
            temp.removeAttribute("f");
        }
    }
    YChart.removeEvent(document.body, "click", YChart.DateBox.hide);
}
YChart.DateBox.createCalendar = function (container) {
    var v = document.createElement("div");
    v.style.margin = "2px 0px 0px 17px";
    var v1 = document.createElement("div");
    v1.style.float = "left";
    v1.style.height = "22px";
    v1.style.cursor = "pointer";
    v1.id = "_yc_datebox_prev";
    v.appendChild(v1);
    v1.onmouseover = function () {
        this.style.backgroundColor = "#F3F3F3";
    }
    v1.onmouseout = function () {
        this.style.backgroundColor = "";
    }
    v1.onclick = YChart.DateBox.decreaseMonth;
    var v11 = YChart.createElement("span", { className: "fa fa-angle-left yc_datebox_prev" });
    v1.appendChild(v11);
    v1 = document.createElement("div");
    v1.style.float = "left";
    v.appendChild(v1);
    v11 = document.createElement("span");
    v11 = YChart.createElement("span", { id: "_yc_datebox_year" }, {
        cursor: "pointer",
        height: "22px",
        display: "inline-block",
        lineHeight: "22px",
        padding: "0px 5px 0px 5px"
    });
    v11.onmouseover = function () {
        this.style.backgroundColor = "#F3F3F3";
    }
    v11.onmouseout = function () {
        this.style.backgroundColor = "";
    }
    v11.onclick = function () {
        var div = document.getElementById("_yc_datebox_content");
        var v = document.getElementById("_yc_datebox_yearchoose");
        if (!v) {
            v = YChart.createElement("div", { id: "_yc_datebox_yearchoose" }, { height: "1px", width: "100%", backgroundColor: "#FFFFFF", position: "absolute", top: "0px", overflow: "hidden", borderTop: "1px solid #EFEFEF" });
            div.appendChild(v);
            v.appendChild(YChart.createElement("div"));
            v.appendChild(YChart.createElement("div", null, { float: "left" }));
            var b1 = YChart.createElement("div", { className: "yc_datebox_txt1" }, { float: "left", width: "36px", height: "22px", padding: "3px 21px 3px 21px", marginLeft: "4px" });
            v.childNodes[1].appendChild(b1);
            var b = YChart.createElement("input", { type: "text", value: YChart.DateBox.currentYear }, { width: "36px", float: "left", height: "22px", border: "none", outline: "none" });
            b.onkeydown = function (event) {
                if (event.keyCode == 13) {
                    v.childNodes[1].childNodes[1].click();
                }
            }
            b1.appendChild(b);
            var btn = YChart.DateBox.getImitateButton("确定");
            btn.style.marginLeft = "10px";
            btn.onclick = function () {
                var y = YChart.trim(b.value);
                var yt = /^\d{1,4}$/;
                if (!yt.test(y)) {
                    return;
                }
                YChart.DateBox.changeYear(parseInt(y));
            }
            v.childNodes[1].appendChild(btn);
        }
        var f = v.getAttribute("f");
        if (YChart.DateBox.yearAnimation) {
            clearInterval(YChart.DateBox.yearAnimation);
        }
        if (f && f == "1") {
            YChart.DateBox.hideYearChoose(v);
        }
        else {
            YChart.DateBox.showYearChoose(v, div.offsetHeight - 2);
        }
    }
    v1.appendChild(v11);
    v1.appendChild(document.createTextNode("年"));
    v11 = document.createElement("span");
    v11 = YChart.createElement("span", { id: "_yc_datebox_month" }, {
        cursor: "pointer",
        height: "22px",
        display: "inline-block",
        lineHeight: "22px",
        padding: "0px 5px 0px 5px"
    });
    v11.onmouseover = function () {
        this.style.backgroundColor = "#F3F3F3";
    };
    v11.onmouseout = function () {
        this.style.backgroundColor = "";
    };
    v11.onclick = function () {
        var div = document.getElementById("_yc_datebox_content");
        var v = document.getElementById("_yc_datebox_monthchoose");
        if (!v) {
            v = YChart.createElement("div", { id: "_yc_datebox_monthchoose" }, { height: "1px", width: "100%", backgroundColor: "#FFFFFF", position: "absolute", top: "0px", overflow: "hidden", borderTop: "1px solid #EFEFEF" });
            div.appendChild(v);
        }
        if (YChart.DateBox.monthAnimation) {
            clearInterval(YChart.DateBox.monthAnimation);
        }
        var f = v.getAttribute("f");
        if (f && f == "1") {
            YChart.DateBox.hideMonthChoose(v);
        }
        else {
            YChart.DateBox.showMonthChoose(v, div.offsetHeight - 2);
        }
    }
    v1.appendChild(v11);
    v1.appendChild(document.createTextNode("月"));
    v1 = YChart.createElement("div", null, { cursor: "pointer" });
    v1.style.float = "left";
    v1.style.height = "22px";
    v1.id = "_yc_datebox_next";
    v.appendChild(v1);
    v1.onmouseover = function () {
        this.style.backgroundColor = "#F3F3F3";
    }
    v1.onmouseout = function () {
        this.style.backgroundColor = "";
    }
    v1.onclick = YChart.DateBox.increaseMonth;
    v11 = YChart.createElement("span", { className: "fa fa-angle-right yc_datebox_prev" });
    v1.appendChild(v11);
    container.appendChild(v);
    v = YChart.createElement("div", { id: "_yc_datebox_content" }, { margin: "2px 5px 0px 5px", position: "relative", float: "left" });
    container.appendChild(v);
    v11 = YChart.createElement("table");
    v11.className = "yc_datebox_cal";
    v11.style.margin = "0px";
    v11.id = "_yc_datebox_dates";
    v.appendChild(v11);
    var v111, v1111;
    v111 = v11.insertRow(-1);
    //v111.className = "header";
    for (var i = 0; i < YChart.miniWeek.length; i++) {
        v1111 = v111.insertCell(-1);
        v1111.appendChild(document.createTextNode(YChart.miniWeek[i]));
    }
}

YChart.miniWeek = ["一", "二", "三", "四", "五", "六", "日"];

YChart.getFullMonthDays = function (year, month) {
    var startDate = new Date(year, month, 1, 0, 0, 0);
    var week = startDate.getDay();
    if (week != 1) {
        if (week == 0) {
            week = 7;
        }
        //若不为周一，则开始日期为周一
        startDate = new Date(startDate.valueOf() - (week - 1) * 24 * 60 * 60 * 1000);
    }
    var endDate = new Date(year, month, YChart.getDaysInMonth(year, month), 23, 59, 59);
    week = endDate.getDay();
    if (week != 0) {
        endDate = new Date(endDate.valueOf() + (7 - week) * 24 * 60 * 60 * 1000);
    }
    var dates = [];
    do {
        dates.push(startDate);
        if (YChart.dateEqual(endDate, startDate)) {
            break;
        }
        startDate = new Date(startDate.valueOf() + 24 * 60 * 60 * 1000);
    }
    while (1 == 1);
    if (dates.length == 35) {
        for (var i = 0; i < 7; i++) {
            startDate = new Date(startDate.valueOf() + 24 * 60 * 60 * 1000);
            dates.push(startDate);
        }
    }
    return dates;
}

YChart.getDaysInMonth = function (year, month) {
    var b = new Date(year, month, 1);
    b.setMonth(b.getMonth() + 1);
    b.setDate(0);
    return b.getDate();
}

YChart.dateEqual = function (a, b) {
    if (a.getFullYear() == b.getFullYear() && a.getMonth() == b.getMonth() && a.getDate() == b.getDate()) {
        return true;
    }
    return false;
}

YChart.DateBox.generateDates = function () {
    var v111, v1111, v11 = document.getElementById("_yc_datebox_dates");
    for (var i = v11.rows.length - 1; i > 0; i--) {
        v11.deleteRow(i);
    }
    var dates = document.getElementById("_yc_datebox_year");
    dates.innerHTML = "";
    dates.appendChild(document.createTextNode(YChart.supplyString(YChart.DateBox.currentYear, 4)));
    dates = document.getElementById("_yc_datebox_month");
    dates.innerHTML = "";
    dates.appendChild(document.createTextNode(YChart.supplyString(YChart.DateBox.currentMonth + 1)));
    dates = YChart.getFullMonthDays(YChart.DateBox.currentYear, YChart.DateBox.currentMonth);
    var currentDate = new Date(), currentCell = null;
    for (var i = 0; i < dates.length; i++) {
        if (i % 7 == 0) {
            v111 = v11.insertRow(-1);
        }
        v1111 = v111.insertCell(-1);
        v1111.appendChild(document.createTextNode(YChart.supplyString(dates[i].getDate().toString())));
        if (dates[i].getMonth() != YChart.DateBox.currentMonth) {
            v1111.style.color = "#666666";
            if (dates[i].getFullYear() > YChart.DateBox.currentYear || (dates[i].getFullYear() == YChart.DateBox.currentYear && dates[i].getMonth() > YChart.DateBox.currentMonth)) {
                v1111.setAttribute("nm", "1");
            }
            else {
                v1111.setAttribute("nm", "-1");
            }
        }
        if (dates[i].getDate() == YChart.DateBox.choosed.date && dates[i].getMonth() == YChart.DateBox.choosed.month && dates[i].getFullYear() == YChart.DateBox.choosed.year) {
            v1111.setAttribute("l", "1");
            v1111.style.backgroundColor = "#3C60F0";
            v1111.style.color = "#ffffff";
        }
        if (YChart.dateEqual(dates[i], currentDate)) {
            v1111.style.fontWeight = "bolder";
        }
        if (YChart.DateBox.minDate && (dates[i] < YChart.DateBox.minDate)) {
            v1111.style.color = "#BBBBBB";
            v1111.style.fontStyle = "italic";
        }
        else if (YChart.DateBox.maxDate && (dates[i] > YChart.DateBox.maxDate)) {
            v1111.style.color = "#BBBBBB";
            v1111.style.fontStyle = "italic";
        }
        else {
            v1111.style.cursor = "pointer";
            v1111.onmouseover = function () {
                if (this.getAttribute("l") == "1") {
                    return;
                }
                this.style.backgroundColor = "#e9e184";
            }
            v1111.onmouseout = function () {
                if (this.getAttribute("l") == "1") {
                    return;
                }
                this.style.backgroundColor = "";
            }
            v1111.onclick = function () {
                if (currentCell) {
                    currentCell.removeAttribute("l", "1");
                    currentCell.style.backgroundColor = "";
                    if (currentCell.getAttribute("nm")) {
                        currentCell.style.color = "#929292";
                    }
                    else {
                        currentCell.style.color = "";
                    }
                }
                this.style.backgroundColor = "#3C60F0";
                this.style.color = "#ffffff";
                currentCell = this;
                var s = this.getAttribute("nm");
                if (s) {
                    s = YChart.DateBox.currentMonth + parseInt(s);
                }
                else {
                    s = YChart.DateBox.currentMonth;
                }
                if (s >= 12) {
                    YChart.DateBox.currentYear = YChart.DateBox.currentYear + 1;
                    s = s - 12;
                }
                else if (s == -1) {
                    YChart.DateBox.currentYear = YChart.DateBox.currentYear - 1;
                    s = 11;
                }
                YChart.DateBox.target.value = YChart.DateBox.currentYear + "-" + YChart.supplyString((s + 1)) + "-" + YChart.supplyString(YChart.getText(currentCell));
                YChart.DateBox.hide();
            }
        }
    }
}

YChart.DateBox.decreaseMonth = function () {
    if (YChart.DateBox.currentMonth == 0) {
        YChart.DateBox.currentMonth = 11;
        YChart.DateBox.currentYear--;
    }
    else {
        YChart.DateBox.currentMonth--;
    }
    YChart.DateBox.generateDates();
}

YChart.DateBox.increaseMonth = function () {
    if (YChart.DateBox.currentMonth == 11) {
        YChart.DateBox.currentMonth = 0;
        YChart.DateBox.currentYear++;
    }
    else {
        YChart.DateBox.currentMonth++;
    }
    YChart.DateBox.generateDates();
}

YChart.DateBox.rollBackYear = function () {
    YChart.DateBox.rollAmount--;
    YChart.DateBox.generateYears(document.getElementById("_yc_datebox_yearchoose"));
}

YChart.DateBox.rollFowardYear = function () {
    YChart.DateBox.rollAmount++;
    YChart.DateBox.generateYears(document.getElementById("_yc_datebox_yearchoose"));
}

YChart.DateBox.generateYears = function (container) {
    container.childNodes[0].innerHTML = "";
    var v1, v11, flag = 0, i = 0;
    if (YChart.DateBox.rollAmount <= 0) {
        i = YChart.DateBox.currentYear + (12 * (2 * YChart.DateBox.rollAmount - 1));
    }
    else {
        i = YChart.DateBox.currentYear + (12 * (2 * YChart.DateBox.rollAmount - 1)) + 1;
    }
    while (flag < 24) {
        if ((i + flag) == YChart.DateBox.currentYear) {
            i = i + 1;
            continue;
        }
        if (flag % 6 == 0) {
            v1 = YChart.createElement("ul", null, { width: "25%", listStyle: "none", float: "left", textAlign: "center", margin: "0px", padding: "0px" });
            container.childNodes[0].appendChild(v1);
        }
        v11 = YChart.createElement("li", null, { cursor: "pointer", height: "21px", lineHeight: "21px" });
        v1.appendChild(v11);
        if ((i + flag) <= 0) {
            v11.appendChild(document.createTextNode("----"));
            flag++;
            continue;
        }
        v11.appendChild(document.createTextNode(YChart.supplyString((i + flag), 4)));
        v11.onmouseover = function () {
            this.style.backgroundColor = "#F3F3F3";
        }
        v11.onmouseout = function () {
            this.style.backgroundColor = "";
        }
        v11.onclick = function () {
            YChart.DateBox.changeYear(parseInt(YChart.getText(this)));
        }
        flag++;
    }
}

YChart.DateBox.changeYear = function (yearValue) {
    document.getElementById("_yc_datebox_year").click();
    if (yearValue == YChart.DateBox.currentYear) {
        return;
    }
    YChart.DateBox.currentYear = yearValue;
    YChart.DateBox.generateDates();
}
YChart.DateBox.showYearChoose = function (srcElement, maxHeight) {
    if (srcElement.getAttribute("f") == "1") {
        return;
    }
    var monthchoose = document.getElementById("_yc_datebox_monthchoose");
    if (monthchoose) {
        if (monthchoose.style.display == "" && monthchoose.getAttribute("f") == "1") {
            monthchoose.style.zIndex = 20;
            srcElement.style.zIndex = 21;
            if (YChart.DateBox.monthAnimation) {
                clearInterval(YChart.DateBox.monthAnimation);
            }
            YChart.DateBox.hideMonthChoose(monthchoose);
        }
    }
    srcElement.style.display = "";
    srcElement.setAttribute("f", "1");
    YChart.DateBox.rollAmount = 0;
    srcElement.childNodes[1].childNodes[0].childNodes[0].value = YChart.supplyString(YChart.DateBox.currentYear, 4);
    YChart.DateBox.generateYears(srcElement);
    document.getElementById("_yc_datebox_prev").onclick = YChart.DateBox.rollBackYear;
    document.getElementById("_yc_datebox_next").onclick = YChart.DateBox.rollFowardYear;
    YChart.DateBox.yearAnimation = setInterval(function () {
        var h = srcElement.offsetHeight + 10;
        if (h >= maxHeight) {
            h = maxHeight;
            clearInterval(YChart.DateBox.yearAnimation);
            delete YChart.DateBox.yearAnimation;
            srcElement.childNodes[1].childNodes[0].focus();
        }
        srcElement.style.height = h + "px";
    }, 20);
}

YChart.DateBox.hideYearChoose = function (srcElement) {
    if (srcElement.getAttribute("f") == "0") {
        return;
    }
    srcElement.setAttribute("f", "0");
    document.getElementById("_yc_datebox_prev").onclick = YChart.DateBox.decreaseMonth;
    document.getElementById("_yc_datebox_next").onclick = YChart.DateBox.increaseMonth;
    YChart.DateBox.yearAnimation = setInterval(function () {
        var h = srcElement.offsetHeight - 10;
        if (h <= 0) {
            h = 0;
            srcElement.style.display = "none";
            clearInterval(YChart.DateBox.yearAnimation);
            delete YChart.DateBox.yearAnimation;
        }
        srcElement.style.height = h + "px";
    }, 20);
}

YChart.DateBox.showMonthChoose = function (srcElement, maxHeight) {
    if (srcElement.getAttribute("f") == "1") {
        return;
    }
    var yearchoose = document.getElementById("ls_cal_yearchoose");
    if (yearchoose) {
        if (yearchoose.style.display == "" && yearchoose.getAttribute("f") == "1") {
            yearchoose.style.zIndex = 20;
            srcElement.style.zIndex = 21;
            if (YChart.DateBox.yearAnimation) {
                clearInterval(YChart.DateBox.yearAnimation);
            }
            YChart.DateBox.hideYearChoose(yearchoose);
        }
    }
    srcElement.style.display = "";
    srcElement.setAttribute("f", "1");
    document.getElementById("_yc_datebox_prev").onclick = null;
    document.getElementById("_yc_datebox_next").onclick = null;
    YChart.DateBox.generateMonths(srcElement);
    YChart.DateBox.monthAnimation = setInterval(function () {
        var h = srcElement.offsetHeight + 10;
        if (h >= maxHeight) {
            h = maxHeight;
            clearInterval(YChart.DateBox.monthAnimation);
            delete YChart.DateBox.monthAnimation;
        }
        srcElement.style.height = h + "px";
    }, 20);
}

YChart.DateBox.hideMonthChoose = function (srcElement) {
    if (srcElement.getAttribute("f") == "0") {
        return;
    }
    srcElement.setAttribute("f", "0");
    document.getElementById("_yc_datebox_prev").onclick = YChart.DateBox.decreaseMonth;
    document.getElementById("_yc_datebox_next").onclick = YChart.DateBox.increaseMonth;
    YChart.DateBox.monthAnimation = setInterval(function () {
        var h = srcElement.offsetHeight - 10;
        if (h <= 0) {
            h = 0;
            srcElement.style.display = "none";
            clearInterval(YChart.DateBox.monthAnimation);
            delete YChart.DateBox.monthAnimation;
        }
        srcElement.style.height = h + "px";
    }, 20);
}

YChart.DateBox.generateMonths = function (container) {
    container.innerHTML = "";
    var v1, v2, v11;
    v1 = YChart.createElement("ul", null, { width: "50%", listStyle: "none", float: "left", textAlign: "center", magrin: "0px", padding: "0px" });
    container.appendChild(v1);
    v2 = YChart.createElement("ul", null, { width: "50%", listStyle: "none", float: "left", textAlign: "center", magrin: "0px", padding: "0px" });
    container.appendChild(v2);
    for (var i = 0; i < 12; i++) {
        v11 = YChart.createElement("li", null, { cursor: "pointer", height: "26px", lineHeight: "26px" });
        v11.appendChild(document.createTextNode(YChart.supplyString(i + 1)));
        if (i % 2 == 0) {
            v1.appendChild(v11);
        }
        else {
            v2.appendChild(v11);
        }
        if (i == YChart.DateBox.currentMonth) {
            v11.style.backgroundColor = "#3777D7";
            v11.style.color = "#FFFFFF";
            v11.style.cursor = "default";
            v11.onclick = function () { YChart.DateBox.hideMonthChoose(container) };
        }
        else {
            v11.onmouseover = function () {
                this.style.backgroundColor = "#F3F3F3";
            }
            v11.onmouseout = function () {
                this.style.backgroundColor = "";
            }
            v11.onclick = function () {
                YChart.DateBox.currentMonth = parseInt(YChart.getText(this)) - 1;
                YChart.DateBox.hideMonthChoose(container);
                YChart.DateBox.generateDates();
            }
        }
    }
}

YChart.DateBox.getImitateButton = function (text) {
    var div = YChart.createElement("div", null, { border: "1px solid #F0F0F0", float: "left", height: "27px", lineHeight: "27px", borderRadius: "2px", width: "80px", backgroundColor: "#F5F5F5", textAlign: "center", cursor: "pointer" });
    if (text) {
        div.appendChild(document.createTextNode(text));
    }
    YChart.DateBox.registerButtonEffect(div);
    return div;
}

YChart.DateBox.registerButtonEffect = function (button) {
    YChart.attachEvent(button, "mouseover", function () {
        button.style.boxShadow = "0 0 5px #848484";
    });
    YChart.attachEvent(button, "mouseout", function () {
        button.style.boxShadow = "";
    });
    YChart.attachEvent(button, "mousedown", function () {
        button.style.boxShadow = "inset 0 0 5px #848484";
    });
    YChart.attachEvent(button, "mouseup", function () {
        button.style.borderColor = "";
    });
}