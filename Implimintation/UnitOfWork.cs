using Shopify.API.Abstract;
using Shopify.API.Data;
using Shopify.API.Models;

namespace Shopify.API.Implimintation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbcontext _context;

		public UnitOfWork(ApplicationDbcontext context)
		{
			_context = context;
		}

		public IBaseRepository<Category> categories => new BaseRepository<Category>(_context);

		public IBaseRepository<Product> Products => new BaseRepository<Product>(_context);

		public int Commit() => _context.SaveChanges();
	}
}
