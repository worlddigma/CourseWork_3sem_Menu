using CourseWork_3sem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_3sem_Menu.Forms
{// Форма для управления выполненными рейсами
    public partial class FormTransportation : Form
    {
        private RouteCollection _RouteCollection;           // Список маршрутов
        private DriverStaff _DriverStaff;                   // Список водителей
        private BusFleet _BusFleet;                         // Список автобусов
        private VolumeOfTransportation _VolumeOfTransportation; // Список рейсов
        private Form ActiveForm;                            // Текущая дочерняя форма

        public FormTransportation(RouteCollection routeCollection, BusFleet busFleet,
            DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _RouteCollection = routeCollection;
            _BusFleet = busFleet;
            _DriverStaff = driverStaff;
            _VolumeOfTransportation = volumeOfTransportation;

            // Настройка скролл-панели
            panelTransportationList.AutoScroll = true;
            panelTransportationList.HorizontalScroll.Visible = false;

            LoadVolumeOfTransportation(); // Загрузка рейсов
        }

        // Загрузка и отображение списка выполненных рейсов
        public void LoadVolumeOfTransportation()
        {
            panelTransportationList.Controls.Clear();

            if (_VolumeOfTransportation.CompletedTransportations == null ||
                _VolumeOfTransportation.CompletedTransportations.Count == 0)
            {
                labelNoTransportation.Visible = true;
                panelTransportationList.Controls.Add(labelNoTransportation);
                return;
            }

            labelNoTransportation.Visible = false;
            int yPosition = 10;

            // Создание панели для каждого рейса
            foreach (var completedTransportation in _VolumeOfTransportation.CompletedTransportations)
            {
                Panel panel = CreateTransportationPanel(completedTransportation, yPosition);
                panelTransportationList.Controls.Add(panel);
                yPosition += panel.Height + 10;
            }
        }

        // Создание карточки рейса
        private Panel CreateTransportationPanel(CompletedTransportation completedTransportation, int yPosition)
        {
            Panel panel = new Panel
            {
                Size = new Size(panelTransportationList.Width - 25, 120),
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = completedTransportation // Ссылка на объект рейса
            };

            // Информация о рейсе
            Label specsLabel = new Label
            {
                Text = completedTransportation.ToString(),
                Location = new Point(0, 0),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Кнопка редактирования
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                Location = new Point(panel.Width - 220, 80),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = completedTransportation
            };
            editButton.Click += (s, e) => EditTransportation(completedTransportation);

            // Кнопка удаления
            Button deleteButton = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = completedTransportation
            };
            deleteButton.Click += (s, e) => DeleteTransportation(completedTransportation);

            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        // Открытие формы редактирования рейса
        private void EditTransportation(CompletedTransportation completedTransportation)
        {
            OpenChildForm(new Forms.EditForms.TransportationEdit(_VolumeOfTransportation,
                _RouteCollection, _BusFleet, _DriverStaff, this, completedTransportation), null);
        }

        // Удаление рейса
        private void DeleteTransportation(CompletedTransportation completed)
        {
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить рейс от {completed.TransportationDate:dd.MM.yyyy}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _VolumeOfTransportation.CompletedTransportations.Remove(completed);
                LoadVolumeOfTransportation(); // Обновление списка

                MessageBox.Show("Рейс успешно удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Открытие дочерней формы
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (ActiveForm != null)
                ActiveForm.Close();

            ActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Top;

            this.panelTransportationList.Controls.Clear();
            this.panelTransportationList.Controls.Add(childForm);
            this.panelTransportationMenuTitle.Visible = false;

            childForm.BringToFront();
            childForm.Show();
        }

        // Открытие формы добавления нового рейса
        private void buttonTransportationAdd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.TransportationEdit(_VolumeOfTransportation,
                _RouteCollection, _BusFleet, _DriverStaff, this), sender);
        }
    }
}
