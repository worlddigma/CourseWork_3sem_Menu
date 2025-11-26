namespace CourseWork_3sem
{
    public class FullName
    {
        private string _LastName; //        Фамилия
        private string _FirstName; //         Имя
        private string _Patronymoc; //       Отчество
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _LastName = value;
            }
        }
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
                _FirstName = value;
            }
        }
        public string Patronymoc
        {
            get { return _Patronymoc; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) value = "";
                _Patronymoc = value;
            }
        }

        public FullName(string lastName, string firstName, string patronymoc)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymoc = patronymoc;
        }

        public override string ToString() =>
            $"{LastName} {FirstName} {Patronymoc}";
    }
}
