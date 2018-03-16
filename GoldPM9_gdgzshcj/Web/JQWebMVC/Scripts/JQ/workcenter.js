(function () {
    document.getElementById("div_todotaskamount").onclick = function () {
        window.openToDoTask();
    };
    document.getElementById("div_todoprojectamount").onclick = function () {
        window.openProjectForm();
    };
    document.getElementById("div_todooaamount").onclick = function () {
        window.openOAForm();
    };
    document.getElementById("div_todoexchrecamount").onclick = function () {
        window.openExchange();
    };
    var datas = $.parseJSON($("#NoticeList_Json").text());
    $("#NoticeList_Json").remove();
    var _noticeblock = document.getElementById("noticeblock");
    for (var i in datas) {
        var div = createElement("div");
        if (i == 0) {
            div.className = "item active item-panel";
        }
        else {
            div.className = "item item-panel";
        }
        _noticeblock.appendChild(div);
        var a = createElement("a", { href: "javascript:void(0)" });
        div.appendChild(a);
        a.appendChild(createElement("p", null, null, datas[i].Title));
        a.onclick = function (id, title) {
            return function () {
                window.top.JQ.dialog.dialog({
                    title: title + " - 公告",
                    url: window.urls.noticeDetail + "?id=" + id,
                    width: 800,
                    height: 600,
                    iconCls: "fa fa-file"
                });
            };
        }(datas[i].Id, datas[i].Title)
        div.appendChild(createElement("span", { className: "clearfix" }));
        var div_1 = createElement("div", { className: " mt-10 small gray-span" });
        div.appendChild(div_1);
        div_1.appendChild(createElement("span", null, null, datas[i].CreatorEmpName));
        div_1.appendChild(createElement("span", { className: "ml-5" }, null, datas[i].CreationTime.replace(/T/g, " ").substr(0, 16)));
        var _a = createElement("a", { href: "javascript:void(0)", className: "gray-span ml-5 gray-hover" });
        div_1.appendChild(_a);
        div.appendChild(createElement("div", { className: "clearfix" }));
        _a.appendChild(createElement("i", { className: "fa fa-link", "aria-hidden": "true" }, { display: "none" }));
        var span_content = createElement("div", { className: " small mt-10 li-span-content" });
        div.appendChild(span_content);
        var contents = datas[i].Content.split('\n');
        for (var j in contents) {
            span_content.appendChild(createElement("p", null, { margin: "0px" }, contents[j]));
        }
    }
    datas = $.parseJSON($("#NewsList_Json").text());
    $("#NewsList_Json").remove();
    var _newsblock = document.getElementById("newsblock");
    for (var i in datas) {
        var li = createElement("li");
        _newsblock.appendChild(li);
        var e = createElement("div", { className: "media" });
        li.appendChild(e);
        e.appendChild(createElement("div", { className: "media-left media-left-" + supplyString((parseInt(i) + 1), 2) }, null, supplyString((parseInt(i) + 1), 2)));
        var e1 = createElement("div", { className: "media-body" });
        e.appendChild(e1);
        var e11 = createElement("h5", { className: "media-heading" });
        e1.appendChild(e11);
        var title = createElement("a", { href: "javascript:void(0)" }, null, datas[i].NewsTitle);
        e11.appendChild(title);
        title.onclick = function (id, title) {
            return function () {
                window.top.JQ.dialog.dialog({
                    title: title + " - 新闻",
                    url: window.urls.newsDetail + "?id=" + id,
                    width: 860,
                    height: 680,
                    iconCls: "fa fa-file"
                });
            };
        }(datas[i].Id, datas[i].NewsTitle)
        e.appendChild(createElement("span", { className: "small" }, null, datas[i].CreatorEmpName + " " + datas[i].NewsDate.replace(/T/g, " ").substr(0, 16)));
    }
    datas = $.parseJSON($("#DiscussList_Json").text());
    $("#DiscussList_Json").remove();
    var _discussblock = document.getElementById("discussblock");
    for (var i in datas) {
        var li = createElement("li");
        li.setAttribute("dataid", datas[i].Id);
        _discussblock.appendChild(li);
        var a = createElement("a", { href: "javascript:void(0)" });
        li.appendChild(a);
        a.onclick = function (id, title) {
            return function () {
                window.top.JQ.dialog.dialogIframe({
                    title: title + " - 讨论",
                    url: window.urls.discussDetail + "?id=" + id + "&view=1",
                    width: 830,
                    height: 680,
                    iconCls: "fa fa-file",
                    onClose: function () {
                        var temp = document.getElementById("discussblock");
                        var temp1 = null;
                        for (var i = 0; i < temp.childNodes.length; i++) {
                            if (temp.childNodes[i].tagName && temp.childNodes[i].getAttribute("dataid") == id) {
                                temp1 = temp.childNodes[i];
                                break;
                            }
                        }
                        if (!temp1) {
                            return;
                        }
                        $.ajax({
                            url: window.urls.discussInfo,
                            type: "post",
                            data: { id: id },
                            success: function (result) {
                                if (result.Result == false) {
                                    return;
                                }
                                temp1.childNodes[0].childNodes[0].innerHTML = "";
                                temp1.childNodes[0].childNodes[0].appendChild(document.createTextNode(result.Title + "(" + result.ReplyCount + "/" + result.ReadCount + ")"));
                            }
                        });
                    }
                });
            };
        }(datas[i].Id, datas[i].TalkTitle)
        a.appendChild(createElement("p", null, null, datas[i].TalkTitle));
        var span = createElement("span", { className: "pull-right small gray" }, null, datas[i].CreationTime.replace(/T/g, " ").substr(0, 10));
        li.appendChild(span);
        if (li.clientWidth - span.offsetWidth > 50) {
            a.childNodes[0].style.width = (li.clientWidth - span.offsetWidth - 10) + "px";
        }
    }

    datas = $.parseJSON($("#TaskGantt_Json").text());
    $("#TaskGantt_Json").remove();
    var _taskGanttblock = document.getElementById("taskGanttblock");
    for (var i in datas) {
        var li = createElement("li");
        _taskGanttblock.appendChild(li);
        var a = createElement("a", { href: "javascript:void(0)" });
        li.appendChild(a);
        a.onclick = function (id, title) {
            return function () {
                window.top.JQ.dialog.dialog({
                    title: title + " - 任务提醒",
                    url: window.urls.taskGanttAvatar + "?id=" + id + "&view=1",
                    width: 800,
                    height: 600,
                    iconCls: "fa fa-file"
                });
            };
        }(datas[i].Id, datas[i].Name)
        a.appendChild(createElement("p", null, null, datas[i].Name));
        var span = createElement("span", { className: "pull-right small gray" }, null, datas[i].DatePlanStart.replace(/T/g, " ").substr(0, 10));
        li.appendChild(span);
        if (li.clientWidth - span.offsetWidth > 50) {
            a.childNodes[0].style.width = (li.clientWidth - span.offsetWidth - 10) + "px";
        }
    }

    datas = $.parseJSON($("#EmpAgents_Json").text());
    $("#EmpAgents_Json").remove();
    var _agentblock = document.getElementById("agentblock");
    if (datas.length == 0) {
        _agentblock.innerHTML = "<div style=\"text-align:center;padding:3px;min-height:110px;width:100%;float:left;cursor:default;color:#888888\"><span class=\"fa fa-info-circle\"></span><span style=\"margin-left:3px;\">无委托授权</span></div>";
    }
    else {
        for (var i in datas) {
            var e = createElement("div", { className: "jq_agent_block" });
            _agentblock.appendChild(e);
            var e_1 = createElement("div", null, { float: "left", width: "100%" });
            e.appendChild(e_1);
            var e_1_1 = createElement("div", null, { float: "left" }, "委托人：" + datas[i].FromEmpName);
            e_1.appendChild(e_1_1);
            e_1_1.appendChild(createElement("span", null, { color: "#888888", marginLeft: "5px" }, datas[i].AgenTypeText));
            var e_1_2 = createElement("div", null, { float: "right" });
            e_1.appendChild(e_1_2);
            var e_1_2_1 = createElement("span", { className: "fa fa-random", title: "切换" }, { lineHeight: "20px", fontSize: "16px" });
            e_1_2.appendChild(e_1_2_1);
            e_1_2_1.onclick = function (agentID, fromEmpName) {
                return function () {
                    window.top.$.messager.confirm("提示", "确定要切换以【" + fromEmpName + "】的委托授权身份登录吗？", function (result) {
                        if (!result) {
                            return;
                        }
                        //使用数据
                        $.ajax({
                            url: window.urls.switchAgent,
                            type: "POST",
                            data: { empAgenID: agentID },
                            success: function (result) {
                                if (result.Result == false) {
                                    alert(result.Message);
                                    return;
                                }
                                window.top.location.href = result.Url;
                            }
                        });
                    });
                };
            }(datas[i].BaseEmpAgenID, datas[i].FromEmpName);
            e.appendChild(createElement("div", null, { float: "left", width: "100%" }, "期限：" + datas[i].DateLower + " 至 " + datas[i].DateUpper));
            if (datas[i].AgenType == 0 || datas[i].AgenType == 1) {
                e.appendChild(createElement("div", null, { float: "left", width: "100%" }, "菜单：" + datas[i].Menus));
            }
            if (datas[i].AgenType == 0 || datas[i].AgenType == 2) {
                e.appendChild(createElement("div", null, { float: "left", width: "100%" }, "流程：" + datas[i].Flows));
            }
        }
    }
}());
window.openToDoTask = function () {
    window.top.addTab("待办项目生产任务", window.urls.todoTask + "?time=" + (new Date()).getTime(), "fa fa-tasks");
};
window.openProjectForm = function () {
    window.top.addTab("待办项目表单任务", window.urls.todoList + "?modular=1", "fa fa-tasks");
};
window.openOAForm = function () {
    window.top.addTab("待办办公表单任务", window.urls.todoList + "?modular=2", "fa fa-tasks");
};
window.openExchange = function () {
    window.top.addTab("待办项目收资任务", window.urls.todoExchRecAmount + "?modular=3", "fa fa-tasks");
};
window.refreshToDoForm = function () {
    if (!window) {
        return;
    }
    document.getElementById("todoprojectamount").innerHTML = "<span class=\"fa fa-lg fa-spin fa-spinner\" style=\"display:inline-block;line-height:42px;\"></span>";
    document.getElementById("todooaamount").innerHTML = "<span class=\"fa fa-lg fa-spin fa-spinner\" style=\"display:inline-block;line-height:42px;\"></span>";
    document.getElementById("todoexchrecamount").innerHTML = "<span class=\"fa fa-lg fa-spin fa-spinner\" style=\"display:inline-block;line-height:42px;\"></span>";
    $.ajax({
        url: window.urls.todoListAmount,
        type: "post",
        success: function (result) {
            if (result.Result == false) {
                document.getElementById("todooaamount").innerHTML = "<span class=\"fa fa-lg fa-exclamation-circle\" style=\"display:inline-block;line-height:42px;color:#FFFFFF\"></span>";
                document.getElementById("todoexchrecamount").innerHTML = "<span class=\"fa fa-lg fa-exclamation-circle\" style=\"display:inline-block;line-height:42px;color:#FFFFFF\"></span>";
                document.getElementById("todoprojectamount").innerHTML = "<span class=\"fa fa-lg fa-exclamation-circle\" style=\"display:inline-block;line-height:42px;color:#FFFFFF\"></span>";
                return;
            }
            document.getElementById("todooaamount").innerHTML = "";
            document.getElementById("todooaamount").appendChild(document.createTextNode(result.OAAmount))
            document.getElementById("todoprojectamount").innerHTML = "";
            document.getElementById("todoprojectamount").appendChild(document.createTextNode(result.ProjectAmount));
            document.getElementById("todoexchrecamount").innerHTML = "";
            document.getElementById("todoexchrecamount").appendChild(document.createTextNode(result.ExchRec));
        }
    });

    document.getElementById("todotaskamount").innerHTML = "<span class=\"fa fa-lg fa-spin fa-spinner\" style=\"display:inline-block;line-height:42px;\"></span>";
    $.ajax({
        url: window.urls.todoTaskAmount,
        type: "post", dataType: "json",
        success: function (result) {
            if (result.Result == false) {
                document.getElementById("todotaskamount").innerHTML = "<span class=\"fa fa-lg fa-exclamation-circle\" style=\"display:inline-block;line-height:42px;color:#ef3838\"></span>";
                return;
            }
            document.getElementById("todotaskamount").innerHTML = "";
            document.getElementById("todotaskamount").appendChild(document.createTextNode(result.Amount));
        }
    });

}

