$(function () {
    $('#tab_menu-tabrefresh').click(function () {
        /*重新设置该标签 */
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src", url);
    });
    //在新窗口打开该标签
    $('#tab_menu-openFrame').click(function () {
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        window.open(url);
    });
    //关闭当前
    $('#tab_menu-tabclose').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('#mainTab').tabs('close', currtab_title);
        if ($(".tabs li").length == 0) {
            //open menu
            $(".layout-button-right").trigger("click");
        }
        initTabs();
    });
    //全部关闭
    $('#tab_menu-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                $('#mainTab').tabs('close', t);
            }
        });
        initTabs();
        //open menu
        $(".layout-button-right").trigger("click");
    });
    //关闭除当前之外的TAB
    $('#tab_menu-tabcloseother').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                if (t != currtab_title)
                    $('#mainTab').tabs('close', t);
            }
        });
        initTabs();
    });
    //关闭当前右侧的TAB
    $('#tab_menu-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            $.messager.alert(index_lang_tip, index_NoTabsOnTheLeft, 'warning');
            return false;
        }
        nextall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        initTabs();
        return false;
    });
    //关闭当前左侧的TAB
    $('#tab_menu-tabcloseleft').click(function () {

        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            $.messager.alert(index_lang_tip, index_NoTabsOnTheRight, 'warning');
            return false;
        }
        prevall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        initTabs();
        return false;
    });
    /*为选项卡绑定右键*/
    $("#mainTab").tabs({
        onSelect: function (title, index) {
            initTabs();
        },
        onContextMenu: function (e, title) {
            /* 选中当前触发事件的选项卡 */
            var subtitle = $(this).text();
            $('#mainTab').tabs('select', subtitle);
            //显示快捷菜单
            e.preventDefault();
            //阻止冒泡
            $('#tab_menu').menu('show', {
                left: e.pageX,
                top: e.pageY
            });
            return false;
        },
        onBeforeClose: function (title) {
            if (title == "个人中心") {
                return false;
            }
        }
    })
    $("#mainTab .tabs ").attr("style", "height:34px;line-height:34px");
    $("#mainTab .tabs li").find("a:first").attr("style", "height:32px;line-height:32px");
    $('#showUserInfo').tooltip({
        content: $("<div class=\"userinfodiv\"><ul>" + (window.isAgentLogin == 0 ? "<li onclick=\"showProfile()\"><span class=\"fa fa-gear fa-lg\"></span><span class=\"usertitle\">个人设置</span></li><li onclick=\"showchangepwd()\"><span class=\"fa fa-expeditedssl fa-lg\"></span><span class=\"usertitle\">修改密码</span></li>" : "") + (window.isAgentLogin == 1 ? "<li onclick=\"switchAgent()\"><span class=\"fa fa-random fa-lg\"></span><span class=\"usertitle\">切回登录</span></li>" : "") + "<li onclick=\"SignOut()\"><span class=\"fa fa-sign-out fa-lg\"></span><span class=\"usertitle\">注销登录</span></li></ul></div>"),
        onUpdate: function (content) {
            content.parent().parent().css({ padding: "0px", borderRadius: "0px" });
            content.panel({
                width: 176,
                border: false
            });
        },
        onShow: function () {
            var t = $(this);
            t.tooltip('tip').unbind().bind('mouseenter', function () {
                t.tooltip('show');
            }).bind('mouseleave', function () {
                t.tooltip('hide');
            });
        }
    });
    if (window.isAgentLogin == 1) {
        document.getElementById("showUserInfo").style.display = "inline-block";
    }
    window.reloadScheduler();
    window.reloadMail();
    window.reloadMessage();
});


function initTabs() {
    $("#mainTab .tabs ").attr("style", "height:33px;line-height:33px");
    $("#mainTab .tabs li").find("a:first").attr("style", "height:32px;line-height:32px");
}

function Profile() {
    addTab(index_lang_info, "../../" + _globalConfig.CurrentCulture + "/Home/Info", "fa fa-credit-card");
}

function switchAgent() {
    $.messager.confirm("系统提示", "您确定要切回由自己的身份登录吗？", function (r) {
        if (!r) {
            return;
        }
        $.ajax({
            url: window.urls.switchAgentUrl,
            type: "POST",
            success: function (result) {
                if (result.Result == false) {
                    JQ.dialog.error(result.Message);
                    return;
                }
                window.location.reload();
            }
        });
    });
}

//tabs页码bug
$(window).resize(function () {
    setTimeout(function () {
        initTabs()
    }, 100);
});



