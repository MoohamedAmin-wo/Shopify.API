namespace Shopify.API.Abstract
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		ValueTask<EntityEntry<T>> CreateAsync(T entity);
		ValueTask<EntityEntry<T>> DeleteAsync(T entity);
		ValueTask<EntityEntry<T>> UpdateAsync(T entity);

		Task<T?> FindByIdAsync(int id);
		Task<T?> FindByCreatriaAsync(Expression<Func<T, bool>> predicate);
		Task<IReadOnlyCollection<T>> FindByCreatriaAsync(Expression<Func<T, bool>> predicate, bool stopTracking = false);
		Task<IReadOnlyCollection<T>> GetAsync(
			Expression<Func<T, bool>> predicate = null
			, bool stopTracking = false
			, string orderByDirection = "ASC");
		Task<IReadOnlyCollection<T>> GetAsync(
			Expression<Func<T, bool>> predicate = null
			, bool stopTracking = false
			, string orderByDirection = "ASC"
			, int take = 0
			, int skip = 0);
		Task<IReadOnlyCollection<T>> GetAsync(
		Expression<Func<T, bool>> predicate = null
		, string[] includes = null
		, bool stopTracking = false
		, string orderByDirection = "ASC"
		, int take = 0
		, int skip = 0);

	}

}
