﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("narilearsi", "My primera api narilearsi")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedScopes = { "narilearsi" }
                },

                // MVC client using hybrid flow
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "narilearsi"
                    }
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "clientNarileasi",
                    //ClientName = "SPA Client",
                    //ClientUri = "http://localhost:8014",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RedirectUris =
                    {
                        "http://localhost:8014/static/index.html",
                        "http://localhost:8014/static/callback.html",
                        "http://localhost:8014/static/silent-renew.html",
                        "http://localhost:8014/static/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:8014/index.html" },
                    AllowedCorsOrigins = { "http://localhost:8014" },

                    AllowedScopes = { "openid", "profile", "narilearsi" }
                }
            };
        }
    }
}