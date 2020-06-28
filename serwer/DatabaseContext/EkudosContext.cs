using DatabaseContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext
{
    public class EkudosContext : DbContext
    {
        public EkudosContext(DbContextOptions<EkudosContext> options) : base(options)
        { }

        public DbSet<EkudosModel> Kudoses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EkudosModel>().ToTable("Kudoses");
        }
    }
}
