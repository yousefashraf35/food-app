using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {   

         public DbSet<Address> Addresses { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade); // optional: cascade delete addresses if user deleted

            builder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne(mi => mi.Restaurant)
                .HasForeignKey(mi => mi.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade); // optional: cascade delete menu items if restaurant deleted

            builder.Entity<Category>()
                .HasMany(c => c.MenuItems)
                .WithOne(mi => mi.Category)
                .HasForeignKey(mi => mi.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // optional: cascade delete menu items if category deleted
        }

        
    }
}
