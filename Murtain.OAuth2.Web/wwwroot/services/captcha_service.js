"use strict";

define(['app', 'app.constants'], function (app) {

    app.factory('captcha_service', CaptchaService);

    CaptchaService.$inject = ['$http', 'constants'];

    function CaptchaService($http, constants) {

        return {
            validate: validate,
            sendCaptcha: sendCaptcha,
        };

        /**
         * 验证图形验证码并发送短信验证码
         * @param {any} payload
         */
        function sendCaptcha(payload) {

            return $http.post(constants.rootPath + 'account/sms', payload)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    return error.data.error;
                });
        }

        /**
         * 验证短信验证码
         * @param {any} payload
         */
        function validate(payload) {

            return $http.put(constants.rootPath + 'account/sms-validate', payload)
                .then(function (response) {
                    console.log(response)
                })
                .catch(function (error) {
                    return error.data.error;
                });
        }
    }

});


