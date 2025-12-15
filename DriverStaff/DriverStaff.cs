using System;
using System.Collections.Generic;

namespace CourseWork_3sem
{
    // Класс для управления штатом водителей
    public class DriverStaff
    {
        // Список водителей в штате
        public List<Driver> Drivers;

        // Конструктор с передачей готового списка водителей
        public DriverStaff(List<Driver> drivers) => Drivers = drivers;

        // Конструктор по умолчанию создает пустой список водителей
        public DriverStaff() => Drivers = [];


        // Метод для добавления водителя из строки
        public void Add(string value)
        {
            // Проверяем, что строка не пустая и не состоит только из пробелов
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Объявляем переменные для хранения распарсенных данных
                int id, workExperience;
                FullName name;
                Class driverClass;
                DateTime dateOfBirth;
                Category category;

                // Разделяем строку на части по точке с запятой
                string[] strings = value.Split(';');

                try
                {
                    // Парсинг ФИО (первый элемент массива)
                    // Разделяем ФИО по пробелам
                    string[] NameInputs = (strings[0]).Split(' ');

                    // Проверяем, что ФИО состоит из 3 частей (Фамилия Имя Отчество)
                    if (NameInputs.Length > 3)
                        throw new ArgumentException("ФИО должно содержать ровно три части: Фамилия Имя Отчество");

                    // Создаем объект FullName из трех частей
                    name = new(NameInputs[0], NameInputs[1], NameInputs[2]);

                    // Парсинг табельного номера (второй элемент)
                    if (!int.TryParse(strings[1], out id))
                        throw new ArgumentException("Неверный формат табельного номера");

                    // Парсинг даты рождения (третий элемент)
                    if (!DateTime.TryParse(strings[2], out dateOfBirth))
                        throw new ArgumentException("Неверный формат даты рождения");

                    // Парсинг опыта работы (четвертый элемент)
                    if (!int.TryParse(strings[3], out workExperience))
                        throw new ArgumentException("Неверный формат опыта работы");

                    // Парсинг категории прав (пятый элемент)
                    if (!Enum.TryParse(strings[4], out category))
                        throw new ArgumentException("Неверный формат категории прав");

                    // Парсинг класса водителя (шестой элемент)
                    if (!Enum.TryParse(strings[5], out driverClass))
                        throw new ArgumentException("Неверный формат класса водителя");

                    // Создаем объект Driver с распарсенными данными
                    Driver driver = new(name, id, dateOfBirth, workExperience, category, driverClass);

                    // Добавляем водителя в список
                    Drivers.Add(driver);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Ошибка при добавлении водителя: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    // Обработка других исключений 
                    throw new Exception($"Произошла ошибка при обработке данных водителя: {ex.Message}", ex);
                }
            }
            else
            {
                // Если строка пустая, выбрасываем исключение
                throw new ArgumentException("Строка с данными водителя не может быть пустой");
            }
        }

        // Метод для создания копии штата водителей
        // Возвращает новый объект DriverStaff
        public DriverStaff DeepCopy()
        {
            // Создаем новый экземпляр DriverStaff
            DriverStaff other = new();

            // Копируем всех водителей из текущего списка в новый
            foreach (var driver in Drivers)
            {
                // Добавляем ссылку на тот же объект Driver
                other.Drivers.Add(driver);
            }
            return other;
        }
    }
}