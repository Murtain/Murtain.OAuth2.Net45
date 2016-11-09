"use strict";

define(['angular', 'angular-ui-router', 'angular-loading-bar', 'angular-animate', 'angular-toastr', 'angular-AMD', 'angular-AMD-ngload'], function (angular, angularAMD) {

    var app = angular.module('app', ['ui.router', 'angular-loading-bar']);
    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
        cfpLoadingBarProvider.includeSpinner = true;
    }]);

    return angularAMD.bootstrap(app);

});