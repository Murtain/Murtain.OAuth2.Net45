'use strict';

define(['angular-AMD', 'app.module'], function ( angularAMD,app) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise("/Login");

        $stateProvider.state('Login', angularAMD.route({
            url: '/Login',
            controllerUrl: '/content/js/controllers/LoginController.js',
            templateUrl: '/Account/Index',
            controllerAs: 'LoginViewModel',
        }))
        $stateProvider.state('LocalRegistration', angularAMD.route({
            url: '/LocalRegistration',
            controllerUrl: '/content/js/controllers/LocalRegistrationController.js',
            templateUrl: '/Account/LocalRegistration',
            controllerAs: 'LocalRegistrationViewModel',
        }))
        .state('ValidateCaptcha', angularAMD.route({
            url: '/ValidateCaptcha',
            controllerUrl: '/content/js/controllers/ValidateCaptchaController.js',
            templateUrl: '/Account/ValidateCaptcha'
        }))
        .state('FogotPassword', angularAMD.route({
            url: '/FogotPassword',
            controllerUrl: '/content/js/controllers/FogotPasswordController.js',
            templateUrl: '/Account/FogotPassword'
        }))
        .state('SetPassword', angularAMD.route({
            url: '/FogotPassword',
            controllerUrl: '/content/js/controllers/SetPasswordController.js',
            templateUrl: '/Account/SetPassword'
        }))
        .state('ResetPassword', angularAMD.route({
            url: '/FogotPassword',
            controllerUrl: '/content/js/controllers/ResetPasswordController.js',
            templateUrl: '/Account/ResetPassword'
        }));
    }]);
})