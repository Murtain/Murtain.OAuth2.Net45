'use strict';

define(['app', 'app.constants'], function (app) {

    app.controller('IndexController', fnIndexController)

    fnIndexController.$inject = ['$scope', '$timeout', 'account_service', 'constants'];

    function fnIndexController($scope, $timeout, account_service, constants) {

        console.log("IndexController running ...");

        var that = {
        };


        that = angular.extend(this, that);

        fnActived();

        function fnActived() {

        }

    };
    return fnIndexController;
});