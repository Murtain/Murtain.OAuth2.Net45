"use strict";

define(['app', 'services/AccountService'], function (app) {

    app.controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$timeout', 'AccountService'];

    function LoginController($scope, $timeout, AccountService) {
        console.log("LoginController running ...");

        var that = this;

        that.Identity = null;
        that.Login = _Login;


        _Active();

        function _Active() {
            $timeout(function () {
                that.Identity = JSON.parse(document.getElementById('IdentityModelString').value);

                console.log(that);
            });
        }

        function _Login() {
            return false;
        }


    }

    return LoginController;
});