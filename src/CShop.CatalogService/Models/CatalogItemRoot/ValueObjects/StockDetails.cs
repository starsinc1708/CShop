using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot.ValueObjects
{
	public class StockDetails : ValueObject
	{
		public int AvailableStock { get; private set; }
		public int RestockThreshold { get; private set; }
		public int MaxStockThreshold { get; private set; }
		public bool MaxStockThreshHoldReached { get; private set; }
		public bool RestockThreshHoldReached { get; private set; }

		public StockDetails(int availableStock, int restockThreshold, int maxStockThreshold)
		{
			if (availableStock < 0)
				throw new ArgumentOutOfRangeException(nameof(availableStock), "Available stock cannot be negative.");

			if (restockThreshold < 0)
				throw new ArgumentOutOfRangeException(nameof(restockThreshold), "Restock threshold cannot be negative.");

			if (maxStockThreshold <= 0)
				throw new ArgumentOutOfRangeException(nameof(maxStockThreshold), "Max stock threshold must be greater than zero.");

			if (restockThreshold >= maxStockThreshold)
				throw new ArgumentException("Restock threshold must be less than max stock threshold.");

			AvailableStock = availableStock;
			RestockThreshold = restockThreshold;
			MaxStockThreshold = maxStockThreshold;

			MaxStockThreshHoldReached = AvailableStock >= MaxStockThreshold;
			RestockThreshHoldReached = AvailableStock <= RestockThreshold;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return AvailableStock;
			yield return RestockThreshold;
			yield return MaxStockThreshold;
		}
	}
}
