using MassTransit;
using Shared.Models;

namespace Ticket.Consumer.Controllers
{
    public class TicketConsumer : IConsumer<TicketModel>
    {
        public async Task Consume(ConsumeContext<TicketModel> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS

            Console.WriteLine("<br/> UserName: {0} - Date: {1}", data.UserName, data.BookedOn );
        }
    }
}
