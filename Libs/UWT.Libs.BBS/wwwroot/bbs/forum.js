$(function () {
    $('.area-item').click(function () {
        window.location.href = "/bbs/area/" + $(this).data('aid');
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
    cacheUserInfos = {};
    function usershowdlgcallback() {
        usershowtime--;
        if (usershowtime === 0) {
            $(userdlg).remove();
            usershowing = null;
        } else {
            setTimeout(usershowdlgcallback, 100);
        }
    }

    function showUserInfo(info, hw, style, uid) {
        if (info === null) {
            $('body').append(`<div class='userinfo-dlg' id='userdlg-${uid}' style='${style}'>无信息</div>`);
            return;
        }
        $('body').append(
            `<div class='userinfo-dlg' id='userdlg-${uid}' style='${style}'>
<div class='arrow' style='margin-left: ${hw}px'><arrow></arrow></div>
<div class='content'>
    <div class='avatar'>
        <img src='${info.avatar}' />
    </div>
    <div class='info'>
        <div class='nickname'><a href='/bbs/user/${info.id}'>${info.nickName}</a></div>
        <div class='infos'>
            <div class='flow'>${info.followCnt}</div>
            <div class='fans'>${info.fansCnt}</div>
            <div class='topic'>${info.topicCnt}</div>
        </div>
    </div>
</div>
</div>`);
    }

    $('.user-show').hover(function () {
        var uid = $(this).data("uid");
        usershowtime = 500;
        if (usershowing === uid) {
            return;
        }
        style = "left: " + $(this).offset().left + "px; top: " + ($(this).offset().top + $(this).height() + 8) + "px;";
        var halfWidth = $(this).width() / 2 - 20;
        usershowing = uid;
        setTimeout(usershowdlgcallback, 100);
        if (uid in cacheUserInfos) {
            showUserInfo(cacheUserInfos[uid], halfWidth, style, uid);
            return;
        }
        api("/forums/user/info?id=" + uid, null, function (rx) {
            cacheUserInfos[uid] = rx.data;
            showUserInfo(rx.data, halfWidth, style, uid);
        }, function (rx) {
               cacheUserInfos[uid] = null;
               showUserInfo(null, halfWidth, style, uid);
        }, "form");
    }, function () {
            usershowtime = waittimes;
    })

    $('body').on('mouseover mouseout', userdlg, function () {
        if (event.type == "mouseover") {
            usershowtime = 500;
        } else if (event.type == "mouseout") {
            usershowtime = waittimes;
        }
    })

    $('.goto-list').click(function () {
        window.location.href = $(this).data("url");
    })

    $('.search_text').change(function () {
        var txt = $(this).val();
        if (txt === "") {
            $(this).addClass('empty');
        } else {
            $(this).removeClass('empty');
        }
    })

    $(".profile>.cnt-infos>div").click(function () {
        window.location.href = "/bbs/user/" + $(this).attr('class') + "?uid=" + $(this).parents(".profile").data('id');
    })
})