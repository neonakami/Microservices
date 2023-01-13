using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/fabrics/{fabricId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommandRepository> _logger;

        public CommandsController(ICommandRepository repository, IMapper mapper, ILogger<CommandRepository> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForCPU(int fabricId)
        {
            _logger.LogInformation($"--> Hit GetCommandsForCPU: {fabricId}");

            if (!_repository.CPUExits(fabricId))
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(_repository.GetCommandsForCPU(fabricId)));
        }

        [HttpGet("by-command-id/{commandId}", Name = "GetCommandForCPU")]
        public ActionResult<CommandReadDTO> GetCommandForCPU(int fabricId, int commandId)
        {
            _logger.LogInformation($"--> Hit GetCommandForCPU: {fabricId} / {commandId}");

            if (!_repository.CPUExits(fabricId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(fabricId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForCPU(int fabricId, CommandCreateDTO commandCreateDTO)
        {
            _logger.LogInformation($"--> Hit CreateCommandForCPU: {fabricId}");

            if (!_repository.CPUExits(fabricId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreateDTO);

            _repository.CreateCommand(fabricId, command);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            return CreatedAtRoute(nameof(GetCommandForCPU),
                new { fabricId = fabricId, commandId = commandReadDTO.Id }, commandReadDTO);
        }
        #endregion
    }
}
