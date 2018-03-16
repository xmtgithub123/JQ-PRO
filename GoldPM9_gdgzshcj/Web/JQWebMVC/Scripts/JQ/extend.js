
//================================String====================================

// 产生重复字符
String.prototype.pad = function (n, char) {
    var str = this;
    var len = str.length;
    while (len < n) {
        str = char + str;
        len++;
    }
    return str;
};

// 去除左右空格
String.prototype.trim = function () {
    var str = this,
	str = str.replace(/^\s\s*/, ''),
	ws = /\s/,
	i = str.length;
    while (ws.test(str.charAt(--i)));
    return str.slice(0, i + 1);
}

// 去除左边空格
String.prototype.trimStart = function () {
    return this.replace(/(^\s*)/g, "");
};

// 去除右边空格
String.prototype.trimEnd = function () {
    return this.replace(/(\s*$)/g, "");
};

// 去除左右空格
String.prototype.clearSpaceToOne = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "").replace(/\s+/g, " ");
};

// 判断字符是否是 str 开头
String.prototype.startWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substr(0, str.length) == str)
        return true;
    else
        return false;
    return true;
};

// 判断字符是否是 str 结尾
String.prototype.endWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substring(this.length - str.length) == str)
        return true;
    else
        return false;
    return true;
};

// 得到字符长度，中文算2个字符
String.prototype.getByteLength = function () {
    var ch, bytenum = 0;
    var pt = /[^\x00-\xff]/;
    for (var i = 0; i < this.length; i++) {
        ch = this.substr(i, 1);
        if (ch.match(pt)) {
            bytenum += 2;
        } else {
            bytenum += 1;
        }
    }
    return bytenum;
};

// 字符截取 num 长度，并在结尾加 ostr
String.prototype.getByteLengthString = function (num, ostr) {
    var ch, bytenum = 0;
    var rs = "";
    var pt = /[^\x00-\xff]/;
    for (var i = 0; i < this.length; i++) {
        ch = this.substr(i, 1);
        if (ch.match(pt)) {
            bytenum += 2;
            if (bytenum > num) {
                return rs + ostr;
            }
        } else {
            bytenum += 1;
            if (bytenum == num) {
                return rs + ostr;
            }
        }
        rs += ch;
    }
    return rs;
};

// 仿 C# String.Format
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g, function (m, i) {
        return args[i];
    });
};

//// 匹配正则表达式
//String.prototype.match = function () {

//};

// 转换为 数字 类型
String.prototype.toInt = function () {
    return parseInt(this, 10);
};

// 仿 C# String.Format
String.format = function () {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
};

/**
* 过滤字符串里面的html标记，类似于 innerText
*/
String.prototype.stripTags = function () {
    return this.replace(/<\/?[^>]+>/gi, '');
};

