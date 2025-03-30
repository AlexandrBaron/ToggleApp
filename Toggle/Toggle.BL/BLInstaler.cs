using Microsoft.Extensions.DependencyInjection;
using Toggle.BL.Facades;
using Toggle.BL.Facades.Interfaces;
using Toggle.BL.Mappers;

namespace Toggle.BL
{
    public static class BLInstaler
    {
        public static IServiceCollection AddBussinessLayer(this IServiceCollection services)
        {
            services.AddTransient<IActivityFacade, ActivityFacade>();
            services.AddTransient<IPersonFacade, PersonFacade>();
            services.AddTransient<IProjectFacade, ProjectFacade>();

            services.AddAutoMapper(sp =>
            {
                sp.AddProfile<ActivityMapperProfile>();
                sp.AddProfile<PersonMapperProfile>();
                sp.AddProfile<ProjectMapperProfile>();
            });
            return services;
        }
    }
}
