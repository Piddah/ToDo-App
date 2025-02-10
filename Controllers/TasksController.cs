using Controllers.Data;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = Controllers.Models.Task;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("Tasks")]
    public class TasksController : Controller
    {
        private readonly ToDoContext _context;

        [HttpGet("projects/{projectId}/tasks")]
        public IActionResult GetTasks(int projectId)
        {
            var project = _context.Projects
                .Include(p => p.Tasks)
                //.ThenInclude(t => t.Tags)
                .FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound("Project was not found.");
            }

            return Ok(project.Tasks);
        }

        [HttpPost("{projectId}/tasks")]
        public IActionResult CreateTask([FromBody] Task task, int projectId)
        {
            var project = _context.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == projectId);
            if (project == null) return NotFound("Project not found.");

            task.ProjectId = projectId;
            task.Tags = new List<Tag>();

            project.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTasks), new { projectId = projectId, taskId = task.Id }, task);
        }

        [HttpDelete("projects/{projectId}/tasks/{taskId}")]
        public IActionResult DeleteTask(int projectId, int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId && projectId == t.ProjectId);
            if (task == null) return NotFound("Task was not found.");

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
