using Dapper;
using Domain.Common;
using System;

namespace Domain.Entities
{
    [Table("ticket")]
    public class Ticket : BaseEntity<int>
    {
        public string username { get; set; }
        public DateTime insert_date { get; set; }
        public DateTime update_date { get; set; }
        public bool status { get; set; }
    }
}
