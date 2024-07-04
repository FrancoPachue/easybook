using EasyBook.Domain.Shared;
using MediatR;

namespace EasyBook.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}