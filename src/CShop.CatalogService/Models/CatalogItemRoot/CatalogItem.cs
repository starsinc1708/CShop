using CShop.CatalogService.Models.CatalogItemRoot.Events;
using CShop.CatalogService.Models.CatalogItemRoot.ValueObjects;
using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot
{
	public class CatalogItem : Entity, IAggregationRoot
	{
		public Name Name { get; private set; }
		public Description Description { get; private set; }
		public Price Price { get; private set; }
		public StockDetails Stock { get; private set; }
		public bool IsActive { get; private set; }

		public CatalogItemType Type { get; private set; }
		public CatalogItemBrand Brand { get; private set; }

		public CatalogItem() { }

		public CatalogItem(
			Name name,
			Description description,
			Price price,
			StockDetails stock,
			bool isActive = true)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Price = price ?? throw new ArgumentNullException(nameof(price));
			Stock = stock ?? throw new ArgumentNullException(nameof(stock));
			IsActive = isActive;
		}

		public CatalogItem(
			Name name,
			Description description,
			Price price,
			StockDetails stock,
			CatalogItemType type,
			CatalogItemBrand brand,
			bool isActive = true) : this(name, description, price, stock, isActive)
		{
			Type = type;
			Brand = brand;
		}

		public CatalogItem ReduceStock(int quantity)
		{
			if (quantity <= 0)
				throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

			if (Stock.AvailableStock < quantity)
				throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be less or equal available stock");

			Stock = new StockDetails(Stock.AvailableStock - quantity, Stock.RestockThreshold, Stock.MaxStockThreshold);

			if (Stock.RestockThreshHoldReached)
				AddDomainEvent(new CatalogItemRestockThresholdTriggeredEvent(Id, Stock));

			return this;
		}

		public CatalogItem IncreaseStock(int quantity)
		{
			if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

			Stock = new StockDetails(Stock.AvailableStock + quantity, Stock.RestockThreshold, Stock.MaxStockThreshold);

			if (Stock.MaxStockThreshHoldReached)
				AddDomainEvent(new CatalogItemMaxStockThresholdReachedEvent(Id, Stock));

			return this;
		}

		public CatalogItem Activate()
		{
			if (IsActive) return this;

			IsActive = true;
			AddDomainEvent(new CatalogItemActivatedEvent(Id));
			return this;
		}

		public CatalogItem Deactivate()
		{
			if (!IsActive) return this;

			IsActive = false;
			AddDomainEvent(new CatalogItemDeactivatedEvent(Id));
			return this;
		}
	}
}
