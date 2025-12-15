using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CourseWork_3sem
{
    // Класс, представляющий автобус с основными характеристиками
    public class Bus
    {
        // Класс констант для валидации данных автобуса
        public static class Constants
        {
            // Константы для вместимости автобуса
            public const int MinCapacity = 5;
            public const int MaxCapacity = 100;

            // Константы для года выпуска
            public const int MinYear = 1950;

            // Константы для пробега
            public const int MinMileage = 0;
            public const int MaxMileage = 99_999_999;

            // Константы для длины строковых полей
            public const int StateNumberLength = 9;
            public const int MaxBrandLength = 50;
            public const int MinBrandLength = 2;
            public const int MaxModelLength = 50;
            public const int MinModelLength = 2;
        }

        // Массив разрешенных букв для государственного номера (русские буквы, имеющие аналоги в латинице)
        public static readonly char[] AllowedStateLetters =
        {
            'A', 'B', 'E', 'K', 'M', 'H', 'O', 'P', 'C', 'T', 'Y', 'X', // Заглавные латинские
            'a', 'b', 'e', 'm', 'n', 'o', 'p', 'c', 't', 'y', 'x'       // Строчные латинские
        };

        // Приватные поля для хранения данных автобуса
        private string _StateNumber;     // Государственный номер
        private string _Brand;          // Бренд автобуса
        private string _Model;          // Модель автобуса
        private int _Capacity;          // Вместимость автобуса (количество мест в салоне)
        private int _Year;              // Год выпуска автобуса
        private int _YearOfMajorRepair; // Год капитального ремонта
        private int _Mileage;           // Пробег на начало текущего года
        public string _Photo;           // Фото автобуса

        // Свойство для государственного номера с валидацией
        public string StateNumber
        {
            get => _StateNumber;
            set
            {
                IsValidStateNumber(value);  // Валидация перед установкой значения
                _StateNumber = value;
            }
        }

        // Свойство для бренда автобуса с валидацией
        public string Brand
        {
            get => _Brand;
            set
            {
                IsValidBrand(value);  // Валидация перед установкой значения
                _Brand = value;
            }
        }

        // Свойство для модели автобуса с валидацией
        public string Model
        {
            get => _Model;
            set
            {
                IsValidModel(value);  // Валидация перед установкой значения
                _Model = value;
            }
        }

        // Свойство для вместимости автобуса с валидацией
        public int Capacity
        {
            get => _Capacity;
            set
            {
                IsValidCapacity(value);  // Валидация перед установкой значения
                _Capacity = value;
            }
        }

        // Свойство для года выпуска с валидацией
        public int Year
        {
            get => _Year;
            set
            {
                IsValidYear(value);  // Валидация перед установкой значения
                _Year = value;
            }
        }

        // Свойство для года капитального ремонта с валидацией
        public int YearOfMajorRepair
        {
            get => _YearOfMajorRepair;
            set
            {
                IsValidYearOfMajorRepair(value, Year);  // Валидация с учетом года выпуска
                _YearOfMajorRepair = value;
            }
        }

        // Свойство для пробега с валидацией
        public int Mileage
        {
            get => _Mileage;
            set
            {
                IsValidMileage(value);  // Валидация перед установкой значения
                _Mileage = value;
            }
        }

        // Свойство для фото автобуса с валидацией
        public string Photo
        {
            get => _Photo;
            set
            {
                IsValidPhoto(value);  // Валидация перед установкой значения
                _Photo = value;
            }
        }

        // Конструктор класса Bus для инициализации всех полей
        public Bus(string stateNumber,
                   string brand,
                   string model,
                   int capacity,
                   int year,
                   int yearOfMajorRepair,
                   int mileage,
                   string photo)
        {
            // Используем свойства для установки значений, чтобы выполнялась валидация
            StateNumber = stateNumber;
            Brand = brand;
            Model = model;
            Capacity = capacity;
            Year = year;
            YearOfMajorRepair = yearOfMajorRepair;
            Mileage = mileage;
            Photo = photo;
        }

        // Переопределение метода ToString для вывода информации об автобусе
        // Возвращает строковое представление объекта Bus
        public override string ToString() =>
            $"Автобус" +
            $"\n|--Государственный номер: {StateNumber}" +
            $"\n|--Бренд: {Brand}" +
            $"\n|--Модель: {Model}" +
            $"\n|--Вместимость: {Capacity}" +
            $"\n|--Год выпуска: {Year}" +
            $"\n|--Год капитального ремонта: {YearOfMajorRepair}" +
            $"\n|--Пробег на начало текущего года({DateTime.Now.Year}): {Mileage}";

        // Метод валидации государственного номера
        public static void IsValidStateNumber(string stateNumber)
        {
            // Проверка на пустую строку или строку из пробелов
            if (string.IsNullOrWhiteSpace(stateNumber))
                throw new ArgumentException("Государственный номер не может быть пустым или состоять только из пробелов");

            // Проверка длины номера
            if (stateNumber.Length != Constants.StateNumberLength)
                throw new ArgumentException($"Длина гос.номера должна быть 9 символов. Получено: {stateNumber.Length}. Пример: A111AA111");

            // Проверка формата номера: буква, три цифры, две буквы, три цифры
            bool isValid = AllowedStateLetters.Contains(stateNumber[0]) &&  // Первая буква
                   char.IsDigit(stateNumber[1]) &&  // Первая цифра
                   char.IsDigit(stateNumber[2]) &&  // Вторая цифра
                   char.IsDigit(stateNumber[3]) &&  // Третья цифра
                   AllowedStateLetters.Contains(stateNumber[4]) &&  // Четвертый символ - буква
                   AllowedStateLetters.Contains(stateNumber[5]) &&  // Пятый символ - буква
                   char.IsDigit(stateNumber[6]) &&  // Шестая цифра
                   char.IsDigit(stateNumber[7]) &&  // Седьмая цифра
                   char.IsDigit(stateNumber[8]);    // Восьмая цифра

            if (!isValid)
                throw new ArgumentException($"Неверный формат гос.номера. Ожидается: БукваЦифраЦифраЦифраБукваБукваЦифраЦифраЦифра. Пример: A111AA111");
        }

        // Метод валидации бренда автобуса
        public static void IsValidBrand(string brand)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Бренд автобуса не может быть пустым");

            // Проверка минимальной длины
            if (brand.Length < Constants.MinBrandLength)
                throw new ArgumentException("Бренд должен содержать минимум 2 символа");

            // Проверка максимальной длины
            if (brand.Length > Constants.MaxBrandLength)
                throw new ArgumentException("Бренд не может превышать 50 символов");
        }

        // Метод валидации модели автобуса\
        public static void IsValidModel(string model)
        {
            // Проверка на пустую строку
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Модель автобуса не может быть пустой");

            // Проверка минимальной длины
            if (model.Length < Constants.MinModelLength)
                throw new ArgumentException("Модель должна содержать минимум 2 символа");

            // Проверка максимальной длины
            if (model.Length > Constants.MaxModelLength)
                throw new ArgumentException("Модель не может превышать 50 символов");
        }

        // Метод валидации вместимости автобуса
        public static void IsValidCapacity(int capacity)
        {
            // Проверка минимальной вместимости
            if (capacity < Constants.MinCapacity)
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    $"Вместимость автобуса не может быть меньше 5 мест. Получено: {capacity}");

            // Проверка максимальной вместимости
            if (capacity > Constants.MaxCapacity)
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    $"Вместимость автобуса не может превышать 100 мест. Получено: {capacity}");
        }

        // Метод валидации года выпуска
        public static void IsValidYear(int year)
        {
            int currentYear = DateTime.Now.Year;

            // Проверка минимального года выпуска
            if (year < Constants.MinYear)
                throw new ArgumentOutOfRangeException(nameof(year),
                    $"Год выпуска не может быть раньше 1950. Получено: {year}");

            // Проверка, что год выпуска не в будущем
            if (year > currentYear)
                throw new ArgumentOutOfRangeException(nameof(year),
                    $"Год выпуска не может быть в будущем. Текущий год: {currentYear}, получено: {year}");
        }

        // Метод валидации года капитального ремонта
        public static void IsValidYearOfMajorRepair(int yearOfMajorRepair, int busYear)
        {
            int currentYear = DateTime.Now.Year;

            // Проверка на отрицательное значение
            if (yearOfMajorRepair < 0)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта не может быть отрицательным. Получено: {yearOfMajorRepair}");

            // Проверка, что ремонт не раньше выпуска
            if (yearOfMajorRepair < busYear)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта ({yearOfMajorRepair}) не может быть раньше года выпуска автобуса ({busYear})");

            // Проверка, что ремонт не слишком в будущем (допускается +1 год)
            if (yearOfMajorRepair > currentYear + 1)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта не может быть больше {currentYear + 1}. Получено: {yearOfMajorRepair}");
        }

        // Метод валидации пробега
        public static void IsValidMileage(int mileage)
        {
                // Проверка минимального пробега
                if (mileage < Constants.MinMileage)
                    throw new ArgumentOutOfRangeException(nameof(mileage),
                        $"Пробег не может быть отрицательным. Получено: {mileage}");

            // Проверка максимального пробега
            if (mileage > Constants.MaxMileage)
                throw new ArgumentOutOfRangeException(nameof(mileage),
                    $"Пробег не может превышать 99 999 999 км. Получено: {mileage}");
        }

        // Метод валидации фото 
        public static void IsValidPhoto(string photo)
        {
            // Проверка на null
            if (photo == null)
                throw new ArgumentNullException(nameof(photo));

        }
    }
}