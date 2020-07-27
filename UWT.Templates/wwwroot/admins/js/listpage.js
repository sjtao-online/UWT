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
        if (isscroll) {
            $('#uwt-query-scroll-block').show();
        } else {
            $('#uwt-query-scroll-block').hide();
        }
    }
    $(window).resize(resizePageList);
    resizePageList();
})