using EasyValidation.Core.Results;
using MediatR;
using TwitterClone.Domain.Repositories.Data.Base;

namespace TwitterClone.Application.MediatrPipeline;

public class UoWPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class, new()
    where TRequest : class
{
    private readonly IUnitOfWork _unitOfWork;

    public UoWPipeline(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (typeof(TResponse) != typeof(IResultData))
            return await next();

        _unitOfWork.BeginTransaction();

        var result = await next();
        var resultData = (IResultData)result;

        if (resultData.IsValid)
            _unitOfWork.CommitTransaction();
        else
            _unitOfWork.RollBackTransaction();

        return result;
    }
}
