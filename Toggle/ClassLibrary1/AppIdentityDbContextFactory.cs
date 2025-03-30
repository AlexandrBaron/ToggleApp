using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Toggle.IdentityProvider.DAL;


namespace ClassLibrary1
{
    public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>, IDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }
        public AppIdentityDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            optionsBuilder.UseSqlServer($"Data source = (LocalDB)\\MSSQLLocalDB; Initial Catalog = ToggleIdentity; MultipleActiveResultSets = True; Integrated Security = True")
                .LogTo(Console.WriteLine, LogLevel.Information);
            return new AppIdentityDbContext(optionsBuilder.Options);

        }
    }
}
