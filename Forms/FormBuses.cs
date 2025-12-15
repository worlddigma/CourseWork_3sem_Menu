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
    // Форма для отображения и управления списком автобусов
    public partial class FormBuses : Form
    {
        // Приватные поля для управления интерфейсом и данных
        private Form ActiveForm;                    // Текущая активная дочерняя форма
        private BusFleet _BusFleet;                // Коллекция автобусов (модель данных)
        private VolumeOfTransportation _VolumeOfTransportation; // Коллекция выполненных перевозок

        // Конструктор формы
        public FormBuses(BusFleet busFleet, VolumeOfTransportation volumeOfTransportation)
        {
            // Сохраняем переданные коллекции данных
            _BusFleet = busFleet;
            _VolumeOfTransportation = volumeOfTransportation;

            // Инициализация компонентов формы (сгенерированных дизайнером)
            InitializeComponent();

            // Настраиваем панель для отображения списка автобусов
            panelBusesList.AutoScroll = true;              // Включаем вертикальную прокрутку
            panelBusesList.AutoScrollMinSize = new Size(0, 0); // Минимальный размер для скролла
            panelBusesList.VerticalScroll.Visible = true; // Отображаем вертикальный скроллбар
            panelBusesList.HorizontalScroll.Visible = false; // Скрываем горизонтальный скроллбар
            panelBusesList.AutoScrollMargin = new Size(0, 10); // Отступ для скролла

            // Загружаем и отображаем список автобусов
            LoadBusesFleet();
        }

        // Метод для загрузки и отображения списка автобусов
        public void LoadBusesFleet()
        {
            // Приостанавливаем перерисовку панели для предотвращения мерцания
            panelBusesList.SuspendLayout();

            try
            {
                // Очищаем панель от предыдущих элементов
                panelBusesList.Controls.Clear();

                // Проверяем, есть ли автобусы для отображения
                if (_BusFleet.Buses == null || _BusFleet.Buses.Count == 0)
                {
                    // Если автобусов нет, показываем сообщение
                    labelNoBuses.Visible = true;
                    panelBusesList.Controls.Add(labelNoBuses);
                    return;
                }

                // Скрываем сообщение "нет автобусов"
                labelNoBuses.Visible = false;

                int yPosition = 10; // Начальная позиция для первого элемента

                // Ограничиваем количество отображаемых элементов для производительности
                int maxToShow = 50; // Показываем только 50 автобусов
                int countToShow = Math.Min(maxToShow, _BusFleet.Buses.Count);

                // Создаем и добавляем панели для каждого автобуса
                for (int i = 0; i < countToShow; i++)
                {
                    var bus = _BusFleet.Buses[i];
                    Panel busPanel = CreateBusPanel(bus, yPosition);
                    panelBusesList.Controls.Add(busPanel);
                    yPosition += busPanel.Height + 10; // Увеличиваем позицию для следующего элемента
                }

                // Если автобусов больше, чем максимальное количество для отображения
                // Показываем информационное сообщение
                if (_BusFleet.Buses.Count > maxToShow)
                {
                    var labelMore = new Label
                    {
                        Text = $"Показано: {countToShow} из {_BusFleet.Buses.Count} автобусов",
                        Location = new Point(10, yPosition + 10),
                        AutoSize = true,
                        Font = new Font("Arial", 9, FontStyle.Italic),
                        ForeColor = Color.Gray
                    };
                    panelBusesList.Controls.Add(labelMore);
                }
            }
            finally
            {
                // Возобновляем перерисовку панели
                panelBusesList.ResumeLayout(true);
            }
        }

        // Метод для создания панели отдельного автобуса
        private Panel CreateBusPanel(Bus bus, int yPosition)
        {
            // Создаем новую панель для каждого автобуса
            Panel panel = new Panel
            {
                Size = new Size(panelBusesList.Width - 25, 120), // Ширина с учетом скроллбара
                Location = new Point(10, yPosition),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Tag = bus // Сохраняем ссылку на объект автобуса для доступа к данным
            };

            // PictureBox для отображения изображения автобуса
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(150, 100),
                Location = new Point(10, 10),
                SizeMode = PictureBoxSizeMode.Zoom, // Изображение масштабируется с сохранением пропорций
                BorderStyle = BorderStyle.FixedSingle
            };

            // Загружаем изображение автобуса, если путь к файлу указан и файл существует
            if (!string.IsNullOrEmpty(bus.Photo) && System.IO.File.Exists(bus.Photo))
            {
                pictureBox.Image = Image.FromFile(bus.Photo);
            }
            else
            {
                // Создаем заглушку, если изображение отсутствует
                CreateImagePlaceholder(pictureBox);
            }

            // Label с информацией об автобусе
            Label specsLabel = new Label
            {
                Text = bus.ToString(), // Используем переопределенный метод ToString() класса Bus
                Location = new Point(170, 10),
                AutoSize = true,
                Font = new Font("Arial", 9)
            };

            // Кнопка редактирования автобуса
            Button editButton = new Button
            {
                Text = "Редактировать",
                Size = new Size(110, 30),
                Location = new Point(panel.Width - 220, 80),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Right, // Кнопка привязана к правому краю
                AutoSize = true,
                Tag = bus // Сохраняем ссылку на автобус
            };
            editButton.Click += (s, e) => EditBus(bus); // Привязываем обработчик события

            // Кнопка удаления автобуса
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
            deleteButton.Click += (s, e) => DeleteBus(bus); // Привязываем обработчик события

            // Добавляем созданные элементы на панель автобуса
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(specsLabel);
            panel.Controls.Add(editButton);
            panel.Controls.Add(deleteButton);

            return panel;
        }

        // Статический метод для создания заглушки изображения
        public static void CreateImagePlaceholder(PictureBox pictureBox)
        {
            // Создаем новое растровое изображение нужного размера
            Bitmap placeholder = new Bitmap(pictureBox.Width, pictureBox.Height);

            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.LightGray); // Заливаем светло-серым цветом

                // Настраиваем формат текста (выравнивание по центру)
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                // Рисуем текст на изображении
                g.DrawString("Нет изображения",
                    new Font("Arial", 8),
                    Brushes.DarkGray,
                    new RectangleF(0, 0, placeholder.Width, placeholder.Height),
                    format);
            }

            pictureBox.Image = placeholder;
        }

        // Метод для открытия формы редактирования автобуса
        private void EditBus(Bus bus)
        {
            // Открываем форму редактирования с передачей выбранного автобуса
            OpenChildForm(new Forms.EditForms.BusEdit(_BusFleet, this, bus), null);
        }

        // Метод для удаления автобуса
        private void DeleteBus(Bus bus)
        {
            // Списки для хранения связанных данных
            List<string> delTrans = new List<string>();
            List<CompletedTransportation> toDelete = [];

            // Проверяем, есть ли выполненные рейсы с этим автобусом
            if (_VolumeOfTransportation.CompletedTransportations.Count != 0)
            {
                // Находим все рейсы, связанные с удаляемым автобусом
                foreach (var completed in _VolumeOfTransportation.CompletedTransportations)
                {
                    if (completed.Bus == bus)
                    {
                        toDelete.Add(completed);
                    }
                }

                // Собираем даты найденных рейсов для отображения в сообщении
                foreach (var del in toDelete)
                {
                    delTrans.Add(del.TransportationDate.ToString("dd.MM.yyyy"));
                }
            }

            // Формируем сообщение в зависимости от наличия связанных рейсов
            string message;
            if (delTrans.Count > 0)
            {
                message = $"Вы уверены, что хотите удалить автобус {bus.StateNumber}?\n\n" +
                          $"Будут также удалены рейсы:\n{string.Join("\n", delTrans)}";
            }
            else
            {
                message = $"Вы уверены, что хотите удалить автобус {bus.StateNumber}?";
            }

            // Запрашиваем подтверждение у пользователя
            DialogResult result = MessageBox.Show(
                message,
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Если пользователь подтвердил удаление
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

                // Удаляем сам автобус из коллекции
                _BusFleet.Buses.Remove(bus);

                // Обновляем отображение списка автобусов
                LoadBusesFleet();

                // Формируем сообщение об успешном удалении
                string successMessage = "Автобус успешно удален";
                if (delTrans.Count > 0)
                {
                    successMessage += $". Также удалено {delTrans.Count} рейсов";
                }

                MessageBox.Show(successMessage, "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
            this.panelBusesList.Controls.Clear();
            this.panelBusesList.Controls.Add(childForm);
            this.panelBusesList.Tag = childForm;

            // Скрываем заголовок меню
            this.panelBusesMenuTitle.Visible = false;

            // Показываем дочернюю форму
            childForm.BringToFront();
            childForm.Show();
        }

        // Обработчик события нажатия на кнопку "Добавить автобус"
        private void buttonBusAdd_Click(object sender, EventArgs e)
        {
            // Открываем форму добавления нового автобуса
            OpenChildForm(new Forms.EditForms.BusEdit(_BusFleet, this), sender);
        }
    }
}