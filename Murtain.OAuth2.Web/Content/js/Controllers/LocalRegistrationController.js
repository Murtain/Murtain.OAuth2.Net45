'use strict';

define(['app'], function () {

    return ["$scope", function ($scope) {
        console.log('LocalRegistrationController loading ...');
        // properties
        $scope.identity = {
            signIn: getQueryString('signIn'),
            key: (new Date).valueOf(),
            captcha: 'http://murtain.imwork.net/Account/GenderatorImageCaptcha'
        };
        $scope.refreshCaptcha = function () {
            $scope.identity.captcha = $scope.identity.captcha.split('?')[0] + '?key=' + (new Date).valueOf();
        }
    }];

    
});