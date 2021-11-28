using EasyValidation.DependencyInjection;

namespace TwitterClone.Application.Handlers;

public interface IHandlerBus
{
    IValidatorLocator Validator { get; }
}
