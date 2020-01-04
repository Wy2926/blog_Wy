// JavaScript Document
//  加入收藏 <a onclick="AddFavorite(window.location,document.title)">加入收藏</a>

function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}
//设为首页 <a onclick="SetHome(this,window.location)">设为首页</a>
function SetHome(obj, vrl) {
    try {
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
}

//显示提交中
function submiting(btnId) {
    setTimeout(function () {
        $("#" + btnId).val("提交中").attr("disabled", "disabled");
    }, 10);
}



//若session过期或者页面未进行登录时，返回登陆页面`
function Exit() {
    window.top.location.href = "/YYS_manager/YYS_Login.aspx";
}

////打开界面
//function openlayer(url,titlestr) {
//    art.dialog.open(url, { title: titlestr});
//}

function getCookieVal(offset) //取得项名称为offset的cookie值
{
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1)
        endstr = document.cookie.length;
    return unescape(document.cookie.substring(offset, endstr));
}

function GetCookie(name) //取得名称为name的cookie值 
{
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen) {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return getCookieVal(j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0) break;
    }
    return null;
}
function SetCookie(name, value) { //设置名称为name,值为value的Cookie 
    var argc = SetCookie.arguments.length;
    var argv = SetCookie.arguments;
    var path = (argc > 3) ? argv[3] : null;
    var domain = (argc > 4) ? argv[4] : null;
    var secure = (argc > 5) ? argv[5] : false;

    var mm = 365 * 3600 * 1000 * 24;
    var date = new Date();
    date.setTime(date.getTime() + mm);


    document.cookie = name + "=" + escape(value) +
        ((path == null) ? "" : ("; path=" + path)) +
        ((domain == null) ? "" : ("; domain=" + domain)) +
        ((secure == true) ? "; secure" : "") + ";expires=" + date.toGMTString();
}
function DelCookie(name) {   //删除名称为name的Cookie  
    var ThreeDays = 3 * 24 * 60 * 60 * 1000;//设置过期时间为三天前
    var expDate = new Date();
    expDate.setTime(expDate.getTime() - ThreeDays);
    document.cookie = name + "=;expires=" + expDate.toGMTString();
}

function setclearCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}
//获取浏览器地址栏中参数（注意非空判断）
//var myurl = GetQueryString("url");
//if (myurl != null && myurl.toString().length > 1) {
//    alert(GetQueryString("url"));
//}
function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//展示原图
function ShowImage(title, imgurl) {
    art.dialog({
        padding: 0,
        title: title,
        content: imgurl,
        lock: true
    });
}

/**
* 弹出确认层
* @content  {String}   消息内容
* @callback {function} 确认按钮回调函数 
*/
function showAlert(content, icon, callback) {
    var w = this.window;
    try {
        w = getRootWindow(window);
    } catch (e) { }
    w.art.dialog({
        title: '消息',
        content: content,
        icon: icon || 'warning',
        //  lock: true,
        ok: function () {
            if (callback != null && callback != undefined)
                callback();
            else
                this.close();
        },
        init: function () {
            this.content(content);
        }
    });
}

function alerLoading(strMsg) {
    $.dialog({
        id: "loginload",
        content: '<div><img style="vertical-align:middle;" src="/images/loading.gif" width="28" height="28" title="正在提交..."/>&nbsp;&nbsp;<span style="vertical-align:middle;font-size:15px;font-weight:600;"> ' + strMsg + '</span></div>',
        esc: false,
        cancel: false,
        drag: false
    });

}

//弹出层窗口
//参数：
//obj:{url:'system/branchdept/infoBranchdept.do?parentNo="+treeNode.id+"',width:'420px',height:'280px',title:'我是标题',ico:'images/swlc.gif'}
//ico不设置时则不使用图标，如果空值则使用默认图标
//返回值：窗口对象
function layerOpen(obj) {
    obj.resize = false;
    obj.opacity = 0.5;
    var title = obj.title;
    var lock = obj.lock || false; //是否锁屏
    var max = obj.max || false; //是否显示最大化按钮
    var min = obj.min || false; //是否显示最小化按钮
    //   var resize = obj.resize || true; //是否可以调用窗口大小
    var skin = obj.skin || ''; //皮肤
    var id = obj.id || 'lhgDiv'; //窗口ID
    var drag = obj.drag || true; //是否禁止窗口移动
    var padding = obj.padding || '0px';
    var onClose = obj.onClose;
    if (!title) {
        title = "";
    }

    art.dialog.open(obj.url, obj);
}

/**
* 关闭弹出层
* @author LiWeiWen
* @since 2012-07-11 21:12:23
*/
function layerClose() {
    art.dialog.close();
}

/**
* 获取弹出层的父层
* @author RaymonLee
* @since 2012-09-24 21:12:23
*/
function getParentLayer() {
    return art.dialog.open.origin;
}

/**
* 文字提示层
* @content	    {String}	消息内容
* @time	    {int}	窗口显示时间，默认3秒
* @callback	{function}	回调函数
* @showLoadIco {bool}  显示正在加载图标
*/
function showTips(content, time) {
    art.dialog.tips(content, time || 3);
}

/**
* 弹出确认层，点击确认时调用callbackYes函数,取消时调用callbackNo函数
* @content	    {String}	消息内容
* @callbackYes {function}  确认按钮回调函数
* @callbackNo  {function}  取消按钮回调函数
*/
function showConfirm(content, callbackYes, callbackNo) {
    art.dialog.confirm(content, callbackYes, callbackNo);
}


/*
比较两个时间大小，时间格式 yyyy-MM-dd HH:mm:ss
*/
//function checkEndTime(startTime, endTime) {
//    var start = new Date(startTime.replace("-", "/").replace("-", "/"));
//    var end = new Date(endTime.replace("-", "/").replace("-", "/"));
//    if (end < start) {
//        return false;
//    }
//    return true;
//}


//function p(s) {
//    return s < 10 ? '0' + s : s;
//}

//function getnowdateime() {
//    var myDate = new Date();
//    //获取当前年
//    var year = myDate.getFullYear();
//    //获取当前月
//    var month = myDate.getMonth() + 1;
//    //获取当前日
//    var date = myDate.getDate();
//    var h = myDate.getHours();       //获取当前小时数(0-23)
//    var m = myDate.getMinutes();     //获取当前分钟数(0-59)
//    var s = myDate.getSeconds();

//    var now = year + '-' + p(month) + "-" + p(date) + " " + p(h) + ':' + p(m) + ":" + p(s);

//    return now;
//}