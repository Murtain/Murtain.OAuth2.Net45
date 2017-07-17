'use strict';

define(['angular-AMD', 'app.module'], function (angularAMD, app) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise(function ($injector, $location) {
            if (window.location.href.indexOf('/login') > 0) {
                $location.path('/login-web');
            }
            if (window.location.href.indexOf('/permissions') > 0) {
                $location.path('/permissions');
            }
        });


        $stateProvider
            .state('login', angularAMD.route({
                url: '/login-web',
                controllerUrl: '/wwwroot/controllers/login_web.js',
                templateUrl: '/login/index',
                controllerAs: 'model',
            }))
            .state('id-validate', angularAMD.route({
                url: '/id-validate/:type',
                controllerUrl: '/wwwroot/controllers/id_validate.js',
                templateUrl: '/login/validateid',
                controllerAs: 'model',
            }))
            .state('captcha-validate', angularAMD.route({
                url: '/captcha-validate/:mobile/:type',
                controllerUrl: '/wwwroot/controllers/captcha_validate.js',
                templateUrl: '/login/validatecaptcha',
                controllerAs: 'model',
            }))
            .state('password-set', angularAMD.route({
                url: '/password-set/:mobile/:type',
                controllerUrl: '/wwwroot/controllers/password_set.js',
                templateUrl: '/login/setpassword',
                controllerAs: 'model',
            }))

            .state('profile', angularAMD.route({
                url: '/profile',
                controllerUrl: '/wwwroot/controllers/profile.js',
                templateUrl: '/manage/profile',
                controllerAs: 'model',
            }))
            .state('security', angularAMD.route({
                url: '/security',
                controllerUrl: '/wwwroot/controllers/security.js',
                templateUrl: '/manage/security',
                controllerAs: 'model',
            }))
            .state('permissions', angularAMD.route({
                url: '/permissions',
                controllerUrl: '/wwwroot/controllers/permissions.js',
                templateUrl: '/manage/permissions',
                controllerAs: 'model',
            }))
            ;
    }]);
})