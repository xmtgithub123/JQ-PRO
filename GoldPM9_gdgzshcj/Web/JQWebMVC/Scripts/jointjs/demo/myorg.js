// 首先是定义画板和画布，这里重写了ElementView和LinkView，目的是为了让画出来的流程图不能被删除和编辑

var graph = new joint.dia.Graph();

var ElementView = joint.dia.ElementView.extend({
	pointerdown: function () {
		this._click = true;
		joint.dia.ElementView.prototype.pointerdown.apply(this, arguments);
	},
	pointermove: function(evt, x, y) {
		this._click = false;
		joint.dia.ElementView.prototype.pointermove.apply(this, arguments);
	},
	pointerup: function (evt, x, y) {
		if (this._click) {
			// triggers an event on the paper and the element itself
			this.notify('cell:click', evt, x, y); 
		} else {
			joint.dia.ElementView.prototype.pointerup.apply(this, arguments);
		}
	}
});
var LinkView = joint.dia.LinkView.extend({
	addVertex: function(evt, x, y) {},
	removeVertex: function(endType) {},
	pointerdown:function(evt, x, y) {}
});

// 定义画布
var paper = new joint.dia.Paper({
	//el: $('#paper'),
	width: 1200,
	height: 600,
	gridSize: 1,
	model: graph,
	elementView: ElementView,
	linkView:LinkView
});

var paperScroller = new joint.ui.PaperScroller({
    paper: paper,
    autoResizePaper: true
});

paper.on('blank:pointerdown', paperScroller.startPanning);
paperScroller.$el.css({ width: '100%', height: '100%' }).appendTo('#paper');

var graphLayout = new joint.layout.TreeLayout({
    graph: graph,
    direction: 'R',
	gap: 60,
	siblingGap: 40
});

var treeLayoutView = new joint.ui.TreeLayoutView({
    paper: paper,
    model: graphLayout,
    previewAttrs: {
        parent: { rx: 10, ry: 10 }
    }
});


// paper.$el.css('pointer-events', 'none')//去除默认样式，使所有事件不可用





// 然后我写了两个函数分别用来创建形状和连线，这样写可以减少代码量，官方的demo也大都是这样写的

//定义形状
var state = function(x, y, shape, background, text){
	var cell;
	if(shape==="rect"){
		cell = new joint.shapes.basic.Rect({
			//position: { x: x, y: y },//坐标
			size: { width: 140, height: 40 },//宽高
			attrs: { 
				rect: {
					fill: {
						type: 'linearGradient',
						stops: [
							{ offset: '0%', color: background },//渐变开始
							{ offset: '100%', color: '#fe8550' }//渐变结束
						],
						attrs: { x1: '0%', y1: '0%', x2: '0%', y2: '100%' }
					},
					stroke: background,//边框颜色
					'stroke-width': 1//边框大小
				},
				text: { text: text } //显示文字
			}
		});
	} else if(shape==="ellipse"){
		cell = new joint.shapes.basic.Ellipse({
			//position: { x: x, y: y },//坐标
			size: { width: 140, height: 40 },//宽高
			attrs: { 
				ellipse: {
					fill: {
						type: 'linearGradient',
						stops: [
							{ offset: '0%', color: background },//渐变开始
							{ offset: '100%', color: '#FFFFFF' }//渐变结束
						],
						attrs: { x1: '0%', y1: '0%', x2: '0%', y2: '100%' }
					},
					stroke: background,//边框颜色
					'stroke-width': 1//边框大小
				},
				text: { text: text } //显示文字
			}
		});
	}
	graph.addCell(cell);
	return cell;
};

//定义连线
function link(source, target, label){
	var cell = new joint.dia.Link({ 
		source: { id: source.id },
		target: { id: target.id },
		labels: [{ position: 0.5, attrs: { text: { text: label || '', 'font-weight': 'bold' } } }],
//		router: { name: 'manhattan' },//设置连线弯曲样式 manhattan直角
//		attrs: {
//			'.connection': {
//				stroke: '#333333',//连线颜色
//				'stroke-width': 2//连线粗细
//			},
//			'.marker-target': {
//				fill: '#333333',//箭头颜色
//				d: 'M 10 0 L 0 5 L 10 10 z'//箭头样式
//			}
//		}
	});
	graph.addCell(cell);
	return cell;
}



// 最后就是我们实际的业务代码了，这里我们可以整理一下数据结构，把数据定义成json格式，然后写一个函数通过json直接生成流程图，当然坐标需要寻找规律自己计算一下

//创建元素
var start = state(500,100,"ellipse","#00FFFF", "视频播放成功率");
var state1 = state(500,200,"rect","#f7a07b", "GET响应成功率");
var state2 = state(400,300,"rect","#f7a07b", "HTTP错误码分析");
var state3 = state(600,300,"rect","#f7a07b", joint.util.breakText("TCP异常和其他原因",{width:80}));
var state4 = state(400,400,"rect","#f7a07b", "4XX、5XX分析");
var state5 = state(600,400,"rect","#f7a07b", "接口以上分析");
var state6 = state(750,400,"rect","#f7a07b", "接口以下分析");

// 创建连线
link(start, state1, "");
link(state1, state2, "≥70%");
link(state1, state3, "<70%");
link(state2, state4, "");
link(state3, state5, "是");
link(state3, state6, "否");


// 自动布局

graphLayout.layout();
paperScroller.zoom(0);
paperScroller.centerContent();



// 下面是给每个元素（不包含连线）添加了一个点击事件，弹出一个模态框，显示当前点击的内容。
paper.on('cell:click', function (e) {
//	$("#detailModal .modal-body").html("");
//	var arr = $("#"+e.id+" tspan");
//	if(arr.length===1){
//		$("#detailModal .modal-body").append($(arr).html());
//		$("#detailModal").modal();
//	} else{
//		var tmp="";
//		$.each(arr, function(k,v){
//			tmp+=$(v).html();
//		});
//		$("#detailModal .modal-body").append(tmp);
//		$("#detailModal").modal();
//	}
	alert(e.id);
});