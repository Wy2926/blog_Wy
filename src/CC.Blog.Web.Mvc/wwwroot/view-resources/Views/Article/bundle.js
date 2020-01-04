$('body').show();
$('.version').text(NProgress.version);
NProgress.start();
setTimeout(function () {
    NProgress.done();
    $('.fade').removeClass('out')
}, 1000);
(function () {
    $('img').attr('draggable', 'false');
    $('a').attr('draggable', 'false');
})();

//function setCookie(name, value, time) {
//	var strsec = getsec(time);
//	var exp = new Date();
//	exp.setTime(exp.getTime() + strsec * 1);
//	document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString()
//}
//function getsec(str) {
//	var str1 = str.substring(1, str.length) * 1;
//	var str2 = str.substring(0, 1);
//	if (str2 == "s") {
//		return str1 * 1000
//	} else if (str2 == "h") {
//		return str1 * 60 * 60 * 1000
//	} else if (str2 == "d") {
//		return str1 * 24 * 60 * 60 * 1000
//	}
//}
//function getCookie(name) {
//	var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
//	if (arr = document.cookie.match(reg)) {
//		return unescape(arr[2])
//	} else {
//		return null
//	}
//}
$.fn.navSmartFloat = function () {
    var position = function (element) {
        var top = element.position().top,
            pos = element.css("position");
        $(window).scroll(function () {
            var scrolls = $(this).scrollTop();
            if (scrolls > top) {
                $('.header-topbar').fadeOut(0);
                if (window.XMLHttpRequest) {
                    element.css({
                        position: "fixed",
                        top: 0
                    }).addClass("shadow")
                } else {
                    element.css({
                        top: scrolls
                    })
                }
            } else {
                $('.header-topbar').fadeIn(500);
                element.css({
                    position: pos,
                    top: top
                }).removeClass("shadow")
            }
        })
    };
    return $(this).each(function () {
        position($(this))
    })
};
//$("#navbar").navSmartFloat();
$("#gotop").hide();
$(window).scroll(function () {
    if ($(window).scrollTop() > 100) {
        $("#gotop").fadeIn()
    } else {
        $("#gotop").fadeOut()
    }
});
$("#gotop").click(function () {
    $('html,body').animate({
        'scrollTop': 0
    }, 500)
});
$("img.thumb").lazyload({
    placeholder: "../images/occupying.png",
    effect: "fadeIn"
});
$(".single .content img").lazyload({
    placeholder: "../images/occupying.png",
    effect: "fadeIn"
});

$('[data-toggle="tooltip"]').tooltip();


$(window).scroll(function () {
    var sidebar = $('.sidebar');
    var sidebarHeight = sidebar.height();
    var windowScrollTop = $(window).scrollTop();
    if (windowScrollTop > sidebarHeight + 395 && sidebar.length) {

        $('.fixed').css({
            'position': 'fixed',
            'top': '15px',
            'width': '360px',
            'animation': 'fadeInDown 1s .2s ease both',
            '-webkit-animation': 'fadeInDown 1s .2s ease both',
            '-moz-animation': 'fadeInDown 1s .2s ease both'
        })
        //$('.modal-content').css("z-index", "99999");
    } else {
        $('.fixed').removeAttr("style");
    }
});
//(function() {
//	var oMenu = document.getElementById("rightClickMenu");
//	var aLi = oMenu.getElementsByTagName("li");
//	for (i = 0; i < aLi.length; i++) {
//		aLi[i].onmouseover = function() {
//			$(this).addClass('rightClickMenuActive');
//		};
//		aLi[i].onmouseout = function() {
//			$(this).removeClass('rightClickMenuActive');
//		}
//	}
//	document.oncontextmenu = function(event) {
//		$(oMenu).fadeOut(0);
//		var event = event || window.event;
//		var style = oMenu.style;
//		$(oMenu).fadeIn(300);
//		style.top = event.clientY + "px";
//		style.left = event.clientX + "px";
//		return false
//	};
//	document.onclick = function() {
//		$(oMenu).fadeOut(100);
//	}
//})();
document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e.keyCode === 67 || e.keyCode === 86 || e.keyCode === 13) return true;
    if ((e.keyCode === 123) || (e.ctrlKey) || (e.ctrlKey) && (e.keyCode === 85)) {
        return false
    }
};
//try {
//	if (window.console && window.console.log) {
//		console.log("\n欢迎访问站长素材！\n\n");
//		console.log("\n请记住我们的网址：%c sc.chinaz.com", "color:red")
//	}
//} catch (e) {};

