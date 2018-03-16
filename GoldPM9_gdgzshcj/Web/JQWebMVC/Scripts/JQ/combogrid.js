var JQ = JQ || {};
JQ.combogrid = {

    selEmp: function (parames) {
        parames = $.extend({
            id: null,//控件的ID
            isMult: false,//是否支持多选(暂时不支持)
            width: '180',
            height: '400'
        }, parames);


        $('#' + parames.id).combogrid({

            panelWidth: parames.width,
            panelHeight: parames.height,
            idField: 'Id',        //ID字段
            textField: 'CustName',    //显示的字段
            url: window.top.rootPath + parames.url,
            fitColumns: true,
            striped: true,
            editable: true,
            pagination: true,           //是否分页
            rownumbers: true,           //序号
            collapsible: false,         //是否可折叠的
            fit: true,                  //自动大小
            method: 'post',
            columns: [[
                        { field: 'CustName', title: '单位名称', width: 150 },
                        { field: 'CustAddress', title: '单位地址', width: 150 }
            ]],
            keyHandler: {
                up: function () {

                    var selected = $('#' + parames.id).combogrid('grid').datagrid('getSelected');
                    if (selected) {
                        var index = $('#' + parames.id).combogrid('grid').datagrid('getRowIndex', selected);
                        if (index > 0) {
                            $('#' + parames.id).combogrid('grid').datagrid('selectRow', index - 1);
                        }
                    } else {
                        var rows = $('#' + parames.id).combogrid('grid').datagrid('getRows');
                        $('#' + parames.id).combogrid('grid').datagrid('selectRow', rows.length - 1);
                    }
                },
                down: function () {

                    var selected = $('#' + parames.id).combogrid('grid').datagrid('getSelected');
                    if (selected) {
                        var index = $('#' + parames.id).combogrid('grid').datagrid('getRowIndex', selected);
                        if (index < $('#' + parames.id).combogrid('grid').datagrid('getData').rows.length - 1) {
                            $('#' + parames.id).combogrid('grid').datagrid('selectRow', index + 1);
                        }
                    } else {
                        $('#' + parames.id).combogrid('grid').datagrid('selectRow', 0);
                    }
                },
                enter: function () {
                    var row = $('#' + parames.id).combogrid('grid').datagrid('getSelected');
                    $('#' + parames.id).combogrid('hidePanel');

                    if (typeof (parames.isUpdateOther) != "undefined") {
                        window.onEnterFunction.call();
                    }
                },
                query: function (keyword) {
                    var queryParams = $('#' + parames.id).combogrid("grid").datagrid('options').queryParams;
                    queryParams.keyword = keyword;
                    $('#' + parames.id).combogrid("grid").datagrid('options').queryParams = queryParams;
                    var pager = $('#' + parames.id).combogrid('grid').datagrid('getPager');
                    pager.pagination('refresh');
                    pager.pagination('select', 1);
                    $('#' + parames.id).combogrid("grid").datagrid("reload");
                    $('#' + parames.id).combogrid("setValue", keyword);

                    if (typeof (parames.isUpdateOther) != "undefined") {
                        window.onQueryFunction.call();
                    }

                }
            },
            onSelect: function () {
                var row = $('#' + parames.id).combogrid('grid').datagrid('getSelected');
               // debugger;
                if (typeof (parames.isUpdateOther) != "undefined") {
                    window.onSelectFunction.call();
                }
            }
        });
    }

}
