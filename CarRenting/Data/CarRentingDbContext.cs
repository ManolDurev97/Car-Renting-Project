namespace CarRenting.Data
{
    using CarRenting.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarRentingDbContext : IdentityDbContext<User>
    {
        
        public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(x => x.Dealer)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.DealerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category() { Id = 1 , Name = "Economy" },
                new Category() { Id = 2 , Name = "Electric" },
                new Category() { Id = 3 , Name = "Mini" },
                new Category() { Id = 4 , Name = "Minivan" },
                new Category() { Id = 5 , Name = "Sport" },
                new Category() { Id = 6 , Name = "SUV" },
                new Category() { Id = 7 , Name = "Luxury" },
                new Category() { Id = 8 , Name = "Super Sport" },
                new Category() { Id = 9 , Name = "Luxury SUV" },
                new Category() { Id = 10 , Name = "Ultra Luxury" }
                );
        }
        
    }
}