using System;
using System.Threading;
using System.Threading.Tasks;
using TwitterClone.Application.Commands;
using TwitterClone.Application.Handlers;

namespace TwitterClone.Application.Test.Handlers;

public class StubCommand : IRequestExecution<Guid>
{ }

public class HandlerBaseStub : HandlerBase<StubCommand, CustomResultData<Guid>>
{
    public HandlerBaseStub(IHandlerBus handlerBus) : base(handlerBus)
    { }

    public override Task<CustomResultData<Guid>> HandleExecution(StubCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CustomResultData<Guid>());
    }
}
