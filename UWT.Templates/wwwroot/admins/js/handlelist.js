layui.use(['form'], function () {
    var $ = layui.$;
    const batchCheckBoxClass = '.batch-check-box';
    var confirmPlusHandles = [];
    function confirmPlus(tipcontent, title, btns, calbak) {
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
    function walkHandleItem(type, target, btn) {
        var method = 'get';
        if (type === 'navigate') {
            window.location.href = target;
            return;
        } else if (type === 'api-get') {
            method = 'get';
        } else if (type === 'api-post') {
            method = 'post';
        }
        btn.attr('disabled', 'disabled');
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
        api(target, data, function () {
            alert("操作成功");
            window.location.reload();
        })
    }
    function handleBtnItem(type, target, tooltip, btn) {
        if (type === 'comfirm') {
            var btns = target;
            if (typeof (btns) == 'string') {
                btns = JSON.parse(btns);
            }
            confirmPlus(tooltip, "提示", btns, function (i, data) {
                console.log(i, data)
                handleBtnItem(data.Type, data.Target, data.AskTooltip, btn);
            })
            return;
            var optional = {
                btn: [],
                title: '提示'
            };
            for (var i in btns) {
                var c = btns[i];
                optional.btn.push(c.Title);
                optional['btn' + (Number(i) + 1)] = function (i, d) {
                    console.log(i, d)
                    layui.layer.close(i);
                    handleBtnItem(c.Type, c.Target, c.AskTooltip, btn);
                }
            }
            layui.layer.confirm(tooltip, optional);
        } else {
            if (tooltip != null && tooltip != '') {
                layer.confirm(tooltip, function (index) {
                    layer.close(index);
                    walkHandleItem(type, target, btn);
                })
            } else {
                walkHandleItem(type, target, btn);
            }
        }
    }
    $('.handle-item').click(function () {
        handleBtnItem($(this).data('type'), $(this).data('target'), $(this).data('ask'), $(this));
    });
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
})