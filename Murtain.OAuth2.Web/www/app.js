"use strict";


define(['app.module', 'angular-AMD', 'angular-AMD-ngload', 'app.router', 'app.constants', 'factories/identity-server'], function (app, angularAMD) {

    app.controller('index', fnIndexController);

    fnIndexController.$inject = ['$scope', '$timeout','constants'];

    function fnIndexController($scope, $timeout,constants) {

        console.log("IndexController running ...");

        var that = {
            identity: {},
            recordation: constants.recordation,
            company: {
                zh: constants.company.zh,
                en: constants.company.en
            },
            support: {
                agreement: constants.support.agreement,
                privacy: constants.support.privacy,
            }
        };

        that = angular.extend(this, that);

        active();

        function active() {
            $timeout(function () {
                that.identity = windows.identityServer.getModel();

                if (that.identity.autoRedirect && that.identity.redirectUrl) {
                    if (that.identity.autoRedirectDelay < 0) {
                        that.identity.autoRedirectDelay = 0;
                    }
                    $timeout(function () {
                        window.location = that.identity.redirectUrl;
                    }, that.identity.autoRedirectDelay * 1000);
                }
            });
        }
    }

    return angularAMD.bootstrap(app);
});