"use strict"
if (typeof (JQ) == "undefined") {
    JQ = {};
}
if (typeof (JQ.Upload) == "undefined") {
    JQ.Upload = {};

    JQ.Upload.init = function (options) {
        this.tempID = 0;
        this.isUploaded = true;
        var i = 0;
        while (document.getElementById("btnChooseFiles" + i)) {
            i++;
        }
        var input = document.createElement("input");
        input.setAttribute("type", "button");
        input.setAttribute("id", "btnChooseFiles" + i);
        input.style.display = "none";
        options.container.appendChild(input);
        this.container = options.container;
        this.browser = "btnChooseFiles" + i;
        this.cancelUrl = options.cancelUrl;
        this.downloadUrl = options.downloadUrl;
        this.batchDownloadUrl = options.batchDownloadUrl;
        this.cacheID = options.cacheID;
        this.deleteTempUrl = options.deleteTempUrl;
        this.deleteUrl = options.deleteUrl;
        var _this = this;
        this.uploader = new plupload.Uploader({
            runtimes: "html5,flash,html4,silverlight",
            browse_button: _this.browser,
            chunk_size: "1mb",
            url: options.url,
            flash_swf_url: options.flash_swf_url,
            silverlight_xap_url: options.silverlight_xap_url,
            multi_selection: true,
            filters: {
                max_file_size: "20gb",
                prevent_duplicates: true
                //mime_types: [
                //        { title: "Image files", extensions: "jpg,gif,png" },
                //        { title: "Zip files", extensions: "zip" }
                //]
            },
            init: {
                FilesAdded: _this.filesAdded,
                BeforeUpload: _this.beforeUpload,
                UploadProgress: _this.uploadProgress,
                FileUploaded: _this.fileUploaded
            }
        });
        this.uploader.init();
        this.uploader.source = this;
        this.uploaders = [];
        this.cache = [];
    }
    //注册上传控件
    JQ.Upload.init.prototype.register = function (uploaderID, refID, refTable, mode) {
        if (!this.isExistsUploader(uploaderID)) {
            this.uploaders.push({ id: uploaderID, refID: refID, refTable: refTable, mode: mode });
        }
    }

    JQ.Upload.init.prototype.findUploader = function (uploaderID) {
        for (var i = 0; i < this.uploaders.length; i++) {
            if (this.uploaders[i].id == uploaderID) {
                return this.uploaders[i];
            }
        }
    }

    JQ.Upload.init.prototype.isExistsUploader = function (uploaderID) {
        if (this.findUploader(uploaderID)) {
            return true;
        }
        else {
            return false;
        }
    }

    //打开
    JQ.Upload.init.prototype.open = function (uploaderID, mode) {
        this.uploaderID = uploaderID;
        document.getElementById(this.browser).click();
    }

    //选择文件后
    JQ.Upload.init.prototype.filesAdded = function (up, files) {
        var gridID = "files_" + this.source.uploaderID;
        var uploader = this.source.findUploader(this.source.uploaderID);
        if (!uploader) {
            return;
        }
        var _this = this.source;
        var $grid = $("#" + gridID);
        for (var i = 0; i < files.length; i++) {
            this.source.tempID--;
            files[i].rowID = this.source.tempID;
            files[i].mode = uploader.mode;
            files[i].uploaderID = uploader.id;
            $grid.datagrid("appendRow", {
                ID: this.source.tempID,
                Name: files[i].name,
                Size: files[i].size,
                LastModifyDate: JQ.Flow.getDateTimeText(files[i].lastModifiedDate),
                UploadDate: null,
                FileID: files[i].id,
                Version: 0,
                UploadStatus: 1
            });
            var remove = document.getElementById("r_" + files[i].id);
            remove.setAttribute("gridid", gridID);
            remove.setAttribute("tempid", this.source.tempID);
            remove.setAttribute("fileid", files[i].id);
            remove.onclick = function () {
                var fileID = this.getAttribute("fileid");
                _this.uploader.removeFile(fileID);
                var _$g = $("#" + this.getAttribute("gridid"));
                _$g.datagrid("deleteRow", _$g.datagrid("getRowIndex", this.getAttribute("tempid")));
                $.ajax({
                    url: _this.cancelUrl,
                    type: "POST",
                    data: { fileID: fileID },
                    success: function () { }
                });
            }
        }
        this.source.isUploaded = false;
        up.start();
    }

    //上传文件之前
    JQ.Upload.init.prototype.beforeUpload = function (up, file) {
        var uploader = this.source.findUploader(this.source.uploaderID);
        if (!uploader) {
            return;
        }
        $("#toggle" + file.uploaderID).linkbutton("disable");
        up.settings.multipart_params = {
            refID: uploader.refID,
            refTable: uploader.refTable,
            fileID: file.id,
            lastModifiedTime: JQ.Flow.getDateTimeText(file.lastModifiedDate),
            parentID: 0,
            mode: file.mode
        };
    }

    //上传文件进度
    JQ.Upload.init.prototype.uploadProgress = function (up, file) {
        var progress = document.getElementById("p_" + file.id);
        if (progress) {
            //显示进度
            progress.childNodes[0].style.width = (file.percent * 0.6) + "px";
            progress.childNodes[1].innerHTML = "";
            progress.childNodes[1].appendChild(document.createTextNode(file.percent.toString() + "%"));
        }
    }

    //上传文件完成
    JQ.Upload.init.prototype.fileUploaded = function (up, file, info) {
        if (!info.response) {
            return;
        }
        var uploader = this.source.findUploader(file.uploaderID);
        if (!uploader) {
            return;
        }
        info = JQ.Flow.parseJson(info.response);
        var $grid = $("#files_" + file.uploaderID);
        var rowIndex = $grid.datagrid("getRowIndex", file.rowID);
        if (uploader.refID == "0") {
            //变更上传状态
            $grid.datagrid("updateRow", {
                index: rowIndex,
                row: {
                    Version: 1,
                    RemoteName: info.Name,
                    UploadStatus: 2,
                    UploadDate: info.UploadDate,
                    EmpName: decodeURIComponent(info.EmpName)
                }
            });
            //document.getElementById("p_" + file.id).parentNode.innerHTML = info.UploadDate;
            //document.getElementById("e_" + file.id).parentNode.innerHTML = decodeURIComponent(info.EmpName);
            var temp = document.getElementById("f_" + file.id).parentNode;
            temp.innerHTML = "";
            temp.appendChild(JQ.Flow.createElement("a", { href: JQ.Upload.Tools.buildDownloadUrl(this.source.downloadUrl, 0, { name: info.Name, realName: file.name }) }, null, file.name));
            this.source.addToCache(uploader.refTable, file.id, file.name, info.Name, JQ.Flow.getDateTimeText(file.lastModifiedDate));
        }
        else {
            var isIn = false;
            if (info.Status == 2) {
                up.removeFile(file.id);
                JQ.dialog.show("文件" + file.name + "未修改，无需保存！");
                isIn = true;
                $grid.datagrid("deleteRow", rowIndex);
            }
            else if (info.Version > 1 && file.mode == 0) {
                var rows = $grid.datagrid("getRows");
                for (var i in rows) {
                    if (rows[i].Name == file.name) {
                        isIn = true;
                        $grid.datagrid("deleteRow", rowIndex);
                        $grid.datagrid("updateRow", {
                            index: $grid.datagrid("getRowIndex", rows[i].ID),
                            row: {
                                ID: info.AttachID,
                                Version: info.Version,
                                UploadDate: info.UploadDate,
                                LastModifyDate: JQ.Flow.getDateTimeText(file.lastModifiedDate),
                                EmpName: decodeURIComponent(info.EmpName),
                                UploadStatus: 2,
                                Type: "attach"
                            }
                        });
                    }
                }
            }
            if (!isIn) {
                if (rowIndex > -1) {
                    if (file.mode == 0) {
                        //合并最新
                        $grid.datagrid("updateRow", {
                            index: rowIndex,
                            row: {
                                ID: info.AttachID,
                                Version: info.Version,
                                EmpName: decodeURIComponent(info.EmpName),
                                UploadDate: info.UploadDate,
                                UploadStatus: 2,
                                Type: "attach"
                            }
                        });
                    }
                    else {
                        $grid.datagrid("updateRow", {
                            index: rowIndex,
                            row: {
                                ID: info.AttachVersionID,
                                Version: info.Version,
                                EmpName: decodeURIComponent(info.EmpName),
                                UploadDate: info.UploadDate,
                                UploadStatus: 2,
                                Type: "attachversion"
                            }
                        });
                    }
                }
            }
        }
        this.source.setUploadFilesHTML(file.uploaderID);
        var rows = $grid.datagrid("getRows");
        for (var i in rows) {
            if (rows[i].UploadStatus == 1) {
                return;
            }
        }
        this.source.isUploaded = true;
        $("#toggle_" + file.uploaderID).linkbutton("enable");
    }

    //切换显示版本
    JQ.Upload.init.prototype.toggleVersion = function (element, uploaderID) {
        var uploader = this.findUploader(uploaderID);
        if (!uploader) {
            return;
        }
        element.childNodes[0].childNodes[0].innerHTML = "";
        if (element.getAttribute("mode") == "0") {
            element.setAttribute("mode", "1");
            uploader.mode = 1;
            element.childNodes[0].childNodes[0].appendChild(document.createTextNode("显示最新版本"));
        }
        else {
            element.setAttribute("mode", "0");
            uploader.mode = 0;
            element.childNodes[0].childNodes[0].appendChild(document.createTextNode("显示所有版本"));
        }
        $("#files_" + uploaderID).datagrid("clearSelections").datagrid("load", { refID: uploader.refID, refTable: uploader.refTable, mode: element.getAttribute("mode") });
    }

    //加入缓存中
    JQ.Upload.init.prototype.addToCache = function (refTable, fileID, displayName, remoteName, lastModifiedTime, realUploadTime) {
        var _cache = null;
        if (!this.cache[refTable]) {
            this.cache[refTable] = [];
        }
        var data = { fileID: fileID, RemoteName: remoteName, fileName: displayName, lastModifiedTime: lastModifiedTime };
        if (realUploadTime) {
            data.realUploadTime = realUploadTime;
        }
        this.cache[refTable].push(data);
        this.saveCache();
    }

    //从缓存中获取文件名称数据
    JQ.Upload.init.prototype.getNamesFromCache = function (refTable, files) {
        if (!this.cache[refTable]) {
            return;
        }
        var result = [];
        for (var i = 0; i < this.cache[refTable].length; i++) {
            for (var j = 0; j < files.length; j++) {
                if (this.cache[refTable][i].fileID == files[j]) {
                    result.push(this.cache[refTable][i].RemoteName);
                    break;
                }
            }
        }
        return result;
    }

    //从缓存中删除
    JQ.Upload.init.prototype.deleteFromCache = function (refTable, files) {
        if (!this.cache[refTable] || files.length == 0) {
            return;
        }
        for (var i = 0; i < this.cache[refTable].length; i++) {
            for (var j = 0; j < files.length; j++) {
                if (this.cache[refTable][i].fileID == files[j]) {
                    this.uploader.removeFile(this.cache[refTable][i].fileID);
                    this.cache[refTable].splice(i, 1);
                    i--;
                    break;
                }
            }
        }
        this.saveCache();
    }

    //保存缓存
    JQ.Upload.init.prototype.saveCache = function () {
        var cache = null;
        for (var i = 0; i < this.container.childNodes.length; i++) {
            if (!this.container.childNodes[i].tagName) {
                continue;
            }
            if (this.container.childNodes[i].getAttribute("name") == this.cacheID) {
                cache = this.container.childNodes[i];
                break;
            }
        }
        if (!cache) {
            cache = JQ.Flow.createElement("input", { name: this.cacheID, type: "hidden" });
            this.container.appendChild(cache);
        }
        var xml = GoldPM.loadXml("<Root></Root>");
        for (var i in this.cache) {
            var xfiles = xml.createElement("Files");
            xml.documentElement.appendChild(xfiles);
            xfiles.setAttribute("RefTable", i);
            for (var j = 0; j < this.cache[i].length; j++) {
                var xfile = xml.createElement("File");
                xfiles.appendChild(xfile);
                xfile.setAttribute("FileName", this.cache[i][j].fileName);
                xfile.setAttribute("LastModifiedTime", this.cache[i][j].lastModifiedTime);
                if (this.cache[i][j].realUploadTime) {
                    xfile.setAttribute("RealUploadTime", this.cache[i][j].realUploadTime);
                }
                xfile.appendChild(document.createTextNode(this.cache[i][j].RemoteName));
            }
        }
        cache.setAttribute("value", JQ.Flow.htmlEncode(xml.documentElement.outerHTML));
    }

    //设置上传文件的HTML（主要为导出成 word 使用）
    JQ.Upload.init.prototype.setUploadFilesHTML = function (uploaderID) {
        var datas = $("#files_" + uploaderID).datagrid("getData").rows;
        var text = "";
        for (var i in datas) {
            if (i != 0) {
                text += "<br />";
            }
            text += (datas[i].Version > 0 ? ("(" + datas[i].Version.toString() + ")") : "") + datas[i].Name;
        }
        document.getElementById(uploaderID + "_bookmark").innerHTML = "";
        document.getElementById(uploaderID + "_bookmark").appendChild(document.createTextNode(text));
    }

    //下载文件
    JQ.Upload.init.prototype.download = function (uploaderID) {
        var $grid = $("#files_" + uploaderID);
        var choosed = $grid.datagrid("getChecked");
        if (choosed.length == 0) {
            return;
        }
        if (choosed.length == 1) {
            JQ.Upload.Tools.downloadTemp(JQ.Upload.Tools.buildDownloadUrl(this.downloadUrl, choosed[0].ID, { type: choosed[0].Type, name: choosed[0].RemoteName, realName: choosed[0].Name }), "_self");
        }
        else {
            var $btn = $("#download_" + uploaderID);
            if (typeof (window.JinQu) == "undefined") {
                $btn.linkbutton("disable")[0].childNodes[0].childNodes[0].innerHTML = "打包中";
                var xml = GoldPM.loadXml("<Root></Root>")
                for (var i = 0; i < choosed.length; i++) {
                    var xFile = xml.createElement("File");
                    xml.documentElement.appendChild(xFile);
                    xFile.setAttribute("ID", choosed[i].ID);
                    if (choosed[i].ID > 0) {
                        xFile.setAttribute("Type", choosed[i].Type);
                    }
                    else {
                        xFile.setAttribute("RealName", choosed[i].Name);
                        xFile.setAttribute("Type", "temp");
                        xFile.appendChild(document.createTextNode(choosed[i].RemoteName));
                    }
                }
                $.ajax({
                    url: this.batchDownloadUrl,
                    type: "POST",
                    data: { data: JQ.Flow.htmlEncode(xml.documentElement.outerHTML) },
                    success: function (result) {
                        if (result.Result == false) {
                            JQ.dialog.error(result.Message);
                            return;
                        }
                        JQ.Upload.Tools.downloadTemp(JQ.Upload.Tools.buildDownloadUrl(this.downloadUrl, 0, { name: result.RemoteName, realName: choosed[0].encodeURIComponent(result.RealName) }));
                    },
                    error: function () {
                        JQ.dialog.error("批量下载失败！");
                    },
                    complete: function () {
                        $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                    }
                });
            }
            else {
                //使用客户端下载
                $btn.linkbutton("disable")[0].childNodes[0].childNodes[0].innerHTML = "下载中";
                var urls = [];
                var dirs = [];
                for (var i in choosed) {
                    urls.push(window.location.protocol + "//" + window.location.host + JQ.Upload.Tools.buildDownloadUrl(this.downloadUrl, choosed[i].ID, { type: choosed[i].Type, name: choosed[i].RemoteName, realName: choosed[i].Name }));
                    dirs.push(choosed[i].Name);
                }
                window.JinQu.query("httpDownloadMulti", { url: urls, local: dirs },
                    function (file, uploadSize, totalSize) { },
                    function (response, request) {
                        $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                    }, function (error_code, error_message) {
                        $btn.linkbutton("enable")[0].childNodes[0].childNodes[0].innerHTML = "下载";
                    }
                );
            }
        }
    }

    //删除选中文件
    JQ.Upload.init.prototype.delete = function (uploaderID) {
        var $grid = $("#files_" + uploaderID)
        var selections = $grid.datagrid("getChecked");
        if (selections.length == 0) {
            return;
        }
        var _this = this;
        JQ.dialog.confirm("确定要删除选中的文件吗？", function () {
            var uploader = _this.findUploader(uploaderID);
            if (!uploader) {
                return;
            }
            $grid.datagrid("loading");
            if (uploader.refID == "0") {
                var fileIDSet = [];
                var idSet = [];
                for (var i in selections) {
                    fileIDSet.push(selections[i].FileID);
                    idSet.push(selections[i].ID);
                }
                $.ajax({
                    url: _this.deleteTempUrl,
                    type: "POST",
                    data: { nameSet: _this.getNamesFromCache(uploader.refTable, fileIDSet).join(",") },
                    success: function (result) {
                        _this.deleteFromCache(uploader.refTable, fileIDSet);
                        //删除选中行
                        for (var i in idSet) {
                            $grid.datagrid("deleteRow", $grid.datagrid("getRowIndex", idSet[i]));
                        }
                    },
                    error: function () {
                        JQ.dialog.error("操作失败！");
                    }, complete: function () {
                        $grid.datagrid("loaded");
                    }
                });
            }
            else {
                var idSet = "";
                var fileIDSet = [];
                for (var i = 0; i < selections.length; i++) {
                    if (i != 0) {
                        idSet += ",";
                    }
                    idSet += selections[i].ID;
                    selections[i].FileID && fileIDSet.push(selections[i].FileID);
                }
                $.ajax({
                    url: _this.deleteUrl,
                    type: "POST",
                    data: { idSet: idSet, mode: uploader.mode },
                    success: function (result) {
                        for (var i in fileIDSet) {
                            _this.uploader.removeFile(fileIDSet[i]);
                        }
                        $grid.datagrid("reload");
                    },
                    error: function () {
                        $grid.datagrid("loaded");
                        JQ.dialog.error("操作失败！");
                    }
                });
            }
        });
    }

    JQ.Upload.getFileSizeText = function (size) {
        if (!size && size != 0) {
            return "";
        }
        var st = "";
        if (size >= 1024 * 1024 * 1024) {
            st = (size / 1024 / 1024 / 1024).toFixed(2) + "GB";
        } else if (size >= 1024 * 1024) {
            st = (size / 1024 / 1024).toFixed(2) + "MB";
        } else if (size >= 1024) {
            st = (size / 1024).toFixed(2) + "KB";
        } else {
            st = size + "B";
        }
        return st;
    }

    JQ.Upload.Tools = {};

    JQ.Upload.Tools.buildDownloadUrl = function (downloadUrl, id, options) {
        if (id <= 0) {
            return downloadUrl + "?id=0&name=" + options.name + "&realName=" + options.realName;
        }
        else {
            return downloadUrl + "?id=" + id + "&type=" + options.type;
        }
    }

    JQ.Upload.Tools.downloadTemp = function (url) {
        var _a = document.createElement("a");
        _a.setAttribute("href", url);
        document.body.appendChild(_a);
        _a.click();
        document.body.removeChild(_a);
    }
}
