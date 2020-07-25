using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aio.Domain.Entity;
using Aio.Domain.Entity.Build;
using Aio.Domain.Entity.Build.Containers;

namespace Aio.Domain.Service.Interface {
    public interface IBuildDomainService {
        BuildDomainEntity CreateBuild(string name);
        BuildDomainEntity AppendNewStepToBuild(BuildDomainEntity build, string stepName);
        BuildRunContainer GetNextIdleRun();
        RunDomainEntity CreateRun(string buildID);
        BuildRunContainer StartBuildRun(BuildDomainEntity build, RunDomainEntity run);
        BuildRunContainer StopBuildRun(BuildDomainEntity build, RunDomainEntity run);
        IEnumerable<BuildDomainEntity> UpdateBuildsOrder(int startIndex, int endIndex);
    }
}