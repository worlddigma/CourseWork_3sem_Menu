namespace CourseWork_3sem
{

    partial class Program
    {
        public class AllMenu
        {
            public static int Menu()
            {
                bool key = false;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(" Система учета пассажирских перевозок");
                    Console.WriteLine("1. Настройка данных(парк автобусов, маршруты, штат водителей)");
                    Console.WriteLine("2. Показать парк автобусов");
                    Console.WriteLine("3. Показать маршруты");
                    Console.WriteLine("4. Показать штат водителей");
                    Console.WriteLine("5. Показать объем перевозок");
                    Console.WriteLine("6. Добавить рейс");
                    Console.WriteLine("7. Изменить рейс");
                    Console.WriteLine("8. Удалить рейс");
                    Console.WriteLine("9. Выход");

                    if (key == true) Console.WriteLine("\nОшибка: введите число от 1 до 9");
                    Console.Write("\nВыберите что хотите сделать: ");

                    string choice = Console.ReadLine();
                    if (!int.TryParse(choice, out int x)) { key = true; continue; }
                    if (x < 0 || x > 9) { key = true; continue; }

                    return x;
                }
            }
            public static int MenuSettings()
            {
                bool key = false;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(" Настройка системы учета пассажирских перевозок");
                    Console.WriteLine("1. Управление парком автобусов ");
                    Console.WriteLine("2. Управление маршрутами");
                    Console.WriteLine("3. Управление штатом водителей");
                    Console.WriteLine("4. Вернуться назад");
                    if (key == true) Console.WriteLine("\nОшибка: введите число от 1 до 4");
                    Console.Write("\nВыберите что хотите сделать: ");

                    string choice = Console.ReadLine();
                    if (!int.TryParse(choice, out int x)) { key = true; continue; }
                    if (x < 0 || x > 4) { key = true; continue; }

                    return x;
                }
            }
            public static bool BusSettings(BusFleet busFleet, VolumeOfTransportation volumeOfTransportation)
            {
                while (true)
                {
                    int choise;
                    bool key = false;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(" Управление парком автобусов");
                        Console.WriteLine("1. Добавить автобус ");
                        Console.WriteLine("2. Изменить автобус");
                        Console.WriteLine("3. Удалить автобус");
                        Console.WriteLine("4. Посмотреть парк автобусов");
                        Console.WriteLine("5. Вернуться назад");
                        Console.WriteLine("6. Вернуться на главную");
                        if (key == true) Console.WriteLine("\nОшибка: введите число от 1 до 6");
                        Console.Write("\nВыберите что хотите сделать: ");

                        string choice = Console.ReadLine();
                        if (!int.TryParse(choice, out choise)) { key = true; continue; }
                        if (choise < 0 || choise > 6) { key = true; continue; }

                        break;
                    }
                    switch (choise)
                    {
                        case 1:
                            Console.Clear();
                            busFleet.Add();
                            break;
                        case 2:
                            Console.Clear();
                            busFleet.Edit();
                            break;
                        case 3:
                            Console.Clear();
                            busFleet.Delete(volumeOfTransportation);
                            break;
                        case 4:
                            Console.Clear();
                            busFleet.Print();

                            Console.WriteLine("Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                            
                        case 5:
                            return false;
                        case 6:
                            return true;
                    }
                }
            }
            public static bool RouteSettings(RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
            {
                while (true)
                {
                    int choise;
                    bool key = false;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(" Управление маршрутами");
                        Console.WriteLine("1. Добавить маршрут ");
                        Console.WriteLine("2. Изменить маршрут ");
                        Console.WriteLine("3. Удалить маршрут");
                        Console.WriteLine("4. Посмотреть маршруты");
                        Console.WriteLine("5. Вернуться назад");
                        Console.WriteLine("6. Вернуться на главную");
                        if (key == true) Console.WriteLine("\nОшибка: введите число от 1 до 6");
                        Console.Write("\nВыберите что хотите сделать: ");

                        string choice = Console.ReadLine();
                        if (!int.TryParse(choice, out choise)) { key = true; continue; }
                        if (choise < 0 || choise > 6) { key = true; continue; }

                        break;
                    }
                    switch (choise)
                    {
                        case 1:
                            Console.Clear();
                            routeCollection.Add();
                            break;
                        case 2:
                            Console.Clear();
                            routeCollection.Edit();
                            break;
                        case 3:
                            Console.Clear();
                            routeCollection.Delete(volumeOfTransportation);
                            break;
                        case 4:
                            Console.Clear();
                            routeCollection.Print();

                            Console.WriteLine("Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        case 5:
                            return false;
                        case 6:
                            return true;
                    }
                }
            }

            public static bool DriversSettings(DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
            {
                while (true)
                {
                    int choise;
                    bool key = false;
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(" Управление штатом водителей");
                        Console.WriteLine("1. Добавить водителя");
                        Console.WriteLine("2. Изменить водителя");
                        Console.WriteLine("3. Удалить водителя");
                        Console.WriteLine("4. Посмотреть водителей");
                        Console.WriteLine("5. Вернуться назад");
                        Console.WriteLine("6. Вернуться на главную");
                        if (key == true) Console.WriteLine("\nОшибка: введите число от 1 до 6");
                        Console.Write("\nВыберите что хотите сделать: ");

                        string choice = Console.ReadLine();
                        if (!int.TryParse(choice, out choise)) { key = true; continue; }
                        if (choise < 0 || choise > 6) { key = true; continue; }

                        break;
                    }
                    switch (choise)
                    {
                        case 1:
                            Console.Clear();
                            driverStaff.Add();
                            break;
                        case 2:
                            Console.Clear();
                            driverStaff.Edit();
                            break;
                        case 3:
                            Console.Clear();
                            driverStaff.Delete(volumeOfTransportation);
                            break;
                        case 4:
                            Console.Clear();
                            driverStaff.Print();


                            Console.WriteLine("Нажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        case 5:
                            return false;
                        case 6:
                            return true;
                    }
                }
            }
        }
    }
}