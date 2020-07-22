using System;
using Aio.Persistence.Model.Interface;

namespace Aio.Persistence.Repository.Interface {
    public interface IRepository<T> : IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(string ID);
    }
}