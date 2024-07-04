
using DotPulsar;
using DotPulsar.Extensions;
using EasyBook.Domain;
using EasyBook.Domain.Errors;
using EasyBook.Domain.Shared;
using EasyBook.Infrastructure.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBook.Infrastructure.Services
{

    public class PulsarService:IPulsarService
    {
        private readonly ILogger<PulsarService> _logger;
        public PulsarService(ILogger<PulsarService> logger)
        {
            _logger = logger;
        }

        public async Task<Result> SendMessageToTopic(string message)
        {
            try
            {
                await using var client = PulsarClient.Builder().Build();
                await using var producer = client.NewProducer().Topic($"topic-configs").Create();
                await producer.Send(Encoding.UTF8.GetBytes(message));
                _logger.LogInformation($"Message sended: {message}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Source} : {ex.Message}");
                return Result.Failure(InfraErrors.Pulsar.ConnectionIssue);
            }
        }
    }
}
