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
    // Форма для добавления/редактирования водителя
    public partial class DriverEdit : Form
    {
        private Class _Class;              // Класс водителя
        private DriverStaff _DriverStaff;  // Коллекция водителей
        private Driver _Driver;            // Редактируемый водитель (null при добавлении)
        private FormDrivers parentForm;    // Родительская форма

        // Конструктор для редактирования существующего водителя
        public DriverEdit(DriverStaff driverStaff, FormDrivers parent, Driver driver)
        {
            InitializeComponent();
            parentForm = parent;
            _DriverStaff = driverStaff;

            // Заполнение полей данными редактируемого водителя
            textBoxName.Text = driver.Name.ToString();
            textBoxId.Text = driver.Id.ToString();
            dateTimePickerDateOfBirth.Value = driver.DateOfBirth;
            textBoxWorkExperience.Text = driver.WorkExperience.ToString();

            // Установка категории прав
            if (driver.Category == Category.D) checkedListBoxCategory.SetItemChecked(0, true);
            else checkedListBoxCategory.SetItemChecked(1, true);

            // Установка класса водителя
            _Class = driver.Class;
            _Driver = driver;

            labelChosenClass.Text = $"Класс: {(int)_Class}";
        }

        // Конструктор для добавления нового водителя
        public DriverEdit(DriverStaff driverStaff, FormDrivers parent)
        {
            InitializeComponent();
            _DriverStaff = driverStaff;
            parentForm = parent;
            _Driver = null;
        }

        // Автоматический выбор класса водителя на основе опыта работы
        private void buttonChooseClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBoxWorkExperience.Text, out int experience) || experience < 0)
                    throw new ArgumentException("Опыт работы должен быть положительным числом.");

                // Определение класса по опыту работы и нарушениям ПДД
                if (experience == 0)
                {
                    _Class = Class.Class3;
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                        "Имеет ли водитель нарушения ПДД?", "Выбор",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    bool hasViolations = result == DialogResult.Yes;

                    _Class = (experience, hasViolations) switch
                    {
                        ( < 5, true) => Class.Class3,
                        ( < 5, false) => Class.Class2,
                        ( >= 5, true) => Class.Class2,
                        ( >= 5, false) => Class.Class1
                    };
                }

                labelChosenClass.Text = $"Класс: {(int)_Class}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        // Сохранение изменений
        private void buttonSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Валидация обязательных полей
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxId.Text) ||
                    string.IsNullOrWhiteSpace(textBoxWorkExperience.Text))
                {
                    throw new Exception("Все обязательные поля должны быть заполнены.");
                }

                if (checkedListBoxCategory.CheckedItems.Count == 0)
                    throw new Exception("Необходимо выбрать категорию прав.");

                if (_Class == null)
                    throw new Exception("Необходимо выбрать класс водителя.");

                // Парсинг и валидация ФИО
                string[] nameParts = textBoxName.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameParts.Length < 2)
                    throw new Exception("ФИО должно содержать фамилию и имя.");

                string patronymic = nameParts.Length == 3 ? nameParts[2] : "";
                FullName fullName = new(nameParts[0], nameParts[1], patronymic);

                // Парсинг числовых значений
                if (!int.TryParse(textBoxId.Text, out int id) ||
                    !int.TryParse(textBoxWorkExperience.Text, out int experience))
                    throw new Exception("Некорректные числовые значения.");

                // Получение категории прав
                Category category = GetCategory();

                // Валидация данных через статические методы класса Driver
                Driver.IsValidName(fullName);
                Driver.IsValidId(id);
                Driver.IsValidDateOfBirth(dateTimePickerDateOfBirth.Value);
                Driver.IsValidWorkExperience(experience, dateTimePickerDateOfBirth.Value);
                Driver.IsValidCategory(category);
                Driver.IsValidClass(_Class);

                // Проверка уникальности табельного номера (при добавлении)
                if (_Driver == null && _DriverStaff.Drivers.Any(d => d.Id == id))
                    throw new Exception("Водитель с таким табельным номером уже существует.");

                if (_Driver == null) // Добавление нового водителя
                {
                    Driver driver = new Driver(fullName, id, dateTimePickerDateOfBirth.Value,
                                             experience, category, _Class);
                    _DriverStaff.Drivers.Add(driver);
                    MessageBox.Show("Водитель успешно добавлен!", "Успех");
                }
                else // Редактирование существующего
                {
                    _Driver.Name = fullName;
                    _Driver.Id = id;
                    _Driver.DateOfBirth = dateTimePickerDateOfBirth.Value;
                    _Driver.WorkExperience = experience;
                    _Driver.Category = category;
                    _Driver.Class = _Class;
                    MessageBox.Show("Водитель успешно изменен!", "Успех");
                }

                // Закрытие формы и обновление родительской формы
                parentForm.LoadDriverStaff();
                parentForm.panelDriversMenuTitle.Show();
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
            parentForm.LoadDriverStaff();
            parentForm.panelDriversMenuTitle.Show();
            this.Close();
        }

        // Получение выбранной категории прав
        private Category GetCategory()
        {
            return checkedListBoxCategory.CheckedIndices[0] == 0 ? Category.D : Category.E;
        }

        // Обработчики валидации ввода

        private void textName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только буквы и пробелы
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
        }

        private void textBoxId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxWorkExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем только цифры с проверкой максимального возраста
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

            string newText = textBoxWorkExperience.Text + e.KeyChar;
            if (int.TryParse(newText, out int experience))
            {
                int age = DateTime.Now.Year - dateTimePickerDateOfBirth.Value.Year;
                if (experience > Math.Max(0, age - 18)) // Максимальный опыт = возраст - 18
                    e.Handled = true;
            }
        }

        // Обеспечение выбора только одной категории прав
        private void checkedListBoxCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < checkedListBoxCategory.Items.Count; i++)
                {
                    if (i != e.Index)
                        checkedListBoxCategory.SetItemChecked(i, false);
                }
            }
        }
    }
}
