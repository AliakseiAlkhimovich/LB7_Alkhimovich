using System.Data.Entity;


namespace LB7_Alkhimovich
{
    public class CarContext : DbContext
    {
        public CarContext() : base("CarConnection")
        {
            Database.SetInitializer<CarContext>(new CarDatabaseInitializer());
        }

        public DbSet<Car> Cars { get; set; }
    }
}
