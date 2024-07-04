using EasyBook.Domain.Entities;
using EasyBook.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyBook.Infrastructure.Repository
{
    public class InMemoryContext : DbContext, IUnitOfWork
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public InMemoryContext(DbContextOptions<InMemoryContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(InMemoryContext).Assembly);
    }
}
