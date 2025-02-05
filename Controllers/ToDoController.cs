﻿using Controllers.Data;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult GetTags(int projectId, int taskId, [FromBody] Tag tag)
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

        [HttpDelete("project/tasks/tags/{id}")]
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

        [HttpGet("projects/{projectId}/tasks")]
        public IActionResult GetTasks(int projectId)
        {
            var project = _context.Projects
                .Include(p => p.Tasks)
                .ThenInclude(t => t.Tags)
                .FirstOrDefault(p => p.Id == projectId);

            if (project == null)
            {
                return NotFound("Project was not found.");
            }

            return Ok(project.Tasks);
        }

        [HttpPost("projects/tasks")]
        public IActionResult CreateTask(Task task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpDelete("projects/tasks/{id}")]
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

