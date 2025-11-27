namespace CourseWork_3sem
{
    public class DriverStaff
    {
        public List<Driver> Drivers;
        public DriverStaff(List<Driver> drivers) => Drivers = drivers;
        public DriverStaff() => Drivers = [];

      
        public void Add(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {

                int id, workExperience;
                FullName name;
                Class driverClass;
                DateTime dateOfBirth;
                Category category;
                string[] strings = value.Split(';');
                try 
                {
                    string[] NameInputs = (strings[0]).Split(' ');
                    if (NameInputs.Length > 3) throw new ArgumentException();
                    name = new(NameInputs[0], NameInputs[1], NameInputs[2]);

                    if (!int.TryParse(strings[1], out id)) throw new ArgumentException();
                    if (!DateTime.TryParse(strings[2], out dateOfBirth)) throw new ArgumentException();
                    if (!int.TryParse(strings[3], out workExperience)) throw new ArgumentException();
                    if (!Enum.TryParse(strings[4], out category)) throw new ArgumentException();
                    if (!Enum.TryParse(strings[5], out driverClass)) throw new ArgumentException();

                    Driver driver = new(name, id, dateOfBirth, workExperience, category, driverClass);

                    Drivers.Add(driver);

                }
                catch (Exception ex) { }
            }
        }
        
        public DriverStaff DeepCopy()
        {
            DriverStaff other = new();
            foreach (var driver in Drivers)
            {
                other.Drivers.Add(driver);
            }
            return other;
        }
    }
}