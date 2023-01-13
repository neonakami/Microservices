using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandsService.Models
{
    [Table("CommadsCPU")]
    public class CPU
    {
        [Key]
        [Required] public int Id { get; set; }
        [Required] public int ExternalID { get; set; }
        [Required] public string Name { get; set; }

        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}
