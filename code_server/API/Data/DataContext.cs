using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<UserType> Types { get; set; }
        
        public DbSet<AppFilm> Films { get; set; }
        
        public DbSet<AppSeries> Series { get; set; }
    }
}