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
        private FormTransportation parentForm;
        private VolumeOfTransportation _VolumeOfTransportation;
        private DriverStaff _DriverStaff;
        private RouteCollection _RouteCollection;
        private BusFleet _BusFleet;
        private CompletedTransportation _CompletedTransportation;
        private Route _Route;
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
        }

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
        private void buttonChooseRoute_Click(object sender, EventArgs e)
        {
            Panel panelRoutesList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelRoutesList",
                Size = new Size(800, 396),
                TabIndex = 2
            };
            var toChoose = _RouteCollection.DeepCopy();

            List<Route> toDelete = [];
            foreach (var route in toChoose.Routes)
            {
                if (!route.DepartureDays.Contains(dateTimePickerDateOfTransportation.Value.DayOfWeek)) toDelete.Add(route); // Проверка совпадения дня недели рейса и дней когда выполняется маршрут
            }
            foreach (var del in toDelete) toChoose.Routes.Remove(del);
            if (toChoose.Routes == null || toChoose.Routes.Count == 0)
            {
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

                panelRoutesList.Click += (s, e) => panelList_CLick(panelRoutesList);
                panelRoutesList.Controls.Add(labelNoRoutes);
                this.Controls.Clear();
                this.Controls.Add(panelRoutesList);
                return;
            }

            int yPosition = 10; // Начальная позиция

            
            foreach (var route in toChoose.Routes)
            {
                Panel routePanel = CreatePanel(route, yPosition, panelRoutesList, ChooseButton_Click);
                panelRoutesList.Controls.Add(routePanel);

                yPosition += routePanel.Height + 10; // Отступ 
            }
            Label InfoLabel = new Label
            {
                Text = "Маршруты день недели отправления которых совпадают с днем выезда",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelRoutesList);
        }

        private void panelList_CLick(Panel panel)
        {
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
        }

        public Panel CreatePanel<T>(T item, int yPosition, Panel panelList, Action<T> ChooseAction)
        {
            if (item is Bus bus)
            {
                // PictureBox для изображения
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(150, 100),
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle
                };

                // Загружаем изображение
                if (!string.IsNullOrEmpty(bus.Photo) && System.IO.File.Exists(bus.Photo))
                {
                    pictureBox.Image = Image.FromFile(bus.Photo);
                }
                else
                {
                    // Создаем заглушку
                    FormBuses.CreateImagePlaceholder(pictureBox);
                }
            }
            // Создаем новую панель
            Panel panel = new Panel
            {
                Size = new Size(panelList.Width - 25, 120),
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true,
                Tag = item // Сохраняем ссылку
            };

            // Информация о маршруте
            Label specsLabel = new Label
            {
                Text = item.ToString(),
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Кнопка выбора
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

        private void ChooseButton_Click(Route route)
        {
            textBoxCode.Text = route.Code;
            textBoxCode.Tag = route;
            this.Controls.Clear();
            this.Controls.Add(panelTransportationEdit);
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
        private void buttonChooseDriver_Click(object sender, EventArgs e)
        {
            Panel panelDriversList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelDriversList",
                Size = new Size(800, 396),
                TabIndex = 2
            };
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

            if (_DriverStaff.Drivers == null || _DriverStaff.Drivers.Count == 0)
            {

                panelDriversList.Controls.Add(LabelClickToLeave);
                panelDriversList.Click += (s, e) => panelList_CLick(panelDriversList);
                panelDriversList.Controls.Add(labelNoDrivers);
                this.Controls.Clear();
                this.Controls.Add(panelDriversList);
                return;
            }
            int yPosition = 10; // Начальная позиция

            var toChoose = _DriverStaff.DeepCopy();
            List<Driver> toDelete = [];
            foreach (var dateOfBirth in toChoose.Drivers)
            {
                if (dateTimePickerDateOfTransportation.Value.Year - dateOfBirth.DateOfBirth.Year < 18)
                    toDelete.Add(dateOfBirth);
            }
            foreach (var del in toDelete) toChoose.Drivers.Remove(del);
            if (toChoose.Drivers.Count == 0)
            {
                panelDriversList.Controls.Add(LabelClickToLeave);
                panelDriversList.Click += (s, e) => panelList_CLick(panelDriversList);
                panelDriversList.Controls.Add(labelNoDrivers);
                this.Controls.Clear();
                this.Controls.Add(panelDriversList);
                return;
            }
            if (_VolumeOfTransportation.CompletedTransportations.Count == 0)
            {
                foreach (var driver in toChoose.Drivers)
                {
                    Panel driverPanel = CreatePanel(driver, yPosition, panelDriversList, ChooseButton_Click);
                    panelDriversList.Controls.Add(driverPanel);

                    yPosition += driverPanel.Height + 10; // Отступ 
                }

            }
            else
            {
                foreach (var driver in _VolumeOfTransportation.CompletedTransportations) //     Удаление выполняющих в это время рейс водителей
                {
                    //      Проверка дата выполненного рейса < выбранное время рейса < дата окончания выполненного рейса
                    if (driver.TransportationDate <= dateTimePickerDateOfTransportation.Value && driver.TransportationDate + driver.RouteCode.TransportationTime <= dateTimePickerDateOfTransportation.Value)
                        toChoose.Drivers.Remove(driver.DriverCode);
                }
                foreach (var driver in toChoose.Drivers)
                {
                    Panel driverPanel = CreatePanel(driver, yPosition, panelDriversList, ChooseButton_Click);
                    panelDriversList.Controls.Add(driverPanel);

                    yPosition += driverPanel.Height + 10; // Отступ 
                }
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
            Label InfoLabel = new Label
            {
                Text = "Водители которым на момент рейса было больше 18 лет",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelDriversList);
        }

        private void buttonChooseBus_Click(object sender, EventArgs e)
        {
            Panel panelBusesList = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 54),
                Name = "panelBusList",
                Size = new Size(800, 396),
                TabIndex = 2
            };
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

            if (_BusFleet.Buses == null || _BusFleet.Buses.Count == 0)
            {

                panelBusesList.Controls.Add(LabelClickToLeave);

                panelBusesList.Click += (s, e) => panelList_CLick(panelBusesList);
                panelBusesList.Controls.Add(labelNoBuses);
                this.Controls.Clear();
                this.Controls.Add(panelBusesList);
                return;
            }
            int yPosition = 10; // Начальная позиция

            var toChoose = _BusFleet.DeepCopy();
            List<Bus> toDelete = [];
            foreach (var year in toChoose.Buses)
            {
                if (dateTimePickerDateOfTransportation.Value.Year - year.Year < 0) // Проверка выпуска автобуса и года выполнения рейса
                    toDelete.Add(year);
            }
            foreach (var del in toDelete) toChoose.Buses.Remove(del);

            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                foreach (var completedTransportation in _VolumeOfTransportation.CompletedTransportations)
                {
                    // Проверка пересечения по времени
                    DateTime existingStart = completedTransportation.TransportationDate;
                    DateTime existingEnd = existingStart + completedTransportation.RouteCode.TransportationTime;
                    DateTime newStart = dateTimePickerDateOfTransportation.Value;
                    DateTime newEnd = newStart + dateTimePickerDateOfTransportation.Value.TimeOfDay; // нужно знать время нового маршрута

                    bool timeConflict = newStart < existingEnd && newEnd > existingStart;

                    if (timeConflict)
                        toChoose.Buses.Remove(completedTransportation.Bus);
                }
            }
            foreach (var bus in toChoose.Buses)
            {
                Panel busPanel = CreatePanel(bus, yPosition, panelBusesList, ChooseButton_Click);
                panelBusesList.Controls.Add(busPanel);

                yPosition += busPanel.Height + 10; // Отступ 
            }

            if (toChoose.Buses.Count == 0)
            {
                panelBusesList.Controls.Add(LabelClickToLeave);
                panelBusesList.Click += (s, e) => panelList_CLick(panelBusesList);
                panelBusesList.Controls.Add(labelNoBuses);
                this.Controls.Clear();
                this.Controls.Add(panelBusesList);
                return;
            }
                
            Label InfoLabel = new Label
            {
                Text = "Доступные автобусы",
                Location = new Point(11, yPosition),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            this.Controls.Clear();
            this.Controls.Add(InfoLabel);
            this.Controls.Add(panelBusesList);

        }

        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            this.parentForm.LoadVolumeOfTransportation();
            this.parentForm.BringToFront();
            this.parentForm.Show();
            this.parentForm.panelTransportationMenuTitle.Show();
            this.Close();
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxSoldTickets.Text))
                {
                    throw new Exception("Поле с количеством проданных билетов не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxTicketCost.Text))
                {
                    throw new Exception("Поле c ценой билетов не может быть пустым!");
                }
                Tickets tickets = new(int.Parse(textBoxSoldTickets.Text), int.Parse(textBoxTicketCost.Text));
                if (_CompletedTransportation == null)
                {
                    // Проверка на дубликат государственного номера
                    if (_VolumeOfTransportation.CompletedTransportations.Any(completed => completed.TransportationDate == dateTimePickerDateOfTransportation.Value))
                    {
                        MessageBox.Show("Рейс в этот день уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создание и добавление автобуса
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
                else
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

            private void ClearForm()
        {
            // Очистка текстовых полей
            textBoxCode.Clear();
            textBoxId.Clear();
            textBoxStateNumber.Clear();
            textBoxSoldTickets.Clear();
            textBoxTicketCost.Clear();

            // Очистка Tag свойств
            textBoxCode.Tag = null;
            textBoxId.Tag = null;
            textBoxStateNumber.Tag = null;

            // Установка даты по умолчанию (текущая дата)
            dateTimePickerDateOfTransportation.Value = DateTime.Now;
        }
        private void textBoxTicketCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }
    }
}
