using System.ComponentModel.DataAnnotations;

namespace CPUMiroservice.Dtos
{
    public class CPUCreateDTO
    {
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
