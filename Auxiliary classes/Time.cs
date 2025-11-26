namespace CourseWork_3sem
{
        public class Time
        {
            private int _Hours;
            private int _Minutes;
            private int _Seconds;
        public int Hours
        {
            get
            {
                return _Hours;
            }
            set
            {
                if (value < 0 || value > 24) throw new ArgumentOutOfRangeException();
                _Hours = value;
            }
        }

        public int Minutes
        {
            get
            {
                return _Minutes;
            }
            set
            {
                if (value < 0 || value > 60) throw new ArgumentOutOfRangeException();
                _Minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return _Seconds;
            }
            set
            {
                if (value < 0 || value > 60) throw new ArgumentOutOfRangeException();
                _Seconds = value;
            }
        }

        public Time(int hours, int minutes, int seconds)
            {
                Hours = hours;
                Minutes = minutes;
                Seconds = seconds;
            }
            public override string ToString()
            {
                return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
            }
        }
}

//route code;
//starting point;
//ending point;
//list of intermediate points where the bus stops;
//departure time;
//departure days;
//travel time to the final destination.