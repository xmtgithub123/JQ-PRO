var JQ = JQ || {};
JQ.textbox = {
    selEmp: function (parames) {
        parames = $.extend({
            id: null,//控件的ID
            isMult: true,//是否支持多选(暂时不支持)
            setID: null,//选择后需要赋值的控件ID
        }, parames);
        var $dg = JQ.tools.findCurControl(parames.id);
        $dg.attr("JQControl","SelectEmp").textbox({
            buttonText: '选择',
            prompt: '请选择人员',
            buttonIcon: 'fa fa-search',
            height: 25,
            width: parames.width || 350,
            editable: false,
            onClickButton: function () {          
                JQ.dialog.dialog({
                    url: window.top.rootPath+ '/Core/usercontrol/selEmployee?isMult=' + parames.isMult + '&id=' + parames.id + '&setID=' + parames.setID + '&vals=' + JQ.tools.findCurControl(parames.setID).val(),
                    width: 450,
                    height: '85%',
                    title: '请选择人员'
                });
            }
        });
    },
    search: function (parames) {
        parames = $.extend({
            id: null,
            dgID: null,
            $dg: null,
            prompt: null,
            loadingType: 'datagrid',
            $panel: null
        }, parames);
        var $c;
        if (parames.$panel != null) {
            $c = parames.$panel.find("#" + parames.id);
        }
        else {
            $c = $("#" + parames.id);
        }
        $c.textbox({
            buttonText: '筛选',
            buttonIcon: 'fa fa-search',
            height: 26,
            prompt: parames.prompt,
            onClickButton: function () {
                JQ.datagrid.searchGrid(parames);
            }
        });
        //回车后自动筛选
        $c.textbox('textbox').keydown(function (e) {
            if (e.keyCode == 13) {
                JQ.datagrid.searchGrid(parames);
            }
        });
    },

   
};