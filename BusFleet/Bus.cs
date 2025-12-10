using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace CourseWork_3sem
{
    public class Bus
    {
        public static class Constants
        {
            public const int MinCapacity = 5;
            public const int MaxCapacity = 100;
            public const int MinYear = 1950;
            public const int MinMileage = 0;
            public const int MaxMileage = 99_999_999;
            public const int StateNumberLength = 9;
            public const int MaxBrandLength = 50;
            public const int MinBrandLength = 2;
            public const int MaxModelLength = 50;
            public const int MinModelLength = 2;
        }
        public static readonly char[] AllowedStateLetters =
        { 'A', 'B', 'E', 'K', 'M', 'H', 'O', 'P', 'C', 'T', 'Y', 'X', 'a','b','e','m','n','o','p',
        'c','t','y','x'};
        private string _StateNumber; //     Государственный номер
        private string _Brand; //  Бренд автобуса
        private string _Model; //  Модель автобуса
        private  int _Capacity; //  Вместимость автобуса(количество мест в салоне)
        private int _Year; //      Год выпуска автобуса
        private int _YearOfMajorRepair; //  Год капитального ремонта
        private int _Mileage; //            Пробег на начало текущего года
        public string _Photo;     //          Фото автобуса

        public string StateNumber { get => _StateNumber;
            set
            {
                IsValidStateNumber(value);
                _StateNumber = value;
            }
        }
        public string Brand
        {
            get
            {
                return _Brand;
            }
            set 
            {
                IsValidBrand(value);
                _Brand = value;
            }
        }

        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                IsValidModel(value);
                _Model = value;
            }
        }

        public int Capacity
        {
            get
            {
                return _Capacity;
            }
            set
            {
                IsValidCapacity(value);
                _Capacity = value;
            }
        }

        public int Year
        {
            get
            {
                return _Year;
            }
            set
            {
                IsValidYear(value);
                _Year = value;
            }
        }

        public int YearOfMajorRepair
        { get => _YearOfMajorRepair;
            set
            {
                IsValidYearOfMajorRepair(value, Year);
                _YearOfMajorRepair = value;
            }
        }
        public int Mileage { get => _Mileage;
            set
            {
                IsValidMileage(value);
                _Mileage = value;
            }
        }

        public string Photo
        {
            get => _Photo;
            set
            {
                IsValidPhoto(value);
                _Photo = value;
            }
        }
        public Bus(string stateNumber,
                   string brand,
                   string model,
                   int capacity,
                   int year,
                   int yearOfMajorRepair,
                   int mileage,
                   string photo)
        {
            StateNumber = stateNumber;
            Brand = brand;
            Model = model;
            Capacity = capacity;
            Year = year;
            YearOfMajorRepair = yearOfMajorRepair;
            Mileage = mileage;
            Photo = photo;
        }


        public override string ToString() => 
            $"Автобус" +
            $"\n|--Государственный номер: {StateNumber}" +
            $"\n|--Бренд: {Brand}" +
            $"\n|--Модель: {Model}" +
            $"\n|--Вместимость: {Capacity}" +
            $"\n|--Год выпуска: {Year}" +
            $"\n|--Год капитального ремонта: {YearOfMajorRepair}" +
            $"\n|--Пробег на начало текущего года({DateTime.Now.Year}): {Mileage}";


        public static void IsValidStateNumber(string stateNumber)
        {
            if (string.IsNullOrWhiteSpace(stateNumber))
                throw new ArgumentException("Государственный номер не может быть пустым или состоять только из пробелов");

            if (stateNumber.Length != Constants.StateNumberLength)
                throw new ArgumentException($"Длина гос.номера должна быть 9 символов. Получено: {stateNumber.Length}. Пример: A111AA111");

            bool isValid = AllowedStateLetters.Contains(stateNumber[0]) &&  // Первая буква
                   char.IsDigit(stateNumber[1]) &&
                   char.IsDigit(stateNumber[2]) &&
                   char.IsDigit(stateNumber[3]) &&
                   AllowedStateLetters.Contains(stateNumber[4]) &&  // Буква
                   AllowedStateLetters.Contains(stateNumber[5]) &&  // Буква
                   char.IsDigit(stateNumber[6]) &&
                   char.IsDigit(stateNumber[7]) &&
                   char.IsDigit(stateNumber[8]);

            if (!isValid)
                throw new ArgumentException($"Неверный формат гос.номера. Ожидается: БукваЦифраЦифраЦифраБукваБукваЦифраЦифраЦифра. Пример: A111AA111");
        }

        public static void IsValidBrand(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Бренд автобуса не может быть пустым");

            if (brand.Length < Constants.MinBrandLength)
                throw new ArgumentException("Бренд должен содержать минимум 2 символа");

            if (brand.Length > Constants.MaxBrandLength)
                throw new ArgumentException("Бренд не может превышать 50 символов");
        }

        public static void IsValidModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Модель автобуса не может быть пустой");

            if (model.Length < Constants.MinModelLength)
                throw new ArgumentException("Модель должна содержать минимум 2 символа");

            if (model.Length > Constants.MaxModelLength)
                throw new ArgumentException("Модель не может превышать 50 символов");
        }

        public static void IsValidCapacity(int capacity)
        {
            if (capacity < Constants.MinCapacity)
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    $"Вместимость автобуса не может быть меньше 5 мест. Получено: {capacity}");

            if (capacity > Constants.MaxCapacity)
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    $"Вместимость автобуса не может превышать 100 мест. Получено: {capacity}");
        }

        public static void IsValidYear(int year)
        {
            int currentYear = DateTime.Now.Year;

            if (year < Constants.MinYear)
                throw new ArgumentOutOfRangeException(nameof(year),
                    $"Год выпуска не может быть раньше 1950. Получено: {year}");

            if (year > currentYear)
                throw new ArgumentOutOfRangeException(nameof(year),
                    $"Год выпуска не может быть в будущем. Текущий год: {currentYear}, получено: {year}");
        }

        public static void IsValidYearOfMajorRepair(int yearOfMajorRepair, int busYear)
        {
            int currentYear = DateTime.Now.Year;

            if (yearOfMajorRepair < 0)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта не может быть отрицательным. Получено: {yearOfMajorRepair}");

            if (yearOfMajorRepair < busYear)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта ({yearOfMajorRepair}) не может быть раньше года выпуска автобуса ({busYear})");

            if (yearOfMajorRepair > currentYear + 1)
                throw new ArgumentOutOfRangeException(nameof(yearOfMajorRepair),
                    $"Год капитального ремонта не может быть больше {currentYear + 1}. Получено: {yearOfMajorRepair}");
        }

        public static void IsValidMileage(int mileage)
        {
            if (mileage < Constants.MinMileage)
                throw new ArgumentOutOfRangeException(nameof(mileage),
                    $"Пробег не может быть отрицательным. Получено: {mileage}");

            if (mileage > Constants.MaxMileage)
                throw new ArgumentOutOfRangeException(nameof(mileage),
                    $"Пробег не может превышать 99 999 999 км. Получено: {mileage}");
        }

        public static void IsValidPhoto(string photo)
        {
            if (photo == null) throw new ArgumentNullException(nameof(photo));

        }
    }
}
