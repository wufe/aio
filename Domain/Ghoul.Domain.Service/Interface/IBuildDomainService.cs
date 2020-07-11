using Ghoul.Domain.Entity;

namespace Ghoul.Domain.Service.Interface {
    public interface IBuildDomainService {
        BuildDomainEntity CreateBuild(string name);
    }
}