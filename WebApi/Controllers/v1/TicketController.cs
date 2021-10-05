using Application.DTOs.Tickets;
using Application.Features.Tickets.Command.CreateTicket;
using Application.Features.Tickets.Command.DeleteTicket;
using Application.Features.Tickets.Command.UpdateTicket;
using Application.Features.Tickets.Queries.GetAllTicketPaginated;
using Application.Features.Tickets.Queries.GetById;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TicketController : BaseApiController
    {
        public TicketController()
        {
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Paginated tickets", Description = "Types Filter: username, id . If TypeFilter has null or empty search in ocurrence in any paramaters (id, username)")]
        public async Task<ActionResult<PaginationResponse<List<TicketModel>>>> CreateTicket([FromQuery] GetAllTicketPaginatedQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet]
        [Route("{ticketId}")]
        [SwaggerOperation(Summary = "Ticket detail")]
        [SwaggerResponse(200, "Ticket Found")]
        [SwaggerResponse(400, "Not found ticket")]
        public async Task<ActionResult<Response<TicketModel>>> DetailTicket([FromRoute] int ticketId)
        {
            return Ok(await Mediator.Send(new GetTicketByIdQuery()
            {
                TicketId = ticketId
            }
            ));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Created new ticket")]
        [SwaggerResponse(200, "Ticket Found")]
        [SwaggerResponse(400, "Validation errors")]
        public async Task<ActionResult<Response<TicketModel>>> CreateTicket([FromBody] CreateTicketCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Route("{ticketId}")]
        [SwaggerOperation(Summary = "Update ticket")]
        [SwaggerResponse(200, "Ticket Updated!")]
        [SwaggerResponse(400, "Not found ticket")]
        public async Task<ActionResult<Response<TicketModel>>> UpdateTicket([FromRoute] int ticketId, [FromBody] UpdateTicketCommand command)
        {
            if (ticketId != command.Id)
                return BadRequest();

            command.Id = ticketId;

            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        [Route("{ticketId}")]
        [SwaggerOperation(Summary = "Delete ticket")]
        [SwaggerResponse(200, "Ticket Deleted!")]
        [SwaggerResponse(400, "Not found ticket")]
        public async Task<ActionResult<Response<bool>>> DeleteTicket([FromRoute] int ticketId)
        {
            return Ok(await Mediator.Send(new DeleteTicketCommand()
            {
                TicketId = ticketId
            }));
        }
    }
}
