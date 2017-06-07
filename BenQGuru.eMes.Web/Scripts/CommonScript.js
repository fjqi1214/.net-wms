
function BrowserType() {

    var Sys = {};

    var ua = navigator.userAgent.toLowerCase();

    var s;

    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :

            (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :

            (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :

            (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :

            (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

    return Sys;
}

window.onload = function DoWindowLoad() {
    try {

        var objs = document.body.all.tags("INPUT");
        for (var i = 0; i < objs.length; i++) {
            if (objs[i].type == "text" && (!objs[i].disabled) && (!objs[i].readOnly)) {
                objs[i].focus();
                break;
            }
        }
    }
    catch (e) {
    }
    //	document.oncontextmenu = function(){
    //		event.returnValue = false ;
    //		event.cancelBubble = true ;
    //	}
}

function showDialog(type) {
    var owidth = window.screen.width;
    var oheight = window.screen.height;

    var width = 0;
    var height = 0;

    if (owidth == 800) {
        if (type == 6) {
            width = 350;
            height = 350;
        }
        if (type == 7) {
            width = 550;
            height = 550;
        }
    }
    else {
        if (type == 6) {
            width = 350;
            height = 350;
        }
        if (type == 7) {
            width = 850;
            height = 550;
        }
    }

    var strFeature = "dialogWidth:" + width + "px;" + "dialogHeight:" + height + "px;center:yes;help:no;status:no;";
    return strFeature;
}


//下载导入模板
function DownLoadFile() {
    //debugger;
    try {
        var hf = document.all.aFileDownLoad.href;
        if (hf == "") return;
        var path;
        var pix = hf.substr(0, 4);
        if (pix.toLowerCase() == "file") path = hf.substr(8, hf.length - 8);
        else if (pix.toLowerCase() == "http") path = hf;
        var fl = path.split('/');
        var file = fl[fl.length - 2] + '/' + fl[fl.length - 1];

        var frameDown = $('<a></a>');
        frameDown.appendTo($('form'));
        frameDown.html('<span></span>');
        frameDown.attr('href', "http://" + fl[fl.length - 4] + "/" + fl[fl.length - 3] + "/FDownload.aspx?&fileName=" + escape(file));
        frameDown.children().click();
        frameDown.remove();
        return false;

    } catch (e) { alert(e.description); }
}


function Hashtable() {
    this._hash = new Object();
    this.add = function (key, value) {
        if (typeof (key) != "undefined") {
            if (this.contains(key) == false) {
                this._hash[key] = typeof (value) == "undefined" ? null : value;
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
    this.remove = function (key) { delete this._hash[key]; }
    this.count = function () { var i = 0; for (var k in this._hash) { i++; } return i; }
    this.getValue = function (key) { return this._hash[key]; }
    this.setValue = function (key, value) {
        if (typeof (key) != "undefined") {
            this._hash[key] = typeof (value) == "undefined" ? null : value;
            return true;
        } else {
            return false;
        }
    }
    this.contains = function (key) { return typeof (this._hash[key]) != "undefined"; }
    this.clear = function () { for (var k in this._hash) { delete this._hash[k]; } }

}


//Link按钮点击
function Gird_ClientClick(sender, e) {
    if (e.get_item() == null)
        return;

    if ($(e.get_item().get_element()).css('backgroundImage') == 'none')
        return;

    var key = e.get_item().get_column().get_key();
    var index = e.get_item().get_row().get_index();

    document.forms[0].__EVENTTARGET.value = 'Link';
    document.forms[0].__EVENTARGUMENT.value = key + ',' + index;

    $('#btnPostBack').click();

}

var gridData;
var minWidth;

var Key_isLastHasVcroll = '_isLastHasVcroll';
var Key_isWidthSet = '_isWidthSet';
var Key_lastColId = '_lastColId';
var isQueryPage = false;

function Grid_Initialize(grid, e) {
    
    if (gridData == null)
        gridData = new Hashtable();

    //最小宽度设置
    if (typeof (minWidth) == 'number') {
        if (minWidth < 0 || minWidth > 150) {
            minWidth = 70;
        }
    }
    else {
        minWidth = 70;
    }              

    var gridId = grid.get_id();
    //如果宽度没有设定并且能查出数据，则以显示出的每列列宽为真是列宽。QPage每次都需要重新处理宽度。
    if (gridData.getValue(gridId + Key_isWidthSet) == null ||isQueryPage) {
        if (grid.get_rows().get_length() > 0) {
            for (var i = 0; i < grid.get_columns().get_length(); i++) {
                var col = grid.get_columns().get_column(i);
                if (col.get_hidden()) {
                    continue;
                }
                //存储Grid最后显示的一列
                gridData.setValue(gridId + Key_lastColId, col.get_key());

                if (col.get_width() != '') {
                    //已经设置了宽度的不需要初始化
                    continue;
                }
                var width = $(col.get_headerElement()).width();

                if (width < minWidth) {
                    //最小宽度设置
                    width = minWidth;
                }
                col.set_width(width - 7 + 'px');

            }
            gridData.setValue(gridId + Key_isWidthSet, true);
        }
    }
    else {

        //上次是否有竖直滚动条决定最后一列是否要改变列宽
        if (gridData.getValue(gridId + Key_isLastHasVcroll) != null) {
            var lastCol = grid.get_columns().get_columnFromKey(gridData.getValue(gridId + Key_lastColId));
            if (gridData.getValue(gridId + Key_isLastHasVcroll) && $(grid._vScrBar).children().height() == 0) {
                //滚动条消失
                lastCol.set_width(parseInt(lastCol.get_width()) + 18 + 'px');
            }
            else if (!gridData.getValue(gridId + Key_isLastHasVcroll) && $(grid._vScrBar).children().height() > 0) {
                //滚动条出现
                lastCol.set_width(parseInt(lastCol.get_width()) - 18 + 'px');
            }
        }

        //无数据的时候需要重新调整显示的头部宽度
        if (grid.get_rows().get_length() == 0) {
            for (var i = 0; i < grid.get_columns().get_length(); i++) {
                var col = grid.get_columns().get_column(i);
                if (col.get_hidden()) {
                    continue;
                }

                var width = parseInt(col.get_width());
                $(col.get_headerElement()).width(width + 7);
            }
        }
    }

    if ($(grid._vScrBar).children().height() == 0) {
        gridData.setValue(gridId + Key_isLastHasVcroll, false);
    }
    else {
        gridData.setValue(gridId + Key_isLastHasVcroll, true);
    }

    //针对工单导入界面，有异常的行无法选中
    if (grid.get_columns().get_columnFromKey('importException') != null) {
        for (var i = 0; i < grid.get_rows().get_length(); i++) {
            var row = grid.get_rows().get_row(i);

            if (row.get_cellByColumnKey('importException').get_value() != '') {
                var tdImage = $(row.get_cellByColumnKey('Check').get_element()).find('img');
                tdImage.css('background-color', 'gray');
                tdImage.css('filter', 'alpha(opacity=30)');
                tdImage.css('-moz-opacity', '0.3');
                tdImage.css('-khtml-opacity', '0.3');
                tdImage.css('opacity', '0.3');
                tdImage.wrap('<div style=\'cursor: auto;height: 100%;width: 100%;z-index: 100;\'></div>');
                        }
//            if (row.get_cellByColumnKey('importException').get_value() == '') {
//                row.get_cellByColumnKey('Check').set_value(true);
//            }
        }
    }
}

function ColumnResizing(grid, e) {
    if (grid.get_rows().get_length() == 0) {
        e.set_cancel(true);
    }
}

function ColumnSorting(grid, e) {
    if (grid.get_rows().get_length() == 0) {
        e.set_cancel(true);
    }
}

function CellValueChanged(grid, e) {
    grid.get_behaviors().get_editingCore().commit();
}

function GetSelectRowGUIDS(gridId) {
    if ($('#' + gridId).html() == null)
        return;

    var grid = $find(gridId);
    var hdnId = '#hdnSelectRowGUIDS_' + gridId;
    $(hdnId).val('');
    if (grid.get_columns().get_columnFromKey('Check') == null)
        return;

    for (var i = 0; i < grid.get_rows().get_length(); i++) {
        var row = grid.get_rows().get_row(i);
            
        if (row.get_cellByColumnKey('Check').get_value()) {
            $(hdnId).val($(hdnId).val() + '(' + row.get_cellByColumnKey('GUID').get_value() + ')');
        }
    }
    //alert($('#hdnSelectRowGUIDS').val());
}

