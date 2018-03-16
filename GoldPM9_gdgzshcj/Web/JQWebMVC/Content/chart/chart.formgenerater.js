if (typeof (YChart) == "undefined") {
    YChart = {};
}
YChart.FormGenerater = function (options) {
    if (!options.container) {
        return;
    }
    this.container = options.container;
    if (options.html) {
        options.container.innerHTML = options.html;
    }
    var _div = YChart.getFirstChild(options.container);
    if (_div) {
        _div.style.marginBottom = "10px";
    }
    this.currentDate = options.currentDate;
    this.currentUserName = options.currentUserName;
    this.currentDepartmentName = options.currentDepartmentName;
    this.properties = options.properties;
    this.hiddens = options.hiddens;
    this.steps = options.steps;
    this.allowEdit = options.allowEdit;
    if (options.dataValue) {
        this.dataValue = options.dataValue;
    }
    this.urls = options.urls;
    this.process = options.process;
    this.formID = options.formID;
    if (this.hiddens) {
        for (var i = 0; i < this.hiddens.length; i++) {
            var value = this.hiddens[i].value;
            if (this.dataValue) {
                var node = YChart.selectSingleNode(this.dataValue, "Item[@name=\"" + this.hiddens[i].name + "\"]");
                if (node) {
                    value = node.getAttribute("value");
                }
            }
            this.container.appendChild(YChart.createElement("input", { type: "hidden", id: this.hiddens[i].name, name: this.hiddens[i].name, value: value, ycelement: "hidden" }))
        }
    }
    this.formTemplateID = options.formTemplateID;
    this.callbacks = {};
    var _this = this;
    //窗口消息事件处理
    YChart.attachEvent(window, "message", function (ev) {
        if (!ev.data.command) {
            return;
        }
        switch (ev.data.command) {
            case "closeDialog":
                YChart.Dialog.close(ev.data.dialogID);
                break;
            case "execMethod":
                if (!ev.data.methodName || !_this.callbacks[ev.data.methodName]) {
                    return;
                }
                if (YChart.isArray(ev.data.arguments) && ev.data.isMulti == true) {
                    _this.callbacks[ev.data.methodName].apply(null, ev.data.arguments);
                }
                else {
                    _this.callbacks[ev.data.methodName](ev.data.arguments);
                }
                if (ev.data.isCloseDialog == true && ev.data.dialogID) {
                    YChart.Dialog.close(ev.data.dialogID);
                }
                break;
        }
    });
    if (options.toolbar) {
        //初始化工具栏按钮
        if ((this.process && this.process.allowEdit == "1") || (!this.process && this.allowEdit == "1")) {
            //允许编辑 添加保存按钮
            this.addToolbarButton(options.toolbar, "fa-save", "保存", function () {
                _this.save();
            });
        }
        if (this.process) {
            //包含流程、并且允许编辑，显示处理界面
            if ((this.process && this.process.allowEdit == "1") || (!this.process && this.allowEdit == "1")) {
                this.addToolbarButton(options.toolbar, "fa-flag", "审批处理", function () {
                    if (!_this.validate()) {
                        return;
                    }
                    var processOpt = _this.urls.process;
                    processOpt.title = "审批处理（" + _this.process.Step.displayName + "）";
                    processOpt.api = {
                        getData: function () {
                            //处理数据
                            return _this.process;
                        },
                        getVariables: function () {
                            //获取出变量
                            var xml = YChart.loadXml("<Root></Root>");
                            for (var i = 0; i < _this.properties.length; i++) {
                                if (_this.properties[i].inlineTableName || !_this.properties[i].flow || _this.properties[i].type == "table") {
                                    continue;
                                }
                                var element = document.getElementById(_this.properties[i].elementName);
                                var control = _this.findControl(element.getAttribute("ycelement"));
                                if (!control || typeof (control.getValue) != "function" || !element) {
                                    return;
                                }
                                var value = control.getValue(element);
                                if (typeof (value) == "object") {
                                    if (_this.properties[i].flow.textVariableName) {
                                        //获取出文本
                                        YChart.FormGenerater.appendSaveItem(xml.documentElement, element.getAttribute("ycelement"), _this.properties[i].flow.textVariableName, value.text);
                                    }
                                    if (_this.properties[i].flow.valueVariableName) {
                                        //获取出值
                                        YChart.FormGenerater.appendSaveItem(xml.documentElement, element.getAttribute("ycelement"), _this.properties[i].flow.valueVariableName, value.value);
                                    }
                                }
                                else if (_this.properties[i].flow.variableName) {
                                    //直接获取出值
                                    YChart.FormGenerater.appendSaveItem(xml.documentElement, element.getAttribute("ycelement"), _this.properties[i].flow.variableName, value);
                                }
                            }
                            if (_this.hiddens) {
                                for (var i = 0; i < _this.hiddens.length; i++) {
                                    if (_this.hiddens[i].variableName) {
                                        var element = document.getElementById(_this.hiddens[i].name);
                                        if (element) {
                                            YChart.FormGenerater.appendSaveItem(xml.documentElement, element.getAttribute("ycelement"), _this.hiddens[i].variableName, YChart.FormGenerater.Hidden.getValue(element));
                                        }
                                    }
                                }
                            }
                            return xml.documentElement.outerHTML;
                        },
                        getFormSaveOpions: function () {
                            return { formID: _this.formID, formTemplateID: _this.formTemplateID, formData: _this.getSaveData() };
                        },
                        afterSubmit: function () {
                            if (window.frameElement.api && window.frameElement.api.refreshGrid) {
                                window.frameElement.api.refreshGrid();
                            }
                            frameElement.closeDialog();
                        }
                    };
                    new YChart.Dialog(processOpt);
                });
            }
            this.addToolbarButton(options.toolbar, "fa-th-list", "查看进度", function () {
                var progressOpt = _this.urls.progress;
                if (_this.process.isTemplate == "1") {
                    progressOpt.url = progressOpt.urlPrefix + "?templateID=" + _this.process.id;
                }
                else {
                    progressOpt.url = progressOpt.urlPrefix + "?id=" + _this.process.id;
                }
                progressOpt.title = _this.process.name;
                new YChart.Dialog(progressOpt);
            });
        }
        if (frameElement.closeDialog) {
            this.addToolbarButton(options.toolbar, "fa-close", "关闭", function () {
                frameElement.closeDialog();
            });
        }
        //添加打印按钮
        this.addToolbarButton(options.toolbar, "fa-print", "打印", function () {
            _this.print();
        });
        //添加节点名称
        if (this.process && this.process.Step) {
            options.toolbar.appendChild(YChart.createElement("span", null, { lineHeight: "36px", marginLeft: "5px", cursor: "default" }, "当前步骤：" + this.process.Step.displayName));
        }
    }
    this.referenceField = options.referenceField;
    this.referenceID = options.referenceID;
    this.initControls();
    this.initUI(options.container);
}

//获取属性
YChart.FormGenerater.prototype.getProperty = function (elementName) {
    for (var i = 0; i < this.properties.length; i++) {
        if (this.properties[i].elementName == elementName) {
            return this.properties[i];
        }
    }
}

