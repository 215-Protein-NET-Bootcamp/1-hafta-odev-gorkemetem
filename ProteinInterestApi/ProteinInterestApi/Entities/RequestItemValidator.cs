using FluentValidation;

namespace ProteinBankApi.Entities
{
    public class RequestItemValidator : AbstractValidator<RequestItem>
    {
        public RequestItemValidator()
        {
            RuleFor(x => x.Expiry).NotEmpty().WithMessage("Expiry must not null.");
            RuleFor(x => x.Expiry).GreaterThan(0).WithMessage("Expiry must greater than 0.");
            RuleFor(x => x.DesiredAmount).NotEmpty().WithMessage("Desired amount must not null.");
            RuleFor(x => x.DesiredAmount).GreaterThan(0).WithMessage("Desired amount must greater than 0.");
        }
    }
}
