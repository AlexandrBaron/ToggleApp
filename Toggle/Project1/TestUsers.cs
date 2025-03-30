using System.Security.Claims;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace Toggle.IdentityProvider.Api
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>()
        {

            new TestUser()
            {
            SubjectId = "123",
            Username = "User1",
            Password = "Password1",
            Claims =
                {
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.FamilyName, "User"),
                    new Claim(JwtClaimTypes.Name, "Test User"),
                    new Claim("role", "admin"),
                    //new Claim("activities", "read")
                }
            }
        };
    }

}
