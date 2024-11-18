using CShop.CatalogService.Models.CatalogItemRoot.Events;
using CShop.CatalogService.Models.CatalogItemRoot.ValueObjects;
using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot
{
	public class CatalogItemBrand : Entity
	{
		public Name Name { get; private set; }
		public Description Description { get; private set; }
		public bool IsActive { get; private set; }

		public IEnumerable<CatalogItem> CatalogItems { get; private set; }

		private CatalogItemBrand() { }

		public CatalogItemBrand(Name name, Description description, bool isActive = true)
		{
			Name = name;
			Description = description;
			IsActive = isActive;
		}

		public void Deactivate()
		{
			if (!IsActive)
				throw new InvalidOperationException("Catalog item Brand is already inactive.");

			IsActive = false;

			AddDomainEvent(new CatalogItemBrandDeactivatedEvent(Id));
		}

		public void Activate()
		{
			if (IsActive)
				throw new InvalidOperationException("Catalog item Brand is already active.");

			IsActive = true;

			AddDomainEvent(new CatalogItemBrandActivatedEvent(Id));
		}
	}
}
