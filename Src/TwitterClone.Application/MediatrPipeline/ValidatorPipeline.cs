using EasyValidation.Core;
using EasyValidation.Core.Results;
using MediatR;

namespace TwitterClone.Application.MediatrPipeline;

public class ValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class, new()
    where TRequest : class
{
    private readonly IValidation<TRequest> _validator;

    public ValidatorPipeline(
        IValidation<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (_validator is null)
            throw new Exception($"No validator found for {typeof(TRequest).Name}.");

        _validator.SetValue(request);
        _validator.Validate();

        if (_validator.HasErrors)
        {
            if (typeof(TResponse) != typeof(IResultData<>))
                return (TResponse)_validator.ResultData;

            var res = new TResponse();
            var fieldErrors = new Dictionary<string, IList<string>>();
            var assignFieldErrors = new Dictionary<string, IResultData>();

            foreach (var fieldError in _validator.ResultData.FieldErrors)
                fieldErrors.Add(fieldError.Key, fieldError.Value);
            foreach (var item in _validator.ResultData.AssignFieldErrors)
                assignFieldErrors.Add(item.Key, item.Value);

            ((IResultData)res).IncoporateErrors(fieldErrors, assignFieldErrors);
            return res;
        }

        var response = await next();

        return response;
    }
}