$(function () {
    $("#easyMod").click(function () {
        $('#easyLayout').layout('remove', 'north');
        $('#easyLayout').layout('remove', 'south');
    });
});

function fullSetButtonOut() {
    if ($("#north").is(":hidden")) {
        return "<div class='fullSet'></div><div id='fullSetButton' class='fa fa-compress'></div>";
    } else {
        return "<div class='fullSet'></div><div id='fullSetButton' class='fa fa-expand'></div>";
    }
}

function fullSet() {
    $("#north").slideToggle(150, function () {
        if ($("#north").is(":hidden")) {
            $("#easyLayoutByIndex").layout('collapse', 'west');
        }
        else {
            $("#easyLayoutByIndex").layout('expand', 'west');
        }
    });
}

window.reloadScheduler = function (data) {
    var _schedulerblock = document.getElementById("scheduler_block");
    if (!_schedulerblock || _schedulerblock.getAttribute("loading")) {
        return;
    }
    _schedulerblock.setAttribute("loading", "1");
    var _schedulercount = document.getElementById("schedulercount");
    _schedulercount.innerHTML = "<span class=\"fa fa-spinner fa-spin\" style=\"font-size:8px;text-align:center;display:inline-block;line-height:14px\"></span>";
    var ul;
    if (_schedulerblock.getAttribute("loaded")) {
        ul = $(_schedulerblock).tooltip("options").content[0];
        ul.childNodes[1].innerHTML = "";
        ul.childNodes[0].childNodes[1].innerHTML = "";
        $(ul.childNodes[0].childNodes[2]).addClass("fa-spin")[0].style.color = "#228DAA";
    }
    if (!data) {
        $.ajax({
            type: "POST",
            url: window.urls.getSchedulers,
            success: function (result) {
                loadSchedulerInfo(result, _schedulerblock, _schedulercount, ul);
            }
        });
    }
    else {

    }
}
function loadSchedulerInfo(result, _schedulerblock, _schedulercount, ul) {
    if (result.Result == false) {
        _schedulercount.innerHTML = "<span class=\"fa fa-exclamation-circle\" style=\"font-size:8px;display:inline-block;line-height:14px;text-align:center;color:#ef3838\"></span>";
        return;
    }
    _schedulercount.innerHTML = result.length;
    if (result.length == 0) {
        _schedulercount.style.display = "none";
    }
    if (!ul) {
        ul = JQ.Flow.createElement("ul", { className: "dropdown_block" });
        var li = JQ.Flow.createElement("li", { className: "dropdown_block_head" });
        ul.appendChild(li);
        li.appendChild(JQ.Flow.createElement("span", { className: "fa fa-bell" }));
        li.appendChild(JQ.Flow.createElement("span", null, { marginLeft: "5px", fontSize: "14px" }));
        var refresh = JQ.Flow.createElement("span", { className: "fa fa-refresh dropdown_refresh" });
        li.appendChild(refresh);
        refresh.onclick = function () {
            reloadScheduler();
        };
        li = JQ.Flow.createElement("li", { className: "dropdown_block_body" });
        ul.appendChild(li);
        li = JQ.Flow.createElement("li", { className: "dropdown_block_foot" });
        var div_all = JQ.Flow.createElement("div", { className: "dropdown_block_all" }, null, "打开日程表");
        li.appendChild(div_all);
        div_all.onclick = function () {
            window.openSchedulerTab();
        }
        ul.appendChild(li);
    }
    ul.childNodes[0].childNodes[1].appendChild(document.createTextNode(result.length + "条日程提醒"));
    for (var i in result) {
        appendScheduler(ul.childNodes[1], result[i]);
    }
    if (_schedulerblock.getAttribute("loaded")) {
        $(_schedulerblock).tooltip("update", $(ul));
        $(ul.childNodes[0].childNodes[2]).removeClass("fa-spin")[0].style.color = "";
    }
    else {
        _schedulerblock.setAttribute("loaded", "1");
        $(_schedulerblock).tooltip({
            content: $(ul),
            onUpdate: function (content) {
                content.parent().parent().css({ padding: "0px", borderRadius: "0px", borderColor: "#bcd4e5" });
                content.panel({
                    width: 220,
                    border: false
                });
            },
            onShow: function () {
                var t = $(this);
                t.tooltip("tip").unbind().bind("mouseenter", function () {
                    t.tooltip("show");
                }).bind("mouseleave", function () {
                    t.tooltip("hide");
                });
            }
        });
    }
    _schedulerblock.removeAttribute("loading");
}
var removeScheulerInfo = function (schdulerID) {
    var _schedulerblock = document.getElementById("scheduler_block");
    if (!_schedulerblock || !_schedulerblock.getAttribute("loaded")) {
        return;
    }
    //获取出弹出层
    var ul = $(_schedulerblock).tooltip("options").content[0];
    var bodyblock = ul.childNodes[1];
    for (var i = 0; i < bodyblock.childNodes.length; i++) {
        if (bodyblock.childNodes[i].getAttribute("dataid") == schdulerID) {
            if (bodyblock.childNodes[i].nextSibling == null && bodyblock.childNodes[i].previousSibling != null) {
                bodyblock.childNodes[i].previousSibling.style.borderBottom = "";
            }
            bodyblock.removeChild(bodyblock.childNodes[i]);
            break;
        }
    }
    setSchedulerAmount(bodyblock.childNodes.length, ul);
}
var changeSchedulerInfo = function (data) {
    var _schedulerblock = document.getElementById("scheduler_block");
    if (!_schedulerblock || !_schedulerblock.getAttribute("loaded")) {
        return;
    }
    var ul = $(_schedulerblock).tooltip("options").content[0];
    var bodyblock = ul.childNodes[1];
    for (var i = 0; i < bodyblock.childNodes.length; i++) {
        if (bodyblock.childNodes[i].getAttribute("dataid") == data.ID.toString()) {
            if (bodyblock.childNodes[i].nextSibling == null && bodyblock.childNodes[i].previousSibling != null) {
                bodyblock.childNodes[i].previousSibling.style.borderBottom = "";
            }
            bodyblock.removeChild(bodyblock.childNodes[i]);
            break;
        }
    }
    appendScheduler(bodyblock, data);
    setSchedulerAmount(bodyblock.childNodes.length, ul);
}
var timerSchedulerInfo = function (data) {
    var _schedulerblock = document.getElementById("scheduler_block");
    if (!_schedulerblock || !_schedulerblock.getAttribute("loaded")) {
        return;
    }
    var ul = $(_schedulerblock).tooltip("options").content[0];
    var bodyblock = ul.childNodes[1];
    if (data.ToRemoveData.length > 0) {
        for (var i = 0; i < data.ToRemoveData.length; i++) {
            for (var j = 0; j < bodyblock.childNodes.length; j++) {
                if (bodyblock.childNodes[j].getAttribute("dataid") == data.ToRemoveData[i].toString()) {
                    bodyblock.removeChild(bodyblock.childNodes[j]);
                    break;
                }
            }
        }
    }
    if (data.ToAddData.length > 0) {
        for (var i = 0; i < data.ToAddData.length; i++) {
            var isIn = false;
            for (var j = 0; j < bodyblock.childNodes.length; j++) {
                if (bodyblock.childNodes[j].getAttribute("dataid") == data.ToAddData[i].ID.toString()) {
                    isIn = true;
                    break;
                }
            }
            if (!isIn) {
                appendScheduler(bodyblock, data.ToAddData[i]);
            }
        }
    }
    setSchedulerAmount(bodyblock.childNodes.length, ul);
}
function setSchedulerAmount(amount, ul) {
    var _schedulercount = document.getElementById("schedulercount");
    if (!_schedulercount) {
        return;
    }
    _schedulercount.innerHTML = amount;
    if (amount == 0) {
        _schedulercount.style.display = "none";
    }
    else {
        _schedulercount.style.display = "inline";
    }
    ul.childNodes[0].childNodes[1].innerHTML = "";
    ul.childNodes[0].childNodes[1].appendChild(document.createTextNode(amount + "条日程提醒"));
}

