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
    public partial class TransportationEdit : Form
    {
        // Поля для хранения ссылок на объекты данных
        private FormTransportation parentForm;
        private VolumeOfTransportation _VolumeOfTransportation;
        private DriverStaff _DriverStaff;
        private RouteCollection _RouteCollection;
        private BusFleet _BusFleet;
        private CompletedTransportation _CompletedTransportation;
        private Route _Route;

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

            // Если редактируем существующий рейс, заполняем форму его данными
            if (_CompletedTransportation != null)
            {
                // Заполняем поля данными из объекта CompletedTransportation
                textBoxCode.Text = _CompletedTransportation.RouteCode?.Code;
                textBoxCode.Tag = _CompletedTransportation.RouteCode; // Сохраняем объект в Tag

                textBoxId.Text = _CompletedTransportation.DriverCode?.Id.ToString();
                textBoxId.Tag = _CompletedTransportation.DriverCode;

                textBoxStateNumber.Text = _CompletedTransportation.Bus?.StateNumber;
                textBoxStateNumber.Tag = _CompletedTransportation.Bus;

                dateTimePickerDateOfTransportation.Value = _CompletedTransportation.TransportationDate;

                // Заполняем данные о билетах
                if (_CompletedTransportation.SoldTickets != null)
                {
                    textBoxSoldTickets.Text = _CompletedTransportation.SoldTickets.SoldTickets.ToString();
                    textBoxTicketCost.Text = _CompletedTransportation.SoldTickets.TicketCost.ToString();
                }
            }
        }

        // Конструктор для создания нового рейса
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

        // Обработчик выбора маршрута
        private void buttonChooseRoute_Click(object sender, EventArgs e)
        {
            // Создание панели для отображения списка маршрутов
            Panel panelRoutesList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelRoutesList",
                Size = new Size(800, 396),
                TabIndex = 2,
                AutoScroll = true,
                AutoScrollMinSize = new Size(0, 0),
                AutoScrollMargin = new Size(0, 10)
            };
            panelRoutesList.HorizontalScroll.Visible = false;
            panelRoutesList.AutoScrollMargin = new Size(0, 10);

            // Создаем копию коллекции маршрутов для фильтрации
            var toChoose = _RouteCollection.DeepCopy();

            // Фильтрация маршрутов по дню недели отправления
            List<Route> toDelete = [];
            foreach (var route in toChoose.Routes)
            {
                // Проверка совпадения дня недели рейса и дней когда выполняется маршрут
                if (!route.DepartureDays.Contains(dateTimePickerDateOfTransportation.Value.DayOfWeek))
                    toDelete.Add(route);
            }
            foreach (var del in toDelete) toChoose.Routes.Remove(del);

            // Проверка наличия доступных маршрутов
            if (toChoose.Routes == null || toChoose.Routes.Count == 0)
            {
                // Создаем метки для информирования пользователя
                Label labelNoRoutes = new Label
                {
                    Anchor = AnchorStyles.None,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                    Location = new Point(288, 171),
                    Name = "labelNoRoutes",
                    Size = new Size(211, 37),
                    TabIndex = 5,
                    Text = "Маршрутов нет",
                    Visible = true
                };
                Label LabelClickToLeave = new Label
                {
                    Anchor = AnchorStyles.None,
                    Text = "Кликните в любом месте чтобы выйти",
                    Location = new Point(labelNoRoutes.Width + 50, labelNoRoutes.Height + 100),
                    AutoSize = true,
                    Font = new Font("Arial", 10)
                };
                panelRoutesList.Controls.Add(LabelClickToLeave);

                // Добавляем обработчик клика для возврата
                panelRoutesList.Click += (s, e) => panelList_CLick(panelRoutesList);
                panelRoutesList.Controls.Add(labelNoRoutes);
                this.Controls.Clear();
                this.Controls.Add(panelRoutesList);
                return;
            }

            int yPosition = 10; // Начальная позиция для размещения панелей маршрутов

            // Создание панелей для каждого доступного маршрута
            foreach (var route in toChoose.Routes)
            {
                Panel routePanel = CreatePanel(route, yPosition, panelRoutesList, ChooseButton_Click);
                panelRoutesList.Controls.Add(routePanel);

                yPosition += routePanel.Height + 10; // Отступ между панелями
            }

            // Информационная метка для пользователя
            Label InfoLabel = new Label
            {
                Text = "Маршруты, день недели отправления которых совпадают с днем выезда",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Обновление интерфейса: показываем панель со списком маршрутов
            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelRoutesList);
        }

        // Обработчик клика по панели для возврата к форме редактирования
        private void panelList_CLick(Panel panel)
        {
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
        }

        // Универсальный метод создания панели для отображения объекта
        public Panel CreatePanel<T>(T item, int yPosition, Panel panelList, Action<T> ChooseAction)
        {
            // Обработка специального случая для автобуса (добавление изображения)
            if (item is Bus bus)
            {
                // PictureBox для изображения автобуса
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(150, 100),
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Загружаем изображение, если путь указан и файл существует
                if (!string.IsNullOrEmpty(bus.Photo) && System.IO.File.Exists(bus.Photo))
                {
                    pictureBox.Image = Image.FromFile(bus.Photo);
                }
                else
                {
                    // Создаем заглушку, если изображение отсутствует
                    FormBuses.CreateImagePlaceholder(pictureBox);
                }
            }

            // Создаем новую панель для отображения информации об объекте
            Panel panel = new Panel
            {
                Size = new Size(panelList.Width - 25, 120),
                Location = new Point(10, yPosition),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = item // Сохраняем ссылку на объект в Tag для доступа позже
            };

            // Метка с информацией об объекте (использует ToString())
            Label specsLabel = new Label
            {
                Text = item.ToString(),
                Location = new Point(0, 0),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Кнопка выбора объекта
            System.Windows.Forms.Button ChooseButton = new System.Windows.Forms.Button
            {
                Text = "Выбрать",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
                Anchor = AnchorStyles.Right,
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = item
            };
            ChooseButton.Click += (s, e) => ChooseAction(item);

            panel.Controls.Add(specsLabel);
            panel.Controls.Add(ChooseButton);

            return panel;
        }

        // Перегрузки метода для обработки выбора разных типов объектов
        private void ChooseButton_Click(Route route)
        {
            textBoxCode.Text = route.Code;
            textBoxCode.Tag = route; // Сохраняем объект маршрута
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit); // Возвращаемся к форме редактирования
        }

        private void ChooseButton_Click(Driver driver)
        {
            textBoxId.Text = driver.Id.ToString();
            textBoxId.Tag = driver;
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
        }

        private void ChooseButton_Click(Bus bus)
        {
            textBoxStateNumber.Text = bus.StateNumber;
            textBoxStateNumber.Tag = bus;
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
        }

        // Обработчик выбора водителя
        private void buttonChooseDriver_Click(object sender, EventArgs e)
        {
            // Создание панели для отображения списка водителей
            Panel panelDriversList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelDriversList",
                Size = new Size(800, 396),
                TabIndex = 2,
                AutoScroll = true,
                AutoScrollMinSize = new Size(0, 0),
                AutoScrollMargin = new Size(0, 10)
            };
            panelDriversList.HorizontalScroll.Visible = false;
            panelDriversList.AutoScrollMargin = new Size(0, 10);

            // Создание меток для случаев отсутствия данных
            Label labelNoDrivers = new Label
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(288, 171),
                Name = "labelNoRoutes",
                Size = new Size(211, 37),
                TabIndex = 5,
                Text = "Водителей нет",
                Visible = true
            };
            Label LabelClickToLeave = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "Кликните в любом месте чтобы выйти",
                Location = new Point(labelNoDrivers.Width + 50, labelNoDrivers.Height + 100),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            // Проверка наличия водителей в коллекции
            if (_DriverStaff.Drivers == null || _DriverStaff.Drivers.Count == 0)
            {
                panelDriversList.Controls.Add(LabelClickToLeave);
                panelDriversList.Click += (s, e) => panelList_CLick(panelDriversList);
                panelDriversList.Controls.Add(labelNoDrivers);
                this.Controls.Clear();
                this.Controls.Add(panelDriversList);
                return;
            }

            int yPosition = 10; // Начальная позиция для размещения панелей

            // Создаем копию коллекции водителей для фильтрации
            var toChoose = _DriverStaff.DeepCopy();
            List<Driver> toDelete = [];

            // Фильтрация водителей по возрасту (должно быть 18+ лет)
            foreach (var driver in toChoose.Drivers)
            {
                // Упрощенная проверка возраста (только по году)
                // ВНИМАНИЕ: эта проверка неточна - нужно учитывать месяц и день рождения
                if (dateTimePickerDateOfTransportation.Value.Year - driver.DateOfBirth.Year < 18)
                    toDelete.Add(driver);
            }
            foreach (var del in toDelete) toChoose.Drivers.Remove(del);

            // Если нет водителей подходящего возраста
            if (toChoose.Drivers.Count == 0)
            {
                panelDriversList.Controls.Add(LabelClickToLeave);
                panelDriversList.Click += (s, e) => panelList_CLick(panelDriversList);
                panelDriversList.Controls.Add(labelNoDrivers);
                this.Controls.Clear();
                this.Controls.Add(panelDriversList);
                return;
            }

            // Если нет выполненных рейсов, показываем всех доступных водителей
            if (_VolumeOfTransportation.CompletedTransportations.Count == 0)
            {
                foreach (var driver in toChoose.Drivers)
                {
                    Panel driverPanel = CreatePanel(driver, yPosition, panelDriversList, ChooseButton_Click);
                    panelDriversList.Controls.Add(driverPanel);

                    yPosition += driverPanel.Height + 10; // Отступ между панелями
                }
            }
            else
            {
                // Удаление водителей, которые в это время выполняют другой рейс
                foreach (var completedTransportation in _VolumeOfTransportation.CompletedTransportations)
                {
                    // Проверка: выбранное время попадает в интервал выполнения другого рейса
                    // Условие: дата начала рейса <= выбранное время < дата окончания рейса
                    if (completedTransportation.TransportationDate <= dateTimePickerDateOfTransportation.Value &&
                        completedTransportation.TransportationDate + completedTransportation.RouteCode.TransportationTime >= dateTimePickerDateOfTransportation.Value)
                    {
                        toChoose.Drivers.Remove(completedTransportation.DriverCode);
                    }
                }

                // Показываем оставшихся доступных водителей
                foreach (var driver in toChoose.Drivers)
                {
                    Panel driverPanel = CreatePanel(driver, yPosition, panelDriversList, ChooseButton_Click);
                    panelDriversList.Controls.Add(driverPanel);

                    yPosition += driverPanel.Height + 10; // Отступ между панелями
                }

                // Если после фильтрации не осталось доступных водителей
                if (toChoose.Drivers.Count == 0)
                {
                    panelDriversList.Controls.Add(LabelClickToLeave);
                    panelDriversList.Click += (s, e) => panelList_CLick(panelDriversList);
                    panelDriversList.Controls.Add(labelNoDrivers);
                    this.Controls.Clear();
                    this.Controls.Add(panelDriversList);
                    return;
                }
            }

            // Информационная метка о возрастном ограничении
            Label InfoLabel = new Label
            {
                Text = "Водители, которым на момент рейса было больше 18 лет",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Обновление интерфейса
            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelDriversList);
        }

        // Обработчик выбора автобуса
        private void buttonChooseBus_Click(object sender, EventArgs e)
        {
            // Создание панели для отображения списка автобусов
            Panel panelBusesList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelBusList",
                Size = new Size(800, 396),
                TabIndex = 2,
                AutoScroll = true,
                AutoScrollMinSize = new Size(0, 0),
                AutoScrollMargin = new Size(0, 10)
            };
            panelBusesList.HorizontalScroll.Visible = false;
            panelBusesList.AutoScrollMargin = new Size(0, 10);

            // Создание меток для случаев отсутствия данных
            Label labelNoBuses = new Label
            {
                Anchor = AnchorStyles.None,
                AutoSize = true,
                Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 204),
                Location = new Point(288, 171),
                Name = "labelNoRoutes",
                Size = new Size(211, 37),
                TabIndex = 5,
                Text = "Автобусов нет",
                Visible = true
            };
            Label LabelClickToLeave = new Label
            {
                Anchor = AnchorStyles.None,
                Text = "Кликните в любом месте чтобы выйти",
                Location = new Point(labelNoBuses.Width + 50, labelNoBuses.Height + 100),
                AutoSize = true,
                Font = new Font("Arial", 10)
            };

            // Проверка наличия автобусов в парке
            if (_BusFleet.Buses == null || _BusFleet.Buses.Count == 0)
            {
                panelBusesList.Controls.Add(LabelClickToLeave);
                panelBusesList.Click += (s, e) => panelList_CLick(panelBusesList);
                panelBusesList.Controls.Add(labelNoBuses);
                this.Controls.Clear();
                this.Controls.Add(panelBusesList);
                return;
            }

            int yPosition = 10; // Начальная позиция для размещения панелей

            // Создаем копию парка автобусов для фильтрации
            var toChoose = _BusFleet.DeepCopy();
            List<Bus> toDelete = [];

            // Фильтрация по году выпуска автобуса
            // Автобус не может быть выпущен позже года выполнения рейса
            foreach (var bus in toChoose.Buses)
            {
                if (dateTimePickerDateOfTransportation.Value.Year - bus.Year < 0)
                    toDelete.Add(bus);
            }
            foreach (var del in toDelete) toChoose.Buses.Remove(del);

            // Проверка занятости автобусов по времени
            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                foreach (var completedTransportation in _VolumeOfTransportation.CompletedTransportations)
                {
                    // Проверка: выбранное время попадает в интервал выполнения другого рейса
                    if (completedTransportation.TransportationDate <= dateTimePickerDateOfTransportation.Value &&
                        completedTransportation.TransportationDate + completedTransportation.RouteCode.TransportationTime >= dateTimePickerDateOfTransportation.Value)
                    {
                        toChoose.Buses.Remove(completedTransportation.Bus);
                    }
                }
            }

            // Создание панелей для каждого доступного автобуса
            foreach (var bus in toChoose.Buses)
            {
                Panel busPanel = CreatePanel(bus, yPosition, panelBusesList, ChooseButton_Click);
                panelBusesList.Controls.Add(busPanel);

                yPosition += busPanel.Height + 10; // Отступ между панелями
            }

            // Если нет доступных автобусов после фильтрации
            if (toChoose.Buses.Count == 0)
            {
                panelBusesList.Controls.Add(LabelClickToLeave);
                panelBusesList.Click += (s, e) => panelList_CLick(panelBusesList);
                panelBusesList.Controls.Add(labelNoBuses);
                this.Controls.Clear();
                this.Controls.Add(panelBusesList);
                return;
            }

            // Информационная метка
            Label InfoLabel = new Label
            {
                Text = "Доступные автобусы",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Обновление интерфейса
            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelBusesList);
        }

        // Обработчик отмены изменений
        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            this.parentForm.LoadVolumeOfTransportation(); // Обновление данных в родительской форме
            this.parentForm.BringToFront();
            this.parentForm.Show();
            this.parentForm.panelTransportationMenuTitle.Show();
            this.Close(); // Закрытие формы редактирования
        }

        // Обработчик сохранения изменений
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация обязательных полей
                if (string.IsNullOrWhiteSpace(textBoxSoldTickets.Text))
                {
                    throw new Exception("Поле с количеством проданных билетов не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxTicketCost.Text))
                {
                    throw new Exception("Поле c ценой билетов не может быть пустым!");
                }

                // Создание объекта Tickets
                Tickets tickets = new(int.Parse(textBoxSoldTickets.Text), int.Parse(textBoxTicketCost.Text));

                if (_CompletedTransportation == null) // Создание нового рейса
                {
                    // Проверка на дубликат рейса в этот день
                    if (_VolumeOfTransportation.CompletedTransportations.Any(completed =>
                        completed.TransportationDate == dateTimePickerDateOfTransportation.Value))
                    {
                        MessageBox.Show("Рейс в этот день уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создание и добавление нового рейса
                    CompletedTransportation completedTransportation = new(
                        (Route)textBoxCode.Tag,
                        (Driver)textBoxId.Tag,
                        (Bus)textBoxStateNumber.Tag,
                        dateTimePickerDateOfTransportation.Value,
                        tickets
                    );
                    _VolumeOfTransportation.CompletedTransportations.Add(completedTransportation);

                    MessageBox.Show("Рейс успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Редактирование существующего рейса
                {
                    _CompletedTransportation.RouteCode = (Route)textBoxCode.Tag;
                    _CompletedTransportation.DriverCode = (Driver)textBoxId.Tag;
                    _CompletedTransportation.Bus = (Bus)textBoxStateNumber.Tag;
                    _CompletedTransportation.TransportationDate = dateTimePickerDateOfTransportation.Value;
                    _CompletedTransportation.SoldTickets = tickets;
                    _CompletedTransportation.TotalRevenue = tickets.SoldTickets * tickets.TicketCost;

                    MessageBox.Show("Маршрут успешно изменен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Очистка формы и возврат к родительской форме
                ClearForm();
                this.parentForm.LoadVolumeOfTransportation();
                this.parentForm.BringToFront();
                this.parentForm.Show();
                this.parentForm.panelTransportationMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения маршрута");
            }
        }

        // Метод очистки формы
        private void ClearForm()
        {
            // Очистка текстовых полей
            textBoxCode.Clear();
            textBoxId.Clear();
            textBoxStateNumber.Clear();
            textBoxSoldTickets.Clear();
            textBoxTicketCost.Clear();

            // Очистка Tag свойств (хранят ссылки на объекты)
            textBoxCode.Tag = null;
            textBoxId.Tag = null;
            textBoxStateNumber.Tag = null;

            // Установка даты по умолчанию (текущая дата)
            dateTimePickerDateOfTransportation.Value = DateTime.Now;
        }

        // Обработчик ввода для поля стоимости билета (разрешаем только цифры)
        private void textBoxTicketCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) // Разрешаем управляющие символы (Backspace, Delete)
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar)) // Запрещаем все, кроме цифр
            {
                e.Handled = true;
                return;
            }
        }
    }
}