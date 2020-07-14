using Ghoul.Domain.Entity;
using Ghoul.Domain.Entity.Build;

namespace Ghoul.Domain.Service.Interface {
    public interface IBuildDomainService {
        BuildDomainEntity CreateBuild(string name);
    }
}