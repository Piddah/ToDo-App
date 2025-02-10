using Controllers.Data;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("Tags")]
    public class TagsController : Controller
    {
        private readonly ToDoContext _context; 
        
        [HttpGet("projects/{projectId}/tasks/{taskId}/tags")]
        public IActionResult GetTags(int projectId, int taskId)
        {
            var task = _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefault(t => t.Id == taskId && t.ProjectId == projectId);

            if (task == null) return NotFound("No Tags Found.");

            return Ok(task.Tags);
        }

        [HttpPost("projects/{projectId}/tasks/{taskId}/tags")]
        public IActionResult CreateTag(int projectId, int taskId, [FromBody] Tag tag)
        {
            var task = _context.Tasks
                .Include(t => t.Tags)
                .FirstOrDefault(t => t.Id == taskId && t.ProjectId == projectId);
            if (task == null) return NotFound("Task not found.");

            var existingTag = _context.Tags.FirstOrDefault(t => t.TagName == tag.TagName);
            if (existingTag == null)
            {
                existingTag = new Tag { TagName = tag.TagName };
                _context.Tags.Add(existingTag);
                _context.SaveChanges();
            }
            task.Tags.Add(existingTag);
            _context.SaveChanges();

            return Ok(task.Tags);
        }

        [HttpDelete("projects/{projectId}/tasks/{taskId}/tags/{tagId}")]
        public IActionResult DeleteTag(int projectId, int taskId, int tagId)
        {
            var task = _context.Tasks.FirstOrDefault(t => 
                taskId == t.Id && 
                projectId == t.ProjectId);

            if (task == null) return NotFound("Task not found.");

            var tag = _context.Tags.FirstOrDefault(t => t.Id == tagId);

            if (tag == null) return NotFound("Tag not found.");

            _context.Tags.Remove(tag);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
