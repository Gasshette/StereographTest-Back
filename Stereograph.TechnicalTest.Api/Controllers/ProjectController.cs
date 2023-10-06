using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;

namespace Stereograph.TechnicalTest.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController : ControllerBase
{
    public TesttechniqueContext _context;

    public ProjectController(TesttechniqueContext context)
    {
         _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Project>> Get()
    {
        return Ok("all");
    }

    [HttpPost]
    public ActionResult<Project> Add([FromBody] Project project)
    {
        _context.Add(project);
        _context.SaveChanges();
        return Ok(project);
    }
}
