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
{// Форма для добавления/редактирования автобуса
    public partial class BusEdit : Form
    {
        private BusFleet _BusFleet;        // Коллекция автобусов
        private FormBuses parentForm;      // Родительская форма
        private Bus _Bus;                  // Редактируемый автобус (null при добавлении)

        // Конструктор для редактирования существующего автобуса
        public BusEdit(BusFleet busFleet, FormBuses parent, Bus bus)
        {
            InitializeComponent();
            _BusFleet = busFleet;
            parentForm = parent;
            _Bus = bus;

            // Заполнение полей данными редактируемого автобуса
            textBoxStateNumber.Text = bus.StateNumber;
            textBoxBrand.Text = bus.Brand;
            textBoxModel.Text = bus.Model;
            textBoxCapacity.Text = bus.Capacity.ToString();
            textBoxYear.Text = bus.Year.ToString();
            textBoxYearOfRepair.Text = bus.YearOfMajorRepair.ToString();
            textBoxMilleage.Text = bus.Mileage.ToString();

            // Загрузка изображения, если оно существует
            if (!string.IsNullOrWhiteSpace(bus.Photo) && System.IO.File.Exists(bus.Photo))
            {
                pictureBoxBusImage.Image = Image.FromFile(bus.Photo);
                pictureBoxBusImage.Tag = bus.Photo; // Сохраняем путь к файлу
            }
        }

        // Конструктор для добавления нового автобуса
        public BusEdit(BusFleet busFleet, FormBuses parent)
        {
            InitializeComponent();
            _BusFleet = busFleet;
            parentForm = parent;
        }

        // Выбор изображения автобуса
        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files | *.jpg;*.png;*.jpeg;*.bmp;";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxBusImage.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBoxBusImage.Tag = openFileDialog.FileName; // Сохраняем путь
                }
            }
        }

        // Удаление изображения
        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            pictureBoxBusImage.Image = null;
            pictureBoxBusImage.Tag = null;
        }

        // Сохранение изменений
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            // Валидация обязательных полей
            if (string.IsNullOrWhiteSpace(textBoxStateNumber.Text) ||
                string.IsNullOrWhiteSpace(textBoxBrand.Text) ||
                string.IsNullOrWhiteSpace(textBoxModel.Text) ||
                string.IsNullOrWhiteSpace(textBoxCapacity.Text) ||
                string.IsNullOrWhiteSpace(textBoxYear.Text) ||
                string.IsNullOrWhiteSpace(textBoxYearOfRepair.Text) ||
                string.IsNullOrWhiteSpace(textBoxMilleage.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка");
                return;
            }

            // Парсинг числовых значений
            if (!int.TryParse(textBoxCapacity.Text, out int capacity) ||
                !int.TryParse(textBoxYear.Text, out int year) ||
                !int.TryParse(textBoxYearOfRepair.Text, out int repairYear) ||
                !int.TryParse(textBoxMilleage.Text, out int mileage))
            {
                MessageBox.Show("Некорректные числовые значения.", "Ошибка");
                return;
            }

            // Логические проверки
            if (year > repairYear)
            {
                MessageBox.Show("Год выпуска не может быть больше года ремонта.", "Ошибка");
                return;
            }

            string stateNumber = textBoxStateNumber.Text.Trim();

            try
            {
                if (_Bus == null) // Добавление нового автобуса
                {
                    // Проверка уникальности госномера
                    if (_BusFleet.Buses.Any(b => b.StateNumber.Equals(stateNumber, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Автобус с таким госномером уже существует.", "Ошибка");
                        return;
                    }

                    Bus bus = new Bus(
                        stateNumber,
                        textBoxBrand.Text.Trim(),
                        textBoxModel.Text.Trim(),
                        capacity,
                        year,
                        repairYear,
                        mileage,
                        pictureBoxBusImage.Tag?.ToString() ?? string.Empty
                    );

                    _BusFleet.Buses.Add(bus);
                }
                else // Редактирование существующего
                {
                    _Bus.StateNumber = stateNumber;
                    _Bus.Brand = textBoxBrand.Text.Trim();
                    _Bus.Model = textBoxModel.Text.Trim();
                    _Bus.Capacity = capacity;
                    _Bus.Year = year;
                    _Bus.YearOfMajorRepair = repairYear;
                    _Bus.Mileage = mileage;
                    _Bus.Photo = pictureBoxBusImage.Tag?.ToString() ?? string.Empty;
                }

                // Обновление родительской формы
                parentForm.LoadBusesFleet();
                parentForm.panelBusesMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        // Отмена изменений
        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            parentForm.LoadBusesFleet();
            parentForm.panelBusesMenuTitle.Show();
            this.Close();
        }

        // Обработчики ввода для валидации

        private void textBoxCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры до максимальной вместимости
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxCapacity.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > Bus.Constants.MaxCapacity)
                e.Handled = true;
        }

        private void textBoxStateNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Валидация формата госномера: буква-цифра-цифра-цифра-буква-буква-цифра-цифра-цифра
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;
            int pos = textBox.Text.Length;

            if (char.IsControl(e.KeyChar)) return;

            // Определяем разрешенные символы для каждой позиции
            bool isValid = pos switch
            {
                0 or 4 or 5 => Bus.AllowedStateLetters.Contains(e.KeyChar), // Буквы
                1 or 2 or 3 or 6 or 7 or 8 => char.IsDigit(e.KeyChar), // Цифры
                _ => false // Не должно быть больше 9 символов
            };

            e.Handled = !isValid;
        }

        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ввод года выпуска с проверкой диапазона
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxYear.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > DateTime.Now.Year)
                e.Handled = true;
        }

        private void textBoxMilleage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Ввод пробега с проверкой максимального значения
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxMilleage.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > Bus.Constants.MaxMileage)
                e.Handled = true;
        }

        // Аналогичные обработчики для других полей...
        private void textBoxBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, цифры и пробелы
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
        }

        private void textBoxModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
        }

        private void textBoxYearOfRepair_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxYearOfRepair.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > DateTime.Now.Year)
                e.Handled = true;
        }
    }
}
