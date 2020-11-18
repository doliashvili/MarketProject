using Microsoft.EntityFrameworkCore;

namespace ReadModelRepository.MSSQL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
