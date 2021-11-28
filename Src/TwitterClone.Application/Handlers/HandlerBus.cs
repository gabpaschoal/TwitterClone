using EasyValidation.DependencyInjection;

namespace TwitterClone.Application.Handlers;

public class HandlerBus : IHandlerBus
{
    public HandlerBus(IValidatorLocator validator)
    {
        Validator = validator;
    }

    public IValidatorLocator Validator { get; }
}
