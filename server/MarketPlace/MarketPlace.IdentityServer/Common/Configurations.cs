using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace MarketPlace.IdentityServer.Common;

public static class Configurations
{
    public static List<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "Api",
                ClientName = "Api",
                ClientSecrets = {new Secret("client_secret".ToSha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api"
                }
            }
        };
    }

    public static List<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope
            {
                Name = "api",
                DisplayName = "api",
                Enabled = true,
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            }
        };
    }

    public static List<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("api", "api") {Scopes = new List<string> {"api"}}
        };
    }

    public static List<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
    }
}