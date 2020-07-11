using System;
using System.Linq;
using System.Linq.Expressions;
using Ghoul.Persistence.Model.Interface;

namespace Ghoul.Persistence.Repository.Interface
{
    public interface IReadRepository<T>
        where T: IPrimaryKeyEntity
    {
        T Find(string ID);
        IQueryable<T> FindAll(Expression<Func<T, bool>> selector);
        IQueryable<T> FindAll();
    }
}
