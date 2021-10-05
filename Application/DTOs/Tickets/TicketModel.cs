using System;

namespace Application.DTOs.Tickets
{
    public class TicketModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }
    }
}