/**
* 类似 escape，转换 " & < > 为 &quot; &amp; %lt; &gt;
*/
String.prototype.escapeHTML = function () {
    var div = document.createElement('div');
    var text = document.createTextNode(this);
    div.appendChild(text);
    return div.innerHTML.replace(/"/g, "&quot;").replace(/'/g, "&sbquo");
    //return this.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/\ /g, "&nbsp;");
};

/**
* 和上面相反
*/
String.prototype.unescapeHTML = function () {
    var div = document.createElement('div');
    div.innerHTML = this.stripTags();
    return div.childNodes[0] ? div.childNodes[0].nodeValue : '';
};

// 生成重复字符串
String.prototype.times = function (n) {
    if (n == 1) {
        return this;
    }
    var s = this.times(Math.floor(n / 2));
    s += s;
    if (n % 2) {
        s += this;
    }
    return s;
}

// 得到安全字符串（用于搜索）
String.prototype.getSafeText = function () {
    var _regSafeText = /(\'|\-{2,2})+/;
    return this.replace(_regSafeText, "");
};

// 反序列化提交参数对象 如把 name=aa&sex=1 转换为 {name:"aaa",sex="1"}
String.prototype.deserialize = function (separator) {
    var hash = {};
    var match = this.trim().match(/([^?#]*)(#.*)?$/);
    if (!match) return {};
    var querys = match[1].split(separator || '&');
    for (var i = 0; i < querys.length; i++) {
        var pair = querys[i].split('=')
        if (pair[0]) {
            var key = decodeURIComponent(pair.shift());
            var value = pair.length > 1 ? pair.join('=') : pair[0];
            if (value != undefined) value = decodeURIComponent(value);
            hash[key] = value;
        }
    }
    return hash;
};

// 转换Json对象为url参数 如把 {name:"aaa",sex="1"} 转换为 name=aa&sex=1
String.serialize = function (oSender, sSeparator) {
    var stringBuilder = [];
    for (var p in oSender) {
        try {
            if (typeof (oSender[p]) != "function") {
                if (oSender[p] == null || oSender[p] == undefined) {
                    stringBuilder.push(p + "=");
                } else if (oSender[p] instanceof Array) {
                    stringBuilder.push(p + "=" + oSender[p].join(','));
                } else {
                    stringBuilder.push(p + "=" + encodeURIComponent(oSender[p].toString()));
                }
            }
        } catch (e) {

        }
    }
    return stringBuilder.join(sSeparator);
};

//================================Data====================================

var R_ISO8601_STR = /^(\d{4})-?(\d\d)-?(\d\d)(?:T(\d\d)(?::?(\d\d)(?::?(\d\d)(?:\.(\d+))?)?)?(Z|([+-])(\d\d):?(\d\d))?)?$/;
//                      1        2       3         4          5          6          7          8  9     10      11
Date.jsonStringToDate = function (string) {
    var match;
    if (string == undefined) return new Date(0);
    if (match = string.match(R_ISO8601_STR)) { //
        var date = new Date(0),
            tzHour = 0,
            tzMin = 0,
            dateSetter = match[8] ? date.setUTCFullYear : date.setFullYear,
            timeSetter = match[8] ? date.setUTCHours : date.setHours;

        if (match[9]) {
            tzHour = (match[9] + match[10]).toInt();
            tzMin = (match[9] + match[11]).toInt();
        }
        dateSetter.call(date, (match[1]).toInt(), (match[2]).toInt() - 1, (match[3]).toInt());
        var h = (match[4] || "0").toInt() - tzHour;
        var m = (match[5] || "0").toInt() - tzMin;
        var s = (match[6] || "0").toInt();
        var ms = Math.round(parseFloat('0.' + (match[7] || 0)) * 1000);
        timeSetter.call(date, h, m, s, ms);
        return date;
    }
    return new Date(0);
};
//把时间字符串转换为时间字符串,替换'/'为'-',替换1900,0001等时间为空
Date.jsonStringToDateString = function (string, formatString) {
    if (string == undefined) return "";
    string = string.replace('/', '-');
    var match;
    if (match = string.match(R_ISO8601_STR)) { //
        var date = new Date(0),
            tzHour = 0,
            tzMin = 0,
            dateSetter = match[8] ? date.setUTCFullYear : date.setFullYear,
            timeSetter = match[8] ? date.setUTCHours : date.setHours;

        if (match[9]) {
            tzHour = (match[9] + match[10]).toInt();
            tzMin = (match[9] + match[11]).toInt();
        }
        dateSetter.call(date, (match[1]).toInt(), (match[2]).toInt() - 1, (match[3]).toInt());
        var h = (match[4] || "0").toInt() - tzHour;
        var m = (match[5] || "0").toInt() - tzMin;
        var s = (match[6] || "0").toInt();
        var ms = Math.round(parseFloat('0.' + (match[7] || 0)) * 1000);
        timeSetter.call(date, h, m, s, ms);
        if (date.getFullYear() == "1" || date.getFullYear() == "1900") {
            return "";
        }
        return date.format(formatString);
    }
    return "";
};

Date.prototype.format = function (format) {
    // 时间格式化
    var o = {
        "M+": this.getMonth() + 1,                      //month 
        "d+": this.getDate(),                           //day 
        "h+": this.getHours(),                          //hour 
        "m+": this.getMinutes(),                        //minute 
        "s+": this.getSeconds(),                        //second 
        "q+": Math.floor((this.getMonth() + 3) / 3),    //quarter 
        "S": this.getMilliseconds()                     //millisecond 
    }
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};

//日期增加 "2000/12/31".dateAdd('d',1,);
Date.prototype.dateAdd = function (strInterval, Number) {
    var dtTmp = this;
    switch (strInterval) {
        case 's': return new Date(Date.parse(dtTmp) + (1000 * Number));
        case 'n': return new Date(Date.parse(dtTmp) + (60000 * Number));
        case 'h': return new Date(Date.parse(dtTmp) + (3600000 * Number));
        case 'd': return new Date(Date.parse(dtTmp) + (86400000 * Number));
        case 'w': return new Date(Date.parse(dtTmp) + ((86400000 * 7) * Number));
        case 'q': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number * 3, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'm': return new Date(dtTmp.getFullYear(), (dtTmp.getMonth()) + Number, dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
        case 'y': return new Date((dtTmp.getFullYear() + Number), dtTmp.getMonth(), dtTmp.getDate(), dtTmp.getHours(), dtTmp.getMinutes(), dtTmp.getSeconds());
    }
};

//+---------------------------------------------------  
//| 比较日期差 dtEnd 格式为日期型或者 有效日期格式字符串  
//+---------------------------------------------------
Date.prototype.dateDiff = function (strInterval, dtEnd) {
    var dtStart = this;
    if (typeof dtEnd == 'string')//如果是字符串转换为日期型  
    {
        dtEnd = new Date(dtEnd);
    }
    switch (strInterval) {
        case 's': return parseInt((dtEnd - dtStart) / 1000);
        case 'n': return parseInt((dtEnd - dtStart) / 60000);
        case 'h': return parseInt((dtEnd - dtStart) / 3600000);
        case 'd': return parseInt((dtEnd - dtStart) / 86400000);
        case 'w': return parseInt((dtEnd - dtStart) / (86400000 * 7));
        case 'm': return (dtEnd.getMonth() + 1) + ((dtEnd.getFullYear() - dtStart.getFullYear()) * 12) - (dtStart.getMonth() + 1);
        case 'y': return dtEnd.getFullYear() - dtStart.getFullYear();
    }
};

//+---------------------------------------------------  
//| 把日期分割成数组  
//+---------------------------------------------------
Date.prototype.toArray = function () {
    var myDate = this;
    var myArray = Array();
    myArray[0] = myDate.getFullYear();
    myArray[1] = myDate.getMonth();
    myArray[2] = myDate.getDate();
    myArray[3] = myDate.getHours();
    myArray[4] = myDate.getMinutes();
    myArray[5] = myDate.getSeconds();
    return myArray;
};

//+---------------------------------------------------  
//| 取得日期数据信息  
//| 参数 interval 表示数据类型  
//| y 年 m月 d日 w星期 ww周 h时 n分 s秒  
//+---------------------------------------------------
Date.prototype.datePart = function (interval) {
    var myDate = this;
    var partStr = '';
    var week = ['日', '一', '二', '三', '四', '五', '六'];
    switch (interval) {
        case 'y': partStr = myDate.getFullYear(); break;
        case 'm': partStr = myDate.getMonth() + 1; break;
        case 'd': partStr = myDate.getDate(); break;
        case 'w': partStr = week[myDate.getDay()]; break;
        case 'ww': partStr = myDate.WeekNumOfYear(); break;
        case 'h': partStr = myDate.getHours(); break;
        case 'n': partStr = myDate.getMinutes(); break;
        case 's': partStr = myDate.getSeconds(); break;
    }
    return partStr;
};

//+---------------------------------------------------  
//| 取得当前日期所在月的最大天数  
//+---------------------------------------------------
Date.prototype.maxDayOfDate = function () {
    var myDate = this;
    var ary = myDate.toArray();
    var date1 = (new Date(ary[0], ary[1] + 1, 1));
    var date2 = date1.dateAdd(1, 'm', 1);
    var result = dateDiff(date1.format('yyyy-MM-dd'), date2.format('yyyy-MM-dd'));
    return result;
};




/**  
 * layout显示及隐藏方法扩展  
 * @param {Object} jq  
 * @param {Object} region  
 */
$.extend($.fn.layout.methods, {
    /**  
     * 面板是否存在和可见  
     * @param {Object} jq  
     * @param {Object} params  
     */
    isVisible: function (jq, params) {
        var panels = $.data(jq[0], 'layout').panels;
        var pp = panels[params];
        if (!pp) {
            return false;
        }
        if (pp.length) {
            return pp.panel('panel').is(':visible');
        } else {
            return false;
        }
    },
    /**  
     * 隐藏除某个region，center除外。  
     * @param {Object} jq  
     * @param {Object} params  
     */
    hidden: function (jq, params) {
        return jq.each(function () {
            var opts = $.data(this, 'layout').options;
            var panels = $.data(this, 'layout').panels;
            if (!opts.regionState) {
                opts.regionState = {};
            }
            var region = params;
            function hide(dom, region, doResize) {
                var first = region.substring(0, 1);
                var others = region.substring(1);
                var expand = 'expand' + first.toUpperCase() + others;
                if (panels[expand]) {
                    if ($(dom).layout('isVisible', expand)) {
                        opts.regionState[region] = 1;
                        panels[expand].panel('close');
                    } else if ($(dom).layout('isVisible', region)) {
                        opts.regionState[region] = 0;
                        panels[region].panel('close');
                    }
                } else {
                    panels[region].panel('close');
                }
                if (doResize) {
                    $(dom).layout('resize');
                }
            };
            if (region.toLowerCase() == 'all') {
                hide(this, 'east', false);
                hide(this, 'north', false);
                hide(this, 'west', false);
                hide(this, 'south', true);
            } else {
                hide(this, region, true);
            }
        });
    },
    /**  
     * 显示某个region，center除外。  
     * @param {Object} jq  
     * @param {Object} params  
     */
    show: function (jq, params) {
        return jq.each(function () {
            var opts = $.data(this, 'layout').options;
            var panels = $.data(this, 'layout').panels;
            var region = params;

            function show(dom, region, doResize) {
                var first = region.substring(0, 1);
                var others = region.substring(1);
                var expand = 'expand' + first.toUpperCase() + others;
                if (panels[expand]) {
                    if (!$(dom).layout('isVisible', expand)) {
                        if (!$(dom).layout('isVisible', region)) {
                            if (opts.regionState[region] == 1) {
                                panels[expand].panel('open');
                            } else {
                                panels[region].panel('open');
                            }
                        }
                    }
                } else {
                    panels[region].panel('open');
                }
                if (doResize) {
                    $(dom).layout('resize');
                }
            };
            if (region.toLowerCase() == 'all') {
                show(this, 'east', false);
                show(this, 'north', false);
                show(this, 'west', false);
                show(this, 'south', true);
            } else {
                show(this, region, true);
            }
        });
    }
});

$.extend($.fn.validatebox.defaults.rules, {
    dateISO: {
        validator: function (value, param) {
            return /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}$/.test(value);
        },
        message: '日期格式不正确'
    },
    intOrFloat: {// 验证整数或小数
        validator: function (value) {
            return /^\d+(\.\d+)?$/i.test(value);
        },
        message: '请输入数字，并确保格式正确'
    }
})


if (typeof (GoldPM) == "undefined") {
    GoldPM = {};
}

GoldPM.loadXml = function (xmlContent) {
    var xmlDoc;
    if ("ActiveXObject" in window) {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        xmlDoc.loadXML(xmlContent);
    }
    else if (window.DOMParser) {
        parser = new DOMParser();
        xmlDoc = parser.parseFromString(xmlContent, "text/xml");
    }
    return xmlDoc;
};

GoldPM.selectNodes = function (node, xpath) {
    if ("ActiveXObject" in window) {
        return node.selectNodes(xpath);
    }
    else {
        var xpe = new XPathEvaluator();
        var nsResolver = xpe.createNSResolver(node.ownerDocument == null ? node.documentElement : node.ownerDocument.documentElement);
        var result = xpe.evaluate(xpath, node, nsResolver, 0, null);
        var found = [];
        var res;
        while (res = result.iterateNext())
            found.push(res);
        return found;
    }
};
