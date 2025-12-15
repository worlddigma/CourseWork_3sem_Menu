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
    // Форма для управления маршрутами
    public partial class FormRoutes : Form
    {
        private RouteCollection _RouteCollection;           // Список всех маршрутов
        private Form ActiveForm;                            // Текущая дочерняя форма
        private VolumeOfTransportation _VolumeOfTransportation; // Список выполненных рейсов

        public FormRoutes(RouteCollection routeCollection, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();
            _RouteCollection = routeCollection;
            _VolumeOfTransportation = volumeOfTransportation;

            // Настройка скролл-панели
            panelRoutesList.AutoScroll = true;
            panelRoutesList.HorizontalScroll.Visible = false;

            LoadRouteCollection(); // Загрузка маршрутов
        }

        // Загрузка и отображение списка маршрутов
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
            int yPosition = 10;

            // Создание панели для каждого маршрута
            foreach (var route in _RouteCollection.Routes)
            {
                Panel routePanel = CreateRoutePanel(route, yPosition);
                panelRoutesList.Controls.Add(routePanel);
                yPosition += routePanel.Height + 10;
            }
        }

        // Создание карточки маршрута
        public Panel CreateRoutePanel(Route route, int yPosition)
        {
            Panel panel = new Panel
            {
                Size = new Size(panelRoutesList.Width - 25, 120),
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = route // Ссылка на объект маршрута
            };

            // Информация о маршруте
            Label specsLabel = new Label
            {
                Text = route.ToString(),
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
                Tag = route
            };
            editButton.Click += (s, e) => EditRoute(route);

            // Кнопка удаления
            Button deleteButton = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 30),
                Location = new Point(panel.Width - 110, 80),
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

        // Открытие формы редактирования маршрута
        private void EditRoute(Route route)
        {
            OpenChildForm(new Forms.EditForms.RouteEdit(_RouteCollection, this, route), null);
        }

        // Удаление маршрута с проверкой связанных рейсов
        private void DeleteRoute(Route route)
        {
            // Поиск рейсов с этим маршрутом
            List<CompletedTransportation> toDelete = _VolumeOfTransportation.CompletedTransportations
                .Where(ct => ct.RouteCode == route)
                .ToList();

            List<string> delTrans = toDelete
                .Select(d => d.TransportationDate.ToString("dd.MM.yyyy"))
                .ToList();

            // Запрос подтверждения
            string message = $"Вы уверены, что хотите удалить маршрут {route.Code}?";
            if (delTrans.Count > 0)
                message += $"\n\nБудут также удалены рейсы:\n{string.Join("\n", delTrans)}";

            DialogResult result = MessageBox.Show(message, "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Удаление связанных рейсов
                foreach (var del in toDelete)
                    _VolumeOfTransportation.CompletedTransportations.Remove(del);

                // Удаление маршрута
                _RouteCollection.Routes.Remove(route);

                // Обновление списка
                LoadRouteCollection();

                // Сообщение об успехе
                string successMessage = "Маршрут успешно удален";
                if (delTrans.Count > 0)
                    successMessage += $". Также удалено {delTrans.Count} рейсов";

                MessageBox.Show(successMessage, "Успех",
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

            this.panelRoutesList.Controls.Clear();
            this.panelRoutesList.Controls.Add(childForm);
            this.panelRoutesMenuTitle.Visible = false;

            childForm.BringToFront();
            childForm.Show();
        }

        // Открытие формы добавления нового маршрута
        private void buttonRoutesAdd_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.EditForms.RouteEdit(_RouteCollection, this), sender);
        }
    }
}
