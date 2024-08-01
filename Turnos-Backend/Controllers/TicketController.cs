using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Turnos_Backend.DTO;
using Turnos_Backend.Models;
using Turnos_Backend.Services;

namespace Turnos_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext _context;
        private ICounterService _counters;

        public TicketController(TicketContext context, ICounterService counters)
        {
            _context = context;
            _counters = counters;
        }

        [HttpPost("{customer}")]
        public async Task<IActionResult> AddTicket(int customer)
        {
            //Genera el número de ticket, solo el número.
            var contadorPrueba = _counters.GetNextCounter(customer);
            //Instanciamos el ticketService, pero debería ir por Inyección de dependencias.
            var ticketService = new TicketService(contadorPrueba, (CustomerType)customer);
            //Generamos el ticket.
            var ticket = ticketService.GetTicket();
            //Generamos el DTO.
            var ticketDTO = new TicketDTO
            {
                TicketNumber = ticket.TicketNumber,
                DateTime = ticket.DateTime
            };
 


            return Ok(ticketDTO);
        }
    }
}