//初始化UI
YChart.FormGenerater.prototype.initUI = function (container, parentElement, rowData) {
    var _this = this;
    YChart.recuriseChildNodes(container, function (element) {
        if (element.nodeType != 1 || !element.getAttribute("ycelement")) {
            return;
        }
        var control = _this.findControl(element.getAttribute("ycelement"));
        if (control) {
            control.init.call(_this, element, _this.getProperty(element.getAttribute("id")), parentElement, rowData);
        }
    });
}

//验证
YChart.FormGenerater.prototype.validate = function () {
    var result = true;
    YChart.recuriseChildNodes(this.container, function (element) {
        if (element.nodeType != 1 || !element.getAttribute("ycelement")) {
            return;
        }
        //遍历验证
        switch (element.getAttribute("ycelement")) {
            case "textbox":
            case "datebox":
            case "numberbox":
            case "textarea":
                if (!YChart.fireEvent(element, "validate").returnValue) {
                    result = false;
                    return false;
                }
            case "checkbox":
            case "radio":
                if (!YChart.fireEvent(element, "validate").returnValue) {
                    result = false;
                    return false;
                }
                return true;
        }
    });
    return result;
}

//获取保存的数据
YChart.FormGenerater.prototype.getSaveData = function () {
    var xmlDocument = YChart.loadXml("<Root></Root>");
    //遍历获取数据
    this.recuriseGetSaveData(this.container, xmlDocument.documentElement);
    if (this.referenceField && this.referenceID) {
        YChart.FormGenerater.appendSaveItem(xmlDocument.documentElement, "reference", this.referenceField, this.referenceID);
    }
    //返回出xml
    return xmlDocument.documentElement.outerHTML;
}

//遍历获取
//parent:父控件
//xmlElement:值的集合（xml格式）
//targetName:目标的属性名称
YChart.FormGenerater.prototype.recuriseGetSaveData = function (parent, xmlElement, targetName) {
    var _this = this;
    YChart.recuriseChildNodes(parent, function (element) {
        if (element.nodeType != 1 || !element.getAttribute("ycelement") || element.getAttribute("ycprocessdisabled")) {
            return;
        }
        var ycElement = element.getAttribute("ycelement");
        if (ycElement == "table") {
            //table需要遍历子行
            var xItem = xmlElement.ownerDocument.createElement("Items");
            xItem.setAttribute("type", "table");
            xItem.setAttribute("name", element.getAttribute(targetName || "name"));
            xmlElement.appendChild(xItem);
            for (var i = 2; i < element.childNodes[2].rows.length; i++) {
                var xRowItem = xmlElement.ownerDocument.createElement("Row");
                xItem.appendChild(xRowItem);
                xRowItem.setAttribute("dataid", element.childNodes[2].rows[i].getAttribute("dataid") || -1);
                _this.recuriseGetSaveData(element.childNodes[2].rows[i], xRowItem, "yctargetname");
            }
            //返回true表示断开连接
            return true;
        }
        else {
            var control = _this.findControl(ycElement);
            if (!control || !control.getValue) {
                return;
            }
            var data = control.getValue(element);
            if (typeof (data) == "object") {
                YChart.FormGenerater.appendSaveItem(xmlElement, ycElement, element.getAttribute(targetName || "name"), data.value, data.text);
            }
            else {
                YChart.FormGenerater.appendSaveItem(xmlElement, ycElement, element.getAttribute(targetName || "name"), data);
            }
        }
    });
}

//将值（或文本值）添加到对应的数据集合中
YChart.FormGenerater.appendSaveItem = function (xmlElement, itemType, itemName, itemValue, itemText) {
    var xItem = xmlElement.ownerDocument.createElement("Item");
    xItem.setAttribute("type", itemType);
    xItem.setAttribute("name", itemName);
    if (itemText) {
        xItem.setAttribute("text", itemText);
    }
    xItem.textContent = itemValue;
    xmlElement.appendChild(xItem);
}

//添加工具栏按钮
YChart.FormGenerater.prototype.addToolbarButton = function (toolbar, icon, text, onClick) {
    var div = document.createElement("div");
    div.className = "headtoolbarbtn";
    toolbar.appendChild(div);
    var span = document.createElement("span");
    span.className = "fa" + (icon ? (" " + icon) : "");
    div.appendChild(span);
    var span = document.createElement("span");
    span.appendChild(document.createTextNode(text));
    span.style.marginLeft = "4px";
    div.appendChild(span);
    onClick && YChart.attachEvent(div, "click", onClick);
}

//是否允许编辑
YChart.FormGenerater.prototype.isAllowEdit = function (flow) {
    if ((this.process && this.process.allowEdit == "0") || (!this.process && this.allowEdit == "0")) {
        return false;
    }
    var isEditable = false;
    if (this.process) {
        //如果包含流程
        if (this.process.isApproved == "1" && flow) {
            //已经审批完成
            if (this.process.isPassed == "1" && flow.isEditableWhenPassed == "1") {
                //通过的处理
                isEditable = true;
            }
            else if (this.process.isPassed == "0" && flow.isEditableWhenPassed == "1") {
                //不通过的处理
                isEditable = true;
            }
        }
        else if (this.process.Step) {
            //获取出当前步骤
            for (var i = 0; i < flow.editableSteps.length; i++) {
                if (flow.editableSteps[i] == this.process.Step.name) {
                    //如果该控件在可编辑的控件范围内
                    isEditable = true;
                    break;
                }
            }
        }
    }
    else {
        if (this.allowEdit == "1") {
            //依据当前的环境编辑为准
            isEditable = true;
        }
    }
    return isEditable;
}

//保存
YChart.FormGenerater.prototype.save = function () {
    //保存数据前先做验证
    if (!this.validate()) {
        return;
    }
    YChart.ajax({
        url: this.urls.save,
        type: "POST",
        data: { data: this.getSaveData() },
        success: function (result) {
            //处理数据
            if (result.Result == false) {
                YChart.Dialog.alert("保存错误", result.Message);
            }
            else {
                if (window.frameElement.api && window.frameElement.api.refreshGrid) {
                    //刷新前页的表格
                    window.frameElement.api.refreshGrid();
                }
                if (window.frameElement.treeApi && window.frameElement.treeApi.refresh) {
                    window.frameElement.treeApi.refresh();
                }
                frameElement.closeDialog && frameElement.closeDialog();
            }
        }
    });
}

//打印
YChart.FormGenerater.prototype.print = function () {
    var selects = document.getElementsByTagName("select");
    for (var i = 0; i < selects.length; i++) {
        for (var j = 0; j < selects[i].options.length; j++) {
            if (selects[i].options[j].selected) {
                selects[i].options[j].setAttribute("selected", "selected");
            }
        }
    }
    var inputs = document.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        switch (inputs[i].getAttribute("type")) {
            case "checkbox":
            case "radio":
                if (inputs[i].checked) {
                    inputs[i].setAttribute("checked", "checked");
                }
                break;
        }
    }
    var div = document.createElement("div");
    div.style.display = "none";
    document.body.appendChild(div);
    var node = document.getElementById("divcontent").cloneNode(true);
    for (var i = 0; i < node.childNodes.length; i++) {
        if (node.childNodes[i].nodeType == 1 && node.childNodes[i].tagName == "DIV") {
            node.childNodes[i].style.border = "none";
        }
        div.appendChild(node.childNodes[i]);
    }
    var _this = this;
    YChart.recuriseChildNodes(div, function (element) {
        if (element.nodeType != 1 || !element.getAttribute("ycelement")) {
            return;
        }
        var control = _this.findControl(element.getAttribute("ycelement"));
        if (control && typeof (control.disable) == "function") {
            control.disable(element, true);
        }
    });
    var iframe = document.createElement("iframe");
    iframe.style.display = "none";
    document.body.appendChild(iframe);
    iframe.setAttribute("src", this.urls.print);
    iframe.onload = function () {
        this.contentWindow.afterPrint = function () {
            YChart.remove(iframe);
        }
        this.contentWindow.setContent(div.innerHTML);
        YChart.remove(div);
    }
}

