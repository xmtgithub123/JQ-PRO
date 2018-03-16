joint.shapes.org.Member.prototype.markup = [
    '<g class="rotatable">',
    '<g class="scalable">',
    '<rect class="card"/><image/>',
    '</g>',
    '<text class="rank"/><text class="name"/>',
    '<g class="btn add"><circle class="add"/><text class="add">+</text></g>',
    '<g class="btn del"><circle class="del"/><text class="del">-</text></g>',
    '<g class="btn edit"><rect class="edit"/><text class="edit">EDIT</text></g>',
    '</g>'
].join('');

var member = function(rank, name, image, background, textColor) {

    textColor = textColor || "#000";

    var cell = new joint.shapes.org.Member({
        size: { width: 260, height: 90 },
        attrs: {
            '.card': { fill: background, 'stroke-width': 0 },
            image: { 'xlink:href': image, 'ref-y': 10, opacity: 0.7 },
            '.rank': { fill: textColor, text: '', 'font-size': 13, 'text-decoration': 'none', 'ref-x': 0.95, 'ref-y': 0.5, 'y-alignment': 'middle', 'word-spacing': '-5px', 'letter-spacing': 0 },
            '.name': { fill: textColor, text: '', 'ref-x': 0.95, 'ref-y': 0.62, 'font-family': 'Arial' },
            '.btn.add': { 'ref-dx': -15,'ref-y': 15, 'ref': '.card' },
            '.btn.del': { 'ref-dx': -45,'ref-y': 15, 'ref': '.card' },
            '.btn.edit': { 'ref-dx': -140,'ref-y': 5, 'ref': '.card' },
            '.btn>circle': { r: 10, fill: 'transparent', stroke: '#333', 'stroke-width': 1 },
            '.btn>rect': { height: 20, width: 45, rx: 2, ry: 2, fill: 'transparent', 'stroke-width': 1 },
            '.btn.add>text': { fill: textColor,'font-size': 23, 'font-weight': 800, stroke: "#000", x: -6.5, y: 8, 'font-family': 'Times New Roman' },
            '.btn.del>text': { fill: textColor,'font-size': 28, 'font-weight': 500, stroke: "#000", x: -4.5, y: 6, 'font-family': 'Times New Roman' },
            '.btn.edit>text': { fill: textColor,'font-size': 15, 'font-weight': 500, stroke: "#000", x: 5, y: 15, 'font-family': 'Sans Serif' }
        }
    }).on({
        'change:name': function(cell, name) {
            cell.attr('.name/text', joint.util.breakText(name, { width: 160, height: 45 }, cell.attr('.name')));
        },
        'change:rank': function(cell, rank) {
            cell.attr('.rank/text', joint.util.breakText(rank, { width: 165, height: 45 }, cell.attr('.rank')));
        }
    }).set({
        name: name,
        rank: rank
    });


    return cell;
};

function link(source, target) {

    //var cell = new joint.shapes.org.Arrow({
    var cell = new joint.dia.Link({
        source: { id: source.id },
        target: { id: target.id }
    });

    return cell;
}

var members = [
    member('Founder & Chairman', 'Pierre Omidyar', 'male.png', '#31d0c6').position(100,350),
    member('President & CEO', 'Margaret C. Whitman', 'female.png', '#31d0c6'),
    member('President, PayPal', 'Scott Thompson', 'male.png', '#7c68fc'),
    member('President, Ebay Global Marketplaces' , 'Devin Wenig', 'male.png', '#7c68fc'),
    member('Senior Vice President Human Resources', 'Jeffrey S. Skoll', 'male.png', '#fe854f'),
    member('Senior Vice President Controller', 'Steven P. Westly', 'male.png', '#feb663')
];

var connections = [
    link(members[0], members[1]),
    link(members[1], members[2]),
    link(members[1], members[3]),
    link(members[1], members[4]),
    link(members[1], members[5])
];

var graph = new joint.dia.Graph();

var paper = new joint.dia.Paper({
    //el: $('#paper'),
    width: 1000,
    height: 1000,
    gridSize: 1,
    model: graph,
    interactive: false,
    defaultLink: new joint.shapes.org.Arrow()
});

var paperScroller = new joint.ui.PaperScroller({
    paper: paper,
    autoResizePaper: true
});

paperScroller.$el.css({ width: '100%', height: '100%' }).appendTo('#paper');

graph.resetCells(members.concat(connections));

var graphLayout = new joint.layout.TreeLayout({
    graph: graph,
    direction: 'R',
	gap: 80,
	siblingGap: 40
});

//var treeLayoutView = new joint.ui.TreeLayoutView({
    //paper: paper,
    //model: graphLayout,
    //previewAttrs: {
        //parent: { rx: 10, ry: 10 }
    //}
//});

graphLayout.layout();

paperScroller.zoom(-0.2);
paperScroller.centerContent();

paper.on('cell:pointerup', function(cellView, evt, x, y) {

    if (V(evt.target).hasClass('add')) {

        var newMember = member('Employee', 'New Employee', 'female.png', '#c6c7e2');
        var newConnection = link(cellView.model, newMember);
        graph.addCells([newMember, newConnection]);
        graphLayout.prepare().layout();

    } else if (V(evt.target).hasClass('del')) {

        cellView.model.remove();
        graphLayout.prepare().layout();

    } else if (V(evt.target).hasClass('edit')) {

        var inspector = new joint.ui.Inspector({
            inputs: {
                'rank': {
                    type: 'text',
                    label: 'Rank',
                    index: 1
                },
                'name': {
                    type: 'text',
                    label: 'Name',
                    index: 2
                },
                'attrs/image/xlink:href': {
                    type: 'select',
                    label: 'Sex',
                    options: [
                        { value: 'male.png', content: 'Male' },
                        { value: 'female.png', content: 'Female' }
                    ],
                    index: 3
                },
                'attrs/.card/fill': {
                    type: 'color-palette',
                    label: 'Color',
                    index: 5,
                    options: [
                        { content: '#31d0c6' },
                        { content: '#7c68fc' },
                        { content: '#fe854f' },
                        { content: '#feb663' },
                        { content: '#c6c7e2' }
                    ]
                }
            },
            cellView: cellView
        });

        var dialog = new joint.ui.Dialog({
            width: 250,
            title: 'Edit Member',
            content: inspector.render().el
        });

        dialog.on('action:close', inspector.remove, inspector);
        dialog.open();
    }
});

var directionPicker = new joint.ui.SelectBox({
    width: 150,
    options: [
        { value: 'L', content: 'Right-Left' },
        { value: 'R', content: 'Left-Right', selected: true },
        { value: 'T', content: 'Bottom-Top' },
        { value: 'B', content: 'Top-Bottom' },
    ]
}).on('option:select', function(option) {
    _.invoke(graph.getElements(), 'set', 'direction', option.value);
    graphLayout.layout();
    paperScroller.centerContent();
});

$('#orgchart-direction').append(directionPicker.render().el);
