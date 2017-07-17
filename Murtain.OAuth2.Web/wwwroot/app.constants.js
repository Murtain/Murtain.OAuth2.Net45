"use strict";

define(['app.module', 'app.config'], function (app, config) {

    app.constant('constants', {
        rootPath: config.domain + '/api/',
        recordation: '皖ICP备17003321号-1',
        company: {
            zh: 'x-dva Digital Co., Ltd.',
            en: 'x-dva Digital Co., Ltd.'
        },
        contact: {
            email: '392327013@qq.com'
        },
        support: {
            agreement: config.domain + '/support/#/agreement',
            privacy: config.domain + '/support/#/privacy',
        }

    });


});