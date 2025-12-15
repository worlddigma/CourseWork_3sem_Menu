using CourseWork_3sem;
using CourseWork_3sem_Menu.Forms.EditForms;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWork_3sem_Menu
{
    public partial class StartingMenu : Form
    {
        // Приватные поля для управления интерфейсом
        private Button _CurrentButton;        // Текущая активная кнопка меню
        private Form _ActiveForm;             // Текущая активная дочерняя форма
        private BusFleet _BusFleet;           // Коллекция автобусов
        private RouteCollection _RouteCollection; // Коллекция маршрутов
        private DriverStaff _DriverStaff;     // Штат водителей
        private VolumeOfTransportation _VolumeOfTransportation; // Объем перевозок

        // Конструктор главного меню
        public StartingMenu(BusFleet busFleet, RouteCollection routeCollection,
                          DriverStaff driverStaff, VolumeOfTransportation volumeOfTransportation)
        {
            InitializeComponent();

            // Инициализация коллекций данных
            _BusFleet = busFleet;
            _RouteCollection = routeCollection;
            _DriverStaff = driverStaff;
            _VolumeOfTransportation = volumeOfTransportation;

            // Устанавливаем размер формы
            this.Size = new Size(900, 500);
        }

        // Метод для активации кнопки меню
        // Изменяет внешний вид активной кнопки
        private void ActivateButton(object btnSender)
        {
            // Проверяем, что параметр не null
            if (btnSender != null)
            {
                // Сбрасываем стиль всех кнопок
                DisableButton();

                // Устанавливаем новый стиль для активной кнопки
                if (_CurrentButton != (Button)btnSender)
                {
                    _CurrentButton = (Button)btnSender;
                    _CurrentButton.BackColor = Color.Gray;        // Серый фон
                    _CurrentButton.ForeColor = Color.White;       // Белый текст
                    _CurrentButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
                }
            }
        }

        // Метод для сброса стиля всех кнопок меню
        // Возвращает кнопки к стандартному виду
        private void DisableButton()
        {
            // Проходим по всем элементам панели меню
            foreach (Control previousButton in panelMenu.Controls)
            {
                // Устанавливаем стандартный стиль
                previousButton.BackColor = Color.DarkGray;    // Темно-серый фон
                previousButton.ForeColor = Color.Black;       // Черный текст
                previousButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            }
        }

        // Метод для открытия дочерней формы
        public void OpenChildForm(Form childForm, object btnSender)
        {
            // Закрываем предыдущую активную форму, если она существует
            if (_ActiveForm != null)
            {
                _ActiveForm.Close();
            }

            // Активируем кнопку меню
            ActivateButton(btnSender);

            // Устанавливаем новую активную форму
            _ActiveForm = childForm;

            // Настраиваем свойства дочерней формы
            childForm.TopLevel = false;                       // Не верхнеуровневая форма
            childForm.FormBorderStyle = FormBorderStyle.None; // Без рамки
            childForm.Dock = DockStyle.Fill;                  // Заполняет всю панель

            // Добавляем форму на панель Desktop
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;

            // Перемещаем форму на передний план
            childForm.BringToFront();

            // Отображаем форму
            childForm.Show();

            // Обновляем заголовок в соответствии с открытой формой
            labelTitelText.Text = childForm.Text;
        }

        // Обработчик события нажатия на кнопку "Автобусы"
        private void buttonBuses_Click(object sender, EventArgs e)
        {
            // Открываем форму для работы с автобусами
            OpenChildForm(new Forms.FormBuses(_BusFleet, _VolumeOfTransportation), sender);
        }

        // Обработчик события нажатия на кнопку "Маршруты"
        private void buttonRoutes_Click(object sender, EventArgs e)
        {
            // Открываем форму для работы с маршрутами
            OpenChildForm(new Forms.FormRoutes(_RouteCollection, _VolumeOfTransportation), sender);
        }

        // Обработчик события нажатия на кнопку "Водители"
        private void buttonDrivers_Click(object sender, EventArgs e)
        {
            // Открываем форму для работы с водителями
            OpenChildForm(new Forms.FormDrivers(_DriverStaff, _VolumeOfTransportation), sender);
        }

        // Обработчик события нажатия на кнопку "Перевозки"
        private void buttonTransportation_Click(object sender, EventArgs e)
        {
            // Открываем форму для работы с перевозками
            OpenChildForm(new Forms.FormTransportation(_RouteCollection, _BusFleet,
                         _DriverStaff, _VolumeOfTransportation), sender);
        }

        // Обработчик события закрытия формы
        // Автоматически сохраняет данные при закрытии приложения
        private void StartingMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Автоматически сохраняем все данные в файлы
                WriteFile.Write(_BusFleet, _DriverStaff, _RouteCollection, _VolumeOfTransportation);
            }
            catch (Exception ex)
            {
                // В случае ошибки показываем сообщение пользователю
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка сохранения",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}