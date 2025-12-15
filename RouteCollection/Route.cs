using CourseWork_3sem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CourseWork_3sem
{
    // Класс, представляющий маршрут автобуса
    public class Route
    {
        // Константы для валидации данных маршрута
        public static class Constants
        {
            // Максимальная длина шифра маршрута
            public const int MaxCodeLength = 20;

            // Минимальная и максимальная длина названия пункта
            public const int MinPointLength = 2;
            public const int MaxPointLength = 50;

            // Диапазон значений для времени
            public const int MinDepartureHour = 0;     // Минимальный час (полночь)
            public const int MaxDepartureHour = 23;    // Максимальный час
            public const int MinMinute = 0;            // Минимальная минута
            public const int MaxMinute = 59;           // Максимальная минута
            public const int MinSecond = 0;            // Минимальная секунда
            public const int MaxSecond = 59;           // Максимальная секунда

            // Максимальное время в пути (в днях)
            public const int MaxTransportationDays = 7;

            // Минимальное время в пути (в минутах)
            public const int MinTransportationMinutes = 1;

            // Минимальное и максимальное количество дней отправления
            public const int MinDepartureDays = 1;
            public const int MaxDepartureDays = 7;
        }

        // Приватные поля для хранения данных маршрута
        private string _Code;                       // Шифр маршрута
        private string _StartingPoint;              // Начальный пункт
        private string _EndingPoint;                // Конечный пункт
        private List<string> _IntermediatePoints;   // Промежуточные пункты
        private DateTime _DepartureTime;            // Время отправления
        private List<DayOfWeek> _DepartureDays;     // Дни отправления
        private TimeSpan _TransportationTime;       // Время в пути

        // Свойство для шифра маршрута с валидацией
        public string Code
        {
            get => _Code;
            set
            {
                IsValidCode(value);  // Валидация шифра
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Шифр маршрута не может быть пустым");
                _Code = value;
            }
        }

        // Свойство для начального пункта с валидацией
        public string StartingPoint
        {
            get => _StartingPoint;
            set
            {
                IsValidPoint(value, "Начальный пункт");  // Валидация пункта
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Начальный пункт не может быть пустым");
                _StartingPoint = value;
            }
        }

        // Свойство для конечного пункта с валидацией
        public string EndingPoint
        {
            get => _EndingPoint;
            set
            {
                IsValidPoint(value, "Конечный пункт");  // Валидация пункта
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Конечный пункт не может быть пустым");
                _EndingPoint = value;
            }
        }

        // Свойство для промежуточных пунктов с валидацией
        public List<string> IntermediatePoints
        {
            get => _IntermediatePoints;
            set
            {
                IsValidIntermediatePoints(value);  // Валидация списка промежуточных пунктов
                if (value == null)
                    throw new ArgumentException("Список промежуточных пунктов не может быть null");
                _IntermediatePoints = value;
            }
        }

        // Свойство для дней отправления с валидацией
        public List<DayOfWeek> DepartureDays
        {
            get => _DepartureDays;
            set
            {
                IsValidDepartureDays(value);  // Валидация дней отправления
                _DepartureDays = value;
            }
        }

        // Свойство для времени в пути с валидацией
        public TimeSpan TransportationTime
        {
            get => _TransportationTime;
            set
            {
                IsValidTransportationTime(value);  // Валидация времени в пути
                _TransportationTime = value;
            }
        }

        // Свойство для времени отправления с валидацией
        public DateTime DepartureTime
        {
            get => _DepartureTime;
            set
            {
                IsValidDepartureTime(value);  // Валидация времени отправления

                // Дополнительные проверки времени
                if (value.Hour < 0 || value.Hour > 23)
                    throw new ArgumentException("Часы должны быть в диапазоне от 0 до 23");
                if (value.Minute < 0 || value.Minute > 59)
                    throw new ArgumentException("Минуты должны быть в диапазоне от 0 до 59");
                if (value.Second > 59 || value.Second < 0)
                    throw new ArgumentException("Секунды должны быть в диапазоне от 0 до 59");

                _DepartureTime = value;
            }
        }

        // Конструктор класса Route для инициализации всех полей
        public Route(string code, string startingPoint, string endingPoint,
                    List<string> intermediatePoints, List<DayOfWeek> departureDays,
                    TimeSpan transportationTime, DateTime departureTime)
        {
            // Используем свойства для установки значений, чтобы выполнялась валидация
            Code = code ?? throw new ArgumentNullException(nameof(code), "Шифр маршрута не может быть null");
            StartingPoint = startingPoint ?? throw new ArgumentNullException(nameof(startingPoint), "Начальный пункт не может быть null");
            EndingPoint = endingPoint ?? throw new ArgumentNullException(nameof(endingPoint), "Конечный пункт не может быть null");
            IntermediatePoints = intermediatePoints ?? throw new ArgumentNullException(nameof(intermediatePoints), "Список промежуточных пунктов не может быть null");
            DepartureDays = departureDays;
            TransportationTime = transportationTime;
            DepartureTime = departureTime;
        }

        // Переопределение метода ToString для вывода информации о маршруте
        public override string ToString() =>
    $"Маршрут" +
    $"\n|--Шифр маршрута: {Code}" +
    $"\n|--Начальный пункт: {StartingPoint}" +
    $"\n|--Конечный пункт: {EndingPoint}" +
    $"\n|--Промежуточные пункты: {(IntermediatePoints.Count > 0 ? string.Join(" → ", _IntermediatePoints) : "отсутствуют")}" +
    $"\n|--Время отправления: {DepartureTime:HH:mm}" +
    $"\n|--Дни отправления: {string.Join(", ", DaysToRussian(DepartureDays))}" +
    $"\n|--Время в пути: {TransportationTime}";

        // Вспомогательный метод для преобразования дней недели в русские названия
        private static string[] DaysToRussian(List<DayOfWeek> departureDays)
        {
            if (departureDays == null || departureDays.Count == 0)
                return Array.Empty<string>();

            return [.. departureDays.Select(day => day switch
                {
                    DayOfWeek.Sunday => "Воскресенье",
                    DayOfWeek.Monday => "Понедельник",
                    DayOfWeek.Tuesday => "Вторник",
                    DayOfWeek.Wednesday => "Среда",
                    DayOfWeek.Thursday => "Четверг",
                    DayOfWeek.Friday => "Пятница",
                    DayOfWeek.Saturday => "Суббота",
                    _ => "Неизвестно"
                })];
        }


        // Статические методы валидации

        // Валидация шифра маршрута
        public static void IsValidCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Шифр маршрута не может быть пустым или состоять только из пробелов");

            if (code.Length > Constants.MaxCodeLength)
                throw new ArgumentException($"Шифр маршрута не может превышать 20 символов. Получено: {code.Length}");
        }

        // Валидация пункта (начального, конечного или промежуточного)
        public static void IsValidPoint(string point, string pointName)
        {
            if (string.IsNullOrWhiteSpace(point))
                throw new ArgumentException($"{pointName} не может быть пустым или состоять только из пробелов");

            if (point.Length < Constants.MinPointLength)
                throw new ArgumentException($"{pointName} должен содержать минимум 2 символа. ({point})");

            if (point.Length > Constants.MaxPointLength)
                throw new ArgumentException($"{pointName} не может превышать 50 символов. ({point.Length})");

            // Проверка что название состоит из букв, цифр, пробелов и дефисов
            if (!point.All(c => char.IsLetterOrDigit(c) || c == ' ' || c == '-' || c == '.'))
                throw new ArgumentException($"{pointName} содержит недопустимые символы. Допустимы только буквы, цифры, пробелы, дефисы и точки");
        }

        // Валидация списка промежуточных пунктов
        public static void IsValidIntermediatePoints(List<string> intermediatePoints)
        {
            if (intermediatePoints == null)
                throw new ArgumentNullException(nameof(intermediatePoints), "Список промежуточных пунктов не может быть null");

            // Проверка каждого промежуточного пункта
            foreach (var point in intermediatePoints)
            {
                IsValidPoint(point, "промежуточный пункт");
            }

            // Проверка на дубликаты (без учета регистра)
            var distinctPoints = intermediatePoints.Select(p => p.ToLower()).Distinct();
            if (distinctPoints.Count() != intermediatePoints.Count)
                throw new ArgumentException("Список промежуточных пунктов содержит дубликаты");
        }

        // Валидация дней отправления
        public static void IsValidDepartureDays(List<DayOfWeek> departureDays)
        {
            if (departureDays == null)
                throw new ArgumentNullException(nameof(departureDays), "Список дней отправления не может быть null");

            if (departureDays.Count < Constants.MinDepartureDays)
                throw new ArgumentException("Должен быть указан хотя бы один день отправления");

            if (departureDays.Count > Constants.MaxDepartureDays)
                throw new ArgumentException("Не может быть больше 7 дней отправления в неделе");

            // Проверка что все дни валидны
            foreach (var day in departureDays)
            {
                if (!Enum.IsDefined(typeof(DayOfWeek), day))
                    throw new ArgumentException($"Недопустимый день недели: {day}");
            }

            // Проверка на дубликаты
            if (departureDays.Distinct().Count() != departureDays.Count)
                throw new ArgumentException("Список дней отправления содержит дубликаты");
        }

        // Валидация времени в пути
        public static void IsValidTransportationTime(TimeSpan transportationTime)
        {
            if (transportationTime <= TimeSpan.Zero)
                throw new ArgumentException("Время в пути должно быть положительным. Получено: " + transportationTime);

            if (transportationTime > TimeSpan.FromDays(Constants.MaxTransportationDays))
                throw new ArgumentException("Время в пути не может превышать 7 дней. Получено: " + transportationTime);

            if (transportationTime.TotalMinutes < Constants.MinTransportationMinutes)
                throw new ArgumentException("Время в пути должно быть не менее 1 минуты. Получено: " + transportationTime);
        }

        // Валидация времени отправления
        public static void IsValidDepartureTime(DateTime departureTime)
        {
            // Проверка времени (часы, минуты, секунды)
            if (departureTime.Hour < Constants.MinDepartureHour || departureTime.Hour > Constants.MaxDepartureHour)
                throw new ArgumentException($"Часы должны быть в диапазоне 0-23. Получено: {departureTime.Hour}");

            if (departureTime.Minute < Constants.MinMinute || departureTime.Minute > Constants.MaxMinute)
                throw new ArgumentException($"Минуты должны быть в диапазоне 0-59. Получено: {departureTime.Minute}");

            if (departureTime.Second < Constants.MinSecond || departureTime.Second > Constants.MaxSecond)
                throw new ArgumentException($"Секунды должны быть в диапазоне 0-59. Получено: {departureTime.Second}");
        }
    }
}