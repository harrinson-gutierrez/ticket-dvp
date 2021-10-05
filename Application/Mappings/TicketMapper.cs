using Application.DTOs.Tickets;
using Application.Interfaces.Mappers;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Mappings
{
    public class TicketMapper : MapperBase, ITicketMapper
    {
        public TicketModel Convert(Ticket ticket)
        {
            return new TicketModel()
            {
                Id = ticket.id,
                InsertDate = ticket.insert_date,
                UpdateDate = ticket.update_date,
                Status = ticket.status,
                Username = ticket.username
            };
        }

        public List<TicketModel> Convert(List<Ticket> list)
        {
            return ConvertList(list, (item) =>
            {
                return Convert(item);
            });
        }
    }
}