function appendScheduler(parent, data) {
    var index = -1;
    for (var i = 0; i < parent.childNodes.length; i++) {
        var starttime = new Date(parent.childNodes[i].getAttribute("starttime").replace(/-/g, "/"));
        var endtime = new Date(parent.childNodes[i].getAttribute("endtime").replace(/-/g, "/"));
        if (data.IsFullDay) {
            if (!parent.childNodes[i].getAttribute("fullday")) {
                index = i;
                break;
            }
            else if ((starttime == data.StartTime && endtime > data.EndTime) || starttime > data.starttime) {
                index = i;
                break;
            }
        }
        else if ((starttime == data.StartTime && endtime > data.EndTime) || starttime > data.starttime) {
            index = i;
            break;
        }
    }
    var div = JQ.Flow.createElement("div", { className: "dropdown_block_item", dataid: data.ID });
    parent.appendChild(div);
    if (index > parent.childNodes.length - 1 || index == -1) {
        parent.appendChild(div);
    }
    else {
        parent.insertBefore(div, parent.childNodes[index]);
    }
    div.onclick = function () {
        JQ.dialog.dialog({
            title: "日程查看",
            url: window.urls.schedulerDetail + "?id=" + this.getAttribute("dataid") + "&source=remind",
            width: "800",
            height: "600",
            iconCls: "fa fa-clock-o"
        });
    }
    var e = JQ.Flow.createElement("div", null, { margin: "0px 3px 0px 3px", padding: "2px 0px 2px 0px", lineHeight: "18px", float: "left", textOverflow: "ellipsis" });
    div.appendChild(e);
    var starttime = JQ.Flow.parseDateTimeText(data.StartTime);
    var endtime = JQ.Flow.parseDateTimeText(data.EndTime);
    div.setAttribute("starttime", starttime);
    div.setAttribute("endtime", endtime);
    if (data.IsFullDay) {
        div.setAttribute("fullday", "1");
        //是全天
        starttime = starttime.substring(0, 10);
        endtime = endtime.substring(0, 10);
        if (starttime == endtime) {
            e.appendChild(JQ.Flow.createElement("div", { className: "dropdown_block_info" }, { fontSize: "12px", padding: "0px" }, starttime + " （全天）"));
        }
        else {
            e.appendChild(JQ.Flow.createElement("div", { className: "dropdown_block_info" }, { fontSize: "12px", padding: "0px" }, starttime + " - " + endtime + " （全天）"));
        }
    }
    else {
        e.appendChild(JQ.Flow.createElement("div", { className: "dropdown_block_info" }, { fontSize: "12px", padding: "0px" }, starttime.substring(0, 16) + " - " + endtime.substring(0, 16)));
    }
    var e_2 = JQ.Flow.createElement("div", { className: "dropdown_block_content" });
    e.appendChild(e_2);
    var contents = data.Content.split('\n');
    for (var j in contents) {
        e_2.appendChild(JQ.Flow.createElement("p", null, null, contents[j]));
    }
    if (e.previousSibling != null) {
        e.previousSibling.style.borderBottom = "1px solid #c8dded";
    }
    if (e.nextSibling != null) {
        e.nextSibling.style.borderBottom = "1px solid #c8dded";
    }
}

