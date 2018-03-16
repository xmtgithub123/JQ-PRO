var JQ = JQ || {};

//下拉列表隐藏面板
function comBoxHidePanel(control) {
    var _options = $(control).combobox('options');
    var _data = $(control).combobox('getData');/* 下拉框所有选项 */
    var _value = $(control).combobox('getValue');/* 用户输入的值 */
    var _b = false;/* 标识是否在下拉列表中找到了用户输入的字符 */
    for (var i = 0; i < _data.length; i++) {
        if (_data[i][_options.valueField] == _value) {
            _b = true;
            break;
        }
    }
    if (!_b) {
        $(control).combobox('setValue', '');
    }
}

//金曲combobox
JQ.combobox = {
    //为easyui的combobox默认项加载数据
    ajaxByCombobox: function (parames) {
        var Json = [];
        $.ajax({
            type: JQ.tools.isNotEmpty(parames.type) ? parames.type : 'POST',
            url: parames.url,
            cache: false,
            async: false,
            dataType: 'json',
            data: parames.data,
            success: function (backJson) {
                if (typeof (backJson) == 'string') {
                    Json = eval("(" + backJson + ")");
                }
                else {
                    Json = backJson;
                }

            }
        });
        if (parames.pusuParames) {
            Json.unshift(parames.pusuParames);
        }
        return Json;
    },
    selEmp: function (parames) {
        parames = $.extend({
            id: null//控件的ID
        }, parames);
        var $c = JQ.tools.findCurControl(parames.id),
            val = $c.attr("JQvalue");
        $c.combobox({
            width: parames.width != undefined ? parames.width:180,
            valueField: 'EmpID',
            textField: 'EmpName',
            groupField: 'EmpDepName',
            required: parames.required != undefined?parames.required:true,
            validType: 'comboboxVali[true]',//是否做完整型验证
            url: window.top.rootPath + '/Core/usercontrol/employeeJson',
            prompt: '请选择人员',
            groupFormatter: function (group) {
                // alert(group);
                return '<b style="color:black">' + group + '</b>';
            },
            onLoadSuccess: function () {
                if (JQ.tools.isNotEmpty(val)) {
                    $(this).combobox('setValue', val);
                }
            },
            onHidePanel: function () {
                comBoxHidePanel(this);
            }
        });
    },

    selQtEmp: function (parames) {
        parames = $.extend({
            id: null//控件的ID
        }, parames);
        var $c = JQ.tools.findCurControl(parames.id),
            val = $c.attr("JQvalue");
        $c.combobox({
            width: parames.width != undefined ? parames.width:180,
            valueField: 'EmpID',
            textField: 'EmpName',
            groupField: 'EmpDepName',
            required: true,
            validType: 'comboboxVali[true]',//是否做完整型验证
            url: window.top.rootPath + '/Core/usercontrol/employeeQtJson',
            prompt: '请选择人员',
            groupFormatter: function (group) {
                return '<b style="color:black">' + group + '</b>';
            },
            onLoadSuccess: function () {
                if (JQ.tools.isNotEmpty(val)) {

                    $(this).combobox('setValue', val);
                }
            }
        });
    },

    selEmpEx: function (parames) {
        parames = $.extend({
            id: null//控件的ID
        }, parames);
        var $c = JQ.tools.findCurControl(parames.id),
            val01 = $c.attr("value");
        val = $c.attr("JQvalue");
        $c.combobox({
            width: parames.width != undefined ? parames.width:180,
            valueField: 'EmpID',
            textField: 'EmpName',
            groupField: 'EmpDepName',
            required: true,
            validType: 'comboboxVali[true]',//是否做完整型验证
            url: window.top.rootPath + '/Core/usercontrol/employeeJson',
            prompt: '请选择人员',
            groupFormatter: function (group) {
                //  alert(group);
                return '<b style="color:black">' + group + '</b>';
            },
            onLoadSuccess: function () {
                //  alert(val01);
                if (JQ.tools.isNotEmpty(val)) {
                    $(this).combobox('setValue', val);
                }
                if (val01 == 0) {
                    $(this).combobox('setValue', "");
                }


            }
        });
    },

    common: function (parames) {
        parames = $.extend({
            url: null,//请求地址
            id: null,//控件的ID
            type: 'dataGrid',//控件的类型dataGrid,dataTree,combobox,
            valueField: '',
            textField: '',
            pagination: false//是否分页
        }, parames);
        var $c = JQ.tools.findCurControl(parames.id);
        if (parames.type == "dataGrid") {
            var onSelect = parames.onSelect,
                onUnselect = parames.onUnselect,
                onLoadSuccess = parames.onLoadSuccess,
                onBeforeLoad = parames.onBeforeLoad;
            parames.idField = parames.valueField; //ID字段
            parames.striped = true;
            parames.pageList = [5, 10, 20, 30, 50, 100, 200];
            parames.onUnselect = function (rowIndex, rowData) {
                var sels = $c.combogrid('grid').datagrid('getSelections');
                sels = JQ.tools.arrFindS(sels, parames.valueField);
                sels = JQ.tools.delArr(sels, rowData[parames.valueField]);
                $c.combogrid('setValues', sels);
                if (JQ.tools.isExitsFunction(onUnselect)) {//如果有自定义的先运行自定义的函数
                    onUnselect(rowIndex, rowData);
                }
            };
            parames.onSelect = function (rowIndex, rowData) {
                var sels = $c.combogrid('grid').datagrid('getSelections');
                sels = JQ.tools.arrFindS(sels, parames.valueField);
                sels.push(rowData[parames.valueField]);
                $c.combogrid('setValues', sels);
                if (JQ.tools.isExitsFunction(onSelect)) {//如果有自定义的先运行自定义的函数
                    onSelect(rowIndex, rowData);
                }
            };
            parames.onBeforeLoad = function (param) {//请求之前事件
                JQ.datagrid.selectFiled({ $dg: $c.combogrid('grid'), arr: parames.JQFields, queryParams: param });
                //if (JQ.tools.isExitsFunction(onBeforeLoad)) {//如果有自定义的先运行自定义的函数
                //    onBeforeLoad(param);
                //}
            };
            parames.keyHandler = function ()//禁用上下移动键和回车键
            {
                up = function () {
                    return false;
                },
                    down = function () {
                        return false;
                    },
                    enter = function () {
                        return false;
                    }
            };
            $c.combogrid(parames);

            if (parames.pagination) {
                var pager = $c.combogrid('grid').datagrid('getPager');
                if (pager) {
                    $(pager).pagination({
                        afterPageText: '{pages}页',
                        displayMsg: '共{total}条'
                    });
                }
            }
            if (JQ.tools.isJson(parames.JQSearch)) {
                parames.JQSearch.$dg = $c.combogrid("grid"), //parames.JQID;
                    JQ.textbox.search(parames.JQSearch);
            }
        };
    },

    getValues: function (parames) {
        parames = $.extend({
            id: null,//控件的ID
            type: 'combo'  //combo,combobox,combogrid,combotree
        }, parames);
        var result, $c = JQ.tools.findCurControl(parames.id);// $("#" + parames.id);
        switch (parames.type) {
            case "combo":
                result = $c.combo('getValues');
                break;
            case "combobox":
                result = $c.combobox('getValues');
                break;
            case "combogrid":
                result = $c.combogrid('getValues');
                break;
            case "combotree":
                result = $c.combotree('getValues');
                break;
        }
        return result;
    }
};


//扩展验证方法
$.extend($.fn.validatebox.defaults.rules, {
    comboboxVali: {
        validator: function (value, param) {//param为默认值
            if (JQ.tools.isNotEmpty(value)) {
                if (JQ.tools.isArray(param) && param[0]) {
                    var $c = $(this).parent().prev(),
                        opts = $c.combobox('options'),
                        data = $c.combobox('getData'),
                        textField = opts.textField,
                        vals = JQ.tools.arrFind(data, textField, value);
                    if (JQ.tools.isArray(vals)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        },
        message: '请选择内容'
    }
});