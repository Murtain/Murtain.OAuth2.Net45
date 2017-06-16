'use strict';

define(['app', 'app.constants', 'jquery'], function (app) {

    app.controller('PermissionsController', fnPermissionsController)

    fnPermissionsController.$inject = ['$scope', '$http', '$timeout', '$window', 'constants'];

    function fnPermissionsController($scope, $http, $timeout, $window, constants) {

        console.log("LoginController running ...");

        var that = {
            permissions: {},
            formData: {},
            submit: fnSubmit
        };


        that = angular.extend(this, that);

        fnActived();

        function fnActived() {

            $timeout(function () {
                that.permissions = JSON.parse(document.getElementById('permissions').value);
                console.log(that.permissions)
            });

        }

        function fnSubmit(index, clients) {
            $http({
                method: 'POST',
                url: that.permissions.RevokePermissionUrl,
                data: $("#permission-form").serialize(),  // pass in data as strings
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }  // set the headers so angular passing info as form data (not request payload)
            }).success(function (data) {
                that.permissions.Clients.splice(index, 1);
            });
        }

    };
    return fnPermissionsController;
});