window.reloadMail = function (data) {
    var _mailblock = document.getElementById("mail_block");
    if (!_mailblock || _mailblock.getAttribute("loading")) {
        return;
    }
    _mailblock.setAttribute("loading", "1");
    var _mailcount = document.getElementById("mailcount");
    _mailcount.style.display = "inline";
    _mailcount.innerHTML = "<span class=\"fa fa-spinner fa-spin\" style=\"font-size:8px;text-align:center;display:inline-block;line-height:14px\"></span>";
    var ul;
    if (_mailblock.getAttribute("loaded")) {
        ul = $(_mailblock).tooltip("options").content[0];
        ul.childNodes[1].innerHTML = "";
        ul.childNodes[0].childNodes[1].innerHTML = "";
        $(ul.childNodes[0].childNodes[2]).addClass("fa-spin")[0].style.color = "#228DAA";
    }
    if (!data) {
        $.ajax({
            type: "POST",
            url: window.urls.getMails,
            success: function (result) {
                loadMailInfo(result, _mailblock, _mailcount, ul);
            }
        });
    }
    else if (ul) {
        loadMailInfo(data, _mailblock, _mailcount, ul);
    }
}
function loadMailInfo(result, _mailblock, _mailcount, ul) {
    if (result.Result == false) {
        _mailcount.innerHTML = "<span class=\"fa fa-exclamation-circle\" style=\"font-size:8px;display:inline-block;line-height:14px;text-align:center;color:#ef3838\"></span>";
        return;
    } _mailcount.innerHTML = "";
    if (result.Total == 0) {
        _mailcount.style.display = "none";
    }
    else {
        _mailcount.appendChild(document.createTextNode(result.Total));
    }
    if (!ul) {
        ul = JQ.Flow.createElement("ul", { className: "dropdown_block" });
        var li = JQ.Flow.createElement("li", { className: "dropdown_block_head" });
        ul.appendChild(li);
        li.appendChild(JQ.Flow.createElement("span", { className: "fa fa-envelope" }));
        li.appendChild(JQ.Flow.createElement("span", null, { marginLeft: "5px", fontSize: "14px" }));
        var refresh = JQ.Flow.createElement("span", { className: "fa fa-refresh dropdown_refresh" });
        li.appendChild(refresh);
        refresh.onclick = function () {
            reloadMail();
        };
        li = JQ.Flow.createElement("li", { className: "dropdown_block_body" });
        ul.appendChild(li);
        li = JQ.Flow.createElement("li", { className: "dropdown_block_foot" });
        var div_all = JQ.Flow.createElement("div", { className: "dropdown_block_all" }, null, "打开收件箱查看更多");
        li.appendChild(div_all);
        div_all.onclick = function () {
            window.openMailTab();
        }
        ul.appendChild(li);
    }
    ul.childNodes[0].childNodes[1].appendChild(document.createTextNode(result.Total + "条未读邮件"));
    for (var i in result.Datas) {
        var div = JQ.Flow.createElement("div", { className: "dropdown_block_item" });
        ul.childNodes[1].appendChild(div);
        var e = JQ.Flow.createElement("div", null, { margin: "0px 3px 0px 3px", lineHeight: "20px", float: "left" });
        div.appendChild(e);
        var e_1 = JQ.Flow.createElement("div", { className: "dropdown_block_title", title: result.Datas[i].MailTitle }, null, result.Datas[i].MailTitle);
        e.appendChild(e_1);
        var e_2 = JQ.Flow.createElement("div", { className: "dropdown_block_info" }, null, (result.Datas[i].MailIsBBC == 1 ? "******" : result.Datas[i].CreatorEmpName) + " " + JQ.Flow.parseDateTimeText(result.Datas[i].CreationTime));
        e.appendChild(e_2);
        if (i != result.Datas.length - 1) {
            e.style.borderBottom = "1px solid #c8dded";
        }
        div.onclick = function (mailID, mailTitle) {
            return function () {
                JQ.dialog.dialog({
                    title: mailTitle + " - 邮件查看",
                    url: window.urls.mailDetail + "?id=" + mailID + "&ReceiveFlag=1",
                    width: "800",
                    height: "600",
                    iconCls: "fa fa-plus"
                });
                JQ.Flow.stopBubble();
            }
        }(result.Datas[i].Id, result.Datas[i].MailTitle);
    }
    if (_mailblock.getAttribute("loaded")) {
        $(_mailblock).tooltip("update", $(ul));
        $(ul.childNodes[0].childNodes[2]).removeClass("fa-spin")[0].style.color = "";
    }
    else {
        _mailblock.setAttribute("loaded", "1");
        $(_mailblock).tooltip({
            content: $(ul),
            onUpdate: function (content) {
                content.parent().parent().css({ padding: "0px", borderRadius: "0px", borderColor: "#bcd4e5" });
                content.panel({
                    width: 220,
                    border: false
                });
            },
            onShow: function () {
                var t = $(this);
                t.tooltip("tip").unbind().bind("mouseenter", function () {
                    t.tooltip("show");
                }).bind("mouseleave", function () {
                    t.tooltip("hide");
                });
            }
        });
    }
    _mailblock.removeAttribute("loading");
}

