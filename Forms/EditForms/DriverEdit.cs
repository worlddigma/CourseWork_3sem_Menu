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
    public partial class DriverEdit : Form
    {
        private Class _Class;
        private DriverStaff _DriverStaff;
        private Driver _Driver;
        private FormDrivers parentForm;
        public DriverEdit(DriverStaff driverStaff, FormDrivers parent, Driver driver)
        {
            InitializeComponent();
            parentForm = parent;
            _DriverStaff = driverStaff;
            textBoxName.Text = driver.Name.ToString();
            textBoxId.Text = driver.Id.ToString();
            dateTimePickerDateOfBirth.Value = driver.DateOfBirth;
            textBoxWorkExperience.Text = driver.WorkExperience.ToString();
            if (driver.Category == Category.D) checkedListBoxCategory.SetItemChecked(0, true);
            else checkedListBoxCategory.SetItemChecked(1, true);
            if (driver.Class == Class.Class1) _Class = Class.Class1;
            if (driver.Class == Class.Class2) _Class = Class.Class2;
            if (driver.Class == Class.Class3) _Class = Class.Class3;
            _Driver = driver;
        }

        public DriverEdit(DriverStaff driverStaff, FormDrivers parent)
        {
            InitializeComponent();
            _DriverStaff = driverStaff;
            parentForm = parent;
            _Driver = null;
        }


        private void buttonChooseClass_Click(object sender, EventArgs e)
        {
            try
            {

                if (int.Parse(textBoxWorkExperience.Text) < 0)
                    throw new ArgumentException("Опыт работы не может быть отрицательным.");

                if (int.Parse(textBoxWorkExperience.Text) == 0) _Class = Class.Class3;

                DialogResult result = MessageBox.Show(
                    "Имеет ли водитель нарушения ПДД?",
                    "Выбор",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);

                if (int.Parse(textBoxWorkExperience.Text) < 5)
                {
                    if (result == DialogResult.Yes) _Class = Class.Class3;
                    else _Class = Class.Class2;
                }
                else
                {
                    if (result == DialogResult.Yes) _Class = Class.Class2;
                    else _Class = Class.Class1;
                }
                Label labelClass = new Label
                {
                    Text = ((int)_Class).ToString(),
                    Location = new Point(325, 220),
                    AutoSize = true,
                    Font = new Font("Arial", 9)
                };
                this.Controls.Add(labelClass);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                return;
            }
        }

        private void textBoxId_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void textBoxWorkExperience_KeyPress(object sender, KeyPressEventArgs e)
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
            if (int.TryParse(newText, out int result))
            {
                int currentYear = DateTime.Now.Year;

                if (newText.Length == 2 && result > (currentYear - dateTimePickerDateOfBirth.Value.Year))
                {
                    e.Handled = true;
                    return;
                }

            }
        }

        private void buttonDisChanges_Click(object sender, EventArgs e)
        {
            this.parentForm.LoadDriverStaff();
            this.parentForm.BringToFront();
            this.parentForm.Show();
            this.parentForm.panelDriversMenuTitle.Show();
            this.Close();
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверки на пустые поля
                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                {
                    throw new Exception("Поле с ФИО не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxId.Text))
                {
                    throw new Exception("Поле с табельным номером не может быть пустым");
                }
                if (string.IsNullOrWhiteSpace(textBoxWorkExperience.Text))
                {
                    throw new Exception("Поле с опытом работы не может быть пустым!");
                }

                // Проверка выбора категории
                if (checkedListBoxCategory.CheckedItems.Count == 0)
                {
                    throw new Exception("Необходимо выбрать категорию прав!");
                }

                // Проверка выбора класса водителя
                if (_Class == null)
                {
                    throw new Exception("Необходимо выбрать класс водителя!");
                }

                // Проверка и создание ФИО
                string[] Name = textBoxName.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (Name.Length != 3)
                {
                    throw new Exception("ФИО должно содержать фамилию, имя и отчество через пробел");
                }
                FullName fullName = new(Name[0], Name[1], Name[2]);

                // Проверки через статические методы класса Driver
                Driver.IsValidName(fullName);
                Driver.IsValidId(int.Parse(textBoxId.Text));
                Driver.IsValidDateOfBirth(dateTimePickerDateOfBirth.Value.Date);
                Driver.IsValidWorkExperience(int.Parse(textBoxWorkExperience.Text), dateTimePickerDateOfBirth.Value.Date);

                // Получение и проверка категории
                Category category = GetCategory();
                Driver.IsValidCategory(category);

                // Проверка класса
                Driver.IsValidClass(_Class);

                if (_Driver == null)
                {
                    // Проверка на дубликат табельного номера
                    if (_DriverStaff.Drivers.Any(driver => driver.Id == int.Parse(textBoxId.Text)))
                    {
                        MessageBox.Show("Водитель с таким табельным номером уже существует.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создание и добавление водителя
                    Driver driver = new Driver(
                        fullName,
                        int.Parse(textBoxId.Text),
                        dateTimePickerDateOfBirth.Value.Date,
                        int.Parse(textBoxWorkExperience.Text),
                        category,
                        _Class
                    );
                    _DriverStaff.Drivers.Add(driver);

                    MessageBox.Show("Водитель успешно добавлен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Обновление существующего водителя
                    _Driver.Id = int.Parse(textBoxId.Text);
                    _Driver.WorkExperience = int.Parse(textBoxWorkExperience.Text);
                    _Driver.Class = _Class;
                    _Driver.Name = fullName;
                    _Driver.DateOfBirth = dateTimePickerDateOfBirth.Value;
                    _Driver.Category = category;

                    MessageBox.Show("Водитель успешно изменен!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm();
                this.parentForm.LoadDriverStaff();
                this.parentForm.BringToFront();
                this.parentForm.Show();
                this.parentForm.panelDriversMenuTitle.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка сохранения водителя");
            }
        }

        private void ClearForm()
        {
            // Очистка текстовых полей
            textBoxName.Clear();
            textBoxId.Clear();
            textBoxWorkExperience.Clear();

            // Сброс выбранной категории в CheckedListBox
            for (int i = 0; i < checkedListBoxCategory.Items.Count; i++)
            {
                checkedListBoxCategory.SetItemChecked(i, false);
            }

            // Установка даты рождения по умолчанию (например, 18 лет назад)
            dateTimePickerDateOfBirth.Value = DateTime.Today.AddYears(-18);

            // Сброс класса водителя
            _Class = Class.Class3; // или значение по умолчанию

            _Driver = null;
            // Установка фокуса на первое поле
            textBoxName.Focus();
        }
        private Category GetCategory()
        {
            string category = checkedListBoxCategory.Text;
            switch (category)
            {
                case "D": return Category.D;
                case "E": return Category.E;
                default:
                    return Category.D;
            }
        }

        private void checkedListBoxCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxCategory.CheckedItems.Count; i++)
                {
                    if (i != e.Index && checkedListBoxCategory.GetItemChecked(i))
                    {
                        checkedListBoxCategory.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void checkedListBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
