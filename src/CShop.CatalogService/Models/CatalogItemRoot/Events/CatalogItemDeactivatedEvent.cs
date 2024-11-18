using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemDeactivatedEvent(long id) : INotification
	{
		public long ItemId { get; } = id;
	}
}