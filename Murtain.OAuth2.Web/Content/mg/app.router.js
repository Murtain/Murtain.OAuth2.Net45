'use strict';

define(['angular-AMD', 'app.module'], function (angularAMD, app) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise("/profile");
        $stateProvider
            .state('profile', angularAMD.route({
                url: '/profile',
                controllerUrl: '/content/mg/controllers/profile_controller.js',
                templateUrl: '/passport/profile',
                controllerAs: 'model',
            }))
            .state('security', angularAMD.route({
                url: '/security',
                controllerUrl: '/content/mg/controllers/security_controller.js',
                templateUrl: '/passport/security',
                controllerAs: 'model',
            }))
            .state('permissions', angularAMD.route({
                url: '/permissions',
                controllerUrl: '/content/mg/controllers/permissions_controller.js',
                templateUrl: '/connect/permissions',
                controllerAs: 'model',
            }))
            ;
    }]);
})