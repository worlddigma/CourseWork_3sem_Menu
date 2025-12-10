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
{
    public partial class FormTransportation : Form
    {
        private RouteCollection _RouteCollection;
        private DriverStaff _DriverStaff;
        private BusFleet _BusFleet;
        private VolumeOfTransportation _VolumeOfTransportation;
        private Form ActiveForm;

        public FormTransportation(RouteCollection routeCollection, BusFleet busFleet,
            DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _RouteCollection = routeCollection;
            _BusFleet = busFleet;
            _DriverStaff = driverStaff;
            _VolumeOfTransportation = volumeOfTransportation;
            LoadVolumeOfTransportation();
        }

        public void LoadVolumeOfTransportation()
        {
            panelTransportationList.Controls.Clear();

            if (_VolumeOfTransportation.CompletedTransportations == null ||_VolumeOfTransportation.CompletedTransportations.Count == 0)
            {
                labelNoTransportation.Visible = true;
                panelTransportationList.Controls.Add(labelNoTransportation);
                return;
            }

            labelNoTransportation.Visible = false;

            int yPosition = 10; // Начальная позиция

            foreach (var completedTransportation in _VolumeOfTransportation.CompletedTransportations)
            {
                Panel completedTransportationPanel = CreateTransportationPanel(completedTransportation, yPosition);
                panelTransportationList.Controls.Add(completedTransportationPanel);

                yPosition += completedTransportationPanel.Height + 10; // Отступ 
            }
        }
        private Panel CreateTransportationPanel(CompletedTransportation completedTransportation, int yPosition)
        {
            // Создаем новую панель для 
            Panel panel = new Panel
            {
                Size = new Size(panelTransportationList.Width - 25, 120),
                Location = new Point(10, yPosition),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BackColor = Color.White,
                AutoSize = true,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = completedTransportation // Сохраняем ссылку 
            };

            // Информация о маршруте
            Label specsLabel = new Label
            {
                Text = completedTransportation.ToString(),
                Location = new Point(5, yPosition-10),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Кнопка редактирования
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                Location = new Point(panel.Width - 220, 80),
                Anchor = AnchorStyles.Right,
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
                Anchor = AnchorStyles.Right,
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
        private void EditTransportation(CompletedTransportation completedTransporation)
        {
            OpenChildForm(new Forms.EditForms.TransportationEdit(_VolumeOfTransportation, _RouteCollection, _BusFleet, _DriverStaff, this, completedTransporation), null);
        }

        private void DeleteTransportation(CompletedTransportation completed)
        {
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить рейс {completed.TransportationDate.Date}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _VolumeOfTransportation.CompletedTransportations.Remove(completed);
                LoadVolumeOfTransportation(); // Обновляем список
                MessageBox.Show("Рейс успешно удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (ActiveForm != null)
            {
                ActiveForm.Close();
            }
            ActiveForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Top;
            this.panelTransportationList.Controls.Clear();
            this.panelTransportationList.Controls.Add(childForm);
            this.panelTransportationList.Tag = childForm;
            this.panelTransportationMenuTitle.Visible = false;
            childForm.BringToFront();
            childForm.Show();
        }
        private void buttonTransportationAdd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.TransportationEdit(_VolumeOfTransportation, _RouteCollection, _BusFleet, _DriverStaff, this), sender);
        }

    }
}
