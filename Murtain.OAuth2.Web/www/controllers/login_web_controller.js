'use strict';

define(['app', 'app.constants', 'services/account_service'], function (app) {

    app.controller('LoginController', fnLoginController)

    fnLoginController.$inject = ['$scope', '$timeout', 'account_service', 'constants'];

    function fnLoginController($scope, $timeout, account_service, constants) {

        console.log("LoginController running ...");

        var that = {
            identity: {},

            fnLogin: fnLogin
        };


        that = angular.extend(this, that);

        fnActived();

        function fnActived() {

            $timeout(function () {
                that.identity = JSON.parse(document.getElementById('IdentityModelString').value);
                console.log(that.identity.ExternalProviders)
            });

        }
        function fnLogin() {
            return account_service.fnLogin()
                                  .then(function () {

                                  });
        }

    };
    return fnLoginController;
});