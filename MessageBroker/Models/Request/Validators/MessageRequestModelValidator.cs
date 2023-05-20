using FluentValidation;

namespace MessageBroker.Models.Request.Validators;

public class MessageRequestModelValidator : AbstractValidator<MessageRequestModel>
{
    public MessageRequestModelValidator()
        => RuleFor(m => m.TopicMessage).NotEmpty();
}
