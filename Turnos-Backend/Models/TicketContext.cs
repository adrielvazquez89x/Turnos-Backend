using Microsoft.EntityFrameworkCore;

namespace Turnos_Backend.Models
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }
        public DbSet<Ticket> Ticket { get; set; }
        
    }
}
