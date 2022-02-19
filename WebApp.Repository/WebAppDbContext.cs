using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Repository.Entities;
using WebApp.Repository.Settings;
using WebApp.Settings;

namespace WebApp.Repository
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext()
        { }

        public WebAppDbContext(ISqlDbSettings settings)
            : base(new DbContextOptionsBuilder<WebAppDbContext>()
                  .UseSqlServer(settings.ConnectionString).Options)
        { }

        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        { }

        public virtual DbSet<DbUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonFile("globalSettings.json")
                    .Build();

                var dbSettings = configuration.GetSection(SettingsSections.Sql).Get<SqlDbSettings>();

                optionsBuilder.UseSqlServer(dbSettings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");
        }
    }
}
