using CourseWork_3sem;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_3sem_Menu.Forms
{
    public partial class FormDrivers : Form
    {
        private DriverStaff _DriverStaff;
        private Form ActiveForm;
        private VolumeOfTransportation _VolumeOfTransportation;
        public FormDrivers(DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _DriverStaff = driverStaff;
           _VolumeOfTransportation = volumeOfTransportation;

            // Настраиваем панель для скролла
            panelDriversList.AutoScroll = true;
            panelDriversList.AutoScrollMinSize = new Size(0, 0);
            panelDriversList.VerticalScroll.Visible = true;
            panelDriversList.HorizontalScroll.Visible = false;
            panelDriversList.AutoScrollMargin = new Size(0, 10);
            LoadDriverStaff();
        }
        public void LoadDriverStaff()
        {
            panelDriversList.Controls.Clear();

            if (_DriverStaff.Drivers == null || _DriverStaff.Drivers.Count == 0)
            {
                labelNoDrivers.Visible = true;
                panelDriversList.Controls.Add(labelNoDrivers);
                return;
            }

            labelNoDrivers.Visible = false;

            int yPosition = 10; // Начальная позиция

            foreach (var driver in _DriverStaff.Drivers)
            {
                Panel driverPanel = CreateDriverPanel(driver, yPosition);
            panelDriversList.Controls.Add(driverPanel);

                yPosition += driverPanel.Height + 10; // Отступ 
            }
        }
        private Panel CreateDriverPanel(Driver driver, int yPosition)
        {
            // Создаем новую панель для каждого автобуса
            Panel panel = new Panel
            {
                Size = new Size(panelDriversList.Width - 25, 120),
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = driver // Сохраняем ссылку на автобус
            };

            // Информация о маршруте
            Label specsLabel = new Label
            {
                Text = driver.ToString(),
                Location = new Point(0,0),
                AutoSize = true,
                Font = new Font("Arial", 9),
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            // Кнопка редактирования
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                Location = new Point(panel.Width - 220, 80),
                Anchor = AnchorStyles.Right,
                BackColor = Color.DarkGray,
                AutoSize = true,
                ForeColor = Color.White,
                Tag = driver
            };
            editButton.Click += (s, e) => EditDriver(driver);

            // Кнопка удаления
            Button deleteButton = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
                Anchor = AnchorStyles.Right,
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = driver
            };
            deleteButton.Click += (s, e) => DeleteDriver(driver);

            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        private void DeleteDriver(Driver driver)
        {
            List<string> delTrans = new List<string>();
            List<CompletedTransportation> toDelete = [];
            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                foreach (var toDel in _VolumeOfTransportation.CompletedTransportations)
                {
                    if (toDel.DriverCode == driver)
                    {
                        toDelete.Add(toDel);
                    }
                }
                foreach (var del in toDelete)
                {
                    delTrans.Add(del.TransportationDate.ToString("dd.MM.yyyy"));
                }
            }
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить водителя {driver.Id}? {(delTrans.Count != 0 ? $"\nБудут также удалены рейсы: \n{string.Join("\n", delTrans)}" : "")}",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (delTrans.Count != 0) foreach (var del in toDelete) _VolumeOfTransportation.CompletedTransportations.Remove(del);
                _DriverStaff.Drivers.Remove(driver);
                LoadDriverStaff(); // Обновляем список
                MessageBox.Show("Водитель успешно удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditDriver(Driver driver)
        {
            OpenChildForm(new Forms.EditForms.DriverEdit(_DriverStaff, this, driver), null);
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
            this.panelDriversList.Controls.Clear();
            this.panelDriversList.Controls.Add(childForm);
            this.panelDriversList.Tag = childForm;
            this.panelDriversMenuTitle.Visible = false;
            childForm.BringToFront();
            childForm.Show();
        }
        
        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.DriverEdit(_DriverStaff, this), sender);
        }
    }
}
