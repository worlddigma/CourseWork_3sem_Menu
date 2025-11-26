namespace CourseWork_3sem
{
    public class Date 
    {
        private int _Day;
        private int _Month;
        private int _Year;
        public int Day
        {
            get
            {
                return _Day;
            }
            set
            {
                if (value < 1 || value > 31) throw new ArgumentOutOfRangeException();
                _Day = value;
            }
        }

        public int Month
        {
            get
            {
                return _Month;
            }
            set
            {
                if (value < 1 || value > 12) throw new ArgumentOutOfRangeException();
                _Month = value;
            }
        }

        public int Year
        {
            get
            {
                return _Year;
            }
            set
            {
                if (value < 1960 || value > 2025) throw new ArgumentOutOfRangeException();
                _Year = value;
            }
        }

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }
        public override string ToString()
        {
            return $"{Day:D2}:{Month:D2}:{Year:D2}";
        }
    }
}