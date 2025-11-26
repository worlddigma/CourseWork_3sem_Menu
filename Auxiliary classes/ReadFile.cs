using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    public static class ReadFile
    {
        public static void Read(BusFleet busFleet, DriverStaff driverStaff,
            RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            try
            {
                string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Passenger travel system");

                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                string BusFleetPath = Path.Combine(directory, "Bus fleet.txt");
                string DriverStaffPath = Path.Combine(directory, "Driver staff.txt");
                string RouteCollectionPath = Path.Combine(directory, "Route collection.txt");
                string VolumeOfTransportationPath = Path.Combine(directory, "Volume of transportation.txt");

                if (!File.Exists(BusFleetPath)) File.Create(BusFleetPath);
                if (!File.Exists(DriverStaffPath)) File.Create(DriverStaffPath);
                if (!File.Exists(RouteCollectionPath)) File.Create(RouteCollectionPath);
                if (!File.Exists(VolumeOfTransportationPath)) File.Create(VolumeOfTransportationPath);

                using (StreamReader BusFleetRead = new(BusFleetPath))
                {
                    string line;
                    while ((line = BusFleetRead.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            busFleet.Add(line);
                        }
                    }
                }
                using (StreamReader DriverStaff = new(DriverStaffPath))
                {
                    string line;
                    while ((line = DriverStaff.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            driverStaff.Add(line);
                        }

                    }
                }

                using (StreamReader RouteCollection = new(RouteCollectionPath))
                {
                    string line;
                    while ((line = RouteCollection.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            routeCollection.Add(line);
                        }
                    }
                }
                using (StreamReader VolumeOfTransportation = new(VolumeOfTransportationPath))
                {
                    string text;
                    while ((text = VolumeOfTransportation.ReadToEnd()) != "")
                    {
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            string[] line = text.Split('[');
                            foreach (var one in line)
                            {
                                volumeOfTransportation.Add(one, busFleet, driverStaff, routeCollection);
                            }
                        }
                    }
                }


            }
            catch (Exception ex) { }
        }
    }
}
