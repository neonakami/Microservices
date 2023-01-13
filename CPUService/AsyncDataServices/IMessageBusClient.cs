using CPUMiroservice.Dtos;

namespace CPUMiroservice.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewCPU(CPUPublishedDTO CPUPublishedDTO);
    }
}
