using EasyBook.Domain.Shared;
using MediatR;

namespace EasyBook.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
