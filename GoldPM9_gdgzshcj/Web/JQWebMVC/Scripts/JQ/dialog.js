
var JQ = JQ || {};
//金曲弹出框
JQ.dialog = {
    //弹出框iframe模式
    dialogIframe: function (parames) {
        var dialogOptions = $.extend({
            iconCls: 'fa fa-table',
            width: 760,
            height: 360,
            closed: false,
            cache: false,
            modal: true,
            collapsible: true,
            needBody: false,
        }, parames);
        var dialogName = 'JQ_Dialog_' + (new Date()).getTime();
        //var $div = $("<div id='" + dialogName + "' style='overflow: hidden;'><iframe src='" + parames.url + "' style='width:100%; height:100%' frameborder='0'></iframe></div>");
        //$div.appendTo(window.top.$("body"));
        var $div;
        debugger
        if (dialogOptions.needBody === true) {
            $div = $("<div id='" + dialogName + "' style='overflow: hidden;'><iframe src='core/layout/DefaultEmpty' style='width:100%; height:100%' frameborder='0'></iframe></div>");
            $div.appendTo(window.top.$("body"));
            window.top.$("#" + dialogName).find("iframe")[0].contentWindow.frameElement.url = parames.url;
        }
        else {
            $div = $("<div id='" + dialogName + "' style='overflow: hidden;'><iframe src='about:blank' style='width:100%; height:100%' frameborder='0'></iframe></div>");
            $div.appendTo(window.top.$("body"));
            $div.find("iframe").attr("src", dialogOptions.url)[0].onload = function () {
                this.contentWindow.closeSelf = function () {
                    window.top.$("#" + dialogName).dialog("close");
                }
            };
        }
      
        /* 如果Dialog高度或宽度大于等于当前可视区域，则自动调整 */
        var maxH = $(window.top.window).height();
        var maxW = $(window.top.window).width();
        if (dialogOptions.height > maxH) dialogOptions.height = maxH;
        if (dialogOptions.width > maxW) dialogOptions.width = maxW;
        dialogOptions.onClose = function () {
            parames.onClose && parames.onClose();
            $div.parent().nextAll().remove();
            $div.parent().remove();
        }
        window.top.$("#" + dialogName).dialog(dialogOptions);
    },
    //弹出框div模式
    dialog: function (parames) {
        parames = $.extend({
            iconCls: 'fa fa-table',
            width: 760,
            height: 360,
            iframeID: null,
            istoolbar: true,
            JQLoadingType: '',
            JQID: '',
            url: null,
            method: 'get',
            queryParams: {},
        }, parames);

        /* 如果Dialog高度或宽度大于等于当前可视区域，则自动调整 */
        var maxH = $(window.top.window).height();
        var maxW = $(window.top.window).width();
        if (parames.height > maxH) parames.height = maxH;
        if (parames.width > maxW) parames.width = maxW;
        try {
            parames.iframeID = window.frameElement.id;
        }
        catch (e) {
            parames.iframeID = null;
        }
        var divid = JQ.tools.getMathNo();
        var parentid = '';
        var divinfo = JQ.tools.findDialogInfo();
        if (JQ.tools.isJson(divinfo)) {
            parentid = divinfo.divid;
        }
        if (window.top.$("#" + divid).size() <= 0) {
            window.top.$("body").append("<div class='rwpdialogdiv' id='" + divid + "' iframeID='" + parames.iframeID + "' loadingType='" + parames.JQLoadingType + "' dgID='" + parames.JQID + "' parentid='" + parentid + "'></div>");
        }
        if (parames.url != null) {
            parames.url += ((parames.url.indexOf("?") > 0 ? '&' : '?') + 'divid=' + divid + '&iframeID=' + parames.iframeID + '&loadingType=' + parames.JQLoadingType + '&dgID=' + parames.JQID);
        }
        var paraJosn = {
            iconCls: parames.iconCls,
            title: "<b>" + parames.title + "</b>",
            href: parames.url,
            method: parames.method,
            queryParams: parames.queryParams,
            loader: parames.loader,
            width: parames.width,
            height: parames.height,
            maximizable: false,
            resizable: false,
            collapsible: true,
            modal: true,
            toolbar: "<div style='display:none;' id='toolbar' divid='" + divid + "'></div>",
            onClose: function () {
                parames.onClose && parames.onClose();
                window.top.$("#" + divid).parent().nextAll().each(function (index) {//删除之后所有的元素防止bug
                    if (this.className.indexOf('window') <= -1) {
                        $(this).remove();
                    }
                });
                window.top.$("#" + divid).parent().remove();
            },
            onResize: function (width, height) {
                window.top.$.parser.parse($("#" + divid));
            }
        };
        if (paraJosn)
            var dialog = window.top.$("#" + divid).dialog(paraJosn);
        return divid;
    },

    //选择数据弹出框
    dialogSel: function (parames) {
        var divid = JQ.tools.getMathNo();
        if ($("#" + divid).size() <= 0) {
            $("body").append("<div class='rwpdialogseliv' id='" + divid + "'></div>");
        }
        var dialog = $("#" + divid).dialog({
            title: parames.title,
            href: parames.url,
            width: parames.width,
            height: parames.height,
            resizable: true,
            maximizable: true,
            collapsible: true,
            modal: true,
            onClose: function () {
                $("#" + divid).remove();
            },
            buttons: [{
                text: '选 择',
                iconCls: 'icon-ok',
                handler: function () {
                    var options = $('#' + parames.treeGirdID).treegrid('options');
                    var arrSel = $('#' + parames.treeGirdID).treegrid('getSelections');
                    var idField = ""; var treeField = "";
                    if (arrSel && arrSel.length > 0) {
                        for (var i = 0; i < arrSel.length; i++) {
                            idField += i > 0 ? "," + arrSel[i][options.idField] : arrSel[i][options.idField];
                            treeField += i > 0 ? "," + arrSel[i][options.treeField] : arrSel[i][options.treeField];
                        }
                        $("#" + parames.setValue).val(idField);
                        $("#" + parames.setText).textbox('setValue', treeField);
                        $("#" + divid).dialog('close');
                    };
                }
            }, {
                text: '关 闭',
                iconCls: 'icon-clear',
                handler: function () {
                    $("#" + divid).dialog('close');
                }
            }]
        });
    },

    //关闭弹出框
    dialogClose: function (parames) {
        var info = JQ.tools.findDialogInfo();
        window.top.$("#" + info.divid).dialog('close');
    },

    //上传附件弹出框
    fields: function (parames) {
        parames = $.extend({
            AttachRefID: 0,
            AttachRefTable: '',
            JQID: '',
            JQLoadingType: 'datagrid',
            iSRefresh: true
        }, parames);
        JQ.dialog.dialog({
            iconCls: 'fa fa-upload',
            title: "<b>上传图片窗体</b>",
            width: '760',
            height: '330',
            url: JQ.tools.getRootPath() + '/Core/usercontrol/uploadFile?AttachRefID=' + parames.AttachRefID + '&AttachRefTable=' + parames.AttachRefTable,
        });
    },

    //提示2秒后消失
    show: function (message) {
        window.top.$.messager.show({
            title: '信息',
            msg: "<div style='text-align:center;font-weight:bold;line-height:30px;'>" + message + "</div>",
            timeout: 2000,
            showType: 'slide'
        });
    },

    //提示
    error: function (message) {
        window.top.$.messager.alert("错误", "<div style='text-align:center;line-height:30px;font-weight:bold;'>" + message + "</div>", 'error');
    },

    //提示
    info: function (message) {
        window.top.$.messager.alert("消息", "<div style='text-align:center;line-height:30px;font-weight:bold;'>" + message + "</div>", 'info');
    },

    //提示
    question: function (message) {
        window.top.$.messager.alert("询问", "<div style='text-align:center;line-height:30px;font-weight:bold;'>" + message + "</div>", 'question');
    },

    //提示
    warning: function (message) {
        window.top.$.messager.alert("警告", "<div style='text-align:center;line-height:30px;font-weight:bold;'>" + message + "</div>", 'warning');
    },

    //提示
    confirm: function (message, callback, params) {
        window.top.$.messager.confirm("询问", "<div style='text-align:center;line-height:30px;font-weight:bold;'>" + message + "</div>", function (r) {
            if (!r) {
                return;
            }
            callback(params);
        });
    }
};