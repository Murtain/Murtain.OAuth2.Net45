'use strict';

define(['app', 'app.constants', 'jquery'], function (app) {

    app.controller('PermissionsController', PermissionsController)

    PermissionsController.$inject = ['$scope', '$http', '$timeout', '$window', 'constants'];

    function PermissionsController($scope, $http, $timeout, $window, constants) {

        console.log("PermissionsController running ...");

        var that = {
            identity: {},
            formData: {},
            submit: submit
        };


        that = angular.extend(this, that);

        active();

        function active() {

            $timeout(function () {
                that.identity = window.identityServer.getModel();
            });

        }

        function submit(index, clients) {
            $http({
                method: 'POST',
                url: that.identity.revokePermissionUrl,
                data: $("#permission-form").serialize(),  // pass in data as strings
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
            }).success(function (data) {
                that.identity.clients.splice(index, 1);
            });
        }

    };
    return PermissionsController;
});