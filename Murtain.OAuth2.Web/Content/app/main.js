'use strict';

require.config({

    baseUrl: "https://localhost:44373/content/app",

    paths: {
        "jquery": "https://cdn.bootcss.com/jquery/3.1.1/jquery.min",
        "bootstrap": "https://cdn.bootcss.com/bootstrap/3.2.0/js/bootstrap.min",
        "bootstrap-tooltip": "https://cdn.bootcss.com/bootstrap/3.2.0/js/tooltip",

        'angular': "https://cdn.bootcss.com/angular.js/1.5.8/angular",
        'angular-cookies': "https://cdn.bootcss.com/angular.js/1.5.8/angular-cookies",
        'angular-animate': "https://cdn.bootcss.com/angular.js/1.5.8/angular-animate",
        'angular-AMD': "https://cdn.jsdelivr.net/angular.amd/0.2.1/angularAMD.min",
        'angular-AMD-ngload': "https://cdn.jsdelivr.net/angular.amd/0.2.1/ngload.min",
        'angular-bindonce': "https://pasvaz.github.io/bindonce/javascripts/bindonce-0.3.1/bindonce.min",
        'angular-loading-bar': "https://cdn.bootcss.com/angular-loading-bar/0.9.0/loading-bar.min",
        'angular-ui-router': "https://cdn.bootcss.com/angular-ui-router/1.0.0-beta.3/angular-ui-router.min",
        'angular-toastr':'https://unpkg.com/angular-toastr@2.1.1/dist/angular-toastr.tpls',

        'app': '/content/app/app',

    },
    shim: {
        'angular': {
            exports: 'angular'
        },
        'angular-animate':{
            exports: 'angular-animate',
            deps: ['angular'],
        },
        'angular-ui-router': {
            exports: 'angular-ui-router',
            deps: ['angular'],
        },
        'angular-loading-bar': {
            exports: 'angular-loading-bar',
            deps: ['angular'],
        },
        'angular-AMD': {
            exports: 'angular-AMD',
            deps: ['angular'],
        },
        'angular-AMD-ngload': {
            exports: 'angular-AMD-ngload',
            deps: ['angular'],
        },
        'angular-toastr': {
            exports: 'angular-toastr',
            deps: ['angular'],
        }
    },
    deps: ['app'],
    urlArgs: "v=" + (new Date()).getTime()
});



