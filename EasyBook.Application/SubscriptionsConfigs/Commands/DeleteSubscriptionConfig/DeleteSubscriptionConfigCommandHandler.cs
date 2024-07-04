using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;
using EasyBook.Application.SubscriptionsConfigs.Commands.DeleteSubscriptionConfig;
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

namespace EasyBook.Application.SubscriptionsConfigs.Commands.DeleteSubscriptionConfig;

internal sealed class DeleteSubscriptionConfigCommandHandler : ICommandHandler<DeleteSubscriptionConfigCommand>
{
    private readonly PulsarService _pulsarService;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSubscriptionConfigCommandHandler> _logger;

    public DeleteSubscriptionConfigCommandHandler(
        PulsarService pulsarService,
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSubscriptionConfigCommandHandler> logger)
    {
        _pulsarService = pulsarService;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteSubscriptionConfigCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var id = request.SubscriptionConfigId;
            var subDeleted = await _subscriptionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);           
            var pulsarSubscription = new PulsarSubscription(OperationType.Delete, subDeleted);
            var json = JsonConvert.SerializeObject(pulsarSubscription);
            var result = await _pulsarService.SendMessageToTopic(json);
            if (result.IsFailure)
            {
                _logger.LogError($"Error : {GetType().Name} - {result.Error.Message}");
                return result;
            }
            _logger.LogInformation($"{GetType().Name} - Endpoint: {subDeleted.Endpoint}, " +
                             $"Parameters: {string.Join(", ", subDeleted.Parameters.Select(p => $"{p.Name}: {p.Value}"))}");
            return Result.Success();
        }
        catch(Exception ex)
        {
            _logger.LogError($"Error : {GetType().Name} - {ex.Message}");
            return Result.Failure(new Error(ex.Source,ex.Message));
        }
    }
}