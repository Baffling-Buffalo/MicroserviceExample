// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.API
{
    // NOT IN USE, ONLY FOR EXAMPLE
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //var roleResourse = new IdentityResource
            //{
            //    Name = "roles",
            //    DisplayName = "Roles",
            //    Description = "Allow the service access to your user roles.",
            //    UserClaims = new[] { JwtClaimTypes.Role, ClaimTypes.Role },
            //    ShowInDiscoveryDocument = true,
            //    Required = true,
            //    Emphasize = true
            //};

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                //roleResourse
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "API 1"){ },
                new ApiResource("api2", "API 2"),
                new ApiResource("contactsapi", "ContactsAPI"),
                new ApiResource("apiGW", "API Gateway"),
                new ApiResource("signalrhub", "SignalRHub")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {


                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    FrontChannelLogoutUri =  "http://localhost:5002/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        //"roles", // try
                        "api1",
                        "api2",
                        "contactsapi",
                        "signalrhub"
                    },

                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,

                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    UpdateAccessTokenClaimsOnRefresh = true,
                },
                


                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =           { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                }
            };
        }
    }
}