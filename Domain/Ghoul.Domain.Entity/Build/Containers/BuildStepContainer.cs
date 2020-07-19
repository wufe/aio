namespace Ghoul.Domain.Entity.Build.Containers {
    public class BuildStepContainer {
        public BuildStepDomainEntity Step { get; private set; }
        public int Index { get; private set; }

        public BuildStepContainer(BuildStepDomainEntity step, int index)
        {
            Step = step;
            Index = index;
        }
    }
}