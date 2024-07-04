using EasyBook.Domain.Entities;

namespace EasyBook.Application.SubscriptionsConfigs.Queries.GetAllSubscriptionsConfigs;

public sealed record AllSubscriptionConfigResponse(List<Subscription> subscriptions);