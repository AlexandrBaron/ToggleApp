using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Toggle.IdentityProvider.Api
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<IdentityUser> userManager;
        public ResourceOwnerPasswordValidator(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await userManager.FindByNameAsync(context.UserName);
            if (user is not null
                && await userManager.CheckPasswordAsync(user, context.Password))
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
