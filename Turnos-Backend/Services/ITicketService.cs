using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public interface ITicketService
    {
        Ticket CreateTicket(int countNumber, CustomerType customerType);
    }
}
