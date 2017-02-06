'use strict';

define(['app', 'app.constants', 'services/account-service', 'services/captcha-service'], function (app) {

    app.controller('SetPasswordController', fnSetPasswordController)

    fnSetPasswordController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'constants', '$stateParams', '$interval', '$state'];

    function fnSetPasswordController($scope, $rootScope, $timeout, account_service,  constants, $stateParams, $interval, $state) {

        console.log("SetPasswordController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {},
            error: {},
            fnLocalRegistration: fnLocalRegistration
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

            });
        }

        function fnLocalRegistration() {

            if (!fnValidatePayload()) {
                return;
            }

            account_service.fnLocalRegistration(that.payload)
                           .then(function (data) {
                               if (data) {
                                   $state.go('login-web')
                                   return;
                               }
                               that.error = data;
                           });

        }
        function fnValidatePayload() {

            if (!that.payload.password) {
                that.error.message = "请输入密码";

                return;
            }

            if (!/^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z\!\#\$\%\^\&\*\.\~]{6,21}$/.test(that.payload.password)) {
                that.error.message = "密码格式不正确";

                return;
            }

            if (!that.payload.confirm_password) {
                that.error.message = "请输入确认密码";

                return;
            }
            if (that.payload.password !== that.payload.confirm_password) {
                that.error.message = "两次输入的密码不正确";

                return;
            }
            return true;
        }
    }
    return fnSetPasswordController;
});