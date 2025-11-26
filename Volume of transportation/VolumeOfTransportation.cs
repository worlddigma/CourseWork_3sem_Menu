namespace CourseWork_3sem
{
    public class VolumeOfTransportation //      Объем перевозок
    {
        private List<CompletedTransportation> _CompletedTransportations;

        public List<CompletedTransportation> CompletedTransportations
        {
            get { return _CompletedTransportations; }
       set { _CompletedTransportations = value; }
        }


        public VolumeOfTransportation(List<CompletedTransportation> completedTransportations)
        {
            CompletedTransportations = completedTransportations;
        }

        public VolumeOfTransportation()
        {
            CompletedTransportations = [];
        }

        public void Add(RouteCollection routeCollection, BusFleet busFleet, 
            DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            try
            {
                if (routeCollection.Routes.Count == 0) throw new ArgumentException("Нет маршрутов для добавления рейса");
                if (busFleet.Buses.Count == 0) throw new ArgumentException("Нет автобусов для добавления рейса");
                if (driverStaff.Drivers.Count == 0) throw new ArgumentException("Нет водителей для добавления рейса");

                DateTime transportationDate = AddTransportationDate();

                Route route = CompletedTransportation.AddRoute(routeCollection, transportationDate);

                Driver driver = CompletedTransportation.AddDriver(driverStaff, volumeOfTransportation, transportationDate);

                Bus bus = CompletedTransportation.AddBus(busFleet, volumeOfTransportation, transportationDate);

                int ticketCost = AddTicketCost();
                int tickets = AddTickets();

                Tickets soldTickets = new(tickets, ticketCost);

                CompletedTransportation completedTransportation = new(route, driver, bus, transportationDate, soldTickets);

                CompletedTransportations.Add(completedTransportation);
                Console.WriteLine("Рейс успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении рейса: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static DateTime AddTransportationDate()
        {
            Console.Write("Введите дату рейса (формат гггг.мм.дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime transportationDate))
                throw new Exception("Некорректная дата рейса");
            return transportationDate;
        }

        private static int AddTickets()
        {
            Console.Write("Введите количество проданнных билетов: ");
            if (!int.TryParse(Console.ReadLine(), out int tickets)) throw new ArgumentException($"Введено неверное значение {tickets}, введите число");
            return tickets;
        }

        private static int AddTicketCost()
        {
            Console.Write("Введите цену одного билета: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketCost)) throw new ArgumentException($"Введено неверное значение {ticketCost}, введите число");
            return ticketCost;
        }

        public void Add(string value, BusFleet busFleet, DriverStaff driverStaff,
            RouteCollection routeCollection)
        {
            if (routeCollection.Routes.Count == 0) return;
            if (busFleet.Buses.Count == 0) return;
            if (driverStaff.Drivers.Count == 0) return;

            Route route = null;
            Bus bus = null;
            Driver driver = null;

            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] strings = value.Split(';');
                try
                {
                    foreach (var routes in routeCollection.Routes)
                    {
                        if (string.Equals(strings[0], routes.ToString()))
                        {
                            route = routes;
                            break;
                        }
                    }
                    foreach (var drivers in driverStaff.Drivers)
                    {
                        if (string.Equals(strings[1], drivers.ToString()))
                        {
                            driver = drivers;
                            break;
                        }
                    }
                    foreach (var buses in busFleet.Buses)
                    {
                        if (string.Equals(strings[2], buses.ToString()))
                        {
                            bus = buses;
                            break;
                        }
                    }
                    if (!DateTime.TryParse(strings[3], out DateTime transportationDate)) throw new ArgumentException();

                    string[] ticket = strings[4].Split(" ");
                    Tickets tickets = new(int.Parse(ticket[0]), int.Parse(ticket[1]));

                    CompletedTransportation completedtransportation = new(route, driver, bus, transportationDate, tickets);

                    CompletedTransportations.Add(completedtransportation);

                }
                catch (Exception ex)
                { }
            }
        }

        public void Edit(RouteCollection routeCollection, BusFleet busFleet,
            DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            ArgumentNullException.ThrowIfNull(CompletedTransportations);
            if (CompletedTransportations.Count == 0)
            {
                Console.WriteLine("Нет рейсов для удаления");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }

            Print();
            Console.Write("Введите номер рейса для изменения: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= CompletedTransportations.Count)
            {
                Console.Clear();
                Console.WriteLine($"Рейс {CompletedTransportations[index - 1].TransportationDate.Date} выбран");
                Console.WriteLine(CompletedTransportations[index - 1].ToEdit());
                Console.Write("Введите номер характеристики рейса для изменения: ");
                if (int.TryParse(Console.ReadLine(), out int ind) && ind > 0 && ind <= 7)
                {
                    switch (ind)
                    {
                        case 1:
                            { CompletedTransportations[index - 1].RouteCode = CompletedTransportation.AddRoute(routeCollection, CompletedTransportations[index - 1].TransportationDate); break; }
                        case 2:
                            { CompletedTransportations[index - 1].DriverCode = CompletedTransportation.AddDriver(driverStaff, volumeOfTransportation, CompletedTransportations[index - 1].TransportationDate); break; }
                        case 3:
                            { CompletedTransportations[index - 1].Bus = CompletedTransportation.AddBus(busFleet, volumeOfTransportation, CompletedTransportations[index - 1].TransportationDate); break; }
                        case 4:
                            { CompletedTransportations[index - 1].TransportationDate = AddTransportationDate(); break; }
                        case 5:
                            { CompletedTransportations[index - 1].SoldTickets = new Tickets(AddTickets(), CompletedTransportations[index - 1].SoldTickets.TicketCost); break; }
                        case 6:
                            { CompletedTransportations[index - 1].SoldTickets = new Tickets(CompletedTransportations[index - 1].SoldTickets.TicketCost, AddTicketCost()); break; }
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный номер характеристики");
                }
            }
            else
            {
                Console.WriteLine("Некорректный номер рейса");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();

        }
        public void Delete()
        {
            ArgumentNullException.ThrowIfNull(CompletedTransportations);
            if (CompletedTransportations.Count == 0)
            {
                Console.WriteLine("Нет рейсов для удаления");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }

            Print();
            Console.Write("Введите номер рейса для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= CompletedTransportations.Count)
            {
                var completedTransportation = CompletedTransportations[index - 1];
                CompletedTransportations.RemoveAt(index - 1);
                Console.WriteLine($"Рейс с кодом маршрута{completedTransportation.ToString}\n удален");
            }
            else
            {
                Console.WriteLine("Некорректный номер рейса");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        public void Print()
        {
            if (CompletedTransportations.Count == 0)
            {
                Console.WriteLine("Рейсов нет");
            }
            else
            {
                Console.WriteLine("Список рейсов");
                for (int i = 0; i < CompletedTransportations.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{CompletedTransportations[i]}");
                }
                Console.WriteLine($"Всего рейсов: {CompletedTransportations.Count}");
            }
        }
    }

}