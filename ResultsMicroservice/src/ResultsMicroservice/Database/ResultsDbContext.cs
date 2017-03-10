using Microsoft.EntityFrameworkCore;

namespace ResultsMicroservice.Database
{
    public class ResultsDbContext : DbContext
    {
        public DbSet<Result> Results { get; set; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public ResultsDbContext(DbContextOptions<ResultsDbContext> options) : 
            base(options)
        {
            
        }
    }
}