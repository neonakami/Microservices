using CPUMiroservice.Models;

namespace CPUMiroservice.Data
{
    public class CPURepository : ICPURepository
    {
        private readonly AppDdContext _context;
        
        public CPURepository(AppDdContext context)
        {
            _context = context;
        }

        public void CreateCPU(CPU CPU)
        {
            if (CPU == null)
            {
                throw new ArgumentNullException(nameof(CPU));
            }

            _context.CPUs.Add(CPU);
        }

        public IEnumerable<CPU> GetAllCPUs()
        {
            return _context.CPUs.ToList();
        }

        public CPU? GetCPUById(int id)
        {
            return _context.CPUs.FirstOrDefault(CPU => CPU.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
