using EasyBook.Domain.Entities;
using EasyBook.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBook.Infrastructure.Repository
{
    public class SubscriptionDbRepository : ISubscriptionRepository
    {

        public void Add(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Subscription>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Subscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}
