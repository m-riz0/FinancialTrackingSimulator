using FinancialTrackingSimulator.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialTrackingSimulator.Context
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        private IWebHostEnvironment _environment;

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IWebHostEnvironment environment) : base(options)
        {
            _environment = environment;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "database.db");
            optionbuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
