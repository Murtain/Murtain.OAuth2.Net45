'use strict';

define(['app', 'app.constants', 'services/account_service', 'services/captcha_service'], function (app) {

    app.controller('ValidateCaptchaController', ValidateCaptchaController)

    ValidateCaptchaController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'captcha_service', 'constants', '$stateParams', '$interval', '$state'];

    function ValidateCaptchaController($scope, $rootScope, $timeout, account_service, captcha_service, constants, $stateParams, $interval, $state) {

        console.log("ValidateCaptchaController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {
                captcha_type: $state.params.type
            },
            error: {},
            timer: {
                second: 59,
                timePromise: undefined,
                text: '重发验证码',
                paraevent: true
            },

            next: next,
            resendCaptcha: resendCaptcha,
        };


        that = angular.extend(this, that);

        active();

        /**
         *  初始化
         */
        function active() {

            $timeout(function () {

                that.payload.mobile = $stateParams.mobile;

                that.identity = JSON.parse(document.getElementById('IdentityModelString').value);

                that.endpoint.login = that.identity.LoginUrl;
                that.endpoint.agreement = constants.support.agreement;
                that.endpoint.privacy = constants.support.privacy;

                _timerStart();

            });
        }

        /**
         *  重新发送短信验证码
         */
        function resendCaptcha() {
            if (that.timer.paraevent) {
                return;
            }

            fnTimerStart();
        }

        /**
         *  下一步
         */
        function next() {

            if (!that.payload.captcha) {
                that.error.message = "请输入短信验证码"

                return;
            }

            // 验证短信验证码
            captcha_service.validate(that.payload)
                .then(function (data) {
                    if (!data) {
                        $state.go('password-set', { mobile: that.payload.mobile, type: $state.params.type })
                        return;
                    }
                    that.error = data;
                });

        }

        /**
         *  开始倒计时
         */
        function _timerStart() {

            _timerReset();

            that.timer.timePromise = $interval(function () {
                if (that.timer.second <= 0) {

                    _timerReset();

                } else {
                    that.timer.paraevent = true;
                    that.timer.text = "重发验证码（" + that.timer.second + "）";
                    that.timer.second--;
                }
            }, 1000, 100);
        }

        /**
         *  重置倒计时
         */
        function _timerReset() {

            $interval.cancel(that.timer.timePromise);
            that.timer.timePromise = undefined;

            that.timer.second = 59;
            that.timer.text = "重发验证码";
            that.timer.paraevent = false;
        }

    }

    return ValidateCaptchaController;
});