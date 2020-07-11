using Ghoul.Persistence.Model.Interface;

namespace Ghoul.Persistence.Repository.Interface {
    public interface IMongoRepository<T>
        where T: class, IPrimaryKeyEntity
    {
        
    }
}