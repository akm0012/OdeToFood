using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options) : base(options)
        {
            // Nothing here, but need to pass options to "base" constructor 
        }
        
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}