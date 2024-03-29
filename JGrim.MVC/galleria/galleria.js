/*
Galleria v 1.2.2 2011-02-25
http://galleria.aino.se

Copyright (c) 2011, Aino
Licensed under the MIT license.
*/
(function (e) {
    var s = this, t = s.document, I = e(t), E = false, x = navigator.userAgent.toLowerCase(), J = s.location.hash.replace(/#\//, ""), y = function () { return j.TOUCH ? "touchstart" : "click" }, u = function () { var a = 3, b = t.createElement("div"), c = b.getElementsByTagName("i"); do b.innerHTML = "<!--[if gt IE " + ++a + "]><i></i><![endif]--\>"; while (c[0]); return a > 4 ? a : void 0 } (), z = function () { return { html: t.documentElement, body: t.body, head: t.getElementsByTagName("head")[0], title: t.title} }, K = function () {
        var a = []; e.each("data ready thumbnail loadstart loadfinish image play pause progress fullscreen_enter fullscreen_exit idle_enter idle_exit rescale lightbox_open lightbox_close lightbox_image".split(" "),
function (b, c) { a.push(c); /_/.test(c) && a.push(c.replace(/_/g, "")) }); return a
    } (), L = function (a) { var b; if (typeof a !== "object") return a; e.each(a, function (c, d) { if (/^[a-z]+_/.test(c)) { b = ""; e.each(c.split("_"), function (i, k) { b += i > 0 ? k.substr(0, 1).toUpperCase() + k.substr(1) : k }); a[b] = d; delete a[c] } }); return a }, F = function (a) { if (e.inArray(a, K) > -1) return j[a.toUpperCase()]; return a }, B = { trunk: {}, add: function (a, b, c, d) {
        d = d || false; this.clear(a); if (d) { var i = b; b = function () { i(); B.add(a, b, c) } } this.trunk[a] = s.setTimeout(b,
c)
    }, clear: function (a) { var b = function (d) { s.clearTimeout(this.trunk[d]); delete this.trunk[d] }, c; if (a && a in this.trunk) b.call(B, a); else if (typeof a === "undefined") for (c in this.trunk) this.trunk.hasOwnProperty(c) && b.call(B, c) } 
    }, C = [], g = function () {
        return { array: function (a) { return Array.prototype.slice.call(a) }, create: function (a, b) { b = b || "div"; var c = t.createElement(b); c.className = a; return c }, forceStyles: function (a, b) { a = e(a); a.attr("style") && a.data("styles", a.attr("style")).removeAttr("style"); a.css(b) }, revertStyles: function () {
            e.each(g.array(arguments),
function (a, b) { b = e(b).removeAttr("style"); b.data("styles") && b.attr("style", b.data("styles")).data("styles", null) })
        }, moveOut: function (a) { g.forceStyles(a, { position: "absolute", left: -1E4 }) }, moveIn: function () { g.revertStyles.apply(g, g.array(arguments)) }, hide: function (a, b, c) { a = e(a); a.data("opacity") || a.data("opacity", a.css("opacity")); var d = { opacity: 0 }; b ? a.stop().animate(d, b, c) : a.css(d) }, show: function (a, b, c) {
            a = e(a); var d = parseFloat(a.data("opacity")) || 1, i = { opacity: d }; d === 1 && a.data("opacity", null); b ? a.stop().animate(i,
b, c) : a.css(i)
        }, addTimer: function () { B.add.apply(B, g.array(arguments)); return this }, clearTimer: function () { B.clear.apply(B, g.array(arguments)); return this }, wait: function (a) { a = e.extend({ until: function () { return false }, success: function () { }, error: function () { j.raise("Could not complete wait function.") }, timeout: 3E3 }, a); var b = g.timestamp(), c, d, i = function () { d = g.timestamp(); c = d - b; if (a.until(c)) { a.success(); return false } if (d >= b + a.timeout) { a.error(); return false } s.setTimeout(i, 2) }; s.setTimeout(i, 2) }, toggleQuality: function (a,
b) { if (!(u !== 7 && u !== 8 || !a)) { if (typeof b === "undefined") b = a.style.msInterpolationMode === "nearest-neighbor"; a.style.msInterpolationMode = b ? "bicubic" : "nearest-neighbor" } }, insertStyleTag: function (a) { var b = t.createElement("style"); z().head.appendChild(b); if (b.styleSheet) b.styleSheet.cssText = a; else { a = t.createTextNode(a); b.appendChild(a) } }, loadScript: function (a, b) {
    var c = false, d = e("<script>").attr({ src: a, async: true }).get(0); d.onload = d.onreadystatechange = function () {
        if (!c && (!this.readyState || this.readyState ===
"loaded" || this.readyState === "complete")) { c = true; d.onload = d.onreadystatechange = null; typeof b === "function" && b.call(this, this) } 
    }; z().head.appendChild(d)
}, parseValue: function (a) { if (typeof a === "number") return a; else if (typeof a === "string") return (a = a.match(/\-?\d/g)) && a.constructor === Array ? parseInt(a.join(""), 10) : 0; else return 0 }, timestamp: function () { return (new Date).getTime() }, loadCSS: function (a, b, c) {
    var d, i = false, k; e("link[rel=stylesheet]").each(function () { if (RegExp(a).test(this.href)) { d = this; return false } });
    if (typeof b === "function") { c = b; b = void 0 } c = c || function () { }; if (d) { c.call(d, d); return d } k = t.styleSheets.length; if (E) a += "?" + g.timestamp(); if (e("#" + b).length) { e("#" + b).attr("href", a); k--; i = true } else { d = e("<link>").attr({ rel: "stylesheet", href: a, id: b }).get(0); s.setTimeout(function () { var l = e('link[rel="stylesheet"], style'); l.length ? l.get(0).parentNode.insertBefore(d, l[0]) : z().head.appendChild(d); if (u) d.attachEvent("onreadystatechange", function () { if (d.readyState === "complete") i = true }); else i = true }, 10) } typeof c ===
"function" && g.wait({ until: function () { return i && t.styleSheets.length > k }, success: function () { g.addTimer("css", function () { c.call(d, d) }, 100) }, error: function () { j.raise("Theme CSS could not load") }, timeout: 1E3 }); return d
} 
        }
    } (), G = { fade: function (a, b) { e(a.next).css("opacity", 0).show().animate({ opacity: 1 }, a.speed, b); a.prev && e(a.prev).css("opacity", 1).show().animate({ opacity: 0 }, a.speed) }, flash: function (a, b) {
        e(a.next).css("opacity", 0); a.prev ? e(a.prev).animate({ opacity: 0 }, a.speed / 2, function () {
            e(a.next).animate({ opacity: 1 },
a.speed, b)
        }) : e(a.next).animate({ opacity: 1 }, a.speed, b)
    }, pulse: function (a, b) { a.prev && e(a.prev).hide(); e(a.next).css("opacity", 0).animate({ opacity: 1 }, a.speed, b) }, slide: function (a, b) { var c = e(a.next).parent(), d = this.$("images"), i = this._stageWidth, k = this.getOptions("easing"); c.css({ left: i * (a.rewind ? -1 : 1) }); d.animate({ left: i * (a.rewind ? 1 : -1) }, { duration: a.speed, queue: false, easing: k, complete: function () { d.css("left", 0); c.css("left", 0); b() } }) }, fadeslide: function (a, b) {
        var c = 0, d = this.getOptions("easing"), i = this.getStageWidth();
        if (a.prev) { c = g.parseValue(e(a.prev).css("left")); e(a.prev).css({ opacity: 1, left: c }).animate({ opacity: 0, left: c + i * (a.rewind ? 1 : -1) }, { duration: a.speed, queue: false, easing: d }) } c = g.parseValue(e(a.next).css("left")); e(a.next).css({ left: c + i * (a.rewind ? -1 : 1), opacity: 0 }).animate({ opacity: 1, left: c }, { duration: a.speed, complete: b, queue: false, easing: d })
    } 
    }, j = function () {
        var a = this; this._theme = void 0; this._options = {}; this._playing = false; this._playtime = 5E3; this._active = null; this._queue = { length: 0 }; this._data = []; this._dom =
{}; this._thumbnails = []; this._initialized = false; this._stageHeight = this._stageWidth = 0; this._target = void 0; this._id = Math.random(); e.each("container stage images image-nav image-nav-left image-nav-right info info-text info-title info-description info-author thumbnails thumbnails-list thumbnails-container thumb-nav-left thumb-nav-right loader counter tooltip".split(" "), function (f, h) { a._dom[h] = g.create("galleria-" + h) }); e.each("current total".split(" "), function (f, h) { a._dom[h] = g.create("galleria-" + h, "span") });
        var b = this._keyboard = { keys: { UP: 38, DOWN: 40, LEFT: 37, RIGHT: 39, RETURN: 13, ESCAPE: 27, BACKSPACE: 8, SPACE: 32 }, map: {}, bound: false, press: function (f) { var h = f.keyCode || f.which; h in b.map && typeof b.map[h] === "function" && b.map[h].call(a, f) }, attach: function (f) { var h, n; for (h in f) if (f.hasOwnProperty(h)) { n = h.toUpperCase(); if (n in b.keys) b.map[b.keys[n]] = f[h] } if (!b.bound) { b.bound = true; I.bind("keydown", b.press) } }, detach: function () { b.bound = false; I.unbind("keydown", b.press) } }, c = this._controls = { 0: void 0, 1: void 0, active: 0,
            swap: function () { c.active = c.active ? 0 : 1 }, getActive: function () { return c[c.active] }, getNext: function () { return c[1 - c.active] } 
        }, d = this._carousel = { next: a.$("thumb-nav-right"), prev: a.$("thumb-nav-left"), width: 0, current: 0, max: 0, hooks: [], update: function () {
            var f = 0, h = 0, n = [0]; e.each(a._thumbnails, function (o, p) { if (p.ready) { f += p.outerWidth || e(p.container).outerWidth(true); n[o + 1] = f; h = Math.max(h, p.outerHeight || e(p.container).outerHeight(true)) } }); a.$("thumbnails").css({ width: f, height: h }); d.max = f; d.hooks = n; d.width =
a.$("thumbnails-list").width(); d.setClasses(); a.$("thumbnails-container").toggleClass("galleria-carousel", f > d.width)
        }, bindControls: function () {
            var f; d.next.bind(y(), function (h) { h.preventDefault(); if (a._options.carouselSteps === "auto") for (f = d.current; f < d.hooks.length; f++) { if (d.hooks[f] - d.hooks[d.current] > d.width) { d.set(f - 2); break } } else d.set(d.current + a._options.carouselSteps) }); d.prev.bind(y(), function (h) {
                h.preventDefault(); if (a._options.carouselSteps === "auto") for (f = d.current; f >= 0; f--) if (d.hooks[d.current] -
d.hooks[f] > d.width) { d.set(f + 2); break } else { if (f === 0) { d.set(0); break } } else d.set(d.current - a._options.carouselSteps)
            })
        }, set: function (f) { for (f = Math.max(f, 0); d.hooks[f - 1] + d.width >= d.max && f >= 0; ) f--; d.current = f; d.animate() }, getLast: function (f) { return (f || d.current) - 1 }, follow: function (f) { if (f === 0 || f === d.hooks.length - 2) d.set(f); else { for (var h = d.current; d.hooks[h] - d.hooks[d.current] < d.width && h <= d.hooks.length; ) h++; if (f - 1 < d.current) d.set(f - 1); else f + 2 > h && d.set(f - h + d.current + 2) } }, setClasses: function () {
            d.prev.toggleClass("disabled",
!d.current); d.next.toggleClass("disabled", d.hooks[d.current] + d.width >= d.max)
        }, animate: function () { d.setClasses(); var f = d.hooks[d.current] * -1; isNaN(f) || a.$("thumbnails").animate({ left: f }, { duration: a._options.carouselSpeed, easing: a._options.easing, queue: false }) } 
        }, i = this._tooltip = { initialized: false, open: false, init: function () {
            i.initialized = true; g.insertStyleTag(".galleria-tooltip{padding:3px 8px;max-width:50%;background:#ffe;color:#000;z-index:3;position:absolute;font-size:11px;line-height:1.3opacity:0;box-shadow:0 0 2px rgba(0,0,0,.4);-moz-box-shadow:0 0 2px rgba(0,0,0,.4);-webkit-box-shadow:0 0 2px rgba(0,0,0,.4);}");
            a.$("tooltip").css("opacity", 0.8); g.hide(a.get("tooltip"))
        }, move: function (f) { var h = a.getMousePosition(f).x; f = a.getMousePosition(f).y; var n = a.$("tooltip"); h = h; var o = f, p = n.outerHeight(true) + 1, q = n.outerWidth(true), r = p + 15; q = a.$("container").width() - q - 2; p = a.$("container").height() - p - 2; if (!isNaN(h) && !isNaN(o)) { h += 10; o -= 30; h = Math.max(0, Math.min(q, h)); o = Math.max(0, Math.min(p, o)); if (f < r) o = r; n.css({ left: h, top: o }) } }, bind: function (f, h) {
            i.initialized || i.init(); var n = function (o, p) {
                i.define(o, p); e(o).hover(function () {
                    g.clearTimer("switch_tooltip");
                    a.$("container").unbind("mousemove", i.move).bind("mousemove", i.move).trigger("mousemove"); i.show(o); j.utils.addTimer("tooltip", function () { a.$("tooltip").stop().show(); g.show(a.get("tooltip"), 400); i.open = true }, i.open ? 0 : 500)
                }, function () { a.$("container").unbind("mousemove", i.move); g.clearTimer("tooltip"); a.$("tooltip").stop(); g.hide(a.get("tooltip"), 200, function () { a.$("tooltip").hide(); g.addTimer("switch_tooltip", function () { i.open = false }, 1E3) }) })
            }; typeof h === "string" ? n(f in a._dom ? a.get(f) : f, h) : e.each(f,
function (o, p) { n(a.get(o), p) })
        }, show: function (f) { f = e(f in a._dom ? a.get(f) : f); var h = f.data("tt"), n = function (o) { s.setTimeout(function (p) { return function () { i.move(p) } } (o), 10); f.unbind("mouseup", n) }; if (h = typeof h === "function" ? h() : h) { a.$("tooltip").html(h.replace(/\s/, "&nbsp;")); f.bind("mouseup", n) } }, define: function (f, h) { if (typeof h !== "function") { var n = h; h = function () { return n } } f = e(f in a._dom ? a.get(f) : f).data("tt", h); i.show(f) } 
        }, k = this._fullscreen = { scrolled: 0, active: false, enter: function (f) {
            k.active = true;
            g.hide(a.getActiveImage()); a.$("container").addClass("fullscreen"); k.scrolled = e(s).scrollTop(); g.forceStyles(a.get("container"), { position: "fixed", top: 0, left: 0, width: "100%", height: "100%", zIndex: 1E4 }); var h = { height: "100%", overflow: "hidden", margin: 0, padding: 0 }; g.forceStyles(z().html, h); g.forceStyles(z().body, h); a.attachKeyboard({ escape: a.exitFullscreen, right: a.next, left: a.prev }); a.rescale(function () {
                g.addTimer("fullscreen_enter", function () { g.show(a.getActiveImage()); typeof f === "function" && f.call(a) },
100); a.trigger(j.FULLSCREEN_ENTER)
            }); e(s).resize(function () { k.scale() })
        }, scale: function () { a.rescale() }, exit: function (f) { k.active = false; g.hide(a.getActiveImage()); a.$("container").removeClass("fullscreen"); g.revertStyles(a.get("container"), z().html, z().body); s.scrollTo(0, k.scrolled); a.detachKeyboard(); a.rescale(function () { g.addTimer("fullscreen_exit", function () { g.show(a.getActiveImage()); typeof f === "function" && f.call(a) }, 50); a.trigger(j.FULLSCREEN_EXIT) }); e(s).unbind("resize", k.scale) } 
        }, l = this._idle =
{ trunk: [], bound: false, add: function (f, h) { if (f) { l.bound || l.addEvent(); f = e(f); var n = {}, o; for (o in h) if (h.hasOwnProperty(o)) n[o] = f.css(o); f.data("idle", { from: n, to: h, complete: true, busy: false }); l.addTimer(); l.trunk.push(f) } }, remove: function (f) { f = jQuery(f); e.each(l.trunk, function (h, n) { if (n.length && !n.not(f).length) { a._idle.show(f); a._idle.trunk.splice(h, 1) } }); if (!l.trunk.length) { l.removeEvent(); g.clearTimer("idle") } }, addEvent: function () { l.bound = true; a.$("container").bind("mousemove click", l.showAll) }, removeEvent: function () {
    l.bound =
false; a.$("container").unbind("mousemove click", l.showAll)
}, addTimer: function () { g.addTimer("idle", function () { a._idle.hide() }, a._options.idleTime) }, hide: function () { a.trigger(j.IDLE_ENTER); e.each(l.trunk, function (f, h) { var n = h.data("idle"); if (n) { h.data("idle").complete = false; h.stop().animate(n.to, { duration: a._options.idleSpeed, queue: false, easing: "swing" }) } }) }, showAll: function () { g.clearTimer("idle"); e.each(a._idle.trunk, function (f, h) { a._idle.show(h) }) }, show: function (f) {
    var h = f.data("idle"); if (!h.busy &&
!h.complete) { h.busy = true; a.trigger(j.IDLE_EXIT); g.clearTimer("idle"); f.stop().animate(h.from, { duration: a._options.idleSpeed / 2, queue: false, easing: "swing", complete: function () { e(this).data("idle").busy = false; e(this).data("idle").complete = true } }) } l.addTimer()
} 
}, m = this._lightbox = { width: 0, height: 0, initialized: false, active: null, image: null, elems: {}, init: function () {
    a.trigger(j.LIGHTBOX_OPEN); if (!m.initialized) {
        m.initialized = true; var f = {}, h = a._options, n = ""; h = { overlay: "position:fixed;display:none;opacity:" +
h.overlayOpacity + ";filter:alpha(opacity=" + h.overlayOpacity * 100 + ");top:0;left:0;width:100%;height:100%;background:" + h.overlayBackground + ";z-index:99990", box: "position:fixed;display:none;width:400px;height:400px;top:50%;left:50%;margin-top:-200px;margin-left:-200px;z-index:99991", shadow: "position:absolute;background:#000;width:100%;height:100%;", content: "position:absolute;background-color:#fff;top:10px;left:10px;right:10px;bottom:10px;overflow:hidden", info: "position:absolute;bottom:10px;left:10px;right:10px;color:#444;font:11px/13px arial,sans-serif;height:13px",
            close: "position:absolute;top:10px;right:10px;height:20px;width:20px;background:#fff;text-align:center;cursor:pointer;color:#444;font:16px/22px arial,sans-serif;z-index:99999", image: "position:absolute;top:10px;left:10px;right:10px;bottom:30px;overflow:hidden;display:block;", prevholder: "position:absolute;width:50%;top:0;bottom:40px;cursor:pointer;", nextholder: "position:absolute;width:50%;top:0;bottom:40px;right:-1px;cursor:pointer;", prev: "position:absolute;top:50%;margin-top:-20px;height:40px;width:30px;background:#fff;left:20px;display:none;line-height:40px;text-align:center;color:#000",
            next: "position:absolute;top:50%;margin-top:-20px;height:40px;width:30px;background:#fff;right:20px;left:auto;display:none;line-height:40px;text-align:center;color:#000", title: "float:left", counter: "float:right;margin-left:8px;"
        }; var o = {}; if (u === 8) { h.nextholder += "background:#000;filter:alpha(opacity=0);"; h.prevholder += "background:#000;filter:alpha(opacity=0);" } e.each(h, function (p, q) { n += ".galleria-lightbox-" + p + "{" + q + "}" }); g.insertStyleTag(n); e.each("overlay box content shadow title info close prevholder prev nextholder next counter image".split(" "),
function (p, q) { a.addElement("lightbox-" + q); f[q] = m.elems[q] = a.get("lightbox-" + q) }); m.image = new j.Picture; e.each({ box: "shadow content close prevholder nextholder", info: "title counter", content: "info image", prevholder: "prev", nextholder: "next" }, function (p, q) { var r = []; e.each(q.split(" "), function (w, v) { r.push("lightbox-" + v) }); o["lightbox-" + p] = r }); a.append(o); e(f.image).append(m.image.container); e(z().body).append(f.overlay, f.box); (function (p) {
    return p.hover(function () { e(this).css("color", "#bbb") }, function () {
        e(this).css("color",
"#444")
    })
})(e(f.close).bind(y(), m.hide).html("&#215;")); e.each(["Prev", "Next"], function (p, q) { var r = e(f[q.toLowerCase()]).html(/v/.test(q) ? "&#8249;&nbsp;" : "&nbsp;&#8250;"), w = e(f[q.toLowerCase() + "holder"]); w.bind(y(), function () { m["show" + q]() }); u < 8 ? r.show() : w.hover(function () { r.show() }, function () { r.stop().fadeOut(200) }) }); e(f.overlay).bind(y(), m.hide)
    } 
}, rescale: function (f) {
    var h = Math.min(e(s).width() - 40, m.width), n = Math.min(e(s).height() - 60, m.height); n = Math.min(h / m.width, n / m.height); h = m.width * n + 40; n =
m.height * n + 60; h = { width: h, height: n, marginTop: Math.ceil(n / 2) * -1, marginLeft: Math.ceil(h / 2) * -1 }; f ? e(m.elems.box).css(h) : e(m.elems.box).animate(h, a._options.lightboxTransitionSpeed, a._options.easing, function () { var o = m.image, p = a._options.lightboxFadeSpeed; a.trigger({ type: j.LIGHTBOX_IMAGE, imageTarget: o.image }); o.show(); g.show(o.image, p); g.show(m.elems.info, p) })
}, hide: function () {
    m.image.image = null; e(s).unbind("resize", m.rescale); e(m.elems.box).hide(); g.hide(m.elems.info); g.hide(m.elems.overlay, 200, function () {
        e(this).hide().css("opacity",
a._options.overlayOpacity); a.trigger(j.LIGHTBOX_CLOSE)
    })
}, showNext: function () { m.show(a.getNext(m.active)) }, showPrev: function () { m.show(a.getPrev(m.active)) }, show: function (f) {
    m.active = f = typeof f === "number" ? f : a.getIndex(); m.initialized || m.init(); e(s).unbind("resize", m.rescale); var h = a.getData(f), n = a.getDataLength(); g.hide(m.elems.info); m.image.load(h.image, function (o) {
        m.width = o.original.width; m.height = o.original.height; e(o.image).css({ width: "100.5%", height: "100.5%", top: 0, zIndex: 99998, opacity: 0 }); m.elems.title.innerHTML =
h.title; m.elems.counter.innerHTML = f + 1 + " / " + n; e(s).resize(m.rescale); m.rescale()
    }); e(m.elems.overlay).show(); e(m.elems.box).show()
} 
}; return this
    }; j.prototype = { constructor: j, init: function (a, b) {
        var c = this; b = L(b); C.push(this); this._original = { target: a, options: b, data: null }; if (this._target = this._dom.target = a.nodeName ? a : e(a).get(0)) {
            this._options = { autoplay: false, carousel: true, carouselFollow: true, carouselSpeed: 400, carouselSteps: "auto", clicknext: false, dataConfig: function () { return {} }, dataSelector: "img", dataSource: this._target,
                debug: void 0, easing: "galleria", extend: function () { }, height: "auto", idleTime: 3E3, idleSpeed: 200, imageCrop: false, imageMargin: 0, imagePan: false, imagePanSmoothness: 12, imagePosition: "50%", keepSource: false, lightboxFadeSpeed: 200, lightboxTransition_speed: 500, linkSourceTmages: true, maxScaleRatio: void 0, minScaleRatio: void 0, overlayOpacity: 0.85, overlayBackground: "#0b0b0b", pauseOnInteraction: true, popupLinks: false, preload: 2, queue: true, show: 0, showInfo: true, showCounter: true, showImagenav: true, thumbCrop: true, thumbEventType: y(),
                thumbFit: true, thumbMargin: 0, thumbQuality: "auto", thumbnails: true, transition: "fade", transitionInitial: void 0, transitionSpeed: 400, width: "auto"
            }; if (b && b.debug === true) E = true; e(this._target).children().hide(); typeof j.theme === "object" ? this._init() : g.wait({ until: function () { return typeof j.theme === "object" }, success: function () { c._init.call(c) }, error: function () { j.raise("No theme found.", true) }, timeout: 5E3 })
        } else j.raise("Target not found.")
    }, _init: function () {
        var a = this; if (this._initialized) {
            j.raise("Init failed: Gallery instance already initialized.");
            return this
        } this._initialized = true; if (!j.theme) { j.raise("Init failed: No theme found."); return this } e.extend(true, this._options, j.theme.defaults, this._original.options); this.bind(j.DATA, function () {
            this._original.data = this._data; this.get("total").innerHTML = this.getDataLength(); var b = this.$("container"), c = { width: 0, height: 0 }, d = g.create("galleria-image"); g.wait({ until: function () {
                e.each(["width", "height"], function (k, l) {
                    c[l] = a._options[l] && typeof a._options[l] === "number" ? a._options[l] : Math.max(g.parseValue(b.css(l)),
g.parseValue(a.$("target").css(l)), b[l](), a.$("target")[l]())
                }); var i = function () { return true }; if (a._options.thumbnails) { a.$("thumbnails").append(d); i = function () { return !!e(d).height() } } return i() && c.width && c.height > 10
            }, success: function () { e(d).remove(); b.width(c.width); b.height(c.height); j.WEBKIT ? s.setTimeout(function () { a._run() }, 1) : a._run() }, error: function () { j.raise("Width & Height not found.", true) }, timeout: 2E3
            })
        }); this.bind(j.READY, function (b) {
            return function () {
                g.show(this.get("counter")); this._options.carousel &&
this._carousel.bindControls(); if (this._options.autoplay) { this.pause(); if (typeof this._options.autoplay === "number") this._playtime = this._options.autoplay; this.trigger(j.PLAY); this._playing = true } if (b) typeof this._options.show === "number" && this.show(this._options.show); else {
                    b = true; if (this._options.clicknext) { e.each(this._data, function (c, d) { delete d.link }); this.$("stage").css({ cursor: "pointer" }).bind(y(), function () { a.next() }) } j.History && j.History.change(function (c) {
                        c = parseInt(c.value.replace(/\//, ""), 10);
                        isNaN(c) ? s.history.go(-1) : a.show(c, void 0, true)
                    }); j.theme.init.call(this, this._options); this._options.extend.call(this, this._options); /^[0-9]{1,4}$/.test(J) && j.History ? this.show(J, void 0, true) : this.show(this._options.show)
                } 
            } 
        } (false)); this.append({ "info-text": ["info-title", "info-description", "info-author"], info: ["info-text"], "image-nav": ["image-nav-right", "image-nav-left"], stage: ["images", "loader", "counter", "image-nav"], "thumbnails-list": ["thumbnails"], "thumbnails-container": ["thumb-nav-left", "thumbnails-list",
"thumb-nav-right"], container: ["stage", "thumbnails-container", "info", "tooltip"]
        }); g.hide(this.$("counter").append(this.get("current"), " / ", this.get("total"))); this.setCounter("&#8211;"); g.hide(a.get("tooltip")); e.each(Array(2), function (b) { var c = new j.Picture; e(c.container).css({ position: "absolute", top: 0, left: 0 }); a.$("images").append(c.container); a._controls[b] = c }); this.$("images").css({ position: "relative", top: 0, left: 0, width: "100%", height: "100%" }); this.$("thumbnails, thumbnails-list").css({ overflow: "hidden",
            position: "relative"
        }); this.$("image-nav-right, image-nav-left").bind(y(), function (b) { a._options.clicknext && b.stopPropagation(); a._options.pause_on_interaction && a.pause(); b = /right/.test(this.className) ? "next" : "prev"; a[b]() }); e.each(["info", "counter", "image-nav"], function (b, c) { a._options["show" + c.substr(0, 1).toUpperCase() + c.substr(1).replace(/-/, "")] === false && g.moveOut(a.get(c.toLowerCase())) }); this.load(); if (!this._options.keep_source && !u) this._target.innerHTML = ""; this.$("target").append(this.get("container"));
        this._options.carousel && this.bind(j.THUMBNAIL, function () { this.updateCarousel() }); return this
    }, _createThumbnails: function () {
        var a, b, c, d, i, k = this, l = this._options, m = function () { var q = k.$("thumbnails").find(".active"); if (!q.length) return false; return q.find("img").attr("src") } (), f = typeof l.thumbnails === "string" ? l.thumbnails.toLowerCase() : null, h = function (q) { return t.defaultView && t.defaultView.getComputedStyle ? t.defaultView.getComputedStyle(c.container, null)[q] : i.css(q) }, n = function (q, r, w) {
            return function () {
                e(w).append(q);
                k.trigger({ type: j.THUMBNAIL, thumbTarget: q, index: r })
            } 
        }, o = function (q) { l.pauseOnInteraction && k.pause(); var r = e(q.currentTarget).data("index"); k.getIndex() !== r && k.show(r); q.preventDefault() }, p = function (q) {
            q.scale({ width: q.data.width, height: q.data.height, crop: l.thumbCrop, margin: l.thumbMargin, complete: function (r) {
                var w = ["left", "top"], v, A; e.each(["Width", "Height"], function (D, H) {
                    v = H.toLowerCase(); if ((l.thumbCrop !== true || l.thumbCrop === v) && l.thumbFit) { A = {}; A[v] = r[v]; e(r.container).css(A); A = {}; A[w[D]] = 0; e(r.image).css(A) } r["outer" +
H] = e(r.container)["outer" + H](true)
                }); g.toggleQuality(r.image, l.thumbQuality === true || l.thumbQuality === "auto" && r.original.width < r.width * 3); k.trigger({ type: j.THUMBNAIL, thumbTarget: r.image, index: r.data.order })
            } 
            })
        }; this._thumbnails = []; this.$("thumbnails").empty(); for (a = 0; this._data[a]; a++) {
            d = this._data[a]; if (l.thumbnails === true) {
                c = new j.Picture(a); b = d.thumb || d.image; this.$("thumbnails").append(c.container); i = e(c.container); c.data = { width: g.parseValue(h("width")), height: g.parseValue(h("height")), order: a };
                l.thumbFit && l.thumbCrop !== true ? i.css({ width: 0, height: 0 }) : i.css({ width: c.data.width, height: c.data.height }); c.load(b, p); l.preload === "all" && c.add(d.image)
            } else if (f === "empty" || f === "numbers") { c = { container: g.create("galleria-image"), image: g.create("img", "span"), ready: true }; f === "numbers" && e(c.image).text(a + 1); this.$("thumbnails").append(c.container); s.setTimeout(n(c.image, a, c.container), 50 + a * 20) } else c = { container: null, image: null }; e(c.container).add(l.keepSource && l.linkSourceImages ? d.original : null).data("index",
a).bind(l.thumbEventType, o); m === b && e(c.container).addClass("active"); this._thumbnails.push(c)
        } 
    }, _run: function () { var a = this; a._createThumbnails(); g.wait({ until: function () { j.OPERA && a.$("stage").css("display", "inline-block"); a._stageWidth = a.$("stage").width(); a._stageHeight = a.$("stage").height(); return a._stageWidth && a._stageHeight > 50 }, success: function () { a.trigger(j.READY) }, error: function () { j.raise("Stage measures not found", true) } }) }, load: function (a, b, c) {
        var d = this; this._data = []; this._thumbnails = [];
        this.$("thumbnails").empty(); if (typeof b === "function") { c = b; b = null } a = a || this._options.dataSource; b = b || this._options.dataSelector; c = c || this._options.dataConfig; if (a.constructor === Array) { if (this.validate(a)) { this._data = a; this._parseData().trigger(j.DATA) } else j.raise("Load failed: JSON Array not valid."); return this } e(a).find(b).each(function (i, k) {
            k = e(k); var l = {}, m = k.parent().attr("href"); if (/\.(png|gif|jpg|jpeg)(\?.*)?$/i.test(m)) l.image = m; else if (m) l.link = m; d._data.push(e.extend({ title: k.attr("title"),
                thumb: k.attr("src"), image: k.attr("src"), description: k.attr("alt"), link: k.attr("longdesc"), original: k.get(0)
            }, l, c(k)))
        }); this.getDataLength() ? this.trigger(j.DATA) : j.raise("Load failed: no data found."); return this
    }, _parseData: function () { var a = this; e.each(this._data, function (b, c) { if ("thumb" in c === false) a._data[b].thumb = c.image }); return this }, splice: function () { Array.prototype.splice.apply(this._data, g.array(arguments)); return this._parseData()._createThumbnails() }, push: function () {
        Array.prototype.push.apply(this._data,
g.array(arguments)); return this._parseData()._createThumbnails()
    }, _getActive: function () { return this._controls.getActive() }, validate: function () { return true }, bind: function (a, b) { a = F(a); this.$("container").bind(a, this.proxy(b)); return this }, unbind: function (a) { a = F(a); this.$("container").unbind(a); return this }, trigger: function (a) { a = typeof a === "object" ? e.extend(a, { scope: this }) : { type: F(a), scope: this }; this.$("container").trigger(a); return this }, addIdleState: function () {
        this._idle.add.apply(this._idle, g.array(arguments));
        return this
    }, removeIdleState: function () { this._idle.remove.apply(this._idle, g.array(arguments)); return this }, enterIdleMode: function () { this._idle.hide(); return this }, exitIdleMode: function () { this._idle.showAll(); return this }, enterFullscreen: function () { this._fullscreen.enter.apply(this, g.array(arguments)); return this }, exitFullscreen: function () { this._fullscreen.exit.apply(this, g.array(arguments)); return this }, toggleFullscreen: function () {
        this._fullscreen[this.isFullscreen() ? "exit" : "enter"].apply(this, g.array(arguments));
        return this
    }, bindTooltip: function () { this._tooltip.bind.apply(this._tooltip, g.array(arguments)); return this }, defineTooltip: function () { this._tooltip.define.apply(this._tooltip, g.array(arguments)); return this }, refreshTooltip: function () { this._tooltip.show.apply(this._tooltip, g.array(arguments)); return this }, openLightbox: function () { this._lightbox.show.apply(this._lightbox, g.array(arguments)); return this }, closeLightbox: function () { this._lightbox.hide.apply(this._lightbox, g.array(arguments)); return this },
        getActiveImage: function () { return this._getActive().image || void 0 }, getActiveThumb: function () { return this._thumbnails[this._active].image || void 0 }, getMousePosition: function (a) { return { x: a.pageX - this.$("container").offset().left, y: a.pageY - this.$("container").offset().top} }, addPan: function (a) {
            if (this._options.imageCrop !== false) {
                a = e(a || this.getActiveImage()); var b = this, c = a.width() / 2, d = a.height() / 2, i = parseInt(a.css("left"), 10), k = parseInt(a.css("top"), 10), l = i || 0, m = k || 0, f = 0, h = 0, n = false, o = g.timestamp(), p = 0,
q = 0, r = function (v, A, D) { if (v > 0) { q = Math.round(Math.max(v * -1, Math.min(0, A))); if (p !== q) { p = q; if (u === 8) a.parent()["scroll" + D](q * -1); else { v = {}; v[D.toLowerCase()] = q; a.css(v) } } } }, w = function (v) { if (!(g.timestamp() - o < 50)) { n = true; c = b.getMousePosition(v).x; d = b.getMousePosition(v).y } }; if (u === 8) { a.parent().scrollTop(m * -1).scrollLeft(l * -1); a.css({ top: 0, left: 0 }) } this.$("stage").unbind("mousemove", w).bind("mousemove", w); g.addTimer("pan", function () {
    if (n) {
        f = a.width() - b._stageWidth; h = a.height() - b._stageHeight; i = c / b._stageWidth *
f * -1; k = d / b._stageHeight * h * -1; l += (i - l) / b._options.imagePanSmoothness; m += (k - m) / b._options.imagePanSmoothness; r(h, m, "Top"); r(f, l, "Left")
    } 
}, 50, true); return this
            } 
        }, proxy: function (a, b) { if (typeof a !== "function") return function () { }; b = b || this; return function () { return a.apply(b, g.array(arguments)) } }, removePan: function () { this.$("stage").unbind("mousemove"); g.clearTimer("pan"); return this }, addElement: function () { var a = this._dom; e.each(g.array(arguments), function (b, c) { a[c] = g.create("galleria-" + c) }); return this },
        attachKeyboard: function () { this._keyboard.attach.apply(this._keyboard, g.array(arguments)); return this }, detachKeyboard: function () { this._keyboard.detach.apply(this._keyboard, g.array(arguments)); return this }, appendChild: function (a, b) { this.$(a).append(this.get(b) || b); return this }, prependChild: function (a, b) { this.$(a).prepend(this.get(b) || b); return this }, remove: function () { this.$(g.array(arguments).join(",")).remove(); return this }, append: function (a) {
            var b, c; for (b in a) if (a.hasOwnProperty(b)) if (a[b].constructor ===
Array) for (c = 0; a[b][c]; c++) this.appendChild(b, a[b][c]); else this.appendChild(b, a[b]); return this
        }, _scaleImage: function (a, b) { b = e.extend({ width: this._stageWidth, height: this._stageHeight, crop: this._options.imageCrop, max: this._options.maxScaleRatio, min: this._options.minScaleRatio, margin: this._options.imageMargin, position: this._options.imagePosition }, b); (a || this._controls.getActive()).scale(b); return this }, updateCarousel: function () { this._carousel.update(); return this }, rescale: function (a, b, c) {
            var d = this;
            if (typeof a === "function") { c = a; a = void 0 } var i = function () { d._stageWidth = a || d.$("stage").width(); d._stageHeight = b || d.$("stage").height(); d._scaleImage(); d._options.carousel && d.updateCarousel(); d.trigger(j.RESCALE); typeof c === "function" && c.call(d) }; j.WEBKIT && !a && !b ? g.addTimer("scale", i, 5) : i.call(d); return this
        }, refreshImage: function () { this._scaleImage(); this._options.imagePan && this.addPan(); return this }, show: function (a, b, c) {
            if (!(a === false || !this._options.queue && this._queue.stalled)) {
                a = Math.max(0, Math.min(parseInt(a,
10), this.getDataLength() - 1)); b = typeof b !== "undefined" ? !!b : a < this.getIndex(); c = c || false; if (!c && j.History) j.History.value(a.toString()); else { this._active = a; Array.prototype.push.call(this._queue, { index: a, rewind: b }); this._queue.stalled || this._show(); return this } 
            } 
        }, _show: function () {
            var a = this, b = this._queue[0], c = this.getData(b.index); if (c) {
                var d = c.image, i = this._controls.getActive(), k = this._controls.getNext(), l = k.isCached(d), m = this._thumbnails[b.index], f = function () {
                    a._queue.stalled = false; g.toggleQuality(k.image,
a._options.imageQuality); e(i.container).css({ zIndex: 0, opacity: 0 }); e(k.container).css({ zIndex: 1, opacity: 1 }); a._controls.swap(); a._options.imagePan && a.addPan(k.image); c.link && e(k.image).css({ cursor: "pointer" }).bind(y(), function () { if (a._options.popupLinks) s.open(c.link, "_blank"); else s.location.href = c.link }); Array.prototype.shift.call(a._queue); a._queue.length && a._show(); a._playCheck(); a.trigger({ type: j.IMAGE, index: b.index, imageTarget: k.image, thumbTarget: m.image })
                }; this._options.carousel && this._options.carouselFollow &&
this._carousel.follow(b.index); if (this._options.preload) { var h, n, o = this.getNext(); try { for (n = this._options.preload; n > 0; n--) { h = new j.Picture; h.add(a.getData(o).image); o = a.getNext(o) } } catch (p) { } } g.show(k.container); e(a._thumbnails[b.index].container).addClass("active").siblings(".active").removeClass("active"); a.trigger({ type: j.LOADSTART, cached: l, index: b.index, imageTarget: k.image, thumbTarget: m.image }); k.load(d, function (q) {
    a._scaleImage(q, { complete: function (r) {
        g.show(r.container); "image" in i && g.toggleQuality(i.image,
false); g.toggleQuality(r.image, false); a._queue.stalled = true; a.removePan(); a.setInfo(b.index); a.setCounter(b.index); a.trigger({ type: j.LOADFINISH, cached: l, index: b.index, imageTarget: r.image, thumbTarget: a._thumbnails[b.index].image }); var w = i.image === null && a._options.transitionInitial ? a._options.transition_Initial : a._options.transition; w in G === false ? f() : G[w].call(a, { prev: i.image, next: r.image, rewind: b.rewind, speed: a._options.transitionSpeed || 400 }, f)
    } 
    })
})
            } 
        }, getNext: function (a) {
            a = typeof a === "number" ? a : this.getIndex();
            return a === this.getDataLength() - 1 ? 0 : a + 1
        }, getPrev: function (a) { a = typeof a === "number" ? a : this.getIndex(); return a === 0 ? this.getDataLength() - 1 : a - 1 }, next: function () { this.getDataLength() > 1 && this.show(this.getNext(), false); return this }, prev: function () { this.getDataLength() > 1 && this.show(this.getPrev(), true); return this }, get: function (a) { return a in this._dom ? this._dom[a] : null }, getData: function (a) { return a in this._data ? this._data[a] : this._data[this._active] }, getDataLength: function () { return this._data.length },
        getIndex: function () { return typeof this._active === "number" ? this._active : false }, getStageHeight: function () { return this._stageHeight }, getStageWidth: function () { return this._stageWidth }, getOptions: function (a) { return typeof a === "undefined" ? this._options : this._options[a] }, setOptions: function (a, b) { if (typeof a === "object") e.extend(this._options, a); else this._options[a] = b; return this }, play: function (a) { this._playing = true; this._playtime = a || this._playtime; this._playCheck(); this.trigger(j.PLAY); return this }, pause: function () {
            this._playing =
false; this.trigger(j.PAUSE); return this
        }, playToggle: function (a) { return this._playing ? this.pause() : this.play(a) }, isPlaying: function () { return this._playing }, isFullscreen: function () { return this._fullscreen.active }, _playCheck: function () {
            var a = this, b = 0, c = g.timestamp(), d = "play" + this._id; if (this._playing) {
                g.clearTimer(d); var i = function () {
                    b = g.timestamp() - c; if (b >= a._playtime && a._playing) { g.clearTimer(d); a.next() } else if (a._playing) {
                        a.trigger({ type: j.PROGRESS, percent: Math.ceil(b / a._playtime * 100), seconds: Math.floor(b /
1E3), milliseconds: b
                        }); g.addTimer(d, i, 20)
                    } 
                }; g.addTimer(d, i, 20)
            } 
        }, setIndex: function (a) { this._active = a; return this }, setCounter: function (a) { if (typeof a === "number") a++; else if (typeof a === "undefined") a = this.getIndex() + 1; this.get("current").innerHTML = a; if (u) { a = this.$("counter"); var b = a.css("opacity"), c = a.attr("style"); c && parseInt(b, 10) === 1 ? a.attr("style", c.replace(/filter[^\;]+\;/i, "")) : this.$("counter").css("opacity", b) } return this }, setInfo: function (a) {
            var b = this, c = this.getData(a); e.each(["title", "description",
"author"], function (d, i) { var k = b.$("info-" + i); c[i] ? k[c[i].length ? "show" : "hide"]().html(c[i]) : k.empty().hide() }); return this
        }, hasInfo: function (a) { var b = "title description".split(" "), c; for (c = 0; b[c]; c++) if (this.getData(a)[b[c]]) return true; return false }, jQuery: function (a) { var b = this, c = []; e.each(a.split(","), function (i, k) { k = e.trim(k); b.get(k) && c.push(k) }); var d = e(b.get(c.shift())); e.each(c, function (i, k) { d = d.add(b.get(k)) }); return d }, $: function () { return this.jQuery.apply(this, g.array(arguments)) } 
    }; e.each(K,
function (a, b) { var c = /_/.test(b) ? b.replace(/_/g, "") : b; j[b.toUpperCase()] = "galleria." + c }); e.extend(j, { IE9: u === 9, IE8: u === 8, IE7: u === 7, IE6: u === 6, IE: !!u, WEBKIT: /webkit/.test(x), SAFARI: /safari/.test(x), CHROME: /chrome/.test(x), QUIRK: u && t.compatMode && t.compatMode === "BackCompat", MAC: /mac/.test(navigator.platform.toLowerCase()), OPERA: !!s.opera, IPHONE: /iphone/.test(x), IPAD: /ipad/.test(x), ANDROID: /android/.test(x), TOUCH: !!(/iphone/.test(x) || /ipad/.test(x) || /android/.test(x)) }); j.addTheme = function (a) {
    a.name || j.raise("No theme name specified");
    a.defaults = typeof a.defaults !== "object" ? {} : L(a.defaults); var b = false, c; if (typeof a.css === "string") { e("link").each(function (d, i) { c = RegExp(a.css); if (c.test(i.href)) { b = true; j.theme = a; return false } }); b || e("script").each(function (d, i) { c = RegExp("galleria\\." + a.name.toLowerCase() + "\\."); if (c.test(i.src)) { b = i.src.replace(/[^\/]*$/, "") + a.css; g.addTimer("css", function () { g.loadCSS(b, "galleria-theme", function () { j.theme = a }) }, 1) } }); b || j.raise("No theme CSS loaded") } else j.theme = a; return a
}; j.loadTheme = function (a,
b) { var c = false, d = C.length; j.theme = void 0; g.loadScript(a, function () { c = true }); g.wait({ until: function () { return c }, error: function () { j.raise("Theme at " + a + " could not load, check theme path.", true) }, success: function () { if (d) { var i = []; e.each(j.get(), function (k, l) { var m = e.extend(l._original.options, { data_source: l._data }, b); l.$("container").remove(); var f = new j; f._id = l._id; f.init(l._original.target, m); i.push(f) }); C = i } }, timeout: 2E3 }) }; j.get = function (a) {
    if (C[a]) return C[a]; else if (typeof a !== "number") return C;
    else j.raise("Gallery index " + a + " not found")
}; j.addTransition = function (a, b) { G[a] = b }; j.utils = g; j.log = function () { try { s.console.log.apply(s.console, g.array(arguments)) } catch (a) { try { s.opera.postError.apply(s.opera, arguments) } catch (b) { s.alert(g.array(arguments).split(", ")) } } }; j.raise = function (a, b) { if (E || b) throw Error((b ? "Fatal error" : "Error") + ": " + a); }; j.Picture = function (a) {
    this.id = a || null; this.image = null; this.container = g.create("galleria-image"); e(this.container).css({ overflow: "hidden", position: "relative" });
    this.original = { width: 0, height: 0 }; this.loaded = this.ready = false
}; j.Picture.prototype = { cache: {}, add: function (a) { var b = 0, c = this, d = new Image, i = function () { if ((!this.width || !this.height) && b < 1E3) { b++; e(d).load(i).attr("src", a + "?" + (new Date).getTime()) } c.original = { height: this.height, width: this.width }; c.cache[a] = a; c.loaded = true }; e(d).css("display", "block"); if (c.cache[a]) { d.src = a; i.call(d); return d } e(d).load(i).attr("src", a); return d }, show: function () { g.show(this.image) }, hide: function () { g.moveOut(this.image) },
    clear: function () { this.image = null }, isCached: function (a) { return !!this.cache[a] }, load: function (a, b) { var c = this; e(this.container).empty(true); this.image = this.add(a); g.hide(this.image); e(this.container).append(this.image); g.wait({ until: function () { return c.loaded && c.image.complete && c.original.width && c.image.width }, success: function () { s.setTimeout(function () { b.call(c, c) }, 50) }, error: function () { s.setTimeout(function () { b.call(c, c) }, 50); j.raise("image not loaded in 10 seconds: " + a) }, timeout: 1E4 }); return this.container },
    scale: function (a) {
        a = e.extend({ width: 0, height: 0, min: void 0, max: void 0, margin: 0, complete: function () { }, position: "center", crop: false }, a); if (!this.image) return this.container; var b, c, d = this, i = e(d.container); g.wait({ until: function () { b = a.width || i.width() || g.parseValue(i.css("width")); c = a.height || i.height() || g.parseValue(i.css("height")); return b && c }, success: function () {
            var k = (b - a.margin * 2) / d.original.width, l = (c - a.margin * 2) / d.original.height, m = { "true": Math.max(k, l), width: k, height: l, "false": Math.min(k, l)}[a.crop.toString()];
            if (a.max) m = Math.min(a.max, m); if (a.min) m = Math.max(a.min, m); e(d.container).width(b).height(c); e.each(["width", "height"], function (o, p) { e(d.image)[p](d.image[p] = d[p] = Math.round(d.original[p] * m)) }); var f = {}, h = {}; k = function (o, p, q) { var r = 0; if (/\%/.test(o)) { o = parseInt(o, 10) / 100; p = d.image[p] || e(d.image)[p](); r = Math.ceil(p * -1 * o + q * o) } else r = g.parseValue(o); return r }; var n = { top: { top: 0 }, left: { left: 0 }, right: { left: "100%" }, bottom: { top: "100%"} }; e.each(a.position.toLowerCase().split(" "), function (o, p) {
                if (p === "center") p =
"50%"; f[o ? "top" : "left"] = p
            }); e.each(f, function (o, p) { n.hasOwnProperty(p) && e.extend(h, n[p]) }); f = f.top ? e.extend(f, h) : h; f = e.extend({ top: "50%", left: "50%" }, f); e(d.image).css({ position: "relative", top: k(f.top, "height", c), left: k(f.left, "width", b) }); d.show(); d.ready = true; a.complete.call(d, d)
        }, error: function () { j.raise("Could not scale image: " + d.image.src) }, timeout: 1E3
        }); return this
    } 
}; e.extend(e.easing, { galleria: function (a, b, c, d, i) { if ((b /= i / 2) < 1) return d / 2 * b * b * b * b + c; return -d / 2 * ((b -= 2) * b * b * b - 2) + c }, galleriaIn: function (a,
b, c, d, i) { return d * (b /= i) * b * b * b + c }, galleriaOut: function (a, b, c, d, i) { return -d * ((b = b / i - 1) * b * b * b - 1) + c } 
}); e.fn.galleria = function (a) { return this.each(function () { (new j).init(this, a) }) }; s.Galleria = j
})(jQuery);