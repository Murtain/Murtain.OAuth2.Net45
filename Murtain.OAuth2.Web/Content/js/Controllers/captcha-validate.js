'use strict';

define(['app', 'app.constants', 'services/account-service', 'services/captcha-service'], function (app) {

    app.controller('ValidateCaptchaController', fnValidateCaptchaController)

    fnValidateCaptchaController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'captcha_service', 'constants', '$stateParams', '$interval', '$state'];

    function fnValidateCaptchaController($scope, $rootScope, $timeout, account_service, captcha_service, constants, $stateParams, $interval, $state) {

        console.log("ValidateCaptchaController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {},
            error: {},
            timer: {
                second: 59,
                timePromise: undefined,
                text: '重发验证码',
                paraevent: true
            },
            fnNext: fnNext,
            fnResendCaptcha: fnResendCaptcha,
        };


        that = angular.extend(this, that);

        fnActived();

        function fnActived() {

            $timeout(function () {

                that.payload.mobile = $stateParams.mobile;

                that.identity = JSON.parse(document.getElementById('IdentityModelString').value);

                that.endpoint.login = that.identity.LoginUrl;
                that.endpoint.agreement = constants.support.agreement;
                that.endpoint.privacy = constants.support.privacy;

                fnTimerStart();

            });
        }

        function fnResendCaptcha() {
            if (that.timer.paraevent) {
                return;
            }

            fnTimerStart();
        }

        function fnNext() {

            if (!fnValidatePayload()) {
                return;
            }

            captcha_service.fnValidateMessageCaptcha(that.payload)
                           .then(function (data) {
                               if (data) {
                                   $state.go('password-set', { mobile: that.payload.mobile })
                                   return;
                               }
                               that.error = data;
                           });

        }

        function fnTimerStart() {

            fnTimerReset();

            that.timer.timePromise = $interval(function () {
                if (that.timer.second <= 0) {

                    fnTimerReset();

                } else {
                    that.timer.paraevent = true;
                    that.timer.text = "重发验证码（" + that.timer.second + "）";
                    that.timer.second--;
                }
            }, 1000, 100);
        }

        function fnTimerReset() {

            $interval.cancel(that.timer.timePromise);
            that.timer.timePromise = undefined;

            that.timer.second = 59;
            that.timer.text = "重发验证码";
            that.timer.paraevent = false;
        }

        function fnValidatePayload() {

            if (!that.payload.captcha) {
                that.error.message = "请输入短信验证码"

                return;
            }

            return true;
        }
    }
    return fnValidateCaptchaController;
});