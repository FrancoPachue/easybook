using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;
using EasyBook.Domain.Entities;
using EasyBook.Domain.Enums;
using EasyBook.Domain.Errors;
using EasyBook.Domain.Repositories;
using EasyBook.Domain.Shared;
using EasyBook.Infrastructure.Dtos;
using EasyBook.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;

internal sealed class CreateSubscriptionConfigCommandHandler : ICommandHandler<CreateSubscriptionConfigCommand>
{
    private readonly PulsarService _pulsarService;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateSubscriptionConfigCommandHandler> _logger;

    public CreateSubscriptionConfigCommandHandler(
        PulsarService pulsarService,
        ISubscriptionRepository memberRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateSubscriptionConfigCommandHandler> logger)
    {
        _pulsarService = pulsarService;
        _subscriptionRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(CreateSubscriptionConfigCommand request, CancellationToken cancellationToken)
    {
        try
        {           
            _subscriptionRepository.Add(request.Subscription);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var pulsarSubscription = new PulsarSubscription(OperationType.Insert, request.Subscription);
            var json = JsonConvert.SerializeObject(pulsarSubscription);
            var result = await _pulsarService.SendMessageToTopic(json);
            if (result.IsFailure)
            {
                _logger.LogError($"Error : {GetType().Name} - {result.Error.Message}");
                return result;
            }
            _logger.LogInformation($"{GetType().Name} - Endpoint: {request.Subscription.Endpoint}, " +
                                         $"Parameters: {string.Join(", ", request.Subscription.Parameters.Select(p => $"{p.Name}: {p.Value}"))}");
            return Result.Success();
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error : {GetType().Name} - {ex.Message}");
            return Result.Failure(new Error($"{ex.Source}",$"{ex.Message}"));
        }
    }
}