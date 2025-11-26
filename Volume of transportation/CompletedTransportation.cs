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
        { get => _RouteCode;
            set => _RouteCode = value;
        }
        public Driver DriverCode
        {  get => _DriverCode;
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
            { if (value.SoldTickets < 0 || value.SoldTickets > Bus.Capacity)
                    throw new ArgumentOutOfRangeException("Количество проданных билетов " +
                        "должно быть не меньше нуля и меньше чем мест в автобусе", nameof(value.SoldTickets));
                _SoldTickets = value; }
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

        public static Route AddRoute(RouteCollection routeCollection, DateTime transportationDate)
        {
            ArgumentNullException.ThrowIfNull(routeCollection);
            if (routeCollection.Routes.Count == 0) throw new ArgumentException("Маршрутов для добавления рейса нет",nameof(routeCollection));
            var toChoose = routeCollection.DeepCopy();

            List<Route> toDelete = [];
            foreach (var route in toChoose.Routes)
            {
                if(!route.DepartureDays.Contains(transportationDate.DayOfWeek)) toDelete.Add(route); // Проверка совпадения дня недели рейса и дней когда выполняется маршрут
            }
            foreach (var del in toDelete) toChoose.Routes.Remove(del);
            while (true)
            {
                toChoose.Print();
                Console.WriteLine("Выберите номер маршрута доступного для выбранного вами времени");
                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toChoose.Routes.Count)
                {
                    var route = toChoose.Routes[index - 1];
                    Console.WriteLine($"Маршрут {route.Code} выбран");
                    return route;
                }
                else Console.WriteLine("Некорректный номер маршрута");
            }
        }

        public static Driver AddDriver(DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation, DateTime transportationDate)
        {
            ArgumentNullException.ThrowIfNull(driverStaff);
            if (driverStaff.Drivers.Count == 0) throw new ArgumentException("Водителей для добавления рейса нет", nameof(driverStaff));
            var toChoose = driverStaff.DeepCopy();
            List<Driver> toDelete = [];
            foreach(var dateOfBirth in toChoose.Drivers) 
            {
                if (transportationDate.Year - dateOfBirth.DateOfBirth.Year < 18)
                    toDelete.Add(dateOfBirth);
            }
            foreach(var del in toDelete) toChoose.Drivers.Remove(del);

            
            if (volumeOfTransportation.CompletedTransportations == null) //    Если выполненных рейсов нет, выбор любого водителя 
            {
                while (true) 
                {
                    toChoose.Print();
                    Console.WriteLine("Выберите номер водителя доступного для выбранного вами времени");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toChoose.Drivers.Count)
                    {
                        var driver = toChoose.Drivers[index - 1];
                        Console.WriteLine($"Водитель {driver.Id} выбран");
                        return driver;
                    }
                    else Console.WriteLine("Некорректный номер водителя");
                }
            }
            else
            {
                foreach (var driver in volumeOfTransportation.CompletedTransportations) //     Удаление выполняющих в это время рейс водителей
                {     
                    //      Проверка дата выполненного рейса < выбранное время рейса < дата окончания выполненного рейса
                    if (driver.TransportationDate <= transportationDate && driver.TransportationDate + driver._RouteCode.TransportationTime <= transportationDate)
                        toChoose.Drivers.Remove(driver.DriverCode);
                }
                while (true)
                {
                    if (toChoose.Drivers.Count == 0) throw new Exception("Нет водителей на это время.");
                    toChoose.Print();
                    Console.WriteLine("Выберите номер водителя доступного для выбранного вами времени");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toChoose.Drivers.Count)
                    {
                        var driver = toChoose.Drivers[index - 1];
                        Console.WriteLine($"Водитель {driver.Id} выбран");
                        return driver;
                    }
                    else Console.WriteLine("Некорректный номер водителя");
                }
            }
        }
        public static Bus AddBus(BusFleet busFleet, VolumeOfTransportation volumeOfTransportation, DateTime transportationDate)
        {
            ArgumentNullException.ThrowIfNull(busFleet);
            if (busFleet.Buses.Count == 0) throw new ArgumentException("Автобусов для добавления рейса нет", nameof(busFleet));
            var toChoose = busFleet.DeepCopy();
            List<Bus> toDelete = [];
            foreach (var year in toChoose.Buses)
            {
                if (transportationDate.Year - year.Year < 0) // Проверка выпуска автобуса и года выполнения рейса
                    toDelete.Add(year);
            }
            foreach(var del in toDelete) toChoose.Buses.Remove(del);
            if (volumeOfTransportation.CompletedTransportations == null) //    Если выполненных рейсов нет, выбор любого водителя 
            {
                while (true)
                {
                    toChoose.Print();
                    Console.WriteLine("Выберите номер автобуса доступного для выбранного вами времени");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toChoose.Buses.Count)
                    {
                        var driver = toChoose.Buses[index - 1];
                        Console.WriteLine($"Автобус {driver.StateNumber} выбран");
                        return driver;
                    }
                    else Console.WriteLine("Некорректный номер автобуса");
                }
            }
            else
            {
                foreach (var completedTransportation in volumeOfTransportation.CompletedTransportations)
                {
                    // Проверка пересечения по времени
                    DateTime existingStart = completedTransportation.TransportationDate;
                    DateTime existingEnd = existingStart + completedTransportation._RouteCode.TransportationTime;
                    DateTime newStart = transportationDate;
                    DateTime newEnd = newStart + transportationDate.TimeOfDay; // нужно знать время нового маршрута

                    bool timeConflict = newStart < existingEnd && newEnd > existingStart;

                    if (timeConflict)
                        toChoose.Buses.Remove(completedTransportation.Bus);
                }
                while (true)
                {
                    if (toChoose.Buses.Count == 0) throw new Exception("Нет автобусов на это время.");
                    toChoose.Print();
                    Console.WriteLine("Выберите номер автобуса доступного для выбранного вами времени");
                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= toChoose.Buses.Count)
                    {
                        var bus = toChoose.Buses[index - 1];
                        Console.WriteLine($"Автобус  {bus.StateNumber} выбран");
                        return bus;
                    }
                    else Console.WriteLine("Некорректный номер автобуса");
                }
            }
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

       public string ToEdit() =>
    $"Рейс" +
    $"\n1. Шифр маршрута: {RouteCode.Code}" +
    $"\n2. Табельный номер водителя: {DriverCode.Id}" +
    $"\n3. Автобус на которым был выполнен рейс: {Bus.StateNumber}" +
    $"\n4. Дата рейса: {TransportationDate}" +
    $"\n5. Продано билетов: {SoldTickets.SoldTickets}" +
    $"\n6. Цена билета: {SoldTickets.TicketCost}";
    }
}