//初始化控件集
YChart.FormGenerater.prototype.initControls = function () {
    this.controls = [];
    var _this = this;
    this.registerControl("drop_td", {
        init: function (element) {
            element.removeAttribute("contenteditable");
        }
    });
    this.registerControl("label", YChart.FormGenerater.Label);
    this.registerControl("textbox", YChart.FormGenerater.TextBox);
    this.registerControl("textarea", YChart.FormGenerater.TextArea);
    this.registerControl("radio", YChart.FormGenerater.Radio);
    this.registerControl("checkbox", YChart.FormGenerater.CheckBox);
    this.registerControl("select", YChart.FormGenerater.Select);
    this.registerControl("datebox", YChart.FormGenerater.DateBox);
    this.registerControl("numberbox", YChart.FormGenerater.NumberBox);
    this.registerControl("table", YChart.FormGenerater.Table);
    this.registerControl("panel", YChart.FormGenerater.Panel);
    this.registerControl("map", YChart.FormGenerater.Map);
    this.registerControl("signbox", YChart.FormGenerater.SignBox);
    this.registerControl("hidden", YChart.FormGenerater.Hidden);
}

//注册控件
YChart.FormGenerater.prototype.registerControl = function (controlName, events) {
    for (var i = 0; i < this.controls.length; i++) {
        if (controlName == this.controls[i].name) {
            return;
        }
    }
    this.controls.push({ name: controlName, events: events });
}

//设置步骤签名信息
YChart.FormGenerater.prototype.setStepSign = function (element, flow, setValue) {
    if (!flow) {
        return;
    }
    for (var i = 0; i < this.steps.length; i++) {
        if (this.steps[i].getAttribute("name") != flow.displayStep) {
            continue;
        }
        if (this.steps[i].getAttribute("symbol") == "activity") {
            var actor = YChart.selectSingleNode(this.steps[i], "Actor");
            if (!actor) {
                break;
            }
            if (flow.isShowProcessUser == "1") {
                setValue(element, actor.getAttribute("name"));
            }
            else if (flow.isShowProcessTime == "1") {
                if (actor.getAttribute("time")) {
                    setValue(element, YChart.getDateText(YChart.parseDate(actor.getAttribute("time")), "yyyy-MM-dd HH:mm"));
                }
            }
            else if (flow.isShowProcessNote == "1") {
                if (actor.textContent == "") {
                    element.setAttribute("placeholder", "未填写意见");
                }
                else {
                    setValue(element, actor.textContent);
                }
            }
        }
        break;
    }
}

//获取出控件
YChart.FormGenerater.prototype.findControl = function (controlName) {
    for (var i = 0; i < this.controls.length; i++) {
        if (controlName == this.controls[i].name) {
            return this.controls[i].events;
        }
    }
}

//添加表格行
YChart.FormGenerater.prototype.addTableRow = function (element, rowData) {
    //添加新行
    var _table = element.parentNode.nextSibling;
    var _tr = _table.rows[1].cloneNode(true);
    _tr.style.display = "";
    _table.appendChild(_tr);
    if (rowData) {
        _tr.setAttribute("dataid", rowData.getAttribute("id"));
    }
    this.initUI(_tr, _tr, rowData);
    YChart.recuriseChildNodes(_tr, function (_element) {
        if (_element.nodeType != 1) {
            return;
        }
        if (_element.getAttribute("ycelement")) {
            _element.setAttribute("yctargetid", _element.getAttribute("id"));
            if (_element.tagName != "INPUT" || _element.getAttribute("type") != "radio") {
                _element.setAttribute("yctargetname", _element.getAttribute("name"));
            }
        }
        _element.removeAttribute("id");
        if (_element.tagName != "INPUT" || _element.getAttribute("type") != "radio") {
            _element.removeAttribute("name");
        }
    });
}

//删除表格行
YChart.FormGenerater.prototype.deleteTableRow = function (element) {
    //获取出选中的行
    var _table = element.parentNode.nextSibling;
    var _trs = [];
    for (var i = 2; i < _table.rows.length; i++) {
        if (_table.rows[i].cells[0].childNodes[0].checked) {
            _trs.push(_table.rows[i]);
        }
    }
    if (_trs.length == 0) {
        return;
    }
    YChart.Dialog.confirm("提示", "确定要删除选中的行吗？", function (result) {
        if (!result) {
            return;
        }
        for (var i = 0; i < _trs.length; i++) {
            _table.removeChild(_trs[i]);
        }
    });
}

//上移表格行（未实现）
YChart.FormGenerater.moveUpTableRow = function (element) {

}

//下移表格行（未实现）
YChart.FormGenerater.moveDownTableRow = function (element) {

}

//禁用textbox
YChart.FormGenerater.disableTextBox = function (element, isPrint) {
    if (element.getAttribute("ycprocessdisabled")) {
        return;
    }
    element.setAttribute("ycprocessdisabled", "1");
    var margin = element.style.margin;
    if (margin) {
        YChart.insertAfter(YChart.createElement("span", null, { margin: margin }, element.value), element);
    }
    else {
        YChart.insertAfter(document.createTextNode(element.value), element);
    }
    if (isPrint) {
        YChart.remove(element);
    }
    else {
        element.style.display = "none";
    }
}

//禁用textarea
YChart.FormGenerater.disableTextArea = function (element, isPrint) {
    if (element.getAttribute("ycprocessdisabled")) {
        return;
    }
    element.setAttribute("ycprocessdisabled", "1");
    var marginLeft = element.style.marginLeft || "0px";
    var marginRight = element.style.marginRight || "0px";
    var cs = element.value.split('\n');
    var temp = element;
    for (var i = 0; i < cs.length; i++) {
        var _p = YChart.createElement("p", { className: "p_normaltext" }, { marginLeft: marginLeft, marginRight: marginRight }, cs[i]);
        if (i == 0 && element.style.marginTop) {
            _p.style.marginTop = element.style.marginTop;
        }
        if (i == cs.length - 1 && element.style.marginBottom) {
            _p.style.marginBottom = element.style.marginBottom;
        }
        YChart.insertAfter(_p, temp);
        temp = _p;
    }
    if (isPrint) {
        YChart.remove(element);
    }
    else {
        element.style.display = "none";
    }
}

