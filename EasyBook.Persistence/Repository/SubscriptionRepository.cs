using EasyBook.Domain.Entities;
using EasyBook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyBook.Infrastructure.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly InMemoryContext _dbContext;

        public SubscriptionRepository(InMemoryContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<Subscription> GetByIdAsync(string id, CancellationToken cancellationToken) => await _dbContext.Subscriptions.Include(s => s.Parameters).FirstOrDefaultAsync(s => s.Id.ToString() == id);

        public async Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken) => await _dbContext.Subscriptions.Include(s => s.Parameters).ToListAsync();
        public void Add(Subscription subscription) => _dbContext.Subscriptions.Add(subscription);
        public void Update(Subscription subscription) => _dbContext.Subscriptions.Update(subscription);
        public async Task<Subscription> Delete(int id)
        {
            var subscriptionToDelete = await _dbContext.Subscriptions.FindAsync(id);
            if (subscriptionToDelete != null)
            {
                _dbContext.Set<Subscription>().Remove(subscriptionToDelete);
            }
            return subscriptionToDelete;
        }
    }
}