"use strict";

define(['app', 'app.constants'], function (app) {

    app.factory('account_service', fnAccountService);

    fnAccountService.$inject = ['$http', 'constants'];

    function fnAccountService($http, constants) {

        return {
            fnLocalRegistration: fnLocalRegistration
        };


        function fnLocalRegistration(payload) {

            return $http.post(constants.rootPath + 'account', payload)
                     .then(function (response) {
                         console.log(response)
                     })
                     .catch(function (error) {
                         return error.data.error;
                         console.log('XHR Failed for ' + error.data.request);
                     });
        }

    }

});


