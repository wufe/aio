using Aio.Persistence.Model.Interface;

namespace Aio.Persistence.Repository.Interface {
    public interface IMongoRepository<T>
        where T: class, IPrimaryKeyEntity
    {
        
    }
}