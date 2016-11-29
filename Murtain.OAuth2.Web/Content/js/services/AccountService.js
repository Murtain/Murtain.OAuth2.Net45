"use strict";

define(['app'], function (app) {

    app.factory('AccountService', AccountService);

    AccountService.$inject = ['$http', 'Constants'];

    function AccountService($http, Constants) {

        return {
            Login: _Login,
            Register: _Register,
        };

        function _Login(url, formData) {

        }

        function _Register(payload) {

            return $http.post(Constants.USER_ACCOUNT_LOCAL_REGISTER, payload)
                       .then(_RequestComplete)
                       .catch(_RequestFailed);
        }


        function _RequestComplete() {
            return response.data.result
        }

        function _RequestFailed() {
            console.log('XHR Failed for login.' + error.data);
        }

    }

});


