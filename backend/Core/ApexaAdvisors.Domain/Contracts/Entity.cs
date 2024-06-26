using System.Collections.ObjectModel;

namespace ApexaAdvisors.Domain.Contracts;

public abstract class Entity
{
    public Guid Id { get; set; }

    private List<IDomainEvent>? _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly()
        ?? ReadOnlyCollection<IDomainEvent>.Empty;

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= [];
        if (!_domainEvents.Contains(eventItem))
            _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient()
    {
        return Id == default;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity item)
            return false;

        if (ReferenceEquals(this, item))
            return true;

        if (GetType() != item.GetType())
            return false;

        if (item.IsTransient() || IsTransient())
            return false;

        return item.Id == Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            return Id.GetHashCode();
        }

        return base.GetHashCode();
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (Equals(left, null))
            return Equals(right, null);

        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