//label控件
YChart.FormGenerater.Label = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                //取上次保存的值
                YChart.FormGenerater.Label.setValue(element, node.getAttribute("value"));
            }
        }
        else {
            var text = "";
            if (property.isShowDepartmentName == "1") {
                if (text != "") {
                    text += " ";
                }
                text += this.currentDepartmentName;
            }
            if (property.isShowUserName == "1") {
                if (text != "") {
                    text += " ";
                }
                text += this.currentUserName;
            }
            if (property.isShowCurrentDate == "1") {
                if (text != "") {
                    text += " ";
                }
                text += YChart.getDateText(this.currentDate, "yyyy-MM-dd");
            }
            if (text) {
                //取系统值
                YChart.FormGenerater.Label.setValue(element, text);
            }
            else {
                //取默认值
                YChart.FormGenerater.Label.setValue(element, property.text || "");
            }
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("element")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
    },
    setValue: function (element, text) {
        element.textContent = text;
    },
    getValue: function (element) {
        return element.textContent;
    }
};

//textbox控件
YChart.FormGenerater.TextBox = {
    isValue: true,
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        if (property.placeHolder) {
            element.setAttribute("placeholder", property.placeHolder);
        }
        if (property.isReadonly == "1") {
            element.setAttribute("readonly", "readonly");
        }
        else {
            element.removeAttribute("readonly");
        }
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                YChart.FormGenerater.TextBox.setValue(element, node.getAttribute("value"));
            }
        }
        else if (property.defaultValue) {
            YChart.FormGenerater.TextBox.setValue(element, property.defaultValue);
        }
        if (property.maxLength) {
            element.setAttribute("maxlength", property.maxLength);
        }
        if (property.dialog && property.dialog.url) {
            if (property.dialog.methodName && property.dialog.methodBody) {
                var _this = this;
                eval("_this.callbacks." + property.dialog.methodName + "=function(" + (property.dialog.methodArguments || "") + "){" + property.dialog.methodBody + "};");
            }
            YChart.attachEvent(element, property.dialog.openMode, function () {
                var dialog = new YChart.Dialog({
                    title: property.dialog.title,
                    url: property.dialog.url,
                    width: property.dialog.width,
                    height: property.dialog.height,
                    icon: "fa-window-maximize",
                    onLoaded: function (_frame) {
                        _frame.contentWindow.postMessage({ commad: "openDialog", dialogID: dialog.id, methodName: property.dialog.methodName }, _frame.getAttribute("src"));
                    }
                });
            });
        }
        //显示相关签名信息
        if (property.flow && property.flow.displayStep) {
            this.setStepSign(element, property.flow, YChart.FormGenerater.TextBox.setValue);
        }
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1") {
                //如果控件已经禁用，则不许用再使用
                return;
            }
            if (property.isRequire == "1" && YChart.trim(this.value) == "") {
                //提醒空值
                YChart.showTip(this, "请输入文本框值");
                ev.returnValue = false;
                return;
            }
            if ((property.minLength && this.value.length < property.minLength) || (property.maxLength && this.value.length > property.maxLength)) {
                var text = "当前已输入" + this.value.length + "个字符，";
                if (property.minLength && property.maxLength) {
                    text += "字符长度限制在" + property.minLength + "至" + property.maxLength + "之间";
                }
                else if (property.minLength) {
                    text += "请至少输入" + property.minLength + "个字符";
                } else if (property.maxLength) {
                    text += "输入字符不得超过" + property.maxLength + "个";
                }
                YChart.showTip(this, text);
                ev.returnValue = false;
                return;
            }
            for (var i = 0; i < property.rules.length; i++) {
                try {
                    var reg = new RegExp(property.rules[i].regular);
                    if (!reg.test(this.value)) {
                        YChart.showTip(this, property.rules[i].message);
                        ev.returnValue = false;
                        return;
                    }
                }
                catch (err) { }
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.TextBox.disable(element, false);
        }
    },
    disable: YChart.FormGenerater.disableTextBox,
    setValue: function (element, value) {
        element.value = value;
    },
    getValue: function (element) {
        return element.value;
    }
};

//textarea控件
YChart.FormGenerater.TextArea = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        if (property.placeHolder) {
            element.setAttribute("placeholder", property.placeHolder);
        }
        if (property.isReadonly == "1") {
            element.setAttribute("readonly", "readonly");
        }
        else {
            element.removeAttribute("readonly");
        }
        if (property.dialog && property.dialog.url) {
            if (property.dialog.methodName && property.dialog.methodBody) {
                var _this = this;
                eval("this.callbacks." + property.dialog.methodName + "=function(" + (property.dialog.methodArguments || "") + "){" + property.dialog.methodBody + "};");
            }
            YChart.attachEvent(element, property.dialog.openMode, function () {
                var dialog = new YChart.Dialog({
                    title: property.dialog.title,
                    url: property.dialog.url,
                    width: property.dialog.width,
                    height: property.dialog.height,
                    icon: "fa-window-maximize",
                    onLoaded: function (_frame) {
                        _frame.contentWindow.postMessage({
                            commad: "openDialog", dialogID: dialog.id, methodName: property.dialog.methodName
                        }, _frame.getAttribute("src"));
                    }
                });
            });
        }
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                YChart.FormGenerater.TextArea.setValue(element, node.getAttribute("value"));
            }
        }
        else if (property.defaultValue) {
            YChart.FormGenerater.TextArea.setValue(element, property.defaultValue);
        }
        if (property.maxLength) {
            element.setAttribute("maxlength", property.maxLength);
        }
        //显示相关签名信息
        if (property.flow && property.flow.displayStep) {
            this.setStepSign(element, property.flow, YChart.FormGenerater.TextArea.setValue);
        }
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1") {
                //如果控件已经禁用，则不许用再使用
                return;
            }
            if (property.isRequire == "1" && YChart.trim(this.value) == "") {
                YChart.showTip(this, "请输入多行文本框的值");
                ev.returnValue = false;
                return;
            }
            //验证输入长度
            if ((property.minLength && this.value.length < property.minLength) || (property.maxLength && this.value.length > property.maxLength)) {
                var text = "当前已输入" + this.value.length + "个字符，";
                if (property.minLength && property.maxLength) {
                    text += "字符长度限制在" + property.minLength + "至" + property.maxLength + "之间";
                }
                else if (property.minLength) {
                    text += "请至少输入" + property.minLength + "个字符";
                } else if (property.maxLength) {
                    text += "输入字符不得超过" + property.maxLength + "个";
                }
                YChart.showTip(this, text);
                ev.returnValue = false;
                return;
            }
            //验证正则表达式
            for (var i = 0; i < property.rules.length; i++) {
                try {
                    var reg = new RegExp(property.rules[i].regular);
                    if (!reg.test(this.value)) {
                        YChart.showTip(this, property.rules[i].message);
                        ev.returnValue = false;
                        return;
                    }
                }
                catch (err) { }
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.TextArea.disable(element, false);
        }
    },
    disable: YChart.FormGenerater.disableTextArea,
    setValue: function (element, value) {
        element.textContent = value;
    },
    getValue: function (element) {
        return element.value;
    }
};

