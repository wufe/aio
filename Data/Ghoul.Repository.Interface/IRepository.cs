using System;
using Ghoul.Data.Interface;

namespace Ghoul.Repository.Interface {
    public interface IRepository<T> : IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Guid ID);
    }
}