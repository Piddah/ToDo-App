using Controllers.Data;
using Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = Controllers.Models.Task;

namespace Controllers.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet("projects")]
        public List<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }


        [HttpPost("projects")]
        public bool CreateProject(Project project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return true;
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

        [HttpGet("tags")]
        public List<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }

        [HttpPost("tags")]
        public bool GetTags(Tag tag)
        {
            _context.Add(tag);
            _context.SaveChanges();
            return true;
        }

        [HttpDelete("tags/{id}")]
        public IActionResult DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("tasks")]
        public List<Task> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        [HttpPost("tasks")]
        public IActionResult CreateTask(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpDelete("tasks/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }


    }
}

