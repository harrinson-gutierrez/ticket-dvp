using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Tickets.Command.UpdateTicket
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator(ITicketRepository ticketRepository)
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull().WithMessage("{PropertyName} is required.")
               .CustomAsync(async (value, context, cancellation) =>
               {
                   var search = await ticketRepository.GetByIdAsync(value);

                   if(search == null)
                   {
                       context.AddFailure("Id", "Ticket has not found with id " + value);
                   } 
               });
        }
    }
}
