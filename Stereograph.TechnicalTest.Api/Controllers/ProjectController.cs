using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        List<Project> projects = _context.Projects.ToList();
        return Ok(projects);
    }

    [HttpPost]
    public ActionResult<Project> Add([FromBody] Project project)
    {
        _context.Add(project);
        _context.SaveChanges();
        return Ok(project);
    }

    [HttpPut]
    public ActionResult<Project> Put([FromBody] Project project)
    {
        _context.Update(project);
        _context.SaveChanges();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Project project = _context.Projects.FirstOrDefault(x => x.Id == id);

        if (project != null)
        {
            _context.Remove(project);
            _context.SaveChanges();
            return NoContent();
        }

        return Ok($"No project found with id :{id}");
    }
}
