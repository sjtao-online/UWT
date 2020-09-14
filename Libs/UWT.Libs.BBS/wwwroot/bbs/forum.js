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

    usershowing = null;
    usershowtime = 0;
    const waittimes = 4;
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
        style = "left: " + $(this).offset().left + "px; top: " + ($(this).offset().top + $(this).height() + 4) + "px;";
        usershowing = uid;
        setTimeout(usershowdlgcallback, 100);
        api("/forums/user/info?id=" + uid, null, function (rx) {
            $('body').append("<div>" + rx.data.nickName + "</div>");
        }, function (rx) {
                $('body').append(
`<div class='userinfo-dlg' id='userdlg-${uid}' style='${style}'>
<arrowb></arrowb>
<arrow></arrow>
<arrow2></arrow2>
<div class='content'>
    <div></div>
</div>
</div>`);
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
})