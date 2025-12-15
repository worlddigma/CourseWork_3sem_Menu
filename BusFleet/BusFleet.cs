using System.Collections.Generic;

namespace CourseWork_3sem
{
    // Класс, представляющий автопарк (коллекцию автобусов)
    public class BusFleet
    {
        // Список автобусов в автопарке
        public List<Bus> Buses;

        // Конструктор по умолчанию инициализирует пустой список автобусов
        public BusFleet() => Buses = [];

        // Метод для добавления автобуса из строки с валидацией

        public bool TryAddFromString(string input)
        {
            try
            {
                // Парсим строку в объект Bus с помощью парсера
                var bus = BusParser.ParseFromString(input);

                // Добавляем успешно распарсенный автобус в список
                Buses.Add(bus);

                return true;
            }
            catch (FormatException ex)
            {
                // Обработка ошибок формата (например, неверный формат числа)
                return false;
            }
            catch (ArgumentException ex)
            {
                // Обработка ошибок аргументов (например, невалидные данные)
                return false;
            }
        }

        // Метод для создания копии автопарка
        public BusFleet DeepCopy()
        {
            // Создаем новый экземпляр автопарка
            BusFleet other = new();

            // Копируем каждый автобус из текущего списка в новый список
            foreach (var bus in Buses)
            {
                // Добавляем ссылку на тот же объект Bus
                other.Buses.Add(bus);
            }

            // Возвращаем копию автопарка
            return other;
        }
    }
}