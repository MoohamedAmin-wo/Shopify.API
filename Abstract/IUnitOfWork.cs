namespace Shopify.API.Abstract
{
	public interface IUnitOfWork
	{
		IBaseRepository<Category> categories { get; }
		IBaseRepository<Product> Products { get; }
		int Commit();
	}
}
