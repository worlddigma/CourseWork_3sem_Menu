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
    // Форма для отображения и управления списком водителей
    public partial class FormDrivers : Form
    {
        // Приватные поля для хранения данных и управления интерфейсом
        private DriverStaff _DriverStaff;                // Коллекция водителей
        private Form ActiveForm;                        // Текущая активная дочерняя форма
        private VolumeOfTransportation _VolumeOfTransportation; // Коллекция выполненных перевозок

        // Конструктор формы
        public FormDrivers(DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            // Инициализация компонентов формы
            InitializeComponent();

            // Сохраняем переданные коллекции данных
            _DriverStaff = driverStaff;
            _VolumeOfTransportation = volumeOfTransportation;

            // Настраиваем панель для отображения списка водителей
            panelDriversList.AutoScroll = true;              // Включаем вертикальную прокрутку
            panelDriversList.AutoScrollMinSize = new Size(0, 0); // Минимальный размер для скролла
            panelDriversList.VerticalScroll.Visible = true;  // Отображаем вертикальный скроллбар
            panelDriversList.HorizontalScroll.Visible = false; // Скрываем горизонтальный скроллбар
            panelDriversList.AutoScrollMargin = new Size(0, 10); // Отступ для скролла

            // Загружаем и отображаем список водителей
            LoadDriverStaff();
        }
        // Метод для загрузки и отображения списка водителей
        public void LoadDriverStaff()
        {
            // Очищаем панель от предыдущих элементов
            panelDriversList.Controls.Clear();

            // Проверяем, есть ли водители для отображения
            if (_DriverStaff.Drivers == null || _DriverStaff.Drivers.Count == 0)
            {
                // Если водителей нет, показываем сообщение
                labelNoDrivers.Visible = true;
                panelDriversList.Controls.Add(labelNoDrivers);
                return;
            }

            // Скрываем сообщение "нет водителей"
            labelNoDrivers.Visible = false;

            int yPosition = 10; // Начальная позиция для первого элемента

            // Создаем и добавляем панели для каждого водителя
            foreach (var driver in _DriverStaff.Drivers)
            {
                Panel driverPanel = CreateDriverPanel(driver, yPosition);
                panelDriversList.Controls.Add(driverPanel);

                yPosition += driverPanel.Height + 10; // Увеличиваем позицию для следующего элемента
            }
        }

        // Метод для создания панели отдельного водителя
        private Panel CreateDriverPanel(Driver driver, int yPosition)
        {
            // Создаем новую панель для каждого водителя
            Panel panel = new Panel
            {
                Size = new Size(panelDriversList.Width - 25, 120), // Ширина с учетом скроллбара
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = driver // Сохраняем ссылку на объект водителя для доступа к данным
            };

            // Label с информацией о водителе
            Label specsLabel = new Label
            {
                Text = driver.ToString(), // Используем переопределенный метод ToString() класса Driver
                Location = new Point(0, 0),
                AutoSize = true,
                Font = new Font("Arial", 9),
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            // Кнопка редактирования водителя
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                Location = new Point(panel.Width - 220, 80),
                Anchor = AnchorStyles.Right, // Кнопка привязана к правому краю
                BackColor = Color.DarkGray,
                AutoSize = true,
                ForeColor = Color.White,
                Tag = driver // Сохраняем ссылку на водителя
            };
            editButton.Click += (s, e) => EditDriver(driver); // Привязываем обработчик события

            // Кнопка удаления водителя
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
            deleteButton.Click += (s, e) => DeleteDriver(driver); // Привязываем обработчик события

            // Добавляем созданные элементы на панель водителя
            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        // Метод для удаления водителя
        private void DeleteDriver(Driver driver)
        {
            // Списки для хранения связанных данных
            List<string> delTrans = new List<string>();
            List<CompletedTransportation> toDelete = [];

            // Проверяем, есть ли выполненные рейсы с этим водителем
            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                // Находим все рейсы, связанные с удаляемым водителем
                foreach (var toDel in _VolumeOfTransportation.CompletedTransportations)
                {
                    if (toDel.DriverCode == driver)
                    {
                        toDelete.Add(toDel);
                    }
                }

                // Собираем даты найденных рейсов для отображения в сообщении
                foreach (var del in toDelete)
                {
                    delTrans.Add(del.TransportationDate.ToString("dd.MM.yyyy"));
                }
            }

            // Формируем сообщение с информацией о связанных рейсах
            string relatedTripsMessage = delTrans.Count != 0
                ? $"\nБудут также удалены рейсы: \n{string.Join("\n", delTrans)}"
                : "";

            // Запрашиваем подтверждение у пользователя
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить водителя {driver.Id}?{relatedTripsMessage}",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Если пользователь подтвердил удаление
            if (result == DialogResult.Yes)
            {
                // Удаляем связанные рейсы
                if (delTrans.Count != 0)
                {
                    foreach (var del in toDelete)
                        _VolumeOfTransportation.CompletedTransportations.Remove(del);
                }

                // Удаляем самого водителя из коллекции
                _DriverStaff.Drivers.Remove(driver);

                // Обновляем отображение списка водителей
                LoadDriverStaff();

                // Показываем сообщение об успешном удалении
                MessageBox.Show("Водитель успешно удален", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Метод для открытия формы редактирования водителя
        private void EditDriver(Driver driver)
        {
            // Открываем форму редактирования с передачей выбранного водителя
            OpenChildForm(new Forms.EditForms.DriverEdit(_DriverStaff, this, driver), null);
        }

        // Метод для открытия дочерней формы
        private void OpenChildForm(Form childForm, object btnSender)
        {
            // Закрываем предыдущую активную форму, если она существует
            if (ActiveForm != null)
            {
                ActiveForm.Close();
            }

            // Устанавливаем новую активную форму
            ActiveForm = childForm;

            // Настраиваем свойства дочерней формы
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Top;

            // Очищаем панель и добавляем дочернюю форму
            this.panelDriversList.Controls.Clear();
            this.panelDriversList.Controls.Add(childForm);
            this.panelDriversList.Tag = childForm;

            // Скрываем заголовок меню
            this.panelDriversMenuTitle.Visible = false;

            // Показываем дочернюю форму
            childForm.BringToFront();
            childForm.Show();
        }

        // Обработчик события нажатия на кнопку "Добавить водителя"
        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            // Открываем форму добавления нового водителя
            OpenChildForm(new Forms.EditForms.DriverEdit(_DriverStaff, this), sender);
        }
    }
}