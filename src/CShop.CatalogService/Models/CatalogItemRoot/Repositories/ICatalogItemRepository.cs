using CShop.DomainDefaults;

namespace CShop.CatalogService.Models.CatalogItemRoot.Repositories
{
	public interface ICatalogItemRepository : IRepository<CatalogItem>
	{
		Task<IEnumerable<CatalogItem>> GetPagedItemsAsync(int pageSize, int pageIndex, CancellationToken token = default);
		Task<int> GetItemCountAsync(CancellationToken token = default);
	}
}
