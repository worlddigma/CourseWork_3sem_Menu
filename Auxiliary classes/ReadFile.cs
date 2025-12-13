using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    public class ReadFile : FileDataManager
    {
        public static void Read(BusFleet busFleet, DriverStaff driverStaff,
        RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            try
            {
                EnsureDataDirectoryExists();

                string BusFleetPath = GetFilePath(FilePaths["BusFleet"]);
                string DriverStaffPath = GetFilePath(FilePaths["DriverStaff"]);
                string RouteCollectionPath = GetFilePath(FilePaths["RouteCollection"]);
                string VolumeOfTransportationPath = GetFilePath(FilePaths["Transportation"]);

                // Корректное создание файлов
                CreateFileIfNotExists(BusFleetPath);
                CreateFileIfNotExists(DriverStaffPath);
                CreateFileIfNotExists(RouteCollectionPath);
                CreateFileIfNotExists(VolumeOfTransportationPath);

                // Чтение BusFleet
                using (StreamReader BusFleetRead = new(BusFleetPath))
                {
                    string line;
                    while ((line = BusFleetRead.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            busFleet.TryAddFromString(line);
                        }
                    }
                }

                // Чтение DriverStaff
                using (StreamReader DriverStaffRead = new(DriverStaffPath))
                {
                    string line;
                    while ((line = DriverStaffRead.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            driverStaff.Add(line);
                        }
                    }
                }

                // Чтение RouteCollection
                using (StreamReader RouteCollectionRead = new(RouteCollectionPath))
                {
                    string line;
                    while ((line = RouteCollectionRead.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            routeCollection.Add(line);
                        }
                    }
                }

                // Чтение VolumeOfTransportation
                using (StreamReader VolumeOfTransportationRead = new(VolumeOfTransportationPath))
                {
                    string text = VolumeOfTransportationRead.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lines)
                        {
                            if (!string.IsNullOrWhiteSpace(line.Trim()))
                            {
                                volumeOfTransportation.Add(line.Trim(), busFleet, driverStaff, routeCollection);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файлов: {ex.Message}");
                // Или используйте MessageBox для WinForms
                // MessageBox.Show($"Ошибка при чтении файлов: {ex.Message}", "Ошибка");
            }
        }

        private static void CreateFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }
        }
    }
}
