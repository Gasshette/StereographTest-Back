using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Stereograph.TechnicalTest.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController : ControllerBase
{
    [HttpGet]
    public ActionResult<String> Get()
    {
        return Ok("all");
    }
}
