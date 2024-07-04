using EasyBook.Domain.Entities;

namespace EasyBook.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Subscription subscription);
    void Update(Subscription subscription);
    Task<Subscription> Delete(int id);
}