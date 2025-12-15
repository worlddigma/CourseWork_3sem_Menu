namespace CourseWork_3sem
{
    public class RouteCollection
    {
        public List<Route> Routes;

        // Конструктор - инициализирует пустой список маршрутов
        public RouteCollection() => Routes = [];

        // Добавление маршрута через строку с разделителями ';'
        public void Add(string value)
        {
            // Проверка на пустую строку или пробелы
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Временные переменные для парсинга данных
                string code, startingPoint, endingPoint;
                List<string> intermediatePoints = new List<string>();
                DateTime departureTime;
                List<DayOfWeek> departureDays = new List<DayOfWeek>();
                TimeSpan transportationTime;

                // Разделение входной строки по точкам с запятой
                string[] strings = value.Split(';');
                try
                {
                    // Парсинг основных строковых полей
                    code = strings[0]; // Код маршрута
                    startingPoint = strings[1]; // Начальный пункт
                    endingPoint = strings[2]; // Конечный пункт

                    // Парсинг промежуточных пунктов (разделены запятыми)
                    string[] points = strings[3].Split(",");
                    foreach (var point in points) intermediatePoints.Add(point);

                    // Парсинг времени отправления
                    if (!DateTime.TryParse(strings[4], out departureTime)) throw new ArgumentException("Неверный формат времени отправления");

                    // Парсинг дней отправления (разделены запятыми)
                    string[] days = strings[5].Split(",");
                    foreach (var day in days)
                    {
                        if (!Enum.TryParse<DayOfWeek>(day, out DayOfWeek dayOfWeek)) throw new ArgumentException($"Неверный день недели: {day}");
                        departureDays.Add(dayOfWeek);
                    }

                    // Парсинг времени в пути
                    if (!TimeSpan.TryParse(strings[6], out transportationTime)) throw new ArgumentException("Неверный формат времени в пути");

                    // Создание объекта Route с распарсенными данными
                    Route route = new(code, startingPoint, endingPoint, intermediatePoints,
                                      departureDays, transportationTime, departureTime);

                    // Добавление маршрута в список
                    Routes.Add(route);
                }
                catch (Exception ex)
                {
                    // Вывести сообщение об ошибке для отладки
                    Console.WriteLine($"Ошибка при добавлении маршрута: {ex.Message}");
                    Console.WriteLine($"Входная строка: {value}");
                }
            }
        }

        // копирование объекта RouteCollection
        public RouteCollection DeepCopy()
        {
            RouteCollection other = new RouteCollection();
            // Копирование каждого маршрута (поверхностное копирование объектов Route)
            foreach (Route route in Routes)
            {
                other.Routes.Add(route);
            }
            return other;
        }
    }
}