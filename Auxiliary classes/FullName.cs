namespace CourseWork_3sem
{
    // Класс, представляющий полное имя человека (ФИО)
    public class FullName
    {
        // Приватные поля для хранения частей имени
        private string _LastName;    // Фамилия
        private string _FirstName;   // Имя
        private string _Patronymoc;  // Отчество (может быть пустым)

        // Свойство для фамилии с валидацией
        public string LastName
        {
            get { return _LastName; }
            set
            {
                // Проверка, что фамилия не пустая и не состоит только из пробелов
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Фамилия не может быть пустой или состоять только из пробелов");
                _LastName = value;
            }
        }

        // Свойство для имени с валидацией
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                // Проверка, что имя не пустое и не состоит только из пробелов
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя не может быть пустым или состоять только из пробелов");
                _FirstName = value;
            }
        }

        // Свойство для отчества с валидацией
        public string Patronymoc
        {
            get { return _Patronymoc; }
            set
            {
                // Отчество может быть пустым (не у всех людей есть отчество)
                // Если передана пустая строка или null, заменяем на пустую строку
                if (string.IsNullOrWhiteSpace(value))
                    value = "";
                _Patronymoc = value;
            }
        }

        // Конструктор класса FullName
        public FullName(string lastName, string firstName, string patronymoc)
        {
            // Используем свойства для установки значений, чтобы выполнялась валидация
            LastName = lastName;
            FirstName = firstName;
            Patronymoc = patronymoc;
        }

        // Переопределение метода ToString для вывода полного имени в формате "Фамилия Имя Отчество"
        public override string ToString() =>
            // Если отчество пустое, выводим только фамилию и имя
            string.IsNullOrWhiteSpace(Patronymoc)
                ? $"{LastName} {FirstName}"
                : $"{LastName} {FirstName} {Patronymoc}";
    }
}