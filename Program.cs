using CourseWork_3sem;

namespace CourseWork_3sem_Menu
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BusFleet busFleet = new();
            DriverStaff driverStaff = new();
            VolumeOfTransportation volumeOfTransportation = new();
            RouteCollection routeCollection = new();
            ReadFile.Read(busFleet, driverStaff, routeCollection, volumeOfTransportation);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new StartingMenu(busFleet, routeCollection, driverStaff, volumeOfTransportation));
            WriteFile.Write(busFleet,driverStaff,routeCollection,volumeOfTransportation);
        }
    }
}