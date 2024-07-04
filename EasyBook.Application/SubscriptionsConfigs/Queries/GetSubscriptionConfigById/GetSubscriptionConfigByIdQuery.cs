using EasyBook.Application.Abstractions.Messaging;

namespace EasyBook.Application.SubscriptionsConfigs.Queries.GetSubscriptionConfigById;

public sealed record GetSubscriptionConfigByIdQuery(string Id) : IQuery<SubscriptionConfigResponse>;