using CShop.DomainDefaults;
using System.Linq.Expressions;

namespace CShop.DomainDefaults
{
	public interface IRepository<T> where T : Entity
	{
		Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
		Task<T> GetByNameAsync(string name, CancellationToken token = default);
		Task AddAsync(T entity, CancellationToken cancellationToken = default);
		Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
		Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
