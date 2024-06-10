using BloodGL.Core.Database.Entity;
using BloodGL.Core.Database.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace BloodGL.Core.Database.Repository
{
	public class RepositoryBase<T> :IBaseRepository<T> where T : BaseEntity
	{
		private readonly IMongoCollection<T> collection;

		public RepositoryBase(IOptions<MongoDbOptions> options)
		{
			string collecitonName = typeof(T).Name;
			var client = new MongoClient(options.Value.ConnectionString);
			var database = client.GetDatabase(options.Value.DatabaseName);
			collection = CreateOrGetCollection(database, collecitonName);
		}

		private IMongoCollection<T> CreateOrGetCollection(IMongoDatabase database, string collectionName)
		{
			var filter = new BsonDocument("name", collectionName);
			var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });

			var doesCollectionExist = collections.Any();

			if (!doesCollectionExist)
			{
				database.CreateCollection(collectionName);
			}

			return database.GetCollection<T>(collectionName);
		}

		public async Task Add(T entity)
		{
			entity.CreatedAt=DateTime.Now;
			await collection.InsertOneAsync(entity);
		}

		public async Task Delete(T entity)
		{
			entity.DeletedAt = DateTime.Now;
			await collection.DeleteOneAsync<T>(doc => doc.Id == entity.Id);
		}

		public async Task Delete(Expression<Func<T, bool>> where)
		{
			await collection.DeleteOneAsync<T>(where);
		}

		public async Task<T> Get(Expression<Func<T, bool>> where)
		{
			var doc = await collection.FindAsync<T>(where);

			return await doc.FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> GetAll()
		{
			var doc = await collection.Find(new BsonDocument()).ToListAsync();

			return doc;
		}

		public async Task<T> GetById(string id)
		{
			var doc = await collection.FindAsync(doc => doc.Id == id);
			return await doc.FirstOrDefaultAsync();
		}

		public async Task<IReadOnlyList<T>> GetMany(Expression<Func<T, bool>> where)
		{
			var doc = collection.Find<T>(where).SortByDescending(x=>x.CreatedAt);
		
			return await doc.ToListAsync();
		}

		public async Task Update(T entity)
		{
			////await collection.UpdateOneAsync(doc=>doc.Id==entity.Id,entity);
			//FilterDefinition<T> filter = Builders<T>.Filter.Eq(x=>x.Id, entity.Id);
			//UpdateDefinition<T> update = Builders<T>.Update.
			//await collection.UpdateOneAsync(filter, update);
			//return;
			throw new NotImplementedException();

		}

		public async Task UpdateMany(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}

	}
}
