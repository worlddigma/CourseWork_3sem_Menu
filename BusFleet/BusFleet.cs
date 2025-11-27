namespace CourseWork_3sem
{
    public class BusFleet
    {
        public List<Bus> Buses;

        public BusFleet() => Buses = [];

        public void Add(string value)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string stateNumber, brand, model, photo;
                    int capacity, year, yearOfMajorRepair, mileage;

                    string[] strings = value.Split([';']);

                    stateNumber = strings[0];
                    brand = strings[1];
                    model = strings[2];
                    if (!int.TryParse(strings[3], out capacity)) throw new ArgumentException();
                    year = int.Parse(strings[4]);
                    yearOfMajorRepair = int.Parse(strings[5]);
                    mileage = int.Parse(strings[6]);
                    photo = strings[7];

                    Bus bus = new(stateNumber, brand, model, capacity, year, yearOfMajorRepair, mileage, photo);
                    Buses.Add(bus);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении автобуса: {ex.Message}");
            }
        }

        public BusFleet DeepCopy()
        {
            BusFleet other = new();
            foreach (var bus in Buses)
            {
                other.Buses.Add(bus);
            }
            return other;
        }

    }
}