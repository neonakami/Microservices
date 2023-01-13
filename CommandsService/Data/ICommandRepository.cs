using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        // CPUs
        IEnumerable<CPU> GetAllCPUs();
        void CreateCPU(CPU fabric);
        bool CPUExits(int fabricId);
        bool ExternalCPUExists(int externaCPUId);

        // Commands
        IEnumerable<Command> GetCommandsForCPU(int fabricId);
        Command? GetCommand(int fabricId, int commandId);
        void CreateCommand(int fabricId, Command command);
    }
}
