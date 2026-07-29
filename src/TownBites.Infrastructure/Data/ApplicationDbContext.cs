using Microsoft.EntityFrameworkCore;
using TownBites.Domain.Entities;

namespace TownBites.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(x => x.Name)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.OwnerName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.PhoneNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.Address)
                    .HasMaxLength(500)
                    .IsRequired();
                entity.Property(x => x.Description)
                .HasMaxLength(1000);

                entity.Property(x => x.LogoUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.CoverImageUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.DeliveryCharge)
                    .HasColumnType("decimal(18,2)");

                entity.Property(x => x.MinimumOrderAmount)
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(x => x.Name)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(x => x.DisplayOrder)
                      .IsRequired();

                entity.HasOne(x => x.Restaurant)
                      .WithMany(x => x.Categories)
                      .HasForeignKey(x => x.RestaurantId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(x => x.Name)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(x => x.Description)
                      .HasMaxLength(500);

                entity.Property(x => x.Price)
                      .HasPrecision(10, 2);

                entity.Property(x => x.ImageUrl)
                      .HasMaxLength(500);

                entity.HasOne(x => x.Category)
                      .WithMany(x => x.MenuItems)
                      .HasForeignKey(x => x.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Cart>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId);

            modelBuilder.Entity<CartItem>()
                .Property(x => x.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.MenuItem)
                .WithMany()
                .HasForeignKey(x => x.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.ContactPerson)
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.City)
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.State)
                .HasMaxLength(100);

            modelBuilder.Entity<CustomerAddress>()
                .Property(x => x.Pincode)
                .HasMaxLength(10);
        }
    }
}