function changeTodoTaskAmount(amount) {
    var _e = document.getElementById("todotaskamount");
    //var number = parseNumber(_e.textContent);
    _e.innerHTML = "";
    _e.appendChild(document.createTextNode(amount));
}

function changeToDoOAAmount(amount) {
    var _e = document.getElementById("todooaamount");
    var number = parseNumber(_e.textContent);
    _e.innerHTML = "";
    _e.appendChild(document.createTextNode(number + amount));
}

function changeToDoProjectAmount(amount) {
    var _e = document.getElementById("todoprojectamount");
    var number = parseNumber(_e.textContent);
    _e.innerHTML = "";
    _e.appendChild(document.createTextNode(number + amount));
}

function changeToDoExchRecAmount(amount) {
    var _e = document.getElementById("todoexchrecamount");
    var number = parseNumber(_e.textContent);
    _e.innerHTML = "";
    _e.appendChild(document.createTextNode(number + amount));
}

function parseNumber(str) {
    var result = parseFloat(str);
    if (isNaN(result)) {
        return 0;
    }
    return result;
}

function showShortcutSetting() {
    window.top.JQ.dialog.dialog({
        title: "快捷方式管理",
        url: window.urls.shortcutSetting,
        width: 450,
        height: 500,
        iconCls: "fa fa-share-square"
    });
}

