﻿using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LB7_Alkhimovich
{
   
    public partial class MainWindow : Window
    {
        ObservableCollection<Car> Cars;

        public MainWindow()
        {
            Cars = new ObservableCollection<Car>();
            InitializeComponent();
            lBox.DataContext = Cars;
        }

        public class CarContext : DbContext
        {
            public CarContext() : base("CarConnection")
            {
                Database.SetInitializer<CarContext>(new CarDatabaseInitializer());
            }

            public DbSet<Car> Cars { get; set; }
        }

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

        void FillData()
        {
            Cars.Clear();
            foreach (var item in Car.GetAllCars())
            {
                Cars.Add(item);
            }
        }

        private void BtnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var correctWindow = new Correct();
            correctWindow.Title = "Добавить новый автомобиль";
            correctWindow.EditButton.Visibility = Visibility.Collapsed;
            if (correctWindow.ShowDialog() == true) // Открыть окно и дождаться его закрытия
            {
                var car = new Car()
                {
                    Marka = correctWindow.CarMarka,
                    Model = correctWindow.CarModel,
                    Year = correctWindow.CarYear,
                    Color = correctWindow.CarColor
                };

                car.Insert();
                FillData();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный автомобиль из списка
            var car = (Car)lBox.SelectedItem;
            // Открываем окно редактирования, передавая ему выбранный автомобиль
            var correctWindow = new Correct(car);
            correctWindow.Title = "Редактировать автомобиль";
            correctWindow.AddButton.Visibility = Visibility.Collapsed;

            if (car != null)
            {
                // Открыть окно и дождаться его закрытия
                if (correctWindow.ShowDialog() == true)
                {
                    // Обновляем данные автомобиля
                    car.Marka = correctWindow.CarMarka;
                    car.Model = correctWindow.CarModel;
                    car.Year = correctWindow.CarYear;
                    car.Color = correctWindow.CarColor;

                    car.Update();
                    FillData();
                }
            }
            else
            {
                //MessageBox.Show("Пожалуйста, выберите автомобиль для редактирования.");
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            var car = (Car)lBox.SelectedItem;
            if (car != null && result == MessageBoxResult.Yes)
            {
                Car.Delete(car.CarId);
                FillData();
            }
        }
        private void DGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}