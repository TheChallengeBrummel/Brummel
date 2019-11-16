using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using TheChallenge.Backend.DataAccess.Model;

namespace TheChallenge.Backend.DataAccess
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        private readonly string collectionName;
        private readonly IMongoDatabase mongoDatabase;

        public BaseRepository(string collectionName, DatabaseContext dbContext)
        {
            this.collectionName = collectionName;
            this.mongoDatabase = dbContext.Database;
        }

        private IMongoCollection<T> Documents => mongoDatabase.GetCollection<T>(collectionName);

        public async Task<List<T>> GetAll(CancellationToken cancellationToken = default)
        {
            var result = await Documents.FindAsync(_ => true);
            return await result.ToListAsync(cancellationToken);
        }

        public async Task<Guid> Insert(T document, CancellationToken cancellationToken = default)
        {
            var options = new InsertOneOptions();

            if (!document.Id.HasValue)
            {
                document.Id = Guid.NewGuid();
            }

            await Documents.InsertOneAsync(document, options, cancellationToken);

            return document.Id.Value;
        }

        public async Task<T> Get(Guid documentId, CancellationToken cancellationToken = default)
        {
            var result = await Documents.FindAsync(o => o.Id == documentId);
            var foundDocument = await result.FirstOrDefaultAsync(cancellationToken);

            return foundDocument;
        }

        public async Task Update(T replacedObject)
        {
            if (!replacedObject.Id.HasValue)
            {
                throw new NullReferenceException("The object you want to update has no ID.");
            }
            var result = await Documents.ReplaceOneAsync<T>(o => o.Id.Value == replacedObject.Id.Value, replacedObject);
        }
    }
}