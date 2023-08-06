namespace CarRenting.Data
{
    using CarRenting.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarRentingDbContext : IdentityDbContext
    {
        public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category() { Id = 1 , Name = "Economy" },
                new Category() { Id = 2 , Name = "Compact" },
                new Category() { Id = 3 , Name = "Full Size-Wagon" },
                new Category() { Id = 4 , Name = "SUV" },
                new Category() { Id = 5 , Name = "Minivan" },
                new Category() { Id = 6 , Name = "luxury" },
                new Category() { Id = 7 , Name = "Cargo" }
                );
        }
        
    }
}