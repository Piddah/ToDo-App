using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controllers.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Tag>? Tags { get; set; } = new List<Tag>();
        public DateOnly Deadline { get; set; }

        [JsonIgnore]
        public int ProjectId { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
    }
}
