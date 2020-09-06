$(function () {
    $('.area-item').click(function () {

    })

    $('.page-selector>button').click(function () {
        if ($(this).hasClass('unhandle')) {
            return;
        }
        var pageIndex = $(this).data("pi");
        window.location.href = $(this).parent().data('href-base') + pageIndex;
    })
})