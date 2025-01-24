using Controllers.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task = Controllers.Models.Task;

namespace Controllers.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
