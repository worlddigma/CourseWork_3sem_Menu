using System;
using System.Collections.Generic;

namespace CourseWork_3sem
{
    // Класс для управления коллекцией маршрутов
    public class RouteCollection
    {
        // Список маршрутов в коллекции
        public List<Route> Routes;

        // Конструктор по умолчанию, создает пустую коллекцию маршрутов
        public RouteCollection() => Routes = [];

        // Метод для добавления маршрута из строки
        public void Add(string value)
        {
            // Проверяем, что строка не пустая и не состоит только из пробелов
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Объявляем переменные для хранения распарсенных данных
                string code, startingPoint, endingPoint;
                List<string> intermediatePoints = new List<string>();
                DateTime departureTime;
                List<DayOfWeek> departureDays = new List<DayOfWeek>();
                TimeSpan transportationTime;

                // Разделяем строку на части по точке с запятой
                string[] strings = value.Split(';');

                try
                {
                    // Парсинг шифра маршрута (первый элемент)
                    code = strings[0];

                    // Парсинг начального пункта (второй элемент)
                    startingPoint = strings[1];

                    // Парсинг конечного пункта (третий элемент)
                    endingPoint = strings[2];

                    // Парсинг промежуточных пунктов (четвертый элемент)
                    // Разделяем по запятой, могут быть пустыми
                    string[] points = strings[3].Split(",");
                    foreach (var point in points)
                    {
                        if (!string.IsNullOrWhiteSpace(point))
                            intermediatePoints.Add(point);
                    }

                    // Парсинг времени отправления (пятый элемент)
                    if (!DateTime.TryParse(strings[4], out departureTime))
                        throw new ArgumentException("Неверный формат времени отправления");

                    // Парсинг дней отправления (шестой элемент)
                    string[] days = strings[5].Split(",");
                    foreach (var day in days)
                    {
                        if (!string.IsNullOrWhiteSpace(day))
                        {
                            if (!Enum.TryParse<DayOfWeek>(day, out DayOfWeek dayOfWeek))
                                throw new ArgumentException($"Неверный формат дня недели: '{day}'");
                            departureDays.Add(dayOfWeek);
                        }
                    }

                    // Парсинг времени в пути (седьмой элемент)
                    if (!TimeSpan.TryParse(strings[6], out transportationTime))
                        throw new ArgumentException("Неверный формат времени в пути");

                    // Создаем объект Route с распарсенными данными
                    Route route = new(code, startingPoint, endingPoint, intermediatePoints,
                                      departureDays, transportationTime, departureTime);

                    // Добавляем маршрут в список
                    Routes.Add(route);
                }
                catch (ArgumentException ex)
                {
                    // Пробрасываем ArgumentException с понятным сообщением
                    throw new ArgumentException($"Ошибка при добавлении маршрута: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    // Обработка других исключений с понятным сообщением
                    throw new Exception($"Произошла ошибка при обработке данных маршрута: {ex.Message}", ex);
                }
            }
            else
            {
                // Если строка пустая, выбрасываем исключение
                throw new ArgumentException("Строка с данными маршрута не может быть пустой");
            }
        }

        // Метод для создания копии коллекции маршрутов
        // Возвращает новый объект RouteCollection  
        public RouteCollection DeepCopy()
        {
            // Создаем новый экземпляр RouteCollection
            RouteCollection other = new RouteCollection();

            // Копируем все маршруты из текущего списка в новый
            foreach (Route route in Routes)
            {
                // Добавляем ссылку на тот же объект Route
                other.Routes.Add(route);
            }
            return other;
        }
    }
}