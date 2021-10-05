using Application.DTOs.Tickets;
using Application.Features.Tickets.Queries.GetAllTicketPaginated;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TicketRepository : RepositoryPostgresql<int, Ticket>, ITicketRepository
    {
        public TicketRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<PaginationResponse<List<TicketModel>>> GetAllPaginated(GetAllTicketPaginatedQuery request)
        {
            using IDbConnection conn = new NpgsqlConnection(ConnectionString);

            var conditions = new List<string>();

            if (!string.IsNullOrEmpty(request.TypeFilter))
            {
                switch (request.TypeFilter)
                {
                    case "username":
                        {
                            conditions.Add("username LIKE @filter");
                            break;
                        }
                    case "id":
                        {
                            conditions.Add("cast(id as varchar) LIKE @filter");
                            break;
                        }
                }
            }

            if (string.IsNullOrEmpty(request.TypeFilter) && !string.IsNullOrEmpty(request.FilterSearch))
            {
                conditions.Add("(username LIKE @filter OR cast(id as varchar) LIKE @filter)");
            }

            var conditionRaw = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            var sql = @$"SELECT COUNT(id) FROM ticket {conditionRaw}; 
                      SELECT 
                          id as ""Id"",
                          username as ""Username"",
                          insert_date as ""InsertDate"",
                          update_date as ""UpdateDate"",
                          status as ""Status""
                      FROM ticket {conditionRaw}  OFFSET @PageNumber ROWS FETCH NEXT @PageSize ROWS ONLY;";

            using var multi = await conn.QueryMultipleAsync(sql, new {PageNumber = (request.PageNumber * request.PageSize), request.PageSize, filter = "%" + request.FilterSearch + "%" });
            var countTotal = multi.Read<int>().Single();
            var entityList = multi.Read<TicketModel>().ToList();
            return new PaginationResponse<List<TicketModel>>(entityList, request.PageNumber, request.PageSize, countTotal);
        }
    }
}
