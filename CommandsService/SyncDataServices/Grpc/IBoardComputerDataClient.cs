using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc
{
    public interface ICPUDataClient
    {
        IEnumerable<CPU> ReturnAllCPUs();
    }
}