//radio控件
YChart.FormGenerater.Radio = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        YChart.clear(element);
        if (property.rowAmount == 0 || property.items.length <= property.rowAmount) {
            //直接平铺
            var _newName;
            for (var i = 0; i < property.items.length; i++) {
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "radio", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (i != 0 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (i != property.items.length - 1 && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                element.appendChild(r);
                element.appendChild(l);
                if (parentElement) {
                    _element.setAttribute("yctargetname", _element.getAttribute("name"));
                    if (!_newName) {
                        var index = 0;
                        var _name = _element.getAttribute("name");
                        while (document.getElementsByName(_name + "_" + index)) {
                            index++;
                        }
                        _newName = _name + "_" + index;
                    }
                    r.setAttribute("name", _newName);
                    r.setAttribute("id", _newName + "_" + property.items[i].id);
                    l.setAttribute("for", _newName + "_" + property.items[i].id);
                }
            }
        }
        else {
            var amount = 0;
            var _tb = YChart.createElement("table", null, { borderCollapse: "collapse", width: "auto", height: "auto" });
            element.appendChild(_tb);
            var _tr, _newName;
            var lastRowIndex = (property.items.length % property.rowAmount == 0 ? (property.items.length / property.rowAmount) : parseInt((property.items.length / property.rowAmount) + 1)) - 1;
            for (var i = 0; i < property.items.length; i++) {
                if (amount == 0 || amount == property.rowAmount) {
                    _tr = YChart.createElement("tr");
                    _tb.appendChild(_tr);
                    amount = 0;
                }
                amount++;
                var _tc = _tr.insertCell(-1);
                if (_tr.rowIndex != 0 && property.paddingTop > 0) {
                    _tc.style.paddingTop = property.paddingTop + "px";
                }
                if (_tr.rowIndex != lastRowIndex && property.paddingBottom > 0) {
                    _tc.style.paddingBottom = property.paddingBottom + "px";
                }
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "radio", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (amount != 1 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (amount != property.rowAmount && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                _tc.appendChild(r);
                _tc.appendChild(l);
                if (parentElement) {
                    _element.setAttribute("yctargetname", _element.getAttribute("name"));
                    if (!_newName) {
                        var index = 0;
                        var _name = _element.getAttribute("name");
                        while (document.getElementsByName(_name + "_" + index)) {
                            index++;
                        }
                        _newName = _name + "_" + index;
                    }
                    r.setAttribute("name", _newName);
                    r.setAttribute("id", _newName + "_" + property.items[i].id);
                    l.setAttribute("for", _newName + "_" + property.items[i].id);
                }
            }
            amount--;
            if (_tr) {
                while (amount < property.rowAmount) {
                    _tr.insertCell(-1);
                    amount++;
                }
            }
        }
        var values;
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                values = node.getAttribute("value");
            }
        }
        else if (!values) {
            values = [];
            for (var i = 0; i < property.items.length; i++) {
                if (property.items[i].isChecked == "1") {
                    values.push(property.items[i].value);
                }
            }
        }
        YChart.FormGenerater.CheckBox.setValue(element, values);
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1") {
                //如果控件已经禁用，则不许用再使用
                return;
            }
            if (property.isRequire == "1") {
                var isChecked = false;
                YChart.recuriseChildNodes(this, function (_element) {
                    if (_element.nodeType != 1 || _element.tagName != "INPUT" || _element.getAttribute("type") != "radio") {
                        return;
                    }
                    if (_element.checked) {
                        isChecked = true;
                        return false;
                    }
                });
                if (!isChecked) {
                    YChart.showTip(this, "请至少选择一项！");
                    ev.returnValue = false;
                    return;
                }
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.Radio.disable(element, false);
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
        for (var i = 0; i < element.childNodes.length; i++) {
            if (element.childNodes[i].nodeType != 1 || element.childNodes[i].tagName != "INPUT" || element.childNodes[i].getAttribute("type") != "radio") {
                continue;
            }
            var text = "";
            if (element.childNodes[i].checked) {
                text = "fa-dot-circle-o";
            }
            else {
                text = "fa-circle-thin";
            }
            var margin = element.childNodes[i].style.margin;
            if (!margin) {
                margin = "0px 2px 0px 0px";
            }
            else {
                var strs = margin.split(" ");
                margin = strs[0] + " 2px " + strs[2] + " " + strs[3];
            }
            YChart.insertAfter(YChart.createElement("span", { className: "fa " + text }, { margin: margin }), element.childNodes[i]);
            if (isPrint) {
                YChart.remove(element.childNodes[i]);
            }
            else {
                element.childNodes[i].style.display = "none";
            }
        }
    },
    setValue: function (element, values) {
        if (typeof (values) == "string") {
            values = values.split(",");
        }
        YChart.recuriseChildNodes(element, function (_element) {
            if (_element.nodeType != 1 || _element.tagName != "INPUT" || _element.getAttribute("type") != "radio") {
                return;
            }
            var _value = _element.getAttribute("value");
            if (values) {
                _element.checked = YChart.isInArray(values, _value);
            }
            else {
                var isIn = false;
                for (var i = 0; i < property.items.length; i++) {
                    if (property.items[i].isChecked == "1" && property.items[i].value == _value) {
                        isIn = true;
                    }
                }
                _element.checked = isIn;
            }
        });
    },
    getValue: function (element) {
        var result = { text: "", value: "" };
        for (var i = 0; i < element.childNodes.length; i++) {
            if (element.childNodes[i].nodeType != 1) {
                continue;
            }
            if (element.childNodes[i].tagName == "INPUT" && element.childNodes[i].getAttribute("type") == "radio" && element.childNodes[i].checked) {
                result.text = element.childNodes[i].nextSibling.textContent;
                result.value = element.childNodes[i].getAttribute("value");
                break;
            }
        }
        return result;
    }
};

