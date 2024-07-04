using EasyBook.Application.Abstractions.Messaging;
using EasyBook.Domain.Repositories;
using EasyBook.Domain.Shared;

namespace EasyBook.Application.SubscriptionsConfigs.Queries.GetSubscriptionConfigById;

internal sealed class GetSubscriptionConfigByIdQueryHandler
    : IQueryHandler<GetSubscriptionConfigByIdQuery, SubscriptionConfigResponse>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionConfigByIdQueryHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<Result<SubscriptionConfigResponse>> Handle(
        GetSubscriptionConfigByIdQuery request,
        CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (subscription is null)
        {
            return Result.Failure<SubscriptionConfigResponse>(new Error(
                "Member.NotFound",
                $"The subscription with Id {request.Id} was not found"));
        }

        var response = new SubscriptionConfigResponse(subscription);

        return response;
    }
}