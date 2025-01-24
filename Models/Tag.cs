using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controllers.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Task>? Tasks { get; set; } = new List<Task>();
    }
}
