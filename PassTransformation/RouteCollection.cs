namespace CourseWork_3sem
{
    public class RouteCollection
    {
        public List<Route> Routes { get; set; }

        public RouteCollection() => Routes = [];
        
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