"use strict";

define(['app.module'], function (app) {

    app.constant('constants', {
        rootPath: 'https://localhost:44373/api/',

		//USER_ACCOUNT_LOGIN: '/Account/Login',
		//USER_ACCOUNT_LOCAL_REGISTER: '/api/account/local-registration',
		//USER_ACCOUNT_VALIDATE_CAPTCHA: '/api/account/validate-captcha',
        //refreshCaptcha: 'http://murtain.imwork.net/Account/GenderatorImageCaptcha',
        recordation: '京ICP备14041720号-1 京ICP证140622号 京公网安备11010502025474',
        company: {
            zh: '上海MT信息科技有限公司',
            en:'Murtain Digital Co., Ltd.'
        },
        support:{
            agreement: 'http://murtain.imwork.net/support/#/agreement',
            privacy: 'http://murtain.imwork.net/support/#/agreement',
        }

	});


});