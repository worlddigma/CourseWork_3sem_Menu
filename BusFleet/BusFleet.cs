namespace CourseWork_3sem
{
    public class BusFleet
    {
        public List<Bus> Buses;

        public BusFleet() => Buses = [];

        public bool TryAddFromString(string input)
        {

            try
            {
                var bus = BusParser.ParseFromString(input);
                Buses.Add(bus);
                return true;
            }
            catch (FormatException ex)
            {
                return false;
            }
            catch (ArgumentException ex)
            {
                return false;
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