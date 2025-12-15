using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_3sem
{
    // Класс, представляющий информацию о билетах (проданных билетах и их стоимости)
    public class Tickets
    {
        // Приватные поля для хранения данных о билетах
        private int _SoldTickets;    // Количество проданных билетов
        private int _TicketCost;     // Стоимость одного билета (в условных единицах)

        // Свойство для количества проданных билетов
        public int SoldTickets
        {
            get { return _SoldTickets; }
            set { _SoldTickets = value; }
        }

        // Свойство для стоимости билета
        public int TicketCost
        {
            get { return _TicketCost; }
            set { _TicketCost = value; }
        }

        // Конструктор класса Tickets
        public Tickets(int soldTickets, int ticketCost)
        {
            // Проверка количества проданных билетов на отрицательное значение
            ArgumentOutOfRangeException.ThrowIfNegative(soldTickets);

            // Проверка стоимости билета
            // Если стоимость отрицательная, устанавливаем минимальное значение 1
            if (ticketCost < 0)
                ticketCost = 1;

            // Устанавливаем значения
            SoldTickets = soldTickets;
            TicketCost = ticketCost;
        }

        // Переопределение метода ToString для вывода информации о билетах
        public override string ToString() => $"{SoldTickets} {TicketCost}";
}
}