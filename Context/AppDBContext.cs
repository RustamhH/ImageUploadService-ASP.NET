using ImageUploadService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageUploadService.Context
{
    public class AppDBContext:DbContext
    {

        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        // Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        
        // Tables

        public virtual DbSet<Image> Images { get; set; }
    }
}