//checkbox控件
YChart.FormGenerater.CheckBox = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        YChart.clear(element);
        property.minChooseAmount = YChart.parseInt(property.minChooseAmount);
        property.maxChooseAmount = YChart.parseInt(property.maxChooseAmount);
        element.setAttribute("minChooseAmount", property.minChooseAmount);
        element.setAttribute("maxChooseAmount", property.maxChooseAmount);
        element.setAttribute("choosedamount", "0");
        var onCheckChange = function () {
            var choosed = YChart.parseInt(this.parentNode.getAttribute("choosedamount"));
            if (this.checked) {
                var minChooseAmount = parseInt(element.getAttribute("minChooseAmount"));
                var maxChooseAmount = parseInt(element.getAttribute("maxChooseAmount"));
                if (maxChooseAmount != 0 && maxChooseAmount < choosed) {
                    this.checked = false;
                }
                else {
                    this.parentNode.setAttribute("choosedamount", choosed + 1);
                }
            }
            else {
                this.parentNode.setAttribute("choosedamount", choosed - 1);
            }
        }
        //生成checkbox
        if (property.rowAmount == 0 || property.items.length <= property.rowAmount) {
            var _newName;
            for (var i = 0; i < property.items.length; i++) {
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "checkbox", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                r.onchange = onCheckChange;
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (i != 0 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (i != property.items.length - 1 && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                element.appendChild(r);
                element.appendChild(l);
                if (parentElement) {
                    _element.setAttribute("yctargetname", _element.getAttribute("name"));
                    if (!_newName) {
                        var index = 0;
                        var _name = _element.getAttribute("name");
                        while (document.getElementsByName(_name + "_" + index)) {
                            index++;
                        }
                        _newName = _name + "_" + index;
                    }
                    r.setAttribute("name", _newName);
                    r.setAttribute("id", _newName + "_" + property.items[i].id);
                    l.setAttribute("for", _newName + "_" + property.items[i].id);
                }
            }
        }
        else {
            var amount = 0;
            var _tb = YChart.createElement("table", null, { borderCollapse: "collapse", width: "auto", height: "auto" });
            element.appendChild(_tb);
            var _tr, _newName;
            var lastRowIndex = (property.items.length % property.rowAmount == 0 ? (property.items.length / property.rowAmount) : parseInt((property.items.length / property.rowAmount) + 1)) - 1;
            for (var i = 0; i < property.items.length; i++) {
                if (amount == 0 || amount == property.rowAmount) {
                    _tr = YChart.createElement("tr");
                    _tb.appendChild(_tr);
                    amount = 0;
                }
                amount++;
                var _tc = _tr.insertCell(-1);
                if (_tr.rowIndex != 0 && property.paddingTop > 0) {
                    _tc.style.paddingTop = property.paddingTop + "px";
                }
                if (_tr.rowIndex != lastRowIndex && property.paddingBottom > 0) {
                    _tc.style.paddingBottom = property.paddingBottom + "px";
                }
                var r = YChart.createElement("input", { id: property.groupName + "_" + property.items[i].id, name: property.groupName, type: "checkbox", checked: property.items[i].isChecked == 1 ? "checked" : "", value: property.items[i].value });
                r.onchange = onCheckChange;
                var l = YChart.createElement("label", { "for": property.groupName + "_" + property.items[i].id }, null, property.items[i].name);
                if (amount != 1 && property.paddingLeft > 0) {
                    r.style.marginLeft = property.paddingLeft + "px";
                }
                if (amount != property.rowAmount && property.paddingRight > 0) {
                    l.style.marginRight = property.paddingRight + "px";
                }
                _tc.appendChild(r);
                _tc.appendChild(l);
                if (parentElement) {
                    _element.setAttribute("yctargetname", _element.getAttribute("name"));
                    if (!_newName) {
                        var index = 0;
                        var _name = _element.getAttribute("name");
                        while (document.getElementsByName(_name + "_" + index)) {
                            index++;
                        }
                        _newName = _name + "_" + index;
                    }
                    r.setAttribute("name", _newName);
                    r.setAttribute("id", _newName + "_" + property.items[i].id);
                    l.setAttribute("for", _newName + "_" + property.items[i].id);
                }
            }
            amount--;
            if (_tr) {
                while (amount < rowAmount) {
                    _tr.insertCell(-1);
                    amount++;
                }
            }
        }
        if (property.maxChooseAmount > 0) {
            element.setAttribute("title", "最多可选中" + property.maxChooseAmount + "项");
        }
        var values;
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                values = node.getAttribute("value").split(",");
            }
        }
        else {
            values = [];
            for (var i = 0; i < property.items.length; i++) {
                if (property.items[i].isChecked == "1") {
                    values.push(property.items[i].value);
                }
            }
        }
        YChart.FormGenerater.CheckBox.setValue(element, values);
        var choosed = 0;
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1" || (!property.minChooseAmount && !property.maxChooseAmount)) {
                return;
            }
            //获取出当前已经选中的数量
            var currentAmount = parseInt(element.getAttribute("choosedamount"));
            if ((property.minChooseAmount && currentAmount < property.minChooseAmount) || (property.maxChooseAmount && currentAmount > property.maxChooseAmount)) {
                var text = "当前已选择" + currentAmount + "项，";
                if (property.minChooseAmount && property.maxChooseAmount) {
                    text += "选项限制在" + property.minChooseAmount + "至" + property.maxChooseAmount + "之间";
                }
                else if (property.minChooseAmount) {
                    text += "请至少选择" + property.minChooseAmount + "项";
                } else if (property.maxChooseAmount) {
                    text += "选择不得超过" + property.maxChooseAmount + "项";
                }
                YChart.showTip(this, text);
                ev.returnValue = false;
                return;
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.CheckBox.disable(element, false);
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
        for (var i = 0; i < element.childNodes.length; i++) {
            if (element.childNodes[i].nodeType != 1 || element.childNodes[i].tagName != "INPUT" || element.childNodes[i].getAttribute("type") != "checkbox") {
                continue;
            }
            var text = "";
            if (element.childNodes[i].checked) {
                text = "fa-check-square-o";
            }
            else {
                text = "fa-square-o";
            }
            var margin = window.getComputedStyle(element.childNodes[i]).margin;
            if (!margin) {
                margin = "0px 2px 0px 0px";
            }
            else {
                var strs = margin.split(" ");
                margin = strs[0] + " 2px " + strs[2] + " " + strs[3];
            }
            YChart.insertAfter(YChart.createElement("span", { className: "fa " + text }, { margin: margin }), element.childNodes[i]);
            if (isPrint) {
                YChart.remove(element.childNodes[i]);
            }
            else {
                element.childNodes[i].style.display = "none";
            }
        }
    },
    setValue: function (element, values) {
        if (typeof (values) == "string") {
            values = values.split(",");
        }
        var minChooseAmount = parseInt(element.getAttribute("minChooseAmount"));
        var maxChooseAmount = parseInt(element.getAttribute("maxChooseAmount"));
        var choosed = parseInt(element.getAttribute("choosedamount"));
        YChart.recuriseChildNodes(element, function (_element) {
            if (_element.nodeType != 1 || _element.tagName != "INPUT" || _element.getAttribute("type") != "checkbox") {
                return;
            }
            _element.checked = YChart.isInArray(values, _element.getAttribute("value"));
        });
    },
    getValue: function (element) {
        var result = { text: "", value: "" };
        for (var i = 0; i < element.childNodes.length; i++) {
            if (element.childNodes[i].nodeType != 1 || element.childNodes[i].tagName != "INPUT") {
                continue;
            }
            if (element.childNodes[i].tagName == "INPUT" && element.childNodes[i].getAttribute("type") == "checkbox" && element.childNodes[i].checked) {
                if (result.value != "") {
                    result.value += ",";
                    result.text += ",";
                }
                result.value += element.childNodes[i].getAttribute("value");
                result.text += element.childNodes[i].nextSibling.textContent;
            }
        }
        return result;
    }
}

//select控件
YChart.FormGenerater.Select = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        //重新添加option
        YChart.clear(element);
        for (var i = 0; i < property.items.length; i++) {
            var option = document.createElement("option");
            option.appendChild(document.createTextNode(property.items[i].name));
            option.setAttribute("value", property.items[i].value || property.items[i].name);
            element.appendChild(option);
            if (property.items[i].isChecked == "1") {
                option.selected = true;
            }
        }
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                YChart.FormGenerater.Select.setValue(element, node.getAttribute("value"));

            }
        }
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.Select.disable(element, false);
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
        var margin = element.style.margin;
        if (margin) {
            YChart.insertAfter(YChart.createElement("span", null, { margin: margin }, YChart.getSelectedText(element)), element);
        }
        else {
            YChart.insertAfter(document.createTextNode(element.value), YChart.getSelectedText(element));
        }
        if (isPrint) {
            YChart.remove(element);
        }
        else {
            element.style.display = "none";
        }
    },
    setValue: function (element, value) {
        YChart.setSelectedByValue(element, value);
    },
    getValue: function (element) {
        var result = { value: "", text: "" };
        for (var i = 0; i < element.options.length; i++) {
            if (element.options[i].selected) {
                result.value = element.options[i].getAttribute("value");
                result.text = element.options[i].textContent;
                break;
            }
        }
        return result;
    }
};

