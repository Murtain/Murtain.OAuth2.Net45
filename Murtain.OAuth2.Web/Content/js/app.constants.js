"use strict";

define(['app.module'], function (app) {

	app.constant('Constants', {
		USER_ACCOUNT_LOGIN: '/Account/Login',
		USER_ACCOUNT_LOCAL_REGISTER: '/api/account/local-registration',
		USER_ACCOUNT_VALIDATE_CAPTCHA: '/api/account/validate-captcha',
		USER_ACCOUNT_REFRESH_CAPTCHA: 'http://murtain.imwork.net/Account/GenderatorImageCaptcha',

		SUPPORT_AGREEMENT_ENDPOINT: 'http://murtain.imwork.net/support/#/agreement',
		SUPPORT_PRIVACY_ENDPOINT: 'http://murtain.imwork.net/support/#/agreement',

	});


});