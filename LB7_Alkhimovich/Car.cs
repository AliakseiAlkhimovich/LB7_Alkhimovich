using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LB7_Alkhimovich
{
    //[Table("Cars")] 
    public class Car
    {
        [Key] // Указываем, что это первичный ключ
        public int CarId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return String.Format("CarId={0} - Marka: {1} - Model: {2} - Year: {3} - Color: {4}", CarId, Marka, Model, Year, Color);
        }

        public static IEnumerable<Car> GetAllCars()
        {
            using (var context = new CarContext())
            {
                return context.Cars.ToList(); // Получаем все записи из базы данных
            }
        }

        public void Insert()
        {
            using (var context = new CarContext())
            {
                context.Cars.Add(this); // Добавляем текущий объект
                context.SaveChanges(); // Сохраняем изменения 
            }
        }

        public static Car GetCar(int id)
        {
            using (var context = new CarContext())
            {
                return context.Cars.Find(id); // Находим автомобиль по ID 
            }
        }

        public void Update()
        {
            using (var context = new CarContext())
            {
                context.Entry(this).State = EntityState.Modified; // Указываем, что объект изменен
                context.SaveChanges(); // Сохраняем изменения }
            }
        }

        public static void Delete(int id)
        {
            using (var context = new CarContext())
            {
               var car = context.Cars.Find(id); // Находим автомобиль по ID if (car != null)
                 {
                    context.Cars.Remove(car); // Удаляем автомобиль
                    context.SaveChanges(); // Сохраняем изменения 
                 }
            }
        }
    }
}
        
    
