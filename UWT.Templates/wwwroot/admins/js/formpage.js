var arr = ['jquery', 'form', 'laypage', 'element'];
if (getClassNameTagCount(["form-date", "form-datetime", "form-time"]) > 0) {
    arr.push('laydate');
}
if (getClassNameTagCount(['form-slider']) > 0) {
    arr.push('slider');
}

var hasWangEditor = getClassNameTagCount(['wangEditor']) > 0;

if (getClassNameTagCount(['layui-editor']) > 0) {
    arr.push('layedit');
}

if (getClassNameTagCount(['form-color-picker']) > 0) {
    arr.push('colorpicker');
}

if (getClassNameTagCount(['choose']) > 0) {
    arr.push('tree')
}

var LoadFormValues = null;

layui.use(arr, function () {
    var $ = layui.$;
    var uploadurl = $('body').data('fupload');
    var uploaddlg = null;
    var uploadfilebtn = null;
    var uploadindex = 0;
    var reloadfilehis = false;
    var selectedFiled = false;
    var wangEditorMap = {};
    if (arr.indexOf('laydate') != -1) {
        function laydateRender(c, r) {
            $('.' + c).each(function () {
                r.elem = "#" + $(this).attr('id');
                if ($(this).data('max') != '') {
                    r.max = $(this).data('max');
                }
                if ($(this).data('min') != '') {
                    r.min = $(this).data('min');
                }
                layui.laydate.render(r);
            });
        }
        laydateRender('form-date', {});
        laydateRender('form-datetime', { type: 'datetime' });
        laydateRender('form-time', { type: 'time' });
    }
    if (arr.indexOf('slider') != -1) {
        $('.form-slider').each(function () {
            var id = "#" + $(this).data('rid');
            var max = $(id).data('max');
            var min = $(id).data('min');
            var r = {
                elem: id + '__slider',
                change: function (value) {
                    $(id).val(value);
                    if (Array.isArray(value)) {
                        $(id + '_show').text(value[0] + " - " + value[1]);
                    } else {
                        $(id + '_show').text(value);
                    }
                },
                max: max,
                min: min,
                step: $(id).data('step')
            }
            if ($(id).hasClass('hasrange')) {
                r.range = true;
                r.value = $(id).val().split(',');
                if (r.value == '') {
                    r.value = [min, max];
                    $(id).val(min + "," + max);
                    $(id + '_show').text(min + " - " + max);
                }
            } else {
                r.value = $(id).val();
                if (r.value == '') {
                    r.value = min;
                    $(id).val(min);
                    $(id + '_show').text(min);
                }
            }
            layui.slider.render(r);
        })
    }
    if (arr.indexOf('layedit') != -1) {
        layui.layedit.set({
            uploadImage: {
                url: uploadurl//接口url
                , type: 'post' //默认post
            }
        })
        $('.layui-editor').each(function () {
            var id = $(this).attr('id');
            var index = layui.layedit.build(id);
            layui.layedit.setContent(index, $('#' + id + "-src").html(), false)
            $(this).data('editorid', index);
        })
    }
    if (hasWangEditor) {
        var E = window.wangEditor;
        $('.wangEditor').each(function () {
            var id = $(this).attr('id');
            var ceditor = new E('#' + id);
            ceditor.customConfig.customUploadImg = function (files, insert) {
                for (var i in files) {
                    const file = files[i];
                    var uploadFileData = new FormData();
                    uploadFileData.append("desc", "");
                    uploadFileData.append('file', file);
                    uploadfile(uploadurl, uploadFileData, function (rx) {
                        insert(rx.data.src);
                    }, function () { }, function () {

                    })
                }
            }
            ceditor.create();
            wangEditor[id] = ceditor;
        })
    }
    if (arr.indexOf('colorpicker') != -1) {
        $('.form-color-picker').each(function () {
            var id = $(this).attr('id')
            layui.colorpicker.render({
                elem: '#' + id,
                color: $(this).data('color'),
                done: function (color) {
                    $("#" + id.substr(0, id.length - 7)).val(color);
                }
            })
        })
    }
    const unitArr = ["天", "时", "分", "秒"];
    const plusArr = ['day', 'hour', 'minu', 'second'];
    function buildInputHtml(plus, value, maxMinArr, index) {
        return `<input class=ts-input type=number id=${plus}-${plusArr[index]} value=${value[plusArr[index]]} max=${maxMinArr[index].max} min=${maxMinArr[index].min}>${unitArr[index]}`;
    }
    function buildMaxMinObject(max, min) {
        //var maxs = max.split('-');
        //var mins = min.split('-');
        var obj = [{ max: 24, min: -24 }, { max: 23, min: 0 }, { max: 59, min: 0 }, { max: 59, min: 0 }];

        return obj;
    }


    function buildTimeSpanObj(current) {
        var r = {
            day: 0,
            hour: 0,
            minu: 0,
            second: 0
        };
        var start = 0;
        function calcTimeFromIndex(i) {
            var index = current.indexOf(unitArr[i]);
            if (index != -1) {
                r[plusArr[i]] = current.substr(start, index - start);
                start = index + 1;
            }
        }
        for (var i = 0; i < 4; i++) {
            calcTimeFromIndex(i)
        }
        return r;
    }

    function buildTimeSpan(size, maxMinArr, current, plus) {
        var value = buildTimeSpanObj(current);

        switch (size) {
            case 'Day':
                return buildInputHtml(plus, value, maxMinArr, 0);
            case 'Hour':
                return buildInputHtml(plus, value, maxMinArr, 0)
                    + buildInputHtml(plus, value, maxMinArr, 1);
            case 'Minute':
                return buildInputHtml(plus, value, maxMinArr, 0)
                    + buildInputHtml(plus, value, maxMinArr, 1)
                    + buildInputHtml(plus, value, maxMinArr, 2);
            case 'Second':
                return buildInputHtml(plus, value, maxMinArr, 0)
                    + buildInputHtml(plus, value, maxMinArr, 1)
                    + buildInputHtml(plus, value, maxMinArr, 2)
                    + buildInputHtml(plus, value, maxMinArr, 3);
            default:
                break;
        }
        return "";
    }
    var currentTimeSpanInput = null;
    $('.uwt-timespan').click(function () {
        var content = '';
        var size = $(this).data('size');
        var maxMinArr = buildMaxMinObject($(this).data('max'), $(this).data('min'))
        if ($(this).data('range') == 1) {
            var values = ["", ""];
            var value = $(this).val();
            if (value.indexOf(' - ') != -1) {
                values = value.split(' - ');
            }
            content = buildTimeSpan(size, maxMinArr, values[0], 'begin') + "<div>　-　</div>" + buildTimeSpan(size, maxMinArr, values[1], 'end');
        } else {
            content = buildTimeSpan(size, maxMinArr, $(this).val(), 'c');
        }
        var off = $(this).offset();
        currentTimeSpanInput = $(this);
        $('body').append("<div class=uwt-shade></div><div class='uwt-timespan-dlg'><div class=uwt-flex>" + content + "</div><div class=uwt-flex><button class='layui-btn layui-btn-xs apply'>应用</button><button class='layui-btn layui-btn-xs clear'>清空</button></div></div>");
        $(".uwt-timespan-dlg").css('left', off.left);
        $(".uwt-timespan-dlg").css('top', off.top + $(this).height());
    })
    $('body').on("click", '.uwt-shade', function () {
        $('.uwt-shade').remove();
        $('.uwt-timespan-dlg').remove();
    })
    $('body').on('click', '.uwt-timespan-dlg .clear', function () {
        $('.uwt-shade').remove();
        $('.uwt-timespan-dlg').remove();
        currentTimeSpanInput.val("")
    })
    $('body').on('blur', '.ts-input', function () {
        var val = Number($(this).val());
        if (val == '') {
            $(this).val('0');
        } else if (val > $(this).prop('max')) {
            $(this).val($(this).prop('max'));
        } else if (val < $(this).prop('min')) {
            $(this).val($(this).prop('min'));
        }
    })
    function buildNewValue(plus) {
        var newValue = '';
        for (var i = 0; i < 4; i++) {
            var input = $('#' + plus + '-' + plusArr[i]);
            if (input.length == 0) {
                continue;
            }
            var v = input.val();
            if (v != null && v != '' && v != 0) {
                newValue += v + unitArr[i];
            } else {
                newValue += '0' + unitArr[i];
            }
        }
        return newValue;
    }
    $('body').on('click', '.uwt-timespan-dlg .apply', function () {
        var newValue = '';
        if (currentTimeSpanInput.data('range') == 1) {
            newValue = buildNewValue('begin') + " - " + buildNewValue('end');
        } else {
            newValue = buildNewValue('c');
        }
        currentTimeSpanInput.val(newValue)
        $('.uwt-shade').remove();
        $('.uwt-timespan-dlg').remove();
    })
    $('#uwt-file-input').on("change", function () {
        function selectfileerror(errormsg) {
            layui.layer.msg(errormsg);
            $("#uwt-select-file-btn").text("选择文件")
            $('#file-show').val('[未选择]')
            selectedFiled = false;
        }
        if (this.files.length == 0) {
            selectfileerror('文件未选择');
        } else {
            console.log(this.files[0])
            var maxfilesize = uploadfilebtn.data('max');
            var filename = this.files[0].name.toLowerCase();
            var index = filename.indexOf('.');
            var ext = filename.substr(index, filename.length - index);
            if ($(this).attr('accept').indexOf(ext) == -1) {
                selectfileerror('扩展名不对');
                return;
            }
            if (maxfilesize != -1 && maxfilesize < this.files[0].size) {
                selectfileerror('文件不可超出大小');
                return;
            }
            selectedFiled = true;
            $('#file-show').val(this.files[0].name)
            $("#uwt-select-file-btn").text("重新选择")
        }
    })
    $(".form-file").click(function () {
        uploadfilebtn = $(this);
        layui.layer.open({
            type: 1,
            title: '上传文件',
            shade: 0.3,
            area: '600px',
            skin: 'layui-layer-lan',
            btn: null,
            resize: false,
            offset: 't',
            moveType: 0,
            content: $('#UploadFileDialog'),
            cancel: function (index, layero) {
                closeUploadDlg(null);
                return true;
            },
            success: function (layero, index) {
                uploadindex = index;
                uploaddlg = layero;
                reloadfilehis = true;
                if (uploadfilebtn.data('max') != -1) {
                    $('#file-size-div').show();
                    $('#file-size').html("小于" + renderSize(uploadfilebtn.data('max')));
                } else {
                    $('#file-size-div').hide();
                }
                if (uploadfilebtn.data('ft') != '') {
                    $('#file-type-div').show();
                    $('#file-type').html(uploadfilebtn.data('ft'));
                } else {
                    $('#file-type-div').hide();
                }
                var fileshow = $('#file-show');
                $('#uwt-file-input').attr('accept', uploadfilebtn.data('ft'));
                if ($("#" + uploadfilebtn.data('inputid')).val() != '') {
                    $("#uwt-select-file-btn").text("重新选择")
                    fileshow.val($("#" + uploadfilebtn.data('inputid')).val())
                } else {
                    $("#uwt-select-file-btn").text("选择文件")
                    fileshow.val("[未选择]")
                }
                if (uploadfilebtn.data('canselectreadyall') == 0) {
                    layero.find('.files-his').hide();
                } else {
                    layero.find('.files-his').show();
                }
                if (uploadfilebtn.data('canlinkother') == 0) {
                    layero.find('.files-link').hide();
                } else {
                    layero.find('.files-link').show();
                }
            }
        })
    })
    $('.choose').click(function () {
        var chooseinput = $(this);
        var isMultiSelect = chooseinput.data('mselect') == 'True';
        var btn =  null;
        if (isMultiSelect) {
            btn = '确定'
        }
        layui.layer.open({
            type: 1,
            title: "选择",
            shade: 0.3,
            area: ['600px','600px'],
            skin: 'layui-layer-lan',
            btn: btn,
            resize: false,
            offset: 't',
            moveType: 0,
            content: $("#ChooseIdDialog"),
            yes: function (index) {
                var arrids = [];
                var arrtexts = [];
                $('input:checkbox:checked').each(function () {
                    arrids.push($(this).val());
                    arrtexts.push($(this).next().next().html());
                });
                var idtxt = chooseinput.parent().prev();
                idtxt.val(arrids.join());
                chooseinput.val(arrtexts.join());
                layui.layer.close(index);
                idtxt.blur();
            },
            success: function (layero, index) {
                var url = chooseinput.data("url");
                if (chooseinput.data('ukey') == '1') {
                    url += "?rkey=" + chooseinput.data('rkey')
                } else {
                }
                api(url, null, function (rx) {
                    var renderdata = {
                        elem: '#chooseTreeDiv',
                        data: rx.data,
                    }
                    if (isMultiSelect) {
                        var ids = chooseinput.parent().prev().val().split(',');
                        function checkChildren(list) {
                            for (var i in list) {
                                if (ids.indexOf(list[i].id + "") > -1) {
                                    list[i].checked = true;
                                }
                                if ('children' in list[i] && list[i].children != null) {
                                    checkChildren(list[i].children);
                                }
                            }
                        }
                        checkChildren(renderdata.data);
                        renderdata.showCheckbox = true;
                    } else {
                        renderdata.data = [{id: 0, title: "[无选择]"}].concat(rx.data);
                        renderdata.click = function (obj) {
                            var idtxt = chooseinput.parent().prev();
                            idtxt.val(obj.data.id);
                            chooseinput.val(obj.data.title);
                            layui.layer.close(index);
                            idtxt.blur();
                        }
                    }
                    layui.tree.render(renderdata)
                })
            }
        })
    })
    $(".btn-back").click(function () {
        var burl = $(this).data('burl');
        if (burl == '') {
            hisBack();
        } else {
            hisBack(burl);
        }
    })
    element.on('collapse(uploadfile)', function (data) {
        if (reloadfilehis && data.title.data('type') == 'his') {
            reloadfilehis = false;
            var fd = {};
            fd[data.title.data('typeparam')] = uploadfilebtn.data('ft');
            api(data.title.data('url'), fd, function (rx) {
                layui.laypage.render({
                    elem: 'file-his-pageselector',
                    count: rx.data.itemTotal / rx.data.pageSize + (rx.data.itemTotal % rx.data.pageSize == 0 ? 0 : 1)
                })
                for (var i = 0; i < rx.data.items.length; i++) {
                    $('.files-his-table').append('<tr><td>' + rx.data.items[i].title + '</td><td>' + rx.data.items[i].size + '</td><td>' + rx.data.items[i].addTime + '</td><td><button class="layui-btn layui-btn-xs select-his-file-btn" data-file="' + rx.data.items[i].src+'">选择</button></td></tr>')
                }
            }, function () {

            })
        }
    });
    function closeUploadDlg(file) {
        if (file != null) {
            $("#" + uploadfilebtn.data('inputid')).val(file);
        }
        var ts = uploaddlg.find(".layui-colla-content");
        if (!ts.eq(0).hasClass("layui-show")) {
            ts.eq(0).prev().click();
        }
        layui.layer.close(uploadindex);
    }
    $('.files-his-table').on('click', '.select-his-file-btn', function () {
        closeUploadDlg($(this).data('file'))
    })
    $('#UploadFileDialog').on('click', '#import-linker-file-btn', function () {
        closeUploadDlg($('#import-linker-file').val())
    })
    $('#uwt-btn-upload').click(function () {
        if (uploaddlg.find('#uwt-file-input')[0].files.length == 0 || (!selectedFiled)) {
            alert('请选择文件再继续');
            return;
        }
        var uploadFileData = new FormData();
        uploaddlg.find("#uwt-file-desc").attr("disabled", "")
        uploadFileData.append("desc", uploaddlg.find("#uwt-file-desc").val());
        uploadFileData.append('file', uploaddlg.find('#uwt-file-input')[0].files[0]);
        var proc = $('.file-upload-progress');
        proc.children("div").css("width", "0%");
        proc.show();
        uploadfile(uploadurl, uploadFileData, function (rx) {
            ajaxSuccess(rx, function (x) {
                proc.hide();
                uploaddlg.find("#uwt-file-desc").removeAttr("disabled")
                closeUploadDlg(x.data.src);
            })
        }, function () { }, function (process) {
            proc.children("div").css("width", process + "%");
        })
    })
    $('#file-show').click(function () {
        uploaddlg.find('#uwt-file-input').click()
    })
    $('#uwt-select-file-btn').click(function () {
        uploaddlg.find('#uwt-file-input').click()
    })
    LoadFormValues = function (tipindex) {
        var errorMsg = "";
        var data = {};
        function isEmptyObject(obj) {
            for (var i in obj) {
                return false;
            }
            return true;
        }
        $('.uwt-form-hidden-item').each(function () {
            if ($(this).data('type') == 'num') {
                data[$(this).attr('name')] = Number($(this).val());
            } else {
                data[$(this).attr('name')] = $(this).val();
            }
        })
        //  处理cshtml的回调方法
        $('.uwt-cshtml-callback').each(function () {
            var key = $(this).data('key');
            var funcname = $(this).data('func');
            var retData = window[funcname].call();
            if (retData.code == 0) {
                data[key] = retData.data;
            } else {
                errorMsg = "1";
                return;
            }
        })
        if (errorMsg.length > 0) {
            return null;
        }
        //  处理标准输入
        $('.layui-form-item.uwt-form-item,.layui-form-item .layui-inline.uwt-form-item').each(function () {
            var curData = null;
            var key = $(this).data('name');
            var that = this;
            var selector = '#' + key;
            const start = "-start";
            const end = "-end";
            function isRange() {
                return $(that).find('.hasrange').length != 0;
            }
            function checkRangeMaxMin(d) {
                if (d.Max == undefined || d.Min == undefined) {
                    return true;
                }
                if (d.Max > d.Min) {
                    return true;
                }
                appendError("终值不大于起值");
                return false;
            }
            function checkMaxMin(num, s, change) {
                if (num == null) {
                    return true;
                }
                if (change == undefined) {
                    change = function (r) {
                        return r;
                    }
                }
                var value = change(num);
                if (value > change(s.data('max'))) {
                    appendError('不可大于' + s.data('max'));
                    return false;
                }
                if (value < change(s.data('min'))) {
                    appendError('不可小于' + s.data('min'));
                    return false;
                }
                return true;
            }
            function appendError(e) {
                isError = true;
                errorMsg += $(that).data('title') + ' ' + e + "</br>";
            }
            function checkRegex() {
                var r = $(selector).data('regex');
                var regex = new RegExp(r);
                if (!regex.test(curData)) {
                    appendError('不符合规则');
                    return false;
                }
                return true;
            }
            function checkMinLen() {
                if ($(selector).data('minlen') > curData.length) {
                    $(selector).focus();
                    appendError('字数不得少于' + $(selector).data('minlen'))
                    return false;
                }
                return true;
            }
            switch ($(this).data('itemtype')) {
                case 'Hidden'://    单独处理，此处也不处理
                    return;
                case 'Text':
                    var t = $(this).find('.layui-input');
                    if (t.length != 0) {
                        curData = t.val();
                        if (!checkMinLen()) {
                            return;
                        }
                        if (!checkRegex()) {
                            return;
                        }
                        break;
                    }
                    t = $(this).find('.layui-textarea');
                    if (t.length != 0) {
                        curData = t.val();
                        if (!checkMinLen()) {
                            return;
                        }
                        if (!checkRegex()) {
                            return;
                        }
                        break;
                    }
                    t = $(this).find('.layui-editor');
                    if (t.length != 0) {
                        curData = layui.layedit.getContent(t.data('editorid'));
                        break;
                    }
                    t = $(this).find('.wangEditor');
                    if (t.length != 0) {
                        curData = wangEditor[key].txt.html();
                        break;
                    }
                    break;
                case 'Integer':
                case 'Float':
                    if (isRange()) {
                        curData = {};
                        var vmb = $(selector + start).val();
                        if (vmb != '') {
                            curData.Min = Number(vmb);
                            if (!checkMaxMin(curData.Min, $(selector + start))) {
                                return;
                            }
                        }
                        var vme = $(selector + end).val();
                        if (vme != '') {
                            curData.Max = Number(vme);
                            if (!checkMaxMin(curData.Max, $(selector + end))) {
                                return;
                            }
                        }
                        if (isEmptyObject(curData)) {
                            curData = null;
                        } else {
                            if (!checkRangeMaxMin(curData)) {
                                return;
                            }
                        }
                    } else {
                        curData = $(selector).val();
                        if (curData == '') {
                            curData = null;
                        }
                        if (!checkMaxMin(curData, $(selector))) {
                            return;
                        }
                        curData = Number(curData);
                    }
                    break;
                case 'Money':
                    if (isRange()) {
                        curData = {};
                        var vmb = $(selector + start).val();
                        if (vmb != '') {
                            curData.Max = Number(vmb) * (10 ** $(selector + start).data('ex'));
                        }
                        var vme = $(selector + end).val();
                        if (vme != '') {
                            curData.Min = Number(vme) * (10 ** $(selector + end).data('ex'));
                        }
                        if (isEmptyObject(curData)) {
                            curData = null;
                        } else {
                            if (!checkRangeMaxMin(curData)) {
                                return;
                            }
                        }
                    } else {
                        var vm = $(selector).val();
                        if (vm == '') {
                            curData = null;
                        }
                        curData = vm * (10 ** $(selector).data('ex'));
                    }
                    break;
                case 'Date':
                case 'DateTime':
                    var change = function (r) { return new Date(r); }
                    if (isRange()) {
                        curData = {};
                        if ($(selector + start).val() != '') {
                            curData.Min = $(selector + start).val();
                            if (!checkMaxMin(curData.Min, $(selector + start), change)) {
                                return;
                            }
                        }
                        if ($(selector + end).val() != '') {
                            curData.Max = $(selector + end).val();
                            if (!checkMaxMin(curData.Max, $(selector + end), change)) {
                                return;
                            }
                        }
                        if (isEmptyObject(curData)) {
                            curData = null;
                        } else {
                            if (!checkRangeMaxMin(curData)) {
                                return;
                            }
                        }
                    } else {
                        curData = $(selector).val();
                        if (curData == '') {
                            curData = null;
                        } else {
                            if (!checkMaxMin(curData, $(selector), change)) {
                                return;
                            }
                        }
                    }
                    break;
                case 'Time':
                    var change = function (r) {
                        var arr = r.split(':');
                        if (arr.length == 3) {
                            return Number(arr[0]) * 60 * 60 + Number(arr[1]) * 60 + Number(arr[2]);
                        }
                        return -1;
                    }
                    if (isRange()) {
                        curData = {};
                        if ($(selector + start).val() != '') {
                            curData.Min = $(selector + start).val();
                            if (!checkMaxMin(curData.Min, $(selector + start), change)) {
                                return;
                            }
                            curData.Min = change(curData.Min);
                        }
                        if ($(selector + end).val() != '') {
                            curData.Max = $(selector + end).val();
                            if (!checkMaxMin(curData.Max, $(selector + end), change)) {
                                return;
                            }
                            curData.Max = change(curData.Max);
                        }
                        if (isEmptyObject(curData)) {
                            curData = null;
                        } else {
                            if (!checkRangeMaxMin(curData)) {
                                return;
                            }
                        }
                    } else {
                        curData = $(selector).val();
                        if (curData == '') {
                            curData = null;
                        } else {
                            if (!checkMaxMin(curData, $(selector), change)) {
                                return;
                            }
                            curData = change(curData);
                        }
                    }
                    break;
                case 'TimeSpan':
                    var val = $(selector).val();
                    if (isRange()) {
                        if (val == '' || val == ' - ') {
                            curData = null
                        } else {
                            var arr = val.split(' - ');
                            var maxt = buildTimeSpanObj(arr[0]);
                            var mint = buildTimeSpanObj(arr[1]);
                            curData = {
                                max: maxt.day + '.' + maxt.hour + ':' + maxt.minu + ':' + maxt.second,
                                min: mint.day + '.' + mint.hour + ':' + mint.minu + ':' + mint.second
                            }
                        }
                    } else {
                        if (val == '') {
                            curData = null;
                        } else {
                            var mint = buildTimeSpanObj(val);
                            curData = mint.day + '.' + mint.hour + ':' + mint.minu + ':' + mint.second;
                        }
                    }
                    break;
                case 'Password':
                    if ($(selector).data('hasconfirm') == 1) {
                        if ($(selector).val() != $(selector + '-2').val()) {
                            appendError("两次输入密码不同");
                            return;
                        }
                    }
                    curData = $(selector).val();
                    if (!checkMinLen()) {
                        return;
                    }
                    if (!checkRegex()) {
                        return;
                    }
                    break;
                case 'File':
                case 'Color':
                    curData = $(selector).val();
                    break;
                case 'ChooseId':
                    curData = $(selector).val();
                    if ($(selector + "-show").data('mselect') == 'True') {
                        if (curData == '') {
                            curData = [];
                        } else {
                            curData = curData.split(',');
                            var newData = [];
                            for (var i in curData) {
                                newData.push(Number(curData[i]));
                            }
                            curData = newData;
                        }
                    } else {
                        if (curData == '') {
                            curData = 0;
                        } else {
                            curData = Number(curData);
                        }
                    }
                    break;
                case 'SimpleSelect':
                    curData = $(selector).val();
                    if (curData == '') {
                        curData = null;
                    }
                    break;
                case 'MultiSelect':
                    curData = [];
                    $(selector).children("input:checkbox:checked").each(function () {
                        curData.push($(this).data('value'));
                    })
                    break;
                case 'Switch':
                    curData = $(selector).attr('checked') == 'checked';
                    break;
                case 'Slider':
                    curData = $(selector).val();
                    if (isRange()) {
                        var ss = curData.split(',');
                        curData = {
                            Max: ss[1],
                            Min: ss[0]
                        };
                    } else {
                        curData = Number(curData);
                    }
                    break;
                case 'List':
                    break;
                case 'PartCshtml'://    本项使用回调方法不在此处处理
                //  后面两个用来显示 不处理
                case 'ShowImage':
                case 'ShowText':
                    return;

                default:
            }
            if ($(this).data('required') == 1 && (curData == null || curData == '')) {
                appendError("为必填项");
                return;
            }
            data[key] = curData;
        })
        if (errorMsg != '') {
            layui.layer.msg(errorMsg);
            if (tipindex != undefined && tipindex != null) {
                layui.layer.close(tipindex);
            }
            return null;
        }
        return data;
    }
    layui.form.on('checkbox', function (dt) {
        console.log(dt);
        var ms = $(dt.elem).parent();
        if (ms.data('maxselect') == ms.children("input:checkbox:checked").length) {
            ms.children('input:checkbox:not(:checked)').attr("disabled", true);
        } else {
            ms.children('input:checkbox:not(:checked)').attr("disabled", false);
        }
    })
    $('.form-handler').click(function () {
        var loadindex = layui.layer.load(2);
        var cb = $(this).data('cb');
        if (cb != undefined && !window[cb]()) {
            layui.layer.close(loadindex);
            return;
        }
        var data = LoadFormValues(loadindex)
        if (data == null) {
            layui.layer.close(loadindex);
            return;
        }
        var url = "";
        if ($(this).hasClass("form-child")) {
            url = $(this).parents('form').data('url');
        } else {
            url = $("#" + $(this).data('formid')).data("url")
        }
        var h = $(this).data("h");
        if (h != undefined && h != '') {
            url += "?handler=" + $(this).data("h");
        }
        api(url, data, function (rx) {
                layui.layer.close(loadindex);
                layui.layer.msg("成功");
                setTimeout(function () {
                    self.location = document.referrer;
                }, 500);
            }, function (rx) {
                layui.layer.msg(rx.msg);
                layui.layer.close(loadindex);
            }, 'json'
        )
    })
    function refreshDisplayGroups() {
        $(".display-group-select").each(function () {
            var groupname = $(this).data("groupname");
            var val = $(this).val();
            $(".group-" + groupname).hide();
            $('.groupitem-' + val +".group-" + groupname).show();
        })
    }
    refreshDisplayGroups();
    layui.form.on('select(displaygroup)', function (data) {
        refreshDisplayGroups();
    });
})
