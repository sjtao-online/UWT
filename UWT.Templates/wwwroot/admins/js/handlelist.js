layui.use(['form'], function () {
    var $ = layui.$;
    const batchCheckBoxClass = '.batch-check-box';
    var confirmPlusHandles = [];
    function confirmPlus(tipcontent, title, btns, calbak, btn) {
        var handleIndex = confirmPlusHandles.length;
        confirmPlusHandles.push(calbak);
        var content = "<div>";
        content += tipcontent;
        content += "</div><div class='layui-layer-btn' style='margin: 20px -20px -20px -20px'>"
        for (var i in btns) {
            var c = btns[i];
            if (typeof c == 'string') {
                content += "<a class='confirm-handle' data-bindex='" + i + "' data-hindex='" + handleIndex + "'>" + c +"</a>";
            } else {
                content += "<a class='confirm-handle' data-bindex='" + i + "' data-hindex='" + handleIndex + "' data-data='"+JSON.stringify(c)+"'>" + c.Title +"</a>";
            }
        }
        content += "</div>"
        layui.layer.open({
            title: title,
            content: content,
            btn: null
        })
    }
    $('body').on('click', ".confirm-handle", function () {
        var hindex = $(this).data('hindex');
        confirmPlusHandles[hindex]($(this).data('bindex'), $(this).data('data'));
    })

    function parseTarget(old) {
        var news = old;
        if (typeof (news) === 'string') {
            news = JSON.parse(old);
        }
        return news;
    }

    function uwtWalkHandleItem(type, target, btn) {
        switch (type) {
            case "PopupDlg":
                var popup = parseTarget(target);
                var area = [];
                if (popup.width !== "" && popup.height !== "") {
                    area.push(popup.width);
                    area.push(popup.height);
                }
                layui.layer.open({
                    type: 2,
                    title: "",
                    content: popup.url,
                    area
                });
                return;
            case "Download":
                var download = parseTarget(target);
                openDownloadDialog(download.url, download.filename);
                return;
            case "EvalJS":
                eval("func" + target + "()");
                return;
            case "Navigate":
                window.location.href = target;
                return;
            case "ApiPost":
                method = "POST";
                break;
            case "ApiGet":
                method = "GET";
                break;
            default:
        }
        var data = {};
        if (btn.hasClass('batch')) {
            var listids = [];
            $(batchCheckBoxClass).each(function () {
                if (btn.is(':checked')) {
                    listids.push(btn.data('batch-key'))
                }
            })
        }
        data[btn.data('batch-key')] = listids;
        layer.load(1, {
            shade: [0.1, '#000']
        });
        api(target, data, function () {
            layer.msg("操作成功", {
                time: 1500
            }, function () {
                window.location.reload();
            })
        }, null, "JSON", method);
    }

    function uwtHandleBtnItem(type, target, tooltip, btn) {
        if (type === 'Comfirm') {
            var btns = parseTarget(target);
            confirmPlus(tooltip, "提示", btns, function (i, data) {
                uwtHandleBtnItem(data.Type, data.Target, data.AskTooltip, btn);
            }, btn)
        } else if (type === 'MultiButtons') {
            var menus = parseTarget(target);
            var contents = "";
            console.log(menus);
            for (var i in menus) {
                var item = menus[i];
                contents += "<button class='uwt-handle-item clear-pop' data-type='" + item.Type + "' data-target='" + item.Target + "' data-ask='" + item.AskContent + "' title='" + item.Tooltip + "'>" + item.Title + "</button>";
            }
            $('body').append("<div class='uwt-layer-zz'></div>");
            var offset = btn.offset();
            $('body').append("<div class='uwt-popup-menus' style='top: " + (offset.top + btn.height()) + "px;left: " + offset.left + "px'>" + contents + "</div>");
        } else {
            if (tooltip !== undefined && tooltip !== null && tooltip !== '') {
                layer.confirm(tooltip, function (index) {
                    layer.close(index);
                    uwtWalkHandleItem(type, target, btn);
                })
            } else {
                uwtWalkHandleItem(type, target, btn);
            }
        }
    }
    function openDownloadDialog(url, saveName) {
        if (typeof url == 'object' && url instanceof Blob) {
            url = URL.createObjectURL(url); // 创建blob地址
        }
        var aLink = document.createElement('a');
        aLink.href = url;
        aLink.download = saveName || ''; // HTML5新增的属性，指定保存文件名，可以不要后缀，注意，file:///模式下不会生效
        var event;
        if (window.MouseEvent) event = new MouseEvent('click');
        else {
            event = document.createEvent('MouseEvents');
            event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
        }
        aLink.dispatchEvent(event);
    }
    $('body').on('click', '.uwt-handle-item', function () {
        if ($(this).hasClass('clear-pop')) {
            $('.uwt-layer-zz').next().remove();
            $('.uwt-layer-zz').remove();
        }
        uwtHandleBtnItem($(this).data('type'), $(this).data('target'), $(this).data('ask'), $(this));
    })
    layui.form.on('checkbox(batch)', function () {
        var checkCount = 0;
        var count = $(batchCheckBoxClass).length;
        $(batchCheckBoxClass).each(function () {
            if ($(this).is(':checked')) {
                checkCount++;
            }
        })
        if (checkCount > 0) {
            btnEnable($('.batch'))
        } else {
            btnDisable($('.batch'));
        }
    })
    btnDisable($('.batch'));
    $('body').on('click','.uwt-layer-zz', function () {
        $(this).next().remove();
        $(this).remove();
    })
})