//datebox控件
YChart.FormGenerater.DateBox = {
    init: function (element, property, parentElement, rowData) {
        if (!property) {
            return;
        }
        if (property.placeHolder) {
            element.setAttribute("placeholder", property.placeHolder);
        }
        var options = {};
        if (property.minDateMode == "1" && property.minDate) {
            options.minDate = property.minDate;
        }
        else if (property.minDateMode == "2" && property.minDateElement) {
            if (parentElement) {
                YChart.recuriseChildNodes(parentElement, function (_element) {
                    if (_element.nodeType == 1 && _element.getAttribute("ycelement") == "datebox" && _element.getAttribute("id") == property.minDateElement) {
                        options.minDateElement = _element;
                        return false;
                    }
                });
            }
            if (!options.minDateElement) {
                options.minDateElement = document.getElementById(property.minDateElement);
            }
        }
        if (property.maxDateMode == "1" && property.maxDate) {
            options.maxDate = property.maxDate;
        }
        else if (property.maxDateMode == "2" && property.maxDateElement) {
            if (parentElement) {
                YChart.recuriseChildNodes(parentElement, function (_element) {
                    if (_element.nodeType == 1 && _element.getAttribute("ycelement") == "datebox" && _element.getAttribute("id") == property.maxDateElement) {
                        options.maxDateElement = _element;
                        return false;
                    }
                });
            }
            if (!options.maxDateElement) {
                options.maxDateElement = document.getElementById(property.maxDateElement);
            }
        }
        YChart.DateBox.init(element, options);
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                var datettime = new Date(node.getAttribute("value"));
                if (!isNaN(datettime)) {
                    YChart.FormGenerater.DateBox.setValue(element, YChart.getDateText(datettime, "yyyy-MM-dd"));
                }
            }
        }
        else if (property.isDefaultCurrentDate == "1") {
            YChart.FormGenerater.DateBox.setValue(element, YChart.getDateText(this.currentDate, "yyyy-MM-dd"));
        }
        //显示相关签名信息
        if (property.flow && property.flow.displayStep) {
            this.setStepSign(element, property.flow, YChart.FormGenerater.DateBox.setValue);
        }
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1") {
                //如果控件已经禁用，则不许用再使用
                return;
            }
            if (property.isRequire == "1" && YChart.trim(this.value) == "") {
                //提醒空值
                YChart.showTip(this, "请选择日期");
                ev.returnValue = false;
                return;
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.DateBox.disable(element, false);
        }
    },
    disable: YChart.FormGenerater.disableTextBox,
    setValue: function (element, value) {
        element.value = value;
    },
    getValue: function (element) {
        return element.value;
    }
};

//numberbox控件
YChart.FormGenerater.NumberBox = {
    init: function (element, property, parentElement, rowData) {
        element.removeAttribute("readonly");
        if (!property) {
            return;
        }
        YChart.registerNumberBox({
            element: element,
            precision: property.precision,
            min: property.minValue || "",
            max: property.maxValue || "",
        });
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                YChart.FormGenerater.NumberBox.setValue(element, node.getAttribute("value"));
            }
            else {
                YChart.FormGenerater.NumberBox.setValue(element, 0);
            }
        } else if (property.defaultValue) {
            YChart.FormGenerater.NumberBox.setValue(element, property.defaultValue);
            YChart.fireEvent(element, "change");
        }
        else {
            YChart.FormGenerater.NumberBox.setValue(element, 0);
        }
        YChart.attachEvent(element, "validate", function (ev) {
            if (this.getAttribute("ycprocessdisabled") == "1") {
                //如果控件已经禁用，则不许用再使用
                return;
            }
            if (property.isRequire == "1" && YChart.trim(this.value) == "") {
                //提醒空值
                YChart.showTip(this, "请输入数字");
                ev.returnValue = false;
                return;
            }
        });
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.NumberBox.disable(element, false);
        }
    },
    disable: YChart.FormGenerater.disableTextBox,
    setValue: function (element, value) {
        element.value = value;
    },
    getValue: function (element) {
        return element.value;
    }
};

