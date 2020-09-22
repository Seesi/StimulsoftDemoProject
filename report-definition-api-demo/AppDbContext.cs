using Microsoft.EntityFrameworkCore;

namespace report_definition_api_demo
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ReportDefinition> ReportDefinitions { get; set; }
    }
}
