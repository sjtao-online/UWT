﻿function clipboardCopy(copyText) {
    var txt = document.createElement("input");
    txt.value = copyText;
    txt.style.opacity = 0;
    document.body.appendChild(txt)
    txt.select();
    document.execCommand("Copy");
}

//格式化文件大小
function renderSize(value) {
    if (null == value || value == "") {
        return "0 Bytes";
    }
    var unitArr = new Array("Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB");
    var index = 0;
    var srcsize = parseFloat(value);
    index = Math.floor(Math.log(srcsize) / Math.log(1024));
    var size = srcsize / Math.pow(1024, index);
    size = size.toFixed(2);//保留的小数位数
    return size + unitArr[index];
}

function btnDisable(jqueryBtn) {
    jqueryBtn.prop('disabled', 'disabled');
    jqueryBtn.addClass('layui-btn-disabled');
}

function btnEnable(jqueryBtn) {
    jqueryBtn.removeAttr('disabled');
    jqueryBtn.removeClass('layui-btn-disabled');
}

function getClassNameTagCount(arr) {
    var num = 0;
    for (var i in arr) {
        var c = arr[i];
        num += document.getElementsByClassName(c).length;
    }
    return num;
}

function api(url, data, success, fail, type, method) {
    if (method === undefined) {
        method = "POST";
    }
    var ajaxdata = {
        error: function () {
            alert("网络错误");
        },
        success: function (rx) {
            ajaxSuccess(rx, success, fail);
        },
        dataType: "json",
        type: method,
        headers: {
            "client-version": "Web-1.0"
        }
    };
    if (url === null) {
        url = window.location.href;
    }
    ajaxdata.url = url;
    if (type === 'form') {
        ajaxdata.contentType = "application/x-www-form-urlencoded";
        ajaxdata.data = data;
    } else {
        ajaxdata.contentType = "application/json";
        ajaxdata.data = JSON.stringify(data);
    }
    $.ajax(ajaxdata);
}

function hisBack(backurl) {
    if (backurl == undefined) {
        self.location = document.referrer;
    } else {
        self.location = backurl;
    }
}

function uploadfile(url,data, success, fail, process) {

    $.ajax({
        url: url,
        contentType: false,
        processData: false,
        data: data,
        dataType: "json",
        type: "post",
        xhr: function () { //获取ajaxSettings中的xhr对象，为它的upload属性绑定progress事件的处理函数  
            myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) { //检查upload属性是否存在  
                //绑定progress事件的回调函数  
                myXhr.upload.addEventListener('progress', function (e) {
                    var curr = e.loaded;
                    var total = e.total;
                    process(curr / total * 100);
                }, false);
            }
            return myXhr; //xhr对象返回给jQuery使用 
        },
        error: function () {
            alert("网络错误");
        },
        success: function (rx) {
            ajaxSuccess(rx, success, fail);
        }
    });
}




function ajaxSuccess(rx, success, fail) {
    if ('__vmap__' in rx) {
        if (!('code' in rx)) {
            rx.code = rx[rx.__vmap__[0]];
        }
        if (!('msg' in rx)) {
            rx.msg = rx[rx.__vmap__[1]];
        }
        if (!('data' in rx) && rx.__vmap__.length > 2 && rx[rx.__vmap__[2]] in rx) {
            rx.data = rx[rx.__vmap__[2]];
        }
    }
    if (rx.code === 0) {
        success(rx);
        return;
    }
    console.log(rx.code, rx.msg);
    if (fail == null || fail == undefined) {
        alert(rx.msg);
    }
    fail(rx);
}