using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{

    public partial class UpdateStreamerHandler
    {
        public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand> 
        {
            public UpdateStreamerCommandValidator()
            {

                RuleFor(n => n.Nombre)
                    .NotNull().WithMessage("{Nombre} no puede estar en nulo");

                RuleFor(n => n.Url)
                    .NotEmpty().WithMessage("{Url} no puede estar en blanco");

            }
        }
    }
}
