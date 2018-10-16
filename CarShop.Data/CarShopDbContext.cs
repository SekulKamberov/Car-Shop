namespace CarShop.Web.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CarShop.Data.EntityModels;

    public class CarShopDbContext : IdentityDbContext<User>
    {
        public CarShopDbContext(DbContextOptions<CarShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; } 

        public DbSet<Dealer> Dealers { get; set; } 

        public DbSet<Extra> Extras { get; set; } 

        public DbSet<Brand> Brands { get; set; } 

        public DbSet<Car> Cars { get; set; } 

        public DbSet<CarDealer> CarDealers { get; set; } 

        public DbSet<CarData> CarInfo { get; set; } 

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Comment>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(a => a.AuthorId);

            builder
                .Entity<Comment>()
                .HasOne(a => a.Car)
                .WithMany(r => r.Comments)
                .HasForeignKey(a => a.CarId);

            builder
                .Entity<Car>()
                .HasOne(r => r.Brand)
                .WithMany(l => l.Cars)
                .HasForeignKey(r => r.BrandId);

            builder
                .Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            builder
                .Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            builder
                .Entity<CarDealer>()
                .HasKey(ma => new { ma.CarId, ma.DealerId });

            builder
                .Entity<CarDealer>()
                .HasOne(ra => ra.Dealer)
                .WithMany(a => a.Cars)
                .HasForeignKey(ra => ra.DealerId)
                .OnDelete(DeleteBehavior.Restrict); 
                

            builder
                .Entity<CarDealer>()
                .HasOne(ra => ra.Car)
                .WithMany(r => r.Dealers)
                .HasForeignKey(ra => ra.CarId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
               .Entity<CarData>()
               .HasKey(rf => new { rf.CarId, rf.ExtraId });

            builder
              .Entity<CarData>()
              .HasOne(rf => rf.Car)
              .WithMany(r => r.CarInfos)
              .HasForeignKey(rf => rf.CarId);

            builder
              .Entity<CarData>()
              .HasOne(rf => rf.Extra)
              .WithMany(f => f.CarInfos)
              .HasForeignKey(rf => rf.ExtraId);
        }
    }
}