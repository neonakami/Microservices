using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using System.Text.Json;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor (IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CPUPublished:
                    {
                        AddCPU(message);
                    }
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            _logger.LogInformation("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notifcationMessage);

            switch (eventType.Event)
            {
                case "CPU_Published":
                    {
                        _logger.LogInformation("--> CPU Published Event Detected");

                        return EventType.CPUPublished;
                    }
                default:
                    {
                        _logger.LogInformation("--> Could not determine the event type");

                        return EventType.Undetermined;
                    }
            }
        }

        private void AddCPU(string fabricPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

                var fabricPublishedDto = JsonSerializer.Deserialize<CPUPublishedDTO>(fabricPublishedMessage);

                try
                {
                    var plat = _mapper.Map<CPU>(fabricPublishedDto);
                    if (!repository.ExternalCPUExists(plat.ExternalID))
                    {
                        repository.CreateCPU(plat);
                        repository.SaveChanges();

                        _logger.LogInformation("--> CPU added!");
                    }
                    else
                    {
                        _logger.LogWarning("--> CPU already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"--> Could not add CPU to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        CPUPublished,
        Undetermined
    }
}
