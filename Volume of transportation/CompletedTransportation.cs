using System.Xml.Serialization;

namespace CourseWork_3sem
{
    public class CompletedTransportation
    {
        private Route _RouteCode;  //                Шифр маршрута
        private Driver _DriverCode; //               Табельный номер водителя
        private Bus _Bus; //                         Автобус на котторым был выполнен рейс
        private DateTime _TransportationDate; //     Дата рейса
        private Tickets _SoldTickets; //             Продано билетов
        private int _TotalRevenue; //                Общая выручка за рейс


        public DateTime TransportationDate
        {
            get => _TransportationDate;
            set => _TransportationDate = value;
        }
        public Route RouteCode
        {
            get => _RouteCode;
            set => _RouteCode = value;
        }
        public Driver DriverCode
        {
            get => _DriverCode;
            set => _DriverCode = value;
        }
        public Bus Bus
        {
            get => _Bus;
            set => _Bus = value;
        }
        public Tickets SoldTickets
        {
            get => _SoldTickets;
            set
            {
                if (value.SoldTickets < 0 || value.SoldTickets > Bus.Capacity)
                    throw new ArgumentOutOfRangeException("Количество проданных билетов " +
                        "должно быть не меньше нуля и меньше чем мест в автобусе", nameof(value.SoldTickets));
                _SoldTickets = value;
            }
        }
        public int TotalRevenue
        {
            get => _TotalRevenue;
            set
            {
                if (value == SoldTickets.SoldTickets * SoldTickets.TicketCost)
                    _TotalRevenue = value;
            }
        }

        public CompletedTransportation(Route route, Driver driver, Bus bus, DateTime transportationDate, Tickets soldTickets)
        {
            RouteCode = route ?? throw new ArgumentException("Добавьте маршрут по которому был выполнен рейс", nameof(route));
            DriverCode = driver ?? throw new ArgumentException("Добавьте водителя который завершил рейс", nameof(driver));
            Bus = bus ?? throw new ArgumentException("Добавьте Автобус на котором был завершен рейс", nameof(driver));
            TransportationDate = transportationDate;
            SoldTickets = soldTickets;
            TotalRevenue = soldTickets.SoldTickets * soldTickets.TicketCost;
        }

        public override string ToString() =>
    $"Рейс" +
    $"\n|--Шифр маршрута: {RouteCode.Code}" +
    $"\n|--Табельный номер водителя: {DriverCode.Id}" +
    $"\n|--Автобус на которым был выполнен рейс: {Bus.StateNumber}" +
    $"\n|--Дата рейса: {TransportationDate.Date}" +
    $"\n|--Продано билетов: {SoldTickets.SoldTickets}" +
    $"\n|--Цена билета: {SoldTickets.TicketCost}" +
    $"\n|--Общая выручка за рейс: {TotalRevenue}";
    }
}