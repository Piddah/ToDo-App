using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Controllers.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public List<Task>? Tasks { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
