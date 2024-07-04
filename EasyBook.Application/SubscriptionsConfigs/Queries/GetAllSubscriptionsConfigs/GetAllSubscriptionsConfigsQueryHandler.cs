using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Domain.Repositories;
using EasyBook.Domain.Shared;

namespace EasyBook.Application.SubscriptionsConfigs.Queries.GetAllSubscriptionsConfigs;

internal sealed class GetAllSubscriptionsConfigsQueryHandler
    : IQueryHandler<GetAllSubscriptionsConfigsQuery, AllSubscriptionConfigResponse>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetAllSubscriptionsConfigsQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Result<AllSubscriptionConfigResponse>> Handle(
        GetAllSubscriptionsConfigsQuery request,
        CancellationToken cancellationToken)
    {
        var subscriptions = await _subscriptionRepository.GetAllAsync(
            cancellationToken);

        if (subscriptions is null)
        {
            return Result.Failure<AllSubscriptionConfigResponse>(new Error(
                "Subscription.NotFound",
                $"No subs was found"));
        }

        var response = new AllSubscriptionConfigResponse(subscriptions);

        return response;
    }
}