window.reloadShortcut = function () {
    var _divShortcuts = document.getElementById("divShortcuts");
    _divShortcuts.innerHTML = "<div style=\"width:100%;text-align:center;\"><span class=\"fa fa-spinner fa-spin\" style=\"display:inline-block;font-size:20px;line-height:60px;color:#ebab22\"></span></div>";
    $.ajax({
        type: "post",
        url: window.urls.getShortcuts,
        success: function (data) {
            _divShortcuts.innerHTML = "";
            var indicators = createElement("ol", { className: "carousel-indicators" });
            _divShortcuts.appendChild(indicators);
            var inner = createElement("div", { className: "carousel-inner" });
            _divShortcuts.appendChild(inner);
            var ul = null;
            var div = null;
            for (var i in data) {
                if (i % 12 == 0) {
                    var oli = createElement("li", { "data-target": "#divShortcuts", "data-slide-to": (i / 12).toString() });
                    indicators.appendChild(oli);
                    div = createElement("div", { className: "item" });
                    inner.appendChild(div);
                    if (i / 12 == 0) {
                        oli.className = "active";
                        div.className += " active";
                    }
                    ul = document.createElement("ul");
                    ul.className = "clearfix";
                    div.appendChild(ul);
                }
                var li = document.createElement("li", null, { height: "69px" });
                ul.appendChild(li);
                li.setAttribute("title", data[i].MenuName);
                var span = document.createElement("span");
                span.className = (data[i].MenuImageUrl || "fa fa-file") + " jq_shortcut_icon";
                li.appendChild(span);
                var p = document.createElement("p");
                p.className = "jq_shortcut_p";
                p.appendChild(document.createTextNode(data[i].MenuName));
                li.appendChild(p);
                li.onclick = function (title, url, icon) {
                    return function () {
                        window.top.addTab(title, window.urls.sitePath + url, icon);
                    }
                }(data[i].MenuName, data[i].MenuCommand, (data[i].MenuImageUrl || "fa fa-file"));
            }
            // 若每页不满12个设置边框高度
            if (ul) {
                while (ul.childNodes.length < 12) {
                    var li = document.createElement("li");
                    li.style.height = "69px";
                    ul.appendChild(li);
                }
            }
        }
    });
}

