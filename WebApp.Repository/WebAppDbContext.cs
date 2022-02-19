using Microsoft.EntityFrameworkCore;
using WebApp.Repository.Settings;

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
    }
}
