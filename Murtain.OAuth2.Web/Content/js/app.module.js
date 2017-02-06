"use strict";

define(['angular', 'angular-ui-router', 'angular-loading-bar', 'angular-animate', 'angular-toastr'], function (angular) {

    var app = angular.module('app', ['ui.router', 'angular-loading-bar']);

    app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    }]);

    app.config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({ enable: true, requireBase: false });
    }]);

    app.config(['$provide', function ($provide) {
        $provide.decorator('$rootScope', ['$delegate', function ($delegate) {
            Object.defineProperty($delegate.constructor.prototype, '$onRootScope', {
                value: function (name, listener) {
                    var unsubscribe = $delegate.$on(name, listener);
                    this.$on('$destroy', unsubscribe);
                    return unsubscribe;
                }, enumerable: false
            });

            return $delegate;
        }]);
    }]);

    return app;

});