using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Turnos_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnsController : ControllerBase
    {
        private readonly IHubContext<TurnsHub> _hubContext;

        public TurnsController(IHubContext<TurnsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostTurn([FromBody] string turn)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTurn", turn);
            return Ok(new {message = "Turn sent succesfully"});
        }
    }
}
