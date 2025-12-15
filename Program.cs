using CourseWork_3sem;
using System;
using System.Windows.Forms;

namespace CourseWork_3sem_Menu
{
    // Главный класс программы - точка входа в приложение
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа в приложение
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Инициализация коллекций данных

            // Создаем пустой автопарк (коллекция автобусов)
            BusFleet busFleet = new();

            // Создаем пустой штат водителей
            DriverStaff driverStaff = new();

            // Создаем пустой объем перевозок (история выполненных рейсов)
            VolumeOfTransportation volumeOfTransportation = new();

            // Создаем пустую коллекцию маршрутов
            RouteCollection routeCollection = new();

            // Загружаем данные из файлов при запуске приложения
            ReadFile.Read(busFleet, driverStaff, routeCollection, volumeOfTransportation);

            ApplicationConfiguration.Initialize();

            // Запускаем главную форму приложения (StartingMenu)
            Application.Run(new StartingMenu(busFleet, routeCollection, driverStaff, volumeOfTransportation));
        }

    }
}