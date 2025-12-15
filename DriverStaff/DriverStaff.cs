namespace CourseWork_3sem
{
    public class DriverStaff
    {
        public List<Driver> Drivers;

        // Конструктор - инициализирует пустой список водителей
        public DriverStaff() => Drivers = [];

        // Добавление водителя через строку с разделителями ';'
        public void Add(string value)
        {
            // Проверка на пустую строку или пробелы
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Временные переменные для парсинга данных
                int id, workExperience;
                FullName name;
                Class driverClass;
                DateTime dateOfBirth;
                Category category;

                // Разделение входной строки по точкам с запятой
                string[] strings = value.Split(';');

                try
                {
                    // Парсинг ФИО (разделено пробелами)
                    string[] NameInputs = (strings[0]).Split(' ');
                    if (NameInputs.Length > 3) throw new ArgumentException("ФИО должно содержать не более 3 частей");
                    // Обработка ФИО: может быть 2 или 3 части
                    if (NameInputs.Length == 2)
                    {
                        name = new(NameInputs[0], NameInputs[1], "");
                    }
                    else name = new(NameInputs[0], NameInputs[1], NameInputs[2]);

                    // Парсинг числовых и перечисляемых полей
                    if (!int.TryParse(strings[1], out id)) throw new ArgumentException("Неверный формат ID");
                    if (!DateTime.TryParse(strings[2], out dateOfBirth)) throw new ArgumentException("Неверный формат даты рождения");
                    if (!int.TryParse(strings[3], out workExperience)) throw new ArgumentException("Неверный формат стажа работы");
                    if (!Enum.TryParse(strings[4], out category)) throw new ArgumentException("Неверная категория водителя");
                    if (!Enum.TryParse(strings[5], out driverClass)) throw new ArgumentException("Неверный класс водителя");
                    // Создание объекта Driver с распарсенными данными
                    Driver driver = new(name, id, dateOfBirth, workExperience, category, driverClass);

                    // Добавление водителя в список
                    Drivers.Add(driver);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении водителя: {ex.Message}");
                    Console.WriteLine($"Входная строка: {value}");
                }
            }
        }

        //  копирование объекта DriverStaff
        public DriverStaff DeepCopy()
        {
            DriverStaff other = new();
            // Копирование каждого водителя (поверхностное копирование объектов Driver)
            foreach (var driver in Drivers)
            {
                other.Drivers.Add(driver);
            }
            return other;
        }
    }
}