'use strict';

define(['app'], function () {

    angular.module('app')
            .config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

                $urlRouterProvider.otherwise("/");

                $stateProvider.state('LocalRegistration', angularAMD.route({
                    url: '/LocalRegistration',
                    controllerUrl: '/content/js/controllers/LocalRegistrationController.js',
                    templateUrl: '/Account/LocalRegistration'
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