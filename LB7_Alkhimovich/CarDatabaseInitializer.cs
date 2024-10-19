using System.Data.Entity;
using System.Linq;

namespace LB7_Alkhimovich
{
    public class CarDatabaseInitializer : DropCreateDatabaseIfModelChanges<CarContext>
    {
        protected override void Seed(CarContext context)
        {
            // Добавление начальных данных для таблицы Cars
            if (!context.Cars.Any())  // Если таблица пуста, мы добавляем начальные данные с помощью метода AddRange
            {
                context.Cars.AddRange(new Car[]
                {
                    new Car { Marka = "Toyota", Model = "Corolla", Year = 2020, Color = "Red" },
                    new Car { Marka = "Honda", Model = "Civic", Year = 2019, Color = "Blue" },
                    new Car { Marka = "Ford", Model = "Focus", Year = 2021, Color = "Black" }
                });
                context.SaveChanges();
            }
        }
    }
}
