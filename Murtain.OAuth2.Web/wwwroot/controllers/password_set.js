'use strict';

define(['app', 'app.constants', 'services/account_service', 'services/captcha_service'], function (app) {

    app.controller('PasswordSetController', PasswordSetController)

    PasswordSetController.$inject = ['$scope', '$rootScope', '$timeout', 'account_service', 'constants', '$stateParams', '$interval', '$state'];

    function PasswordSetController($scope, $rootScope, $timeout, account_service, constants, $stateParams, $interval, $state) {

        console.log("PasswordSetController running ...");

        var that = {
            identity: {},
            endpoint: {},
            payload: {
                captcha_type: $state.params.type
            },
            error: {},
            registration: registration
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

            });
        }

        /**
         * 立即注册
         */
        function registration() {

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

            if (that.payload.captcha_type == "register") {
                account_service.registration(that.payload).then(function (data) {
                    if (!data) {
                        $state.go('login')
                        return;
                    }
                    that.error = data;
                });
            }

            if (that.payload.captcha_type == "retrieve_password") {
                account_service.retrievePassword(that.payload).then(function (data) {
                    if (!data) {
                        $state.go('login')
                        return;
                    }
                    that.error = data;
                });
            }


        }
    }

    return PasswordSetController;
});