using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    // Класс для записи данных в файлы
    // Наследуется от базового класса FileDataManager
    public class WriteFile : FileDataManager
    {
        // Статический метод для записи всех данных в файлы
        public static void Write(BusFleet busFleet, DriverStaff driverStaff,
            RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            // Убеждаемся, что директория данных существует
            EnsureDataDirectoryExists();

            // Получаем пути к файлам из словаря FilePaths
            string BusFleetPath = GetFilePath(FilePaths["BusFleet"]);
            string DriverStaffPath = GetFilePath(FilePaths["DriverStaff"]);
            string RouteCollectionPath = GetFilePath(FilePaths["RouteCollection"]);
            string VolumeOfTransportationPath = GetFilePath(FilePaths["Transportation"]);

            // Запись данных об автобусах в файл
            using (StreamWriter BusFleetWrite = new(BusFleetPath, false))
            {
                // Проверяем, что коллекция автобусов не null
                if (busFleet.Buses != null)
                {
                    // Записываем каждый автобус в формате CSV (значения разделены точкой с запятой)
                    foreach (var buses in busFleet.Buses)
                    {
                        BusFleetWrite.WriteLine($"{buses.StateNumber};{buses.Brand};{buses.Model};{buses.Capacity};{buses.Year};{buses.YearOfMajorRepair};" +
                            $"{buses.Mileage};{buses.Photo}");
                    }
                }
            }

            // Запись данных о водителях в файл
            using (StreamWriter DriverStaffWrite = new(DriverStaffPath, false))
            {
                // Проверяем, что коллекция водителей не null
                if (driverStaff.Drivers != null)
                {
                    // Записываем каждого водителя в формате CSV
                    foreach (var drivers in driverStaff.Drivers)
                    {
                        DriverStaffWrite.WriteLine($"{drivers.Name};{drivers.Id};{drivers.DateOfBirth.ToString("dd-MM-yyyy")}" +
                            $";{drivers.WorkExperience};" +
                            $"{drivers.Category};{drivers.Class}");
                    }
                }
            }

            // Запись данных о маршрутах в файл
            using (StreamWriter RouteCollectionWrite = new(RouteCollectionPath, false))
            {
                // Проверяем, что коллекция маршрутов не null
                if (routeCollection.Routes != null)
                {
                    // Записываем каждый маршрут в формате CSV
                    foreach (var route in routeCollection.Routes)
                    {
                        RouteCollectionWrite.WriteLine($"{route.Code};{route.StartingPoint};{route.EndingPoint};{string.Join(',', route.IntermediatePoints)};" +
                            $"{route.DepartureTime.TimeOfDay};{string.Join(',', route.DepartureDays)};{route.TransportationTime}");
                    }
                }
            }

            // Запись данных о выполненных перевозках в файл
            using (StreamWriter VolumeOfTransportationWrite = new(VolumeOfTransportationPath, false))
            {
                // Проверяем, что коллекция перевозок не null
                if (volumeOfTransportation.CompletedTransportations != null)
                {
                    // Записываем каждую выполненную перевозку в формате CSV
                    foreach (var completedTransportation in volumeOfTransportation.CompletedTransportations)
                    {
                        VolumeOfTransportationWrite.WriteLine($"{completedTransportation.RouteCode};{completedTransportation.DriverCode};" +
                            $"{completedTransportation.Bus};{completedTransportation.TransportationDate.Date};{completedTransportation.SoldTickets};" +
                            $"{completedTransportation.TotalRevenue}");
                    }
                }
            }
        }
    }
}