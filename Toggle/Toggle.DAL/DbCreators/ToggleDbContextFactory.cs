using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Toggle.DAL.DbCreators
{
    public class ToggleDbContextFactory : IDesignTimeDbContextFactory<ToggleDbContext>, IDbContextFactory<ToggleDbContext>
    {
        public ToggleDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public ToggleDbContext CreateDbContext()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<ToggleDbContextFactory>()
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ToggleDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Toggle"));

            return new ToggleDbContext(optionsBuilder.Options);
        }
    }
}
