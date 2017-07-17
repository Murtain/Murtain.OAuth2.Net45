"use strict";

define(['app', 'app.constants'], function (app) {

    app.factory('account_service', AccountService);

    AccountService.$inject = ['$http', 'constants'];

    function AccountService($http, constants) {

        return {
            retrievePassword: retrievePassword,
            registration: registration
        };


        function registration(payload) {

            return $http.post(constants.rootPath + 'account', payload)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    return error.data.error;
                });
        }
        function retrievePassword(payload) {

            return $http.post(constants.rootPath + 'account/password', payload)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    return error.data.error;
                });
        }
    }

});


