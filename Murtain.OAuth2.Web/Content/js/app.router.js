'use strict';

define(['angular-AMD', 'app.module'], function ( angularAMD,app) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise("/login-web");

        $stateProvider.state('login-web', angularAMD.route({
            url: '/login-web',
            controllerUrl: '/content/js/controllers/login-web.js',
            templateUrl: '/account/index',
            controllerAs: 'model',
        }))
        $stateProvider.state('registration-local', angularAMD.route({
            url: '/registration-local',
            controllerUrl: '/content/js/controllers/registration-local.js',
            templateUrl: '/account/localregistration',
            controllerAs: 'model',
        }))
        .state('captcha-validate', angularAMD.route({
            url: '/captcha-validate/:mobile',
            controllerUrl: '/content/js/controllers/captcha-validate.js',
            templateUrl: '/account/validatecaptcha',
            controllerAs: 'model',
        }))
        .state('password-fogot', angularAMD.route({
            url: '/password-fogot',
            controllerUrl: '/content/js/controllers/password-fogot.js',
            templateUrl: '/account/fogotpassword',
            controllerAs: 'model',
        }))
        .state('password-set', angularAMD.route({
            url: '/password-set/:mobile',
            controllerUrl: '/content/js/controllers/password-set.js',
            templateUrl: '/account/setpassword',
            controllerAs: 'model',
        }))
        .state('password-reset', angularAMD.route({
            url: '/password-reset',
            controllerUrl: '/content/js/controllers/password-reset.js',
            templateUrl: '/account/resetpassword',
            controllerAs: 'model',
        }));
    }]);
})