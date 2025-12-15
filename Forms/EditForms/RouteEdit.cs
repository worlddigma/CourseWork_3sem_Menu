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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CourseWork_3sem_Menu.Forms.EditForms
{
    // Форма для добавления/редактирования маршрута
    public partial class RouteEdit : Form
    {
        private RouteCollection _RouteCollection;  // Коллекция маршрутов
        private FormRoutes parentForm;             // Родительская форма
        private Route _Route;                      // Редактируемый маршрут (null при добавлении)

        // Сопоставление русских названий дней недели с enum DayOfWeek
        private Dictionary<string, DayOfWeek> _russianDaysMapping = new()
    {
        { "Понедельник", DayOfWeek.Monday },
        { "Вторник", DayOfWeek.Tuesday },
        { "Среда", DayOfWeek.Wednesday },
        { "Четверг", DayOfWeek.Thursday },
        { "Пятница", DayOfWeek.Friday },
        { "Суббота", DayOfWeek.Saturday },
        { "Воскресенье", DayOfWeek.Sunday }
    };

        // Конструктор для редактирования существующего маршрута
        public RouteEdit(RouteCollection RouteCollection, FormRoutes parent, Route Route)
        {
            InitializeComponent();
            _RouteCollection = RouteCollection;
            parentForm = parent;
            _Route = Route;

            // Заполнение полей данными редактируемого маршрута
            textBoxCode.Text = _Route.Code;
            textBoxStartingPoint.Text = _Route.StartingPoint;
            textBoxEndingPoint.Text = _Route.EndingPoint;

            // Промежуточные пункты (через запятую)
            textBoxIntermediatePoints.Text = _Route.IntermediatePoints != null
                ? string.Join(", ", _Route.IntermediatePoints)
                : string.Empty;

            dateTimePickerDepartureTime.Value = _Route.DepartureTime;

            // Время транспортировки (дни + часы/минуты)
            textBoxTransportationTimeDay.Text = ((int)_Route.TransportationTime.TotalDays).ToString();
            dateTimePickerTransportationTimeHours.Value = DateTime.Today.Add(_Route.TransportationTime - TimeSpan.FromDays((int)_Route.TransportationTime.TotalDays));

            // Установка дней отправления
            for (int i = 0; i < checkedListBoxDepartureDays.Items.Count; i++)
            {
                string russianDayName = checkedListBoxDepartureDays.Items[i].ToString();
                if (_russianDaysMapping.TryGetValue(russianDayName, out DayOfWeek day))
                {
                    checkedListBoxDepartureDays.SetItemChecked(i, _Route.DepartureDays.Contains(day));
                }
            }
        }

        // Конструктор для добавления нового маршрута
        public RouteEdit(RouteCollection RouteCollection, FormRoutes parent)
        {
            InitializeComponent();
            _RouteCollection = RouteCollection;
            parentForm = parent;

            // Значения по умолчанию
            textBoxTransportationTimeDay.Text = "0";
            dateTimePickerTransportationTimeHours.Value = DateTime.Today.AddHours(1);
            dateTimePickerDepartureTime.Value = DateTime.Today.AddHours(8);
        }

        // Сохранение изменений
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация обязательных полей
                if (string.IsNullOrWhiteSpace(textBoxCode.Text) ||
                    string.IsNullOrWhiteSpace(textBoxStartingPoint.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEndingPoint.Text))
                {
                    throw new Exception("Заполните все обязательные поля.");
                }

                // Валидация через статические методы класса Route
                Route.IsValidCode(textBoxCode.Text.Trim());
                Route.IsValidPoint(textBoxStartingPoint.Text.Trim(), "Начальный пункт");
                Route.IsValidPoint(textBoxEndingPoint.Text.Trim(), "Конечный пункт");

                // Получение и валидация промежуточных пунктов
                List<string> intermediatePoints = GetIntermediatePoints();
                Route.IsValidIntermediatePoints(intermediatePoints);

                // Получение и валидация дней отправления
                List<DayOfWeek> departureDays = GetDepartureDays();
                Route.IsValidDepartureDays(departureDays);

                // Получение и валидация времени транспортировки
                if (!int.TryParse(textBoxTransportationTimeDay.Text, out int days) || days < 0)
                    throw new Exception("Дни транспортировки должны быть неотрицательным числом.");

                TimeSpan hoursPart = dateTimePickerTransportationTimeHours.Value.TimeOfDay;
                TimeSpan transportationTime = TimeSpan.FromDays(days).Add(hoursPart);
                Route.IsValidTransportationTime(transportationTime);

                // Валидация времени отправления
                DateTime departureTime = dateTimePickerDepartureTime.Value;
                Route.IsValidDepartureTime(departureTime);

                // Проверка уникальности шифра маршрута (при добавлении)
                string code = textBoxCode.Text.Trim();
                if (_Route == null && _RouteCollection.Routes.Any(r => r.Code.Equals(code, StringComparison.OrdinalIgnoreCase)))
                    throw new Exception("Маршрут с таким шифром уже существует.");

                if (_Route == null) // Добавление нового маршрута
                {
                    Route route = new Route(code, textBoxStartingPoint.Text.Trim(),
                        textBoxEndingPoint.Text.Trim(), intermediatePoints, departureDays,
                        transportationTime, departureTime);
                    _RouteCollection.Routes.Add(route);
                    MessageBox.Show("Маршрут успешно добавлен!", "Успех");
                }
                else // Редактирование существующего
                {
                    _Route.Code = code;
                    _Route.StartingPoint = textBoxStartingPoint.Text.Trim();
                    _Route.EndingPoint = textBoxEndingPoint.Text.Trim();
                    _Route.IntermediatePoints = intermediatePoints;
                    _Route.DepartureDays = departureDays;
                    _Route.TransportationTime = transportationTime;
                    _Route.DepartureTime = departureTime;
                    MessageBox.Show("Маршрут успешно изменен!", "Успех");
                }

                // Закрытие формы и обновление родительской формы
                parentForm.LoadRouteCollection();
                parentForm.panelRoutesMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Отмена изменений
        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            parentForm.LoadRouteCollection();
            parentForm.panelRoutesMenuTitle.Show();
            this.Close();
        }

        // Получение списка промежуточных пунктов из текстового поля
        private List<string> GetIntermediatePoints()
        {
            if (string.IsNullOrWhiteSpace(textBoxIntermediatePoints.Text))
                return new List<string>();

            return textBoxIntermediatePoints.Text
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();
        }

        // Получение списка дней отправления из CheckedListBox
        private List<DayOfWeek> GetDepartureDays()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            foreach (string checkedItem in checkedListBoxDepartureDays.CheckedItems)
            {
                if (_russianDaysMapping.TryGetValue(checkedItem, out DayOfWeek day))
                    days.Add(day);
            }
            return days;
        }

        // Обработчики валидации ввода

        private void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, цифры и Enter для перехода между полями
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
                textBoxStartingPoint.Focus();
        }

        private void textBoxStartingPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
                textBoxEndingPoint.Focus();
        }

        private void textBoxEndingPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
                textBoxIntermediatePoints.Focus();
        }

        private void textBoxIntermediatePoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, цифры, запятые и пробелы для списка пунктов
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != ' ')
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Enter)
                dateTimePickerDepartureTime.Focus();
        }

        private void textBoxTransportationTimeDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры для дней транспортировки
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxTransportationTimeDay.Text + e.KeyChar;
            if (int.TryParse(newText, out int days) && days > Route.Constants.MaxTransportationDays)
                e.Handled = true;
        }
    }
}