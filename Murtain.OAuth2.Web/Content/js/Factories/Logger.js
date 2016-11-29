﻿'use strict';
defined(['app.module'], function () {

    angular.module('app').factory('logger', ['$log', 'toastr', function ($log, toastr) {
        var factory = {
            toastr: true,

            error: error,
            info: info,
            success: success,
            warning: warning,
            // straight to console; by pass toastr
            log: $log.log
        }

        return factory;
    }]);

    function error(message, data, title) {
        toastr.error(message, title);
        $log.error('Error: ' + message, data);
    }

    function info(message, data, title) {
        toastr.info(message, title);
        $log.info('Info: ' + message, data);
    }

    function success(message, data, title) {
        toastr.success(message, title);
        $log.info('Success: ' + message, data);
    }

    function warning(message, data, title) {
        toastr.warning(message, title);
        $log.warn('Warning: ' + message, data);
    }
});