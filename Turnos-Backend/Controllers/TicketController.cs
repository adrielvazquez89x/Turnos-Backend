using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;
using Turnos_Backend.DTO;
using Turnos_Backend.Models;
using Turnos_Backend.Services;

namespace Turnos_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
    
        private ICounterService _counters;
        private ITicketService _ticketService;
        private readonly IHubContext<TurnsHub> _hubContext;

        public TicketController(ICounterService counters, ITicketService ticketService, IHubContext<TurnsHub> hubContext)
        {
            _counters = counters;
            _ticketService = ticketService;
            _hubContext = hubContext;
        }

        [HttpPost("{customer}")]
        public async Task<IActionResult> AddTicket(int customer)
        {

            if (!Enum.IsDefined(typeof(CustomerType), customer))
            {
                return BadRequest("Invalid customer type");
            }

            //Genera el número de ticket, solo el número.
            var number = _counters.GetNextCounter(customer);
            var ticketDTO = await _ticketService.CreateTicket(number, (CustomerType)customer);

            if (ticketDTO == null)
            {
                return BadRequest("Error creating ticket");
            }

            await _hubContext.Clients.All.SendAsync("ReceiveTurn", ticketDTO);

            return Ok(ticketDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _ticketService.GetTickets();

            return Ok(tickets);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketBoxDTO ticketUpdateDto)
        {
            var ticketUpdated = await _ticketService.UpdateTicket(id, ticketUpdateDto);

            return Ok(ticketUpdated);
        }

        [HttpGet("GetCustomersCount")]
        public IActionResult GetCustomersCount()
        {
           var customers =  _ticketService.GetTypeCustomers();

            return customers != null ? Ok(customers) : NotFound();
        }
    }
}
