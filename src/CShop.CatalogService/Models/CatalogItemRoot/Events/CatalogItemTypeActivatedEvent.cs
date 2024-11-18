using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemTypeActivatedEvent(long id) : INotification
	{
		public long TypeId { get; } = id;
	}
}