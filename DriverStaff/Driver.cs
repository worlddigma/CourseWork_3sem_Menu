using System.Globalization;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CourseWork_3sem
{
    public enum Category
    {
        D = 'D',
        E = 'E'
    }
    public enum Class
    {
        Class1 = 1,
        Class2 = 2,
        Class3 = 3
    }
    public class Driver
    {
        public static class Constants
        {

        }
        private FullName _Name; //          ФИО
        private int _Id; //                 Табельный номер
        private DateTime _DateOfBirth; //   Дата рождения
        private int _WorkExperience; //     Опыт работы
        private Category _Category; //      Категория прав
        private Class _Class; //            Классность водителя

        public FullName Name
        {
            get => _Name;
            set
            {
                IsValidName(value);
                _Name = value;
            }
        }

        public int Id
        {
            get => _Id;
            set
            {
                IsValidId(value);
                _Id = value;
            }
        }

        public DateTime DateOfBirth
        {
            get => _DateOfBirth;
            set
            {
                IsValidDateOfBirth(value);
                _DateOfBirth = value;
            }
        }

        public int WorkExperience
        {
            get => _WorkExperience;
            set
            {
                IsValidWorkExperience(value, _DateOfBirth);
                _WorkExperience = value;
            }
        }

        public Category Category
        {
            get => _Category;
            set
            {
                IsValidCategory(value);
                _Category = value;
            }
        }

        public Class Class
        {
            get => _Class;
            set
            {
                IsValidClass(value);
                _Class = value;
            }
        }

        public Driver(FullName name, int id, DateTime dateOfBirth, int workExperience, Category category, Class driverClass)
        {
            Name = name;
            Id = id;
            DateOfBirth = dateOfBirth;
            WorkExperience = workExperience;
            Category = category;
            Class = driverClass;
        }

        public override string ToString() =>
            $"Водитель" +
            $"\n|--ФИО: {Name}" +
            $"\n|--Табельный номер: {Id}" +
            $"\n|--Дата рождения: {DateOfBirth:dd.MM.yyyy} (Возраст: {CalculateAge()} лет)" +
            $"\n|--Опыт работы: {WorkExperience}" +
            $"\n|--Категория прав: {Category}" +
            $"\n|--Классность: {(int)Class}";

        public int CalculateAge()
        {
            return DateTime.Now.Year - DateOfBirth.Year;
        }
        // Статические методы валидации
        public static void IsValidName(FullName name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name), "ФИО не может быть null");

            // Если у FullName есть свои свойства для проверки
            if (string.IsNullOrWhiteSpace(name.ToString()))
                throw new ArgumentException("ФИО не может быть пустым или состоять только из пробелов");
        }

        public static void IsValidId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Табельный номер должен быть положительным числом.");

            if (id > 999999)
                throw new ArgumentException("Табельный номер не может превышать 999999.");
        }

        public static void IsValidDateOfBirth(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Now;
            DateTime minDate = new DateTime(1950, 1, 1);

            if (dateOfBirth > currentDate)
                throw new ArgumentException("Дата рождения не может быть в будущем");

            if (dateOfBirth < minDate)
                throw new ArgumentException($"Дата рождения не может быть раньше {minDate:dd.MM.yyyy}. Введено: {dateOfBirth:dd.MM.yyyy}");

            int age = currentDate.Year - dateOfBirth.Year;
            if (currentDate < dateOfBirth.AddYears(age)) age--;

            if (age < 18)
                throw new ArgumentException($"Водитель должен быть старше 18 лет. Текущий возраст: {age} лет");

            if (age > 70)
                throw new ArgumentException($"Водитель не может быть старше 70 лет. Текущий возраст: {age} лет");
        }

        public static void IsValidWorkExperience(int workExperience, DateTime dateOfBirth)
        {
            if (workExperience < 0)
                throw new ArgumentException("Опыт работы не может быть отрицательным.");

            if (workExperience > 50)
                throw new ArgumentException("Опыт работы не может превышать 50 лет.");

            // Проверка что опыт работы не больше возраста минус 18 лет
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now < dateOfBirth.AddYears(age)) age--;

            int maxPossibleExperience = Math.Max(0, age - 18);
            if (workExperience > maxPossibleExperience)
                throw new ArgumentException($"Опыт работы ({workExperience} лет) не может превышать возраст минус 18 лет ({maxPossibleExperience} лет)");
        }

        public static void IsValidCategory(Category category)
        {
            if (!Enum.IsDefined(typeof(Category), category))
                throw new ArgumentException($"Недопустимая категория прав: {category}. Допустимые значения: D, E");

            // Дополнительная проверка для автобусов
            if (category != Category.D && category != Category.E)
                throw new ArgumentException($"Для управления автобусами требуется категория D или E. Получено: {category}");
        }

        public static void IsValidClass(Class driverClass)
        {
            if (!Enum.IsDefined(typeof(Class), driverClass))
                throw new ArgumentException($"Недопустимый класс водителя: {(int)driverClass}. Допустимые значения: 1, 2, 3");
        }
    }
}

