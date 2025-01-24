using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controllers.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;

        public List<Tag>? Tags { get; set; } = new List<Tag>();

        public DateOnly Deadline { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
