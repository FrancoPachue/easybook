using EasyBook.Application.Abstractions.Messaging;

namespace EasyBook.Application.SubscriptionsConfigs.Queries.GetAllSubscriptionsConfigs;

public sealed record GetAllSubscriptionsConfigsQuery() : IQuery<AllSubscriptionConfigResponse>;