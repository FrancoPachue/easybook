using EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;
using FluentValidation;


namespace EasyBook.Application.Behaviors
{
    public sealed partial class CreateSubscriptionConfigCommandValidator : AbstractValidator<CreateSubscriptionConfigCommand>
    {
        public CreateSubscriptionConfigCommandValidator()
        {
            RuleFor(x => x.Subscription.Endpoint).NotEmpty().WithMessage("Endpoint cannot be empty");

            RuleFor(x => x.Subscription.Parameters).SetValidator(new OddsParametersValidator()).When(x => x.Subscription.Endpoint == "/odds");

            RuleFor(x => x.Subscription.Parameters).SetValidator(new SynthOddsParametersValidator()).When(x => x.Subscription.Endpoint == "/synthetic_odds");
        }
    }
}
