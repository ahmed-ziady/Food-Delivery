namespace FoodDelivery.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType>
    : Entity<TId>, IAggregateRootMarker
    where TId : AggregateRootId<TIdType>
{
    protected AggregateRoot() { }

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

}
