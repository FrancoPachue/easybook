using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Domain.Entities;

namespace EasyBook.Application.SubscriptionsConfigs.Commands.UpdateSubscriptionConfig;

public sealed record UpdateSubscriptionConfigCommand(Subscription Subscription) : ICommand;