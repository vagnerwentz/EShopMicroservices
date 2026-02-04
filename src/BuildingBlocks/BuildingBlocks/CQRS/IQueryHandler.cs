using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>, IRequest<TResponse>
    where TResponse : notnull
{
    
}