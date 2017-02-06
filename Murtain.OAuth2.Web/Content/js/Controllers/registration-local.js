'use strict';

define(['app', 'app.constants', 'services/account-service', 'services/captcha-service'], function (app) {

    app.controller('LocalRegistrationController', fnLocalRegistrationController)

    fnLocalRegistrationController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'captcha_service', 'constants', '$state'];

    function fnLocalRegistrationController($scope, $rootScope, $timeout, account_service, captcha_service, constants, $state) {

        console.log("LocalRegistrationController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {},
            error: {},

            fnNext: fnNext,
            fnRefreshCaptcha: fnRefreshCaptcha,
        };


        that = angular.extend(this, that);

        fnActived();

        function fnActived() {
            console.log("LocalRegistrationController actived ...");

            $timeout(function () {
                that.identity = JSON.parse(document.getElementById('IdentityModelString').value);

                that.endpoint.login = that.identity.LoginUrl;
                that.endpoint.captcha = '/Account/GetLocalRistrationGraphicCaptcha';
                that.endpoint.agreement = constants.support.agreement;
                that.endpoint.privacy = constants.support.privacy;

            });
        }

        function fnGetKey() {
            return (new Date).valueOf();
        }

        function fnRefreshCaptcha() {
            that.endpoint.captcha = '/Account/GetLocalRistrationGraphicCaptcha' + '?key=' + fnGetKey();

        }
        function fnNext() {

            if (!fnValidatePayload()) {
                return;
            }

            captcha_service.fnValidateGraphicCapthcaAndSendMessage(that.payload)
                           .then(function (data) {
                               if (data) {
                                   $state.go('captcha-validate', { mobile: that.payload.mobile })
                                   return;
                               }
                               that.error = data;
                           });

        }

        function fnValidatePayload() {

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


            return true;
        }
    }
    return fnLocalRegistrationController;
});