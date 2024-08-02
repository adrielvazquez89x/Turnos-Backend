using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos_Backend.DTO;
using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public class TicketService : ITicketService
    {
        private TicketContext _context;
        public TicketService(TicketContext context)
        {
            _context = context;
        }

        public async Task<TicketDTO> CreateTicket(int countNumber, CustomerType customer)
        {
            //Esto a la DB
            var ticket = new Ticket
            {
                TicketNumber = TicketNumberGenerator(countNumber, customer),
                Customer = customer,
                DateTime = DateTime.Now,
                Called = false,
                Status = false,
                CounterNumber = 0
            };

            //Generamos el DTO para enviar al front
            var ticketDTO = new TicketDTO
            {
                TicketNumber = ticket.TicketNumber,
                DateTime = ticket.DateTime
            };

            // Se guarda en a la DB
            await _context.Ticket.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return ticketDTO;
        }

        public async Task<IEnumerable<TicketBoxDTO>> GetTickets()
        {
            var query = _context.Ticket.AsQueryable();
            query = query.Where(ticket => ticket.DateTime.Date == DateTime.Today.Date);

            return await query.Select(ticket => new TicketBoxDTO
            {
                Id = ticket.Id,
                TicketNumber = ticket.TicketNumber,
                Customer = ticket.Customer,
                DateTime = ticket.DateTime,
                Called = ticket.Called,
                Status = ticket.Status,
                CounterNumber = ticket.CounterNumber

            }).ToListAsync();
        }

        public async Task<TicketBoxDTO> UpdateTicket(int id, TicketBoxDTO ticketUpdateDto)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket != null)
            {
                ticket.CounterNumber = ticketUpdateDto.CounterNumber;
                ticket.Called = ticketUpdateDto.Called;
                ticket.Status = ticketUpdateDto.Status;

                await _context.SaveChangesAsync();

                var TicketBoxDTO = new TicketBoxDTO
                {
                    Id = ticket.Id,
                    TicketNumber = ticket.TicketNumber,
                    Customer = ticket.Customer,
                    DateTime = ticket.DateTime,
                    Called = ticket.Called,
                    Status = ticket.Status,
                    CounterNumber = ticket.CounterNumber
                };

                return TicketBoxDTO;
            }
            return null;
        }

        String TicketNumberGenerator(int countNumber, CustomerType customer)
        {
            string formatedNumber = countNumber.ToString("D3");

            switch (customer)
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

    }
}
