using ClassLibrary1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Toggle.IdentityProvider.DAL
{
    public static class IdentityProviderInstaler
    {
        public static IServiceCollection AddIdentityProviderDataAccessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IDbContextFactory<AppIdentityDbContext>, AppIdentityDbContextFactory>();
            services.AddTransient(serviceProvider =>
            {
                var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<AppIdentityDbContext>>();
                return dbContextFactory.CreateDbContext();
            });
            services.AddScoped<IUserStore<IdentityUser>, UserStore<IdentityUser, IdentityRole, AppIdentityDbContext>>();
            services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole, AppIdentityDbContext>>();
            return services;
        }
    }
}
