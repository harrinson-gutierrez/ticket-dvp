using Application.DTOs.Tickets;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Request;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Tickets.Queries.GetAllTicketPaginated
{
    public class GetAllTicketPaginatedQuery : PaginationRequest, IRequest<PaginationResponse<List<TicketModel>>>
    {
        public string TypeFilter { get; set; }
        public string FilterSearch { get; set; }
    }

    public class GetAllTicketPaginatedQueryHandler : IRequestHandler<GetAllTicketPaginatedQuery, PaginationResponse<List<TicketModel>>>
    {
        private readonly ITicketRepository TicketRepository;
        public GetAllTicketPaginatedQueryHandler(ITicketRepository ticketRepository)
        {
            TicketRepository = ticketRepository;
        }

        public async Task<PaginationResponse<List<TicketModel>>> Handle(GetAllTicketPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await TicketRepository.GetAllPaginated(request);
        }
    }
}
