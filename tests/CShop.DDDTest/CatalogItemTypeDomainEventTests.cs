using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.DDDTest
{
	public class CatalogItemTypeDomainEventTests
	{
		[Fact]
		public void Deactivate_ShouldAddDeactivatedEvent_WhenTypeIsActive()
		{
			// Arrange
			var itemType = new CatalogItemType(new Name("Type A"));

			// Act
			itemType.Deactivate();

			// Assert
			var domainEvents = itemType.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemTypeDeactivatedEvent);

			var deactivationEvent = domainEvents.OfType<CatalogItemTypeDeactivatedEvent>().FirstOrDefault();
			Assert.NotNull(deactivationEvent);
			Assert.Equal(itemType.Id, deactivationEvent.CatalogItemTypeId);
		}

		[Fact]
		public void Activate_ShouldAddActivatedEvent_WhenTypeIsInactive()
		{
			// Arrange
			var itemType = new CatalogItemType(new Name("Type A"), false);

			// Act
			itemType.Activate();

			// Assert
			var domainEvents = itemType.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemTypeActivatedEvent);

			var activationEvent = domainEvents.OfType<CatalogItemTypeActivatedEvent>().FirstOrDefault();
			Assert.NotNull(activationEvent);
			Assert.Equal(itemType.Id, activationEvent.CatalogItemTypeId);
		}
	}
}
