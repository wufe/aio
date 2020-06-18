using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ghoul.Data.Interface;
using Ghoul.Repository.Interface;
using Ghould.Database.Mongo;
using MongoDB.Driver;

namespace Ghoul.Repository.Mongo
{
    public class ReadRepository<T> : IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        private readonly MongoDBContext _context;

        public ReadRepository(MongoDBContext context)
        {
            _context = context;
        }

        public T Find(Guid ID) =>
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
