using EasyValidation.Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace TwitterClone.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SampleController : ControllerBase
{
    // GET: api/<SampleController>
    [HttpGet]
    public IResultData Get()
    {
        return new ResultData();
    }
}
