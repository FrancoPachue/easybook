using EasyBook.Application.Abstractions.Messaging;

namespace EasyBook.Application.SubscriptionsConfigs.Commands.DeleteSubscriptionConfig;

public sealed record DeleteSubscriptionConfigCommand(
    int SubscriptionConfigId) : ICommand;