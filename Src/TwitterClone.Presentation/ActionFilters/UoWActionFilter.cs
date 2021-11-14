using Microsoft.AspNetCore.Mvc.Filters;
using TwitterClone.Domain.Repositories.Data.Base;
using EasyValidation.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.Presentation.ActionFilters;

public class UoWActionFilter : IActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public UoWActionFilter(IUnitOfWork unitOfWork)
    { _unitOfWork = unitOfWork; }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _unitOfWork.BeginTransaction();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var newResult = context.Result as ObjectResult;

        if (newResult is null)
            return;

        if (newResult.Value is IResultData resultData && resultData is not null && resultData.IsValid)
            _unitOfWork.RollBackTransaction();
        else
            _unitOfWork.CommitTransaction();
    }
}
