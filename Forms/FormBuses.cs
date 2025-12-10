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
    public partial class FormBuses : Form
    {
        private Form ActiveForm;
        private BusFleet _BusFleet;
        public FormBuses(BusFleet busFleet)
        {
            _BusFleet = busFleet;
            InitializeComponent();
            LoadBusesFleet();
        }
        public void LoadBusesFleet()
        {
            panelBusesList.Controls.Clear();

            if (_BusFleet.Buses == null || _BusFleet.Buses.Count == 0)
            {
                labelNoBuses.Visible = true;
                panelBusesList.Controls.Add(labelNoBuses);
                return;
            }

            labelNoBuses.Visible = false;

            int yPosition = 10; // Начальная позиция

            foreach (var bus in _BusFleet.Buses)
            {
                // Создаем НОВУЮ панель для каждого автобуса
                Panel busPanel = CreateBusPanel(bus, yPosition);
                panelBusesList.Controls.Add(busPanel);

                yPosition += busPanel.Height + 10; // Отступ между автобусами
            }
        }

        private Panel CreateBusPanel(Bus bus, int yPosition)
        {
            // Создаем новую панель для каждого автобуса
            Panel panel = new Panel
            {
                Size = new Size(panelBusesList.Width - 25, 120),
                Location = new Point(10, yPosition),
                AutoSize = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Tag = bus // Сохраняем ссылку на автобус
            };

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
                CreateImagePlaceholder(pictureBox);
            }

            // Информация об автобусе
            Label specsLabel = new Label
            {
                Text = bus.ToString(),
                Location = new Point(170, 10),
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
                Anchor = AnchorStyles.Right,
                AutoSize = true,
                Tag = bus

            };
            editButton.Click += (s, e) => EditBus(bus);

            // Кнопка удаления
            Button deleteButton = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 30),
                Anchor = AnchorStyles.Right,
                Location = new Point(panel.Width - 110, 80),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = bus
            };
            deleteButton.Click += (s, e) => DeleteBus(bus);

            // Добавляем элементы на панель
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        public static void CreateImagePlaceholder(PictureBox pictureBox)
        {
            Bitmap placeholder = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.LightGray);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("Нет изображения",
                    new Font("Arial", 8),
                    Brushes.DarkGray,
                    new RectangleF(0, 0, placeholder.Width, placeholder.Height),
                    format);
            }
            pictureBox.Image = placeholder;
        }

        private void EditBus(Bus bus)
        {
            OpenChildForm(new Forms.EditForms.BusEdit(_BusFleet, this, bus), null);
        }

        private void DeleteBus(Bus bus)
        {
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить автобус {bus.StateNumber}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _BusFleet.Buses.Remove(bus);
                LoadBusesFleet(); // Обновляем список
                MessageBox.Show("Автобус успешно удален", "Успех",
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
            this.panelBusesList.Controls.Clear();
            this.panelBusesList.Controls.Add(childForm);
            this.panelBusesList.Tag = childForm;
            this.panelBusesMenuTitle.Visible = false;
            childForm.BringToFront();
            childForm.Show();
        }



        private void buttonBusAdd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.BusEdit(_BusFleet, this), sender);
            

        }

    }
}
