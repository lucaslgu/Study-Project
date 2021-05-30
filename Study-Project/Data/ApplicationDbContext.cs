using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Study_Project.Models;

namespace Study_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category);

            builder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products);

            builder.Entity<Expense>().HasMany(p => p.Categories).WithMany(c => c.Expenses);
            builder.Entity<Expense>().HasMany(p => p.Products).WithMany(c => c.Expenses);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
