namespace CPUMiroservice.Dtos
{
    public class CPUReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public float Frequency { get; set; }
        public float Cache { get; set; }
        public int TechnicalProcess { get; set; }
        public int NumberOfCores { get; set; }
        public int NumebrOfThreads { get; set; }
    }
}