window.reloadFavouriteProjects = function () {

    var _divFavouriteProjects = document.getElementById("divFavouriteProjects");
    _divFavouriteProjects.removeAttribute("loaded");
    _divFavouriteProjects.innerHTML = "<div style=\"width:100%;height:100%;text-align:center;position:relative\"><span class=\"fa fa-spinner fa-spin\" style=\"display:inline-block;font-size:28px;margin-top:100px;color:#ebab22\"></span></div>";
    $.ajax({
        url: window.urls.getFavouriteProjects,
        type: "POST",
        success: function (datas) {
            _divFavouriteProjects.innerHTML = "";
            var width = _divFavouriteProjects.clientWidth;
            for (var i in datas) {
                var div = createElement("div", { className: "col-md-12" });
                _divFavouriteProjects.appendChild(div);
                var div1 = createElement("div", { className: "fav_project" });
                div.appendChild(div1);
                var div1_1 = createElement("div", { className: "fav_project_left", title: datas[i].ProjName }, { color: "#848484", fontSize: "12px", padding: "3px 0" });
                div1.appendChild(div1_1);
                var a = createElement("span", { className: "fav_project_left1" }, null, datas[i].ProjName);
                div1_1.appendChild(a);
                a.onclick = function (projectID, title) {
                    return function () {
                        window.top.addTab(title, window.urls.projectCenter + "?projectID=" + projectID);
                    };
                }(datas[i].Id, datas[i].ProjNumber + " - 项目中心");
                var div1_2 = createElement("div", { className: "fav_project_right" });
                div1.appendChild(div1_2);
                var span = createElement("div", null, { width: "70px", height: "32px", textAlign: "center", float: "right", color: "#848484", marginLeft: "20px" }, getDateTime(datas[i].DatePlanStart));
                div1_2.appendChild(span);
                span = createElement("span", { className: "label label-info" }, { height: "18px", marginTop: "10px", textAlign: "center", float: "left", overflow: "hidden" }, datas[i].ProjectTypeName);
                div1_2.appendChild(span);
                span = createElement("span", { className: "label label-warning" }, { height: "18px", marginTop: "10px", textAlign: "center", float: "left" }, datas[i].ProjectVoltName);
                div1_2.appendChild(span);
                if (div1.clientWidth - div1_2.offsetWidth > 50) {
                    div1_1.style.width = (div1.clientWidth - div1_2.offsetWidth - 10) + "px";
                }
            }
            _divFavouriteProjects.setAttribute("loaded", "1");
        }
    })
};

