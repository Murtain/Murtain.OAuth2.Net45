'use strict';

define(['app.module'], function (app) {

    app.factory('browser', function ($location) {
        return {
            getLocationUrlQueryString: getLocationUrlQueryString(),
        }
        function getLocationUrlQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]);
            return null;
        }
    });

});