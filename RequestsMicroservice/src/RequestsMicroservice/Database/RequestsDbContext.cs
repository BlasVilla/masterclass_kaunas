using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RequestsMicroservice.Database
{
    public class RequestsDbContext : DbContext
    {
        public DbSet<Batch> Batches { get; set; }

        public DbSet<Request> Requests { get; set; }

        // ReSharper disable once SuggestBaseTypeForParameter
        public RequestsDbContext(DbContextOptions<RequestsDbContext> dbOptions) : 
            base(dbOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Batch)
                .WithMany(b => b.Requests)
                .HasForeignKey(r => r.BatchId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}