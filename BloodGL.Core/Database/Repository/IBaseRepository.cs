using BloodGL.Core.Database.Entity;
using System.Linq.Expressions;

namespace BloodGL.Core.Database.Repository
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		Task Add(T entity);
		Task Update(T entity);
		Task UpdateMany(IEnumerable<T> entities);
		Task Delete(T entity);
		Task Delete(Expression<Func<T, bool>> where);
		Task<T> GetById(string id);
		Task<T> Get(Expression<Func<T, bool>> where);
		Task<IReadOnlyList<T>> GetAll();
		Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> where);
	}
}
