using System;
using System.Collections.Generic;

namespace CourseWork_3sem
{
    // Класс для учета объема перевозок (совершенных рейсов)
    public class VolumeOfTransportation
    {
        // Список выполненных перевозок (рейсов)
        private List<CompletedTransportation> _CompletedTransportations;

        // Свойство для доступа к списку выполненных перевозок
        public List<CompletedTransportation> CompletedTransportations
        {
            get { return _CompletedTransportations; }
            set { _CompletedTransportations = value; }
        }

        // Конструктор по умолчанию, создает пустой список выполненных перевозок
        public VolumeOfTransportation()
        {
            CompletedTransportations = [];
        }

        // Метод для добавления выполненной перевозки из строки
        public void Add(string value, BusFleet busFleet, DriverStaff driverStaff,
            RouteCollection routeCollection)
        {
            // Проверяем, что все необходимые коллекции не пустые
            if (routeCollection.Routes.Count == 0)
                throw new InvalidOperationException("Невозможно добавить перевозку: коллекция маршрутов пуста");

            if (busFleet.Buses.Count == 0)
                throw new InvalidOperationException("Невозможно добавить перевозку: автопарк пуст");

            if (driverStaff.Drivers.Count == 0)
                throw new InvalidOperationException("Невозможно добавить перевозку: штат водителей пуст");

            // Объявляем переменные для хранения найденных объектов
            Route route = null;
            Bus bus = null;
            Driver driver = null;

            // Проверяем, что входная строка не пустая
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Разделяем строку на части по точке с запятой
                string[] strings = value.Split(';');

                try
                {
                    // Поиск маршрута по его строковому представлению
                    // Предполагается, что strings[0] содержит строку, возвращаемую методом ToString() маршрута
                    foreach (var routes in routeCollection.Routes)
                    {
                        if (string.Equals(strings[0], routes.ToString()))
                        {
                            route = routes;
                            break;
                        }
                    }

                    // Если маршрут не найден, выбрасываем исключение
                    if (route == null)
                        throw new ArgumentException($"Маршрут не найден: '{strings[0]}'");

                    // Поиск водителя по его строковому представлению
                    // Предполагается, что strings[1] содержит строку, возвращаемую методом ToString() водителя
                    foreach (var drivers in driverStaff.Drivers)
                    {
                        if (string.Equals(strings[1], drivers.ToString()))
                        {
                            driver = drivers;
                            break;
                        }
                    }

                    // Если водитель не найден, выбрасываем исключение
                    if (driver == null)
                        throw new ArgumentException($"Водитель не найден: '{strings[1]}'");

                    // Поиск автобуса по его строковому представлению
                    // Предполагается, что strings[2] содержит строку, возвращаемую методом ToString() автобуса
                    foreach (var buses in busFleet.Buses)
                    {
                        if (string.Equals(strings[2], buses.ToString()))
                        {
                            bus = buses;
                            break;
                        }
                    }

                    // Если автобус не найден, выбрасываем исключение
                    if (bus == null)
                        throw new ArgumentException($"Автобус не найден: '{strings[2]}'");

                    // Парсинг даты перевозки
                    if (!DateTime.TryParse(strings[3], out DateTime transportationDate))
                        throw new ArgumentException("Неверный формат даты перевозки");

                    // Парсинг информации о билетах (количество и цена)
                    string[] ticket = strings[4].Split(" ");

                    // Проверяем, что строка с билетами содержит два значения
                    if (ticket.Length != 2)
                        throw new ArgumentException("Неверный формат данных о билетах. Ожидается: 'количество цена'");

                    // Парсим количество и цену билетов
                    if (!int.TryParse(ticket[0], out int ticketCount))
                        throw new ArgumentException("Неверный формат количества билетов");

                    if (!int.TryParse(ticket[1], out int ticketPrice))
                        throw new ArgumentException("Неверный формат цены билета");

                    // Создаем объект Tickets
                    Tickets tickets = new(ticketCount, ticketPrice);

                    // Создаем объект CompletedTransportation
                    CompletedTransportation completedtransportation =
                        new(route, driver, bus, transportationDate, tickets);

                    // Добавляем выполненную перевозку в список
                    CompletedTransportations.Add(completedtransportation);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Ошибка при добавлении перевозки: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Произошла ошибка при обработке данных перевозки: {ex.Message}", ex);
                }
            }
            else
            {
                // Если строка пустая, выбрасываем исключение
                throw new ArgumentException("Строка с данными перевозки не может быть пустой");
            }
        }
    }
}