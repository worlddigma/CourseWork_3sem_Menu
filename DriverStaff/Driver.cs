using System.Globalization;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CourseWork_3sem
{
    // Перечисление для категорий прав водителей
    public enum Category
    {
        D = 'D',  // Категория D для автобусов
        E = 'E'   // Категория E для автобусов с прицепом
    }

    // Перечисление для класса водителей
    // Определяет уровень квалификации водителя
    public enum Class
    {
        Class1 = 1,  // Первый класс (высший)
        Class2 = 2,  // Второй класс
        Class3 = 3   // Третий класс (базовый)
    }

    // Класс, представляющий водителя автобуса
    public class Driver
    {
        // Приватные поля для хранения данных водителя
        private FullName _Name;          // ФИО водителя
        private int _Id;                 // Табельный номер
        private DateTime _DateOfBirth;   // Дата рождения
        private int _WorkExperience;     // Опыт работы (в годах)
        private Category _Category;      // Категория прав
        private Class _Class;            // Классность водителя

        // Свойство для ФИО с валидацией
        public FullName Name
        {
            get => _Name;
            set
            {
                IsValidName(value);      // Валидация ФИО
                _Name = value;
            }
        }

        // Свойство для табельного номера с валидацией
        public int Id
        {
            get => _Id;
            set
            {
                IsValidId(value);        // Валидация табельного номера
                _Id = value;
            }
        }

        // Свойство для даты рождения с валидацией
        public DateTime DateOfBirth
        {
            get => _DateOfBirth;
            set
            {
                IsValidDateOfBirth(value);  // Валидация даты рождения
                _DateOfBirth = value;
            }
        }

        // Свойство для опыта работы с валидацией
        // Валидация зависит от установленной даты рождения
        public int WorkExperience
        {
            get => _WorkExperience;
            set
            {
                // Проверка, что дата рождения уже установлена
                if (_DateOfBirth == default)
                    throw new InvalidOperationException("Сначала установите дату рождения");

                // Валидация опыта работы с учетом даты рождения
                IsValidWorkExperience(value, _DateOfBirth);
                _WorkExperience = value;
            }
        }

        // Свойство для категории прав с валидацией
        public Category Category
        {
            get => _Category;
            set
            {
                IsValidCategory(value);  // Валидация категории прав
                _Category = value;
            }
        }

        // Свойство для класса водителя с валидацией
        public Class Class
        {
            get => _Class;
            set
            {
                IsValidClass(value);     // Валидация класса водителя
                _Class = value;
            }
        }

        // Конструктор класса Driver
        public Driver(FullName name, int id, DateTime dateOfBirth,
                     int workExperience, Category category, Class driverClass)
        {
            // Используем свойства для установки значений, чтобы выполнялась валидация
            Name = name;
            Id = id;
            DateOfBirth = dateOfBirth;
            WorkExperience = workExperience;
            Category = category;
            Class = driverClass;
        }

        // Переопределение метода ToString для вывода информации о водителе
        public override string ToString() =>
            $"Водитель" +
            $"\n|--ФИО: {Name}" +
            $"\n|--Табельный номер: {Id}" +
            $"\n|--Дата рождения: {DateOfBirth:dd.MM.yyyy} (Возраст: {CalculateAge()} лет)" +
            $"\n|--Опыт работы: {WorkExperience}" +
            $"\n|--Категория прав: {Category}" +
            $"\n|--Классность: {(int)Class}";

        // Метод для расчета возраста водителя на текущую дату
        public int CalculateAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }

        // Статические методы валидации

        // Валидация ФИО водителя
        public static void IsValidName(FullName name)
        {
            // Проверка на null
            if (name == null)
                throw new ArgumentNullException(nameof(name), "ФИО не может быть null");

            // Проверка на пустое значение (если у FullName есть метод ToString())
            if (string.IsNullOrWhiteSpace(name.ToString()))
                throw new ArgumentException("ФИО не может быть пустым или состоять только из пробелов");
        }

        // Валидация табельного номера
        public static void IsValidId(int id)
        {
            // Проверка на положительное значение
            if (id <= 0)
                throw new ArgumentException("Табельный номер должен быть положительным числом.");

            // Проверка максимального значения (6 цифр)
            if (id > 999999)
                throw new ArgumentException("Табельный номер не может превышать 999999.");
        }

        // Валидация даты рождения
        public static void IsValidDateOfBirth(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            DateTime minDate = new DateTime(1950, 1, 1);  // Минимальная дата рождения

            // Проверка, что дата рождения не в будущем
            if (dateOfBirth > currentDate)
                throw new ArgumentException("Дата рождения не может быть в будущем");

            // Проверка минимальной даты рождения
            if (dateOfBirth < minDate)
                throw new ArgumentException($"Дата рождения не может быть раньше {minDate:dd.MM.yyyy}. Введено: {dateOfBirth:dd.MM.yyyy}");

            // Расчет точного возраста
            int age = currentDate.Year - dateOfBirth.Year;
            if (currentDate < dateOfBirth.AddYears(age)) age--;

            // Проверка минимального возраста (18 лет)
            if (age < 18)
                throw new ArgumentException($"Водитель должен быть старше 18 лет. Текущий возраст: {age} лет");

            // Проверка максимального возраста (70 лет)
            if (age > 70)
                throw new ArgumentException($"Водитель не может быть старше 70 лет. Текущий возраст: {age} лет");
        }

        // Валидация опыта работы
        public static void IsValidWorkExperience(int workExperience, DateTime dateOfBirth)
        {
            // Проверка на отрицательное значение
            if (workExperience < 0)
                throw new ArgumentException("Опыт работы не может быть отрицательным.");

            // Проверка максимального опыта работы
            if (workExperience > 50)
                throw new ArgumentException("Опыт работы не может превышать 50 лет.");

            // Расчет возраста водителя
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(age)) age--;

            // Расчет максимально возможного опыта работы (возраст минус 18 лет)
            int maxPossibleExperience = Math.Max(0, age - 18);

            // Проверка, что опыт работы не превышает максимально возможный
            if (workExperience > maxPossibleExperience)
                throw new ArgumentException($"Опыт работы ({workExperience} лет) не может превышать возраст минус 18 лет ({maxPossibleExperience} лет)");
        }

        // Валидация категории прав
        public static void IsValidCategory(Category category)
        {
            // Проверка, что значение является допустимым элементом перечисления
            if (!Enum.IsDefined(typeof(Category), category))
                throw new ArgumentException($"Недопустимая категория прав: {category}. Допустимые значения: D, E");

            // Дополнительная проверка для автобусов (только D или E)
            if (category != Category.D && category != Category.E)
                throw new ArgumentException($"Для управления автобусами требуется категория D или E. Получено: {category}");
        }

        // Валидация класса водителя
        public static void IsValidClass(Class driverClass)
        {
            // Проверка, что значение является допустимым элементом перечисления
            if (!Enum.IsDefined(typeof(Class), driverClass))
                throw new ArgumentException($"Недопустимый класс водителя: {(int)driverClass}. Допустимые значения: 1, 2, 3");
        }
    }
}