using Microsoft.AspNetCore.Mvc;
using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public class TicketService
    {
        private Ticket _ticket;

        public TicketService(int countNumber, CustomerType customer)
        {
            _ticket = new Ticket
            {
                TicketNumber = TicketNumberGenerator(countNumber, customer),
                Customer = customer,
                DateTime = DateTime.Now,
                Called = false,
                Status = false,
                CounterNumber = 0
            };


        }

        String TicketNumberGenerator(int countNumber, CustomerType customer)
        {
            string formatedNumber = countNumber.ToString("D3");

            switch(customer)
            {
                case CustomerType.IOMAPAMI:
                    return "IOMA" + formatedNumber;
                case CustomerType.ObraSocial:
                    return "OS" + formatedNumber;
                case CustomerType.Particular:
                    return "PA" + formatedNumber;
                case CustomerType.Perfumeria:
                    return "PERF" + formatedNumber;
                default:
                    return "ERROR";
            }
        }

        public Ticket GetTicket()
        {
            return _ticket;
        }
    }
}
