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
    public partial class RouteEdit : Form
    {
        private RouteCollection _RouteCollection;
        private FormRoutes parentForm;
        private Route _Route;
        private Dictionary<string, DayOfWeek> _russianDaysMapping = new Dictionary<string, DayOfWeek>
{
    { "Понедельник", DayOfWeek.Monday },
    { "Вторник", DayOfWeek.Tuesday },
    { "Среда", DayOfWeek.Wednesday },
    { "Четверг", DayOfWeek.Thursday },
    { "Пятница", DayOfWeek.Friday },
    { "Суббота", DayOfWeek.Saturday },
    { "Воскресенье", DayOfWeek.Sunday }
};
        public RouteEdit(RouteCollection RouteCollection, FormRoutes parent, Route Route)
        {
            InitializeComponent();
            _RouteCollection = RouteCollection;
            parentForm = parent;
            _Route = Route;
            textBoxCode.Text = _Route.Code;
            textBoxStartingPoint.Text = _Route.StartingPoint;
            textBoxEndingPoint.Text = _Route.EndingPoint;
            textBoxIntermediatePoints.Text = _Route.IntermediatePoints.ToArray().ToString();
            dateTimePickerDepartureTime.Value = _Route.DepartureTime;
            dateTimePickerTransportationTimeHours.Value += _Route.TransportationTime;
            for (int i = 0; i < checkedListBoxDepartureDays.Items.Count; i++)
            {
                string russianDayName = checkedListBoxDepartureDays.Items[i].ToString();
                if (_russianDaysMapping.TryGetValue(russianDayName, out DayOfWeek day))
                {
                    checkedListBoxDepartureDays.SetItemChecked(i, _Route.DepartureDays.Contains(day));
                }
            }
        }
        public RouteEdit(RouteCollection RouteCollection, FormRoutes parent)
        {
            InitializeComponent();
            _RouteCollection = RouteCollection;
            parentForm = parent;
        }


        private void RouteEdit_Load(object sender, EventArgs e)
        {

        }

        private void buttonDisChanges_Click(object sender, EventArgs e)
        {

            this.parentForm.LoadRouteCollection();
            this.parentForm.BringToFront();
            this.parentForm.Show();
            this.parentForm.panelRoutesMenuTitle.Show();
            this.Close();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }


        private void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBoxStartingPoint.Focus();
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxStartingPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBoxEndingPoint.Focus();
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxEndingPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBoxIntermediatePoints.Focus();
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxIntermediatePoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                dateTimePickerDepartureTime.Focus();
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != ' ')
            {
                e.Handled = true;
                return;
            }
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxCode.Text))
                {
                    throw new Exception("Поле с шифром маршрута не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxStartingPoint.Text))
                {
                    throw new Exception("Поле с начальным пунктом не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxEndingPoint.Text))
                {
                    throw new Exception("Поле с конечным пунктом не может быть пустым!");
                }
                if (string.IsNullOrWhiteSpace(textBoxTransportationTimeDay.Text))
                {
                    throw new Exception("Поле с количеством дней не может быть пустым!");
                }

                Route.IsValidCode(textBoxCode.Text.Trim());
                Route.IsValidPoint(textBoxStartingPoint.Text.Trim(), "Начальный пункт");
                Route.IsValidPoint(textBoxEndingPoint.Text.Trim(), "Конечный пункт");

                // Получаем и валидируем промежуточные пункты
                List<string> intermediatePoints = GetIntermediatePoints();
                Route.IsValidIntermediatePoints(intermediatePoints);

                // Получаем и валидируем дни отправления
                List<DayOfWeek> departureDays = GetDepartureDays();
                Route.IsValidDepartureDays(departureDays);

                if (textBoxTransportationTimeDay.Text == null)
                {
                    textBoxTransportationTimeDay.Text = "1";
                }
                // Валидация времени
                TimeSpan transportationTime = dateTimePickerTransportationTimeHours.Value.TimeOfDay + new TimeSpan(0, int.Parse(textBoxTransportationTimeDay.Text), 0);
                DateTime departureTime = dateTimePickerDepartureTime.Value;
                Route.IsValidTransportationTime(transportationTime);
                Route.IsValidDepartureTime(departureTime);
                if (_Route == null)
                {
                    // Проверка на дубликат государственного номера
                    if (_RouteCollection.Routes.Any(route => route.Code.Equals(textBoxCode.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Маршрут с таким шифром уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создание и добавление автобуса
                    Route route = new Route(
            textBoxCode.Text.Trim(),
            textBoxStartingPoint.Text.Trim(),
            textBoxEndingPoint.Text.Trim(),
            intermediatePoints,
            departureDays,
            transportationTime,
            departureTime
        );
                    _RouteCollection.Routes.Add(route);

                    MessageBox.Show("Маршрут успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    _Route.Code = textBoxCode.Text;
                    _Route.StartingPoint = textBoxStartingPoint.Text;
                    _Route.EndingPoint = textBoxEndingPoint.Text;
                    _Route.IntermediatePoints = intermediatePoints;
                    _Route.DepartureTime = departureTime;
                    _Route.DepartureDays = departureDays;
                    _Route.TransportationTime = transportationTime;
                    MessageBox.Show("Маршрут успешно изменен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm();

                this.parentForm.LoadRouteCollection();
                this.parentForm.BringToFront();
                this.parentForm.Show();
                this.parentForm.panelRoutesMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения маршрута");
            }
        }
        private List<string> GetIntermediatePoints()
        {
            return textBoxIntermediatePoints.Text
                .Split(new[] { '\r', '\n', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();
        }

        // Метод для получения дней отправления
        private List<DayOfWeek> GetDepartureDays()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            foreach (string day in checkedListBoxDepartureDays.CheckedItems)
            {
                switch (day)
                {
                    case "Понедельник": { days.Add(DayOfWeek.Monday); break; }
                    case "Вторник": { days.Add(DayOfWeek.Tuesday); break; }
                    case "Среда": { days.Add(DayOfWeek.Wednesday); break; }
                    case "Четверг": { days.Add(DayOfWeek.Thursday); break; }
                    case "Пятница": { days.Add(DayOfWeek.Friday); break; }
                    case "Суббота": { days.Add(DayOfWeek.Saturday); break; }
                    case "Воскресенье": { days.Add(DayOfWeek.Sunday); break; }
                }
            }
            return days;
        }

        // Метод очистки формы
        private void ClearForm()
        {
            textBoxCode.Clear();
            textBoxStartingPoint.Clear();
            textBoxEndingPoint.Clear();
            textBoxIntermediatePoints.Clear();

            // Сброс выбранных дней
            for (int i = 0; i < checkedListBoxDepartureDays.Items.Count; i++)
            {
                checkedListBoxDepartureDays.SetItemChecked(i, false);
            }

            // Установка времени по умолчанию
            dateTimePickerTransportationTimeHours.Value = DateTime.Today.AddHours(1); // 1 час по умолчанию
            dateTimePickerDepartureTime.Value = DateTime.Today.AddHours(8); // 8:00 по умолчанию
        }

        private void textBoxTransportationTimeDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;
            string newText = textBox.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > Route.Constants.MaxDepartureDays)
            {
                e.Handled = true;
            }
        }

        private void checkedListBoxDepartureDays_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
