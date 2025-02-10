using Controllers.Data;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ProjectsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet("projects")]
        public List<Project> GetProjects()
        {
            return _context.Projects
            .Include(p => p.Tasks)
            .ThenInclude(t => t.Tags)
            .ToList();
        }


        [HttpPost("projects")]
        public IActionResult CreateProject(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }

        [HttpPut("projects/{id}")]
        public IActionResult UpdateProject(int id, Project project)
        {
            Project existingProject = _context.Projects.Find(id);   
            if (existingProject == null)
            {
                return BadRequest("Project not found.");
            }
            _context.Entry(existingProject)
                .CurrentValues
                .SetValues(project);
            
            _context.SaveChanges();
            return NoContent();
        }
        

        [HttpDelete("projects/{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return NoContent();
        }


    }
}

