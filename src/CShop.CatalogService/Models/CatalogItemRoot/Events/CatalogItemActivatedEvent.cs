using MediatR;

namespace CShop.CatalogService.Models.CatalogItemRoot.Events
{
	public class CatalogItemActivatedEvent(long id) : INotification
	{
		public long ItemId { get; } = id;
	}
}