using Application.DTOs.Tickets;
using Application.Exceptions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries.GetById
{
    public class GetTicketByIdQuery : IRequest<Response<TicketModel>>
    {
        public int TicketId { get; set; }
    }

    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Response<TicketModel>>
    {
        private readonly ITicketRepository TicketRepository;
        private readonly ITicketMapper TicketMapper;
        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
        {
            TicketRepository = ticketRepository;
            TicketMapper = ticketMapper;
        }

        public async Task<Response<TicketModel>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await TicketRepository.GetByIdAsync(request.TicketId);

            if (ticket == null)
                throw new ApiException("Ticket "+ request.TicketId + " not found");

            return new Response<TicketModel>(TicketMapper.Convert(ticket));
        }
    }
}
