using CPUMiroservice.Models;

namespace CPUMiroservice.Data
{
    public interface ICPURepository
    {
        bool SaveChanges();

        IEnumerable<CPU> GetAllCPUs();
        CPU? GetCPUById(int id);
        void CreateCPU(CPU CPU);
    }
}
