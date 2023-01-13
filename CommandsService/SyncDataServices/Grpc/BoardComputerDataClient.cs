using AutoMapper;
using CommandsService.Models;
using CommandsService.Protos;
using Grpc.Net.Client;

namespace CommandsService.SyncDataServices.Grpc
{
    public class CPUDataClient : ICPUDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CPUDataClient> _logger;

        public CPUDataClient(IConfiguration configuration, IMapper mapper, ILogger<CPUDataClient> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<CPU>? ReturnAllCPUs()
        {
            _logger.LogInformation($"--> Calling GRPC Service {_configuration["GrpcCPU"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcCPU"]);
            var client = new GrpcCPU.GrpcCPUClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllCPUs(request);
                return _mapper.Map<IEnumerable<CPU>>(reply.CPU);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
