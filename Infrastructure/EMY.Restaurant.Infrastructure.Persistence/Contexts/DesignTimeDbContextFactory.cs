using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EMY.Restaurant.Infrastructure.Persistence.Contexts
{
    public class EMYRestaurantContextFactory : IDesignTimeDbContextFactory<EMYRestaurantDbContext>
    {

        EMYRestaurantDbContext IDesignTimeDbContextFactory<EMYRestaurantDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EMYRestaurantDbContext>();
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);

            return new EMYRestaurantDbContext(optionsBuilder.Options);
        }
    }
}
