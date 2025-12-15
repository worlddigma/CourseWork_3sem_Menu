using System.Xml.Serialization;

namespace CourseWork_3sem
{
    // Класс, представляющий выполненный рейс (завершенную перевозку)
    public class CompletedTransportation
    {
        // Приватные поля для хранения данных о выполненном рейсе
        private Route _RouteCode;            // Шифр маршрута (объект Route)
        private Driver _DriverCode;          // Табельный номер водителя (объект Driver)
        private Bus _Bus;                    // Автобус, на котором был выполнен рейс
        private DateTime _TransportationDate; // Дата выполнения рейса
        private Tickets _SoldTickets;        // Информация о проданных билетах
        private int _TotalRevenue;           // Общая выручка за рейс

        // Свойство для даты выполнения рейса
        public DateTime TransportationDate
        {
            get => _TransportationDate;
            set => _TransportationDate = value;
        }

        // Свойство для маршрута (шифра маршрута)
        public Route RouteCode
        {
            get => _RouteCode;
            set => _RouteCode = value;
        }

        // Свойство для водителя (табельный номер водителя)
        public Driver DriverCode
        {
            get => _DriverCode;
            set => _DriverCode = value;
        }

        // Свойство для автобуса
        public Bus Bus
        {
            get => _Bus;
            set => _Bus = value;
        }

        // Свойство для проданных билетов с валидацией
        public Tickets SoldTickets
        {
            get => _SoldTickets;
            set
            {
                // Проверяем, что количество проданных билетов корректно
                if (value.SoldTickets < 0)
                    throw new ArgumentOutOfRangeException(nameof(value.SoldTickets),
                        "Количество проданных билетов не может быть отрицательным");

                // Проверяем, что не продано больше билетов, чем мест в автобусе
                if (Bus != null && value.SoldTickets > Bus.Capacity)
                    throw new ArgumentOutOfRangeException(nameof(value.SoldTickets),
                        $"Количество проданных билетов ({value.SoldTickets}) " +
                        $"не может превышать вместимость автобуса ({Bus.Capacity} мест)");

                _SoldTickets = value;
            }
        }

        // Свойство для общей выручки с валидацией
        public int TotalRevenue
        {
            get => _TotalRevenue;
            set
            {
                // Проверяем, что значение выручки соответствует расчетному
                if (SoldTickets != null && value == SoldTickets.SoldTickets * SoldTickets.TicketCost)
                    _TotalRevenue = value;
                else
                {
                    // Если выручка не соответствует, выбрасываем исключение с пояснением
                    int calculatedRevenue = SoldTickets?.SoldTickets * SoldTickets?.TicketCost ?? 0;
                    throw new ArgumentException(
                        $"Неверное значение выручки. Ожидается: {calculatedRevenue} " +
                        $"(билетов: {SoldTickets?.SoldTickets}, цена: {SoldTickets?.TicketCost}). " +
                        $"Получено: {value}");
                }
            }
        }

        // Конструктор класса CompletedTransportation
        public CompletedTransportation(Route route, Driver driver, Bus bus,
                                     DateTime transportationDate, Tickets soldTickets)
        {
            // Проверяем обязательные параметры на null
            RouteCode = route ?? throw new ArgumentException(
                "Добавьте маршрут, по которому был выполнен рейс", nameof(route));

            DriverCode = driver ?? throw new ArgumentException(
                "Добавьте водителя, который завершил рейс", nameof(driver));

            Bus = bus ?? throw new ArgumentException(
                "Добавьте автобус, на котором был завершен рейс", nameof(bus));

            TransportationDate = transportationDate;
            SoldTickets = soldTickets ?? throw new ArgumentException(
                "Добавьте информацию о проданных билетах", nameof(soldTickets));

            // Автоматически рассчитываем общую выручку
            TotalRevenue = soldTickets.SoldTickets * soldTickets.TicketCost;
        }

        // Переопределение метода ToString для вывода информации о выполненном рейсе
        public override string ToString() =>
    $"Выполненный рейс" +
    $"\n|--Шифр маршрута: {RouteCode.Code}" +
    $"\n|--Табельный номер водителя: {DriverCode.Id}" +
    $"\n|--Автобус, которым был выполнен рейс: {Bus.StateNumber}" +
    $"\n|--Дата рейса: {TransportationDate:dd.MM.yyyy}" +
    $"\n|--Продано билетов: {SoldTickets.SoldTickets}" +
    $"\n|--Цена билета: {SoldTickets.TicketCost}" +
    $"\n|--Общая выручка за рейс: {TotalRevenue}";
    }
}