using System;
using System.Collections.ObjectModel;
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
            FillData(); // Обновляем отображение данных
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

                  car.Insert(); // Используем метод Insert из класса Car
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

                    car.Update(); // Используем метод Update из класса Car
                    FillData();
                }
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            var car = (Car)lBox.SelectedItem;
            if (car == null)
            {
                MessageBox.Show("Пожалуйста, выберите автомобиль для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
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
