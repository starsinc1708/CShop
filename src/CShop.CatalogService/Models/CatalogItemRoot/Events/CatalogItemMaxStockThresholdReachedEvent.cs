using CShop.CatalogService.Models.CatalogItemRoot.ValueObjects;
using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemMaxStockThresholdReachedEvent(long id, StockDetails stock) : INotification
	{
		public long ItemId { get; } = id;
		public StockDetails Stock { get; } = stock;
	}
}