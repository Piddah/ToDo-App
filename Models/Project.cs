using System.ComponentModel.DataAnnotations;

namespace Controllers.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Tag>? Tags { get; set; }
        public List<Task>? Tasks { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
