using Application.DTOs.Tickets;
using Application.Features.Tickets.Queries.GetAllTicketPaginated;
using Application.Wrappers;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ITicketRepository : IRepository<int, Ticket>
    {
        Task<PaginationResponse<List<TicketModel>>> GetAllPaginated(GetAllTicketPaginatedQuery request);
    }
}
