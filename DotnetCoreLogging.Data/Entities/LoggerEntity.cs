using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetCoreLogging.Data.Entities
{
    [Table("Logger")]
    public class LoggerEntity
    {
        [Required]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } 
    }
}
