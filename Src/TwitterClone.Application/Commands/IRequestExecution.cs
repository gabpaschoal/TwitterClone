using MediatR;

namespace TwitterClone.Application.Commands;

public interface IRequestExecution<T> : IRequest<CustomResultData<T>> where T : new()
{ }

public interface IRequestExecution : IRequest<CustomResultData>
{ }