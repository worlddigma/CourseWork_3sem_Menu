using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    public class WriteFile : FileDataManager
    {
        public static void Write(BusFleet busFleet, DriverStaff driverStaff,
            RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            EnsureDataDirectoryExists();
            string BusFleetPath = GetFilePath(FilePaths["BusFleet"]);
            string DriverStaffPath = GetFilePath(FilePaths["DriverStaff"]);
            string RouteCollectionPath = GetFilePath(FilePaths["RouteCollection"]);
            string VolumeOfTransportationPath = GetFilePath(FilePaths["Transportation"]);

            using (StreamWriter BusFleetWrite = new(BusFleetPath, false))
            {
                if (busFleet.Buses != null)
                {
                    foreach (var buses in busFleet.Buses)
                    {
                        BusFleetWrite.WriteLine($"{buses.StateNumber};{buses.Brand};{buses.Model};{buses.Capacity};{buses.Year};{buses.YearOfMajorRepair};" +
                            $"{buses.Mileage};{buses.Photo}");
                    }
                }
            }
            using (StreamWriter DriverStaffWrite = new(DriverStaffPath, false))
            {
                if (driverStaff.Drivers != null)
                {
                    foreach (var drivers in driverStaff.Drivers)
                    {
                        DriverStaffWrite.WriteLine($"{drivers.Name};{drivers.Id};{drivers.DateOfBirth.ToString("dd-MM-yyyy")}" +
                            $";{drivers.WorkExperience};" +
                            $"{drivers.Category};{drivers.Class}");
                    }
                }
            }
            using (StreamWriter RouteCollectionWrite = new(RouteCollectionPath, false))
            {
                if (routeCollection.Routes != null)
                {
                    foreach (var route in routeCollection.Routes)
                    {
                        RouteCollectionWrite.WriteLine($"{route.Code};{route.StartingPoint};{route.EndingPoint};{string.Join(',',route.IntermediatePoints)};" +
                            $"{route.DepartureTime.TimeOfDay};{string.Join(',',route.DepartureDays)};{route.TransportationTime}");
                    }
                }
            }
            using (StreamWriter  VolumeOfTransportationWrite = new(VolumeOfTransportationPath, false))
            {
                if (volumeOfTransportation.CompletedTransportations != null)
                {
                    foreach (var completedTransportation in  volumeOfTransportation.CompletedTransportations)
                    {
                        VolumeOfTransportationWrite.WriteLine($"{completedTransportation.RouteCode};{completedTransportation.DriverCode};" +
                            $"{completedTransportation.Bus};{completedTransportation.TransportationDate.Date};{completedTransportation.SoldTickets};" +
                            $"{completedTransportation.TotalRevenue}");
                    }
                }
            }
        }
    }
}
