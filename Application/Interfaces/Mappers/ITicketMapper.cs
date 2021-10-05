using Application.DTOs.Tickets;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces.Mappers
{
    public interface ITicketMapper
    {
       TicketModel Convert(Ticket ticket);
       List<TicketModel> Convert(List<Ticket> list);
    }
}
