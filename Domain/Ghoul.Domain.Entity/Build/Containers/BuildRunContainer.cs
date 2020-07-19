namespace Ghoul.Domain.Entity.Build.Containers {
    public class BuildRunContainer {
        public BuildDomainEntity Build { get; private set; }
        public RunDomainEntity Run { get; private set; }

        public BuildRunContainer(BuildDomainEntity build, RunDomainEntity run)
        {
            Build = build;
            Run = run;
        }
    }
}