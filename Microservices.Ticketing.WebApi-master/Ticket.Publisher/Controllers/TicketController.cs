using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Ticket.Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;

        public TicketController(IBus bus)
        {
            _bus = bus;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketModel ticket)
        {
            if (ticket != null)
            {
                for (int i = 0; i < 500; i++)
                {
                    ticket.UserName = ticket.UserName + "_" + i.ToString();
                    ticket.BookedOn = DateTime.Now;
                    await _bus.Publish(ticket);
                }

                return Ok();
            }
            return BadRequest();
        }
    }
}