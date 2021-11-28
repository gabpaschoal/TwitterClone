﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TwitterClone.Application.Commands.User;

namespace TwitterClone.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost, Route("")]
    public async Task<IActionResult> Post(
        [FromBody] UserCreateCommand command)
    {
        var response = await _mediator.Send(command);

        if (response.IsValid)
            return Ok(response);
        return BadRequest(response);
    }
}
