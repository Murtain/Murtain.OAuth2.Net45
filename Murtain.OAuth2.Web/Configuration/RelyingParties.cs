using IdentityModel.Constants;
using IdentityServer3.WsFederation.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;

namespace Murtain.OAuth2.Web.Configuration
{
    public class RelyingParties
    {
        public static IEnumerable<RelyingParty> Get()
        {
            return new List<RelyingParty>
            {

                /////////////////////////////////////////////////////////////
                // MVC WsFederation （Murtain.OAuth2.MVC.WsFederationAuthentication）
                /////////////////////////////////////////////////////////////
                new RelyingParty
                {
                    Realm = "urn:owinrp:murtain_oauth2_mvc_wsfederationauthentication",
                    Enabled = true,
                    ReplyUrl = "https://localhost:44327/",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1
                },


                new RelyingParty
                {
                    Realm = "urn:testrp",
                    Name = "Test RP",
                    Enabled = true,
                    ReplyUrl = "https://web.local/idsrvrp/",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1,

                    ClaimMappings = new Dictionary<string,string>
                    {
                        { "id", ClaimTypes.NameIdentifier },
                        { "given_name", ClaimTypes.Name },
                        { "email", ClaimTypes.Email }
                    }
                },
                new RelyingParty
                {
                    Realm = "urn:owinrp",
                    Enabled = true,
                    ReplyUrl = "http://localhost:31208/account/member",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1,

                    ClaimMappings = new Dictionary<string, string>
                    {
                        { "id", ClaimTypes.NameIdentifier },
                        { "name", ClaimTypes.Name },
                        { "email", ClaimTypes.Email },
                    }
                },
                 new RelyingParty
                {
                    Realm = "urn:owinrp:square",
                    Enabled = true,
                    ReplyUrl = "http://square.x-dva.com/",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1,

                    ClaimMappings = new Dictionary<string, string>
                    {
                        { "id", ClaimTypes.NameIdentifier },
                        { "name", ClaimTypes.Name },
                        { "email", ClaimTypes.Email },
                    }
                },
                new RelyingParty
                {
                    Realm = "urn:owinsinalr",
                    Enabled = true,
                    ReplyUrl = "http://localhost:24000/",
                    TokenType = TokenTypes.Saml2TokenProfile11,
                    TokenLifeTime = 1,

                    ClaimMappings = new Dictionary<string, string>
                    {
                        { "id", ClaimTypes.NameIdentifier },
                        { "name", ClaimTypes.Name },
                        { "email", ClaimTypes.Email }
                    }
                }
            };
        }

    }
}