using System.Threading;
using System.Threading.Tasks;
using TwitterClone.Application.Commands;
using TwitterClone.Application.Handlers;

namespace TwitterClone.Application.Test.Handlers;

public class StubCommand : IRequestExecution
{ }

public class HandlerBaseStub : HandlerBase<StubCommand, CustomResultData>
{
    public int HandleExecutionCalls { get; private set; }
    public HandlerBaseStub(IHandlerBus handlerBus) : base(handlerBus)
    {
        HandleExecutionCalls = default;
    }

    public override Task<CustomResultData> HandleExecution(StubCommand request, CancellationToken cancellationToken)
    {
        HandleExecutionCalls++;
        return Task.FromResult(new CustomResultData());
    }
}
