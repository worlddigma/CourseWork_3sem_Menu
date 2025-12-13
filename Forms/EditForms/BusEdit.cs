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
    public partial class BusEdit : Form
    {
        private BusFleet _BusFleet;
        private FormBuses parentForm;
        private Bus _Bus;
        public BusEdit(BusFleet busFleet, FormBuses parent, Bus bus)
        {
            InitializeComponent();
            _BusFleet = busFleet;
            parentForm = parent;
            _Bus = bus;
            textBoxStateNumber.Text = bus.StateNumber;
            textBoxBrand.Text = bus.Brand;
            textBoxModel.Text = bus.Model;
            textBoxCapacity.Text = bus.Capacity.ToString();
            textBoxYear.Text = bus.Year.ToString();
            textBoxYearOfRepair.Text = bus.YearOfMajorRepair.ToString();
            textBoxMilleage.Text = bus.Mileage.ToString();
            if (!string.IsNullOrWhiteSpace(bus.Photo)) pictureBoxBusImage.Image = Image.FromFile(bus.Photo);

        }
        public BusEdit(BusFleet busFleet, FormBuses parent)
        {
            InitializeComponent();
            _BusFleet = busFleet;
            parentForm = parent;
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files | *.jpg;*.png;*.jpeg;*.bmp;";
                openFileDialog.Title = "Выберите изображение";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBoxBusImage.Image = Image.FromFile(openFileDialog.FileName);
                        pictureBoxBusImage.Name = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                    }
                }
            }
        }

        private void buttonDelImage_Click(object sender, EventArgs e)
        {
            if (pictureBoxBusImage.Image == null)
            {
                MessageBox.Show("Нет изображения для удаления");
                return;
            }
            pictureBoxBusImage.Image = null;
            pictureBoxBusImage.Name = null;
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(textBoxStateNumber.Text))
            {
                MessageBox.Show("Государственный номер не может быть пустым.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxBrand.Text))
            {
                MessageBox.Show("Марка автобуса не может быть пустой.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxModel.Text))
            {
                MessageBox.Show("Модель автобуса не может быть пустой.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxCapacity.Text))
            {
                MessageBox.Show("Вместимость не может быть пустой.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxYear.Text))
            {
                MessageBox.Show("Год выпуска не может быть пустым.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxYearOfRepair.Text))
            {
                MessageBox.Show("Год капитального ремонта не может быть пустым.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxMilleage.Text))
            {
                MessageBox.Show("Пробег не может быть пустым.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка числовых значений
            if (!int.TryParse(textBoxCapacity.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Вместимость должна быть положительным числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxYear.Text, out int year) || year < Bus.Constants.MinYear || year > DateTime.Now.Year)
            {
                MessageBox.Show($"Год выпуска должен быть числом между {Bus.Constants.MinYear} и {DateTime.Now.Year}.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxYearOfRepair.Text, out int repairYear) || repairYear < Bus.Constants.MinYear || repairYear > DateTime.Now.Year)
            {
                MessageBox.Show($"Год капитального ремонта должен быть числом между {Bus.Constants.MinYear} и {DateTime.Now.Year}.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBoxMilleage.Text, out int mileage) || mileage < 0)
            {
                MessageBox.Show("Пробег должен быть неотрицательным числом.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка логики годов
            if (year > repairYear)
            {
                MessageBox.Show("Год выпуска не может быть больше года капитального ремонта.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка изображения
            if (pictureBoxBusImage.Image == null)
            {
                DialogResult result = MessageBox.Show("Изображение автобуса не добавлено. Продолжить сохранение?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            string stateNumber = textBoxStateNumber.Text.Trim();

            try
            {
                if (_Bus == null)
                {
                    // Проверка на дубликат государственного номера
                    if (_BusFleet.Buses.Any(bus => bus.StateNumber.Equals(stateNumber, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Автобус с таким государственным номером уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создание и добавление автобуса
                    Bus bus = new Bus(
                        stateNumber,
                        textBoxBrand.Text.Trim(),
                        textBoxModel.Text.Trim(),
                        capacity,
                        year,
                        repairYear,
                        mileage,
                        pictureBoxBusImage.Image != null ? pictureBoxBusImage.Name : string.Empty
                    );

                    _BusFleet.Buses.Add(bus);

                    MessageBox.Show("Автобус успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
                else
                {
                    _Bus.StateNumber = stateNumber;
                    _Bus.Brand = textBoxBrand.Text.Trim();
                    _Bus.Model = textBoxModel.Text.Trim();
                    _Bus.Capacity = capacity;
                    _Bus.Year = year;
                    _Bus.YearOfMajorRepair = repairYear;
                    _Bus.Mileage = mileage;
                    _Bus.Photo = pictureBoxBusImage.Image != null ? pictureBoxBusImage.Name : string.Empty;
                    MessageBox.Show("Автобус успешно изменен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                textBoxStateNumber.Clear();
                textBoxBrand.Clear();
                textBoxModel.Clear();
                textBoxCapacity.Clear();
                textBoxYear.Clear();
                textBoxYearOfRepair.Clear();
                textBoxMilleage.Clear();
                pictureBoxBusImage.Image = null;
                textBoxStateNumber.Focus();

                this.parentForm.LoadBusesFleet();
                this.parentForm.BringToFront();
                this.parentForm.Show();
                this.parentForm.panelBusesMenuTitle.Show();
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении автобуса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void buttonDisChanges_Click(object sender, EventArgs e)
        {

            this.parentForm.LoadBusesFleet();
            this.parentForm.BringToFront();
            this.parentForm.Show();
            this.parentForm.panelBusesMenuTitle.Show();
            this.Close();

        }

        private void textBoxCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Запрещаем все, кроме цифр
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Запрещаем первый ноль (опционально)
            if (e.KeyChar == '0' && textBox.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            // Ограничение максимального значения
            string newText = textBox.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > Bus.Constants.MaxCapacity)
            {
                e.Handled = true;
            }
        }

        private void textBoxStateNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length == 0 && !Bus.AllowedStateLetters.Contains(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 1)
            {

                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 2)
            {
                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 3)
            {
                e.Handled = true;
                return;
            }
            if (textBox.Text.Length == 4 && !Bus.AllowedStateLetters.Contains(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (textBox.Text.Length == 5 && !Bus.AllowedStateLetters.Contains(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 6)
            {
                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 7)
            {
                e.Handled = true;
                return;
            }
            if (char.IsLetter(e.KeyChar) && textBox.Text.Length == 8)
            {
                e.Handled = true;
                return;
            }

        }

        private void textBoxBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '0' && textBox.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            string newText = textBox.Text + e.KeyChar;
            if (int.TryParse(newText, out int result))
            {
                int currentYear = DateTime.Now.Year;

                if (newText.Length == 4 && result < Bus.Constants.MinYear)
                {
                    e.Handled = true;
                    return;
                }
                // Проверяем только максимальное значение (текущий год)
                if (result > currentYear)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void textBoxYearOfRepair_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == '0' && textBox.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            string newText = textBox.Text + e.KeyChar;
            if (int.TryParse(newText, out int result))
            {
                int currentYear = DateTime.Now.Year;

                if (newText.Length == 4 && result < Bus.Constants.MinYear)
                {
                    e.Handled = true;
                    return;
                }
                // Только проверка максимума
                if (result > currentYear)
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void textBoxMilleage_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)sender;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Запрещаем все, кроме цифр
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Запрещаем первый ноль (опционально)
            if (e.KeyChar == '0' && textBox.Text.Length == 0)
            {
                e.Handled = true;
                return;
            }

            // Ограничение максимального значения
            string newText = textBox.Text + e.KeyChar;
            if (int.TryParse(newText, out int result) && result > Bus.Constants.MaxMileage)
            {
                e.Handled = true;
            }

        }

    }
}
