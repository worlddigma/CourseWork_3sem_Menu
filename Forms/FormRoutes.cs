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
    public partial class FormRoutes : Form
    {
        private RouteCollection _RouteCollection;
        private Form ActiveForm;
        private VolumeOfTransportation _VolumeOfTransportation;
        public FormRoutes(RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _RouteCollection = routeCollection;
            _VolumeOfTransportation = volumeOfTransportation;
            // Настраиваем панель для скролла
            panelRoutesList.AutoScroll = true;
            panelRoutesList.AutoScrollMinSize = new Size(0, 0);
            panelRoutesList.VerticalScroll.Visible = true;
            panelRoutesList.HorizontalScroll.Visible = false;
            panelRoutesList.AutoScrollMargin = new Size(0, 10);
            LoadRouteCollection();
        }

        public void LoadRouteCollection()
        {
            panelRoutesList.Controls.Clear();

            if (_RouteCollection.Routes == null || _RouteCollection.Routes.Count == 0)
            {
                labelNoRoutes.Visible = true;
                panelRoutesList.Controls.Add(labelNoRoutes);
                return;
            }

            labelNoRoutes.Visible = false;

            int yPosition = 10; // Начальная позиция

            foreach (var route in _RouteCollection.Routes)
            {
                Panel routePanel = CreateRoutePanel(route, yPosition);
                panelRoutesList.Controls.Add(routePanel);

                yPosition += routePanel.Height + 10; // Отступ 
            }
        }
        public Panel CreateRoutePanel(Route route, int yPosition)
        {

            // Создаем новую панель для каждого автобуса
            Panel panel = new Panel
            {
                Size = new Size(panelRoutesList.Width - 25, 120),
                Location = new Point(10, yPosition),
                AutoSize = false,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = route // Сохраняем ссылку на автобус
            };

            // Информация о маршруте
            Label specsLabel = new Label
            {
                Text = route.ToString(),
                Location = new Point(0, 0),
                AutoSize = true,
                Font = new Font("Arial", 9),
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };

            // Кнопка редактирования
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                AutoSize = true,
                Location = new Point(panel.Width - 220, 80),
                Anchor = AnchorStyles.Right,
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = route
            };
            editButton.Click += (s, e) => EditRoute(route);

            // Кнопка удаления
            Button deleteButton = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
                Anchor = AnchorStyles.Right,
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Tag = route
            };
            deleteButton.Click += (s, e) => DeleteRoute(route);

            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }
        private void EditRoute(Route route)
        {
            OpenChildForm(new Forms.EditForms.RouteEdit(_RouteCollection, this, route), null);
        }

        private void DeleteRoute(Route route)
        {
            List<string> delTrans = new List<string>();
            List<CompletedTransportation> toDelete = [];

            // Проверяем, есть ли рейсы с этим маршрутом
            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                foreach (var completed in _VolumeOfTransportation.CompletedTransportations)
                {
                    if (completed.RouteCode == route)
                    {
                        toDelete.Add(completed);
                    }
                }

                // Собираем даты рейсов для отображения
                foreach (var del in toDelete)
                {
                    delTrans.Add(del.TransportationDate.ToString("dd.MM.yyyy"));
                }
            }

            // Создаем сообщение в зависимости от наличия связанных рейсов
            string message;
            if (delTrans.Count > 0)
            {
                message = $"Вы уверены, что хотите удалить маршрут {route.Code}?\n\n" +
                          $"Будут также удалены рейсы:\n{string.Join("\n", delTrans)}";
            }
            else
            {
                message = $"Вы уверены, что хотите удалить маршрут {route.Code}?";
            }

            DialogResult result = MessageBox.Show(
                message,
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Удаляем связанные рейсы
                if (toDelete.Count != 0)
                {
                    foreach (var del in toDelete)
                    {
                        _VolumeOfTransportation.CompletedTransportations.Remove(del);
                    }
                }

                // Удаляем сам маршрут
                _RouteCollection.Routes.Remove(route);
                LoadRouteCollection(); // Обновляем список

                // Сообщение об успехе с информацией об удаленных рейсах
                string successMessage = "Маршрут успешно удален";
                if (delTrans.Count > 0)
                {
                    successMessage += $". Также удалено {delTrans.Count} рейсов";
                }

                MessageBox.Show(successMessage, "Успех",
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
            this.panelRoutesList.Controls.Clear();
            this.panelRoutesList.Controls.Add(childForm);
            this.panelRoutesList.Tag = childForm;
            this.panelRoutesMenuTitle.Visible = false;
            childForm.BringToFront();
            childForm.Show();
        }
        private void buttonRoutesAdd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.RouteEdit(_RouteCollection, this), sender);
        }

    }
}
