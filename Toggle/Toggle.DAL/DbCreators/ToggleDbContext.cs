using Microsoft.EntityFrameworkCore;
using Toggle.DAL.Entities;

namespace Toggle.DAL.DbCreators
{
    public class ToggleDbContext : DbContext
    {
        DbSet<PersonEntity> people { get; set; }
        DbSet<ActivityEntity> activities { get; set; }
        DbSet<ProjectEntity> projects { get; set; }
        public ToggleDbContext(DbContextOptions<ToggleDbContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonEntity>()
                .HasMany(p => p.Projects)
                .WithMany(c => c.People)
                .UsingEntity<Dictionary<string, string>>(
                "PersonProject",
                j => j.HasOne<ProjectEntity>().WithMany().HasForeignKey("ProjectId"),
                j => j.HasOne<PersonEntity>().WithMany().HasForeignKey("PersonId"),
                j => j.HasKey("PersonId", "ProjectId")
                );
        }
    }
}