function resizeFavouriteProjects() {
    var _divFavouriteProjects = document.getElementById("divFavouriteProjects");
    if (!_divFavouriteProjects.getAttribute("loaded")) {
        return;
    }
    //处理文件
    for (var i in _divFavouriteProjects.childNodes) {
        if (!_divFavouriteProjects.childNodes[i].tagName) {
            continue;
        }
        if (divFavouriteProjects.childNodes[i].childNodes[0].clientWidth - divFavouriteProjects.childNodes[i].childNodes[0].childNodes[1].offsetWidth > 100) {
            divFavouriteProjects.childNodes[i].childNodes[0].childNodes[0].style.width = (divFavouriteProjects.childNodes[i].childNodes[0].clientWidth - divFavouriteProjects.childNodes[i].childNodes[0].childNodes[1].offsetWidth - 10) + "px";
        }
    }
}

function resizeDiscuss() {
    var _discussblock = document.getElementById("discussblock");
    for (var i = 0; i < _discussblock.childNodes.length; i++) {
        if (_discussblock.childNodes[i].clientWidth - _discussblock.childNodes[i].childNodes[1].offsetWidth > 50) {
            _discussblock.childNodes[i].childNodes[0].childNodes[0].style.width = (_discussblock.childNodes[i].clientWidth - _discussblock.childNodes[i].childNodes[1].offsetWidth - 10) + "px";
        }
    }
}

