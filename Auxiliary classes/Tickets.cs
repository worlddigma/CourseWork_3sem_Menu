using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    public class Tickets
    {
        private int _SoldTickets;
        private int _TicketCost;
        
        public int SoldTickets
        {
            get { return _SoldTickets; }
            set { _SoldTickets = value; }
        }
        public int TicketCost
        {
            get { return _TicketCost; }
            set { _TicketCost = value; }
        }

        public Tickets(int soldTickets, int ticketCost)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(soldTickets);
            if (ticketCost < 0) ticketCost = 1;
            SoldTickets = soldTickets;
            TicketCost = ticketCost;
        }
        public override string ToString() => $"{SoldTickets} {TicketCost}";
    }
}
