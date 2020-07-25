using System.Collections.Generic;
using System.Linq;
using Aio.Domain.Entity.Build;
using Aio.Domain.Entity.Build.Containers;

namespace Aio.Domain.Repository.Interface.Build
{
    public interface IBuildRepository
    {
        bool BuildExistsByName(string name);
        bool BuildExistsByID(string name);
        BuildRunContainer GetNextIdleRun();
        IEnumerable<BuildDomainEntity> GetAllBuilds();
        int GetHighestBuildOrder();
    }
}