function createElement(tagName, attributes, styles, text) {
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

function getDateTime(value) {
    try {
        var date = new Date(parseInt(/^\/Date\(([0-9]*)\)\/$/.exec(value)[1]));
        return date.getFullYear().toString() + "-" + supplyString((date.getMonth() + 1)) + "-" + supplyString(date.getDate());
    }
    catch (err) { return ""; }
}

function supplyString(content, digit) {
    if (!digit) {
        digit = 2;
    }
    content = content.toString();
    while (content.length < digit) {
        content = "0" + content;
    }
    return content;
}

function changeAvatar() {
    window.top.JQ.dialog.dialog({
        title: "更换头像",
        url: window.urls.changeAvatar,
        width: 700,
        height: 490,
        iconCls: "fa fa-user"
    });
}


var reloadAvatar = function () {
    var img = document.getElementById("empavatar");
    var src = img.getAttribute("originalsrc");
    if (!src) {
        src = img.getAttribute("src");
        img.setAttribute("originalsrc", src);
    }
    img.setAttribute("src", src + "&time=" + new Date().valueOf());
    window.top.reloadAvatar();
}


var afterChangeProfile = function (data) {
    var _empsignbox = document.getElementById("empsignbox");
    _empsignbox.innerHTML = "";
    var notes = data.note.split("\n");
    for (var i = 0; i < notes.length; i++) {
        var p = document.createElement("p");
        p.style.marginBottom = "0px";
        p.appendChild(document.createTextNode(notes[i]));
        _empsignbox.appendChild(p);
    }
}

window.refreshtaskGantt = function () {
    $.ajax({
        url: window.urls.taskGanttCount,
        type: "post",
        dataType: "json",
        success: function (result) {
            if (result.length.length == 0) {
                return;
            }
            var _taskGanttblock = document.getElementById("taskGanttblock");
            _taskGanttblock.innerHTML = "";
            for (var i in result) {
                var li = createElement("li");
                _taskGanttblock.appendChild(li);
                var a = createElement("a", { href: "javascript:void(0)" });
                li.appendChild(a);
                a.onclick = function (id, title) {
                    return function () {
                        window.top.JQ.dialog.dialog({
                            title: title + " - 任务提醒",
                            url: window.urls.taskGanttAvatar + "?id=" + id + "&view=1",
                            width: 800,
                            height: 600,
                            iconCls: "fa fa-file"
                        });
                    };
                }(result[i].Id, result[i].Name)
                a.appendChild(createElement("p", null, null, result[i].Name));
                var span = createElement("span", { className: "pull-right small gray" }, null, result[i].DatePlanStart.replace(/T/g, " ").substr(0, 10));
                li.appendChild(span);
                if (li.clientWidth - span.offsetWidth > 50) {
                    a.childNodes[0].style.width = (li.clientWidth - span.offsetWidth - 10) + "px";
                }
            }
        }
    });
}