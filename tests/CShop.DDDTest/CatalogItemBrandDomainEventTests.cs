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
	public class CatalogItemBrandDomainEventTests
	{
		[Fact]
		public void Deactivate_ShouldAddDeactivatedEvent_WhenBrandIsActive()
		{
			// Arrange
			var brand = new CatalogItemBrand(new Name("Brand A"), new Description("Desc A"));

			// Act
			brand.Deactivate();

			// Assert
			var domainEvents = brand.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemBrandDeactivatedEvent);

			var deactivationEvent = domainEvents.OfType<CatalogItemBrandDeactivatedEvent>().FirstOrDefault();
			Assert.NotNull(deactivationEvent);
			Assert.Equal(brand.Id, deactivationEvent.BrandId);
		}

		[Fact]
		public void Activate_ShouldAddActivatedEvent_WhenBrandIsInactive()
		{
			// Arrange
			var brand = new CatalogItemBrand(new Name("Brand A"), new Description("Desc A"), false);

			// Act
			brand.Activate();

			// Assert
			var domainEvents = brand.DomainEvents;
			Assert.Contains(domainEvents, e => e is CatalogItemBrandActivatedEvent);

			var activationEvent = domainEvents.OfType<CatalogItemBrandActivatedEvent>().FirstOrDefault();
			Assert.NotNull(activationEvent);
			Assert.Equal(brand.Id, activationEvent.BrandId);
		}
	}
}
