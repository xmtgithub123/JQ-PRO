var JQ = JQ || {};
//金曲combotree
JQ.combotree = {
    //获取combotree选中的值
    geCheckedVals: function (parames) {
        parames = $.extend({
            treeID: null,
            $tree: null
        }, parames);
        var arr = [], s = '', $c = JQ.tools.findCurControl(parames.treeID);
        if (parames.$tree == null) {
            arr = $c.combotree("getValues");
        }
        else {
            arr = parames.$tree.combotree("getValues");
        }
        if (JQ.tools.isNotEmpty) {
            for (var i = 0; i < arr.length; i++) {
                if (s != '') s += ',';
                s += arr[i];
            }
        }
        return s;
    },

    //获取combotree选中的TEXT
    geCheckedTexts: function (parames) {
        parames = $.extend({
            treeID: null,
            $tree: null
        }, parames);
        var texts = '',
           $c = JQ.tools.findCurControl(parames.treeID);
        if (parames.$tree == null) {
            texts = $c.combotree("getText");
        }
        else {
            texts = parames.$tree.combotree("getText");
        }
        return texts;
    },

    selEmp: function (parames) {
        parames = $.extend({
            id: null,//控件的ID
            isMult: false,//是否支持多选(暂时不支持)
            width: '180',
        }, parames);
        var $c = JQ.tools.findCurControl(parames.id);
        $c.combotree({
            width: parames.width,
            animate: true,
            onlyLeafCheck: true,
            cascadeCheck: false,
            multiple: parames.isMult,
            url: window.top.rootPath + '/Core/usercontrol/employeeByDepJson?isDialog=false',
            prompt: '请选择人员',
            onClick: function (node) {
                var tree = $(this).tree;
                var isLeaf = tree('isLeaf', node.target);
                if (!isLeaf) {   //选中的节点是否为叶子节点,如果不是叶子节点,清除选中  
                    $('#' + parames.id).combotree('showPanel');
                    $('#' + parames.id).combotree('clear');
                }
            }
        });
    }
};