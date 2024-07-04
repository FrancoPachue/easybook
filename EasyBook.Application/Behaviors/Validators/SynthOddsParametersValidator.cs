using EasyBook.Domain.Entities;
using FluentValidation;


namespace EasyBook.Application.Behaviors
{
    public sealed partial class CreateSubscriptionConfigCommandValidator
    {
        public class SynthOddsParametersValidator : AbstractValidator<List<Parameter>>
        {
            public SynthOddsParametersValidator()
            {
                RuleFor(parameters => parameters)
                    .NotEmpty().WithMessage("Parameters cannot be empty")
                            .Must(parameters => parameters.Any(p => p.Name == "Sport"))
                            .WithMessage("Sport cannot be empty")
                            .Must(parameters => parameters.Any(p => p.Name == "SuperOddsType"))
                            .WithMessage("SuperOddsType cannot be empty")
                            .Must(parameters => parameters.Any(p => p.Name == "InRunning"))
                            .WithMessage("InRunning cannot be empty")
                            .Must(parameters => parameters.Any(p => p.Name == "Synth"))
                            .WithMessage("Synth cannot be empty")
                    .When(parameters => parameters != null);
            }
        }
    }
}
