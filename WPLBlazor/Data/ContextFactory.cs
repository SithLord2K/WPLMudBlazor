using Microsoft.EntityFrameworkCore;
using WPLBlazor.Data;

namespace WPLBlazor.Data
{
    public class ContextFactory : DbContext
    {
        
        public ContextFactory(DbContextOptions<WPLStatsDBContext> options) : base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            
            optionsBuilder.UseSqlServer(GetConn());
        }
        private string GetConn()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var constring = config.GetConnectionString("WPLStatsDB");
            if (constring is null)
            {
                throw new Exception("Connection string not found");
            }
            return constring;
        }
    }
}
