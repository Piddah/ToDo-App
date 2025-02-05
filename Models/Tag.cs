using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Controllers.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; } = string.Empty;
        
        [JsonIgnore]
        public int TaskId { get; set; }
        [JsonIgnore]
        public List<Task>? Tasks { get; set; } = new List<Task>();
    }
}
