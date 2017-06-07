﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core
{
    public static class Constants
    {

        public static class ClaimTypes
        {

            public const string Name = "name";
            public const string NickName = "nick_name";
            public const string Birthday = "birthday";
            public const string Age = "age";
            public const string Gender = "gender";
            public const string Avatar = "avatar";
            public const string Mobile = "mobile";
            public const string Email = "email";
            public const string Country = "country";
            public const string Province = "province";
            public const string City = "city";
            public const string Address = "address";

            public static class Wechat 
            {
                public const string Name = "name";
                public const string NickName = "nick_name";
                public const string Birthday = "birthday";
                public const string Age = "age";
                public const string Gender = "sex";
                public const string Avatar = "headimgurl";
                public const string Mobile = "mobile";
                public const string Email = "email";
                public const string Country = "country";
                public const string Province = "province";
                public const string City = "city";
                public const string Address = "address";

            }
        }

        public static class Localization
        {
            public static class SourceName
            {
                public const string Messages = "Messages";
                public const string Events = "Events";
                public const string Views = "Views";
                public const string Scopes = "Scopes";
            }
            public static class RootNamespace
            {
                public const string Messages = "Murtain.OAuth2.Web.Configuration.Localization.Messages";
                public const string Events = "Murtain.OAuth2.Web.Configuration.Localization.Events";
                public const string Views = "Murtain.OAuth2.Web.Configuration.Localization.Views";
                public const string Scopes = "Murtain.OAuth2.Web.Configuration.Localization.Scopes";
            }


            public static class LocalizationCategories
            {
                public const string Messages = "Messages";
                public const string Events = "Events";
                public const string Scopes = "Scopes";
                public const string Views = "Views";
            }

            public class EventIds
            {
                public const string ClientPermissionsRevoked = "ClientPermissionsRevoked";
                public const string CspReport = "CspReport";
                public const string ExternalLoginError = "ExternalLoginError";
                public const string ExternalLoginFailure = "ExternalLoginFailure";
                public const string ExternalLoginSuccess = "ExternalLoginSuccess";
                public const string LocalLoginFailure = "LocalLoginFailure";
                public const string LocalLoginSuccess = "LocalLoginSuccess";
                public const string LogoutEvent = "LogoutEvent";
                public const string PartialLogin = "PartialLogin";
                public const string PartialLoginComplete = "PartialLoginComplete";
                public const string PreLoginFailure = "PreLoginFailure";
                public const string PreLoginSuccess = "PreLoginSuccess";
                public const string ResourceOwnerFlowLoginFailure = "ResourceOwnerFlowLoginFailure";
                public const string ResourceOwnerFlowLoginSuccess = "ResourceOwnerFlowLoginSuccess";
            }
            public class MessageIds
            {
                public const string ClientIdRequired = "ClientIdRequired";
                public const string ExternalProviderError = "ExternalProviderError";
                public const string Invalid_request = "invalid_request";
                public const string Invalid_scope = "invalid_scope";
                public const string InvalidUsernameOrPassword = "InvalidUsernameOrPassword";
                public const string MissingClientId = "MissingClientId";
                public const string MissingToken = "MissingToken";
                public const string MustSelectAtLeastOnePermission = "MustSelectAtLeastOnePermission";
                public const string NoExternalProvider = "NoExternalProvider";
                public const string NoMatchingExternalAccount = "NoMatchingExternalAccount";
                public const string NoSignInCookie = "NoSignInCookie";
                public const string NoSubjectFromExternalProvider = "NoSubjectFromExternalProvider";
                public const string PasswordRequired = "PasswordRequired";
                public const string SslRequired = "SslRequired";
                public const string Unauthorized_client = "unauthorized_client";
                public const string UnexpectedError = "UnexpectedError";
                public const string Unsupported_response_type = "unsupported_response_type";
                public const string UnsupportedMediaType = "UnsupportedMediaType";
                public const string UsernameRequired = "UsernameRequired";

                public const string InvalidMessageCode = "InvalidMessageCode";

                public const string UserAddComplete = "UserAddComplete";
                public const string UserAlreadyExists = "UserAlreadyExists";

                public const string CAPTCHA_MESSAGE_SEND_FAIL = "CAPTCHA_MESSAGE_SEND_FAIL";
                public const string CAPTCHA_MESSAGE_SEND_SUCCESS = "CAPTCHA_MESSAGE_SEND_SUCCESS";
                public const string CAPTCHA_MESSAGE_SEND_FAIL_NET = "CAPTCHA_MESSAGE_SEND_FAIL_NET";
                public const string CAPTCHA_EMAIL_SEND_FAIL = "CAPTCHA_EMAIL_SEND_FAIL";
                public const string CAPTCHA_EMAIL_SEND_SUCCESS = "CAPTCHA_EMAIL_SEND_SUCCESS";

                public const string NOT_FIND_BY_PRIMARY_KEY = "NOT_FIND_BY_PRIMARY_KEY";


                public const string USER_ACCOUNT_OLD_PASSWORD_ERROR = "USER_ACCOUNT_OLD_PASSWORD_ERROR";
                public const string USER_ACCOUNT_NOT_EXSIT_MOBILE = "USER_ACCOUNT_NOT_EXSIT_MOBILE";
                public const string USER_ACCOUNT_ERROR_CAPTCHA = "USER_ACCOUNT_ERROR_CAPTCHA";
                public const string USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA = "USER_ACCOUNT_EXPIRED_MESSAGE_CAPTCHA";
                public const string USER_ACCOUNT_MESSAGE_CAPTCHA_VALIDATE_OK = "USER_ACCOUNT_MESSAGE_CAPTCHA_VALIDATE_OK";
                public const string USER_ACCOUNT_PASSWORD_NOT_MATCHA = "USER_ACCOUNT_PASSWORD_NOT_MATCHA";

            }

            public class ScopeIds
            {
                public const string Address_DisplayName = "address_DisplayName";
                public const string All_claims_DisplayName = "all_claims_DisplayName";
                public const string Email_DisplayName = "email_DisplayName";
                public const string Offline_access_DisplayName = "offline_access_DisplayName";
                public const string Openid_DisplayName = "openid_DisplayName";
                public const string Phone_DisplayName = "phone_DisplayName";
                public const string Profile_Description = "profile_Description";
                public const string Profile_DisplayName = "profile_DisplayName";
                public const string Roles_DisplayName = "roles_DisplayName";
            }
        }


        public static class CacheNames
        {
            public const string MessageCaptcha = "AUTHORIZATION:MESSAGE_CAPTCHA_CODE:{0}_{1}_{2}";
            public const string EmailCaptcha = "AUTHORIZATION:EMAIL_CAPTCH_CODE:{0}_{1}_{2}";
        }

        public static class CookieNames
        {
            public const string LocalRistration = "Murtain.Passport.Restration.Local";
            public const string LocalRestrationSetPassword = "Murtain.Passport.Restration.SetPassword";

        }


        public static class Settings
        {
            public static class Resources
            {
                public const string Domain = "Settings.Resources.Domain";
            }

            public static class Authorization
            {
                public const string Domain = "Settings.Authorization.Domain";
                public const string CaptchaAddress = "Settings.Authorization.CaptchaAddress";

            }

            public static class Message
            {
                public const string MessageContentTemplate = "Settings.Message.ContentTemplate";
                public const string MessageSeverUrl = "Settings.Message.SeverUrl";
                public const string MessageDeptType = "Settings.Message.DeptType";
                public const string MessageBesType = "Settings.Message.BesType";
                public const string MessageExpiredTime = "Settings.Message.ExpiredTime";
            }
        }

    }
}
