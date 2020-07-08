var initTreeData;
layui.use(['form', 'tree'], function () {
    var $ = layui.$;
    const tree_id = "main_tree_id";
    var treeData = null;
    var newData = null;
    var defaultTitleProperty = null;
    const formtreemain = '#form-tree-main';
    const formtreetxt = '.layui-tree-txt';
    const editdiv = '#edit-div';
    const formtreeselector = 'layui-tree-txt-selected';
    const moveupbtn = '#move-up';
    const movedownbtn = '#move-down';
    var newidlast = -1;
    var showingEditId = false;
    var showingEditObj = null;
    var delIdList = [];
    function ergodicTreeData(p, callback) {
        for (var i in p) {
            var c = p[i];
            if (c.id == showingEditId) {
                callback(i, p);
                return true;
            }
            if ('children' in c) {
                if (ergodicTreeData(c.children, callback)) {
                    return true;
                }
            }
        }
    }
    
    function calcMoveBtnStatus() {
        ergodicTreeData(treeData, function (i, p) {
            if (i != 0) {
                $(moveupbtn).removeAttr('disabled');
                $(moveupbtn).removeClass('layui-btn-disabled')
            } else {
                $(moveupbtn).attr('disabled', 'disabled');
                $(moveupbtn).addClass('layui-btn-disabled')
            }
            if (i != p.length - 1) {
                $(movedownbtn).removeAttr('disabled');
                $(movedownbtn).removeClass('layui-btn-disabled')
            } else {
                $(movedownbtn).attr('disabled', 'disabled');
                $(movedownbtn).addClass('layui-btn-disabled')
            }
        })
    }
    function p(id) {
        return $('#' + id);
    }
    function fillToEditDiv() {
        $(editdiv).find(".uwt-form-item").each(function () {
            var itemtype = $(this).data('itemtype');
            var itemname = $(this).data('name');
            if (!(itemname in showingEditObj)) {
                return;
            }
            console.log(itemtype + ":" + itemname + "-" + showingEditObj[itemname]);
            switch (itemtype) {
                case "Text":
                    p(itemname).val(showingEditObj[itemname]);
                    break;
                case "Integer":
                case "Float":
                    if (typeof (showingEditObj[itemname]) == 'object') {
                        p(itemname + '-start').val(showingEditObj[itemname].Min)
                        p(itemname + '-end').val(showingEditObj[itemname].Max)
                    } else {
                        p(itemname).val(showingEditObj[itemname])
                    }
                    break;
                case "ChooseId":
                    var arrs = [];
                    var values = chooseAllValues[p(itemname).data("rkey")];
                    if (typeof showingEditObj[itemname] == 'number') {
                        for (var j in values) {
                            if (showingEditObj[itemname] == values[j].Id) {
                                arrs.push(values[j].Title);
                                break;
                            }
                        }
                    } else {
                        var arr = showingEditObj[itemname].split(',');
                        for (var i in arr) {
                            var c = arr[i];
                            for (var j in values) {
                                if (c == values[j].Id) {
                                    arrs.push(values[j].Title);
                                    break;
                                }
                            }
                        }
                    }
                    p(itemname + '-show').val(arrs.toString());
                    break;
                case "SimpleSelect":
                    p(itemname).val(showingEditObj[itemname]);
                    break;
                default:
            }
        })
    }
    function fillTitle(list) {
        for (var i in list) {
            list[i].title = list[i][defaultTitleProperty];
            if ('children' in list[i]) {
                fillTitle(list[i].children);
            }
        }
    }
    function initTreeDataFunc(treedata, defaultNewData, defaultNewTitlePropertyInDefaultNewData) {
        treeData = treedata;
        newData = defaultNewData;
        defaultTitleProperty = defaultNewTitlePropertyInDefaultNewData;
        if (treeData == null) {
            treeData = [];
        }
        fillTitle(treeData)
        layui.tree.render({
            elem: formtreemain,
            data: treeData,
            id: tree_id,
            edit: ['del'],
            onlyIconControl: true,
            click: function (obj) {
                if (obj.data.id !== showingEditId) {
                    showingEditId = obj.data.id
                    showingEditObj = obj.data;
                    $(formtreemain).find(formtreetxt).removeClass(formtreeselector);
                    obj.elem.find(formtreetxt).eq(0).addClass(formtreeselector);
                    $(editdiv).show();
                    fillToEditDiv();
                } else {
                    showingEditId = false;
                    $(formtreemain).find(formtreetxt).removeClass(formtreeselector);
                    $(editdiv).hide();
                    $(moveupbtn).attr('disabled', 'disabled');
                    $(moveupbtn).addClass('layui-btn-disabled')
                    $(movedownbtn).attr('disabled', 'disabled');
                    $(movedownbtn).addClass('layui-btn-disabled')
                }
                calcMoveBtnStatus()
            }, operate: function (obj) {
                if (obj.type === 'del') {
                    showingEditId = obj.data.id;
                    ergodicTreeData(treeData, function (i, p) {
                        p.splice(i, 1);
                    });
                    if (showingEditId > 0) {
                        delIdList.push(showingEditId)
                    }
                    layui.tree.reload(tree_id, { data: treeData });
                    calcMoveBtnStatus();
                }
            }
        })
    }
    initTreeData = initTreeDataFunc;
    formTreeCallback();
    var swapItems = function (arr, index1, index2) {
        if (index2 < 0 || index2 >= arr.length) {
            return;
        }
        arr[index1] = arr.splice(index2, 1, arr[index1])[0];
        return arr;
    };
    $(movedownbtn).click(function () {
        ergodicTreeData(treeData, function (i, p) {
            swapItems(p, i, Number(i) + 1);
        })
        layui.tree.reload(tree_id, { data: treeData });
        calcMoveBtnStatus();
        $('[data-id="' + showingEditId + '"]').find(formtreetxt).eq(0).addClass(formtreeselector);
    })
    $(moveupbtn).click(function () {
        ergodicTreeData(treeData, function (i, p) {
            swapItems(p, i, Number(i) - 1);
        })
        layui.tree.reload(tree_id, { data: treeData });
        calcMoveBtnStatus();
        $('[data-id="' + showingEditId + '"]').find(formtreetxt).eq(0).addClass(formtreeselector);
    })
    $('#tree-add').click(function () {
        var { ...newObj } = newData;
        newObj.id = newidlast;
        newObj.title = newObj[defaultTitleProperty];
        newidlast--;
        if (showingEditId == null) {
            //  顶部添加
            treeData.push(newObj);
        } else { 
            //  下层添加
            var suc = ergodicTreeData(treeData, function (i, p) {
                if (p[i].children == undefined) {
                    p[i].children = [];
                }
                p[i].children.push(newObj);
                p[i].spread = true;
            });
            if (!suc) {
                treeData.push(newObj);
            }
        }
        showingEditId = newObj.id;
        showingEditObj = newObj;
        layui.tree.reload(tree_id, { data: treeData });
        calcMoveBtnStatus();
        $('[data-id="' + showingEditId + '"]').find(formtreetxt).eq(0).addClass(formtreeselector);
        $(editdiv).show();
        fillToEditDiv();
    })
    function replaceEditObject(p, data) {
        for (var i in p) {
            if (p[i].id == data.id) {
                p[i] = data
                return;
            } else {
                if (p[i].children != null && p[i].children != undefined) {
                    replaceEditObject(p[i].children, data)
                }
            }
        }
    }
    $("#save-to-tree-btn").click(function () {
        data = LoadFormValues();
        if (data != null) {
            for (var i in data) {
                if (i == defaultTitleProperty) {
                    showingEditObj.title = data[i];
                }
                showingEditObj[i] = data[i];
            }
            replaceEditObject(treeData, showingEditObj)
            layui.tree.reload(tree_id, { data: treeData });
            $(editdiv).hide();
        }
        return false;
    })
    $("#save-tree-btn").click(function () {
        var url = $(this).data("url");
        api(url, { id: Number($(this).data('id')),delIdList,treeModel : treeData }, function (rx) {
            layui.layer.msg('成功');
            hisBack();
        }, function () {
            console.log(this)
        })
    })
})
