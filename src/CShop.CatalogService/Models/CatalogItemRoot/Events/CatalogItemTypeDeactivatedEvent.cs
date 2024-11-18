using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemTypeDeactivatedEvent(long id) : INotification
	{
		public long TypeId { get; } = id;
	}
}