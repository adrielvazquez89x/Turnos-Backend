using Turnos_Backend.Models;

namespace Turnos_Backend.DTO
{
    public class TicketBoxDTO
    {
        public int Id { get; set; }
        public string? TicketNumber { get; set; }
        public CustomerType Customer { get; set; }
        public DateTime DateTime { get; set; }
        public bool Called { get; set; }
        public bool Status { get; set; }
        public int CounterNumber { get; set; }
    }
}
