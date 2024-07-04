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

namespace EasyBook.Application.SubscriptionsConfigs.Commands.UpdateSubscriptionConfig;

internal sealed class UpdateSubscriptionConfigCommandHandler : ICommandHandler<UpdateSubscriptionConfigCommand>
{
    private readonly PulsarService _pulsarService;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSubscriptionConfigCommandHandler> _logger;

    public UpdateSubscriptionConfigCommandHandler(
        ILogger<UpdateSubscriptionConfigCommandHandler> logger,
        PulsarService pulsarService,
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
        _pulsarService = pulsarService;
    }

    public async Task<Result> Handle(UpdateSubscriptionConfigCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var subscription = request.Subscription;
            _subscriptionRepository.Update(subscription);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var pulsarSubscription = new PulsarSubscription(OperationType.Update, subscription);
            var json = JsonConvert.SerializeObject(pulsarSubscription);
            var result = await _pulsarService.SendMessageToTopic(json);
            if (result.IsFailure)
            {
                _logger.LogError($"Error : {GetType().Name} - {result.Error.Message}");
                return result;
            }
            _logger.LogInformation($"{GetType().Name} - Endpoint: {subscription.Endpoint}, " +
                             $"Parameters: {string.Join(", ", subscription.Parameters.Select(p => $"{p.Name}: {p.Value}"))}");
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error : {GetType().Name} - {ex.Message}");
            return Result.Failure(new Error(ex.Source, ex.Message));
        }
    }
}