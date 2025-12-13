namespace CourseWork_3sem
{
    // Базовый класс для работы с файлами данных
    public abstract class FileDataManager
    {
        // Общие константы и пути
        protected static string DataDirectory =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Passenger travel system");

        protected static string GetFilePath(string fileName) =>
            Path.Combine(DataDirectory, fileName);

        protected static readonly Dictionary<string, string> FilePaths = new()
        {
            ["BusFleet"] = "Bus fleet.txt",
            ["DriverStaff"] = "Driver staff.txt",
            ["RouteCollection"] = "Route collection.txt",
            ["Transportation"] = "Volume of transportation.txt"
        };

        // Методы для работы с директорией
        protected static void EnsureDataDirectoryExists()
        {
            if (!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
                Console.WriteLine($"Создана директория данных: {DataDirectory}");
            }
        }

    }
}
