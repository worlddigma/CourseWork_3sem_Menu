namespace CourseWork_3sem
{
    public class BusParser
    {
        public static Bus ParseFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Входная строка не может быть пустой");

            string[] parts = input.Split(';');

            if (parts.Length != 8)
                throw new FormatException(
                    $"Ожидается 8 значений через ';'. Получено: {parts.Length}");

            return new Bus(
                stateNumber: parts[0].Trim(),
                brand: parts[1].Trim(),
                model: parts[2].Trim(),
                capacity: ParseInt(parts[3], "вместимость"),
                year: ParseInt(parts[4], "год выпуска"),
                yearOfMajorRepair: ParseInt(parts[5], "год кап. ремонта"),
                mileage: ParseInt(parts[6], "пробег"),
                photo: parts[7].Trim()
            );
        }

        private static int ParseInt(string value, string fieldName)
        {
            if (!int.TryParse(value, out int result))
                throw new FormatException(
                    $"Неверный формат {fieldName}: '{value}'");
            return result;
        }
    }
}