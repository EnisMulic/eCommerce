using Common.Basket.Authorization;
using Common.Order.Authorization;
using Common.Product.Authorization;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using static IdentityServer4.IdentityServerConstants;

namespace Identity.Api
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    UserClaims = { JwtClaimTypes.Role }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(ProductApi.Scope.Read.Name, ProductApi.Scope.Read.DisplayName),
                new ApiScope(ProductApi.Scope.Write.Name, ProductApi.Scope.Write.DisplayName),
                new ApiScope(ProductApi.Scope.Delete.Name, ProductApi.Scope.Delete.DisplayName),
                new ApiScope(OrderApi.Scope.Read.Name, OrderApi.Scope.Read.DisplayName),
                new ApiScope(OrderApi.Scope.Write.Name, OrderApi.Scope.Write.DisplayName),
                new ApiScope(BasketApi.Resource.Name, BasketApi.Resource.DisplayName),
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new ApiResource[]
            {
                new ApiResource
                {
                    Name = ProductApi.Resource.Name,
                    DisplayName = ProductApi.Resource.DisplayName,
                    Scopes = new List<string>
                    {
                        ProductApi.Scope.Read.Name,
                        ProductApi.Scope.Write.Name,
                        ProductApi.Scope.Delete.Name,
                    },
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                },
                new ApiResource
                {
                    Name = OrderApi.Resource.Name,
                    DisplayName = OrderApi.Resource.DisplayName,
                    Scopes = new List<string>
                    {
                        OrderApi.Scope.Read.Name,
                        OrderApi.Scope.Write.Name,
                    },
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                },
                new ApiResource
                {
                    Name = BasketApi.Resource.Name,
                    DisplayName = BasketApi.Resource.DisplayName,
                    Scopes = new List<string>
                    {
                        BasketApi.Resource.Name
                    },
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                },
            };

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls) =>
            new Client[]
            {
                new Client
                {
                    ClientId = ProductSwaggerClient.Id,
                    ClientName = ProductSwaggerClient.Name,
                    AllowedGrantTypes = 
                    { 
                        GrantType.ClientCredentials, 
                        GrantType.Implicit
                    },
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["ProductsApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["ProductsApi"]}/swagger/" },
                    AllowedCorsOrigins = { clientUrls["ProductsApi"] },
                    AllowedScopes = 
                    { 
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        ProductApi.Resource.Name,
                        ProductApi.Scope.Read.Name,
                        ProductApi.Scope.Write.Name,
                        ProductApi.Scope.Delete.Name,
                        "roles"
                    },
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = OrderSwaggerClient.Id,
                    ClientName = OrderSwaggerClient.Name,
                    AllowedGrantTypes = 
                    { 
                        GrantType.ClientCredentials,
                        GrantType.Implicit
                    },
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["OrdersApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["OrdersApi"]}/swagger/" },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        OrderApi.Resource.Name,
                        OrderApi.Scope.Read.Name,
                        OrderApi.Scope.Write.Name,
                        "roles"
                    },
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = BasketSwaggerClient.Id,
                    ClientName = BasketSwaggerClient.Name,
                    AllowedGrantTypes = 
                    { 
                        GrantType.ClientCredentials,
                        GrantType.Implicit
                    },
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["BasketApi"]}/swagger/" },
                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        BasketApi.Resource.Name,
                        "roles"
                    },
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                }
            };
    }
}
