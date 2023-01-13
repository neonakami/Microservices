using System.ComponentModel.DataAnnotations;

namespace CPUMiroservice.Models
{
    public class CPU
    {
        [Key]
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Company { get; set; }
        [Required] public string Model { get; set; }
        [Required] public float Frequency { get; set; }
        [Required] public float Cache { get; set; }
        [Required] public int TechnicalProcess { get; set; }
        [Required] public int NumberOfCores { get; set; }
        [Required] public int NumebrOfThreads { get; set; }
    }
}