window.openMailTab = function () {
    window.top.addTab("收件箱", window.urls.mailReceive, "fa fa-file");
}
window.openMessageTab = function () {
    window.top.addTab("消息列表", window.urls.messageList, "fa fa-file");
}
window.openSchedulerTab = function () {
    window.addTab("日程表", window.urls.scheculer, "fa fa-calendar");
}

window.reloadMessage = function (data) {
    var _messagesblock = document.getElementById("messages_block");
    if (!_messagesblock || _messagesblock.getAttribute("loading")) {
        return;
    }
    var _messageCount = document.getElementById("mesCount");
    _messageCount.style.display = "inline";
    _messageCount.innerHTML = "<span class=\"fa fa-spinner fa-spin\" style=\"font-size:8px;text-align:center;display:inline-block;line-height:14px\"></span>";
    var ul;
    if (_messagesblock.getAttribute("loaded")) {
        ul = $(_messagesblock).tooltip("options").content[0];
        ul.childNodes[1].innerHTML = "";
        ul.childNodes[0].childNodes[1].innerHTML = "";
        $(ul.childNodes[0].childNodes[2]).addClass("fa-spin")[0].style.color = "#228DAA";
    }
    if (!data) {
        $.ajax({
            type: "POST",
            url: window.urls.getMessages,
            success: function (result) {
                loadMesageInfo(result, _messagesblock, _messageCount, ul);
            }
        });
    }
    else if (ul) {
        loadMesageInfo(data, _messagesblock, _messageCount, ul);
    }
}

