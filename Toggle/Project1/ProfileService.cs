using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;

namespace Toggle.IdentityProvider.Api
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> userManager;
        public ProfileService(
            UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(subjectId);
            if (user is not null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var userClaims = await userManager.GetClaimsAsync(user);
                context.AddRequestedClaims(userRoles.Select(role => new Claim("role", role)));
                context.AddRequestedClaims(userClaims);
            }
        }
        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }

}
