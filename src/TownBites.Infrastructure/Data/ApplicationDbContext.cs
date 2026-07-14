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

    }
}
