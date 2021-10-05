using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Command.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<Response<bool>>
    {
        public int TicketId { get; set; }
    }

    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Response<bool>>
    {
        private readonly ITicketRepository TicketRepository;
        public DeleteTicketCommandHandler(ITicketRepository ticketRepository)
        {
            TicketRepository = ticketRepository;
        }

        public async Task<Response<bool>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await TicketRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
                throw new ApiException("Ticket " + request.TicketId + " not found");

            return new Response<bool>((await TicketRepository.DeleteAsync(ticket)) == 1); 
        }
    }
}
