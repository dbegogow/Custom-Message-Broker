using FluentValidation;

namespace MessageBroker.Models.Request.Validators;

public class SubscriptionRequestModelValidator : AbstractValidator<SubscriptionRequestModel>
{
    public SubscriptionRequestModelValidator()
        => RuleFor(s => s.Name).NotEmpty();
}
