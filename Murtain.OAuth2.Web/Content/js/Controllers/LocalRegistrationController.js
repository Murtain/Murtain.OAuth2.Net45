'use strict';

define(['app', 'app.constants', 'services/AccountService'], function (app) {

    app.controller('LocalRegistrationController', LocalRegistrationController)

    LocalRegistrationController.$inject = ['$scope', '$timeout', 'AccountService', 'Constants'];

    function LocalRegistrationController($scope, $timeout, AccountService, Constants) {

        console.log("LocalRegistrationController running ...");

        var that = this;

        that.LoginEndpoint = null;
        that.CaptchaEndpoint = null;
        that.AgreementEndpoint = null;
        that.PrivacyEndpoint = null;

        that.RefreshCaptcha = _RefreshCaptcha;
        that.Register = _Register;


        _Active();

        function _Active() {
            console.log("LocalRegistrationController active ...");
            $timeout(function () {
                that.LoginEndpoint = JSON.parse(document.getElementById('IdentityModelString').value).LoginUrl;
            });

            that.AgreementEndpoint = Constants.SUPPORT_AGREEMENT_ENDPOINT;
            that.PrivacyEndpoint = Constants.SUPPORT_PRIVACY_ENDPOINT;

            that.CaptchaEndpoint = _GetCaptcha();
        }
        function _GetCaptcha() {
            return Constants.USER_ACCOUNT_REFRESH_CAPTCHA + '?key=' + _GetKey();
        }

        function _GetKey() {
            return (new Date).valueOf();
        }

        function _RefreshCaptcha() {
            that.CaptchaEndpoint = _GetCaptcha();

        }

    };
    return LocalRegistrationController;
});