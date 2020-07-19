using Ghoul.Domain.Entity;
using Ghoul.Domain.Entity.Build;
using Ghoul.Domain.Entity.Build.Containers;

namespace Ghoul.Domain.Service.Interface {
    public interface IBuildDomainService {
        BuildDomainEntity CreateBuild(string name);
        BuildDomainEntity AppendNewStepToBuild(BuildDomainEntity build, string stepName);
        BuildRunContainer GetNextIdleRun();
        RunDomainEntity CreateRun(string buildID);
        BuildRunContainer StartBuildRun(BuildDomainEntity build, RunDomainEntity run);
        BuildRunContainer StopBuildRun(BuildDomainEntity build, RunDomainEntity run);
    }
}