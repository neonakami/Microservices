using CPUMiroservice.Dtos;
using System.Text;
using System.Text.Json;

namespace CPUMiroservice.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpCommandDataClient> _logger;

        public HttpCommandDataClient (HttpClient client, IConfiguration configuration, ILogger<HttpCommandDataClient> logger)
        {
            _client = client;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendCPUToCommand(CPUReadDTO CPU)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(CPU),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Sync POST to CommandService was OK!");
            }
            else
            {
                _logger.LogWarning("--> Sync POST to CommandService was NOT OK!");
            }
        }
    }
}
