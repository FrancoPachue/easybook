using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Domain.Entities;

namespace EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;

public sealed record CreateSubscriptionConfigCommand(
    Subscription Subscription) : ICommand;