function loadMesageInfo(result, _messagesblock, _messageCount, ul) {
    if (result.Result == false) {
        _messageCount.innerHTML = "<span class=\"fa fa-exclamation-circle\" style=\"font-size:8px;display:inline-block;line-height:14px;text-align:center;color:#ef3838\"></span>";
        return;
    }
    _messageCount.innerHTML = "";
    if (result.Total == 0) {
        _messageCount.style.display = "none";
    }
    else {
        _messageCount.appendChild(document.createTextNode(result.Total));
    }
    if (!ul) {
        ul = JQ.Flow.createElement("ul", { className: "dropdown_block" });
        var li = JQ.Flow.createElement("li", { className: "dropdown_block_head" });
        ul.appendChild(li);
        li.appendChild(JQ.Flow.createElement("span", { className: "fa fa-bell" }));
        li.appendChild(JQ.Flow.createElement("span", null, { marginLeft: "5px", fontSize: "14px" }));
        var refresh = JQ.Flow.createElement("span", { className: "fa fa-refresh dropdown_refresh" });
        li.appendChild(refresh);
        refresh.onclick = function () {
            reloadMessage();
        };
        li = JQ.Flow.createElement("li", { className: "dropdown_block_body" });
        ul.appendChild(li);
        li = JQ.Flow.createElement("li", { className: "dropdown_block_foot" });
        var div_all = JQ.Flow.createElement("div", { className: "dropdown_block_all" }, null, "更多 >>");
        li.appendChild(div_all);
        div_all.onclick = function () {
            window.openMessageTab();
        }
        ul.appendChild(li);
    }
    ul.childNodes[0].childNodes[1].appendChild(document.createTextNode(result.Total + "条未读消息"));
    for (var i in result.Datas) {
        var div = JQ.Flow.createElement("div", { className: "dropdown_block_item" });
        ul.childNodes[1].appendChild(div);
        var e = JQ.Flow.createElement("div", null, { margin: "0px 3px 0px 3px", lineHeight: "20px", float: "left", textOverflow: "ellipsis" });
        div.appendChild(e);
        var e_1 = JQ.Flow.createElement("div", { className: "dropdown_block_title", title: result.Datas[i].MessTitle }, null, result.Datas[i].MessTitle);
        e.appendChild(e_1);
        var e_2 = JQ.Flow.createElement("div", { className: "dropdown_block_info" }, null, result.Datas[i].MessEmpName + " " + JQ.Flow.parseDateTimeText(result.Datas[i].MessDate));
        e.appendChild(e_2);
        if (i != result.Datas.length - 1) {
            e.style.borderBottom = "1px solid #c8dded";
        }
        div.onclick = function (messageID, messageTitle) {
            return function () {
                JQ.dialog.dialog({
                    title: messageTitle + " - 消息查看",
                    url: window.urls.messageDetail + "?id=" + messageID,
                    width: "800",
                    height: "600",
                    iconCls: "fa fa-plus"
                });
                JQ.Flow.stopBubble();
            }
        }(result.Datas[i].Id, result.Datas[i].MessTitle);
    }
    if (_messagesblock.getAttribute("loaded")) {
        $(_messagesblock).tooltip("update", $(ul));
        $(ul.childNodes[0].childNodes[2]).removeClass("fa-spin")[0].style.color = "";
    }
    else {
        _messagesblock.setAttribute("loaded", "1");
        $(_messagesblock).tooltip({
            content: $(ul),
            onUpdate: function (content) {
                content.parent().parent().css({ padding: "0px", borderRadius: "0px", borderColor: "#bcd4e5" });
                content.panel({
                    width: 220,
                    border: false
                });
            },
            onShow: function () {
                var t = $(this);
                t.tooltip("tip").unbind().bind("mouseenter", function () {
                    t.tooltip("show");
                }).bind("mouseleave", function () {
                    t.tooltip("hide");
                });
            }
        });
    }
    _messagesblock.removeAttribute("loading");
}


var reloadAvatar = function () {
    var img = document.getElementById("empavatar");
    var src = img.getAttribute("originalsrc");
    if (!src) {
        src = img.getAttribute("src");
        img.setAttribute("originalsrc", src);
    }
    img.setAttribute("src", src + "&time=" + new Date().valueOf());
}

var showClientNotify = function () {

}