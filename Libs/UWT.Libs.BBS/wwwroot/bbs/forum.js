$(function () {
    $('.area-item').click(function () {

    })

    $('.page-selector>button').click(function () {
        if ($(this).hasClass('unhandle') || $(this).hasClass('current')) {
            return;
        }
        var pageIndex = $(this).data("pi");
        window.location.href = $(this).parent().data('href-base') + pageIndex;
    })

    usershowing = null;
    usershowtime = 0;
    const waittimes = 2;
    const userdlg = ".userinfo-dlg";
    function usershowdlgcallback() {
        usershowtime--;
        if (usershowtime === 0) {
            $(userdlg).remove();
            usershowing = null;
        } else {
            setTimeout(usershowdlgcallback, 100);
        }
    }

    $('.user-show').hover(function () {
        var uid = $(this).data("uid");
        usershowtime = 1000;
        if (usershowing === uid) {
            return;
        }
        style = "left: " + $(this).offset().left + "px; top: " + ($(this).offset().top + $(this).height() + 8) + "px;";
        var halfWidth = $(this).width() / 2 - 20;
        usershowing = uid;
        setTimeout(usershowdlgcallback, 100);
        api("/forums/user/info?id=" + uid, null, function (rx) {
            $('body').append(
                `<div class='userinfo-dlg' id='userdlg-${uid}' style='${style}'>
<div class='arrow' style='margin-left: ${halfWidth}px'><arrow></arrow></div>
<div class='content'>
    <div class='avatar'>
        <img src='${rx.data.avatar}' />
    </div>
    <div class='info'>
        <div class='nickname'><a href='/bbs/user/${rx.data.id}'>${rx.data.nickName}</a></div>
        <div class='infos'>
            <div class='flow'>${rx.data.followCnt}</div>
            <div class='fans'>${rx.data.fansCnt}</div>
            <div class='topic'>${rx.data.topicCnt}</div>
        </div>
    </div>
</div>
</div>`);
        }, function (rx) {
                $('body').append(`<div class='userinfo-dlg' id='userdlg-${uid}' style='${style}'>无信息</div>`);
        }, "form");
    }, function () {
            usershowtime = waittimes;
    })

    $('body').on('mouseover mouseout', userdlg, function () {
        if (event.type == "mouseover") {
            usershowtime = 1000;
        } else if (event.type == "mouseout") {
            usershowtime = waittimes;
        }
    })

    $('.goto-list').click(function () {
        window.location.href = $(this).data("url");
    })

    $('.search_text').change(function () {
        var txt = $(this).val();
        if (txt == "") {
            $(this).addClass('empty');
        } else {
            $(this).removeClass('empty');
        }
    })
})