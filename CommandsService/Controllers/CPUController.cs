using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CPUController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CPUController> _logger;

        public CPUController(ICommandRepository repository, IMapper mapper, ILogger<CPUController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<CPUReadDTO>> GetCPUs()
        {
            _logger.LogInformation("--> Getting CPUs from CommandsService");

            return Ok(_mapper.Map<IEnumerable<CPUReadDTO>>(_repository.GetAllCPUs()));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            _logger.LogInformation("--> Inbound POST # Command Service");

            return Ok("Inbound test of from CPUs Controler");
        }
        #endregion
    }
}
