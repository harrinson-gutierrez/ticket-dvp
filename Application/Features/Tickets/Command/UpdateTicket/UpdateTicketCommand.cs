using Application.DTOs.Tickets;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Command.UpdateTicket
{
    public class UpdateTicketCommand : IRequest<Response<TicketModel>>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Response<TicketModel>>
    {
        private readonly ITicketRepository TicketRepository;
        private readonly ITicketMapper TicketMapper;
        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
        {
            TicketRepository = ticketRepository;
            TicketMapper = ticketMapper;
        }

        public async  Task<Response<TicketModel>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await TicketRepository.GetByIdAsync(request.Id);

            ticket.update_date = DateTime.Now;
            ticket.status = request.Status;
            ticket.username = request.Username;

            await TicketRepository.UpdateAsync(ticket);

            return new Response<TicketModel>(TicketMapper.Convert(ticket));
        }
    }
}