//table控件
YChart.FormGenerater.Table = {
    init: function (element, property) {
        element.style.border = "none";
        var _toolbar = element.childNodes[1];
        var _this = this;
        for (var i = 0; i < _toolbar.childNodes.length; i++) {
            YChart.attachEvent(_toolbar.childNodes[i], "click", function () {
                switch (this.getAttribute("yc_action")) {
                    case "add":
                        _this.addTableRow(this);
                        break;
                    case "delete":
                        _this.deleteTableRow(this);
                        break;
                    case "moveup":
                        _this.moveUpTableRow(this);
                        break;
                    case "movedown":
                        _this.moveDownTableRow(this);
                        break;
                }
            });
        }
        element.childNodes[2].rows[1].style.display = "none";
        for (var i = 0; i < element.childNodes[2].rows[1].cells.length; i++) {
            element.childNodes[2].rows[1].cells[i].removeAttribute("contenteditable");
            var columnid = element.childNodes[2].rows[0].cells[i].getAttribute("columnid");
            for (var j = 0; j < property.columns.length; j++) {
                if (columnid == property.columns[j].id) {
                    element.childNodes[2].rows[1].cells[i].setAttribute("yctextalign", property.columns[j].textAlign);
                    break;
                }
            }
        }
        //checkbox添加全选事件
        element.childNodes[2].rows[0].cells[0].childNodes[0].onchange = function () {
            var _table = this.parentNode.parentNode.parentNode;
            if (_table.tagName == "TBODY") {
                _table = _table.parentNode;
            }
            for (var i = 2; i < _table.rows.length; i++) {
                _table.rows[i].cells[0].childNodes[0].checked = this.checked;
            }
        }
        if (this.dataValue) {
            //添加行
            var rows = YChart.selectNodes(this.dataValue, "Items[@name=\"" + property.elementName + "\"][@type=\"" + property.type + "\"]/Row");
            for (var i = 0; i < rows.length; i++) {
                this.addTableRow(element.childNodes[1].childNodes[0], rows[i]);
            }
        }
        if (!this.isAllowEdit(property.flow)) {
            YChart.FormGenerater.Table.disable(element, false);
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
        YChart.remove(element.childNodes[1]);
        var _table = element.childNodes[1];
        _table.deleteRow(1);
        for (var i = 0; i < _table.rows.length; i++) {
            _table.rows[i].deleteCell(0);
            if (i == 0) {
                continue;
            }
            for (var j = 0; j < _table.rows[i].cells.length; j++) {
                if (_table.rows[i].cells[j].getAttribute("yctextalign")) {
                    _table.rows[i].cells[j].style.textAlign = _table.rows[i].cells[j].getAttribute("yctextalign");
                }
            }
        }
    }
};

//panel控件
YChart.FormGenerater.Panel = {
    init: function (element, property) {
        element.removeAttribute("contenteditable");
        element.style.border = "none";
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
    }
};

//map控件
YChart.FormGenerater.Map = {
    init: function (element, property, parentElement, rowData) {
        element.style.border = "none";
        var _text = element.childNodes[0];
        _text.removeAttribute("readonly");
        var _val = element.childNodes[1];
        var _allmap = element.childNodes[2];
        _allmap.innerHTML = "";
        if (rowData || this.dataValue) {
            var node = YChart.selectSingleNode(rowData || this.dataValue, "Item[@name=\"" + property.elementName + "\"]");
            if (node) {
                YChart.FormGenerater.Map.setValue(element, node.getAttribute("text"), node.getAttribute("value"));
            }
        }
        element.map = new BMap.Map(_allmap, { enableMapClick: false });
        var map = element.map;
        if (_val.value == '') {
            // 没有覆盖物，就定位到戚墅堰街道
            var poi = new BMap.Point(120.06496, 31.723543);
            map.centerAndZoom(poi, 16);
        } else {
            // 有覆盖物
            var overlayJson = JSON.parse(_val.value);
            if (overlayJson.type == 'Marker') {
                // 如果覆盖物类型为 标注
                var poi = new BMap.Point(overlayJson.arg.lng, overlayJson.arg.lat); // 标注需要一个 point 参数
                map.centerAndZoom(poi, 16);
                var marker = new BMap.Marker(poi);
                map.addOverlay(marker);    //添加标注
            }
        }
        map.enableScrollWheelZoom();

        // 绘制工具
        var overlays = [];
        var overlaycomplete = function (e) {
            overlays.push(e.overlay);
        };
        var styleOptions = {
            strokeColor: "red",    //边线颜色。
            fillColor: "red",      //填充颜色。当参数为空时，圆形将没有填充效果。
            strokeWeight: 3,       //边线的宽度，以像素为单位。
            strokeOpacity: 0.8,	   //边线透明度，取值范围0 - 1。
            fillOpacity: 0.6,      //填充的透明度，取值范围0 - 1。
            strokeStyle: 'solid' //边线的样式，solid或dashed。
        }
        //实例化鼠标绘制工具
        var drawingManager = new BMapLib.DrawingManager(map, {
            isOpen: false, //是否开启绘制模式
            enableDrawingTool: true, //是否显示工具栏
            drawingToolOptions: {
                anchor: BMAP_ANCHOR_TOP_RIGHT, //位置
                offset: new BMap.Size(5, 5), //偏离值
            },
            circleOptions: styleOptions, //圆的样式
            polylineOptions: styleOptions, //线的样式
            polygonOptions: styleOptions, //多边形的样式
            rectangleOptions: styleOptions //矩形的样式
        });
        //添加鼠标绘制工具监听事件，用于获取绘制结果
        drawingManager.addEventListener('overlaycomplete', overlaycomplete);

        // 文本框搜索
        var ac = new BMap.Autocomplete(    //建立一个自动完成的对象
            {
                "input": _text,
                "location": map
            });

        var myValue;
        ac.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
            var _value = e.item.value;
            myValue = _value.province + _value.city + _value.district + _value.street + _value.business;
            //G("searchResultPanel").innerHTML = "onconfirm<br />index = " + e.item.index + "<br />myValue = " + myValue;

            setPlace();
        });

        function setPlace() {
            map.clearOverlays();    //清除地图上所有覆盖物
            function myFun() {
                var pp = local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
                _val.value = JSON.stringify({ type: 'Marker', arg: pp });
                map.centerAndZoom(pp, 16);
                var marker = new BMap.Marker(pp);
                map.addOverlay(marker);    //添加标注
            }
            var local = new BMap.LocalSearch(map, { //智能搜索
                onSearchComplete: myFun
            });
            local.search(myValue);
        }
    },
    disable: function (element, isPrint) {
        if (element.getAttribute("ycprocessdisabled")) {
            return;
        }
        element.setAttribute("ycprocessdisabled", "1");
    },
    getValue: function (element) {
        return { text: element.childNodes[0].value, value: element.childNodes[1].value };
    },
    setValue: function (element, text, value) {
        element.childNodes[0].value = text;
        element.childNodes[1].value = value;
    }
};

//uploader控件
YChart.FormGenerater.Uploader = {

};

//signbox控件
YChart.FormGenerater.SignBox = {
    init: function (element, property, parentElement, rowData) {
        element.style.border = "none";
        if (!property) {
            return;
        }
        var _tr = element.parentNode;
        element.style.display = "none";
        while (_tr.nodeType == 1 && _tr.tagName != "TR" && _tr.tagName != "BODY") {
            _tr = _tr.parentNode;
        }
        if (_tr.nodeType != 1 || _tr.tagName != "TR" || _tr.cells.length < 2) {
            return;
        }
        //获取出其第一个tr
        if (!this.steps || this.steps.length == 0) {
            _tr.style.display = "none";
            return;
        }
        var firstTR = _tr;
        for (var i = 0; i < this.steps.length; i++) {
            if (i != 0) {
                var newTR = _tr.cloneNode(true);
                YChart.insertAfter(newTR, _tr);
                _tr = newTR;
            }
            for (var j = 0; j < _tr.cells.length; j++) {
                //清除多余的元素，防止出现意外
                _tr.cells[j].innerHTML = "";
            }
            if (property.isShowStepName == "1") {
                _tr.cells[0].textContent = this.steps[i].getAttribute("displayName");
            }
            _tr.setAttribute("ycelement", "signboxrow");
            var actors = YChart.selectNodes(this.steps[i], "Actor");
            for (var j = 0; j < actors.length; j++) {
                if (property.isShowProcessNote == "1") {
                    //获取出note
                    var width = _tr.cells[1].clientWidth - 20;
                    if (width < 40) {
                        width = 40;
                    }
                    var _ta = YChart.createElement("textarea", { className: "yc_textbox", readonly: "readonly", rows: "3", ycelement: "signboxnote" }, { border: "none", backgroundColor: "transparent", width: width + "px", resize: "none" });
                    _ta.value = actors[j].textContent;
                    if (j != 0) {
                        _ta.style.marginTop = "5px";
                    }
                    if (actors[j].textContent == "") {
                        _ta.setAttribute("placeholder", "未填写意见");
                    }
                    _tr.cells[1].appendChild(_ta);
                }
                if (property.isShowProcessUser == "1" || property.isShowProcessTime == "1") {
                    var _div = YChart.createElement("div", null, { textAlign: "right", marginRight: "10px" });
                    _tr.cells[1].appendChild(_div);
                    if (property.isShowProcessUser == "1") {
                        _div.appendChild(YChart.createElement("span", null, { marginRight: "10px" }, actors[j].getAttribute("name")));
                    }
                    if (property.isShowProcessTime == "1") {
                        if (actors[j].getAttribute("isFinished") == "1") {
                            var time = actors[j].getAttribute("time");
                            if (time == "") {
                                return;
                            }
                            time = new Date(time);
                            _div.appendChild(YChart.createElement("span", null, null, property.ProcessTimeFormat.replace("yyyy", time.getFullYear()).replace("MM", YChart.supplyString((time.getMonth() + 1))).replace("dd", YChart.supplyString((time.getDate()))).replace("HH", YChart.supplyString((time.getHours()))).replace("mm", YChart.supplyString((time.getMinutes()))).replace("ss", YChart.supplyString((time.getSeconds())))));
                        }
                        else {
                            _div.appendChild(YChart.createElement("span", null, null, "未处理"));
                        }
                    }
                }
            }
        }
    },
    disable: YChart.FormGenerater.disableTextArea
};

//hidden控件
YChart.FormGenerater.Hidden = {
    init: function () {

    },
    setValue: function (element, value) {
        element.setAttribute("value", value);
    },
    getValue: function (element, value) {
        return element.getAttribute("value");
    }
};