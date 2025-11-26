namespace CourseWork_3sem
{
    public class DriverStaff
    {
        public List<Driver> Drivers;
        public DriverStaff(List<Driver> drivers) => Drivers = drivers;
        public DriverStaff() => Drivers = [];

        public void Add()
        {
            try
            {
                int id, workExperience;
                FullName name;
                Class driverClass;
                DateTime dateOfBirth;
                Category category;
                while (true)
                {
                    try
                    {
                        name = AddFullName();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе ФИО: {ex.Message}\n"); }
                }

                while (true)
                {
                    try
                    {
                        id = AddId();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе табельного номера: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        dateOfBirth = AddDateOfBirth();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе даты рождения: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        workExperience = AddWorkExperience(dateOfBirth); 
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе опыта работы: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        driverClass = AddDriverClass(workExperience);
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе классности водителя: {ex.Message}\n"); }
                }
                while (true)
                {
                    try
                    {
                        category = AddCategory();
                        break;
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка при вводе категории водителя: {ex.Message}\n"); }
                }
                   
                Driver driver = new(name, id, dateOfBirth, workExperience, category, driverClass);

                Drivers.Add(driver);
                Console.WriteLine("Водитель успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении водителя: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static Category AddCategory()
        {
            Category category;
            Console.WriteLine("Введите категорию прав водителя(D - автобусы,E - автобусы с прицепом)");
            if (!char.TryParse(Console.ReadLine(), out char inputedCategory)) throw new ArgumentException("Ввели неверное значение");
            if (!Enum.TryParse<Category>(inputedCategory.ToString(), out category) || !Enum.IsDefined(typeof(Category), category)) throw new ArgumentException("Введена неверная категория водителя", nameof(category));
            Driver.IsValidCategory(category);
            return category;
        }

        private static Class AddDriverClass(int workExperience)
        {
            Class driverClass;
            Console.WriteLine("Ввод классности водителя");
            driverClass = Driver.WhichClass(workExperience);
            Driver.IsValidClass(driverClass);
            return driverClass;
        }

        private static int AddWorkExperience(DateTime dateOfBirth)
        {
            int workExperience;
            Console.Write("Введите опыт работы: ");
            if (!int.TryParse(Console.ReadLine(), out workExperience)) throw new ArgumentException();
            Driver.IsValidWorkExperience(workExperience, dateOfBirth);
            return workExperience;
        }

        private static DateTime AddDateOfBirth()
        {
            DateTime dateOfBirth;
            Console.Write("Введите дату рождения (формат гггг.мм.дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                throw new Exception("Некорректная дата рождения");
            Driver.IsValidDateOfBirth(dateOfBirth);
            return dateOfBirth;
        }

        private int AddId()
        {
            int id;
            Console.Write("Введите табельный номер(Например 123): ");
            if (!int.TryParse(Console.ReadLine(), out id)) throw new ArgumentException();
            Driver.IsValidId(id);
            if (Drivers != null)
            {
                foreach (var drivers in Drivers)
                    if (drivers.Id == id) throw new Exception("Водитель с таким табельным номером уже есть, введите другой");
            }

            return id;
        }

        private static FullName AddFullName()
        {
            FullName name;
            Console.Write("Введите ФИО водителя(Например Оюн Айдаш Алексеевич): ");
            string[] NameInputs = (Console.ReadLine() ?? "").Split(' ');
            if (NameInputs.Length > 3) throw new ArgumentException();
            name = new(NameInputs[0], NameInputs[1], NameInputs[2]);
            Driver.IsValidName(name);
            return name;
        }

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
        public void Edit()
        {
            if (Drivers.Count == 0)
            {
                Console.WriteLine("Нет водителей для изменения");
            }
            else
            {
                Print();
                Console.Write("Введите номер водителя для изменения: ");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Drivers.Count)
                {
                    Console.Clear();
                    Console.WriteLine($"Водитель {Drivers[index - 1].Id} выбран");
                    Console.WriteLine(Drivers[index - 1].ToEdit());
                    Console.Write("Введите номер характеристики водителя для изменения: ");
                    if (int.TryParse(Console.ReadLine(), out int ind) && ind > 0 && ind <= 6)
                    {
                        switch (ind)
                        {
                            case 1:
                                { Drivers[index - 1].Name = AddFullName(); break; }
                            case 2:
                                { Drivers[index - 1].Id = AddId(); break; }
                            case 3:
                                { Drivers[index - 1].DateOfBirth = AddDateOfBirth(); break; }
                            case 4:
                                { Drivers[index - 1].WorkExperience = AddWorkExperience(Drivers[index - 1].DateOfBirth); break; }
                                case 5:
                                { Drivers[index - 1].Category = AddCategory(); break; }
                                case 6:
                                { Drivers[index - 1].Class = AddDriverClass(Drivers[index - 1].WorkExperience); break; }
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
            if (Drivers.Count == 0)
            {
                Console.WriteLine("Нет водителей для удаления");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }

            Print();
            Console.Write("Введите номер водителя для удаления: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Drivers.Count)
            {
                var driver = Drivers[index - 1];
                if (volumeOfTransportation.CompletedTransportations != null)
                {
                    List<CompletedTransportation> toDelete = [];
                    foreach (var toDel in volumeOfTransportation.CompletedTransportations)
                    {
                        if (Bus.Equals(toDel.DriverCode, driver))
                        {
                            Console.WriteLine($"Рейс {toDel.TransportationDate} удален");
                            toDelete.Add(toDel);
                        }
                    }
                    foreach (var del in toDelete) volumeOfTransportation.CompletedTransportations.Remove(del);
                }
                Drivers.RemoveAt(index - 1);
                Console.WriteLine($"Водитель {driver.Id} удален");
            }
            else
            {
                Console.WriteLine("Некорректный номер водителя");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
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

        public void Print()
        {
            if (Drivers.Count == 0)
            {
                Console.WriteLine("Водителей нет");
            }
            else
            {
                Console.WriteLine("Список водителей");
                for (int i = 0; i < Drivers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{Drivers[i]}");
                }
                Console.WriteLine($"Всего водителей: {Drivers.Count}");
            }
        }
    }
}