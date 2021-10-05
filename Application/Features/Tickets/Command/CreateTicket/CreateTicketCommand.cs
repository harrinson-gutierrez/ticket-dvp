using Application.DTOs.Tickets;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Command.CreateTicket
{
    public class CreateTicketCommand : IRequest<Response<TicketModel>>
    {
        public string Username { get; set; }
        public bool Status { get; set; }
    }

    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Response<TicketModel>>
    {
        private readonly ITicketRepository TicketRepository;
        private readonly ITicketMapper TicketMapper;
        public CreateTicketCommandHandler(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
        {
            TicketRepository = ticketRepository;
            TicketMapper = ticketMapper;
        }

        public async  Task<Response<TicketModel>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var newTicket = new Ticket()
            {
                insert_date = DateTime.Now,
                update_date = DateTime.Now,
                status = request.Status,
                username = request.Username
            };

            newTicket.id =  await TicketRepository.CreateAsync(newTicket);

            return new Response<TicketModel>(TicketMapper.Convert(newTicket));
        }
    }
}
