using FluentValidation;
using SD.Mini.ZooManagement.Domain.Models.Animal;

namespace SD.Mini.ZooManagement.Application.Validators;

public class AnimalModelValidator: AbstractValidator<AnimalModel>
{
    public AnimalModelValidator()
    {
        RuleFor(m => m.FavouriteFood).NotNull().NotEmpty();
        RuleFor(m => m.Nickname).NotNull().NotEmpty();
    }
}