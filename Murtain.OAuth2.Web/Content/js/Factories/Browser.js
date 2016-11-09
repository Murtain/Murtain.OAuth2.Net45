'use strict';

defined(['app.module'], function () {

    angular.module('app').factory('Browser', ['$location', '$q', '$rootScope', '$timeout', 'logger', function ($location, $q, $rootScope, $timeout, logger) {
        var factory = {
            $broadcast: $broadcast,
            $q: $q,
            $timeout: $timeout,
            logger: logger,
            textContains: textContains,
            getLocationUrlQueryString: getLocationUrlQueryString(),
        }

        return factory;
    }]);

    function $broadcast() {
        return $rootScope.$broadcast.apply($rootScope, arguments);
    }
    function getLocationUrlQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);
        return null;
    }
    function textContains(text, searchText) {
        return text && -1 !== text.toLowerCase().indexOf(searchText.toLowerCase());
    }
});