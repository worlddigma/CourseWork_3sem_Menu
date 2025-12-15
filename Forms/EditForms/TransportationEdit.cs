using CourseWork_3sem;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CourseWork_3sem_Menu.Forms.EditForms
{
    // Форма для добавления/редактирования выполненного рейса
    public partial class TransportationEdit : Form
    {
        private FormTransportation parentForm;          // Родительская форма
        private VolumeOfTransportation _VolumeOfTransportation; // Список рейсов
        private DriverStaff _DriverStaff;               // Список водителей
        private RouteCollection _RouteCollection;       // Список маршрутов
        private BusFleet _BusFleet;                     // Список автобусов
        private CompletedTransportation _CompletedTransportation; // Редактируемый рейс
        private Route _Route;                           // Выбранный маршрут

        // Конструктор для редактирования существующего рейса
        public TransportationEdit(VolumeOfTransportation volumeOfTransportation,
                                  RouteCollection routeCollection,
                                  BusFleet busFleet,
                                  DriverStaff driverStaff,
                                  FormTransportation parent,
                                  CompletedTransportation completedTransportation)
        {
            InitializeComponent();
            parentForm = parent;
            _VolumeOfTransportation = volumeOfTransportation;
            _RouteCollection = routeCollection;
            _BusFleet = busFleet;
            _DriverStaff = driverStaff;
            _CompletedTransportation = completedTransportation;

            if (_CompletedTransportation != null)
            {
                // Заполнение полей данными редактируемого рейса
                textBoxCode.Text = _CompletedTransportation.RouteCode?.Code;
                textBoxCode.Tag = _CompletedTransportation.RouteCode;

                textBoxId.Text = _CompletedTransportation.DriverCode?.Id.ToString();
                textBoxId.Tag = _CompletedTransportation.DriverCode;

                textBoxStateNumber.Text = _CompletedTransportation.Bus?.StateNumber;
                textBoxStateNumber.Tag = _CompletedTransportation.Bus;

                dateTimePickerDateOfTransportation.Value = _CompletedTransportation.TransportationDate;

                if (_CompletedTransportation.SoldTickets != null)
                {
                    textBoxSoldTickets.Text = _CompletedTransportation.SoldTickets.SoldTickets.ToString();
                    textBoxTicketCost.Text = _CompletedTransportation.SoldTickets.TicketCost.ToString();
                }
            }
        }

        // Конструктор для добавления нового рейса
        public TransportationEdit(VolumeOfTransportation volumeOfTransportation,
                                  RouteCollection routeCollection,
                                  BusFleet busFleet,
                                  DriverStaff driverStaff,
                                  FormTransportation parent)
        {
            InitializeComponent();
            _VolumeOfTransportation = volumeOfTransportation;
            _RouteCollection = routeCollection;
            _BusFleet = busFleet;
            _DriverStaff = driverStaff;
            parentForm = parent;
        }

        // Выбор маршрута для рейса
        private void buttonChooseRoute_Click(object sender, EventArgs e)
        {
            // Создание панели для выбора маршрутов
            Panel panelRoutesList = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                HorizontalScroll = { Visible = false }
            };

            // Фильтрация маршрутов по дню недели
            var availableRoutes = _RouteCollection.DeepCopy();
            availableRoutes.Routes.RemoveAll(route =>
                !route.DepartureDays.Contains(dateTimePickerDateOfTransportation.Value.DayOfWeek));

            // Проверка наличия маршрутов
            if (!availableRoutes.Routes.Any())
            {
                ShowNoItemsMessage(panelRoutesList, "Маршрутов нет",
                    "Маршрутов для выбранного дня недели не найдено");
                return;
            }

            // Отображение доступных маршрутов
            int yPosition = 10;
            foreach (var route in availableRoutes.Routes)
            {
                Panel routePanel = CreatePanel(route, yPosition, panelRoutesList, ChooseRoute);
                panelRoutesList.Controls.Add(routePanel);
                yPosition += routePanel.Height + 10;
            }

            ShowSelectionPanel(panelRoutesList,
                "Маршруты с отправлением в выбранный день недели");
        }

        // Выбор водителя для рейса
        private void buttonChooseDriver_Click(object sender, EventArgs e)
        {
            Panel panelDriversList = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                HorizontalScroll = { Visible = false }
            };

            // Фильтрация водителей по возрасту (должно быть >= 18 лет)
            var availableDrivers = _DriverStaff.DeepCopy();
            availableDrivers.Drivers.RemoveAll(driver =>
                dateTimePickerDateOfTransportation.Value.Year - driver.DateOfBirth.Year < 18);

            // Исключение водителей, уже занятых в это время
            if (_VolumeOfTransportation.CompletedTransportations.Any())
            {
                var busyDrivers = GetBusyDriversAtTime(dateTimePickerDateOfTransportation.Value);
                availableDrivers.Drivers.RemoveAll(driver => busyDrivers.Contains(driver));
            }

            // Проверка наличия водителей
            if (!availableDrivers.Drivers.Any())
            {
                ShowNoItemsMessage(panelDriversList, "Водителей нет",
                    "Нет свободных водителей на выбранное время");
                return;
            }

            // Отображение доступных водителей
            int yPosition = 10;
            foreach (var driver in availableDrivers.Drivers)
            {
                Panel driverPanel = CreatePanel(driver, yPosition, panelDriversList, ChooseDriver);
                panelDriversList.Controls.Add(driverPanel);
                yPosition += driverPanel.Height + 10;
            }

            ShowSelectionPanel(panelDriversList, "Свободные водители на выбранное время");
        }

        // Выбор автобуса для рейса
        private void buttonChooseBus_Click(object sender, EventArgs e)
        {
            Panel panelBusesList = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                HorizontalScroll = { Visible = false }
            };

            // Фильтрация автобусов (год выпуска должен быть <= года рейса)
            var availableBuses = _BusFleet.DeepCopy();
            availableBuses.Buses.RemoveAll(bus =>
                dateTimePickerDateOfTransportation.Value.Year - bus.Year < 0);

            // Исключение автобусов, уже занятых в это время
            if (_VolumeOfTransportation.CompletedTransportations.Any())
            {
                var busyBuses = GetBusyBusesAtTime(dateTimePickerDateOfTransportation.Value);
                availableBuses.Buses.RemoveAll(bus => busyBuses.Contains(bus));
            }

            // Проверка наличия автобусов
            if (!availableBuses.Buses.Any())
            {
                ShowNoItemsMessage(panelBusesList, "Автобусов нет",
                    "Нет свободных автобусов на выбранное время");
                return;
            }

            // Отображение доступных автобусов
            int yPosition = 10;
            foreach (var bus in availableBuses.Buses)
            {
                Panel busPanel = CreatePanel(bus, yPosition, panelBusesList, ChooseBus);
                panelBusesList.Controls.Add(busPanel);
                yPosition += busPanel.Height + 10;
            }

            ShowSelectionPanel(panelBusesList, "Свободные автобусы на выбранное время");
        }

        // Сохранение рейса
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация полей
                if (string.IsNullOrWhiteSpace(textBoxSoldTickets.Text) ||
                    string.IsNullOrWhiteSpace(textBoxTicketCost.Text))
                    throw new Exception("Заполните все поля с билетами.");

                if (textBoxCode.Tag == null || textBoxId.Tag == null || textBoxStateNumber.Tag == null)
                    throw new Exception("Выберите маршрут, водителя и автобус.");

                // Парсинг данных о билетах
                if (!int.TryParse(textBoxSoldTickets.Text, out int soldTickets) ||
                    !int.TryParse(textBoxTicketCost.Text, out int ticketCost))
                    throw new Exception("Некорректные значения билетов.");

                Tickets tickets = new(soldTickets, ticketCost);

                // Проверка уникальности рейса (для добавления)
                if (_CompletedTransportation == null)
                {
                    if (_VolumeOfTransportation.CompletedTransportations.Any(ct =>
                        ct.TransportationDate.Date == dateTimePickerDateOfTransportation.Value.Date))
                    {
                        MessageBox.Show("Рейс в этот день уже существует.", "Ошибка");
                        return;
                    }

                    // Создание нового рейса
                    CompletedTransportation completedTransportation = new(
                        (Route)textBoxCode.Tag,
                        (Driver)textBoxId.Tag,
                        (Bus)textBoxStateNumber.Tag,
                        dateTimePickerDateOfTransportation.Value,
                        tickets
                    );
                    _VolumeOfTransportation.CompletedTransportations.Add(completedTransportation);
                    MessageBox.Show("Рейс успешно добавлен!", "Успех");
                }
                else
                {
                    // Обновление существующего рейса
                    _CompletedTransportation.RouteCode = (Route)textBoxCode.Tag;
                    _CompletedTransportation.DriverCode = (Driver)textBoxId.Tag;
                    _CompletedTransportation.Bus = (Bus)textBoxStateNumber.Tag;
                    _CompletedTransportation.TransportationDate = dateTimePickerDateOfTransportation.Value;
                    _CompletedTransportation.SoldTickets = tickets;
                    _CompletedTransportation.TotalRevenue = tickets.SoldTickets * tickets.TicketCost;
                    MessageBox.Show("Рейс успешно изменен!", "Успех");
                }

                // Закрытие формы и обновление родительской формы
                parentForm.LoadVolumeOfTransportation();
                parentForm.panelTransportationMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Отмена изменений
        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            parentForm.LoadVolumeOfTransportation();
            parentForm.panelTransportationMenuTitle.Show();
            this.Close();
        }

        // Вспомогательные методы

        // Создание панели для выбора элемента
        private Panel CreatePanel<T>(T item, int yPosition, Panel parentPanel, Action<T> chooseAction)
        {
            Panel panel = new Panel
            {
                Size = new Size(parentPanel.Width - 25, 120),
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = item
            };

            Label specsLabel = new Label
            {
                Text = item.ToString(),
                Location = new Point(0, 0),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            System.Windows.Forms.Button chooseButton = new System.Windows.Forms.Button
            {
                Text = "Выбрать",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = item
            };
            chooseButton.Click += (s, ev) => chooseAction(item);

            panel.Controls.Add(specsLabel);
            panel.Controls.Add(chooseButton);
            return panel;
        }

        // Показать сообщение об отсутствии элементов
        private void ShowNoItemsMessage(Panel panel, string title, string message)
        {
            panel.Controls.Clear();

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 20),
                AutoSize = true,
                Location = new Point(288, 171)
            };

            Label messageLabel = new Label
            {
                Text = message,
                Font = new Font("Arial", 10),
                AutoSize = true,
                Location = new Point(titleLabel.Width + 50, titleLabel.Height + 100)
            };

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(messageLabel);
            panel.Click += (s, e) => ReturnToEditForm(panel);

            this.Controls.Clear();
            this.Controls.Add(panel);
        }

        // Показать панель выбора
        private void ShowSelectionPanel(Panel selectionPanel, string infoText)
        {
            Label infoLabel = new Label
            {
                Text = infoText,
                Location = new Point(11, selectionPanel.Height + 10),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            this.Controls.Clear();
            this.Controls.Add(infoLabel);
            this.Controls.Add(selectionPanel);
        }

        // Возврат к форме редактирования
        private void ReturnToEditForm(Panel panel)
        {
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
        }

        // Обработчики выбора элементов

        private void ChooseRoute(Route route)
        {
            textBoxCode.Text = route.Code;
            textBoxCode.Tag = route;
            ReturnToEditForm(null);
        }

        private void ChooseDriver(Driver driver)
        {
            textBoxId.Text = driver.Id.ToString();
            textBoxId.Tag = driver;
            ReturnToEditForm(null);
        }

        private void ChooseBus(Bus bus)
        {
            textBoxStateNumber.Text = bus.StateNumber;
            textBoxStateNumber.Tag = bus;
            ReturnToEditForm(null);
        }

        // Получение занятых водителей на указанное время
        private List<Driver> GetBusyDriversAtTime(DateTime time)
        {
            return _VolumeOfTransportation.CompletedTransportations
                .Where(ct => IsTimeOverlap(ct.TransportationDate,
                       ct.TransportationDate + ct.RouteCode.TransportationTime, time))
                .Select(ct => ct.DriverCode)
                .Distinct()
                .ToList();
        }

        // Получение занятых автобусов на указанное время
        private List<Bus> GetBusyBusesAtTime(DateTime time)
        {
            return _VolumeOfTransportation.CompletedTransportations
                .Where(ct => IsTimeOverlap(ct.TransportationDate,
                       ct.TransportationDate + ct.RouteCode.TransportationTime, time))
                .Select(ct => ct.Bus)
                .Distinct()
                .ToList();
        }

        // Проверка пересечения временных интервалов
        private bool IsTimeOverlap(DateTime start1, DateTime end1, DateTime checkTime)
        {
            return checkTime >= start1 && checkTime <= end1;
        }

        // Валидация ввода цены билета
        private void textBoxTicketCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
