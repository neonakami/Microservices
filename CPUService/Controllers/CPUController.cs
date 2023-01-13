using AutoMapper;
using CPUMiroservice.AsyncDataServices;
using CPUMiroservice.Data;
using CPUMiroservice.Dtos;
using CPUMiroservice.Models;
using CPUMiroservice.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace CPUMiroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPUController : ControllerBase
    {
        private readonly ICPURepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly ILogger<CPUController> _logger;
        private readonly IMessageBusClient _messageBusClient;

        public CPUController(ICPURepository repository, IMapper mapper, ICommandDataClient commandDataClient, 
            IMessageBusClient messageBusClient, ILogger<CPUController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<CPUReadDTO>> GetAllCPUs()
        {
            _logger.LogInformation("--> Getting CPUs....");

            return Ok(_mapper.Map<IEnumerable<CPUReadDTO>>(_repository.GetAllCPUs()));
        }

        [HttpGet("get/{id}", Name = "GetCPUById")]
        public ActionResult<CPUReadDTO> GetCPUById(int id) 
        {
            var CPU = _repository.GetCPUById(id);

            if (CPU != null)
            {
                return Ok(_mapper.Map<CPUReadDTO>(CPU));
            }

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult<CPUReadDTO>> CreateCPU(CPUCreateDTO CPUCreateDTO)
        {
            var CPUToCreate = _mapper.Map<CPU>(CPUCreateDTO);

            _repository.CreateCPU(CPUToCreate);
            _repository.SaveChanges();

            var CPUReadDTO = _mapper.Map<CPUReadDTO>(CPUToCreate);

            try
            {
                await _commandDataClient.SendCPUToCommand(CPUReadDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not send synchronously: {ex.Message}");
            }

            try
            {
                var CPUPublishedDTO = _mapper.Map<CPUPublishedDTO>(CPUReadDTO);
                CPUPublishedDTO.Event = "CPU_Published";
                _messageBusClient.PublishNewCPU(CPUPublishedDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetCPUById), new { Id = CPUReadDTO.Id }, CPUReadDTO);
        }
        #endregion
    }
}
