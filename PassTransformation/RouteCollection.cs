namespace CourseWork_3sem
{
    public class RouteCollection
    {
        public List<Route> Routes { get; set; }

        public RouteCollection() => Routes = [];
        public RouteCollection(List<Route> routes) => Routes = routes;

        public void Add()
            {
            string code, startingPoint, endingPoint;
            List<string> intermediatePoints;
            DateTime departureTime;
            List<DayOfWeek> departureDays;
            TimeSpan transportationTime;
            try
            {
                while (true)
                {
                    try
                    {
                        code = AddCode();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе шифра: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        startingPoint = AddStartingPoint();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе начального пункта: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        endingPoint = AddEndingPoint();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе конечный пункт: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        intermediatePoints = AddIntermediatePoints();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе промежуточных пунктов: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        departureTime = AddDepartureTime();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе времени отправления: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        departureDays = AddDepartureDays();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе дней отправления: {ex.Message}\n"); }
                }

                while (true)
                {
                    try
                    {
                        transportationTime = AddTransportationTime();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе времени в пути: {ex.Message}\n"); }
                }
                    Route route = new(code, startingPoint, endingPoint, intermediatePoints,
                                         departureDays, transportationTime, departureTime);
                    Routes.Add(route);
                    Console.WriteLine("Маршрут успешно добавлен!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении маршрута: {ex.Message}");
                }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            }

        private static TimeSpan AddTransportationTime()
        {
            TimeSpan transportationTime;
            // Время в пути
            Console.Write("Введите время в пути (формат дд:чч:мм): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out transportationTime))
                throw new Exception("Некорректное время в пути");
            Route.IsValidTransportationTime(transportationTime);
            return transportationTime;
        }

        private static List<DayOfWeek> AddDepartureDays()
        {
            // Дни отправления
            List<DayOfWeek> departureDays = [];
            Console.WriteLine("Выберите дни отправления (введите цифры через пробел):");
            Console.WriteLine("1 - воскресенье, 2 - понедельник, 3 - вторник, 4 - среда, 5 - четверг, 6 - пятница, 7 - суббота");

            string[] dayInputs = (Console.ReadLine() ?? "").Split(' ');
            foreach (string dayInput in dayInputs)
            {
                if (int.TryParse(dayInput, out int dayNum) && dayNum >= 1 && dayNum <= 7)
                {
                    departureDays.Add((DayOfWeek)(dayNum - 1));
                }
            }
            Route.IsValidDepartureDays(departureDays);
            return departureDays;
        }

        private static DateTime AddDepartureTime()
        {
            DateTime departureTime;
            // Время отправления
            Console.Write("Введите время отправления (формат часы:минуты(чч:мм)): ");
            if (!DateTime.TryParse(Console.ReadLine(), out departureTime))
                throw new Exception("Некорректное время отправления");
            Route.IsValidDepartureTime(departureTime);
            return departureTime;
        }

        private static List<string> AddIntermediatePoints()
        {
            // Промежуточные пункты
            List<string> intermediatePoints = new List<string>();
            Console.WriteLine("Введите промежуточные пункты (для завершения введите пустую строку):");
            while (true)
            {
                Console.Write("Промежуточный пункт: ");
                string point = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(point)) break;
                intermediatePoints.Add(point);
            }
            Route.IsValidIntermediatePoints(intermediatePoints);
            return intermediatePoints;
        }

        private static string AddEndingPoint()
        {
            string endingPoint;
            Console.Write("Введите конечный пункт: ");
            endingPoint = Console.ReadLine() ?? "";
            Route.IsValidPoint(endingPoint, "конечный пункт");
            return endingPoint;
        }

        private static string AddStartingPoint()
        {
            string startingPoint;
            Console.Write("Введите начальный пункт: ");
            startingPoint = Console.ReadLine() ?? "";
            Route.IsValidPoint(startingPoint, "начальный пункт");
            return startingPoint;
        }

        private string AddCode()
        {
            string code;
            Console.Write("Введите шифр маршрута(Пример: 1abCs): ");
            code = Console.ReadLine() ?? "";
            Route.IsValidCode(code);
            if (Routes != null)
            {
                foreach (var routes in Routes)
                    if (routes.Code == code) throw new Exception("Маршрут с таким шифром уже есть, введите другой");
            }

            return code;
        }

        public void Add(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string code, startingPoint, endingPoint;
                List<string> intermediatePoints = new List<string>();
                DateTime departureTime;
                List<DayOfWeek> departureDays = new List<DayOfWeek>();
                TimeSpan transportationTime;

                string[] strings = value.Split(';');
                try
                {
                    code = strings[0];
                    startingPoint = strings[1];
                    endingPoint = strings[2];

                    string[] points = strings[3].Split(",");
                    foreach (var point in points) intermediatePoints.Add(point);

                    if (!DateTime.TryParse(strings[4], out departureTime)) throw new ArgumentException();

                    string[] days = strings[5].Split(",");
                    foreach (var day in days)
                    {
                        if (!Enum.TryParse<DayOfWeek>(day, out DayOfWeek dayOfWeek)) throw new ArgumentException();
                        departureDays.Add(dayOfWeek);
                    }

                    if (!TimeSpan.TryParse(strings[6], out transportationTime)) throw new ArgumentException();

                    Route route = new(code, startingPoint, endingPoint, intermediatePoints,
                                      departureDays, transportationTime, departureTime);

                    Routes.Add(route);
                }
                catch (Exception ex)
                { }
            }
        }
        
        public void Edit()
        {
            if (Routes.Count == 0)
            {
                Console.WriteLine("Нет маршрутов для изменения");
            }
            else
            {
                Print();
                Console.Write("Введите номер маршрутов для изменения: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Routes.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Маршрут {Routes[index - 1].Code} выбран");
                    Console.WriteLine(Routes[index - 1].ToEdit());
                    Console.Write("Введите номер характеристики маршрутов для изменения: ");
                    if (int.TryParse(Console.ReadLine(), out int ind) && ind > 0 && ind <= 7)
                    {
                        switch (ind)
                        {
                            case 1:
                                { Routes[index - 1].Code = AddCode(); break; }
                            case 2:
                                { Routes[index - 1].StartingPoint = AddStartingPoint(); break; }
                            case 3:
                                { Routes[index - 1].EndingPoint = AddEndingPoint(); break; }
                            case 4:
                                { Routes[index - 1].IntermediatePoints = AddIntermediatePoints(); break; }
                            case 5:
                                { Routes[index - 1].DepartureTime = AddDepartureTime(); break; }
                            case 6:
                                { Routes[index - 1].DepartureDays = AddDepartureDays(); break; }
                            case 7:
                                { Routes[index - 1].TransportationTime = AddTransportationTime(); break; }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный номер характеристики");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный номер маршрута");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        public void Delete(VolumeOfTransportation volumeOfTransportation)
            {
                if (Routes.Count == 0)
                {
                    Console.WriteLine("Нет маршрутов для удаления");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
                }

                Print();
                Console.Write("Введите номер маршрута для удаления: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Routes.Count)
                {
                    var route = Routes[index - 1];
                if (volumeOfTransportation.CompletedTransportations != null)
                {
                    List<CompletedTransportation> toDelete = [];
                    foreach (var toDel in volumeOfTransportation.CompletedTransportations)
                    {
                        if (Bus.Equals(toDel.RouteCode, route))
                        {
                            Console.WriteLine($"Рейс {toDel.TransportationDate} удален");
                            toDelete.Add(toDel);
                        }
                    }
                    foreach (var del in toDelete) volumeOfTransportation.CompletedTransportations.Remove(del);
                }
                Routes.RemoveAt(index - 1);
                    Console.WriteLine($"Маршрут {route.Code} удален");
                }
                else
                {
                    Console.WriteLine("Некорректный номер маршрута");
                }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

            public void Print()
            {
                if (Routes.Count == 0)
                {
                    Console.WriteLine("Маршрутов нет");
                }
                else
                {
                    Console.WriteLine("Список маршрутов");
                    for (int i = 0; i < Routes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.{Routes[i]}");
                    }
                    Console.WriteLine($"Всего маршрутов: {Routes.Count}");
                }
        }
        public RouteCollection DeepCopy()
        {
            RouteCollection other = new RouteCollection();
            foreach (Route route in Routes)
            {
                other.Routes.Add(route);
                //    other.Routes.Add(new Route(route.Code,route.StartingPoint,route.EndingPoint,route.IntermediatePoints,route.DepartureDays,
                //        route.TransportationTime,route.DepartureTime));
            }
            return other;
        }
    }
}