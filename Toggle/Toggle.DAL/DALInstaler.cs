using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Toggle.DAL.DbCreators;
using Toggle.DAL.Entities;
using Toggle.DAL.Repositories;

namespace Toggle.DAL
{
    public static class DALInstaler
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddDbContextFactory<ToggleDbContext, ToggleDbContextFactory>();
            services.AddScoped<ToggleDbContext>(sp => sp.GetRequiredService<IDbContextFactory<ToggleDbContext>>().CreateDbContext());

            services.AddTransient(typeof(Repository<>));
            services.AddTransient<Repository<PersonEntity>, PersonRepository>();
            services.AddTransient<Repository<ProjectEntity>, ProjectRepository>();

            return services;
        }
    }
}
