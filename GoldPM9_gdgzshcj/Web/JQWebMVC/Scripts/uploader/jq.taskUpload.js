//附件上传
JQ = JQ || {};

JQ.taskUpload = function () {

    //外部参数
    this.requireurl = "";
    this.uploadurl = "";
    this.downloadUrl = "";
    this.batchDownloadUrl = "";
    this.cancelUrl = "";
    this.flash_swf_url = "";
    this.silverlight_xap_url = "";
    this.CreateFolderUrl = "";
    this.girdParams = {};

    //内部参数
    this.IsActionreload = true;
    this.tempID = 0;
    this.gridId = 0;//列表Id
    this.girdId_Upload = null;//列表easyui对象
    this.gridPanel = null;//列表dom对象
    this.uploadBtn = null;//上传按钮
    this.changeMore = null;//查看切换版本
    this.uploader = null;//上传对象
    this.refID = 0;
    this.refTable = "";
    this.mode = 0;//文本版本显示模式
    this.parentID = 0;
    this.nowCheckRow = null;
    this.nowUncheckRow = null;
};


JQ.taskUpload.prototype = {
    
    init : function (id, option) {
        var othis = this;
        this.girdId_Upload = $(id);
        this.gridId = this.girdId_Upload.attr("id");

        this.girdParams = $.extend({
            idField: 'AttachID',
            treeField: 'AttachName',
            border: 0,
            fit: true,
            lines: true,
            //animate: true,
            selectOnCheck: false,
            checkOnSelect: false,
            singleSelect: true,
            ctrlSelect: false,
            url: this.requireurl,
            onCheck: function (row) {
                if (othis.nowCheckRow == null) {
                    othis.nowCheckRow = row;
                }
                console.log("onCheck %o %o %o", row, othis.nowCheckRow, row == othis.nowCheckRow);
                if (row == othis.nowCheckRow) {
                    opts = othis.girdId_Upload.treegrid("options");
                    if (opts.checkOnSelect && opts.singleSelect) { return; }
                    var idField = opts.idField, id = row[idField];

                    console.log("向上遍历父节点，判断是否要被选中");
                    var parent = othis.girdId_Upload.treegrid("getParent", id);
                    console.log("onCheck parent %o", parent);
                    while (parent) {
                        var parentChildren, parentChecked;
                        parentChildren = othis.girdId_Upload.treegrid("getChildren", parent[idField]);
                        parentChecked = othis.girdId_Upload.treegrid("getChecked");
                        console.log("onCheck, children, checked %o, %o", parentChildren, parentChecked);
                        if (Enumerable.From(parentChildren).Where("x => x.__checkbox == true").ToArray().every(function (val) { return Enumerable.From(parentChecked).Contains(val); })) {
                            var CheckB = othis.girdId_Upload.treegrid("getPanel").find("tr[node-id='" + parent[idField] + "'] input[type='checkbox']");
                            if (!$(CheckB).is(":hidden") && !$(CheckB).prop("checked")) {
                                othis.girdId_Upload.treegrid("checkRow", parent[idField]);
                            }
                        }
                        parent = othis.girdId_Upload.treegrid("getParent", parent[idField]);
                    }

                    // 注意这里有个问题，当文件夹被折叠后 虽然能访问到子节点中的CheckB，但是 CheckB.is(":hidden") 永远等于 true，最终导致子文件不能被正确选中
                    // 这里为了避免这个文件 当选中折叠的文件夹时先展开文件夹，在做后面的子节点选中操作
                    if (row.AttachExt == ".") {
                        if (row.state == "closed") {
                            othis.girdId_Upload.treegrid("expand", row.AttachID);
                        }
                    }

                    console.log("向下遍历子节点，判断是否要被选中");
                    var children = othis.girdId_Upload.treegrid("getChildren", id);
                    console.log("onCheck children %o", children);
                    $.each(children, function (i, n) {
                        // 注意 treegrid("checkRow") 子节点时，不会触发节点的 onCheck事件。
                        // 所以在遍历子节点时也要展开文件夹。
                        if (n.AttachExt == ".") {
                            if (n.state == "closed") {
                                othis.girdId_Upload.treegrid("expand", n.AttachID);
                            }
                        }
                        var CheckB = othis.girdId_Upload.treegrid("getPanel").find("tr[node-id='" + n[idField] + "'] input[type='checkbox']");
                        if (!$(CheckB).is(":hidden") && !$(CheckB).prop("checked")) {
                            othis.girdId_Upload.treegrid("checkRow", n[idField]);
                        }
                    });
                    othis.nowCheckRow = null;
                }
            },
            onUncheck: function (row) {
                console.log("onUncheck %o", row);
                if (othis.nowUncheckRow == null) {
                    othis.nowUncheckRow = row;
                }
                if (row == othis.nowUncheckRow) {
                    opts = othis.girdId_Upload.treegrid("options");
                    if (opts.checkOnSelect && opts.singleSelect) { return; }
                    var idField = opts.idField, id = row[idField];

                    console.log("向上遍历父节点，判断是否要被取消选中");
                    var parent = othis.girdId_Upload.treegrid("getParent", id);
                    while (parent) {
                        var CheckB = othis.girdId_Upload.treegrid("getPanel").find("tr[node-id='" + parent[idField] + "'] input[type='checkbox']");
                        if (!$(CheckB).is(":hidden") && $(CheckB).prop("checked")) {
                            othis.girdId_Upload.treegrid("uncheckRow", parent[idField]);
                        }
                        parent = othis.girdId_Upload.treegrid("getParent", parent[idField]);
                    }

                    // 注意这里有个问题，当文件夹被折叠后 虽然能访问到子节点中的CheckB，但是 CheckB.is(":hidden") 永远等于 true，最终导致子文件不能被正确取消选中
                    // 这里为了避免这个文件 当选中折叠的文件夹时先展开文件夹，在做后面的子节点取消选中操作
                    if (row.AttachExt == ".") {
                        if (row.state == "closed") {
                            othis.girdId_Upload.treegrid("expand", row.AttachID);
                        }
                    }

                    console.log("向下遍历子节点，判断是否要被取消选中");
                    var children = othis.girdId_Upload.treegrid("getChildren", id);
                    $.each(children, function (i, n) {
                        // 注意 treegrid("checkRow") 子节点时，不会触发节点的 onUncheck事件。
                        // 所以在遍历子节点时也要展开文件夹。
                        if (n.AttachExt == ".") {
                            if (n.state == "closed") {
                                othis.girdId_Upload.treegrid("expand", n.AttachID);
                            }
                        }
                        var CheckB = othis.girdId_Upload.treegrid("getPanel").find("tr[node-id='" + n[idField] + "'] input[type='checkbox']");
                        if (!$(CheckB).is(":hidden") && $(CheckB).prop("checked")) {
                            othis.girdId_Upload.treegrid("uncheckRow", n[idField]);
                        }
                    });
                    othis.nowUncheckRow = null;
                }
            },
            onCheckAll: function (rows) {
                console.log("onCheckAll %o", rows);
                othis.nowCheckRow = {};
                othis.nowUncheckRow = {};
                othis.girdId_Upload.treegrid("getPanel").find("input[type='checkbox'][name='ck']").each(function (i, CheckB) {
                    if ($(CheckB).is(":hidden")) {
                        othis.girdId_Upload.treegrid('uncheckRow', rows[i].AttachID);
                    }
                });
                othis.girdId_Upload.treegrid("getPanel").find("tr.datagrid-header-row input[type='checkbox']").prop("checked", true);
                othis.nowCheckRow = null;
                othis.nowUncheckRow = null;
            },
            onUncheckAll: function (rows) {
                console.log("onUncheckAll %o", rows);
                othis.nowCheckRow = {};
                othis.nowUncheckRow = {};
                othis.girdId_Upload.treegrid("getPanel").find("input[type='checkbox'][name='ck']").each(function (i, CheckB) {
                    if ($(CheckB).is(":checked")) {
                        othis.girdId_Upload.treegrid('unselect', rows[i].AttachID);
                    }
                });
                othis.nowCheckRow = null;
                othis.nowUncheckRow = null;
            },
            onClickRow: function (row) {
                console.log("onClickRow %o", row);
                var CheckB = othis.girdId_Upload.treegrid("getPanel").find("tr[node-id='" + row.AttachID + "'] input[type='checkbox']");
                if ($(CheckB).is(":hidden")) {
                    othis.girdId_Upload.treegrid('uncheckRow', row.AttachID);
                }
            },
            onDblClickCell: function (field, row) {
                console.log("onDblClickCell %o", row);
                if (row.AttachExt == ".") {
                    if (row.state == "open") {
                        othis.girdId_Upload.treegrid("collapse", row.AttachID);
                    } else {
                        othis.girdId_Upload.treegrid("expand", row.AttachID);
                    }
                }
            }
        }, option);

        $.extend($.fn.treegrid.defaults.view, {
            onAfterRender: function (target) {
                if (target.id == othis.gridId) {
                    othis.gridPanel = othis.girdId_Upload.treegrid("getPanel");
                    var roots = $(target).treegrid("getRoots");
                    if (roots.length > 0) {
                        $.each(roots, function (i, e) {
                            if (e.AttachExt == ".") {
                                $(target).treegrid("update", { id: e.AttachID, row: { iconCls: "fa-folder" } });
                            }
                            //判断是否子节点
                            var Chiledroots = $(target).treegrid("getChildren", e.AttachID);
                            if (Chiledroots.length > 0) {
                                $.each(Chiledroots, function (ic, ec) {
                                    if (ec.AttachExt == ".") {
                                        $(target).treegrid("update", { id: ec.AttachID, row: { iconCls: "fa-folder" } });

                                    }
                                })
                            }
                        });
                        $("span[class='tree-icon tree-file fa-folder']").removeClass("tree-file");
                    }
                }
            }
        });

        //初始化 表格
        this.girdId_Upload.treegrid(this.girdParams);

        //切换版本
        if (option.hasOwnProperty("changeBtn")) {
            this.changeMore = $("#" + option.changeBtn);
            this.changeMore.attr("mode", 0);
        }

        //上传控件容器
        this.uploadBtn = $("<input type='button' id='uploadContent_" + this.gridId + "' name='uploadContent_" + this.gridId + "'  style='display:none'  />")
        this.uploadBtn.appendTo(this.girdId_Upload.parent());//隐藏的上传按钮

        this.uploader = new plupload.Uploader({
            runtimes: "html5,flash,html4,silverlight",
            browse_button: "uploadContent_" + this.gridId + "",
            chunk_size: "1mb",
            url: this.uploadurl,
            flash_swf_url: this.flash_swf_url,
            silverlight_xap_url: this.silverlight_xap_url,
            multi_selection: true,
            filters: {
                max_file_size: "20gb",
                prevent_duplicates: false
            },
            init: {
                FilesAdded: function (up, files) {
                    othis.filesAdded(othis, up, files);
                },
                BeforeUpload: function (up, file) {
                    othis.beforeUpload(othis, up, file);
                },
                UploadProgress: function (up, file) {
                    othis.uploadProgress(othis, up, file);
                },
                FileUploaded: function (up, file, info) {
                    othis.fileUploaded(othis, up, file, info);
                },
                UploadComplete: function (up, file) {
                    othis.UploadComplete(othis, up, file);
                }
            },

        });
        this.uploader.init();
    },

    //下载
    buildDownloadUrl: function (id, options) {
        if (id <= 0) {
            return this.downloadUrl + "?id=0&name=" + options.name + "&realName=" + options.realName;
        }
        else {
            return this.downloadUrl + "?id=" + id + "&type=" + options.type;
        }
    },

    //将选择文件 添加到队列中
    filesAdded: function (othis, up, files) {
        var othis = othis;
        for (var i in files) {
            othis.tempID--;
            files[i].refID = othis.refID;
            files[i].refTable = othis.refTable;
            files[i].rowID = othis.tempID;
            files[i].mode = othis.mode;
            files[i].parentID = othis.parentID;

            othis.girdId_Upload.treegrid("append", {
                parent: othis.parentID,
                data: [{
                    AttachID: othis.tempID,
                    AttachName: files[i].name,
                    AttachSize: files[i].size,
                    AttachDateChange: JQ.Flow.getDateTimeText(files[i].lastModifiedDate),
                    AttachDateUpload: null,
                    FileID: files[i].id,
                    AttachVer: 1,
                    UploadStatus: 1,
                    _parentId: othis.parentID,
                }]
            });

            othis.gridPanel.find("#r_" + files[i].id).onclick = function () {
                up.removeFile(files[i].id);
                othis.girdId_Upload.treegrid("remove", othis.tempID);
                $.ajax({
                    url: othis.cancelUrl,
                    type: "POST",
                    data: { fileID: fileID },
                    success: function () { }
                });
            }
        }
        up.start();
    },

    //队列中某个文件开始上传时
    beforeUpload: function (othis, up, file) {
        if (othis.changeMore != null) {
            othis.changeMore.hide();//上传前将 切换版本按钮隐藏
        }
        up.settings.multipart_params = {
            refID: file.refID,
            refTable: file.refTable,
            fileID: file.id,
            lastModifiedTime: JQ.Flow.getDateTimeText(file.lastModifiedDate),
            mode: file.mode,
            parentID: file.parentID,
        };
    },

    uploadProgress: function (othis, up, file) {
        //获取出显示进度的进度条
        var progress = othis.gridPanel.find("#p_" + file.id)[0];
        if (progress) {
            progress.childNodes[0].style.width = (file.percent * 0.6) + "px";
            progress.childNodes[1].innerHTML = "";
            progress.childNodes[1].appendChild(document.createTextNode(file.percent.toString() + "%"));
        }
    },

    fileUploaded: function (othis, up, file, info) {
        if (!info.response) {
            return;
        }

        info = JQ.Flow.parseJson(info.response);
        var $grid = othis.girdId_Upload;

        var rootId = file.rowID;
        if (othis.refID != "0") {
            //获取出同名的文件
            var isIn = false;
            if (info.Status == 2) {
                othis.uploader.removeFile(file.id);
                isIn = true;
                $grid.treegrid("remove", rootId);//同名文件移除
                JQ.dialog.show("文件" + file.name + "重复上传！");
            }
            if (info.Version > 1 && file.mode == 0) {
                var parentId = file.parentID;
                if (parentId == 0) {
                    //根节点中判断是否有重复
                    var rows = $grid.treegrid("getRoots");
                    for (var i in rows) {
                        if (rows[i].AttachVer == ".") return true;
                        if (rows[i].AttachName == file.name) {
                            //处理文件名称
                            isIn = true;
                            $grid.treegrid("remove", file.rowID);
                            $grid.treegrid("update", {
                                id: rows[i].AttachID,
                                row: {
                                    AttachID: info.AttachID,
                                    AttachVer: info.Version,
                                    AttachDateUpload: info.UploadDate,
                                    AttachDateChange: JQ.Flow.getDateTimeText(file.lastModifiedDate),
                                    AttachEmpName: decodeURIComponent(info.EmpName),
                                    UploadStatus: 2,
                                    Type: "attach"
                                }
                            });
                        }
                    }

                } else {
                    //从子节点中判断是否有重复
                    var rows = $grid.treegrid("getChildren", file.parentID);
                    for (var i in rows) {
                        if (rows[i].AttachName == file.name && rows[i]._parentId == file.parentID) {
                            //处理文件名称
                            isIn = true;
                            $grid.treegrid("remove", file.rowID);
                            $grid.treegrid("update", {
                                id: rows[i].AttachID,
                                row: {
                                    AttachID: info.AttachID,
                                    AttachVer: info.Version,
                                    AttachDateUpload: info.UploadDate,
                                    AttachDateChange: JQ.Flow.getDateTimeText(file.lastModifiedDate),
                                    AttachEmpName: decodeURIComponent(info.EmpName),
                                    UploadStatus: 2,
                                    Type: "attach"
                                }
                            });
                        }
                    }
                }

            }

            if (!isIn) {
                //直接变更
                if (file.mode == 0) {
                    //合并最新
                    $grid.treegrid("update", {
                        id: rootId,
                        row: {
                            AttachID: info.AttachID,
                            AttachVer: info.Version,
                            AttachEmpName: decodeURIComponent(info.EmpName),
                            AttachDateUpload: info.UploadDate,
                            UploadStatus: 2,
                            Type: "attach"
                        }
                    });
                }
                else {
                    $grid.datagrid("update", {
                        id: rootId,
                        row: {
                            AttachID: info.AttachVersionID,
                            AttachVer: info.Version,
                            AttachEmpName: decodeURIComponent(info.EmpName),
                            AttachDateUpload: info.UploadDate,
                            UploadStatus: 2,
                            Type: "attachversion"
                        }
                    });

                }
            }
        }
        //验证该grid下是否所有的文件都上传完成了
        var rows = $grid.treegrid("getRoots");
        for (var i in rows) {
            if (rows[i].UploadStatus == 1) {
                return;
            }
        }

        if (othis.uploadFinish != null && typeof (othis.uploadFinish) == "function") {
            othis.uploadFinish();
        }
        if (othis.changeMore != null) {
            othis.changeMore.show();//上传前将 切换版本按钮隐藏
        }
    },

    UploadComplete: function (othis, up, files) {
        if (othis.IsActionreload) {
            JQ.dialog.show("上传完毕！！！");
            othis.girdId_Upload.treegrid("reload");
        }
    },

    //创建文件夹
    CreateFolder: function (RefID, RefTable) {
        var othis = this;
        var getSelects = this.girdId_Upload.treegrid("getSelections");
        var fatherId = 0;
        var fatherNode = null;
        if (getSelects.length > 0) {
            if (getSelects[0].AttachExt == ".") {
                //是文件夹 fatherNode = 这个选中行
                fatherNode = getSelects[0];
                fatherId = fatherNode.AttachID;
            } else {
                // 选中行是文件的话，fatherNode = 取文件所在文件夹
                fatherNode = this.girdId_Upload.treegrid("getParent", getSelects[0].AttachID);
                if (fatherNode != null) {
                    fatherId = fatherNode.AttachID;
                }
            }
        }

        var dialogName = 'JQ_Dialog_' + (new Date()).getTime();
        var $div = $(
            "<div id='" + dialogName + "' style='padding:20px;'>"
                + "<div>"
                    + "<label>创建位置：</label>"
                    + "<span style='display:inline-block;padding:8px 0;'>" + (fatherNode == null ? "根" : fatherNode.AttachName) + "</span>"
                + "</div>"
                + "<div>"
                    + "<label>文件夹名：</label>"
                    + "<input id=\"FolderNameDiv\"  type=\"text\"  style=\"width:280px\" />"
                + "</div>"
            + "</div>");
        $div.appendTo(window.top.$("body"));
        window.top.$("#" + dialogName).dialog({
            iconCls: 'fa fa-folder-open',
            title: "创建文件夹",
            width: 400,
            height: 200,
            closed: false,
            cache: false,
            modal: true,
            buttons: [{
                text: '保存',
                iconCls: 'fa fa-save',
                onClick: function () {
                    othis.CreateFunc(fatherId, window.top.$("#" + dialogName), window.top.$("#FolderNameDiv", $("#" + dialogName)), RefID, RefTable);
                }
            }]
        });
        window.top.$("#FolderNameDiv", $("#" + dialogName)).textbox({ required: true });
    },

    //创建文件夹（数据库操作）
    CreateFunc: function (fatherId, dlgFolder, iptFolderName, RefID, RefTable) {
        var othis = this;
        if (iptFolderName.textbox("isValid")) {
            var FolderName = iptFolderName.textbox("getValue");
            $.post(this.CreateFolderUrl, { AttachRefID: RefID, AttachRefTable: RefTable, AttachName: FolderName, AttachParentID: fatherId }, function () {
                othis.girdId_Upload.treegrid("clearSelections");
                othis.girdId_Upload.treegrid("clearChecked");
                othis.girdId_Upload.treegrid("reload");
                iptFolderName.textbox("setValue", "");
                dlgFolder.dialog('close');
                JQ.dialog.show("操作成功！！！");
            }).error(function () {
                JQ.dialog.show("操作失败！！！");
            });
        }
    },

    //上传按钮调用
    open : function (RefID, RefTable) {

        this.refID = RefID;
        this.refTable = RefTable;
        //查看模式
        if (this.changeMore == null) {
            this.mode = 0;
        } else {
            this.mode = this.changeMore.attr("mode");
        }
        //判断是否有父节点
        var parentID = 0;
        var getSelects = this.girdId_Upload.treegrid("getSelections");
        if (getSelects.length > 0) {
            if (getSelects[0].AttachExt == ".") {
                parentID = getSelects[0].AttachID;
            } else {
                if (this.girdId_Upload.treegrid("getParent", getSelects[0].AttachID) != null) {
                    parentID = this.girdId_Upload.treegrid("getParent", getSelects[0].AttachID).AttachID;
                }
            }
        }

        this.parentID = parentID;
        this.uploadBtn.click();
    },

    // 下载单文件，或批量打包下载
    download: function (btnDownFile) {
        var othis = this;
        var choosed = othis.girdId_Upload.treegrid("getChecked");
        choosed = Enumerable.From(choosed).Where("x => x.AttachExt != '.'").ToArray();
        if (choosed.length == 0) {
            JQ.dialog.info("请勾选要批量下载的文件！");
            return;
        }
        if (choosed.length == 1) {
            JQ.taskUpload.Tools.downloadTemp(othis.buildDownloadUrl(choosed[0].AttachID, { type: "attach", name: choosed[0].AttachName, realName: choosed[0].AttachName }), "_self");
        }
        else {
            var $btn = btnDownFile;
            if ($btn.linkbutton('options').disabled == false) {
                //if (typeof (window.JinQu) == "undefined") {
                $btn.linkbutton("disable")[0].childNodes[0].childNodes[0].innerHTML = "打包中";
                var xml = GoldPM.loadXml("<Root></Root>")
                for (var i = 0; i < choosed.length; i++) {
                    var xFile = xml.createElement("File");
                    xml.documentElement.appendChild(xFile);
                    xFile.setAttribute("ID", choosed[i].AttachID);
                    if (typeof (choosed[i]._groupName) != "undefined") {
                        //保存分组名称，用于批量下载时创建对应的分组文件夹
                        xFile.setAttribute("Group", choosed[i]._groupName);
                    }
                    if (choosed[i].AttachID > 0) {
                        xFile.setAttribute("Type", "attach");
                    }
                    else {
                        xFile.setAttribute("RealName", choosed[i].AttachName);
                        xFile.setAttribute("Type", "temp");
                        xFile.appendChild(document.createTextNode(choosed[i].AttachName));
                    }
                }
                //console.log(xml.documentElement.outerHTML);
                $.ajax({
                    url: othis.batchDownloadUrl,
                    type: "POST",
                    data: { data: JQ.Flow.htmlEncode(xml.documentElement.outerHTML) },
                    success: function (result) {
                        if (result.Result == false) {
                            JQ.dialog.error(result.Message);
                            return;
                        }
                        JQ.taskUpload.Tools.downloadTemp(JQ.taskUpload.Tools.buildDownloadUrl(othis.downloadUrl, 0, { name: result.RemoteName, realName: encodeURIComponent(result.RealName) }));
                    },
                    error: function () {
                        JQ.dialog.error("批量下载失败！");
                    },
                    complete: function () {
                        $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                    }
                });
                //}
                //else {
                //    //使用客户端下载
                //    $btn.linkbutton("disable")[0].childNodes[0].childNodes[0].innerHTML = "下载中";
                //    var urls = [];
                //    var dirs = [];
                //    for (var i = 0; i < choosed.length; i++) {
                //        urls.push(window.location.protocol + "//" + window.location.host + JQ.taskUpload.Tools.buildDownloadUrl(othis.downloadUrl, choosed[i].AttachID, { type: "attach", name: choosed[i].AttachName, realName: choosed[i].AttachName }));
                //        dirs.push(choosed[i].AttachName);
                //    }
                //    window.JinQu.query("httpDownloadMulti", { url: urls, local: dirs },
                //        function (file, uploadSize, totalSize) {
                //        },
                //        function (response, request) {
                //            $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                //        }, function (error_code, error_message) {
                //            $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                //        }
                //    );
                //}
            }
        }
    },
    
    // 删除
    DeleteFiles : function () {
        var getChecks = this.girdId_Upload.treegrid("getChecked");
        if (getChecks.length == 0) {
            JQ.dialog.warning("请勾选要删除的项！！！");
            return false;
        }
        this.delFunc(getChecks);
    },
    
    delFunc : function (getChecks) {
        var othis = this;
        return JQ.dialog.confirm("确定要删除勾选项吗？", function () {
            var _idSet = Enumerable.From(getChecks).Select("x=>x.AttachID").ToArray();
            var Folder = Enumerable.From(getChecks).Where("x=>x.AttachExt=='.'").ToArray();
            //console.log(_idSet);
            $.each(Folder, function (i, e) {
                // 如果文件夹中包含不可处理的项，那么该文件夹就不可删除
                var children = othis.girdId_Upload.treegrid("getChildren", e.AttachID);
                //console.log("%s, %d,  %o", e.AttachName, e.AttachID, children);
                var checkboxf = Enumerable.From(children).Where("x => x.__checkbox == false").Count();
                if (checkboxf > 0) {
                    //console.log("x.__checkbox == false ： %d", e.AttachID);
                    _idSet = Enumerable.From(_idSet).Except([e.AttachID]).ToArray();
                }
            });
            // console.log(_idSet);
            var model = 0;
            if (othis.changeMore != null) {
                model = othis.changeMore.attr("mode");
            }

            $.ajax({
                url: othis.DeleteFileUrl,
                type: "POST",
                data: { idSet: "" + _idSet + "", mode: model },
                success: function (result) {
                    othis.girdId_Upload.treegrid("clearSelections");
                    othis.girdId_Upload.treegrid("clearChecked");
                    othis.girdId_Upload.treegrid("reload");
                },
                error: function () {
                    JQ.dialog.error("操作失败！");
                }
            });
        })
    }
};

JQ.taskUpload.Tools = {};

JQ.taskUpload.Tools.buildDownloadUrl = function (downloadUrl, id, options) {
    if (id <= 0) {
        return downloadUrl + "?id=0&name=" + options.name + "&realName=" + options.realName;
    }
    else {
        return downloadUrl + "?id=" + id + "&type=" + options.type;
    }
}

JQ.taskUpload.Tools.downloadTemp = function (url) {
    var _a = document.createElement("a");
    _a.setAttribute("href", url);
    document.body.appendChild(_a);
    _a.click();
    document.body.removeChild(_a);
}