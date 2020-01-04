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