'use strict';

define(['angular-AMD', 'app.module'], function (angularAMD, app) {

    app.run(function ($rootScope, $state, $stateParams) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
    });

    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider", "$uiViewScrollProvider", function ($stateProvider, $urlRouterProvider, $locationProvider, $uiViewScrollProvider) {

        $uiViewScrollProvider.useAnchorScroll();

        $urlRouterProvider.otherwise("/login-web");


        $stateProvider
            //登录
            .state('login', angularAMD.route({
                url: '/login-web',
                controllerUrl: '/content/app/controllers/login_web_controller.js',
                templateUrl: '/www/templates/login-web.html',
                controllerAs: 'model',
            }))
            //本地注册
            .state('registration-local', angularAMD.route({
                url: '/registration-local',
                controllerUrl: '/content/app/controllers/registration_local_controller.js',
                templateUrl: '/login/localregistration',
                controllerAs: 'model',
            }))
            //验证码
            .state('captcha-validate', angularAMD.route({
                url: '/captcha-validate/:mobile',
                controllerUrl: '/content/app/controllers/captcha_validate_controller.js',
                templateUrl: '/login/validatecaptcha',
                controllerAs: 'model',
            }))
            //忘记密码
            .state('password-fogot', angularAMD.route({
                url: '/password-fogot',
                controllerUrl: '/content/app/controllers/password_fogot_controller.js',
                templateUrl: '/login/fogotpassword',
                controllerAs: 'model',
            }))
            //设置密码
            .state('password-set', angularAMD.route({
                url: '/password-set/:mobile',
                controllerUrl: '/content/app/controllers/password_set_controller.js',
                templateUrl: '/login/setpassword',
                controllerAs: 'model',
            }))
            //重置密码
            .state('password-reset', angularAMD.route({
                url: '/password-reset',
                controllerUrl: '/content/app/controllers/password_reset_controller.js',
                templateUrl: '/login/resetpassword',
                controllerAs: 'model',
            }));
    }]);
})