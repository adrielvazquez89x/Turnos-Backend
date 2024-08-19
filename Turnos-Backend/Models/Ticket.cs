using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turnos_Backend.Models
{
    public enum CustomerType
    {
        IOMA_PAMI = 1,   
        Obra_Social = 2,
        Particular = 3,
        Perfumeria = 4
    }

    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? TicketNumber { get; set; }
        public CustomerType Customer { get; set; }
        public DateTime DateTime { get; set; }
        public bool Called { get; set; }
        public bool Status { get; set; }
        
        public int CounterNumber { get; set; }

    }
}
