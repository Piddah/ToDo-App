using System.ComponentModel.DataAnnotations;

namespace Controllers.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
