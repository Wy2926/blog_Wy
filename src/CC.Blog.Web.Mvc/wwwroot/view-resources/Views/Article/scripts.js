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


