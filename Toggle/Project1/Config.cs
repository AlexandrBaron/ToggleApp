using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Project1
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                //new IdentityResource("roles", new[] { "role" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                {
                    new("toggleapi", new List<string>{"activities"}),
                    new("roles"){UserClaims = new List<string>{"role"}},
                    new ApiScope(IdentityServerConstants.StandardScopes.OfflineAccess)
                };


        public static IEnumerable<Client> Clients =>
            new Client[]
                { new Client()
                {
                    ClientName = "Toggle Client",
                    ClientId = "toggleclient",
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    RedirectUris = new List<string>{"https://oauth.pstmn.io/v1/callback" },
                    AllowedGrantTypes = new List<string>
                    {
                        GrantType.ClientCredentials,
                        GrantType.ResourceOwnerPassword,
                        GrantType.AuthorizationCode,
                    },
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = new List<string>
                    {
                        "toggleapi",
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        "roles"
                    }
                }
            };
    }
}