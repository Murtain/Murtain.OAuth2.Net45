"use strict";


define(['app.module', 'angular-AMD', 'angular-AMD-ngload', 'app.router', 'app.constants'], function (app, angularAMD) {

    app.controller('index', fnIndexController);

    fnIndexController.$inject = ['$scope', '$timeout','constants'];

    function fnIndexController($scope, $timeout,constants) {

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
                that.identity = JSON.parse(document.getElementById('IdentityModelString').value);
            });
        }


    }

    return angularAMD.bootstrap(app);
});