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

        public VolumeOfTransportation()
        {
            CompletedTransportations = [];
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
    }

}