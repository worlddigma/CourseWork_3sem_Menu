using CourseWork_3sem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CourseWork_3sem
{
    public class Route
    {
        // Константы для валидации
        public static class Constants
        {
            // Длины строк
            public const int MaxCodeLength = 20;
            public const int MinPointLength = 2;
            public const int MaxPointLength = 50;

            // Время
            public const int MinDepartureHour = 0;
            public const int MaxDepartureHour = 23;
            public const int MinMinute = 0;
            public const int MaxMinute = 59;
            public const int MinSecond = 0;
            public const int MaxSecond = 59;
            public const int MaxTransportationDays = 7;
            public const int MinTransportationMinutes = 1;

            // Количество дней
            public const int MinDepartureDays = 1;
            public const int MaxDepartureDays = 7;
        }

        private string _Code; //                       Шифр 
        private string _StartingPoint; //              Начальная пункт
        private string _EndingPoint; //                Конечный пункт
        private List<string> _IntermediatePoints;//    Промежуточные пункты
        private DateTime _DepartureTime; //            Время отправления
        private List<DayOfWeek> _DepartureDays; //        Дни отправления
        private TimeSpan _TransportationTime; //       Время в пути
        public string Code
        {
            get => _Code;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _Code = value;
            }
        }
        public string StartingPoint
        {
            get => _StartingPoint;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _StartingPoint = value;
            }
        }
        public string EndingPoint
        {
            get => _EndingPoint;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _EndingPoint = value;
            }
        }
        public List<string> IntermediatePoints
        {
            get => _IntermediatePoints;
            set
            {
                if (value.DefaultIfEmpty() == null) throw new ArgumentException();
                _IntermediatePoints = value;
            }
        }
        public List<DayOfWeek> DepartureDays
        {
            get => _DepartureDays;
            set
            {
                _DepartureDays = value;
            }
        }

        public TimeSpan TransportationTime
        {
            get => _TransportationTime;
            set
            {
                _TransportationTime = value;
            }
        }
        public DateTime DepartureTime
        {
            get => _DepartureTime;
            set
            {
                if (value.Hour < 0 || value.Hour > 23) throw new ArgumentException();
                if (value.Minute < 0 || value.Minute > 59) throw new ArgumentException();
                if (value.Second > 59 || value.Second < 0) throw new ArgumentException();
                _DepartureTime = value;
            }
        }

        public Route(string code, string startingPoint, string endingPoint, List<string> intermediatePoints, List<DayOfWeek> departureDays, TimeSpan transportationTime, DateTime departureTime)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            StartingPoint = startingPoint ?? throw new ArgumentNullException(nameof(startingPoint));
            EndingPoint = endingPoint ?? throw new ArgumentNullException(nameof(endingPoint));
            IntermediatePoints = intermediatePoints ?? throw new ArgumentNullException(nameof(intermediatePoints));
            DepartureDays = departureDays;
            TransportationTime = transportationTime;
            DepartureTime = departureTime;
        }

        public override string ToString() =>
    $"Маршрут" +
    $"\n|--Шифр маршрута: {Code}" +
    $"\n|--Начальный пункт: {StartingPoint}" +
    $"\n|--Конечный пункт: {EndingPoint}" +
    $"\n|--Промежуточные пункты: {(IntermediatePoints.Count > 0 ? string.Join(" → ", _IntermediatePoints) : "отсутствуют")}" +
    $"\n|--Время отправления: {DepartureTime:HH:mm}" +
    $"\n|--Дни отправления: {string.Join(", ", DaysToRussian(DepartureDays))}" +
    $"\n|--Время в пути: {TransportationTime}";
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
        public static void IsValidCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Шифр маршрута не может быть пустым или состоять только из пробелов");

            if (code.Length > Constants.MaxCodeLength)
                throw new ArgumentException($"Шифр маршрута не может превышать 20 символов. Получено: {code.Length}");
        }
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

        public static void IsValidTransportationTime(TimeSpan transportationTime)
        {
            if (transportationTime <= TimeSpan.Zero)
                throw new ArgumentException("Время в пути должно быть положительным. Получено: " + transportationTime);

            if (transportationTime > TimeSpan.FromDays(Constants.MaxTransportationDays))
                throw new ArgumentException("Время в пути не может превышать 7 дней. Получено: " + transportationTime);

            if (transportationTime.TotalMinutes < Constants.MinTransportationMinutes)
                throw new ArgumentException("Время в пути должно быть не менее 1 минуты. Получено: " + transportationTime);
        }

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

//route code;
//starting point;
//ending point;
//list of intermediate points where the bus stops;
//departure time;
//departure days;
//travel time to the final destination.