using FluentValidation;

namespace MessageBroker.Models.Request.Validators;

public class TopicRequestModelValidator : AbstractValidator<TopicRequestModel>
{
    public TopicRequestModelValidator()
        => RuleFor(t => t.Name).NotEmpty();
}
