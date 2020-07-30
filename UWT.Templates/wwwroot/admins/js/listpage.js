layui.use(['form'], function () {
    var $ = layui.$;
    $(".layui-table-body").scroll(function () {
        $(".layui-table-header").scrollLeft($(this).scrollLeft());
    })
    function resizePageList() {
        var height = $('.uwt-list-parent').height();
        if ($(".uwt-query-list").length !== 0) {
            height -= $(".uwt-query-list").height() + 40;
        }
        if ($(".uwt-handle-list").length !== 0) {
            height -= $(".uwt-handle-list").height() + 40;
        }
        if ($(".uwt-page-selector").length !== 0) {
            height -= $(".uwt-page-selector").height() + 40;
        }
        $('.uwt-list-body').height(height - 30);
        var isscroll = false;
        var st = $('.layui-table-body').scrollTop();
        if (st === 0) {
            $('.layui-table-body').scrollTop(1);
            st = $('.layui-table-body').scrollTop();
            if (st !== 0) {
                isscroll = true;
                $('.layui-table-body').scrollTop(0);
            }
        }
        var minWidth = 0;
        var tableWidth = $(".layui-table-header").width() - 30;
        var styles = "";
        var swidth = 0;
        var starArr = [];
        var starCnt = 0;
        if (isscroll) {
            tableWidth -= 20;
        }
        $('.layui-table-box .layui-table-header th').each(function () {
            var cmw = $(this).data("min-width");
            var name = $(this).data("cname");
            styles += ".uwt-table-cell-" + name + "{ min-width: " + cmw + "px;";
            if ($(this).data('max-width') !== undefined) {
                styles += "max-width: " + $(this).data('max-width') + "px;";
            }
            var widthv = $(this).data('widthv');
            switch ($(this).data('widtht')) {
                case "*":
                    starArr.push(widthv);
                    starCnt += Number(widthv);
                    styles += "width: " + widthv + "*;"
                    break;
                case "px":
                    swidth += Number(widthv) + 30;
                    styles += "width: " + widthv + "px;";
                    break;
                case "auto":
                    var w = 0;
                    $('td div.uwt-table-cell-' + name).each(function () {
                        if (w < $(this).width()) {
                            w = $(this).width();
                        }
                    })
                    if (w < cmw) {
                        w = cmw;
                    }
                    swidth += w + 30;
                    styles += "width: " + w + "px;";
                    break;
                default:
            }
            styles += "}";
        });
        var startWidth = (tableWidth - swidth) / starCnt - 30;
        for (var i in starArr) {
            styles = styles.replace(starArr[i] + "*", startWidth * starArr[i] + "px");
        }
        $(".layui-table-box style").remove();
        $('.layui-table-box').append("<style>" + styles + "</style>");
    }
    $(window).resize(resizePageList);
    resizePageList();
})