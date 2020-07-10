var hasdate = 'hasdate' in this.document.currentScript.attributes;
var uilist = ["form", "layer"];
if (hasdate) {
    uilist.push("date");
}
function check(c) {
    var r = /^[0-9]+([.]{1}[0-9]+){0,1}$/;
    return r.test(c);
}
layui.use(uilist, function () {
    var $ = layui.$;
    $('#btn-search').click(function () {
        var queryx = "";
        error = '';
        var appendError = function (id, err) {
            error += $(id + "-title").val() + ":" + err + "\n";
        }
        $(".layui-inline").each(function () {
            var valueType = $(this).data('value-type');
            var filterType = $(this).data('filter-type');
            var name = $(this).data('name');
            var tvalue = "";
            var op = "";
            switch (valueType) {
                case "FloatNumber":
                case "IntegerNumber":
                    if (filterType === 'Between') {
                        op = $('#' + name + '-start').data('op');
                        var begin = $('#' + name + '-start').val();
                        var end = $('#' + name + '-end').val();
                        if (begin === '' && end === '') {
                            return;
                        }
                        if (begin !== '') {
                            if (!check(begin)) {
                                appendError(name, "不是正常数值");
                                return;
                            }
                        }
                        if (end !== '') {
                            if (!check(end)) {
                                appendError(name, "不是正常数值");
                                return;
                            }
                        }
                        tvalue = {
                            Min: Number(begin),
                            Max: Number(end),
                        }
                    } else {
                        op = $('#' + name).data('op');
                        tvalue = $('#' + name).val();
                        if (tvalue === '') {
                            return;
                        }
                        tvalue = Number(tvalue);
                    }
                    break;
                case "Money":
                    if (filterType === 'Between') {
                        op = $('#' + name + '-start').data('op');
                        var begin = $('#' + name + '-start').val();
                        var end = $('#' + name + '-end').val();
                        if (begin === '' && end === '') {
                            return;
                        }
                        if (begin !== '') {
                            if (!check(begin)) {
                                appendError(name, "不是正常数值");
                                return;
                            }
                            begin *= 100;
                        }
                        if (end !== '') {
                            if (!check(end)) {
                                appendError(name, "不是正常数值");
                                return;
                            }
                            end *= 100;
                        }
                        tvalue = {
                            Min: Number(begin),
                            Max: Number(end),
                        }
                    } else {
                        op = $('#' + name).data('op');
                        tvalue = $('#' + name).val();
                        if (tvalue === '') {
                            return;
                        }
                        tvalue = Number(tvalue) * 100;
                    }
                    break;
                case "Text":
                    tvalue = $('#' + name).val().replace("\\", "\\\\");
                    if (tvalue === '') {
                        return;
                    }
                    tvalue = '\\' + tvalue + "\\";
                    op = $('#' + name).data('op');
                    break;
                case "DateTime":
                    op = $('#' + name).data('op');
                    if (filterType === 'Between') {
                        tvalue = $('#' + name).val();
                    } else {
                        tvalue = $('#' + name).val();
                    }
                    break;
                case "IdComboBox":
                    op = "";
                    tvalue = $('#' + name).val();
                    if (tvalue.length === 2) {
                        return;
                    }
                    break;
                case "TagMSelector":

                    break;
                case "TagSSelector":
                    tvalue = $('#' + name).val();
                    op = "";
                    if (tvalue.length === 2) {
                        return;
                    }
                    break;
                default:
            }
            queryx += name + ":" + op + tvalue + "&";
        });
        if (error !== '') {
            layui.layer.msg(error);
            return;
        }
        var urlbase = "?";
        $('.cshtml-filter').each(function () {
            var q = window[$(this).data('search')]();
            for (var i in q) {
                urlbase += i + "=" + URL.encodeURIComponent(q[i]) + "&";
            }
        })
        if (queryx.length > 1) {
            queryx = queryx.substring(0, queryx.length - 1);
            window.location.href = urlbase + "query=" + encodeURIComponent(queryx);
        } else {
            window.location.href = urlbase;
        }
    });
    $('#btn-reset').click(function () {
        $('.layui-inline').each(function () {
            $(this).find("input").val("");
        })
        $('.cshtml-filter').each(function () {
            var reset = $(this).data('reset');
            window[reset]();
        })
    });
    $('.select-has-children').change(function () {

    })
});