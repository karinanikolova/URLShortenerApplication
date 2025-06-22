namespace URLShortenerApp.Data.Utilities.Contracts
{
	/// <summary>
	///  Providing a single database context for use across multiple services, thus making future database provider changes easier over time, if necessary.
	/// </summary>
	public interface IRepository
	{
		IQueryable<TEntity> All<TEntity>() where TEntity : class;

		IQueryable<TEntity> AllReadOnly<TEntity>() where TEntity : class;

		Task AddAsync<TEntity>(TEntity entity) where TEntity : class;

		void Add<TEntity>(TEntity entity) where TEntity : class;

		Task<int> SaveChangesAsync();

		int SaveChanges();

		void Delete<TEntity>(TEntity entityToDelete) where TEntity : class;

		void DeleteRange<TEntity>(params TEntity[] entitiesToDelete) where TEntity : class;

		Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class;
	}
}
