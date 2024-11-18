using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShop.DDDTest
{
	public class CatalogItemBrandDomainEventTests
	{
		[Fact]
		public void Deactivate_ShouldAddDeactivatedEvent_WhenBrandIsActive()
		{
			// Arrange
			var brand = new CatalogItemBrand(new Name("Brand A"));

			// Act
			brand.Deactivate();

			// Assert
			var domainEvents = brand.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemTypeDeactivatedEvent);

			var deactivationEvent = domainEvents.OfType<CatalogItemTypeDeactivatedEvent>().FirstOrDefault();
			Assert.NotNull(deactivationEvent);
			Assert.Equal(brand.Id, deactivationEvent.CatalogItemTypeId);
		}

		[Fact]
		public void Activate_ShouldAddActivatedEvent_WhenBrandIsInactive()
		{
			// Arrange
			var brand = new CatalogItemBrand(new Name("Brand A"), false);

			// Act
			brand.Activate();

			// Assert
			var domainEvents = brand.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemTypeActivatedEvent);

			var activationEvent = domainEvents.OfType<CatalogItemTypeActivatedEvent>().FirstOrDefault();
			Assert.NotNull(activationEvent);
			Assert.Equal(brand.Id, activationEvent.CatalogItemTypeId);
		}
	}
}
