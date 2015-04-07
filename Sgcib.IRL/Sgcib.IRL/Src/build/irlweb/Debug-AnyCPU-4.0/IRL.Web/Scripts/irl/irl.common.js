// irl Library 0.1

(function () {

    //Declare variables
    var 
	window = this,
	undefined,
	irl = window.irl = {};

    //Global Helpers

    irl.fn = irl.prototype = {
        irl: '0.1'
    };

    // Array Remove - By John Resig (MIT Licensed)
    Array.prototype.remove = function (from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };

    if (!Array.prototype.indexOf) {
        Array.prototype.indexOf = function (elt /*, from*/) {
            var len = this.length >>> 0;

            var from = Number(arguments[1]) || 0;
            from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
            if (from < 0)
                from += len;

            for (; from < len; from++) {
                if (from in this &&
          this[from] === elt)
                    return from;
            }
            return -1;
        };
    }

    //Core
    irl.core = {};
    irl.core.namespace = function () {
        var a = arguments, o = null, i, j, d;
        for (i = 0; i < a.length; i = i + 1) {
            d = a[i].split(".");
            o = irl;

            // irl is implied, so it is ignored if it is included
            for (j = (d[0] == "irl") ? 1 : 0; j < d.length; j = j + 1) {
                o[d[j]] = o[d[j]] || {};
                o = o[d[j]];
            }
        }

        return o;
    };

    //System
    irl.core.namespace('system');
    
    //UI
    irl.core.namespace('ui');

})();