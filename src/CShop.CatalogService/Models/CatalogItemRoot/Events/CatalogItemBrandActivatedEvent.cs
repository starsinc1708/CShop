using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemBrandActivatedEvent(long id) : INotification
	{
		public long BrandId { get; } = id;
	}
}