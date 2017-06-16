"use strict";

define(['app', 'app.constants'], function (app) {

    app.factory('captcha_service', fnCaptchaService);

    fnCaptchaService.$inject = ['$http', 'constants'];

    function fnCaptchaService($http, constants) {

        return {
            fnValidateMessageCaptcha: fnValidateMessageCaptcha,
            fnValidateGraphicCapthcaAndSendMessage: fnValidateGraphicCapthcaAndSendMessage,
        };


        function fnValidateGraphicCapthcaAndSendMessage(payload) {

            return $http.post(constants.rootPath + 'account/sms', payload)
                       .then(function (response) {
                           console.log(response)
                       })
                       .catch(function (error) {
                           return error.data.error;
                           console.log('XHR Failed for ' + error.data.request);
                       });
        }

        function fnValidateMessageCaptcha(payload) {

            return $http.put(constants.rootPath + 'account/sms-validate', payload)
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


