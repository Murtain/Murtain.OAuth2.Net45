"use strict";

define(['app'], function (app) {

    app.directive('iAntiForgeryToken', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                token: "="
            },
            template: "<input type='hidden' name='{{token.name}}' value='{{token.value}}'>"
        };
    });

});


