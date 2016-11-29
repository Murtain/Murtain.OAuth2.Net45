"use strict";

define(['angular', 'angular-AMD', 'angular-AMD-ngload', 'angular-ui-router', 'angular-loading-bar', 'angular-animate', 'angular-toastr'], function (angular, angularAMD) {

    var app = angular.module('app', ['ui.router', 'angular-loading-bar']);

    app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    }]);

    app.config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode({ enable: true, requireBase: false });
    }]);

    return angularAMD.bootstrap(app);

});