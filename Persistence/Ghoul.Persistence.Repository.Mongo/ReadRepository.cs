using System;
using System.Linq;
using System.Linq.Expressions;
using Ghoul.Persistence.Model.Interface;
using Ghoul.Persistence.Repository.Interface;
using Ghoul.Persistence.Database.Mongo;
using MongoDB.Driver;

namespace Ghoul.Persistence.Repository.Mongo
{
    public class ReadRepository<T> : IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        private readonly MongoDBContext _context;

        public ReadRepository(MongoDBContext context)
        {
            _context = context;
        }

        public T Find(string ID) =>
            _context.GetCollection<T>()
                .AsQueryable()
                .FirstOrDefault(x => x.ID == ID);

        public IQueryable<T> FindAll(Expression<Func<T, bool>> selector) =>
            _context.GetCollection<T>()
                .AsQueryable()
                .Where(selector);

        public IQueryable<T> FindAll() =>
            _context.GetCollection<T>()
                .AsQueryable();
    }
}
