namespace CourseWork_3sem
{
    public class BusFleet
    {
        public List<Bus> Buses;

        public BusFleet(List<Bus> buses) => Buses = buses;
        public BusFleet() => Buses = [];

        public void Add()
        {
            
            string stateNumber, brand, model, photo;
            int capacity, year, yearOfMajorRepair,mileage;
            try
            {
                while (true)
                {
                    try
                    {
                        stateNumber = AddStateNumber();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при добавлении гос.номера: {e.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        brand = AddBrand();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе бренда: {e.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        model = AddModel();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе модели: {e.Message} \n"); }
                }
                while (true)
                {
                    try
                    {
                        capacity = AddCapacity();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе вместимости: {e.Message} \n"); }
                }
                while (true)
                {
                    try
                    {
                        year = AddYear();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе года выпуска: {e.Message} \n"); }
                }
                while (true)
                {
                    try
                    {
                        yearOfMajorRepair = AddYearOfMajorRepair(year);
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе года капитального ремонта: {e.Message}  \n"); }
                }
                while (true)
                {
                    try
                    {
                        mileage = AddMileage();
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе пробега: {e.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        photo = "";
                        break;
                    }
                    catch (Exception e) { Console.WriteLine($"Ошибка при вводе фото: {e.Message}\n"); }
                }
                Bus bus = new(stateNumber, brand, model, capacity, year, yearOfMajorRepair, mileage, photo);
                Buses.Add(bus);

                Console.WriteLine("Автобус успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении автобуса: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static int AddMileage()
        {
            int mileage;
            Console.Write("Введите пробег автобуса: ");
            if (!int.TryParse(Console.ReadLine(), out mileage)) throw new Exception();
            Bus.IsValidMileage(mileage);
            return mileage;
        }

        private static int AddYearOfMajorRepair(int year)
        {
            int yearOfMajorRepair;
            Console.Write("Введите год капитального ремонта автобуса: ");
            if (!int.TryParse(Console.ReadLine(), out yearOfMajorRepair)) throw new Exception();
            Bus.IsValidYearOfMajorRepair(yearOfMajorRepair, year);
            return yearOfMajorRepair;
        }

        private static int AddYear()
        {
            int year;
            Console.Write($"Введите год выпуска автобуса({Bus.Constants.MinYear} - {DateTime.Now.Year}): ");
            if (!int.TryParse(Console.ReadLine(), out year)) throw new Exception();
            Bus.IsValidYear(year);
            return year;
        }

        private static int AddCapacity()
        {
            int capacity;
            Console.Write("Введите вместимость автобуса: ");
            if (!int.TryParse(Console.ReadLine(), out capacity)) throw new Exception();
            Bus.IsValidCapacity(capacity);
            return capacity;
        }

        private static string AddModel()
        {
            string model;
            Console.Write("Введите модель автобуса: ");
            model = Console.ReadLine();
            Bus.IsValidModel(model);
            return model;
        }

        private static string AddBrand()
        {
            string brand;
            Console.Write("Введите бренд автобуса: ");
            brand = Console.ReadLine();
            Bus.IsValidBrand(brand);
            return brand;
        }

        private string AddStateNumber()
        {
            string stateNumber;
            Console.Write("Введите государственный номер автобуса(Пример: A111AA001): ");
            stateNumber = Console.ReadLine();
            Bus.IsValidStateNumber(stateNumber);
            stateNumber = stateNumber.ToUpper();
            if (Buses != null)
            {
                foreach (var buses in Buses)
                    if (buses.StateNumber == stateNumber) throw new Exception("Автобус с таким государственным" +
                        " номером уже есть введите другой");
            }

            return stateNumber;
        }

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

                    Bus bus = new(stateNumber, brand, model, capacity, year, yearOfMajorRepair, mileage,photo);
                    Buses.Add(bus);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении автобуса: {ex.Message}");
            }
        }

        public void Edit()
        {
            if (Buses.Count == 0)
            {
                Console.WriteLine("Нет автобусов для изменения");
            }
            else
            {
                Print();
                Console.Write("Введите номер автобуса для изменения: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Buses.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Автобус {Buses[index - 1].StateNumber} выбран");
                    Console.WriteLine(Buses[index - 1].ToEdit());
                    Console.Write("Введите номер характеристики автобуса для изменения: ");
                    if (int.TryParse(Console.ReadLine(), out int ind) && ind > 0 && ind <= 3)
                    {
                        switch (ind)
                        {
                            case 1:
                                { Buses[index - 1].YearOfMajorRepair = AddYearOfMajorRepair(Buses[index - 1].Year); break; }
                            case 2:
                                { Buses[index - 1].Mileage = AddMileage(); break;  }
                            case 3:
                                { break; }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный номер характеристики");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный номер автобуса");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        public void Delete(VolumeOfTransportation volumeOfTransportation)
        {
            if (Buses.Count == 0)
            {
                Console.WriteLine("Нет автобусов для удаления");
            }
            else
            {
                Print();
                Console.Write("Введите номер автобуса для удаления: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Buses.Count)
                {
                    
                    var bus = Buses[index - 1];
                    if (volumeOfTransportation.CompletedTransportations != null)
                    {
                        List<CompletedTransportation> toDelete = [];
                        foreach (var toDel in volumeOfTransportation.CompletedTransportations)
                        {
                            if (Bus.Equals(toDel.Bus, bus))
                            {
                                Console.WriteLine($"Рейс {toDel.TransportationDate} удален");
                                toDelete.Add(toDel);
                            }
                        }
                        foreach (var del in toDelete) volumeOfTransportation.CompletedTransportations.Remove(del);
                    }
                    Buses.RemoveAt(index - 1);
                    Console.WriteLine($"Автобус {bus.StateNumber} удален");
                }
                else
                {
                    Console.WriteLine("Некорректный номер автобуса");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
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

        public void Print()
        {
            if (Buses == null)
            {
                Console.WriteLine("Автобусов нет");
                return;
            }
            else
            {
                int count = 0;
                Console.WriteLine("Список автобусов");
                foreach (var bus in Buses)
                {
                    Console.WriteLine($"\n{count + 1}.{bus}");
                    count++;
                }
                Console.WriteLine($"Всего автобусов: {count}");
            }
        }
    }
}