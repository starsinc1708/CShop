using CShop.CatalogService.Models.CatalogItemRoot.Events;
using CShop.CatalogService.Models.CatalogItemRoot.ValueObjects;
using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot
{
	public class CatalogItemType : Entity
	{
		public Name Name { get; private set; }
		public bool IsActive { get; private set; }

		public IEnumerable<CatalogItem> CatalogItems { get; private set; }

		private CatalogItemType() { }

		public CatalogItemType(Name name, bool isActive = true)
		{
			Name = name;
			IsActive = isActive;
		}

		public void Deactivate()
		{
			if (!IsActive)
				throw new InvalidOperationException("Catalog item type is already inactive.");

			IsActive = false;

			AddDomainEvent(new CatalogItemTypeDeactivatedEvent(Id));
		}

		public void Activate()
		{
			if (IsActive)
				throw new InvalidOperationException("Catalog item type is already active.");

			IsActive = true;

			AddDomainEvent(new CatalogItemTypeActivatedEvent(Id));
		}
	}
}
