using FluentValidation;
using SD.Mini.ZooManagement.Domain.Models.Enclosure;

namespace SD.Mini.ZooManagement.Application.Validators;

public class EnclosureModelValidator: AbstractValidator<EnclosureModel>
{
    public EnclosureModelValidator()
    {
        RuleFor(m => m.Volume).GreaterThan(1);
        RuleFor(m => (int)m.MaximumCapacity).GreaterThanOrEqualTo(1);
    }
}