using Ghoul.Data.Interface;

namespace Ghoul.Repository.Interface {
    public interface IMongoRepository<T>
        where T: class, IPrimaryKeyEntity
    {
        
    }
}