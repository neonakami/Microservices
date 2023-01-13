using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CommandRepository> _logger;

        public CommandRepository(AppDbContext context, ILogger<CommandRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void CreateCommand(int fabricId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.CPUId = fabricId;

            _context.Commands.Add(command);
        }

        public void CreateCPU(CPU fabric)
        {
            if (fabric== null)
            {
                throw new ArgumentNullException(nameof(fabric));
            }

            _context.CPUs.Add(fabric);
        }

        public bool ExternalCPUExists(int externaCPUId)
        {
            return _context.CPUs.Any(fabric => fabric.ExternalID == externaCPUId);
        }

        public bool CPUExits(int fabricId)
        {
            return _context.CPUs.Any(fabric => fabric.Id == fabricId);
        }

        public IEnumerable<CPU> GetAllCPUs()
        {
            return _context.CPUs.ToList();
        }

        public Command? GetCommand(int fabricId, int commandId)
        {
            return _context.Commands
                .Where(command => command.CPUId == fabricId && command.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForCPU(int fabricId)
        {
            return _context.Commands
                .Where(command => command.CPUId == fabricId)
                .OrderBy(command => command.CPUs.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
