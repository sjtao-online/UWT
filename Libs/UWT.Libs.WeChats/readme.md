# 微信


```js
//  uwt.api.js
var defaultHost = '';
var currentToken = '';
function api_basic (param, funcSuccess, funcComplete, funcError, token) {
    var url = '';
    //  确定URL
    if ('url' in param) {
        url = param.url;
    } else {
        console.log('访问网络，但未定义URL')
    }
    //  确定ContentType
    var header = {
        'Content-Type': "application/x-www-form-urlencoded"
    };
    var data = null;
    if ('data' in param) {
        data = param.data;
    }
    if ('type' in param) {
        if (param.type == 'json') {
            header['Content-Type'] = "application/json";
            data = JSON.stringify(data)
        }
    }
    if (token != '' && token != undefined) {
        header['Access-Token'] = token;
    }
    wx.request({
        url: defaultHost + url,
        data: data,
        header: header,
        method: 'POST',
        dataType: 'json',
        responseType: 'text',
        success: function (res) {
            if (funcSuccess != null) {
                if (res.data.code == 0) {
                    funcSuccess(res.data);
                } else {
                    if (funcError != null && funcError != undefined) {
                        funcError(res.data);
                    } else {
                        wx.showToast({
                            title: res.data.msg,
                        })
                    }
                }
            }
        },
        fail: function (res) { },
        complete: function (res) {
            if (funcComplete != null) {
                funcComplete();
            }
        },
    })
}

function uwt_api(param, funcSuccess, funcComplete, funcError) {
    api_basic(param, funcSuccess, funcComplete, funcError, currentToken);
}

function uwt_login() {
    api_basic({
        url: '/wechat/login',// 更改为登录URL
        data: {
            code: '',
            userInfo: {
                gender: 0
            }
        }
    }, function (rd) {
        //  登录成功
    }, function () {
        //  完成
    }, function (rd) {
        //  出错
    })
}
```
