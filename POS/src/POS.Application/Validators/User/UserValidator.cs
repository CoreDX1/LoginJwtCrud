using FluentValidation;
using POS.Application.Dto.Request;

namespace POS.Application.Validators.User;

public class UserValidator : AbstractValidator<UserRequestDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("El capom de usuario no puede estar vacio")
            .NotNull()
            .WithMessage("El campo nombre no pude ser nulo");
    }
}
