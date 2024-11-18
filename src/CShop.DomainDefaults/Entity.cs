namespace CShop.DomainDefaults
{
	public abstract class Entity
	{
		protected int? _requestedHashCode;
		public virtual long Id { get; protected set; }

		private readonly List<INotification> _domainEvents = [];

		public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

		public void AddDomainEvent(INotification eventItem)
		{
			_domainEvents.Add(eventItem);
		}

		public void RemoveDomainEvent(INotification eventItem)
		{
			_domainEvents.Remove(eventItem);
		}

		public void ClearDomainEvents()
		{
			_domainEvents.Clear();
		}

		public bool IsTransient()
		{
			return Id == default;
		}

		public override bool Equals(object obj)
		{
			if (obj is not Entity entity)
				return false;

			if (ReferenceEquals(this, entity))
				return true;

			if (GetType() != entity.GetType())
				return false;

			if (entity.IsTransient() || IsTransient())
				return false;
			
			return entity.Id == Id;
		}

		public override int GetHashCode()
		{
			if (!IsTransient())
			{
				if (!_requestedHashCode.HasValue)
					_requestedHashCode = HashCode.Combine(Id, 31);

				return _requestedHashCode.Value;
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
}
