
JQ.DesFlow = {};

JQ.DesFlow.show = function (option) {
    option = $.extend({
        Ctl: '',
        FlowData: [],
        setNode: true,
        JustSetDesignNode: false,
        DesignNodeType: 19
    }, option);

    var $ContrainCtl = $(option.Ctl);
    
    var FlowNodeData = Enumerable.From(option.FlowData).OrderBy(function (x) { return x.rownum.toInt(); }).ToArray();
    $.each(FlowNodeData, function (i, e) {
        $("<div class=\"approve-node node-status-" + (option.setNode ? (option.JustSetDesignNode == true ? (e.FlowNodeTypeID == option.DesignNodeType ? e.FlowNodeStatus : '') : e.FlowNodeStatus) : "") + "\" title=\"此节点状态为" + getStatusTitle(e.FlowNodeStatus) + "\"><span><span class=\"bold\">" + e.FlowNodeName + ":</span>" + (e.FlowNodeEmpName == undefined ? "" : e.FlowNodeEmpName) + "</span></div>").appendTo($ContrainCtl);
    })
}


function getStatusTitle(typeId) {
    var title = "";
    switch (typeId) {
        case "0":
            title = "未安排";
            break;
        case "1":
            title = "已安排";
            break;
        case "2":
            title = "进行中";
            break;
        case "3":
            title = "已完成";
            break;
        case "4":
            title = "停止";
            break;
        case "5":
            title = "回退";
            break;
    }
    return title;
}
