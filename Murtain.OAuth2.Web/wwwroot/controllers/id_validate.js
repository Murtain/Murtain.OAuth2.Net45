'use strict';

define(['app', 'app.constants', 'services/account_service', 'services/captcha_service'], function (app) {

    app.controller('LocalRegistrationController', LocalRegistrationController)

    LocalRegistrationController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'captcha_service', 'constants', '$state'];

    function LocalRegistrationController($scope, $rootScope, $timeout, account_service, captcha_service, constants, $state, $stateParams) {

        console.log("LocalRegistrationController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {
                captcha_type: $state.params.type
            },
            error: {},

            next: next,
            refreshCaptcha: refreshCaptcha,
        };


        that = angular.extend(this, that);

        active();

        /**
         * 初始化
         */
        function active() {

            $timeout(function () {
                that.identity = JSON.parse(document.getElementById("IdentityModelString").value);

                that.endpoint.login = that.identity.LoginUrl;
                that.endpoint.captcha = "/Login/GetGraphicCaptchaAsync?type=" + $state.params.type;
                that.endpoint.agreement = constants.support.agreement;
                that.endpoint.privacy = constants.support.privacy;

            });
        }

        /**
         *  刷新图形验证码
         */
        function refreshCaptcha() {
            that.endpoint.captcha = "/Login/GetGraphicCaptchaAsync?type=" + $state.params.type + "&key=" + _getKey();
        }

        /**
         * 下一步
         */
        function next() {

            if (!that.payload.mobile) {
                that.error.message = "请输入手机号码"

                return;
            }

            if (!(/^1[34578]\d{9}$/.test(that.payload.mobile))) {
                that.error.message = "请输入正确的手机号"
                return;
            }

            if (!that.payload.graphic_captcha) {
                that.error.message = "请输入图形验证码"

                return;
            }

            // 验证图形验证码并发送短信验证码
            captcha_service.sendCaptcha(that.payload)
                .then(function (data) {

                    if (!data) {
                        $state.go("captcha-validate", { mobile: that.payload.mobile, type: $state.params.type })
                        return;
                    }

                    that.error = data;
                });
        }


        /**
         * 生成时间戳
         */
        function _getKey() {
            return (new Date).valueOf();
        }

    }
    return LocalRegistrationController;
});