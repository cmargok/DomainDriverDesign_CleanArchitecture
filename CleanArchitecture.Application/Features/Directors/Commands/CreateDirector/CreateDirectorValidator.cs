using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorValidator : AbstractValidator<CreateDirectorCommand> 
    {
        public CreateDirectorValidator()
        {
            RuleFor(cd => cd.Nombre)
                .NotNull().WithMessage("Nombre no puede ser nulo");

            RuleFor(cd => cd.Apellido)
                .NotNull().WithMessage("Nombre no puede ser nulo");

        }

    }

    
}