/*只有使用正则全部一次性进行替换*/
function html_encode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
    s = s.replace(/ /g, "&nbsp;");
    return s;
}

function html_decode(str) {
    var s = "";
    if (str.length == 0) return "";
    s = str.replace(/&lt;/g, "<");
    s = s.replace(/&gt;/g, ">");
    s = s.replace(/&nbsp;/g, " ");
    return s;
}

function SiteSearch(send_url, divTgs) {
    var str = $.trim($(divTgs).val());
    if (str.length > 0 && str != "请输入关键字") {
        str = str.replace(/\s+/g, "");
        str = str.replace(/[\ |\~|\`|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\-|\_|\+|\=|\||\\|\[|\]|\{|\}|\;|\:|\"|\'|\,|\<|\.|\>|\/|\?|\，|\。|\：|\；|\·|\~|\！|\、|\《|\》|\‘|\“|\”|\【|\】|\?{|\}|\-|\=|\——|\+|\’|\—|\？]/g, "");
        str = str.replace(/<[^>]*>|/g, "");
        window.location.href = send_url + "?keyword=" + encodeURI(str)
    }
    return false
}

//获取浏览器版本号
var browser = {
    versions: function () {
        var u = navigator.userAgent,
            app = navigator.appVersion;
        return {
            //移动终端浏览器版本信息

            trident: u.indexOf('Trident') > -1,
            //IE内核
            presto: u.indexOf('Presto') > -1,
            //opera内核
            webKit: u.indexOf('AppleWebKit') > -1,
            //苹果、谷歌内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1,
            //火狐内核
            mobile: !!u.match(/AppleWebKit.*Mobile.*/),
            //是否为移动终端
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/),
            //ios终端
            window: u.indexOf('Windows') > -1,
            //windiw系统
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1,
            //android终端或uc浏览器
            iPhone: u.indexOf('iPhone') > -1,
            //是否为iPhone或者QQHD浏览器
            iPad: u.indexOf('iPad') > -1,
            //是否iPad
            webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
        };
    }(),
    language: (navigator.browserLanguage || navigator.language).toLowerCase()
}

//提示弹出框函数
function infoalert(titlecon, textcon, typecon, controlid) {
    sweetAlert({
        title: titlecon,
        text: textcon,
        type: typecon
    });
}

function getbrowserdata() {
    var browserarray = new Array();
    var strbrowser = "";
    //是否为移动端
    strbrowser = strbrowser + browser.versions.mobile + ",";
    //浏览器内核类型
    if (browser.versions.trident == true) {
        strbrowser = strbrowser + "IE,";
    }
    else if (browser.versions.presto == true) {
        strbrowser = strbrowser + "Opera,";
    }
    else if (browser.versions.webKit == true) {
        strbrowser = strbrowser + "Chrome/Apple,";
    }
    else if (browser.versions.gecko == true) {
        strbrowser = strbrowser + "FireFox,";
    }
    //浏览器所属平台
    if (browser.versions.window == true) {
        strbrowser = strbrowser + "Window,";
    }
    else if (browser.versions.ios == true) {
        strbrowser = strbrowser + "IOS,";
    }
    else if (browser.versions.android == true) {
        strbrowser = strbrowser + "Android/Linux,";
    }
    else if (browser.versions.iPhone == true) {
        strbrowser = strbrowser + "iPhone,";
    }
    else if (browser.versions.iPad == true) {
        strbrowser = strbrowser + "iPad,";
    }
    else if (browser.versions.webApp == true) {
        strbrowser = strbrowser + "Safari,";
    }
    //是否为移动端，浏览器类型，浏览器所在系统
    strbrowser = strbrowser.substr(0, strbrowser.length - 1);
    return strbrowser;
}

/********************************用于网站搜索***********************************/
//$(document).ready(function () {
//    $("#btnsearch").click(function () {

//        if ($.trim($("#txtsearch").val()) == "") {
//            txtturn();
//        }
//        else {
//            window.open("../JSSearch.aspx?Searchcon=" + escape(html_encode($.trim($("#txtsearch").val()))), "_blank");//重新打开一个新页面（不关闭细览页）
//        }
//    })
//})

function txtturn() {
    $("#txtsearch").attr("placeholder", "搜索内容不能为空，请输入关键字");
    $("#txtsearch").attr("style", "border:1px solid #ED8C24");
    setTimeout(function () {
        $("#txtsearch").removeAttr("style");
        setTimeout(function () {
            $("#txtsearch").attr("style", "border:1px solid #ED8C24");
            setTimeout(function () {
                $("#txtsearch").removeAttr("style");
                setTimeout(function () {
                    $("#txtsearch").attr("style", "border:1px solid #ED8C24");
                    setTimeout(function () {
                        $("#txtsearch").removeAttr("style");
                        setTimeout(function () {
                            $("#txtsearch").attr("placeholder", "请输入关键字");
                        }, 5000);
                    }, 200)
                }, 200)
            }, 200)
        }, 200)
    }, 200);
}
/********************************用于（移动端）网站搜索***********************************/

//window.onload = function () {
//    $("#btnsearchbymobile").click(function () {
//        if ($.trim($("#txtsearchbymobile").val()) == "") {
//            txtturnbymobile();
//        }
//        else {
//            //  window.location.href = "../JSSearch.aspx?Searchcon=" + escape($.trim($("#txtsearch").val()));//跳转页面
//            window.open("../JSSearch.aspx?Searchcon=" + escape(html_encode($.trim($("#txtsearchbymobile").val()))), "_blank");//重新打开一个新页面（不关闭细览页）
//        }
//    })
//}

function txtturnbymobile() {
    $("#txtsearchbymobile").attr("placeholder", "搜索内容不能为空，请输入关键字");
    $("#txtsearchbymobile").attr("style", "border:1px solid #ED8C24");
    setTimeout(function () {
        $("#txtsearchbymobile").removeAttr("style");
        setTimeout(function () {
            $("#txtsearchbymobile").attr("style", "border:1px solid #ED8C24");
            setTimeout(function () {
                $("#txtsearchbymobile").removeAttr("style");
                setTimeout(function () {
                    $("#txtsearchbymobile").attr("style", "border:1px solid #ED8C24");
                    setTimeout(function () {
                        $("#txtsearchbymobile").removeAttr("style");
                        setTimeout(function () {
                            $("#txtsearchbymobile").attr("placeholder", "请输入关键字");
                        }, 5000);
                    }, 200)
                }, 200)
            }, 200)
        }, 200)
    }, 200);
}



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
(function(e) {
	"use strict";
	Date.now = Date.now ||
	function() {
		return +(new Date)
	}, e.ias = function(t) {
		function u() {
			var t;
			i.onChangePage(function(e, t, r) {
				s && s.setPage(e, r), n.onPageChange.call(this, e, r, t)
			});
			if (n.triggerPageThreshold > 0) a();
			else if (e(n.next).attr("href")) {
				var u = r.getCurrentScrollOffset(n.scrollContainer);
				E(function() {
					p(u)
				})
			}
			return s && s.havePage() && (l(), t = s.getPage(), r.forceScrollTop(function() {
				var n;
				t > 1 ? (v(t), n = h(!0), e("html, body").scrollTop(n)) : a()
			})), o
		}
		function a() {
			c(), n.scrollContainer.scroll(f)
		}
		function f() {
			var e, t;
			e = r.getCurrentScrollOffset(n.scrollContainer), t = h(), e >= t && (m() >= n.triggerPageThreshold ? (l(), E(function() {
				p(e)
			})) : p(e))
		}
		function l() {
			n.scrollContainer.unbind("scroll", f)
		}
		function c() {
			e(n.pagination).hide()
		}
		function h(t) {
			var r, i;
			return r = e(n.container).find(n.item).last(), r.size() === 0 ? 0 : (i = r.offset().top + r.height(), t || (i += n.thresholdMargin), i)
		}
		function p(t, r) {
			var s;
			s = e(n.next).attr("href");
			if (!s) return n.noneleft && e(n.container).find(n.item).last().after(n.noneleft), l();
			if (n.beforePageChange && e.isFunction(n.beforePageChange) && n.beforePageChange(t, s) === !1) return;
			i.pushPages(t, s), l(), y(), d(s, function(t, i) {
				var o = n.onLoadItems.call(this, i),
					u;
				o !== !1 && (e(i).hide(), u = e(n.container).find(n.item).last(), u.after(i), e(i).fadeIn()), s = e(n.next, t).attr("href"), e(n.pagination).replaceWith(e(n.pagination, t)), b(), c(), s ? a() : l(), n.onRenderComplete.call(this, i), r && r.call(this)
			})
		}
		function d(t, r, i) {
			var s = [],
				o, u = Date.now(),
				a, f;
			i = i || n.loaderDelay, e.get(t, null, function(t) {
				o = e(n.container, t).eq(0), 0 === o.length && (o = e(t).filter(n.container).eq(0)), o && o.find(n.item).each(function() {
					s.push(this)
				}), r && (f = this, a = Date.now() - u, a < i ? setTimeout(function() {
					r.call(f, t, s)
				}, i - a) : r.call(f, t, s))
			}, "html")
		}
		function v(t) {
			var n = h(!0);
			n > 0 && p(n, function() {
				l(), i.getCurPageNum(n) + 1 < t ? (v(t), e("html,body").animate({
					scrollTop: n
				}, 400, "swing")) : (e("html,body").animate({
					scrollTop: n
				}, 1e3, "swing"), a())
			})
		}
		function m() {
			var e = r.getCurrentScrollOffset(n.scrollContainer);
			return i.getCurPageNum(e)
		}
		function g() {
			var t = e(".ias_loader");
			return t.size() === 0 && (t = e('<div class="ias_loader">' + n.loader + "</div>"), t.hide()), t
		}
		function y() {
			var t = g(),
				r;
			n.customLoaderProc !== !1 ? n.customLoaderProc(t) : (r = e(n.container).find(n.item).last(), r.after(t), t.fadeIn())
		}
		function b() {
			var e = g();
			e.remove()
		}
		function w(t) {
			var r = e(".ias_trigger");
			return r.size() === 0 && (r = e('<div class="ias_trigger"><a href="javascript:;">' + n.trigger + "</a></div>"), r.hide()), e("a", r).unbind("click").bind("click", function() {
				return S(), t.call(), !1
			}), r
		}
		function E(t) {
			var r = w(t),
				i;
			n.customTriggerProc !== !1 ? n.customTriggerProc(r) : (i = e(n.container).find(n.item).last(), i.after(r), r.fadeIn())
		}
		function S() {
			var e = w();
			e.remove()
		}
		var n = e.extend({}, e.ias.defaults, t),
			r = new e.ias.util,
			i = new e.ias.paging(n.scrollContainer),
			s = n.history ? new e.ias.history : !1,
			o = this;
		u()
	}, e.ias.defaults = {
		container: "#container",
		scrollContainer: e(window),
		item: ".item",
		pagination: "#pagination",
		next: ".next",
		noneleft: !1,
		loader: '<img src="images/loader.gif"/>',
		loaderDelay: 600,
		triggerPageThreshold: 3,
		trigger: "Load more items",
		thresholdMargin: 0,
		history: !0,
		onPageChange: function() {},
		beforePageChange: function() {},
		onLoadItems: function() {},
		onRenderComplete: function() {},
		customLoaderProc: !1,
		customTriggerProc: !1
	}, e.ias.util = function() {
		function i() {
            e(window).load(function() {
				t = !0
			})
		}
		var t = !1,
			n = !1,
			r = this;
		i(), this.forceScrollTop = function(i) {
			e("html,body").scrollTop(0), n || (t ? (i.call(), n = !0) : setTimeout(function() {
				r.forceScrollTop(i)
			}, 1))
		}, this.getCurrentScrollOffset = function(e) {
			var t, n;
			return e.get(0) === window ? t = e.scrollTop() : t = e.offset().top, n = e.height(), t + n
		}
	}, e.ias.paging = function() {
		function s() {
			e(window).scroll(o)
		}
		function o() {
			var t, s, o, f, l;
			t = i.getCurrentScrollOffset(e(window)), s = u(t), o = a(t), r !== s && (f = o[0], l = o[1], n.call({}, s, f, l)), r = s
		}
		function u(e) {
			for (var n = t.length - 1; n > 0; n--) if (e > t[n][0]) return n + 1;
			return 1
		}
		function a(e) {
			for (var n = t.length - 1; n >= 0; n--) if (e > t[n][0]) return t[n];
			return null
		}
		var t = [
			[0, document.location.toString()]
		],
			n = function() {},
			r = 1,
			i = new e.ias.util;
		s(), this.getCurPageNum = function(t) {
			return t = t || i.getCurrentScrollOffset(e(window)), u(t)
		}, this.onChangePage = function(e) {
			n = e
		}, this.pushPages = function(e, n) {
			t.push([e, n])
		}
	}, e.ias.history = function() {
		function n() {
			t = !! (window.history && history.pushState && history.replaceState), t = !1
		}
		var e = !1,
			t = !1;
		n(), this.setPage = function(e, t) {
			this.updateState({
				page: e
			}, "", t)
		}, this.havePage = function() {
			return this.getState() !== !1
		}, this.getPage = function() {
			var e;
			return this.havePage() ? (e = this.getState(), e.page) : 1
		}, this.getState = function() {
			var e, n, r;
			if (t) {
				n = history.state;
				if (n && n.ias) return n.ias
			} else {
				e = window.location.hash.substring(0, 7) === "#/page/";
				if (e) return r = parseInt(window.location.hash.replace("#/page/", ""), 10), {
					page: r
				}
			}
			return !1
		}, this.updateState = function(t, n, r) {
			e ? this.replaceState(t, n, r) : this.pushState(t, n, r)
		}, this.pushState = function(n, r, i) {
			var s;
			t ? history.pushState({
				ias: n
			}, r, i) : (s = n.page > 0 ? "#/page/" + n.page : "", window.location.hash = s), e = !0
		}, this.replaceState = function(e, n, r) {
			t ? history.replaceState({
				ias: e
			}, n, r) : this.pushState(e, n, r)
		}
	}
})(jQuery);
$(function () {
    $('#ReturnUrlHash').val(location.hash);

    var $loginForm = $('#LoginForm');

    $loginForm.validate({
        highlight: function (input) {
            $(input).parents('.form-line').addClass('error');
        },
        unhighlight: function (input) {
            $(input).parents('.form-line').removeClass('error');
        },
        errorPlacement: function (error, element) {
            $(element).parents('.input-group').append(error);
        }
    });

    $loginForm.submit(function (e) {
        e.preventDefault();

        if (!$loginForm.valid()) {
            return;
        }

        abp.ui.setBusy(
            $('#LoginArea'),

            abp.ajax({
                contentType: 'application/x-www-form-urlencoded',
                url: $loginForm.attr('action'),
                data: $loginForm.serialize()
            })
        );
    });

    $('a.social-login-link').click(function () {
        var $a = $(this);
        var $form = $a.closest('form');
        $form.find('input[name=provider]').val($a.attr('data-provider'));
        $form.submit();
    });

    $loginForm.find('input[type=text]:first-child').focus();
});

(function () {
    $(function () {
        var _$modal = $('#linkModel');
        var _$form = _$modal.find('form');

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();
            var link = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            abp.ajax({
                url: abp.appPath +'api/services/app/Blog/FriendshipLinkApply',
                type: 'POST',
                data: JSON.stringify(link),
                contentType: 'application/json',
                success: function (content) {
                    abp.message.success(abp.utils.formatString(abp.localization.localize('网站：{0} 链接 {1}', 'Blog'), link.Name, link.Url), '申请成功，等待审核');
                },
                error: function (e) { }
            });
        });

        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });
    });
})();

/*
Water ripple effect

Original code (Java) by Neil Wallis
Code snipplet adapted to Javascript by Sergey Chikuyonok
Code re-written as jQuery plugin by Niklas Knaack

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

!function(){function a(a,b){function z(){e=new Image,e.onload=function(){B()},e.src=c.image}function A(){y.drawImage(e,0,0),t=y.getImageData(0,0,h,i),s=y.getImageData(0,0,h,i),u=s.data,v=t.data}function B(){A();for(var a=0;a<l;a++)r[a]=q[a]=0;C(),c.auto&&(g=setInterval(function(){E(Math.random()*h,Math.random()*i)},f),E(Math.random()*h,Math.random()*i))}function C(){requestAnimFrame(C),D()}function D(){var a;a=m,m=n,n=a,a=0,o=m;for(var b=0;b<i;b++)for(var c=0;c<h;c++){var d=q[o-h]+q[o+h]+q[o-1]+q[o+1]>>1;d-=q[n+a],d-=d>>5,q[n+a]=d,d=1024-d;var e=r[a];if(r[a]=d,e!=d){var f=((c-j)*d/1024<<0)+j,g=((b-k)*d/1024<<0)+k;f>=h&&(f=h-1),f<0&&(f=0),g>=i&&(g=i-1),g<0&&(g=0);var l=4*(f+g*h),p=4*a;u[p]=v[l],u[p+1]=v[l+1],u[p+2]=v[l+2]}++o,++a}y.putImageData(s,0,0)}function E(a,b){a<<=0,b<<=0;for(var c=b-p,d=b+p;c<d;c++)for(var e=a-p,f=a+p;e<f;e++)q[m+c*h+e]+=512}var c={image:"",rippleRadius:3,width:480,height:480,delay:1,auto:!0};if(void 0!==b)for(var d in b)b.hasOwnProperty(d)&&c.hasOwnProperty(d)&&(c[d]=b[d]);if(!c.image.length)return!1;var e,g,s,t,u,v,f=1e3*c.delay,h=c.width,i=c.height,j=h/2,k=i/2,l=h*(i+2)*2,m=h,n=h*(i+3),o=0,p=c.rippleRadius,q=[],r=[],w=document.createElement("div");w.style.width=h+"px",w.style.height=i+"px",a.appendChild(w);var x=document.createElement("canvas");x.width=h,x.height=i,w.appendChild(x);var y=x.getContext("2d");y.fillStyle=c.bgColor,y.fillRect(0,0,h,i),window.requestAnimFrame=function(){return window.requestAnimationFrame||window.webkitRequestAnimationFrame||window.mozRequestAnimationFrame||function(a){window.setTimeout(a,1e3/60)}}(),this.disturb=function(a,b){E(a,b)},z()}window.WaterRippleEffect=a}(),"undefined"!=typeof jQuery&&!function(a){a.fn.waterRippleEffect=function(b){var c=arguments;return this.each(function(){if(a.data(this,"plugin_WaterRippleEffect")){var d=a.data(this,"plugin_WaterRippleEffect");d[b]?d[b].apply(this,Array.prototype.slice.call(c,1)):a.error("Method "+b+" does not exist on jQuery.waterRippleEffect")}else a.data(this,"plugin_WaterRippleEffect",new WaterRippleEffect(this,b))})}}(jQuery);