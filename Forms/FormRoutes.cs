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
        public FormRoutes(RouteCollection routeCollection)
        {
            InitializeComponent();
            _RouteCollection = routeCollection;
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
                AutoSize = true,
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
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить маршрут {route.Code}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _RouteCollection.Routes.Remove(route);
                LoadRouteCollection(); // Обновляем список
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

        private void panelRoutesMenuTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRoutesMenu_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
