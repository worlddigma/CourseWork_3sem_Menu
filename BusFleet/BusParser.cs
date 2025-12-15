using System;

namespace CourseWork_3sem
{
    // Класс для парсинга (разбора) строки в объект Bus
    public class BusParser
    {
        // Статический метод для преобразования строки в объект Bus
        public static Bus ParseFromString(string input)
        {
            // Проверка на пустую или состоящую из пробелов строку
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входная строка не может быть пустой");

            // Разделяем строку на части по разделителю ';'
            string[] parts = input.Split(';');

            // Проверяем количество частей - должно быть 8, по одной на каждое поле автобуса
            if (parts.Length != 8)
                throw new FormatException(
                    $"Ожидается 8 значений через ';'. Получено: {parts.Length}");

            // Создаем и возвращаем новый объект Bus
            return new Bus(
                stateNumber: parts[0].Trim(),         // Государственный номер
                brand: parts[1].Trim(),               // Бренд
                model: parts[2].Trim(),               // Модель
                capacity: ParseInt(parts[3], "вместимость"),           // Вместимость
                year: ParseInt(parts[4], "год выпуска"),               // Год выпуска
                yearOfMajorRepair: ParseInt(parts[5], "год кап. ремонта"), // Год кап. ремонта
                mileage: ParseInt(parts[6], "пробег"),                 // Пробег
                photo: parts[7].Trim()               // Фото
            );
        }

        // Вспомогательный метод для преобразования строки в целое число
        private static int ParseInt(string value, string fieldName)
        {
            // Пытаемся преобразовать строку в целое число
            if (!int.TryParse(value, out int result))
                // Если не удалось, выбрасываем исключение с информацией о поле
                throw new FormatException(
                    $"Неверный формат {fieldName}: '{value}'");
            return result;
        }

    }
}