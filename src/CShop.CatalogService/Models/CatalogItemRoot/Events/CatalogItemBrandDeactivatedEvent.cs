using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemBrandDeactivatedEvent(long id) : INotification
	{
		public long BrandId { get; } = id;
	}
}