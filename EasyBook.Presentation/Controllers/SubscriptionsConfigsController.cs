using EasyBook.Application.SubscriptionsConfigs.Commands.CreateSubscriptionConfig;
using EasyBook.Application.SubscriptionsConfigs.Commands.DeleteSubscriptionConfig;
using EasyBook.Application.SubscriptionsConfigs.Commands.UpdateSubscriptionConfig;
using EasyBook.Application.SubscriptionsConfigs.Queries.GetAllSubscriptionsConfigs;
using EasyBook.Application.SubscriptionsConfigs.Queries.GetSubscriptionConfigById;
using EasyBook.Domain.Entities;
using EasyBook.Domain.Shared;
using EasyBook.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyBook.Presentation.Controllers;

[Route("api/subscriptions")]
public sealed class SubscriptionsConfigsController : ApiController
{
    public SubscriptionsConfigsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscriptionConfig(Subscription subscription,CancellationToken cancellationToken)
    {
        var command = new CreateSubscriptionConfigCommand(
            subscription);

        var result = await Sender.Send(command, cancellationToken);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSubscriptionConfig(Subscription subscription, CancellationToken cancellationToken)
    {
        var command = new UpdateSubscriptionConfigCommand(
            subscription);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscriptionConfig(int id, CancellationToken cancellationToken)
    {
        var query = new DeleteSubscriptionConfigCommand(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok() : NotFound(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionConfigById(string id, CancellationToken cancellationToken)
    {
        var query = new GetSubscriptionConfigByIdQuery(id);

        Result<SubscriptionConfigResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubscriptionsConfigs(CancellationToken cancellationToken)
    {
        var query = new GetAllSubscriptionsConfigsQuery();

        Result<AllSubscriptionConfigResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}