using System;
using Ghoul.Persistence.Model.Interface;

namespace Ghoul.Persistence.Repository.Interface {
    public interface IRepository<T> : IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(Guid ID);
    }
}