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

namespace Identity.Api
{
    public static class Config
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
        {
          new TestUser
          {
            SubjectId = "818727",
            Username = "alice",
            Password = "alice",
            Claims =

            {
              new Claim(JwtClaimTypes.Name, "Alice Smith"),
              new Claim(JwtClaimTypes.GivenName, "Alice"),
              new Claim(JwtClaimTypes.FamilyName, "Smith"),
              new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
              new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
              new Claim(JwtClaimTypes.Role, "admin"),
              new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
              new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                IdentityServerConstants.ClaimValueTypes.Json)
            }
          },
          new TestUser
          {
            SubjectId = "88421113",
            Username = "bob",
            Password = "bob",
            Claims =
            {
              new Claim(JwtClaimTypes.Name, "Bob Smith"),
              new Claim(JwtClaimTypes.GivenName, "Bob"),
              new Claim(JwtClaimTypes.FamilyName, "Smith"),
              new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
              new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
              new Claim(JwtClaimTypes.Role, "user"),
              new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
              new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                IdentityServerConstants.ClaimValueTypes.Json)
            }
          }
        };
            }
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
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
                    }
                },
                new ApiResource
                {
                    Name = OrderApi.Resource.Name,
                    DisplayName = OrderApi.Resource.DisplayName,
                    Scopes = new List<string>
                    {
                        OrderApi.Scope.Read.Name,
                        OrderApi.Scope.Write.Name,
                    }
                },
                new ApiResource
                {
                    Name = BasketApi.Resource.Name,
                    DisplayName = BasketApi.Resource.DisplayName,
                    Scopes = new List<string>
                    {
                        BasketApi.Resource.Name
                    }
                },
            };

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientUrls) =>
            new Client[]
            {
                new Client
                {
                    ClientId = ProductSwaggerClient.Id,
                    ClientName = ProductSwaggerClient.Name,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["ProductsApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["ProductsApi"]}/swagger/" },
                    AllowedScopes = { ProductApi.Resource.Name }
                },
                new Client
                {
                    ClientId = OrderSwaggerClient.Id,
                    ClientName = OrderSwaggerClient.Name,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["OrdersApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["OrdersApi"]}/swagger/" },
                    AllowedScopes = { OrderApi.Resource.Name }
                },
                new Client
                {
                    ClientId = BasketSwaggerClient.Id,
                    ClientName = BasketSwaggerClient.Name,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret( "secret".Sha256())
                    },
                    RedirectUris = { $"{clientUrls["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{clientUrls["BasketApi"]}/swagger/" },
                    AllowedScopes = { BasketApi.Resource.Name }
                }
            };
    }
}
