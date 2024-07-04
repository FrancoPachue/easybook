using EasyBook.Domain.Entities;
using EasyBook.Domain.Enums;

namespace EasyBook.Infrastructure.Dtos
{
    public class PulsarSubscription
    {
        public PulsarSubscription(OperationType operationType, Subscription subscription)
        {
            OperationType = operationType;
            Subscription = subscription;
        }

        public OperationType OperationType { get; set; }
        public Subscription Subscription { get; set; }
    }
}
