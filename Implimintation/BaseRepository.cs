namespace Shopify.API.Implimintation
{
	public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{
		private readonly ApplicationDbcontext _context;
		readonly DbSet<T> _entities;
		public BaseRepository(ApplicationDbcontext context)
		{
			_context = context;
			_entities = _context.Set<T>();
		}
		public async ValueTask<EntityEntry<T>> CreateAsync(T entity)
		{
			return await _entities.AddAsync(entity);
		}
		public ValueTask<EntityEntry<T>> DeleteAsync(T entity)
		{
			return ValueTask.FromResult(_entities.Remove(entity));
		}
		public ValueTask<EntityEntry<T>> UpdateAsync(T entity)
		{
			return ValueTask.FromResult(_entities.Update(entity));
		}

		public async Task<T?> FindByIdAsync(int id)
		{
			return await _entities.SingleOrDefaultAsync(pk => pk.Id == id);
		}
		public async Task<T?> FindByCreatriaAsync(Expression<Func<T, bool>> predicate)
		{
			return await _entities.SingleOrDefaultAsync(predicate);
		}
		public async Task<IReadOnlyCollection<T>> FindByCreatriaAsync(Expression<Func<T, bool>> predicate, bool stopTracking = false)
		{
			var query = _entities.AsQueryable();
			if (!stopTracking)
				query = query.AsNoTracking();

			return await query.Where(predicate).ToListAsync();
		}
		public async Task<IReadOnlyCollection<T>> GetAsync(
			Expression<Func<T, bool>> predicate = null
			, bool stopTracking = false
			, string orderByDirection = "ASC")
		{
			var query = _entities.AsQueryable();
			if (!stopTracking)
				query = query.AsNoTracking();

			if (predicate != null)
				query = query.Where(predicate);

			if (orderByDirection == "DESC")
				query = query.OrderByDescending( o=> o.Id);

			return await query.ToListAsync();
		}
		public async Task<IReadOnlyCollection<T>> GetAsync(
			Expression<Func<T, bool>> predicate = null
			, bool stopTracking = false
			, string orderByDirection = "ASC"
			, int take = 0
			, int skip = 0)
		{
			var query = _entities.AsQueryable();
			if (!stopTracking)
				query = query.AsNoTracking();

			if (predicate != null)
				query = query.Where(predicate);

			if(take > 0)
				query = query.Take(take);

			if (skip > 0)
				query = query.Skip(skip);

			if (orderByDirection == "DESC")
				query = query.OrderByDescending(o => o.Id);

			return await query.ToListAsync();
		}
		public async Task<IReadOnlyCollection<T>> GetAsync(
		Expression<Func<T, bool>> predicate = null
		, string[] includes = null
		, bool stopTracking = false
		, string orderByDirection = "ASC"
		, int take = 0
		, int skip = 0)
		{
			var query = _entities.AsQueryable();
			if (!stopTracking)
				query = query.AsNoTracking();

			if (includes != null)
				foreach (var include in includes)
					query = query.Include(include);

			if (predicate != null)
				query = query.Where(predicate);

			if (take > 0)
				query = query.Take(take);

			if (skip > 0)
				query = query.Skip(skip);

			if (orderByDirection == "DESC")
				query = query.OrderByDescending(o => o.Id);

			return await query.ToListAsync();

		}
	}
}
