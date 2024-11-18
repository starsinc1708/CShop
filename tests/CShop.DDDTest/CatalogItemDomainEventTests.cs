using CShop.CatalogService.Models.CatalogItemRoot;
using CShop.CatalogService.Models.CatalogItemRoot.Events;
using CShop.CatalogService.Models.CatalogItemRoot.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.DDDTest
{
	public class CatalogItemDomainEventTests
	{
		[Fact]
		public void ReduceStock_ShouldAddRestockThresholdTriggeredEvent_WhenAvailableStockReachesThreshold()
		{
			// Arrange
			var stock = new StockDetails(5, 3, 10);
			var item = CreateTestCatalogItem(stock);

			// Act
			item.ReduceStock(3);

			// Assert
			var domainEvents = item.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemRestockThresholdTriggeredEvent);

			var restockEvent = domainEvents.OfType<CatalogItemRestockThresholdTriggeredEvent>().FirstOrDefault();
			Assert.NotNull(restockEvent);
			Assert.Equal(item.Id, restockEvent.ItemId);
			Assert.Equal(2, restockEvent.Stock.AvailableStock); // Stock after reduction
		}

		[Fact]
		public void IncreaseStock_ShouldAddMaxStockThresholdReachedEvent_WhenAvailableStockExceedsMaxThreshold()
		{
			// Arrange
			var stock = new StockDetails(8, 2, 10);
			var item = CreateTestCatalogItem(stock);

			// Act
			item.IncreaseStock(5); // Exceeds max threshold

			// Assert
			var domainEvents = item.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemMaxStockThresholdReachedEvent);

			var maxStockEvent = domainEvents.OfType<CatalogItemMaxStockThresholdReachedEvent>().FirstOrDefault();
			Assert.NotNull(maxStockEvent);
			Assert.Equal(item.Id, maxStockEvent.ItemId);
			Assert.Equal(13, maxStockEvent.Stock.AvailableStock); // Stock after addition
		}

		[Fact]
		public void Activate_ShouldAddActivatedEvent_WhenItemWasInactive()
		{
			// Arrange
			var item = CreateTestCatalogItem();
			item.Deactivate(); // Ensure item is inactive

			// Act
			item.Activate();

			// Assert
			var domainEvents = item.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemActivatedEvent);

			var activationEvent = domainEvents.OfType<CatalogItemActivatedEvent>().FirstOrDefault();
			Assert.NotNull(activationEvent);
			Assert.Equal(item.Id, activationEvent.ItemId);
		}

		[Fact]
		public void Deactivate_ShouldAddDeactivatedEvent_WhenItemWasActive()
		{
			// Arrange
			var item = CreateTestCatalogItem();

			// Act
			item.Deactivate();

			// Assert
			var domainEvents = item.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemDeactivatedEvent);

			var deactivationEvent = domainEvents.OfType<CatalogItemDeactivatedEvent>().FirstOrDefault();
			Assert.NotNull(deactivationEvent);
			Assert.Equal(item.Id, deactivationEvent.ItemId);
		}

		private CatalogItem CreateTestCatalogItem(StockDetails? stock = null)
		{
			var name = new Name("Test Item");
			var description = new Description("Test Description");
			var price = new Price(100);
			var stockDetails = stock ?? new StockDetails(10, 2, 20);
			return new CatalogItem(name, description, price, stockDetails);
		}
	}
}
