using CPUMiroservice.Dtos;

namespace CPUMiroservice.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendCPUToCommand(CPUReadDTO CPU);
    }
}
