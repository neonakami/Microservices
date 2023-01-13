using AutoMapper;
using CPUMiroservice.Data;
using CPUMiroservice.Protos;
using Grpc.Core;

namespace CPUMiroservice.SyncDataServices.Grpc
{
    public class GrpcCPUService : GrpcCPU.GrpcCPUBase
    {
        private readonly ICPURepository _repository;
        private readonly ILogger<GrpcCPUService> _logger;
        private readonly IMapper _mapper;

        public GrpcCPUService(ICPURepository repository, ILogger<GrpcCPUService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override Task<CPUResponse> GetAllCPUs(GetAllRequest request, ServerCallContext context)
        {
            var response = new CPUResponse();
            var CPUs = _repository.GetAllCPUs();

            foreach (var CPU in CPUs)
            {
                response.CPU.Add(_mapper.Map<GrpcCPUModel>(CPU));
            }

            return Task.FromResult(response);
        }
    }
}
