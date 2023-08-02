namespace CarRenting.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CarRentingDbContext : IdentityDbContext
    {
        public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
            : base(options)
        {
        }
    }
}