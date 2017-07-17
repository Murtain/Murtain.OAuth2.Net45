'use strict';

define(['app', 'app.constants', 'services/account_service'], function (app) {

    app.controller('LoginController', LoginController)

    LoginController.$inject = ['$scope', '$timeout', 'account_service', 'constants'];

    function LoginController($scope, $timeout, account_service, constants) {

        console.log("LoginController running ...");

        var that = {
            formData: {
                username: '',
                password: '',
                rememberMe: true
            },
            identity: {},
        };


        that = angular.extend(this, that);

        active();

        /**
         * 初始化
         */
        function active() {

            $timeout(function () {
                that.identity = window.identityServer.getModel();
            });

        }

    };

    return LoginController;
});