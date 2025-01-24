using System.ComponentModel.DataAnnotations;

namespace Controllers.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string ToDo { get; set; } = string.Empty;
        public List<Tag>? Tags { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
