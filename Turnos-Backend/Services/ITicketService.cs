using Turnos_Backend.DTO;
using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public interface ITicketService
    {
        Task<TicketDTO> CreateTicket(int countNumber, CustomerType customerType);

        Task<IEnumerable<TicketBoxDTO>> GetTickets();

        Task<TicketBoxDTO> UpdateTicket(int id, TicketBoxDTO ticketUpdateDto);
    }
}
